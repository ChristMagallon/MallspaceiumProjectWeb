<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master"  AutoEventWireup="true" CodeBehind="EditProfilePage.aspx.cs" Inherits="mallspacium_web.Shopper.EditProfilePage" %>

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
        <asp:Label ID="Label7" runat="server" Text="Birthday: "></asp:Label>
        <asp:TextBox ID="birthdayTextbox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="birthdayRequiredFieldValidator" runat="server"
            ControlToValidate="birthdayTextbox"
            ErrorMessage="*Required" 
            style="color: red" />
    </div>

    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Gender: "></asp:Label>
        <asp:DropDownList ID="genderDropDownList" runat="server" ValidationGroup="Validate" CssClass="form-control">
            <asp:ListItem Value="">--Select a Gender--</asp:ListItem>
            <asp:ListItem Value="Male">Male</asp:ListItem>
            <asp:ListItem Value="Female">Female</asp:ListItem>
            <asp:ListItem Value="Others">Others</asp:ListItem>
        </asp:DropDownList> 
        <asp:RequiredFieldValidator ID="genderRequiredFieldValidator" runat="server"
            ControlToValidate="genderDropDownList"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> 
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
        <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" CssClass="btn btn-primary"/> <br/> <br />
   </div>
</asp:Content>  
