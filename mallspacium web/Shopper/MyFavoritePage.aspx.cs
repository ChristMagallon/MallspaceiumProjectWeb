using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.Shopper
{
    public partial class MyFavoritePage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getFavorite();
        }

        public async void getFavorite()
        {
            // Query the Firestore database to get the Wishlist collection for the current user
            CollectionReference wishlistRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Favorite");
            QuerySnapshot querySnapshot = await wishlistRef.GetSnapshotAsync();

            // Create a DataTable to store the retrieved data
            DataTable favoriteGridViewTable = new DataTable();
            favoriteGridViewTable.Columns.Add("shopName", typeof(string));
            favoriteGridViewTable.Columns.Add("image", typeof(byte[]));
            favoriteGridViewTable.Columns.Add("shopDescription", typeof(string));
            favoriteGridViewTable.Columns.Add("email", typeof(string));
            favoriteGridViewTable.Columns.Add("phoneNumber", typeof(string));
            favoriteGridViewTable.Columns.Add("address", typeof(string));

            // Loop through each document in the Wishlist collection and add its data to the products list
            foreach (DocumentSnapshot doc in querySnapshot.Documents)
            {
                string name = doc.GetValue<string>("shopName");
                string base64String = doc.GetValue<string>("shopImage");
                byte[] image = Convert.FromBase64String(base64String);
                string description = doc.GetValue<string>("shopDescription");
                string email = doc.GetValue<string>("email");
                string phoneNumber = doc.GetValue<string>("phoneNumber");
                string address = doc.GetValue<string>("address");

                DataRow dataRow = favoriteGridViewTable.NewRow();

                dataRow["shopName"] = name;
                dataRow["image"] = image;
                dataRow["shopDescription"] = description;
                dataRow["email"] = email;
                dataRow["phoneNumber"] = phoneNumber;
                dataRow["address"] = address;

                favoriteGridViewTable.Rows.Add(dataRow);
            }

            // Bind the products list to the GridView
            myFavoriteGridView.DataSource = favoriteGridViewTable;
            myFavoriteGridView.DataBind();
        }

        protected void myFavoriteGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "image");
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
        }

        protected async void myFavoriteGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the row being deleted
            GridViewRow row = myFavoriteGridView.Rows[e.RowIndex];

            // Get shop name from the row
            string shopName = row.Cells[0].Text;

            // Query the Users collection to get the User document that contains the Favorite collection
            Query query = database.Collection("Users").WhereEqualTo("email", (string)Application.Get("usernameget"));
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = querySnapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Favorite collection from the User document
                CollectionReference favoriteRef = userDoc.Reference.Collection("Favorite");

                // Query the Favorite collection to get the shops with the given document ID (which is equal to the selected shopName value)
                Query favoriteQuery = favoriteRef.WhereEqualTo("shopName", shopName);
                QuerySnapshot favoriteQuerySnapshot = await favoriteQuery.GetSnapshotAsync();

                // Get the first document from the query result (assuming there's only one matching document)
                DocumentSnapshot favoriteDoc = favoriteQuerySnapshot.Documents.FirstOrDefault();
                if (favoriteDoc != null)
                {
                    // Delete the document from the Wishlist collection
                    await favoriteDoc.Reference.DeleteAsync();

                    // Refresh the GridView
                    Response.Write("<script>alert('Successfully Removed Shop to the Favorite List!');</script>");
                    getFavorite();
                }
                else
                {
                    Response.Write("<script>alert('Error Removing to the Favorite List.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
        }
    }
}