<%@ Page Async="true" Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ManageUserForm.aspx.cs" Inherits="mallspacium_web.WebForm1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
      
    <!-- start here -->
    <div class="container"> 
    <div class="form">
        <div class="col p-5">  
            <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search" aria-label="Search" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        </div>
    </div>

     <div class="form">

         <asp:GridView ID="manageUsersGridView" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="auto-style1" Height="255px" Width="975px" DataKeyNames="username" EnableViewState="False" OnSelectedIndexChanged="manageUsersGridView_SelectedIndexChanged" EmptyDataText="No Record Found! Enter another Username.">
             <Columns>
            <asp:BoundField HeaderText="Username" DataField="username" SortExpression="username"/>
            <asp:BoundField HeaderText="ID" DataField="id" SortExpression="id"/>
            <asp:BoundField HeaderText="Account Type" DataField="accountType" SortExpression="accountType"/>
            <asp:BoundField HeaderText="Date Created" DataField="dateCreated" SortExpression="dateCreated"/>
            <asp:BoundField HeaderText="Email" DataField="email" SortExpression ="email"/>
            <asp:BoundField HeaderText="Address" DataField="address" SortExpression="address"/>
            <asp:BoundField HeaderText="Contact Number" DataField="contactNumber" SortExpression="contactNumber"/>
            <asp:CommandField ShowSelectButton="True" SelectText="View Data" />
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
    <br/> 
    <br/> 
    
        </div>
</asp:Content>


