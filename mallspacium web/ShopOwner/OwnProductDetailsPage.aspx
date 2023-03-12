<%@ Page  Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="OwnProductDetailsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.OwnProductDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!-- starts here-->

    <div class="form">

       <asp:Label ID="Label4" runat="server" Text="Edit Product"></asp:Label>  <br/> <br /> 

        <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label> 
        <asp:Textbox ID="nameTextbox" runat="server" Text=""></asp:Textbox>
        <asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server"
            ControlToValidate="nameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

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
        
        <asp:Label ID="Label6" runat="server" Text="Image: "></asp:Label> 
        <asp:FileUpload ID="imageFileUpload" runat="server" />
        <asp:RequiredFieldValidator ID="imageFileUploadValidator" runat="server"
            ControlToValidate="imageFileUpload"
            ErrorMessage="*Required" 
            style="color: red" /> <br/> <br>
        
        <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" />
    </div>
</asp:Content>