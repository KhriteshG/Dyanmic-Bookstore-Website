using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace Assignment_Web_Application.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Fix for WebForms UnobtrusiveValidationMode
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                new ScriptResourceDefinition
                {
                    Path = "https://code.jquery.com/jquery-3.7.1.min.js",
                    DebugPath = "https://code.jquery.com/jquery-3.7.1.min.js",
                    CdnSupportsSecureConnection = true
                });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserID"] = null;
                Session["Username"] = null;
                Session["Role"] = null;

                // Load remembered username if cookie exists
                if (Request.Cookies["BookSmart_User"] != null)
                {
                    txtUser.Text = Request.Cookies["BookSmart_User"].Value;
                    chkRemember.Checked = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string userInput = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            try
            {
                using (var con = new SqlConnection(ConnStr))
                using (var cmd = new SqlCommand(@"
                    SELECT UserID, Username
                    FROM Users
                    WHERE Status='Active'
                      AND (Username=@input OR Email=@input)
                      AND PasswordHash=@pass", con))
                {
                    cmd.Parameters.AddWithValue("@input", userInput);
                    cmd.Parameters.AddWithValue("@pass", password); // Hash in production

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Set session
                        Session["UserID"] = reader["UserID"];
                        Session["Username"] = reader["Username"];

                        // Handle "Remember Me"
                        if (chkRemember.Checked)
                        {
                            HttpCookie cookie = new HttpCookie("BookSmart_User", userInput)
                            {
                                Expires = DateTime.Now.AddDays(30)
                            };
                            Response.Cookies.Add(cookie);
                        }
                        else if (Request.Cookies["BookSmart_User"] != null)
                        {
                            var cookie = new HttpCookie("BookSmart_User") { Expires = DateTime.Now.AddDays(-1) };
                            Response.Cookies.Add(cookie);
                        }

                        // Update master page badges
                        if (Master is Site1 m)
                        {
                            m.SetCartCount();
                            m.SetWishlistCount();
                        }

                        Response.Redirect("/Pages/Home.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "Invalid username/email or password.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = $"Login failed: {ex.Message}";
            }
        }
    }
}

