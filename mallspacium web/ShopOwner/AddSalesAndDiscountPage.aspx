<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"   AutoEventWireup="true" CodeBehind="AddSalesAndDiscountPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.AddSalesAndDiscountPage" %>

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
                    <h5 class="card-title">Add New Sale and Discount</h5>
                    <hr>
                    <div class="form-group">
                        <label for="shopNameTextbox">Shop Name:</label>
                        <asp:Textbox ID="shopNameTextbox" runat="server" Text="" AutoCompleteType="Disabled" CssClass="form-control"></asp:Textbox>
                    </div>
                    <div class="form-group">
                        <label for="imageFileUpload">Image:</label>
                        <asp:FileUpload ID="imageFileUpload" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="imageFileUploadValidator" runat="server"
                            ControlToValidate="imageFileUpload"
                            ErrorMessage="*Required"
                            CssClass="text-danger" />
                    </div>
                    <div class="form-group">
                        <label for="descriptionTextbox">Description:</label>
                        <asp:Textbox ID="descriptionTextbox" runat="server" Text="" AutoCompleteType="Disabled" TextMode="MultiLine" Height="80px" Width="205px" CssClass="form-control"></asp:Textbox>
                        <asp:RequiredFieldValidator ID="descriptionTextboxValidator" runat="server"
                            ControlToValidate="descriptionTextbox"
                            ErrorMessage="*Required"
                            CssClass="text-danger" />
                    </div>
                    <div class="form-group">
                        <label for="startDateTextBox">Start Date:</label>
                        <asp:TextBox ID="startDateTextBox" runat="server" type="text" TextMode="Date" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="startDateTextboxValidator" runat="server"
                            ControlToValidate="startDateTextBox"
                            ErrorMessage="*Required"
                            CssClass="text-danger" />
                    </div>
                    <div class="form-group">
                        <label for="endDateTextBox">End Date:</label>
                        <asp:TextBox ID="endDateTextBox" runat="server" type="text" TextMode="Date" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="endDateTextboxValidator" runat="server"
                            ControlToValidate="endDateTextBox"
                            ErrorMessage="*Required"
                            CssClass="text-danger" />
                    </div>
                    <div class="form-group text-center">
                        <asp:Button ID="addButton" runat="server" Text="Add" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


</asp:Content>
