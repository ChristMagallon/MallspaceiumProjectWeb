﻿<%@ Page Async="true" Title="" Language="C#" MasterPageFile="./Site1.Master" AutoEventWireup="true" CodeBehind="DowntimeForm.aspx.cs" Inherits="mallspacium_web.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
      
    <!-- starts here-->
    <div class="row">
         <div class="container">
             <center>  
                    <asp:HyperLink ID="downtime" runat="server" NavigateUrl="~/MasterForm/DowntimeForm.aspx" Text="Downtime"></asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="adminAccount" runat="server" NavigateUrl="~/MasterForm/AdminAccountForm.aspx" Text="Admin Account"></asp:HyperLink>
             </center>
         </div>
    </div>

        <div class="row">
            <div class="container">
            </div>
        </div>

        <div class="row">
            <div class="container">
            
            </div>
        </div>
   
</asp:Content>
