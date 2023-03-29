<%@Page Async="true"Title="" Language="C#" MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="NotificationPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
    <div class="form">
  <div class="col p-5">
    <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Shop Name" aria-label="Search" AutoPostBack="True"></asp:TextBox>
  </div>

  <div class="form">
    <asp:GridView ID="NotificationGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False">
      <Columns>
        <asp:BoundField HeaderText="NOTIFICATION" DataField="Notification"></asp:BoundField>
      </Columns>
      <FooterStyle BackColor="White" ForeColor="#000066" />
      <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
      <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
      <RowStyle ForeColor="#000066" />
      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
      <SortedAscendingCellStyle BackColor="#F1F1F1" />
      <SortedAscendingHeaderStyle BackColor="#007DBB" />
      <SortedDescendingCellStyle BackColor="#CAC9C9" />
      <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>

    <div class="text-center mt-3">
      <asp:Label ID="errorMessageLabel" runat="server" Visible="false" class="text-danger"></asp:Label>
    </div>
  </div>
</div>

</asp:Content>
