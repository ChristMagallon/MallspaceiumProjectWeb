<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewPurchaseSubscriptionPage.aspx.cs" Inherits="mallspacium_web.form.ReviewPurchaseSubscriptionPage" %>

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
            <img src="../design/mallspaceium_logo.png" class="mx-auto d-block" width="150" height="150"/>
          <h1>Mallspaceium</h1>
          <p>Improving the Quality of Shopping Experience</p> 
        </div>
        <div class="container" style="width: 50%; height: 50%;">
            <br />
            <br />
            <asp:Label ID="SubscriptionTypeLabel" runat="server"></asp:Label>
            &nbsp;<asp:Label ID="Label1" runat="server" Text="to be added to your account"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Total:"></asp:Label>
&nbsp;<asp:Label ID="SubscriptionPriceLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Payment method:"></asp:Label>
&nbsp;<asp:Label ID="Label4" runat="server" Text="Gcash"></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Email:"></asp:Label>
&nbsp;<asp:Label ID="UserEmailLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:CheckBox ID="AgreementTermsCheckBox" runat="server" />
            <div class="form-check">
                <asp:Label ID="AgreementTermsLabel" runat="server" AssociatedControlID="AgreementTermsCheckBox" CssClass="form-check-label" Text="I agree to the terms of the Mallspaceium Subscriber Agreement (last updated 22 Apr, 2023.) Gcash transactions are authorized through the merchat directly. Click the button below to open a new web browser to initiate the transaction." />
            </div>

            <br />
            <br />
            <asp:Button ID="PurchaseButton" runat="server" class="btn btn-primary" Text="Purchase" Width="100px" OnClick="PurchaseButton_Click" />
        </div>
    </form>
</body>
</html>
