using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.ShopOwner
{
    public partial class OwnProductDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            retrieveData();
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            update();
        }

        public async void retrieveData()
        {
            if (!IsPostBack)
            {
                // Retrieve the document ID from the query string
                string prodName = Request.QueryString["prodName"];

                // Use the document ID to retrieve the data from Firestore
                DocumentReference docRef = database.Collection("Users").Document("ruYerFhJsxLm3ONnMzdc").Collection("Product").Document(prodName);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists)
                {
                    // Retrieve the data from the document
                    string id = snapshot.GetValue<string>("prodId");
                    string name = snapshot.GetValue<string>("prodName");
                    string desc = snapshot.GetValue<string>("prodDesc");
                    string price = snapshot.GetValue<string>("prodPrice");
                    string tag = snapshot.GetValue<string>("prodTag");

                    string base64String = snapshot.GetValue<string>("prodImage");
                    //byte[] productImage = Convert.FromBase64String(base64String);

                    // Convert the image string to a byte array
                    byte[] imageBytes = Convert.FromBase64String(base64String);
                    // Set the image in the FileUpload control
                    string imageBase64String = Convert.ToBase64String(imageBytes);
                    string imageSrc = $"data:image/png;base64,{imageBase64String}";
                    Image1.ImageUrl = imageSrc;

                    // Display the data
                    idTextbox.Text = id;
                    nameTextbox.Text = name;
                    descriptionTextbox.Text = desc;
                    priceTextbox.Text = price;
                    tagTextbox.Text = tag;
                }
            }
        }

        public async void update()
        {
            //Create an instance of Bitmap from the uploaded file using the FileUpload control
            Bitmap image = new Bitmap(imageFileUpload.PostedFile.InputStream);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = stream.ToArray();

            //Convert the Bitmap image to a Base64 string
            string base64String = Convert.ToBase64String(bytes);

            DocumentReference docRef = database.Collection("Users").Document("ruYerFhJsxLm3ONnMzdc").Collection("Product").Document(nameTextbox.Text);

            Dictionary<string, object> data = new Dictionary<string, object>
{
                {"prodId", nameTextbox.Text},
                {"prodName", idTextbox.Text},
                {"prodDesc", descriptionTextbox.Text},
                {"prodPrice", priceTextbox.Text},
                {"prodTag", tagTextbox.Text},
                {"prodImage", base64String},
            };

            if (nameTextBoxValidator.IsValid && descriptionTextboxValidator.IsValid && priceTextboxValidator.IsValid && tagTextboxValidator.IsValid)
            {

                try
                {
                    await docRef.SetAsync(data, SetOptions.MergeAll);

                    string message = "Product Successfully Updated";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    Response.Redirect("~/ShopOwner/OwnShopProductsPage.aspx", false);
                }
                catch (Exception ex)
                {
                    string message = "Error Updating Product";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    Response.Redirect("~/ShopOwner/OwnShopProductsPage.aspx", false);
                }
            }
        }
    }
}