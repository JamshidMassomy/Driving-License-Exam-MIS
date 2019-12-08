<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../skin/Limitless/global_assets/css/icons/icomoon/styles.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/Limitless/assets/css/colors.min.css" rel="stylesheet" type="text/css" />
    <script src="../../skin/Limitless/global_assets/js/main/jquery.min.js"></script>
    <title>Print</title>
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
    #End_btn{
        margin: auto;
    }
    .bg-blue{
        background-color: #3f51b5 !important;
    }
</style>
</head>

<body dir="rtl">
<form id="form1" runat="server">

<header>
    <div class="navbar navbar-expand-md navbar-dark bg-indigo navbar-static">
        <div class="navbar-brand">
            <img src="../../skin/logo/AK_white_DA.png" alt="" style="height: 2rem;" />
        </div>
        <div class="collapse navbar-collapse" >
            <ul class="navbar-nav ml-md-auto">
              
            </ul>
        </div>
    </div>
</header> 
<div class="info" runat="server" id="Result">
    <div class="card col-md-10 offset-1">
        <div class="card-body">
            <div class="text-center mb-3 py-2">
                <h4 class="font-weight-semibold mb-1"> نتیجه </h4>
            </div>
            <div class="table-responsive" runat="server" id="Result_Table">
            </div>
            <br/>
            <div class="col-md-4 offset-4" runat="server" id="Result_Info">
               <%-- <div class="card card-body border-top-1 border-top-pink">
                    <div class="text-center">
                        <h6 class="font-weight-bold mb-0">کامیاب</h6>
                        <p class="mb-3">90</p>

                    </div>
                </div>--%>
            </div>
            <div class="d-md-flex align-items-md-center flex-md-wrap text-center text-md-left">
                <asp:Button runat="server" ID="End_btn" CssClass="btn btn-primary btn-lg legitRipple" Text="ختم" OnClick="End" ></asp:Button>
            </div>
        </div>
    </div>
</div>
</form>
</body>
</html>
<script>
    //reset timer 
    $(function () {
        localStorage.removeItem('end2');
    })();


</script>