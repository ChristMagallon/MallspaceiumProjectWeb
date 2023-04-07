<%@ Page Async="true" Title=""  Language="C#"  MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="AboutUsPage.aspx.cs" Inherits="mallspacium_web.Shopper.AboutUsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

     <!-- starts here-->

    <center>
        <div class="container" >
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <h1 class="card-title">About Us</h1>
                        <hr>
                        <h5 class="card-title" style="text-align: justify" >
                                <asp:Label ID="Label1" runat="server" Text="Mallspaceium is an application that will help shoppers find and locate their desired stores from their current location. It also allows admins, including store owners, to have independent access to the application to have business promotions and notify users. The application would improve the shopping experience by making it quicker, faster, and more convenient for customers to locate stores and products of interest, and improve safety management by allowing consumers to evacuate a mall more swiftly in the event of an emergency or fire." ></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </center>
</asp:Content>  

