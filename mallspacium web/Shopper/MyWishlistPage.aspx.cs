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
    public partial class MyWishlistPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getWishlist();
        }

        public async void getWishlist()
        {
            // Query the Firestore database to get the Wishlist collection for the current user
            CollectionReference wishlistRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Wishlist");
            QuerySnapshot querySnapshot = await wishlistRef.GetSnapshotAsync();

            // Create a DataTable to store the retrieved data
            DataTable wishlistGridViewTable = new DataTable();
            wishlistGridViewTable.Columns.Add("prodName", typeof(string));
            wishlistGridViewTable.Columns.Add("prodImage", typeof(byte[]));
            wishlistGridViewTable.Columns.Add("prodDesc", typeof(string));
            wishlistGridViewTable.Columns.Add("prodPrice", typeof(string));
            wishlistGridViewTable.Columns.Add("prodTag", typeof(string));
            wishlistGridViewTable.Columns.Add("prodShopName", typeof(string));

            // Loop through each document in the Wishlist collection and add its data to the products list
            foreach (DocumentSnapshot doc in querySnapshot.Documents)
            {
                string productName = doc.GetValue<string>("prodName");
                string base64String = doc.GetValue<string>("prodImage");
                byte[] productImage = Convert.FromBase64String(base64String);
                string productDescription = doc.GetValue<string>("prodDesc");
                string productPrice = doc.GetValue<string>("prodPrice");
                string productTag = doc.GetValue<string>("prodTag");
                string productShopName = doc.GetValue<string>("prodShopName");

                DataRow dataRow = wishlistGridViewTable.NewRow();

                dataRow["prodName"] = productName;
                dataRow["prodImage"] = productImage;
                dataRow["prodDesc"] = productDescription;
                dataRow["prodPrice"] = productPrice;
                dataRow["prodTag"] = productTag;
                dataRow["prodShopName"] = productShopName;

                wishlistGridViewTable.Rows.Add(dataRow);
            }

            // Bind the products list to the GridView
            myWishlistGridView.DataSource = wishlistGridViewTable;
            myWishlistGridView.DataBind();
        }
    

        protected void myWishlistGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "prodImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }
        }

        protected async void myWishlistGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the row being deleted
            GridViewRow row = myWishlistGridView.Rows[e.RowIndex];

            // Get shop name from the row
            string prodName = row.Cells[0].Text;

            // Query the Users collection to get the User document that contains the Wishlist collection
            Query query = database.Collection("Users").WhereEqualTo("email", (string)Application.Get("usernameget"));
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = querySnapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Wishlist collection from the User document
                CollectionReference wishlistRef = userDoc.Reference.Collection("Wishlist");

                // Query the Wishlist collection to get the product with the given document ID (which is equal to the selected shopName value)
                Query wishlistQuery = wishlistRef.WhereEqualTo("prodName", prodName);
                QuerySnapshot wishlistQuerySnapshot = await wishlistQuery.GetSnapshotAsync();

                // Get the first document from the query result (assuming there's only one matching document)
                DocumentSnapshot wishlistDoc = wishlistQuerySnapshot.Documents.FirstOrDefault();
                if (wishlistDoc != null)
                {
                    // Delete the document from the Wishlist collection
                    await wishlistDoc.Reference.DeleteAsync();

                    // Refresh the GridView
                    Response.Write("<script>alert('Successfully Removed Shop to the Wishlist!');</script>");
                    getWishlist();
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