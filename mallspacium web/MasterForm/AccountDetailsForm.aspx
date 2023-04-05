<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="AccountDetailsForm.aspx.cs" Inherits="mallspacium_web.MasterForm.AdminAccountDetailsForm" %>

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
    <link rel="stylesheet" href="Style.css" />

    <!-- starts here-->

    <div class="container">
        <div class="form">
            <h4 class="mb-4"><asp:Label ID="Label1" runat="server" Text="Account Details"></asp:Label></h4>

            <div class="mb-3">
                <asp:Label ID="Label8" runat="server" Text="Id: "></asp:Label>
                <asp:Textbox ID="idTextbox" runat="server" Text="" CssClass="form-control"></asp:Textbox>
            </div>

            <div class="mb-3">
                <asp:Label ID="Label2" runat="server" Text="Email "></asp:Label>
                <asp:Textbox ID="emailTextbox" runat="server" Text="" CssClass="form-control"></asp:Textbox>
            </div>

            <div class="mb-3">
                <asp:Label ID="Label4" runat="server" Text="Username: "></asp:Label>
                <asp:Textbox ID="usernameTextbox" runat="server" Text="" CssClass="form-control"></asp:Textbox>
            </div>

            <div class="mb-3">
                <asp:Label ID="Label5" runat="server" Text="Date Created: "></asp:Label>
                <asp:Textbox ID="dateCreatedTextbox" runat="server" Text="" CssClass="form-control"></asp:Textbox> <br /> 
            </div>
        </div>


        <div class="form">
            <h4 class="mb-4"><asp:Label ID="Label3" runat="server" Text="Change Password"></asp:Label></h4>

            <div class="mb-3">
                <asp:Label ID="Label6" runat="server" Text="Current Password: "></asp:Label>
                <asp:Textbox ID="currentPasswordTextbox" runat="server" Text="" CssClass="form-control" TextMode="Password"></asp:Textbox>
                <asp:RequiredFieldValidator ID="currentPasswordRequiredFieldValidator" runat="server" ControlToValidate="currentPasswordTextbox" 
                    ErrorMessage="Required*" 
                    ForeColor="Red" 
                    ValidationGroup="Validate">
                </asp:RequiredFieldValidator>
                <asp:Label ID="errorCurrentPasswordLabel" runat="server" ForeColor="Red"></asp:Label>
            </div>

            <div class="mb-3">
                <asp:Label ID="Label7" runat="server" Text="Confirm Current Password: "></asp:Label>
                <asp:Textbox ID="confirmCurrentPasswordTextbox" runat="server" Text="" CssClass="form-control" TextMode="Password"></asp:Textbox>
                <asp:RequiredFieldValidator ID="confirmCurrentPasswordRequiredFieldValidator" runat="server" ControlToValidate="confirmCurrentPasswordTextbox" 
                    ErrorMessage="Required*" 
                    ForeColor="Red" 
                    ValidationGroup="Validate">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="confirmCurrentPasswordCompareValidator" runat="server" ControlToCompare="currentPasswordTextBox"
                    ControlToValidate="confirmCurrentPasswordTextBox" 
                    Display="Dynamic" 
                    ErrorMessage="Password does not match!" 
                    ForeColor="Red" 
                    ValidationGroup="Validate">
                </asp:CompareValidator>
            </div>

            <div class="mb-3">
                <asp:Label ID="Label9" runat="server" Text="New Password: "></asp:Label>
                <asp:Textbox ID="newPasswordTextbox" runat="server" Text="" CssClass="form-control" TextMode="Password"></asp:Textbox>
                <asp:RequiredFieldValidator ID="newPasswordRequiredFieldValidator" runat="server" ControlToValidate="newPasswordTextbox" 
                    ErrorMessage="Required*" 
                    ForeColor="Red" 
                    ValidationGroup="Validate">
                </asp:RequiredFieldValidator>
            </div>

            <div class="mb-3">
                <asp:Label ID="Label10" runat="server" Text="Confirm New Password: "></asp:Label>
                <asp:Textbox ID="confirmNewPasswordTextbox" runat="server" Text="" CssClass="form-control" TextMode="Password"></asp:Textbox>
                <asp:RequiredFieldValidator ID="confirmNewPasswordRequiredFieldValidator" runat="server" ControlToValidate="confirmNewPasswordTextbox" 
                    ErrorMessage="Required*" 
                    ForeColor="Red" 
                    ValidationGroup="Validate">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="confirmNewPasswordCompareValidator" runat="server" ControlToCompare="newPasswordTextBox"
                    ControlToValidate="confirmNewPasswordTextBox" 
                    Display="Dynamic" 
                    ErrorMessage="Password does not match!" 
                    ForeColor="Red" 
                    ValidationGroup="Validate">
                </asp:CompareValidator>
            </div>

            <div class="mb-3">
                <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" CssClass="btn btn-primary" />
                <asp:Button ID="deleteButton" runat="server" Text="DELETE" OnClick="deleteButton_Click" CssClass="btn btn-danger" />
            </div>
        </div>
    </div>

</asp:Content>
