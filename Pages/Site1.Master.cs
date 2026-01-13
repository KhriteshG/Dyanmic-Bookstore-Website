using System;using System.Configuration;using System.Data.SqlClient;namespace Assignment_Web_Application.Pages{    public partial class Site1 : System.Web.UI.MasterPage    {
        // Database connection string
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;        protected void Page_Load(object sender, EventArgs e)        {            if (!IsPostBack)            {                BindGenres();                SetCartCount();                SetWishlistCount();            }        }







        /// <summary>        /// Load all active genres from DB for dropdown menu.        /// </summary>                                                                                                                   private void BindGenres()        {            const string sql = @"                SELECT GenreID, Name                FROM Genres                WHERE Status = 'Active'                ORDER BY Name ASC";            using (var conn = new SqlConnection(ConnStr))            using (var cmd = new SqlCommand(sql, conn))            {                conn.Open();                rptGenres.DataSource = cmd.ExecuteReader();                rptGenres.DataBind();            }        }







        /// <summary>        /// Sets cart count badge.        /// </summary>                                                                                        public void SetCartCount()        {            int count = 0;            if (Session["CartCount"] != null && int.TryParse(Session["CartCount"].ToString(), out var c))                count = Math.Max(0, c);            else if (Session["UserID"] != null)            {                int userId = Convert.ToInt32(Session["UserID"]);                count = GetCartItemsCount(userId);            }            CartCount.InnerText = count.ToString();            CartCount.Visible = count > 0;        }        private int GetCartItemsCount(int userId)        {            const string sql = @"                SELECT ISNULL(SUM(ci.Quantity), 0)                FROM Cart c                LEFT JOIN CartItems ci ON c.CartID = ci.CartID                WHERE c.UserID = @UserID AND c.Status = 'Active'";            using (var conn = new SqlConnection(ConnStr))            using (var cmd = new SqlCommand(sql, conn))            {                cmd.Parameters.AddWithValue("@UserID", userId);                conn.Open();                object result = cmd.ExecuteScalar();                return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);            }        }







        /// <summary>        /// Sets wishlist count badge.        /// </summary>                                                                                            public void SetWishlistCount()        {            int count = 0;            if (Session["WishlistCount"] != null && int.TryParse(Session["WishlistCount"].ToString(), out var c))                count = Math.Max(0, c);            else if (Session["UserID"] != null)            {                int userId = Convert.ToInt32(Session["UserID"]);                count = GetWishlistItemsCount(userId);            }            WishlistCount.InnerText = count.ToString();            WishlistCount.Visible = count > 0;        }        private int GetWishlistItemsCount(int userId)        {            const string sql = @"                SELECT COUNT(*)                FROM Wishlist w                WHERE w.UserID = @UserID AND w.Status = 'Active'";            using (var conn = new SqlConnection(ConnStr))            using (var cmd = new SqlCommand(sql, conn))            {                cmd.Parameters.AddWithValue("@UserID", userId);                conn.Open();                object result = cmd.ExecuteScalar();                return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);            }        }









        /// <summary>        /// Search button click event.        /// Redirects to Books page with query.        /// </summary>                                                                                                                                            protected void btnSearch_Click(object sender, EventArgs e)        {            var q = (txtSearch.Text ?? string.Empty).Trim();            if (!string.IsNullOrEmpty(q))            {                Response.Redirect("/Pages/Books.aspx?search=" + Server.UrlEncode(q));            }            else            {                Response.Redirect("/Pages/Books.aspx");            }        }    }}