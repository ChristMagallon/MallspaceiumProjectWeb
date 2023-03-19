<%@ Page Language="C#" Async="true" UnobtrusiveValidationMode = "none" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="mallspacium_web.form.LoginPage" %>

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

                <div class="mb-3">
                      <label for="email" class="form-label">Email:</label>
                      <asp:TextBox ID="EmailTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Email" AutoCompleteType="Disabled" TextMode="Email" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate" Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ControlToValidate="EmailTextBox" Display="Dynamic" ErrorMessage="Invalid email address!" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                </div>

                <div class="mb-3">
                      <label for="password" class="form-label">Password:</label>
                      <asp:TextBox ID="PasswordTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Password" AutoCompleteType="Disabled" TextMode="Password" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>

            </div>

            <div class="mb-3">
                <asp:Button ID="SigninButton" runat="server" type="submit" class="btn btn-primary" Text="Sign In" OnClick="SignupButton_Click" ValidationGroup="Validate"/>
            </div>
        </div>  
    </form>
</body>
</html>
