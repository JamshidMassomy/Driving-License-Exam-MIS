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
    </style>
</head>
<body dir="rtl">
<form runat="server" id="form1">
<header>
<div class="navbar navbar-expand-md navbar-dark bg-indigo navbar-static">
    <div class="navbar-brand">
    <a href="index.html" class="d-inline-block">
	    <img src="../skin/logo/AK_white_DA.png" alt="" style="height: 2rem;" />
    </a>
    </div>
    <div class="collapse navbar-collapse" id="navbar-mobile">
        <ul class="navbar-nav ml-md-auto">
	        <li class="nav-item dropdown">
		        <a href="#" class="navbar-nav-link " data-toggle="dropdown">
			        <i class="icon-watch2 mr-3 icon-2x">00:00</i>
		        </a>	            
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
        <div class="card">
            <div class="card-body" runat="server" id="Xm_Question">
                <blockquote class="blockquote blockquote-bordered py-2 pl-3 mb-0">
                    <p class="mb-2 font-size-lg" runat="server" ID="Question"></p>
                    <div class="form-check">
                         <asp:CheckBox runat="server" ID="_1thBox" CssClass="form-check-input" Text="1rd Option"/>
                    </div>
                    <br/>
                    <div class="form-check">
                        <asp:CheckBox runat="server" ID="_2thBox" CssClass="form-check-input" Text="2rd Option"/>
                    </div>
                    <br/>
                    <div class="form-check">
                         <asp:CheckBox runat="server" ID="_3thBox" CssClass="form-check-input" Text="3rd Option"/>
                    </div>
                    <br/>
                    <div class="form-check">
                       <asp:CheckBox runat="server" ID="_4thBox" Text="Something" class="form-check-input"/>
                    </div>
                </blockquote>
            </div>
        </div>
    </div>
    <div id="CaptureResult">
        <input type="hidden" runat="server" id="HQID"/>
        <input type="hidden" runat="server" id="HCHID"/>
    </div>
</div>
<footer>
 <div class="d-flex justify-content-center mt-3 mb-3">
     <ul class="pagination shadow-1">
         <li class="page-item">
             <a href="#" class="page-link page-link-white legitRipple">
                 <i class="icon-arrow-left15"></i>
             </a>
         </li>
         <li class="page-item active"><a href="#" class="page-link page-link-white legitRipple">1</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">2</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">3</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">4</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">5</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">6</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">7</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">8</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">9</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">10</a></li>
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple">
                 <i class="icon-circle2"></i>
             </a>

         </li>
     </ul>
 </div>
 <div class="d-flex justify-content-center mt-3 mb-3">
 <ul class="list-inline mb-0">
     <li class="list-inline-item">
         <asp:LinkButton runat="server" ID="Btn_Next" class="btn btn-primary ml-auto legitRipple" OnClick="Next">
             بعدی 
             <i class="icon-backward2 mr-3 icon-2x"></i>
         </asp:LinkButton>
     </li>
     <li class="list-inline-item">
         <asp:LinkButton runat="server" ID="Btn_Confirm" class="btn btn-primary ml-auto legitRipple" OnClick="Save">
             تایید 
             <i class="icon-checkmark4 mr-3 icon-2x"></i>
         </asp:LinkButton>
     </li>
 </ul>
 </div>
</footer>
   
</form>
</body>
</html>

<script type="text/javascript">
    //$('#Btn_Next').on({
    //    click: function (e) {
    //        e.preventDefault();
    //        //for (var i = 10; i <= 10; i++) {
    //        //    $('.page-item').removeClass('active');
    //        //    $(this).addClass('active');
    //        //}
    //        $('.page-item').each(function() {
    //            $(this).addClass('active');
    //        });
    //        //$('.page-item').addClass('active');
    //        $(this).addClass('active');
           
    //    }
    //});
    //var _ul = document.getElementsByName("pagination shadow-1");

    //$('#Btn_Next').on('click', function () {
    //    $(this).addClass('active').siblings().removeClass('active');
    //});
</script>
