<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="EditProfilePicturePage.aspx.cs" Inherits="mallspacium_web.ShopOwner.EditProfilePicturePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- starts here-->

    <div class="form">
            <div class="col p-5">
                <asp:Label ID="Label1" runat="server" Text="Changing the Image of the "></asp:Label> 
                <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label> <br /> <br /> <br />

                <asp:Label ID="Label" runat="server" Text="Image: "></asp:Label>
                <asp:FileUpload ID="imageFileUpload" runat="server" Height="35px" Width="219px" /> 
                <asp:RequiredFieldValidator ID="imageRequiredFieldValidator" runat="server" ControlToValidate="imageFileUpload" ErrorMessage="Please select an image." style="color: red"></asp:RequiredFieldValidator><br /> <br />

                <asp:Button ID="saveButton" runat="server" Text="SAVE" OnClick="saveButton_Click" />

           
        </div>
    </div>
</asp:Content>

