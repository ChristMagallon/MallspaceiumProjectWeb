<%@ Page Language="C#" Async="true" UnobtrusiveValidationMode = "none"AutoEventWireup="true" CodeBehind="AdvertiseProductPaymentSummary.aspx.cs" Inherits="mallspacium_web.ShopOwner.AdvertiseProductPaymentSummary" %>

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

            <asp:Label runat="server">Your Advertisement</asp:Label>
            <br />
            <asp:Label ID="Label7" runat="server" Text="will be visible to others"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label9" runat="server" Text="Payment method:"></asp:Label>
            &nbsp;<asp:Label ID="Label10" runat="server" Text="Gcash"></asp:Label>
            <br />
            <asp:Label ID="Label11" runat="server" Text="Email:"></asp:Label>
            &nbsp;<asp:Label ID="UserEmailLabel" runat="server"></asp:Label>
            <br />
            <br />

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
            <asp:RequiredFieldValidator ID="PaymentFileRequiredFieldValidator" runat="server" Display="Dynamic" ErrorMessage="field is required *" ForeColor="Red" ValidationGroup="Validate" ControlToValidate="PaymentFileUpload"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Advertisement Summary"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Pay to:"></asp:Label>
&nbsp;<asp:Label ID="Label5" runat="server" Text="Mallspaceium Merchant"></asp:Label>
            <br />
            Order info:
            <asp:Label ID="Label6" runat="server" Text="Advertisement posting"></asp:Label>
            <br />
            <br />
            Total to pay:
            <asp:Label runat="server" ID="AdvertisementPriceLabel">150.00</asp:Label>
            <br />
            <br />
            <asp:CheckBox ID="AgreementTermsCheckBox" runat="server" />
            <br />
            <div class="form-check">
                <asp:Label ID="AgreementTermsLabel" runat="server" AssociatedControlID="AgreementTermsCheckBox" CssClass="form-check-label" Text="I agree to the terms of the Mallspaceium Subscriber Agreement (last updated 22 Apr, 2023.) Gcash transactions are authorized through the merchat directly. Click the button below to open a new web browser to initiate the transaction." />
            </div>
            <br />
            <br />
            <asp:Button ID="ProceedButton" runat="server" class="btn btn-primary" Text="Proceed" Width="100px" OnClick="ProceedButton_Click" ValidationGroup="Validate" />
        </div>
    </form>
</body>
</html>
