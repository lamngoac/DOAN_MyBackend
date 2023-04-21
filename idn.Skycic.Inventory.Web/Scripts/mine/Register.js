function Register() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';

    this.checkForm = function () {

        if (!commonUtils.checkElementIsNullOrEmpty('#Usercode', 'has-error-fix', 'Tên đăng nhập không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#VerCode', 'has-error-fix', 'Mã xác thực không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#Password', 'has-error-fix', 'Mật khẩu không được trống!')) {
            return false;
        }
        var password = commonUtils.returnValueTextOrNull('#Password');
        if (password.trim().length < 6) {
            alert('Mật khẩu > 5 ký tự!');
            commonUtils.addClassCss('#Password', 'has-error-fix');
            $('#Password').focus();
            return false;
        }
        else {
            commonUtils.removeClassCss('#Password', 'has-error-fix');
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#Repassword', 'has-error-fix', 'Nhập lại mật khẩu không được trống!')) {
            return false;
        }
        var repassword = commonUtils.returnValueTextOrNull('#Repassword');
        if (password !== repassword) {
            alert('Nhập lại mật khẩu không đúng, xin vui lòng nhập lại!');
            commonUtils.addClassCss('#Repassword', 'has-error-fix');
            $('#Repassword').focus();
            return false;
        }
        else {
            commonUtils.removeClassCss('#Repassword', 'has-error-fix');
        }
        if (repassword.trim().length < 6) {
            alert('Nhập lại mật khẩu > 5 ký tự!');
            commonUtils.addClassCss('#Repassword', 'has-error-fix');
            $('#Repassword').focus();
            return false;
        }
        else {
            commonUtils.removeClassCss('#Repassword', 'has-error-fix');
        }

        var flagCheckReCaptcha = objAccuracyEmail.FlagCheckReCaptcha;
        var flagVerifyEmail = objAccuracyEmail.FlagVerifyEmail;
        if (flagCheckReCaptcha === '1' && flagVerifyEmail === '1') {
            var email = objAccuracyEmail.Email;
            var verificationEmailCode = objAccuracyEmail.VerificationEmailCode;

            var usercode = commonUtils.returnValueTextOrNull('#Usercode');
            var vercode = commonUtils.returnValueTextOrNull('#VerCode');
            if (usercode !== email || vercode !== verificationEmailCode) {
                alert('Email chưa được xác thực!');
                commonUtils.addClassCss('#VerCode', 'has-error-fix');
                $('#VerCode').focus();
                return false;
            }

        } else {
            alert('Email chưa được xác thực!');
            commonUtils.addClassCss('#VerCode', 'has-error-fix');
            $('#VerCode').focus();
            return false;
        }
        return true;
    };
    this.checkFormStep = function (step) {

        if (step === 1) {
            var totalAmount = $('#TotalAmount').val();
            var flagactivefirst = $('input[name="ListOS_Inos_Package[0].FlagActive"]').val();
            if (commonUtils.isNullOrEmpty(totalAmount) && totalAmount < 0) {
                alert("Chọn ít nhất một gói license");
                return false;
            }
            if (flagactivefirst === 0) {
                alert("Bạn phải mua ít nhất 1 năm phí thuê bao hàng năm");
                return false;
            }
            return true;
        }
        if (step === 2) {
            //if (!commonUtils.checkElementIsNullOrEmpty('#MST', 'has-error-fix', 'Mã tổ chức không được trống!')) {
            //    return false;
            //}
            if (!commonUtils.checkElementIsNullOrEmpty('#ContactName', 'has-error-fix', 'Họ tên không được trống!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNullOrEmpty('#NNTFullName', 'has-error-fix', 'Tên tổ chức không được trống!')) {
                return false;
            }
            //if (!commonUtils.checkElementIsNullOrEmpty('#NNTAddress', 'has-error-fix', 'Địa chỉ không được trống!')) {
            //    return false;
            //}
            //if (!commonUtils.checkElementIsNullOrEmpty('#ProvinceCode', 'has-error-fix', 'Tỉnh/Thành phố không được trống!')) {
            //    return false;
            //}
            //if (!commonUtils.checkElementIsNullOrEmpty('#DistrictCode', 'has-error-fix', 'Quận/Huyện không được trống!')) {
            //    return false;
            //}

            //if (!commonUtils.checkElementIsNullOrEmpty('#ContactPhone', 'has-error-fix', 'Điện thoại không được trống!')) {
            //    return false;
            //}
            if (!commonUtils.checkElementIsNullOrEmpty('#ContactEmail', 'has-error-fix', 'Email liên hệ không được trống!')) {
                return false;
            }
            var email = commonUtils.returnValueTextOrNull('#ContactEmail');
            if (commonUtils.validateEmail(email) === false) {
                alert("Email liên hệ không đúng định dạng");
                $('#ContactEmail').focus();
                return false;
            }
        }
        if (step === 3) {
            if (!commonUtils.checkElementIsNullOrEmpty('#Usercode', 'has-error-fix', 'Tên đăng nhập không được trống!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNullOrEmpty('#VerCode', 'has-error-fix', 'Mã xác thực không được trống!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNullOrEmpty('#Password', 'has-error-fix', 'Mật khẩu không được trống!')) {
                return false;
            }
            var password = commonUtils.returnValueTextOrNull('#Password');
            if (password.trim().length < 6) {
                alert('Mật khẩu > 5 ký tự!');
                commonUtils.addClassCss('#Password', 'has-error-fix');
                $('#Password').focus();
                return false;
            }
            else {
                commonUtils.removeClassCss('#Password', 'has-error-fix');
            }
            if (!commonUtils.checkElementIsNullOrEmpty('#Repassword', 'has-error-fix', 'Nhập lại mật khẩu không được trống!')) {
                return false;
            }
            var repassword = commonUtils.returnValueTextOrNull('#Repassword');
            if (password !== repassword) {
                alert('Nhập lại mật khẩu không đúng, xin vui lòng nhập lại!');
                commonUtils.addClassCss('#Repassword', 'has-error-fix');
                $('#Repassword').focus();
                return false;
            }
            else {
                commonUtils.removeClassCss('#Repassword', 'has-error-fix');
            }
            if (repassword.trim().length < 6) {
                alert('Nhập lại mật khẩu > 5 ký tự!');
                commonUtils.addClassCss('#Repassword', 'has-error-fix');
                $('#Repassword').focus();
                return false;
            }
            else {
                commonUtils.removeClassCss('#Repassword', 'has-error-fix');
            }
        }
        return true;
    };
    this.checkFormStep1 = function (step) {

        if (step === 1) {
            var totalAmount = $('#TotalAmount').val();
            var flagactivefirst = $('input[name="ListOS_Inos_Package[0].FlagActive"]').val();
            if (commonUtils.isNullOrEmpty(totalAmount) && totalAmount < 0) {
                alert("Chọn ít nhất một gói license");
                return false;
            }
            if (flagactivefirst === 0) {
                alert("Bạn phải mua ít nhất 1 năm phí thuê bao hàng năm");
                return false;
            }
            return true;
        }
        if (step === 2) {
            if (!commonUtils.checkElementIsNullOrEmpty('#ContactName', 'has-error-fix', 'Họ tên không được trống!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNullOrEmpty('#NNTFullName', 'has-error-fix', 'Tên tổ chức không được trống!')) {
                return false;
            }
            if (!commonUtils.checkElementIsNullOrEmpty('#ContactEmail', 'has-error-fix', 'Email liên hệ không được trống!')) {
                return false;
            }
            var email = commonUtils.returnValueTextOrNull('#ContactEmail');
            if (commonUtils.validateEmail(email) === false) {
                alert("Email liên hệ không đúng định dạng");
                $('#ContactEmail').focus();
                return false;
            }
        }
        return true;
    };
    this.getvalue = function () {
        var value = $('#ContactEmail').val();
        $('#Usercode').val(value);
    };

    this.totalPrice = function () {
        var rows = $("tbody#tbodyLicense tr.trdata").length;
        if (rows > 0) {
            var totalAmount = 0.0;
            var totalgiamgia = 0.0;
            var pTotalGiamGia = 0.0;
            var totalamountactual = 0.0;
            $("tbody#tbodyLicense tr.trdata").each(function () {

                var trCur = $(this);
                var idx = trCur.attr('idx');
                var flagdiscount = trCur.attr('flagdiscount');

                var price = commonUtils.returnValueOrNull(trCur.find('input[type="hidden"][name="ListOS_Inos_Package[' + idx + '].Price"]').val());
                var qty = commonUtils.returnValueOrNull(trCur.find('input[type="hidden"][name="ListOS_Inos_Package[' + idx + '].Qty"]').val());
                var flagActive = commonUtils.returnValueOrNull(trCur.find('input:checked[type="checkbox"][name="ListOS_Inos_Package[' + idx + '].FlagActive"]').val());
                if (IsNullOrEmpty(price)) {
                    price = 0;
                }
                if (IsNullOrEmpty(qty)) {
                    qty = 0;
                }
                if (IsNullOrEmpty(flagActive)) {
                    flagActive = 0;
                }
                var amountCur = (parseFloat(qty) * parseFloat(price));
                if (flagActive === '1') {
                    totalAmount += amountCur;
                }
                if (flagActive === '1' && flagdiscount === 'True') {

                    pTotalGiamGia += amountCur;
                }

                //$(trCur).find('div.formatnumber').html(formatNumberInteger(amountCur));
            });
            if (!IsNullOrEmpty(objMaGiamGia.LoaiGiamGia)) {
                var fTotalGiamGia = 0.0;
                if (objMaGiamGia.LoaiGiamGia === '2') {
                    fTotalGiamGia = parseFloat(objMaGiamGia.DiscountAmount);
                }
                else if (objMaGiamGia.LoaiGiamGia === '1') {
                    var fPercent = parseFloat(objMaGiamGia.DiscountAmount);
                    fTotalGiamGia = (pTotalGiamGia * fPercent) / 100;
                }
                $('#TotalGiamGia').val(fTotalGiamGia);
            }

            totalgiamgia = commonUtils.returnValueFloat('#TotalGiamGia');
            totalamountactual = totalAmount - totalgiamgia;
            $('#TotalAmount').val(totalAmount);
            $('#TotalGiamGia').val(totalgiamgia);
            $('#TotalAmountActual').val(totalamountactual);

            $('tfoot#tfootLicense').find('div.totalamount').html(formatNumberInteger(totalAmount));
            $('tfoot#tfootLicense').find('div.totalgiamgia').html(formatNumberInteger(totalgiamgia));
            $('tfoot#tfootLicense').find('div.totalamountactual').html(formatNumberInteger(totalamountactual));

        }
    };

    this.checkVal = function () {
        var nextcur = commonUtils.parseInt($('.btn-next').attr('val'));
        var prevcur = commonUtils.parseInt($('.btn-prev').attr('val'));
        if (prevcur === 1) {
            $(".btn-prev").hide();
            document.getElementById("agree").checked = false;
        }
        else
            $(".btn-prev").show();
        if (nextcur === 3) {
            $(".btn-next").hide();
            $(".btn-register-form").show();
            $(".btn-cancel-form").hide();
            $(".btn-pay-form").hide();
        }
        else {
            $(".btn-next").show();
            $(".btn-register-form").hide();
            $(".btn-cancel-form").hide();
            $(".btn-pay-form").hide();
        }
    };
    this.checkVal1 = function () {
        var nextcur = commonUtils.parseInt($('.btn-next').attr('val'));
        var prevcur = commonUtils.parseInt($('.btn-prev').attr('val'));
        if (prevcur === 1) {
            $(".btn-prev").hide();
            document.getElementById("agree").checked = false;
        }
        else
            $(".btn-prev").show();
        //if (nextcur == 4) {
        //    $(".btn-next").hide();
        //    $(".btn-prev").hide();
        //    $(".btn-register-form").hide();
        //    $(".btn-cancel-form").show();
        //    $(".btn-pay-form").show();
        //}
        if (nextcur == 2) {
            $(".btn-next").hide();
            $(".btn-register-form").show();
            $(".btn-cancel-form").hide();
            $(".btn-pay-form").hide();
        }
        else {
            $(".btn-next").show();
            $(".btn-register-form").hide();
            $(".btn-cancel-form").hide();
            $(".btn-pay-form").hide();
        }
    };
    this.closeModalConfirm = function () {
        $("#Confirm,.site-overlay").removeClass("active");
        //var session_popup_require = parseInt($.session.get('session_popup_require')) + 1;
        //$.session.set('session_popup_require', session_popup_require);

        //if (session_popup_require == 2) {
        //    setTimeout(function () {
        //        showPopupRequire();
        //    }, 60000);

        //}
        //if (session_popup_require == 3) {
        //    setTimeout(function () {
        //        showPopupRequire();
        //    }, 120000);

        //}
    };

    this.next = function () {

        var cur = commonUtils.parseInt($('.btn-next').attr('val'));
        var nextstep = cur + 1;
        var check = false;
        if (cur === 1) {
            check = this.checkFormStep(cur);
            if (check === true) {
                $("#Confirm,.site-overlay").addClass("active");
            }
            else return;
        }
        if (cur === 2) {
            var email = objAccuracyEmail.Email;
            if (email !== undefined && email !== null && email.toString().length > 0) {
                var contactEmail = commonUtils.returnValueTextOrNull('#ContactEmail');
                if (contactEmail !== email) {
                    $('VerCode').val('');
                    $('Password').val('');
                    $('Repassword').val('');

                    //
                    objAccuracyEmail.Email = '';
                    objAccuracyEmail.VerificationEmailCode = '';
                    objAccuracyEmail.FlagCheckReCaptcha = '0';
                    objAccuracyEmail.FlagVerifyEmail = '0';
                }
            }
            getvalue();
        }


        if (!IsNullOrEmpty(cur)) {
            check = this.checkFormStep(cur);
        }

        if (check) {
            var checkbox = document.getElementById("agree").checked;
            if (checkbox === true) {
                commonUtils.addClassCss('#step' + nextstep + '', "active");
                commonUtils.removeClassCss('#step' + cur + '', "active");
                commonUtils.addClassCss('#stepli' + nextstep + '', "active");
                commonUtils.removeClassCss('#stepli' + cur + '', "active");

                $('.btn-next, .btn-prev').attr('val', nextstep);
                this.checkVal();
            }
        }
    };
    this.next1 = function () {

        var cur = commonUtils.parseInt($('.btn-next').attr('val'));
        var nextstep = cur + 1;
        var check = false;
        if (cur === 1) {
            check = this.checkFormStep(cur);
            if (check === true) {
                $("#Confirm,.site-overlay").addClass("active");
            }
            else return;
        }
        //if (cur === 2) {
        //    var email = objAccuracyEmail.Email;
        //    if (email !== undefined && email !== null && email.toString().length > 0) {
        //        var contactEmail = commonUtils.returnValueTextOrNull('#ContactEmail');
        //        if (contactEmail !== email) {
        //            $('VerCode').val('');
        //            $('Password').val('');
        //            $('Repassword').val('');

        //            //
        //            objAccuracyEmail.Email = '';
        //            objAccuracyEmail.VerificationEmailCode = '';
        //            objAccuracyEmail.FlagCheckReCaptcha = '0';
        //            objAccuracyEmail.FlagVerifyEmail = '0';
        //        }
        //    }
        //    getvalue();
        //}


        if (!IsNullOrEmpty(cur)) {
            check = this.checkFormStep(cur);
        }

        if (check) {
            var checkbox = document.getElementById("agree").checked;
            if (checkbox === true) {
                commonUtils.addClassCss('#step' + nextstep + '', "active");
                commonUtils.removeClassCss('#step' + cur + '', "active");
                commonUtils.addClassCss('#stepli' + nextstep + '', "active");
                commonUtils.removeClassCss('#stepli' + cur + '', "active");

                $('.btn-next, .btn-prev').attr('val', nextstep);
                this.checkVal1();
            }
        }
    };
    this.confirmstep = function () {
        var checkbox = document.getElementById("agree").checked;
        if (checkbox === false) {
            alert("Bạn cần phải đồng ý với điều khoản của chúng tôi!");
        }
        else {
            this.next();
            this.closeModalConfirm();
        }
    };
    this.confirmstep1 = function () {
        debugger;
        var checkbox = document.getElementById("agree").checked;
        if (checkbox === false) {
            alert("Bạn cần phải đồng ý với điều khoản của chúng tôi!");
        }
        else {
            this.next1();
            this.closeModalConfirm();
        }
    };
    this.prev = function () {
        var cur = commonUtils.parseInt($('.btn-prev').attr('val'));
        var prevstep = cur - 1;
        commonUtils.addClassCss('#step' + prevstep + '', "active");
        commonUtils.removeClassCss('#step' + cur + '', "active");
        commonUtils.addClassCss('#stepli' + prevstep + '', "active");
        commonUtils.removeClassCss('#stepli' + cur + '', "active");

        $('.btn-next, .btn-prev').attr('val', prevstep);
        this.checkVal();
    };

    this.showModalGetCode = function () {
        var usercode = commonUtils.returnValueTextOrNull('#Usercode');
        if (IsNullOrEmpty(usercode))
            alert('Tên đăng nhập không được trống!');
        else {
            var captcharesponse = grecaptcha.getResponse();
            grecaptcha.reset();
            $('#GetCode').modal({
                backdrop: false,
                keyboard: true
            });
            $('#GetCode').modal('show');
        }
    };
    this.closeModalGetCode = function () {
        //$('#GetCode').modal("hide");
        $('#GetCode').hide();
    };
    this.closeModalConfirm = function () {
        $("#Confirm,.site-overlay").removeClass("active");
    };

    this.confirmCode = function (objRegister) {
        var captcharesponse = grecaptcha.getResponse();
        var usercode = commonUtils.returnValueTextOrNull('#Usercode');
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var _ajaxSettings = objRegister.ajaxSettings;
        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token
                , usercode: usercode
                , grecaptcharesponse: captcharesponse
            },
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {

            doneFunctionConfirmCode(objResult, objRegister);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
    };
    //this.sendEmailVerificationEmail = function (objRegister) {
    //    
    //    var _ajaxSettings = objRegister.ajaxSettings;
    //    objAccuracyEmail.FlagCheckReCaptcha = '1';
    //    $.ajax({
    //        type: _ajaxSettings.Type,
    //        data: {
    //            __RequestVerificationToken: token
    //            , email: usercode
    //        },
    //        url: objRegister.UrlSendEmail,
    //        dataType: _ajaxSettings.DataType,
    //        beforeSend: function () {
    //        }
    //    }).done(function (objResult) {
    //        doneFunctionSendEmail(objResult);
    //    }).fail(function (jqXHR, textStatus, errorThrown) {
    //        failFunction(jqXHR, textStatus, errorThrown);
    //    }).always(function () {
    //        alwaysFunction();
    //    });
    //};
    this.verifyEmail = function (objRegister) {
        var usercode = commonUtils.returnValueTextOrNull('#Usercode');
        var vercode = commonUtils.returnValueTextOrNull('#VerCode');
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var _ajaxSettings = objRegister.ajaxSettings;
        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token
                , email: usercode
                , code: vercode
            },
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionVerifyEmail(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
    };

    this.apdung = function (objRegister) {
        $('.alert-discount').html("");
        $('.alert-discount').css('color', 'red');
        var discountcode = commonUtils.returnValueTextOrNull('#DiscountCode');
        var dealercode = $('#DLCode').val();
        if (discountcode === undefined || discountcode === "") {
            alert("Mã giảm giá trống.");
            //$('#DiscountCode').focus();
            $('.discountprice').html('');
            //$('.formatnumber').css('text-decoration', 'none').css('font-style', 'normal').css('font-size', '12px');
            $('#TotalGiamGia').val(0);
            objMaGiamGia.LoaiGiamGia = null;
            totalPrice();

            return;
        }
        if (dealercode === undefined || dealercode === "") {
            alert("Mã đại lý trống.");
            $('#DLCode').focus();
            return;
        }
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        var _ajaxSettings = objRegister.ajaxSettings;
        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token
                , discountcode: discountcode
                , dealercode: dealercode
            },
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionApDung(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
    };

    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var mST = commonUtils.returnValueText('#MST');
        var nNTFullName = commonUtils.returnValueText('#NNTFullName');
        var nNTAddress = commonUtils.returnValueText('#NNTAddress');
        var provinceCode = commonUtils.returnValueText('#ProvinceCode');
        var districtCode = commonUtils.returnValueText('#DistrictCode');
        var nNTMobile = commonUtils.returnValueText('#NNTMobile');
        var nNTPhone = commonUtils.returnValueText('#NNTPhone');
        var bizType = commonUtils.returnValueTextOrNull('#BizType');
        var bizFieldCode = commonUtils.returnValueTextOrNull('#BizFieldCode');
        var bizSizeCode = commonUtils.returnValueTextOrNull('#BizSizeCode');
        var dLCode = commonUtils.returnValueText('#DLCode');
        var contactName = commonUtils.returnValueText('#ContactName');
        var contactPhone = commonUtils.returnValueText('#ContactPhone');
        var contactEmail = commonUtils.returnValueText('#ContactEmail');
        var website = commonUtils.returnValueText('#Website');
        //step3
        var usercode = commonUtils.returnValueText('#Usercode');
        var verCode = commonUtils.returnValueText('#VerCode');
        var password = commonUtils.returnValueText('#Password');
        var repassword = commonUtils.returnValueText('#Repassword');
        //invisible
        var accNo = commonUtils.returnValueText('#AccNo');
        var accHolder = commonUtils.returnValueText('#AccHolder');
        var bankName = commonUtils.returnValueText('#BankName');
        var cANumber = commonUtils.returnValueText('#CANumber');
        var cAOrg = commonUtils.returnValueText('#CAOrg');
        var cAEffDTimeUTCStart = commonUtils.returnValueText('#CAEffDTimeUTCStart');
        var cAEffDTimeUTCEnd = commonUtils.returnValueText('#CAEffDTimeUTCEnd');

        var departmentCode = commonUtils.returnValueText('#DepartmentCode');
        var departmentName = commonUtils.returnValueText('#DepartmentName');
        var nNTType = commonUtils.returnValueText('#NNTType');
        var qtyLicense = commonUtils.returnValueText('#TotalQtyLicense');

        var lstSolution = [];
        $('#step1').find('.button-SP').each(function () {
            var obj = new Object();
            var solutioncode = $(this).attr("solutioncode");
            var valcheck = $(this).attr("valcheck");
            obj.SolutionCode = solutioncode;
            obj.ValCheck = valcheck;
            lstSolution.push(obj);
        });
        var lstSolutionCur = JSON.stringify(lstSolution);

        var objRegister = new Object();
        objRegister.MST = mST;
        objRegister.NNTFullName = nNTFullName;

        objRegister.NNTAddress = nNTAddress;
        objRegister.ProvinceCode = provinceCode;
        objRegister.DistrictCode = districtCode;

        objRegister.NNTMobile = nNTMobile;
        objRegister.NNTPhone = nNTPhone;
        objRegister.BizType = bizType;
        objRegister.BizFieldCode = bizFieldCode;
        objRegister.BizSizeCode = bizSizeCode;
        objRegister.DLCode = dLCode;
        objRegister.QtyLicense = qtyLicense;

        //step2
        objRegister.ContactName = contactName;
        objRegister.ContactPhone = contactPhone;
        objRegister.ContactEmail = contactEmail;
        objRegister.Website = website;

        //invisible
        objRegister.AccNo = accNo;
        objRegister.AccHolder = accHolder;
        objRegister.BankName = bankName;
        objRegister.CANumber = cANumber;
        objRegister.CAOrg = cAOrg;
        objRegister.CAEffDTimeUTCStart = cAEffDTimeUTCStart;
        objRegister.CAEffDTimeUTCEnd = cAEffDTimeUTCEnd;

        objRegister.NNTType = nNTType;

        var objOS_Inos_User = new Object();
        objOS_Inos_User.Email = usercode;
        objOS_Inos_User.Name = contactName;
        objOS_Inos_User.Password = password;
        objOS_Inos_User.VerificationCode = verCode;

        var objOS_Inos_UserCur = JSON.stringify(objOS_Inos_User);

        var objRQ_OS_Inos_Org = new Object(); // tạo Org cha

        var objOS_Inos_Org = new Object();

        objOS_Inos_Org.Name = nNTFullName;
        objOS_Inos_Org.OrgSize = bizSizeCode;
        objOS_Inos_Org.ContactName = contactName;
        objOS_Inos_Org.Email = contactEmail;
        objOS_Inos_Org.PhoneNo = contactPhone;

        var objiNOS_Mst_BizType = new Object();
        objiNOS_Mst_BizType.BizType = bizType;

        var objiNOS_Mst_BizField = new Object();
        objiNOS_Mst_BizField.BizFieldCode = bizFieldCode;

        var objOS_Inos_OrgCur = JSON.stringify(objOS_Inos_Org);
        var objiNOS_Mst_BizTypeCur = JSON.stringify(objiNOS_Mst_BizType);
        var objiNOS_Mst_BizFieldCur = JSON.stringify(objiNOS_Mst_BizField);

        // Tạo đơn hàng
        var objInos_LicOrder = new Object();
        objInos_LicOrder.DiscountCode = commonUtils.returnValueTextOrNull('#DiscountCode');
        objInos_LicOrder.TotalCost = commonUtils.returnValueText('#TotalAmountActual');

        var inos_DetailList = [];
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
                    inos_DetailList.push(objInos_LicOrderDetail);
                }
            });
        }
        objInos_LicOrder.Inos_DetailList = inos_DetailList;
        var objInos_LicOrderCur = JSON.stringify(objInos_LicOrder);


        if (!commonUtils.isNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {
                if (commonUtils.checkElementExists('#FlagActive')) {
                    var flagActive = '0';
                    var inputFlagActive = $('#FlagActive');
                    if (inputFlagActive !== undefined && inputFlagActive !== null) {
                        if (inputFlagActive.prop('checked')) {
                            flagActive = '1';
                        }
                    }
                    objRegister.FlagActive = flagActive;
                }
            }
        }

        var modelCur = commonUtils.returnJSONValue(objRegister);
        var data = {
            __RequestVerificationToken: token
            , mstnnt: modelCur
            , osinosuser: objOS_Inos_UserCur
            , osinosorg: objOS_Inos_OrgCur
            , inosmstbiztype: objiNOS_Mst_BizTypeCur
            , inosmstbizfield: objiNOS_Mst_BizFieldCur
            , inoslicorder: objInos_LicOrderCur
        };
        return data;
    };
    this.getData1 = function () {
        var inosNetwork = ReturnValueText('#InosNetwork');
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var mST = commonUtils.returnValueText('#MST');
        var nNTFullName = commonUtils.returnValueText('#NNTFullName');
        var nNTAddress = commonUtils.returnValueText('#NNTAddress');
        var provinceCode = commonUtils.returnValueText('#ProvinceCode');
        var districtCode = commonUtils.returnValueText('#DistrictCode');
        var nNTMobile = commonUtils.returnValueText('#NNTMobile');
        var nNTPhone = commonUtils.returnValueText('#NNTPhone');
        var bizType = commonUtils.returnValueTextOrNull('#BizType');
        var bizFieldCode = commonUtils.returnValueTextOrNull('#BizFieldCode');
        var bizSizeCode = commonUtils.returnValueTextOrNull('#BizSizeCode');
        var dLCode = commonUtils.returnValueText('#DLCode');
        var contactName = commonUtils.returnValueText('#ContactName');
        var contactPhone = commonUtils.returnValueText('#ContactPhone');
        var contactEmail = commonUtils.returnValueText('#ContactEmail');
        var website = commonUtils.returnValueText('#Website');
        //step3
        var usercode = commonUtils.returnValueText('#Usercode');
        var verCode = commonUtils.returnValueText('#VerCode');
        var password = commonUtils.returnValueText('#Password');
        var repassword = commonUtils.returnValueText('#Repassword');
        //invisible
        var accNo = commonUtils.returnValueText('#AccNo');
        var accHolder = commonUtils.returnValueText('#AccHolder');
        var bankName = commonUtils.returnValueText('#BankName');
        var cANumber = commonUtils.returnValueText('#CANumber');
        var cAOrg = commonUtils.returnValueText('#CAOrg');
        var cAEffDTimeUTCStart = commonUtils.returnValueText('#CAEffDTimeUTCStart');
        var cAEffDTimeUTCEnd = commonUtils.returnValueText('#CAEffDTimeUTCEnd');

        var departmentCode = commonUtils.returnValueText('#DepartmentCode');
        var departmentName = commonUtils.returnValueText('#DepartmentName');
        var nNTType = commonUtils.returnValueText('#NNTType');
        var qtyLicense = commonUtils.returnValueText('#TotalQtyLicense');

        var lstSolution = [];
        $('#step1').find('.button-SP').each(function () {
            var obj = new Object();
            var solutioncode = $(this).attr("solutioncode");
            var valcheck = $(this).attr("valcheck");
            obj.SolutionCode = solutioncode;
            obj.ValCheck = valcheck;
            lstSolution.push(obj);
        });
        var lstSolutionCur = JSON.stringify(lstSolution);

        var objRegister = new Object();
        objRegister.MST = mST;
        objRegister.NNTFullName = nNTFullName;
        objRegister.ContactName = contactName;
        objRegister.ContactEmail = contactEmail;

        objRegister.NNTAddress = nNTAddress;
        objRegister.ProvinceCode = provinceCode;
        objRegister.DistrictCode = districtCode;

        objRegister.NNTMobile = nNTMobile;
        objRegister.NNTPhone = nNTPhone;
        objRegister.BizType = bizType;
        objRegister.BizFieldCode = bizFieldCode;
        objRegister.BizSizeCode = bizSizeCode;
        objRegister.DLCode = dLCode;
        objRegister.QtyLicense = qtyLicense;
        
        //invisible
        objRegister.AccNo = accNo;
        objRegister.AccHolder = accHolder;
        objRegister.BankName = bankName;
        objRegister.CANumber = cANumber;
        objRegister.CAOrg = cAOrg;
        objRegister.CAEffDTimeUTCStart = cAEffDTimeUTCStart;
        objRegister.CAEffDTimeUTCEnd = cAEffDTimeUTCEnd;

        objRegister.NNTType = nNTType;
        
        var objRQ_OS_Inos_Org = new Object(); // tạo Org cha

        var objOS_Inos_Org = new Object();

        objOS_Inos_Org.Name = nNTFullName;
        objOS_Inos_Org.OrgSize = bizSizeCode;
        objOS_Inos_Org.ContactName = contactName;
        objOS_Inos_Org.Email = contactEmail;
        objOS_Inos_Org.PhoneNo = contactPhone;

        var objiNOS_Mst_BizType = new Object();
        objiNOS_Mst_BizType.BizType = bizType;

        var objiNOS_Mst_BizField = new Object();
        objiNOS_Mst_BizField.BizFieldCode = bizFieldCode;

        var objOS_Inos_OrgCur = JSON.stringify(objOS_Inos_Org);
        var objiNOS_Mst_BizTypeCur = JSON.stringify(objiNOS_Mst_BizType);
        var objiNOS_Mst_BizFieldCur = JSON.stringify(objiNOS_Mst_BizField);

        // Tạo đơn hàng
        var objInos_LicOrder = new Object();
        objInos_LicOrder.DiscountCode = commonUtils.returnValueTextOrNull('#DiscountCode');
        objInos_LicOrder.TotalCost = commonUtils.returnValueText('#TotalAmountActual');

        var inos_DetailList = [];
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
                    inos_DetailList.push(objInos_LicOrderDetail);
                }
            });
        }
        objInos_LicOrder.Inos_DetailList = inos_DetailList;
        var objInos_LicOrderCur = JSON.stringify(objInos_LicOrder);


        if (!commonUtils.isNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {
                if (commonUtils.checkElementExists('#FlagActive')) {
                    var flagActive = '0';
                    var inputFlagActive = $('#FlagActive');
                    if (inputFlagActive !== undefined && inputFlagActive !== null) {
                        if (inputFlagActive.prop('checked')) {
                            flagActive = '1';
                        }
                    }
                    objRegister.FlagActive = flagActive;
                }
            }
        }

        var modelCur = commonUtils.returnJSONValue(objRegister);
        var data = {
            __RequestVerificationToken: token
            , mstnnt: modelCur
            , osinosorg: objOS_Inos_OrgCur
            , inosmstbiztype: objiNOS_Mst_BizTypeCur
            , inosmstbizfield: objiNOS_Mst_BizFieldCur
            , inoslicorder: objInos_LicOrderCur
            , inosNetwork: inosNetwork
        };
        return data;
    };
    this.getData2 = function () {
        
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var mST = commonUtils.returnValueText('#MST');
        var nNTFullName = commonUtils.returnValueText('#NNTFullName');
        var nNTAddress = commonUtils.returnValueText('#NNTAddress');
        var provinceCode = commonUtils.returnValueText('#ProvinceCode');
        var districtCode = commonUtils.returnValueText('#DistrictCode');
        var nNTMobile = commonUtils.returnValueText('#NNTMobile');
        var nNTPhone = commonUtils.returnValueText('#NNTPhone');
        var bizType = commonUtils.returnValueTextOrNull('#BizType');
        var bizFieldCode = commonUtils.returnValueTextOrNull('#BizFieldCode');
        var bizSizeCode = commonUtils.returnValueTextOrNull('#BizSizeCode');
        var dLCode = commonUtils.returnValueText('#DLCode');
        var contactName = commonUtils.returnValueText('#ContactName');
        var contactPhone = commonUtils.returnValueText('#ContactPhone');
        var contactEmail = commonUtils.returnValueText('#ContactEmail');
        var website = commonUtils.returnValueText('#Website');
        //step3
        var usercode = commonUtils.returnValueText('#Usercode');
        var verCode = commonUtils.returnValueText('#VerCode');
        var password = commonUtils.returnValueText('#Password');
        var repassword = commonUtils.returnValueText('#Repassword');
        //invisible
        var accNo = commonUtils.returnValueText('#AccNo');
        var accHolder = commonUtils.returnValueText('#AccHolder');
        var bankName = commonUtils.returnValueText('#BankName');
        var cANumber = commonUtils.returnValueText('#CANumber');
        var cAOrg = commonUtils.returnValueText('#CAOrg');
        var cAEffDTimeUTCStart = commonUtils.returnValueText('#CAEffDTimeUTCStart');
        var cAEffDTimeUTCEnd = commonUtils.returnValueText('#CAEffDTimeUTCEnd');

        var departmentCode = commonUtils.returnValueText('#DepartmentCode');
        var departmentName = commonUtils.returnValueText('#DepartmentName');
        var nNTType = commonUtils.returnValueText('#NNTType');
        var qtyLicense = commonUtils.returnValueText('#TotalQtyLicense');

        var lstSolution = [];
        $('#step1').find('.button-SP').each(function () {
            var obj = new Object();
            var solutioncode = $(this).attr("solutioncode");
            var valcheck = $(this).attr("valcheck");
            obj.SolutionCode = solutioncode;
            obj.ValCheck = valcheck;
            lstSolution.push(obj);
        });
        var lstSolutionCur = JSON.stringify(lstSolution);

        var objRegister = new Object();
        objRegister.MST = mST;
        objRegister.NNTFullName = nNTFullName;
        objRegister.ContactName = contactName;
        objRegister.ContactEmail = contactEmail;

        objRegister.NNTAddress = nNTAddress;
        objRegister.ProvinceCode = provinceCode;
        objRegister.DistrictCode = districtCode;

        objRegister.NNTMobile = nNTMobile;
        objRegister.NNTPhone = nNTPhone;
        objRegister.BizType = bizType;
        objRegister.BizFieldCode = bizFieldCode;
        objRegister.BizSizeCode = bizSizeCode;
        objRegister.DLCode = dLCode;
        objRegister.QtyLicense = qtyLicense;

        //invisible
        objRegister.AccNo = accNo;
        objRegister.AccHolder = accHolder;
        objRegister.BankName = bankName;
        objRegister.CANumber = cANumber;
        objRegister.CAOrg = cAOrg;
        objRegister.CAEffDTimeUTCStart = cAEffDTimeUTCStart;
        objRegister.CAEffDTimeUTCEnd = cAEffDTimeUTCEnd;

        objRegister.NNTType = nNTType;

        var objRQ_OS_Inos_Org = new Object(); // tạo Org cha

        var objOS_Inos_Org = new Object();

        objOS_Inos_Org.Name = nNTFullName;
        objOS_Inos_Org.OrgSize = bizSizeCode;
        objOS_Inos_Org.ContactName = contactName;
        objOS_Inos_Org.Email = contactEmail;
        objOS_Inos_Org.PhoneNo = contactPhone;

        var objiNOS_Mst_BizType = new Object();
        objiNOS_Mst_BizType.BizType = bizType;

        var objiNOS_Mst_BizField = new Object();
        objiNOS_Mst_BizField.BizFieldCode = bizFieldCode;

        var objOS_Inos_OrgCur = JSON.stringify(objOS_Inos_Org);
        var objiNOS_Mst_BizTypeCur = JSON.stringify(objiNOS_Mst_BizType);
        var objiNOS_Mst_BizFieldCur = JSON.stringify(objiNOS_Mst_BizField);

        // Tạo đơn hàng
        var objInos_LicOrder = new Object();
        objInos_LicOrder.DiscountCode = commonUtils.returnValueTextOrNull('#DiscountCode');
        objInos_LicOrder.TotalCost = commonUtils.returnValueText('#TotalAmountActual');

        var inos_DetailList = [];
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
                    inos_DetailList.push(objInos_LicOrderDetail);
                }
            });
        }
        objInos_LicOrder.Inos_DetailList = inos_DetailList;
        var objInos_LicOrderCur = JSON.stringify(objInos_LicOrder);


        if (!commonUtils.isNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {
                if (commonUtils.checkElementExists('#FlagActive')) {
                    var flagActive = '0';
                    var inputFlagActive = $('#FlagActive');
                    if (inputFlagActive !== undefined && inputFlagActive !== null) {
                        if (inputFlagActive.prop('checked')) {
                            flagActive = '1';
                        }
                    }
                    objRegister.FlagActive = flagActive;
                }
            }
        }

        var modelCur = commonUtils.returnJSONValue(objRegister);
        var data = {
            __RequestVerificationToken: token
            , mstnnt: modelCur
            , osinosorg: objOS_Inos_OrgCur
            , inosmstbiztype: objiNOS_Mst_BizTypeCur
            , inosmstbizfield: objiNOS_Mst_BizFieldCur
            , inoslicorder: objInos_LicOrderCur
        };
        return data;
    };
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
    this.saveData1 = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData1();
        if (this.checkFormStep1(2)) {
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
    this.saveData2 = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData1();
        if (this.checkFormStep1(2)) {
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
    this.later = function () {
        var _ajaxSettings = this.ajaxSettings;
        bootbox.confirm({
            title: this.Image_Message,
            message: this.Confirm_Message,
            closeButton: false,
            buttons: {
                'cancel': {
                    label: 'Thoát',
                    className: 'btn mybtn-Button btnButton select-hide'
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
    this.createPayment = function (objRegister) {
        var Id = $("#Payment_Id").val();
        var PaymentCode = $("#Payment_PaymentCode").val();
        var Remark = $("#Payment_Remark").val();
        if (Remark === "") {
            Remark = "Thanh toan mua License";
        }
        var TotalCost = $("#Payment_TotalCost").val();
        var CreateDTime = $("#Payment_CreateDTime").val();
        var OrgID = $("#Payment_OrgID").val();

        if (PaymentCode === null || PaymentCode === "") {
            alert("Mã lệnh thanh toán không tồn tại.");
            return;
        }

        if (TotalCost === null || TotalCost === "") {
            alert("Giá trị thanh toán không hợp lệ.");
            return;
        }

        var objInos_LicOrder = {
            Id: Id,
            PaymentCode: PaymentCode,
            Remark: Remark,
            TotalCost: TotalCost,
            StrCreateDTime: CreateDTime,
            OrgID: OrgID
        };
        var language = "vn";
        var ordertype = "billpayment"; // Mặc định thanh toán hóa đơn
        var inos_LicOrder = JSON.stringify(objInos_LicOrder);

        $.ajax({
            type: objRegister.ajaxSettings.Type,
            data: {
                Inos_LicOrder: inos_LicOrder,
                language: language,
                ordertype: ordertype
            },
            url: objRegister.ajaxSettings.Url,
            dataType: objRegister.ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionCreatePayment(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
    };

    this.changeCheckBoxPackage = function (thiz) {
        var checkBox = $(thiz);
        if (checkBox !== null && checkBox !== undefined) {
            var trCur = $(checkBox).parent().parent().parent().parent();
            var divCur = $(trCur).find("div.formatnumber");
            var divCur2 = $(trCur).find("div.discountprice");
            if (checkBox.is(':checked')) {
                checkBox.prop('checked', true);
                checkBox.val('1');
                if (divCur !== undefined && divCur !== null) {
                    if ($(divCur).hasClass("display-none")) {
                        $(divCur).removeClass("display-none");
                    }
                }
                if (divCur2 !== undefined && divCur2 !== null) {
                    if ($(divCur2).hasClass("display-none")) {
                        $(divCur2).removeClass("display-none");
                    }
                }
            } else {
                checkBox.prop('checked', false);
                checkBox.val('0');
                if (divCur !== undefined && divCur !== null) {
                    if (!$(divCur).hasClass("display-none")) {
                        $(divCur).addClass("display-none");
                    }
                }
                if (divCur2 !== undefined && divCur2 !== null) {
                    if (!$(divCur2).hasClass("display-none")) {
                        $(divCur2).addClass("display-none");
                    }
                }
            }
            totalPrice();
        }
    };

    this.loadDistrict = function (thiz) {
        var provincecode = $(thiz).val();
        var _ajaxSettings = this.ajaxSettings;
        if (!commonUtils.isNullOrEmpty(provincecode)) {
            var token = $('#manageForm input[name=__RequestVerificationToken]').val();
            $.ajax({
                type: _ajaxSettings.Type,
                data: {
                    __RequestVerificationToken: token
                    , provincecode: provincecode
                },
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                doneFunctionLoadDistrict(objResult);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        } else {
            $("#DistrictCode").html('');
        }
    };
    this.loadMst_GovTaxID = function (thiz) {
        var districtcode = $(thiz).val();
        if (!commonUtils.isNullOrEmpty(districtcode)) {

            var token = $('#manageForm input[name=__RequestVerificationToken]').val();
            $.ajax({
                type: _ajaxSettings.Type,
                data: {
                    __RequestVerificationToken: token
                    , districtcode: districtcode
                },
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                doneFunctionloadMst_GovTaxID(objResult);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        } else {
            $("#GovTaxID").html('');
        }
    };

    function doneFunction(objResult) {
        if (objResult.Success) {
            var objInos_LicOrderData = objResult.Inos_LicOrder;
            var listInos_DetailList = objResult.Inos_LicOrder.Inos_DetailList;
            var packageId = '';
            $('#Payment_Id').val(objInos_LicOrderData.Id);
            $('#Payment_OrgID').val(objInos_LicOrderData.OrgId);
            $('#Payment_PaymentCode').val(objInos_LicOrderData.PaymentCode);
            $('#Payment_TotalCost').val(objInos_LicOrderData.TotalCost);
            $('#Payment_CreateDTime').val(objInos_LicOrderData.StrCreateDTime);
            $('#Payment_Remark').val(objInos_LicOrderData.Remark);

            // Tổng tiền thanh toán
            $('#subtotal').text(objInos_LicOrderData.TotalCost);
            $('#subtotal').number(true, 0);
            //

            if (listInos_DetailList !== undefined &&
                listInos_DetailList !== null &&
                listInos_DetailList.length > 0) {
                for (var i = 0; i < listInos_DetailList.length; i++) {
                    packageId += listInos_DetailList[i].PackageId;
                    if (i !== listInos_DetailList.length - 1) {
                        packageId += ';';
                    }
                }
            }
            $('#Payment_PackageId').val(packageId);

            var objRegister = new Register();
            objRegister.next();
            $(".btn-next").hide();
            $(".btn-prev").hide();
            $(".btn-register-form").hide();
            $(".btn-cancel-form").hide();
            $(".btn-pay-form").show();
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function doneFunctionloadMst_GovTaxID(objResult) {
        if (objResult.Success) {
            $("#GovTaxID").html(objResult.Html);
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function doneFunctionLoadDistrict(objResult) {

        if (objResult.Success) {
            $("#DistrictCode").html(objResult.Html);
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };

    function doneFunctionConfirmCode(objResult, objRegister) {
        if (objResult.Success) {
            objAccuracyEmail.FlagCheckReCaptcha = '1';
            sendEmailVerificationEmail(objRegister);
        }
        else {
            objAccuracyEmail.FlagCheckReCaptcha = '0';
            grecaptcha.reset();

            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            else if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function sendEmailVerificationEmail(objRegister) {

        var email = commonUtils.returnValueTextOrNull('#Usercode');
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var _ajaxSettings = objRegister.ajaxSettings;
        objAccuracyEmail.FlagCheckReCaptcha = '1';
        $.ajax({
            type: _ajaxSettings.Type,
            data: {
                __RequestVerificationToken: token
                , email: email
            },
            url: objRegister.UrlSendEmail,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionSendEmail(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
    };
    function doneFunctionSendEmail(objResult) {

        if (objResult.Success) {

            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages);
            }
            //closeModalGetCode();
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                commonUtils.window_location_href(objResult.RedirectUrl);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function doneFunctionVerifyEmail(objResult) {
        if (objResult.Success) {

            objAccuracyEmail.FlagVerifyEmail = '1';

            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Email)) {
                objAccuracyEmail.Email = objResult.Email.toString().trim();
            }
            if (!commonUtils.isNullOrEmpty(objResult.VerificationEmailCode)) {
                objAccuracyEmail.VerificationEmailCode = objResult.VerificationEmailCode.toString().trim();
            }
        }
        else {
            objAccuracyEmail.FlagVerifyEmail = '0';

            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            else if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function doneFunctionApDung(objResult) {
        //debugger;
        if (objResult.Success) {
            var objOS_Inos_LicOrder = objResult.OS_Inos_LicOrder;
            if (objOS_Inos_LicOrder !== undefined && objOS_Inos_LicOrder !== null) {

                var discountAmount = 0.0;
                var fPercent = 0.0;
                var fTotalAmount = commonUtils.returnValueFloat('#TotalAmount');
                var discountType = commonUtils.returnValue(objOS_Inos_LicOrder.DiscountType);

                var rows = $("tbody#tbodyLicense tr.trdata").length;
                if (rows > 0) {

                    $("tbody#tbodyLicense tr.trdata").each(function () {
                        var trCur = $(this);
                        var idx = trCur.attr('idx');

                        var flagdiscount = trCur.attr('flagdiscount');
                        if (flagdiscount === "True") {
                            var price = trCur.find('input[type="hidden"][name="ListOS_Inos_Package[' + idx + '].Price"]').val();

                            var discountprice = 0;
                            if (discountType.toLowerCase().trim() === '2'.toLowerCase().trim()) {
                                discountprice = price - objOS_Inos_LicOrder.DiscountAmount;
                            }
                            else if (discountType.toLowerCase().trim() === '1'.toLowerCase().trim()) {
                                discountprice = price * (100 - objOS_Inos_LicOrder.DiscountAmount) / 100;
                            }

                            //trCur.find('div[name="discountprice[' + idx + ']"]').html(formatNumberInteger(discountprice));
                            //trCur.find('.formatnumber').css('text-decoration', 'line-through rgb(255, 0, 0)').css('font-style', 'italic').css('font-size', '11px');
                        }
                    });
                }

                objMaGiamGia.MaGiamGia = objOS_Inos_LicOrder.Code;
                objMaGiamGia.LoaiGiamGia = discountType;

                if (discountType.toLowerCase().trim() === '2'.toLowerCase().trim()) {
                    var fTotalGiamGia = parseFloat(objOS_Inos_LicOrder.DiscountAmount);
                    objMaGiamGia.DiscountAmount = fTotalGiamGia;
                    $('#TotalGiamGia').val(fTotalGiamGia);
                }
                else if (discountType.toLowerCase().trim() === '1'.toLowerCase().trim()) {
                    fPercent = parseFloat(objOS_Inos_LicOrder.DiscountAmount);
                    objMaGiamGia.DiscountAmount = fPercent;
                    var fTotalGiamGia = (fTotalAmount * fPercent) / 100;
                    $('#TotalGiamGia').val(fTotalGiamGia);
                }

                $('.alert-discount').html(objOS_Inos_LicOrder.Description);
                $('.alert-discount').css('color', '#007FC7')
            }
            this.totalPrice();
        }
        else {
            $('#TotalGiamGia').val(0);

            objMaGiamGia.LoaiGiamGia = null;
            this.totalPrice();

            $('.discountprice').html('');
            //$('.formatnumber').css('text-decoration', 'none').css('font-style', 'normal').css('font-size', '12px');

            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                //commonUtils.showAlert(objResult.Messages);
                $('.alert-discount').html(objResult.Messages);
            }
            else if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function doneFunctionCreatePayment(objResult) {
        if (objResult.code === '00') {
            vnpay.open({ width: 340, height: 490, url: objResult.data });
            return false;
        } else {
            alert(objResult.Message);
        }
    };

    function failFunction(jqXHR, textStatus, errorThrown) {
    };
    function alwaysFunction() {
    };
    return this;
}