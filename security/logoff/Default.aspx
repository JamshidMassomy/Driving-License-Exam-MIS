<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="security_logoff_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Traffic Archive</title>
    <link rel="icon" type="image/png" href="http://192.168.1.2/skin/logo/favicon-32.png">
    <link href="../../skin/style/default.css" rel="stylesheet" />
    <script>
        setTimeout(function () {
            
            window.location.href = window.location.href.replace('logoff', 'login');
        }, 4000);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div-logo" id="logo" runat="server" >
		<img src="../../skin/logo/Traffic2.png" style="width: 44%; opacity: 0.5;margin:  1% 2% 3% 4%;">
    </div>

    <p style="color: #888;
    font-size: 24px;
    position: fixed;
    right: 200px;
    text-shadow: 2px 1px 0 rgba(255, 211, 211, 0.4);
    top: 40%;">
        <img src="../../skin/img/ajaxloader2.gif"/>
        <label style="font-size:22px; direction:rtl; text-align:right; ">
           شما در حال خروج شدن از سسیتم استید .....
        </label>
        
    </p>
    </form>
</body>
</html>
