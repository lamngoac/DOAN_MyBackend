function License() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettingsInit = function () {
        var _ajaxSettings = {
            Type: '',
            DataType: '',
            Url: '',
        };
        this.ajaxSettings = _ajaxSettings;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    // Gen số
    this.showPopupSinhSo = function () {
        $('#ShowPopupSinhSo').modal({
            backdrop: false,
            keyboard: true,
        });
        $('#ShowPopupSinhSo').modal('show');
    };
    this.closePopupSinhSo = function () {
        commonUtils.setValueElement($('#Qty_SinhSo'), '');
        $('#ShowPopupSinhSo').modal('hide');
    };
    this.checkFormPopupSinhSo = function () {
        debugger;
        if (!commonUtils.checkElementIsNullOrEmpty('#Qty_SinhSo', 'has-error-fix', 'Số lượng sinh số không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNumber('#Qty_SinhSo', 'has-error-fix', 'Số lượng sinh số không là số!')) {
            return false;
        }
        if (!commonUtils.checkIsNumber_IsGreater_Zero('#Qty_SinhSo', 'has-error-fix', 'Số lượng sinh số > 0!')) {
            return false;
        }
        var tongSLConLai = commonUtils.returnValueInt('#TongSLConLai');
        var qty = commonUtils.returnValueInt('#Qty_SinhSo');
        if (qty > tongSLConLai) {
            var message = 'Số lượng sinh số <= ' + tongSLConLai;
            commonUtils.setFocus('Qty_SinhSo');
            commonUtils.showAlert(message);
            return false;
        }
        return true;
    };
    this.getDataPopupSinhSo = function () {
        var token = $('#manageFormShowPopupSinhSo input[name=__RequestVerificationToken]').val();
        var qty = commonUtils.returnValueText('#Qty_SinhSo');
        var objInv_GenTimes = new Object();
        objInv_GenTimes.Qty = qty;
        var modelCur = commonUtils.returnJSONValue(objInv_GenTimes);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };
    this.sinhSo = function () {
        if (this.checkFormPopupSinhSo()) {
            var dataInput = this.getDataPopupSinhSo();
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                doneFunctionSinhSo(objResult);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunctionSinhSo(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunctionSinhSo();
            });
        }
    };

    function doneFunctionSinhSo (objResult) {
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
    }

    function failFunctionSinhSo (jqXHR, textStatus, errorThrown) {
        
    }

    function alwaysFunctionSinhSo () {
        
    }
    // End Gen số

    // Mua License
    this.getDataShowPopupLicense = function (){
        var data = {};
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        if (!commonUtils.isNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }
        return data;
    };

    this.showPopupLicense = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataShowPopupLicense();
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionShowPopupLicense(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionShowPopupLicense(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionShowPopupLicense();
        });
    };

    function doneFunctionShowPopupLicense (objResult) {
        if (objResult.Success) {
            $('#ShowPopupLicense').modal({
                backdrop: false,
                keyboard: true,
            });
            $('#ShowPopupLicense').html(objResult.Html);
        } else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi', objResult.Detail);
            }
        }
    };

    function failFunctionShowPopupLicense (jqXHR, textStatus, errorThrown) {
        
    };

    function alwaysFunctionShowPopupLicense () {
        
    };

    this.closePopupLicense = function () {
        $('#ShowPopupLicense').modal('hide');
        $('#ShowPopupLicense').html('');
    };
    this.buttonNext = function () {
        $('#tblPackage').css("display", "none");
        $('#divThanhToan').css("display", "");

        var totalAmountActual = $('#TotalAmountActual').val();
        var strTotalAmountActual = '0';
        if (commonUtils.isNullOrEmpty(totalAmountActual)) {
            strTotalAmountActual = '0';
        } else {
            if (commonUtils.isNumber(totalAmountActual)) {
                strTotalAmountActual = commonUtils.formatNumber(commonUtils.parseFloat(totalAmountActual), 0);
            }
        }
        $('#subtotal').text(": " + strTotalAmountActual);

        // Show hide button
        $('#btnNext').css("display", "none");
        $('#btnPrev').css("display", "");
        $('#btnLater').css("display", "none");
        $('#btnPayment').css("display", "");
    };
    this.buttonPrev = function () {
        $('#tblPackage').css("display", "");
        $('#divThanhToan').css("display", "none");
        
        // Show hide button
        $('#btnNext').css("display", "");
        $('#btnPrev').css("display", "none");
        $('#btnLater').css("display", "none");
        $('#btnPayment').css("display", "none");
    };
    this.later = function () {
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
                commonUtils.window_location_href(_ajaxSettings.Url);
            }
        });
    };

    this.totalPrice = function (objMaGiamGia) {
        var rows = $("tbody#tbodyLicense tr.trdata").length;
        if (rows > 0) {
            var totalAmount = 0.0;
            var totalGiamGia = 0.0;
            var pTotalGiamGia = 0.0;
            var totalAmountActual = 0.0;

            $("tbody#tbodyLicense tr.trdata").each(function() {
                var trCur = $(this);
                var idx = trCur.attr('idx');
                var flagdiscount = trCur.attr('flagdiscount');

                var price = commonUtils.returnValue(trCur.find('input[type="hidden"][name="ListOS_Inos_Package[' + idx + '].Price"]').val());
                var qty = commonUtils.returnValue(trCur.find('input[type="hidden"][name="ListOS_Inos_Package[' + idx + '].Qty"]').val());
                var flagActive = commonUtils.returnValue(trCur.find('input:checked[type="checkbox"][name="ListOS_Inos_Package[' + idx + '].FlagActive"]').val());

                if (commonUtils.isNullOrEmpty(price)) {
                    price = 0;
                }
                if (commonUtils.isNullOrEmpty(qty)) {
                    qty = 0;
                }
                if (commonUtils.isNullOrEmpty(flagActive)) {
                    flagActive = 0;
                }
                var amountCur = (commonUtils.parseFloat(qty) * commonUtils.parseFloat(price));
                if (flagActive === '1') {
                    totalAmount += amountCur;
                }
                if (flagActive === '1' && flagdiscount === 'True') {
                    pTotalGiamGia += amountCur;
                }
                $(trCur).find('div.formatnumber').html(commonUtils.formatNumber(amountCur, 0));
            });
            if (!commonUtils.isNullOrEmpty(objMaGiamGia.LoaiGiamGia)) {
                var fTotalGiamGia = 0.0;
                if (objMaGiamGia.LoaiGiamGia === '2') {
                    fTotalGiamGia = commonUtils.parseFloat(objMaGiamGia.DiscountAmount);
                }
                else if (objMaGiamGia.LoaiGiamGia === '1') {
                    var fPercent = commonUtils.parseFloat(objMaGiamGia.DiscountAmount);
                    fTotalGiamGia = (pTotalGiamGia * fPercent) / 100;
                }
                $('#TotalGiamGia').val(fTotalGiamGia);
            }
            totalGiamGia = commonUtils.returnValueFloat('#TotalGiamGia');
            totalAmountActual = totalAmount - totalGiamGia;
            $('#TotalAmount').val(totalAmount);
            $('#TotalGiamGia').val(totalGiamGia);
            $('#TotalAmountActual').val(totalAmountActual);

            $('tfoot#tfootLicense').find('div.totalamount').html(commonUtils.formatNumber(totalAmount, 0));
            $('tfoot#tfootLicense').find('div.totalgiamgia').html(commonUtils.formatNumber(totalGiamGia, 0));
            $('tfoot#tfootLicense').find('div.totalamountactual').html(commonUtils.formatNumber(totalAmountActual, 0));

        }
    };

    this.changeCheckBoxPackage = function (thiz, objMaGiamGia) {
        var checkBox = $(thiz);
        if (!commonUtils.checkElementUndefinedOrNull(checkBox)) {
            var trCur = $(checkBox).parent().parent().parent().parent();
            var divCur = $(trCur).find("div.formatnumber");
            var divCur2 = $(trCur).find("div.discountprice");
            if (checkBox.is(':checked')) {
                checkBox.prop('checked', true);
                checkBox.val('1');
                if (!commonUtils.checkElementUndefinedOrNull(divCur)) {
                    if ($(divCur).hasClass("display-none")) {
                        $(divCur).removeClass("display-none");
                    }
                }
                if (!commonUtils.checkElementUndefinedOrNull(divCur2)) {
                    if ($(divCur2).hasClass("display-none")) {
                        $(divCur2).removeClass("display-none");
                    }
                }
            } else {
                checkBox.prop('checked', false);
                checkBox.val('0');
                if (!commonUtils.checkElementUndefinedOrNull(divCur)) {
                    if (!$(divCur).hasClass("display-none")) {
                        $(divCur).addClass("display-none");
                    }
                }
                if (!commonUtils.checkElementUndefinedOrNull(divCur2)) {
                    if (!$(divCur2).hasClass("display-none")) {
                        $(divCur2).addClass("display-none");
                    }
                }
            }
            this.totalPrice(objMaGiamGia);
        }
    };

    this.checkDieuKienApdung = function (objMaGiamGia) {
        var discountcode = commonUtils.returnValueText('#DiscountCode');
        var dealercode = commonUtils.returnValueText('#DLCode');

        if (commonUtils.isNullOrEmpty(dealercode)) {
            commonUtils.showAlert('Mã đại lý trống!');
            commonUtils.setFocus('DLCode');
            return false;
        }
        if (commonUtils.isNullOrEmpty(discountcode)) {
            commonUtils.showAlert('Mã giảm giá trống!');
            commonUtils.setFocus('DiscountCode');
            $('.discountprice').html('');
            $('.formatnumber').css('text-decoration', 'none').css('font-style', 'normal').css('font-size', '12px');
            $('#TotalGiamGia').val(0);
            objMaGiamGia.LoaiGiamGia = null;
            debugger;
            this.totalPrice(objMaGiamGia);
            return false;
        }

        return true;
    };
    this.getDataDieuKienApdung = function () {
        var discountcode = commonUtils.returnValueText('#DiscountCode');
        var dealercode = commonUtils.returnValueText('#DLCode');
        var data = {};
        var token = $('#manageFormShowPopupLicense input[name=__RequestVerificationToken]').val();
        if (!commonUtils.isNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }
        data.discountcode = discountcode;
        data.dealercode = dealercode;
        return data;
    };
    this.apdung = function (objMaGiamGia) {
        $('.alert-discount').html('');
        $('.alert-discount').css('color', 'red');
        if (this.checkDieuKienApdung(objMaGiamGia)) {
            var _ajaxSettings = this.ajaxSettings;
            var dataInput = this.getDataDieuKienApdung();

            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                async: false,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                debugger;
                doneFunctionLicense(objResult, objMaGiamGia);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunctionLicense(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunctionLicense();
            });
            // Tính toám lại
            debugger;
            this.totalPrice(objMaGiamGia);
        }
    };

    function doneFunctionLicense(objResult, objMaGiamGia) {
        debugger;
        if (objResult.Success) {
            var objOS_Inos_LicOrder = objResult.OS_Inos_LicOrder;
            if (objOS_Inos_LicOrder !== undefined && objOS_Inos_LicOrder !== null) {
                var rows = $("tbody#tbodyLicense tr.trdata").length;
                if (rows > 0) {
                    $("tbody#tbodyLicense tr.trdata").each(function () {
                        var trCur = $(this);
                        var idx = trCur.attr('idx');
                        var flagdiscount = trCur.attr('flagdiscount');
                        if (commonUtils.toLowerCase(flagdiscount) === 'true') {
                            var price = commonUtils.returnValue(trCur.find('input[type="hidden"][name="ListOS_Inos_Package[' + idx + '].Price"]').val());
                            var discountprice = price * (100 - objOS_Inos_LicOrder.DiscountAmount) / 100;
                            trCur.find('div[name="discountprice[' + idx + ']"]').html(commonUtils.formatNumber(discountprice, 0));
                            trCur.find('.formatnumber').css('text-decoration', 'line-through rgb(255, 0, 0)').css('font-style', 'italic').css('font-size', '11px');
                        }
                    });
                }

                var discountAmount = 0.0;
                var fPercent = 0.0;
                var fTotalAmount = commonUtils.returnValueFloat('#TotalAmount');
                var discountType = commonUtils.returnValue(objOS_Inos_LicOrder.DiscountType);

                objMaGiamGia.MaGiamGia = objOS_Inos_LicOrder.Code;
                objMaGiamGia.LoaiGiamGia = discountType;

                if (discountType === '2') {
                    var fTotalGiamGia = commonUtils.parseFloat(objOS_Inos_LicOrder.DiscountAmount);
                    objMaGiamGia.DiscountAmount = fTotalGiamGia;
                    $('#TotalGiamGia').val(fTotalGiamGia);
                }
                else if (discountType === '1') {
                    fPercent = commonUtils.parseFloat(objOS_Inos_LicOrder.DiscountAmount);
                    objMaGiamGia.DiscountAmount = fPercent;
                    var fTotalGiamGia = (fTotalAmount * fPercent) / 100;
                    $('#TotalGiamGia').val(fTotalGiamGia);
                }

                $('.alert-discount').html(objOS_Inos_LicOrder.Description);
                $('.alert-discount').css('color', '#007FC7')
            }
        } else {
            $('#TotalGiamGia').val(0);
            objMaGiamGia.LoaiGiamGia = null;
            $('.discountprice').html('');
            $('.formatnumber').css('text-decoration', 'none').css('font-style', 'normal').css('font-size', '12px');

            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123("Lỗi!", objResult.Detail);
            }
            else if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                var messages = commonUtils.returnValue(objResult.Messages);
                $('.alert-discount').html(messages);
            }
        }
        return objMaGiamGia;
    }

    function failFunctionLicense(jqXHR, textStatus, errorThrown) {
        
    }

    function alwaysFunctionLicense() {
        
    }
    // End mua License

    // Thanh toán
    this.getDataCreateOrder = function() {
        var data = {};
        var token = $('#manageFormShowPopupLicense input[name=__RequestVerificationToken]').val();
        if (!commonUtils.isNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }

        var objInos_LicOrder = new Object();
        objInos_LicOrder.DiscountCode = commonUtils.returnValueText('#DiscountCode');
        objInos_LicOrder.TotalCost = commonUtils.returnValueText('#TotalAmountActual');
        objInos_LicOrder.OrgId = commonUtils.returnValueText('#OrgId_Popup');
        var Inos_DetailList = [];
        var rows = $("tbody#tbodyLicense tr.trdata").length;
        if (rows > 0) {
            $("tbody#tbodyLicense tr.trdata").each(function () {
                var trCur = $(this);
                var idx = trCur.attr('idx');
                var flagActive = commonUtils.returnValue(trCur.find('input:checked[type="checkbox"][name="ListOS_Inos_Package[' + idx + '].FlagActive"]').val());
                if (flagActive === '1') {
                    var packageId = commonUtils.returnValue(trCur.find('input[type="hidden"][name="ListOS_Inos_Package[' + idx + '].Id"]').val());
                    var objInos_LicOrderDetail = {};
                    objInos_LicOrderDetail.PackageId = packageId;
                    Inos_DetailList.push(objInos_LicOrderDetail);
                }
            });
        }

        objInos_LicOrder.Inos_DetailList = Inos_DetailList;
        var objInos_LicOrderCur = commonUtils.returnJSONValue(objInos_LicOrder);
        data.inoslicorder = objInos_LicOrderCur;
        return data;
    };
    this.createOrder = function(paymentType) {
        //paymentType = 0: thanh toán qua VNPay; 1: thanh toán bằng tiền mặt; 2: thanh toán qua ngân hàng điện tử
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataCreateOrder();
        var checkCreateOrder = false;
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            async: false,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            checkCreateOrder = doneFunctionCreateOrder(objResult, paymentType);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            checkCreateOrder = false;
            failFunctionCreateOrder(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionCreateOrder();
        });
        return checkCreateOrder;
    };

    function doneFunctionCreateOrder(objResult, paymentType) {
        var checkCreateOrder = false;
        if (objResult.Success) {
            checkCreateOrder = true;
            if (paymentType === '1') {
                commonUtils.showAlert(
                    "Cảm ơn quý khách đã đăng ký thêm gói dịch vụ Inbrand. Vui lòng liên hệ với idocNet để được hướng dẫn thanh toán và kích hoạt các gói dịch vụ mới.");
            } else {
                // Thanh toán qua VNPay => xử lý bên ngoài

                var objInos_LicOrderData = objResult.Inos_LicOrder;
                var packageId = '';
                $('#Payment_Id').val(commonUtils.returnValue(objInos_LicOrderData.Id));
                $('#Payment_OrgID').val(commonUtils.returnValue(objInos_LicOrderData.OrgId));
                $('#Payment_PaymentCode').val(commonUtils.returnValue(objInos_LicOrderData.PaymentCode));
                $('#Payment_TotalCost').val(commonUtils.returnValue(objInos_LicOrderData.TotalCost));
                $('#Payment_CreateDTime').val(commonUtils.returnValue(objInos_LicOrderData.StrCreateDTime));
                $('#Payment_Remark').val(commonUtils.returnValue(objInos_LicOrderData.Remark));
                $('#Payment_PackageId').val(commonUtils.returnValue(packageId));
                
                // Tổng tiền thanh toán
                var totalCost = commonUtils.returnValue(objInos_LicOrderData.TotalCost)
                var strTotalAmountActual = '0';
                if (commonUtils.isNullOrEmpty(totalCost)) {
                    strTotalAmountActual = '0';
                } else {
                    if (commonUtils.isNumber(totalCost)) {
                        strTotalAmountActual = commonUtils.formatNumber(commonUtils.parseFloat(totalCost), 0);
                    }
                }
                $('#subtotal').text(": " + strTotalAmountActual);
            }
        } else {
            checkCreateOrder = false;
            if (objResult.Messages !== null && objResult.Messages !== undefined && objResult.Messages.length > 0) {
                var messages = commonUtils.returnValue(objResult.Messages[0]);
                if (!commonUtils.isNullOrEmpty(messages)) {
                    commonUtils.showAlert(messages);
                }
            }
            var detail = commonUtils.returnValue(objResult.Detail);
            if (!commonUtils.isNullOrEmpty(detail)) {
                _showErrorMsg123("Lỗi!", detail);
            }
        }
        return checkCreateOrder;
    }
    function failFunctionCreateOrder(jqXHR, textStatus, errorThrown) {

    }
    function alwaysFunctionCreateOrder() {

    }

    this.checkCreatePayment = function () {
        //var remark = commonUtils.returnValueText('#Payment_Remark');
        //if (commonUtils.isNullOrEmpty(remark)) {
        //    commonUtils.showAlert('Thanh toán mua thêm License trống!');
        //    return false;
        //}
        var paymentCode = commonUtils.returnValueText('#Payment_PaymentCode');
        if (commonUtils.isNullOrEmpty(paymentCode)) {
            commonUtils.showAlert('Mã lệnh thanh toán không tồn tại!');
            return false;
        }
        var totalCost = commonUtils.returnValueText('#Payment_TotalCost');
        if (commonUtils.isNullOrEmpty(totalCost)) {
            commonUtils.showAlert('Giá trị thanh toán không hợp lệ!');
            return false;
        }
        return true;
    };
    this.getDataCreatePayment = function () {

        var payment_Id = commonUtils.returnValueText('#Payment_Id');
        var payment_PaymentCode = commonUtils.returnValueText('#Payment_PaymentCode');
        var payment_Remark = commonUtils.returnValueText('#Payment_Remark');
        var payment_TotalCost = commonUtils.returnValueText('#Payment_TotalCost');
        var payment_CreateDTime = commonUtils.returnValueText('#Payment_CreateDTime');
        var payment_OrgID = commonUtils.returnValueText('#Payment_OrgID');
        payment_Remark = 'Thanh toan mua them License - Inbrand';
        var objInos_LicOrder = {
            Id: payment_Id,
            PaymentCode: payment_PaymentCode,
            Remark: payment_Remark,
            TotalCost: payment_TotalCost,
            StrCreateDTime: payment_CreateDTime,
            OrgID: payment_OrgID
        };
        var language = "vn";
        var ordertype = "billpayment"; // Mặc định thanh toán hóa đơn
        var modelCur = commonUtils.returnJSONValue(objInos_LicOrder);

        var data = {};
        //var token = $('#manageFormShowPopupLicense input[name=__RequestVerificationToken]').val();
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        if (!commonUtils.isNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }
        data.model = modelCur;
        data.language = language;
        data.ordertype = ordertype;
        return data;

    };
    this.createPayment = function () {
        debugger;
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataCreatePayment();
        var checkCreatePayment = false;
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            async: false,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            debugger;
            checkCreatePayment = doneFunctionCreatePayment(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            checkCreatePayment = false;
            failFunctionCreatePayment(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionCreatePayment();
        });
        return checkCreatePayment;
    };

    function doneFunctionCreatePayment(objResult) {
        debugger;
        var checkCreatePayment = false;
        if (objResult.code === '00') {
            checkCreatePayment = true;
            vnpay.open({ width: 340, height: 490, url: objResult.data });
            return false;
        } else {
            checkCreatePayment = false;
            var message = commonUtils.returnValue(objResult.Message);
            if (!commonUtils.isNullOrEmpty(message)) {
                commonUtils.showAlert(message);
            }
        }
    }
    function failFunctionCreatePayment(jqXHR, textStatus, errorThrown) {

    }
    function alwaysFunctionCreatePayment() {

    }
    // End thanh toán
    return this;
}