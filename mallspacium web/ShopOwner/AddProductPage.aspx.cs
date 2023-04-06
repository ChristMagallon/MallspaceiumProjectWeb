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

            shopNameTextbox.Enabled = false;
            getShopName();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            AddProduct();
        }

        public async void AddProduct()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string productID = "PROD" + randomIDNumber.ToString();

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
                { "prodShopName", shopNameTextbox.Text },
                { "prodId", productID},
                { "prodName", nameTextbox.Text},
                { "prodDesc", descriptionTextbox.Text},
                { "prodColor", colorTextBox.Text },
                { "prodSize", sizeTextBox.Text },
                { "prodPrice", "₱" + priceTextbox.Text},
                { "prodTag", tagDropDownList.SelectedValue},
                { "prodAvailability", availablityDropDownList.SelectedValue },
                { "prodImage", base64String}
            };

            if (nameTextBoxValidator.IsValid && descriptionTextboxValidator.IsValid && colorRequiredFieldValidator.IsValid && sizeRequiredFieldValidator.IsValid && priceTextboxValidator.IsValid && tagRequiredFieldValidator.IsValid && imageFileUploadValidator.IsValid && shopNameRequiredFieldValidator.IsValid)
            {
                await doc.SetAsync(data1);

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Added a New Product!');", true);

                // Redirect to another page after a delay
                string url = "MyShopProductsPage.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }


        public async void getShopName()
        {
            if (!IsPostBack)
            {
                Query query = database.Collection("Users").WhereEqualTo("email", (string)Application.Get("usernameget"));
                QuerySnapshot snap = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snap.Documents)
                {
                    // Retrieve the data from the document
                    string shopName = documentSnapshot.GetValue<string>("shopName");

                    shopNameTextbox.Text = shopName;
                }
            }
        }
    }
}