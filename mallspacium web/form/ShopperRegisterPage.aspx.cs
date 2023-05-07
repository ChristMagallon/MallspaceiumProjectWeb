using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
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
            string shopperImage = "/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBWRXhpZgAATU0AKgAAAAgABAEaAAUAAAABAAAAPgEbAAUAAAABAAAARgEoAAMAAAABAAIAAAITAAMAAAABAAEAAAAAAAAAAAEsAAAAAQAAASwAAAAB/9sAQwAIBgYHBgUIBwcHCQkICgwUDQwLCwwZEhMPFB0aHx4dGhwcICQuJyAiLCMcHCg3KSwwMTQ0NB8nOT04MjwuMzQy/9sAQwEJCQkMCwwYDQ0YMiEcITIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIy/8AAEQgBaAFoAwEiAAIRAQMRAf/EAB8AAAEFAQEBAQEBAAAAAAAAAAABAgMEBQYHCAkKC//EALUQAAIBAwMCBAMFBQQEAAABfQECAwAEEQUSITFBBhNRYQcicRQygZGhCCNCscEVUtHwJDNicoIJChYXGBkaJSYnKCkqNDU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6g4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2drh4uPk5ebn6Onq8fLz9PX29/j5+v/EAB8BAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKC//EALURAAIBAgQEAwQHBQQEAAECdwABAgMRBAUhMQYSQVEHYXETIjKBCBRCkaGxwQkjM1LwFWJy0QoWJDThJfEXGBkaJicoKSo1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoKDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uLj5OXm5+jp6vLz9PX29/j5+v/aAAwDAQACEQMRAD8A6CiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACirNpY3V/JstYHkI6kDgfUngV0Vl4KkYBr25CeqRDJ/M8foaAOUpMgdSPzr0eDwvpNuP+PbzSO8rFv06fpWhHYWcQxHawIPaMD+lAHlOR6/rSZHYj869c8mPGPLTHptFRSWFnKMSWsDj3jB/pQB5TRXo0/hfSZx/wAe3lE94mK/p0/SsS98FSqC1lch/RJRg/mOP0FAHKUVZu7C6sJNl1A8ZPQkcH6EcGq1ABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUVJDDJcTJDEheRzhVFADFRnYIilmY4AAySfQCut0jwgSFm1I4zyIFP8A6ER/IfnWtoegQ6VGJZQsl0w5fHC+y/49TW3QBFDDFbxLFDGscajhVGAKloooAKKKKACiiigAooooAimhiuImimjWSNhyrDINcnq/hAgNNppzjkwMf/QSf5H867GigDyFkZGKOpVlOCCMEH0IpK9E1zQIdVjMsQWO6UcPjhvZv8eorz6aGS3meGZCkiHDKaAGUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAYJIABOTgADk16B4c0RdNtxPMo+1SD5v9gf3R/X/61YnhHSRc3RvplzHCcRgjgv6/h/Mj0ruqACiiigAooooAKKKKACiiigAooooAKKKKACsDxHoi6lbmeFR9qjHy/wC2P7p/p/8AXrfooA8gwQSCCCOCD1FFdL4u0kW10L6FcRzHEgA4D+v4/wAwfWuaoAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACnIjSyLGgyzEKo9STgCm1ueE7T7TraORlYFMhz69B+pz+FAHcafZpYWENqn/LNQCfU9z+Jq3RRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAVNQs0v7Ca1f/AJaKQD6HsfwNeWyI0UjRuMMpKsPQg4Neu1554stPs2tu4GFnUSDHr0P6jP40AYdFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABXaeCYcWt1Pj70gQH2Az/ADNcXXoPhBNugof70jn9cf0oA3qKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigArlPG0ObW1nx92QoT7EZ/mK6usHxem7QXP8AdkQ/rj+tAHn1FFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABXonhMg+H4fZnH/jxrzuu88GyhtGePPMczDH1wf8aAOjooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACsTxYQPD83uyD/AMeFbdc54zlCaMkeeZJlGPpk/wCFAHB0UUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFdV4JuQt1c2xI+dRIPqOD+hH5VytXdJvP7P1S3uTwqth/wDdPB/Q5/CgD1KikBBGQcg+lLQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFcV42ud91bWwx+7UufqTgfoD+ddoSAMk4A9a8u1a8+36pcXIOVZsJ/ujgfoM/jQBSooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAPQPCupC90wQO2ZrfCnJ6r2P5cfhW/Xlml6hJpd/HcpkgcOmfvKeo/qPcCvTba4iureOeFg0cgyp9RQBNRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRUNzcRWtvJPMwWOMZY+goAxvFWpCy0wwI2JrjKjB6L3P5cfjXn9XNU1CTVL+S5fIB4RM/dUdB/U+5NU6ACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAK3PD2utpc3kzEtaSHkdSh9R7eo/H64dFAHriSLKivGwZWGQQcgin15zoniCbSmEb5ltSeUzyvuv+HQ+1d7Z3tvfQCa2lDoe47exHY0AWaKKKACiiigAooooAKKKKACiiq15e29jAZrmUIg7nv7AdzQBM8ixIzyMFVRkknAArz/xDrrapN5MJK2kZ4HQufU+3oPx+jNb8QTaqxjTMVqDwmeW92/w6D3rGoAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACp7S9uLGcTW0zRv3x0PsR0I+tQUUAdrp3jKGQBL+Pyn6eYgJU/UdR+tdJb3MF1H5kEySqe6MDXk1PilkgkDxSPGw/iRiD+YoA9corzi38UatAAPtAlA7SqD+owf1rQTxtdj/AFlpA3+6xH+NAHb0Vxv/AAnL/wDPgv8A39P+FRP42uz/AKu0gX/eYn/CgDt6huLmC1j8yeZIlHd2Arz648UatOCPtAiB7RKB+pyf1rKllknkLyyPIx/idiT+ZoA7LUfGUMYKWEfmv08xwQo+g6n9K5G7vbi+nM1zM0j9s9B7AdAPpUFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRWtpvh2/1LDhPJhP/AC0kBGR7Dqf0HvQBk1estHv9Qx9nt3ZD/G3yr+Z6/hmu20/wzp9jhmj+0Sj+OQcD6DoK2gABigDj7bwSxTN1eYYjhYlyB9Sev5CsHU9Fu9Kk/fJuhJwsqj5T9fQ+x/WvT6Y6LKjI6hlYYIIyDQB5HRXcaj4PtrgtJZP9nf8AuHlD9O4/Ue1cxeaFqViSZbVmQfxxfMv6cj8QKAM6ijvjuO1FABRRRQAUUd8dz2rRs9C1K+IMVsyof45BtX9eT+ANAGdWhpmi3eqyfuU2wg4aVh8o+nqfYfpXUad4PtrcrJev9of+4OEH17n9B7V0iIsSKiKFVRgADAFAHI3PglgmbW8ywHKyrgH6EdPyNc/e6Pf6fn7RbuqD+NfmX8x0/HFeo0hAIxQB5DRXomoeGdPvsssf2eU/xxjg/UdDXI6l4dv9Ny5TzoR/y0jBOB7jqP1HvQBk0UUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAVasrC51GbyraMue56BR7ntWlovhufU9s826K1zkN/E/09vf8s13draQWUCw28SxoOgUdfr6mgDH0rwvaWAWW4AuLjrlh8qn2H+NdBRRQAUUUUAFFFFABRRRQBVuNPs7v/X2sUme7ICfzqhJ4W0h+RalD/sSMP61s0UAYP/CIaT/cm/7+mpY/C2kJybUuf9uRj/WtmigCrb6fZ2uPItYo/dUGfzq1RRQAUUUUAFFFFABRRRQBz+q+F7S/DS24FvcdcqPlY+4/wrir2wudOm8q5jKHseoYex716rVe6s4L2BobiJZEPUMOnuPQ0AeUUVua14bn0zdPDultc5LfxJ9fb3/PFYdABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABXVeH/DHnbbzUExGcFISPvehb29u/f0qTw34d3bL++TjrFEw/JiP5D8fSuxoAQAAYAwB0paKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAQgEYIyD1ri/EHhjyd15p6ZjGS8IH3fUr7e3bt6V2tFAHkFFdd4k8O7d9/Ypx1liUfmwH8x+PrXI0AFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABXSeGNB+2OL26T/R0P7tSPvkd/oP1P0rO0PSX1a+EfIgTDSsOw7Ae5/xNekRxJDEscahUUAKo6ACgCSiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAK4TxPoP2Nze2qf6O5/eKB9wnv9D+h+td3UckSTRNHIoZGBDKehBoA8korT1zSX0m+MfJgfLRMe47g+4/wNZlABRRRQAUUUUAFFFFABRRRQAUUUUAFOjjeaVYo1LO5Cqo6kngCm11ng7TN7tqMq8KdkQPr3P4dPzoA6LR9NTS7BIBgyH5pGH8TH+nYewrRoooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAztY01NUsHgOBIPmjY/wsP6dj7GvM5I3hlaKRSroSrKeoI4Ir12uM8Y6ZsddRiXhjslA9ex/Hp+VAHJ0UUUAFFFFABRRRQAUUUUAFFFFAE1tbvd3UVvHy8jBR7Z7/h1/CvUbS2js7SK3iGEjXaP8fxNcj4MsPMuJr5xxGPLjJ/vEc/kMD8a7agAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKr3dtHeWktvKMpIpU/5+tWKKAPJrm3e0upbeTh42Kn3x3/Hr+NQ11PjOw8u4hvkHEg8uQj1A4/MZH4Vy1ABRRRQAUUUUAFFFFABRRV7RrX7ZrFrARlS4Zh7Dk/yx+NAHoGiWX2DSLeEjD7dz/wC8eT/OtGiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAM7W7L7fpFxCBl9u5P94cj+VeY16/Xl2s2v2PWLqADChyyj2PI/nj8KAKNFFFABRRRQAUUUUAFdR4Kt999cXBHEaBQfQsf8B+tcvXd+DINmkSS45llPPsMD/GgDpKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAK4jxrb7L63uAOJEKk+pU/4H9K7eub8Zwb9IjlxzFKOfY5H+FAHCUUUUAFFFFABRRRQAV6V4bj8rw/Zj1QsfxJP9a816An2r1TTEEelWiDtCg/QUAW6KKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKyvEkfm+H7weiBh+BB/pWrVTU0EmlXaHvC4/Q0AeV0UdQD7UUAFFFFABRRRQAh+6foa9atRi0hHoij9BRRQBNRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABUN0M2kw9UYfoaKKAPJR90fQUtFFABRRRQB//9k=";

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
                {"userNotif", true },
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
            String subscriptionType = "Free Subscription";
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