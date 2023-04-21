function InvF_InventoryInFG() {
    this.ActionType = '';
    //debugger;
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#InvCode', 'has-error-fix', 'Chưa chọn mã kho!')) {
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
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var rows = $("tbody#table-tbodyID tr.trdata").length;
        debugger;
        if (rows > 0) {
            var trArr = $('tbody#table-tbodyID tr.trdata').has('input[name="ckb"]:checked');
            if (trArr !== null && trArr.length > 0) {
                var tem = new Object();
                var iF_InvInFGNo = $('#IF_InvInFGNo').val();
                //var createDTimeUTC = $('#CreateDTimeUTC').val();
                var remark = $('#Remark').val();
                var invcode = $('#InvCode').val();
                var mst = $("#MST").val();
                var invintype = $("#InvInType").val();
                tem.IF_InvInFGNo = iF_InvInFGNo;
                //tem.CreateDTimeUTC = createDTimeUTC;
                tem.Remark = remark;
                tem.InvCode = invcode;
                tem.MST = mst;
                tem.InvInType = invintype;
                var InvF_InventoryInFGDtls = [];
                for (var i = 0; i < trArr.length; i++) {
                    var trCur = trArr[i];
                    if (trCur !== null && trCur !== undefined) {
                        var idx = $(trCur).attr('idx');
                        var temDtlCur = {};
                        temDtlCur.PartCode = $(trCur).find('input[type="hidden"][name="ListInvF_InventoryInFGDtl[' + idx + '].PartCode"]').val();
                        temDtlCur.Qty = $(trCur).find('input[type="text"][name="ListInvF_InventoryInFGDtl[' + idx + '].Qty"]').val();
                        temDtlCur.ProductionDate = $(trCur).find('input[type="hidden"][name="ListInvF_InventoryInFGDtl[' + idx + '].ProductionDate"]').val();
                        InvF_InventoryInFGDtls.push(temDtlCur);
                    }
                }
                tem.Lst_InvF_InventoryInFGDtl = InvF_InventoryInFGDtls;

                //var token = $('#manageForm input[name=__RequestVerificationToken]').val();
                var token = $('input[name=__RequestVerificationToken]').val();
                var modelCur = JSON.stringify(tem);
                var data = {
                    __RequestVerificationToken: token,
                    model: modelCur,
                };
                return data;
            }
        }
        else {
            alert("Chưa chọn sản phẩm cho phiếu nhập kho!");
            return false;
        }
        alert("Danh sách sản phẩm trống!");
        return false;
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
    this.deleteData = function (objInvF_InventoryInFG) {
        var ifinvinfgno = commonUtils.returnValue(objInvF_InventoryInFG.IF_InvInFGNo);

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
    this.showPopupSerial = function (ifinvinfgno, partcode) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = {
            ifinvinfgno: ifinvinfgno,
            partcode: partcode,
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
            doneFunctionShowPopupSerial(objResult, objVariablesInit);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionShowPopupSerial(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionShowPopupSerial();
        });
    };
    function doneFunctionShowPopupSerial(objResult, objVariablesInit) {
        debugger;
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
    function failFunctionShowPopupSerial(jqXHR, textStatus, errorThrown) {

    };
    function alwaysFunctionShowPopupSerial() {

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