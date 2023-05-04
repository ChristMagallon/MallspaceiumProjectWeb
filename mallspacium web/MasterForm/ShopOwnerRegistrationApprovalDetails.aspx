<%@ Page Async="true" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ShopOwnerRegistrationApprovalDetails.aspx.cs" Inherits="mallspacium_web.MasterForm.ShopOwnerRegistrationApprovalDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="Style.css" />

    <!-- start here -->

    <div class="container">
        <div class="form">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Shop owner Registration Details" CssClass="h3"></asp:Label>
            </div>
        
            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="User ID: "></asp:Label> 
                <asp:Label ID="userIdLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label5" runat="server" Text="Email: "></asp:Label> 
                <asp:Label ID="emailLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label7" runat="server" Text="First Name: "></asp:Label> 
                <asp:Label ID="firstNameLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label9" runat="server" Text="Last Name: "></asp:Label> 
                <asp:Label ID="lastNameLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label" runat="server" Text="Shop Image: "></asp:Label> <br />
                <asp:Image ID="shopImage" runat="server" class="img-fluid mb-4"  />
                <asp:HiddenField ID="shopImageHiddenField" runat="server" />
            </div> 

            <div class="form-group">
                <asp:Label ID="Label13" runat="server" Text="Shop Name: "></asp:Label> 
                <asp:Label ID="shopNameLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label17" runat="server" Text="Shop Description: "></asp:Label> 
                <asp:Label ID="descriptionLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label21" runat="server" Text="Phone Number: "></asp:Label> 
                <asp:Label ID="phoneNumberLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label23" runat="server" Text="Address: "></asp:Label> 
                <asp:Label ID="addressLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label25" runat="server" Text="Username: "></asp:Label> 
                <asp:Label ID="usernameLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Account Type: "></asp:Label> 
                <asp:Label ID="userRoleLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label20" runat="server" Text="Date Registered: "></asp:Label> 
                <asp:Label ID="dateLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> <br /> <br />

            <div class="form">
                <div class="form-group">
                    <asp:HiddenField ID="imageHiddenField" runat="server" />
                    <asp:Button ID="viewShopPermitDetailsButton" runat="server" Text="View Shop Permit" CssClass="btn btn-primary" ValidationGroup="None" OnClick="viewShopPermitDetailsButton_Click" /> <br /> <br /> <br />
                </div>
            </div>

            <div class="form-group">
                <asp:Button ID="approveButton" runat="server" Text="APPROVE" CssClass="btn btn-success" OnClick="approveButton_Click" /> 
                <asp:Button ID="disapproveButton" runat="server" Text="DISAPPROVE" CssClass="btn btn-danger" OnClick="disapproveButton_Click" />
            </div>
        </div>
    </div>
</asp:Content>
