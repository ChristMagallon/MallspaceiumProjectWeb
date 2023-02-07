<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="mallspacium_web.form.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous">
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
              <div class="col">
                    <div class="mb-3 mt-3">
                      <label for="fn" class="form-label">First name:</label>
                      <asp:TextBox ID="fn" runat="server" type="text" class="form-control" placeholder="Enter FirstName" name="firstname"></asp:TextBox>
                    </div>
                   <div class="mb-3 mt-3">
                      <label for="dob" class="form-label">Date of birth:</label>
                      <asp:TextBox ID="dob" runat="server" type="text" class="form-control" placeholder="Enter Date of Birth" name="dateofbirth" TextMode="Date"></asp:TextBox>
                    </div>
                   <div class="mb-3 mt-3">
                      <label for="phonenumber" class="form-label">Phone Number:</label>
                      <asp:TextBox ID="phonenumber" runat="server" type="text" class="form-control" placeholder="Enter Phone Number" name="dateofbirth" TextMode="Number" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                            <div class="mb-3 mt-3">
            <label for="email" class="form-label">Email:</label>
            <asp:TextBox ID="email" runat="server" type="text" class="form-control" placeholder="Enter email" name="email" AutoCompleteType="Disabled" TextMode="Email"></asp:TextBox>
          </div>
                    <div class="mb-3">
            <label for="pwd" class="form-label">Password:</label>
            <asp:TextBox ID="pwed" runat="server" type="text" class="form-control" placeholder="Enter password" name="pswd" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
          </div>
              </div>
              <div class="col">
                        <div class="mb-3 mt-3">
                           <label for="ln" class="form-label">Last Name:</label>
                           <asp:TextBox ID="ln" runat="server" type="text" class="form-control" placeholder="Enter LastName" name="lastname"></asp:TextBox>
                        </div>
                   <div class="mb-3 mt-3">
                           <label for="gender" class="form-label">Gender:</label><asp:RadioButtonList ID="GenderRadioButtonList" runat="server" RepeatDirection="Horizontal">
                               <asp:ListItem>&nbsp Male &nbsp &nbsp</asp:ListItem>
                               <asp:ListItem>&nbsp Female &nbsp &nbsp</asp:ListItem>
                               <asp:ListItem>&nbsp Prefer not to say</asp:ListItem>
                           </asp:RadioButtonList>
                        </div>
                  <div class="mb-3 mt-3">
                      <label for="address" class="form-label">Address:</label>
                      <asp:TextBox ID="address" runat="server" type="text" class="form-control" placeholder="Enter Address" name="address" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                  <div class="mb-3 mt-3">
                      <label for="username" class="form-label">Username:</label>
                      <asp:TextBox ID="username" runat="server" type="text"  class="form-control" placeholder="Enter Username" name="username" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                     <label for="cpwd" class="form-label">Confirm Password:</label>
                        <asp:TextBox ID="cpwd" runat="server" type="text"  class="form-control" placeholder="Confirm Password" name="cswd" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                    </div>
              </div>   
            </div>
         <form >         
          <asp:button ID="Registerbutton" runat="server" type="submit" class="btn btn-primary" Text="REGISTER"/>
        </form>
       
       
        </div>
    </form>
 
</body>
</html>
