using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.MasterForm3
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
            getProducts();
        }

        public async void getProducts()
        {
            CollectionReference usersRef = database.Collection("Users");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = await usersRef.GetSnapshotAsync();

            // Create a DataTable to store the retrieved data

            DataTable productGridViewTable = new DataTable();
            productGridViewTable.Columns.Add("prodName", typeof(string));
            productGridViewTable.Columns.Add("prodImage", typeof(byte[]));
            productGridViewTable.Columns.Add("prodDesc", typeof(string));
            productGridViewTable.Columns.Add("prodPrice", typeof(string));
            productGridViewTable.Columns.Add("prodTag", typeof(string));
            productGridViewTable.Columns.Add("prodShopName", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                // Create a reference to the child collection inside the parent document
                CollectionReference productsRef = documentSnapshot.Reference.Collection("Product");

                // Retrieve the documents from the child collection
                QuerySnapshot productsSnapshot = await productsRef.GetSnapshotAsync();

                foreach (DocumentSnapshot productDoc in productsSnapshot.Documents)
                {
                    string productName = productDoc.GetValue<string>("prodName");
                    string base64String = productDoc.GetValue<string>("prodImage");
                    byte[] productImage = Convert.FromBase64String(base64String);
                    string productDescription = productDoc.GetValue<string>("prodDesc");
                    string productPrice = productDoc.GetValue<string>("prodPrice");
                    string productTag = productDoc.GetValue<string>("prodTag");
                    string productShopName = productDoc.GetValue<string>("prodShopName");

                    DataRow dataRow = productGridViewTable.NewRow();

                    dataRow["prodName"] = productName;
                    dataRow["prodImage"] = productImage;
                    dataRow["prodDesc"] = productDescription;
                    dataRow["prodPrice"] = productPrice;
                    dataRow["prodTag"] = productTag;
                    dataRow["prodShopName"] = productShopName;

                    productGridViewTable.Rows.Add(dataRow);
                }
            }
            // Bind the DataTable to the GridView control
            productGridView.DataSource = productGridViewTable;
            productGridView.DataBind();
        }

        protected void productGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected async void productGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected prodName value from the productGridView
            GridViewRow row = productGridView.SelectedRow;
            if (row != null)
            {
                string prodName = row.Cells[0].Text;
                string prodShopName = row.Cells[5].Text;

                // Query the Users collection to get the User document that contains the Product collection
                Query query = database.Collection("Users").WhereEqualTo("shopName", prodShopName);
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                // Get the first document from the query result (assuming there's only one matching document)
                DocumentSnapshot userDoc = querySnapshot.Documents.FirstOrDefault();
                if (userDoc != null)
                {
                    // Get the Product collection from the User document
                    CollectionReference productsRef = userDoc.Reference.Collection("Product");

                    // Query the Product collection to get the product with the given document ID (which is equal to the selected prodName value)
                    Query productQuery = productsRef.WhereEqualTo("prodName", prodName);
                    QuerySnapshot productQuerySnapshot = await productQuery.GetSnapshotAsync();

                    // Get the first document from the query result (assuming there's only one matching document)
                    DocumentSnapshot productDoc = productQuerySnapshot.Documents.FirstOrDefault();
                    if (productDoc != null)
                    {
                        // Get the product details from the document data
                        string productName = productDoc.GetValue<string>("prodName");
                        string productImage = productDoc.GetValue<string>("prodImage");
                        string productDescription = productDoc.GetValue<string>("prodDesc");
                        string productPrice = productDoc.GetValue<string>("prodPrice");
                        string productTag = productDoc.GetValue<string>("prodTag");
                        string productShopName = productDoc.GetValue<string>("prodShopName");

                        // Save the product details to the Wishlist collection in Firestore
                        // You can use the AddAsync method to add a new document to a collection
                        DocumentReference wishlistRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Wishlist").Document(prodName);
                        Dictionary<string, object> wishlistData = new Dictionary<string, object>()
                    {
                        { "prodName", productName },
                        { "prodImage", productImage },
                        { "prodDesc", productDescription },
                        { "prodPrice", productPrice },
                        { "prodTag", productTag },
                        { "prodShopName", productShopName },
                        };

                        try
                        {
                            await wishlistRef.SetAsync(wishlistData);
                            Response.Write("<script>alert('Successfully Added Product to the Wishlist!');</script>");
                        }
                        catch (Exception)
                        {
                            Response.Write("<script>alert('Error Adding to the Wishlist.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Error: Product Not Found.');</script>");
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
            }
        }
    }
}