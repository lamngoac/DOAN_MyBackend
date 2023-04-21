function OS_PrdCenter_PrdPrdIDCustomField() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var objPrdPrdIDCustomField = [];
        var idx = 0;
        $('div.divcusfield').each(function () {
            idx = $(this).attr('idx');
            var flagActive = commonUtils.returnValueOrNull($(this).find('input[type="checkbox"][name="ListPrd_PrdIDCustomField[' + idx + '].FlagActive"]').val());
            var prdCustomFieldCode = commonUtils.returnValueOrNull($(this).find('input[type="hidden"][name="ListPrd_PrdIDCustomField[' + idx + '].PrdCustomFieldCode"]').val());
            var prdCustomFieldName = commonUtils.returnValueOrNull($(this).find('input[type="text"][name="ListPrd_PrdIDCustomField[' + idx + '].PrdCustomFieldName"]').val());
            var objSpecCustomField = {};

            objSpecCustomField.FlagActive = flagActive;
            objSpecCustomField.PrdCustomFieldCode = prdCustomFieldCode;
            objSpecCustomField.PrdCustomFieldName = prdCustomFieldName;

            objPrdPrdIDCustomField.push(objSpecCustomField);
        });
        
        var modelCur = commonUtils.returnJSONValue(objPrdPrdIDCustomField);
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
    this.deleteData = function (objOS_PrdCenter_PrdPrdIDCustomField) {
        var unitcode = commonUtils.returnValue(objOS_PrdCenter_PrdPrdIDCustomField.UnitCode);
        var unitname = commonUtils.returnValue(objOS_PrdCenter_PrdPrdIDCustomField.UnitName);
        if (!commonUtils.isNullOrEmpty(unitcode) && !commonUtils.isNullOrEmpty(unitname)) {
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
    this.setValueCheckBox = function (thiz) {
        var checkbox = false;
        if ($(thiz).is(":checked")) {
            checkbox = true;
            $(thiz).val('1');
        } else {
            $(thiz).val('0');
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