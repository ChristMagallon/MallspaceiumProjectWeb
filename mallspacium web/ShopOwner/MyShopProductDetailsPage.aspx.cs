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

            
            idTextbox.Enabled = false;
            nameTextbox.Enabled = false;
            shopNameTextbox.Enabled = false;

            if (!IsPostBack){
                retrieveData();
            }
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
                DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Product").Document(prodName);
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
                    string image = snapshot.GetValue<string>("prodImage");
                    string shop = snapshot.GetValue<string>("prodShopName");
                    // Convert the image string to a byte array
                    byte[] imageBytes = Convert.FromBase64String(image);

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
                    imageHiddenField.Value = image;
                    shopNameTextbox.Text = shop;
                }
            }
        }

        public async void update()
        {
            // Retrieve the document ID from the query string
            string prodName = Request.QueryString["prodName"];

            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Product").Document(prodName);

            Dictionary<string, object> data = new Dictionary<string, object>
{
                {"prodId", idTextbox.Text},
                {"prodName", nameTextbox.Text},
                {"prodDesc", descriptionTextbox.Text},
                {"prodPrice", priceTextbox.Text},
                {"prodTag", tagTextbox.Text},
                {"prodImage", imageHiddenField.Value},
                {"prodShopName", shopNameTextbox.Text }
            };

            if (nameTextBoxValidator.IsValid && descriptionTextboxValidator.IsValid && priceTextboxValidator.IsValid && tagTextboxValidator.IsValid && shopNameRequiredFieldValidator.IsValid)
            {

                try
                {
                    await docRef.SetAsync(data, SetOptions.MergeAll);

                    string message = "Product Successfully Updated";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    Response.Redirect("~/ShopOwner/MyShopProductsPage.aspx", false);
                }
                catch (Exception)
                {
                    string message = "Error Updating Product";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    Response.Redirect("~/ShopOwner/MyShopProductsPage.aspx", false);
                }
            }
        }

        protected void changeButton_Click(object sender, EventArgs e)
        {
            // Get the name of the product
            string prodName = nameTextbox.Text;

            // Set the value of the hidden field
            hfProductName.Value = prodName;

            // Open the second popup page
            string url = "ChangeProductImagePage.aspx?prodName=" + prodName;
            Response.Redirect(url);
            //string s = "window.open('" + url + "', 'popup_window', 'width=500,height=300,left=100,top=100,resizable=yes');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "popupScript", s, true);
        }
    }
}