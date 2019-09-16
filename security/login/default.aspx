<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="security_login_default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login </title>
    <link href="../../skin/Limitless/global_assets/css/icons/icomoon/styles.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/components.min.css" rel="stylesheet" type="text/css" />
    <style>
        .navbar
        {
            background-color: #004889;
        }
        .transport-logo{
        width: 45em;
        float: left;
        margin-left:4em;
        margin-top: 2em;
        }
        .card{
           margin-top: 7em;
           border-radius: 2.187rem;
           max-height: 30em;
       }
       .xm-type li{
           margin: auto;
           width: 16em;
           border-bottom: 0px;
       }
    </style>
</head>
<body dir="rtl">
<form id="form1" runat="server">
<header>
<div class="navbar navbar-expand-md navbar-dark bg-indigo navbar-static">
    <div class="navbar-brand">
        <a href="#" class="d-inline-block">
            <img src="../../skin/logo/AK_white_DA.png" alt="" style="height: 2rem;" />
        </a>
    </div>
</div>
</header>
<div class="row" style="margin-left: 0px;">
    <div class="card-one card col-md-4 offset-1">
        <div class="card-body text-center">
            <h4 class="mt-2 mb-3">Admin</h4>
            <div class="list-group-item list-group-divider"></div>
            <ul class="xm-type pricing-table-list list-unstyled mb-3">
	           <li>
                   
                   <asp:TextBox ID="UserName" placeholder="اسم ورد" class="form-control form-control-lg" runat="server" Style="background: #fff url('../../skin/icon/luser.png') left center no-repeat;"  ></asp:TextBox>
               </li>            
                <li>
                    <asp:TextBox ID="Password" class="form-control" placeholder="رمز" runat="server" TextMode="Password" Style="background: #fff url('../../skin/icon/lpass.png') left center no-repeat;"></asp:TextBox>
                </li>
                <li>
                    <asp:TextBox ID="SecurityCode" class="form-control" placeholder=" نمبر کارت هویت " runat="server" TextMode="Password" Style="background: #fff url('../../skin/icon/lpass.png') left center no-repeat;"></asp:TextBox>
                </li>
                <li>
                    <asp:Button ID="ImageButton1" OnClick="uxLogin_Click"  runat="server"  class="btn btn-primary btn-block " Text="Login" >
                    </asp:Button>
                    <asp:Label ID="FailureText" runat="server" Visible="false"  ></asp:Label>
                </li>
            </ul>
        </div>
    </div>
    
    <div class="col-md-6 offset-1">
        <img src="../../skin/logo/Transport2.png" class="transport-logo"/>
    </div>
</div>
</form>
</body>
</html>