using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Assignment_Web_Application.Pages
{
    public partial class Wishlist : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("/Pages/Login.aspx?returnUrl=" + Server.UrlEncode("/Pages/Wishlist.aspx"));
                return;
            }
            if (!IsPostBack) BindWishlist();
        }

        // Model for repeater binding
        public class WishlistItem
        {
            public int WishlistItemID { get; set; }
            public int BookID { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
            public string CoverImageBase64 { get; set; }
        }

        private void BindWishlist()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            var items = new List<WishlistItem>();

            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT wi.WishlistItemID, b.BookID, b.Title, b.Price, b.CoverImage
                FROM Wishlist w
                INNER JOIN WishlistItems wi ON w.WishlistID = wi.WishlistID
                INNER JOIN Books b ON wi.BookID = b.BookID
                WHERE w.UserID=@u AND w.Status='Active'
                ORDER BY b.Title", con))
            {
                cmd.Parameters.AddWithValue("@u", userId);
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string coverImageBase64 = "/Images/no-cover.png"; // default
                        if (!reader.IsDBNull(reader.GetOrdinal("CoverImage")))
                        {
                            object coverObj = reader["CoverImage"];
                            if (coverObj is byte[] imgBytes)
                                coverImageBase64 = "data:image/png;base64," + Convert.ToBase64String(imgBytes);
                            else if (coverObj is string path)
                                coverImageBase64 = path;
                        }

                        items.Add(new WishlistItem
                        {
                            WishlistItemID = reader.GetInt32(reader.GetOrdinal("WishlistItemID")),
                            BookID = reader.GetInt32(reader.GetOrdinal("BookID")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            CoverImageBase64 = coverImageBase64
                        });
                    }
                }
            }

            rptWishlist.DataSource = items;
            rptWishlist.DataBind();
            pnlEmpty.Visible = items.Count == 0;
        }

        protected void rptWishlist_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                int wishlistItemId = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(Session["UserID"]);

                using (var con = new SqlConnection(ConnStr))
                using (var cmd = new SqlCommand(@"
                    DELETE FROM WishlistItems 
                    WHERE WishlistItemID=@id AND WishlistID IN 
                        (SELECT WishlistID FROM Wishlist WHERE UserID=@u)", con))
                {
                    cmd.Parameters.AddWithValue("@id", wishlistItemId);
                    cmd.Parameters.AddWithValue("@u", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                BindWishlist();

                // update badge on master page
                if (Page.Master is Site1 master)
                {
                    master.SetWishlistCount();
                }
            }
        }
    }
}






