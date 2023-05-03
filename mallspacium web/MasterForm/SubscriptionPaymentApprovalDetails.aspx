<%@ Page Async="true" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="SubscriptionPaymentApprovalDetails.aspx.cs" Inherits="mallspacium_web.MasterForm.SubscriptionPaymentApprovalDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="Style.css" />

    <!-- start here -->

    <div class="container">
        <div class="form">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Subscription Payment Details" CssClass="h3"></asp:Label>
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
                <asp:Label ID="Label6" runat="server" Text="Subscription ID: "></asp:Label> 
                <asp:Label ID="subscriptionIdLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Subscription Type: "></asp:Label> 
                <asp:Label ID="subscriptionTypeLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="Label8" runat="server" Text="Price: "></asp:Label> 
                <asp:Label ID="priceLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div> <br /> <br />

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