<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/ShopOwner/Site2.Master" AutoEventWireup="true" CodeBehind="ProfileSetting.aspx.cs" Inherits="mallspacium_web.ShopOwner.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <table class="auto-style1">
          <tr>
              <td class="auto-style2">Turn on/off Notifications</td>
              <td>
                  <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></td>
              
          </tr>
          <tr>
              <td class="auto-style2">Download Account Data</td>
              <td><asp:Button ID="Button2" runat="server" Text="Button" /></td>
          </tr>
          <tr>
              <td class="auto-style2">Set Language</td>
              <td><asp:Button ID="Button3" runat="server" Text="Button" /></td>
          </tr>
          <tr>
              <td class="auto-style2">About Us</td>
              <td>
                  <asp:Button ID="Button4" runat="server" Text="Button" OnClick="Button4_Click" /></td>
          </tr>
          <tr>
              <td class="auto-style2">Help Center</td>
              <td><asp:Button ID="Button5" runat="server" Text="Button" /></td>
          </tr>
      </table>

      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />

  

</asp:Content>