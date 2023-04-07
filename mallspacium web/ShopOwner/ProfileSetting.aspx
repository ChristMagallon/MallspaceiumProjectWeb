<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/ShopOwner/Site2.Master" AutoEventWireup="true" CodeBehind="ProfileSetting.aspx.cs" Inherits="mallspacium_web.ShopOwner.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
  <div class="form">
    <table class="table table-bordered table-condensed table-responsive bg-white">
        <tr>
            <td class="col-sm-8">Turn on/off Notifications</td>
            <td class="col-sm-4">
                <asp:Button ID="Button1" runat="server" Text="Switch" OnClick="Button1_Click" CssClass="btn btn-primary" Width="145px" />
            </td>
        </tr>
        <tr>
            <td class="col-sm-8">Download Account Data</td>
            <td class="col-sm-4">
                <asp:Button ID="Button2" runat="server" Text="Download Data" OnClick="Button2_Click1" CssClass="btn btn-primary" Width="145px" />
            </td>
        </tr>
        <tr>
            <td class="col-sm-8">About Us</td>
            <td class="col-sm-4">
                <asp:Button ID="Button4" runat="server" Text="More Info" OnClick="Button4_Click" CssClass="btn btn-primary" Width="145px" />
            </td>
        </tr>
        <tr>
            <td class="col-sm-8">Help Center</td>
            <td class="col-sm-4">
                <asp:Button ID="Button5" runat="server" Text="Contact Us" CssClass="btn btn-primary" Width="145px" />
            </td>
        </tr>
    </table>
</div>


</asp:Content>