using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace mallspacium_web.ShopOwner
{
    public partial class ShopOwnProductsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAllShopProducts();
        }

        public async void getAllShopProducts()
        {
            // Create a reference to the parent collection
            //CollectionReference parentCollectionReference = database.Collection("Users");

            // Create a reference to the parent document
            //DocumentReference parentDocumentReference = parentCollectionReference.Document();

            // Create a reference to the child collection inside the parent document
            //CollectionReference childCollectionReference = parentDocumentReference.Collection("Product");

            // Retrieve the documents from the child collection

            CollectionReference usersRef = database.Collection("Users");
            QuerySnapshot querySnapshot = await usersRef.GetSnapshotAsync();

            // Create a DataTable to store the retrieved data
            DataTable allProductsGridViewTable = new DataTable();

            allProductsGridViewTable.Columns.Add("prodName", typeof(string));
            allProductsGridViewTable.Columns.Add("prodImage", typeof(byte[]));
            allProductsGridViewTable.Columns.Add("prodDesc", typeof(string));
            allProductsGridViewTable.Columns.Add("prodPrice", typeof(string));
            allProductsGridViewTable.Columns.Add("prodTag", typeof(string));
            allProductsGridViewTable.Columns.Add("prodShopName", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                CollectionReference productsRef = documentSnapshot.Reference.Collection("Product");
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

                    DataRow dataRow = allProductsGridViewTable.NewRow();

                    dataRow["prodName"] = productName;
                    dataRow["prodImage"] = productImage;
                    dataRow["prodDesc"] = productDescription;
                    dataRow["prodPrice"] = productPrice;
                    dataRow["prodTag"] = productTag;
                    dataRow["prodShopName"] = productShopName;

                    allProductsGridViewTable.Rows.Add(dataRow);

                    // Bind the DataTable to the GridView control
                    allShopProductGridView.DataSource = allProductsGridViewTable;
                    allShopProductGridView.DataBind();
                }
            }
        }

        protected void allShopProductGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}