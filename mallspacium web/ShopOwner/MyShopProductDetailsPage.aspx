<%@ Page  UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="MyShopProductDetailsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.OwnProductDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!-- starts here-->

    <div class="form">

       <asp:Label ID="Label4" runat="server" Text="Edit Product"></asp:Label>  <br/> <br /> 

        <asp:HiddenField ID="hfProductName" runat="server" />

        <asp:Label ID="Label7" runat="server" Text="ID: "></asp:Label> 
        <asp:Textbox ID="idTextbox" runat="server" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="IdTextBoxRequiredFieldValidator" runat="server"
            ControlToValidate="idTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label> 
        <asp:Textbox ID="nameTextbox" runat="server" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server"
            ControlToValidate="nameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label6" runat="server" Text="Image: "></asp:Label> 
        <asp:Image ID="Image1" runat="server" />
        <asp:HiddenField ID="imageHiddenField" runat="server" /> 
        <asp:Button ID="changeButton" runat="server" Text="CHANGE" OnClick="changeButton_Click" /> <br />

        <asp:Label ID="Label2" runat="server" Text="Description: "></asp:Label> 
        <asp:Textbox ID="descriptionTextbox" runat="server" Text=""></asp:Textbox> 
        <asp:RequiredFieldValidator ID="descriptionTextboxValidator" runat="server"
            ControlToValidate="descriptionTextbox"
            ErrorMessage="*Required"
            style="color: red" /> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Price: "></asp:Label> 
        <asp:Textbox ID="priceTextbox" runat="server" Text=""></asp:Textbox> 
        <asp:RequiredFieldValidator ID="priceTextboxValidator" runat="server"
            ControlToValidate="priceTextbox"
            ErrorMessage="*Required"
            style="color: red" /> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Tag: "></asp:Label> 
        <asp:Textbox ID="tagTextbox" runat="server" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="tagTextboxValidator" runat="server"
            ControlToValidate="tagTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label8" runat="server" Text="Shop Name: "></asp:Label> 
        <asp:Textbox ID="shopNameTextbox" runat="server" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server"
            ControlToValidate="shopNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> <br /> 
        
        <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" />
    </div>
</asp:Content>