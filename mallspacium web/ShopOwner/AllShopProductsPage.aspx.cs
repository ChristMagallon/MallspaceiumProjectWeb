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
                    string productTag = productDoc.GetValue<string>("prodTag");
                    string productShopName = productDoc.GetValue<string>("prodShopName");

                    DataRow dataRow = allShopProductGridViewTable.NewRow();

                    dataRow["prodName"] = productName;
                    dataRow["prodImage"] = productImage;
                    dataRow["prodDesc"] = productDescription;
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(allShopProductGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void allShopProductGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = allShopProductGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string prodShopName = allShopProductGridView.DataKeys[selectedIndex].Values["prodShopName"].ToString();
            string prodName = allShopProductGridView.DataKeys[selectedIndex].Values["prodName"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("AllProductDetailsPage.aspx?prodShopName=" + prodShopName + "&prodName=" + prodName, false);
        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        // method for searching product name
        public async void search()
        {
            string searchProdName = searchTextBox.Text;

            if (searchTextBox.Text == "")
            {
                getAllShopProducts();
            }
            else
            {
                CollectionReference usersRef = database.Collection("Users");
                // Retrieve the documents from the parent collection
                QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

                // Create a DataTable to store the retrieved data
                DataTable allShopProductGridViewTable = new DataTable();

                allShopProductGridViewTable.Columns.Add("prodName", typeof(string));
                allShopProductGridViewTable.Columns.Add("prodImage", typeof(byte[]));
                allShopProductGridViewTable.Columns.Add("prodDesc", typeof(string));
                allShopProductGridViewTable.Columns.Add("prodTag", typeof(string));
                allShopProductGridViewTable.Columns.Add("prodShopName", typeof(string));

                // Iterate through the documents and populate the DataTable
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    Query query = database.Collection("Product")
                    .WhereGreaterThanOrEqualTo("prodName", searchProdName)
                    .WhereLessThanOrEqualTo("prodName", searchProdName + "\uf8ff");

                    // Retrieve the search results
                    QuerySnapshot snapshot = await query.GetSnapshotAsync();

                    if (snapshot.Documents.Count > 0)
                    {
                        foreach (DocumentSnapshot productDoc in snapshot.Documents)
                        {
                            string productName = productDoc.GetValue<string>("prodName");
                            string base64String = productDoc.GetValue<string>("prodImage");
                            byte[] productImage = Convert.FromBase64String(base64String);
                            string productDescription = productDoc.GetValue<string>("prodDesc");
                            string productTag = productDoc.GetValue<string>("prodTag");
                            string productShopName = productDoc.GetValue<string>("prodShopName");

                            DataRow dataRow = allShopProductGridViewTable.NewRow();

                            dataRow["prodName"] = productName;
                            dataRow["prodImage"] = productImage;
                            dataRow["prodDesc"] = productDescription;
                            dataRow["prodTag"] = productTag;
                            dataRow["prodShopName"] = productShopName;

                            allShopProductGridViewTable.Rows.Add(dataRow);
                        }

                        // Bind the DataTable to the GridView control
                        allShopProductGridView.DataSource = allShopProductGridViewTable;
                        allShopProductGridView.DataBind();
                    }
                    else
                    {
                        // Display an error message if no search results are found
                        errorMessageLabel.Text = "No results found.";
                        errorMessageLabel.Visible = true;
                        allShopProductGridView.Visible = false;
                    }
                }
            }
        }
    }
}