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
    public partial class ChangeImagePopUp : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            retrieveProductName();
        }

        public async void retrieveProductName()
        {
            if (!IsPostBack)
            {
                // Get the product name from the hidden field
                string prodName = Request.QueryString["prodName"];

                if (!string.IsNullOrEmpty(prodName))
                {
                    // Fetch the product details from Firestore database
                    Query query = database.Collection("Users").Document("ruYerFhJsxLm3ONnMzdc").Collection("Product").WhereEqualTo("prodName", prodName);
                    QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                    if (querySnapshot.Documents.Count > 0)
                    {
                        DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

                        // Display the product name on the page
                        nameLabel.Text = documentSnapshot.GetValue<string>("prodName");

                        // You can also use the product details to display an image uploader or perform other operations on the product
                    }
                }
            }
        }
    

        protected async void saveButton_Click(object sender, EventArgs e)
        {
            if (imageFileUpload.HasFile)
            {
                //Create an instance of Bitmap from the uploaded file using the FileUpload control
                Bitmap image = new Bitmap(imageFileUpload.PostedFile.InputStream);
                MemoryStream stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] bytes = stream.ToArray();

                //Convert the Bitmap image to a Base64 string
                string base64String = Convert.ToBase64String(bytes);


                // Get the document ID from the name label
                string documentId = nameLabel.Text;

                // Update the file in Firestore
                DocumentReference docRef = database.Collection("Users").Document("ruYerFhJsxLm3ONnMzdc").Collection("Product").Document(documentId);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    //{ "ContentType", contentType },
                    { "prodImage", base64String }
                };
                //await FirestoreDb.CreateOrUpdateDocumentAsync(collectionPath, documentId, data);
                await docRef.SetAsync(data, SetOptions.MergeAll);

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Changed the Image!');", true);

                // Redirect to another page after a delay
                string url = "MyShopProductsPage.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 3000);", true);

                // Close the pop-up window
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "closePopupScript", "window.close();", true);
            }
        }
    }
}