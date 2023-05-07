using System;
using System.Collections.Generic;
using System.Data;
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
                getVistedShops();
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
                bool verified = documentSnapshot.GetValue<bool>("verified"); // Modify to retrieve as bool

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
        public void getVistedShops()
        {
            Query query = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("VisitedShop");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable visitedShopsGridViewTable = new DataTable();

            visitedShopsGridViewTable.Columns.Add("shopName", typeof(string));
            visitedShopsGridViewTable.Columns.Add("shopImage", typeof(byte[]));
            visitedShopsGridViewTable.Columns.Add("shopDescription", typeof(string));
            visitedShopsGridViewTable.Columns.Add("dateVisited", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                string shopName = documentSnapshot.GetValue<string>("shopName");
                string base64String = documentSnapshot.GetValue<string>("shopImage");
                byte[] shopImage = Convert.FromBase64String(base64String);
                string shopDescription = documentSnapshot.GetValue<string>("shopDescription");
                string date = documentSnapshot.GetValue<string>("dateVisited");

                DataRow dataRow = visitedShopsGridViewTable.NewRow();

                dataRow["shopName"] = shopName;
                dataRow["shopImage"] = shopImage;
                dataRow["shopDescription"] = shopDescription;
                dataRow["dateVisited"] = date;

                visitedShopsGridViewTable.Rows.Add(dataRow);
            }

            // Use DataView to sort DataTable by date field
            DataView dataView = visitedShopsGridViewTable.DefaultView;
            dataView.Sort = "dateVisited DESC";
            visitedShopsGridViewTable = dataView.ToTable();

            // Bind the DataTable to the GridView control
            visitedShopsGridView.DataSource = visitedShopsGridViewTable;
            visitedShopsGridView.DataBind();
        }

        protected void visitedShopsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "shopImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
                else
                {
                    // If no image is available, show a default image instead
                    imageControl.ImageUrl = "/Images/no-image.jpg";
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(visitedShopsGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void visitedShopsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = visitedShopsGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string shopName = visitedShopsGridView.DataKeys[selectedIndex].Values["shopName"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("~/Shopper/PopularShopDetailsPage.aspx?shopName=" + shopName, false);
        }


        protected void editProfileButton_Click(object sender, EventArgs e)
        {
            // Redirect to another page 
            Response.Redirect("EditProfilePage.aspx");
        }
    }
}