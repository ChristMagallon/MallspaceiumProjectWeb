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
    public partial class AddProductPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            AddProduct();
        }

        public async void AddProduct()
        {
            //auto generated unique id
            Guid id = Guid.NewGuid();
            string uniqueId = id.ToString();

            //Create an instance of Bitmap from the uploaded file using the FileUpload control
            Bitmap image = new Bitmap(imageFileUpload.PostedFile.InputStream);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = stream.ToArray();

            //Convert the Bitmap image to a Base64 string
            string base64String = Convert.ToBase64String(bytes);

            DocumentReference doc = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Product").Document(nameTextbox.Text);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "prodId", uniqueId},
                { "prodName", nameTextbox.Text},
                { "prodDesc", descriptionTextbox.Text},
                { "prodPrice", "₱" + priceTextbox.Text},
                { "prodTag", tagTextbox.Text},
                { "prodImage", base64String},
                {"prodShopName", shopNameTextbox.Text }
            };

            if (nameTextBoxValidator.IsValid && descriptionTextboxValidator.IsValid && priceTextboxValidator.IsValid && tagTextboxValidator.IsValid && imageFileUploadValidator.IsValid && shopNameRequiredFieldValidator.IsValid)
            {
                await doc.SetAsync(data1);
                Response.Write("<script>alert('Successfully Added a New Product.');</script>");
            }

            Response.Redirect("~/ShopOwner/OwnShopProductsPage.aspx", false);
        }
    }
}