<%@ Page Async="true" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="AdvertisementPaymentApprovalDetails.aspx.cs" Inherits="mallspacium_web.MasterForm.AdvertisementPaymentApprovalDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="Style.css" />

    <!-- start here -->

    <div class="container">
        <div class="form">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Advertisement Payment Details" CssClass="h3"></asp:Label>
            </div>
        
            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="Transaction ID: "></asp:Label> 
                <asp:Label ID="transactionIdLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
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
                <asp:Label ID="Label2" runat="server" Text="Account Type: "></asp:Label> 
                <asp:Label ID="userRoleLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label8" runat="server" Text="Price: "></asp:Label> 
                <asp:Label ID="priceLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> 

            <div class="form-group">
                <asp:Label ID="Label10" runat="server" Text="Advertisement Product ID: "></asp:Label> 
                <asp:Label ID="advertisementProductIdLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> 

            <div class="form-group">
                <asp:Label ID="Label12" runat="server" Text="Product Name: "></asp:Label> 
                <asp:Label ID="productNameLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> 

            <div class="form-group">
                <asp:Label ID="Label14" runat="server" Text="Product Image: "></asp:Label> <br />
                <asp:Image ID="productImage" runat="server" class="img-fluid mb-4"  />
                <asp:HiddenField ID="productImageHiddenField" runat="server" />
            </div> 

            <div class="form-group">
                <asp:Label ID="Label16" runat="server" Text="Product Description: "></asp:Label> 
                <asp:Label ID="descriptionLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> 

            <div class="form-group">
                <asp:Label ID="Label18" runat="server" Text="Product Shop Name: "></asp:Label> 
                <asp:Label ID="shopNameLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> 

            <div class="form-group">
                <asp:Label ID="Label20" runat="server" Text="Date: "></asp:Label> 
                <asp:Label ID="dateLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> 
            
            <br /> <br />

            <div class="form">
                <div class="form-group">
                    <asp:HiddenField ID="imageHiddenField" runat="server" />
                    <asp:Button ID="viewpaymentDetailsButton" runat="server" Text="View Payment" CssClass="btn btn-primary" ValidationGroup="None" OnClick="viewpaymentDetailsButton_Click" /> <br /> <br /> <br />
                </div>
            </div>

            <div class="form-group">
                <asp:Button ID="approveButton" runat="server" Text="APPROVE" CssClass="btn btn-success" OnClick="approveButton_Click" /> 
                <asp:Button ID="disapproveButton" runat="server" Text="DISAPPROVE" CssClass="btn btn-danger" OnClick="disapproveButton_Click" />
            </div>
        </div>
    </div>
</asp:Content>
