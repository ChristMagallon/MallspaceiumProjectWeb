<%@ Page Async="true" UnobtrusiveValidationMode="none" Language="C#" AutoEventWireup="true" CodeBehind="ChangeProductImagePage.aspx.cs" Inherits="mallspacium_web.ShopOwner.ChangeImagePopUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        #myDiv {
            width: 300px; /* set a width for the div */
            height: 200px; /* set a height for the div */
            margin: auto; /* center the div horizontally */
            text-align: center; /* center the content vertically */
            display: flex; /* enable flexbox */
            justify-content: center; /* center the content horizontally */
            align-items: center; /* center the content vertically */
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <div id="myDiv">
            <center> 
                <asp:Label ID="Label1" runat="server" Text="Changing the Image of the "></asp:Label> 
                <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label> <br /> <br /> <br />

                <asp:Label ID="Label" runat="server" Text="Image: "></asp:Label>
                <asp:FileUpload ID="imageFileUpload" runat="server" Height="35px" Width="219px" /> 
                <asp:RequiredFieldValidator ID="imageRequiredFieldValidator" runat="server" ControlToValidate="imageFileUpload" ErrorMessage="Please select an image." style="color: red"></asp:RequiredFieldValidator><br /> <br />

                <asp:Button ID="saveButton" runat="server" Text="SAVE" OnClick="saveButton_Click" />

            </center>
        </div>
    </form>
</body>
</html>
