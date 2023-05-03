using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.ShopOwner
{
    public partial class AdvertiseProductPaymentSummary : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");

            string merchantPhoneNumber = ConfigurationManager.AppSettings["MerchantPhoneNumber"];
            MerchantPhoneNumberLabel.Text = merchantPhoneNumber;

            retrieveAdvertiseProductData();
        }

        protected void ProceedButton_Click(object sender, EventArgs e)
        {
            if (!AgreementTermsCheckBox.Checked)
            {
                // Display an error message
                Response.Write("<script>alert('Please agree to the terms and conditions.');</script>");
                return;
            }

            // Checkbox is checked, submit the form
            postAdvertisement();
        }

        private void retrieveAdvertiseProductData()
        {
            // Retrieve the advertise product data from the session variable
            AdvertiseProductData advertiseProductData = (AdvertiseProductData)Session["AdvertiseProductData"];
            string userEmail = advertiseProductData.UserEmail;
            UserEmailLabel.Text = userEmail;
        }

        private async void postAdvertisement()
        {
            // Retrieve the advertise product data from the session variable
            AdvertiseProductData advertiseProductData = (AdvertiseProductData)Session["AdvertiseProductData"];

            // Use the advertise product data as needed
            string userEmail = advertiseProductData.UserEmail;
            string firstName = advertiseProductData.FirstName;
            string lastName = advertiseProductData.LastName;
            string userRole = advertiseProductData.UserRole;
            string adsProductID = advertiseProductData.AdsProductID;
            string adsProductName = advertiseProductData.AdsProductName;
            string adsProductDesc = advertiseProductData.AdsProductDesc;
            string adsProductImage = advertiseProductData.AdsProductImage;
            string adsProductShopName = advertiseProductData.AdsProductShopName;
            string adsProductDate = advertiseProductData.AdsProductDate;

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Generate random ID number
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 1000000);
            string advertisementID = "ADS" + randomIDNumber.ToString();

            // Generate transaction number by get the current date and time
            DateTime now = DateTime.UtcNow;
            // Generate a random number between 1000 and 9999
            int randomNumber = random.Next(1000, 10000);
            // Combine the date and random number to create the transaction number
            string transactionID = string.Format("{0:yyyyMMddHHmmss}{1}", now, randomNumber);

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("AdvertisementPaymentApproval").Document(userEmail);

            if (PaymentFileUpload.HasFile)
            {
                //Create an instance of Bitmap from the uploaded file using the FileUpload control
                Bitmap image = new Bitmap(PaymentFileUpload.PostedFile.InputStream);
                MemoryStream stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] bytes = stream.ToArray();

                //Convert the Bitmap image to a Base64 string
                string base64String = Convert.ToBase64String(bytes);

                // Set the data for the new document
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "transactionID", transactionID},
                    { "advertisementID", advertisementID},
                    { "price", AdvertisementPriceLabel.Text},
                    { "paymentFile", base64String},
                    { "userEmail", userEmail},
                    { "firstName", firstName},
                    { "lastName", lastName},
                    { "userRole", userRole},
                    { "adsProductID", adsProductID},
                    { "adsProductName", adsProductName},
                    { "adsProductImage", adsProductImage},
                    { "adsProductDesc", adsProductDesc},
                    { "adsProductShopName", adsProductShopName},
                    { "adsProductDate", adsProductDate}
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(data);

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Please wait for a moment, we are validating your request.');", true);

                // Redirect to another page after a delay
                string url = "AdvertiseProductsPage.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }
    }
}