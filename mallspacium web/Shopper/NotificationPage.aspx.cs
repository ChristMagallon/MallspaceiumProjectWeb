using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.MasterForm3
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        FirestoreDb database;
        static string notificationpicker = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            checknotif();

        }

        public async void checknotif()
        {
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

                object field = data["userNotif"];

                // Do something with the field value

                if (field.ToString() == "On")
                {
                    getNotification();
                }
                else
                {

                }
            }
            else
            {
                // Document does not exist
            }
        }

        protected void NotificationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected row from the GridView
            GridViewRow selectedRow = NotificationGridView.SelectedRow;

            // Get the value of the selected column
            string selectedValue = selectedRow.Cells[0].Text;

            // Display the selected value in the Label control
            notificationpicker = selectedValue;
            getmessage();
        }

        public async void getmessage()
        {
            object field = "";

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = database.Collection("Users");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget")).Collection("Notification").Document(notificationpicker);

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Get the data as a Dictionary
                Dictionary<string, object> data = snapshot.ToDictionary();
                // Access the specific field you want
                field = data["message"];

                // Do something with the field value 
                SelectedNotificationLabel.Text = "Notification Details: " + field.ToString();
            }
            else
            {
                // Document does not exist
            }
        }

        public async void getNotification()
        {
            Query usersQue = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Notification");
            QuerySnapshot snap = await usersQue.GetSnapshotAsync();

            // Create a new DataTable to hold the data
            DataTable notificationTable = new DataTable();
            notificationTable.Columns.Add("Notification", typeof(string));


            // Loop through each document in the snapshot and add its data to the DataTable
            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                if (docsnap.Exists)
                {
                    // Convert the document data to a dictionary
                    Dictionary<string, object> data = docsnap.ToDictionary();

                    // Get the values of the fields we want to display
                    string Notification = docsnap.Id;

                    // Add the data to the DataTable
                    notificationTable.Rows.Add(Notification);
                }
            }

            // Set the DataTable as the DataSource for the GridView
            NotificationGridView.DataSource = notificationTable;

            // Bind the data to the GridView
            NotificationGridView.DataBind();
        }

        protected async void NotificationGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the document ID from the DataKeys collection
            string docId = NotificationGridView.DataKeys[e.RowIndex]["Notification"].ToString();

            // Get a reference to the document to be deleted 
            DocumentReference docRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Notification").Document(docId);

            // Delete the document
            await docRef.DeleteAsync();

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Deleted Notification!');", true);


            // Redirect to another page after a delay
            string url = "NotificationPage.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }
    }
}