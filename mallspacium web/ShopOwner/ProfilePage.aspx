<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
     <!-- starts here-->
   <div class="container">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card">
                <div class="card-body">
                    <center>
                        <asp:Image ID="Image1" runat="server" CssClass="img-fluid mb-3" /> <br />
                        <asp:Button ID="editProfileButton" runat="server" Text="Edit Profile" OnClick="editProfileButton_Click" CssClass="btn btn-primary btn-sm mb-3" />
                    </center>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Shop Name:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="nameLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Description:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="descriptionLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Email:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="emailLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Phone Number:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="phoneNumberLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Address:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="addressLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <div class="row justify-content-center mt-5">
        <div class="col-md-8">
            <div class="form-group">
                <asp:TextBox ID="productSearchTextBox" runat="server" class="form-control" type="search" placeholder="Search Product Name" aria-label="Search" AutoPostBack="True" OnTextChanged="productSearchTextBox_TextChanged"></asp:TextBox> <br /> <br />
            </div>
        </div>
    </div>
       <div class="container">
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="productGridView" CssClass="table table-striped table-bordered table-hover" DataKeyNames="prodName, prodShopName"  runat="server" AutoGenerateColumns="False" OnRowDataBound="productGridView_RowDataBound" OnSelectedIndexChanged="productGridView_SelectedIndexChanged" >
                <Columns>
                    <asp:BoundField HeaderText="Name" DataField="prodName" SortExpression="prodName" ></asp:BoundField>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Description" DataField="prodDesc" SortExpression="prodDesc"></asp:BoundField>
                    <asp:BoundField HeaderText="Tag" DataField="prodTag" SortExpression="prodTag"></asp:BoundField>
                    <asp:BoundField HeaderText="Shop" DataField="prodShopName" SortExpression="prodShopName"></asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-danger" role="alert">
                        No products found.
                    </div>
                </EmptyDataTemplate>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#007bff" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <center>
                <asp:Label ID="productErrorMessageLabel" runat="server" Visible="false"></asp:Label> <br /> <br />
            </center>
        </div>
    </div>
</div>

       <div class="container">
    <div class="row">
        <div class="col-md-12">
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
</asp:Content>        