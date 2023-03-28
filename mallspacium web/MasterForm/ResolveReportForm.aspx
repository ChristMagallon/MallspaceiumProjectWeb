<%@ Page Async="true" Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ResolveReportForm.aspx.cs" Inherits="mallspacium_web.MasterForm.ReportDetailsForm" %>

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
        <!-- start here -->
    <div class="form">
    <h2>Resolve Report</h2>
    <div class="form-group">
        <label for="usernameTextbox">Reported Username:</label>
        <input type="text" class="form-control" id="usernameTextbox" runat="server" placeholder="Enter username">
    </div>
    <div class="form-group">
        <label for="emailTextbox">Email:</label>
        <input type="email" class="form-control" id="emailTextbox" runat="server" placeholder="Enter email">
    </div>
    <div class="form-group">
        <label for="phoneNumberTextbox">Phone Number:</label>
        <input type="tel" class="form-control" id="phoneNumberTextbox" runat="server" placeholder="Enter phone number">
    </div>
    <div class="form-group">
        <label for="dateCreatedTextbox">Date Created:</label>
        <input type="date" class="form-control" id="dateCreatedTextbox" runat="server">
    </div>
    <div class="form-group">
        <label for="passwordTextbox">Password:</label>
        <input type="password" class="form-control" id="passwordTextbox" runat="server" placeholder="Enter password">
    </div>
    <div class="form-group">
        <label for="confirmPasswordTextbox">Confirm Password:</label>
        <input type="password" class="form-control" id="confirmPasswordTextbox" runat="server" placeholder="Confirm password">
    </div>
    <button type="submit" class="btn btn-primary" id="addButton" runat="server" OnClick="addButton_Click">Add Account</button>
</div>


</asp:Content>
