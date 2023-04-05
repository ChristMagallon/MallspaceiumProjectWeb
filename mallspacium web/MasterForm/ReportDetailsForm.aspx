<%@ Page UnobtrusiveValidationMode="none" EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ReportDetailsForm.aspx.cs" Inherits="mallspacium_web.MasterForm.ReportDetailsForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="Style.css" />

    <!-- start here -->

    <div class="form">
        <h2>Report Details</h2>

        <div class="form-group">
            <asp:HiddenField ID="emailHiddenField" runat="server" />
            <asp:HiddenField ID="userRoleHiddenField" runat="server" />
            <asp:Label ID="Label" runat="server" Text="ID: "></asp:Label>
            <asp:Label ID="idLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Reported Shop Name: "></asp:Label>
            <asp:Label ID="shopNameLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Reason: "></asp:Label>
            <asp:Label ID="reasonLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label3" runat="server" Text="Detailed Reason: "></asp:Label>
            <asp:Label ID="detailedReasonLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label4" runat="server" Text="Reported By: "></asp:Label>
            <asp:Label ID="reportedByLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="Label6" runat="server" Text="Date: "></asp:Label>
            <asp:Label ID="dateLabel" runat="server" Text=""></asp:Label> 
        </div>

        <div class="form-group">
            <asp:Label ID="Label8" runat="server" Text="Status: "></asp:Label>
            <asp:Label ID="statusLabel" runat="server" Text=""></asp:Label> <br /> <br />
        </div>
    </div>

    <div class="form">
        <div class="form-group">
            <asp:HiddenField ID="imageHiddenField" runat="server" />
            <asp:Button ID="viewSupportingImageButton" runat="server" Text="View Supporting Image" CssClass="btn btn-primary" OnClick="viewSupportingImageButton_Click" /> <br /> <br /> <br />
         </div>
     </div>

     <div class="form">
        <asp:GridView ID="reportstatusGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" DataKeyNames="noteId" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="reportstatusGridView_SelectedIndexChanged" OnRowDataBound="reportstatusGridView_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="noteId" SortExpression="noteId"></asp:BoundField>
            <asp:BoundField HeaderText="Note" DataField="note" SortExpression="note"></asp:BoundField>
            <asp:BoundField HeaderText="date" DataField="date" SortExpression="date"></asp:BoundField>
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
        </asp:GridView> <br /> <br />
    </div>

    <div class="form">
        <div class="form-group">
            <asp:Label ID="Label9" runat="server" Text="Status: "></asp:Label>
            <asp:DropDownList ID="statusDropDownList" runat="server" ValidationGroup="Validate">
                <asp:ListItem Value="">--Select a Status--</asp:ListItem>
                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                </asp:DropDownList> 
            <asp:RequiredFieldValidator ID="statusRequiredFieldValidator" runat="server" ErrorMessage="Required*" 
                ControlToValidate="statusDropDownList" 
                CssClass="text-danger">
            </asp:RequiredFieldValidator> <br /> <br />
        </div>

        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Upload Proof: "></asp:Label>
            <asp:FileUpload ID="proofFileUpload" runat="server" /> 
            <asp:RequiredFieldValidator ID="proofRequiredFieldValidator" runat="server" ErrorMessage="Required*" 
                ControlToValidate="proofFileUpload" 
                CssClass="text-danger">
            </asp:RequiredFieldValidator> <br /> <br />
        </div>

        <div class="form-group">
            <asp:Label ID="Label7" runat="server" Text="Note: "></asp:Label>
            <asp:TextBox ID="noteTextBox" runat="server" TextMode="MultiLine" Rows="4" AutoCompleteType="Disabled" Width="234px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="noteRequiredFieldValidator" runat="server" ErrorMessage="Required*" 
                ControlToValidate="noteTextBox" 
                CssClass="text-danger">
            </asp:RequiredFieldValidator> <br /> <br />
        </div>

        <div class="form-group">
            <asp:Button ID="addProofNoteButton" runat="server" Text="Add Proof & Note"  CssClass="btn btn-primary" OnClick="addProofNoteButton_Click"/>
         </div>
    </div>
</asp:Content>
    


