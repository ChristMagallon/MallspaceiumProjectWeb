using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm3
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                getShops();
                getUserSubDetails();
            }
        }

        public void getShops()
        {
            string role = "ShopOwner";
            Query query = database.Collection("Users").WhereEqualTo("userRole", role);
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable shopsGridViewTable = new DataTable();

            shopsGridViewTable.Columns.Add("shopName", typeof(string));
            shopsGridViewTable.Columns.Add("shopImage", typeof(byte[]));
            shopsGridViewTable.Columns.Add("shopDescription", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                string shopName = documentSnapshot.GetValue<string>("shopName");
                string base64String = documentSnapshot.GetValue<string>("shopImage");
                byte[] shopImage = Convert.FromBase64String(base64String);
                string shopDescription = documentSnapshot.GetValue<string>("shopDescription");

                DataRow dataRow = shopsGridViewTable.NewRow();

                dataRow["shopName"] = shopName;
                dataRow["shopImage"] = shopImage;
                dataRow["shopDescription"] = shopDescription;

                shopsGridViewTable.Rows.Add(dataRow);
            }
            // Bind the DataTable to the GridView control
            shopsGridView.DataSource = shopsGridViewTable;
            shopsGridView.DataBind();
        }

        protected void shopsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "shopImage");
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(shopsGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void shopsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = shopsGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string shopName = shopsGridView.DataKeys[selectedIndex].Values["shopName"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("PopularShopDetailsPage.aspx?shopName=" + shopName);
        }

        // Check the user subscription status
        public async void getUserSubDetails()
        {
            DateTime currentDate = DateTime.Now;
            bool isSubscriptionExpired = true;

            // Query the Firestore collection for a user with a specific email address
            CollectionReference subscriptionRef = database.Collection("AdminManageSubscription");
            DocumentReference docRef = subscriptionRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Get the data as a Dictionary
                Dictionary<string, object> data = snapshot.ToDictionary();

                DateTime startDate;
                DateTime endDate;

                if (DateTime.TryParseExact(data["startDate"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) &&
                    DateTime.TryParseExact(data["endDate"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                {
                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        // User subscription is still active
                        isSubscriptionExpired = false;
                    }
                }
            }

            if (isSubscriptionExpired)
            {             
                revertSubscription();
            }
        }

        // Revert the subscription back to free
        public async void revertSubscription()
        {
            string subscriptionType = "Free";
            string subscriptionPrice = "0.00";
            string status = "Expired";
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
                string userRole = data["userRole"].ToString();

                // Create a new collection reference
                DocumentReference subscriptionRef = database.Collection("AdminManageSubscription").Document(userEmail);

                // Check if the document exists
                DocumentSnapshot subscriptionSnapshot = await subscriptionRef.GetSnapshotAsync();
                if (subscriptionSnapshot.Exists)
                {
                    // Document exists, update the fields
                    Dictionary<string, object> dataUpdate = new Dictionary<string, object>
                    {
                        {"subscriptionType", subscriptionType},
                        {"price", subscriptionPrice},
                        {"startDate", "Not Available"},
                        {"endDate", "Not Available"},
                        {"status", status}
                    };
     
                    // Update the data in the Firestore document
                    await subscriptionRef.UpdateAsync(dataUpdate);
                    Response.Write("<script>alert('Your subscription has expired.');</script>");
                }
                else
                {
                    // Set the data for the new document
                    Dictionary<string, object> dataInsert = new Dictionary<string, object>
                    {
                        {"subscriptionType", subscriptionType},
                        {"price", subscriptionPrice},
                        {"userEmail", userEmail},
                        {"userRole", userRole},
                        {"startDate", "Not Available"},
                        {"endDate", "Not Available"},
                        {"status", status}
                    };

                    // Set the data in the Firestore document
                    await subscriptionRef.SetAsync(dataInsert);
                }
            }
        }
    }
}