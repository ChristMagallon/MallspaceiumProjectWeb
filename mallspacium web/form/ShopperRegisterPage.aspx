﻿<%@ Page Language="C#" Async="true" AutoEventWireup="true" UnobtrusiveValidationMode = "none" CodeBehind="ShopperRegisterPage.aspx.cs" Inherits="mallspacium_web.form.ShopperRegisterPage" %>

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
                      <label for="firstName" class="form-label">First Name:</label>
                      <asp:TextBox ID="FirstNameTextBox" runat="server" type="text" class="form-control" placeholder="Enter First Name" AutoCompleteType="Disabled" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstNameTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>
                
                <div class="mb-3 mt-3">
                      <label for="lastName" class="form-label">Last Name:</label>
                      <asp:TextBox ID="LastNameTextBox" runat="server" type="text" class="form-control" placeholder="Enter Last Name" AutoCompleteType="Disabled" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastNameTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>

                <div class="mb-3 mt-3">
                      <label for="birthday" class="form-label">Date of Birth:</label>
                      <asp:TextBox ID="DOBTextBox" runat="server" type="text" class="form-control" placeholder="Enter Date of Birth" TextMode="Date" AutoCompleteType="Disabled" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DOBTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>
                            
                <div class="mb-3">
                      <label for="gender" class="form-label">Gender:</label>
                      <br />
                      <asp:DropDownList ID="GenderDropDownList" runat="server" ValidationGroup="Validate">
                          <asp:ListItem Value="">--Select a Gender--</asp:ListItem>
                          <asp:ListItem Value="Male">Male</asp:ListItem>
                          <asp:ListItem Value="Female">Female</asp:ListItem>
                          <asp:ListItem Value="Others">Others</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:RequiredFieldValidator ID="GenderRequiredFieldValidator" runat="server" ControlToValidate="GenderDropDownList" InitialValue="" ErrorMessage="Please select a gender*" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                      <label for="phoneNumber" class="form-label">Phone Number:</label>
                      <asp:TextBox ID="PhoneNumberTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Phone Number" AutoCompleteType="Disabled" TextMode="Phone" ValidationGroup="Validate" Text="+639"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="PhoneNumberTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate" Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:Label ID="ErrorPhoneNumberLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="mb-3">
                      <label for="address" class="form-label">Address:</label>
                      <asp:TextBox ID="AddressTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Address" AutoCompleteType="Disabled" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="AddressTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                      <label for="email" class="form-label">Email:</label>
                      <asp:TextBox ID="EmailTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Email" AutoCompleteType="Disabled" TextMode="Email" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="EmailTextBox" Display="Dynamic" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ControlToValidate="EmailTextBox" Display="Dynamic" ErrorMessage="Invalid email address!" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                      <asp:Label ID="ErrorEmailAddressLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="mb-3">
                      <label for="username" class="form-label">Username:</label>
                      <asp:TextBox ID="UsernameTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Username" AutoCompleteType="Disabled" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="UsernameTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate" Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:Label ID="ErrorUsernameLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="mb-3">
                      <label for="password" class="form-label">Password:</label>
                      <asp:TextBox ID="PasswordTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Password" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                      <label for="confirmPassword" class="form-label">ConfirmPassword:</label>
                      <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" type="text"  class="form-control" placeholder="Enter Confirm Password" AutoCompleteType="Disabled" TextMode="Password" ValidationGroup="Validate"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ConfirmPasswordTextBox" Display="Dynamic" ErrorMessage="Field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="ConfirmPasswordCompareValidator" runat="server" ControlToCompare="PasswordTextBox" ControlToValidate="ConfirmPasswordTextBox" Display="Dynamic" ErrorMessage="Password does not match!" ForeColor="Red" ValidationGroup="Validate"></asp:CompareValidator>
                </div>

            </div>

            <div class="mb-3">
                <asp:Button ID="SignupButton" runat="server" class="btn btn-primary" Text="Sign Up" OnClick="SignupButton_Click" ValidationGroup="Validate" />
                <br />
                <br />
                <asp:LinkButton ID="LoginLinkButton" runat="server" OnClick="LoginLinkButton_Click" ForeColor="#0066FF">already have an account?</asp:LinkButton>
            </div>
        </div>  
    </form>
</body>
</html>
