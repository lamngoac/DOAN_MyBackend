function CheckForm() {
    if (!commonUtils.checkElementIsNullOrEmpty('#InvOutType', 'has-error-fix', 'Loại xuất kho không được trống!')) {
        return false;
    }
    if (!commonUtils.checkElementIsNullOrEmpty('#CustomerCode', 'has-error-fix', 'Khách hàng không được trống!')) {
        return false;
    }
    if (!commonUtils.checkElementIsNullOrEmpty('#InvCodeOut', 'has-error-fix', 'Kho xuất không được trống!')) {
        return false;
    }
    if ($('tbody#table-tbodyID tr.trdata').length == 0) {
        alert('Danh mục hàng hóa trống!');
        return false;
    }
    return true;
}

function SaveData(url) {
    debugger;
    var checkform = CheckForm();
    if (checkform === false) return;
    var objRQ_InvF_InventoryOut = new Object();

    var objInvF_InventoryOut = new Object();
    var IF_InvOutNo = $('#IF_InvOutNo').val();
    var InvOutType = $('#InvOutType').val();
    var InvCodeOut = $('#InvCodeOut').val();
    var CustomerCode = $('#CustomerCode').val();
    var RefNo = $('#RefNo').val();
    var RefType = $('#RefType').val();
    if (RefType === 'RO') {
        RefNo = RefNo.substring(3);//Nếu là RO thì cắt 3 ký tự đầu 'RO-'
    }
    var RefNoSys = $('#RefNoSys').val();
    var TotalValOut = $('#TotalValOut').val();
    var TotalValOutDesc = $('#TotalValOutDesc').val();
    var TotalValOutAfterDesc = $('#TotalValOutAfterDesc').val();
    var Remark = $('#Remark').val();
    var invFCFOutCode01 = $('#InvFCFOutCode01').val();
    var invFCFOutCode02 = $('#InvFCFOutCode02').val();
    var invFCFOutCode03 = $('#InvFCFOutCode03').val();

    objInvF_InventoryOut.IF_InvOutNo = IF_InvOutNo;
    objInvF_InventoryOut.InvOutType = InvOutType;
    objInvF_InventoryOut.InvCodeOut = InvCodeOut;
    objInvF_InventoryOut.CustomerCode = CustomerCode;
    objInvF_InventoryOut.RefNo = RefNo;
    objInvF_InventoryOut.RefType = RefType;
    objInvF_InventoryOut.RefNoSys = RefNoSys;
    objInvF_InventoryOut.TotalValOut = TotalValOut;
    objInvF_InventoryOut.TotalValOutDesc = TotalValOutDesc;
    objInvF_InventoryOut.TotalValOutAfterDesc = TotalValOutAfterDesc;
    objInvF_InventoryOut.Remark = Remark;
    objInvF_InventoryOut.InvFCFOutCode01 = invFCFOutCode01;
    objInvF_InventoryOut.InvFCFOutCode02 = invFCFOutCode02;
    objInvF_InventoryOut.InvFCFOutCode03 = invFCFOutCode03;

    var Lst_InvF_InventoryOutDtl = [];
    var Lst_InvF_InventoryOutCover = [];
    var Lst_InvF_InventoryOutInstLot = [];
    var Lst_InvF_InventoryOutInstSerial = [];
    var Lst_InvF_InventoryOutQR = [];

    //Lấy chi tiết hàng hóa
    var check = true;
    if ($('tbody#table-tbodyID').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            debugger;
            var tr = $(this);            
            var flagCombo = $(tr).attr('flagCombo');
            var flagserial = $(tr).attr('flagserial');
            var flaglot = $(tr).attr('flaglot');
            var idx = $(tr).attr('idx');

            var strInvCodeOutActual = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].InvCodeOutActual"]').val();
            var strProductCodeRoot = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ProductCodeRoot"]').val();
            var strProductCode = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ProductCode"]').val();
            var strQty = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Qty"]').val();
            var selectdonvi = tr.find('select[name="Lst_InvF_InventoryOutDtl[' + idx + '].UnitCode"]');
            var strUnitCode = $(selectdonvi).val();
            var strRemark = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Remark"]').val();
            var strUPOut = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].SellProduct"]').val();
            var strUPOutDesc = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].DiscountPrice"]').val();
            var strValOutAfterDesc = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ValAmount"]').val();  
            var objInvF_InventoryOutCover = new Object();

            if (flagCombo === "0") {
                debugger;
                // Check dữ liệu hàng hóa thường lưu trong tableComboDetail
                var trProductInCB = $('#table-detailCombo').find('tr.trdata[productcode=' + strProductCode + ']');

                var qtyTableDtl = trProductInCB.length;

                if (trProductInCB.length > 0) {
                    var qtyCB = 0;
                    trProductInCB.each(function () {
                        var idz = $(this).attr("idx");
                        var strqtyCB = $(this).find('input[name="Lst_InvF_InventoryOutComboDtl[' + idz + '].Qty"]').val();
                        if ($.isNumeric(strqtyCB)) {
                            qtyCB += parseFloat(strqtyCB);
                        }
                    });

                    var qtyDtl = parseFloat(strQty);
                    
                    if (qtyCB !== qtyDtl) {
                        if (qtyTableDtl === 1) { // Tự phân bổ
                            // Tự phân bổ, bỏ cảnh báo
                            $('#table-detailCombo tr.trdata').find('input.Qty').val(qtyDtl);                            
                        }
                        else if (qtyTableDtl > 1) { // Cảnh báo
                            alert("Số lượng hàng không khớp với số lượng đã chọn trong tồn kho.");
                            check = false;
                            return;
                        }                       
                    }
                }
                //else {
                //    if (flagserial != "1" && flaglot != "1") {
                //        // List hàng hóa khi không chọn vị trí tồn 2020-04-17
                //        var objInvF_InventoryOutDtl = new Object();
                //        objInvF_InventoryOutDtl.InvCodeOutActual = strInvCodeOutActual;
                //        objInvF_InventoryOutDtl.ProductCodeRoot = strProductCodeRoot;
                //        objInvF_InventoryOutDtl.ProductCode = strProductCode;
                //        objInvF_InventoryOutDtl.Qty = strQty;
                //        objInvF_InventoryOutDtl.UnitCode = strUnitCode;
                //        objInvF_InventoryOutDtl.Remark = strRemark;
                //        Lst_InvF_InventoryOutDtl.push(objInvF_InventoryOutDtl);
                //    //
                //    }                    
                //}
                //

                //InvF_InventoryOutCover                            
                objInvF_InventoryOutCover.ProductCodeRoot = strProductCodeRoot;
                objInvF_InventoryOutCover.ProductCode = strProductCode;
                objInvF_InventoryOutCover.Qty = strQty;
                objInvF_InventoryOutCover.UnitCode = strUnitCode;
                objInvF_InventoryOutCover.Remark = strRemark;
                objInvF_InventoryOutCover.UPOut = strUPOut;
                objInvF_InventoryOutCover.UPOutDesc = strUPOutDesc;
                objInvF_InventoryOutCover.ValOutAfterDesc = strValOutAfterDesc;

                Lst_InvF_InventoryOutCover.push(objInvF_InventoryOutCover);
                // End InvF_InventoryOutCover 
            }
            else if(flagCombo === "1"){               
                //InvF_InventoryOutCover                            
                //objInvF_InventoryOutCover.InvCodeOutActual = strInvCodeOutActual;
                objInvF_InventoryOutCover.ProductCodeRoot = strProductCodeRoot;
                objInvF_InventoryOutCover.ProductCode = strProductCode;
                objInvF_InventoryOutCover.Qty = strQty;
                objInvF_InventoryOutCover.UnitCode = strUnitCode;
                objInvF_InventoryOutCover.Remark = strRemark;
                objInvF_InventoryOutCover.UPOut = strUPOut;
                objInvF_InventoryOutCover.UPOutDesc = strUPOutDesc;
                objInvF_InventoryOutCover.ValOutAfterDesc = strValOutAfterDesc;

                Lst_InvF_InventoryOutCover.push(objInvF_InventoryOutCover);
                // End InvF_InventoryOutCover 
            }
        });
    }
    if (check === false) return;

    // Thông tin Lot
    if ($('tbody#table-detailLot').length > 0) {
        //var qtylot = $("tbody#table-detailLot tr.trdata").length;
        $("tbody#table-detailLot tr.trdata").each(function () {
            var tr = $(this);
            var x = tr.attr('idx');
            var objInvF_InventoryOutInstLot = new Object();

            var strInvCodeOutActual0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].InvCodeOutActual"]').val();
            var strProductCodeRoot0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].ProductCodeRoot"]').val();
            var strProductCode0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].ProductCode"]').val();
            var strQty0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].Qty"]').val();
            if (strQty0 === '0') {
                return;
            }
            var strProductLotNo0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].ProductLotNo"]').val();
            var strProductionDate0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].ProductionDate"]').val();
            var strExpiredDate0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].ExpiredDate"]').val();

            objInvF_InventoryOutInstLot.InvCodeOutActual = strInvCodeOutActual0;
            objInvF_InventoryOutInstLot.ProductCodeRoot = strProductCodeRoot0;
            objInvF_InventoryOutInstLot.ProductCode = strProductCode0;
            objInvF_InventoryOutInstLot.ProductLotNo = strProductLotNo0;
            objInvF_InventoryOutInstLot.Qty = strQty0;
            objInvF_InventoryOutInstLot.ProductionDate = strProductionDate0;
            objInvF_InventoryOutInstLot.ExpiredDate = strExpiredDate0;

            Lst_InvF_InventoryOutInstLot.push(objInvF_InventoryOutInstLot);
            
            //InvF_InventoryOutDtl
            var objInvF_InventoryOutDtl = new Object();
            objInvF_InventoryOutDtl.InvCodeOutActual = strInvCodeOutActual0;
            objInvF_InventoryOutDtl.ProductCodeRoot = strProductCodeRoot0;
            objInvF_InventoryOutDtl.ProductCode = strProductCode0;
            objInvF_InventoryOutDtl.Qty = strQty0;

            var trInvOutDtl = $("tbody#table-tbodyID tr.trdata[productcode=" + strProductCodeRoot0 + "]");
            var strUnitCode = "";
            var strRemark = "";
            if (trInvOutDtl.length !== 0) {
                var idx = trInvOutDtl.attr('idx');
                strUnitCode = trInvOutDtl.find('select[name="Lst_InvF_InventoryOutDtl[' + idx + '].UnitCode"]').val();
                strRemark = trInvOutDtl.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Remark"]').val();
            }
            objInvF_InventoryOutDtl.UnitCode = strUnitCode;
            objInvF_InventoryOutDtl.Remark = strRemark;

            AddOrChangeQtyDtl(Lst_InvF_InventoryOutDtl, objInvF_InventoryOutDtl);
            //Lst_InvF_InventoryOutDtl.push(objInvF_InventoryOutDtl);
            // End InvF_InventoryOutDtl
        });
    }
    //

    // Thông tin Serial
    
    if ($('tbody#table-detailSerial').length > 0) {
        //var qtySerial = $("tbody#table-detailSerial tr.trdata").length;
        $("tbody#table-detailSerial tr.trdata").each(function () {
            var tr = $(this);
            var z = tr.attr('idx');
            var strInvCodeOutActual3 = tr.find('input[name="Lst_InvF_InventoryOutSerialDtl[' + z + '].InvCodeOutActual"]').val();
            var strProductCodeRoot3 = tr.find('input[name="Lst_InvF_InventoryOutSerialDtl[' + z + '].ProductCodeRoot"]').val();
            var strProductCode3 = tr.find('input[name="Lst_InvF_InventoryOutSerialDtl[' + z + '].ProductCode"]').val();
            var strSerialNo3 = tr.find('input[name="Lst_InvF_InventoryOutSerialDtl[' + z + '].SerialNo"]').val();

            var objInvF_InventoryOutInstSerial = new Object();
            objInvF_InventoryOutInstSerial.InvCodeOutActual = strInvCodeOutActual3;
            objInvF_InventoryOutInstSerial.ProductCodeRoot = strProductCodeRoot3;
            objInvF_InventoryOutInstSerial.ProductCode = strProductCode3;
            objInvF_InventoryOutInstSerial.SerialNo = strSerialNo3;

            Lst_InvF_InventoryOutInstSerial.push(objInvF_InventoryOutInstSerial);

            //InvF_InventoryOutDtl         
            var trInvOutDtl = $("tbody#table-tbodyID tr.trdata[productcode=" + strProductCodeRoot3 + "]");
            var strUnitCode = "";
            var strRemark = "";
            if (trInvOutDtl.length !== 0) {
                var idx = trInvOutDtl.attr("idx");
                strUnitCode = trInvOutDtl.find('select[name="Lst_InvF_InventoryOutDtl[' + idx + '].UnitCode"]').val();
                strRemark = trInvOutDtl.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Remark"]').val();
            }
            var objInvF_InventoryOutDtl = new Object();
            objInvF_InventoryOutDtl.UnitCode = strUnitCode;
            objInvF_InventoryOutDtl.Remark = strRemark;            
            objInvF_InventoryOutDtl.InvCodeOutActual = strInvCodeOutActual3;
            objInvF_InventoryOutDtl.ProductCodeRoot = strProductCodeRoot3;
            objInvF_InventoryOutDtl.ProductCode = strProductCode3;
            objInvF_InventoryOutDtl.Qty = 1;

            AddOrChangeQtyDtl(Lst_InvF_InventoryOutDtl, objInvF_InventoryOutDtl);
            //Lst_InvF_InventoryOutDtl.push(objInvF_InventoryOutDtl);
            // End InvF_InventoryOutDtl
        });
    }
    //

    // Thông tin Combo ----- Add thêm theo COMBO InvF_InventoryOutCover and Lst_InvF_InventoryOutDtl
    if ($('tbody#table-detailCombo').length > 0) {        
        $("tbody#table-detailCombo tr.trdata").each(function () {
            var tr = $(this);
            var objInvF_InventoryOutDtl1 = new Object();
            //var objInvF_InventoryOutCover1 = new Object();
            var y = $(tr).attr('idx');
            //InvF_InventoryOutDtl
            var strInvCodeOutActual1 = tr.find('input[name="Lst_InvF_InventoryOutComboDtl['+y+'].InvCodeOutActual"]').val();
            var strProductCodeRoot1 = tr.find('input[name="Lst_InvF_InventoryOutComboDtl[' + y +'].ProductCodeRoot"]').val();
            var strProductCode1 = tr.find('input[name="Lst_InvF_InventoryOutComboDtl[' + y +'].ProductCode"]').val();
            var strQty1 = tr.find('input[name="Lst_InvF_InventoryOutComboDtl[' + y +'].Qty"]').val();
            var strUnitCode1 = tr.find('input[name="Lst_InvF_InventoryOutComboDtl[' + y +'].UnitCode"]').val();
            var strRemark1 = tr.find('input[name="Lst_InvF_InventoryOutComboDtl[' + y + '].Remark"]').val();

            var trInvOutDtl = $("tbody#table-tbodyID tr.trdata[productcode=" + strProductCodeRoot1 + "]");
            var flagCombo = tr.attr('flagcombo');
            var qtyCombo = 0;
            if (trInvOutDtl.length !== 0) {
                var idx = trInvOutDtl.attr("idx");
                var qty = trInvOutDtl.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Qty"]').val();
                if (qty === "") {
                    alert("Số lượng combo không hợp lệ");
                    check = false;
                    return;
                }
                qtyCombo = parseFloat(qty) * parseFloat(strQty1);               
            }

           

            objInvF_InventoryOutDtl1.InvCodeOutActual = strInvCodeOutActual1;
            objInvF_InventoryOutDtl1.ProductCodeRoot = strProductCodeRoot1;
            objInvF_InventoryOutDtl1.ProductCode = strProductCode1;

            if (flagCombo === "1") {
                objInvF_InventoryOutDtl1.Qty = qtyCombo;
            }
            else {
                objInvF_InventoryOutDtl1.Qty = strQty1;
            }
            
            objInvF_InventoryOutDtl1.UnitCode = strUnitCode1;
            objInvF_InventoryOutDtl1.Remark = strRemark1;

            Lst_InvF_InventoryOutDtl.push(objInvF_InventoryOutDtl1);
            //AddOrChangeQtyDtl(Lst_InvF_InventoryOutDtl, objInvF_InventoryOutDtl1);
            // End InvF_InventoryOutDtl

            
            //InvF_InventoryOutCover         
            //var trInvOutDtl = $("tbody#table-tbodyID tr.trdata[productcode=" + strProductCodeRoot1 + "]");
            //var qtyCombo = 0;
            //if (trInvOutDtl.length !== 0) {
            //    var idx = trInvOutDtl.attr("idx");
            //    var qty = trInvOutDtl.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Qty"]').val();
            //    if (qty === "") {
            //        alert("Số lượng combo không hợp lệ");
            //        check = false;
            //        return;
            //    }
            //    qtyCombo = parseFloat(qty) * parseFloat(strQty1);

            //    var strUPOut1 = trInvOutDtl.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].SellProduct"]').val();
            //    var strUPOutDesc1 = trInvOutDtl.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].DiscountPrice"]').val();
            //    var strValOutAfterDesc1 = trInvOutDtl.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ValAmount"]').val();

            //    objInvF_InventoryOutCover1.UPOut = strUPOut1;
            //    objInvF_InventoryOutCover1.UPOutDesc = strUPOutDesc1;
            //    objInvF_InventoryOutCover1.ValOutAfterDesc = strValOutAfterDesc1;
            //}

            ////objInvF_InventoryOutCover1.InvCodeOutActual = strInvCodeOutActual1;
            //objInvF_InventoryOutCover1.ProductCodeRoot = strProductCodeRoot1;
            //objInvF_InventoryOutCover1.ProductCode = strProductCode1;
            //objInvF_InventoryOutCover1.Qty = qtyCombo;//strQty1;
            //objInvF_InventoryOutCover1.UnitCode = strUnitCode1;
            //objInvF_InventoryOutCover1.Remark = strRemark1;            

            //Lst_InvF_InventoryOutCover.push(objInvF_InventoryOutCover1);
            // End InvF_InventoryOutCover
        });
    }
    //

    // Lấy thông tin xác thực
    if ($('#table-tbodyIDXacThuc tr.trdata').length > 0) {
        $("#table-tbodyIDXacThuc tr.trdata").each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');
            var strProductCode = tr.find('input[name="Lst_InvF_InventoryOutQR[' + idx + '].ProductCode"]').val();
            var strProductName = tr.find('input[name="Lst_InvF_InventoryOutQR[' + idx + '].ProductName"]').val();
            var strQRCode = tr.find('input[name="Lst_InvF_InventoryOutQR[' + idx + '].QRCode"]').val();
            var selectQRType = tr.find('select[name="Lst_InvF_InventoryOutQR[' + idx + '].QRType"]');
            var strQRType = "";
            if (selectQRType !== undefined) {
                strQRType = selectQRType.val();
            }
            var objInvF_InventoryOutQR = new Object();
            objInvF_InventoryOutQR.ProductCode = strProductCode;
            objInvF_InventoryOutQR.ProductName = strProductName;
            objInvF_InventoryOutQR.QRCode = strQRCode;
            objInvF_InventoryOutQR.QRType = strQRType;
            Lst_InvF_InventoryOutQR.push(objInvF_InventoryOutQR);
        });
    }
    //

    // Gán các list vào đối tượng request
    objRQ_InvF_InventoryOut.InvF_InventoryOut = objInvF_InventoryOut;
    objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover = Lst_InvF_InventoryOutCover;
    objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutDtl = Lst_InvF_InventoryOutDtl;
    objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstLot = Lst_InvF_InventoryOutInstLot;
    objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutInstSerial = Lst_InvF_InventoryOutInstSerial;
    objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutQR = Lst_InvF_InventoryOutQR;
    //

    var objJson = JSON.stringify(objRQ_InvF_InventoryOut);

    $.ajax({
        url: url,
        data: {
            model: objJson
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

    //$.ajax({
    //    url: url,
    //    type: 'Post',
    //    beforeSend: function () { },
    //    success: function (result) {
    //        var getData = result;
    //        if (getData.Success === false || getData.Success === 'false') {
    //            showErrorDialog(getData.Detail);
    //        } else {
    //            if (getData.Success === true || getData.Success === 'true') {
    //                alert(getData.Title);
    //                if (getData.CheckSuccess === "1") {
    //                    if (getData.strUrl !== null && getData.strUrl.length > 0) {
    //                        window.location = getData.strUrl;
    //                    }
    //                }
    //            }
    //        }
    //    },
    //    data: {
    //        model: objJson
    //    },
    //    cache: false,
    //    contentType: false,
    //    dataType: "json",
    //    processData: false
    //});
}

function SaveDataExpCross(url) {
    debugger;
    var checkform = CheckForm();
    if (checkform === false) return;
    var objRQ_InvF_InventoryOut = new Object();

    var objInvF_InventoryOut = new Object();
    var IF_InvOutNo = $('#IF_InvOutNo').val();
    var IF_InvOutNo_Root = $('#IF_InvOutNo_Root').val();
    var InvOutType = $('#InvOutType').val();
    var InvCodeOut = $('#InvCodeOut').val();
    var CustomerCode = $('#CustomerCode').val();
    var OrderNo = $('#OrderNo').val();
    var OrderType = $('#OrderType').val();
    var OrderNoSys = $('#OrderNoSys').val();
    var TotalValOut = $('#TotalValOut').val();
    var TotalValOutDesc = $('#TotalValOutDesc').val();
    var TotalValOutAfterDesc = $('#TotalValOutAfterDesc').val();
    var Remark = $('#Remark').val();
    var invFCFOutCode01 = $('#InvFCFOutCode01').val();
    var invFCFOutCode02 = $('#InvFCFOutCode02').val();
    var invFCFOutCode03 = $('#InvFCFOutCode03').val();

    objInvF_InventoryOut.IF_InvOutNo = IF_InvOutNo;
    objInvF_InventoryOut.IF_InvOutNo_Root = IF_InvOutNo_Root;
    objInvF_InventoryOut.InvOutType = InvOutType;
    objInvF_InventoryOut.InvCodeOut = InvCodeOut;
    objInvF_InventoryOut.CustomerCode = CustomerCode;
    objInvF_InventoryOut.OrderNo = OrderNo;
    objInvF_InventoryOut.OrderType = OrderType;
    objInvF_InventoryOut.OrderNoSys = OrderNoSys;
    objInvF_InventoryOut.TotalValOut = TotalValOut;
    objInvF_InventoryOut.TotalValOutDesc = TotalValOutDesc;
    objInvF_InventoryOut.TotalValOutAfterDesc = TotalValOutAfterDesc;
    objInvF_InventoryOut.Remark = Remark;
    objInvF_InventoryOut.InvFCFOutCode01 = invFCFOutCode01;
    objInvF_InventoryOut.InvFCFOutCode02 = invFCFOutCode02;
    objInvF_InventoryOut.InvFCFOutCode03 = invFCFOutCode03;

    var Lst_InvF_InventoryOutCover = [];

    //Lấy chi tiết hàng hóa
    var check = true;
    if ($('tbody#table-tbodyID').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');

            var strProductCodeRoot = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ProductCodeRoot"]').val();
            var strProductCode = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ProductCode"]').val();
            //var strQty = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Qty"]').val();
            var strQtyOut = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].QtyOut"]').val();            
            var selectdonvi = tr.find('select[name="Lst_InvF_InventoryOutDtl[' + idx + '].UnitCode"]');
            var strUnitCode = $(selectdonvi).val();
            var strRemark = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Remark"]').val();
            var strUPOut = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].SellProduct"]').val();
            var strUPOutDesc = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].DiscountPrice"]').val();
            var strValOutAfterDesc = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ValAmount"]').val();
            var objInvF_InventoryOutCover = new Object();

            //InvF_InventoryOutCover                            
            objInvF_InventoryOutCover.ProductCodeRoot = strProductCodeRoot;
            objInvF_InventoryOutCover.ProductCode = strProductCode;
            objInvF_InventoryOutCover.Qty = strQtyOut;
            objInvF_InventoryOutCover.UnitCode = strUnitCode;
            objInvF_InventoryOutCover.Remark = strRemark;
            objInvF_InventoryOutCover.UPOut = strUPOut;
            objInvF_InventoryOutCover.UPOutDesc = strUPOutDesc;
            objInvF_InventoryOutCover.ValOutAfterDesc = strValOutAfterDesc;

            Lst_InvF_InventoryOutCover.push(objInvF_InventoryOutCover);
            // End InvF_InventoryOutCover 

        });
    }
    if (check === false) return;        

    // Gán các list vào đối tượng request
    objRQ_InvF_InventoryOut.InvF_InventoryOut = objInvF_InventoryOut;
    objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover = Lst_InvF_InventoryOutCover;
    //

    var objJson = JSON.stringify(objRQ_InvF_InventoryOut);

    $.ajax({
        url: url,
        data: {
            model: objJson
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

//function ReturnQtyInDtl(Lst_InvF_InventoryOutDtl, InvCode, ProductCode, qty) {
//    
//    if (Lst_InvF_InventoryOutDtl.length === 0) return qty;    
//    for (var i = 0; i < Lst_InvF_InventoryOutDtl.length; i++) {
//        var it = Lst_InvF_InventoryOutDtl[i];
//        if (it.InvCodeOutActual === InvCode && it.ProductCodeRoot === ProductCodeRoot && it.ProductCode === ProductCode) {
//            qty = parseFloat(it.Qty) + qty;
//            return;
//        }
//    }
//    return qty;
//}

function AddOrChangeQtyDtl(Lst_InvF_InventoryOutDtl, objInvF_InventoryOutDtl) {
    debugger;
    if (Lst_InvF_InventoryOutDtl.length === 0) {
        Lst_InvF_InventoryOutDtl.push(objInvF_InventoryOutDtl);
        return Lst_InvF_InventoryOutDtl;
    }
    var checkexist = false;
    for (var i = 0; i < Lst_InvF_InventoryOutDtl.length; i++) {
        var it = Lst_InvF_InventoryOutDtl[i];
        var InvCode = objInvF_InventoryOutDtl.InvCodeOutActual;
        var ProductCodeRoot = objInvF_InventoryOutDtl.ProductCodeRoot;
        var ProductCode = objInvF_InventoryOutDtl.ProductCode;
        var qty = parseFloat(objInvF_InventoryOutDtl.Qty);
        if (it.InvCodeOutActual === InvCode && it.ProductCodeRoot === ProductCodeRoot && it.ProductCode === ProductCode) {
            qty = parseFloat(it.Qty) + qty;
            Lst_InvF_InventoryOutDtl[i].Qty = qty;
            checkexist = true;
            return;
        }
    }
    if (checkexist === false) {
        Lst_InvF_InventoryOutDtl.push(objInvF_InventoryOutDtl);
    }
    return Lst_InvF_InventoryOutDtl;
}

function TongTien() {
    debugger
    var sumtienhang = 0.0;
    var sumgiamgia = 0.0;
    var sumthanhtoan = 0.0;
    if ($('tbody#table-tbodyID').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            var tr = $(this);
            var idx = tr.attr('idx');
            var strgiaban = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].SellProduct"]').val();
            var strQty = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Qty"]').val();
            var strDiscountPrice = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].DiscountPrice"]').val();
            //var strValAmount = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ValAmount"]').val();

            var giaban = parseFloat(strgiaban);
            var qty = parseFloat(strQty);
            var discountprice = parseFloat(strDiscountPrice);
            //var valamout = parseFloat(strValAmount);
            var valamout = (giaban - discountprice) * qty;
            tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ValAmount"]').val(valamout);
            //

            var tienhang = giaban * qty;
            sumtienhang += tienhang;

            sumthanhtoan += valamout;
        });

        sumgiamgia = sumtienhang - sumthanhtoan;

        $('#TotalValOut').val(sumtienhang);
        $('#TotalValOutDesc').val(sumgiamgia);
        $('#TotalValOutAfterDesc').val(sumthanhtoan);

        FormatNumber('#TotalValOut', 2);
        FormatNumber('#TotalValOutDesc', 2);
        FormatNumber('#TotalValOutAfterDesc', 2);
    }
}


function TongTienExpCross() {
    var sumtienhang = 0.0;
    var sumgiamgia = 0.0;
    var sumthanhtoan = 0.0;
    if ($('tbody#table-tbodyID').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            var tr = $(this);
            var idx = tr.attr('idx');
            var strgiaban = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].SellProduct"]').val();
            var strQtyRoot = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].Qty"]').val();
            var strQtyOut = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].QtyOut"]').val();
            var strDiscountPrice = tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].DiscountPrice"]').val();
            if (parseFloat(strQtyOut) > parseFloat(strQtyRoot)) {
                alert('Số lượng xuất > Số lượng ban đầu');
                tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].QtyOut"]').val(strQtyRoot);
                tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].QtyOut"]').focus();
                strQtyOut = strQtyRoot;
            }

            var giaban = parseFloat(strgiaban);
            var qty = parseFloat(strQtyOut);
            var discountprice = parseFloat(strDiscountPrice);
            var valamout = (giaban - discountprice) * qty;
            tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].ValAmount"]').val(valamout);
            var qtyRemain = parseFloat(strQtyRoot) - qty;
            tr.find('input[name="Lst_InvF_InventoryOutDtl[' + idx + '].QtyRemain"]').val(qtyRemain);
            //

            var tienhang = giaban * qty;
            sumtienhang += tienhang;

            sumthanhtoan += valamout;
        });

        sumgiamgia = sumtienhang - sumthanhtoan;

        $('#TotalValOut').val(sumtienhang);
        $('#TotalValOutDesc').val(sumgiamgia);
        $('#TotalValOutAfterDesc').val(sumthanhtoan);

        FormatNumber('#TotalValOut', 2);
        FormatNumber('#TotalValOutDesc', 2);
        FormatNumber('#TotalValOutAfterDesc', 2);
    }
}

function FormatNumber(thiz, numerLetter) {
    $(thiz).number(true, numerLetter);
}

function StartScanXacThuc() {
    alert("Bắt đầu chuyển sang chế độ quét");
    $('#QRNoSearch').focus();
}

function StartScanProduct(thiz) {
    //alert("Bắt đầu chuyển sang chế độ quét");    
    //$('#ProductCode').hide();
    //$('#ProductScan').removeAttr('hidden');
    //$('#ProductScan').focus();
    
    var checkdisplay = $('#ProductScan').css("display");
    if (checkdisplay === "none") {
        alert("Bắt đầu chuyển chế độ quét.");
        $('#ProductScan').css("display", "");
        $('#ProductScan').focus();
        $('#ProductCode').next(".select2-container").hide();
        $('#btnScanProduct').html("Dừng quét");
        return;
    }
    else {
        alert("Bắt đầu tắt chế độ quét.");
        $('#ProductScan').css("display", "none");
        $('#ProductCode').next(".select2-container").show();
        $('#btnScanProduct').html("Quét");
        return;
    }        
}

function Product_Scan(thiz, url) {
    debugger;
    var productcode = $(thiz).val();
    //var productUser = $(thiz).find('option:selected').attr('productcodeuser');
    if (productcode === "") {
        return;
    }    

    // Người dùng quét mã ProductCodeUser
    AddProductByScan($('#rowtemplateProduct'), $('#table-tbodyID'), "", url, "black", productcode);
}

function ShowLoDetail(if_invoutno, productCode, url, viewonly, productcodebase, valconvert) {
    var invBUPattern = "";
    var invcodeout = $('#InvCodeOut').val();

    if (invcodeout === "") {
        alert("Kho xuất chưa được chọn");
        $('#InvCodeOut').focus();
        return;
    }
    var optSelect = $('#InvCodeOut').find('option:selected');
    invBUPattern = $(optSelect).attr("invBUPattern");

    debugger;
    var listLot = [];
    var rowsgetlot = $("tbody#table-detailLot tr.trdata").length;
    if (rowsgetlot > 0) {
        var trArr = $('tbody#table-detailLot tr.trdata');
        if (trArr !== null && trArr.length > 0) {
            for (var i = 0; i < trArr.length; i++) {
                var trCur = trArr[i];
                if (trCur !== null && trCur !== undefined) {
                    var idx = $(trCur).attr('idx');
                    var prdCodeCur = $(trCur).find('input[type="hidden"][name="Lst_InvF_InventoryOutLotDtl[' + idx + '].ProductCode"]').val();
                    if (productCode !== prdCodeCur) {
                        continue;
                    }

                    var lotCur = {};
                    lotCur.ProductCode = prdCodeCur;
                    lotCur.ProductLotNo = $(trCur).find('input[type="hidden"][name="Lst_InvF_InventoryOutLotDtl[' + idx + '].ProductLotNo"]').val();
                    lotCur.Qty = $(trCur).find('input[type="hidden"][name="Lst_InvF_InventoryOutLotDtl[' + idx + '].Qty"]').val();
                    lotCur.InvCodeOutActual = $(trCur).find('input[type="hidden"][name="Lst_InvF_InventoryOutLotDtl[' + idx + '].InvCodeOutActual"]').val();

                    listLot.push(lotCur);
                }
            }
        }
    }

    var objListLot = commonUtils.returnJSONValue(listLot);

    $.ajax({
        url: url,
        data: {
            IF_InvOutNo: if_invoutno,
            ProductCode: productCode,
            invBUPattern: invBUPattern,
            viewonly: viewonly,
            ProductCodeBase: productcodebase,
            valconvert: valconvert,
            listLot: objListLot
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



function Delete(IF_InvOutNo, url, InvCodeOut, InvOutType, CustomerCode) {
    if (IF_InvOutNo === "") {
        alert("Số phiếu xuất không tồn tại.");
        return;
    }
    var objRQ_InvF_InventoryOut = new Object();
    
    var objInvF_InventoryOut = new Object();
    objInvF_InventoryOut.IF_InvOutNo = IF_InvOutNo;
    objInvF_InventoryOut.InvCodeOut = InvCodeOut;
    objInvF_InventoryOut.InvOutType = InvOutType;
    objInvF_InventoryOut.CustomerCode = CustomerCode;
    objInvF_InventoryOut.TotalValOut = 0;
    objInvF_InventoryOut.TotalValOutDesc = 0;
    objInvF_InventoryOut.TotalValOutAfterDesc = 0;
    objRQ_InvF_InventoryOut.InvF_InventoryOut = objInvF_InventoryOut;
    
    bootbox.confirm("Đồng ý xóa phiếu xuất kho " + IF_InvOutNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: url,
                data: {
                    model: JSON.stringify(objRQ_InvF_InventoryOut),
                    flagIsDelete: "1"
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

//function Approve(IF_InvOutNo, url) {
//    if (IF_InvOutNo === "") {
//        alert("Số phiếu xuất không tồn tại.");
//        return;
//    }   

//    bootbox.confirm("Đồng ý duyệt phiếu xuất kho " + IF_InvOutNo + "?", function (result) {
//        if (result) {
//            $.ajax({
//                url: url,
//                data: {
//                    IF_InvOutNo: IF_InvOutNo
//                },
//                type: 'post',
//                dataType: 'json',
//                traditional: true,
//                success: function (data) {
//                    if (data.Success) {
//                        alert(data.Messages);
//                        if (data.RedirectUrl !== null) {
//                            window.location.href = data.RedirectUrl;
//                        }
//                    } else {
//                        showErrorDialog(data.Detail);
//                    }
//                }
//            });
//        }
//    });
//}
function CheckSelect() {
    var lengthCheck = $('#table-tbodyID').find('input.inputcheck:checked').length;
    if (lengthCheck === 0) {
        alert("Không có phiếu xuất nào được chọn");
        return false;
    }
    return true;
}
function Approve(urlApprove) {
    debugger;
    var check = CheckSelect();
    if (check === false) return;
    var arrIF_InvOutNo = [];
    var listIF_InvOutNo = [];
    var stop = false;
    $('#table-tbodyID').find('input.inputcheck:checked').each(function () {
        var checkbox = $(this);
        var IF_InvOutNo = checkbox.attr('IF_InvOutNo');
        var status = checkbox.attr('status');
        if (status !== "PENDING") {
            alert("Trạng thái phiếu xuất " + IF_InvOutNo+ " không hợp lệ.");
            stop = true;
            return false;
        }
        var $tr = $(this).parent().parent();
        iF_InvOutNo = commonUtils.returnValue($tr.find('input[type="hidden"][name="IF_InvOutNo"]').val());
        remark = commonUtils.returnValue($tr.find('input[type="hidden"][name="Remark"]').val());
        var objInvF_InventoryOut = {
            IF_InvOutNo: iF_InvOutNo,
            Remark: remark,
        };
        listIF_InvOutNo.push(objInvF_InventoryOut);
        arrIF_InvOutNo.push(iF_InvOutNo);
    });
    if (stop === true) return;
    var arrIFInvOutNo = JSON.stringify(arrIF_InvOutNo);
    var lstIF_InvOutNo = JSON.stringify(listIF_InvOutNo);

    bootbox.confirm("Đồng ý duyệt phiếu xuất kho " + arrIFInvOutNo + " ?", function (result) {
        if (result) {
            $.ajax({
                url: urlApprove,
                data: {
                    //IF_InvOutNo: IF_InvOutNo
                    lstIF_InvOutNo: lstIF_InvOutNo
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

function Edit(url) {
    window.location.href = url;
}

function Cancel(urlCanCel) {
    var check = CheckSelect();
    if (check === false) return;
    var arrIF_InvOutNo = [];
    var listIF_InvOutNo = [];
    var stop = false;
    $('#table-tbodyID').find('input.inputcheck:checked').each(function () {
        var checkbox = $(this);
        var IF_InvOutNo = checkbox.attr('IF_InvOutNo');
        var status = checkbox.attr('status');
        if (status !== "PENDING") {
            alert("Trạng thái phiếu xuất " + IF_InvOutNo + " không hợp lệ.");
            stop = true;
            return false;
        }

        iF_InvOutNo = commonUtils.returnValue($tr.find('input[type="hidden"][name="IF_InvOutNo"]').val());
        remark = commonUtils.returnValue($tr.find('input[type="hidden"][name="Remark"]').val());
        var objInvF_InventoryOut = {
            IF_InvOutNo: iF_InvOutNo,
            Remark: remark,
        };
        listIF_InvOutNo.push(objInvF_InventoryOut);
        arrIF_InvOutNo.push(iF_InvOutNo);
    });
    if (stop === true) return;
    var arrIFInvOutNo = JSON.stringify(arrIF_InvOutNo);
    var lstIF_InvOutNo = JSON.stringify(listIF_InvOutNo);

    bootbox.confirm("Đồng ý hủy phiếu xuất kho " + lstIF_InvOutNo + " ?", function (result) {
        if (result) {
            $.ajax({
                url: urlCanCel,
                data: {
                    //IF_InvOutNo: IF_InvOutNo
                    lstIF_InvOutNo: lstIF_InvOutNo
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

function DeleteMulti(urlDelete) {
    var check = CheckSelect();
    if (check === false) return;
    var arrInvF_InventoryOut = [];
    var stop = false;
    var str = "";
    $('#table-tbodyID').find('input.inputcheck:checked').each(function () {
        var checkbox = $(this);
        var IF_InvOutNo = checkbox.attr('IF_InvOutNo');
        if (str !== "") {
            str += "," + IF_InvOutNo;
        }
        else {
            str += IF_InvOutNo;
        }
        var status = checkbox.attr('status');
        var InvCodeOut = checkbox.attr('InvCodeOut');
        var InvOutType = checkbox.attr('InvOutType');
        var CustomerCode = checkbox.attr('CustomerCode');
        if (status !== "PENDING") {
            alert("Trạng thái phiếu xuất " + IF_InvOutNo + " không hợp lệ.");
            stop = true;
            return false;
        }        
        var objInvF_InventoryOut = new Object();
        objInvF_InventoryOut.IF_InvOutNo = IF_InvOutNo;
        objInvF_InventoryOut.InvCodeOut = InvCodeOut;
        objInvF_InventoryOut.InvOutType = InvOutType;
        objInvF_InventoryOut.CustomerCode = CustomerCode;
        objInvF_InventoryOut.TotalValOut = 0;
        objInvF_InventoryOut.TotalValOutDesc = 0;
        objInvF_InventoryOut.TotalValOutAfterDesc = 0;

        arrInvF_InventoryOut.push(objInvF_InventoryOut);
    });
    if (stop === true) return;
    var lstInvF_InventoryOut = JSON.stringify(arrInvF_InventoryOut);

    bootbox.confirm("Đồng ý xóa phiếu xuất kho " + str + " ?", function (result) {
        if (result) {
            $.ajax({
                url: urlDelete,
                data: {                    
                    lstInvF_InventoryOut: lstInvF_InventoryOut
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

function ShowDetail(url) {
    window.location.href = url;
}

function ChangeXuatTuDH(thiz) {
    var checked = $(thiz).prop('checked');
    if (checked === true) {
        $('#OrderNo').removeAttr("readonly");
    }
    else {
        $('#OrderNo').attr("readonly", "readonly");
        $('#CustomerCode').removeAttr("readonly");       
    }
}

function FillCustomerOrder(thiz, urlGetOrderDetail) {
    debugger;
    var refno = $(thiz).val();
    var reftype = "";
    if (refno === "") {
        //alert("Đơn hàng chưa được nhập.");
        $('#RefType').val('');
        $('#RefNoSys').val('');
        $('#myInput').show();
        return;
    }
    else if (refno.length > 3) {
        var listPrefix = ['RO-', 'ODL', 'OSO'];
        var strPrefix = refno.substring(0, 3);
        if (!listPrefix.includes(strPrefix)) {
            alert("Số RefNo không hợp lệ!");
            $(thiz).val('');
            $(thiz).focus();
            $('#RefType').val('');
            $('#RefNoSys').val('');
            return;
        }
        else {
            if (strPrefix === 'RO-') {
                $('#RefType').val('RO');
                reftype = 'RO';
                refno = refno.substring(3);
            }
            if (strPrefix === 'ODL') {
                $('#RefType').val('ORDERDL');
                reftype = 'ORDERDL';
            }
            if (strPrefix === 'OSO') {
                $('#RefType').val('ORDERSO');
                reftype = 'ORDERSO';
            }
        }
    }
    else {
        alert("Số RefNo không hợp lệ!");
        $(thiz).val('');
        $(thiz).focus();
        $('#RefType').val('');
        $('#RefNoSys').val('');
        return;
    }

    $.ajax({
        url: urlGetOrderDetail,
        data: {
            RefNo: refno,
            RefType: reftype
        },
        type: 'post',
        dataType: 'json',
        traditional: true,
        success: function (data) {
            if (data.Success) {
                var result = data.data;
                if (result.length === 0) {
                    alert("Số RefNo không tồn tại trong hệ thống.");
                    return;
                }
                var order = result[0];
                var CustomerCode = order.CustomerCodeSys;

                var RefNoSys = "";
                var QtyInvOutAvail = "";
                if (reftype === 'RO') {
                    RefNoSys = order.RONoSys;
                    $('#InvFCFOutCode03').val(order.PlateNo);//Fill biển số
                }
                else {
                    RefNoSys = order.OrderNoSys;
                    QtyInvOutAvail = order.QtyInvOutAvail;
                }
                
                if (CustomerCode !== undefined && CustomerCode !== "") {
                    $('#CustomerCode').val(CustomerCode);
                    $('#CustomerCode').trigger('change');
                    //$('#CustomerCode').attr("readonly", "readonly");
                    $('#CustomerCode').attr("disabled", "disabled");
                }
                
                $('#RefNoSys').val(RefNoSys);
                $('#QtyInvOutAvail').val(QtyInvOutAvail);

                $('#myInput').hide();

                // Xóa danh sách hàng hóa đang tồn tại trong lưới Nâng cấp 2020-08-06
                $('#table-tbodyID').html('');
                //


                // Load lại danh sách sản phẩm theo đơn hàng bán hàng
                //var htmlOption = "";
                //if (result !== undefined) {
                //    for (var i = 0; i < result.length; i++) {
                //        debugger;
                //        var productcodebase = result[i].ProductCodeBase;
                //        var productcoderoot = result[i].ProductCodeRoot;
                //        var productcode = result[i].ProductCode;
                //        var flagLo = result[i].FlagLo;
                //        var flagSerial = result[i].FlagSerial;
                //        var flagCombo = result[i].FlagCombo;
                //        var productname = result[i].ProductName;
                //        var productcodeuser = result[i].ProductCodeUser;
                //        var unitcode = result[i].UnitCode;    
                //        var valconvert = result[i].ValConvert;  
                //        var _QtyInvOutAvail = result[i].QtyInvOutAvail;  
                        
                //        var opt = '<option QtyOrder="' + _QtyInvOutAvail + '" QtyInvOutAvail="' + _QtyInvOutAvail + '" productcodeuser="' + productcodeuser + '" valconvert="' + valconvert + '" productcodebase="' + productcodebase + '" productcoderoot="' + productcoderoot + '" value="' + productcode + '" flaglo="' + flagLo + '" flagserial="' + flagSerial + '" flagcombo="' + flagCombo + '" invcode="" productcode="' + productcode + '" productname="' + productname + '" unitcode="' + unitcode + '" sellprice="0" sellorder="0" qtytotalok="0" discountprice="0">' + productcodeuser + ' - ' + productname + ' (' + unitcode + ')</option>';
                //        htmlOption += opt;
                //    }
                //}
                //htmlOption += "<option value=\"Search\">Tìm kiếm</option>";
                //$('#ProductCode').html(htmlOption);
                //$('#ProductCode').val("");
                //$('#ProductCode').trigger('change');
                //
                
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
}

function Search(urlSearch) {
    $('#manageForm').attr('action', urlSearch).submit();
    return;    
}

function Export(urlExport) {
    var check = CheckSelect();
    if (check === false) return;
    // Lấy danh sách phiếu 
    var lstIF_InvOutNo = [];
    $('#table-tbodyID').find('input.inputcheck:checked').each(function () {
        var checkbox = $(this);
        var IF_InvOutNo = checkbox.attr('IF_InvOutNo');
        lstIF_InvOutNo.push(IF_InvOutNo);
    });
    //

    var objlstIF_InvOutNo = JSON.stringify(lstIF_InvOutNo);

    var recordcount = commonUtils.returnValueText('#recordcount');
    var pagecur = commonUtils.returnValueText('#page');
    var formdata = new window.FormData($('#manageForm')[0]);
    formdata.append("page", pagecur);
    formdata.append("recordcount", recordcount);      
    formdata.append("lstIF_InvOutNo", objlstIF_InvOutNo);     

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

function ShowComboAppr(IF_InvOutNo, productCode/*, ProductCodeBase*/, urlGetCombo, ProductCodeUser, ProductName) {
    //var url = '@Url.Action("Combo", "ModalCommon")';
    $.ajax({
        url: urlGetCombo,
        data: {
            productCode: productCode,
            //productCodeBase: ProductCodeBase,
            productCodeUser: ProductCodeUser,
            productName: ProductName,
            IF_InvOutNo: IF_InvOutNo
        },
        type: 'post',
        dataType: 'json',
        traditional: true,
        success: function (data) {
            if (data.Success) {

                $('#ShowPopupCombo').modal({
                    backdrop: false,
                    keyboard: true
                });
                $("#ShowPopupCombo").html(data.Html); // truyen html vao #form
                var display = $("#ShowPopupCombo").css('display');
                if (display === "none") {
                    $("#ShowPopupCombo").show();
                }
            } else {
                showErrorDialog(data.Detail);
            }
        }
    });
}

function CheckAll() {
    var ischeck = $('#checkAll').prop('checked');
    if (ischeck === true) {
        $('#table-tbodyID').find('input.inputcheck').prop("checked", true);
    }
    else {
        $('#table-tbodyID').find('input.inputcheck').prop("checked", false);
    }
}








