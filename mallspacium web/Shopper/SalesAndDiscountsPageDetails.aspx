<%@ Page Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="SalesAndDiscountsPageDetails.aspx.cs" Inherits="mallspacium_web.Shopper.SalesAndDiscountsPageDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <!-- starts here-->
    <div class="form">
        <asp:Label ID="label" runat="server" Text="Sales or Discount Details"></asp:Label>  <br/> <br />
        
        <asp:Label ID="Label4" runat="server" Text="Shop Name: "></asp:Label> 
        <asp:Label ID="shopNameLabel" runat="server" Text=""></asp:Label> <br/> <br /> 

        <asp:Image ID="Image1" runat="server" /> <br />

        <asp:Label ID="Label1" runat="server" Text="Description: "></asp:Label> 
        <asp:Label ID="descriptionLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label2" runat="server" Text="Start Date: "></asp:Label> 
        <asp:Label ID="startDateLabel" runat="server" Text=""></asp:Label> <br/> 

        <asp:Label ID="Label3" runat="server" Text="End Date: "></asp:Label> 
        <asp:Label ID="endDateLabel" runat="server" Text=""></asp:Label> <br/> <br /> 
   </div>
</asp:Content>
