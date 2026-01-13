using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Assignment_Web_Application.Pages
{
    public partial class Contact : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["BookSmartDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string subject = txtSubject.Text.Trim();
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
            {
                ShowMessage("Please fill in all fields.", "alert-danger");
                return;
            }

            int? userId = Session["UserID"] != null ? (int?)Convert.ToInt32(Session["UserID"]) : null;

            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"
                INSERT INTO ContactMessages (UserID, Name, Email, Subject, Message, DateSubmitted, Status)
                VALUES (@UserID, @Name, @Email, @Subject, @Message, GETDATE(), 'New')", conn))
            {
                cmd.Parameters.AddWithValue("@UserID", (object)userId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Subject", subject);
                cmd.Parameters.AddWithValue("@Message", message);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            ShowMessage("Your message has been sent successfully!", "alert-success");
            pnlForm.Visible = false;
        }

        private void ShowMessage(string message, string cssClass)
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;
            lblMessage.CssClass = "alert " + cssClass;
        }
    }
}

