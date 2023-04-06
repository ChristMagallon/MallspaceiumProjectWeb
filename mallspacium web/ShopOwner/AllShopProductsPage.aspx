<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="AllShopProductsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.ShopOwnProductsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <!-- starts here-->

    <div class="row">
         <div class="container">
             <center>  
                    <asp:HyperLink ID="allProducts" runat="server" NavigateUrl="~/ShopOwner/AllShopProductsPage.aspx" Text="All Products"></asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="ownProducts" runat="server" NavigateUrl="~/ShopOwner/MyShopProductsPage.aspx" Text="My Products"></asp:HyperLink> <br /> <br />
             </center>
         </div>
    </div>

    <div class="form">
    <div class="col p-5">
        <asp:TextBox ID="searchTextBox" runat="server" CssClass="form-control" Type="search" Placeholder="Search Username" aria-label="Search" AutoPostBack="True" AutoCompleteType="Disabled"></asp:TextBox>
    </div>
    
    <div class="form">
        <asp:GridView ID="allShopProductGridView" CssClass="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" DataKeyNames="prodName, prodShopName" AutoGenerateColumns="False" OnRowDataBound="allShopProductGridView_RowDataBound" OnSelectedIndexChanged="allShopProductGridView_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="Name" DataField="prodName" SortExpression="prodName" ></asp:BoundField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Description" DataField="prodDesc" SortExpression="prodDesc"></asp:BoundField>
                <asp:BoundField HeaderText="Tag" DataField="prodTag" SortExpression="prodTag" />
                <asp:BoundField HeaderText="Shop" DataField="prodShopName" SortExpression="prodShopName"></asp:BoundField>
            </Columns>
            <FooterStyle CssClass="bg-white" ForeColor="#000066" />
            <HeaderStyle CssClass="bg-primary text-white" Font-Bold="True" />
            <PagerStyle CssClass="bg-white" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle CssClass="bg-info text-white" Font-Bold="True" />
            <SortedAscendingCellStyle CssClass="bg-light" />
            <SortedAscendingHeaderStyle CssClass="bg-primary text-white" />
            <SortedDescendingCellStyle CssClass="bg-light" />
            <SortedDescendingHeaderStyle CssClass="bg-primary text-white" />
        </asp:GridView>
    </div>
</div>

</asp:Content>
