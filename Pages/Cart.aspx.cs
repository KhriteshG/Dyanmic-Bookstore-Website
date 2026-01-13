using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Assignment_Web_Application.Pages
{
    public partial class Cart : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/Pages/Login.aspx?returnUrl=" + Server.UrlEncode("/Pages/Cart.aspx"));
                return;
            }
            if (!IsPostBack) BindCart();
        }

        private void BindCart()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT ci.BookID, ci.Quantity, b.Title, b.Price, b.ISBN, b.CoverImage
                FROM Cart c
                LEFT JOIN CartItems ci ON c.CartID = ci.CartID
                LEFT JOIN Books b ON b.BookID = ci.BookID
                WHERE c.UserID=@u AND c.Status='Active'
                ORDER BY b.Title", con))
            {
                cmd.Parameters.AddWithValue("@u", userId);
                con.Open();
                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                pnlEmpty.Visible = dt.Rows.Count == 0;
                rptCart.DataSource = dt;
                rptCart.DataBind();

                if (dt.Rows.Count > 0)
                {
                    decimal total = dt.AsEnumerable().Sum(r => r.Field<decimal>("Price") * r.Field<int>("Quantity"));
                    litTotal.Text = total.ToString("F2");
                    pnlSummary.Visible = true;
                }
                else
                {
                    pnlSummary.Visible = false;
                }
            }
        }

        protected void rptCart_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int bookId = Convert.ToInt32(e.CommandArgument);
            int userId = Convert.ToInt32(Session["UserID"]);
            int cartId;

            using (var con = new SqlConnection(ConnStr))
            {
                con.Open();
                using (var getCart = new SqlCommand("SELECT CartID FROM Cart WHERE UserID=@u AND Status='Active'", con))
                {
                    getCart.Parameters.AddWithValue("@u", userId);
                    cartId = Convert.ToInt32(getCart.ExecuteScalar());
                }

                if (e.CommandName == "update")
                {
                    var txtQty = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtQty");
                    int qty = Math.Max(1, Convert.ToInt32(txtQty.Text));
                    using (var cmd = new SqlCommand("UPDATE CartItems SET Quantity=@q, LastModified=GETDATE() WHERE CartID=@c AND BookID=@b", con))
                    {
                        cmd.Parameters.AddWithValue("@q", qty);
                        cmd.Parameters.AddWithValue("@c", cartId);
                        cmd.Parameters.AddWithValue("@b", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (e.CommandName == "remove")
                {
                    using (var cmd = new SqlCommand("DELETE FROM CartItems WHERE CartID=@c AND BookID=@b", con))
                    {
                        cmd.Parameters.AddWithValue("@c", cartId);
                        cmd.Parameters.AddWithValue("@b", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Session["CartCount"] = null;
            BindCart();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pages/Checkout.aspx");
        }
    }
}
