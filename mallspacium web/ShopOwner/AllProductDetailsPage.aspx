<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="AllProductDetailsPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.ProductDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

<!-- start here -->

 <div class="container">
  <div class="row justify-content-center">
    <div class="col-md-8 col-lg-6">
      <div class="card border-0 shadow">
        <div class="card-body">
            <center> 
                <h5 class="card-title mb-4">Product Details</h5>
                <asp:Label ID="label" runat="server" Text=""></asp:Label> 
            </center>
          <hr>
          <div class="row">
            <div class="form-group">
              <asp:Label ID="Label7" runat="server" Text="Product Name: "></asp:Label> 
              <asp:Label ID="productNameLabel" runat="server" Text=""></asp:Label> <br /> <br />
            </div>

            <div class="form-group">
              <asp:Label ID="Label4" runat="server" Text="Description: "></asp:Label> 
              <asp:Label ID="descriptionLabel" runat="server" Text=""></asp:Label> <br /> <br />
            </div>

            <div class="form-group">
              <asp:Label ID="Label1" runat="server" Text="Color: "></asp:Label> 
              <asp:Label ID="colorLabel" runat="server" Text=""></asp:Label> <br /> <br />
            </div>

            <div class="form-group">
              <asp:Label ID="Label6" runat="server" Text="Size: "></asp:Label> 
              <asp:Label ID="sizeLabel" runat="server" Text=""></asp:Label> <br /> <br />
            </div>

            <div class="form-group">
              <asp:Label ID="Label2" runat="server" Text="Price: "></asp:Label> 
              <asp:Label ID="priceLabel" runat="server" Text=""></asp:Label> <br /> <br />
            </div>

            <div class="form-group">
              <asp:Label ID="Label3" runat="server" Text="Tag: "></asp:Label> 
              <asp:Label ID="tagLabel" runat="server" Text=""></asp:Label> <br /> <br />
            </div>

            <div class="form-group">
              <asp:Label ID="Label8" runat="server" Text="Availability: "></asp:Label> 
              <asp:Label ID="availabilityLabel" runat="server" Text=""></asp:Label> <br />  <br />  
            </div>

            <div class="form-group">
              <asp:Label ID="Label9" runat="server" Text="Shop Name: "></asp:Label> 
              <asp:Label ID="shopNameLabel" runat="server" Text=""></asp:Label> 
            </div>
          </div>

          <hr>
          <asp:Image ID="Image1" runat="server" class="img-fluid mb-4" />
          <hr>

        </div>
      </div>
    </div>
  </div>
</div>
</asp:Content>
