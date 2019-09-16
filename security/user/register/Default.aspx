<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="security_user_register_Default" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Traffic Archive</title>
    

    <script src="http://localhost/apis/js/external/jquery/jquery-2.1.4.min.js"></script>

    <%--<script src="https://MushfiqMac/apiplus/js/external/jquery.treeview.js?ts=1429599434118"></script>--%>

    <script src="http://localhost/apis/js/external/calendar/calendars.js?ts=1464093604195"
            type="text/javascript"></script>
    <script src="http://localhost/apis/js/external/calendar/calendars.plus.js?ts=1464093604195"
            type="text/javascript"></script>
    <script src="http://localhost/apis/js/external/calendar/calendars.picker.js?ts=1464093604195" type="text/javascript"></script>
    <script src="http://localhost/apis/js/external/calendar/calendars.shamsi.js?ts=1464093604195"
            type="text/javascript"></script>
    <script src="http://localhost/apis/js/external/calendar/calendars.da-pa.js?ts=1464093604195"
            type="text/javascript"></script>

    <script src="http://localhost/apis/js/debug/plus-v1.0.0.001.js"></script>

    <%--<script src="https://MushfiqMac/apiplus/js/debug/api/plus.treeview-v1.0.0.001.js"></script>
    <script src="https://MushfiqMac/apiplus/js/debug/api/plus.treeselector-v1.0.0.001.js"></script>--%>

    <script src="http://localhost/apis/js/debug/api/plus.validator-v1.0.0.001.js"></script>
    
    <style type="text/css">        
    .rtl .div-row input[type="password"], .rtl .div-row input[type="number"] {
        float: right;
    }
    .div-row input[type="password"], .div-row input[type="number"] {
        border: 1px solid #aaa;
        float: left;
        height: 32px;
        margin: -8px 0;
        min-width: 187px;
        padding: 0 6px;
        width: auto;
    }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            plus.init();
            $('form').validator();

            $('#left-bar').unbind('mouseover').mouseover(function () {
                $(this).data('status', 'hover');
            })
            $('#left-bar').unbind('mouseout').mouseout(function () {
                $(this).data('status', 'out');
                setTimeout(function () { if ($('#left-bar').data('status') === 'out') $('#left-bar').animate({ 'left': '-226px' }, 250) }, 15000)
            });

            $('#ribbon').click(function () {
                var l = $('#left-bar').css('left') == '0px' ? '-226px' : '0';
                $('#left-bar').animate({ 'left': l }, 100);
            });

            // calendar
            //plus.config.setCalendar($('#uxCalendarType').val() - 0);
            
            //$('.date').hide();
            //$('.date[ctype=' + $('#uxCalendarType').val() + ']').show();
            plus.config.setCalendar(1)
            $('.date').calendarsPicker();


            //$('#uxCalendarType').change(function () {
            //    var i = $(this).val(), pi = !i;
            //    var v = $('#uxDateOfBirth' + (i ? '_Shamsi' : '')).val(); //$('.date[ctype=' + (pi - 0) + ']').val();
            //    //  alert(i);
            //    plus.config.setCalendar(i - 0);
            //    $('.date[ctype=' + (i - 0) + ']').val('');
            //    var calendar = i ? $.calendars.instance('shamsi', 'da') : $.calendars.instance('');
            //    if (pi) v = plus.date.readAsDate(v);
            //    else {
            //        var c = $.calendars.instance('shamsi', 'da');
            //        alert(c.parseDate("yyyy-mm-dd", v).toJSDate());
            //        //var j = c.parseDate("yyyy-mm-dd", v.substring(0, 10)).toJSDate();
            //        //v = j.getFullYear() + '-' + (('0' + (j.getMonth() + 1)).slice(-2)) + '-' + (('0' + (j.getDate())).slice(-2));
            //    }

            //    alert(plus.date.write(v));
            //    $('.date[ctype=' + (i-0) + ']').val( plus.date.write(v) );
            //    $('.date').hide();
            //    $('.date[ctype=' + (i-0) + ']').show();

            //    //var i = $(this).attr('ctype') - 0;
            //    //var v = $(this).val();
            //    //alert(v);
            //});
            $('#uxRegister').click(function () {
                plus.widget.loading(true, 'لطفاْ صبر کنید');
            });
        });
    </script>
</head>
<body class="rtl">
    <div class="div-header" id="header">
        <a href="https://MushfiqMac/lmis/cpanel/" class="home">
            <img src="http://localhost/skin/icon/home1.png" /></a>
        <img src="http://localhost/lmis/skin/icon/users.png" style="float: left; height: 33px; margin: 5px;" />
        <div class="log" id="user.detail"></div>
    </div>
    <div class="head-spacer"></div>
    <div class="div-footer"></div>
    <div id="left-bar">
        <label id="ribbon">
            ...
        </label>
        <ul id="left-ul"></ul>
    </div>

    <form id="form1" runat="server">
        <div id="sidebar" class="sidebar">
            <ul id="uxSidebarUL">
            </ul>
        </div>
        <div class="div-form  widget" runat="server" id="dvAsanWazifa_prf_Profile">
            <h2 class="title">فورم راجستر</h2>
            <div class="div-row">
                <label>اسم مکمل</label>
                <asp:TextBox id="uxFullName" runat="server" rule="{fn:'required'}" vgroup="s" CssClass="w400"></asp:TextBox>
            </div>
            <div class="div-row">
                <label>تخلص</label>
                <asp:TextBox id="uxLastName" runat="server" rule="{fn:'required'}" vgroup="s" CssClass="w400"></asp:TextBox>
            </div>
            <div class="div-row">
                <label>اسم پدر</label>
                <asp:TextBox id="uxFatherName" runat="server" rule="{fn:'required'}" vgroup="s" CssClass="w400"></asp:TextBox>
            </div>
            <div class="div-row">
                <label>تاریخ تولد</label>
                <asp:TextBox id="uxDateOfBirth_Shamsi" runat="server" rule="{fn:'required'}" vgroup="s" CssClass="w100 date" ctype="1"></asp:TextBox>
                <%--<asp:TextBox id="uxDateOfBirth" runat="server" rule="{fn:'required'}" vgroup="s" CssClass="w100 date" style="display:none"  ctype="0"></asp:TextBox>

                <asp:DropDownList ID="uxCalendarType" runat="server" style="margin-right:5px; " CssClass="w150" >
                    <asp:ListItem Text="هجری شمسی" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="میلادی" Value="0"></asp:ListItem>
                </asp:DropDownList>--%>
                
            </div>
            <%--<div class="div-row">
                <label>محل تولد</label>
                <asp:TextBox ID="uxPlaceOfBirth" runat="server" CssClass="w100 tree"></asp:TextBox>
            </div>--%>
            <div class="div-row">
                <label>جنسیت</label>
                <asp:DropDownList ID="uxGenderID" runat="server" rule="{fn:'required'}" vgroup="s" path="look.Gender"></asp:DropDownList>
            </div>
            <div class="div-row">
                <label>حالت مدنی</label>
                <asp:DropDownList ID="uxMaritalStatusID" runat="server" rule="{fn:'required'}" vgroup="s" path="look.MaritalStatus"></asp:DropDownList>
            </div>
            <div class="div-row">
                <label>ایمل</label>
                <asp:TextBox id="uxEmail" runat="server" rule="{fn:'required'}, {fn:'email'}" vgroup="s" CssClass="w400"></asp:TextBox>
            </div>
            <div class="div-row">
                <label>نمبر موبایل</label>
                <asp:TextBox id="uxMobile" runat="server" rule="{fn:'required'}, {fn:'phone'}" vgroup="s" CssClass="w100"></asp:TextBox>
                <label class="w400" style="font-weight:bold">* نمبر موبایل برای ورود به سیستم لازمی میباشد.</label>
            </div>
            <div class="div-separator"></div>
            <h2>معلومات کاربر</h2>
            <div class="div-row">
                <label>اسم کاربر</label>
                <asp:TextBox id="uxUserName" runat="server" rule="{fn:'required'}" vgroup="s" ></asp:TextBox>
            </div>
            <div class="div-row">
                <label>رمز عبور</label>
                <asp:TextBox id="uxPassword" TextMode="Password"  runat="server" rule="{fn:'required'}" vgroup="s" ></asp:TextBox>
            </div>
            <div class="div-row">
                <label>تکرار رمز عبور</label>
                <asp:TextBox id="uxPasswordRepeat" TextMode="Password"  runat="server" rule="{fn:'required'}, {fn:'eq', child0:'uxPassword'}" vgroup="s" ></asp:TextBox>
            </div>

            <div class="div-form-control">
                <asp:Button ID="uxRegister" OnClick="Register" Text="ثبت" vgroup="s" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
