using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAllSalesAndDiscounts();
        }

        public void getAllSalesAndDiscounts()
        {
            CollectionReference usersRef = database.Collection("Users");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable allSaleDiscountGridViewTable = new DataTable();

            allSaleDiscountGridViewTable.Columns.Add("saleDiscId", typeof(string));
            allSaleDiscountGridViewTable.Columns.Add("saleDiscImage", typeof(byte[]));
            allSaleDiscountGridViewTable.Columns.Add("saleDiscDesc", typeof(string));
            allSaleDiscountGridViewTable.Columns.Add("saleDiscStartDate", typeof(string));
            allSaleDiscountGridViewTable.Columns.Add("saleDiscEndDate", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                // Create a reference to the child collection inside the parent document
                CollectionReference productsRef = documentSnapshot.Reference.Collection("SaleDiscount");

                // Retrieve the documents from the child collection
                QuerySnapshot productsSnapshot = productsRef.GetSnapshotAsync().Result;

                foreach (DocumentSnapshot productDoc in productsSnapshot.Documents)
                {
                    //string saleDiscId = productDoc.GetValue<string>("saleDiscId");
                    string base64String = productDoc.GetValue<string>("saleDiscImage");
                    byte[] saleDiscImage = Convert.FromBase64String(base64String);
                    string saleDiscDescription = productDoc.GetValue<string>("saleDiscDesc");
                    string saleDiscStartDate = productDoc.GetValue<string>("saleDiscStartDate");
                    string saleDiscEndDate = productDoc.GetValue<string>("saleDiscEndDate");

                    DataRow dataRow = allSaleDiscountGridViewTable.NewRow();

                    dataRow["saleDiscId"] = saleDiscDescription;
                    dataRow["saleDiscImage"] = saleDiscImage;
                    dataRow["saleDiscDesc"] = saleDiscDescription;
                    dataRow["saleDiscStartDate"] = saleDiscStartDate;
                    dataRow["saleDiscEndDate"] = saleDiscEndDate;

                    allSaleDiscountGridViewTable.Rows.Add(dataRow);
                }
            }
            // Bind the DataTable to the GridView control
            allSaleDiscountGridView.DataSource = allSaleDiscountGridViewTable;
            allSaleDiscountGridView.DataBind();
        }

        protected void allSaleDiscountGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "saleDiscImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 200; // set the width of the image
                    imageControl.Height = 200; // set the height of the image
                }
            }
        }
    }
}