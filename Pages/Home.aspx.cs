using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Assignment_Web_Application.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private string connStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFeaturedBooks();
                LoadStaffPicks();
            }
        }

        private void LoadFeaturedBooks()
        {
            string query = @"
                SELECT TOP 8 BookID, Title, Price, CoverImage
                FROM Books
                WHERE Status='Active'
                ORDER BY DateAddedTimeStamp DESC";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                rptFeaturedBooks.DataSource = cmd.ExecuteReader();
                rptFeaturedBooks.DataBind();
            }
        }

        private void LoadStaffPicks()
        {
            string query = @"
                SELECT TOP 4 B.BookID, B.Title, B.Price, B.CoverImage
                FROM Books B
                JOIN Reviews R ON B.BookID = R.BookID
                WHERE R.Rating >= 4 AND B.Status='Active'
                GROUP BY B.BookID, B.Title, B.Price, B.CoverImage
                ORDER BY NEWID()";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                rptStaffPicks.DataSource = cmd.ExecuteReader();
                rptStaffPicks.DataBind();
            }
        }
    }
}


