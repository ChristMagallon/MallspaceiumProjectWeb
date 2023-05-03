using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.Shopper
{
    public partial class PopularShopDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            // Retrieve the "shopName" value from the query string
            string shopName = Request.QueryString["shopName"];

            label.Text = shopName + " Details";

            if (!IsPostBack)
            {
                retrieveShopDetails();
                getShopProducts();
                getShopSaleDiscount();
            }
        }

        public async void retrieveShopDetails()
        {
            if (!IsPostBack)
            {
                // Retrieve the shop name from the query string
                string shopName = Request.QueryString["shopName"];

                // Use the shop name to retrieve the data from Firestore
                Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    // Retrieve the data from the document
                    string image = documentSnapshot.GetValue<string>("shopImage");
                    string name = documentSnapshot.GetValue<string>("shopName");
                    string desc = documentSnapshot.GetValue<string>("shopDescription");
                    string email = documentSnapshot.GetValue<string>("email");
                    string phoneNumber = documentSnapshot.GetValue<string>("phoneNumber");
                    string address = documentSnapshot.GetValue<string>("address");

                    // Convert the image string to a byte array
                    byte[] imageBytes;
                    if (string.IsNullOrEmpty(image))
                    {
                        // If the image string is null or empty, use the default image instead
                        imageBytes = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                    }
                    else
                    {
                        imageBytes = Convert.FromBase64String(image);
                    }

                    // Set the image in the FileUpload control
                    string imageBase64String = Convert.ToBase64String(imageBytes);
                    string imageSrc = $"data:image/png;base64,{imageBase64String}";
                    Image1.ImageUrl = imageSrc;


                    // Retrieve the user's email from the Application object
                    string userEmail = (string)Application.Get("usernameget");
                    string reportEmail = email;


                    if (userEmail == reportEmail)
                    {
                        // Disable the report button if the emails match
                        reportButton.Enabled = false;
                    }
                    else
                    {
                        // Enable the report button if the emails do not match
                        reportButton.Enabled = true;
                    }

                    // Display the data
                    nameLabel.Text = name;
                    descriptionLabel.Text = desc;
                    emailLabel.Text = email;
                    phoneNumberLabel.Text = phoneNumber;
                    addressLabel.Text = address;
                    imageHiddenField.Value = image;
                }
            }
        }

        public async void getShopProducts()
        {
            // Retrieve the shop name from the query string
            string shopName = Request.QueryString["shopName"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Product collection from the User document
                CollectionReference productRef = userDoc.Reference.Collection("Product");
                QuerySnapshot productSnapshot = await productRef.GetSnapshotAsync();

                if (productRef != null)
                {
                    // Create a DataTable to store the retrieved data
                    DataTable ownProductsGridViewTable = new DataTable();

                    ownProductsGridViewTable.Columns.Add("prodName", typeof(string));
                    ownProductsGridViewTable.Columns.Add("prodImage", typeof(byte[]));
                    ownProductsGridViewTable.Columns.Add("prodDesc", typeof(string));
                    ownProductsGridViewTable.Columns.Add("prodShopName", typeof(string));

                    // Iterate through the documents and populate the DataTable
                    foreach (DocumentSnapshot documentSnapshot in productSnapshot.Documents)
                    {

                        string productName = documentSnapshot.GetValue<string>("prodName");
                        string base64String = documentSnapshot.GetValue<string>("prodImage");
                        byte[] productImage = Convert.FromBase64String(base64String);
                        string productDescription = documentSnapshot.GetValue<string>("prodDesc");
                        string productShopName = documentSnapshot.GetValue<string>("prodShopName");

                        DataRow dataRow = ownProductsGridViewTable.NewRow();

                        dataRow["prodName"] = productName;
                        dataRow["prodImage"] = productImage;
                        dataRow["prodDesc"] = productDescription;
                        dataRow["prodShopName"] = productShopName;

                        ownProductsGridViewTable.Rows.Add(dataRow);
                    }
                    // Bind the DataTable to the GridView control
                    productGridView.DataSource = ownProductsGridViewTable;
                    productGridView.DataBind();
                }
            }
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
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
            Response.Redirect("~/Shopper/ProductDetailsPage.aspx?prodShopName=" + prodShopName + "&prodName=" + prodName, false);
        }

        public async void getShopSaleDiscount()
        {
            // Retrieve the shop name from the query string
            string shopName = Request.QueryString["shopName"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Sale Discount collection from the User document
                CollectionReference saleDiscRef = userDoc.Reference.Collection("SaleDiscount");

                // Retrieve the documents from the child collection
                QuerySnapshot querySnapshot = await saleDiscRef.GetSnapshotAsync();

                // Create a DataTable to store the retrieved data
                DataTable saleDiscountGridViewTable = new DataTable();

                //saleDiscountGridViewTable.Columns.Add("shopName", typeof(string));
                saleDiscountGridViewTable.Columns.Add("saleDiscShopName", typeof(string));
                saleDiscountGridViewTable.Columns.Add("saleDiscImage", typeof(byte[]));
                saleDiscountGridViewTable.Columns.Add("saleDiscDesc", typeof(string));

                // Iterate through the documents and populate the DataTable
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    string saleDiscShopName = documentSnapshot.GetValue<string>("saleDiscShopName");
                    string base64String = documentSnapshot.GetValue<string>("saleDiscImage");
                    byte[] saleDiscImage = Convert.FromBase64String(base64String);
                    string saleDiscDesc = documentSnapshot.GetValue<string>("saleDiscDesc");

                    DataRow dataRow = saleDiscountGridViewTable.NewRow();

                    //dataRow["shopName"] = shopName;
                    dataRow["saleDiscShopName"] = saleDiscShopName;
                    dataRow["saleDiscImage"] = saleDiscImage;
                    dataRow["saleDiscDesc"] = saleDiscDesc;

                    saleDiscountGridViewTable.Rows.Add(dataRow);

                    // Bind the DataTable to the GridView control
                    saleDiscountGridView.DataSource = saleDiscountGridViewTable;
                    saleDiscountGridView.DataBind();
                }

            }
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
        }



        protected void saleDiscountGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(saleDiscountGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void saleDiscountGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = saleDiscountGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string saleDiscShopName = saleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscShopName"].ToString();
            string saleDiscDesc = saleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscDesc"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("~/Shopper/SalesAndDiscountDetailsPage.aspx?saleDiscShopName=" + saleDiscShopName + "&saleDiscDesc=" + saleDiscDesc);
        }

        protected void reportButton_Click(object sender, EventArgs e)
        {
            report();
        }
        public void report()
        {
            string shopName = nameLabel.Text;
            string email = emailLabel.Text;

            // Redirect to another page after a delay
            string url = "ReportShopPage.aspx?shopName=" + shopName + "&email=" + email;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 50);", true);
        }

        protected void addFavoriteButton_Click(object sender, EventArgs e)
        {
            AddFavorite();
        }

        public async void AddFavorite()
        {
            DocumentReference doc = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Favorite").Document(nameLabel.Text);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "shopName", nameLabel.Text},
                { "shopImage", imageHiddenField.Value},
                { "shopDescription", descriptionLabel.Text},
                { "email", emailLabel.Text},
                { "phoneNumber", phoneNumberLabel.Text },
                { "address", addressLabel.Text }
            };

            await doc.SetAsync(data1);
            Response.Write("<script>alert('Successfully Added Shop to the Favorites.');</script>");

            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string favID = "FAV" + randomIDNumber.ToString();

            // Specify the name of the document using a variable or a string literal
            string documentName = "Added your shop to Favorites. " + favID ;
            DocumentReference notifRef = database.Collection("Users").Document(emailLabel.Text).Collection("Notification").Document(documentName);

            Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        {"notifDetail", "Shopper " + (string)Application.Get("usernameget") + " added your shop to favorites list." },
                        {"notifImage", "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" }
                    };
            await notifRef.SetAsync(data);
        }
    }
}