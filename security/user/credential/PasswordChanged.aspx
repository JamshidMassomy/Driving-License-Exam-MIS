<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordChanged.aspx.cs" Inherits="security_user_credential_PasswordChanged" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AsanWazifa.com :: تغییر رمز عبور</title>
    <link href="../../../skin/style/default.css" rel="stylesheet" />
    <link href="../../../skin/style/login.css" rel="stylesheet" />
    <script>
        setTimeout(function () {
            window.location.hash = "";
            window.location.href = window.location.protocol + window.location.pathname.replace("user/credential/PasswordChanged.aspx", "login");
        }, 4000);

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div-logo" id="logo" runat="server" >
            <img src="../../../skin/logo/molsamd.png" style="width: 40%; opacity: 0.1; margin: 3% 40% 5% 10%" />
        </div>
        <div style="color: #888;font-size: 24px;position: fixed;right: 200px;text-shadow: 2px 1px 0 rgba(255, 211, 211, 0.4);top: 30%;">
        <p style="direction:rtl;">
            <label style="font-size:22px">رمز عبور موفقانه تبدیل گردید.</label>
        </p>
        <p style="direction:rtl;">
            <label>
                 برای امنیت بهتر سیستم شما از سیستم در چند لحظه خارج میشوید.
            </label>
            <img src="../../../skin/img/ajaxloader2.gif" />
        </p>
    </div>
        <div class="div-footer" id="footer" runat="server" >
            <h3 style="font-size:1.1em; color:#222;">Afghanistan Government | Asan Khedmat  <br /><span style="font-size:0.8em;">
                The Asan Wazifa is developed by API + framework <br />
                API + version 1.8.2.1
            </span></h3>
        </div>
    </form>
</body>
</html>
