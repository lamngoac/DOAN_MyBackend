function InvF_MoveOrd() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.formatNumber = function () {
        format_Number();
    };
    function format_Number() {
        var listMst_ColumnConfig = commonInventory.ListMst_ColumnConfig;

        //Số lượng điều chuyển màn tạo mới
        var objQtyMoveOrdFormat = {};
        objQtyMoveOrdFormat.TableName = 'InvF_MoveOrd';
        objQtyMoveOrdFormat.ColumnName = 'Qty';
        var columnQtyMoveOrd = objMst_ColumnConfig.returnValueColumnFormat(listMst_ColumnConfig, objQtyMoveOrdFormat);        
        $(".qty-moveord").each(function () {
            commonUtils.formatNumberByJqueryNumber($(this), columnQtyMoveOrd);
        });

        //Số lượng tồn popup chọn vị trí nhập/xuất
        var objQtyTotalOKFormat = {};
        objQtyTotalOKFormat.TableName = 'Inv_InventoryBalance';
        objQtyTotalOKFormat.ColumnName = 'QtyTotalOK';
        var columnQtyTotalOK = objMst_ColumnConfig.returnValueColumnFormat(listMst_ColumnConfig, objQtyTotalOKFormat);
        commonUtils.formatNumberByJqueryNumber('.qtytotalok', columnQtyTotalOK);
        
    };
    this.checkForm = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeOut', 'has-error-fix', 'Chưa chọn kho xuất!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#InvCodeIn', 'has-error-fix', 'Chưa chọn kho nhập!');

        //???
        if (lst_InvF_MoveOrdDtl.length === 0
            && lst_InvF_MoveOrdInstLot.legnth === 0
            && lst_InvF_MoveOrdInstSerial.length === 0) {
            let objToastr = {
                ToastrType: 'error', ToastrMessage: "Không có hàng hoá điều chuyển!"
            };
            listError.push(objToastr);
        }

        //check lưới hàng hóa trống
        if ($("#Lst_InvF_MoveOrdProduct tr.trdata").length <= 0) {
            let objToastr = {
                ToastrType: 'error', ToastrMessage: "Không có hàng hoá điều chuyển!"
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var objInvF_MoveOrd = new Object();
        var objRQ_InvF_MoveOrd = new Object();
        objInvF_MoveOrd.IF_MONo = commonUtils.returnValueText('#IF_MONo');
        objInvF_MoveOrd.InvCodeIn = commonUtils.returnValueText('#InvCodeIn');
        objInvF_MoveOrd.InvCodeOut = commonUtils.returnValueText('#InvCodeOut');
        objInvF_MoveOrd.Remark = commonUtils.returnValueText('#Remark');
        objRQ_InvF_MoveOrd.InvF_MoveOrd = objInvF_MoveOrd;
        objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl = [];
        objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot = [];
        objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial = [];

        // Gán remark, unitcode cho từng product
        for (let InvF_MoveOrdDtl of lst_InvF_MoveOrdDtl) {
            let productCode = InvF_MoveOrdDtl['ProductCode'];
            let remark = $('#Lst_InvF_MoveOrdProduct tr.trdata[ProductCode="' + productCode + '"] textarea[name="Remark"]').val();
            let unitCode = $('#Lst_InvF_MoveOrdProduct tr.trdata[ProductCode="' + productCode + '"]').attr('unitcode');
            if (InvF_MoveOrdDtl.LstMoveOrd_Dtl != undefined && InvF_MoveOrdDtl.LstMoveOrd_Dtl.length > 0) {
                for (let dtl of InvF_MoveOrdDtl.LstMoveOrd_Dtl) {
                    dtl.Remark = remark;
                    dtl.UnitCode = unitCode;
                }
            }
            // Merge array by spread op
            objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl = [...objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl, ...InvF_MoveOrdDtl.LstMoveOrd_Dtl];
        }

        // Gán remark, unitcode cho từng lot
        for (let InvF_MoveOrdInstLot of lst_InvF_MoveOrdInstLot) {
            let productCode = InvF_MoveOrdInstLot['ProductCode'];
            let remark = $('#Lst_InvF_MoveOrdProduct tr.trdata[ProductCode="' + productCode + '"] textarea[name="Remark"]').val();
            let unitCode = $('#Lst_InvF_MoveOrdProduct tr.trdata[ProductCode="' + productCode + '"]').attr('unitcode');
            if (InvF_MoveOrdInstLot.LstMoveOrd_Lot != undefined && InvF_MoveOrdInstLot.LstMoveOrd_Lot.length > 0) {
                for (const lot of InvF_MoveOrdInstLot.LstMoveOrd_Lot) {
                    lot.Remark = remark;
                    lot.UnitCode = unitCode;
                }
            }

            // Distinc theo productcode, kho nhập, kho xuất --> add vào list Dtl Lst_InvF_MoveOrdDtl
            if (InvF_MoveOrdInstLot.LstMoveOrd_LotDistinc != undefined && InvF_MoveOrdInstLot.LstMoveOrd_LotDistinc.length > 0) {
                for (const lotDistinc of InvF_MoveOrdInstLot.LstMoveOrd_LotDistinc) {
                    let invF_MoveOrdInstLot_Cur = lst_InvF_MoveOrdInstLot
                        .filter(function (el) { return el.ProductCode == productCode });
                    let lstMoveOrd_LotCur = invF_MoveOrdInstLot_Cur[0].LstMoveOrd_Lot
                        .filter(function (el) {
                            return el.ProductCode == lotDistinc.ProductCode
                                && el.InvCodeIn == lotDistinc.InvCodeIn
                                && el.InvCodeOut == lotDistinc.InvCodeOut
                        })
                    let qtySum = lstMoveOrd_LotCur.reduce((qty, objInvf) => Number(qty) + (Number(objInvf['Qty']) || 0), 0);
                    lotDistinc.Remark = remark;
                    lotDistinc.UnitCode = unitCode;
                    lotDistinc.Qty = qtySum;
                }
                objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl = [...objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl, ...InvF_MoveOrdInstLot.LstMoveOrd_LotDistinc]
            }
            // Merge array
            objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot = [...objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstLot, ...InvF_MoveOrdInstLot.LstMoveOrd_Lot];
        }


        // Gán remark, unitcode cho từng serial
        for (let InvF_MoveOrdInstSerial of lst_InvF_MoveOrdInstSerial) {
            let productCode = InvF_MoveOrdInstSerial['ProductCode'];
            let remark = $('#Lst_InvF_MoveOrdProduct tr.trdata[ProductCode="' + productCode + '"] textarea[name="Remark"]').val();
            let unitCode = $('#Lst_InvF_MoveOrdProduct tr.trdata[ProductCode="' + productCode + '"]').attr('unitcode');
            if (InvF_MoveOrdInstSerial.LstMoveOrd_Serial != undefined && InvF_MoveOrdInstSerial.LstMoveOrd_Serial.length > 0) {
                for (let serial of InvF_MoveOrdInstSerial.LstMoveOrd_Serial) {
                    serial.Remark = remark;
                    serial.UnitCode = unitCode;
                    serial.Qty = "1";
                }
            }

            // Distinc theo productcode, kho nhập, kho xuất --> add vào list Dtl Lst_InvF_MoveOrdDtl
            if (InvF_MoveOrdInstSerial.LstMoveOrd_SerialDistinc != undefined && InvF_MoveOrdInstSerial.LstMoveOrd_SerialDistinc.length > 0) {
                for (const serialDistinc of InvF_MoveOrdInstSerial.LstMoveOrd_SerialDistinc) {
                    let invF_MoveOrdInstSerial_Cur = lst_InvF_MoveOrdInstSerial
                        .filter(function (el) { return el.ProductCode == productCode });
                    let lstMoveOrd_SerialCur = invF_MoveOrdInstSerial_Cur[0].LstMoveOrd_Serial
                        .filter(function (el) {
                            return el.ProductCode == serialDistinc.ProductCode
                                && el.InvCodeIn == serialDistinc.InvCodeIn
                                && el.InvCodeOut == serialDistinc.InvCodeOut
                        })
                    let qtySum = lstMoveOrd_SerialCur.reduce((qty, objInvf) => Number(qty) + (Number(objInvf['Qty']) || 0), 0);

                    serialDistinc.Remark = remark;
                    serialDistinc.UnitCode = unitCode;
                    serialDistinc.Qty = qtySum;
                }
                // 
                objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl = [...objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdDtl, ...InvF_MoveOrdInstSerial.LstMoveOrd_SerialDistinc]
            }
            // Merge array
            objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial = [...objRQ_InvF_MoveOrd.Lst_InvF_MoveOrdInstSerial, ...InvF_MoveOrdInstSerial.LstMoveOrd_Serial];
        }

        var modelCur = JSON.stringify(objRQ_InvF_MoveOrd);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };
    this.searchData = function () {
        //debugger
        var _ajaxSettings = this.ajaxSettings;
        doSearchData(_ajaxSettings);
    }
    function doSearchData(_ajaxSettings) {
        //debugger
        var IF_MONo = commonUtils.returnValueText('#IF_MONo');
        var CreateTimeFrom = commonUtils.returnValueText('#CreateTimeFrom');
        var CreateTimeTo = commonUtils.returnValueText('#CreateTimeTo');
        var InvCodeOut = commonUtils.returnValueText('#InvCodeOut');
        var ApprTimeFrom = commonUtils.returnValueText('#ApprTimeFrom');
        var ApprTimeTo = commonUtils.returnValueText('#ApprTimeTo');
        var InvCodeIn = commonUtils.returnValueText('#InvCodeIn');
        var Remark = commonUtils.returnValueText('#Remark');
        var chkPending = commonUtils.returnValueCheckBox('#chk-pending');
        var chkApproved = commonUtils.returnValueCheckBox('#chk-approved');
        var chkCanceled = commonUtils.returnValueCheckBox('#chk-canceled');
        var recordcount = commonUtils.returnValueText('#recordcount');
        var pagecur = commonUtils.returnValueText('#page');
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();


        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token,
                IF_MONo: IF_MONo,
                CreateTimeFrom: CreateTimeFrom,
                CreateTimeTo: CreateTimeTo,
                InvCodeOut: InvCodeOut,
                ApprTimeFrom: ApprTimeFrom,
                ApprTimeTo: ApprTimeTo,
                InvCodeIn: InvCodeIn,
                Remark: Remark,
                chkPending: chkPending,
                chkApproved: chkApproved,
                chkCanceled: chkCanceled,
                page: pagecur,
                recordcount: recordcount,
            },
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () { }
        }).done(function (result) {
            //debugger
            doneFunctionLoadDoSearch(result);
        }).fail(function (jqXHR, textStatus, errorThrown) {

        }).always(function (jqXHROrData, textStatus, jqXHROrErrorThrown) {
            //alert("complete");
        });
    }
    function doneFunctionLoadDoSearch(result) {
        //debugger
        if (result.Success) {
            $('#BindDataInvF_MoveOrd').html('');
            $('#BindDataInvF_MoveOrd').html(result.Html);
            initDefault();
            $('.numberic').number(true, 2);
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
    this.deleteData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu điều chuyển nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            var listInvF_MoveOrd = [];
            var hasErr = false;
            $(checkedItem).each(function () {
                debugger
                var iF_MONo = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_MONo"]').val());
                var iF_MOStatus = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_MOStatus"]').val());
                var remark = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="Remark"]').val());
                if (iF_MOStatus !== 'PENDING') {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu xuất ' + '' + iF_MONo + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    hasErr = true;
                    return false;
                }
                var objInvF_MoveOrd = {};
                objInvF_MoveOrd.IF_MONo = iF_MONo;
                objInvF_MoveOrd.Remark = remark;
                listInvF_MoveOrd.push(objInvF_MoveOrd);

            });
            if (hasErr) {
                return false;
            }

            if (listInvF_MoveOrd !== undefined && listInvF_MoveOrd !== null && listInvF_MoveOrd.length > 0) {
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
                                model: commonUtils.returnJSONValue(listInvF_MoveOrd),
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

    this.approveData = function () {
        //debugger
        var _ajaxSettings = this.ajaxSettings;
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu điều chuyển nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            var listInvF_MoveOrd = [];
            var hasErr = false;
            $(checkedItem).each(function () {
                var iF_MONo = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_MONo"]').val());
                var iF_MOStatus = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_MOStatus"]').val());
                var remark = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="Remark"]').val());
                if (iF_MOStatus !== 'PENDING') {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu xuất ' + '' + iF_MONo + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    hasErr = true;
                    return false;
                }
                var objInvF_MoveOrd = {};
                objInvF_MoveOrd.IF_MONo = iF_MONo;
                objInvF_MoveOrd.Remark = remark;
                listInvF_MoveOrd.push(objInvF_MoveOrd);
            });
            if (hasErr) {
                return false;
            }
            if (listInvF_MoveOrd !== undefined && listInvF_MoveOrd !== null && listInvF_MoveOrd.length > 0) {
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
                                model: commonUtils.returnJSONValue(listInvF_MoveOrd),
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
    this.approved = function (listInvF_MoveOrd) {
        var _ajaxSettings = this.ajaxSettings;
        if (listInvF_MoveOrd !== undefined && listInvF_MoveOrd !== null && listInvF_MoveOrd.length > 0) {
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
                            model: commonUtils.returnJSONValue(listInvF_MoveOrd),
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
    };
    this.cancelData = function () {
        //debugger
        var _ajaxSettings = this.ajaxSettings;
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu điều chuyển nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            var listInvF_MoveOrd = [];
            var hasErr = false;
            $(checkedItem).each(function () {
                var iF_MONo = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_MONo"]').val());
                var iF_MOStatus = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="IF_MOStatus"]').val());
                var remark = commonUtils.returnValue($(this).closest('tr.trdata').find('input[type="hidden"][name="Remark"]').val());
                if (iF_MOStatus === 'CANCEL') {
                    var objToastr1 = {
                        ToastrType: 'error',
                        ToastrMessage: 'Trạng thái phiếu xuất ' + '' + iF_MONo + '' + ' không hợp lệ.!'
                    };
                    listError.push(objToastr1);
                    commonUtils.showToastr(listError);
                    hasErr = true;
                    return false;
                }
                var objInvF_MoveOrd = {};
                objInvF_MoveOrd.IF_MONo = iF_MONo;
                objInvF_MoveOrd.Remark = remark;
                listInvF_MoveOrd.push(objInvF_MoveOrd);
            });
            if (hasErr) {
                return false;
            }
            if (listInvF_MoveOrd !== undefined && listInvF_MoveOrd !== null && listInvF_MoveOrd.length > 0) {
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
                                model: commonUtils.returnJSONValue(listInvF_MoveOrd),
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
    this.canceled = function (listInvF_MoveOrd) {
        var _ajaxSettings = this.ajaxSettings;
        if (listInvF_MoveOrd !== undefined && listInvF_MoveOrd !== null && listInvF_MoveOrd.length > 0) {
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
                            model: commonUtils.returnJSONValue(listInvF_MoveOrd),
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
    };
    this.printData = function () {
        //debugger
        var _ajaxSettings = this.ajaxSettings;
        Print(_ajaxSettings);
    }
    function Print(_ajaxSettings) {
        //debugger
        var listError = [];
        var checkedItem = $('#table-tbodyID').find('input.idn-checkbox:checked');
        if (checkedItem.length == 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Không có phiếu điều chuyển nào được chọn!'
            };
            listError.push(objToastr);
        }
        if (checkedItem.length > 1) {
            var objToastr1 = {
                ToastrType: 'error',
                ToastrMessage: 'Chỉ chọn 1 phiếu điều chuyển!'
            };
            listError.push(objToastr1);
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);

        }
        else {
            var if_mono = '';
            $(checkedItem).each(function () {
                if_mono = $(this).closest('tr.trdata').find('input[type="hidden"][name="IF_MONo"]').val();
                return false;
            });
            $.ajax({
                type: _ajaxSettings.Type,
                data:
                {
                    if_mono: if_mono
                },
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () { }
            }).done(function (result) {
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

    function printPdf(linkPdf) {
        printJS({
            printable: linkPdf,
            type: 'pdf',
            showModal: true,
            //onPrintDialogClose: () => console.log('The print dialog was closed'),
            //onPdfOpen: () => console.log('Pdf was opened in a new tab due to an incompatible browser')
        })
    }

    this.saveData = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData();
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
   


   
    function doneFunction(objResult) {
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                var listToastr = [];
                objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages[0] };
                listToastr.push(objToastr);
                commonUtils.showToastr(listToastr);
            }
            if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                commonUtils.window_location_href(objResult.RedirectUrl);
            }
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123("Lỗi!", objResult.Detail);
            }
        }
    };
    function failFunction(jqXHR, textStatus, errorThrown) {
    };
    function alwaysFunction() {
    };
    return this;
}

// Constructor function 
function InvF_MoveOrdInstLot() {
    this.InvCodeOut = "";
    this.InvCodeIn = "";
    this.ProductCode = "";
    this.ProductLotNo = "";
    this.Qty = "";
}

function InvF_MoveOrdInstSerial() {
    this.InvCodeOut = "";
    this.InvCodeIn = "";
    this.ProductCode = "";
    this.SerialNo = "";
    this.ProductLotNo = "";
}

function InvF_MoveOrdDtl() {
    this.InvCodeOut = "";
    this.InvCodeIn = "";
    this.ProductCode = "";
    this.UnitCode = "";
    this.Qty = "";
}

var lst_InvF_MoveOrdInstLot = [];
var lst_InvF_MoveOrdInstSerial = [];
var lst_InvF_MoveOrdDtl = [];


/*
 *  
 */

//function Model() {
//    this.lst_InvF_MoveOrdInstLot = [];
//    this.lst_InvF_MoveOrdInstSerial = [];
//    this.lst_InvF_MoveOrdDtl = [];

//    this.renderMainTable = function () {
//        let listDtl = this.lst_InvF_MoveOrdDtl;
//        let listSerial = this.lst_InvF_MoveOrdInstSerial;
//        let listLot = this.lst_InvF_MoveOrdInstLot;
//        // 
//        let listInvCodeOut = [];
//        let listInvCodeIn = [];

//        for (let serial of listSerial) {
//            let productCode = serial.ProductCode;
//            let listSerialToShow = lst_InvF_MoveOrdInstSerial
//                .filter(function (el) {
//                    return el.ProductCode == productCode;
//                });

//        }

//        var productCodeCur = $('#Lst_InvF_MoveOrdInstSerial').attr('serial-productcode');
//        var listSerialToShow = lst_InvF_MoveOrdInstSerial
//            .filter(function (el) { return el.ProductCode == productCodeCur });
//        var itemOnProductTable = $('#Lst_InvF_MoveOrdProduct')
//            .find('tr.trdata[ProductCode="' + productCodeCur + '"]');

//        // SL điều chuyển
//        $(itemOnProductTable).find('input[name="Qty"]')
//            .val(QtySum)
//            .attr('readonly', true);
//        // Vị trí xuất
//        let inputInvOut = '';
//        if (listSerialToShow != undefined && listSerialToShow.length > 0) {
//            for (let srUI of listSerialToShow[0].LstMoveOrd_SerialDistinc) {
//                if (!ListInvCodeOut.includes(srUI.InvCodeOut)) {
//                    ListInvCodeOut.push(srUI.InvCodeOut);
//                }
//                if (!ListInvCodeIn.includes(srUI.InvCodeIn)) {
//                    ListInvCodeIn.push(srUI.InvCodeIn);
//                }
//            }
//        }
//        var strLstInvCodeOut = ListInvCodeOut.join(',');
//        inputInvOut += `<input readonly="readonly" class="col-md-11" name="InvCodeOut" value="${strLstInvCodeOut}">`

//        $(itemOnProductTable).find('td[name="ListInvCodeOut"]').html(inputInvOut);
//        // Vị trí nhập
//        let inputInvIn = '';
//        var strLstInvCodeOut = ListInvCodeIn.join(',');
//        inputInvIn += `<input readonly="readonly" class="col-md-11" name="InvCodeIn" value="${strLstInvCodeOut}">`
//        $(itemOnProductTable).find('td[name="ListInvCodeIn"]').html(inputInvIn);
//    }

//    this.renderSerialProduct({
//        listSerialToShow =[],
//        listInvCodeOut =[],
//        listInvCodeIn = []
//    }){
//        let strListInvCodeOut = listInvCodeOut.join(',');
//        let inputInvOut = '';
//        inputInvOut += `<input readonly="readonly" class="col-md-11" name="InvCodeOut" value="${strListInvCodeOut}">`;

//        $(itemOnProductTable).find('td[name="ListInvCodeOut"]').html(inputInvOut);
//        // Vị trí nhập
//        let inputInvIn = '';
//        var strLstInvCodeOut = ListInvCodeIn.join(',');
//        inputInvIn += `<input readonly="readonly" class="col-md-11" name="InvCodeIn" value="${strLstInvCodeOut}">`
//        $(itemOnProductTable).find('td[name="ListInvCodeIn"]').html(inputInvIn);
//    }
//}





















