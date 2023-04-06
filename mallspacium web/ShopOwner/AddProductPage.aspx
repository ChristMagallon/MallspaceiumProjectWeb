<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="AddProductPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.AddProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <div class="form">
    <center>  
        <h3 class="mb-3">Add New Product</h3> <br />
    </center>

    <div class="form-group">
        <label for="shopNameTextbox">Shop Name:</label>
        <asp:TextBox ID="shopNameTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server" ControlToValidate="shopNameTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="nameTextbox">Name:</label>
        <asp:TextBox ID="nameTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server" 
            ControlToValidate="nameTextbox" 
            ErrorMessage="*Required" 
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
        <label for="colorTextbox">Color:</label>
        <asp:TextBox ID="colorTextBox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="colorRequiredFieldValidator" runat="server" ControlToValidate="colorTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="sizeTextbox">Size:</label>
        <asp:TextBox ID="sizeTextBox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="sizeRequiredFieldValidator" runat="server" ControlToValidate="sizeTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="priceTextbox">Price:</label>
        <asp:TextBox ID="priceTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:RequiredFieldValidator ID="priceTextboxValidator" runat="server" ControlToValidate="priceTextbox" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label for="tagDropDownList">Tag:</label>
        <asp:DropDownList ID="tagDropDownList" runat="server"  CssClass="form-control" ValidationGroup="Validate">
            <asp:ListItem Value="">--Select a Tag--</asp:ListItem>
            <asp:ListItem Value="Home & Living">Home & Living</asp:ListItem>
            <asp:ListItem Value="Beauty & Health">Beauty & Health</asp:ListItem>
            <asp:ListItem Value="Sports & Outdoor">Sports & Outdoor</asp:ListItem>
            <asp:ListItem Value="Underwear & Sleepwear">Underwear & Sleepwear</asp:ListItem>
            <asp:ListItem Value="Women Clothing">Women Clothing</asp:ListItem>
            <asp:ListItem Value="Men Clothing">Men Clothing</asp:ListItem>
            <asp:ListItem Value="Kids">Kids</asp:ListItem>
            <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
            <asp:ListItem Value="Accessories">Accessories</asp:ListItem>
            <asp:ListItem Value="Bags & Luggage">Bags & Luggage</asp:ListItem>
            <asp:ListItem Value="Jewelry & Watches">Jewelry & Watches</asp:ListItem>
            <asp:ListItem Value="Baby">Baby</asp:ListItem>
            <asp:ListItem Value="Pet Supplies">Pet Supplies</asp:ListItem>
            <asp:ListItem Value="Office & School Supplies">Office & School Supplies</asp:ListItem>
            <asp:ListItem Value="Home Appliances">Home Appliances</asp:ListItem>
            <asp:ListItem Value="Tools & Home Improvement">Tools & Home Improvement</asp:ListItem>
            <asp:ListItem Value="Automotive">Automotive</asp:ListItem>
            <asp:ListItem Value="Gadgets">Gadgets</asp:ListItem>
            </asp:DropDownList> <br />
            <asp:RequiredFieldValidator ID="tagRequiredFieldValidator" runat="server" ControlToValidate="tagDropDownList" 
                InitialValue="" ErrorMessage="Required*" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <label for="availablityDropDownList">Availability:</label>
        <asp:DropDownList ID="availablityDropDownList" runat="server"  CssClass="form-control" ValidationGroup="Validate">
            <asp:ListItem Value="">--Select Availablity--</asp:ListItem>
            <asp:ListItem Value="Available">Available</asp:ListItem>
            <asp:ListItem Value="Out of Stock">Out of Stock</asp:ListItem>
            </asp:DropDownList> <br />
            <asp:RequiredFieldValidator ID="availabilityRequiredFieldValidator" runat="server" ControlToValidate="availablityDropDownList" 
                InitialValue="" ErrorMessage="Required*" ForeColor="Red" ValidationGroup="Validate"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <label for="imageFileUpload">Image:</label>
        <asp:FileUpload ID="imageFileUpload" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="imageFileUploadValidator" runat="server" ControlToValidate="imageFileUpload" 
            ErrorMessage="*Required" CssClass="text-danger" />
    </div>
    <div class="form-group">
        <asp:Button ID="addButton" runat="server" Text="Add Product" OnClick="addButton_Click" CssClass="btn btn-primary" />
    </div>
</div>

</asp:Content>
