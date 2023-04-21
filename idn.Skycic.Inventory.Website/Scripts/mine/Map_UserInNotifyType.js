function Map_UserInNotifyType() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };

    this.doSearch = function () {
        var _ajaxSettings = this.ajaxSettings;
        doSearchData(_ajaxSettings);
    };

    function doSearchData(ajaxSettings) {
        var _ajaxSettings = ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var mst = commonUtils.returnValueText('#mst');
        var email = commonUtils.returnValueText('#email');
        
        
        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token
                , mst: mst
                , email: email
            },
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionLoadDoSearch(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
        }).always(function () {
        });
    };
    function doneFunctionLoadDoSearch(objResult) {
        if (objResult.Success) {
            $('#BindDataMap_UserInNotifyType').html('');
            $("#BindDataMap_UserInNotifyType").html(objResult.Html);
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };

    this.checkForm = function () {
        var listError = [];

        var rows = $('tbody#table-tbodyID tr.trdata').length;
        if (rows <= 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Lưới ma trận phân quyền thông báo trống!'
            };
            listError.push(objToastr);
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };

    this.getData = function (flagRedirect) {
        var Lst_Map_UserInNotifyType = []; // Lưới detail
        var rows = $('tbody#table-tbodyID tr.trdata').length;
        if (rows > 0) {
            $('tbody#table-tbodyID tr.trdata').each(function () {
                var $tr = $(this);
                var userCode = commonUtils.returnValue($tr.find('input.UserCode').val());
                var userName = commonUtils.returnValue($tr.find('input.UserName').val());
                var itdCount = $tr.find('td.FlagNotify').length;
                if (itdCount > 0) {
                    $tr.find('td.FlagNotify').each(function () {
                        var $td = $(this);
                        var notifyType = $td.attr('fieldid');
                        if (!commonUtils.isNullOrEmpty(notifyType)) {
                            var $inputCheckBox = $td.find('input.' + notifyType);
                            if ($inputCheckBox !== undefined && $inputCheckBox !== null) {
                                var flagNotify = '0';
                                if ($inputCheckBox.prop('checked')) {
                                    flagNotify = '1';
                                }

                                var objMap_UserInNotifyType = {};
                                objMap_UserInNotifyType.UserCode = userCode;
                                objMap_UserInNotifyType.NotifyType = notifyType;
                                objMap_UserInNotifyType.FlagNotify = flagNotify;

                                Lst_Map_UserInNotifyType.push(objMap_UserInNotifyType);
                            }
                        }
                    });
                }
            });
        }

        var modelCur = commonUtils.returnJSONValue(Lst_Map_UserInNotifyType);
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var data = {
            __RequestVerificationToken: token,
            model: modelCur
        };
        return data;
    };

    this.saveData = function () {
        debugger;
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData();

        if (this.checkForm()) {
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                doneFunction(objResult);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        }
    };

    function doneFunction(objResult) {
        debugger;
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                var listToastr = [];
                objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages[0] };
                listToastr.push(objToastr);
                commonUtils.showToastr(listToastr);
            }
            
        }
        else {
            //if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
            //    var listToastr = [];
            //    objToastr = { ToastrType: 'error', ToastrMessage: objResult.Messages[0] };
            //    listToastr.push(objToastr);
            //    commonUtils.showToastr(listToastr);
            //    return false;
            //}
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function failFunction(jqXHR, textStatus, errorThrown) {
    };
    function alwaysFunction() {
    };

    return this;
}