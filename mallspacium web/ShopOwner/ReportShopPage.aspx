<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"  AutoEventWireup="true" CodeBehind="ReportShopPage.aspx.cs" Inherits="mallspacium_web.MasterForm.ReportShopPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
     <!-- starts here-->
     <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card mt-5">
                    <div class="card-body">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Reporting " CssClass="font-weight-bold"></asp:Label> 
                            <asp:Label ID="shopNameLabel" runat="server" Text="" CssClass="font-weight-bold"></asp:Label>
                        </div>

                        <div class="form-group">
                            <label for="reason" class="form-label">Reason:</label>
                            <asp:DropDownList ID="reasonDropDownList" runat="server" ValidationGroup="Validate">
                                <asp:ListItem Value="">--Select a Reason--</asp:ListItem>
                                <asp:ListItem Value="Prohibited Item">Prohibited Item</asp:ListItem>
                                <asp:ListItem Value="Scam">Scam</asp:ListItem>
                                <asp:ListItem Value="Posting pornographic, obscene and vulgar content">Posting pornographic, obscene and vulgar content</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="GenderRequiredFieldValidator" runat="server" ControlToValidate="reasonDropDownList" 
                                InitialValue="" 
                                ErrorMessage="Required*" 
                                ForeColor="Red"
                                ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        </div>


                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Detailed Reason " CssClass="font-weight-bold"></asp:Label>  <br />
                            <asp:TextBox ID="reasonTextbox" runat="server" placeholder="Please further elaborate on your selected reason" 
                                AutoCompleteType="Disabled" TextMode="MultiLine" Text="" Rows="5" CssClass="font-weight-bold" Width="399px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reasonRequiredFieldValidator" runat="server" 
                                ControlToValidate="reasonTextbox" 
                                ErrorMessage="Required*" 
                                CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label" runat="server" Text="Supporting image " CssClass="font-weight-bold"></asp:Label>
                            <div class="custom-file">
                            <asp:FileUpload ID="imageFileUpload" runat="server" Height="35px" Width="219px" CssClass="custom-file" />
                            <asp:RequiredFieldValidator ID="imageRequiredFieldValidator" runat="server" 
                                ControlToValidate="imageFileUpload" 
                                ErrorMessage="Required*" 
                                CssClass="text-danger"></asp:RequiredFieldValidator> <br /> <br />
                        </div>
                        </div>
                        <div class="form-group text-center">
                            <asp:Button ID="reportutton" runat="server" Text="REPORT" CssClass="btn btn-primary" OnClick="reportButton_Click" />
                        </div>
                </div>
            </div>
        </div>
     </div>
         </div>

</asp:Content> 
