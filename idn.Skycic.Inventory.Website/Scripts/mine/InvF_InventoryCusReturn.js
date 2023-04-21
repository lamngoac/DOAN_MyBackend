
function InvF_InventoryCusReturn() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.formatNumber = function () {
        $('.Qty').number(true, 0);
        $('.UPBuy').number(true, 0);
        $('.ValInDesc').number(true, 0);
        $('.QtyMinSt').number(true, 0);
        $('.QtyEffSt').number(true, 0);
    };

    this.checkForm = function () {

        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#IF_InvCusReturnNo', 'has-error-fix', 'Số phiếu nhập không được trống!');
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvInType', 'has-error-fix', 'Chưa chọn loại khách hàng trả hàng!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeIn', 'has-error-fix', 'Chưa chọn kho nhập!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerCode', 'has-error-fix', 'Chưa chọn khách hàng!');

        if (commonUtils.checkElementExists('#FlagTraTuDH')) {
            var flagtratudh = '0';
            var inputFlagTraTuDH = $('#FlagTraTuDH');
            if (inputFlagTraTuDH !== undefined && inputFlagTraTuDH !== null) {
                if (inputFlagTraTuDH.prop('checked')) {
                    flagtratudh = '1';
                }
            }
        }
        if (flagtratudh === '1') {
            listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#OrderNo', 'has-error-fix', 'Chưa nhập số đơn hàng!');
        }

        var rowsgetbom = $("tbody#table-tbodyID.GetPrd tr.trdata").length;
        if (rowsgetbom > 0) {
            //var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
            //if (trArr !== null && trArr.length > 0) {
            //    for (var i = 0; i < trArr.length; i++) {
            //        var trCur = trArr[i];
            //        if (trCur !== null && trCur !== undefined) {
            //            var idx = $(trCur).attr('idx');
            //            var temDtlCur = {};
            //            debugger;
            //            temDtlCur.ProductType = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].ProductType"]').val();
            //            temDtlCur.ProductName = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].ProductName"]').val();
            //            if (temDtlCur.ProductType === productType) {
            //                listError = commonUtils.checkElementIsSame_AddListError(listError, '#ProductType', 'has-error-fix', 'Loại sản phẩm là combo thì hàng thành phần không chứa combo ' + temDtlCur.ProductName +'!');
            //            }
            //        }
            //    }
            //}
        }
        else {
            listError = commonUtils.checkElementIsSame_AddListError(listError, '#table-tbodyID', 'has-error-fix', 'Danh sách hàng hóa trống!');
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    //this.showPopupWarning = function () {
    //    ShowPopupWarning();
    //};

    this.getData = function (flagRedirect) {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        debugger;
        var iF_InvCusReturnNo = commonUtils.returnValueText('#IF_InvCusReturnNo');
        var invInType = commonUtils.returnValueText('#InvInType');
        var invCodeIn = commonUtils.returnValueText('#InvCodeIn');
        var customerCode = commonUtils.returnValueText('#CustomerCode');
        var invoiceNo = commonUtils.returnValueText('#InvoiceNo');
        var refNo = commonUtils.returnValueText('#RefNo');
        var refNoSys = commonUtils.returnValueText('#RefNo');
        var refType = commonUtils.returnValueText('#RefType');
        //if (refType === 'RO') {
        //    refNo = refNo.substring(3);//Nếu là RO thì cắt 3 ký tự đầu 'RO-'
        //}
        var totalValCusReturn = commonUtils.returnValueTextOrNull('#TotalValCusReturn');
        var totalValCusReturnDesc = commonUtils.returnValueTextOrNull('#TotalValCusReturnDesc');
        var totalValCusReturnAfterDesc = commonUtils.returnValueTextOrNull('#TotalValCusReturnAfterDesc');
        var remarkMst = commonUtils.returnValueTextOrNull('#Remark');

        //if (commonUtils.checkElementExists('#FlagSerial')) {
        //    var flagserial = '0';
        //    var inputFlagActive = $('#FlagSerial');
        //    if (inputFlagActive !== undefined && inputFlagActive !== null) {
        //        if (inputFlagActive.prop('checked')) {
        //            flagserial = '1';
        //        }
        //    }
        //}

        var Lst_InvF_InventoryCusReturnDtl = [];
        var Lst_InvF_InventoryCusReturnInstLot = [];
        var Lst_InvF_InventoryCusReturnInstSerial = [];
        var Lst_InvF_InventoryCusReturnCover = [];

        Lst_InvF_InventoryCusReturnDtl = returnLst_InvF_InventoryCusReturnDtl();
        Lst_InvF_InventoryCusReturnInstLot = returnLst_InvF_InventoryCusReturnInstLot();
        Lst_InvF_InventoryCusReturnInstSerial = returnLst_InvF_InventoryCusReturnInstSerial();
        Lst_InvF_InventoryCusReturnCover = returnLst_InvF_InventoryCusReturnCover();
        

        var Obj_InvF_InventoryCusReturn = {};
        Obj_InvF_InventoryCusReturn.IF_InvCusReturnNo = iF_InvCusReturnNo;
        Obj_InvF_InventoryCusReturn.InvInType = invInType;
        Obj_InvF_InventoryCusReturn.InvCodeIn = invCodeIn;
        Obj_InvF_InventoryCusReturn.CustomerCode = customerCode;
        Obj_InvF_InventoryCusReturn.InvoiceNo = invoiceNo;
        Obj_InvF_InventoryCusReturn.RefNo = refNo;
        Obj_InvF_InventoryCusReturn.RefNoSys = refNoSys;
        Obj_InvF_InventoryCusReturn.RefType = refType;
        Obj_InvF_InventoryCusReturn.TotalValCusReturn = totalValCusReturn;
        Obj_InvF_InventoryCusReturn.TotalValCusReturnDesc = totalValCusReturnDesc;
        Obj_InvF_InventoryCusReturn.TotalValCusReturnAfterDesc = totalValCusReturnAfterDesc;
        Obj_InvF_InventoryCusReturn.Remark = remarkMst;

        var tem = {};
        tem.Obj_InvF_InventoryCusReturn = Obj_InvF_InventoryCusReturn;
        tem.Lst_InvF_InventoryCusReturnDtl = Lst_InvF_InventoryCusReturnDtl;
        tem.Lst_InvF_InventoryCusReturnInstLot = Lst_InvF_InventoryCusReturnInstLot;
        tem.Lst_InvF_InventoryCusReturnInstSerial = Lst_InvF_InventoryCusReturnInstSerial;
        tem.Lst_InvF_InventoryCusReturnCover = Lst_InvF_InventoryCusReturnCover;
        tem.FlagRedirect = flagRedirect;
        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };

    this.saveData = function (flagRedirect) {

        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData(flagRedirect);
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

    function returnLst_InvF_InventoryCusReturnDtl() {
        debugger
        var Lst_InvF_InventoryCusReturnDtl = [];
        var temDtlCur = {};
        var rowsgetprd = $("tbody#table-tbodyID.GetPrd tr.trdata").length;
        if (rowsgetprd > 0) {
            var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
            if (trArr !== null && trArr.length > 0) {
                for (var i = 0; i < trArr.length; i++) {
                    var trCur = trArr[i];
                    if (trCur !== null && trCur !== undefined) {
                        var productCode = $(trCur).attr('productcode');
                        temDtlCur.ProductCode = productCode;
                        debugger

                        var rd = $(trCur).attr('rd');
                        var divCur = productCode;

                        if ($(divCur) !== undefined && $(divCur) !== null) {
                            var productname = $('tbody#table-tbodyID.GetPrd tr td').find('input.productname-' + rd).val();
                            var productcodeuser = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcodeuser-' + rd).val();
                            var productcodebase = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcodebase-' + rd).val();
                            var productcoderoot = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcoderoot-' + rd).val();
                            var flagLot = $('tbody#table-tbodyID.GetPrd tr td').find('input.flaglot-' + rd).val();
                            var flagSerial = $('tbody#table-tbodyID.GetPrd tr td').find('input.flagserial-' + rd).val();
                            var invcodeinactual = $('tbody#table-tbodyID.GetPrd tr td').find('input.invcodeinactual-' + rd).val();
                            var uPIn = $('tbody#table-tbodyID.GetPrd tr td').find('input.upin-' + rd).val();
                            var uPInDesc = $('tbody#table-tbodyID.GetPrd tr td').find('input.upindesc-' + rd).val();
                            //var valinafterdesc = $('tbody#table-tbodyID.GetPrd tr td').find('input.valinafterdesc-' + rd).val();
                            var remark = $('tbody#table-tbodyID.GetPrd tr td').find('textarea.remark-' + rd).val();
                            var qty = $('tbody#table-tbodyID.GetPrd tr td').find('input.qty-' + rd).val();

                            debugger;
                            var unitCode = $('tbody#table-tbodyID.GetPrd tr td.td-select2 .select2-' + rd).val();

                        }
                        debugger;
                        var arrInvCodeInActual = invcodeinactual.split(', ');
                        if (flagLot === '1') {
                            if (arrInvCodeInActual.length > 0) {
                                for (var iInv = 0; iInv < arrInvCodeInActual.length; iInv++) {

                                    var sumqty = 0.0;
                                    var invCodeInActualCur = arrInvCodeInActual[iInv];

                                    $('#table-detailLot').find('tr[productcode="' + productCode + '"]').each(function () {
                                        var tr = $(this);
                                        var idx = $(tr).attr('idx');
                                        var trdata = $('tbody#table-detailLot').find('tr.trdata[idx = "' + idx + '"]');

                                        var strqty = trdata.find('input.Qty').val();
                                        var invCodeInActualLot = trdata.find('input.InvCodeInActual').val();
                                        if (invCodeInActualCur === invCodeInActualLot) {
                                            var qty = parseFloat(strqty);
                                            sumqty += qty;
                                        }
                                    });

                                    var temDtlCurLot = {};
                                    temDtlCurLot.InvCodeInActual = invCodeInActualCur;
                                    temDtlCurLot.ProductCode = productCode;
                                    temDtlCurLot.Qty = sumqty;
                                    temDtlCurLot.ProductCodeRoot = productcoderoot;
                                    temDtlCurLot.UnitCode = unitCode;
                                    temDtlCurLot.Remark = remark;
                                    Lst_InvF_InventoryCusReturnDtl.push(temDtlCurLot);
                                }
                            }

                            continue;
                        }
                        else if (flagSerial == '1') {
                            if (arrInvCodeInActual.length > 0) {
                                for (var iInv = 0; iInv < arrInvCodeInActual.length; iInv++) {

                                    var sumqtyS = 0.0;
                                    var invCodeInActualCur = arrInvCodeInActual[iInv];

                                    $('#table-detailSerial').find('tr[productcode="' + productCode + '"]').each(function () {
                                        var tr = $(this);
                                        var idx = $(tr).attr('idx');
                                        var trdata = $('tbody#table-detailSerial').find('tr.trdata[idx = "' + idx + '"]');

                                        var invCodeInActualSerial = trdata.find('input.InvCodeInActual').val();
                                        if (invCodeInActualCur === invCodeInActualSerial) {
                                            
                                            sumqtyS += 1;
                                        }
                                    });
                                    var temDtlCurSerial = {};

                                    temDtlCurSerial.InvCodeInActual = invCodeInActualCur;
                                    temDtlCurSerial.ProductCode = productCode;
                                    temDtlCurSerial.Qty = sumqtyS;
                                    temDtlCurSerial.ProductCodeRoot = productcoderoot;
                                    temDtlCurSerial.UnitCode = unitCode;
                                    temDtlCurSerial.Remark = remark;
                                    Lst_InvF_InventoryCusReturnDtl.push(temDtlCurSerial);
                                }
                            }

                            continue;
                        }

                        if (arrInvCodeInActual.length > 0) {
                            debugger
                            var result = $('#table-detailInvCodeInActual tr td div.products-list-cache .result[productcode="' + productCode + '"]');
                            for (var iresult = 0; iresult < result.length; iresult++) {
                                var temDtlCurInActual = {};
                                var invCodeInActualDtl = $(result[iresult]).find('input.InvCodeInActual').val();
                                var qty = $(result[iresult]).find('input.Qty').val();

                                temDtlCurInActual.InvCodeInActual = invCodeInActualDtl;
                                temDtlCurInActual.ProductCode = productCode;
                                temDtlCurInActual.ProductCodeRoot = productcoderoot;
                                temDtlCurInActual.Qty = qty;
                                temDtlCurInActual.UnitCode = unitCode;
                                temDtlCurInActual.Remark = remark;
                                Lst_InvF_InventoryCusReturnDtl.push(temDtlCurInActual);
                            }

                        }
                    }
                }
            }
        }
        return Lst_InvF_InventoryCusReturnDtl;
    }

    function returnLst_InvF_InventoryCusReturnInstLot() {
        debugger
        var Lst_InvF_InventoryCusReturnInstLot = [];

        var rowslot = $('tbody#table-detailLot tr.trdata').length;
        if (rowslot > 0) {
            $('tbody#table-detailLot tr.trdata').each(function () {

                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var idx = $(tr).attr('idx');
                    var trdata = $('tbody#table-detailLot').find('tr.trdata[idx = "' + idx + '"]');

                    var temLotCur = {};
                    temLotCur.InvCodeInActual = commonUtils.returnValueOrNull($(trdata).find('input.InvCodeInActual').val());
                    temLotCur.ProductCode = commonUtils.returnValueOrNull($(trdata).find('input.ProductCode').val());
                    temLotCur.ProductLotNo = commonUtils.returnValueOrNull($(trdata).find('input.ProductLotNo').val());
                    temLotCur.Qty = commonUtils.returnValueOrNull($(trdata).find('input.Qty').val());
                    temLotCur.ProductionDate = commonUtils.returnValueOrNull($(trdata).find('input.ProductionDate').val());
                    temLotCur.ProductCodeRoot = commonUtils.returnValueOrNull($(trdata).find('input.ProductCodeRoot').val());
                    temLotCur.ExpiredDate = commonUtils.returnValueOrNull($(trdata).find('input.ExpiredDate').val());
                    temLotCur.ValDateExpired = commonUtils.returnValueOrNull($(trdata).find('input.ValDateExpired').val());

                    Lst_InvF_InventoryCusReturnInstLot.push(temLotCur);
                }
            });
        }
        return Lst_InvF_InventoryCusReturnInstLot;
    }

    function returnLst_InvF_InventoryCusReturnInstSerial() {
        var Lst_InvF_InventoryCusReturnInstSerial = [];

        var rowslot = $('tbody#table-detailSerial tr.trdata').length;
        var rowsS = $('tbody#table-detailSerial tr.trdata');
        if (rowslot > 0) {
            for (var i = 0; i < rowsS.length; i++) {
                var trdata = $('tbody#table-detailSerial').find('tr.trdata td');

                var temSerialCur = {};
                temSerialCur.InvCodeInActual = commonUtils.returnValueOrNull($(trdata[i]).find('input.InvCodeInActual').val());
                temSerialCur.ProductCode = commonUtils.returnValueOrNull($(trdata[i]).find('input.ProductCode').val());
                temSerialCur.ProductCodeRoot = commonUtils.returnValueOrNull($(trdata[i]).find('input.ProductCodeRoot').val());
                temSerialCur.SerialNo = commonUtils.returnValueOrNull($(trdata[i]).find('input.SerialNo').val());

                Lst_InvF_InventoryCusReturnInstSerial.push(temSerialCur);
            }
        }
        return Lst_InvF_InventoryCusReturnInstSerial;
    }

    function returnLst_InvF_InventoryCusReturnCover() {
        var Lst_InvF_InventoryCusReturnCover = [];

        var temDtlCur = {};
        var rowsgetprd = $("tbody#table-tbodyID.GetPrd tr.trdata").length;
        if (rowsgetprd > 0) {
            var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
            if (trArr !== null && trArr.length > 0) {
                for (var i = 0; i < trArr.length; i++) {
                    var trCur = trArr[i];
                    if (trCur !== null && trCur !== undefined) {
                        var productCode = $(trCur).attr('productcode');
                        temDtlCur.ProductCode = productCode;
                        debugger

                        var rd = $(trCur).attr('rd');
                        var divCur = productCode;
                        if ($(divCur) !== undefined && $(divCur) !== null) {
                            var productcoderoot = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcoderoot-' + rd).val();
                            var qty = $('tbody#table-tbodyID.GetPrd tr td').find('input.qty-' + rd).val();
                            var uPCusReturn = $('tbody#table-tbodyID.GetPrd tr td').find('input.upcusreturn-' + rd).val();
                            var uPCusReturnDesc = $('tbody#table-tbodyID.GetPrd tr td').find('input.upcusreturndesc-' + rd).val();
                            var unitCode = $('tbody#table-tbodyID.GetPrd tr td.td-select2 .select2-' + rd).val();
                            var remark = $('tbody#table-tbodyID.GetPrd tr td').find('textarea.remark-' + rd).val();

                            var temCover = {};
                            temCover.ProductCodeRoot = productcoderoot;
                            temCover.Qty = qty;
                            temCover.UPCusReturn = uPCusReturn;
                            temCover.UPCusReturnDesc = uPCusReturnDesc;
                            temCover.UnitCode = unitCode;
                            temCover.Remark = remark;
                            temCover.ValCusReturnAfterDesc = parseFloat(qty) * (parseFloat(uPCusReturn) - parseFloat(uPCusReturnDesc));

                            Lst_InvF_InventoryCusReturnCover.push(temCover);
                        }
                        
                    }
                }
            }
        }
        return Lst_InvF_InventoryCusReturnCover;
    }

    this.deleteData = function (listInvF_InventoryCusReturn) {
        debugger;
        if (listInvF_InventoryCusReturn.length > 0) {
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
                            objlistinvf_inventorycusreturn: commonUtils.returnJSONValue(listInvF_InventoryCusReturn),
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
            //alert("Chưa chọn phiếu khách hàng trả hàng cần xóa!");
            //return;
        }
    };

    this.approveData = function (listInvF_InventoryCusReturn) {

        if (listInvF_InventoryCusReturn.length > 0) {
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
                            objlistinvf_inventorycusreturn: commonUtils.returnJSONValue(listInvF_InventoryCusReturn),
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
            //alert("Chưa chọn phiếu khách hàng trả hàng cần duyệt!");
            //return;
        }
    };

    this.cancelData = function (listInvF_InventoryCusReturn) {

        if (listInvF_InventoryCusReturn.length > 0) {
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
                            objlistinvf_inventorycusreturn: commonUtils.returnJSONValue(listInvF_InventoryCusReturn),
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
            //alert("Chưa chọn phiếu khách hàng trả hàng cần huỷ!");
            //return;
        }
    };

    function doneFunction(objResult) {
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                var listToastr = [];
                objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages };
                listToastr.push(objToastr);
                commonUtils.showToastr(listToastr);
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
};

var FlagScan = '0';
function ChangeTraTuDH(thiz) {
    var checked = $(thiz).prop('checked');
    if (checked === true) {
        $('#OrderNo').removeAttr("readonly");
        $('#OrderNo').focus();
        $('#divPrdOrder').show();
        $('#myInput').hide();
        $('#quetmavach').hide();
        FlagScan = '0';
    }
    else {
        $('#OrderNo').val('');
        $('#OrderNo').attr("readonly", "readonly");
        $('#divPrdOrder').hide();
        $('#myInput').show();
        $('#quetmavach').hide();
        FlagScan = '0';
    }
}

function DeleleRowPrd(productcode) {
    $('tr[data-prdcode="' + productcode + '"]').remove();

    //Xóa lô cũ cùng ProductCode
    var trOldsLot = $('#table-detailLot').find('tr[productcode="' + productcode + '"]');
    $(trOldsLot).each(function () {
        $(this).remove();
        updateTableTrIdx($('#table-detailLot tr'), false);
    });
    //Xóa Serial
    var trOldsSerial = $('#table-detailSerial').find('tr[productcode="' + productcode + '"]');
    $(trOldsSerial).each(function () {
        $(this).remove();
        updateTableTrIdx($('#table-detailSerial tr'), false);
    });
    //Xóa VT nhập
    var trOldsSerial = $('#table-detailInvCodeInActual').find('tr[productcode="' + productcode + '"]');
    $(trOldsSerial).each(function () {
        $(this).remove();
        updateTableTrIdx($('#table-detailInvCodeInActual tr'), false);
    });

    TongTienHang();
    updateTableTrIdx($('#table-tbodyID tr'), false);

    EnableRefNo();
}

function EnableRefNo() {
    var refNo = $('#RefNo').val();
    if (refNo !== undefined && refNo.length > 0) {
        var lenItem = $('tbody#table-tbodyID').find('tr[flagvisible="1"]').length;
        if (lenItem > 0) {
            $('#RefNo').addClass('disabled-fix');
        }
        else {
            $('#RefNo').removeClass('disabled-fix');
        }
    }
}

function ChangUnit(prdcode) {
    debugger;
    //var y = '#Unitchange-' + prdcode;
    var thiz = document.getElementById('Unitchange-' + prdcode);
    var prdcodenew = $('option:selected', $(thiz)).attr('prdcode');
    var base = "#unit_" + prdcodenew;
    var unit = "#unit_" + prdcode;
    $(unit).css('display', 'none');
    $(unit).attr('flagvisible', '0');
    $(base).css('display', 'table-row');
    $(base).attr('flagvisible', '1');
    var selected = $('#Unitchange-' + prdcodenew);
    $(selected).val($(thiz).val()).prop('selected', true);
    $('.listproductbase').select2({
        minimumResultsForSearch: -1
    });

    let qty = document.getElementById('qty_' + prdcode).value;
    document.getElementById('qty_' + prdcodenew).value = qty;
    document.getElementById('uPInDesc_' + prdcodenew).value = document.getElementById('uPInDesc_' + prdcode).value;
    document.getElementById('remark_' + prdcodenew).value = document.getElementById('remark_' + prdcode).value;

    inputUPCusReturnDesc(document.getElementById('uPInDesc_' + prdcodenew));
    TongTienHang();

    //Cập nhật SL hàng hóa tại các vị trí (Popup Vị trí nhập)
    let isFirst = true;
    $('#table-detailInvCodeInActual').find('tr[productcode="' + prdcodenew + '"]').each(function () {
        var tr = $(this);
        var idx = $(tr).attr('idx');

        var qtyNew = isFirst ? parseFloat(qty) : 0;
        tr.find('input[name="Lst_InvF_InventoryCusReturnDtlTemp[' + idx + '].Qty"]').val(qtyNew);
        isFirst = false;
    });
}

//Thay đổi giá, giảm giá, thành tiền
function inputQty(thiz) {
    setTimeout(function () {

        var qty = $(thiz).val();
        if (qty === undefined || qty === '') {
            alert('Vui lòng nhập số lượng!');
            return;
        }
        else if (parseFloat(qty) <= 0) {
            alert('Số lượng phải > 0!');
            return;
        }

        var upin = $(thiz).parents("tr:first").find("[arr='upin']").val();
        var upindesc = $(thiz).parents("tr:first").find("[arr='upindesc']").val();
        if (upindesc == null || upindesc === undefined || upindesc == '') {
            upindesc = 0;
        }

        var valinafterdescElement = $(thiz).parents("tr:first").find("[arr='valinafterdesc']");

        if (!isNaN(upin) && !isNaN(qty)) {
            var totalPrice = (parseFloat(upin) - parseFloat(upindesc)) * parseFloat(qty);
            $(valinafterdescElement).val(totalPrice);
        } else {
            $(valinafterdescElement).val(0.00);
        }
        TongTienHang();

        //Cập nhật SL hàng hóa thành phần (HH Combo)
        var productCodeRoot = $(thiz).parents("tr:first").attr('data-prdcode');
        $('#table-detailCombo').find('tr[productCodeRoot="' + productCodeRoot + '"]').each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');

            var qtyRoot = tr.find('input[type="hidden"][name="Lst_InvF_InventoryCusReturnComboDtl[' + idx + '].QtyRoot"]').val();
            var qtyNew = parseFloat(qty) * parseFloat(qtyRoot);
            tr.find('input[name="Lst_InvF_InventoryCusReturnComboDtl[' + idx + '].Qty"]').val(qtyNew);
        });

        //Cập nhật SL hàng hóa tại các vị trí (Popup Vị trí nhập)        
        let isFirst = true;

        $('#table-detailInvCodeInActual').find('tr[productcode="' + productCodeRoot + '"]').each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');
            
            var qtyNew = isFirst ? parseFloat(qty) : 0;
            tr.find('input[name="Lst_InvF_InventoryCusReturnDtlTemp[' + idx + '].Qty"]').val(qtyNew);
            isFirst = false;
        });
    }, 0);
}

function inputUPCusReturn(thiz) {
    setTimeout(function () {
        var upin = $(thiz).val();
        if (upin === undefined || upin === '') {
            alert('Vui lòng nhập giá nhập!');
            return;
        }
        else if (parseFloat(upin) <= 0) {
            alert('Giá nhập phải > 0!');
            return;
        }

        var qty = $(thiz).parents("tr:first").find("[arr='qty']").val();
        var upindesc = $(thiz).parents("tr:first").find("[arr='upindesc']").val();
        if (upindesc === null || upindesc === undefined || upindesc === '') {
            upindesc = 0;
        }
        if (parseFloat(upindesc) > parseFloat(upin)) {
            alert('Giá nhập phải >= giảm giá!');
            $(thiz).parents("tr:first").find("[arr='upindesc']").val(0);
            return;
        }

        var valinafterdescElement = $(thiz).parents("tr:first").find("[arr='valinafterdesc']");

        if (!isNaN(upin) && !isNaN(qty)) {
            var totalPrice = (parseFloat(upin) - parseFloat(upindesc)) * parseFloat(qty);
            $(valinafterdescElement).val(totalPrice);
        } else {
            $(valinafterdescElement).val(0.00);
        }
        TongTienHang();
    }, 0);
}

function inputUPCusReturnDesc(thiz) {
    setTimeout(function () {
        var upindesc = $(thiz).val();
        if (upindesc === undefined || upindesc === '') {
            alert('Vui lòng nhập giảm giá!');
            return;
        }
        else if (parseFloat(upindesc) < 0) {
            alert('Giảm giá phải >= 0!');
            return;
        }

        var qty = $(thiz).parents("tr:first").find("[arr='qty']").val();
        var upin = $(thiz).parents("tr:first").find("[arr='upin']").val();

        if (upindesc === null || upindesc === undefined || upindesc === '') {
            upindesc = 0;
        }
        if (parseFloat(upindesc) > parseFloat(upin)) {
            alert('Giảm giá phải <= giá nhập!');
            $(thiz).val(0);
            return;
        }

        var valinafterdescElement = $(thiz).parents("tr:first").find("[arr='valinafterdesc']");

        if (!isNaN(upin) && !isNaN(qty)) {
            var totalPrice = (parseFloat(upin) - parseFloat(upindesc)) * parseFloat(qty);
            $(valinafterdescElement).val(totalPrice);
        } else {
            $(valinafterdescElement).val(0.00);
        }
        TongTienHang();
    }, 0);

}

function TongTienHang() {
    var tongtienhang = 0.00;
    var giamgia = 0.00;
    var tongtientrancc = 0.00;
    var rows = $('tbody#table-tbodyID tr').length;
    if (rows > 0) {
        $("#table-tbodyID tr").each(function () {
            if ($(this).is(":hidden")) {
                return;
            }

            var upin = $(this).find("[arr='upin']").val();
            var qty = $(this).find("[arr='qty']").val();
            var upindesc = $(this).find("[arr='upindesc']").val();
            if (upindesc === null || upindesc === undefined || upindesc === '') {
                upindesc = 0;
            }

            if (!isNaN(upin) && !isNaN(qty)) {

                var totalPrice = (parseFloat(upin) - parseFloat(upindesc)) * parseFloat(qty);
                $(this).find("[arr='valinafterdesc']").val(totalPrice);

                var total = parseFloat(upin) * parseFloat(qty);
                tongtienhang += total;

                if (!isNaN(totalPrice)) {
                    tongtientrancc += parseFloat(totalPrice);
                }
            }
        });
    }

    giamgia = tongtienhang - tongtientrancc;
    if ($('#TotalValCusReturn').length) {
        $('#TotalValCusReturn').val(tongtienhang);
    }
    if ($('#TotalValCusReturnDesc').length) {
        $('#TotalValCusReturnDesc').val(giamgia);
    }
    if ($('#TotalValCusReturnAfterDesc').length) {
        $('#TotalValCusReturnAfterDesc').val(tongtientrancc);
    }
}
