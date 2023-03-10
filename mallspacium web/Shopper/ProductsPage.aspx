<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ProductsPage.aspx.cs" Inherits="mallspacium_web.MasterForm3.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!-- start here -->
    <div class="container">
             <center> 
                    <asp:HyperLink ID="myWishlist" runat="server" NavigateUrl="~/Shopper/MyWishlistPage.aspx" Text="My Wishlist"></asp:HyperLink>
             </center>
         </div>

  <div class="form">
            <div class="col p-5">
                  
                <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Username" aria-label="Search" AutoPostBack="True"></asp:TextBox>
               
        </div>

        <div class="form">


            <asp:GridView ID="productGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="prodName" OnRowDataBound="productGridView_RowDataBound" AutoGenerateSelectButton="True" OnSelectedIndexChanged="productGridView_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                      <ItemTemplate>
                        <asp:Button ID="addToWishlistButton" runat="server" CommandName="AddToWishlist"  Text="Add to Wishlist" CommandArgument='<%# Eval("prodName") %>' />
                      </ItemTemplate>
                    </asp:TemplateField>
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
