<%@ Page  UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="MyShopProductDetailsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.OwnProductDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
<!-- starts here-->

    <div class="form">
          <asp:Label ID="label" runat="server" Text=""></asp:Label>
    <h2>Edit Product</h2>
    <asp:HiddenField ID="hfProductName" runat="server" />

    <div class="form-group">
        <asp:Label ID="Label8" runat="server" Text="Shop Name: "></asp:Label>
        <asp:Textbox ID="shopNameTextbox" runat="server" CssClass="form-control" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server"
            ControlToValidate="shopNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <asp:Label ID="Label7" runat="server" Text="ID: "></asp:Label>
        <asp:Textbox ID="idTextbox" runat="server" CssClass="form-control" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="IdTextBoxRequiredFieldValidator" runat="server"
            ControlToValidate="idTextbox"
            ErrorMessage="*Required"
            ForeColor="Red"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label>
        <asp:Textbox ID="nameTextbox" runat="server" CssClass="form-control" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server"
            ControlToValidate="nameTextbox"
            ErrorMessage="*Required"
            ForeColor="Red"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <asp:Label ID="Label6" runat="server" Text="Image: "></asp:Label>
        <asp:Image ID="Image1" runat="server" />
        <asp:HiddenField ID="imageHiddenField" runat="server" />
        <asp:Button ID="changeButton" runat="server" Text="CHANGE" CssClass="btn btn-primary" OnClick="changeButton_Click" />
    </div>

    <div class="form-group">
        <asp:Label ID="Label2" runat="server" Text="Description: "></asp:Label>
        <asp:Textbox ID="descriptionTextbox" runat="server" CssClass="form-control" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="descriptionTextboxValidator" runat="server"
            ControlToValidate="descriptionTextbox"
            ErrorMessage="*Required"
            ForeColor="Red"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <asp:Label ID="Label4" runat="server" Text="Color: "></asp:Label>
        <asp:Textbox ID="colorTextbox" runat="server" CssClass="form-control" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="colorRequiredFieldValidator" runat="server"
            ControlToValidate="colorTextbox"
            ErrorMessage="*Required"
            ForeColor="Red"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <asp:Label ID="Label9" runat="server" Text="Size: "></asp:Label>
        <asp:Textbox ID="sizeTextbox" runat="server" CssClass="form-control" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="sizeRequiredFieldValidator" runat="server"
            ControlToValidate="sizeTextbox"
            ErrorMessage="*Required"
            ForeColor="Red"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <asp:Label ID="Label3" runat="server" Text="Price: "></asp:Label>
        <asp:Textbox ID="priceTextbox" runat="server" CssClass="form-control" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="priceTextboxValidator" runat="server"
            ControlToValidate="priceTextbox"
            ErrorMessage="*Required"
            ForeColor="Red"></asp:RequiredFieldValidator>
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
         <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" />
    </div>
</div>
</asp:Content>