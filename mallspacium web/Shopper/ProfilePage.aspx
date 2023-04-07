<%@ Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="mallspacium_web.MasterForm3.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- starts here-->
    <div class="form">
        <asp:Image ID="Image1" runat="server" /> <br />

        <asp:Label ID="Label7" runat="server" Text="Full Name: "></asp:Label> 
        <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Button ID="editProfileButton" runat="server" Text="Edit Profile" OnClick="editProfileButton_Click" /> <br /> <br />


        <asp:Label ID="Label1" runat="server" Text="Birthday: "></asp:Label> 
        <asp:Label ID="birthdayLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label2" runat="server" Text="Gender: "></asp:Label> 
        <asp:Label ID="genderLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label4" runat="server" Text="Email: "></asp:Label> 
        <asp:Label ID="emailLabel" runat="server" Text=""></asp:Label> <br/>

        <asp:Label ID="Label3" runat="server" Text="Phone Number: "></asp:Label> 
        <asp:Label ID="phoneNumberLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Address: "></asp:Label> 
        <asp:Label ID="addressLabel" runat="server" Text=""></asp:Label> 
        <br />
        <br />
        <asp:Button ID="VerifyButton" runat="server" OnClick="VerifyButton_Click" Text="Verify" Width="100px" />
        <br/><br /> 
   </div>
</asp:Content>