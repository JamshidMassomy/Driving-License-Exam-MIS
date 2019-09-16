<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="security_user_credential_Default" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Traffic Archive:: تغییر رمز عبور</title>
    <link href="../../../skin/style/default.css" rel="stylesheet" />
    <link href="../../../skin/style/login.css" rel="stylesheet" />
    <style>
        
    
.div-header {
    background: #fff none repeat scroll 0 0;
    border-bottom: 7px solid #aaa;
    height: 35px;
    left: 0;
    padding: 10px 0;
    position: fixed;
    text-align: right;
    top: 0;
    width: 100%;
}

.div-header .home {
    border: 0 none;
    float: right;
    height: auto;
    margin: 10px 0;
}
.div-header a {
    margin: -20px 10px 0 0;
}
a {
    text-decoration: none;
}

.div-header .home img {
    border: 0 none;
    height: 30px;
    margin: -7px 20px;
}
    </style>
</head>
<body class="rtl">
    <form id="form1" runat="server">
        <div class="div-header" id="header">
            <a href="../../../cpanel/" class="home"> <img src="../../../skin/icon/home1.png" /></a>
        </div>
        <div class="div-logo" id="logo" runat="server" >
            <img src="../../../skin/logo/molsamd.png" style="width: 40%; opacity: 0.1; margin: 3% 40% 5% 10%" />
        </div>
        <div id="login" style="height:426px !important; max-height:450px !important">
            <h1 id="loginTitle" runat="server" >
                تغییر رمز عبور
            </h1>
            <p>
                <asp:TextBox ID="UserName" placeholder="اسم ورد" runat="server" Enabled="false" Style="background: #fff url('../../../skin/icon/luser.png') left center no-repeat;"></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="Password" placeholder="رمز" runat="server" TextMode="Password" Style="background: #fff url('../../../skin/icon/lpass.png') left center no-repeat;"></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="uxNewPassword" placeholder="رمز جدید" runat="server" TextMode="Password" Style="background: #fff url('../../../skin/icon/lpass.png') left center no-repeat;"></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="uxConfirmNewPassword" placeholder="تکرار رمز جدید" runat="server" TextMode="Password" Style="background: #fff url('../../../skin/icon/lpass.png') left center no-repeat;"></asp:TextBox>
            </p>
            <asp:ImageButton ImageUrl="~/skin/icon/arrowleft.png" ID="uxChangePassword" OnClick="uxChangePassword_Click" runat="server" CssClass="Loginbtn"/>
            <asp:Label ID="FailureText" runat="server" Visible="false" style="direction:rtl;" ></asp:Label>
        </div>
        <div class="div-footer" id="footer" runat="server" >
            <h3 style="font-size:1.1em; color:#222;">Afghanistan Government | Asan Khedmat  <br /><span style="font-size:0.8em;">
            </span></h3>
        </div>
    </form>
</body>
</html>