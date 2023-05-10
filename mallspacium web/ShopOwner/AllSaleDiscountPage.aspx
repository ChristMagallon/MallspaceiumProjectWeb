<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"  AutoEventWireup="true" CodeBehind="AllSaleDiscountPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <!-- starts here-->

    <div class="container">
        <div class="row mb-3">
            <div class="col-md-6 text-center">
                <a href="AllSaleDiscountPage.aspx" class="btn btn-primary btn-lg">All Sale or Discount</a>
            </div>

             <div class="col-md-6 text-center">
                <a href="MySaleDiscountPage.aspx" class="btn btn-primary btn-lg">My Sale or Discount</a>
            </div>
        </div>
    </div>

    <div class="container mt-5">
        <div class="row">
            <div class="col-md-10 offset-1">
                <div class="card p-3">
                    <div class="table-responsive-xxl">
                        <asp:GridView ID="allSaleDiscountGridView" class="table table-bordered table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="saleDiscShopName, saleDiscDesc" OnRowDataBound="allSaleDiscountGridView_RowDataBound" OnSelectedIndexChanged="allSaleDiscountGridView_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No sale or discount record.">
                            <EmptyDataTemplate>
                                <div style="text-align:center;">
                                    No sale or discount record.
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                            <asp:BoundField HeaderText="Shop Name" DataField="saleDiscShopName" SortExpression="saleDiscShopName"></asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="saleDiscDesc" SortExpression="saleDiscDesc"></asp:BoundField>
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
        </div>
    </div>
</div>
</asp:Content>