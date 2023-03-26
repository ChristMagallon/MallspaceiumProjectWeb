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

            // Loop through each document in the Wishlist collection and add its data to the products list
            foreach (DocumentSnapshot doc in querySnapshot.Documents)
            {
                string name = doc.GetValue<string>("shopName");
                string base64String = doc.GetValue<string>("image");
                byte[] image = Convert.FromBase64String(base64String);
                string description = doc.GetValue<string>("shopDescription");

                DataRow dataRow = favoriteGridViewTable.NewRow();

                dataRow["shopName"] = name;
                dataRow["image"] = image;
                dataRow["shopDescription"] = description;

                favoriteGridViewTable.Rows.Add(dataRow);
            }

            // Bind the products list to the GridView
            myFavoriteGridView.DataSource = favoriteGridViewTable;
            myFavoriteGridView.DataBind();
        }

        protected void myFavoriteGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*GridViewRow row = myFavoriteGridView.SelectedRow;
            if (row != null)
            {
                // Get shop name from the selected row
                string shopName = row.Cells[0].Text;

                // Query the Users collection to get the User document that contains the Favorite collection
                Query query = database.Collection("Users").WhereEqualTo("email", (string)Application.Get("usernameget"));
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                // Get the first document from the query result (assuming there's only one matching document)
                DocumentSnapshot userDoc = querySnapshot.Documents.FirstOrDefault();
                if (userDoc != null)
                {
                    // Get the Product collection from the User document
                    CollectionReference wishlistRef = userDoc.Reference.Collection("Favorite");

                    // Query the Favorite collection to get the shops with the given document ID (which is equal to the selected shopName value)
                    Query wishlistQuery = wishlistRef.WhereEqualTo("shopName", shopName);
                    QuerySnapshot wishlistQuerySnapshot = await wishlistQuery.GetSnapshotAsync();

                    // Get the first document from the query result (assuming there's only one matching document)
                    DocumentSnapshot wishlistDoc = wishlistQuerySnapshot.Documents.FirstOrDefault();
                    if (wishlistDoc != null)
                    {
                        // Delete the document from the Wishlist collection
                        await wishlistDoc.Reference.DeleteAsync();

                        // Refresh the GridView
                        Response.Write("<script>alert('Successfully Removed Shop to the Wishlist!');</script>");
                        getFavorite();
                    }
                    else
                    {
                        Response.Write("<script>alert('Error Removing to the Wishlist.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Error: User Not Found.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Error: No product selected.');</script>");
            }*/

            // Get the index of the selected row
            int selectedIndex = myFavoriteGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string shopName = myFavoriteGridView.DataKeys[selectedIndex].Values["shopName"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("PopularShopDetailsPage.aspx?shopName=" + shopName, false);
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(myFavoriteGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
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
                CollectionReference wishlistRef = userDoc.Reference.Collection("Favorite");

                // Query the Favorite collection to get the shops with the given document ID (which is equal to the selected shopName value)
                Query wishlistQuery = wishlistRef.WhereEqualTo("shopName", shopName);
                QuerySnapshot wishlistQuerySnapshot = await wishlistQuery.GetSnapshotAsync();

                // Get the first document from the query result (assuming there's only one matching document)
                DocumentSnapshot wishlistDoc = wishlistQuerySnapshot.Documents.FirstOrDefault();
                if (wishlistDoc != null)
                {
                    // Delete the document from the Wishlist collection
                    await wishlistDoc.Reference.DeleteAsync();

                    // Refresh the GridView
                    Response.Write("<script>alert('Successfully Removed Shop to the Wishlist!');</script>");
                    getFavorite();
                }
                else
                {
                    Response.Write("<script>alert('Error Removing to the Wishlist.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
        }
    }
}