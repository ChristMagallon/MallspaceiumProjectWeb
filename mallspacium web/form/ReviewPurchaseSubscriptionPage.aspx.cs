using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ReviewPurchaseSubscriptionPage : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");

            retrieveSubscriptionData();
        }

        private void retrieveSubscriptionData()
        {
            // Retrieve the subscription data from the session variable
            SubscriptionData subscriptionData = (SubscriptionData)Session["SubscriptionData"];

            // Use the subscription data as needed
            string subscriptionType = subscriptionData.SubscriptionType;
            string price = subscriptionData.Price;
            string userEmail = subscriptionData.UserEmail;

            SubscriptionTypeLabel.Text = subscriptionType;
            SubscriptionPriceLabel.Text = price;
            UserEmailLabel.Text = userEmail;
        }

        protected void PurchaseButton_Click(object sender, EventArgs e)
        {
            if (!AgreementTermsCheckBox.Checked)
            {
                // Display an error message
                Response.Write("<script>alert('Please agree to the terms and conditions.');</script>");
                return;
            }

            // Checkbox is checked, proceed the next webform
            Response.Redirect("~/form/CheckoutSummaryPage.aspx", false);
        }
    }
}