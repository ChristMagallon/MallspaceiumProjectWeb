﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="mallspacium_web.form.AdminHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
    <title></title>
    <style>
        .geeks {
            height: auto;
        }
    </style>
</head>
  
<body class="d-flex flex-column">
    <form runat="server"> 
            <div class="pt-5  text-black text-center m-2  border border-secondary">
  <h1>  <img src="../design/mallspaceium_logo.png" class="mx-auto d-block" width="50" height="50"> Mallspaceium</h1>
        <div class="dropdown pb-4">
                    <a href="#" class="d-flex align-items-center text-white text-decoration-none dropdown-toggle m-2" id="dropdownUser1" data-bs-toggle="dropdown" aria-expanded="false">
                        <img src="https://images.pexels.com/photos/771742/pexels-photo-771742.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" alt="hugenerd" width="50" height="50" class="rounded-circle">
                        <span class="d-none d-sm-inline mx-1">loser</span>
                    </a>
                    <ul class="dropdown-menu dropdown-menu-light text-small shadow">
                        <li><a class="dropdown-item" href="#">New project...</a></li>
                        <li><a class="dropdown-item" href="#">Settings</a></li>
                        <li><a class="dropdown-item" href="#">Profile</a></li>
                        <li>
                            <hr class="dropdown-divider">
                        </li>
                        <li><a class="dropdown-item" href="#">Sign out</a></li>
                    </ul>
                </div>
</div>
   <div class="container-fluid">
    <div class="row flex-nowrap  ">
        <div class="col-auto col-md-3 col-xl-2 px-sm-2 px-0 border border-secondary m-2 vh-100">
            <div class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2">
                 <ul class="navbar-nav">
      <li class="nav-item">
        <a class="nav-link" href="#">Manage Users</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="#">Manage Subscription</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="#">Admin Activities</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="#">Reports</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="#">System</a>
      </li>
    </ul>
                <hr> 
            </div>
        </div>
        <div class="col py-3 border border-secondary m-2 ">   
            <h3>Display Screen</h3>
        </div>
    </div>
</div>
</form>
</body>
  
</html>
