using Google.Cloud.Firestore;
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

            getMyShopProducts();
        }

        public void getMyShopProducts()
        {
            // Create a reference to the parent collection 
            CollectionReference usersRef = database.Collection("Users");

            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

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
            Response.Redirect("MyShopProductDetailsPage.aspx?prodName=" + prodName, false);
        }

        protected async void OwnShopProductGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the document ID from the DataKeys collection
            string docId = ownShopProductGridView.DataKeys[e.RowIndex]["prodName"].ToString();

            // Get a reference to the document to be deleted 
            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Product").Document(docId);

            // Delete the document
            await docRef.DeleteAsync();

            Response.Write("<script>alert('Product Deleted Successfully!');</script>");

            // Rebind the data to the GridView control
            CollectionReference colRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Product");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();

            ownShopProductGridView.DataSource = querySnapshot.Documents.Select(d => d.ToDictionary()).ToList();
            ownShopProductGridView.DataBind();
        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            searchProduct();
        }
        // method for searching username 
        public async void searchProduct()
        {
            // Get the search term entered by the user
            string searchTerm = searchTextBox.Text;

            // Query the Firebase Cloud Firestore database for documents that match the search term in either the prodName or prodTag field
            Query query = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Product")
                .WhereEqualTo("prodName", searchTerm)
                //.WhereArrayContains("prodTag", searchTerm)
                ;

            // Retrieve the search results
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            List<Product> results = new List<Product>();

            if (snapshot.Documents.Count > 0)
            {
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    //string base64String = document.GetValue<string>("prodImage");
                    //byte[] productImage = Convert.FromBase64String(base64String);
                    Product prod = document.ConvertTo<Product>();
                    results.Add(prod);
                }
                // Bind the list of products to the GridView control
                ownShopProductGridView.DataSource = results;
                ownShopProductGridView.DataBind();
            }
            else
            {
                // Display an error message if no search results are found
                errorMessageLabel.Text = "No results found.";
                errorMessageLabel.Visible = true;
                ownShopProductGridView.Visible = false;
            }




            /*(string searchUsername = searchTextBox.Text;
            Query query = database.Collection("AdminAccount").Document().Collection().WhereEqualTo("username", searchUsername);

            // Retrieve the search results
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            List<AdminAccount> results = new List<AdminAccount>();

            if (snapshot.Documents.Count > 0)
            {
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    AdminAccount model = document.ConvertTo<AdminAccount>();
                    results.Add(model);
                }
                // Bind the search results to the GridView control
                accountGridView.DataSource = results;
                accountGridView.DataBind();
            }
            else
            {
                accountGridView.DataSource = null;
                accountGridView.DataBind();

                string message = "No records found! Please search another username.";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }*/


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

    }
}