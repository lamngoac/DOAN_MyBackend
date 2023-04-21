function Mst_Area() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };


    this.checkForm = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#areaname', 'has-error-fix', 'Tên khu vực không được trống!');
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;

    }

    this.getData = function () {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();


        var areName = $('#areaname').val();
        var areDes = $('#areades').val();
        var areParentCode = $('#AreaCodeParent').val();
        var flagActive = $('#FlagActive').val();

        var tem = {};
        tem.AreaName = areName;
        tem.AreaDesc = areDes;
        tem.FlagActive = flagActive;
        tem.AreaCodeParent = areParentCode;
        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;


    };

    this.getDataUpdate = function () {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var areaCode = $('#areaCode').val();
        var areName = $('#areaname').val();
        var areDes = $('#areades').val();
        var areParentCode = $('#AreaCodeParent').val();
        var flagActive = $('#FlagActive').val();


        var tem = {};
        tem.AreaCode = areaCode;
        tem.AreaName = areName;
        tem.AreaDesc = areDes;
        tem.FlagActive = flagActive;
        tem.AreaCodeParent = areParentCode;


        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    }


    this.getDataDelete = function () {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var areaCode = $('#areaCode').val();
        var tem = {};
        tem.AreaCode = areaCode;
        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    }

    this.saveData = function () {
        debugger
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

    this.updateData = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataUpdate();
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


    this.deleteData = function (areacode) {
        debugger

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
                    debugger

                    //var _ajaxSettings = this.ajaxSettings;
                    //var dataInput = this.getDataDelete();
                    var token = $('#manageForm input[name=__RequestVerificationToken]').val();
                    var areaCode = areacode;
                    var tem = {};
                    tem.AreaCode = areaCode;
                    var modelCur = commonUtils.returnJSONValue(tem);
                    var data = {
                        __RequestVerificationToken: token,
                        model: modelCur,
                    };

                    $.ajax({
                        type: _ajaxSettings.Type,
                        data: data,
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

    };


    //function doneFunction(objResult) {
    //    if (objResult.Success) {
    //        if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
    //            commonUtils.showAlert(objResult.Messages[0]);
    //        }
    //        if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
    //            commonUtils.window_location_href(objResult.RedirectUrl);
    //        }
    //    }
    //    else {
    //        if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
    //            _showErrorMsg123('Lỗi!', objResult.Detail);
    //        }
    //    }
    //};



    function doneFunction(objResult) {
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                var listToastr = [];
                objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages[0] };
                listToastr.push(objToastr);
                commonUtils.showToastr(listToastr);
            }
            setTimeout(function () {
                if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                    var redirectUrl = objResult.RedirectUrl;
                    commonUtils.window_location_href(redirectUrl);
                }
            }, 1000);
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
