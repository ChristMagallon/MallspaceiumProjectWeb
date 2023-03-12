using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ShopperRegisterPage : System.Web.UI.Page
    {
        FirestoreDb db;
        private static String user_role = "Shopper";

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            signupUser();
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            clearInputs();
        }

        public async void signupUser()
        {
        String email = EmailTextBox.Text;

        // Create a new collection reference
        DocumentReference documentRef = db.Collection("Users").Document(email);

            // Set the data for the new document
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"firstName", FirstNameTextBox.Text},
                {"lastName", LastNameTextBox.Text},
                {"dob", DOBTextBox.Text},
                {"gender", GenderDropDownList.SelectedItem.Text},
                {"phoneNumber", PhoneNumberTextBox.Text},
                {"address", AddressTextBox.Text},
                {"email", EmailTextBox.Text},
                {"username", UsernameTextBox.Text},
                {"password", PasswordTextBox.Text},
                {"confirmPassword", ConfirmPasswordTextBox.Text},
                {"userRole", user_role}
            };

            // Set the data in the Firestore document
            await documentRef.SetAsync(data);
            collectionNotif();

            // Message Box
            Response.Write("<script>alert('Successfully registered');</script>");
            clearInputs();
        }

        public async void collectionNotif()
        {
            String email = EmailTextBox.Text;

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("Users").Document(email).Collection("Notifcation").Document("notif");

            // Set the data for the new document
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"notifyUser", "null"}
            };

            // Set the data in the Firestore document
            await documentRef.SetAsync(data);
        }

        // Clear all the data inputted
        public void clearInputs()
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            DOBTextBox.Text = "";
            GenderDropDownList.SelectedIndex = -1;
            PhoneNumberTextBox.Text = "";
            AddressTextBox.Text = "";
            EmailTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
            ConfirmPasswordTextBox.Text = "";
        }
    }
}