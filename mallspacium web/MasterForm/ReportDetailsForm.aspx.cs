using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class ReportDetailsForm : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                retrieveReportDetails();
            }
        }

        public async void retrieveReportDetails()
        {
            if (!IsPostBack)
            {
                // Retrieve the shop name from the query string
                string id = Request.QueryString["id"];

                // Use the shop name to retrieve the data from Firestore
                Query query = database.Collection("AdminReport").WhereEqualTo("id", id);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    // Retrieve the data from the document
                    //string shopName = documentSnapshot.GetValue<string>("shopImage");
                    string shopName = documentSnapshot.GetValue<string>("shopName");
                    string reason = documentSnapshot.GetValue<string>("reason");
                    string detailedReason = documentSnapshot.GetValue<string>("detailedReason");
                    string reportedBy = documentSnapshot.GetValue<string>("reportedBy");
                    string date = documentSnapshot.GetValue<string>("date");

                    // Display the data
                    idLabel.Text = id;
                    shopNameLabel.Text = shopName;
                    reasonLabel.Text = reason;
                    detailedReasonLabel.Text = detailedReason;
                    reportedByLabel.Text = reportedBy;
                    dateLabel.Text = date;
                }
            }
        }

        protected void viewSupportingImageButton_Click(object sender, EventArgs e)
        {
            viewSupportingImage();
        }

        public async void viewSupportingImage()
        {
            // Retrieve the shop name from the query string
            string id = Request.QueryString["id"];

            // Retrieve the data from Firestore database
            DocumentReference docRef = database.Collection("AdminReport").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            string imageString = snapshot.GetValue<string>("supportingImage");

            // Show the modal with the image
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showModal", "showModal('" + imageString + "');", true);
        }
    }
}