<%@ Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="AccountDetailsForm.aspx.cs" Inherits="mallspacium_web.MasterForm.AdminAccountDetailsForm" %>

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
    <!-- starts here-->
    <div class="form">

        <asp:Label ID="Label1" runat="server" Text="Edit Account"></asp:Label> <br/> <br /> 

        <asp:Label ID="Label4" runat="server" Text="Username: "></asp:Label> 
        <asp:Textbox ID="usernameTextbox" runat="server" Text=""></asp:Textbox> <br/> 

        <asp:Label ID="Label8" runat="server" Text="Id: "></asp:Label> 
        <asp:Textbox ID="idTextbox" runat="server" Text=""></asp:Textbox> <br/> 

        <asp:Label ID="Label2" runat="server" Text="Email "></asp:Label> 
        <asp:Textbox ID="emailTextbox" runat="server" Text=""></asp:Textbox> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Phone Number: "></asp:Label> 
        <asp:Textbox ID="phoneNumberTextbox" runat="server" Text=""></asp:Textbox> <br/> 

        <asp:Label ID="Label5" runat="server" Text="Date Created: "></asp:Label> 
        <asp:Textbox ID="dateCreatedTextbox" runat="server" Text=""></asp:Textbox> <br/> 

        <asp:Label ID="Label6" runat="server" Text="Password: "></asp:Label> 
        <asp:Textbox ID="passwordTextbox" runat="server" Text="" ></asp:Textbox> <br/> 

        <asp:Label ID="Label7" runat="server" Text="Confirm Password: "></asp:Label> 
        <asp:Textbox ID="confirmPasswordTextbox" runat="server" Text=""></asp:Textbox> <br/> <br/>
        <div> 
            <asp:Button ID="updateButton" runat="server" Text="UPDATE" OnClick="updateButton_Click" /> <br />
        <asp:Button ID="deleteButton" runat="server" Text="DELETE" OnClick="deleteButton_Click" />

        </div>
        
</div>

</asp:Content>
