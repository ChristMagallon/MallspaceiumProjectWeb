using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.Shopper
{
    public partial class PopularShopDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            // Retrieve the "shopName" value from the query string
            string shopName = Request.QueryString["shopName"];

            label.Text = shopName + " Details";

            if (!IsPostBack)
            {
                retrieveShopDetails();
                getShopProducts();
                getShopSaleDiscount();
            }
        }

        public async void retrieveShopDetails()
        {
            if (!IsPostBack)
            {
                // Retrieve the shop name from the query string
                string shopName = Request.QueryString["shopName"];

                // Use the shop name to retrieve the data from Firestore
                Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    // Retrieve the data from the document
                    string image = documentSnapshot.GetValue<string>("shopImage");
                    string name = documentSnapshot.GetValue<string>("shopName");
                    string desc = documentSnapshot.GetValue<string>("shopDescription");
                    string email = documentSnapshot.GetValue<string>("email");
                    string phoneNumber = documentSnapshot.GetValue<string>("phoneNumber");
                    string address = documentSnapshot.GetValue<string>("address");

                    // Convert the image string to a byte array
                    byte[] imageBytes;
                    if (string.IsNullOrEmpty(image))
                    {
                        // If the image string is null or empty, use the default image instead
                        imageBytes = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                    }
                    else
                    {
                        imageBytes = Convert.FromBase64String(image);
                    }

                    // Set the image in the FileUpload control
                    string imageBase64String = Convert.ToBase64String(imageBytes);
                    string imageSrc = $"data:image/png;base64,{imageBase64String}";
                    Image1.ImageUrl = imageSrc;


                    // Retrieve the user's email from the Application object
                    string userEmail = (string)Application.Get("usernameget");
                    string reportEmail = email;


                    if (userEmail == reportEmail)
                    {
                        // Disable the report button if the emails match
                        reportButton.Enabled = false;
                    }
                    else
                    {
                        // Enable the report button if the emails do not match
                        reportButton.Enabled = true;
                    }

                    // Display the data
                    nameLabel.Text = name;
                    descriptionLabel.Text = desc;
                    emailLabel.Text = email;
                    phoneNumberLabel.Text = phoneNumber;
                    addressLabel.Text = address;
                    imageHiddenField.Value = image;
                }
            }
        }

        public async void getShopProducts()
        {
            // Retrieve the shop name from the query string
            string shopName = Request.QueryString["shopName"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Product collection from the User document
                CollectionReference productRef = userDoc.Reference.Collection("Product");
                QuerySnapshot productSnapshot = await productRef.GetSnapshotAsync();

                if (productRef != null)
                {
                    // Create a DataTable to store the retrieved data
                    DataTable ownProductsGridViewTable = new DataTable();

                    ownProductsGridViewTable.Columns.Add("prodName", typeof(string));
                    ownProductsGridViewTable.Columns.Add("prodImage", typeof(byte[]));
                    ownProductsGridViewTable.Columns.Add("prodDesc", typeof(string));
                    ownProductsGridViewTable.Columns.Add("prodShopName", typeof(string));

                    // Iterate through the documents and populate the DataTable
                    foreach (DocumentSnapshot documentSnapshot in productSnapshot.Documents)
                    {

                        string productName = documentSnapshot.GetValue<string>("prodName");
                        string base64String = documentSnapshot.GetValue<string>("prodImage");
                        byte[] productImage = Convert.FromBase64String(base64String);
                        string productDescription = documentSnapshot.GetValue<string>("prodDesc");
                        string productShopName = documentSnapshot.GetValue<string>("prodShopName");

                        DataRow dataRow = ownProductsGridViewTable.NewRow();

                        dataRow["prodName"] = productName;
                        dataRow["prodImage"] = productImage;
                        dataRow["prodDesc"] = productDescription;
                        dataRow["prodShopName"] = productShopName;

                        ownProductsGridViewTable.Rows.Add(dataRow);
                    }
                    // Bind the DataTable to the GridView control
                    productGridView.DataSource = ownProductsGridViewTable;
                    productGridView.DataBind();
                }
            }
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
        }
        protected void productGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "prodImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(productGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void productGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = productGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string prodShopName = productGridView.DataKeys[selectedIndex].Values["prodShopName"].ToString();
            string prodName = productGridView.DataKeys[selectedIndex].Values["prodName"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("~/Shopper/ProductDetailsPage.aspx?prodShopName=" + prodShopName + "&prodName=" + prodName, false);
        }

        public async void getShopSaleDiscount()
        {
            // Retrieve the shop name from the query string
            string shopName = Request.QueryString["shopName"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Sale Discount collection from the User document
                CollectionReference saleDiscRef = userDoc.Reference.Collection("SaleDiscount");

                // Retrieve the documents from the child collection
                QuerySnapshot querySnapshot = await saleDiscRef.GetSnapshotAsync();

                // Create a DataTable to store the retrieved data
                DataTable saleDiscountGridViewTable = new DataTable();

                //saleDiscountGridViewTable.Columns.Add("shopName", typeof(string));
                saleDiscountGridViewTable.Columns.Add("saleDiscShopName", typeof(string));
                saleDiscountGridViewTable.Columns.Add("saleDiscImage", typeof(byte[]));
                saleDiscountGridViewTable.Columns.Add("saleDiscDesc", typeof(string));

                // Iterate through the documents and populate the DataTable
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    string saleDiscShopName = documentSnapshot.GetValue<string>("saleDiscShopName");
                    string base64String = documentSnapshot.GetValue<string>("saleDiscImage");
                    byte[] saleDiscImage = Convert.FromBase64String(base64String);
                    string saleDiscDesc = documentSnapshot.GetValue<string>("saleDiscDesc");

                    DataRow dataRow = saleDiscountGridViewTable.NewRow();

                    //dataRow["shopName"] = shopName;
                    dataRow["saleDiscShopName"] = saleDiscShopName;
                    dataRow["saleDiscImage"] = saleDiscImage;
                    dataRow["saleDiscDesc"] = saleDiscDesc;

                    saleDiscountGridViewTable.Rows.Add(dataRow);

                    // Bind the DataTable to the GridView control
                    saleDiscountGridView.DataSource = saleDiscountGridViewTable;
                    saleDiscountGridView.DataBind();
                }

            }
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
        }



        protected void saleDiscountGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "saleDiscImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(saleDiscountGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void saleDiscountGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = saleDiscountGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string saleDiscShopName = saleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscShopName"].ToString();
            string saleDiscDesc = saleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscDesc"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("~/Shopper/SalesAndDiscountDetailsPage.aspx?saleDiscShopName=" + saleDiscShopName + "&saleDiscDesc=" + saleDiscDesc);
        }

        protected void reportButton_Click(object sender, EventArgs e)
        {
            report();
        }
        public void report()
        {
            string shopName = nameLabel.Text;
            string email = emailLabel.Text;

            // Redirect to another page after a delay
            string url = "ReportShopPage.aspx?shopName=" + shopName + "&email=" + email;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 50);", true);
        }

        protected void addFavoriteButton_Click(object sender, EventArgs e)
        {
            AddFavorite();
        }

        public async void AddFavorite()
        {
            DocumentReference doc = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Favorite").Document(nameLabel.Text);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "shopName", nameLabel.Text},
                { "shopImage", imageHiddenField.Value},
                { "shopDescription", descriptionLabel.Text},
                { "email", emailLabel.Text},
                { "phoneNumber", phoneNumberLabel.Text },
                { "address", addressLabel.Text }
            };

            await doc.SetAsync(data1);
            Response.Write("<script>alert('Successfully Added Shop to the Favorites.');</script>");

            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string favID = "FAV" + randomIDNumber.ToString();

            // Get current date time 
            DateTime currentDate = DateTime.UtcNow;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Specify the name of the document using a variable or a string literal
            string documentName = "Added your shop to Favorites. " + favID ;
            DocumentReference notifRef = database.Collection("Users").Document(emailLabel.Text).Collection("Notification").Document(documentName);

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"notifDetail", "Shopper " + (string)Application.Get("usernameget") + " added your shop to favorites list." },
                {"notifImage", "iVBORw0KGgoAAAANSUhEUgAAAgAAAAIACAYAAAD0eNT6AAAAAXNSR0IArs4c6QAAAARzQklUCAgICHwIZIgAACAASURBVHic7d15vFXz4v/x99rDGRKFUhmTSnFlqpAhJDKXqZRryHXjEpLrd3Gv+Royc1GGm1m6kTTJkLGSOQmVBhKaiFKdc/be6/fH+h4qDXuvvdb+rL0/r+fjcR516bP22+ncvd77sz7rsxzXdV0BAACrxEwHAAAAhUcBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAsRAEAAMBCFAAAACxEAQAAwEIUAAAALEQBAADAQhQAAAAslDAdACVo4UJp/nzphx+kb7/1fv/dd97XihUbH59MSo0bS9ttJ227rbT99lKTJt7vGzYMPz/gx8KF3s/7999L33zj/b72f6dSGx+/ySbS1lt7X9ts4/28N2ni/b5Bg/DzwzoUAPj31VfSe+9JH37ofc2bJ339dfiv26yZtMMO0p/+JLVqJe2yi9SmjVSvXvivDbv99JP06afStGnS9OnSZ595J/s5c8J/7aZNvTLctq331b69988AnxzXdV3TIVAEli+XJk6U3n9fmjTJ+/r5Z9Op1tS8ubTPPtL++0v77SfttpvpRCh2H3/s/ay//bY0ebJ3so+SzTf//ee9fXvv579OHdOpUCQoAFi3FSuk117zvt54w/ukU2zq1pUOOMD76tBBOugg04kQZStXeiV34kRpwgTv12wuWUVNmzZS585Sp07S4YebToMIowDgd999Jz3zjDR2rHfSL0Wnniqdcop07LGmkyAqhg71fu5HjjSdJHgVFVLHjtJRR0ndu7OWAGugANhu4ULp6aelYcOkd981naZw6teXevaUevTwZgdgl/HjvZP+0KHSr7+aTlM4hx7qFYETTvAuH8BqFAAbua40Zoz00EPer5mM6URm7bSTdNpp0umne4sLUZpmzJAefdQrvN9+azqNWWVlXgk491zvEhmsRAGwycKF0iOPeCf+qC1miopOnaR+/aQuXUwnQVCGD5fuvlt65x3TSaJpl12k887zSvCmm5pOgwKiANhgwgTpgQekIUNMJykezZtLl1wi/fnPUmWl6TTI1S+/SIMHS3fd5d2eio2rrJR69ZIuvNArBSh5FIBS9uij0p13evcsw58GDaQrr5T69jWdBNlYscL7tH/rrV4JgD8dO3pF4PjjTSdBiCgApWjwYOnf/5bmzjWdpHRsvbV0xRXeNVNEz8qV0qBB0k03SUuWmE5TOvbYQ/rnP6WuXU0nQQgoAKXkoYek66/3budDOLbdVrrqKql3b9NJUOv++6XrrpMWLzadpHS1auWVK26fLSkUgFLwxhvSxRcX52Y9xWrnnaUbbpC6dTOdxF5PPildfXVhtp+Gp1Mn77IiawRKAgWgmM2Z4y1UK8UNTIrFIYdI990ntWxpOok9pkzxVq2/957pJPb661+9y4zsJVDUeBxwMXJdr4W3acPJ37TXX5d2313617+kVatMpyltv/ziLUxr146Tv2kPPijtuqv04oumkyAPzAAUm5kzvft1P/zQdBKsbfvtpYED2X89DEOGeLNdCxeaToK1nXSSdO+9PKq7CDEDUExuv11q3ZqTf1R9842353qPHpyogjJvnnT00V7p5XsaTcOGeWsCmI0sOswAFIOffvK2qR071nQSZGuzzbxdF1kk6N8jj0h9+phOgVxceKF0xx2mUyBLFICoe/ddb4rthx9MJ4Eff/+7t1gqxmRb1qqrpQsukP77X9NJ4Ee7dtKzz3qXxBBpFIAou/VWbxc62x/WU+wOOcS7hr3llqaTRN8PP0jHHCN98onpJMjHZptJTz0lHXmk6STYAApAFC1b5j23/qWXTCdBULbdVho1SvrTn0wnia533/V2nGNDn9JxxRXSNdcwAxZRFIComTbNu248e7bpJAhanTre5jXHHWc6SfQMHiydc47pFAjDoYd6M2BbbGE6CdZCLYuSV16R9t+fk3+pWrHCewb7ddeZThIt/fpx8i9l48dL++3nbVyGSGEGICqeflo680yu99uiWzfvGmlZmekk5ixfLp14ovTaa6aToBAaNPAug7VtazoJ/g8zAFFw3XXebX6c/O0xfLj3yFVbr3d/+63UoQMnf5ssXuwtiB0zxnQS/B9mAEzr08e73xl22nZb79JPixamkxTOJ59InTt7+1vATo8+6m3uBKOYATClpkY6+WRO/rb79ltvJmD6dNNJCuP9971PgZz87Xbmmd5DtGAUBcCE5culLl28aWBg4UKvBEydajpJuCZMkA47zLvNFbjoIu82QRjDJYBCW7XKO/m/847pJIiaevW8pwu2aWM6SfDeeUc64gipqsp0EkTNZZdJN95oOoWVKACFVF0tHXssC5+wfltt5W2IU0rbqE6b5t3euny56SSIquuvly6/3HQK61AACiWV8m794oE+2JhmzaRJk0pj6+B586T27aVFi0wnQdTdfbd0/vmmU1iFNQCF8te/cvJHdmbP9lbJF/sn5sWLpU6dOPkjOxddJP3vf6ZTWIUCUAgDBkiPP246BYrJp596T4FMp00n8WflSu9BMOxqiVyccYb0wQemU1iDSwBhGzXKe8AJ4McZZxTfraKu6z3vgBkv+LHlltKHH3p7ZCBUzACE6eOPpe7dTadAMXvsMem220ynyM1ll3Hyh39LlniPhF6xwnSSkscMQFh+/FHaay9voxcgXyNHFsez1Z96ypu1APLVrRtrAkLGDEAY0mnvh5eTP4LSo0f0dwucMEE6+2zTKVAqhg8vvtmvIsMMQBguuUS65x7TKVBqdtxRmjw5ms9VnzdP2nNPaelS00lQShzH2zfloINMJylJFICgjRzpffoHwnDkkd7PWNS0bes95AcIWsOG3l0xDRuaTlJyuAQQpEWLpLPOMp0CpWzsWGnwYNMp1nT99Zz8EZ5Fi1hXEhJm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F0v165tOESnMAKyufn0uAwBAqWnShJP/OlAA1rbnnqYTAACCtPvuphNEEgVgbXvsYToBACBIu+1mOkEkUQDWRlMEgNLC+/o6UQDWtv/+phMAAILUvr3pBJFEAVjbFltIrVubTgEACEKjRlKzZqZTRBIFYF06djSdAAAQhEMOMZ0gsigA63LwwaYTAACCwAe69WIjoHX58Udpq61MpwAA5GvaNGnnnU2niCRmANaFdQAAUPw235yT/wZQANaH60YAUNw6dzadINIoAOtz/PGmEwAA8nHccaYTRBprANYnnZYaN5Z++sl0EgBArioqpEWLpMpK00kiixmA9YnHpe7dTacAAPhx1FGc/DeCArAhp5xiOgEAwI+TTzadIPK4BLAhrittu620YIHpJACAbDH9nxVmADbEcZgFAIBic8wxnPyzQAHYmHPOMZ0AAJCLs84ynaAocAkgGx07ShMmmE4BANiYHXeUZszwZnCxQcwAZOP8800nAABk4+KLOflniRmAbKRS0vbbSwsXmk4CAFifykpv0XadOqaTFAVmALKRSEjnnms6BQBgQ3r35uSfA2YAsrVggTcLkE6bTgIAWJcZM6RmzUynKBrMAGSrUSPptNNMpwAArEvXrpz8c8QMQC5mzpR22cXbIAgAEB2ffuq9PyNrzADkokULng8AAFHTtSsnfx+YAcjVzJlS69amUwAAavHp3xdmAHLVooV0wgmmUwAAJD7954EZAD+mTZN23910CgAAn/59YwbAj113ZS0AAJjWqxcn/zwwA+DX999LLVtKK1eaTgIA9qmslObMkRo0MJ2kaDED4FeTJtI//mE6BQDY6aqrOPnniRmAfFRVSa1aSfPmmU4CAPbYcUfp88+lZNJ0kqLGDEA+ysul224znQIA7DJwICf/ADADEIRDD5Xeest0CgAofUcfLY0YYTpFSaAABOHTT6W99jKdAgBKW0WFNGWKtNNOppOUBC4BBKFNG+maa0ynAIDSdu+9nPwDxAxAkFq2lGbPNp0CAEpPhw5cag0YBSBIb7whHXaY6RQAikXjxspsv71Ur55UWSm3vNy7v72yUm5FhTflLUkrV8pZtcrbd2TVqt9+7/z8s5w5c6TFi83+d4QtmZSmTpWaNzedpKQkTAcoKQcfLPXuLf33v6aTAIiQzP77y23ZUm6zZso0aya3aVO5LVpIdeoE8wLL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" },
                {"notifDate", date }
            };
            await notifRef.SetAsync(data);
        }
    }
}