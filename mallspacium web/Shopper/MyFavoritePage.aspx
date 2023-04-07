<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title=""  Language="C#"  MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="MyFavoritePage.aspx.cs" Inherits="mallspacium_web.Shopper.MyFavoritePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- starts here-->
     <div class="row">
        <div class="col-md-10 offset-1">
            <div class="card p-3">
                <div class="table-responsive-xxl">
                    <asp:GridView ID="myFavoriteGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" DataKeyNames="shopName" AutoGenerateColumns="False" OnRowDataBound="myFavoriteGridView_RowDataBound" OnRowDeleting="myFavoriteGridView_RowDeleting" >
                        <Columns>
                            <asp:BoundField HeaderText="Shop Name" DataField="shopName" SortExpression="shopName" ></asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="shopDescription" SortExpression="shopDescription"></asp:BoundField>
                            <asp:BoundField HeaderText="Email" DataField="email" SortExpression="email"></asp:BoundField>
                            <asp:BoundField HeaderText="Phone Number" DataField="phoneNumber" SortExpression="phoneNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="Address" DataField="address" SortExpression="address"></asp:BoundField>
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