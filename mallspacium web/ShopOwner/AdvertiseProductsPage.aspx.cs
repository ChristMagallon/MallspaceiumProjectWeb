using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm2
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
            shopNameTextbox.Enabled = false;

            if (!IsPostBack)
            {
                getShopName();
                getProductAdvertisement();
            }
        }

        public void getProductAdvertisement()
        {
            // Create a reference to the parent collection 
            CollectionReference usersRef = database.Collection("Users");

            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            CollectionReference productRef = docRef.Collection("Advertisement");

            // Retrieve the documents from the child collection
            QuerySnapshot querySnapshot = productRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable advertisementGridViewTable = new DataTable();

            advertisementGridViewTable.Columns.Add("adsProdId", typeof(string));
            advertisementGridViewTable.Columns.Add("adsProdName", typeof(string));
            advertisementGridViewTable.Columns.Add("adsProdImage", typeof(byte[]));
            advertisementGridViewTable.Columns.Add("adsProdDesc", typeof(string));
            advertisementGridViewTable.Columns.Add("adsProdShopName", typeof(string));
            advertisementGridViewTable.Columns.Add("adsProdDate", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                string id = documentSnapshot.GetValue<string>("adsProdId");
                string name = documentSnapshot.GetValue<string>("adsProdName");
                string base64String = documentSnapshot.GetValue<string>("adsProdImage");
                byte[] image = Convert.FromBase64String(base64String);
                string description = documentSnapshot.GetValue<string>("adsProdDesc");
                string shopName = documentSnapshot.GetValue<string>("adsProdShopName");
                string date = documentSnapshot.GetValue<string>("adsProdDate");

                DataRow dataRow = advertisementGridViewTable.NewRow();

                dataRow["adsProdId"] = id;
                dataRow["adsProdName"] = name;
                dataRow["adsProdImage"] = image;
                dataRow["adsProdDesc"] = description;
                dataRow["adsProdShopName"] = shopName;
                dataRow["adsProdDate"] = date;

                advertisementGridViewTable.Rows.Add(dataRow);
            }
            // Bind the DataTable to the GridView control
            advertisementGridView.DataSource = advertisementGridViewTable;
            advertisementGridView.DataBind();
        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        public async void search()
        {
            string searchAdvertismentName = searchTextBox.Text;

            if (searchTextBox.Text == "")
            {
                getProductAdvertisement();
            }
            else
            {
                Query query = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Advertisement")
                    .WhereGreaterThanOrEqualTo("adsProdName", searchAdvertismentName)
                    .WhereLessThanOrEqualTo("adsProdName", searchAdvertismentName + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    // Create a DataTable to store the retrieved data
                    DataTable advertisementGridViewTable = new DataTable();

                    advertisementGridViewTable.Columns.Add("adsProdId", typeof(string));
                    advertisementGridViewTable.Columns.Add("adsProdName", typeof(string));
                    advertisementGridViewTable.Columns.Add("adsProdImage", typeof(byte[]));
                    advertisementGridViewTable.Columns.Add("adsProdDesc", typeof(string));
                    advertisementGridViewTable.Columns.Add("adsProdShopName", typeof(string));
                    advertisementGridViewTable.Columns.Add("adsProdDate", typeof(string));

                    // Iterate through the documents and populate the DataTable
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        string id = documentSnapshot.GetValue<string>("adsProdId");
                        string name = documentSnapshot.GetValue<string>("adsProdName");
                        string base64String = documentSnapshot.GetValue<string>("adsProdImage");
                        byte[] image = Convert.FromBase64String(base64String);
                        string description = documentSnapshot.GetValue<string>("adsProdDesc");
                        string shopName = documentSnapshot.GetValue<string>("adsProdShopName");
                        string date = documentSnapshot.GetValue<string>("adsProdDate");

                        DataRow dataRow = advertisementGridViewTable.NewRow();

                        dataRow["adsProdId"] = id;
                        dataRow["adsProdName"] = name;
                        dataRow["adsProdImage"] = image;
                        dataRow["adsProdDesc"] = description;
                        dataRow["adsProdShopName"] = shopName;
                        dataRow["adsProdDate"] = date;

                        advertisementGridViewTable.Rows.Add(dataRow);
                    }
                    // Bind the DataTable to the GridView control
                    advertisementGridView.DataSource = advertisementGridViewTable;
                    advertisementGridView.DataBind();
                }
                else
                {
                    // Display an error message if no search results are found
                    errorMessageLabel.Text = "No results found.";
                    errorMessageLabel.Visible = true;
                    advertisementGridView.Visible = false;
                }
            }
        }

        protected void advertisementGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "adsProdImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
                else
                {
                    // If no image is available, show a default image instead
                    imageControl.ImageUrl = "/Images/no-image.jpg";
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }
        }

        protected async void advertisementGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the document ID from the DataKeys collection
            string docId = advertisementGridView.DataKeys[e.RowIndex]["adsProdId"].ToString();

            // Get a reference to the document to be deleted 
            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Advertisement").Document(docId);

            // Delete the document
            await docRef.DeleteAsync();

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Deleted Advertisement Product!');", true);

            
            // Redirect to another page after a delay
            string url = "AdvertiseProductsPage.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            AddAdvertisementProduct();
        }

        public async void AddAdvertisementProduct()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string advertisemetID = "ADSPROD" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            //Create an instance of Bitmap from the uploaded file using the FileUpload control
            Bitmap image = new Bitmap(imageFileUpload.PostedFile.InputStream);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = stream.ToArray();

            //Convert the Bitmap image to a Base64 string
            string base64String = Convert.ToBase64String(bytes);

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = database.Collection("Users");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Get the data as a Dictionary
                Dictionary<string, object> data = snapshot.ToDictionary();
                // Access the specific field you want
                string userEmail = data["email"].ToString();
                string firstName = data["firstName"].ToString();
                string lastName = data["lastName"].ToString();
                string userRole = data["userRole"].ToString();

                // Do something with the field value

                AdvertiseProductData advertiseProductData = new AdvertiseProductData
                {
                    AdsProductID = advertisemetID,
                    AdsProductName = ProductNameTextbox.Text,
                    AdsProductImage = base64String,
                    AdsProductDesc = DescriptionTextbox.Text,
                    AdsProductShopName = shopNameTextbox.Text,
                    AdsProductDate = date,
                    UserEmail = userEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    UserRole = userRole
                };

                if (ProductNameRequiredFieldValidator.IsValid && imageFileUploadValidator.IsValid && DescriptionTextboxValidator.IsValid)
                {
                    // Store the subscription data in a session variable
                    Session["AdvertiseProductData"] = advertiseProductData;
                    Response.Redirect("~/ShopOwner/AdvertiseProductPaymentSummary.aspx", false);
                }
                else
                {
                    Response.Write("<script>alert('Error Adding a Advertisment Product!');</script>");
                }
            } 
        }

        public async void getShopName()
        {
            if (!IsPostBack)
            {
                Query query = database.Collection("Users").WhereEqualTo("email", (string)Application.Get("usernameget"));
                QuerySnapshot snap = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snap.Documents)
                {
                    // Retrieve the data from the document
                    string shopName = documentSnapshot.GetValue<string>("shopName");

                    shopNameTextbox.Text = shopName;
                }
            }
        }
    }
}