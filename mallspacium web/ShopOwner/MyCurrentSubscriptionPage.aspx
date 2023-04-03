<%@ Page Title="" Language="C#" MasterPageFile="~/Shopper/Site1.Master" Async="true" AutoEventWireup="true" CodeBehind="MyCurrentSubscriptionPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.MyCurrentSubscriptionPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <div class="row mb-5">
    <div class="col-md-6 mx-auto">
        <div class="card mb-4 box-shadow">
            <div class="card-header">
                <h4 class="my-0 font-weight-normal">Current Subscription</h4>
            </div>
            <div class="card-body">
                <ul class="list-unstyled mt-3 mb-4">
                    <li><strong>Subscription Type:</strong> <asp:Label ID="SubscriptionTypeLabel" runat="server" Text=""></asp:Label></li>
                    <li><strong>Price:</strong> ₱<asp:Label ID="PriceLabel" runat="server" Text=""></asp:Label></li>
                    <li><strong>Status:</strong> <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label></li>
                </ul>
                <asp:Button ID="CancelSubscriptionButton" runat="server" Text="Cancel Subscription" CssClass="btn btn-lg btn-block btn-danger" OnClick="CancelSubscriptionButton_Click" />
            </div>
        </div>
    </div>
</div>

</asp:Content>