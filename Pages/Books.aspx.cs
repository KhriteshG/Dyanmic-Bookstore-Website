using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace Assignment_Web_Application.Pages
{
    public partial class Books : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;
        private const int PageSize = 12;

        private int PageIndex
        {
            get => ViewState["PageIndex"] == null ? 0 : (int)ViewState["PageIndex"];
            set => ViewState["PageIndex"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGenres();
                // prefill from query
                txtSearch.Text = Request.QueryString["search"] ?? string.Empty;
                var genreId = Request.QueryString["genreId"];
                if (!string.IsNullOrEmpty(genreId)) ddlGenre.SelectedValue = genreId;

                BindBooks();
            }
        }

        private void BindGenres()
        {
            ddlGenre.Items.Clear();
            ddlGenre.Items.Add(new System.Web.UI.WebControls.ListItem("All Genres", "0"));
            using (var con = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"SELECT GenreID, Name FROM Genres WHERE Status='Active' ORDER BY Name", con))
            {
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ddlGenre.Items.Add(new System.Web.UI.WebControls.ListItem(
                            rdr["Name"].ToString(),
                            rdr["GenreID"].ToString()
                        ));
                    }
                }
            }
        }

        private string BuildWhereClause(SqlCommand cmd)
        {
            var sb = new StringBuilder(" WHERE b.Status='Active' ");
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                sb.Append(@" AND (
                        b.Title LIKE @q
                        OR b.ISBN LIKE @q
                        OR a.FirstName + ' ' + a.LastName LIKE @q
                    )");
                cmd.Parameters.AddWithValue("@q", "%" + txtSearch.Text.Trim() + "%");
            }
            if (ddlGenre.SelectedValue != "0")
            {
                sb.Append(" AND b.GenreID=@gid ");
                cmd.Parameters.AddWithValue("@gid", ddlGenre.SelectedValue);
            }
            return sb.ToString();
        }

        private string BuildOrderBy()
        {
            switch (ddlSort.SelectedValue)
            {
                case "plh": return " ORDER BY b.Price ASC ";
                case "phl": return " ORDER BY b.Price DESC ";
                case "title": return " ORDER BY b.Title ASC ";
                default: return " ORDER BY b.DateAddedTimeStamp DESC ";
            }
        }

        private void BindBooks()
        {
            int total = 0;
            using (var con = new SqlConnection(ConnStr))
            {
                con.Open();

                // COUNT
                using (var countCmd = new SqlCommand(@"
                    SELECT COUNT(*) 
                    FROM Books b
                    LEFT JOIN Authors a ON a.AuthorID = b.AuthorID" + BuildWhereClause(new SqlCommand()), con))
                {
                    // Need same parameters
                    countCmd.Parameters.Clear();
                }

                // Build count separately to keep params aligned
                using (var countCmd = new SqlCommand())
                {
                    countCmd.Connection = con;
                    countCmd.CommandText = "SELECT COUNT(*) FROM Books b LEFT JOIN Authors a ON a.AuthorID=b.AuthorID";
                    var dummy = new SqlCommand(); // to collect params
                    string where = BuildWhereClause(dummy);
                    countCmd.CommandText += where;
                    foreach (SqlParameter p in dummy.Parameters) countCmd.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));

                    total = Convert.ToInt32(countCmd.ExecuteScalar());
                }

                // DATA
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"
                        WITH Paged AS (
                          SELECT 
                            b.BookID, b.Title, b.Price, b.CoverImage,
                            ROW_NUMBER() OVER (" + BuildOrderBy() + @") AS rn
                          FROM Books b
                          LEFT JOIN Authors a ON a.AuthorID = b.AuthorID
                          " + BuildWhereClause(cmd) + @"
                        )
                        SELECT BookID, Title, Price, CoverImage
                        FROM Paged
                        WHERE rn BETWEEN @start AND @end;";
                    int start = PageIndex * PageSize + 1;
                    int end = (PageIndex + 1) * PageSize;
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        rptBooks.DataSource = rdr;
                        rptBooks.DataBind();
                    }
                }
            }

            int showingFrom = total == 0 ? 0 : PageIndex * PageSize + 1;
            int showingTo = Math.Min((PageIndex + 1) * PageSize, total);
            lblResults.Text = $"{showingFrom}-{showingTo} of {total} results";
            btnPrev.Enabled = PageIndex > 0;
            btnNext.Enabled = (PageIndex + 1) * PageSize < total;
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            BindBooks();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (PageIndex > 0) PageIndex--;
            BindBooks();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            PageIndex++;
            BindBooks();
        }
    }
}
