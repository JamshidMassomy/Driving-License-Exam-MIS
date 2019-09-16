<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Demo_Default" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <title>امتحان آزمایشی</title>
    <link href="../skin/Limitless/global_assets/css/icons/icomoon/styles.min.css" rel="stylesheet" type="text/css" />
    <link href="../skin/Limitless/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../skin/Limitless/assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css" />
    <link href="../skin/Limitless/assets/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="../skin/Limitless/assets/css/colors.min.css" rel="stylesheet" type="text/css" />
    <style>
        .row{
            margin-left: 0px;
            margin-right: 0px;
        }
        .bg-indigo{
          background-color:#2196f3;
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
</header>
<div class="row">
<div class="col-md-2">
    <div class="card">
        <div class="card-header header-elements-inline pb-0">
            <h6 class="card-title">کاربر</h6>
        </div>
        <div class="list-group-item list-group-divider"></div>
        <div class="list-group list-group-flush">
            <a href="#" class="list-group-item list-group-item-action legitRipple">
                <i class="icon-user mr-3 icon-2x"></i>
                جمشید 
            </a>
            <a href="#" class="list-group-item list-group-item-action legitRipple">
                <i class="icon-credit-card2 mr-3 icon-2x"></i>
                172616
            </a>
            <a href="#" class="list-group-item list-group-item-action legitRipple">
                <i class="icon-users mr-3 icon-2x "></i>
                Massomy
                
            </a>
            <a href="#" class="list-group-item list-group-item-action legitRipple">
                <i class="icon-barcode2 mr-3 icon-2x"></i>
                24923AD
            </a>
            <a href="#" class="list-group-item list-group-item-action legitRipple">
                <i class="icon-watch mr-3 icon-2x"></i>
                <span class="badge bg-danger badge-pill ml-auto">10:00 </span>
            </a>
        </div>
    </div>
</div>

<div class="col-lg-6 col-md-6 offset-1">
    <div class="card">
        <div class="card-body">
            <div class="card-img-actions mb-3" style="max-width: 200px; min-width: 200px;">
                <img class="card-img img-fluid" src="../Exam/XMphoto/637029353437901036.jpg" alt="" />
            </div>
            <blockquote class="blockquote blockquote-bordered py-2 pl-3 mb-0">
                <h6 class="mb-3">
                    <i class="icon-arrow-left13 mr-2"></i>
                    سوال اول
                </h6>
                <p class="mb-2 font-size-base">کدام علامه ترافیکی است؟</p>
                <div class="form-check">
                    <label class="form-check-label">
                        <input type="checkbox" class="form-check-input" />
                        Option 1
                    </label>
                </div>
                <div class="form-check">
                    <label class="form-check-label">
                        <input type="checkbox" class="form-check-input" />
                        Option 2
                    </label>
                </div>
                <div class="form-check">
                    <label class="form-check-label">
                        <input type="checkbox" class="form-check-input" />
                        Option 3
                    </label>
                </div>
                <div class="form-check">
                    <label class="form-check-label">
                        <input type="checkbox" class="form-check-input" />
                        Option 4
                    </label>
                </div>
            </blockquote>
        </div>
    </div>
</div>

</div>
<footer>
 <div class="d-flex justify-content-center mt-3 mb-3">
     <ul class="pagination shadow-1">
         <li class="page-item"><a href="#" class="page-link page-link-white legitRipple"><i class="icon-arrow-small-right"></i></a></li>
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
                 <i class="icon-arrow-small-left"></i>
             </a>

         </li>
     </ul>
 </div>
 <div class="d-flex justify-content-center mt-3 mb-3">
 <ul class="list-inline mb-0">
     <li class="list-inline-item">
         <button type="submit" class="btn btn-primary ml-auto legitRipple">بعدی 
             <i class="icon-forward3 mr-3 icon-2x"></i></button>
     </li>
     <li class="list-inline-item">
         <button type="submit" class="btn btn-primary ml-auto legitRipple">تایید 
             <i class="icon-checkmark4 mr-3 icon-2x"></i></button>
     </li>
 </ul>
 </div>
</footer> 
</form>
</body>
</html>
