function InvFInventoryReturnSup() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };


    this.checkForm = function () {
        var listError = [];
     
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerCode', 'has-error-fix', 'Chưa chọn khách hàng!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeOut', 'has-error-fix', 'Chưa chọn kho xuất!');
        var rowsgetbom = $("tbody#table-tbodyID.GetPrd tr.trdata").length;
        if (rowsgetbom > 0) {

        }
        else {
            listError = commonUtils.checkElementIsSame_AddListError(listError, '#table-tbodyID', 'has-error-fix', 'Danh sách hàng hóa không được trống!');
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;

    }

    this.getData = function (flagRedirect) {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var objInvF_InventoryReturnSup = {};
        var Lst_InvF_InventoryReturnSupDtl = [];
        var Lst_InvF_InventoryReturnSupInstLot = [];
        var Lst_InvF_InventoryReturnSupInstSerial = [];
        debugger
        var objInvF_InventoryReturnSupDtl = returnLst_InvF_InventoryReturnSupDtl();
        if (objInvF_InventoryReturnSupDtl !== null && objInvF_InventoryReturnSupDtl !== undefined) {
            Lst_InvF_InventoryReturnSupDtl = objInvF_InventoryReturnSupDtl.Lst_InvF_InventoryReturnSupDtl;
            Lst_InvF_InventoryReturnSupInstLot = objInvF_InventoryReturnSupDtl.Lst_InvF_InventoryReturnSupInstLot;
            Lst_InvF_InventoryReturnSupInstSerial = objInvF_InventoryReturnSupDtl.Lst_InvF_InventoryReturnSupInstSerial;
        }

        //lay thong tin master
        var IF_InvReturnSupNo = $('#IF_InvReturnSupNo').val();
        var InvCodeOut = $('#InvCodeOut').val();
        var CustomerCode = $('#CustomerCode').val();
        var IF_InvInNo = $('#IF_InvInNo').val();
        var TotalValReturnSup = $('#TotalValReturnSup').val();
        var TotalValReturnSupDesc = $('#TotalValReturnSupDesc').val();
        var TotalValReturnSupAfterDesc = $('#TotalValReturnSupAfterDesc').val();
        var Remark = $('#Remark').val();
        objInvF_InventoryReturnSup.IF_InvReturnSupNo = IF_InvReturnSupNo;
        objInvF_InventoryReturnSup.InvCodeOut = InvCodeOut;
        objInvF_InventoryReturnSup.CustomerCode = CustomerCode;
        objInvF_InventoryReturnSup.IF_InvInNo = IF_InvInNo;
        objInvF_InventoryReturnSup.TotalValReturnSup = TotalValReturnSup;
        objInvF_InventoryReturnSup.TotalValReturnSupDesc = TotalValReturnSupDesc;
        objInvF_InventoryReturnSup.TotalValReturnSupAfterDesc = TotalValReturnSupAfterDesc;
        objInvF_InventoryReturnSup.Remark = Remark;    


        var tem = {};
        tem.InvF_InventoryReturnSup = objInvF_InventoryReturnSup;
        tem.Lst_InvF_InventoryReturnSupDtl = Lst_InvF_InventoryReturnSupDtl;
        tem.Lst_InvF_InventoryReturnSupInstLot = Lst_InvF_InventoryReturnSupInstLot;
        tem.Lst_InvF_InventoryReturnSupInstSerial = Lst_InvF_InventoryReturnSupInstSerial;
        tem.FlagRedirect = flagRedirect;
        tem.FlagIsDelete = '0';

        var modelCur = commonUtils.returnJSONValue(tem);

        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    }



    function returnLst_InvF_InventoryReturnSupDtl() {
        debugger
        var objInventoryReturnSupDtl = {};
        var Lst_InvF_InventoryReturnSupDtl = [];
        var Lst_InvF_InventoryReturnSupInstLot = [];
        var Lst_InvF_InventoryReturnSupInstSerial = [];
        

        var listLotCur = [];
        var temDtlCur = {};
        var rowsgetprd = $("tbody#table-tbodyID.GetPrd tr.trdata").length;
        if (rowsgetprd > 0) {
            var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
            if (trArr !== null && trArr !== undefined) {
                debugger
                for (var i = 0; i < trArr.length; i++) {
                    debugger
                    var $trCur = trArr[i];
                    if ($trCur !== null && $trCur !== undefined) {
                        debugger
                        var productCode = $($trCur).attr('productcode');
                        temDtlCur.ProductCode = productCode;



                        var rd = $($trCur).attr('rd');

                        if (productCode !== null && productCode !== undefined) {
                            var productCode = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcode-' + rd).val();
                            var $trcur1 = $('tbody#table-tbodyID').find('tr[productcode="' + productCode + '"]');
                            var $divProducts = $trcur1.find('div.products-list');
                            var $divProductSelected = $divProducts.find('div[productcode="' + productCode + '"]');
                            var productCodeRoot = $divProductSelected.find('input.ProductCodeRoot').val();
                            var productCodeBase = $divProductSelected.find('input.ProductCodeBase').val();
                            debugger
                            var Qty = $('tbody#table-tbodyID.GetPrd tr td').find('input.qty-' + rd).val();
                            var flagLot = $('tbody#table-tbodyID.GetPrd tr td').find('input.flaglot-' + rd).val();
                            var flagSerial = $('tbody#table-tbodyID.GetPrd tr td').find('input.flagserial-' + rd).val();
                            var unitCode = $('tbody#table-tbodyID.GetPrd tr td.td-select2 .select2-' + rd).val();
                            var remark = $('tbody#table-tbodyID.GetPrd tr td').find('textarea.remark-' + rd).val();
                            var upIn = $('tbody#table-tbodyID.GetPrd tr td').find('input.upin-' + rd).val();
                            var upReturnSup = $('tbody#table-tbodyID.GetPrd tr td').find('input.upreturnsup-' + rd).val();
                            var uPinv = $('tbody#table-tbodyID.GetPrd tr td').find('input.upinv-' + rd).val();
                            debugger
                            if (flagLot === '0' && flagSerial === '0') {
                                var $trcurHH = $('#table-detailInvCodeReturnSupActual').find('tr[productcode="' + productCode + '"]');
                                var result = $trcurHH.find('td div.products-list-cache .result[productcode="' + productCode + '"]');
                                //var result = $('#table-detailInvCodeReturnSupActual tr td div.products-list-cache .result[productcode="' + productCode + '"]');
                                if ($trcurHH.length > 0) {
                                    var sumqty = 0.0;
                                    for (var iresult = 0; iresult < result.length; iresult++) {
                                        debugger
                                        var strqty = $(result[iresult]).find('input.Qty').val();
                                        var qty = parseFloat(strqty);
                                        sumqty += qty;
                                        debugger
                                        if (qty > 0) {
                                            //var remark = "";
                                            var objDTL = {
                                                InvCodeOutActual: $(result[iresult]).find('input.InvCodeInActual').val(),
                                                ProductCode: $(result[iresult]).find('input.ProductCode').val(),
                                                Qty: strqty,
                                                UnitCode: unitCode,
                                                Remark: remark,
                                                UPIn: upIn,
                                                UPReturnSup: upReturnSup,
                                                ValReturnSup: qty * parseFloat(upReturnSup),
                                                UPinv: uPinv,
                                            };
                                            Lst_InvF_InventoryReturnSupDtl.push(objDTL)
                                        }
                                    }
                                }

                            }
                            else {
                                if (flagLot === '1') {
                                    debugger
                                    var listVT = [];
                                    var $tr = $('#table-detailLot').find('tr[productcode="' + productCode + '"]');
                                    if ($tr.length > 0) {
                                        $tr.each(function () {
                                            debugger
                                            var tr = $(this);
                                            var idx = $(tr).attr('idx');
                                            //var trdata = $('tbody#table-detailLot').find('tr.trdata[idx = "' + idx + '"]');
                                            var invCodeInActual = tr.find('input.InvCodeInActual').val();
                                            var strQty = tr.find('input.Qty').val();
                                            var productCodecur = tr.find('input.ProductCode').val();
                                            var qty = parseFloat(strQty);
                                            var productLotNoCur = tr.find('input.ProductLotNo').val();
                                            var ProductionDate = tr.find('input.ProductionDate').val();
                                            var ExpiredDate = tr.find('input.ExpiredDate').val();
                                            var QtyTotalOK = tr.find('input.QtyTotalOK').val();
                                            var remark = "";
                                            if (qty > 0) {
                                                var remark = "";
                                                var objLotDtl = {
                                                    InvCodeOutActual: invCodeInActual,
                                                    ProductCode: productCodecur,
                                                    ProductLotNo: productLotNoCur,
                                                    Qty: strQty,
                                                };

                                                var objViTri = {
                                                    InvCodeOutActual: invCodeInActual
                                                }

                                                Lst_InvF_InventoryReturnSupInstLot.push(objLotDtl);
                                                listVT.push(invCodeInActual);

                                            }
                                            debugger



                                        });

                                        debugger
                                        var uniqueNames = [];
                                        $.each(listVT, function (i, el) {
                                            if ($.inArray(el, uniqueNames) === -1) uniqueNames.push(el);
                                        });
                                        for (var j = 0; j < uniqueNames.length; j++) {
                                            debugger
                                            var sumqty = 0.0;
                                            var qtycur = 0.0;
                                            //var remark = "";
                                            var $trcur = $('#table-detailLot').find('tr[invcodeinactual="' + uniqueNames[j] + '"]');
                                            if ($trcur.length > 0) {
                                                $trcur.each(function () {
                                                    var trd = $(this);
                                                    var strqTyCur = trd.find('input.Qty').val();
                                                    qtycur = parseFloat(strqTyCur);
                                                    sumqty += qtycur;

                                                });
                                                debugger
                                                var objLotCur = {
                                                    ProductCode: productCode,
                                                    Qty: sumqty,
                                                    InvCodeOutActual: uniqueNames[j],
                                                    UnitCode: unitCode,
                                                    Remark: remark,
                                                    UPIn: upIn,
                                                    UPReturnSup: upReturnSup,
                                                    ValReturnSup: sumqty * parseFloat(upReturnSup),
                                                    UPinv: uPinv,
                                                };
                                                Lst_InvF_InventoryReturnSupDtl.push(objLotCur)
                                            }
                                        }

                                    }
                                }
                                else if (flagSerial === '1') {
                                    debugger
                                    var listVTSerial = [];
                                    var $tr = $('#table-detailSerial').find('tr[productcode="' + productCode + '"]');
                                    if ($tr.length > 0) {
                                        $tr.each(function () {
                                            var tr = $(this);
                                            var ProductCodecur = tr.find('input.ProductCode').val();
                                            var SerialNo = tr.find('input.SerialNo').val();
                                            var InvCodeInActual = tr.find('input.InvCodeInActual').val();

                                            var objSerial = {
                                                ProductCode: ProductCodecur,
                                                InvCodeOutActual: InvCodeInActual,
                                                SerialNo: SerialNo
                                            };
                                            Lst_InvF_InventoryReturnSupInstSerial.push(objSerial);
                                            listVTSerial.push(InvCodeInActual);
                                        });
                                        var uniqueNames = [];
                                        $.each(listVTSerial, function (i, el) {
                                            if ($.inArray(el, uniqueNames) === -1) uniqueNames.push(el);
                                        });

                                        for (k = 0; k < uniqueNames.length; k++) {
                                            debugger
                                            var sumqty = 0.0;
                                            var qtycur = 0.0;
                                            //var remark = "";
                                            var $trcur = $('#table-detailSerial').find('tr[invcodeinactual="' + uniqueNames[k] + '"]');
                                            var objSerialCur = {
                                                ProductCode: productCode,
                                                Qty: $trcur.length,
                                                InvCodeOutActual: uniqueNames[k],
                                                UnitCode: unitCode,
                                                Remark: remark,
                                                UPIn: upIn,
                                                UPReturnSup: upReturnSup,
                                                ValReturnSup: $trcur.length * parseFloat(upReturnSup),
                                                UPinv: uPinv,
                                            };
                                            Lst_InvF_InventoryReturnSupDtl.push(objSerialCur)
                                        }

                                    }
                                }



                            }
                           
                        }
                    }
                }
            }
            objInventoryReturnSupDtl.Lst_InvF_InventoryReturnSupDtl = Lst_InvF_InventoryReturnSupDtl;
            objInventoryReturnSupDtl.Lst_InvF_InventoryReturnSupInstLot = Lst_InvF_InventoryReturnSupInstLot;
            objInventoryReturnSupDtl.Lst_InvF_InventoryReturnSupInstSerial = Lst_InvF_InventoryReturnSupInstSerial;
        }
        return objInventoryReturnSupDtl;

    }

    this.saveData = function (flagRedirect) {
        debugger
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
    }


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

    this.doSearch = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        doSearchData(_ajaxSettings);
    }

    function doSearchData(_ajaxSettings) {
        debugger
        var IF_InvReturnSupNo = commonUtils.returnValueText('#IF_InvReturnSupNo');
        var CreateDTimeFrom = commonUtils.returnValueText('#CreateDTimeFrom');
        var CreateDTimeTo = commonUtils.returnValueText('#CreateDTimeTo');
        var ApprDTimeFrom = commonUtils.returnValueText('#ApprDTimeFrom');
        var ApprDTimeTo = commonUtils.returnValueText('#ApprDTimeTo');
        var InvCodeOut = commonUtils.returnValueText('#InvCodeOut');
        var CustomerCode = commonUtils.returnValueText('#CustomerCode');
        var OrgID = commonUtils.returnValueText('#OrgID');
        var chkPending = commonUtils.returnValueCheckBox('#chk-pending');
        var chkApproved = commonUtils.returnValueCheckBox('#chk-approved');
        var chkCanceled = commonUtils.returnValueCheckBox('#chk-canceled');
        var recordcount = commonUtils.returnValueText('#recordcount');
        var pagecur = commonUtils.returnValueText('#page');
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var init = commonUtils.returnValueText('#init');

        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token,
                if_invreturnsupno: IF_InvReturnSupNo,
                createdtimefrom: CreateDTimeFrom,
                createdtimeto: CreateDTimeTo,
                approvedtimefrom: ApprDTimeFrom,
                approvedtimeto: ApprDTimeTo,
                invcodeout: InvCodeOut,
                customercode: CustomerCode,
                orgid: OrgID,
                chkpending: chkPending,
                chkapproved: chkApproved,
                chkcanceled: chkCanceled,
                page: pagecur,
                recordcount: recordcount,
                init: init,
            },
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionLoadDoSearch(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
        }).always(function () {
        });
    }

    function doneFunctionLoadDoSearch(objResult) {
        if (objResult.Success) {
            $('#BindDataInvFInventoryReturnSup').html('');
            $('#BindDataInvFInventoryReturnSup').html(objResult.Html);
            initDefault();
            //$('.numberic').number(true, 2);
             var tableName = 'InvF_InventoryReturnSup';
             var TotalValReturnSupAfterDescFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'TotalValReturnSupAfterDesc');
            $('.numberic').number(true, TotalValReturnSupAfterDescFormat);
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    }

    this.approveData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        Approve(_ajaxSettings);

    }

    function Approve(_ajaxSettings) {
        debugger
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Chưa chọn phiếu trả hàng cần duyệt!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            debugger
            var listInvFInventoryReturnSup = [];
            var hasErr = false;
            $(checkedItem).each(function () {
                debugger
                iF_InvInNo = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvReturnSupNo"]').val();
                iF_InvInStatus = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_ReturnSupStatus"]').val();
                if (iF_InvInStatus !== 'PENDING') {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu trả hàng ' + '' + iF_InvInNo + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    hasErr = true;
                    return false;
                }
                var objDel = {};
                objDel.IF_InvReturnSupNo = iF_InvInNo;
                objDel.Remark = '';
                listInvFInventoryReturnSup.push(objDel);
            });
            if (hasErr) {
                return false;
            }
            if (listInvFInventoryReturnSup.length > 0) {
                debugger
                bootbox.confirm({
                    title: '<i class=\"fas fa-info-circle\"></i> THÔNG BÁO',
                    message: 'Đồng ý duyệt danh sách phiếu trả hàng này?',
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
                        debugger
                        var token = $('input[name=__RequestVerificationToken]').val();
                        var dataInput = {
                            objlistinvf_inventoryreturnsup: commonUtils.returnJSONValue(listInvFInventoryReturnSup),
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
                });
            }

          
        }
    }

    this.cancleData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        Cancel(_ajaxSettings);
    }
    function Cancel(_ajaxSettings) {
        debugger
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Chưa chọn phiếu trả hàng cần huỷ!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            var listInvFInventoryReturnSup = [];
            var hasErr = false;
            $(checkedItem).each(function () {
                iF_InvInNo = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvReturnSupNo"]').val();
                iF_InvInStatus = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_ReturnSupStatus"]').val();
                if (iF_InvInStatus !== 'PENDING') {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu trả hàng ' + '' + iF_InvInNo + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    hasErr = true;
                    return false;
                }


                var objDel = {};
                objDel.IF_InvReturnSupNo = iF_InvInNo;
                objDel.Remark = '';
                listInvFInventoryReturnSup.push(objDel);
            });
            if (hasErr) {
                return false;
            }
            if (listInvFInventoryReturnSup.length > 0) {
                debugger
                bootbox.confirm({
                    title: '<i class=\"fas fa-info-circle\"></i> THÔNG BÁO',
                    message: 'Đồng ý hủy danh sách phiếu trả hàng này?',
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
                                objlistinvf_inventoryreturnsup: commonUtils.returnJSONValue(listInvFInventoryReturnSup),
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
            }
        }
    }

    this.deleteData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        Delete(_ajaxSettings);
    }
    function Delete(_ajaxSettings) {
        debugger
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Chưa chọn phiếu trả hàng cần xoá!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
      
        }
        else {
            var listInvFInventoryReturnSupDel = [];
            var hasErr = false;
            $(checkedItem).each(function () {
                debugger
                iF_InvInNo = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvReturnSupNo"]').val();
                iF_InvInStatus = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_ReturnSupStatus"]').val();
                if (iF_InvInStatus !== 'PENDING') {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu trả hàng ' + '' + iF_InvInNo + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    hasErr = true;
                    return false;
                }
                var objDel = {};
                objDel.IF_InvReturnSupNo = iF_InvInNo;
                objDel.Remark = '';
                listInvFInventoryReturnSupDel.push(objDel);
            });

            if (hasErr) {
                return false;
            }

            if (listInvFInventoryReturnSupDel.length > 0) {
                debugger
                bootbox.confirm({
                    title: '<i class=\"fas fa-info-circle\"></i> THÔNG BÁO',
                    message: 'Đồng ý xoá phiếu trả hàng này?',
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
                                objlistinvf_inventoryreturnsup: commonUtils.returnJSONValue(listInvFInventoryReturnSupDel),
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
            }
        }
    }
    this.epxportData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        ExportData(_ajaxSettings);
    }
    function ExportData(_ajaxSettings) {
        debugger
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Chưa chọn phiếu trả hàng cần xuất excel!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            debugger
            var listInvFInventoryReturnSup = [];
            $(checkedItem).each(function () {
                iF_InvReturnSupNo = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvReturnSupNo"]').val();
                var objSelect = {};
                objSelect.IF_InvReturnSupNo = iF_InvReturnSupNo;
                listInvFInventoryReturnSup.push(objSelect);
            });

            var objCommonExcel = new CommonExcel();
            var ajaxSettings = {};
            ajaxSettings.Type = 'post';
            ajaxSettings.DataType = 'json';
            ajaxSettings.Url = '@Url.Action("Export", "InvFInventoryReturnSup")';
            debugger
            objCommonExcel.ajaxSettingsInit();
            objCommonExcel.ajaxSettings = ajaxSettings;
            objCommonExcel.exportExcelSelected(listInvFInventoryReturnSup);


        }
    }
    this.printData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        Print(_ajaxSettings);
    }
    function printPdf(linkPdf) {
        printJS({
            printable: linkPdf,
            type: 'pdf',
            showModal: true,
            //onPrintDialogClose: () => console.log('The print dialog was closed'),
            //onPdfOpen: () => console.log('Pdf was opened in a new tab due to an incompatible browser')
        })
    }
    function Print(_ajaxSettings) {
        debugger
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Chưa chọn phiếu trả hàng cần xuất in!'
            };
            listError.push(objToastr);
        }
        if (checkedItem.length > 1) {
            var objToastr1 = {
                ToastrType: 'error',
                ToastrMessage: 'Chỉ chọn 1 phiếu trả hàng!'
            };
            listError.push(objToastr1);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            var iF_InvReturnSupNo = '';
            $(checkedItem).each(function () {
                iF_InvReturnSupNo = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvReturnSupNo"]').val();
            });

            $.ajax({
                type: _ajaxSettings.Type,
                data:
                {
                    if_invreturnsupno: iF_InvReturnSupNo
                },
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () { }
            }).done(function (result) {
                debugger
                if (result.Success) {
                    if (result.LinkPDF !== undefined || result.LinkPDF !== '') {
                        printPdf(result.LinkPDF);
                    }
                    else {
                        alert('Không lấy được dữ liệu mẫu in');
                    }
                }
                else {
                    if (result.Messages !== undefined && result.Messages.length > 0) {
                        alert(result.Messages[0]);
                    }
                    if (!commonUtils.isNullOrEmpty(result.Detail)) {
                        showErrorDialog(result.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {

            }).always(function (jqXHROrData, textStatus, jqXHROrErrorThrown) {
                //alert("complete");
            });

        }

    }




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
}