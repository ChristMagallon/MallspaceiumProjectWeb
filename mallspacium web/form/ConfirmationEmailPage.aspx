<%@ Page Language="C#" AutoEventWireup="true"  Async="true"  UnobtrusiveValidationMode = "none" CodeBehind="ConfirmationEmailPage.aspx.cs" Inherits="mallspacium_web.form.ConfirmationEmailPage" %>

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
                 <h1>Confirmation Page</h1>
                <div class="mb-3">
                      <label for="confirmationCode" class="form-label">Please enter your email:</label>
                      <asp:TextBox ID="EmailTextBox" runat="server" type="text" class="form-control" placeholder="Enter Email" AutoCompleteType="Disabled" ValidationGroup="Validate"></asp:TextBox>
                </div>

                <div class="mb-3">
                      <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="ConfirmationCodeTextBox" Display="Dynamic" ErrorMessage="field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" Display="Dynamic" ErrorMessage="Invalid email address!" ForeColor="Red" ValidationGroup="Validate" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                      <asp:Label ID="ErrorEmailAddressLabel" runat="server" ForeColor="Red"></asp:Label>
                      <br />
                </div>

                <div class="mb-3">
                      <label for="confirmationCode" class="form-label">Please enter your confirmation code:</label>
                      <asp:TextBox ID="ConfirmationCodeTextBox" runat="server" type="text" class="form-control" placeholder="Enter Confirmation Code" AutoCompleteType="Disabled" ValidationGroup="Validate"></asp:TextBox>
                </div>

                <div class="mb-3">
                      <asp:RequiredFieldValidator ID="ConfirmCodeRequiredFieldValidator" runat="server" ControlToValidate="ConfirmationCodeTextBox" Display="Dynamic" ErrorMessage="field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                      <asp:Label ID="ErrorConfirmationCodeLabel" runat="server" ForeColor="Red"></asp:Label>
                      <br />
                </div>
                 <div style="text-align: center;">
                     <asp:Button ID="ConfirmButton" runat="server" Text="Confirm" class="btn btn-primary" ValidationGroup="Validate" Width="100px" OnClick="ConfirmButton_Click1" />
                 </div>            
            </div>
          </div>
    </form>
</body>
</html>