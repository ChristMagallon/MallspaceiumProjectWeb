using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm3
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                retrieveShopperDetails();
            }
        }

        public async void retrieveShopperDetails()
        {
            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("email", (string)Application.Get("usernameget"));
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Loop through the documents in the query snapshot
            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                // Retrieve the data from the document
                string image = documentSnapshot.GetValue<string>("shopperImage");
                string firstName = documentSnapshot.GetValue<string>("firstName");
                string lastName = documentSnapshot.GetValue<string>("lastName");
                string birthday = documentSnapshot.GetValue<string>("dob");
                string gender = documentSnapshot.GetValue<string>("gender");
                string email = documentSnapshot.GetValue<string>("email");
                string phoneNumber = documentSnapshot.GetValue<string>("phoneNumber");
                string address = documentSnapshot.GetValue<string>("address");
                 

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

                // Combine the first name and last name into a single string
                string fullName = $"{firstName} {lastName}";

                // Display the data
                nameLabel.Text = fullName;
                birthdayLabel.Text = birthday;
                genderLabel.Text = gender;
                emailLabel.Text = email;
                phoneNumberLabel.Text = phoneNumber;
                addressLabel.Text = address;
            }
        }

        protected void editProfileButton_Click(object sender, EventArgs e)
        {
            // Redirect to another page 
            Response.Redirect("EditProfilePage.aspx");
        }
    }
}