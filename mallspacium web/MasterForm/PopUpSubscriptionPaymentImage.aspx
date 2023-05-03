<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="PopUpSubscriptionPaymentImage.aspx.cs" Inherits="mallspacium_web.MasterForm.PopUpSubscriptionPaymentImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function onPopupClose() {
            window.opener.allowNavigation = true;
            window.close();
        }

        window.onbeforeunload = function () {
            if (!window.allowNavigation) {
                return "Please close this popup window before navigating away.";
            }
        };
    </script>
</head>
<body onload="window.opener.allowNavigation = false;">
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="MyImage" runat="server" Style="height: 100%; width: 100%;" />
            <input type="button" value="Close" onclick="onPopupClose();" />
        </div>
    </form>
</body>
</html>