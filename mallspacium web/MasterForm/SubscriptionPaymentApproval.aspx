﻿<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true"  Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="SubscriptionPaymentApproval.aspx.cs" Inherits="mallspacium_web.MasterForm.SubscriptionApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />

     <!-- start here -->
    <div class="container">
        <div class="row mb-3">
            <div class="col-md text-lg-end">
                <a href="SubscriptionPaymentApproval.aspx" class="btn btn-primary btn-lg">Subscription Payment Approval</a>
            </div>
        </div>
    </div>

    <div class="container"> 
        <div class="form">
            <div class="col p-5">
                <asp:TextBox ID="searchTextBox" runat="server" class="form-control" type="search" placeholder="Search Email" aria-label="Search" AutoPostBack="True" OnTextChanged="searchTextBox_TextChanged"></asp:TextBox>
            </div>
        </div>

    <div class="form">
        <asp:GridView ID="subscriptionGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="userEmail" OnSelectedIndexChanged="subscriptionGridView_SelectedIndexChanged" OnRowDataBound="subscriptionGridView_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Transaction ID" DataField="transactionID"></asp:BoundField>
            <asp:BoundField HeaderText="Email" DataField="userEmail"></asp:BoundField>
            <asp:BoundField HeaderText="Type" DataField="subscriptionType"></asp:BoundField>
            <asp:BoundField HeaderText="Price" DataField="price"></asp:BoundField>
            <asp:BoundField HeaderText="Role" DataField="userRole"></asp:BoundField>
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
</asp:Content>