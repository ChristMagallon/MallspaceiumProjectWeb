<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"  AutoEventWireup="true" CodeBehind="EditProfilePage.aspx.cs" Inherits="mallspacium_web.ShopOwner.EditProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- starts here-->
    <div class="form">
         <asp:Label ID="Label8" runat="server" Text="Profile Pic: "></asp:Label> 
        <asp:Image ID="Image1" runat="server" />
        <asp:HiddenField ID="imageHiddenField" runat="server" />
        <asp:Button ID="editImageButton" runat="server" Text="Edit" OnClick="editImageButton_Click"/> <br /> <br />

        <asp:Label ID="Label4" runat="server" Text="First Name: "></asp:Label> 
        <asp:TextBox ID="firstNameTextBox" runat="server" Text=""></asp:TextBox> 
        <asp:RequiredFieldValidator ID="firstNameRequiredFieldValidator" runat="server"
            ControlToValidate="firstNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/>  


        <asp:Label ID="Label6" runat="server" Text="Last Name: "></asp:Label> 
        <asp:TextBox ID="lastNameTextBox" runat="server" Text=""></asp:TextBox> 
        <asp:RequiredFieldValidator ID="lastNameRequiredFieldValidator" runat="server"
            ControlToValidate="lastNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label7" runat="server" Text="Shop Name: "></asp:Label> 
        <asp:TextBox ID="shopNameTextbox" runat="server" Text=""></asp:TextBox>
        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server"
            ControlToValidate="shopNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label1" runat="server" Text="Description: "></asp:Label> 
        <asp:TextBox ID="descriptionTextbox" runat="server" Text=""></asp:TextBox> 
        <asp:RequiredFieldValidator ID="descriptionRequiredFieldValidator" runat="server"
            ControlToValidate="descriptionTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label2" runat="server" Text="Email: "></asp:Label> 
        <asp:TextBox ID="emailTextbox" runat="server" Text=""></asp:TextBox> 
        <asp:RequiredFieldValidator ID="emailRequiredFieldValidator" runat="server"
            ControlToValidate="emailTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Phone Number: "></asp:Label> 
        <asp:TextBox ID="phoneNumberTextbox" runat="server" Text=""></asp:TextBox> 
        <asp:RequiredFieldValidator ID="phoneNumberRequiredFieldValidator" runat="server"
            ControlToValidate="phoneNumberTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Address: "></asp:Label> 
        <asp:TextBox ID="addressTextbox" runat="server" Text=""></asp:TextBox> 
        <asp:RequiredFieldValidator ID="addressRequiredFieldValidator" runat="server"
            ControlToValidate="addressTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> <br /> 
        
        <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" /> <br/> <br />
   </div>
</asp:Content>  
