<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master"  AutoEventWireup="true" CodeBehind="EditProfilePage.aspx.cs" Inherits="mallspacium_web.Shopper.EditProfilePage" %>

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
        <asp:TextBox ID="firstNameTextBox" runat="server" Text="" AutoCompleteType="Disabled"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="firstNameRequiredFieldValidator" runat="server"
            ControlToValidate="firstNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/>  


        <asp:Label ID="Label6" runat="server" Text="Last Name: "></asp:Label> 
        <asp:TextBox ID="lastNameTextBox" runat="server" Text="" AutoCompleteType="Disabled"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="lastNameRequiredFieldValidator" runat="server"
            ControlToValidate="lastNameTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label7" runat="server" Text="Birthday: "></asp:Label> 
        <asp:TextBox ID="birthdayTextbox" runat="server" Text="" type="text" TextMode="Date" AutoCompleteType="Disabled" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="birthdayRequiredFieldValidator" runat="server"
            ControlToValidate="birthdayTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label1" runat="server" Text="Gender: "></asp:Label>
        <asp:DropDownList ID="genderDropDownList" runat="server" ValidationGroup="Validate">
            <asp:ListItem Value="">--Select a Gender--</asp:ListItem>
            <asp:ListItem Value="Male">Male</asp:ListItem>
            <asp:ListItem Value="Female">Female</asp:ListItem>
            <asp:ListItem Value="Others">Others</asp:ListItem>
        </asp:DropDownList> 
        <asp:RequiredFieldValidator ID="genderRequiredFieldValidator" runat="server"
            ControlToValidate="genderDropDownList"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Phone Number: "></asp:Label> 
        <asp:TextBox ID="phoneNumberTextbox" runat="server" Text="" AutoCompleteType="Disabled"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="phoneNumberRequiredFieldValidator" runat="server"
            ControlToValidate="phoneNumberTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Address: "></asp:Label> 
        <asp:TextBox ID="addressTextbox" runat="server" Text="" AutoCompleteType="Disabled"></asp:TextBox> 
        <asp:RequiredFieldValidator ID="addressRequiredFieldValidator" runat="server"
            ControlToValidate="addressTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> <br /> 
        
        <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" /> <br/> <br />
   </div>
</asp:Content>  
