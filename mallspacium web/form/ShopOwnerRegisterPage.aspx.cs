using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ShopOwnerRegisterPage : System.Web.UI.Page
    {
        FirestoreDb db;
        private static String userRole = "ShopOwner";

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

        public async void validateInput()
        {
            Boolean checker = true;

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

            // Query the Firestore collection for a specific shop name
            CollectionReference usersRef2 = db.Collection("Users");
            Query query2 = usersRef2.WhereEqualTo("shopName", ShopNameTextBox.Text);
            QuerySnapshot snapshot2 = await query2.GetSnapshotAsync();

            // Iterate over the results to find the if the shop name is already taken
            foreach (DocumentSnapshot document2 in snapshot2.Documents)
            {
                if (document2.Exists)
                {
                    // Do something with the registered document
                    ErrorShopNameLabel.Text = "Shop name is already taken!";
                    checker = false;
                }
                else
                {
                    ErrorShopNameLabel.Text = "";
                }
            }

            // Validate a Philippine phone number with no spaces
            bool isValidPhoneNumber = System.Text.RegularExpressions.Regex.IsMatch(PhoneNumberTextBox.Text, @"^\+63\d{10}$");
            if (!isValidPhoneNumber)
            {
                ErrorPhoneNumberLabel.Text = "Invalid phone number!";
                checker = false;
            }
            else
            {
                ErrorPhoneNumberLabel.Text = "";
            }

            // If the input values are valid proceed to complete registration
            if (checker == true)
            {
                signupUser();
            }
        }

        public async void signupUser()
        {
            String email = EmailTextBox.Text;
            String shopImage = "";

            // Generate random ID number
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string userID = "USER" + randomIDNumber.ToString();

            // Get current date time of the account created
            DateTime currentDate = DateTime.Now;
            string dateCreated = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Capitalize first letter of each word in a string
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo ti = cultureInfo.TextInfo;

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("Users").Document(email);

            if (ImageFileUpload.HasFile)
            {
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
                    {"userID", userID},
                    {"firstName", ti.ToTitleCase(FirstNameTextBox.Text)},
                    {"lastName", ti.ToTitleCase(LastNameTextBox.Text)},
                    {"shopImage", shopImage},
                    {"shopName", ShopNameTextBox.Text},
                    {"shopDescription", ti.ToTitleCase(ShopDescriptionTextBox.Text)},
                    {"imageFile", base64String},
                    {"email", EmailTextBox.Text},
                    {"phoneNumber", PhoneNumberTextBox.Text},
                    {"address", ti.ToTitleCase(AddressTextBox.Text)},
                    {"username", UsernameTextBox.Text},
                    {"password", PasswordTextBox.Text},
                    {"confirmPassword", ConfirmPasswordTextBox.Text},
                    {"userRole", userRole},
                    {"dateCreated", dateCreated }
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(data);
                defaultSubscription();

                string loginPageUrl = ResolveUrl("~/form/LoginPage.aspx");
                Response.Write("<script>alert('Successfully Registered'); window.location='" + loginPageUrl + "';</script>");
            }
        }

        // Default subscription
        public async void defaultSubscription()
        {
            String email = EmailTextBox.Text;
            String subscriptionType = "Free";
            String subscriptionPrice = "0.00";
            String currentDate = "n/a";
            String expirationDate = "n/a";
            String status = "Active";

            // Generate random ID number
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string subscriptionID = "SUB" + randomIDNumber.ToString();
            
            // Create a new collection reference
            DocumentReference documentRef = db.Collection("AdminManageSubscription").Document(email);

            // Set the data for the new document
            Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionID},
                    {"subscriptionType", subscriptionType},
                    {"price", subscriptionPrice},
                    {"userEmail", email},
                    {"userRole", userRole},
                    {"startDate", currentDate},
                    {"endDate", expirationDate},
                    {"status", status}
                };
            // Set the data in the Firestore document
            await documentRef.SetAsync(dataInsert);
        }
    }
}