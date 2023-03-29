<%@ Page Async="true" Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="DowntimeForm.aspx.cs" Inherits="mallspacium_web.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
      <link rel="stylesheet" href="Style.css" />
    <!-- starts here-->
    <div class="container">
   <div class="row mb-3">
    <div class="col-md-6 text-center">
      <a href="DowntimeForm.aspx" class="btn btn-primary btn-lg">Downtime</a>
    </div>
    <div class="col-md-6 text-center">
      <a href="AdminAccountForm.aspx" class="btn btn-primary btn-lg">Admin Account</a>
    </div>
  </div>

      <div class="container mt-5">
  <div class="row justify-content-center">
    <div class="col-md-8">
      <div class="card">
        <div class="card-header">
          <h4 class="card-title">DownTime</h4>
        </div>
        <div class="card-body">
          <div class="mb-3">
            <div class="input-group">
              <asp:Calendar ID="Calendar1" runat="server" CssClass="form-control" />
            </div>
          </div>
          <div class="mb-3">
            <label for="startDate" class="form-label">Start Time:</label>
            <div class="input-group">
              <asp:TextBox ID="startDateTextbox" runat="server" Text="" CssClass="form-control" TextMode="DateTimeLocal" />
            </div>
          </div>
          <div class="mb-3">
            <label for="endDate" class="form-label">End Date:</label>
            <div class="input-group">
              <asp:TextBox ID="endDateTextbox" runat="server" Text="" CssClass="form-control" TextMode="DateTimeLocal" />
            </div>
          </div>
          <div class="mb-3">
            <label for="message" class="form-label">Message:</label>
            <div class="input-group">
              <asp:TextBox ID="messageTextbox" runat="server" Text="" CssClass="form-control" Height="58px" Width="364px" />
            </div>
          </div>
          <div class="d-grid">
            <asp:Button ID="saveButton" runat="server" Text="SAVE" OnClick="saveButton_Click" CssClass="btn btn-primary" />
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
        </div>
</asp:Content>
