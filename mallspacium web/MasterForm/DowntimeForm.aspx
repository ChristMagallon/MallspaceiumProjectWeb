<%@ Page Async="true" Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="DowntimeForm.aspx.cs" Inherits="mallspacium_web.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
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
                <asp:Label ID="Label4" runat="server" Text="Start Date: "></asp:Label> 
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                <br/> 

                <asp:Label ID="Label2" runat="server" Text="End Date: "></asp:Label> 
                <asp:Textbox ID="endDateTextbox" runat="server" Text=""></asp:Textbox> <br/> 

                <asp:Label ID="Label3" runat="server" Text="Message: "></asp:Label> 
                <asp:Textbox ID="messageTextbox" runat="server" Text="" Height="58px" Width="364px"></asp:Textbox> <br/> <br />
            
                <asp:Button ID="saveButton" runat="server" Text="SAVE" OnClick="saveButton_Click" />
            </div>
        </div>
   
</asp:Content>
