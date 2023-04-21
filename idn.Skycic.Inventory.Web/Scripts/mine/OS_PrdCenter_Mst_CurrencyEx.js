function OS_PrdCenter_Mst_CurrencyEx() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#CurrencyCode', 'has-error-fix', 'Mã loại tiền không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#CurrencyName', 'has-error-fix', 'Tên loại tiền không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#BuyRate', 'has-error-fix', 'Tỉ giá mua không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNumber('#BuyRate', 'has-error-fix', 'Tỉ giá mua không là số!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNumber_GE_Zero('#BuyRate', 'has-error-fix', 'Tỉ giá mua phải >= 0!')) {
                return false;
            }
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#SellRate', 'has-error-fix', 'Tỉ giá bán không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNumber('#SellRate', 'has-error-fix', 'Tỉ giá bán không là số!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNumber_GE_Zero('#SellRate', 'has-error-fix', 'Tỉ giá bán phải >= 0!')) {
                return false;
            }
        }
        var interEx = commonUtils.returnValueText('#InterEx');
        if (!commonUtils.isNullOrEmpty(interEx)) {
            if (interEx.toString().trim().length > parseInt('400')) {
                alert('Nguồn lấy tỉ giá > 400 ký tự!');
                $('#InterEx').focus();
                AddClassCss('#InterEx', 'has-error-fix');
            } else {
                RemoveClassCss('#InterEx', 'has-error-fix');
            }
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
        var currencyCode = commonUtils.returnValueText('#CurrencyCode');
        var currencyName = commonUtils.returnValueText('#CurrencyName');
        var baseCurrencyCode = commonUtils.returnValueText('#BaseCurrencyCode');
        var buyRate = commonUtils.returnValueText('#BuyRate');
        var sellRate = commonUtils.returnValueText('#SellRate');
        var interEx = commonUtils.returnValueText('#InterEx');

        var objMst_CurrencyEx = new Object();
        objMst_CurrencyEx.CurrencyCode = currencyCode;
        objMst_CurrencyEx.CurrencyName = currencyName;
        objMst_CurrencyEx.BaseCurrencyCode = baseCurrencyCode;
        objMst_CurrencyEx.BuyRate = buyRate;
        objMst_CurrencyEx.SellRate = sellRate;
        objMst_CurrencyEx.InterEx = interEx;

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
                    objMst_CurrencyEx.FlagActive = flagActive;
                }
            }
        }
        var modelCur = JSON.stringify(objMst_CurrencyEx);
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
    this.deleteData = function (objOS_PrdCenter_Mst_CurrencyEx) {
        var currencycode = commonUtils.returnValue(objOS_PrdCenter_Mst_CurrencyEx.CurrencyCode);
        var currencyname = commonUtils.returnValue(objOS_PrdCenter_Mst_CurrencyEx.CurrencyName);

        if (!commonUtils.isNullOrEmpty(currencycode) && !commonUtils.isNullOrEmpty(currencyname)) {
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
                            currencycode: currencycode,
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