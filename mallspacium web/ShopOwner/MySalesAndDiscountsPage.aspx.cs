﻿using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.ShopOwner
{
    public partial class MySalesAndDiscountsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAllSalesAndDiscounts();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShopOwner/AddSalesAndDiscountPage.aspx", false);
        }

        public void getAllSalesAndDiscounts()
        {
            CollectionReference usersRef = database.Collection("Users");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable allSalesAndDiscountGridViewTable = new DataTable();

            allSalesAndDiscountGridViewTable.Columns.Add("saleDiscId", typeof(string));
            allSalesAndDiscountGridViewTable.Columns.Add("saleDiscImage", typeof(byte[]));
            allSalesAndDiscountGridViewTable.Columns.Add("saleDiscDesc", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                // Create a reference to the child collection inside the parent document
                CollectionReference productsRef = documentSnapshot.Reference.Collection("SaleDiscount");

                // Retrieve the documents from the child collection
                QuerySnapshot productsSnapshot = productsRef.GetSnapshotAsync().Result;

                foreach (DocumentSnapshot productDoc in productsSnapshot.Documents)
                {
                    string saleDiscId = productDoc.GetValue<string>("saleDiscId");
                    string base64String = productDoc.GetValue<string>("saleDiscImage");
                    byte[] saleDiscImage = Convert.FromBase64String(base64String);
                    string saleDiscDescription = productDoc.GetValue<string>("saleDiscDesc");

                    DataRow dataRow = allSalesAndDiscountGridViewTable.NewRow();

                    dataRow["saleDiscId"] = saleDiscDescription;
                    dataRow["saleDiscImage"] = saleDiscImage;
                    dataRow["saleDiscDesc"] = saleDiscDescription;

                    allSalesAndDiscountGridViewTable.Rows.Add(dataRow);
                }
            }
            // Bind the DataTable to the GridView control
            allSalesAndDiscountGridView.DataSource = allSalesAndDiscountGridViewTable;
            allSalesAndDiscountGridView.DataBind();
        }

        protected void allSalesAndDiscountGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "saleDiscImage");
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

        protected async void allSalesAndDiscountGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the document ID from the DataKeys collection
            string docId = allSalesAndDiscountGridView.DataKeys[e.RowIndex]["saleDiscId"].ToString();

            // Get a reference to the document to be deleted (to be edited)
            DocumentReference docRef = database.Collection("Users").Document().Collection("SalesAndDiscount").Document(docId);

            // Delete the document
            await docRef.DeleteAsync();

            Response.Write("<script>alert('Successfully Deleted Sale and Discount!');</script>");

            // Rebind the GridView control to reflect the changes
            allSalesAndDiscountGridView.DataBind();
        }
    }
}