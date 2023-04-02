<%@ Page Async="true" Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="AdminActivitiesForm.aspx.cs" Inherits="mallspacium_web.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        /*}*/
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
      <link rel="stylesheet" href="Style.css" />

      <!-- start here -->
       <div class="container"> 
    <div class="form">
        <div class="col p-5">
                  
  <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Email" aria-label="Search" AutoPostBack="True" OnTextChanged="searchTextBox_TextChanged"></asp:TextBox>
        </div>
    </div>
     <div class="form">
        
            <asp:GridView ID="activityGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="id" SortExpression="id"></asp:BoundField>
                    <asp:BoundField HeaderText="Activity" DataField="activity" SortExpression="activity"></asp:BoundField>
                    <asp:BoundField HeaderText="Email" DataField="email" SortExpression="email"></asp:BoundField>
                    <asp:BoundField HeaderText="Date" DataField="date" SortExpression="date"></asp:BoundField>
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
