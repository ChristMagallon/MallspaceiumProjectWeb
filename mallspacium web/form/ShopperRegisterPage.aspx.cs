using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;
using MailKit.Security;
using MimeKit;

namespace mallspacium_web.form
{
    public partial class ShopperRegisterPage : System.Web.UI.Page
    {
        FirestoreDb db;
        private static String userRole = "Shopper";

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            validateInput();
        }

        protected void LoginLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/form/LoginPage.aspx", false);
        }

        public async void validateInput()
        {
            Boolean checker = true;

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            Query query = usersRef.WhereEqualTo("email", EmailTextBox.Text);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Iterate over the results to find the user
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // Do something with the user document
                    ErrorEmailAddressLabel.Text = "Email is already registered!";
                    checker = false;
                }
                else
                {
                    ErrorEmailAddressLabel.Text = "";
                }
            }

            // Query the Firestore collection for a user with a specific username
            CollectionReference usersRef2 = db.Collection("Users");
            Query query2 = usersRef2.WhereEqualTo("username", UsernameTextBox.Text);
            QuerySnapshot snapshot2 = await query2.GetSnapshotAsync();

            // Iterate over the results to find the user
            foreach (DocumentSnapshot document2 in snapshot2.Documents)
            {
                if (document2.Exists)
                {
                    // Do something with the user document
                    ErrorUsernameLabel.Text = "Username is already taken!";
                    checker = false;
                }
                else
                {
                    ErrorUsernameLabel.Text = "";
                }
            }

            // Validate a Philippine phone number with no spaces
            bool isValidPhoneNumber = System.Text.RegularExpressions.Regex.IsMatch(PhoneNumberTextBox.Text, @"^\+63\d{10}$");
            if (!isValidPhoneNumber)
            {
                ErrorPhoneNumberLabel.Text = "Invalid phone number!";
                checker = false;
            }
            else
            {
                ErrorPhoneNumberLabel.Text = "";
            }

            // If the input values are valid proceed to complete registration
            if (checker == true)
            {
                signupUser();
            }
        }

        public async void signupUser()
        {
            String email = EmailTextBox.Text;
            string shopperImage = "";

            // Generate random ID number
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string userID = "USER" + randomIDNumber.ToString();

            // Get current date time of the account created
            DateTime currentDate = DateTime.Now;
            string dateCreated = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Capitalize first letter of each word in a string
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo ti = cultureInfo.TextInfo;

            // Generate confirmation code
            string confirmationCode = GenerateCode();

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("Users").Document(email);

            // Set the data for the new document
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"userID", userID},
                {"firstName", ti.ToTitleCase(FirstNameTextBox.Text)},
                {"lastName", ti.ToTitleCase(LastNameTextBox.Text)},
                {"dob", DOBTextBox.Text},
                {"gender", GenderDropDownList.SelectedItem.Text},
                {"phoneNumber", PhoneNumberTextBox.Text},
                {"address", ti.ToTitleCase(AddressTextBox.Text)},
                {"email", EmailTextBox.Text},
                {"username", UsernameTextBox.Text},
                {"password", PasswordTextBox.Text},
                {"confirmPassword", ConfirmPasswordTextBox.Text},
                {"userRole", userRole},
                {"shopperImage", shopperImage},
                {"dateCreated", dateCreated },
                {"confirmationCode", confirmationCode },
                {"verified", false}
            };

            // Set the data in the Firestore document
            await documentRef.SetAsync(data);

            string recipientEmail = EmailTextBox.Text;
            string recipientName = UsernameTextBox.Text;

            string smtpUserName = ConfigurationManager.AppSettings["SmtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

            // Send confirmation email
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
            smtpClient.EnableSsl = true;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUserName);
            mailMessage.To.Add(recipientEmail);
            mailMessage.Subject = "Confirm Your Registration";
            mailMessage.Body = "Dear " + recipientName + ",<br><br>" +
                           "Thank you for registering with our website! Please enter the following code to verify your account:<br><br>" +
                           "<b>" + confirmationCode + "</b><br><br>" +
                           "If you did not create an account on our website, please ignore this email.<br><br>" +
                           "Best regards,<br>" +
                           "Mallspaceium";
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);

            defaultSubscription();

            Application.Set("emailGet", EmailTextBox.Text);
            string confirmationEmailUrl = ResolveUrl("~/form/ConfirmationEmailPage.aspx");
            Response.Write("<script>alert('Account successfully registered! By doing so, you will receive important email from your registered email address.'); window.location='" + confirmationEmailUrl + "';</script>");
        }

        private string GenerateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return code;
        }

        // Default subscription
        public async void defaultSubscription()
        {
            String email = EmailTextBox.Text;
            String subscriptionType = "Free";
            String subscriptionPrice = "0.00";
            String currentDate = "n/a";
            String expirationDate = "n/a";
            String status = "Active";

            // Generate random ID number
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string subscriptionID = "SUB" + randomIDNumber.ToString();

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("AdminManageSubscription").Document(email);

            // Set the data for the new document
            Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionID},
                    {"subscriptionType", subscriptionType},
                    {"price", subscriptionPrice},
                    {"userEmail", email},
                    {"userRole", userRole},
                    {"startDate", currentDate},
                    {"endDate", expirationDate},
                    {"status", status}
                };

            // Set the data in the Firestore document
            await documentRef.SetAsync(dataInsert);
        }
    }
}