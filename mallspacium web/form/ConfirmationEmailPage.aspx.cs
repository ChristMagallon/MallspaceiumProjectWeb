using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ConfirmationEmailPage : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {
            getUserDetails();
        }

        private async void getUserDetails()
        {
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            Query query = usersRef.WhereEqualTo("email", (string)Application.Get("usernameget"));
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Check if the snapshot is empty
            if (snapshot.Count == 0)
            {
                // Handle the case where the snapshot is empty
            }
            else
            {
                verifyEmail();
            }
        }

        // Landing page for verification code
        public async void verifyEmail()
        {
            // Define the document reference and field name
            DocumentReference docRef = db.Collection("Users").Document((string)Application.Get("usernameget"));
            string confirmationCode = "confirmationCode";
            // Get the field value from Firestore
            DocumentSnapshot docSnapshot = await docRef.GetSnapshotAsync();
            string fieldValue = docSnapshot.GetValue<string>(confirmationCode);
            // Store the field value in a local variable
            string localConfirmCode = fieldValue;

            if (ConfirmationCodeTextBox.Text != localConfirmCode)
            {
                ErrorConfirmationCodeLabel.Text = "Provided confirmation code is invalid!";
            }
            else
            {
                // Update the Users verification status in Firestore
                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "confirmationCode", FieldValue.Delete },
                    { "verified", true }
                };
                await docRef.UpdateAsync(updates);

                string loginPageUrl = ResolveUrl("~/form/LoginPage.aspx");
                Response.Write("<script>alert('Your account is successfully verified! Please login to access your account!'); window.location='" + loginPageUrl + "';</script>");
            }
        }
    }
}