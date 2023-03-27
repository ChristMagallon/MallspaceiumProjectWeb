using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ForgotPasswordPage : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            getUserDetails();
        }

        private async void getUserDetails()
        {
            // Check if email address exists in Firestore
            string userEmail = EmailTextBox.Text;
            CollectionReference usersRef = db.Collection("Users");
            QuerySnapshot usersQuery = await usersRef.WhereEqualTo("email", userEmail).GetSnapshotAsync();
            if (usersQuery.Documents.Count == 0)
            {
                // Email address not found
                string script = "alert('It seems like the email you entered doesn't match our records.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                // Generate confirmation code
                string confirmationCode = GenerateCode();

                // Store code in Firestore
                DocumentReference userRef = usersRef.Document(usersQuery.Documents[0].Id);
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "resetCode", confirmationCode }
                };
                await userRef.UpdateAsync(data);

                // Send confirmation email
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("sysadm1n.mallspaceium@gmail.com", "pbssojpapersldtj");
                smtpClient.EnableSsl = true;
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("sysadm1n.mallspaceium@gmail.com");
                mailMessage.To.Add(userEmail);
                mailMessage.Subject = "Password reset confirmation code";
                mailMessage.Body = "Your confirmation code is " + confirmationCode;
                smtpClient.Send(mailMessage);

                // Do something with the user document
                Application.Set("emailGet", EmailTextBox.Text);

                // Alert message
                Response.Redirect("~/form/ConfirmResetPage.aspx", false);
                /*string confirmResetPageUrl = ResolveUrl("~/form/ConfirmResetPage.aspx");
                Response.Write("<script>alert('Check your email and view the confirmation code we've sent for you.'); window.location='" + confirmResetPageUrl + "';</script>");*/
            }
        }

        private string GenerateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return code;
        }
    }
}