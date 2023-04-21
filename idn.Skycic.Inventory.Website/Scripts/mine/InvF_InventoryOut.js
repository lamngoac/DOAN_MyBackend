function InvF_InventoryOut() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.doSearch = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        doSearchData(_ajaxSettings);
    };

    this.checkForm = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#IF_InvOutNo', 'has-error-fix', 'Số phiếu xuất không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvOutType', 'has-error-fix', 'Chưa chọn loại xuất kho!');
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

    };

    function doSearchData(_ajaxSettings) {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var IF_InvOutNo = commonUtils.returnValueText('#IF_InvOutNo');
        var createdtimefrom = commonUtils.returnValueText('#createdtimefrom');
        var createdtimeto = commonUtils.returnValueText('#createdtimeto');
        var approvedtimefrom = commonUtils.returnValueText('#approvedtimefrom');
        var approvedtimeto = commonUtils.returnValueText('#approvedtimeto');
        var InvOutType = commonUtils.returnValueText('#InvOutType');
        var CustomerCode = commonUtils.returnValueText('#CustomerCode');
        var InvCodeOut = commonUtils.returnValueText('#InvCodeOut');
        //var OrgID = commonUtils.returnValueText('#OrgID');
        var refNo = commonUtils.returnValueText('#refNo');
        var profileStatus = commonUtils.returnValueText('#profileStatus');
        var ckpending = commonUtils.returnValueCheckBox('#chk-pending');
        var ckapprove = commonUtils.returnValueCheckBox('#chk-approved');
        var ckcancle = commonUtils.returnValueCheckBox('#chk-canceled');
        var pagecur = commonUtils.returnValueText('#page');
        var recordcount = commonUtils.returnValueText('#recordcount');

        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token
                , IF_InvOutNo: IF_InvOutNo
                , InvOutType: InvOutType
                , CustomerCode: CustomerCode
                , createdtimefrom: createdtimefrom
                , createdtimeto: createdtimeto
                , approvedtimefrom: approvedtimefrom
                , approvedtimeto: approvedtimeto
                , InvCodeOut: InvCodeOut
                , profileStatus: profileStatus
                , refNo: refNo
                , pending: ckpending
                , approved: ckapprove
                , canceled: ckcancle
                , recordcount: recordcount
                , page: pagecur
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
            $('#BindDataInFInventoryOut').html('');
            $('#BindDataInFInventoryOut').html(objResult.Html);
            initDefault();
            //$('.numberic').number(true, 2);
    var tableName = 'InvF_InventoryOut';
              var totalValOutAfterDescFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'TotalValOutAfterDesc');
        $('.numberic').number(true, totalValOutAfterDescFormat);
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
        approve(_ajaxSettings);
    }


    this.approveDataDetail = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        approveDetail(_ajaxSettings);
    }

    function approve(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu xuất nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            debugger
            var arrIF_InvOutNo = [];
            var listIF_InvOutNo = [];
            var stop = false;
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
                var checkbox = $(this);
                var IF_InvOutNo = checkbox.attr('IF_InvOutNo');
                var status = checkbox.attr('status');
                if (status !== "PENDING") {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu xuất ' + '' + IF_InvOutNo + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
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
            }); if (stop === true) return;
            var arrIFInvOutNo = JSON.stringify(arrIF_InvOutNo);
            var lstIF_InvOutNo = JSON.stringify(listIF_InvOutNo);
            bootbox.confirm("Đồng ý duyệt phiếu xuất kho " + arrIFInvOutNo + " ?", function (result) {
                if (result) {
                    $.ajax({
                        url: _ajaxSettings.Url,
                        data: {
                            //IF_InvOutNo: IF_InvOutNo
                            lstIF_InvOutNo: lstIF_InvOutNo
                        },
                        type: _ajaxSettings.Type,
                        dataType: _ajaxSettings.DataType,
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
    }

    function approveDetail(_ajaxSettings) {
        debugger
        var arrIF_InvOutNo = [];
        var listIF_InvOutNo = [];
        var iF_InvOutNo = $('#IF_InvOutNo').val();
        var remark = $('textarea#Remark').val();
        var objInvF_InventoryOut = {
            IF_InvOutNo: iF_InvOutNo,
            Remark: remark,
        };
        listIF_InvOutNo.push(objInvF_InventoryOut);
        arrIF_InvOutNo.push(iF_InvOutNo);

        var arrIFInvOutNo = JSON.stringify(arrIF_InvOutNo);
        var lstIF_InvOutNo = JSON.stringify(listIF_InvOutNo);
        $.ajax({
            url: _ajaxSettings.Url,
            data: {
                //IF_InvOutNo: IF_InvOutNo
                lstIF_InvOutNo: lstIF_InvOutNo
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
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



    this.cancelData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        cancle(_ajaxSettings);

    }

    this.cancelDataDetail = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        cancleDetail(_ajaxSettings);
    }

    function cancle(_ajaxSettings) {
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu xuất nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            debugger
            var arrIF_InvOutNo = [];
            var listIF_InvOutNo = [];
            var stop = false;
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
                var checkbox = $(this);
                var IF_InvOutNo = checkbox.attr('IF_InvOutNo');
                var status = checkbox.attr('status');
                if (status !== "PENDING") {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu xuất ' + IF_InvOutNo + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
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
            bootbox.confirm("Đồng ý hủy phiếu xuất kho " + arrIFInvOutNo + " ?", function (result) {
                if (result) {
                    debugger
                    $.ajax({
                        url: _ajaxSettings.Url,
                        data: {

                            lstIF_InvOutNo: lstIF_InvOutNo
                        },
                        type: _ajaxSettings.Type,
                        dataType: _ajaxSettings.DataType,
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
    }

    function cancleDetail(_ajaxSettings) {
        debugger
        var arrIF_InvOutNo = [];
        var listIF_InvOutNo = [];
        var iF_InvOutNo = $('#IF_InvOutNo').val();
        var remark = $('textarea#Remark').val();
        var objInvF_InventoryOut = {
            IF_InvOutNo: iF_InvOutNo,
            Remark: remark,
        };
        listIF_InvOutNo.push(objInvF_InventoryOut);
        arrIF_InvOutNo.push(iF_InvOutNo); 
        var arrIFInvOutNo = JSON.stringify(arrIF_InvOutNo);
        var lstIF_InvOutNo = JSON.stringify(listIF_InvOutNo);
        $.ajax({
            url: _ajaxSettings.Url,
            data: {

                lstIF_InvOutNo: lstIF_InvOutNo
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
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


    this.deleteMulti = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        deletemulti(_ajaxSettings);
    }
    this.deleteDetail = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        deleteDetail1(_ajaxSettings);
    }

    function deletemulti(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu xuất nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            debugger
            var arrInvF_InventoryOut = [];
            var stop = false;
            var str = "";
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
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
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu xuất ' + IF_InvOutNo + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
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
                        url: _ajaxSettings.Url,
                        data: {
                            lstInvF_InventoryOut: lstInvF_InventoryOut
                        },
                        type: _ajaxSettings.Type,
                        dataType: _ajaxSettings.DataType,
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
    }


    function deleteDetail1(_ajaxSettings) {
        debugger
        var arrInvF_InventoryOut = [];
        var iF_InvOutNo = $('#IF_InvOutNo').val();
        var remark = $('textarea#Remark').val();
        var InvCodeOut = $('#InvCodeOut').val();
        var InvOutType = $('#InvOutType').val();
        var CustomerCode = $('#CustomerCode').val();
        var objInvF_InventoryOut = new Object();
        objInvF_InventoryOut.IF_InvOutNo = iF_InvOutNo;
        objInvF_InventoryOut.InvCodeOut = InvCodeOut;
        objInvF_InventoryOut.InvOutType = InvOutType;
        objInvF_InventoryOut.CustomerCode = CustomerCode;
        objInvF_InventoryOut.TotalValOut = 0;
        objInvF_InventoryOut.TotalValOutDesc = 0;
        objInvF_InventoryOut.TotalValOutAfterDesc = 0;
        arrInvF_InventoryOut.push(objInvF_InventoryOut);
        var lstInvF_InventoryOut = JSON.stringify(arrInvF_InventoryOut);
        $.ajax({
            url: _ajaxSettings.Url,
            data: {
                lstInvF_InventoryOut: lstInvF_InventoryOut
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
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

    this.Export = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        export1(_ajaxSettings);
    }
    function export1(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu xuất nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            debugger
            var lstIF_InvOutNo = [];
            $('#table-tbodyID').find('input.idn-checkbox:checked').each(function () {
                var checkbox = $(this);
                var IF_InvOutNo = checkbox.attr('IF_InvOutNo');
                lstIF_InvOutNo.push(IF_InvOutNo);
            });
            var objlstIF_InvOutNo = JSON.stringify(lstIF_InvOutNo);

            var recordcount = commonUtils.returnValueText('#recordcount');
            var pagecur = commonUtils.returnValueText('#page');
            var formdata = new window.FormData($('#manageForm')[0]);
            formdata.append("page", pagecur);
            formdata.append("recordcount", recordcount);
            formdata.append("lstIF_InvOutNo", objlstIF_InvOutNo);
            $.ajax({
                url: _ajaxSettings.Url,
                data: formdata,
                processData: false,
                contentType: false,
                dataType: _ajaxSettings.DataType,
                type: _ajaxSettings.Type,
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
    }

    function printPdf(linkPdf) {
        debugger
        printJS({
            printable: linkPdf,
            type: 'pdf',
            showModal: true,
            //onPrintDialogClose: () => console.log('The print dialog was closed'),
            //onPdfOpen: () => console.log('Pdf was opened in a new tab due to an incompatible browser')
        })
    }

    this.Print = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        print(_ajaxSettings);
    }

    function print(_ajaxSettings) {
        debugger
        var listError = [];
        var lengthCheck = $('#table-tbodyID').find('input.idn-checkbox:checked').length;
        if (lengthCheck === 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu xuất nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (lengthCheck > 1) {
            var objToastr1 = {
                ToastrType: 'error',
                ToastrMessage: 'Chỉ chọn 1 phiếu xuất kho!'
            };
            listError.push(objToastr1);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            //return false;
        }
        else {
            debugger
            var IF_InvOutNo = '';
            var checkItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
            $(checkItem).each(function () {
                IF_InvOutNo = $(this).attr('IF_InvOutNo');
                return false;
            });
            $.ajax({
                type: _ajaxSettings.Type,
                data:
                {
                    iF_InvOutNo: IF_InvOutNo
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

    this.getData = function (flagRedirect) {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var objInvF_InventoryOut = {};
        var Lst_InvF_InventoryOutDtl = [];
        var Lst_InvF_InventoryOutCover = [];
        var Lst_InvF_InventoryOutInstLot = [];
        var Lst_InvF_InventoryOutInstSerial = [];
        var Lst_InvF_InventoryOutQR = [];
        var Lst_InvF_InventoryOutAttachFile = [];

        //Lst_InvF_InventoryOutDtl = returnLst_InvF_InventoryOutDtl();
        debugger
        Obj_InvF_InventoryOutCover = returnLst_InvF_InventoryOutCover();
        debugger
        if (Obj_InvF_InventoryOutCover !== undefined && Obj_InvF_InventoryOutCover !== null) {
            Lst_InvF_InventoryOutCover = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutCover;
           Lst_InvF_InventoryOutDtl = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutDtl;
            Lst_InvF_InventoryOutInstLot = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutInstLot;
            Lst_InvF_InventoryOutInstSerial = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutInstSerial;
            Lst_InvF_InventoryOutAttachFile = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutAttachFile;
        }
        //Lst_InvF_InventoryOutInstLot = returnLst_InvF_InventoryOutInstLot();
        //Lst_InvF_InventoryOutInstSerial = returnLst_InvF_InventoryOutInstSerial();
        Lst_InvF_InventoryOutQR = returnLst_InvF_InventoryOutQR();
        //thong tin master
        debugger
        var IF_InvOutNo = $('#IF_InvOutNo').val();
        var InvOutType = $('#InvOutType').val();
        var InvCodeOut = $('#InvCodeOut').val();
        var CustomerCode = $('#CustomerCode').val();
        var RefNo = $('#RefNo').val();
        var RefType = $('#refType').val();
        if (RefType === 'RO') {
          /*  RefNo = RefNo.substring(3); */   //Nếu là RO thì cắt 3 ký tự đầu 'RO-'
        }
        var RefNoSys = $('#RefNoSys').val();
        var TotalValOut = $('#TotalValOut').val();
        var TotalValOutDesc = $('#TotalValOutDesc').val();
        var TotalValOutAfterDesc = $('#TotalValOutAfterDesc').val();
        var Remark = $('#Remark').val();
        var invFCFOutCode01 = $('#InvFCFOutCode01').val();
        var invFCFOutCode02 = $('#InvFCFOutCode02').val();
        var invFCFOutCode03 = $('#InvFCFOutCode03').val();
        var profileStatus = $('#profileStatus').val();


        var AreaCode = $('#AreaCode').val();
        var ShippingCustomerCode = $('#ShippingCustomerCode').val();
        var ShippingAreaCode = $('#ShippingAreaCode').val();
        




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
        objInvF_InventoryOut.ProfileStatus = profileStatus;
        objInvF_InventoryOut.AreaCode = AreaCode;
        objInvF_InventoryOut.ShippingCustomerCode = ShippingCustomerCode;
        objInvF_InventoryOut.ShippingAreaCode = ShippingAreaCode;

        var tem = {};
        tem.InvF_InventoryOut = objInvF_InventoryOut;
        tem.Lst_InvF_InventoryOutCover = Lst_InvF_InventoryOutCover;
        tem.Lst_InvF_InventoryOutDtl = Lst_InvF_InventoryOutDtl;
        tem.Lst_InvF_InventoryOutInstLot = Lst_InvF_InventoryOutInstLot;
        tem.Lst_InvF_InventoryOutInstSerial = Lst_InvF_InventoryOutInstSerial;
        tem.Lst_InvF_InventoryOutQR = Lst_InvF_InventoryOutQR;
        tem.Lst_InvF_InventoryOutAttachFile = Lst_InvF_InventoryOutAttachFile;
        //tem.FlagRedirect = flagRedirect;
        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;

    }


    this.getDataUpdate = function (flagRedirect) {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var objInvF_InventoryOut = {};
        var Lst_InvF_InventoryOutDtl = [];
        var Lst_InvF_InventoryOutCover = [];
        var Lst_InvF_InventoryOutInstLot = [];
        var Lst_InvF_InventoryOutInstSerial = [];
        var Lst_InvF_InventoryOutQR = [];
        var Lst_InvF_InventoryOutAttachFile = [];

        //Lst_InvF_InventoryOutDtl = returnLst_InvF_InventoryOutDtl();
        debugger
        Obj_InvF_InventoryOutCover = returnLst_InvF_InventoryOutCover();
        debugger
        if (Obj_InvF_InventoryOutCover !== undefined && Obj_InvF_InventoryOutCover !== null) {
            Lst_InvF_InventoryOutCover = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutCover;
            Lst_InvF_InventoryOutDtl = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutDtl;
            Lst_InvF_InventoryOutInstLot = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutInstLot;
            Lst_InvF_InventoryOutInstSerial = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutInstSerial;
            Lst_InvF_InventoryOutAttachFile = Obj_InvF_InventoryOutCover.Lst_InvF_InventoryOutAttachFile;
        }
        //Lst_InvF_InventoryOutInstLot = returnLst_InvF_InventoryOutInstLot();
        //Lst_InvF_InventoryOutInstSerial = returnLst_InvF_InventoryOutInstSerial();
        Lst_InvF_InventoryOutQR = returnLst_InvF_InventoryOutQR();
        //thong tin master
        debugger
        var IF_InvOutNo = $('#IF_InvOutNo').val();
        var InvOutType = $('#InvOutType').val();
        var InvCodeOut = $('#InvCodeOut').val();
        var CustomerCode = $('#CustomerCode').val();
        var RefNo = $('#RefNo').val();
        var RefType = $('#refType').val();
        if (RefType === 'RO') {
            RefNo = RefNo.substring(3);  //Nếu là RO thì cắt 3 ký tự đầu 'RO-'
        }
        var RefNoSys = $('#RefNoSys').val();
        var TotalValOut = $('#TotalValOut').val();
        var TotalValOutDesc = $('#TotalValOutDesc').val();
        var TotalValOutAfterDesc = $('#TotalValOutAfterDesc').val();
        var Remark = $('#Remark').val();
        var invFCFOutCode01 = $('#InvFCFOutCode01').val();
        var invFCFOutCode02 = $('#InvFCFOutCode02').val();
        var invFCFOutCode03 = $('#InvFCFOutCode03').val();


        var AreaCode = $('#AreaCode').val();
        var ShippingCustomerCode = $('#ShippingCustomerCode').val();
        var ShippingAreaCode = $('#ShippingAreaCode').val();



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


        objInvF_InventoryOut.AreaCode = AreaCode;
        objInvF_InventoryOut.ShippingCustomerCode = ShippingCustomerCode;
        objInvF_InventoryOut.ShippingAreaCode = ShippingAreaCode;


        var tem = {};
        tem.InvF_InventoryOut = objInvF_InventoryOut;
        tem.Lst_InvF_InventoryOutCover = Lst_InvF_InventoryOutCover;
        tem.Lst_InvF_InventoryOutDtl = Lst_InvF_InventoryOutDtl;
        tem.Lst_InvF_InventoryOutInstLot = Lst_InvF_InventoryOutInstLot;
        tem.Lst_InvF_InventoryOutInstSerial = Lst_InvF_InventoryOutInstSerial;
        tem.Lst_InvF_InventoryOutQR = Lst_InvF_InventoryOutQR;
        tem.Lst_InvF_InventoryOutAttachFile = Lst_InvF_InventoryOutAttachFile;
        //tem.FlagRedirect = flagRedirect;
        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;

    }



    //lay thong tin hang hoa chi tiet
    function returnLst_InvF_InventoryOutCover() {
        debugger
        var Lst_InvF_InventoryOutCover = [];
        var Lst_InvF_InventoryOutDtl = [];
        var Lst_InvF_InventoryOutInstLot = [];
        var Lst_InvF_InventoryOutInstSerial = [];
        var objInventoryOutDtl = {};
        var temDtlCur = {};
        var IF_InvOutNo = $('#IF_InvOutNo').val();
        var rowsgetprd = $("tbody#table-tbodyID.GetPrd tr.trdata").length;
        if (rowsgetprd > 0) {
            var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
            if (trArr !== null && trArr != undefined) {
                for (var i = 0; i < trArr.length; i++) {
                    var $trCur = trArr[i];
                    if ($trCur !== null && $trCur !== undefined) {
                        var productCode = $($trCur).attr('productcode');
                        temDtlCur.ProductCode = productCode;

                        debugger
                        var rd = $($trCur).attr('rd');
                        var divCur = productCode;
                        if (divCur !== null && divCur !== undefined) {
                            var productCode = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcode-' + rd).val();
                            var $trcur1 = $('tbody#table-tbodyID').find('tr[productcode="' + productCode + '"]');
                            var $divProducts = $trcur1.find('div.products-list');
                            var $divProductSelected = $divProducts.find('div[productcode="' + productCode + '"]');
                            var productCodeRoot = $divProductSelected.find('input.ProductCodeRoot').val();

                            var productCodeBase = $divProductSelected.find('input.ProductCodeBase').val();
                            //var productCodeRoot = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcoderoot-' + rd).val();

                            var Qty = $('tbody#table-tbodyID.GetPrd tr td').find('input.qty-' + rd).val();
                            var flagLot = $('tbody#table-tbodyID.GetPrd tr td').find('input.flaglot-' + rd).val();
                            var flagSerial = $('tbody#table-tbodyID.GetPrd tr td').find('input.flagserial-' + rd).val();
                            var flagCombo = $('tbody#table-tbodyID.GetPrd tr td').find('input.flagcombo-' + rd).val();
                            var productName = $('tbody#table-tbodyID.GetPrd tr td').find('input.productname-' + rd).val();
                            var productUser = $('tbody#table-tbodyID.GetPrd tr td').find('input.productcodeuser-' + rd).val();
                            var unitCode = $('tbody#table-tbodyID.GetPrd tr td.td-select2 .select2-' + rd).val();
                            var remark = $('tbody#table-tbodyID.GetPrd tr td').find('textarea.remark-' + rd).val();
                            var UPOut = $('tbody#table-tbodyID.GetPrd tr td').find('input.upout-' + rd).val();
                            var UPOutDesc = $('tbody#table-tbodyID.GetPrd tr td').find('input.upoutdesc-' + rd).val();
                            var UPInv = $('tbody#table-tbodyID.GetPrd tr td').find('input.upinv-' + rd).val();
                            debugger
                            if (flagLot === '1') {
                                debugger
                                var sumqty = 0.0;
                                var listVT = [];
                                var $trLot = $('#table-detailLot').find('tr[productcode="' + productCode + '"]');
                                if ($trLot.length > 0) {
                                    $trLot.each(function () {
                                        var tr = $(this);
                                        var idx = $(tr).attr('idx');
                                        var trdata = $('tbody#table-detailLot').find('tr.trdata[idx = "' + idx + '"]');

                                        var strqty = trdata.find('input.Qty').val();

                                        var qty = parseFloat(strqty);
                                        sumqty += qty;
                                        debugger
                                        if (qty > 0) {
                                            var remark = "";
                                            var objLot = {
                                                InvCodeOutActual: trdata.find('input.InvCodeInActual').val(),
                                                ProductCodeRoot: productCodeRoot,
                                                ProductCode: productCode,
                                                ProductLotNo: trdata.find('input.ProductLotNo').val(),
                                                Qty: trdata.find('input.Qty').val(),
                                                ProductionDate: trdata.find('input.ProductionDate').val(),
                                            };
                                            var objViTri = {
                                                InvCodeOutActual: trdata.find('input.InvCodeInActual').val()
                                            }

                                            Lst_InvF_InventoryOutInstLot.push(objLot);
                                            listVT.push(trdata.find('input.InvCodeInActual').val());
                                        }
                                    });


                                    var uniqueNames = [];
                                    $.each(listVT, function (i, el) {
                                        if ($.inArray(el, uniqueNames) === -1) uniqueNames.push(el);
                                    });
                                    for (var j = 0; j < uniqueNames.length; j++) {
                                        var sumqty1 = 0.0;
                                        var $trcur = $('#table-detailLot').find('tr[invcodeinactual="' + uniqueNames[j] + '"]');
                                        if ($trcur.length > 0) {
                                            $trcur.each(function () {
                                                var trd = $(this);
                                                var strqTyCur = trd.find('input.Qty').val();
                                                qtycur = parseFloat(strqTyCur);
                                                sumqty1 += qtycur;

                                            });
                                            debugger
                                            var objLotCur = {
                                                InvCodeOutActual: uniqueNames[j],
                                                ProductCodeRoot: productCodeRoot,
                                                ProductCode: productCode,
                                                Qty: sumqty1,
                                                Remark: remark,
                                                UnitCode: unitCode,
                                            };
                                            Lst_InvF_InventoryOutDtl.push(objLotCur)
                                        }
                                    }

                                    var strremak = "";
                                    var temDtlCurLot = {};
                                    temDtlCurLot.ProductCodeRoot = productCodeRoot;
                                    temDtlCurLot.ProductCode = productCode;
                                    temDtlCurLot.Qty = sumqty;
                                    temDtlCurLot.UnitCode = unitCode;
                                    temDtlCurLot.Remark = remark;
                                    temDtlCurLot.UPOut = UPOut;
                                    temDtlCurLot.UPOutDesc = UPOutDesc;
                                    temDtlCurLot.ValOutAfterDesc = sumqty * (parseFloat(UPOut) - parseFloat(UPOutDesc));
                                    temDtlCurLot.UPInv = UPInv;
                                    Lst_InvF_InventoryOutCover.push(temDtlCurLot);
                                }
                            }
                            if (flagSerial === '1') {
                                debugger
                                var sumqty = 0.0;
                                var listVTSerial = [];
                                var $trSerial = $('#table-detailSerial').find('tr[productcode="' + productCode + '"]');
                                var qty1 = $trSerial.length;
                                if ($trSerial.length > 0) {
                                    $trSerial.each(function () {
                                
                                        var tr = $(this);
                                        var idx = $(tr).attr('idx');
                                        var trdata = $('tbody#table-detailSerial').find('tr.trdata[idx = "' + idx + '"]');

                                        debugger
                                        var remark = "";
                                        
                                        var objSerial1 = {
                                            InvCodeOutActual: trdata.find('input.InvCodeInActual').val(),
                                            ProductCodeRoot: productCodeRoot,
                                            ProductCode: productCode,
                                            SerialNo: trdata.find('input.SerialNo').val(),
                                        };
                                        
                                        Lst_InvF_InventoryOutInstSerial.push(objSerial1);
                                        listVTSerial.push(trdata.find('input.InvCodeInActual').val());
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
                                            InvCodeOutActual: uniqueNames[k],
                                            ProductCodeRoot: productCodeRoot,
                                            ProductCode: productCode,
                                            Qty: $trcur.length,
                                            Remark: remark,
                                            UnitCode: unitCode,
                                        };
                                        Lst_InvF_InventoryOutDtl.push(objSerialCur)
                                    }


                                    var strremak = "";
                                    var temDtlCurSerial = {};
                                    temDtlCurSerial.ProductCodeRoot = productCodeRoot;
                                    temDtlCurSerial.ProductCode = productCode;
                                    temDtlCurSerial.Qty = qty1;
                                    temDtlCurSerial.UnitCode = unitCode;
                                    temDtlCurSerial.Remark = remark;
                                    temDtlCurSerial.UPOut = UPOut;
                                    temDtlCurSerial.UPOutDesc = UPOutDesc;
                                    temDtlCurSerial.ValOutAfterDesc = qty1 * (parseFloat(UPOut) - parseFloat(UPOutDesc));
                                    temDtlCurSerial.UPInv = UPInv;
                                    Lst_InvF_InventoryOutCover.push(temDtlCurSerial);
                                }
                            }
                            debugger
                            if (flagCombo === '1') {
                                var $trCombo = $('#table-detailCombo').find('tr[productcoderoot="' + productCode + '"]');


                                debugger
                                var $trcurHH = $('#table-detailInvCodeOutActual').find('tr[productcoderoot="' + productCode + '"]');
                                var result = $('#table-detailInvCodeOutActual tr td div.products-list-cache .result[productcoderoot="' + productCode + '"]');

                                if ($trcurHH.length > 0) {

                                    for (var iresult = 0; iresult < result.length; iresult++) {
                                        //var remark = '';
                                        var productCode = $(result[iresult]).find('input.ProductCode').val();
                                        var productCodeRoot = $(result[iresult]).find('input.ProductCodeRoot').val();
                                        var qTy = $(result[iresult]).find('input.Qty').val();
                                        var invCodeOutActual = $(result[iresult]).find('input.InvCodeInActual').val();
                                        var unitCode = $(result[iresult]).find('input.UnitCode').val();
                                    
                                        var objCombocur = {
                                            InvCodeOutActual: invCodeOutActual,
                                            ProductCodeRoot: productCodeRoot,
                                            ProductCode: productCode,
                                            Qty: qTy,
                                            UnitCode: unitCode,
                                            Remark: remark
                                        }

                                        Lst_InvF_InventoryOutDtl.push(objCombocur);
                                    }

                                }

                                var strremak = "";
                                var temDtlCurCombo = {};
                                temDtlCurCombo.ProductCodeRoot = productCodeRoot;
                                temDtlCurCombo.ProductCode = productCode;
                                temDtlCurCombo.Qty = Qty;
                                temDtlCurCombo.UnitCode = unitCode;
                                temDtlCurCombo.Remark = remark;
                                temDtlCurCombo.UPOut = UPOut;
                                temDtlCurCombo.UPOutDesc = UPOutDesc;
                                temDtlCurCombo.ValOutAfterDesc = Qty * (parseFloat(UPOut) - parseFloat(UPOutDesc));
                                temDtlCurCombo.UPInv = UPInv;
                                Lst_InvF_InventoryOutCover.push(temDtlCurCombo);


                            }

                            else if (flagLot !== '1' && flagSerial !== '1') {
                                debugger 
                                //var $trcurHH = $('#table-detailInvCodeOutActual').find('tr[productcodebase="' + productCodeBase + '"]');
                                //var result = $('#table-detailInvCodeOutActual tr td div.products-list-cache .result[productcode="' + productCode + '"]');
                                var $trcurHH = $('#table-detailInvCodeOutActual').find('tr[productcode="' + productCode + '"]');
                                var result = $trcurHH.find('td div.products-list-cache .result[productcode="' + productCode + '"]');
                                if ($trcurHH.length > 0) {
                                    var sumqty = 0.0;
                                    for (var iresult = 0; iresult < result.length; iresult++) {
                                       
                                        var strqty = $(result[iresult]).find('input.Qty').val();

                                        var qty = parseFloat(strqty);
                                        sumqty += qty;
                                        debugger
                                        if (qty > 0) {
                                            //var remark = "";
                                            var objDTL = {
                                                InvCodeOutActual: $(result[iresult]).find('input.InvCodeInActual').val(),
                                                ProductCodeRoot: productCodeRoot,
                                                ProductCode: productCode,
                                                Qty: $(result[iresult]).find('input.Qty').val(),
                                                Remark: remark,
                                                UnitCode: unitCode
                                            }
                                            Lst_InvF_InventoryOutDtl.push(objDTL);
                                        }

                                    }
                                    var strremak = "";
                                    var temDtlCurHHDTL = {};
                                    temDtlCurHHDTL.ProductCodeRoot = productCodeRoot;
                                    temDtlCurHHDTL.ProductCode = productCode;
                                    temDtlCurHHDTL.Qty = sumqty;
                                    temDtlCurHHDTL.UnitCode = unitCode;
                                    temDtlCurHHDTL.Remark = remark;
                                    temDtlCurHHDTL.UPOut = UPOut;
                                    temDtlCurHHDTL.UPOutDesc = UPOutDesc;
                                    temDtlCurHHDTL.ValOutAfterDesc = sumqty * (parseFloat(UPOut) - parseFloat(UPOutDesc));
                                    temDtlCurHHDTL.UPInv = UPInv;
                                    Lst_InvF_InventoryOutCover.push(temDtlCurHHDTL);
                                }
                            }
                        }



                    }
                }


            }
            debugger
            var Lst_InvF_InventoryOutAttachFileUI = [];
            var rows12 = $('tbody#tbody-Order_FileUpload tr.trdata').length;
            if (rows12 > 0) {
                $('tbody#tbody-Order_FileUpload tr.trdata').each(function () {
                    var $tr = $(this);
                    var flagFileUpload = $tr.attr('flagfileupload');
                    var idx = $tr.attr('idx');
                    var filePath = commonUtils.returnValue($tr.find('input.filePath').val());
                    var fileId = commonUtils.returnValue($tr.find('input.fileId').val());
                    var fileName = commonUtils.returnValue($tr.find('input.fileName').val());
                    var description = commonUtils.returnValue($tr.find('input.descript').val());
                    if (flagFileUpload == '1') {
                        var objOrd_OrderSRFilesUploadUI = {
                            Idx: idx,
                            IF_InvOutNo: IF_InvOutNo,
                            FlagIsFilePath: flagFileUpload,
                            AttachFilePath: filePath,
                            AttachFileName: fileName,
                            AttachFileDesc: description
                        };
                        Lst_InvF_InventoryOutAttachFileUI.push(objOrd_OrderSRFilesUploadUI);
                    }
                    else {
                        var objOrd_OrderSRFilesUploadUI = {
                            Idx: idx,
                            IF_InvOutNo: IF_InvOutNo,
                            FlagIsFilePath: flagFileUpload,
                            AttachFileSpec: fileId,
                            AttachFileName: fileName,
                            AttachFileDesc: description
                        };
                        Lst_InvF_InventoryOutAttachFileUI.push(objOrd_OrderSRFilesUploadUI);
                    }
                  
                });
            }

            objInventoryOutDtl.Lst_InvF_InventoryOutCover = Lst_InvF_InventoryOutCover;
            objInventoryOutDtl.Lst_InvF_InventoryOutDtl = Lst_InvF_InventoryOutDtl;
            objInventoryOutDtl.Lst_InvF_InventoryOutInstLot = Lst_InvF_InventoryOutInstLot;
                objInventoryOutDtl.Lst_InvF_InventoryOutInstSerial = Lst_InvF_InventoryOutInstSerial;
                objInventoryOutDtl.Lst_InvF_InventoryOutAttachFile = Lst_InvF_InventoryOutAttachFileUI;
        }
        return objInventoryOutDtl;
    }

    //function returnLst_InvF_InventoryOutDtl() {

    //}
    //function returnLst_InvF_InventoryOutInstLot() {
    //    debugger
    //    var Lst_InvF_InventoryOutInstLot = [];
    //    var rowslot = $('tbody#table-detailLot tr.trdata').length;
    //    if (rowslot > 0) {
    //        $('tbody#table-detailLot tr.trdata').each(function () {
    //            var $tr = $(this);
    //            if ($tr !== null && $tr !== undefined) {
    //                var idx = $(tr).attr('idx');
    //                var trdata = $('tbody#table-detailLot').find('tr.trdata[idx = "' + idx + '"]');
    //            }
    //        });
    //    }
    //}

    //function returnLst_InvF_InventoryOutInstSerial() {

    //}


    function returnLst_InvF_InventoryOutQR() {
        debugger
        var Lst_InvF_InventoryOutQR = [];
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
        return Lst_InvF_InventoryOutQR;
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



    this.getDataExpCross = function () {
        debugger

        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
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
        //var AreaCode = $('#AreaCode').val();

        //var ShippingCustomerCode = $('#ShippingCustomerCode').val();
        //var ShippingAreaCode = $('#ShippingAreaCode').val();


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
        //objInvF_InventoryOut.AreaCode = AreaCode;
        //objInvF_InventoryOut.ShippingCustomerCode = ShippingCustomerCode;
        //objInvF_InventoryOut.ShippingAreaCode = ShippingAreaCode;


        var Lst_InvF_InventoryOutCover = [];

        debugger
        //lấy chi tiết hàng hoá
        var $trcur = $('tbody#table-tbodyID.GetPrd tr.trdata');
        if ($trcur.length > 0) {
            $trcur.each(function () {
                var tr = $(this);
                var rd = $(tr).attr('rd');


                var strProductCode = $(tr).attr('productcode');
                var $divProducts = tr.find('div.products-list');
                var $divProductSelected = $divProducts.find('div[productcode="' + strProductCode + '"]');
                var strProductCodeRoot = $divProductSelected.find('input.ProductCodeRoot').val();

                var strQtyOut = tr.find('input.qtyout-' + rd).val();
                var unitCode = $('tbody#table-tbodyID.GetPrd tr td.td-select2 .select2-' + rd).val();
                var strRemark = tr.find('textarea.remark-' + rd).val();
                var strUPOut = tr.find('input.upout-' + rd).val();
                var strUPOutDesc = tr.find('input.upoutdesc-' + rd).val();
                var strValOutAfterDesc = tr.find('input.valoutafterdesc-' + rd).val();
                var objInvF_InventoryOutCover = new Object();


                objInvF_InventoryOutCover.ProductCodeRoot = strProductCodeRoot;
                objInvF_InventoryOutCover.ProductCode = strProductCode;
                objInvF_InventoryOutCover.Qty = strQtyOut;
                objInvF_InventoryOutCover.UnitCode = unitCode;
                objInvF_InventoryOutCover.Remark = strRemark;
                objInvF_InventoryOutCover.UPOut = strUPOut;
                objInvF_InventoryOutCover.UPOutDesc = strUPOutDesc;
                objInvF_InventoryOutCover.ValOutAfterDesc = strValOutAfterDesc;

                Lst_InvF_InventoryOutCover.push(objInvF_InventoryOutCover);

            });
        }


        //lấy file upload

        var Lst_InvF_InventoryOutAttachFileUI = [];
        var rows12 = $('tbody#tbody-Order_FileUpload tr.trdata').length;
        if (rows12 > 0) {
            $('tbody#tbody-Order_FileUpload tr.trdata').each(function () {
                var $tr = $(this);
                var flagFileUpload = $tr.attr('flagfileupload');
                var idx = $tr.attr('idx');
                var filePath = commonUtils.returnValue($tr.find('input.filePath').val());
                var fileId = commonUtils.returnValue($tr.find('input.fileId').val());
                var fileName = commonUtils.returnValue($tr.find('input.fileName').val());
                var description = commonUtils.returnValue($tr.find('input.descript').val());
                var objOrd_OrderSRFilesUploadUI = {
                    Idx: idx,
                    IF_InvOutNo: IF_InvOutNo,
                    FlagIsFilePath: flagFileUpload,
                    AttachFileSpec: fileId,
                    AttachFileName: fileName,
                    AttachFileDesc: description
                };
                Lst_InvF_InventoryOutAttachFileUI.push(objOrd_OrderSRFilesUploadUI);
            });
        }




        objRQ_InvF_InventoryOut.InvF_InventoryOut = objInvF_InventoryOut;
        objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutCover = Lst_InvF_InventoryOutCover;
        objRQ_InvF_InventoryOut.Lst_InvF_InventoryOutAttachFile = Lst_InvF_InventoryOutAttachFileUI;
        var modelCur = commonUtils.returnJSONValue(objRQ_InvF_InventoryOut);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;


    }


    this.SaveDataExpCross = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataExpCross();
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

    function failFunction(jqXHR, textStatus, errorThrown) {
    };
    function alwaysFunction() {
    };


}