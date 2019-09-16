<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="security_login_default" %>
<!DOCTYPE html>
<html>
<head>
    <link href="../../skin/Limitless/global_assets/css/icons/icomoon/styles.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/components.min.css" rel="stylesheet" type="text/css" />
    <script src="../../skin/Limitless/global_assets/js/main/jquery.min.js"></script>
    <%--<link href="../../skin/Limitless/assets/css/colors.min.css" rel="stylesheet" type="text/css" />--%>
    <title>امتحان نظری </title>
    <style>
        .hide{
            display: none;
        }
        .navbar{
            background-color: #004889;
        }
        .btn-primary{
            background-color:#004889;
            font-size: 20px;
        }
        .transport-logo{
            width: 45em;
            float: left;
            margin-left:4em;
            margin-top: 2em;
        }
       .card{
           margin-top: 11em;
           border-radius: 2.187rem;
           max-height: 26em;
       }
       .xm-type li{
           margin: auto;
           width: 16em;
           border-bottom: 0px;
       }
       body{
           font-family:initial;
       }
       
    </style>
</head>
<body dir="rtl">
<form id="form2" runat="server">
<header>
    <div class="navbar navbar-expand-md navbar-dark bg-indigo navbar-static">
        <div class="navbar-brand">
            <a href="#" class="d-inline-block">
                <img src="../../skin/logo/AK_white_DA.png" alt="" style="height: 2rem;" />
            </a>
        </div>
        <div class="collapse navbar-collapse" id="navbar-mobile">
            <ul class="navbar-nav ml-md-auto">
                <li class="nav-item dropdown">
                    <asp:LinkButton runat="server"  OnClick="admin_click" class="navbar-nav-link"   >
                        <i class="icon-user-lock mr-3 icon-2x">Admin</i>
                    </asp:LinkButton>	            
                </li>
            </ul>
        </div>
    </div>
</header>  
<div class="row" style="margin-left: 0px;">
<div class="card-one card col-md-4 offset-1">
    <div class="card-body text-center">
        <h4 class="mt-2 mb-3">امتحان نظری جواز راننده گی </h4>
        <div class="list-group-item list-group-divider"></div>
        <ul class="xm-type pricing-table-list list-unstyled mb-3">
	       <li>
               <input type="button" name="xm_Btn" value="امتحان" id="xm_Btn" class="btn btn-primary btn-block " style="font-size: 20px;">
           </li>            
            <li>
                <input type="button" name="demo_Btn" id="demo_Btn" class="btn btn-primary btn-block" value="امتحان نمایشی"/>     
            </li>              
        </ul>
    </div>
</div>
<div class="hide card-tow card col-md-4 offset-1">
    <div class="card-body text-center">
        <h4 class="mt-2 mb-3">رمز ورود </h4>
        <ul class="xm-type pricing-table-list list-unstyled mb-3">
            <li>
                <asp:TextBox runat="server" ID="Code" class="form-control form-control-lg" TextMode="Password"></asp:TextBox>
      
            </li>
            <li>
                <asp:Button ID="Proceed_Btn" OnClick="Validate_OTP"  runat="server"  class="btn btn-primary btn-block " Text="ورود"  > 
                </asp:Button>
            </li>       
        </ul>
    </div>
</div>
<div class="col-md-6 offset-1">
    <img src="../../skin/logo/Transport2.png" class="transport-logo"/>
</div>
</div>
<div id="noty_layout__topRight" runat="server" class="noty_layout" Visible="False">
    <div id="noty_bar_85e63e18-6338-4801-afce-5c9671ff59cf" class="noty_bar noty_type__info noty_theme__limitless noty_close_with_click noty_has_timeout noty_has_progressbar">
        <div runat="server" id="noti_message" class="noty_body">
        </div>
    </div>
</div>
</form>
</body>
<script type="text/javascript">
    $('#noty_layout__topRight').fadeOut(3000);
    $('#xm_Btn').on('click', function ()
    {    
        $('.card-one.card').addClass('hide');
        $('.hide.card-tow').removeClass('hide');
        //$('.card-two.card').removeClass('.hide');
    });
</script>
</html>