using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Assignment_Web_Application.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/Pages/Login.aspx?returnUrl=" + Server.UrlEncode("/Pages/Checkout.aspx"));
                return;
            }

            if (!IsPostBack)
            {
                BindSummary();
            }
        }

        private void BindSummary()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT ci.BookID, ci.Quantity, b.Title, b.Price
                FROM Cart c
                INNER JOIN CartItems ci ON c.CartID = ci.CartID
                INNER JOIN Books b ON b.BookID = ci.BookID
                WHERE c.UserID=@u AND c.Status='Active'", con))
            {
                cmd.Parameters.AddWithValue("@u", userId);
                con.Open();

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                rptSummary.DataSource = dt;
                rptSummary.DataBind();

                // Calculate total safely
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    decimal price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0;
                    int qty = row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 0;
                    total += price * qty;
                }

                litOrderTotal.Text = total.ToString("F2");
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            decimal totalAmount = 0;
            decimal.TryParse(litOrderTotal.Text, out totalAmount);

            using (var con = new SqlConnection(ConnStr))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Create Order
                        var insertOrder = new SqlCommand(@"
                            INSERT INTO Orders (UserID, OrderDate, TotalAmount, Status, ShippingAddress, DateAddedTimeStamp, LastModified)
                            OUTPUT INSERTED.OrderID
                            VALUES (@UserID, GETDATE(), @TotalAmount, 'Pending', @ShippingAddress, GETDATE(), GETDATE())", con, tran);

                        insertOrder.Parameters.AddWithValue("@UserID", userId);
                        insertOrder.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        insertOrder.Parameters.AddWithValue("@ShippingAddress", txtAddress.Text.Trim());

                        int orderId = Convert.ToInt32(insertOrder.ExecuteScalar());

                        // 2. Move CartItems -> OrderItems
                        var insertItems = new SqlCommand(@"
                            INSERT INTO OrderItems (OrderID, BookID, Quantity, PriceAtPurchase, DateAddedTimeStamp, LastModified, Status)
                            SELECT @OrderID, ci.BookID, ci.Quantity, b.Price, GETDATE(), GETDATE(), 'Active'
                            FROM Cart c
                            INNER JOIN CartItems ci ON c.CartID = ci.CartID
                            INNER JOIN Books b ON b.BookID = ci.BookID
                            WHERE c.UserID=@UserID AND c.Status='Active'", con, tran);

                        insertItems.Parameters.AddWithValue("@OrderID", orderId);
                        insertItems.Parameters.AddWithValue("@UserID", userId);
                        insertItems.ExecuteNonQuery();

                        // 3. Clear Cart
                        var clearCart = new SqlCommand(@"
                            DELETE ci 
                            FROM CartItems ci
                            INNER JOIN Cart c ON c.CartID = ci.CartID
                            WHERE c.UserID=@UserID AND c.Status='Active';
                            UPDATE Cart SET Status='Completed', LastModified=GETDATE()
                            WHERE UserID=@UserID AND Status='Active'", con, tran);

                        clearCart.Parameters.AddWithValue("@UserID", userId);
                        clearCart.ExecuteNonQuery();

                        tran.Commit();

                        // Redirect to confirmation page
                        Response.Redirect("/Pages/OrderConfirmation.aspx?orderId=" + orderId);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        // Optional: log ex.Message
                        Response.Write("<script>alert('Error placing order. Please try again.');</script>");
                    }
                }
            }
        }
    }
}
