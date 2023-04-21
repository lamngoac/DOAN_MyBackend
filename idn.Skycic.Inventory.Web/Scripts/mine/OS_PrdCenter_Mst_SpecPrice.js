function OS_PrdCenter_Mst_SpecPrice() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#SpecCode', 'has-error-fix', 'Mã tổ chức không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#UnitCode', 'has-error-fix', 'Mã đơn vị không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#BuyPrice', 'has-error-fix', 'Giá mua không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNullOrEmpty('#BuyPrice', 'has-error-fix', 'Giá mua không là số!')) {
                return false;
            }
            if (!CheckIsNumberLonHonBang0('#BuyPrice', 'has-error-fix', 'Giá mua phải >= 0!')) {
                return false;
            }
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#SellPrice', 'has-error-fix', 'Giá bán không được trống!')) {
            return false;
        } else {
            if (!CheckIsNumber('#SellPrice', 'has-error-fix', 'Giá bán không là số!')) {
                return false;
            }
            if (!CheckIsNumberLonHonBang0('#SellPrice', 'has-error-fix', 'Giá bán phải >= 0!')) {
                return false;
            }
        }
        var discountVND = ReturnValueText('#DiscountVND');
        if (!IsNullOrEmpty(discountVND)) {
            if (!CheckIsNumber('#DiscountVND', 'has-error-fix', 'Chiết khấu không là số!')) {
                return false;
            }
            if (!CheckIsNumberLonHonBang0('#DiscountVND', 'has-error-fix', 'Chiết khấu phải >= 0!')) {
                return false;
            }
            else {
                discountVND = null;
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
        //debugger;
        var objMst_SpecPrice = new Object();
        objMst_SpecPrice.SpecCode = commonUtils.returnValueText('#SpecCode');
        objMst_SpecPrice.UnitCode = commonUtils.returnValueText('#UnitCode');
        objMst_SpecPrice.BuyPrice = commonUtils.returnValueText('#BuyPrice');
        objMst_SpecPrice.SellPrice = commonUtils.returnValueText('#SellPrice');
        objMst_SpecPrice.CurrencyCode = commonUtils.returnValueText('#CurrencyCode');
        objMst_SpecPrice.DiscountVND = commonUtils.returnValueTextOrNull('#DiscountVND');
        objMst_SpecPrice.VATRateCode = commonUtils.returnValueTextOrNull('#VATRateCode');
        objMst_SpecPrice.OrgID = commonUtils.returnValueText('#OrgID');

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
                    objMst_SpecPrice.FlagActive = flagActive;
                }
                
            }
        }
        var modelCur = JSON.stringify(objMst_SpecPrice);
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
    this.deleteData = function (objOS_PrdCenter_Mst_SpecPrice) {
        var speccode = commonUtils.returnValue(objOS_PrdCenter_Mst_SpecPrice.Speccode);
        var unitcode = commonUtils.returnValue(objOS_PrdCenter_Mst_SpecPrice.Unitcode);

        if (!commonUtils.isNullOrEmpty(speccode) && !commonUtils.isNullOrEmpty(unitcode)) {
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
                            speccode: speccode,
                            unitcode: unitcode,
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