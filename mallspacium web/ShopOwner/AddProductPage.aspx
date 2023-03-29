<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="AddProductPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.AddProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <div class="form">
    <h3 class="mb-3">Add New Product</h3>

    <div class="form-group">
        <label for="nameTextbox">Name:</label>
        <asp:TextBox ID="nameTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server" ControlToValidate="nameTextbox" ErrorMessage="*Required" 
            CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="descriptionTextbox">Description:</label>
        <asp:TextBox ID="descriptionTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled" 
            TextMode="MultiLine" Rows="4"></asp:TextBox>
        <asp:RequiredFieldValidator ID="descriptionTextboxValidator" runat="server" ControlToValidate="descriptionTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="priceTextbox">Price:</label>
        <asp:TextBox ID="priceTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="priceTextboxValidator" runat="server" ControlToValidate="priceTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="tagTextbox">Tag:</label>
        <asp:TextBox ID="tagTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="tagTextboxValidator" runat="server" ControlToValidate="tagTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="imageFileUpload">Image:</label>
        <asp:FileUpload ID="imageFileUpload" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="imageFileUploadValidator" runat="server" ControlToValidate="imageFileUpload" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="shopNameTextbox">Shop Name:</label>
        <asp:TextBox ID="shopNameTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server" ControlToValidate="shopNameTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <asp:Button ID="addButton" runat="server" Text="Add Product" OnClick="addButton_Click" CssClass="btn btn-primary" />
</div>

</asp:Content>
