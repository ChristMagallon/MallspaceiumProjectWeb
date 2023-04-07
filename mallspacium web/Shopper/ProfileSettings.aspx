<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Shopper/Site1.Master" AutoEventWireup="true" CodeBehind="ProfileSettings.aspx.cs" Inherits="mallspacium_web.Shopper.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <!-- starts here-->

      <div class="form">
        <table class="table table-bordered table-condensed table-responsive bg-white">
            <tr>
              <td class="col-sm-8">Turn on/off Notifications</td>
              <td class="col-sm-4">
                  <asp:Button ID="Button1" runat="server" Text="Switch" OnClick="Button1_Click"  CssClass="btn btn-primary" Width="145px" />
              </td>        
          </tr>
          <tr>
            <td class="col-sm-8">Download Account Data</td>
            <td class="col-sm-4">
                  <asp:Button ID="Button2" runat="server" Text="Download Data" OnClick="Button2_Click"  CssClass="btn btn-primary" Width="145px" />
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
                  <button type="button" class="btn btn-primary" style="width: 150px;" data-bs-toggle="modal" data-bs-target="#myModal">Contact Us</button>
            </td>
        </tr>
      </table>
    </div>

     <!-- The Modal -->
<div class="modal fade" id="myModal">
  <div class="modal-dialog">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">CONTACT US</h4>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>

      <!-- Modal body -->
      <div class="modal-body">
        <p>Please email us at <a href="mailto:sysadm1n.mallspaceium@gmail.com">sysadm1n.mallspaceium@gmail.com</a></p>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
      </div>

    </div>
  </div>
</div>
</asp:Content>
