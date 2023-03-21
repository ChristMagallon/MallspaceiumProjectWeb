<%@ Page UnobtrusiveValidationMode="none" Async="true" Title=""  Language="C#"  MasterPageFile="Site2.Master"   AutoEventWireup="true" CodeBehind="AddSalesAndDiscountPage.aspx.cs" Inherits="mallspacium_web.ShopOwner.AddSalesAndDiscountPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- starts here-->
    <div class="form">

       <asp:Label ID="Label4" runat="server" Text="Add New Sale and Discount"></asp:Label>  <br/> <br /> 

        <asp:Label ID="Label1" runat="server" Text="Shop Name: "></asp:Label> 
        <asp:Textbox ID="shopNameTextbox" runat="server" Text=""></asp:Textbox>  <br/> 

        <asp:Label ID="Label6" runat="server" Text="Image: "></asp:Label> 
        <asp:FileUpload ID="imageFileUpload" runat="server" />
        <asp:RequiredFieldValidator ID="imageFileUploadValidator" runat="server"
            ControlToValidate="imageFileUpload"
            ErrorMessage="*Required" 
            style="color: red" /> <br/>

        <asp:Label ID="Label2" runat="server" Text="Description: "></asp:Label> 
        <asp:Textbox ID="descriptionTextbox" runat="server" Text=""></asp:Textbox> 
        <asp:RequiredFieldValidator ID="descriptionTextboxValidator" runat="server"
            ControlToValidate="descriptionTextbox"
            ErrorMessage="*Required"
            style="color: red" /> <br/> 

        <asp:Label ID="Label3" runat="server" Text="Start Date: "></asp:Label> 
        <asp:TextBox ID="startDateTextBox" runat="server" type="text" TextMode="Date" AutoCompleteType="Disabled" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="startDateTextboxValidator" runat="server"
            ControlToValidate="startDateTextbox"
            ErrorMessage="*Required"
            style="color: red" /> <br/> 

        <asp:Label ID="Label5" runat="server" Text="End Date: "></asp:Label> 
        <asp:TextBox ID="endDateTextBox" runat="server" type="text" TextMode="Date" AutoCompleteType="Disabled" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="endDateTextboxValidator" runat="server"
            ControlToValidate="endDateTextbox"
            ErrorMessage="*Required" 
            style="color: red"  /> <br/> <br>
        
        <asp:Button ID="addButton" runat="server" Text="Add" OnClick="addButton_Click" />
</div>

</asp:Content>
