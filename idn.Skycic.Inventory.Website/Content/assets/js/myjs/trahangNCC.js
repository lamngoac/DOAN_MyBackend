function AddProductToList(thiz, urlSearch, colortext) {
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
        AddProduct($('#rowtemplateProduct'), $('#table-tbodyID'), urlSearch, colortext);
    }
}

function TongTien() {
    var sumtienhang = 0.0;
    var sumgiamgia = 0.0;
    var sumthanhtoan = 0.0;
    if ($('tbody#table-tbodyID').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            debugger;
            var tr = $(this);
            var idx = tr.attr('idx');
            var strUPReturnSup = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].UPReturnSup"]').val();
            var strQty = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].Qty"]').val();                   

            var giatrahang = parseFloat(strUPReturnSup);
            var qty = parseFloat(strQty);            
            var valamout = giatrahang * qty;
            tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].ValReturnSup"]').val(valamout);
            //

            var tienhang = giatrahang * qty;
            sumtienhang += tienhang;

            sumthanhtoan += valamout;
        });

        sumgiamgia = sumtienhang - sumthanhtoan;

        $('#TotalValReturnSup').val(sumtienhang);
        $('#TotalValReturnSupDesc').val(sumgiamgia);
        $('#TotalValReturnSupAfterDesc').val(sumthanhtoan);

        FormatNumber('#TotalValReturnSup', 2);
        FormatNumber('#TotalValReturnSupDesc', 2);
        FormatNumber('#TotalValReturnSupAfterDesc', 2);
    }
}

function FormatNumber(thiz, numerLetter) {
    $(thiz).number(true, numerLetter);
}

function checkForm(){

    var listError = [];
    listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#IF_InvReturnSupNo', 'has-error-fix', 'Số trả hàng NCC không được trống!');
    listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeOut', 'has-error-fix', 'Chưa chọn kho xuất!');
    listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerCode', 'has-error-fix', 'Chưa chọn nhà cung cấp!');

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
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#IF_InvInNo', 'has-error-fix', 'Chưa nhập số phiếu nhập!');
    }

    var rowsgetbom = $("tbody#table-tbodyID tr.trdata").length;
    if (rowsgetbom > 0) {
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

function SaveData(flagredirect, url) {
    debugger;
    if (!checkForm()) {
        return;
    }

    var objInvF_InventoryReturnSupSave = new Object();

    var objInvF_InventoryReturnSup = new Object();
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

    var Lst_InvF_InventoryReturnSupDtl = [];    
    var Lst_InvF_InventoryReturnSupInstLot = [];
    var Lst_InvF_InventoryReturnSupInstSerial = [];
   

    //Lấy chi tiết hàng hóa
    var check = true;
    if ($('tbody#table-tbodyID').length > 0) {
        $("tbody#table-tbodyID tr.trdata").each(function () {
            var tr = $(this);
            var idx = $(tr).attr('idx');
            var strInvCodeOutActual = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].InvCodeOutActual"]').val();
            var strProductCode = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].ProductCode"]').val();
            var strQty = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].Qty"]').val();
            var selectdonvi = tr.find('select[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].UnitCode"]');
            var strUnitCode = $(selectdonvi).val();
            var strRemark = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].Remark"]').val();
            var strUPIn = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].UPIn"]').val();
            var strUPReturnSup = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].UPReturnSup"]').val();
            var strValReturnSup = tr.find('input[name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].ValReturnSup"]').val();
            var flagLot = tr.find('input[type="hidden"][name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].FlagLot"]').val();
            var flagSerial = tr.find('input[type="hidden"][name="Lst_InvF_InventoryReturnSupDtl[' + idx + '].FlagSerial"]').val();

            var arrInvCodeOutActual = strInvCodeOutActual.split(/[\s,]+/);
            if (flagLot == '1') {
                if (arrInvCodeOutActual.length > 0) {
                    for (var iInv = 0; iInv < arrInvCodeOutActual.length; iInv++) {

                        var sumqty = 0.0;
                        var invCodeOutActualCur = arrInvCodeOutActual[iInv];

                        $('#table-detailLot').find('tr[productCode="' + strProductCode + '"]').each(function () {
                            var tr = $(this);
                            var idx = $(tr).attr('idx');
                            var strqty = tr.find('input[type="hidden"][name="Lst_InvF_InventoryOutLotDtl[' + idx + '].Qty"]').val();
                            var invCodeOutActualLot = tr.find('input[type="hidden"][name="Lst_InvF_InventoryOutLotDtl[' + idx + '].InvCodeOutActual"]').val();
                            if (invCodeOutActualCur === invCodeOutActualLot) {
                                var qty = parseFloat(strqty);
                                sumqty += qty;
                            }
                        });

                        var temDtlCur = {};
                        temDtlCur.InvCodeOutActual = invCodeOutActualCur;
                        temDtlCur.ProductCode = strProductCode;
                        temDtlCur.Qty = sumqty;
                        temDtlCur.UnitCode = strUnitCode;
                        temDtlCur.UPIn = strUPIn;
                        temDtlCur.UPReturnSup = strUPReturnSup;
                        temDtlCur.ValReturnSup = sumqty * parseFloat(strUPReturnSup);
                        temDtlCur.Remark = strRemark;
                        Lst_InvF_InventoryReturnSupDtl.push(temDtlCur);
                    }
                }

                return;
            }
            else if (flagSerial == '1') {
                if (arrInvCodeOutActual.length > 0) {
                    for (var iInv = 0; iInv < arrInvCodeOutActual.length; iInv++) {
                        var sumqty = 0.0;
                        var invCodeOutActualCur = arrInvCodeOutActual[iInv];

                        $('#table-detailSerial').find('tr[productCode="' + strProductCode + '"]').each(function () {
                            var tr = $(this);
                            var idx = $(tr).attr('idx');

                            var invCodeOutActualSerial = tr.find('input[type="hidden"][name="Lst_InvF_InventoryOutSerialDtl[' + idx + '].InvCodeOutActual"]').val();
                            if (invCodeOutActualCur === invCodeOutActualSerial) {
                                sumqty += 1;
                            }
                        });

                        var temDtlCur = {};
                        temDtlCur.InvCodeOutActual = invCodeOutActualCur;
                        temDtlCur.ProductCode = strProductCode;
                        temDtlCur.Qty = sumqty;
                        temDtlCur.UnitCode = strUnitCode;
                        temDtlCur.UPIn = strUPIn;
                        temDtlCur.UPReturnSup = strUPReturnSup;
                        temDtlCur.ValReturnSup = sumqty * parseFloat(strUPReturnSup);
                        temDtlCur.Remark = strRemark;
                        Lst_InvF_InventoryReturnSupDtl.push(temDtlCur);
                    }
                }

                return;
            }

            if (arrInvCodeOutActual.length > 0) {
                debugger
                $('#table-detailInvCodeOutActual').find('tr[productcode="' + strProductCode + '"]').each(function () {
                    var tr = $(this);
                    var idx = $(tr).attr('idx');
                    debugger
                    var invCodeOutActualDtl = tr.find('input[type="hidden"][name="Lst_InvF_InventoryReturnSupDtlTemp[' + idx + '].InvCodeOutActual"]').val();
                    var qtyDtl = tr.find('input[type="hidden"][name="Lst_InvF_InventoryReturnSupDtlTemp[' + idx + '].Qty"]').val();
                    if (qtyDtl === undefined || qtyDtl === '0' || qtyDtl === '') {
                        return;
                    }

                    var objInvF_InventoryOutDtl = {};
                    objInvF_InventoryOutDtl.InvCodeOutActual = invCodeOutActualDtl;
                    objInvF_InventoryOutDtl.ProductCode = strProductCode;
                    objInvF_InventoryOutDtl.Qty = qtyDtl;
                    objInvF_InventoryOutDtl.UnitCode = strUnitCode;
                    objInvF_InventoryOutDtl.Remark = strRemark;
                    objInvF_InventoryOutDtl.UPIn = strUPIn;
                    objInvF_InventoryOutDtl.UPReturnSup = strUPReturnSup;
                    objInvF_InventoryOutDtl.ValReturnSup = parseFloat(qtyDtl) * parseFloat(strUPReturnSup);

                    Lst_InvF_InventoryReturnSupDtl.push(objInvF_InventoryOutDtl);
                });
            }
        });
    }
    if (check === false) return;

    // Thông tin Lot
    if ($('tbody#table-detailLot').length > 0) {
        var qtylot = $("tbody#table-detailLot tr.trdata").length;
        $("tbody#table-detailLot tr.trdata").each(function () {
            var tr = $(this);
            var x = tr.attr("idx");
            var objInvF_InventoryOutInstLot = new Object();

            var strInvCodeOutActual0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].InvCodeOutActual"]').val();
            var strProductCode0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].ProductCode"]').val();
            var strQty0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].Qty"]').val();
            var strProductLotNo0 = tr.find('input[name="Lst_InvF_InventoryOutLotDtl[' + x + '].ProductLotNo"]').val();

            objInvF_InventoryOutInstLot.InvCodeOutActual = strInvCodeOutActual0;
            objInvF_InventoryOutInstLot.ProductCode = strProductCode0;
            objInvF_InventoryOutInstLot.ProductLotNo = strProductLotNo0;
            objInvF_InventoryOutInstLot.Qty = strQty0;

            Lst_InvF_InventoryReturnSupInstLot.push(objInvF_InventoryOutInstLot);
        });
    }
    //

    // Thông tin Serial
    if ($('tbody#table-detailSerial').length > 0) {
        //var qtySerial = $("tbody#table-detailSerial tr.trdata").length;        
        $("tbody#table-detailSerial tr.trdata").each(function () {
            var tr = $(this);
            var z = tr.attr("idx");
            var strInvCodeOutActual3 = tr.find('input[name="Lst_InvF_InventoryOutSerialDtl[' + z + '].InvCodeOutActual"]').val();
            var strProductCode3 = tr.find('input[name="Lst_InvF_InventoryOutSerialDtl[' + z + '].ProductCode"]').val();
            var strSerialNo3 = tr.find('input[name="Lst_InvF_InventoryOutSerialDtl[' + z + '].SerialNo"]').val();

            var objInvF_InventoryOutInstSerial = new Object();
            objInvF_InventoryOutInstSerial.InvCodeOutActual = strInvCodeOutActual3;
            objInvF_InventoryOutInstSerial.ProductCode = strProductCode3;
            objInvF_InventoryOutInstSerial.SerialNo = strSerialNo3;

            Lst_InvF_InventoryReturnSupInstSerial.push(objInvF_InventoryOutInstSerial);
        });
    }
    //
    
    // Gán các list vào đối tượng request
    objInvF_InventoryReturnSupSave.InvF_InventoryReturnSup = objInvF_InventoryReturnSup;    
    objInvF_InventoryReturnSupSave.Lst_InvF_InventoryReturnSupDtl = Lst_InvF_InventoryReturnSupDtl;
    objInvF_InventoryReturnSupSave.Lst_InvF_InventoryReturnSupInstLot = Lst_InvF_InventoryReturnSupInstLot;
    objInvF_InventoryReturnSupSave.Lst_InvF_InventoryReturnSupInstSerial = Lst_InvF_InventoryReturnSupInstSerial;
    objInvF_InventoryReturnSupSave.FlagRedirect = flagredirect;
    objInvF_InventoryReturnSupSave.FlagIsDelete = '0';
    //

    var objJson = JSON.stringify(objInvF_InventoryReturnSupSave);

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

function Approve1(IF_InvReturnSupNo, url) {
    if (IF_InvReturnSupNo === "") {
        alert("Số YC trả hàng không tồn tại.");
        return;
    }

    bootbox.confirm("Đồng ý duyệt YC trả hàng " + IF_InvReturnSupNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: url,
                data: {
                    IF_InvReturnSupNo: IF_InvReturnSupNo
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

function Search(urlSearch) {
    debugger;
    $('#manageForm').attr('action', urlSearch).submit();
    return;
}

function Delete1(IF_InvReturnSupNo, url, InvCodeOut, CustomerCode) {
    if (IF_InvReturnSupNo === "") {
        alert("Số YC trả hàng không tồn tại.");
        return;
    }
    var objInvF_InventoryReturnSupSave = new Object();
   
    var objInvF_InventoryReturnSup = new Object();
    objInvF_InventoryReturnSup.IF_InvReturnSupNo = IF_InvReturnSupNo;
    objInvF_InventoryReturnSup.InvCodeOut = InvCodeOut;    
    objInvF_InventoryReturnSupSave.FlagIsDelete = "1";

    // Mặc định để ko lỗi
    objInvF_InventoryReturnSup.CustomerCode = CustomerCode;
    objInvF_InventoryReturnSup.TotalValReturnSup = 0;
    objInvF_InventoryReturnSup.TotalValReturnSupDesc = 0;
    objInvF_InventoryReturnSup.TotalValReturnSupAfterDesc = 0;
    //
    objInvF_InventoryReturnSupSave.InvF_InventoryReturnSup = objInvF_InventoryReturnSup;

    bootbox.confirm("Đồng ý xóa YC trả hàng " + IF_InvReturnSupNo + "?", function (result) {
        if (result) {
            $.ajax({
                url: url,
                data: {
                    model: JSON.stringify(objInvF_InventoryReturnSupSave)                    
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

var FlagScan = '0';

function InvF_InventoryReturnSup() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };

    this.deleteData = function (listInvF_InventoryReturnSup) {
        debugger;
        if (listInvF_InventoryReturnSup.length > 0) {
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
                            objlistinvf_inventoryreturnsup: commonUtils.returnJSONValue(listInvF_InventoryReturnSup),
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
            alert("Chưa chọn phiếu trả hàng cần xóa!");
            return;
        }
    };

    this.approveData = function (listInvF_InventoryReturnSup) {

        if (listInvF_InventoryReturnSup.length > 0) {
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
                            objlistinvf_inventoryreturnsup: commonUtils.returnJSONValue(listInvF_InventoryReturnSup),
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
            alert("Chưa chọn phiếu trả hàng cần duyệt!");
            return;
        }
    };

    this.cancelData = function (listInvF_InventoryReturnSup) {

        if (listInvF_InventoryReturnSup.length > 0) {
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
                            objlistinvf_inventoryreturnsup: commonUtils.returnJSONValue(listInvF_InventoryReturnSup),
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
            alert("Chưa chọn phiếu trả hàng cần huỷ!");
            return;
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
}

function ChangeTraTuPN(thiz) {
    var checked = $(thiz).prop('checked');
    if (checked === true) {
        $('#IF_InvInNo').removeAttr("readonly");
        $('#IF_InvInNo').focus();
        $('#divPrdInvIn').show();
        $('#myInput').hide();
        $('#quetmavach').hide();
        FlagScan = '0';
    }
    else {
        $('#IF_InvInNo').val('');
        $('#IF_InvInNo').attr("readonly", "readonly");
        $('#divPrdInvIn').hide();
        $('#myInput').show();
        $('#quetmavach').hide();
        FlagScan = '0';
    }
}