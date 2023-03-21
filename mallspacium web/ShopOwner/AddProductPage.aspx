<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="AddProductPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.AddProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form">

       <asp:Label ID="Label4" runat="server" Text="Add New Product"></asp:Label>  <br/> <br /> 

        <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label> 
        <asp:Textbox ID="nameTextbox" runat="server" Text="" AutoCompleteType="Disabled"></asp:Textbox>
        <asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server"
            ControlToValidate="nameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label2" runat="server" Text="Description: "></asp:Label> 
        <asp:Textbox ID="descriptionTextbox" runat="server" Text="" AutoCompleteType="Disabled" TextMode="MultiLine" Height="80px" Width="205px" ></asp:Textbox> 
        <asp:RequiredFieldValidator ID="descriptionTextboxValidator" runat="server"
            ControlToValidate="descriptionTextbox"
            ErrorMessage="*Required"
            style="color: red" /> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Price: "></asp:Label> 
        <asp:Textbox ID="priceTextbox" runat="server" Text="" AutoCompleteType="Disabled"></asp:Textbox> 
        <asp:RequiredFieldValidator ID="priceTextboxValidator" runat="server"
            ControlToValidate="priceTextbox"
            ErrorMessage="*Required"
            style="color: red" /> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Tag: "></asp:Label> 
        <asp:Textbox ID="tagTextbox" runat="server" Text="" AutoCompleteType="Disabled" ></asp:Textbox>
        <asp:RequiredFieldValidator ID="tagTextboxValidator" runat="server"
            ControlToValidate="tagTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 
        
        <asp:Label ID="Label6" runat="server" Text="Image: "></asp:Label> 
        <asp:FileUpload ID="imageFileUpload" runat="server" />
        <asp:RequiredFieldValidator ID="imageFileUploadValidator" runat="server"
            ControlToValidate="imageFileUpload"
            ErrorMessage="*Required" 
            style="color: red" /> <br/>

        <asp:Label ID="Label7" runat="server" Text="Shop Name: "></asp:Label> 
        <asp:Textbox ID="shopNameTextbox" runat="server" Text="" AutoCompleteType="Disabled"></asp:Textbox>
        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server"
            ControlToValidate="shopNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/>  <br>
        
        <asp:Button ID="addButton" runat="server" Text="Add Product" OnClick="addButton_Click" />
</div>
</asp:Content>
