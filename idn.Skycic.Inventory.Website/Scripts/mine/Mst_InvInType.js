function Mst_InvInType() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#InvInType', 'has-error-fix', 'Mã loại kho không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#InvInTypeName', 'has-error-fix', 'Tên loại kho không được trống!')) {
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

        var objMst_InvInType = new Object();
        objMst_InvInType.InvInType = commonUtils.returnValueText('#InvInType');
        objMst_InvInType.InvInTypeName = commonUtils.returnValueText('#InvInTypeName');

        if (commonUtils.checkElementExists('#FlagActive')) {
            var flagActive = '0';
            var inputFlagActive = $('#FlagActive');
            if (inputFlagActive !== undefined && inputFlagActive !== null) {
                if (inputFlagActive.prop('checked')) {
                    flagActive = '1';
                }
            }
            objMst_InvInType.FlagActive = flagActive;
        }

        if (commonUtils.checkElementExists('#FlagStatistic')) {
            var flagStatistic = '0';
            var inputFlagStatistic = $('#FlagStatistic');
            if (inputFlagStatistic !== undefined && inputFlagStatistic !== null) {
                if (inputFlagStatistic.prop('checked')) {
                    flagStatistic = '1';
                }
            }
            objMst_InvInType.FlagStatistic = flagStatistic;
        }
        var modelCur = JSON.stringify(objMst_InvInType);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };
    this.saveData = function () {
        debugger
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
                debugger
                doneFunction(objResult);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                debugger
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        }

    };
    this.deleteData = function (objMst_InvInType) {
        var InvInType = commonUtils.returnValue(objMst_InvInType.InvInType);
        if (!commonUtils.isNullOrEmpty(InvInType)) {
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
                            InvInType: InvInType,
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
            alert("Chưa chọn bản ghi cần xóa!");
            return;
        }
    };
    function doneFunction(objResult) {
        if (objResult.Success) {
            debugger
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                commonUtils.window_location_href(objResult.RedirectUrl);
            }
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123("Lỗi!", objResult.Detail);
            }
        }
    };
    function failFunction(jqXHR, textStatus, errorThrown) {
    };
    function alwaysFunction() {
    };
    return this;
}