<%@ Page Async="true" Title=""  Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="mallspacium_web.MasterForm3.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- starts here-->
    <div class="container">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card">
                <div class="card-body">
                    <center>
                        <asp:Image ID="Image1" runat="server" CssClass="img-fluid mb-3" /> <br />
                        <asp:Button ID="editProfileButton" runat="server" Text="Edit Profile" OnClick="editProfileButton_Click" CssClass="btn btn-primary btn-sm mb-3" />
                    </center>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Full Name:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="nameLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Birthday: :</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="birthdayLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Gender:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="genderLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Email: </strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="emailLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Phone Number: </strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="phoneNumberLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <strong>Address:</strong>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="addressLabel" runat="server" CssClass="mb-2"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

    <div class="table-responsive-xxl">
        <asp:GridView ID="recentlyVisitedGridView" CssClass="table table-bordered table-striped table-hover bg-white" runat="server" AutoGenerateColumns="False" DataKeyNames="shopName" OnRowDataBound="shopsGridView_RowDataBound" OnSelectedIndexChanged="shopsGridView_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="Shop Name" DataField="shopName" SortExpression="shopName" ></asp:BoundField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" CssClass="img-fluid" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Description" DataField="shopDescription" SortExpression="shopDescription"></asp:BoundField>
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