using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class SubscriptionPaymentApprovalDetails : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                getSubscriptionPaymentDetails();
            }
        }

        public async void getSubscriptionPaymentDetails()
        {
            // Retrieve the shop name from the query string
            string userEmail = Request.QueryString["userEmail"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("SubscriptionPaymentApproval").WhereEqualTo("userEmail", userEmail);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot subDoc = snapshot.Documents.FirstOrDefault();
            if (subDoc != null)
            {
                // Retrieve the data from the document
                string transID = subDoc.GetValue<string>("transactionID");
                string email = subDoc.GetValue<string>("userEmail");
                string fname = subDoc.GetValue<string>("firstName");
                string lname = subDoc.GetValue<string>("lastName");
                string role = subDoc.GetValue<string>("userRole");
                string subID = subDoc.GetValue<string>("subscriptionID");
                string subType = subDoc.GetValue<string>("subscriptionType");
                string price = subDoc.GetValue<string>("price");
                string payment = subDoc.GetValue<string>("paymentFile");

                // Display the data
                transactionIdLabel.Text = transID;
                emailLabel.Text = email;
                firstNameLabel.Text = fname;
                lastNameLabel.Text = lname;
                userRoleLabel.Text = role;
                subscriptionIdLabel.Text = subID;
                subscriptionTypeLabel.Text = subType;
                priceLabel.Text = price;
                imageHiddenField.Value = payment;
            }
            else
            {
                Response.Write("<script>alert('Error: Subscription Not Found.');</script>");
            }
            
        }

        protected void viewpaymentDetailsButton_Click(object sender, EventArgs e)
        {
            viewPaymentImage();
        }

        public void viewPaymentImage()
        {
            string userEmail = emailLabel.Text;

            // Get the URL for the webform with the image control
            string url = "PopUpSubscriptionPaymentImage.aspx?userEmail=" + userEmail;

            // Set the height and width of the popup window
            int height = 800;
            int width = 800;

            // Open the popup window
            string script = $"window.open('{url}&height={height}&width={width}', '_blank', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
        }

        protected void approveButton_Click(object sender, EventArgs e)
        {

        }

        protected void disapproveButton_Click(object sender, EventArgs e)
        {

        }
    }
}