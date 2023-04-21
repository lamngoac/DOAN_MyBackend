function Mst_NNT() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!CheckIsNullOrEmpty('#MST', 'has-error-fix', 'Mã số thuế không được trống!')) {
            return false;
        }
        //if (!CheckIsNullOrEmpty('#CreatedDate', 'has-error-fix', 'Ngày tạo không được trống!')) {
        //    return false;
        //}
        if (!CheckIsNullOrEmpty('#NNTFullName', 'has-error-fix', 'Tên tổ chức không được trống!')) {
            return false;
        }
        //if (!CheckIsNullOrEmpty('#PresentBy', 'has-error-fix', 'Người đại diện không được trống!')) {
        //    return false;
        //}
        //if (!CheckIsNullOrEmpty('#NNTPosition', 'has-error-fix', 'Chức vụ không được trống!')) {
        //    return false;
        //}
        if (!CheckIsNullOrEmpty('#NNTAddress', 'has-error-fix', 'Địa chỉ không được trống!')) {
            return false;
        }
        if (!CheckIsNullOrEmpty('#ProviceCode', 'has-error-fix', 'Tỉnh/Thành phố không được trống!')) {
            return false;
        }
        if (!CheckIsNullOrEmpty('#DistrictCode', 'has-error-fix', 'Quận/Huyện không được trống!')) {
            return false;
        }
        //if (!CheckIsNullOrEmpty('#GovTaxID', 'has-error-fix', 'Cơ quan thuế quản lý không được trống!')) {
        //    return false;
        //}
        //if (!CheckIsNullOrEmpty('#CANumber', 'has-error-fix', 'Chứng thư số không được trống!')) {
        //    return false;
        //}
        //if (!CheckIsNullOrEmpty('#CAOrg', 'has-error-fix', 'Tổ thức cấp CTS không được trống!')) {
        //    return false;
        //}
        //if (!CheckIsNullOrEmpty('#ContactName', 'has-error-fix', 'Người liên hệ không được trống!')) {
        //    return false;
        //}
        //if (!CheckIsNullOrEmpty('#ContactPhone', 'has-error-fix', 'Điện thoại không được trống!')) {
        //    return false;
        //}
        //if (!CheckIsNullOrEmpty('#ContactEmail', 'has-error-fix', 'Email liên hệ không được trống!')) {
        //    return false;
        //}
        if (!CheckIsNullOrEmpty('#DLCode', 'has-error-fix', 'Mã đại lý không được trống!')) {
            return false;
        }
        if (!CheckIsNullOrEmpty('#DealerType', 'has-error-fix', 'Loại đại lý không được trống!')) {
            return false;
        }
        //var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        //var nNTMobile = $('#NNTMobile').val();
        //if (nNTMobile !== '') {
        //    if (vnf_regex.test(nNTMobile) == false) {
        //        alert('ĐT Di động không đúng định dạng!');
        //        return false;
        //    }
        //}
        //var nNTPhone = $('#NNTPhone').val();
        //if (nNTPhone !== '') {
        //    if (vnf_regex.test(nNTPhone) == false) {
        //        alert('ĐT Cố định không đúng định dạng!');
        //        return false;
        //    }
        //}
        //var contactPhone = $('#ContactPhone').val();
        //if (contactPhone !== '') {
        //    if (vnf_regex.test(contactPhone) == false) {
        //        alert('Điện thoại không đúng định dạng!');
        //        return false;
        //    }
        //}

        return true;
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
        var mSTParent = commonUtils.returnValueText('#MSTParent');
        var provinceCode = commonUtils.returnValueText('#ProvinceCode');
        var districtCode = commonUtils.returnValueText('#DistrictCode');
        //var nNTType = commonUtils.returnValueText('#NNTType');
        var dLCode = commonUtils.returnValueText('#DLCode');
        var nNTAddress = commonUtils.returnValueText('#NNTAddress');
        var nNTMobile = commonUtils.returnValueText('#NNTMobile');
        var nNTPhone = commonUtils.returnValueText('#NNTPhone');
        var nNTFax = commonUtils.returnValueText('#NNTFax');
        var presentBy = commonUtils.returnValueText('#PresentBy');
        var businessRegNo = commonUtils.returnValueText('#BusinessRegNo');
        var nNTPosition = commonUtils.returnValueText('#NNTPosition');
        var presentIDNo = commonUtils.returnValueText('#PresentIDNo');
        var presentIDType = commonUtils.returnValueText('#PresentIDType');
        var govTaxID = commonUtils.returnValueText('#GovTaxID');
        var contactName = commonUtils.returnValueText('#ContactName');
        var contactPhone = commonUtils.returnValueText('#ContactPhone');
        var contactEmail = commonUtils.returnValueText('#ContactEmail');
        var website = commonUtils.returnValueText('#Website');
        var bizType = commonUtils.returnValueText('#BizType');
        var bizFieldCode = commonUtils.returnValueText('#BizFieldCode');
        var bizSizeCode = commonUtils.returnValueText('#BizSizeCode');
        var cANumber = commonUtils.returnValueText('#CANumber');
        var cAOrg = commonUtils.returnValueText('#CAOrg');
        var cAEffDTimeUTCStart = commonUtils.returnValueText('#CAEffDTimeUTCStart');
        var cAEffDTimeUTCEnd = commonUtils.returnValueText('#CAEffDTimeUTCEnd');
        var packageCode = commonUtils.returnValueText('#PackageCode');
        var createdDate = commonUtils.returnValueText('#CreatedDate');
        var accNo = commonUtils.returnValueText('#AccNo');
        var accHolder = commonUtils.returnValueText('#AccHolder');
        var bankName = commonUtils.returnValueText('#BankName');
        var tCTStatus = commonUtils.returnValueText('#TCTStatus');
        var dealertype = commonUtils.returnValueText('#DealerType');

        var objMst_NNT = new Object();

        objMst_NNT.MST = mST;
        objMst_NNT.MSTParent = mSTParent;
        objMst_NNT.ProvinceCode = provinceCode;
        objMst_NNT.DistrictCode = districtCode;
        //objMst_NNT.NNTType = nNTType;
        objMst_NNT.DLCode = dLCode;
        objMst_NNT.NNTFullName = nNTFullName;
        objMst_NNT.NNTAddress = nNTAddress;
        objMst_NNT.NNTMobile = nNTMobile;
        objMst_NNT.NNTPhone = nNTPhone;
        objMst_NNT.NNTFax = nNTFax;
        objMst_NNT.PresentBy = presentBy;
        objMst_NNT.BusinessRegNo = businessRegNo;
        objMst_NNT.NNTPosition = nNTPosition;
        objMst_NNT.PresentIDNo = presentIDNo;
        objMst_NNT.PresentIDType = presentIDType;
        objMst_NNT.GovTaxID = govTaxID;
        objMst_NNT.ContactName = contactName;
        objMst_NNT.ContactPhone = contactPhone;
        objMst_NNT.ContactEmail = contactEmail;
        objMst_NNT.Website = website;
        objMst_NNT.BizType = bizType;
        objMst_NNT.BizFieldCode = bizFieldCode;
        objMst_NNT.BizSizeCode = bizSizeCode;
        objMst_NNT.CANumber = cANumber;
        objMst_NNT.CAOrg = cAOrg;
        objMst_NNT.CAEffDTimeUTCStart = cAEffDTimeUTCStart;
        objMst_NNT.CAEffDTimeUTCEnd = cAEffDTimeUTCEnd;
        objMst_NNT.PackageCode = packageCode;
        objMst_NNT.CreatedDate = createdDate;
        objMst_NNT.AccNo = accNo;
        objMst_NNT.AccHolder = accHolder;
        objMst_NNT.BankName = bankName;
        objMst_NNT.TCTStatus = tCTStatus;
        objMst_NNT.DealerType = dealertype;

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
                    objMst_NNT.FlagActive = flagActive;
                }
            }
        }
        var modelCur = commonUtils.returnJSONValue(objMst_NNT);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
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
    this.deleteData = function (objMst_NNT) {
        var mst = commonUtils.returnValue(objMst_NNT.MST);
        if (!commonUtils.isNullOrEmpty(mst)) {
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
                            mst: mst,
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
            commonUtils.showAlert('Chưa chọn bản ghi cần xóa!');
            return;
        }
    };
    function doneFunction(objResult) {
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
    };
    function failFunction(jqXHR, textStatus, errorThrown) {
    };
    function alwaysFunction() {
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
    function doneFunctionLoadDistrict(objResult) {
        debugger
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
    this.loadMst_GovTaxID = function (thiz) {
        var districtcode = $(thiz).val();
        var _ajaxSettings = this.ajaxSettings;
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
    this.getCert = function () {
        var nkid = commonUtils.returnValueText('#nkid');
        var tem = {};
        tem.NetworkID = nkid;
        var _ajaxSettings = this.ajaxSettings;
        $.ajax({
            type: _ajaxSettings.Type,
            data: tem,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionGetCert(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionGetCert(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
    };
    function doneFunctionGetCert(objResult) {
        if (objResult.Success) {
            debugger
            var cert = objResult.Data;
            if (cert !== null && cert !== undefined) {
                var serialNumber = commonUtils.returnValue(cert.SerialNumber);
                var SubjectCN = commonUtils.returnValue(cert.Subject.CN);
                var cAOrg = commonUtils.returnValue(cert.Issuer.CN);
                //var cAOrg = commonUtils.returnValue(cert.Issuer.O);
                var cAStartEff = commonUtils.returnValue(cert.NotBefore);
                var cAEndEff = commonUtils.returnValue(cert.NotAfter);

                $('#CANumber').val(serialNumber);
                $('#Subject').val(SubjectCN);
                $('#CAOrg').val(cAOrg);
                $('#CAEffDTimeUTCStart').val(cAStartEff);
                $('#CAEffDTimeUTCEnd').val(cAEndEff);
            }
        }
        else {
            var exception = objResult.Exception;
            if (exception !== null && exception !== undefined) {
                if (!IsNullOrEmpty(exception.ErrorDetail)) {
                    _showErrorMsg123("Lỗi!", exception.ErrorDetail);
                } else {
                    if (!IsNullOrEmpty(exception.ErrorMessage)) {
                        alert(exception.ErrorMessage);
                    } else {
                        alert("Có lỗi xảy ra xin vui lòng thử lại!");
                    }
                }
            } else {
                alert("Có lỗi xảy ra xin vui lòng thử lại!");
            }
                    //
        }
    };
    function failFunctionGetCert(jqXHR, textStatus, errorThrown) {
        var mess = jqXHR.responseJSON;
        alert('Bạn chưa cài chương trình ký số điện tử!');
    };
    return this;
}