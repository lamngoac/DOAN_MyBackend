function Sys_Group() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#GroupCode', 'has-error-fix', 'Mã nhóm người dùng không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#GroupName', 'has-error-fix', 'Tên nhóm người dùng không được trống!')) {
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
        var groupcode = commonUtils.returnValueText('#GroupCode');
        var groupname = commonUtils.returnValueText('#GroupName');
        var objSys_Group = new Object();

        var modelCur = commonUtils.returnJSONValue(objSys_Group);
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
    this.closePopup = function () {
        $('#ShowPopup').modal("hide");
        $('#ShowPopup').html('');
    }
    this.getSysUser = function (groupcode) {
        if (groupcode !== undefined && groupcode !== null && groupcode.toString().trim().length > 0) {
            var _ajaxSettings = this.ajaxSettings;
            var token = $('#manageForm input[name=__RequestVerificationToken]').val();
            var dataInput = {
                groupcode: groupcode,
                __RequestVerificationToken: token,
            };
            if (this.checkForm()) {
                $.ajax({
                    type: _ajaxSettings.Type,
                    data: dataInput,
                    url: _ajaxSettings.Url,
                    dataType: _ajaxSettings.DataType,
                    beforeSend: function () {
                    }
                }).done(function (objResult) {
                    if (objResult.Success) {
                        $('#ShowPopup').modal({
                            backdrop: false,
                            keyboard: true,
                        });
                        $("#ShowPopup").html(objResult.Html);
                    } else {
                        _showErrorMsg123("Lỗi!", objResult.Detail);
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    failFunction(jqXHR, textStatus, errorThrown);
                }).always(function () {
                    alwaysFunction();
                });
            }
        }

    };

    this.getSysObject = function (groupcode) {
        if (groupcode !== undefined && groupcode !== null && groupcode.toString().trim().length > 0) {
            var _ajaxSettings = this.ajaxSettings;
            var token = $('#manageForm input[name=__RequestVerificationToken]').val();
            var dataInput = {
                groupcode: groupcode,
                __RequestVerificationToken: token,
            };
            if (this.checkForm()) {
                $.ajax({
                    type: _ajaxSettings.Type,
                    data: dataInput,
                    url: _ajaxSettings.Url,
                    dataType: _ajaxSettings.DataType,
                    beforeSend: function () {
                    }
                }).done(function (objResult) {
                    if (objResult.Success) {
                        $('#ShowPopup').modal({
                            backdrop: false,
                            keyboard: true,
                        });
                        $("#ShowPopup").html(objResult.Html);
                    } else {
                        _showErrorMsg123("Lỗi!", objResult.Detail);
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    failFunction(jqXHR, textStatus, errorThrown);
                }).always(function () {
                    alwaysFunction();
                });
            }
        }

    };
    this.getDataSysUser = function () {
        var rows = $("tbody#table-tbodyID-popup tr.trdata").length;
        if (rows > 0) {
            var groupcode = $('#GroupCodePU').val();
            if (!IsNullOrEmpty(groupcode)) {
                var trArr = $('tbody#table-tbodyID-popup tr.trdata').has('input[name="ckbpopup"]:checked');
                var tem = new Object();
                var sysGroup = new Object();
                sysGroup.GroupCode = groupcode;

                tem.Sys_Group = sysGroup;

                var ListSysUserInGroup = [];
                if (trArr !== undefined && trArr !== null && trArr.length > 0) {
                    for (var i = 0; i < trArr.length; i++) {
                        var trCur = trArr[i];
                        if (trCur != null && trCur != undefined) {
                            var idx = $(trCur).attr('idx');
                            var userCodeCur = $(trCur).find('input[type="hidden"][name="ListSysUser[' + idx + '].UserCode"]').val();

                            var temSysUserInGroup = {};
                            temSysUserInGroup.UserCode = userCodeCur;
                            temSysUserInGroup.GroupCode = groupcode;

                            ListSysUserInGroup.push(temSysUserInGroup);
                        }
                    }
                }
                tem.Lst_Sys_UserInGroup = ListSysUserInGroup;
                var token = $('#manageFormPopupOfSysGroup input[name=__RequestVerificationToken]').val();
                var modelCur = JSON.stringify(tem);
                var dataInput = {
                    __RequestVerificationToken: token,
                    model: modelCur,
                };
                return dataInput;
            } else {
                alert('Mã nhóm người dùng trống!');
                return false;
            }
        } else {
            alert('Lưới danh sách người dùng trống!');
            return false;
        }        
    };
    this.getDataSysObject = function () {
        var rows = $("tbody#table-tbodyID-popup tr.trdata").length;
        if (rows > 0) {
            var groupcode = $('#GroupCodePU').val();
            if (!IsNullOrEmpty(groupcode)) {
                var tem = new Object();
                var sysGroup = new Object();
                sysGroup.GroupCode = groupcode;

                tem.Sys_Group = sysGroup;
                var ListSysObject = [];
                var trArr = $('tbody#table-tbodyID-popup tr.trdata').has('input[name="ckbpopup"]:checked');
                if (trArr !== undefined && trArr !== null && trArr.length > 0) {

                    for (var i = 0; i < trArr.length; i++) {
                        var trCur = trArr[i];
                        if (trCur != null && trCur != undefined) {
                            var idx = $(trCur).attr('idx');
                            var objectCodeCur = $(trCur).find('input[type="hidden"][name="ListSysObject[' + idx + '].ObjectCode"]').val();
                            var so_ObjectTypeCur = $(trCur).find('input[type="hidden"][name="ListSysObject[' + idx + '].so_ObjectType"]').val();
                            var temSysObject = {};
                            temSysObject.ObjectCode = objectCodeCur;
                            temSysObject.GroupCode = groupcode;
                            temSysObject.so_ObjectType = so_ObjectTypeCur;
                            ListSysObject.push(temSysObject);
                        }
                    }
                }
                tem.Lst_Sys_Access = ListSysObject;
                var token = $('#manageFormPopupOfSysGroup input[name=__RequestVerificationToken]').val();
                var modelCur = JSON.stringify(tem);
                var dataInput = {
                    __RequestVerificationToken: token,
                    model: modelCur,
                };
                return dataInput;
            } else {
                alert('Mã nhóm người dùng trống!');
                return false;
            }
        } else {
            alert('Lưới danh sách module trống!');
            return false;
        }
    };
    this.saveUserInGroup = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataSysUser();
        if (dataInput !== false) {
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
        else {
            return false;
        }
    };
    this.saveObjectInGroup = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataSysObject();
        if (dataInput !== false) {
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
        else {
            return false;
        }
    };
    this.deleteData = function (objSys_Group) {
        var groupCode = commonUtils.returnValue(objSys_Group.GroupCode);
        if (!commonUtils.isNullOrEmpty(groupCode)) {
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
                            groupcode: groupCode,
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
            if (!commonUtils.isNullOrEmpty(objResult.Title)) {
                commonUtils.showAlert(objResult.Title);
            }
            if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                commonUtils.window_location_href(objResult.RedirectUrl);
            } else {
                window.location.href = window.location.href;
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