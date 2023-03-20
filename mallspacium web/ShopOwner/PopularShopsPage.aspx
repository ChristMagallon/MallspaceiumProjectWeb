﻿<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never"  Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="PopularShopsPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- starts here-->
    <div class="form">
            <div class="col p-5">
                  
                <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Shop Name" aria-label="Search" AutoPostBack="True"></asp:TextBox>
        </div>

        <div class="form">
            <asp:GridView ID="shopsGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="shopName" OnRowDataBound="shopsGridView_RowDataBound" OnSelectedIndexChanged="shopsGridView_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="Shop Name" DataField="shopName" SortExpression="shopName" ></asp:BoundField>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Description" DataField="shopDescription" SortExpression="shopDescription"></asp:BoundField>
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

            <center> 
                <asp:Label ID="errorMessageLabel" runat="server" Visible="false"></asp:Label>
            </center>
            </div>
        </div>
</asp:Content>     
