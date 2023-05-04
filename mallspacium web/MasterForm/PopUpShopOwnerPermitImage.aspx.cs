using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class PopUpShopOwnerPermitImage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspasceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            retrievePermitImage();
        }

        public async void retrievePermitImage()
        {
            // Retrieve the image ID from the query string
            string email = Request.QueryString["email"];

            // Retrieve the image data from Firestore database
            DocumentReference docRef = database.Collection("ShopOwnerRegistrationApproval").Document(email);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            string image = snapshot.GetValue<string>("permitImage");

            // Convert the image data to a byte array
            byte[] imageBytes = Convert.FromBase64String(image);

            // Set the properties of the MyImage control
            int height = Convert.ToInt32(Request.QueryString["height"]);
            int width = Convert.ToInt32(Request.QueryString["width"]);
            MyImage.Height = height;
            MyImage.Width = width;
            MyImage.Style["max-height"] = "100%";
            MyImage.Style["max-width"] = "100%";

            // Set the image source
            string imageBase64String = Convert.ToBase64String(imageBytes);
            string imageSrc = $"data:image/png;base64,{imageBase64String}";
            MyImage.ImageUrl = imageSrc;
        }
    }
}