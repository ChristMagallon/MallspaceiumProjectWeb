<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="EditProfilePicturePage.aspx.cs" Inherits="mallspacium_web.ShopOwner.EditProfilePicturePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
    <!-- starts here-->

<div class="container">
  <div class="row justify-content-center">
    <div class="col-md-6">
      <div class="card mt-5">
        <div class="card-header bg-primary text-white">
          <h4 class="mb-0">Change Profile Picture</h4>
        </div>
        <div class="card-body">
          <div class="form-group">
              <center>
                  <asp:Label ID="Label2" runat="server" Text="Changing the Image of " CssClass="font-weight-bold" ></asp:Label> 
                  <asp:Label ID="nameLabel" runat="server" CssClass="font-weight-bold"> </asp:Label> 
              </center>
          </div>
          <div class="form-group">
            <label for="imageFileUpload">Image:</label>
            <asp:FileUpload ID="imageFileUpload" runat="server" Height="35px" Width="219px" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="imageRequiredFieldValidator" runat="server" 
                  ControlToValidate="imageFileUpload" 
                  ErrorMessage="Please select an image." 
                  CssClass="text-danger"></asp:RequiredFieldValidator>
          </div>
          <div class="form-group text-center">
            <asp:Button ID="saveButton" runat="server" Text="SAVE" OnClick="saveButton_Click" CssClass="btn btn-primary" />
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
</asp:Content>

