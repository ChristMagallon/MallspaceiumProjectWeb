<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontPage.aspx.cs" Inherits="mallspacium_web.form.FrontPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
    <title></title>
</head>
<body runat="server">
     <form id="form1" runat="server">
        <div class="container-fluid pt-5 text-black text-center">
            <img src="../design/mallspaceium_logo.png" class="mx-auto d-block" width="150" height="150">
          <h1>Mallspaceium</h1>
          <p>Improving the Quality of Shopping Experience</p> 
        </div>
  
        <div class="container" style="width: 20%; height: 20%;">
    
         <form >
          <div class="mb-3 mt-3">
            <label for="email" class="form-label">Email:</label>
            <asp:TextBox ID="email" runat="server" type="email" class="form-control" placeholder="Enter email" name="email"></asp:TextBox>
          </div>
          <div class="mb-3">
            <label for="pwd" class="form-label">Password:</label>
            <asp:TextBox ID="pwd" runat="server" type="email" class="form-control" placeholder="Enter password" name="pswd"></asp:TextBox>
          </div>
          <div class="form-check mb-3">
            <label class="form-check-label">
             <asp:CheckBox ID="CheckBox1" runat="server"/>&nbsp;&nbsp;Remember me
            </label>
          </div>
          <asp:Button ID="LoginButton" runat="server" Text="LOGIN" ype="submit" class="btn btn-primary"/>
        </form>
        </div>
         </form>
</body>
</html>
