<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="EMS_Default" %>
<!DOCTYPE html>
<html lang="en" dir="rtl">
<head runat="server">
<link href="../skin/Limitless/global_assets/css/icons/icomoon/styles.min.css" rel="stylesheet" type="text/css" />
<link href="../skin/Limitless/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="../skin/Limitless/assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css" />
<link href="../skin/Limitless/assets/css/components.min.css" rel="stylesheet" type="text/css" />
<link href="../skin/Limitless/assets/css/colors.min.css" rel="stylesheet" type="text/css" />
<script src="../skin/Limitless/global_assets/js/main/jquery.min.js"></script>
<title>Welcome</title>
<style>
    .breadcrumb-item{
        font-size: 17px;
    }
    .row{
        margin-left:0px;
    }
    img.card-img.img-fluid{
        max-height: 32em;
        max-width: 35em;
    }
    #info_Skip{
        margin: auto;
    }
    #Xm_Question{
        margin-bottom: 50px;
    }
</style>
</head>
<body dir="rtl">


<form runat="server" id="form1">
<header>
    <div class="navbar navbar-expand-md navbar-dark bg-indigo navbar-static">
        <div class="navbar-brand">
            <img src="../skin/logo/AK_white_DA.png" alt="" style="height: 2rem;" />
        </div>
        <div class="collapse navbar-collapse" >
            <ul class="navbar-nav ml-md-auto">
                <li class="nav-item dropdown">
                    <i class="icon-watch2 mr-3 icon-2x" id="timer"></i>    
                </li>
            </ul>
        </div>
    </div>
    <div class="breadcrumb-line breadcrumb-line-light header-elements-md-inline">
        <div class="d-flex offset-3">
            <div class="breadcrumb" runat="server" id="User_Bio">
            </div>
        </div>
    </div>  
</header>
<div class="info" runat="server" ID="info">
<div class="card col-md-10 offset-1">
<div class="card-body">
	<div class="text-center mb-3 py-2">
		<h4 class="font-weight-semibold mb-1">به امتحان نظری جواز راننده گی خوش امدید</h4>
		<span class=" d-block"> امتحان شامل ۹ سوال بوده و ۱۰ دقیقه وقت دارد  
		</span>
        <br/>
        <span>  برای ادامه روی ،ادامه، کلیک نموده و قت شما اغاز میگردد </span>
	</div>
	<div class="d-md-flex align-items-md-center flex-md-wrap text-center text-md-left">
       <asp:Button runat="server" ID="info_Skip" CssClass="btn btn-primary btn-lg legitRipple" Text="ادامه" OnClick="Info_Skip"   ></asp:Button>
	</div>
</div>
</div>
</div>
<div ID="xm" runat="server" Visible="False">
<div class="row">
    <div class="col-md-4 offset-1">
        <div class="card">
            <div class="card-body">
                <div class="card-img-actions mb-3">
                    <img class="card-img img-fluid" alt="" runat="server" ID="QImg" />
                </div>
            </div> 
        </div>
    </div>
    <div class="col-lg-6 col-md-6 ">
        <div class="card" runat="server" ID="QuestionBody">
            <div class="card-body" runat="server" id="Xm_Question">
                <blockquote class="blockquote blockquote-bordered py-2 pl-3 mb-0">
                    <p class="mb-0 font-weight-black" runat="server" ID="Question"></p>
                    <br/>
                    <div class="form-check">
                        <asp:CheckBox runat="server" ID="_1thBox" CssClass="form-check-input"  />
                    </div>
                    <br/>
                    <div class="form-check">
                        <asp:CheckBox runat="server" ID="_2thBox" CssClass="form-check-input"  />
                    </div>
                    <br/>
                    <div class="form-check">
                        <asp:CheckBox runat="server" ID="_3thBox" CssClass="form-check-input"  />
                    </div>
                    <br/>
                    <div class="form-check">
                        <asp:CheckBox runat="server" ID="_4thBox"  class="form-check-input"  />
                    </div>
                </blockquote>
            </div>
        </div>
    </div>
    <div id="CaptureResult">
        <input type="hidden" runat="server" id="HCounter"/>
        <input type="hidden" runat="server" id="HQID"/>
        <input type="hidden" runat="server" id="HCHID"/>
        <input type="hidden" runat="server" id="Qselect" Value="0"/>
        <asp:HiddenField runat="server" ID="time" />
    </div>
</div>
<div class="d-flex justify-content-center mt-3 mb-3">
 <ul class="pagination shadow-1" runat="server" ID="Q_counter">
     <li class="page-item">
         <a href="#" class="page-link page-link-white legitRipple">
             <i class="icon-arrow-left15"></i>
         </a>
     </li>
     <li class="page-item active" runat="server" ID="Q1"><a href="#" class="page-link page-link-white legitRipple">1</a></li>
     <li class="page-item" runat="server" ID="Q2"><a href="#" class="page-link page-link-white legitRipple">2</a></li>
     <li class="page-item" runat="server" ID="Q3"><a href="#" class="page-link page-link-white legitRipple">3</a></li>
     <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">4</a></li>
     <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">5</a></li>
     <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">6</a></li>
     <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">7</a></li>
     <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">8</a></li>
     <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">
             <i class="icon-circle2"></i>
         </a>

     </li>
 </ul>
</div>
<div class="d-flex justify-content-center mt-3 mb-3">
<ul class="list-inline mb-0">
<li class="list-inline-item">
     <asp:LinkButton runat="server" ID="Btn_Next" class="btn btn-primary ml-auto legitRipple" OnClick="Next" >
         بعدی 
         <i class="icon-backward2 mr-3 icon-2x"></i>
     </asp:LinkButton>
</li>
</ul>
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
</html>

<script type="text/javascript">
    //==========================Validation============================
    $('input:checkbox').click(function () {
        $('input:checkbox').not(this).prop('checked', false);
    });
    $('#Btn_Next').on({
        click: function (e) {
            if ($(':input[type="checkbox"]:checked').length <= 0) {
                e.preventDefault();
                alert(" گزینه ها خالی است ");

            }

        }
    });
    $('#noty_layout__topRight').fadeOut(3000);
    //=================================================================
    
    //=======================Clock===================================
    var minutesleft = 40;
    var secondsleft = 0;
    var finishedtext = "وقت شما تمام گردید";
    var end;
    if (localStorage.getItem("end2")) {
        end = new Date(localStorage.getItem("end2"));
    } else {
        end = new Date();
        end.setMinutes(end.getMinutes() + minutesleft);
        end.setSeconds(end.getSeconds() + secondsleft);
    }

    var counter = function() {
            var now = new Date();
            var diff = end - now;
            diff = new Date(diff);
            var sec = diff.getSeconds();
            var min = diff.getMinutes() - 6;
            if (min < 10) {
                min = "0" + min;
            }
            if (sec < 10) {
                sec = "0" + sec;
            }
            if (min <= 1) {
                document.getElementById('timer').style.backgroundColor = "red";
            }
            if (min <= 0) {
                clearTimeout(interval);
                localStorage.setItem("end2", null)
                document.getElementById('timer').innerHTML = finishedtext;
                localStorage.removeItem('end2');
                <%--document.getElementById('<%=time.ClientID%>').value = "0";--%>

                self.location = "http://192.168.2.161/Exam/Exam/Result.aspx";
            }
            else {
                var value = min + ":" + sec;
                localStorage.setItem("end2", end);
                document.getElementById('timer').innerHTML = value;
            }
        }
    
    var interval = setInterval(counter, 1000);

  
  
    //============================================================================


    //==============window control function====================
    //window.unload = function () {
    //    localStorage.removeItem('end2');
    //}
    //window.onbeforeunload = function () {
    //    localStorage.removeItem('end2');
    //}

    //function preBack() { window.history.forward(); }
    //setTimeout('preBack()', 0);
    //window.unload = function() { null; }
    //window.addEventListener('beforeunload', function (e) {
    //    alert('are you sure close');
    //    e.preventDefault();
    //    e.returnValue = '';
    //});
    //=============================================================

</script>
