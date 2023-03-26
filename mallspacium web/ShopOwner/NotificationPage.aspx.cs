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
    public partial class WebForm6 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getShops();
        }

        public async void getShops()
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

    }
}