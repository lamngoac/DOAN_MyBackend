function InvF_InvAudit() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.approveData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        approve(_ajaxSettings);
    }
    this.checkForm = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeAudit', 'has-error-fix', 'Chưa chọn kho kiểm kê!');

        //check nếu hàng hóa k có trên lưới kiểm kê
        if ($('tbody#table-tbodyID tr.trdata').length <= 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không tồn tại hàng hóa trong phiếu kiểm kê!'
            };
            listError.push(objToastr);
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.checkFormAction = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeAudit', 'has-error-fix', 'Chưa chọn kho kiểm kê!');

        //check nếu hàng hóa k có trên lưới kiểm kê
        if ($('tbody#table-tbodyID td input:checked').length <= 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có hàng hóa nào được kiểm chọn!'
            };
            listError.push(objToastr);
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    function approve(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu kiểm kê nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            debugger
            var arrIF_InvAudit = [];
            var listIF_InvAudit = [];
            var stop = false;
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
                var checkbox = $(this);
                var IF_InvAudit = checkbox.attr('IF_InvAudNo');
                var status = checkbox.attr('status');
                if (status !== "PENDING") {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu kiểm kê ' + '' + IF_InvAudit + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    stop = true;
                    return false;
                }
                var $tr = $(this).parent().parent();
                if_InvAudNo = commonUtils.returnValue($tr.find('input[type="hidden"][name="IF_InvAudNo"]').val());
                remark = commonUtils.returnValue($tr.find('input[type="hidden"][name="Remark"]').val());
                var objInvF_InvAudit = {
                    IF_InvAudNo: if_InvAudNo,
                    Remark: remark,
                };
                listIF_InvAudit.push(objInvF_InvAudit);
                arrIF_InvAudit.push(if_InvAudNo);
            }); if (stop === true) return;
            var arrIFInvAudit = JSON.stringify(arrIF_InvAudit);
            var lstIF_InvAudit = JSON.stringify(listIF_InvAudit);
            bootbox.confirm({
                title: "Thông báo",
                message: "Đồng ý duyệt phiếu kiểm kê " + arrIFInvAudit + " ?",
                buttons: {
                    'cancel': {
                        label: 'Thoát',
                        className: 'btn btn-cancel'
                    },
                    'confirm': {
                        label: 'Đồng ý',
                        className: 'btn btn-nc-button btn-yes'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            url: _ajaxSettings.Url,
                            data: {
                                //IF_InvOutNo: IF_InvOutNo
                                lstIF_InvAudNo: lstIF_InvAudit
                            },
                            type: _ajaxSettings.Type,
                            dataType: _ajaxSettings.DataType,
                            traditional: true,
                            success: function (data) {
                                if (data.Success) {
                                    var listToastr = [];
                                    objToastr = { ToastrType: 'success', ToastrMessage: data.Messages };
                                    listToastr.push(objToastr);
                                    commonUtils.showToastr(listToastr);
                                    
                                    if (data.RedirectUrl !== null) {
                                        window.location.href = data.RedirectUrl;
                                    }
                                } else {
                                    showErrorDialog(data.Detail);
                                }
                            }
                        });
                    }
                }
            });
        }
    }
    this.cancelData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        cancel(_ajaxSettings);

    }

    function cancel(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu kiểm kê nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            debugger
            var arrIF_InvAudit = [];
            var listIF_InvAudit = [];
            var stop = false;
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
                var checkbox = $(this);
                var IF_InvAudit = checkbox.attr('IF_InvAudNo');
                var status = checkbox.attr('status');
                if (status === "FINISH") {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu kiểm kê ' + '' + IF_InvAudit + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    stop = true;
                    return false;
                }
                var $tr = $(this).parent().parent();
                if_InvAudNo = commonUtils.returnValue($tr.find('input[type="hidden"][name="IF_InvAudNo"]').val());
                remark = commonUtils.returnValue($tr.find('input[type="hidden"][name="Remark"]').val());
                var objInvF_InvAudit = {
                    IF_InvAudNo: if_InvAudNo,
                    Remark: remark,
                };
                listIF_InvAudit.push(objInvF_InvAudit);
                arrIF_InvAudit.push(if_InvAudNo);
            }); if (stop === true) return;
            debugger;
            var arrIFInvAudit = JSON.stringify(arrIF_InvAudit);
            var lstIF_InvAudit = JSON.stringify(listIF_InvAudit);
            bootbox.confirm({
                title: "Thông báo",
                message: "Đồng ý hủy phiếu kiểm kê " + arrIFInvAudit + " ?",
                buttons: {
                    'cancel': {
                        label: 'Thoát',
                        className: 'btn btn-cancel'
                    },
                    'confirm': {
                        label: 'Đồng ý',
                        className: 'btn btn-nc-button btn-yes'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            url: _ajaxSettings.Url,
                            data: {
                                //IF_InvOutNo: IF_InvOutNo
                                lstIF_InvAudNo: lstIF_InvAudit
                            },
                            type: _ajaxSettings.Type,
                            dataType: _ajaxSettings.DataType,
                            traditional: true,
                            success: function (data) {
                                if (data.Success) {
                                    var listToastr = [];
                                    objToastr = { ToastrType: 'success', ToastrMessage: data.Messages };
                                    listToastr.push(objToastr);
                                    commonUtils.showToastr(listToastr);

                                    if (data.RedirectUrl !== null) {
                                        window.location.href = data.RedirectUrl;
                                    }
                                } else {
                                    showErrorDialog(data.Detail);
                                }
                            }
                        });
                    }
                }
            });
        }
    }
    this.deleteMulti = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        deletemulti(_ajaxSettings);
    }
    function deletemulti(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu kiểm kê nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            debugger
            var arrIF_InvAudit = [];
            var stop = false;
            var str = "";
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
                debugger
                var checkbox = $(this);
                var IF_InvAudit = checkbox.attr('IF_InvAudNo');
                var InvCodeAudit = checkbox.attr('invcodeaudit');
          
                if (str !== "") {
                    str += "," + IF_InvAudit;
                }
                else {
                    str += IF_InvAudit;
                }
                var status = checkbox.attr('status');
                if (status !== "PENDING") {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu xuất ' + IF_InvAudit + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    stop = true;
                    return false;
                }
                var objInvF_InvAudit = {};
                objInvF_InvAudit.IF_InvAudNo = IF_InvAudit;
                objInvF_InvAudit.InvCodeAudit = InvCodeAudit;   

                arrIF_InvAudit.push(objInvF_InvAudit);
            });
            if (stop === true) return;
            //var lstInvF_InvAudit = JSON.stringify(arrIF_InvAudit);
            //var lstInvF_InvAudit = commonUtils.returnJSONValue(arrIF_InvAudit);
            bootbox.confirm({
                title: "Thông báo",
                message: "Đồng ý xóa phiếu kiểm kê " + str + " ?",
                buttons: {
                    'cancel': {
                        label: 'Thoát',
                        className: 'btn btn-cancel'
                    },
                    'confirm': {
                        label: 'Đồng ý',
                        className: 'btn btn-nc-button btn-yes'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            url: _ajaxSettings.Url,
                            data: {
                                //IF_InvOutNo: IF_InvOutNo
                                lstInvF_InvAudit: commonUtils.returnJSONValue(arrIF_InvAudit)
                            },
                            type: _ajaxSettings.Type,
                            dataType: _ajaxSettings.DataType,
                            traditional: true,
                            success: function (data) {
                                if (data.Success) {
                                    var listToastr = [];
                                    objToastr = { ToastrType: 'success', ToastrMessage: data.Messages };
                                    listToastr.push(objToastr);
                                    commonUtils.showToastr(listToastr);

                                    if (data.RedirectUrl !== null) {
                                        window.location.href = data.RedirectUrl;
                                    }
                                } else {
                                    showErrorDialog(data.Detail);
                                }
                            }
                        });
                    }
                }
            });
        }
    }
    this.Export = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        export1(_ajaxSettings);
    }
    function export1(_ajaxSettings) {
        debugger
        var listError = [];
        var countcheck = $('tbody#table-tbodyID tr.trdata').length;
        if (countcheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Lưới dữ liệu trống!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }

        // Lấy danh sách phiếu 
        var lstInvCodeAudit = [];
        $('#table-tbodyID tr.trdata').each(function () {
            debugger;
            var checkbox = $(this);
            var InvCodeAudit = checkbox.attr('IF_InvAudNo');
            lstInvCodeAudit.push(InvCodeAudit);
        });
        //

        var objlstInvCodeAudit = JSON.stringify(lstInvCodeAudit);
        var formdata = new window.FormData($('#manageForm')[0]);

        formdata.append("lstIF_InvAudNo", objlstInvCodeAudit);

        $.ajax({
            url: _ajaxSettings.Url,
            data: formdata,
            processData: false,
            contentType: false,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.Success) {
                    debugger;
                    if (data !== null) {
                        var listToastr = [];
                        objToastr = { ToastrType: 'success', ToastrMessage: data.Title };
                        listToastr.push(objToastr);
                        commonUtils.showToastr(listToastr);
                        window.location.href = data.strUrl;
                    }
                } else {
                    if (!commonUtils.isNullOrEmpty(data.Title)) {
                        var listToastr = [];
                        objToastr = { ToastrType: 'success', ToastrMessage: data.Title };
                        listToastr.push(objToastr);
                        commonUtils.showToastr(listToastr);
                    }
                    
                    showErrorDialog(data.Detail);
                }
            }
        });
    }
    this.Finish = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        finish(_ajaxSettings);
    }
    function finish() {

        var listError = [];
        var countcheck = $('tbody#table-tbodyID input:checked').length;
        if (countcheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu kiểm kê nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        var remark = commonUtils.returnValueText('#Remark');
        var lstIF_InvAudNo = [];
        var objIF_InvAudNo = {
            IF_InvAudNo: IF_InvAudNo,
            Remark: remark,
        };
        lstIF_InvAudNo.push(objIF_InvAudNo);
        bootbox.confirm({
            title: "Thông báo",
            message: "Đồng ý duyệt phiếu kiểm kê " + lstIF_InvAudNo + " ?",
            buttons: {
                'cancel': {
                    label: 'Thoát',
                    className: 'btn btn-cancel'
                },
                'confirm': {
                    label: 'Đồng ý',
                    className: 'btn btn-nc-button btn-yes'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: _ajaxSettings.Url,
                        data: {
                            //IF_InvOutNo: IF_InvOutNo
                            lstIF_InvAudNo: lstIF_InvAudit
                        },
                        type: _ajaxSettings.Type,
                        dataType: _ajaxSettings.DataType,
                        traditional: true,
                        success: function (data) {
                            if (data.Success) {
                                var listToastr = [];
                                objToastr = { ToastrType: 'success', ToastrMessage: data.Messages };
                                listToastr.push(objToastr);
                                commonUtils.showToastr(listToastr);

                                if (data.RedirectUrl !== null) {
                                    window.location.href = data.RedirectUrl;
                                }
                            } else {
                                showErrorDialog(data.Detail);
                            }
                        }
                    });
                }
            }
        });
    }
    this.Action = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        action(_ajaxSettings);
    }
    this.getData = function (savetype) {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var objRQ_InvF_InvAudit = new Object();//đối tượng request gom dữ liệu

        var objInvF_InvAudit = new Object();//thông tin cơ bản của phiếu
        objInvF_InvAudit.IF_InvAudNo = $('#IFInvAudNo').val();
        objInvF_InvAudit.InvCodeAudit = $('#InvCodeAudit').val();
        objInvF_InvAudit.Remark = $('#Remark').val();
        
        var Lst_InvF_InvAuditDtl = [];//chi tiết phiếu kiểm kê
        if ($('tbody#table-tbodyID tr.trdata').length > 0) {
            $("tbody#table-tbodyID tr.trdata").each(function () {
                var tr = $(this);
                var rd = $(tr).attr('rd');
                var idx = $(tr).attr('idx');
                var flaglo = tr.attr('flaglot');
                var flagserial = tr.attr('flagserial');

                var strUnitCode = tr.find('.select2-' + rd + ' option:selected').val();
                var strProductCode = tr.find('input.productcode-'+rd).val();
                //var strInvCodeInit = tr.find('input').val();
                var strInvCodeActual = tr.find('input.invcode-' + rd).val();

                var strQtyInit = tr.find('input.qtytotalok-'+rd).val();

                if (strInvCodeActual !== null && strInvCodeActual !== undefined) {
                    var lstInvCoce = strInvCodeActual.split(', ');
                    if (lstInvCoce.length > 0) {
                        for (var i = 0; i < lstInvCoce.length; i++) {
                            var invCode = lstInvCoce[i];
                            //Hàng hóa lô
                            if (flaglo === "1") {
                                var trLo = $('tbody#table-detailLot').find('tr[productcode="' + strProductCode + '"][InvCodeActual="' + invCode + '"]');

                                var sumqty = 0.0;
                                if (trLo.length > 0) {
                                    trLo.each(function () {
                                        var trLoByInvCode = $(this);
                                        var idxlo = trLoByInvCode.attr('idx');
                                        var rdlo = trLoByInvCode.attr('rd');
                                        var strQty = trLo.find('input.qtyinit-' + rdlo).val();
                                        if ($.isNumeric(strQty) === true) {
                                            sumqty += parseFloat(strQty);
                                        }
                                    });


                                    InvF_InvAuditDtl = new Object();
                                    InvF_InvAuditDtl.InvCodeInit = invCode;
                                    InvF_InvAuditDtl.InvCodeActual = invCode;
                                    InvF_InvAuditDtl.ProductCode = strProductCode;
                                    InvF_InvAuditDtl.QtyInit = sumqty;//strQty;
                                    InvF_InvAuditDtl.UnitCode = strUnitCode;
                                    Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                                }
                            }
                            // Thông tin serial
                            else if (flagserial === "1") {
                                var trSerial = $('tbody#table-detailSerial').find('tr[productcode="' + strProductCode + '"][InvCodeActual="' + invCode + '"]');
                                if (trSerial.length > 0) {
                                    var strQtySerial = trSerial.length;
                                    InvF_InvAuditDtl = new Object();
                                    InvF_InvAuditDtl.InvCodeInit = invCode;
                                    InvF_InvAuditDtl.InvCodeActual = invCode;
                                    InvF_InvAuditDtl.ProductCode = strProductCode;
                                    InvF_InvAuditDtl.QtyInit = strQtySerial;
                                    InvF_InvAuditDtl.UnitCode = strUnitCode;
                                    Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                                }
                            }
                            else {
                                var trDtl = $("tbody#table-detailCombo tr.trdata[productcode='" + strProductCode + "'][invcode='" + invCode + "']");
                                trDtl.each(function () {
                                    var rowDtl = $(this);
                                    var rdy = rowDtl.attr('rd');
                                    strQtyInit = rowDtl.find('input.qty-' + rdy).val();
                                    InvF_InvAuditDtl = new Object();
                                    InvF_InvAuditDtl.InvCodeInit = invCode;
                                    InvF_InvAuditDtl.InvCodeActual = invCode;
                                    InvF_InvAuditDtl.ProductCode = strProductCode;
                                    InvF_InvAuditDtl.QtyInit = strQtyInit;
                                    InvF_InvAuditDtl.UnitCode = strUnitCode;
                                    Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                                });
                            }
                        }
                    }
                }
            });
            
            // Thông tin lô
            var Lst_InvF_InvAuditInstLot = [];
            if ($('tbody#table-detailLot').length > 0) {
                $("tbody#table-detailLot tr.trdata").each(function () {
                    var trLo = $(this);
                    var idlo = $(trLo).attr('idx');
                    var rdl = $(trLo).attr('rd');

                    var objInvF_InvAuditInstLot = new Object();

                    var InvCodeInit = trLo.find('input.invcodeinit-' + rdl).val();
                    var InvCodeActual = trLo.find('input.invcodeactual-' + rdl).val();
                    var ProductCode = trLo.find('input.productcode-' + rdl).val();
                    var ProductLotNo = trLo.find('input.productlotno-' + rdl).val();
                    var QtyInit = trLo.find('input.qtyinit-' + rdl).val();
                    var ProductionDate = trLo.find('input.productiondate-' + rdl).val();
                    var ExpiredDate = trLo.find('input.expireddate-' + rdl).val();

                    objInvF_InvAuditInstLot.InvCodeInit = InvCodeInit;
                    objInvF_InvAuditInstLot.InvCodeActual = InvCodeActual;
                    objInvF_InvAuditInstLot.ProductCode = ProductCode;
                    objInvF_InvAuditInstLot.ProductLotNo = ProductLotNo;
                    objInvF_InvAuditInstLot.QtyInit = QtyInit;
                    objInvF_InvAuditInstLot.ProductionDate = ProductionDate;
                    objInvF_InvAuditInstLot.ExpiredDate = ExpiredDate;
                    Lst_InvF_InvAuditInstLot.push(objInvF_InvAuditInstLot);

                });
            }

            //Thông tin Serial
            var Lst_InvF_InvAuditInstSerial = [];
            if ($('tbody#table-detailSerial').length > 0) {
                $("tbody#table-detailSerial tr.trdata").each(function () {
                    var trSerial = $(this);
                    var idserial = $(trSerial).attr('idx');
                    var rds = $(trSerial).attr('rd');

                    var objInvF_InvAuditInstSerial = new Object();

                    var InvCodeInit = trSerial.find('input.invcodeinit-' + rds).val();
                    var InvCodeActual = trSerial.find('input.invcodeactual-' + rds).val();
                    var ProductCode = trSerial.find('input.productcode-' + rds).val();
                    var SerialNo = trSerial.find('input.serialno-' + rds).val();

                    objInvF_InvAuditInstSerial.InvCodeInit = InvCodeInit;
                    objInvF_InvAuditInstSerial.InvCodeActual = InvCodeActual;
                    objInvF_InvAuditInstSerial.ProductCode = ProductCode;
                    objInvF_InvAuditInstSerial.SerialNo = SerialNo;
                    Lst_InvF_InvAuditInstSerial.push(objInvF_InvAuditInstSerial);
                });
            }
            objRQ_InvF_InvAudit.InvF_InvAudit = objInvF_InvAudit;
            objRQ_InvF_InvAudit.Lst_InvF_InvAuditDtl = Lst_InvF_InvAuditDtl;
            objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstLot = Lst_InvF_InvAuditInstLot;
            objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstSerial = Lst_InvF_InvAuditInstSerial;
        }
        var modelCur = JSON.stringify(objRQ_InvF_InvAudit);
        var invBuPartern = $("#InvCodeAudit").find('option:selected').attr("invbupattern");
        var lst_ProductCodeUser = [];
        if ($('tbody#table-tbodyID tr.trdata').length > 0) {
            $("tbody#table-tbodyID tr.trdata").each(function () {
                var tr = $(this);
                var idx = $(tr).attr('idx');
                var strProductCodeUser = tr.attr("productcodeuser");
                if (lst_ProductCodeUser.includes(strProductCodeUser) === false) {
                    lst_ProductCodeUser.push(strProductCodeUser);
                }
            });
        }
        
        var data = {
            model: modelCur,
            savetype: savetype,
            invBUPattern: invBuPartern,
            lst_ProductCodeUser: JSON.stringify(lst_ProductCodeUser)
        };
        return data;
    };
    this.saveData = function (savetype) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData(savetype);
        if (this.checkForm()) {
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                if (objResult.Success) {
                    var listToastr = [];
                    objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages };
                    listToastr.push(objToastr);
                    commonUtils.showToastr(listToastr);
                    
                    if (savetype == "saveandcreate") {
                        var redirectUrl = commonUtils.returnValue(objResult.RedirectUrl);
                        if (!commonUtils.isNullOrEmpty(redirectUrl)) {
                            window.location.href = redirectUrl;
                        }
                    }
                    else {
                        if (objResult.RedirectUrl !== null) {
                            window.location.href = objResult.RedirectUrl;
                        }
                    }
                } else {
                    showErrorDialog(objResult.Detail);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                //failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                //alwaysFunction();
            });
        }

    };

    this.getDataSaveAction = function (savetype) {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var objRQ_InvF_InvAudit = new Object();


        var objInvF_InvAudit = new Object();
        objInvF_InvAudit.IF_InvAudNo = $('#IFInvAudNo').val();
        objInvF_InvAudit.InvCodeAudit = $('#InvCodeAudit').val();
        objInvF_InvAudit.Remark = $('#Remark').val();
        // Thông tin org và network bổ sung sau

        var check = true;

        var Lst_InvF_InvAuditDtl = [];
        var Lst_InvF_InvAuditInstLot = [];
        var Lst_InvF_InvAuditInstSerial = [];
        
        if ($('tbody#table-tbodyID td input:checked').length > 0) {
            $("tbody#table-tbodyID tr.trdata td input:checked").each(function () {
                var tr = $(this).parents('tr');
                var idx = $(tr).attr('idx');
                var rd = $(tr).attr('rd');
                debugger;
                var flaglo = tr.attr('flaglot');
                var flagserial = tr.attr('flagserial');

                var selectdonvi = tr.find('select.unitcode-'+rd);

                var strProductCode = tr.find('input.productcode-'+rd).val();
                var strInvCodeInit = tr.find('input.invcodeinit'+rd).val();
                var strInvCodeActual = tr.find('input.invcode-'+rd).val();
                var strUnitCode = $(selectdonvi).val();
                var strQtyInit = tr.find('input.qtytotalok-'+rd).val();
                var strQtyActual = tr.find('input.qtyactual-'+rd).val();
                var strFlagExist = tr.find('input.flagexist-'+rd).val();
                debugger;
                if (strInvCodeActual !== null && strInvCodeActual !== undefined) {
                    var lstInvCoce = strInvCodeActual.split(', ');
                    lstInvCoce.push('');//Trường hợp Không có lô thực tế
                    if (lstInvCoce.length > 0) {
                        for (var i = 0; i < lstInvCoce.length; i++) {
                            var invCode = lstInvCoce[i];

                            //var countLoDtl = $('tbody#table-detailLot').length;
                            //if (countLoDtl)
                            if (flaglo === "1") {
                                var trLo = $('tbody#table-detailLot').find('tr[productcode="' + strProductCode + '"][InvCodeActual="' + invCode + '"]');
                                if (trLo.length > 0) {
                                    var idxlo = trLo.attr('idx');
                                    var sumQtyInit = 0.0;
                                    var sumQtyActual = 0.0;
                                    var InvCodeInit = '';

                                    // Chỉ lấy các lot theo sản phẩm được chọn
                                    trLo.each(function () {

                                        var tr_Lo = $(this);
                                        var idlo = $(tr_Lo).attr('idx');
                                        var rdl = $(tr_Lo).attr('rd');
                                        var objInvF_InvAuditInstLot = new Object();

                                        InvCodeInit = tr_Lo.find('input.invcodeinit-' + rdl).val();
                                        var InvCodeActual = tr_Lo.find('input.invcodeactual-' + rdl).val();
                                        var ProductCode = tr_Lo.find('input.productcode-' + rdl).val();
                                        var ProductLotNo = tr_Lo.find('input.productlotno-' + rdl).val();
                                        var QtyInit = tr_Lo.find('input.qtyinit-' + rdl).val();
                                        var QtyActual = tr_Lo.find('input.qtytt-' + rdl).val();
                                        var ProductionDate = tr_Lo.find('input.productiondate-' + rdl).val();
                                        var ExpiredDate = tr_Lo.find('input.expireddate-' + rdl).val();
                                        var flagExistLo = tr_Lo.find('input.flagexist-' + rdl).val();

                                        // Định dạng số
                                        if ($.isNumeric(QtyInit)) {
                                            sumQtyInit += parseFloat(QtyInit);
                                        }

                                        if ($.isNumeric(QtyActual)) {
                                            sumQtyActual += parseFloat(QtyActual);
                                        }
                                        //

                                        objInvF_InvAuditInstLot.InvCodeInit = InvCodeInit;
                                        objInvF_InvAuditInstLot.InvCodeActual = InvCodeActual;
                                        objInvF_InvAuditInstLot.ProductCode = ProductCode;
                                        objInvF_InvAuditInstLot.ProductLotNo = ProductLotNo;
                                        objInvF_InvAuditInstLot.QtyInit = QtyInit;
                                        objInvF_InvAuditInstLot.QtyActual = QtyActual;
                                        objInvF_InvAuditInstLot.ProductionDate = ProductionDate;
                                        objInvF_InvAuditInstLot.ExpiredDate = ExpiredDate;
                                        objInvF_InvAuditInstLot.FlagExist = flagExistLo;
                                        Lst_InvF_InvAuditInstLot.push(objInvF_InvAuditInstLot);
                                        
                                    });
                                    //

                                    // Chi tiết Dtl
                                    InvF_InvAuditDtl = new Object();
                                    InvF_InvAuditDtl.InvCodeInit = InvCodeInit;
                                    InvF_InvAuditDtl.InvCodeActual = invCode;
                                    InvF_InvAuditDtl.ProductCode = strProductCode;
                                    InvF_InvAuditDtl.QtyInit = sumQtyInit;
                                    InvF_InvAuditDtl.QtyActual = sumQtyActual;
                                    InvF_InvAuditDtl.UnitCode = strUnitCode;
                                    InvF_InvAuditDtl.FlagExist = strFlagExist;

                                    Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                                    //
                                }
                            }
                            //

                            // Thông tin serial

                            else if (flagserial === "1") {
                                var trSerial = $('tbody#table-detailSerial').find('tr[productcode="' + strProductCode + '"][invcodeactual="' + invCode + '"]');
                                if (trSerial.length > 0) {

                                    var qtyInit = 0;
                                    trSerial.each(function () {
                                        var tr_Serial = $(this);
                                        var idserial = $(tr_Serial).attr('idx');
                                        var rds = $(tr_Serial).attr('rd');
                                        var flagExist = tr_Serial.find('input.flagexist-'+rds).val();
                                        if (flagExist !== undefined && flagExist === '1') {
                                            qtyInit++;
                                        }
                                    });

                                    trSerial.each(function () {
                                        var qtyDtl = 1;

                                        var tr_Serial = $(this);
                                        var idserial = $(tr_Serial).attr('idx');
                                        var rds = $(tr_Serial).attr('rd');
                                        var objInvF_InvAuditInstSerial = new Object();

                                        var InvCodeInit = tr_Serial.find('input.invcodeinit-'+rds).val();
                                        var InvCodeActual = tr_Serial.find('input.invcodeactual-' + rds).val();
                                        var ProductCode = tr_Serial.find('input.productcode-' + rds).val();
                                        var SerialNo = tr_Serial.find('input.serialno-' + rds).val();

                                        var ProductCodeActual = tr_Serial.find('input.productcodeactual-' + rds).val();
                                        var SerialNoActual = tr_Serial.find('input.serialnoactual-' + rds).val();
                                        var FlagExistSerial = tr_Serial.find('input.flagexist-' + rds).val();
                                        var InventoryAction = tr_Serial.find('input.inventoryaction-' + rds).val();

                                        if (InventoryAction == "OUT") {
                                            //strQtySerial -= 1;
                                            qtyDtl = 0;
                                        }

                                        objInvF_InvAuditInstSerial.InvCodeInit = InvCodeInit;
                                        objInvF_InvAuditInstSerial.InvCodeActual = InvCodeActual;
                                        objInvF_InvAuditInstSerial.ProductCode = ProductCode;
                                        objInvF_InvAuditInstSerial.SerialNo = SerialNo;
                                        objInvF_InvAuditInstSerial.FlagExist = FlagExistSerial;
                                        objInvF_InvAuditInstSerial.InventoryAction = InventoryAction;

                                        Lst_InvF_InvAuditInstSerial.push(objInvF_InvAuditInstSerial);

                                        // sửa lại
                                        InvF_InvAuditDtl = new Object();
                                        InvF_InvAuditDtl.InvCodeInit = InvCodeInit;
                                        InvF_InvAuditDtl.InvCodeActual = InvCodeActual;
                                        InvF_InvAuditDtl.ProductCode = strProductCode;
                                        InvF_InvAuditDtl.QtyInit = qtyInit;
                                        InvF_InvAuditDtl.QtyActual = qtyDtl;
                                        InvF_InvAuditDtl.UnitCode = strUnitCode;
                                        InvF_InvAuditDtl.FlagExist = strFlagExist;
                                        //Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                                        AddOrChangeQtyDtl(Lst_InvF_InvAuditDtl, InvF_InvAuditDtl);
                                        //Add list Dtl
                                    });
                                }
                            }
                            else {
                                // Hàng hóa không quản lý lot var serial   
                                debugger;
                                var trDtl = $('tbody#table-detailCombo').find('tr[productcode="' + strProductCode + '"][invcode="' + invCode + '"]');
                                if (trDtl.length > 0) {
                                    trDtl.each(function () {
                                        var trDtl = $(this);
                                        var idxDtl = trDtl.attr('idx');
                                        var rdtl = trDtl.attr('rd');
                                        var rowDtl = $(this);
                                        var rdy = rowDtl.attr('rd');
                                        //strQtyInit = rowDtl.find('input.qtyInit-' + rdy).val();
                                        var invCodeActual = rowDtl.find('input.invcodeoutactual-' + rdy).val();
                                        var invCodeInit = rowDtl.find('input.invcodeinit-' + rdy).val();
                                        InvF_InvAuditDtl = new Object();
                                        InvF_InvAuditDtl.InvCodeInit = invCodeInit;
                                        InvF_InvAuditDtl.InvCodeActual = invCodeActual;
                                        InvF_InvAuditDtl.ProductCode = strProductCode;
                                        InvF_InvAuditDtl.QtyInit = strQtyInit;
                                        InvF_InvAuditDtl.QtyActual = strQtyActual;
                                        InvF_InvAuditDtl.UnitCode = strUnitCode;
                                        Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                                    });

                                }
                            }
                            //
                        }
                    }
                }

                if (check === false) return;
            });

            if (check === false) return;
            objRQ_InvF_InvAudit.InvF_InvAudit = objInvF_InvAudit;
            objRQ_InvF_InvAudit.Lst_InvF_InvAuditDtl = Lst_InvF_InvAuditDtl;
            objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstLot = Lst_InvF_InvAuditInstLot;
            objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstSerial = Lst_InvF_InvAuditInstSerial;
        }
        var objJson = JSON.stringify(objRQ_InvF_InvAudit);
        var data = {
            model: objJson,
            savetype: savetype,
        };
        return data;
    };
    this.saveAction = function (savetype) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataSaveAction(savetype);
        if (this.checkFormAction()) {
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                if (objResult.Success) {
                    var listToastr = [];
                    objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages };
                    listToastr.push(objToastr);
                    commonUtils.showToastr(listToastr);

                    if (savetype == "saveandcreate") {
                        var redirectUrl = commonUtils.returnValue(objResult.RedirectUrl);
                        if (!commonUtils.isNullOrEmpty(redirectUrl)) {
                            window.location.href = redirectUrl;
                        }
                    }
                    else {
                        if (objResult.RedirectUrl !== null) {
                            window.location.href = objResult.RedirectUrl;
                        }
                    }
                } else {
                    showErrorDialog(objResult.Detail);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                //failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                //alwaysFunction();
            });
        }

    };
    function action(_ajaxSettings) {
        debugger;
        var listToastr = [];
        var countcheck = $('tbody#table-tbodyID input:checked').length;
        if (countcheck === 0) {
            objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu kiểm kê nào được chọn!'
            };
            listToastr.push(objToastr);
        }
        else if (countcheck > 1) {
            objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Chỉ chọn 1 phiếu kiểm kê để thực hiện!'
            };
            listToastr.push(objToastr);
        }
        var chk = $('tbody#table-tbodyID input:checked');
        var status = chk.attr("status");
        var IF_InvAudNo = chk.attr('IF_InvAudNo');

        if (!commonUtils.isNullOrEmpty(status) && status !== "APPROVE") {
            objToastr = {
                ToastrType: 'error',
                ToastrMessage: "Trạng thái phiếu kiểm kê " + IF_InvAudNo + " không hợp lệ!"
            };
            listToastr.push(objToastr);
        }


        if (listToastr !== undefined && listToastr !== null && listToastr.length > 0) {
            commonUtils.showToastr(listToastr);
            return false;
        }
        var IF_InvAudNo = chk.attr('IF_InvAudNo');
        var url = _ajaxSettings.Url + "?IF_InvAudNo=" + IF_InvAudNo;
        window.location.href = url;
    }
    function AddOrChangeQtyDtl(Lst_InvF_InvAuditDtl, objInvF_InvAuditDtl) {
        debugger;
        if (Lst_InvF_InvAuditDtl.length === 0) {
            Lst_InvF_InvAuditDtl.push(objInvF_InvAuditDtl);
            return Lst_InvF_InvAuditDtl;
        }
        var checkexist = false;
        for (var i = 0; i < Lst_InvF_InvAuditDtl.length; i++) {
            var it = Lst_InvF_InvAuditDtl[i];
            var InvCodeInit = objInvF_InvAuditDtl.InvCodeInit;
            var InvCodeActual = objInvF_InvAuditDtl.InvCodeActual;
            var ProductCode = objInvF_InvAuditDtl.ProductCode;
            var qty = parseFloat(objInvF_InvAuditDtl.QtyActual);
            if (it.InvCodeInit === InvCodeInit && it.InvCodeActual === InvCodeActual && it.ProductCode === ProductCode) {
                qty = parseFloat(it.QtyActual) + qty;
                Lst_InvF_InvAuditDtl[i].QtyActual = qty;
                checkexist = true;
                return;
            }
        }
        if (checkexist === false) {
            Lst_InvF_InvAuditDtl.push(objInvF_InvAuditDtl);
        }
        return Lst_InvF_InvAuditDtl;
    }
    this.finishData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        finish(_ajaxSettings);
    }
    function finish(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu kiểm kê nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            debugger
            var arrIF_InvAudit = [];
            var listIF_InvAudit = [];
            var stop = false;
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
                var checkbox = $(this);
                var IF_InvAudit = checkbox.attr('IF_InvAudNo');
                var status = checkbox.attr('status');
                if (status !== "APPROVE") {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu kiểm kê ' + '' + IF_InvAudit + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    stop = true;
                    return false;
                }
                var $tr = $(this).parent().parent();
                if_InvAudNo = commonUtils.returnValue($tr.find('input[type="hidden"][name="IF_InvAudNo"]').val());
                remark = commonUtils.returnValue($tr.find('input[type="hidden"][name="Remark"]').val());
                var objInvF_InvAudit = {
                    IF_InvAudNo: if_InvAudNo,
                    Remark: remark,
                };
                listIF_InvAudit.push(objInvF_InvAudit);
                arrIF_InvAudit.push(if_InvAudNo);
            }); if (stop === true) return;
            var arrIFInvAudit = JSON.stringify(arrIF_InvAudit);
            var lstIF_InvAudit = JSON.stringify(listIF_InvAudit);
            bootbox.confirm({
                title: "Thông báo",
                message: "Đồng ý duyệt phiếu sau kiểm kê " + arrIFInvAudit + " ?",
                buttons: {
                    'cancel': {
                        label: 'Thoát',
                        className: 'btn btn-cancel'
                    },
                    'confirm': {
                        label: 'Đồng ý',
                        className: 'btn btn-nc-button btn-yes'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            url: _ajaxSettings.Url,
                            data: {
                                //IF_InvOutNo: IF_InvOutNo
                                lstIF_InvAudNo: lstIF_InvAudit
                            },
                            type: _ajaxSettings.Type,
                            dataType: _ajaxSettings.DataType,
                            traditional: true,
                            success: function (data) {
                                if (data.Success) {
                                    var listToastr = [];
                                    objToastr = { ToastrType: 'success', ToastrMessage: data.Messages };
                                    listToastr.push(objToastr);
                                    commonUtils.showToastr(listToastr);

                                    if (data.RedirectUrl !== null) {
                                        window.location.href = data.RedirectUrl;
                                    }
                                } else {
                                    showErrorDialog(data.Detail);
                                }
                            }
                        });
                    }
                }
            });
        }
    }
}