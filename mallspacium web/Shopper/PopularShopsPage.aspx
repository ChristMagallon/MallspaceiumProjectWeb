<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never"  Async="true" Title=""  Language="C#"  MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="PopularShopsPage.aspx.cs" Inherits="mallspacium_web.MasterForm3.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- starts here-->

    <div class="container">
        <div class="row mb-3">
            <div class="col-md text-lg-end">
                <a href="MyFavoritePage.aspx" class="btn btn-primary btn-lg">My Favorite</a>
            </div>
        </div>
    </div>

    

    <div class="container mt-5">
    <div class="row">
        <div class="col-md-10 offset-1">
            <div class="card p-3">
                <div class="input-group">
                    <asp:TextBox ID="searchTextBox" runat="server" CssClass="form-control" type="search" placeholder="Search Shop Name" aria-label="Search" AutoPostBack="True" AutoCompleteType="Disabled" OnTextChanged="searchTextBox_TextChanged" ></asp:TextBox>
                    <div class="input-group-append">
                        <button class="btn btn-outline-secondary" type="button">
                            <i class="fa fa-search"></i>
                        </button>
                    </div>
                </div>


                <div class="table-responsive-xxl">
                    <asp:GridView ID="shopsGridView" CssClass="table table-bordered table-striped table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="shopName" OnRowDataBound="shopsGridView_RowDataBound" OnSelectedIndexChanged="shopsGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderText="Shop Name" DataField="shopName" SortExpression="shopName" ></asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" CssClass="img-fluid" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="shopDescription" SortExpression="shopDescription"></asp:BoundField>
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