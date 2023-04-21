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
        objSys_Group.GroupCode = groupcode;
        objSys_Group.GroupName = groupname;

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
                    objSys_Group.FlagActive = flagActive;
                }
            }
        }
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
    this.deleteData = function (objSys_Group) {
        var groupcode = commonUtils.returnValue(objSys_Group.GroupCode);
        var groupname = commonUtils.returnValue(objSys_Group.GroupName);
        if (!commonUtils.isNullOrEmpty(groupcode) && !commonUtils.isNullOrEmpty(groupname)) {
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
                            groupcode: groupcode,
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
    this.getSysUser = function (groupcode) {
        var _ajaxSettings = this.ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        if (!commonUtils.isNullOrEmpty(groupcode)) {
            $.ajax({
                url: _ajaxSettings.Url,
                data: {
                    __RequestVerificationToken: token
                    , groupcode: groupcode
                },
                type: _ajaxSettings.Type,
                dataType: _ajaxSettings.DataType,
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        $("#ShowPopup").addClass('show');
                        $("#ShowPopup").css('display','block');
                        $("#ShowPopup").html(data.Html);
                        $('#ShowPopup').modal({
                            backdrop: false,
                            keyboard: true,
                        });
                        
                    } else {
                        _showErrorMsg123("Lỗi!", data.Detail);
                    }

                }
            });
        }
        else {
            commonUtils.showAlert('Chưa chọn nhóm người dùng!');
            return;
        }
    };
    this.saveUserInGroup = function () {
        var token = $('#manageFormPopupOfSysGroup input[name=__RequestVerificationToken]').val();
        var rows = $("tbody#table-tbodyID-popup tr.trdata").length;
        var _ajaxSettings = this.ajaxSettings;

        if (rows > 0) {
            var groupcode = $('#GroupCodePU').val();
            if (!commonUtils.isNullOrEmpty(groupcode)) {
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
                var modelCur = JSON.stringify(tem);
                
                $.ajax({
                    type: _ajaxSettings.Type,
                    url: _ajaxSettings.Url,
                    data: {
                        __RequestVerificationToken: token
                        , model: modelCur
                    },
                    traditional: true,
                    //contentType: "application/json; charset=utf-8",
                    dataType: _ajaxSettings.DataType,
                    success: function (data) {
                        if (data.Success) {
                            alert(data.Title);

                            var rowsMaster = $("tbody#tbodyID tr.trdata").length;
                            if (rowsMaster > 0) {
                                $('.selected').removeClass('selected');
                                $("tbody#tbodyID tr.trdata").each(function () {
                                    var tr = $(this);
                                    if (tr !== undefined && tr !== null) {
                                        var groupcodeCur = tr.attr('groupcode');
                                        if (groupcodeCur !== undefined && groupcodeCur !== null && groupcodeCur.toString().trim().length > 0) {
                                            if (groupcodeCur.toUpperCase().trim() === groupcode.toUpperCase().trim()) {
                                                $(tr).addClass('selected');
                                                return;
                                            }
                                        }
                                    }

                                });

                            }
                            GetDetailData(groupcode);
                            ClosePopup();

                        }
                        else {
                            if (data.Detail != null) {
                                _showErrorMsg123("Lỗi!", data.Detail);
                            }
                        }
                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        debugger;
                        if (textStatus == 'Unauthorized') {
                            alert('custom message. Error: ' + errorThrown);
                        } else {
                            //alert('custom message. Error2: ' + errorThrown);
                        }
                    }
                });
            }
            else {
                alert('Mã nhóm người dùng trống!');
                return false;
            }
        }
        else {
            alert('Lưới danh sách người dùng trống!');
            return false;
        }
    };
    this.getDetailData = function (groupcode) {
        var _ajaxSettings = this.ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        $.ajax({
            url: _ajaxSettings.Url,
            data: {
                __RequestVerificationToken: token
                , groupcode: groupcode
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
            traditional: true,
            success: function (data) {
                if (data.Success) {
                    $("#divDtl").html(data.Html);
                } else {
                    _showErrorMsg123("Lỗi!", data.Detail);
                }

            }
        });
    };
    this.getSysObject = function (groupcode) {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var _ajaxSettings = this.ajaxSettings;
        $.ajax({
            url: _ajaxSettings.Url,
            data: {
                __RequestVerificationToken: token
                , groupcode: groupcode
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
            traditional: true,
            success: function (data) {
                if (data.Success) {
                    $("#ShowPopup").addClass('show');
                    $("#ShowPopup").css('display', 'block');
                    $('#ShowPopup').modal({
                        backdrop: false,
                        keyboard: true,
                    });
                    $("#ShowPopup").html(data.Html);
                } else {
                    _showErrorMsg123("Lỗi!", data.Detail);
                }

            }
        });
    };
    this.saveObjectInGroup = function () {
        var _ajaxSettings = this.ajaxSettings;
        var rows = $("tbody#table-tbodyID-popup tr.trdata").length;
        var token = $('#manageFormPopupOfSysGroup input[name=__RequestVerificationToken]').val();
        if (rows > 0) {
            var groupcode = $('#GroupCodePU').val();
            if (!commonUtils.isNullOrEmpty(groupcode)) {
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
                
                var modelCur = JSON.stringify(tem);                
                $.ajax({
                    type: _ajaxSettings.Type,
                    url: _ajaxSettings.Url,
                    data: {
                        __RequestVerificationToken: token
                        , model: modelCur
                    },
                    traditional: true,
                    //contentType: "application/json; charset=utf-8",
                    dataType: _ajaxSettings.DataType,
                    success: function (data) {
                        if (data.Success) {
                            alert(data.Title);
                            ClosePopup();
                        }
                        else {
                            if (data.Detail != null) {
                                _showErrorMsg123("Lỗi!", data.Detail);
                            }
                        }
                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        if (textStatus == 'Unauthorized') {
                            alert('custom message. Error: ' + errorThrown);
                        } else {
                            //alert('custom message. Error2: ' + errorThrown);
                        }
                    }
                });
            }
            else {
                alert('Mã nhóm người dùng trống!');
                return false;
            }
        }
        else {
            alert('Lưới danh sách module trống!');
            return false;
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
    return this;
}