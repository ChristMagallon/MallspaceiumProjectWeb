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
            validateInput();
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

        public async void validateInput()
        {
            Boolean checker = true;
            // Define the regular expression pattern
            /*string pattern = @"^\+63\s(9\d{2})\s\d{3}\s\d{4}$";*/

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            Query query = usersRef.WhereEqualTo("email", EmailTextBox.Text);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Iterate over the results to find the user
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // Do something with the user document
                    Response.Write("<script>alert('Email is already registered!');</script>");
                    checker = false;
                }
            }

            /*if (Convert.ToInt32(PhoneNumberTextBox.Text) > 11)
            {
                Response.Write("<script>alert('Invalid phone number!');</script>");
                checker = false;
            }*/
            /*if (PhoneNumberTextBox.Text != pattern)
            {
                Response.Write("<script>alert('Invalid phone number!');</script>");
                checker = false;
            }*/
            if (checker == true)
            {
                signupUser();
            }
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