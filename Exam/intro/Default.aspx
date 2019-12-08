<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<!DOCTYPE html>
<html lang="en" dir="rtl">
<head runat="server">
<link href="../../skin/Limitless/global_assets/css/icons/icomoon/styles.min.css" rel="stylesheet" type="text/css" />
<link href="../../skin/Limitless/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="../../skin/Limitless/assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css" />
<link href="../../skin/Limitless/assets/css/components.min.css" rel="stylesheet" type="text/css" />
<link href="../../skin/Limitless/assets/css/colors.min.css" rel="stylesheet" type="text/css" />
<script src="../../skin/Limitless/global_assets/js/main/jquery.min.js"></script>
<title> امتجان نظری  </title>
<style>
    #info_Skip{
        margin: auto;
    }
    body{
        background-color:#004889;
    }
    .xm_info{
        margin-top:12rem;
    }
</style>
</head>
<body>
<form id="form1" runat="server">
    <div class="xm_info">
        <div class="card col-md-10 offset-1">
        <div class="card-body">
	        <div class="text-center mb-3 py-2">
                <h2 class="font-weight-semibold mb-1">به امتحان نظری جواز راننده گی خوش امدید</h2>
                <span class="d-block"> امتحان شامل ۱۰ سوال بوده و ۱۰ دقیقه میباشد بعد از کلیک ادامه شما به صفحه امتحان رهنمایی میگردد   </span>
                <br/>
            <span>  برای ادامه روی ،ادامه، کلیک نموده و قت شما اغاز میگردد </span>
            </div>
                <div class="d-md-flex align-items-md-center flex-md-wrap text-center text-md-left">
                <asp:Button runat="server" ID="info_Skip" CssClass="btn btn-primary btn-lg legitRipple" Text="ادامه" OnClick="StartSession"   ></asp:Button>
                </div>
            </div>
            </div>
    </div>
</form>
</body>
</html>
