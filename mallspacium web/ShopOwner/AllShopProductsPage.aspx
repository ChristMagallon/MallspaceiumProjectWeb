<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="AllShopProductsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.ShopOwnProductsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- starts here-->
    <div class="row">
         <div class="container">
             <center>  
                    <asp:HyperLink ID="allProducts" runat="server" NavigateUrl="~/ShopOwner/AllShopProductsPage.aspx" Text="All Products"></asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="ownProducts" runat="server" NavigateUrl="~/ShopOwner/MyShopProductsPage.aspx" Text="My Products"></asp:HyperLink>
             </center>
         </div>
    </div>

    <div class="form">
            <div class="col p-5">
                  
                <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Username" aria-label="Search" AutoPostBack="True"></asp:TextBox>
               
        </div>

        <div class="form">


            <asp:GridView ID="allShopProductGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" OnRowDataBound="allShopProductGridView_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Name" DataField="prodName" SortExpression="prodName" ></asp:BoundField>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Description" DataField="prodDesc" SortExpression="prodDesc"></asp:BoundField>
                    <asp:BoundField HeaderText="Price" DataField="prodPrice" SortExpression="prodPrice"></asp:BoundField>
                    <asp:BoundField HeaderText="Tag" DataField="prodTag" SortExpression="prodTag"></asp:BoundField>
                    <asp:BoundField HeaderText="Shop" DataField="prodShopName" SortExpression="prodShopName"></asp:BoundField>
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
