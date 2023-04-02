<%@ Page Async="true" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="UserDetailsPage.aspx.cs" Inherits="mallspacium_web.AdditionalForm.UserDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
      <link rel="stylesheet" href="Style.css" />
    <!-- start here -->
   <div class="container">
    <div class="form">
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Account Details" CssClass="h3"></asp:Label>
        </div>
        
        <div class="form-group">
            <asp:Label ID="Label4" runat="server" Text="Username: "></asp:Label> 
            <asp:Label ID="usernameLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Account Type: "></asp:Label> 
            <asp:Label ID="userRoleLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Email: "></asp:Label> 
            <asp:Label ID="emailLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label7" runat="server" Text="Address: "></asp:Label> 
            <asp:Label ID="addressLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label9" runat="server" Text="Contact Number: "></asp:Label> 
            <asp:Label ID="contactNumberLabel" runat="server" Text="" CssClass="form-control"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Button ID="banButton" runat="server" Text="BAN" CssClass="btn btn-danger" OnClick="banButton_Click" /> 
            <asp:Button ID="unbanButton" runat="server" Text="UNBAN" CssClass="btn btn-success" OnClick="unbanButton_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="Label13" runat="server" Text="Warning Message" CssClass="h4"></asp:Label>
        </div>

        <div class="form-group">
            <asp:TextBox ID="warningMessageTextbox" runat="server" hint="Warning Message Here" 
                TextMode="MultiLine" 
                AutoCompleteType="Disabled" 
                CssClass="form-control" 
                Rows="5"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Button ID="sendButton" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="sendButton_Click" />
        </div>
    </div>
</div>

</asp:Content>
