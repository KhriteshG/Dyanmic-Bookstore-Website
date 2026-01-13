using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

namespace Assignment_Web_Application.Pages
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSendReset_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                lblMsg.Text = "Please enter your email.";
                return;
            }

            try
            {
                using (var conn = new SqlConnection(ConnStr))
                {
                    conn.Open();

                    // Check if email exists
                    using (var cmd = new SqlCommand("SELECT UserID FROM Users WHERE Email=@Email AND Status='Active'", conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        object userIdObj = cmd.ExecuteScalar();
                        if (userIdObj == null)
                        {
                            lblMsg.Text = "Email not found.";
                            return;
                        }

                        int userId = Convert.ToInt32(userIdObj);

                        // Generate token
                        string token = Guid.NewGuid().ToString();

                        // Insert token into DB (expires in 1 hour)
                        using (var insertCmd = new SqlCommand(@"
                            INSERT INTO PasswordResetTokens (UserID, ResetToken, ExpiryDate)
                            VALUES (@UserID, @Token, DATEADD(hour, 1, GETDATE()))", conn))
                        {
                            insertCmd.Parameters.AddWithValue("@UserID", userId);
                            insertCmd.Parameters.AddWithValue("@Token", token);
                            insertCmd.ExecuteNonQuery();
                        }

                        // Send reset email
                        string resetLink = Request.Url.GetLeftPart(UriPartial.Authority) +
                                           "/Pages/ResetPassword.aspx?token=" + token;

                        SendResetEmail(email, resetLink);

                        lblMsg.CssClass = "text-success";
                        lblMsg.Text = "A password reset link has been sent to your email.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
            }
        }

        private void SendResetEmail(string email, string link)
        {
            // Replace with your SMTP settings
            using (var mail = new MailMessage())
            using (var smtp = new SmtpClient())
            {
                mail.To.Add(email);
                mail.From = new MailAddress("no-reply@booksmart.com", "BookSmart");
                mail.Subject = "Password Reset Request";
                mail.Body = $"Click the link to reset your password: {link}";
                mail.IsBodyHtml = true;

                smtp.Host = "smtp.yourserver.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("username", "password");
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
        }
    }
}

