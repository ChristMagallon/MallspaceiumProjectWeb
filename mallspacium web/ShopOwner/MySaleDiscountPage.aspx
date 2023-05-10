<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"  AutoEventWireup="true" CodeBehind="MySaleDiscountPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.MySalesAndDiscountsPage" %>

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
                <div class="card-body">
                    <div class="form-group row">
                        <div class="col-6 col-md-4">
                            <asp:Button ID="addButton" runat="server" Text="+ SALES AND DISCOUNTS" OnClick="addButton_Click" CssClass="btn btn-primary" />
                        </div>
                    </div>

                    <div class="table-responsive-xxl">
                    <asp:GridView ID="mySaleDiscountGridView" class="table table-bordered table-condensed table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="saleDiscId" OnRowDataBound="mySaleDiscountGridView_RowDataBound" OnRowDeleting="mySaleDiscountGridView_RowDeleting" ShowHeaderWhenEmpty="True" EmptyDataText="No sales or discount posted.">
                        <EmptyDataTemplate>
                            <div style="text-align:center;">
                                No sales or discount posted.
                            </div>
                          </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="saleDiscId" SortExpression="saleDiscId"></asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="saleDiscDesc" SortExpression="saleDiscDesc"></asp:BoundField>
                            <asp:BoundField HeaderText="Start Date" DataField="saleDiscStartDate" SortExpression="saleDiscEndDate"></asp:BoundField>
                            <asp:BoundField HeaderText="End Date" DataField="saleDiscEndDate" SortExpression="saleDiscEndDate"></asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" ValidationGroup="DeleteButton" ButtonType="Button" ControlStyle-BackColor="Red">
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
            </div>
        </div>
    </div>
</div>

</asp:Content>
