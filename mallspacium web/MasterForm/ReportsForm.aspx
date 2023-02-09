<%@ Page Title="" Language="C#" MasterPageFile="./Site1.Master" AutoEventWireup="true" CodeBehind="ReportsForm.aspx.cs" Inherits="mallspacium_web.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
      

       <div class="container"> 
    <div class="form">
        <div class="col p-5">
                  
  <asp:TextBox ID="TextBox1" runat="server" class="form-control" type="search" placeholder="Search" aria-label="Search"></asp:TextBox>
        </div>
    </div>
     <div class="form">
        
            <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center">
        <Columns>
            <asp:BoundField HeaderText="USER"></asp:BoundField>
            <asp:BoundField HeaderText="ID"></asp:BoundField>
            <asp:BoundField HeaderText="ROLE"></asp:BoundField>
            <asp:BoundField HeaderText="DATE"></asp:BoundField>
            <asp:BoundField HeaderText="STATUS"></asp:BoundField>
        </Columns>
    </asp:GridView>
    </div>
   
        </div>
</asp:Content>
