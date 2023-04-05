using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2;
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
        protected void addProofNoteButton_Click(object sender, EventArgs e)
        {
            addProofNote();
            addProofNoteActivity();

        }

        protected void viewSupportingImageButton_Click(object sender, EventArgs e)
        {
            viewSupportingImage();
        }

        public async void retrieveReportDetails()
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
                string shopName = documentSnapshot.GetValue<string>("shopName");
                string reason = documentSnapshot.GetValue<string>("reason");
                string detailedReason = documentSnapshot.GetValue<string>("detailedReason");
                string reportedBy = documentSnapshot.GetValue<string>("reportedBy");
                string date = documentSnapshot.GetValue<string>("date");
                string status = documentSnapshot.GetValue<string>("status");
                string image = documentSnapshot.GetValue<string>("supportingImage");
                string email = documentSnapshot.GetValue<string>("email");

                // Display the data
                idLabel.Text = id;
                shopNameLabel.Text = shopName;
                reasonLabel.Text = reason;
                detailedReasonLabel.Text = detailedReason;
                reportedByLabel.Text = reportedBy;
                dateLabel.Text = date;
                statusLabel.Text = status;
                imageHiddenField.Value = image;
                emailHiddenField.Value = email;
            }

            // Use the shop name to retrieve the data from Firestore
            Query roleQuery = database.Collection("Users").WhereEqualTo("email", emailHiddenField.Value);
            QuerySnapshot roleSnapshot = await roleQuery.GetSnapshotAsync();

            // Loop through the documents in the query snapshot
            foreach (DocumentSnapshot roleDocumentSnapshot in roleSnapshot.Documents)
            {
                // Retrieve the data from the document
                string userRole = roleDocumentSnapshot.GetValue<string>("userRole");
                userRoleHiddenField.Value = userRole;
            }

            CollectionReference usersRef = database.Collection("AdminReport").Document(idLabel.Text).Collection("ReportStatus");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable reportstatusGridViewTable = new DataTable();

            reportstatusGridViewTable.Columns.Add("noteId", typeof(string));
            reportstatusGridViewTable.Columns.Add("note", typeof(string));
            reportstatusGridViewTable.Columns.Add("date", typeof(string));

            foreach (DocumentSnapshot docsnap in querySnapshot.Documents)
            {
                string noteId = docsnap.GetValue<string>("noteId");
                string note = docsnap.GetValue<string>("note");
                string date = docsnap.GetValue<string>("date");

                DataRow dataRow = reportstatusGridViewTable.NewRow();

                dataRow["noteId"] = noteId;
                dataRow["note"] = note;
                dataRow["date"] = date;

                reportstatusGridViewTable.Rows.Add(dataRow);
            }
            reportstatusGridView.DataSource = reportstatusGridViewTable;
            reportstatusGridView.DataBind();

        }


        public async void addProofNote()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string noteID = "NOTE" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            if (proofFileUpload.HasFile)
            {
                //Create an instance of Bitmap from the uploaded file using the FileUpload control
                Bitmap image = new Bitmap(proofFileUpload.PostedFile.InputStream);
                MemoryStream stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] bytes = stream.ToArray();

                //Convert the Bitmap image to a Base64 string
                string base64String = Convert.ToBase64String(bytes);

                DocumentReference userRef1 = database.Collection("AdminReport").Document(idLabel.Text);
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                    { "id", idLabel.Text },
                    { "shopName", shopNameLabel.Text },
                    { "reason", reasonLabel.Text},
                    { "detailedReason", detailedReasonLabel.Text },
                    { "supportingImage", imageHiddenField.Value },
                    { "reportedBy", reportedByLabel.Text},
                    { "date", dateLabel.Text },
                    { "status", statusDropDownList.SelectedItem.Text },
                    { "email", emailHiddenField.Value }
                };
                await userRef1.SetAsync(data1);

                DocumentReference userRef2 = database.Collection("AdminReport").Document(idLabel.Text).Collection("ReportStatus").Document(noteID);
                Dictionary<string, object> data2 = new Dictionary<string, object>()
                {
                    { "noteId", noteID },
                    { "note", noteTextBox.Text },
                    { "date", date },
                    { "proofImage", base64String},
                    { "status", statusDropDownList.SelectedItem.Text }
                };
                await userRef2.SetAsync(data2);

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Added Proof and Note!');", true);

                // Redirect to another page after a delay
                string url = "ReportsForm.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }

        public async void addProofNoteActivity()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string activityID = "ACT" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            DocumentReference userRef = database.Collection("AdminActivity").Document(activityID);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", activityID },
                { "activity", (string)Application.Get("usernameget") + " send warning message to user " + emailHiddenField.Value},
                { "email", emailHiddenField.Value },
                { "userRole", userRoleHiddenField.Value },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }

        public void viewSupportingImage()
        {
            string id = idLabel.Text;

            // Get the URL for the webform with the image control
            string url = "PopUpSupportingImage.aspx?id=" + id;

            // Set the height and width of the popup window
            int height = 800;
            int width = 800;

            // Open the popup window
            string script = $"window.open('{url}&height={height}&width={width}', '_blank', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
        }

        protected void reportstatusGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reportId = idLabel.Text;

            // Get the index of the selected row
            int selectedIndex = reportstatusGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string noteId = reportstatusGridView.DataKeys[selectedIndex].Values["noteId"].ToString();

            // Get the URL for the webform with the image control
            string url = "PopUpReportProofImage.aspx?noteId=" + noteId + "&id=" + reportId;

            // Set the height and width of the popup window
            int height = 800;
            int width = 800;

            // Open the popup window
            string script = $"window.open('{url}&height={height}&width={width}', '_blank', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
        }

        protected void reportstatusGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(reportstatusGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view proof.";
            }
        }
    }
}