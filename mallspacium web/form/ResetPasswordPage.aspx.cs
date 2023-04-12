using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ResetPasswordPage : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }

        protected void ResetPasswordButton_Click1(object sender, EventArgs e)
        {
            getUserDetails();
        }

        private async void getUserDetails()
        {
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            Query query = usersRef.WhereEqualTo("email", (string)Application.Get("emailGet"));
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Check if the snapshot is empty
            if (snapshot.Count == 0)
            {
                // Handle the case where the snapshot is empty
            }
            else
            {
                updatePassword();
            }
        }

        // Update the users password after the confirmation process
        private async void updatePassword()
        {
            // Update the user's password in Firestore
            DocumentReference docRef = db.Collection("Users").Document((string)Application.Get("emailGet"));
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "password", PasswordTextBox.Text },
                { "confirmPassword", ConfirmPasswordTextBox.Text },
                { "resetCode", FieldValue.Delete }
            };
            await docRef.UpdateAsync(updates);

            string loginPageUrl = ResolveUrl("~/form/LoginPage.aspx");
            Response.Write("<script>alert('Password Successfully Reset'); window.location='" + loginPageUrl + "';</script>");
        }
    }
}