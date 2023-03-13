using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ShopOwnerRegisterPage : System.Web.UI.Page
    {
        FirestoreDb db;
        private static String user_role = "ShopOwner";

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

        protected void LoginLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/form/LoginPage.aspx", false);
        }

        public async void signupUser()
        {
            String email = EmailTextBox.Text;

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("Users").Document(email);

            //Create an instance of Bitmap from the uploaded file using the FileUpload control
            Bitmap image = new Bitmap(ImageFileUpload.PostedFile.InputStream);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = stream.ToArray();

            //Convert the Bitmap image to a Base64 string
            string base64String = Convert.ToBase64String(bytes);

            // Set the data for the new document
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"firstName", FirstNameTextBox.Text},
                {"lastName", LastNameTextBox.Text},
                {"shopName", ShopNameTextBox.Text},
                {"shopDescription", ShopDescriptionTextBox.Text},
                {"imageFile", base64String},
                {"email", EmailTextBox.Text},
                {"phoneNumber", PhoneNumberTextBox.Text},
                {"address", AddressTextBox.Text},
                {"password", PasswordTextBox.Text},
                {"confirmPassword", ConfirmPasswordTextBox.Text},
                {"userRole", user_role}
            };

            // Set the data in the Firestore document
            await documentRef.SetAsync(data);
            collectionNotif();

            // Message Box
            Response.Write("<script>alert('Successfully registered');</script>");
            Response.Buffer = true;
            Response.Redirect("~/form/LoginPage.aspx", false);
            clearInputs();
        }

        public async void collectionNotif()
        {
            String email = EmailTextBox.Text;

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("Users").Document(email).Collection("Notification").Document("notif");

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
            ShopNameTextBox.Text = "";
            ShopDescriptionTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            AddressTextBox.Text = "";
            PasswordTextBox.Text = "";
            ConfirmPasswordTextBox.Text = "";
        }
    }
}