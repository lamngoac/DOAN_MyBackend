function Sys_User() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        var listError = [];
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#OrgID', 'has-error-fix', 'Mã tổ chức không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageForm #UserName', 'has-error-fix', 'Họ tên không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageForm #UserCode', 'has-error-fix', 'Email không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageForm #MST', 'has-error-fix', 'Chi nhánh không được trống!');
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#DepartmentCode', 'has-error-fix', 'Bộ phận/Phòng ban không được trống!');
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerCodeSys', 'has-error-fix', 'Bạn cần chọn đại lý!');
        
        if (!commonUtils.isNullOrEmpty(commonUtils.returnValueTextOrNull('#manageForm #UserCode'))) {
            var usercode = $('#manageForm #UserCode').val();
            if (!commonUtils.isNullOrEmpty(usercode)) {
                if (!commonUtils.validateEmail(usercode)) {
                    var objToastr = {
                        ToastrType: 'error',
                        ToastrMessage: 'Email không hợp lệ!'
                    };
                    listError.push(objToastr);
                }
            }
        }
        var flagExist = commonUtils.returnValueText('#manageForm #FlagExist');
        if (this.ActionType === 'create') {
            if (flagExist !== "" && flagExist !== null && flagExist !== undefined && flagExist === "1") {

            } else {
                listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageForm #UserPassword', 'has-error-fix', 'Mật khẩu không được trống!');

                var password = commonUtils.returnValueText('#manageForm #UserPassword');
                if (password.trim().length < 6) {
                    var objToastr = {
                        ToastrType: 'error',
                        ToastrMessage: 'Mật khẩu > 5 ký tự!'
                    };
                    listError.push(objToastr);
                }
                listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageForm #ConfirmPassword', 'has-error-fix', 'Nhập lại mật khẩu không được trống!');

                var confirmpassword = commonUtils.returnValueText('#manageForm #ConfirmPassword');
                if (password !== confirmpassword) {
                    var objToastr = {
                        ToastrType: 'error',
                        ToastrMessage: 'Nhập lại mật khẩu không đúng, xin vui lòng nhập lại!'
                    };
                    listError.push(objToastr);
                }

                if (confirmpassword.trim().length < 6) {
                    var objToastr = {
                        ToastrType: 'error',
                        ToastrMessage: 'Nhập lại mật khẩu > 5 ký tự!'
                    };
                    listError.push(objToastr);
                }
            }            
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.checkSelectOrg = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#MST', 'has-error-fix', 'Bạn cần chọn chi nhánh!')) {
            return false;
        }
        return false;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var usercode = commonUtils.returnValueText('#manageForm #UserCode');
        var username = commonUtils.returnValueText('#manageForm #UserName');
        //var userPassword = commonUtils.returnValueText('#UserPassword');
        var mst = commonUtils.returnValueText('#manageForm #MST');
        var departmentCode = commonUtils.returnValueTextOrNull('#manageForm #DepartmentCode');
        var phoneNo = commonUtils.returnValueTextOrNull('#manageForm #PhoneNo');
        var customercodesys = commonUtils.returnValueText('#manageForm #CustomerCodeSys');
        var position = commonUtils.returnValueTextOrNull('#manageForm #Position');
        var flagExist = commonUtils.returnValueText('#manageForm #FlagExist');
        var language = commonUtils.returnValueTextOrNull('#manageForm #Language');
        var timeZone = commonUtils.returnValueTextOrNull('#manageForm #TimeZone');


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


        var objSys_User = new Object();
        objSys_User.UserCode = usercode;
        objSys_User.UserName = username;        
        objSys_User.MST = mst;
        objSys_User.DepartmentCode = departmentCode;
        objSys_User.PhoneNo = phoneNo;
        objSys_User.Position = position;
        objSys_User.FlagNNTAdmin = flagNNTAdmin;
        objSys_User.FlagSysAdmin = flagSysAdmin;
        objSys_User.CustomerCodeSys = customercodesys;
        objSys_User.ACLanguage = language;
        objSys_User.ACTimeZone = timeZone;

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
                    objSys_User.FlagActive = flagActive;
                }
            }
            if (this.ActionType === 'create') {
                if (commonUtils.checkElementExists('#UserPassword')) {
                    var flagActive = '0';
                    var userPassword = commonUtils.returnValueText('#UserPassword');
                    objSys_User.UserPassword = userPassword;
                }
            }
        }

        var modelCur = commonUtils.returnJSONValue(objSys_User);
        //debugger;
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
            flagExist: flagExist
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
    this.loadMstCustomer = function () {
        var _ajaxSettings = this.ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var mst = commonUtils.returnValueText('#MST');
        var dataInput = {
            __RequestVerificationToken: token,
            mst: mst
        };
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionLoadData(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });       
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
                        label: '<i class="fas fa-ban"></i> Thoát',
                        className: 'btn btn-cancel'
                    },
                    'confirm': {
                        label: '<i class="fas fa-check"></i> Đồng ý',
                        className: 'btn btn-nc-button btn-yes'
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
            commonUtils.showAlert('Chưa chọn bản ghi cần xóa!');
            return;
        }
    };
    this.checkExistUser = function () {
        var _ajaxSettings = this.ajaxSettings;
        var email = commonUtils.returnValueText('#UserCode');
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var dataInput = {
            __RequestVerificationToken: token,
            email: email
        };
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionCheckExistUser(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
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
    function doneFunctionLoadData(objResult) {
        if (objResult.Success) {
            $("#CustomerCodeSys").html(objResult.Html);
        } else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function doneFunctionCheckExistUser(objResult) {
        if (objResult.Success) {
            if (objResult.FlagExist) {
                //load dữ liệu và thực hiện ẩn các trường không cần thiết
                if (objResult.UserInos !== null && objResult.UserInos !== undefined && objResult.UserInos !== "") {
                    $("#manageForm #UserName").val(objResult.UserInos.Name);
                    $("#manageForm #UserName").addClass("disabled-fix");
                    $("#manageForm #PhoneNo").val(objResult.UserInos.Phone);
                    $("#manageForm #PhoneNo").addClass("disabled-fix");
                    $("#manageForm #ACId").val(objResult.UserInos.Id);
                    $("#manageForm #UserPassword").parent().parent().parent().css("display","none");
                    $("#manageForm #ConfirmPassword").parent().parent().parent().css("display", "none");
                    $("#manageForm #Language").val(objResult.UserInos.Language);
                    $("#manageForm #Language").addClass("disabled-fix");
                    $("#manageForm #TimeZone").val(objResult.UserInos.TimeZone);
                    $("#manageForm #TimeZone").addClass("disabled-fix");
                    if (objResult.UserInos.Avatar !== null && objResult.UserInos.Avatar !== "" && objResult.UserInos.Avatar !== undefined) {
                        $("#manageForm img.img-avatar").attr('src', objResult.UserInos.Avatar);
                    }                    
                    $("#manageForm #FlagExist").val("1");
                }
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