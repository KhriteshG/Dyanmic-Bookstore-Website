using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Assignment_Web_Application.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Validate required fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ShowMessage("Please fill in all required fields.", "alert-danger");
                return;
            }

            // Check password match
            if (password != confirmPassword)
            {
                ShowMessage("Passwords do not match.", "alert-danger");
                return;
            }

            // Check if email already exists
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email", conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    ShowMessage("Email already registered.", "alert-danger");
                    return;
                }
            }

            // Insert new user
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                INSERT INTO Users 
                (Username, PasswordHash, Email,DateJoined ) 
                VALUES 
                (@Username, @PasswordHash, @Email, GETDATE(),')", conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", HashPassword(password));
                cmd.Parameters.AddWithValue("@Email", email);

            }

            ShowMessage("Registration successful! You can now login.", "alert-success");
            pnlForm.Visible = false;
        }

        private void ShowMessage(string message, string cssClass)
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;
            lblMessage.CssClass = "alert " + cssClass;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

