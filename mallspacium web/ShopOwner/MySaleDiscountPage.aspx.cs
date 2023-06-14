using Google.Cloud.Firestore;
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

            getMySaleDiscount();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShopOwner/AddSalesAndDiscountPage.aspx", false);
        }

        public void getMySaleDiscount()
        {
            // Create a reference to the parent collection 
            CollectionReference usersRef = database.Collection("Users");

            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            CollectionReference saleDiscRef = docRef.Collection("SaleDiscount");

            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = saleDiscRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable mySaleDiscountGridViewTable = new DataTable();

            mySaleDiscountGridViewTable.Columns.Add("saleDiscId", typeof(string));
            mySaleDiscountGridViewTable.Columns.Add("saleDiscImage", typeof(byte[]));
            mySaleDiscountGridViewTable.Columns.Add("saleDiscDesc", typeof(string));
            mySaleDiscountGridViewTable.Columns.Add("saleDiscStartDate", typeof(string));
            mySaleDiscountGridViewTable.Columns.Add("saleDiscEndDate", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                string saleDiscId = documentSnapshot.GetValue<string>("saleDiscId"); 
                string base64String = documentSnapshot.GetValue<string>("saleDiscImage");
                byte[] saleDiscImage = Convert.FromBase64String(base64String);
                string saleDiscDescription = documentSnapshot.GetValue<string>("saleDiscDesc");
                string saleDiscStartDate = documentSnapshot.GetValue<string>("saleDiscStartDate");
                string saleDiscEndDate = documentSnapshot.GetValue<string>("saleDiscEndDate");

                DataRow dataRow = mySaleDiscountGridViewTable.NewRow();

                dataRow["saleDiscId"] = saleDiscId;
                dataRow["saleDiscImage"] = saleDiscImage;
                dataRow["saleDiscDesc"] = saleDiscDescription;
                dataRow["saleDiscStartDate"] = saleDiscStartDate;
                dataRow["saleDiscEndDate"] = saleDiscEndDate;

                mySaleDiscountGridViewTable.Rows.Add(dataRow);

                // Bind the DataTable to the GridView control
                mySaleDiscountGridView.DataSource = mySaleDiscountGridViewTable;
                mySaleDiscountGridView.DataBind();
            }
        }

        protected void mySaleDiscountGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected async void mySaleDiscountGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the document ID from the DataKeys collection
            string docId = mySaleDiscountGridView.DataKeys[e.RowIndex].Value.ToString();

            // Get a reference to the document to be deleted
            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("SaleDiscount").Document(docId);

            // Delete the document
            await docRef.DeleteAsync();

            Response.Write("<script>alert('Successfully Deleted Sale and Discount!');</script>");

            // Rebind the data to the GridView control
            CollectionReference colRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("SaleDiscount");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();

            var data = querySnapshot.Documents.Select(d =>
            {
                Dictionary<string, object> dictionary = d.ToDictionary();
                dictionary["saleDiscId"] = d.Id; // Add saleDiscId property with document ID
                return dictionary;
            }).ToList();

            mySaleDiscountGridView.DataSource = data;
            mySaleDiscountGridView.DataBind();
        }


    }
}