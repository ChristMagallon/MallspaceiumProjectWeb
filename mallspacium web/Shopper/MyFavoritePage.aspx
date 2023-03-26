<%@ Page EnableViewStateMac ="false" EnableSessionState="True" EnableEventValidation ="false" ValidateRequest ="false" ViewStateEncryptionMode ="Never" Async="true" Title=""  Language="C#"  MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="MyFavoritePage.aspx.cs" Inherits="mallspacium_web.Shopper.MyFavoritePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- starts here-->
        <div class="form">
            <asp:GridView ID="myFavoriteGridView" class="table table-bordered table-condensed table-responsive table-hover bg-white" runat="server" DataKeyNames="shopName" AutoGenerateColumns="False" OnSelectedIndexChanged="myFavoriteGridView_SelectedIndexChanged" OnRowDataBound="myFavoriteGridView_RowDataBound" OnRowDeleting="myFavoriteGridView_RowDeleting" >
                <Columns>
                    <asp:BoundField HeaderText="Shop Name" DataField="shopName" SortExpression="shopName" ></asp:BoundField>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Description" DataField="shopDescription" SortExpression="shopDescription"></asp:BoundField>
                    <asp:CommandField DeleteText="Remove" ValidationGroup="RemoveButton" ShowDeleteButton="True"/>
                    
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

</asp:Content>