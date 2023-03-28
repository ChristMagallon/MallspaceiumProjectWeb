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
    public partial class EditProfilePicturePage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            retrieveShopName();
        }

        public async void retrieveShopName()
        {
            if (!IsPostBack)
            {
                // Fetch the product details from Firestore database
                Query query = database.Collection("Users").WhereEqualTo("email",(string)Application.Get("usernameget"));
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

                    // Display the shop name on the page
                    nameLabel.Text = documentSnapshot.GetValue<string>("shopName");

                    // You can also use the product details to display an image uploader or perform other operations on the product
                }
            }
        }

        protected async  void saveButton_Click(object sender, EventArgs e)
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

                // Update the file in Firestore
                DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget"));
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    //{ "ContentType", contentType },
                    { "shopImage", base64String }
                };
                //await FirestoreDb.CreateOrUpdateDocumentAsync(collectionPath, documentId, data);
                await docRef.SetAsync(data, SetOptions.MergeAll);

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Changed the Profile Picture!');", true);

                // Redirect to another page after a delay
                string url = "EditProfilePage.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }
    }
}