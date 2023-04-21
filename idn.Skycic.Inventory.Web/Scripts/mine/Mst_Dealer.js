function Mst_Dealer() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#DLCode', 'has-error-fix', 'Mã đơn vị không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#DLName', 'has-error-fix', 'Tên đơn vị không được trống!')) {
            return false;
        }
        //if (!commonUtils.checkElementIsNullOrEmpty('#DLCodeParent', 'has-error-fix', 'Mã đơn vị cha không được trống!')) {
        //    return false;
        //}
        if (!commonUtils.checkElementIsNullOrEmpty('#InvCode', 'has-error-fix', 'Kho của đơn vị không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#PMType', 'has-error-fix', 'Nhóm sản phẩm không được trống!')) {
            return false;
        }
        return true;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var dlcode = commonUtils.returnValueText('#DLCode');
        var dlname = commonUtils.returnValueText('#DLName');
        var dlcodeparent = commonUtils.returnValueText('#DLCodeParent');
        //var invcode = commonUtils.returnValueText('#InvCode');
        var pmtype = commonUtils.returnValueText('#PMType');
        var remark = commonUtils.returnValueText('#Remark');

        var objMst_Dealer = new Object();
        objMst_Dealer.DLCode = dlcode;
        objMst_Dealer.DLName = dlname;
        objMst_Dealer.DlCodeParent = dlcodeparent;
        //objMst_Dealer.InvCode = invcode;
        objMst_Dealer.PMType = pmtype;
        objMst_Dealer.Remark = remark;

        if (!commonUtils.isNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {
                if (commonUtils.checkElementExists('#FlagActive')) {
                    var flagActive = '0';
                    var inputFlagActive = $('#FlagActive');
                    if (inputFlagActive !== undefined && inputFlagActive !== null) {
                        if (inputFlagActive.prop('checked')) {
                            flagActive = '1';
                        }
                    }
                    objMst_Dealer.FlagActive = flagActive;
                }
            }
        }
        var modelCur = commonUtils.returnJSONValue(objMst_Dealer);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };
    this.saveData = function () {
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
    this.deleteData = function (objMst_Dealer) {
        var dlcode = commonUtils.returnValue(objMst_Dealer.DLCode);
        var dlname = commonUtils.returnValue(objMst_Dealer.DLName);
        if (!commonUtils.isNullOrEmpty(dlcode) && !commonUtils.isNullOrEmpty(dlname)) {
            var _ajaxSettings = this.ajaxSettings;
            bootbox.confirm({
                title: this.Image_Message,
                message: this.Confirm_Message,
                buttons: {
                    'cancel': {
                        label: 'Thoát',
                        className: 'btn mybtn-Button btnButton'
                    },
                    'confirm': {
                        label: 'Đồng ý',
                        className: 'btn mybtn-Button btnButton'
                    }
                },
                callback: function (result) {
                    if (result) {
                        var token = $('input[name=__RequestVerificationToken]').val();
                        var dataInput = {
                            dlcode: dlcode,
                        };
                        if (!commonUtils.isNullOrEmpty(token)) {
                            dataInput.__RequestVerificationToken = token;
                        }
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
                }
            });
        } else {
            commonUtils.showAlert('Chưa chọn bản ghi cần xóa!');
            return;
        }
    };
    function doneFunction(objResult) {
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                commonUtils.window_location_href(objResult.RedirectUrl);
            }
        }
        else {
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