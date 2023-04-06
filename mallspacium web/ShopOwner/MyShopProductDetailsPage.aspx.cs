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
                    string shop = snapshot.GetValue<string>("prodShopName");
                    string id = snapshot.GetValue<string>("prodId");
                    string name = snapshot.GetValue<string>("prodName");
                    string desc = snapshot.GetValue<string>("prodDesc");
                    string color = snapshot.GetValue<string>("prodColor");
                    string size = snapshot.GetValue<string>("prodSize");
                    string price = snapshot.GetValue<string>("prodPrice");
                    string tag = snapshot.GetValue<string>("prodTag");
                    string availability = snapshot.GetValue<string>("prodAvailability");
                    string image = snapshot.GetValue<string>("prodImage");
                    
                    // Convert the image string to a byte array
                    byte[] imageBytes = Convert.FromBase64String(image);

                    // Set the image in the FileUpload control
                    string imageBase64String = Convert.ToBase64String(imageBytes);
                    string imageSrc = $"data:image/png;base64,{imageBase64String}";
                    Image1.ImageUrl = imageSrc;

                    // Display the data
                    shopNameTextbox.Text = shop;
                    idTextbox.Text = id;
                    nameTextbox.Text = name;
                    descriptionTextbox.Text = desc;
                    colorTextbox.Text = color;
                    sizeTextbox.Text = size;
                    priceTextbox.Text = price;
                    tagDropDownList.SelectedValue = tag;
                    availablityDropDownList.SelectedValue = availability;
                    imageHiddenField.Value = image;
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
                {"prodShopName", shopNameTextbox.Text },
                {"prodId", idTextbox.Text},
                {"prodName", nameTextbox.Text},
                {"prodDesc", descriptionTextbox.Text},
                {"prodColor", colorTextbox.Text },
                {"prodSize", sizeTextbox.Text },
                {"prodPrice", priceTextbox.Text},
                {"prodTag", tagDropDownList.SelectedValue},
                {"prodAvailability", availablityDropDownList.SelectedValue },
                {"prodImage", imageHiddenField.Value},
            };

            if (shopNameRequiredFieldValidator.IsValid && IdTextBoxRequiredFieldValidator.IsValid && nameTextBoxValidator.IsValid && descriptionTextboxValidator.IsValid && colorRequiredFieldValidator.IsValid && sizeRequiredFieldValidator.IsValid && priceTextboxValidator.IsValid && tagRequiredFieldValidator.IsValid && availabilityRequiredFieldValidator.IsValid)
            {
                await docRef.SetAsync(data, SetOptions.MergeAll);
/*
                string message = "Product Successfully Updated";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                Response.Redirect("~/ShopOwner/MyShopProductsPage.aspx", false);*/

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Updated Product!');", true);

                // Redirect to another page after a delay
                string url = "MyShopProductsPage.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
            else
            {
                   /* string message = "Error Updating Product";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    Response.Redirect("~/ShopOwner/MyShopProductsPage.aspx", false);*/

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Error Updating Product!');", true);

                // Redirect to another page after a delay
                string url = "MyShopProductsPage.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);

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
        }
    }
}