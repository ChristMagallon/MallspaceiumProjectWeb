<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="AllSaleDiscountDetailsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.AllSaleDiscountDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
 <!-- starts here-->
   <div class="container">
  <h2 class="mt-5 mb-4">Sales or Discount Details</h2>
  <div class="row mb-4">
    <div class="col-sm-3 col-md-2 col-lg-1">
      <label class="font-weight-bold">Shop Name:</label>
    </div>
    <div class="col-sm-9 col-md-10 col-lg-11">
      <asp:Label ID="shopNameLabel" runat="server" CssClass="form-control-plaintext" Text=""></asp:Label>
    </div>
  </div>
  <div class="row mb-4">
    <div class="col-sm-3 col-md-2 col-lg-1">
      <label class="font-weight-bold">Image:</label>
    </div>
    <div class="col-sm-9 col-md-10 col-lg-11">
      <asp:Image ID="Image1" runat="server" CssClass="img-fluid" />
    </div>
  </div>
  <div class="row mb-4">
    <div class="col-sm-3 col-md-2 col-lg-1">
      <label class="font-weight-bold">Description:</label>
    </div>
    <div class="col-sm-9 col-md-10 col-lg-11">
      <asp:Label ID="descriptionLabel" runat="server" CssClass="form-control-plaintext" Text=""></asp:Label>
    </div>
  </div>
  <div class="row mb-4">
    <div class="col-sm-3 col-md-2 col-lg-1">
      <label class="font-weight-bold">Start Date:</label>
    </div>
    <div class="col-sm-9 col-md-10 col-lg-11">
      <asp:Label ID="startDateLabel" runat="server" CssClass="form-control-plaintext" Text=""></asp:Label>
    </div>
  </div>
  <div class="row mb-4">
    <div class="col-sm-3 col-md-2 col-lg-1">
      <label class="font-weight-bold">End Date:</label>
    </div>
    <div class="col-sm-9 col-md-10 col-lg-11">
      <asp:Label ID="endDateLabel" runat="server" CssClass="form-control-plaintext" Text=""></asp:Label>
    </div>
  </div>
</div>


</asp:Content>
