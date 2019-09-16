<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="cpanel_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Traffic CPanel</title>
    <link href="../skin/style/cpanelonly.css" rel="stylesheet" />
    <script src="../../apis/js/external/jquery/jquery-2.1.4.min.js"></script>
    <link rel="icon" type="image/png" href="http://192.168.1.4/skin/logo/favicon-32.png"/>

    <script type="text/javascript">
        if (!Array.indexOf) {
            Array.indexOf = function (a, item, i) {
                i || (i = 0);
                var length = a.length;
                if (i >= 0)
                    for (; i < length; i++)
                        if (a[i] === item) return i;
                return -1;
            }
        }

        var modal = {};
        $(document).ready(function () {
            $('.menu').click(function (e) {
                e.stopPropagation();
                e.preventDefault();
                ShowPanel();
                AddClickOutside();
            });
            modal = $('#myModal');
            $('.closeModal').click(function () {
                modal.hide();
            });
            
        });
        function AddClickOutside() {
            $(document).one('click', function (event) {
                if (!$('.side-panel').find(event.target).length) {
                    ClosePanel();
                }
                else {
                    AddClickOutside();
                }
            });
        }
        function ShowPanel() {
            $('.side-panel').animate({ right: '3px' }, 'slow');
        }
        function ClosePanel() {
            $('.side-panel').animate({ right: '-300px' }, 'fast');
        }

        function showNotification(el) {
            var id = $(el).attr('data-id');
            sendRequest({
                url:   "http://192.168.1.4/json/pull.asmx/Fetch"
                , type:'post'
                , async: false
                , data: JSON.stringify({
                    queries: [{ commandType: 'sp', path: 'Tconfsys.dbo.spGetNotification', param: { ID: id } }]
                })
                , success: function (msg) {
                    
                    var response = JSON.parse(msg.d);
                    showDialog(response[0].rows[0]);
                }
            });
        }

        function showDialog(row) {
            $('#uxTitle').text(row[4]);
            $('#uxBody').html('<p>'+row[5]+'</p>');
            modal.show();            
        }



        function sendRequest(opt) {
            var url = opt.url ||  opt.service;
            if (!opt.url) $.extend(opt, { url: url });
            var complete1 = opt.complete, success1 = opt.success;
            opt.contentType = opt.contentType === undefined ? 'application/json; charset=utf-8' : opt.contentType;
            opt.dataType = opt.dataType === undefined ? 'json' : opt.dataType; // we may require to get list


            return $.ajax($.extend(opt, {
                timeout: 300000,
                complete: function (xhr, status) {
                    if (status === 'timeout')
                        return;
                    if (xhr.status === 400 || xhr.status === 404)
                        ui.error("Invalid request: " + opt.url);
                    if (complete1)
                        complete1(xhr);
                },
                success: function (msg, status) {
                    
                        if (opt.contentType == 'application/json; charset=utf-8') {
                            
                        }
                        if (success1)
                            success1(msg, status, opt);
                    
                },
                error: function (xhr, status, e) {
                    
                }
            }))

        }

        

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.hide();
            }
        } 

        
        //#sourceURL=http://192.168.1.4/app.js
    </script>
</head>
<body class="rtl">
    <form id="form1" runat="server" >
        <div class="div-header " id="header" runat="server">
            <a href="javascript:;" class="menuNotification" >
                <img src="../skin/icon/Menu.png" class="menu" />
            </a>
            <a href="javascript:;" class="menuNotification">
                <img src="../skin/icon/notification1.png" />
                <span id="notificationCount" class="notification" runat="server"></span>
            </a>
        </div>
        <div class="side-panel">
            <div class="menu-header">
                <img src="../skin/icon/close.png" class="close" onclick="ClosePanel();" />
                <span>Asan Khedmat  </span>
            </div>
            <div class="menu-user" id="MenuUserDiv" runat="server" >

            </div>
            <div class="icons-list">
                <a href="javascript:;" class="icons">
                    <img src="../skin/icon/notification1.png" onclick="$('#notificationList').append('پیام جدید وجود ندارد')" />
                    <span id="notification" class="notification" runat="server"></span>
                </a>
                <a href="../security/user/credential" class="icons">
                    <img src="../skin/icon/changepassword2.png" />
                </a>
                <a href="../security/logoff/" class="icons">
                    <img src="../skin/icon/turnoff2.png" />
                </a>
            </div>
            <div class="div-notification-list" id="notificationList" runat="server" ></div>
        </div>
        <div class="div-cpanel" id="cpanel" runat="server"></div>

        <div class="div-footer"></div>
    </form>

    <!-- The Modal -->
    <div id="myModal" class="modal">

      <!-- Modal content -->
      <div class="modal-content">
        <div class="modal-header">
        <span class="closeModal">&times;</span>
        <h2 id="uxTitle" ></h2>
        </div>
        <div class="modal-body" id="uxBody">
        
        </div>
    </div>

    </div>
</body>
</html>
