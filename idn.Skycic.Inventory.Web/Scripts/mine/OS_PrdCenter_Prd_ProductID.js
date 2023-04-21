function OS_PrdCenter_Prd_ProductID() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#ProductID', 'has-error-fix', 'Mã sản phẩm không được trống.')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#SpecCode', 'has-error-fix', 'Mã Spec không được trống.')) {
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
        debugger;
        var objPrd_ProductID = new Object();
        objPrd_ProductID.ProductID = commonUtils.returnValueText('#ProductID');
        objPrd_ProductID.SpecCode = commonUtils.returnValueText('#SpecCode');
        objPrd_ProductID.ProductionDate = commonUtils.returnValueText('#ProductionDate');
        objPrd_ProductID.LOTNo = commonUtils.returnValueText('#LOTNo');
        objPrd_ProductID.BuyDate = commonUtils.returnValueText('#BuyDate');
        objPrd_ProductID.SecretNo = commonUtils.returnValueTextOrNull('#SecretNo');
        objPrd_ProductID.WarrantyStartDate = commonUtils.returnValueTextOrNull('#WarrantyStartDate');
        objPrd_ProductID.WarrantyExpiredDate = commonUtils.returnValueTextOrNull('#WarrantyExpiredDate');
        objPrd_ProductID.WarrantyDuration = commonUtils.returnValueTextOrNull('#WarrantyDuration');

        objPrd_ProductID.RefNo1 = commonUtils.returnValueTextOrNull('#RefNo1');
        objPrd_ProductID.RefBiz1 = commonUtils.returnValueTextOrNull('#RefBiz1');
        objPrd_ProductID.RefNo2 = commonUtils.returnValueTextOrNull('#RefNo2');
        objPrd_ProductID.RefBiz2 = commonUtils.returnValueTextOrNull('#RefBiz2');
        objPrd_ProductID.RefNo3 = commonUtils.returnValueTextOrNull('#RefNo3');
        objPrd_ProductID.RefBiz3 = commonUtils.returnValueTextOrNull('#RefBiz3');
        objPrd_ProductID.Buyer = commonUtils.returnValueTextOrNull('#Buyer');

        objPrd_ProductID.ProductIDStatus = commonUtils.returnValueTextOrNull('#ProductIDStatus');
        objPrd_ProductID.CUSTOMFIELD1 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD1');
        objPrd_ProductID.CUSTOMFIELD2 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD2');
        objPrd_ProductID.CUSTOMFIELD3 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD3');
        objPrd_ProductID.CUSTOMFIELD4 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD4');
        objPrd_ProductID.CUSTOMFIELD5 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD5');
        objPrd_ProductID.OrgID = commonUtils.returnValueText('#OrgID');

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
                    objPrd_ProductID.FlagActive = flagActive;
                }

            }
        }
        var modelCur = JSON.stringify(objPrd_ProductID);
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
    this.deleteData = function (objOS_PrdCenter_Prd_ProductID) {
        var speccode = commonUtils.returnValue(objOS_PrdCenter_Prd_ProductID.Speccode);
        var unitcode = commonUtils.returnValue(objOS_PrdCenter_Prd_ProductID.Unitcode);

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