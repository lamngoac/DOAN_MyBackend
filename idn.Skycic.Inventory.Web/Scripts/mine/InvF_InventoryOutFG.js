function InvF_InventoryOutFG() {
    this.ActionType = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#InvCode', 'has-error-fix', 'Mã kho không được trống!')) {
            return false;
        } else {
            return true;
        }        
        
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
        if (this.checkForm() !== true) {
            return false;
        }
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var rows = $("tbody#table-tbodyID tr.trdata").length;
        if (rows > 0) {
            var trArr = $('tbody#table-tbodyID tr.trdata').has('input[name="ckb"]:checked');
            if (trArr !== null && trArr.length > 0) {
                var tem = new Object();
                var InvF_InventoryOutFG = new Object();

                var IF_InvOutFGNo = $('#IF_InvOutFGNo').val();
                var createDTime = $('#CreateDTime').val();
                var invcode = $('#InvCode').val();
                var remark = $('#Remark').val();
                var invouttype = $('#InvOutType').val();
                var formouttype = $('#FormOutType').val();
                var invfouttype = $('#InvFOutType').val();
                var mst = $('#MST').val();

                InvF_InventoryOutFG.IF_InvOutFGNo = IF_InvOutFGNo;
                InvF_InventoryOutFG.CreateDTime = createDTime;
                InvF_InventoryOutFG.InvCode = invcode;
                InvF_InventoryOutFG.Remark = remark;
                InvF_InventoryOutFG.InvOutType = invouttype;
                InvF_InventoryOutFG.FormOutType = formouttype;
                InvF_InventoryOutFG.InvFOutType = invfouttype;
                InvF_InventoryOutFG.MST = mst;

                var InvF_InventoryOutFGDtls = [];
                for (var i = 0; i < trArr.length; i++) {
                    var trCur = trArr[i];
                    if (trCur !== null && trCur !== undefined) {
                        var idx = $(trCur).attr('idx');
                        var temDtlCur = {};
                        temDtlCur.PartCode = $(trCur).find('input[type="hidden"][name="ListInvF_InventoryOutFGDtl[' + idx + '].PartCode"]').val();
                        temDtlCur.Qty = $(trCur).find('input[type="text"][name="ListInvF_InventoryOutFGDtl[' + idx + '].Qty"]').val();
                        
                        InvF_InventoryOutFGDtls.push(temDtlCur);
                    }
                }
                tem.Lst_InvF_InventoryOutFGDtl = InvF_InventoryOutFGDtls;
                tem.InvF_InventoryOutFG = InvF_InventoryOutFG;

                var modelCur = JSON.stringify(tem);
                var data = {
                    __RequestVerificationToken: token,
                    model: modelCur,
                };
                return data;
            } else {
                alert("Chưa chọn sản phẩm cho phiếu xuất kho!");
                return false;
            }
        }
        alert("Danh sách sản phẩm trống!");
        return false;
    };
    this.saveData = function () {
        //debugger;
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
    this.deleteData = function (objInvF_InventoryOutFG) {
        var ifinvoutfgno = commonUtils.returnValue(objInvF_InventoryOutFG.IF_InvOutFGNo);
        
        if (!commonUtils.isNullOrEmpty(ifinvoutfgno)) {
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
                            ifinvoutfgno: ifinvoutfgno,
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
    this.approveData = function (objInvF_InventoryOutFG) {
        var ifinvoutfgno = commonUtils.returnValue(objInvF_InventoryOutFG.IF_InvOutFGNo);

        if (!commonUtils.isNullOrEmpty(ifinvoutfgno)) {
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
                            ifinvoutfgno: ifinvoutfgno,
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
    this.showPopupSerial = function (ifinvoutfgno, partcode) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = {
            ifinvoutfgno: ifinvoutfgno,
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