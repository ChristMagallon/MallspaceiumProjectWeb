using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

            if (!IsPostBack)
            {
                showAdvertisementPopup();
            }

            getProducts();
        }

        public async void showAdvertisementPopup()
        {
            // Retrieve subscription type from AdminManageSubscriptions collection
            DocumentReference docRef = database.Collection("AdminManageSubscription").Document((string)Application.Get("usernameget"));
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            string subscriptionType = snapshot.GetValue<string>("subscriptionType");

            // Check if subscription type is Free Subscription or Basic Subscription
            if (subscriptionType == "Free Subscription" || subscriptionType == "Basic Subscription")
            {
                // Query AllAdvertisement collection for documents
                QuerySnapshot adSnapshot = await database.Collection("AllAdvertisement").GetSnapshotAsync();

                // If documents exist
                if (adSnapshot.Documents.Count > 0)
                {
                    // Get a random document
                    DocumentSnapshot randomDoc = adSnapshot.Documents[new Random().Next(adSnapshot.Documents.Count)];

                    // Retrieve adProdImage string from selected document
                    string adsProdImage = randomDoc.GetValue<string>("adsProdImage");

                    // Convert the image string to a byte array
                    byte[] imageBytes = Convert.FromBase64String(adsProdImage);

                    // Get the image height and width
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(ms))
                        {
                            // Set the height and width of the popup window
                            int height = img.Height;
                            int width = img.Width;

                            // Create a Base64-encoded data URL for the image
                            string imageBase64String = Convert.ToBase64String(imageBytes);
                            string imageSrc = $"data:image/jpeg;base64,{imageBase64String}";

                            // Register the script to open the popup window using ScriptManager
                            string script = $"var w = window.open('', '', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no'); w.document.write('<html><body><img src=\"{imageSrc}\"/></body></html>');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
                        }
                    }
                }
            }
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
                    string productTag = productDoc.GetValue<string>("prodTag");
                    string productShopName = productDoc.GetValue<string>("prodShopName");

                    DataRow dataRow = productGridViewTable.NewRow();

                    dataRow["prodName"] = productName;
                    dataRow["prodImage"] = productImage;
                    dataRow["prodDesc"] = productDescription;
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(productGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void productGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = productGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string prodShopName = productGridView.DataKeys[selectedIndex].Values["prodShopName"].ToString();
            string prodName = productGridView.DataKeys[selectedIndex].Values["prodName"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("ProductDetailsPage.aspx?prodShopName=" + prodShopName + "&prodName=" + prodName, false);
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
                getProducts();
            }
            else
            {

                CollectionReference usersRef = database.Collection("Users");
                // Retrieve the documents from the parent collection
                QuerySnapshot querySnapshot = await usersRef.GetSnapshotAsync();

                // Create a DataTable to store the retrieved data
                DataTable productGridViewTable = new DataTable();

                productGridViewTable.Columns.Add("prodName", typeof(string));
                productGridViewTable.Columns.Add("prodImage", typeof(byte[]));
                productGridViewTable.Columns.Add("prodDesc", typeof(string));
                productGridViewTable.Columns.Add("prodTag", typeof(string));
                productGridViewTable.Columns.Add("prodShopName", typeof(string));

                bool searchResultFound = false; // flag to keep track of search results

                // Iterate through the documents and populate the DataTable
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    // Create a reference to the child collection inside the parent document
                    Query query = documentSnapshot.Reference.Collection("Product")
                        .WhereGreaterThanOrEqualTo("prodName", searchProdName)
                        .WhereLessThanOrEqualTo("prodName", searchProdName + "\uf8ff");

                    // Retrieve the documents from the child collection
                    QuerySnapshot productsSnapshot = await query.GetSnapshotAsync();

                    if (productsSnapshot.Documents.Count > 0)
                    {
                        foreach (DocumentSnapshot productDoc in productsSnapshot.Documents)
                        {
                            string productName = productDoc.GetValue<string>("prodName");
                            string base64String = productDoc.GetValue<string>("prodImage");
                            byte[] productImage = Convert.FromBase64String(base64String);
                            string productDescription = productDoc.GetValue<string>("prodDesc");
                            string productTag = productDoc.GetValue<string>("prodTag");
                            string productShopName = productDoc.GetValue<string>("prodShopName");

                            DataRow dataRow = productGridViewTable.NewRow();

                            dataRow["prodName"] = productName;
                            dataRow["prodImage"] = productImage;
                            dataRow["prodDesc"] = productDescription;
                            dataRow["prodTag"] = productTag;
                            dataRow["prodShopName"] = productShopName;

                            productGridViewTable.Rows.Add(dataRow);
                        }

                        searchResultFound = true; // search result found for this document
                    }
                }

                if (searchResultFound)
                {
                    // Bind the DataTable to the GridView control
                    productGridView.DataSource = productGridViewTable;
                    productGridView.DataBind();
                }
                else
                {
                    // Display an error message if no search results are found
                    errorMessageLabel.Text = "No results found.";
                    errorMessageLabel.Visible = true;
                    productGridView.Visible = false;
                }
            }
        }

    }
}
