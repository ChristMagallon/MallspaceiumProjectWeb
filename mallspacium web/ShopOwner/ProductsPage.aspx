<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="ProductsPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form">
            <div class="col p-5">
                  
                <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Username" aria-label="Search" AutoPostBack="True" OnTextChanged="searchTextBox_TextChanged"></asp:TextBox>
                <asp:Button ID="addButton" runat="server" Text="+ADD PRODUCT" OnClick="addButton_Click"> </asp:Button>
        </div>
        </div>
</asp:Content>
