var __app = {};
$(document).ajaxStart(function () {
    if ($("#show-loading").length > 0) {
        $("#show-loading").remove();
    }

    if ($("#show-loading").length === 0) {
        //var div = $("<div id='show-loading' style='position:fixed; top:0; right:0; width:120px; height:30px; background:#e00; padding-left:20px;'/>");
        //div.html("<span style='color:#fff; font: 700 12px/30px arial;'>loading...</span>");
        var div = $("<div id='show-loading' style='position: fixed; z-index: 999999; top: 0; left: 0; height: 100%; width: 100%; background: rgba( 236, 236, 236, .5 ) url(/Content/assets/images/showloading/indicatorbig.gif) 50% 50% no-repeat'/>");
        div.appendTo('body');
    }
    $("#show-loading").fadeIn();
});

$(document).ajaxStop(function () {
    $("#show-loading").hide();
});

$(document).ajaxError(
    function (e, x, settings, exception) {
        $("#show-loading").hide();
        var message;
        var statusErrorMap = {
            '999': "Server understood the request but request content was invalid.",
            '401': "Unauthorised access.",
            '403': "Forbidden resouce can't be accessed",
            '500': "Internal Server Error.",
            '503': "Service Unavailable"
        };
        if (x.status) {
            message = statusErrorMap[x.status];
            if (!message) {
                message = "Unknow Error \n.";
            }
        } else if (exception == 'parsererror') {
            message = "Error.\nParsing JSON Request failed.";
        } else if (exception == 'timeout') {
            message = "Request Time out.";
        } else if (exception == 'abort') {
            message = "Request was aborted by the server";
        } else {
            message = "Unknow Error \n.";
        }
        if (x.status != 0)
            _showErrorMsg123("THÔNG BÁO", x.responseText);
    });

window.onerror = function (errorMeaage, fileName, lineNumber) {
    __globalerror = 1;
    var c = 'Script error: ' + errorMeaage + '\nFileName: ' + fileName + '\nLine no: ' + lineNumber;
    //alert(c);
    $("#show-loading").hide();
};


function _showErrorMsg123(msg, detail) {
    if ($("#errorcontainer123").length === 0) {
        $("<div id='errorcontainer123'/>").appendTo('body');
    }

    $("#errorcontainer123").dialog({
        autoOpen: false,
        //width: 600,
        //height: 300,
        width: 800,
        height: 550,
        modal: true,
        title: msg,
        buttons: {
            Close: function () {
                $(this).dialog("close");
            },

            Detail: function (e) {
                $(".error_detail_panel").slideToggle("medium", function () {

                });

                //{ $(e.currentTarget).hide(); }
            },


        },
        close: function () {
            $(this).dialog("close");

        }
    });

    
    //$("#errorcontainer123").empty();
    //$("#errorcontainer123").append(detail);
    //$("#errorcontainer123").dialog("open");
    //$("#errorcontainer123").parent().find("div.ui-dialog-titlebar").addClass("ui-dialog-titlebar-fix");
}