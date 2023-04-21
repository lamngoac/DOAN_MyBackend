function Mst_ColumnConfigGroup() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';

    this.checkForm = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ColumnConfigGrpCode', 'has-error-fix', 'Mã nhóm không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ColumnGrpName', 'has-error-fix', 'Tên nhóm không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ColumnGrpFormat', 'has-error-fix', 'Định dạng số không được trống!');



        var columnGrpFormat = commonUtils.returnValueText("#ColumnGrpFormat");

        if (parseInt(columnGrpFormat) < 0) {
            var objToastr = {
                ToastrType: 'error',
                ToastrMessage: 'Bạn cần phải nhập số!'
            };
            listError.push(objToastr);
            $("#ColumnGrpFormat").focus().addClass("has-error-fix");
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;


    }

    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };

    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var columnConfigGrpCode = commonUtils.returnValueText('#ColumnConfigGrpCode');
        var columnGrpName = commonUtils.returnValueText('#ColumnGrpName');
        var columnGrpFormat = commonUtils.returnValueText('#ColumnGrpFormat');
        var columnGrpDesc = commonUtils.returnValueText('#ColumnGrpDesc');



        var objMst_ColumnConfigGroup = new Object();
        objMst_ColumnConfigGroup.ColumnConfigGrpCode = columnConfigGrpCode;
        objMst_ColumnConfigGroup.ColumnGrpName = columnGrpName;
        objMst_ColumnConfigGroup.ColumnGrpFormat = columnGrpFormat;
        objMst_ColumnConfigGroup.ColumnGrpDesc = columnGrpDesc;
        objMst_ColumnConfigGroup.FlagActive = flagActive;

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
                    objMst_ColumnConfigGroup.FlagActive = flagActive;
                }
            }
        }
        var modelCur = commonUtils.returnJSONValue(objMst_ColumnConfigGroup);
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
    }




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