using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            save();
            sysetmDowntimeActivity();
        }

        public async void save()
        {
            string datetime2 = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            DocumentReference downtimeRef = database.Collection("AdminSystemDowntime").Document("DT: " + datetime2);

            Dictionary<string, object> downtimeData = new Dictionary<string, object>
            {
                {"startTime", startDateTextbox.Text},
                {"endTime", endDateTextbox.Text},
                {"message", messageTextbox.Text}
            };
            await downtimeRef.SetAsync(downtimeData);

            Query usersQue = database.Collection("Users");
            QuerySnapshot snap = await usersQue.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {

                if (docsnap.Exists)
                {
                    // Specify the name of the document using a variable or a string literal
                    string documentName = "server down: " + startDateTextbox.Text + " - " + endDateTextbox.Text;
                    DocumentReference downtimeRef1 = database.Collection("Users").Document(docsnap.Id).Collection("Notification").Document(documentName);

                    Dictionary<string, object> downtimeData1 = new Dictionary<string, object>
                    {
                        {"notifDetail", messageTextbox.Text},
                        {"notifImage", "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" }
                    };
                    await downtimeRef1.SetAsync(downtimeData1);
                }
            }

            Response.Write("<script>alert('Successfully saved setting downtime');</script>");
        }

        public async void sysetmDowntimeActivity()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string activityID = "ACT" + randomIDNumber.ToString();

            //Get current UTC date time
            DateTime currentDate = DateTime.UtcNow;

            // Format the current date time string in the desired format
            string formattedDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            DocumentReference userRef = database.Collection("AdminActivity").Document(activityID);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", activityID },
                { "activity", (string)Application.Get("usernameget") + " sets application and website system downtime" },
                { "email", "NA" },
                { "userRole", "NA" },
                { "date", formattedDate }
            };
            await userRef.SetAsync(data1);
        }
    }
}