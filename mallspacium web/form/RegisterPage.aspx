<%@ Page Async="true" Language="C#"  AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="mallspacium_web.form.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div class="container-fluid pt-5 text-black text-center">
            <img src="../design/mallspaceium_logo.png" class="mx-auto d-block" width="150" height="150">
          <h1>Mallspaceium</h1>
          <p>Improving the Quality of Shopping Experience</p> 
        </div>
  
        <div class="container" style="width: 40%; height: 40%;">
            <div class="row">
                
                <div class="mb-3 mt-3">
                      <label for="username" class="form-label">Username:</label>
                      <asp:TextBox ID="usernameTextbox" runat="server" type="text"  class="form-control" placeholder="Enter Username" name="username" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                
                <div class="mb-3 mt-3">
                      <label for="email" class="form-label">Email:</label>
                      <asp:TextBox ID="emailTextbox" runat="server" type="text" class="form-control" placeholder="Enter email" name="email" AutoCompleteType="Disabled" TextMode="Email"></asp:TextBox>
                </div>

                <div class="mb-3 mt-3">
                      <label for="phonenumber" class="form-label">Phone Number:</label>
                      <asp:TextBox ID="phoneNumberTextbox" runat="server" type="text" class="form-control" placeholder="Enter Phone Number" name="phoneNumber" TextMode="Number" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                            
                <div class="mb-3">
                      <label for="pwd" class="form-label">Password:</label>
                      <asp:TextBox ID="passwordTextbox" runat="server" type="text" class="form-control" placeholder="Enter password" name="pswd" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                </div>

                <div class="mb-3">
                      <label for="cpwd" class="form-label">Confirm Password:</label>
                      <asp:TextBox ID="confirmPasswordTextbox" runat="server" type="text"  class="form-control" placeholder="Confirm Password" name="cswd" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                </div>

            </div>

            <div class="mb-3">
                <asp:Button ID="Registerbutton" runat="server" type="submit" class="btn btn-primary" Text="REGISTER" OnClick="Registerbutton_Click" />
            </div>
        </div>  
    </form>
</body>
</html>

