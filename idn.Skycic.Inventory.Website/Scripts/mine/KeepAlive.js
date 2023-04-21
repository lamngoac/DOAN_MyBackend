//calls the keep alive handler every 600,000 miliseconds, which is 10 minutes
var keepAlive = {
    refresh: function () {
        var objNotification = {};
        objNotification.NotifyID = '';
        objNotification.UserCode = '';
        var modelCur = commonUtils.returnJSONValue(objNotification);
        $.ajax({
            type: 'post',
            data: { model: modelCur },
            url: '/Handlers/KeepAlive.ashx',
            dataType: 'json'
            , beforeSend: function () {
                if ($("#show-loading").length > 0) {
                    $("#show-loading").remove();
                }
            }
        }).done(function (objResult) {
            var iCount = '';
            if (objResult !== null) {
                if (objResult.Success) {
                    var dateCur = '';
                    if (!commonUtils.isNullOrEmpty(objResult.DateTime) && commonUtils.checkDate(objResult.DateTime)) {
                        dateCur = commonUtils.returnValue(objResult.DateTime);
                        var daterejult = new Date(dateCur);
                        var month = daterejult.getMonth() + 1;
                        strdateCur = daterejult.getFullYear() + "-" + month + "-" + daterejult.getDate() + " " + daterejult.getHours() + ":" + daterejult.getMinutes() + ":" + daterejult.getSeconds();
                        dateCur = new Date(strdateCur);
                    }
                    var objInosNotificationResult = objResult.InosNotificationResult;
                    if (objInosNotificationResult !== undefined && objInosNotificationResult !== null) {

                        if (commonUtils.checkElementExists('.notification-cd')) {
                            iCount = objInosNotificationResult.UnreadCount;
                            $('.notification-cd').text(iCount);
                        }

                        var listInosNotification = objInosNotificationResult.List;
                        if (listInosNotification !== undefined && listInosNotification !== null && listInosNotification.length > 0) {
                            $('#pagenotificationhd').val(commonUtils.parseInt(objInosNotificationResult.PageCount));

                            var $ul = $('#notification-list');
                            if ($ul !== undefined && $ul !== null) {
                                $ul.empty();
                                var dUtcOffset = 0.0;
                                var utcOffset = commonUtils.returnValueText('#UtcOffset');
                                if (commonUtils.isNullOrEmpty(utcOffset)) {
                                    utcOffset = '0';
                                }
                                dUtcOffset = commonUtils.parseFloat(utcOffset);
                                for (var i = 0; i < listInosNotification.length; i++) {
                                    var objInosNotification = listInosNotification[i];
                                    objInosNotification.UtcOffset = dUtcOffset;
                                    objInosNotification.DateTime = dateCur;
                                    var $item = genNotifyItem(objInosNotification);
                                    $ul.append($item);
                                }

                                // Biến dùng kiểm tra nếu đang gửi ajax thì ko thực hiện gửi thêm
                                is_busy = false;
                                // Biến lưu trữ trang hiện tại
                                page = 0;
                                // Biến lưu trữ rạng thái phân trang
                                stopped = false;
                                $('[data-toggle="tooltip"]').tooltip();
                            }


                        }
                    }
                }
                else {
                    var errorDetail = commonUtils.returnValue(objResult.ErrorDetail);
                    if (!commonUtils.isNullOrEmpty(errorDetail)) {
                        _showErrorMsg123('Lỗi notify!', errorDetail);
                    }
                }

            }

            //doneFunction(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            //failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            //alwaysFunction();
        });
        //$.get('/keep-alive.ashx');
        setTimeout(keepAlive.refresh, 600000); // 10 minutes
    }
};
$(document).ready(keepAlive.refresh());

