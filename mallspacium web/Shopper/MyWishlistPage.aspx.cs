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

        protected void myWishlistGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the automatically generated Delete button
                Button btnDelete = e.Row.Cells[0].Controls[0] as Button;
                if (btnDelete != null && btnDelete.CommandName == "Delete")
                {
                    // Change the button text to "Remove"
                    btnDelete.Text = "Remove";
                }
            }
        }
    }
}