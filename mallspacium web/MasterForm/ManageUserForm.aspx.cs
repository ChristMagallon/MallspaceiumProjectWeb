using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        FirestoreDb database;
        DataTable usersGridViewTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
     
            usersGridViewTable.Columns.Add("manageUser");
            usersGridViewTable.Columns.Add("manageId");
            usersGridViewTable.Columns.Add("manageRole");
            usersGridViewTable.Columns.Add("manageDate");
            usersGridViewTable.Columns.Add("manageStatus");

            getManageUsers("AdminManageUsers");
        }
        public async void getManageUsers(string AdminManageUsers)
        {
            Query usersQue = database.Collection(AdminManageUsers);
            QuerySnapshot snap = await usersQue.GetSnapshotAsync();

            

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                ManageUsers user = docsnap.ConvertTo<ManageUsers>();

                if (docsnap.Exists)
                {              
                    usersGridViewTable.Rows.Add(user.manageUser, user.manageId, user.manageRole, user.manageDate, user.manageStatus);                  
                }
            }
            manageUsersGridView.DataSource = usersGridViewTable;
            manageUsersGridView.DataBind();

        }       
    }
}