<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="PopularShopDetailsPage.aspx.cs" Inherits="mallspacium_web.Shopper.PopularShopDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

    <!-- starts here-->

   <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="label" runat="server" Text="" Visible="false"></asp:Label> <br />
                <asp:Image ID="Image1" runat="server" />
                <asp:HiddenField ID="imageHiddenField" runat="server" />
            </div>

            <div class="col-md-6">
                <h4><asp:Label ID="nameLabel" runat="server" Text=""></asp:Label> </h4>
                <p> 
                    <asp:Button ID="reportButton" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="reportButton_Click"/> 
                    <asp:Button ID="addFavoriteButton" runat="server" Text="Add to Favorites" CssClass="btn btn-primary" OnClick="addFavoriteButton_Click"/>
                </p>
                <p><asp:Label ID="descriptionLabel" runat="server" Text=""></asp:Label></p>
                <p><i class="fas fa-envelope"></i> <asp:Label ID="emailLabel" runat="server" Text=""></asp:Label></p>
                <p><i class="fas fa-phone"></i> <asp:Label ID="phoneNumberLabel" runat="server" Text=""></asp:Label></p>
                <p><i class="fas fa-map-marker-alt"></i> <asp:Label ID="addressLabel" runat="server" Text=""></asp:Label></p> <br /> <br />
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">

                    <div class="card-header">
                        <h4 class="text-center">Products</h4>
                    </div>

                   <div class="card-body">
                        <asp:GridView ID="productGridView" CssClass="table table-bordered table-condensed table-hover bg-white" runat="server" DataKeyNames="prodShopName, prodName" AutoGenerateColumns="False" OnRowDataBound="productGridView_RowDataBound" OnSelectedIndexChanged="productGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderText="Name" DataField="prodName" SortExpression="prodName" />
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="prodDesc" SortExpression="prodDesc" />
                            <asp:BoundField HeaderText="Shop" DataField="prodShopName" SortExpression="prodShopName" />
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

    <br /> <br />
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">

                    <div class="card-header">
                        <h4 class="text-center">Sale Discounts</h4>
                    </div>

                    <div class="card-body">
                        <asp:GridView ID="saleDiscountGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="saleDiscShopName, saleDiscDesc" OnRowDataBound="saleDiscountGridView_RowDataBound" OnSelectedIndexChanged="saleDiscountGridView_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField HeaderText="Shop" DataField="saleDiscShopName" SortExpression="saleDiscShopName"></asp:BoundField>
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
</div>
    
</asp:Content>
