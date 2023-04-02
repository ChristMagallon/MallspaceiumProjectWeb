<%@ Page Async="true" Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ReportDetailsForm.aspx.cs" Inherits="mallspacium_web.MasterForm.ReportDetailsForm" %>

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
    <asp:Button ID="viewSupportingImageButton" runat="server" Text="View Supporting Image" CssClass="btn btn-primary" OnClick="viewSupportingImageButton_Click"/>
    <br /><br /><br />
</div>

<div class="modal fade" id="imageModal" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="imageModalLabel">Image Preview</h4>
            </div>
            <div class="modal-body">
                <img src="" id="modalImage" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    function showModal(imageString) {
        document.getElementById("modalImage").src = "data:image/jpeg;base64," + imageString;
        $('#imageModal').modal('show');
    }
</script>

    <div class="form-group">
        <asp:Label ID="Label5" runat="server" Text="Upload Proof: "></asp:Label>
        <asp:FileUpload ID="proofFileUpload" runat="server" /> <br /> <br />
    </div>
    <div class="form-group">
        <asp:Label ID="Label7" runat="server" Text="Add Note: "></asp:Label>
        <asp:TextBox ID="noteTextBox" runat="server" TextMode="MultiLine" Rows="4" AutoCompleteType="Disabled" Width="234px"></asp:TextBox> <br /> <br />
    </div>
    <div class="form-group">
        <button type="submit" class="btn btn-primary" id="addNoteButton" runat="server" >Add Note</button>
     </div>
</div>


</asp:Content>
