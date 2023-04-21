function OS_PrdCenter_Mst_SpecUnit() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#OrgID', 'has-error-fix', 'Mã tổ chức không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#SpecCode', 'has-error-fix', 'Mã hàng hóa không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#UnitCode', 'has-error-fix', 'Mã đơn vị không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#StandardUnitCode', 'has-error-fix', 'Mã đơn vị chuẩn không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#Qty', 'has-error-fix', 'Số lượng không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNumber('#Qty', 'has-error-fix', 'Số lượng không là số!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNumber_GE_Zero('#Qty', 'has-error-fix', 'Số lượng phải >= 0!')) {
                return false;
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
        
        var speccode = commonUtils.returnValueTextOrNull('#SpecCode');
        var unitcode = commonUtils.returnValueTextOrNull('#UnitCode');
        var standardunitcode = commonUtils.returnValueTextOrNull('#StandardUnitCode');
        var specunitdesc = commonUtils.returnValueTextOrNull('#SpecUnitDesc');
        var qty = commonUtils.returnValueTextOrNull('#Qty');
        var length = commonUtils.returnValueTextOrNull('#Length');
        var width = commonUtils.returnValueTextOrNull('#Width');
        var height = commonUtils.returnValueTextOrNull('#Height');
        var volume = commonUtils.returnValueTextOrNull('#Volume');
        var weight = commonUtils.returnValueTextOrNull('#Weight');
        var org = commonUtils.returnValueTextOrNull('#OrgID');

        var objMst_SpecUnit = new Object();
        objMst_SpecUnit.SpecCode = speccode;
        objMst_SpecUnit.UnitCode = unitcode;
        objMst_SpecUnit.StandardUnitCode = standardunitcode;
        objMst_SpecUnit.SpecUnitDesc = specunitdesc;
        objMst_SpecUnit.Qty = qty;
        objMst_SpecUnit.Length = length;
        objMst_SpecUnit.Width = width;
        objMst_SpecUnit.Height = height;
        objMst_SpecUnit.Volume = volume;
        objMst_SpecUnit.Weight = weight;
        objMst_SpecUnit.OrgID = org;

        if (!IsNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {
                if (commonUtils.checkElementExists('#FlagActive')) {
                    var flagActive = '0';
                    var inputFlagActive = $('#FlagActive');
                    if (inputFlagActive !== undefined && inputFlagActive !== null) {
                        if (inputFlagActive.prop('checked')) {
                            flagActive = '1';
                        }
                    }
                    objMst_SpecUnit.FlagActive = flagActive;
                }
            }
        }
        var modelCur = commonUtils.returnJSONValue(objMst_SpecUnit);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
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
    this.deleteData = function (objOS_PrdCenter_Mst_SpecUnit) {
        var speccode = commonUtils.returnValue(objOS_PrdCenter_Mst_SpecUnit.SpecCode);
        var unitcode = commonUtils.returnValue(objOS_PrdCenter_Mst_SpecUnit.UnitCode);

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