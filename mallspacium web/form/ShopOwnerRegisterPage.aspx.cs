using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class ShopOwnerRegisterPage : System.Web.UI.Page
    {
        FirestoreDb db;
        private static String userRole = "ShopOwner";

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
                    Response.Write("<script>alert('Email is already registered!');</script>");
                    checker = false;
                }
            }

            // Query the Firestore collection for a specific shop name
            CollectionReference usersRef2 = db.Collection("Users");
            Query query2 = usersRef2.WhereEqualTo("shopName", ShopNameTextBox.Text);
            QuerySnapshot snapshot2 = await query2.GetSnapshotAsync();

            // Iterate over the results to find the if the shop name is already taken
            foreach (DocumentSnapshot document2 in snapshot2.Documents)
            {
                if (document2.Exists)
                {
                    // Do something with the registered document
                    ErrorShopNameLabel.Text = "Shop name is already taken!";
                    checker = false;
                }
                else
                {
                    ErrorShopNameLabel.Text = "";
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
            String shopOwnerImage = "/9j/4AAQSkZJRgABAQEAYABgAAD/4QBaRXhpZgAATU0AKgAAAAgABQMBAAUAAAABAAAASgMDAAEAAAABAAAAAFEQAAEAAAABAQAAAFERAAQAAAABAAAOxFESAAQAAAABAAAOxAAAAAAAAYagAACxj//bAEMACAYGBwYFCAcHBwkJCAoMFA0MCwsMGRITDxQdGh8eHRocHCAkLicgIiwjHBwoNyksMDE0NDQfJzk9ODI8LjM0Mv/bAEMBCQkJDAsMGA0NGDIhHCEyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMv/AABEIAgACAAMBIgACEQEDEQH/xAAfAAABBQEBAQEBAQAAAAAAAAAAAQIDBAUGBwgJCgv/xAC1EAACAQMDAgQDBQUEBAAAAX0BAgMABBEFEiExQQYTUWEHInEUMoGRoQgjQrHBFVLR8CQzYnKCCQoWFxgZGiUmJygpKjQ1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4eLj5OXm5+jp6vHy8/T19vf4+fr/xAAfAQADAQEBAQEBAQEBAAAAAAAAAQIDBAUGBwgJCgv/xAC1EQACAQIEBAMEBwUEBAABAncAAQIDEQQFITEGEkFRB2FxEyIygQgUQpGhscEJIzNS8BVictEKFiQ04SXxFxgZGiYnKCkqNTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqCg4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2dri4+Tl5ufo6ery8/T19vf4+fr/2gAMAwEAAhEDEQA/APf6KKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiimSSRwxPLK6pGilmdjgKB1JPpQA+ivDfGXxmu5riax8NBYLdSV+2suXk91B4Ue5yfpXms2v63ezmSbVb+aVu7XDk/zoA+vKK+Qf7R1n/n8v/8Av6/+NH9o6z/z+X//AH9f/GnyvsK6Pr6ivkH+0dZ/5/L/AP7+v/jR/aOs/wDP5f8A/f1/8aOV9guj6+or5B/tHWf+fy//AO/r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AFfrXXV7OAhy0ubucOIleduwUUUV3GAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFee6zb/ZdXuIwMKW3L9DzXoVcn4ut8T29yB95SjfhyP5n8q4sfDmpX7G+Hladu59HeDdW/tvwfpWoFtzyW6iQ+rr8rfqDW5XlXwM1b7R4ev9LdstaTiRR/sOP8Vb869VrxTuCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAK+ePjVq327xqtirZjsIFQj/bb5j+hX8q+hXdY0Z3IVVGST2FfImtX8mu+I72+wd15cs6g9gzcD8BgUAdP4Zt/I0dHIw0rFz/IfyrYqOCJYLeOFfuooUfhUlfSU4ckFHseXKXNJsKKKKsQUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFZPiO3+0aNKQMtERIPw6/oTWtTJY1lieNhlXUqfoaipDng49xxfK0yl8HNW/s/x3HbM2I76F4TnpuHzL/6Dj8a+jq+P9Pu5dE122u0/1tlcq+PUq2cfpX15BNHc28c8Tbo5EDow7gjIr5tqx6hJRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAcr8SNW/sfwFqkyttkli+zx+uX+Xj6Ak/hXzd4et/tGsw5GVjzIfw6frivWPjvq2220rR0b77NcyD6fKv8AN/yrz7whb4juLkjqRGPw5P8AMV0YWHPVSM6suWDOmooor3zzgooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigDhfElv5GsyMB8soDj+R/UV9D/C7Vv7W+H+nMzbpLZTav7bOF/wDHdteHeLrfdbQXIHKMUP0P/wCr9a7j4D6tiXVtHduoW6jX6fK380rwMXDkrNHoUZXgj2qiiiuc1CiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiqmp30el6VeX8v+rtoXmb6KCf6UAfN3xT1b+1fiBqBVsx2pFqnts+9/48Wq9o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P7X1m5/df2jfy5/g892z+Ga+vqKAPk/SvBPiXWZAllo10R3kkTy0H/AmwK9Y8H/Bi10+SK+8RSpd3CnctrH/AKpT/tE/e+nA+tesUUAIAAAAAAOABS0UUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFAH/9k=";

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
            DocumentReference documentRef = db.Collection("ShopOwnerRegistrationApproval").Document(email);

            if (ImageFileUpload.HasFile)
            {
                //Create an instance of Bitmap from the uploaded file using the FileUpload control
                Bitmap image = new Bitmap(ImageFileUpload.PostedFile.InputStream);
                MemoryStream stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] bytes = stream.ToArray();

                //Convert the Bitmap image to a Base64 string
                string base64String = Convert.ToBase64String(bytes);

                // Set the data for the new document
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    {"userID", userID},
                    {"firstName", ti.ToTitleCase(FirstNameTextBox.Text)},
                    {"lastName", ti.ToTitleCase(LastNameTextBox.Text)},
                    {"shopImage", shopOwnerImage},
                    {"shopName", ShopNameTextBox.Text},
                    {"shopDescription", ti.ToTitleCase(ShopDescriptionTextBox.Text)},
                    {"permitImage", base64String},
                    {"email", EmailTextBox.Text},
                    {"phoneNumber", PhoneNumberTextBox.Text},
                    {"address", ti.ToTitleCase(AddressTextBox.Text)},
                    {"username", UsernameTextBox.Text},
                    {"password", PasswordTextBox.Text},
                    {"confirmPassword", ConfirmPasswordTextBox.Text},
                    {"userRole", userRole},
                    {"dateCreated", dateCreated },
                    {"certifiedShopOwner", false },
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