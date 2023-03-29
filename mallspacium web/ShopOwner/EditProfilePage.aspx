<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"  AutoEventWireup="true" CodeBehind="EditProfilePage.aspx.cs" Inherits="mallspacium_web.ShopOwner.EditProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
     <!-- starts here-->
   <div class="form">
    <div class="form-group">
        <asp:Label ID="Label8" runat="server" Text="Profile Pic: "></asp:Label>
        <div class="d-flex align-items-center">
            <asp:Image ID="Image1" runat="server" CssClass="mr-2" />
            <asp:HiddenField ID="imageHiddenField" runat="server" />
            <asp:Button ID="editImageButton" runat="server" Text="Edit" CssClass="btn btn-secondary" OnClick="editImageButton_Click"/>
        </div>
    </div>

    <div class="form-group">
        <asp:Label ID="Label4" runat="server" Text="First Name: "></asp:Label>
        <asp:TextBox ID="firstNameTextBox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="firstNameRequiredFieldValidator" runat="server"
            ControlToValidate="firstNameTextbox"
            ErrorMessage="*Required" 
            style="color: red" />
    </div>

    <div class="form-group">
        <asp:Label ID="Label6" runat="server" Text="Last Name: "></asp:Label>
        <asp:TextBox ID="lastNameTextBox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="lastNameRequiredFieldValidator" runat="server"
            ControlToValidate="lastNameTextbox"
            ErrorMessage="*Required" 
            style="color: red" />
    </div>

    <div class="form-group">
        <asp:Label ID="Label7" runat="server" Text="Shop Name: "></asp:Label>
        <asp:TextBox ID="shopNameTextbox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server"
            ControlToValidate="shopNameTextbox"
            ErrorMessage="*Required" 
            style="color: red" />
    </div>

    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Description: "></asp:Label>
        <asp:TextBox ID="descriptionTextbox" runat="server" Text="" TextMode="MultiLine" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="descriptionRequiredFieldValidator" runat="server"
            ControlToValidate="descriptionTextbox"
            ErrorMessage="*Required" 
            style="color: red" />
    </div>

    <div class="form-group">
        <asp:Label ID="Label3" runat="server" Text="Phone Number: "></asp:Label>
        <asp:TextBox ID="phoneNumberTextbox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="phoneNumberRequiredFieldValidator" runat="server"
            ControlToValidate="phoneNumberTextbox"
            ErrorMessage="*Required" 
            style="color: red" />
    </div>

    <div class="form-group">
        <asp:Label ID="Label5" runat="server" Text="Address: "></asp:Label>
        <asp:TextBox ID="addressTextbox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="addressRequiredFieldValidator" runat="server"
            ControlToValidate="addressTextbox"
            ErrorMessage="*Required" 
            style="color: red"  />
        </div>
        <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" /> <br/> <br />
       </div>
</asp:Content>  
