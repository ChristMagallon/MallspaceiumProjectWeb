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

        public void getAllShopProducts()
        {
            CollectionReference usersRef = database.Collection("Users");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable allShopProductGridViewTable = new DataTable();

            allShopProductGridViewTable.Columns.Add("prodName", typeof(string));
            allShopProductGridViewTable.Columns.Add("prodImage", typeof(byte[]));
            allShopProductGridViewTable.Columns.Add("prodDesc", typeof(string));
            allShopProductGridViewTable.Columns.Add("prodPrice", typeof(string));
            allShopProductGridViewTable.Columns.Add("prodTag", typeof(string));
            allShopProductGridViewTable.Columns.Add("prodShopName", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                // Create a reference to the child collection inside the parent document
                CollectionReference productsRef = documentSnapshot.Reference.Collection("Product");

                // Retrieve the documents from the child collection
                QuerySnapshot productsSnapshot = productsRef.GetSnapshotAsync().Result;

                

                foreach (DocumentSnapshot productDoc in productsSnapshot.Documents)
                {
                    string productName = productDoc.GetValue<string>("prodName");
                    string base64String = productDoc.GetValue<string>("prodImage");
                    byte[] productImage = Convert.FromBase64String(base64String);
                    string productDescription = productDoc.GetValue<string>("prodDesc");
                    string productPrice = productDoc.GetValue<string>("prodPrice");
                    string productTag = productDoc.GetValue<string>("prodTag");
                    string productShopName = productDoc.GetValue<string>("prodShopName");

                    DataRow dataRow = allShopProductGridViewTable.NewRow();

                    dataRow["prodName"] = productName;
                    dataRow["prodImage"] = productImage;
                    dataRow["prodDesc"] = productDescription;
                    dataRow["prodPrice"] = productPrice;
                    dataRow["prodTag"] = productTag;
                    dataRow["prodShopName"] = productShopName;

                    allShopProductGridViewTable.Rows.Add(dataRow);
                }
            }
            // Bind the DataTable to the GridView control
            allShopProductGridView.DataSource = allShopProductGridViewTable;
            allShopProductGridView.DataBind();
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