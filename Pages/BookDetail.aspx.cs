using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Assignment_Web_Application.Pages
{
    public partial class BookDetails : System.Web.UI.Page
    {
        private int BookID => int.TryParse(Request.QueryString["id"], out var id) ? id : 0;
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadBook();
        }

        private void LoadBook()
        {
            if (BookID <= 0) { pnlNotFound.Visible = true; return; }

            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT b.BookID, b.Title, b.Description, b.Price, b.CoverImage, b.ISBN,
                       g.Name AS GenreName, a.FirstName, a.LastName
                FROM Books b
                LEFT JOIN Genres g ON g.GenreID=b.GenreID
                LEFT JOIN Authors a ON a.AuthorID=b.AuthorID
                WHERE b.BookID=@id AND b.Status='Active'", con))
            {
                cmd.Parameters.AddWithValue("@id", BookID);
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) { pnlNotFound.Visible = true; return; }

                    pnlDetails.Visible = true;
                    imgCover.ImageUrl = rdr["CoverImage"]?.ToString();
                    litTitle.Text = rdr["Title"]?.ToString();
                    litPrice.Text = Convert.ToDecimal(rdr["Price"]).ToString("F2");
                    litISBN.Text = rdr["ISBN"]?.ToString();
                    litGenre.Text = rdr["GenreName"]?.ToString();
                    litAuthor.Text = (rdr["FirstName"] + " " + rdr["LastName"]).Trim();
                    litDescription.Text = rdr["Description"]?.ToString();
                }
            }

            BindReviews();
            pnlReviewForm.Visible = Session["UserID"] != null;
            lblLoginToReview.Visible = Session["UserID"] == null;
        }

        private void BindReviews()
        {
            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT Rating, Comment, DateAddedTimeStamp
                FROM Reviews
                WHERE BookID=@id AND Status='Active'
                ORDER BY DateAddedTimeStamp DESC", con))
            {
                cmd.Parameters.AddWithValue("@id", BookID);
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    rptReviews.DataSource = rdr;
                    rptReviews.DataBind();
                    lblNoReviews.Visible = !rdr.HasRows;
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblAddMsg.CssClass = "text-danger";
                lblAddMsg.Text = "Please log in to add items to your cart.";
                return;
            }

            int qty = 1;
            int.TryParse(txtQty.Text, out qty);
            qty = Math.Max(1, qty);

            int userId = Convert.ToInt32(Session["UserID"]);

            using (var con = new SqlConnection(ConnStr))
            {
                con.Open();
                int cartId;

                // Ensure one cart per user
                using (var cmdGetCart = new SqlCommand(@"
                    IF EXISTS (SELECT 1 FROM Cart WHERE UserID=@u)
                        SELECT CartID FROM Cart WHERE UserID=@u
                    ELSE
                        INSERT INTO Cart(UserID) OUTPUT inserted.CartID VALUES(@u)
                ", con))
                {
                    cmdGetCart.Parameters.AddWithValue("@u", userId);
                    cartId = Convert.ToInt32(cmdGetCart.ExecuteScalar());
                }

                // Upsert CartItems
                using (var cmdItem = new SqlCommand(@"
                    IF EXISTS (SELECT 1 FROM CartItems WHERE CartID=@c AND BookID=@b)
                        UPDATE CartItems SET Quantity = Quantity + @q, LastModified=GETDATE()
                        WHERE CartID=@c AND BookID=@b
                    ELSE
                        INSERT INTO CartItems(CartID, BookID, Quantity) VALUES(@c, @b, @q)
                ", con))
                {
                    cmdItem.Parameters.AddWithValue("@c", cartId);
                    cmdItem.Parameters.AddWithValue("@b", BookID);
                    cmdItem.Parameters.AddWithValue("@q", qty);
                    cmdItem.ExecuteNonQuery();
                }
            }

            // Optional: update Session cart count quickly
            Session["CartCount"] = (Session["CartCount"] == null ? 0 : Convert.ToInt32(Session["CartCount"])) + 1;

            lblAddMsg.CssClass = "text-success";
            lblAddMsg.Text = "Added to cart!";
        }

        protected void btnAddToWishlist_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblWishlistMsg.CssClass = "text-danger";
                lblWishlistMsg.Text = "Please log in to add to wishlist.";
                return;
            }

            var btn = (LinkButton)sender;
            int bookId = Convert.ToInt32(btn.CommandArgument);
            int userId = Convert.ToInt32(Session["UserID"]);

            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM Wishlist WHERE UserID=@uid AND BookID=@bid)
                    INSERT INTO Wishlist(UserID, BookID, DateAdded)
                    VALUES(@uid, @bid, GETDATE())
            ", con))
            {
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@bid", bookId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblWishlistMsg.CssClass = "text-success";
            lblWishlistMsg.Text = "Book added to wishlist!";
        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblReviewMsg.CssClass = "text-danger";
                lblReviewMsg.Text = "Please log in.";
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            int rating = int.Parse(ddlRating.SelectedValue);
            string comment = (txtComment.Text ?? "").Trim();

            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                BEGIN TRY
                    INSERT INTO Reviews(BookID, UserID, Rating, Comment) 
                    VALUES(@b, @u, @r, @c);
                    SELECT 1;
                END TRY
                BEGIN CATCH
                    SELECT -1;
                END CATCH
            ", con))
            {
                cmd.Parameters.AddWithValue("@b", BookID);
                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@r", rating);
                cmd.Parameters.AddWithValue("@c", string.IsNullOrEmpty(comment) ? DBNull.Value : (object)comment);

                con.Open();
                var result = Convert.ToInt32(cmd.ExecuteScalar());

                if (result == -1)
                {
                    lblReviewMsg.CssClass = "text-danger";
                    lblReviewMsg.Text = "You already reviewed this book.";
                }
                else
                {
                    lblReviewMsg.CssClass = "text-success";
                    lblReviewMsg.Text = "Thank you for your review!";
                    txtComment.Text = string.Empty;
                    BindReviews();
                }
            }
        }
    }
}

