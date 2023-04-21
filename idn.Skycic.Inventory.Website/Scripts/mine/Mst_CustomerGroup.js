function Mst_CustomerGroup() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#manageFrm#CustomerGrpName', 'has-error-fix', 'Mã nhóm khách hàng không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#manageFrm#CustomerGrpDesc', 'has-error-fix', 'Tên nhóm khách hàng không được trống!')) {
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
        var customerSourceName = $('#manageFrm #CustomerGrpName').val();
        var customerSourceDesc = $('#manageFrm #CustomerGrpDesc').val();
        var customerSourceCode = $('#manageFrm #CustomerGrpCode').val();

        var objMst_CustomerGroup = new Object();
        objMst_CustomerGroup.CustomerGrpName = customerSourceName;
        objMst_CustomerGroup.CustomerGrpDesc = customerSourceDesc;
        objMst_CustomerGroup.CustomerGrpCode = customerSourceCode;

        if (!commonUtils.isNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {
                if (commonUtils.checkElementExists('#manageFrm #FlagActive')) {
                    var flagActive = '0';
                    var inputFlagActive = $('#manageFrm #FlagActive');
                    if (inputFlagActive !== undefined && inputFlagActive !== null) {
                        if (inputFlagActive.prop('checked')) {
                            flagActive = '1';
                        }
                    }
                    objMst_CustomerGroup.FlagActive = flagActive;
                }
            }
        }
        var modelCur = commonUtils.returnJSONValue(objMst_CustomerGroup);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };
    this.saveData = function (flagAction) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData(flagAction);
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
    this.deleteData = function (objMst_CustomerGroup) {
        if (!commonUtils.isNullOrEmpty(objMst_CustomerGroup.CustomerGrpCode)) {
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
                            customerGrpCode: objMst_CustomerGroup.CustomerGrpCode,
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
    this.getDetailData = function (customerSourceCode, flagEdit) {
        var _ajaxSettings = this.ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        $.ajax({
            url: _ajaxSettings.Url,
            data: {
                __RequestVerificationToken: token,
                customerSourceCode: customerSourceCode
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
            traditional: true,
            success: function (data) {
                if (data.Success) {
                    $("#ShowPopup").html(data.Html);
                    ShowPopup();
                } else {
                    _showErrorMsg123("Lỗi!", data.Detail);
                }

            }
        });
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