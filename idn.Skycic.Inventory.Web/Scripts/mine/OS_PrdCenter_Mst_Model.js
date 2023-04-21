function OS_PrdCenter_Mst_Model() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#OrgID', 'has-error-fix', 'Mã tổ chức không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#ModelCode', 'has-error-fix', 'Mã Model không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#ModelName', 'has-error-fix', 'Tên Model không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#BrandCode', 'has-error-fix', 'Chưa chọn mã Brand!')) {
            return false;
        }
        var remark = $('#Remark').val();
        if (remark !== undefined && remark !== null && remark.toString().trim().length > 400) {
            AddClassCss('#Remark', 'has-error-fix');
            $('#Remark').focus();
            alert('Mô tả > 400 ký tự!');
            return false;
        } else {
            RemoveClassCss('#Remark', 'has-error-fix');
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

        var brandCode = commonUtils.returnValueText('#BrandCode');
        var modelCode = commonUtils.returnValueText('#ModelCode');
        var modelName = commonUtils.returnValueText('#ModelName');
        var orgModelCode = commonUtils.returnValueText('#OrgModelCode');
        var remark = commonUtils.returnValueText('#Remark');
        var org = commonUtils.returnValueText('#OrgID');

        var objMst_Model = new Object();
        objMst_Model.BrandCode = brandCode;
        objMst_Model.ModelCode = modelCode;
        objMst_Model.ModelName = modelName;
        objMst_Model.OrgModelCode = orgModelCode;
        objMst_Model.Remark = remark;
        objMst_Model.OrgID = org;

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
                    objMst_Model.FlagActive = flagActive;
                }
            }
        }
        var modelCur = JSON.stringify(objMst_Model);
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
    this.deleteData = function (objOS_PrdCenter_Mst_Model) {
        var modelcode = commonUtils.returnValue(objOS_PrdCenter_Mst_Model.ModelCode);
        var modelname = commonUtils.returnValue(objOS_PrdCenter_Mst_Model.ModelName);
        var orgID = commonUtils.returnValue(objOS_PrdCenter_Mst_Model.OrgID);

        if (!commonUtils.isNullOrEmpty(modelcode) && !commonUtils.isNullOrEmpty(modelname)) {
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
                            modelcode: modelcode,
                            orgID: orgID,
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