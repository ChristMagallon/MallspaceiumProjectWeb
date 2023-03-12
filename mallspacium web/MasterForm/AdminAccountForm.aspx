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
    <!-- starts here-->
    <div class="row">
         <div class="container">
             <center>  
                    <asp:HyperLink ID="downtime" runat="server" NavigateUrl="~/MasterForm/DowntimeForm.aspx" Text="Downtime"></asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="adminAccount" runat="server" NavigateUrl="~/MasterForm/AdminAccountForm.aspx" Text="Admin Account"></asp:HyperLink>
             </center>
         </div>
    </div>

        <div class="row">
            <div class="container">
            </div>
        </div>
        
        <div class="form">
            <div class="col p-5">
                  
                <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Username" aria-label="Search" AutoPostBack="True" OnTextChanged="searchTextBox_TextChanged"></asp:TextBox>
                <asp:Button ID="addButton" runat="server" Text="+CREATE NEW ACCOUNT" OnClick="addButton_Click"> </asp:Button>
        </div>
    </div>

        <div class="form">
                 <asp:GridView ID="accountGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="adminUsername" AutoGenerateEditButton="true" OnRowEditing="accountGridView_RowEditing" >
                <Columns>
                    <asp:BoundField HeaderText="Username" DataField="adminUsername" SortExpression="adminUsername" ></asp:BoundField>
                    <asp:BoundField HeaderText="ID" DataField="adminId" SortExpression="adminId"></asp:BoundField>
                    <asp:BoundField HeaderText="Email Address" DataField="adminEmail" SortExpression="adminEmail"></asp:BoundField>
                    <asp:BoundField HeaderText="Phone Number" DataField="adminPhoneNumber" SortExpression="adminPhoneNumber"></asp:BoundField>
                    <asp:BoundField HeaderText="Date Created" DataField="adminDateCreated" SortExpression="adminDateCreated"></asp:BoundField>
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
   
</asp:Content>

