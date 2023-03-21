<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"  AutoEventWireup="true" CodeBehind="AllSaleDiscountPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- starts here-->
    <div class="row">
         <div class="container">
             <center>  
                    <asp:HyperLink ID="allSalesDiscounts" runat="server" NavigateUrl="~/ShopOwner/AllSaleDiscountPage.aspx" Text="All Sale or Discount"></asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="mySalesDiscounts" runat="server" NavigateUrl="~/ShopOwner/MySaleDiscountPage.aspx" Text="My Sale or Discount"></asp:HyperLink>
             </center>
         </div>
    </div>

    <div class="form">
        <div class="form">
            <asp:GridView ID="allSaleDiscountGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" OnRowDataBound="allSaleDiscountGridView_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Description" DataField="saleDiscDesc" SortExpression="saleDiscDesc"></asp:BoundField>
                    <asp:BoundField HeaderText="Start Date" DataField="saleDiscStartDate" SortExpression="saleDiscEndDate"></asp:BoundField>
                    <asp:BoundField HeaderText="End Date" DataField="saleDiscEndDate" SortExpression="saleDiscEndDate"></asp:BoundField>
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