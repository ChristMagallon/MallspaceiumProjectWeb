<%@ Page Title="" Language="C#" MasterPageFile="Site2.Master" AutoEventWireup="true" Async="true" UnobtrusiveValidationMode="none" CodeBehind="AdvertiseProductsPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
    <!-- starts here-->
   <div class="container">
    <div class="row">
        <div class="col-md-6 mx-auto">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Add Advertise Product</h5>
                    <hr>
                    <div class="form-group">
                        <label for="productNameTextbox">Product Name:</label>
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
