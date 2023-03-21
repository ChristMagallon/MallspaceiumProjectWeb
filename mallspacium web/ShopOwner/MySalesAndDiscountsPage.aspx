﻿<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"  AutoEventWireup="true" CodeBehind="MySalesAndDiscountsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.MySalesAndDiscountsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- starts here-->
    <div class="row">
         <div class="container">
             <center>  
                    <asp:HyperLink ID="allSalesDiscounts" runat="server" NavigateUrl="~/ShopOwner/AllSalesAndDiscountsPage.aspx" Text="All Sales and Discounts"></asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="mySalesDiscounts" runat="server" NavigateUrl="~/ShopOwner/MySalesAndDiscountsPage.aspx" Text="My Sales and Discounts"></asp:HyperLink>
             </center>
         </div>
    </div>

    <div class="form">
        <asp:Button ID="addButton" runat="server" Text="+ SALES AND DISCOUNTS" OnClick="addButton_Click" />
        <div class="form">
            <asp:GridView ID="allSalesAndDiscountGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" OnRowDataBound="allSalesAndDiscountGridView_RowDataBound" OnRowDeleting="allSalesAndDiscountGridView_RowDeleting">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="saleDiscId" SortExpression="saleDiscId"></asp:BoundField>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Description" DataField="saleDiscDesc" SortExpression="saleDiscDesc"></asp:BoundField>
                    <asp:CommandField ShowDeleteButton="True" ValidationGroup="DeleteButton" ButtonType="Button" ControlStyle-BackColor="Red" >
<ControlStyle BackColor="Red" BorderColor="Red" BorderStyle="Outset"></ControlStyle>
                    </asp:CommandField>
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
