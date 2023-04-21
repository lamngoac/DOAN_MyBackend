var FlagScan = '0';

var ListDataInformation = [];
var ListDataAttribute = [];

function InvF_InventoryIn() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.formatNumber = function() {
        
        
        
        $('.Qty').number(true, 0);
        $('.UPBuy').number(true, 0);
        $('.ValInDesc').number(true, 0);
        $('.QtyMinSt').number(true, 0);
        $('.QtyEffSt').number(true, 0);
    };

    this.checkForm = function () {        

        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#IF_InvInNo', 'has-error-fix', 'Số phiếu nhập không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvInType', 'has-error-fix', 'Chưa chọn loại nhập kho!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeIn', 'has-error-fix', 'Chưa chọn kho nhập!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerCode', 'has-error-fix', 'Chưa chọn nhà cung cấp!');

        if (commonUtils.checkElementExists('#FlagNhapTuDH')) {
            var flagtratudh = '0';
            var inputFlagTraTuDH = $('#FlagNhapTuDH');
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

        var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
        if (trArr !== null && trArr.length > 0) {
            for (var i = 0; i < trArr.length; i++) {
                var trCur = trArr[i];
                if (trCur !== null && trCur !== undefined) {
                    var rd = $(trCur).attr('rd');
                    var productCode = $(trCur).attr('productcode');
                    var qty = $('tbody#table-tbodyID.GetPrd tr td').find('input.qty-' + rd).val();
                    $('#table-tbodyIDXacThuc').find('tr[productcode="' + productCode + '"]').each(function () {
                        debugger
                        var tr = $(this);
                        var idx = $(tr).attr('idx');
                        var trdata = $('tbody#table-tbodyIDXacThuc').find('tr.trdata[idx = "' + idx + '"]');
                        var qrcode = $(trdata).find('input.QRCode').val();
                        var qrcodeCur = qrcode.split(',');

                        if (qty < qrcodeCur.length) {
                            objToastr = {
                                ToastrType: 'error',
                                ToastrMessage: 'Số lượng mã xác thực phải < số lượng hàng hóa'
                            };
                            listError.push(objToastr);
                        }
                    });
                }
            }
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
        var iF_InvInNo = commonUtils.returnValueText('#IF_InvInNo');
        var invInType = commonUtils.returnValueText('#InvInType');
        var invCodeIn = commonUtils.returnValueText('#InvCodeIn');
        var customerCode = commonUtils.returnValueText('#CustomerCode');
        var invoiceNo = commonUtils.returnValueText('#InvoiceNo');
        var refNo = commonUtils.returnValueText('#RefNo');
        var refNoSys = commonUtils.returnValueText('#RefNoSys');
        var refType = commonUtils.returnValueText('#RefType');
        var userDeliver = commonUtils.returnValueText('#UserDeliver');
        var totalValIn = commonUtils.returnValueTextOrNull('#TotalValIn');
        var totalValInDesc = commonUtils.returnValueTextOrNull('#TotalValInDesc');
        var totalValInAfterDesc = commonUtils.returnValueTextOrNull('#TotalValInAfterDesc');        
        var remarkMst = commonUtils.returnValueTextOrNull('#Remark');
        var invFCFInCode01 = commonUtils.returnValueTextOrNull('#InvFCFInCode01');
        var invFCFInCode02 = commonUtils.returnValueTextOrNull('#InvFCFInCode02');
        var invFCFInCode03 = commonUtils.returnValueTextOrNull('#InvFCFInCode03');
        
        
        
        var Lst_InvF_InventoryInDtl = [];
        var Lst_InvF_InventoryInInstLot = [];
        var Lst_InvF_InventoryInInstSerial = [];
        var Lst_InvF_InventoryInQR = [];

        Lst_InvF_InventoryInDtl = returnLst_InvF_InventoryInDtl();
        Lst_InvF_InventoryInInstLot = returnLst_InvF_InventoryInInstLot();
        Lst_InvF_InventoryInInstSerial = returnLst_InvF_InventoryInInstSerial();
        Lst_InvF_InventoryInQR = returnLst_InvF_InventoryInQR();
        

        var Obj_InvF_InventoryIn = {};
        Obj_InvF_InventoryIn.IF_InvInNo = iF_InvInNo;
        Obj_InvF_InventoryIn.InvInType = invInType;
        Obj_InvF_InventoryIn.InvCodeIn = invCodeIn;
        Obj_InvF_InventoryIn.CustomerCode = customerCode;
        Obj_InvF_InventoryIn.InvoiceNo = invoiceNo;
        Obj_InvF_InventoryIn.RefNo = refNo;
        Obj_InvF_InventoryIn.RefNoSys = refNoSys;
        Obj_InvF_InventoryIn.RefType = refType;
        Obj_InvF_InventoryIn.UserDeliver = userDeliver;
        Obj_InvF_InventoryIn.TotalValIn = totalValIn;
        Obj_InvF_InventoryIn.TotalValInDesc = totalValInDesc;
        Obj_InvF_InventoryIn.TotalValInAfterDesc = totalValInAfterDesc;
        Obj_InvF_InventoryIn.Remark = remarkMst;
        Obj_InvF_InventoryIn.InvFCFInCode01 = invFCFInCode01;
        Obj_InvF_InventoryIn.InvFCFInCode02 = invFCFInCode02;
        Obj_InvF_InventoryIn.InvFCFInCode03 = invFCFInCode03;        
        
        var tem = {};
        tem.Obj_InvF_InventoryIn = Obj_InvF_InventoryIn;
        tem.Lst_InvF_InventoryInDtl = Lst_InvF_InventoryInDtl;
        tem.Lst_InvF_InventoryInInstLot = Lst_InvF_InventoryInInstLot;
        tem.Lst_InvF_InventoryInInstSerial = Lst_InvF_InventoryInInstSerial;
        tem.Lst_InvF_InventoryInQR = Lst_InvF_InventoryInQR;
        tem.FlagRedirect = flagRedirect;
        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };

    function returnLst_InvF_InventoryInDtl() {
        debugger
        var Lst_InvF_InventoryInDtl = [];
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
                            var valinafterdesc = $('tbody#table-tbodyID.GetPrd tr td').find('input.valinafterdesc-' + rd).val();
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
                                    temDtlCurLot.UPIn = uPIn;
                                    temDtlCurLot.UPInDesc = uPInDesc;
                                    temDtlCurLot.ValInAfterDesc = sumqty * (parseFloat(uPIn) - parseFloat(uPInDesc));
                                    temDtlCurLot.UnitCode = unitCode;
                                    temDtlCurLot.Remark = remark;
                                    Lst_InvF_InventoryInDtl.push(temDtlCurLot);
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
                                    temDtlCurSerial.UPIn = uPIn;
                                    temDtlCurSerial.UPInDesc = uPInDesc;
                                    temDtlCurSerial.ValInAfterDesc = sumqtyS * (parseFloat(uPIn) - parseFloat(uPInDesc));
                                    temDtlCurSerial.UnitCode = unitCode;
                                    temDtlCurSerial.Remark = remark;
                                    Lst_InvF_InventoryInDtl.push(temDtlCurSerial);
                                    
                                }
                            }

                            continue;
                        }

                        if (arrInvCodeInActual.length > 0) {
                            debugger
                            var $trProductCahe = $('#table-detailInvCodeInActual tr[productcode="' + productCode + '"]');
                            var result = $trProductCahe.find('td div.products-list-cache .result[productcode="' + productCode + '"]');
                            for (var iresult = 0; iresult < result.length; iresult++) {
                                var temDtlCurInActual = {};
                                var invCodeInActualDtl = $(result[iresult]).find('input.InvCodeInActual').val();
                                var qty = $(result[iresult]).find('input.Qty').val();

                                temDtlCurInActual.InvCodeInActual = invCodeInActualDtl;
                                temDtlCurInActual.ProductCode = productCode;
                                temDtlCurInActual.ProductName = productname;
                                temDtlCurInActual.ProductCodeUser = productcodeuser;
                                temDtlCurInActual.ProductCodeBase = productcodebase;
                                temDtlCurInActual.ProductCodeRoot = productcoderoot;
                                temDtlCurInActual.Qty = qty;
                                temDtlCurInActual.UPIn = uPIn;
                                temDtlCurInActual.UPInDesc = uPInDesc;
                                temDtlCurInActual.ValInAfterDesc = parseFloat(qty) * (parseFloat(uPIn) - parseFloat(uPInDesc));
                                temDtlCurInActual.UnitCode = unitCode;
                                temDtlCurInActual.Remark = remark;
                                Lst_InvF_InventoryInDtl.push(temDtlCurInActual);
                            }
                            
                        }
                    }
                }
            }
        }
        return Lst_InvF_InventoryInDtl;
    }

    function returnLst_InvF_InventoryInInstLot() {
        debugger
        var Lst_InvF_InventoryInInstLot = [];

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
                    temLotCur.ExpiredDate = commonUtils.returnValueOrNull($(trdata).find('input.ExpiredDate').val());
                    temLotCur.ValDateExpired = commonUtils.returnValueOrNull($(trdata).find('input.ValDateExpired').val());

                    Lst_InvF_InventoryInInstLot.push(temLotCur);
                }
            });
        }


        return Lst_InvF_InventoryInInstLot;
    }

    function returnLst_InvF_InventoryInInstSerial() {
        var Lst_InvF_InventoryInInstSerial = [];

        var rowslot = $('tbody#table-detailSerial tr.trdata').length;
        var rowsS = $('tbody#table-detailSerial tr.trdata');
        if (rowslot > 0) {
            for (var i = 0; i < rowsS.length; i++) {
                var trdata = $('tbody#table-detailSerial').find('tr.trdata td');

                var temSerialCur = {};
                temSerialCur.InvCodeInActual = commonUtils.returnValueOrNull($(trdata[i]).find('input.InvCodeInActual').val());
                temSerialCur.ProductCode = commonUtils.returnValueOrNull($(trdata[i]).find('input.ProductCode').val());
                temSerialCur.SerialNo = commonUtils.returnValueOrNull($(trdata[i]).find('input.SerialNo').val());

                Lst_InvF_InventoryInInstSerial.push(temSerialCur);
            }
            //$('tbody#table-detailSerial tr.trdata').each(function () {

            //    var tr = $(this);
            //    if (tr !== undefined && tr !== null) {
            //        var idx = $(tr).attr('idx');
            //        var trdata = $('tbody#table-detailSerial').find('tr.trdata[idx = "' + idx + '"]');

            //        var temSerialCur = {};
            //        temSerialCur.InvCodeInActual = commonUtils.returnValueOrNull($(trdata).find('input.InvCodeInActual').val());
            //        temSerialCur.ProductCode = commonUtils.returnValueOrNull($(trdata).find('input.ProductCode').val());
            //        temSerialCur.SerialNo = commonUtils.returnValueOrNull($(trdata).find('input.SerialNo').val());

            //        Lst_InvF_InventoryInInstSerial.push(temSerialCur);
            //    }
            //});
        }

        return Lst_InvF_InventoryInInstSerial;
    }

    function returnLst_InvF_InventoryInQR() {
        debugger
        var Lst_InvF_InventoryInQR = [];
        var rowsgetprdxt = $("tbody#table-tbodyIDXacThuc tr.trdata").length;
        if (rowsgetprdxt > 0) {
            var trArr = $('tbody#table-tbodyIDXacThuc tr.trdata');
            if (trArr !== null && trArr.length > 0) {
                for (var i = 0; i < trArr.length; i++) {
                    var trCur = trArr[i];
                    if (trCur !== null && trCur !== undefined) {

                        var idx = $(trCur).attr('idx');
                        var productCodeQR = $(trCur).attr('productcode');
                        var trdata = $('tbody#table-tbodyIDXacThuc').find('tr.trdata[idx = "' + idx + '"]');

                        var temXtCur = {};
                        temXtCur.QRCode = $(trdata).find('input.QRCode').val();
                        temXtCur.ProductCode = $('input.ProductCode').val();
                        temXtCur.NetworkID = $('input.NetworkID').val();
                        temXtCur.BoxNo = $(trdata).find('input.BoxNo').val();
                        temXtCur.CanNo = $(trdata).find('input.CanNo').val();
                        temXtCur.ProductLotNo = $(trdata).find('input.ProductLotNo').val();
                        temXtCur.ShiftInCode = $(trdata).find('input.ShiftInCode').val();
                        temXtCur.UserKCS = $(trdata).find('input.UserKCS').val();

                        Lst_InvF_InventoryInQR.push(temXtCur);
                    }
                }
            }
        }

        return Lst_InvF_InventoryInQR;

    }

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

    this.deleteData = function (listInvF_InventoryIn) {
        debugger;
        if (listInvF_InventoryIn.length > 0) {
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
                            objlistinvf_inventoryin: commonUtils.returnJSONValue(listInvF_InventoryIn),
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
            //var listError = [];
            //var objToastr = {
            //    ToastrType: 'error',
            //    ToastrMessage: 'Chưa chọn phiếu nhập kho cần xóa!'
            //};
            //listError.push(objToastr);

            //if (listError !== undefined && listError !== null && listError.length > 0) {
            //    commonUtils.showToastr(listError);
            //    //return false;
            //}
            ////alert("Chưa chọn phiếu nhập kho cần xóa!");
            //return;
        }
    };

    this.approveData = function (listInvF_InventoryIn) {

        if (listInvF_InventoryIn.length > 0) {
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
                            objlistinvf_inventoryin: commonUtils.returnJSONValue(listInvF_InventoryIn),
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
            //var listError = [];
            //var objToastr = {
            //    ToastrType: 'error',
            //    ToastrMessage: 'Chưa chọn phiếu nhập kho cần duyệt!'
            //};
            //listError.push(objToastr);

            //if (listError !== undefined && listError !== null && listError.length > 0) {
            //    commonUtils.showToastr(listError);
            //    //return false;
            //}
            ////alert("Chưa chọn phiếu nhập kho cần duyệt!");
            //return;
        }
    };

    this.cancelData = function (listInvF_InventoryIn) {
        
        if (listInvF_InventoryIn.length > 0) {
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
                            objlistinvf_inventoryin: commonUtils.returnJSONValue(listInvF_InventoryIn),
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

function ReloadSelectPrd_Old() {
    $('#ProductCodeQR').find('option').remove();
    $('#ProductCodeQR').append($("<option></option>").attr("value", '').text('--Chọn hàng hóa--'));

    var rows = $('tbody#table-tbodyID tr').length;
    if (rows > 0) {
        $("#table-tbodyID tr").each(function () {
            if ($(this).is(":hidden")) {
                return;
            }

            var idx = $(this).attr('idx');
            var productCode = $(this).find('input[type="hidden"][name="Lst_InvF_InventoryInDtl[' + idx + '].ProductCode"]').val();
            var productCodeUser = $(this).find('input[type="hidden"][name="Lst_InvF_InventoryInDtl[' + idx + '].ProductCodeUser"]').val();
            var productName = $(this).find('input[type="hidden"][name="Lst_InvF_InventoryInDtl[' + idx + '].ProductName"]').val();
            var flagLot = $(this).find('input[type="hidden"][name="Lst_InvF_InventoryInDtl[' + idx + '].FlagLot"]').val();
            var flagSerial = $(this).find('input[type="hidden"][name="Lst_InvF_InventoryInDtl[' + idx + '].FlagSerial"]').val();

            if (flagLot !== '1' && flagSerial !== '1') {
                $('#ProductCodeQR').append($("<option></option>").attr("value", productCode).attr("productcodeuser", productCodeUser).attr("productname", productName).text(productCodeUser + ' - ' + productName));
            }
        });
    }
}

function ChangeProductCodeQR_Old(thiz) {
    var productCode = $(thiz).val();
    if (productCode === undefined || productCode === '') {
        return;
    }

    var productName = $('option:selected', $(thiz)).attr('productname');
    var productCodeUser = $('option:selected', $(thiz)).attr('productcodeuser');

    var strHtml = getHtmlFromTemplate($('#rowtemplateQR'), {
        ProductCode: productCode,
        ProductCodeUser: productCodeUser,
        ProductName: productName,
        QRCode: '',
        NetworkID: '',
        BoxNo: '',
        CanNo: '',
        ProductLotNo: '',
        ShiftInCode: '',
        UserKCS: '',
        idx: 999999
    });

    //var oldItem = $('#table-tbodyIDXacThuc').find('tr[productCode="' + productCode + '"]');
    //if (oldItem != undefined && oldItem.length > 0) {
    //    oldItem.eq(1).replaceWith(strHtml);
    //} else {
    //    $('#table-tbodyIDXacThuc').append(strHtml);
    //}
    $('#table-tbodyIDXacThuc').append(strHtml);

    updateTableTrIdxColumn2($('#table-tbodyIDXacThuc tr'), true);

    $(thiz).val('');
    $(thiz).trigger('change');
}


function ChangeNhapTuDH(thiz) {
    var checked = $(thiz).prop('checked');
    if (checked === true) {
        $('#RefNo').removeClass("disabled-fix");
        $('#RefType').removeClass("disabled-fix");
        $('#RefNo').focus();
    }
    else {
        $('#RefNo').val('');
        $('#RefNo').addClass("disabled-fix");
        $('#RefType').addClass("disabled-fix");
    }
}

//Thay dổi Giá, giảm giá, Tính tổng tiền
function inputQty_Old(thiz) {
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

        //Cập nhật SL hàng hóa tại các vị trí (Popup Vị trí nhập)
        var productCode = $(thiz).parents("tr:first").attr('data-prdcode');
        let isFirst = true;
        
        $('#table-detailInvCodeInActual').find('tr[productcode="' + productCode + '"]').each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');
            debugger;
            var qtyNew = isFirst ? parseFloat(qty) : 0;
            tr.find('input[name="Lst_InvF_InventoryInDtlTemp[' + idx + '].Qty"]').val(qtyNew);
            isFirst = false;
        });

    }, 0);
}

function inputUPIn_Old(thiz) {
    setTimeout(function () {
        var upin = $(thiz).val();
        if (upin === undefined || upin === '') {
            alert('Vui lòng nhập giá nhập!');
            return;
        }
        else if (parseFloat(upin) < 0) {
            alert('Giá nhập phải >= 0!');
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

function inputUPInDesc_Old(thiz) {
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
            alert('Giảm giá  phải <= giá nhập!');
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
    var tongsl = 0.00;
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

                tongsl += parseFloat(qty);
            }
        });
    }

    giamgia = tongtienhang - tongtientrancc;
    if ($('#TotalValIn').length) {
        $('#TotalValIn').val(tongtienhang);
    }
    if ($('#TotalValInDesc').length) {
        $('#TotalValInDesc').val(giamgia);
    }
    if ($('#TotalValInAfterDesc').length) {
        $('#TotalValInAfterDesc').val(tongtientrancc);
    }
    
    document.getElementById('spanTotalPrd').textContent = tongsl;
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
}

function ChangUnit(prdcode) {
    
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

    inputUPInDesc(document.getElementById('uPInDesc_' + prdcodenew));
    TongTienHang();

    //Cập nhật SL hàng hóa tại các vị trí (Popup Vị trí nhập)
    let isFirst = true;    
    $('#table-detailInvCodeInActual').find('tr[productcode="' + prdcodenew + '"]').each(function () {
        var tr = $(this);
        var idx = $(tr).attr('idx');
        
        var qtyNew = isFirst ? parseFloat(qty) : 0;
        tr.find('input[name="Lst_InvF_InventoryInDtlTemp[' + idx + '].Qty"]').val(qtyNew);
        isFirst = false;
    });
}

//Cắt Mã xác thực, mã hộp, mã thùng
function splitQRCode_Old(thiz) {
    setTimeout(function () {

        let qrCodeText = $(thiz).val();
        if (qrCodeText === undefined || qrCodeText === '') {           
            return;
        }

        let fields = qrCodeText.split('=');
        if (fields.length >= 2) {
            let qrCode = fields[1];
            $(thiz).val(qrCode);
        }
        
    }, 0);
}