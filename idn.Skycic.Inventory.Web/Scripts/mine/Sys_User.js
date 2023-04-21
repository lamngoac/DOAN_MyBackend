function Sys_User() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#UserName', 'has-error-fix', 'Tên người dùng không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#MST', 'has-error-fix', 'Trực thuộc đơn vị không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#DepartmentCode', 'has-error-fix', 'Bộ phận/phòng ban không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#UserPassword', 'has-error-fix', 'Mật khẩu không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#UserCode',
            'has-error-fix',
            'Mã người dùng không được trống')) {
            return false;
        } else {
            var usercode = $('#UserCode').val();
            if (!commonUtils.isNullOrEmpty(usercode)) {
                if (!commonUtils.validateEmail(usercode)) {
                    commonUtils.addClassCss('#UserCode', 'has-error-fix');
                    $('#Email').focus();
                    alert('@MvcHtmlString.Create("Mã người dùng không hợp lệ!")');
                    return false;
                }
                else {
                    commonUtils.removeClassCss('#UserCode', 'has-error-fix');
                }
            }
        }
        if (!commonUtils.isNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'create') {
                var password = commonUtils.returnValueTextOrNull('#UserPassword');
                if (password.trim().length < 6) {
                    alert('@MvcHtmlString.Create("Mật khẩu > 5 ký tự!")');
                    !commonUtils.addClassCss('#UserPassword', 'has-error-fix');
                    $('#Password').focus();
                    return false;
                }
                else {
                    commonUtils.removeClassCss('#UserPassword', 'has-error-fix');
                }


                if (!commonUtils.checkElementIsNullOrEmpty('#ConfirmPassword', 'has-error-fix', '@MvcHtmlString.Create("Nhập lại mật khẩu không được trống!")')) {
                    return false;
                }
                var confirmpassword = commonUtils.returnValueTextOrNull('#ConfirmPassword');
                if (password !== confirmpassword) {
                    alert('@MvcHtmlString.Create("Nhập lại mật khẩu không đúng, xin vui lòng nhập lại!")');
                    commonUtils.addClassCss('#ConfirmPassword', 'has-error-fix');
                    $('#ConfirmPassword').focus();
                    return false;
                }
                else {
                    commonUtils.removeClassCss('#ConfirmPassword', 'has-error-fix');
                }

                if (confirmpassword.trim().length < 6) {
                    alert('@MvcHtmlString.Create("Nhập lại mật khẩu > 5 ký tự!")');
                    commonUtils.addClassCss('#ConfirmPassword', 'has-error-fix');
                    $('#ConfirmPassword').focus();
                    return false;
                }
                else {
                    commonUtils.removeClassCss('#ConfirmPassword', 'has-error-fix');
                }

            }
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
        var objSys_User = new Object();
        objSys_User.UserCode = commonUtils.returnValueText('#UserCode');
        objSys_User.UserName = commonUtils.returnValueText('#UserName');
        objSys_User.UserPassword = commonUtils.returnValueText('#UserPassword');
        objSys_User.MST = commonUtils.returnValueText('#MST');
        objSys_User.DepartmentCode = commonUtils.returnValueText('#DepartmentCode');
        objSys_User.PhoneNo = commonUtils.returnValueText('#PhoneNo');

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
                    var flagNNTAdmin = '0';
                    var inputFlagNNTAdmin = $('#FlagNNTAdmin');
                    if (inputFlagNNTAdmin !== undefined && inputFlagNNTAdmin !== null) {
                        if (inputFlagNNTAdmin.prop('checked')) {
                            flagNNTAdmin = '1';
                        }
                    }

                    var flagSysAdmin = '0';
                    var inputFlagSysAdmin = $('#FlagSysAdmin');
                    if (inputFlagSysAdmin !== undefined && inputFlagSysAdmin !== null) {
                        if (inputFlagSysAdmin.prop('checked')) {
                            flagSysAdmin = '1';
                        }
                    }
                    objSys_User.FlagActive = flagActive;
                    objSys_User.FlagNNTAdmin = flagNNTAdmin;
                    objSys_User.FlagSysAdmin = flagSysAdmin;
                }

            }
            else
            {
                var flagNNTAdmin = '0';
                var inputFlagNNTAdmin = $('#FlagNNTAdmin');
                if (inputFlagNNTAdmin !== undefined && inputFlagNNTAdmin !== null) {
                    if (inputFlagNNTAdmin.prop('checked')) {
                        flagNNTAdmin = '1';
                    }
                }

                var flagSysAdmin = '0';
                var inputFlagSysAdmin = $('#FlagSysAdmin');
                if (inputFlagSysAdmin !== undefined && inputFlagSysAdmin !== null) {
                    if (inputFlagSysAdmin.prop('checked')) {
                        flagSysAdmin = '1';
                    }
                }
                objSys_User.FlagNNTAdmin = flagNNTAdmin;
                objSys_User.FlagSysAdmin = flagSysAdmin;
            }
        }
        var modelCur = JSON.stringify(objSys_User);
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
    this.deleteData = function (objSys_User) {
        var usercode = commonUtils.returnValue(objSys_User.UserCode);
        var username = commonUtils.returnValue(objSys_User.UserName);

        if (!commonUtils.isNullOrEmpty(usercode) && !commonUtils.isNullOrEmpty(username)) {
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
                            usercode: usercode,
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
            alert("Chưa chọn bản ghi cần xóa!");
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