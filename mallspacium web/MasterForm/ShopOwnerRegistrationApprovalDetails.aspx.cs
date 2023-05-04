using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class ShopOwnerRegistrationApprovalDetails : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                getShopOwnerRegistrationDetails();
            }
        }

        public async void getShopOwnerRegistrationDetails()
        {
            // Retrieve the shop name from the query string
            string email = Request.QueryString["email"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("ShopOwnerRegistrationApproval").WhereEqualTo("email", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot shopDoc = snapshot.Documents.FirstOrDefault();
            if (shopDoc != null)
            {
                // Retrieve the data from the document
                string userID = shopDoc.GetValue<string>("userID");
                string fname = shopDoc.GetValue<string>("firstName");
                string lname = shopDoc.GetValue<string>("lastName");
                string image = shopDoc.GetValue<string>("shopImage");
                string shopName = shopDoc.GetValue<string>("shopName");
                string desc = shopDoc.GetValue<string>("shopDescription");
                string phoneNumber = shopDoc.GetValue<string>("phoneNumber");
                string address = shopDoc.GetValue<string>("address");
                string username = shopDoc.GetValue<string>("username");
                string role = shopDoc.GetValue<string>("userRole");
                string date = shopDoc.GetValue<string>("dateCreated");

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
                shopImage.ImageUrl = imageSrc;

                // Display the data
                userIdLabel.Text = userID;
                emailLabel.Text = email;
                firstNameLabel.Text = fname;
                lastNameLabel.Text = lname;
                shopNameLabel.Text = shopName;
                descriptionLabel.Text = desc;
                phoneNumberLabel.Text = phoneNumber;
                addressLabel.Text = address;
                usernameLabel.Text = username;
                userRoleLabel.Text = role;
                dateLabel.Text = date;
                shopImageHiddenField.Value = image;
            }
            else
            {
                Response.Write("<script>alert('Error: Advertisement Not Found.');</script>");
            }
        }

        protected void viewShopPermitDetailsButton_Click(object sender, EventArgs e)
        {
            viewPermitImage();
        }

        public void viewPermitImage()
        {
            string email = emailLabel.Text;

            // Get the URL for the webform with the image control
            string url = "PopUpShopOwnerPermitImage.aspx?email=" + email;

            // Set the height and width of the popup window
            int height = 800;
            int width = 800;

            // Open the popup window
            string script = $"window.open('{url}&height={height}&width={width}', '_blank', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
        }

        protected void approveButton_Click(object sender, EventArgs e)
        {

        }

        protected void disapproveButton_Click(object sender, EventArgs e)
        {

        }
    }
}