<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionsPage.aspx.cs" Inherits="mallspacium_web.Shopper.SubscriptionsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="subscription-choice">
  <h2>
      <asp:Label ID="BasicSubscriptionLabel" runat="server" Text="Basic Subscription"></asp:Label>
            </h2>
  <p>₱<asp:Label ID="BasicSubPriceLabel" runat="server" Text="149.80"></asp:Label>
&nbsp;per month</p>
  <ul>
    <li>Access to basic content</li>
    <li>Monthly newsletter</li>
  </ul>
            <asp:Button ID="BasicSubButton" runat="server" OnClick="BasicSubButton_Click" Text="Subscribe" />
</div>

<div class="subscription-choice">
  <h2>
      <asp:Label ID="AdvancedSubscriptionLabel" runat="server" Text="Advanced Subscription"></asp:Label>
    </h2>
  <p>₱<asp:Label ID="AdvancedSubPriceLabel" runat="server" Text="299.06"></asp:Label>
&nbsp;per month</p>
  <ul>
    <li>Access to advanced content</li>
    <li>Weekly newsletter</li>
    <li>Exclusive offers</li>
  </ul>
    <asp:Button ID="AdvanceSubButton" runat="server" OnClick="AdvanceSubButton_Click" Text="Subscribe" />
</div>

<div class="subscription-choice">
  <h2>
      <asp:Label ID="PremiumSubscriptionLabel" runat="server" Text="Premium Subscription"></asp:Label>
    </h2>
    <p>
        ₱<asp:Label ID="PremiumSubPriceLabel" runat="server" Text="449.04"></asp:Label>
&nbsp;per month</p>
    <ul>
        <li>Access to premium contently newsletter</li>
    <li>Exclusive offers</li>
    <li>Priority support</li>
  </ul>
    <asp:Button ID="PremiumSubButton" runat="server" OnClick="PremiumSubButton_Click" Text="Subscribe" />
</div>
    </form>
</body>
</html>
