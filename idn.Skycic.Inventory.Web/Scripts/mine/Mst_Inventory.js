function Mst_Inventory() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!CheckIsNullOrEmpty('#InvCode', 'has-error-fix', 'Mã kho không được trống!')) {
            return false;
        }
        if (!CheckIsNullOrEmpty('#InvName', 'has-error-fix', 'Tên kho không được trống!')) {
            return false;
        }
        if (!CheckIsNullOrEmpty('#InvLevelType', 'has-error-fix', 'Chưa chọn cấp kho!')) {
            return false;
        }
        if (!CheckIsNullOrEmpty('#InvType', 'has-error-fix', 'Chưa chọn loại kho!')) {
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

        var objMst_Inventory = new Object();
        objMst_Inventory.InvCode = ReturnValueText('#InvCode');
        objMst_Inventory.InvName = ReturnValueText('#InvName');
        objMst_Inventory.InvLevelType = ReturnValueText('#InvLevelType');
        objMst_Inventory.InvType = ReturnValueText('#InvType');
        objMst_Inventory.InvCodeParent = ReturnValueText('#InvCodeParent');
        objMst_Inventory.InvAddress = ReturnValueText('#InvAddress');
        objMst_Inventory.InvContactName = ReturnValueText('#InvContactName');
        objMst_Inventory.InvContactEmail = ReturnValueText('#InvContactEmail');
        objMst_Inventory.InvContactPhone = ReturnValueText('#InvContactPhone');
        objMst_Inventory.Remark = ReturnValueText('#Remark');

        if (!IsNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {
                if (CheckElementExists('#FlagActive')) {
                    var flagActive = '0';
                    var inputFlagActive = $('#FlagActive');
                    if (inputFlagActive !== undefined && inputFlagActive !== null) {
                        if (inputFlagActive.prop('checked')) {
                            flagActive = '1';
                        }
                    }
                    objMst_Inventory.FlagActive = flagActive;
                }

            }
        }
        var modelCur = JSON.stringify(objMst_Inventory);
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
    this.deleteData = function (obj_Mst_Inventory) {
        var invcode = ReturnValue(obj_Mst_Inventory.Invcode);
        var invname = ReturnValue(obj_Mst_Inventory.Invname);

        if (!IsNullOrEmpty(invcode) && !IsNullOrEmpty(invname)) {
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
                            invcode: invcode,
                        };
                        if (!IsNullOrEmpty(token)) {
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