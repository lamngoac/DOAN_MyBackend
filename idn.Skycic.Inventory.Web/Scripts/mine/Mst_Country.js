function Mst_Country() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#CountryCode', 'has-error-fix', 'Mã quốc gia không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#CountryName', 'has-error-fix', 'Tên quốc gia không được trống!')) {
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
        var countrycode = commonUtils.returnValueText('#CountryCode');
        var countryname = commonUtils.returnValueText('#CountryName');
        var objMst_Country = new Object();
        objMst_Country.CountryCode = countrycode;
        objMst_Country.CountryName = countryname;

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
                    objMst_Country.FlagActive = flagActive;
                }
            }
        }
        var modelCur = commonUtils.returnJSONValue(objMst_Country);
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
    this.deleteData = function (objMst_Country) {
        var countrycode = commonUtils.returnValue(objMst_Country.CountryCode);
        var countryname = commonUtils.returnValue(objMst_Country.CountryName);
        if (!commonUtils.isNullOrEmpty(countrycode) && !commonUtils.isNullOrEmpty(countryname)) {
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
                            countrycode: countrycode,
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