<%@ Page Title="" Language="C#" MasterPageFile="Site2.Master" AutoEventWireup="true" Async="true" UnobtrusiveValidationMode="none" CodeBehind="AdvertiseProductsPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
    <!-- starts here-->
    <div class="container mt-5">
    <div class="row">
        <div class="col-md-6 offset-md-3">
            <div class="card p-3">
                <div class="input-group">
                    <asp:TextBox ID="searchTextBox" runat="server" CssClass="form-control" type="search" placeholder="Search Product Advertisement Name" aria-label="Search" AutoPostBack="True" AutoCompleteType="Disabled" OnTextChanged="searchTextBox_TextChanged" ></asp:TextBox>
                    <div class="input-group-append">
                        <button class="btn btn-outline-secondary" type="button">
                            <i class="fa fa-search"></i>
                        </button>
                    </div>
                </div>

                <div class="table-responsive mt-3">
                    <asp:GridView ID="advertisementGridView" CssClass="table table-bordered table-striped table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="adsProdId" OnRowDataBound="advertisementGridView_RowDataBound" OnRowDeleting="advertisementGridView_RowDeleting" >
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="adsProdId" SortExpression="adsProdId" ></asp:BoundField>
                            <asp:BoundField HeaderText="Advertisment Name" DataField="adsProdName" SortExpression="adsProdName" ></asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" CssClass="img-fluid" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="adsProdDesc" SortExpression="adsProdDesc"></asp:BoundField>
                            <asp:BoundField HeaderText="Shop Name" DataField="adsProdShopName" SortExpression="adsProdShopName"></asp:BoundField>
                            <asp:BoundField HeaderText="Date Posted" DataField="adsProdDate" SortExpression="adsProdDate"></asp:BoundField>
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
    <br /> <br />


   <div class="container">
    <div class="row">
        <div class="col-md-6 mx-auto">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Add Advertise Product</h5>
                    <hr>
                    <div class="form-group">
                        <label for="shopNameTextbox">Shop Name:</label>
                        <asp:TextBox ID="shopNameTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="shopNameRequiredFieldValidator" runat="server" ControlToValidate="shopNameTextbox" 
                            ErrorMessage="*Required" CssClass="text-danger" />
                    </div>
                    <div class="form-group">
                        <label for="productNameTextbox">Product Advertisement Name:</label>
                        <asp:Textbox ID="ProductNameTextbox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control" ValidationGroup="Validate"></asp:Textbox>
                        <asp:RequiredFieldValidator ID="ProductNameRequiredFieldValidator" runat="server"
                            ControlToValidate="ProductNameTextbox"
                            ErrorMessage="*Required"
                            CssClass="text-danger" ValidationGroup="Validate" />
                    </div>
                    <div class="form-group">
                        <label for="imageFileUpload">Image:</label>
                        <asp:FileUpload ID="imageFileUpload" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="imageFileUploadValidator" runat="server"
                            ControlToValidate="imageFileUpload"
                            ErrorMessage="*Required"
                            CssClass="text-danger" ValidationGroup="Validate" />
                    </div>
                    <div class="form-group">
                        <label for="descriptionTextbox">Product Description:</label>
                        <asp:Textbox ID="DescriptionTextbox" runat="server" Text="" AutoCompleteType="Disabled" TextMode="MultiLine" Height="80px" Width="205px" CssClass="form-control" ValidationGroup="Validate"></asp:Textbox>
                        <asp:RequiredFieldValidator ID="DescriptionTextboxValidator" runat="server"
                            ControlToValidate="DescriptionTextbox"
                            ErrorMessage="*Required"
                            CssClass="text-danger" ValidationGroup="Validate" />                   
                    </div>
                    <div class="form-group text-center">
                        <asp:Button ID="addButton" runat="server" Text="Add" OnClick="addButton_Click" CssClass="btn btn-primary" ValidationGroup="Validate" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


</asp:Content>
