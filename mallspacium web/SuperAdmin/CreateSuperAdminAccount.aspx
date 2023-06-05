<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#" MasterPageFile="Site4.Master"  AutoEventWireup="true" CodeBehind="CreateSuperAdminAccount.aspx.cs" Inherits="mallspacium_web.SuperAdmin.CreateSuperAdminAccount" %>

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

    <!-- starts here-->

    <div class="container">
        <h3>Create New Account</h3>  <br>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="usernameTextbox" class="form-label">Username:</label>
                    <asp:TextBox ID="usernameTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="usernameRequiredFieldValidator" runat="server" ControlToValidate="usernameTextbox" 
                        Display="Dynamic" 
                        ErrorMessage="Required*" 
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <asp:Label ID="errorUsernameLabel" runat="server" ForeColor="Red"></asp:Label> <br />
                </div>

                <div class="form-group">
                    <label for="emailTextbox" class="form-label">Email:</label>
                    <asp:TextBox ID="emailTextbox" runat="server" CssClass="form-control" Text="" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="emailRequiredFieldValidator" runat="server" ControlToValidate="emailTextbox" 
                        Display="Dynamic" 
                        ErrorMessage="Required*" 
                        ForeColor="Red" >
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="emailRegularExpressionValidator" runat="server" ControlToValidate="emailTextbox" 
                        Display="Dynamic" 
                        ErrorMessage="Invalid email address!" 
                        ForeColor="Red" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                    </asp:RegularExpressionValidator>
                    <asp:Label ID="errorEmailLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="passwordTextbox" class="form-label">Password:</label>
                <asp:TextBox ID="passwordTextbox" runat="server" CssClass="form-control" TextMode="Password" Text="" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" runat="server" ControlToValidate="passwordTextbox" 
                    ErrorMessage="Required*" 
                    ForeColor="Red" >
                </asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="confirmPasswordTextbox" class="form-label">Confirm Password:</label>
                <asp:TextBox ID="confirmPasswordTextbox" runat="server" CssClass="form-control" TextMode="Password" Text="" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="confirmPasswordRequiredFieldValidator" runat="server" ControlToValidate="confirmPasswordTextbox" 
                    ErrorMessage="Required*" 
                    ForeColor="Red" >
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="passwordCompareValidator" runat="server" ControlToCompare="passwordTextBox"
                    ControlToValidate="confirmPasswordTextBox" 
                    Display="Dynamic" 
                    ErrorMessage="Password does not match!" 
                    ForeColor="Red" >
                </asp:CompareValidator> <br/> <br />
            </div>
        </div>
    </div>
    
    <div class="form-group">
        <asp:Button ID="addButton" runat="server" Text="Add Account" OnClick="addButton_Click" CssClass="btn btn-primary"></asp:Button>
    </div>

</div>
</asp:Content>
