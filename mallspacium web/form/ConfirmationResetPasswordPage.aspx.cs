using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ConfirmationResetPasswordPage : System.Web.UI.Page
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
                validateConfirmationCode();
            }
        }

        // Validate the confirmation code received from gmail account
        private async void validateConfirmationCode()
        {
            // Define the document reference and field name
            DocumentReference docRef = db.Collection("Users").Document((string)Application.Get("emailGet"));
            string resetCode = "resetCode";
            // Get the field value from Firestore
            DocumentSnapshot docSnapshot = await docRef.GetSnapshotAsync();
            string fieldValue = docSnapshot.GetValue<string>(resetCode);
            // Store the field value in a local variable
            string localConfirmCode = fieldValue;

            if (ConfirmationCodeTextBox.Text != localConfirmCode)
            {
                ErrorConfirmationCodeLabel.Text = "Provided confirmation code is invalid!";
            }
            else
            {
                ErrorConfirmationCodeLabel.Text = "";
                Response.Redirect("~/form/ResetPasswordPage.aspx");
            }
        }
    }
}