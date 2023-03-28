<%@ Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="CreateAdminAccountForm.aspx.cs" Inherits="mallspacium_web.MasterForm.CreateAdminAccountForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <!-- starts here-->

   <div class="container">
  <h3>Create New Account</h3>
  <br>
  <div class="row">
    <div class="col-md-6">
      <div class="form-group">
        <label for="usernameTextbox" class="form-label">Username:</label>
        <asp:TextBox ID="usernameTextbox" runat="server" CssClass="form-control" Text=""></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="emailTextbox" class="form-label">Email:</label>
        <asp:TextBox ID="emailTextbox" runat="server" CssClass="form-control" Text=""></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="phoneNumberTextbox" class="form-label">Phone Number:</label>
        <asp:TextBox ID="phoneNumberTextbox" runat="server" CssClass="form-control" Text=""></asp:TextBox>
      </div>
    </div>
    <div class="col-md-6">
      <div class="form-group">
        <label for="dateCreatedTextbox" class="form-label">Date Created:</label>
        <asp:TextBox ID="dateCreatedTextbox" runat="server" CssClass="form-control" Text=""></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="passwordTextbox" class="form-label">Password:</label>
        <asp:TextBox ID="passwordTextbox" runat="server" CssClass="form-control" TextMode="Password" Text=""></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="confirmPasswordTextbox" class="form-label">Confirm Password:</label>
        <asp:TextBox ID="confirmPasswordTextbox" runat="server" CssClass="form-control" TextMode="Password" Text=""></asp:TextBox>
      </div>
    </div>
  </div>
  <br>
  <div class="form-group">
    <asp:Button ID="addButton" runat="server" Text="Add Account" OnClick="addButton_Click" CssClass="btn btn-primary"></asp:Button>
  </div>
</div>

</asp:Content>