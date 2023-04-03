<%@ Page Title="" Language="C#" MasterPageFile="~/Shopper/Site1.Master" Async="true" AutoEventWireup="true" CodeBehind="SubscriptionPage.aspx.cs" Inherits="mallspacium_web.Shopper.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style type="text/css">
       .logoutbtn
{
 border: none;
 background:none;
 outline: none;
}

       .px-6{
           margin-left: 6px;
           margin-right: 6px;
       }
    </style>
      
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <div class="container">
        <div class="row">
        <div class="col-md-12 mb-4">
            <h3>Current subscription: <asp:Label ID="CurrentSubscriptionLabel" runat="server"></asp:Label></h3>
            <asp:Button ID="ViewSubscriptionButton" runat="server" OnClick="ViewSubscriptionButton_Click" CssClass="btn btn-lg btn-block btn-primary" Text="View My Subscription" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-4 d-flex">
            <div class="card mb-4 box-shadow subscription-choice">
                <div class="card-header">
                    <h4 class="my-0 font-weight-normal">
                        <asp:Label ID="BasicSubscriptionLabel" runat="server" Text="Basic Subscription"></asp:Label>
                    </h4>
                </div>
                <div class="card-body h-100 flex-fill">
                    <h1 class="card-title pricing-card-title">₱<asp:Label ID="BasicSubPriceLabel" runat="server" Text="149.80"></asp:Label> <small class="text-muted">/ 1 month</small></h1>
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>Access to basic content</li>
                        <li>Monthly newsletter</li>
                    </ul>
                    <asp:Button ID="BasicSubButton" runat="server" OnClick="BasicSubButton_Click" CssClass="btn btn-lg btn-block btn-primary" Text="Subscribe" />
                </div>
            </div>
        </div>

    <div class="col-md-4 d-flex">
        <div class="card mb-4 box-shadow subscription-choice">
            <div class="card-header">
                <h4 class="my-0 font-weight-normal">
                    <asp:Label ID="AdvancedSubscriptionLabel" runat="server" Text="Advanced Subscription"></asp:Label>
                </h4>
            </div>
            <div class="card-body h-100 flex-fill">
                <h1 class="card-title pricing-card-title">₱<asp:Label ID="AdvancedSubPriceLabel" runat="server" Text="299.06"></asp:Label> <small class="text-muted">/ 3 month</small></h1>
                <ul class="list-unstyled mt-3 mb-4">
                    <li>Access to advanced content</li>
                    <li>Weekly newsletter</li>
                    <li>Exclusive offers</li>
                </ul>
                <asp:Button ID="AdvanceSubButton" runat="server" OnClick="AdvanceSubButton_Click" CssClass="btn btn-lg btn-block btn-primary" Text="Subscribe" />
            </div>
        </div>
    </div>

    <div class="col-md-4 d-flex">
        <div class="card mb-4 box-shadow subscription-choice">
            <div class="card-header">
                <h4 class="my-0 font-weight-normal">
                    <asp:Label ID="PremiumSubscriptionLabel" runat="server" Text="Premium Subscription"></asp:Label>
                </h4>
            </div>
            <div class="card-body h-100 flex-fill">
                <h1 class="card-title pricing-card-title">₱<asp:Label ID="PremiumSubPriceLabel" runat="server" Text="449.04"></asp:Label> <small class="text-muted">/ 5 month</small></h1>
                <ul class="list-unstyled mt-3 mb-4">
                    <li>Access to premium content</li>
                    <li>Weekly newsletter</li>
                    <li>Exclusive offers</li>
                    <li>Priority support</li>
                </ul>
                <asp:Button ID="PremiumSubButton" runat="server" OnClick="PremiumSubButton_Click" CssClass="btn btn-lg btn-block btn-primary" Text="Subscribe" />
            </div>
        </div>
    </div>
</div>
    </div>
   

</asp:Content>
