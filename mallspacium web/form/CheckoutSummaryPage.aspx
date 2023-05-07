<%@ Page Language="C#" AutoEventWireup="true" UnobtrusiveValidationMode = "none" Async="true" CodeBehind="CheckoutSummaryPage.aspx.cs" Inherits="mallspacium_web.form.CheckoutSummaryPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="width: 50%; height: 50%;">

            <br />
            <asp:Label ID="Label1" runat="server" Text="Scan QR Code to Pay"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Scan the QR code with a mobile payment app"></asp:Label>
            <br />
            <br />
            <asp:Image ID="Image1" runat="server" Height="50px" ImageUrl="~/Logos/gcash_logo.png" Width="100px" />
            <br />
            <br />
            <asp:Image ID="Image2" runat="server" Height="230px" ImageUrl="~/Logos/merchant_qr_code.jpg" Width="230px" />
            <br />
            <br />
            <asp:Label ID="Label12" runat="server" Text="or"></asp:Label>
            <br />
            <asp:Label ID="Label13" runat="server" Text="Send it through the merchant's phone number"></asp:Label>
            <br />
            <asp:Label ID="MerchantPhoneNumberLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:FileUpload ID="PaymentFileUpload" runat="server" />
            <asp:RequiredFieldValidator ID="PaymentFileRequiredFieldValidator" runat="server" ControlToValidate="PaymentFileUpload" Display="Dynamic" ErrorMessage="field is required *" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Order Summary"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Pay to:"></asp:Label>
&nbsp;<asp:Label ID="Label5" runat="server" Text="Mallspaceium Merchant"></asp:Label>
            <br />
            Order info:
            <asp:Label ID="Label6" runat="server" Text="Subscription Purchase"></asp:Label>
            <br />
            Order amount:
            <asp:Label ID="SubscriptionPriceLabel" runat="server"></asp:Label>
            <br />
            <br />
            Total to pay:
            <asp:Label ID="TotalPayLabel" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="ProceedButton" runat="server" class="btn btn-primary" Text="Proceed" Width="100px" OnClick="ProceedButton_Click" />

        </div>
    </form>
</body>
</html>
