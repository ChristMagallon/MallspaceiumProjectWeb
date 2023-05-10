<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="MyShopProductsPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <!-- starts here-->

    <div class="container">
        <div class="row mb-3">
            <div class="col-md-6 text-center">
                <a href="AllShopProductsPage.aspx" class="btn btn-primary btn-lg">All Products</a>
            </div>

             <div class="col-md-6 text-center">
                <a href="MyShopProductsPage.aspx" class="btn btn-primary btn-lg">My Products</a>
            </div>
        </div>
    </div>

   <div class="container mt-5">
   <div class="row">
        <div class="col-md-10 offset-1">
            <div class="card p-3">
                <div class="input-group">
                    <asp:TextBox ID="searchTextBox" runat="server" CssClass="form-control" type="search" placeholder="Search Product Name" aria-label="Search" AutoPostBack="True" AutoCompleteType="Disabled" OnTextChanged="searchTextBox_TextChanged" ></asp:TextBox>
                    <div class="input-group-append">
                        <button class="btn btn-outline-secondary" type="button">
                            <i class="fa fa-search"></i>
                        </button>
                    </div>
                    <asp:Button ID="addButton" runat="server" Text="+ADD PRODUCT" OnClick="addButton_Click" class="btn btn-primary"  ></asp:Button>
                </div>
            
                <div class="table-responsive-xxl">
                    <asp:GridView ID="ownShopProductGridView" runat="server" CssClass="table table-bordered table-condensed table-responsive table-hover bg-white" AutoGenerateColumns="False" DataKeyNames="prodName" OnRowEditing="OwnShopProductGridView_RowEditing" OnRowDeleting="OwnShopProductGridView_RowDeleting" OnRowDataBound="ownShopProductGridView_RowDataBound" ShowHeaderWhenEmpty="True" EmptyDataText="No product posted.">
                        <EmptyDataTemplate>
                            <div style="text-align:center;">
                                No product posted.
                            </div>
                          </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="Name" DataField="prodName" SortExpression="prodName" />
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="prodDesc" SortExpression="prodDesc" />
                            <asp:BoundField HeaderText="Tag" DataField="prodTag" SortExpression="prodTag" />
                            <asp:CommandField ShowEditButton="True" ValidationGroup="EditButton" ButtonType="Button" ControlStyle-BackColor="#0066ff" ItemStyle-CssClass="text-center"> </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" ValidationGroup="DeleteButton" ButtonType="Button" ControlStyle-BackColor="#cc0000" ItemStyle-CssClass="text-center"> </asp:CommandField>
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
        </div>
    </div>
</div>
</asp:Content>
