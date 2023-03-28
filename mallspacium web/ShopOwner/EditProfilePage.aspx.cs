using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.ShopOwner
{
    public partial class EditProfilePage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                shopNameTextbox.Enabled = false;
                emailTextbox.Enabled = false;

                getProfileDetails();
            }
        }

        protected void editImageButton_Click(object sender, EventArgs e)
        {
            // Open the second popup page
            string url = "EditProfilePicturePage.aspx?";
            Response.Redirect(url);
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            update();
        }

        public async void getProfileDetails()
        {
            // Use the document ID to retrieve the data from Firestore
            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget"));
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Retrieve the data from the document
                string image = snapshot.GetValue<string>("shopImage");
                string shopName = snapshot.GetValue<string>("shopName");
                string firstName = snapshot.GetValue<string>("firstName");
                string lastName = snapshot.GetValue<string>("lastName");
                string desc = snapshot.GetValue<string>("shopDescription");
                string email = snapshot.GetValue<string>("email");
                string phoneNumber = snapshot.GetValue<string>("phoneNumber");
                string address = snapshot.GetValue<string>("address");

                // Convert the image string to a byte array
                byte[] imageBytes;
                if (string.IsNullOrEmpty(image))
                {
                    // If the image string is null or empty, use the default image instead
                    imageBytes = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                }
                else
                {
                    imageBytes = Convert.FromBase64String(image);
                }

                // Set the image in the FileUpload control
                string imageBase64String = Convert.ToBase64String(imageBytes);
                string imageSrc = $"data:image/png;base64,{imageBase64String}";
                Image1.ImageUrl = imageSrc;

                // Display the data
                imageHiddenField.Value = image;
                shopNameTextbox.Text = shopName;
                firstNameTextBox.Text = firstName;
                lastNameTextBox.Text = lastName;
                descriptionTextbox.Text = desc;
                emailTextbox.Text = email;
                phoneNumberTextbox.Text = phoneNumber;
                addressTextbox.Text = address;
                
            }
        }

        public async void update()
        {
            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget"));

            Dictionary<string, object> data = new Dictionary<string, object>
{
                {"shopImage", imageHiddenField.Value},
                {"shopName", shopNameTextbox.Text},
                {"firstName", firstNameTextBox.Text},
                {"lastName", lastNameTextBox.Text},
                {"shopDescription", descriptionTextbox.Text},
                {"email", emailTextbox.Text},
                {"phoneNumber", phoneNumberTextbox.Text},
                {"address", addressTextbox.Text }
            };

            if (shopNameRequiredFieldValidator.IsValid && firstNameRequiredFieldValidator.IsValid && lastNameRequiredFieldValidator.IsValid && descriptionRequiredFieldValidator.IsValid && emailRequiredFieldValidator.IsValid && phoneNumberRequiredFieldValidator.IsValid && addressRequiredFieldValidator.IsValid)
            {

                try
                {
                    await docRef.SetAsync(data, SetOptions.MergeAll);

                    /*string message = "Product Successfully Updated";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    Response.Redirect("~/ShopOwner/MyShopProductsPage.aspx", false); */

                    // Display a message
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Updated Profile!');", true);

                    // Redirect to another page after a delay
                    string url = "ProfilePage.aspx?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 1000);", true);
                }
                catch (Exception)
                {
                    /*string message = "Error Updating Product";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                    Response.Redirect("~/ShopOwner/MyShopProductsPage.aspx", false);*/

                    // Display a message
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Error Updating Profile!');", true);

                    // Redirect to another page after a delay
                    string url = "ProfilePage.aspx?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 1000);", true);
                }
            }
        }
    }
}