function Mst_Inventory() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#InvCode', 'has-error-fix', 'Mã kho không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#InvName', 'has-error-fix', 'Tên kho không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#InvType', 'has-error-fix', 'Loại kho không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#InvLevelType', 'has-error-fix', 'Cấp kho không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#FlagIn_Out', 'has-error-fix', 'Flag kho nhập/xuất không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#InvCodeParent', 'has-error-fix', 'Mã kho cha không được trống!')) {
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
        objMst_Inventory.InvCode = commonUtils.returnValueText('#InvCode');
        objMst_Inventory.InvCodeParent = commonUtils.returnValueText('#InvCodeParent');
        objMst_Inventory.InvName = commonUtils.returnValueText('#InvName');
        objMst_Inventory.InvAddress = commonUtils.returnValueText('#InvAddress');
        objMst_Inventory.InvLevelType = commonUtils.returnValueText('#InvLevelType');
        objMst_Inventory.InvType = commonUtils.returnValueText('#InvType');
        objMst_Inventory.InvContactName = commonUtils.returnValueText('#InvContactName');
        objMst_Inventory.InvContactEmail = commonUtils.returnValueText('#InvContactEmail');
        objMst_Inventory.InvContactPhone = commonUtils.returnValueText('#InvContactPhone');
        objMst_Inventory.FlagIn_Out = commonUtils.returnValueText('#FlagIn_Out');
        objMst_Inventory.FlagActive = commonUtils.returnValueText('#FlagActive');

        if (commonUtils.checkElementExists('#FlagActive')) {
            var flagActive = '0';
            var inputFlagActive = $('#FlagActive');
            if (inputFlagActive !== undefined && inputFlagActive !== null) {
                if (inputFlagActive.prop('checked')) {
                    flagActive = '1';
                }
            }
            objMst_Inventory.FlagActive = flagActive;
        }
        var modelCur = JSON.stringify(objMst_Inventory);
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
    this.deleteData = function (objMst_Inventory) {
        var invCode = commonUtils.returnValue(objMst_Inventory.InvCode);
        if (!commonUtils.isNullOrEmpty(invCode)) {
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
                            invCode: invCode,
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