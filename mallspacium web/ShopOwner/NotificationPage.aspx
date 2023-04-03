<%@Page Async="true"Title="" Language="C#" MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="NotificationPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
<link rel="stylesheet" href="style.css" />

    <div class="container-fluid">
    <div class="row">
        <div class="col-md-4">
            <div class="form">
                <div class="col p-5">       
                </div>
                <asp:GridView ID="NotificationGridView" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="NotificationGridView_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="NOTIFICATION" DataField="Notification" />
                        <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>
                                <asp:LinkButton ID="DetailsLinkButton" runat="server" Text="Details" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-md-8">
            <asp:Label ID="SelectedNotificationLabel" runat="server" />
        </div>
    </div>
</div>

   </asp:Content>


