﻿using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.MasterForm2
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getOwnShopProducts();
        }

        public async void getOwnShopProducts()
        {
            // Create a reference to the parent collection 
            CollectionReference usersRef = database.Collection("Users");

            //to be change ni siya dapat ku
            DocumentReference docRef = usersRef.Document("N7oc8axuTkAsWFKtrSyt");

            CollectionReference productRef = docRef.Collection("Product");

            // Retrieve the documents from the child collection
            QuerySnapshot querySnapshot = productRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable ownProductsGridViewTable = new DataTable();

            ownProductsGridViewTable.Columns.Add("prodName", typeof(string));
            ownProductsGridViewTable.Columns.Add("prodImage", typeof(byte[]));
            ownProductsGridViewTable.Columns.Add("prodDesc", typeof(string));
            ownProductsGridViewTable.Columns.Add("prodPrice", typeof(string));
            ownProductsGridViewTable.Columns.Add("prodTag", typeof(string));
            ownProductsGridViewTable.Columns.Add("prodShopName", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                
                    string productName = documentSnapshot.GetValue<string>("prodName");
                    string base64String = documentSnapshot.GetValue<string>("prodImage");
                    byte[] productImage = Convert.FromBase64String(base64String);
                    string productDescription = documentSnapshot.GetValue<string>("prodDesc");
                    string productPrice = documentSnapshot.GetValue<string>("prodPrice");
                    string productTag = documentSnapshot.GetValue<string>("prodTag");
                    string productShopName = documentSnapshot.GetValue<string>("prodShopName");

                    DataRow dataRow = ownProductsGridViewTable.NewRow();

                    dataRow["prodName"] = productName;
                    dataRow["prodImage"] = productImage;
                    dataRow["prodDesc"] = productDescription;
                    dataRow["prodPrice"] = productPrice;
                    dataRow["prodTag"] = productTag;
                    dataRow["prodShopName"] = productShopName;

                    ownProductsGridViewTable.Rows.Add(dataRow);

                    // Bind the DataTable to the GridView control
                    ownShopProductGridView.DataSource = ownProductsGridViewTable;
                    ownShopProductGridView.DataBind();
            }
        }

        protected void ownShopProductGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShopOwner/AddProductPage.aspx", false);
        }

        protected void OwnShopProductGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the selected row
            GridViewRow row = ownShopProductGridView.Rows[e.NewEditIndex];

            // Retrieve the document ID from the DataKeys collection
            string prodName= ownShopProductGridView.DataKeys[e.NewEditIndex].Value.ToString();

            // Redirect to the edit page, passing the document ID as a query string parameter
            Response.Redirect("AccountDetailsForm.aspx?adminUsername=" + prodName, false);
        }

        protected async void OwnShopProductGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the document ID from the DataKeys collection
            string docId = ownShopProductGridView.DataKeys[e.RowIndex]["prodName"].ToString();

            // Get a reference to the document to be deleted (to be edited)
            DocumentReference docRef = database.Collection("Users").Document("N7oc8axuTkAsWFKtrSyt").Collection("Product").Document(docId);

            // Delete the document
            await docRef.DeleteAsync();

            // Remove the item from the data source, if it exists
            /*var dataSource = ownShopProductGridView.DataSource as IList<Product>;
            if (dataSource != null)
            {
                var productToDelete = dataSource.SingleOrDefault(p => p.prodName == docId);
                if (productToDelete != null)
                {
                    dataSource.Remove(productToDelete);

                    // Rebind the GridView control to reflect the changes
                    ownShopProductGridView.DataSource = dataSource;
                    ownShopProductGridView.DataBind();
                }
            }*/
            Response.Write("<script>alert('Product Deleted Successfully!');</script>");

            // Rebind the GridView control to reflect the changes

            ownShopProductGridView.DataBind();
        }
    }
}