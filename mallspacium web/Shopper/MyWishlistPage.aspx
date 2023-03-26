<%@ Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="MyWishlistPage.aspx.cs" Inherits="mallspacium_web.Shopper.MyWishlistPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!-- start here -->
    <div class="form">
            <asp:GridView ID="myWishlistGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" OnRowDataBound="myWishlistGridView_RowDataBound" OnRowDeleting="myWishlistGridView_RowDeleting" >
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
                   <asp:CommandField DeleteText="Remove" ValidationGroup="RemoveButton" ShowDeleteButton="True" ButtonType="Button" />
                    
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