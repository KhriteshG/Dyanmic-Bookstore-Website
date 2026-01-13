using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Assignment_Web_Application.Pages
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token) || !IsTokenValid(token))
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Invalid or expired password reset link.";
                    btnReset.Enabled = false;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMsg.Text = "Please enter all fields.";
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblMsg.Text = "Passwords do not match.";
                return;
            }

            try
            {
                using (var conn = new SqlConnection(ConnStr))
                {
                    conn.Open();

                    // Get UserID from token
                    using (var cmd = new SqlCommand(@"
                        SELECT UserID 
                        FROM PasswordResetTokens 
                        WHERE ResetToken=@Token AND IsUsed=0 AND ExpiryDate>GETDATE()", conn))
                    {
                        cmd.Parameters.AddWithValue("@Token", token);
                        object userIdObj = cmd.ExecuteScalar();
                        if (userIdObj == null)
                        {
                            lblMsg.Text = "Invalid or expired token.";
                            return;
                        }

                        int userId = Convert.ToInt32(userIdObj);

                        // Update password
                        using (var updateCmd = new SqlCommand(@"
                            UPDATE Users 
                            SET Password=@Password 
                            WHERE UserID=@UserID", conn))
                        {
                            // TODO: In production, hash the password before storing
                            updateCmd.Parameters.AddWithValue("@Password", newPassword);
                            updateCmd.Parameters.AddWithValue("@UserID", userId);
                            updateCmd.ExecuteNonQuery();
                        }

                        // Mark token as used
                        using (var tokenCmd = new SqlCommand(@"
                            UPDATE PasswordResetTokens 
                            SET IsUsed=1 
                            WHERE ResetToken=@Token", conn))
                        {
                            tokenCmd.Parameters.AddWithValue("@Token", token);
                            tokenCmd.ExecuteNonQuery();
                        }

                        lblMsg.CssClass = "text-success";
                        lblMsg.Text = "Your password has been reset. <a href='/Pages/Login.aspx'>Login now</a>.";
                        btnReset.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
            }
        }

        private bool IsTokenValid(string token)
        {
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                SELECT COUNT(*) 
                FROM PasswordResetTokens 
                WHERE ResetToken=@Token AND IsUsed=0 AND ExpiryDate>GETDATE()", conn))
            {
                cmd.Parameters.AddWithValue("@Token", token);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}

