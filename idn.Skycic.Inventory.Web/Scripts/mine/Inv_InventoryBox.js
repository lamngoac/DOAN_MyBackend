function Inv_InventoryBox() {
    this.ActionType = '';
    debugger;
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#orgid', 'has-error-fix', 'Chưa chọn tổ chức!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#qty', 'has-error-fix', 'Chưa chọn số lượng!')) {
            return false;
        }
        return true;
    };
    this.variablesInit = function (objVariablesInit) {
        if (objVariablesInit !== undefined && objVariablesInit !== null) {
            var id_Modal = '';
            var id_FormMain = '';
            var id_FormImportExcel = '';
            var id_FileInput = '';
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_Modal)) {
                id_Modal = commonUtils.returnValue(objVariablesInit.Id_Modal);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FormMain)) {
                id_FormMain = commonUtils.returnValue(objVariablesInit.Id_FormMain);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FormImportExcel)) {
                id_FormImportExcel = commonUtils.returnValue(objVariablesInit.Id_FormImportExcel);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FileInput)) {
                id_FileInput = commonUtils.returnValue(objVariablesInit.Id_FileInput);
            }
        }
        this.Id_Modal = id_Modal;
        this.Id_FormMain = id_FormMain;
        this.Id_FormImportExcel = id_FormImportExcel;
        this.Id_FileInput = id_FileInput;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.ajaxSettingsInit = function () {
        var _ajaxSettings = {
            Type: '',
            DataType: '',
            Url: '',
        };
        this.ajaxSettings = _ajaxSettings;
    };
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var orgid = commonUtils.returnValueText('#OrgId');
        var qty = commonUtils.returnValueText('#Qty');
        var boxno = commonUtils.returnValueText('#Boxno');
        var qr_hop = commonUtils.returnValueText('#qr_hop');

        var objInv_InventoryBox = new Object();
        objInv_InventoryBox.OrgId = orgid;
        objInv_InventoryBox.Qty = qty;
        objInv_InventoryBox.Boxno = boxno;
        objInv_InventoryBox.qr_hop = qr_hop;
        var modelCur = commonUtils.returnJSONValue(objInv_InventoryBox);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };


    this.saveData = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData();
        if (dataInput === false) {
            return false;
        } else {

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
        }


    };
    // Start Show Popup Import Excel
    this.getDataShowPopupImportExcel = function () {
        var data = {};
        var token = '';
        if (commonUtils.checkElementExists('#' + this.Id_FormMain)) {
            token = $('#' + this.Id_FormMain + ' input[name=__RequestVerificationToken]').val();
        } else {
            var token = $('input[name=__RequestVerificationToken]').val();
        }
        if (!commonUtils.isNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }
        return data;
    };
    this.ShowPopupCreate = function (Orgid) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = {
            Orgid: Orgid,
        };
        var objVariablesInit = {
            Id_Modal: this.Id_Modal,
        };
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionShowPopupImportExcel(objResult, objVariablesInit);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionShowPopupImportExcel(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionShowPopupImportExcel();
        });
    };
    function doneFunctionShowPopupImportExcel(objResult, objVariablesInit) {
        if (objResult.Success) {
            $('#' + objVariablesInit.Id_Modal).modal({
                backdrop: false,
                keyboard: true,
            });
            $('#' + objVariablesInit.Id_Modal).html(objResult.Html);
        } else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi', objResult.Detail);
            }
        }
    };
    function failFunctionShowPopupImportExcel(jqXHR, textStatus, errorThrown) {

    };
    function alwaysFunctionShowPopupImportExcel() {

    };
    // End Show Popup Import Excel
    this.deleteData = function (objInv_InventoryBox) {
        var ifinvinfgno = commonUtils.returnValue(objInv_InventoryBox.IF_InvInFGNo);

        if (!commonUtils.isNullOrEmpty(ifinvinfgno)) {
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
                            ifinvinfgno: ifinvinfgno,
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