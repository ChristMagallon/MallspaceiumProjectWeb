using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.Shopper
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
                getProfileDetails();
            }
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
                string image = snapshot.GetValue<string>("shopperImage");
                string firstName = snapshot.GetValue<string>("firstName");
                string lastName = snapshot.GetValue<string>("lastName");
                string birthday = snapshot.GetValue<string>("dob");
                string gender = snapshot.GetValue<string>("gender");
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
                firstNameTextBox.Text = firstName;
                lastNameTextBox.Text = lastName;
                birthdayTextbox.Text = birthday;
                genderDropDownList.SelectedValue = gender;
                phoneNumberTextbox.Text = phoneNumber;
                addressTextbox.Text = address;

            }
        }

        protected void editImageButton_Click(object sender, EventArgs e)
        {
            // Redirect to another page
            string url = "EditProfilePicturePage.aspx";
            Response.Redirect(url);
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            update();
        }

        public async void update()
        {
            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget"));

            Dictionary<string, object> data = new Dictionary<string, object>
{
                {"shopImage", imageHiddenField.Value},
                {"firstName", firstNameTextBox.Text},
                {"lastName", lastNameTextBox.Text},
                {"birthday", birthdayTextbox.Text},
                {"gender", genderDropDownList.SelectedItem.Text},
                {"phoneNumber", phoneNumberTextbox.Text},
                {"address", addressTextbox.Text }
            };

            if (firstNameRequiredFieldValidator.IsValid && lastNameRequiredFieldValidator.IsValid && birthdayRequiredFieldValidator.IsValid && genderRequiredFieldValidator.IsValid  && phoneNumberRequiredFieldValidator.IsValid && addressRequiredFieldValidator.IsValid)
            {

                try
                {
                    await docRef.SetAsync(data, SetOptions.MergeAll);

                    // Display a message
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Updated Profile!');", true);

                    // Redirect to another page after a delay
                    string url = "ProfilePage.aspx?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
                }
                catch (Exception)
                {
                    // Display a message
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Error Updating Profile!');", true);

                    // Redirect to another page after a delay
                    string url = "ProfilePage.aspx?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
                }
            }
        }
    }
}