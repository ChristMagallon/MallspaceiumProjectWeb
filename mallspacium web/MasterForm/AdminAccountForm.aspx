<%@ Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="AdminAccountForm.aspx.cs" Inherits="mallspacium_web.MasterForm.AdminAccountForm" %>

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

  <div class="row">
    <div class="col">
      <div class="form-group">
        <asp:TextBox ID="searchTextBox" runat="server" CssClass="form-control" type="search" placeholder="Search Username" aria-label="Search" AutoPostBack="True" OnTextChanged="searchTextBox_TextChanged"></asp:TextBox>
      </div>
    </div>
    <div class="col text-end">
      <asp:Button ID="addButton" runat="server" CssClass="btn btn-primary" Text="+CREATE NEW ACCOUNT" OnClick="addButton_Click"></asp:Button>
    </div>
  </div>

  <div class="form">
            <asp:GridView ID="accountGridView" CssClass="table table-striped table-bordered table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="adminUsername" AutoGenerateEditButton="true" OnRowEditing="accountGridView_RowEditing" >
                <Columns>
                    <asp:BoundField HeaderText="Username" DataField="adminUsername" SortExpression="adminUsername"  ItemStyle-CssClass="text-center" ></asp:BoundField>
                    <asp:BoundField HeaderText="ID" DataField="adminId" SortExpression="adminId"  ItemStyle-CssClass="text-center"></asp:BoundField>
                    <asp:BoundField HeaderText="Email Address" DataField="adminEmail" SortExpression="adminEmail"  ItemStyle-CssClass="text-center"></asp:BoundField>
                    <asp:BoundField HeaderText="Phone Number" DataField="adminPhoneNumber" SortExpression="adminPhoneNumber"  ItemStyle-CssClass="text-center"></asp:BoundField>
                    <asp:BoundField HeaderText="Date Created" DataField="adminDateCreated" SortExpression="adminDateCreated"  ItemStyle-CssClass="text-center"></asp:BoundField>
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
  </div>
</div>

</asp:Content>

