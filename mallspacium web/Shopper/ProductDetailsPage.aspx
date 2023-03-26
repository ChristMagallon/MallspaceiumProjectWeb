<%@ Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ProductDetailsPage.aspx.cs" Inherits="mallspacium_web.Shopper.ProductDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!-- start here -->

    <div class="form">
        <asp:Label ID="label" runat="server" Text=""></asp:Label>  <br/> <br /> 

        <asp:Label ID="Label7" runat="server" Text="Product Name: "></asp:Label> 
        <asp:Label ID="productNameLabel" runat="server" Text=""></asp:Label> <br/>

        <asp:Image ID="Image1" runat="server" />
        <asp:HiddenField ID="imageHiddenField" runat="server" /> <br />

        <asp:Label ID="Label1" runat="server" Text="Description: "></asp:Label> 
        <asp:Label ID="descriptionLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label2" runat="server" Text="Price: "></asp:Label> 
        <asp:Label ID="priceLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Tag: "></asp:Label> 
        <asp:Label ID="tagLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Shop Name: "></asp:Label> 
        <asp:Label ID="shopNameLabel" runat="server" Text=""></asp:Label> <br/> <br /> 

        <asp:Button ID="addWishlistButton" runat="server" Text="ADD TO WISHLIST" OnClick="addWishlistButton_Click" />
   </div>

</asp:Content>