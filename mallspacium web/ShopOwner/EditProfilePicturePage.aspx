<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="EditProfilePicturePage.aspx.cs" Inherits="mallspacium_web.ShopOwner.EditProfilePicturePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
    <!-- starts here-->

<div class="form">
    <div class="col p-5">
        <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Changing the Image of "></asp:Label> 
        <asp:Label ID="nameLabel" runat="server" CssClass="form-label"></asp:Label> <br /> <br /> <br />

        <asp:Label ID="Label" runat="server" CssClass="form-label" Text="Image: "></asp:Label>
        <asp:FileUpload ID="imageFileUpload" runat="server" CssClass="form-control" Height="35px" Width="219px" /> 
        <asp:RequiredFieldValidator ID="imageRequiredFieldValidator" runat="server" ControlToValidate="imageFileUpload" ErrorMessage="Please select an image." style="color: red"></asp:RequiredFieldValidator><br /> <br />

        <asp:Button ID="saveButton" runat="server" CssClass="btn btn-primary" Text="SAVE" OnClick="saveButton_Click" />
    </div>
</div>


</asp:Content>

