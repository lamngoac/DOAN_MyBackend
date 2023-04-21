function Sys_Access() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {        
        return true;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };

    this.deleteData = function (objSys_Access) {
        var Accesscode = commonUtils.returnValue(objSys_Access.AccessCode);
        var Accessname = commonUtils.returnValue(objSys_Access.AccessName);
        if (!commonUtils.isNullOrEmpty(Accesscode) && !commonUtils.isNullOrEmpty(Accessname)) {
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
                            Accesscode: Accesscode,
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
    this.getFunction = function (objectcode) {
        if (!commonUtils.isNullOrEmpty(objectcode)) {
            var token = $('#manageForm input[name=__RequestVerificationToken]').val();
            var _ajaxSettings = this.ajaxSettings;
            var dataInput = {
                __RequestVerificationToken: token,
                objectcode: objectcode,
            };
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                if (objResult.Success) {
                    $('#ShowPopup').addClass('show');
                    $('#ShowPopup').css('display', 'block');
                    $('#ShowPopup').modal({
                        backdrop: false,
                        keyboard: true,
                    });
                    $("#ShowPopup").html(objResult.Html);
                } else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        }
    };
    this.closePopup = function () {
        $('#ShowPopup').html('');
        $('#ShowPopup').modal({
            backdrop: true,
            keyboard: false,
            show: false
        });
        $('#ShowPopup').removeClass('show');
        $('#ShowPopup').css('display', 'none');
    };
    this.getDetailData = function (objectcode) {
        if (!commonUtils.isNullOrEmpty(objectcode)) {
            var token = $('#manageForm input[name=__RequestVerificationToken]').val();
            var _ajaxSettings = this.ajaxSettings;
            var dataInput = {
                __RequestVerificationToken: token,
                objectcode: objectcode,
            };
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                if (objResult.Success) {
                    $("#divDtl").html(objResult.Html);
                } else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        }
    };

    this.getData = function () {
        var token = $('#manageFormPopupOfSysGroup input[name=__RequestVerificationToken]').val();
        var rows = $("tbody#table-tbodyID-popup tr.trdata").length;
        if (rows > 0) {
            var menucode = $('#ObjectCodePU').val();
            if (!commonUtils.isNullOrEmpty(menucode)) {
                var objSys_Access = new Object();
                objSys_Access.MenuCode = menucode;

                var ListObjectCode = [];
                var trArr = $('tbody#table-tbodyID-popup tr.trdata').has('input[name="ckbpopup"]:checked');
                if (trArr !== undefined && trArr !== null && trArr.length > 0) {

                    for (var i = 0; i < trArr.length; i++) {
                        var trCur = trArr[i];
                        if (trCur != null && trCur != undefined) {
                            //debugger;
                            var idx = $(trCur).attr('idx');
                            var objectCodeCur = $(trCur).find('input[type="hidden"][name="ListSysObject[' + idx + '].ObjectCode"]').val();

                            ListObjectCode.push(objectCodeCur);
                        }
                    }
                }
                objSys_Access.Lst_FuncCode = ListObjectCode;
                var modelCur = commonUtils.returnJSONValue(objSys_Access);
                var data = {
                    __RequestVerificationToken: token,
                    model: modelCur,
                };
                return data;
            }
            else {
                alert('Mã nhóm người dùng trống!');
                return false;
            }
        }
        else {
            alert('Lưới danh sách FUNC trống!');
            return false;
        }

    };
    this.saveData = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData();
        if (dataInput === false) {
            return false;
        } else {
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                traditional: true,
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