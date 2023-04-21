function AddProductToList(thiz, urlSearch, colortext) {
    //'@Url.Action("GetProductSearch", "ModalCommon")'
    debugger;
    var productcode = $(thiz).val();
    if (productcode === "Search") {
        $.ajax({
            url: urlSearch,
            data: {
                productkey: "",
                showview: "1"
            },
            type: 'post',
            dataType: 'json',
            traditional: true,
            success: function (data) {
                if (data.Success) {
                    $('#ShowPopupProduct').modal({
                        backdrop: false,
                        keyboard: true
                    });
                    $("#ShowPopupProduct").html(data.Html);
                    var display = $("#ShowPopupProduct").css('display');
                    if (display === "none") {
                        $("#ShowPopupProduct").show();
                    }
                } else {
                    showErrorDialog(data.Detail);
                }
            }
        });
    }
    else {
        if (colortext !== undefined && colortext !== "") {            
            AddProduct($('#rowtemplateProductAddDaKiem'), $('#table-tbodyID'), urlSearch, colortext);
        }
        else {
            AddProduct($('#rowtemplateProduct'), $('#table-tbodyID'), urlSearch, colortext);
        }        
    }
}


function Search(urlSearch) {
    $('#manageForm').attr('action', urlSearch).submit();
    return;
}



// Tận dụng hàm cũ
function TongTien() {
    var sumqtyTotakOk = 0.0;
    //Tính tổng tồn kho
    $("tbody#table-tbodyID tr.trdata").each(function () {
        var tr = $(this);
        var idx = $(tr).attr('idx');
        var strQtyTotalOk = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].QtyInit"]').val();
        var qtytotalok = 0.0;
        if ($.isNumeric(strQtyTotalOk) === true) {
            qtytotalok = parseFloat(strQtyTotalOk);
            sumqtyTotakOk += qtytotalok;
        }
    });
    // Gán vào td tfoot
    $('#tdSumQtyTotal').text(sumqtyTotakOk);
}

function CheckForm() {
    if (!commonUtils.checkElementIsNullOrEmpty('#InvCodeAudit', 'has-error-fix', 'Kho kiểm kê không được trống!')) {
        return false;
    }   
    return true;
}

function SaveData(urlSave, savetype) {
    debugger;
    if (CheckForm() === false) return;
    var objRQ_InvF_InvAudit = new Object();


    var objInvF_InvAudit = new Object();
    objInvF_InvAudit.IF_InvAudNo = $('#IFInvAudNo').val();
    objInvF_InvAudit.InvCodeAudit = $('#InvCodeAudit').val();
    objInvF_InvAudit.Remark = $('#Remark').val();
    // Thông tin org và network bổ sung sau

    //var check = true;

    var Lst_InvF_InvAuditDtl = [];
    if ($('tbody#table-tbodyID tr.trdata').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');            
            var flaglo = tr.attr('flaglot');           
            var flagserial = tr.attr('flagserial');

            var selectdonvi = tr.find('select[name="Lst_InvF_InvAuditDtl[' + idx + '].UnitCode"]');            
            var strProductCode = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].ProductCode"]').val();
            var strInvCodeInit = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].InvCodeInit"]').val();
            var strInvCodeActual = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].InvCodeActual"]').val();            
            var strUnitCode = $(selectdonvi).val();
            var strQtyInit = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].QtyInit"]').val();
           
            if (strInvCodeActual !== null && strInvCodeActual !== undefined) {
                var lstInvCoce = strInvCodeActual.split(', ');
                if (lstInvCoce.length > 0) {
                    for (var i = 0; i < lstInvCoce.length; i++) {
                        var invCode = lstInvCoce[i];

                        //var countLoDtl = $('tbody#table-detailLot').length;
                        //if (countLoDtl)
                        if (flaglo === "1") {
                            var trLo = $('tbody#table-detailLot').find('tr[productcode="' + strProductCode + '"][InvCodeActual="' + invCode + '"]');
                            
                            var sumqty = 0.0;
                            if (trLo.length > 0) {
                                trLo.each(function () {
                                    var trLoByInvCode = $(this);
                                    var idxlo = trLoByInvCode.attr('idx');
                                    var strQty = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idxlo + '].QtyInit"]').val();
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
                        //

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
                                var y = rowDtl.attr('idx');
                                strQtyInit = rowDtl.find('input[name="Lst_InvF_InventoryOutComboDtl[' + y + '].Qty"]').val();                                
                                InvF_InvAuditDtl = new Object();
                                InvF_InvAuditDtl.InvCodeInit = invCode;
                                InvF_InvAuditDtl.InvCodeActual = invCode;
                                InvF_InvAuditDtl.ProductCode = strProductCode;
                                InvF_InvAuditDtl.QtyInit = strQtyInit;
                                InvF_InvAuditDtl.UnitCode = strUnitCode;
                                Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                            });                          
                        }
                        //
                    }
                }
                //else {
                //    InvF_InvAuditDtl = new Object();
                //    InvF_InvAuditDtl.InvCodeInit = strInvCodeInit;
                //    InvF_InvAuditDtl.InvCodeActual = strInvCodeActual;
                //    InvF_InvAuditDtl.ProductCode = strProductCode;
                //    InvF_InvAuditDtl.QtyInit = strQtyInit;
                //    InvF_InvAuditDtl.UnitCode = strUnitCode;
                //    Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                //}
            }  

            //if (check === false) return;
        });

        //if (check === false) return;
        // Thông tin lô
        var Lst_InvF_InvAuditInstLot = [];
        if ($('tbody#table-detailLot').length > 0) {
            $("tbody#table-detailLot tr.trdata").each(function () {
                var trLo = $(this);
                var idlo = $(trLo).attr('idx');

                var objInvF_InvAuditInstLot = new Object();

                var InvCodeInit = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].InvCodeInit"]').val();
                var InvCodeActual = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].InvCodeActual"]').val();
                var ProductCode = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ProductCode"]').val();
                var ProductLotNo = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ProductLotNo"]').val();
                var QtyInit = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].QtyInit"]').val();
                var ProductionDate = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ProductionDate"]').val();
                var ExpiredDate = trLo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ExpiredDate"]').val();

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
        //

        //Thông tin Serial
        var Lst_InvF_InvAuditInstSerial = [];
        if ($('tbody#table-detailSerial').length > 0) {
            $("tbody#table-detailSerial tr.trdata").each(function () {
                var trSerial = $(this);
                var idserial = $(trSerial).attr('idx');

                var objInvF_InvAuditInstSerial = new Object();

                var InvCodeInit = trSerial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].InvCodeInit"]').val();
                var InvCodeActual = trSerial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].InvCodeActual"]').val();
                var ProductCode = trSerial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].ProductCode"]').val();
                var SerialNo = trSerial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].SerialNo"]').val();                

                objInvF_InvAuditInstSerial.InvCodeInit = InvCodeInit;
                objInvF_InvAuditInstSerial.InvCodeActual = InvCodeActual;
                objInvF_InvAuditInstSerial.ProductCode = ProductCode;
                objInvF_InvAuditInstSerial.SerialNo = SerialNo;               
                Lst_InvF_InvAuditInstSerial.push(objInvF_InvAuditInstSerial);
            });
        }
        //

        

        objRQ_InvF_InvAudit.InvF_InvAudit = objInvF_InvAudit;
        objRQ_InvF_InvAudit.Lst_InvF_InvAuditDtl = Lst_InvF_InvAuditDtl;
        objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstLot = Lst_InvF_InvAuditInstLot;
        objRQ_InvF_InvAudit.Lst_InvF_InvAuditInstSerial = Lst_InvF_InvAuditInstSerial;
    }
    else {
        alert("Không tồn tại hàng hóa trong phiếu kiểm kê");
        return;
    }

    var objJson = JSON.stringify(objRQ_InvF_InvAudit);
    var invBuPartern = $("#InvCodeAudit").find('option:selected').attr("invbupattern");
    //var lstProductCode = [];
    var lst_ProductCodeUser = [];
    //var lstUnitCode_Product = [];
    if ($('tbody#table-tbodyID tr.trdata').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');    
            //var strProductCode = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].ProductCode"]').val();
            //if (lstProductCode.includes(strProductCode) === false) {
            //    lstProductCode.push(strProductCode);
            //}

            //var strunitCode = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].UnitCode"]').val();
            //if (lstUnitCode_Product.includes(strunitCode) === false) {
            //    lstUnitCode_Product.push(strunitCode);
            //}
            var strProductCodeUser = tr.attr("productcodeuser");//tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].ProductCodeUser"]').val();
            if (lst_ProductCodeUser.includes(strProductCodeUser) === false) {
                lst_ProductCodeUser.push(strProductCodeUser);
            }
        });
    }

    $.ajax({
        url: urlSave,
        data: {
            model: objJson,
            //flagIsDelete: flagIsDelete,
            savetype: savetype,
            invBUPattern: invBuPartern,
            lst_ProductCodeUser: JSON.stringify(lst_ProductCodeUser)
            //lst_ProductCode: JSON.stringify(lstProductCode),
            //lstUnitCode_Product: JSON.stringify(lstUnitCode_Product)
        },
        type: 'post',
        dataType: 'json',
        traditional: true,
        success: function (data) {
            if (data.Success) {
                alert(data.Messages);
                if (savetype == "saveandcreate") {
                    var redirectUrl = commonUtils.returnValue(data.RedirectUrl);
                    if (!commonUtils.isNullOrEmpty(redirectUrl)) {
                        window.location.href = redirectUrl;
                    }
                }
                else {
                    if (data.RedirectUrl !== null) {
                        window.location.href = data.RedirectUrl;
                    }
                }                
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
}

function Delete(IF_InvAudNo, InvCodeAudit, url) {
    debugger;
    if (IF_InvAudNo === "") {
        alert("Số phiếu kiểm kê không tồn tại.");
        return;
    }
    var objRQ_InvF_InvAudit = new Object();

    var objInvF_InvAudit = new Object();

    objInvF_InvAudit.IF_InvAudNo = IF_InvAudNo;    
    objInvF_InvAudit.InvCodeAudit = InvCodeAudit;

    objRQ_InvF_InvAudit.FlagIsDelete = "1";
    objRQ_InvF_InvAudit.InvF_InvAudit = objInvF_InvAudit;

    bootbox.confirm("Đồng ý xóa phiếu kiểm kê " + IF_InvAudNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: url,
                data: {
                    model: JSON.stringify(objRQ_InvF_InvAudit)
                },
                type: 'post',
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Messages);
                        if (data.RedirectUrl !== null) {
                            window.location.href = data.RedirectUrl;
                        }
                    } else {
                        showErrorDialog(data.Detail);
                    }
                }
            });
        }
    });
}

function Approve(url, IF_InvAudNo) {
    if (IF_InvAudNo === "") {
        alert("Số phiếu kiểm kê không tồn tại.");
        return;
    }
    var remark = commonUtils.returnValueText('#Remark');
    var lstIF_InvAudNo = [];
    var objIF_InvAudNo = {
        IF_InvAudNo: IF_InvAudNo,
        Remark: remark,
    };
    lstIF_InvAudNo.push(objIF_InvAudNo);
    bootbox.confirm("Đồng ý duyệt phiếu kiểm kê " + IF_InvAudNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: url,
                data: {
                    lstIF_InvAudNo: commonUtils.returnJSONValue(lstIF_InvAudNo),
                },
                type: 'post',
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Messages);
                        if (data.RedirectUrl !== null) {
                            window.location.href = data.RedirectUrl;
                        }
                    } else {
                        showErrorDialog(data.Detail);
                    }
                }
            });
        }
    });
}

function Cancel(url, IF_InvAudNo) {
    if (IF_InvAudNo === "") {
        alert("Số phiếu kiểm kê không tồn tại.");
        return;
    }
    var remark = commonUtils.returnValueText('#Remark');
    var lstIF_InvAudNo = [];
    var objIF_InvAudNo = {
        IF_InvAudNo: IF_InvAudNo,
        Remark: remark,
    };
    lstIF_InvAudNo.push(objIF_InvAudNo);
    bootbox.confirm("Đồng ý hủy phiếu kiểm kê " + IF_InvAudNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: url,
                data: {
                    lstIF_InvAudNo: commonUtils.returnJSONValue(lstIF_InvAudNo),
                },
                type: 'post',
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Messages);
                        if (data.RedirectUrl !== null) {
                            window.location.href = data.RedirectUrl;
                        }
                    } else {
                        showErrorDialog(data.Detail);
                    }
                }
            });
        }
    });
}

function Finish(url, IF_InvAudNo) {
    if (IF_InvAudNo === "") {
        alert("Số phiếu kiểm kê không tồn tại.");
        return;
    }
    var remark = commonUtils.returnValueText('#Remark');
    var lstIF_InvAudNo = [];
    var objIF_InvAudNo = {
        IF_InvAudNo: IF_InvAudNo,
        Remark: remark,
    };
    lstIF_InvAudNo.push(objIF_InvAudNo);
    bootbox.confirm("Đồng ý duyệt phiếu kiểm kê đã kiểm " + IF_InvAudNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: url,
                data: {
                    lstIF_InvAudNo: commonUtils.returnJSONValue(lstIF_InvAudNo),
                },
                type: 'post',
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Messages);
                        if (data.RedirectUrl !== null) {
                            window.location.href = data.RedirectUrl;
                        }
                    } else {
                        showErrorDialog(data.Detail);
                    }
                }
            });
        }
    });
}

//function Action(IF_InvAudNo, urlAction) {
//    if (IF_InvAudNo === "") {
//        alert("Số phiếu kiểm kê không tồn tại.");
//        return;
//    }    
//    bootbox.confirm("Đồng ý thực hiện phiếu kiểm kê " + IF_InvAudNo + "?", function (result) {
//        if (result) {
//            SaveData(urlAction, "0");
//        }
//    });
//}

function DeleteMulti(urlDeletemulti) {
    var countcheck = $('tbody#table-tbodyID input:checked').length;
    if (countcheck === 0) {
        alert("Không có phiếu kiểm kê nào được chọn");
        return;
    }
    var lstInvF_InvAudit = [];
    var next = true;
    $('tbody#table-tbodyID input:checked').each(function () {
        var chkbox = $(this);
        var status = chkbox.attr("status");
        var IF_InvAudNo = chkbox.attr('IF_InvAudNo');
        if (status !== "PENDING") {
            alert("Trạng thái phiếu kiểm kê " + IF_InvAudNo + " không hợp lệ.");
            next = false;
            return false;
        }
        var InvCodeAudit = chkbox.attr('InvCodeAudit');
        
        var objInvF_InvAudit = new Object();
        objInvF_InvAudit.IF_InvAudNo = IF_InvAudNo;
        objInvF_InvAudit.InvCodeAudit = InvCodeAudit;      
        lstInvF_InvAudit.push(objInvF_InvAudit);
    });
    if (next == false) return;
    $.ajax({
        url: urlDeletemulti,
        data: {
            lstInvF_InvAudit: JSON.stringify(lstInvF_InvAudit)            
        },
        type: 'post',
        dataType: 'json',
        traditional: true,
        success: function (data) {
            if (data.Success) {
                alert(data.Messages);
                if (data.RedirectUrl !== null) {
                    window.location.href = data.RedirectUrl;
                }
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
}

function StartScanProduct() {
    alert("Bắt đầu chuyển sang chế độ quét");
    //$('#ProductCode').attr('hidden');
    $('#divProduct').hide();
    $('#ProductScan').removeAttr('hidden');
    $('#ProductScan').focus();
}

function ProductScan(thiz, url, colortex) {
    var productcode = $(thiz).val();
    if (productcode === "") {
        return;
    }
    AddProductByScan($('#rowtemplateProduct'), $('#table-tbodyID'), productcode, url, colortex);
}

function CheckAllProduct(thiz) {
    var check = $(thiz).prop('checked');
    if (check === true) {
        $('.checkProduct').prop("checked", true);
    }
    else {
        $('.checkProduct').prop("checked", false);
    }    
}

function SaveAction(urlSave, savetype) {
    debugger;
    if (CheckForm() === false) return;
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

    var countcheck = $('tbody#table-tbodyID td input:checked').length;
    if (countcheck === 0) {
        alert("Không có hàng hóa nào được kiểm chọn.");
        return;
    }
    if ($('tbody#table-tbodyID td input:checked').length > 0) {
        $("tbody#table-tbodyID tr.trdata td input:checked").each(function () {
            var tr = $(this).parents('tr');
            var idx = $(tr).attr('idx');
            debugger;
            var flaglo = tr.attr('flaglot');
            var flagserial = tr.attr('flagserial');

            var selectdonvi = tr.find('select[name="Lst_InvF_InvAuditDtl[' + idx + '].UnitCode"]');

            var strProductCode = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].ProductCode"]').val();
            var strInvCodeInit = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].InvCodeInit"]').val();
            var strInvCodeActual = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].InvCodeActual"]').val();
            var strUnitCode = $(selectdonvi).val();
            var strQtyInit = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].QtyInit"]').val();
            var strQtyActual = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].QtyActual"]').val();
            var strFlagExist = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].FlagExist"]').val();
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
                                    var objInvF_InvAuditInstLot = new Object();

                                    InvCodeInit = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].InvCodeInit"]').val();
                                    var InvCodeActual = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].InvCodeActual"]').val();
                                    var ProductCode = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ProductCode"]').val();
                                    var ProductLotNo = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ProductLotNo"]').val();
                                    var QtyInit = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].QtyInit"]').val();
                                    var QtyActual = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].QtyActual"]').val();
                                    var ProductionDate = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ProductionDate"]').val();
                                    var ExpiredDate = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].ExpiredDate"]').val();
                                    var flagExistLo = tr_Lo.find('input[name="Lst_InvF_InvAuditInstLot[' + idlo + '].FlagExist"]').val();

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

                                    // Chi tiết Dtl
                                    //InvF_InvAuditDtl = new Object();
                                    //InvF_InvAuditDtl.InvCodeInit = invCode;
                                    //InvF_InvAuditDtl.InvCodeActual = invCode;
                                    //InvF_InvAuditDtl.ProductCode = strProductCode;
                                    //InvF_InvAuditDtl.QtyInit = sumQtyInit;
                                    //InvF_InvAuditDtl.QtyActual = QtyActual;
                                    //InvF_InvAuditDtl.UnitCode = strUnitCode;
                                    //InvF_InvAuditDtl.FlagExist = strFlagExist;
                                    //AddOrChangeQtyDtl(Lst_InvF_InvAuditDtl, InvF_InvAuditDtl);
                                    //Lst_InvF_InvAuditDtl.push(InvF_InvAuditDtl);
                                    //
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
                                    var flagExist = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].FlagExist"]').val();
                                    if (flagExist !== undefined && flagExist === '1') {
                                        qtyInit++;
                                    }
                                });

                                trSerial.each(function () {
                                    var qtyDtl = 1;

                                    var tr_Serial = $(this);
                                    var idserial = $(tr_Serial).attr('idx');

                                    var objInvF_InvAuditInstSerial = new Object();

                                    var InvCodeInit = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].InvCodeInit"]').val();
                                    var InvCodeActual = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].InvCodeActual"]').val();
                                    var ProductCode = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].ProductCode"]').val();
                                    var SerialNo = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].SerialNo"]').val();

                                    var ProductCodeActual = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].ProductCodeActual"]').val();
                                    var SerialNoActual = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].SerialNoActual"]').val();
                                    var FlagExistSerial = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].FlagExist"]').val();
                                    var InventoryAction = tr_Serial.find('input[name="Lst_InvF_InvAuditInstSerial[' + idserial + '].InventoryAction"]').val();

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
                                    var strQtyInit = trDtl.find('input[name="Lst_InvF_InventoryOutComboDtl[' + idxDtl + '].QtyInit"]').val();
                                    var strQtyActual = trDtl.find('input[name="Lst_InvF_InventoryOutComboDtl[' + idxDtl + '].Qty"]').val();
                                    var str_InvCodeInit = trDtl.find('input[name="Lst_InvF_InventoryOutComboDtl[' + idxDtl + '].InvCodeInit"]').val();
                                    var str_InvCodeActual = trDtl.find('input[name="Lst_InvF_InventoryOutComboDtl[' + idxDtl + '].InvCodeOutActual"]').val();
                                    var str_InventoryAction= trDtl.find('input[name="Lst_InvF_InventoryOutComboDtl[' + idxDtl + '].InventoryAction"]').val();
                                    InvF_InvAuditDtl = new Object();
                                    InvF_InvAuditDtl.InvCodeInit = str_InvCodeInit;
                                    InvF_InvAuditDtl.InvCodeActual = str_InvCodeActual;
                                    InvF_InvAuditDtl.ProductCode = strProductCode;
                                    InvF_InvAuditDtl.QtyInit = strQtyInit;
                                    InvF_InvAuditDtl.QtyActual = strQtyActual;
                                    InvF_InvAuditDtl.FlagExist = strFlagExist;
                                    InvF_InvAuditDtl.UnitCode = strUnitCode;
                                    InvF_InvAuditDtl.InventoryAction = str_InventoryAction;
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
    else {
        alert("Không tồn tại hàng hóa trong phiếu kiểm kê");
        return;
    }

    var objJson = JSON.stringify(objRQ_InvF_InvAudit);

    $.ajax({
        url: urlSave,
        data: {
            model: objJson,
            //flagIsDelete: flagIsDelete,
            savetype: savetype
        },
        type: 'post',
        dataType: 'json',
        traditional: true,
        success: function (data) {
            if (data.Success) {
                alert(data.Messages);
                if (data.RedirectUrl !== null) {
                    window.location.href = data.RedirectUrl;
                }
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
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

function ChangeQtyActual(thiz) {
    debugger;
    var strqtyactual = $(thiz).val();
    if ($.isNumeric(strqtyactual) === false) {
        alert("Số lượng thực tế phải là số.");
        return;
    }
    var tr = $(thiz).parents('tr');
    var idx = tr.attr('idx');
    var QtyInit = 0;
    var QtyActual = 0;
    var strQtyInit = tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].QtyInit"]').val();
    if ($.isNumeric(strQtyInit) === false) {
        alert("Tồn kho thực tế phải là số.");
        return;
    }
    QtyInit = parseFloat(strQtyInit);
    QtyActual = parseFloat(strqtyactual);
    //if (QtyActual > QtyInit) {
    //    alert("Số lượng thực tế phải <= số lượng tồn kho.");
    //    return;
    //}
    var qtyRemain = QtyInit - QtyActual;
    tr.find('input[name="Lst_InvF_InvAuditDtl[' + idx + '].QtyRemain"]').val(qtyRemain);
}

function ShowLoDetail(IF_InvAudNo, productCode, urlDetailLo, ProductCodeBase, viewonly, ProductCodeUser, ProductName) {
    debugger;
    var invBUPattern = "";
    var InvCodeAudit = $('#InvCodeAudit').val();

    if (InvCodeAudit === "") {
        alert("Kho kiểm kê chưa được chọn");
        $('#InvCodeAudit').focus();
        return;
    }
    var optSelect = $('#InvCodeAudit').find('option:selected');
    invBUPattern = $(optSelect).attr("invBUPattern");

    
    $.ajax({
        url: urlDetailLo,
        data: {
            productCode: productCode,
            productCodeBase: ProductCodeBase,
            invBUPattern: invBUPattern,
            IF_InvAudNo: IF_InvAudNo,
            viewonly: viewonly,
            productCodeUser: ProductCodeUser,
            productName: ProductName
        },
        type: 'post',
        dataType: 'json',
        traditional: true,
        success: function (data) {
            if (data.Success) {

                $('#ShowPopupLo').modal({
                    backdrop: false,
                    keyboard: true
                });
                $("#ShowPopupLo").html(data.Html); // truyen html vao #form
                var display = $("#ShowPopupLo").css('display');
                if (display === "none") {
                    $("#ShowPopupLo").show();
                }
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
}

function ShowSerialDetail(IF_InvAudNo, productCode, urlDetailSerial, ProductCodeBase, viewonly, ProductCodeUser, ProductName) {
    var invBUPattern = "";
    var invcodeout = "";//$('#InvCodeOut').val();    
    var existKho = $('select.mstinventory').length;
    if (existKho > 0) {
        var mstInventory = $('select.mstinventory').eq(0);
        invcodeout = mstInventory.val();

        var optSelect = mstInventory.find('option:selected');
        invBUPattern = $(optSelect).attr("invBUPattern");
    }

    if (invcodeout === "") {
        alert("Kho xuất chưa được chọn");
        $('#InvCodeAudit').focus();
        return;
    }   
    $.ajax({
        url: urlDetailSerial,
        data: {
            productCode: productCode,
            invBUPattern: invBUPattern,
            productCodeBase: ProductCodeBase,
            IF_InvAudNo: IF_InvAudNo,
            viewonly: viewonly,
            productCodeUser: ProductCodeUser,
            productName: ProductName
        },
        type: 'post',
        dataType: 'json',
        traditional: true,
        success: function (data) {
            if (data.Success) {
                $('#ShowPopupSerial').modal({
                    backdrop: false,
                    keyboard: true
                });
                $("#ShowPopupSerial").html(data.Html); // truyen html vao #form
                var display = $("#ShowPopupSerial").css('display');
                if (display === "none") {
                    $("#ShowPopupSerial").show();
                }
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
}

//
//function ShowTonKhoKK(thiz, ProductCode, urlGetTonKho) {
//    alert('getton kho');
//}

function ShowTonKhoKK(thiz, ProductCode, urlGetTonKho, ProductCodeUser, ProductName, flagAudit, IF_InvAudNo) {
    debugger;
    //Url.Action("GetTonKho", "InvFInvAudit")
    var tr = $(thiz).parents('tr');
    var idx = tr.attr("idx");
    var donvi = tr.find('select[name="Lst_InvF_InventoryOutDtl[' + idx + '].UnitCode"]').find('option:selected');
    var strValConvert = "";
    if (donvi.length !== 0) {
        strValConvert = donvi.attr('valconvert');
    }
    else {
        donvi = tr.find("select.UnitCode").find('option:selected');
        strValConvert = donvi.attr('valconvert');
    }
    var ProductCodeBase = tr.attr("productcodebase");
    var invBUPattern = "";

    var optSelect = $('#InvCodeOut').find('option:selected');
    invBUPattern = $(optSelect).attr("invBUPattern");
    if (invBUPattern === undefined || invBUPattern === "") // Áp dụng khi không phải phiếu xuất kho
    {
        optSelect = $('.mstinventory').find('option:selected');
        invBUPattern = $(optSelect).attr("invBUPattern");
    }

    var rows = $('#tbodyCacheProducts').find('tr[productcode="' + ProductCode + '"][productcodebase="' + ProductCodeBase + '"]').length;
    if (rows > 0) {
        // đã được cache
        var $tbody = $('#table-tbodyIDTonKho');
        $tbody.empty();
        var idx = 0;
        $('#tbodyCacheProducts').find('tr[productcode="' + ProductCode + '"][productcodebase="' + ProductCodeBase + '"]').each(function () {
            debugger;
            var $tr = $(this);
            var idrow = $tr.attr('idrow');
            var productCode = $tr.find('input.ProductCode').val();
            var productCodeBase = $tr.find('input.ProductCodeBase').val();
            var qtyTotalOK = $tr.find('input.QtyTotalOK').val();
            var invCode = $tr.find('input.InvCode').val();
            var qtyActual = $tr.find('input.QtyActual').val();
            var invCodeActual = $tr.find('input.InvCodeActual').val();
            var flagRow = $tr.find('input.FlagRow').val();

            var tempRow = '#tmpl_add_products';

            if (flagRow === '1') {
                tempRow = '#tmpl_add_products';
            }
            else {
                tempRow = '#tmpl_add_products_none';
            }

            var objProduct = {
                qtytotalok: qtyTotalOK,
                invcode: invCode,
                qtyactual: qtyActual,
                invcodeactual: invCodeActual,
                
            };
            var extData = {
                idx: idx,
                idrow: idrow
            };

            var $item = $(commonUtils.getHtmlFromTemplate($(tempRow), objProduct, extData));

            var $select = $item.find('select.InvCode');
            if ($select !== undefined && $select !== null) {
                var $optgroup = $select.find('optgroup');
                if ($optgroup !== undefined && $optgroup !== null) {
                    var $option = $optgroup.find('option[value="' + invCode + '"]');
                    $option.attr('selected', true);
                }
            }

            var $selectActual = $item.find('select.InvCodeActual');
            if ($selectActual !== undefined && $selectActual !== null) {
                var $optgroupActual = $selectActual.find('optgroup');
                if ($optgroupActual !== undefined && $optgroupActual !== null) {
                    var $optionActual = $optgroupActual.find('option[value="' + invCodeActual + '"]');
                    $optionActual.attr('selected', true);
                }
            }

            $tbody.append($item);

            idx++;
        });

        var display = $("#ShowPopupTonKho").css('display');
        if (display === "none") {
            $("#ShowPopupTonKho").show();
        }
    }
    else {
        // chưa được cache
        $.ajax({
            url: urlGetTonKho,
            data: {
                productCode: ProductCode,
                invBUPattern: invBUPattern,
                productCodeBase: ProductCodeBase,
                ValConvert: strValConvert,
                productCodeUser: ProductCodeUser,
                productName: ProductName,
                flagAudit: flagAudit,
                IF_InvAudNo: IF_InvAudNo
            },
            type: 'post',
            dataType: 'json',
            traditional: true,
            success: function (data) {
                if (data.Success) {
                    $('#ShowPopupTonKho').modal({
                        backdrop: false,
                        keyboard: true
                    });

                    $("#ShowPopupTonKho").html(data.Html); // truyen html vao #form

                    var display = $("#ShowPopupTonKho").css('display');
                    if (display === "none") {
                        $("#ShowPopupTonKho").show();
                    }

                    // Kiểm tra xem sản phảm có lưu cache không, nếu có gán lại




                } else {
                    showErrorDialog(data.Detail);
                }
            }
        });
    }
    
}

function ShowAddLo() {
    $('#ShowPopupAddLo').modal({
        backdrop: false,
        keyboard: true
    });
    $('#ShowPopupAddLo').show();
}

function ShowAddSerial() {
    $('#ShowPopupAddSerial').modal({
        backdrop: false,
        keyboard: true
    });
    $('#ShowPopupAddSerial').show();
}

function CheckAllProduct() {
    debugger;
    var ischeck = $('#CheckAllProduct').prop('checked');
    if (ischeck == true) {
        $('#table-tbodyID tr.trdata').find('input.checkProduct').prop('checked', true);
    }
    else {
        $('#table-tbodyID tr.trdata').find('input.checkProduct').prop('checked', false);
    }
}

function Action(urlAction) {
    debugger;
    var countcheck = $('tbody#table-tbodyID input:checked').length;
    if (countcheck === 0) {
        alert("Không có phiếu kiểm kê nào được chọn");
        return;
    }
    else if (countcheck > 1) {
        alert("Chỉ chọn 1 phiếu kiểm kê để thực hiện.");
        return;
    }
    var chk = $('tbody#table-tbodyID input:checked');
    var status = chk.attr("status");
    var IF_InvAudNo = chk.attr('IF_InvAudNo');

    if (status !== "APPROVE") {
        alert("Trạng thái phiếu kiểm kê " + IF_InvAudNo + " không hợp lệ.");
        return;
    } 
    var IF_InvAudNo = chk.attr('IF_InvAudNo');
    var url = urlAction + "?IF_InvAudNo=" + IF_InvAudNo;
    window.location.href = url;
}

function ApproveAudit(urlApprove) {
    debugger;
    var countcheck = $('tbody#table-tbodyID input:checked').length;
    if (countcheck === 0) {
        alert("Không có phiếu kiểm kê nào được chọn");
        return;
    }
    var lstIF_InvAudNo = [];
    var arrIF_InvAudNo = [];
    var next = true;
    $('tbody#table-tbodyID input:checked').each(function () {
        var chkbox = $(this);
        var status = chkbox.attr("status");
        //var IF_InvAudNo = chkbox.attr('IF_InvAudNo');
        var iF_InvAudNo = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvAudNo"]').val());
        var remark = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="Remark"]').val());
        if (status !== "PENDING") {
            alert("Trạng thái phiếu kiểm kê " + iF_InvAudNo + " không hợp lệ.");
            next = false;
            return false;
        }       
        var objInvFInvAudit = {};
        objInvFInvAudit.IF_InvAudNo = iF_InvAudNo;
        objInvFInvAudit.Remark = remark;
        lstIF_InvAudNo.push(objInvFInvAudit);
        arrIF_InvAudNo.push(iF_InvAudNo);
    });
    if (next == false) return;
    var objlstIF_InvAudNo = commonUtils.returnJSONValue(lstIF_InvAudNo);
    var strlstIF_InvAudNo = commonUtils.returnJSONValue(arrIF_InvAudNo);
    bootbox.confirm("Đồng ý duyệt phiếu kiểm kê " + strlstIF_InvAudNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: urlApprove,
                data: {
                    lstIF_InvAudNo: objlstIF_InvAudNo
                },
                type: 'post',
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Messages);
                        if (data.RedirectUrl !== null) {
                            window.location.href = data.RedirectUrl;
                        }
                    } else {
                        showErrorDialog(data.Detail);
                    }
                }
            });
        }
    });
}

function CancelAudit(urlCancel) {
    debugger;
    var countcheck = $('tbody#table-tbodyID input:checked').length;
    if (countcheck === 0) {
        alert("Không có phiếu kiểm kê nào được chọn");
        return;
    }
    var lstIF_InvAudNo = [];
    var arrIF_InvAudNo = [];
    var next = true;
    $('tbody#table-tbodyID input:checked').each(function () {
        var chkbox = $(this);
        var status = chkbox.attr("status");
        var iF_InvAudNo = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvAudNo"]').val());
        var remark = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="Remark"]').val());
        if (status !== "PENDING" && status !== "APPROVE") {
            alert("Trạng thái phiếu kiểm kê " + iF_InvAudNo + " không hợp lệ.");
            next = false;
            return false;
        }
        var objInvFInvAudit = {};
        objInvFInvAudit.IF_InvAudNo = iF_InvAudNo;
        objInvFInvAudit.Remark = remark;
        lstIF_InvAudNo.push(objInvFInvAudit);
        arrIF_InvAudNo.push(iF_InvAudNo);
    });
    if (next == false) return;
    var objlstIF_InvAudNo = commonUtils.returnJSONValue(lstIF_InvAudNo);
    var strlstIF_InvAudNo = commonUtils.returnJSONValue(arrIF_InvAudNo);

    bootbox.confirm("Đồng ý hủy phiếu kiểm kê " + strlstIF_InvAudNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: urlCancel,
                data: {
                    lstIF_InvAudNo: objlstIF_InvAudNo
                },
                type: 'post',
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Messages);
                        if (data.RedirectUrl !== null) {
                            window.location.href = data.RedirectUrl;
                        }
                    } else {
                        showErrorDialog(data.Detail);
                    }
                }
            });
        }
    });
}

function Update(urlUpdate) {
    debugger;
    var countcheck = $('tbody#table-tbodyID input:checked').length;
    if (countcheck === 0) {
        alert("Không có phiếu kiểm kê nào được chọn");
        return;
    }
    else if (countcheck > 1) {
        alert("Chỉ chọn 1 phiếu kiểm kê để thực hiện.");
        return;
    }
    var chk = $('tbody#table-tbodyID input:checked');
    var IF_InvAudNo = chk.attr('IF_InvAudNo');
    var status = chk.attr("status");
    if (status != "PENDING") {
        alert("Trạng thái phiếu xuất không hợp lệ.");
        return;
    }
    else {
        var url = urlUpdate + "?IF_InvAudNo=" + IF_InvAudNo;
        window.location.href = url;
    }    
}

function FinishAudit(urlFinish) {
    var countcheck = $('tbody#table-tbodyID input:checked').length;
    if (countcheck === 0) {
        alert("Không có phiếu kiểm kê nào được chọn");
        return;
    }
    var lstIF_InvAudNo = [];
    var arrIF_InvAudNo = [];
    var next = true;
    $('tbody#table-tbodyID input:checked').each(function () {
        var chkbox = $(this);
        var status = chkbox.attr("status");
        var iF_InvAudNo = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_InvAudNo"]').val());
        var remark = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="Remark"]').val());
        if (status !== "APPROVE") {
            alert("Trạng thái phiếu kiểm kê " + iF_InvAudNo + " không hợp lệ.");
            next = false;
            return false;
        }
        var objInvFInvAudit = {};
        objInvFInvAudit.IF_InvAudNo = iF_InvAudNo;
        objInvFInvAudit.Remark = remark;
        lstIF_InvAudNo.push(objInvFInvAudit);
        arrIF_InvAudNo.push(iF_InvAudNo);
    });
    if (next == false) return;
    var objlstIF_InvAudNo = commonUtils.returnJSONValue(lstIF_InvAudNo);
    var strlstIF_InvAudNo = commonUtils.returnJSONValue(arrIF_InvAudNo);

    bootbox.confirm("Đồng ý duyệt phiếu kiểm kê đã kiểm " + strlstIF_InvAudNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: urlFinish,
                data: {
                    lstIF_InvAudNo: objlstIF_InvAudNo
                },
                type: 'post',
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Messages);
                        if (data.RedirectUrl !== null) {
                            window.location.href = data.RedirectUrl;
                        }
                    } else {
                        showErrorDialog(data.Detail);
                    }
                }
            });
        }
    });   
}

function Detail(urlDetail) {
    window.location.href = urlDetail;
}

function Export(urlExport) {
    debugger;
    var countcheck = $('tbody#table-tbodyID input:checked').length;
    if (countcheck === 0) {
        alert("Không có phiếu kiểm kê nào được chọn");
        return;
    }
    
    // Lấy danh sách phiếu 
    var lstInvCodeAudit = [];
    $('#table-tbodyID').find('input.checkProduct:checked').each(function () {
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
        url: urlExport,
        data: formdata,
        processData: false,
        contentType: false,
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            if (data.Success) {
                debugger;
                if (data !== null) {
                    alert(data.Title);
                    window.location.href = data.strUrl;
                }
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
}
