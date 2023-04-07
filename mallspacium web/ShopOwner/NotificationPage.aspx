<%@Page Async="true"Title="" Language="C#" MasterPageFile="Site2.Master" AutoEventWireup="true" CodeBehind="NotificationPage.aspx.cs" Inherits="mallspacium_web.MasterForm2.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
    <link rel="stylesheet" href="style.css" />

    <!-- starts here-->

    <div class="row">
        <div class="col-md-6 offset-md-3">
            <center>
                <asp:Label ID="notifDetailsLabel" runat="server" Text="Notification Details: " Visible="false"/>
                <div class="col-md-8">
                    <asp:Label ID="SelectedNotificationLabel" runat="server" />
                </div> <br /> <br />
            </center>

            <div class="card p-3">
                <div class="table-responsive mt-3">
                    <asp:GridView ID="NotificationGridView" CssClass="table table-bordered table-striped table-hover bg-white" runat="server" DataKeyNames="Notification" AutoGenerateColumns="False" OnSelectedIndexChanged="NotificationGridView_SelectedIndexChanged" OnRowDeleting="NotificationGridView_RowDeleting">
                    <Columns>
                        <asp:BoundField HeaderText="NOTIFICATION" DataField="Notification" />
                        
                        <asp:CommandField SelectText="Details" ShowSelectButton="True" ValidationGroup="SelectButton" ButtonType="Button" ControlStyle-BackColor="#0066ff" ItemStyle-CssClass="text-center" />
                            <asp:CommandField ShowDeleteButton="True" ValidationGroup="DeleteButton" ButtonType="Button" ControlStyle-BackColor="#cc0000" ItemStyle-CssClass="text-center" DeleteText="Remove"> 
                                <ControlStyle BackColor="#CC0000"></ControlStyle>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                        </asp:CommandField>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>


