<%@ Page Async="true" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="UserDetailsPage.aspx.cs" Inherits="mallspacium_web.AdditionalForm.UserDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <!-- start here -->
       <div class="form">

        <asp:Label ID="Label1" runat="server" Text="Account Details"></asp:Label> <br/> <br /> 

        <asp:Label ID="Label4" runat="server" Text="Username: "></asp:Label> 
        <asp:Label ID="usernameLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label8" runat="server" Text="ID: "></asp:Label> 
        <asp:Label ID="idLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label2" runat="server" Text="Account Type: "></asp:Label> 
        <asp:Label ID="accountTypeLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Date Created: "></asp:Label> 
        <asp:Label ID="dateCreatedLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Email: "></asp:Label> 
        <asp:Label ID="emailLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label7" runat="server" Text="Address: "></asp:Label> 
        <asp:Label ID="addressLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label9" runat="server" Text="Contact Number: "></asp:Label> 
        <asp:Label ID="contactNumberLabel" runat="server" Text=""></asp:Label> <br/> <br/> 

       
        <asp:Button ID="banButton" runat="server" Text="BAN" BackColor="Red" OnClick="banButton_Click" /> &nbsp &nbsp &nbsp &nbsp
        <asp:Button ID="unbanButton" runat="server" Text="UNBAN" BackColor="Green" OnClick="unbanButton_Click" /> <br/> <br />

        <asp:Label ID="Label13" runat="server" Text="Warning Message"></asp:Label> <br/>
        <asp:TextBox ID="warningMessageTextbox" runat="server" Text="Type a message." Height="150px" Width="426px"></asp:TextBox> <br/> <br />
        <asp:Button ID="sendButton" runat="server" Text="Send" BackColor="Blue" /> <br/> 


    </div>

</asp:Content>
