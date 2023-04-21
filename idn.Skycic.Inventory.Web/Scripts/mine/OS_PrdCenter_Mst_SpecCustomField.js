function OS_PrdCenter_Mst_SpecCustomField() {
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
    this.getData = function () {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var lstMst_SpecCustomField = [];
        var idx = 0;
        $('div.divcusfield').each(function() {
            idx = $(this).attr('idx');
            var flagActive = ReturnValue($(this).find('input[type="checkbox"][name="ListMst_SpecCustomField[' + idx + '].FlagActive"]').val());
            var specCustomFieldCode = ReturnValue($(this).find('input[type="hidden"][name="ListMst_SpecCustomField[' + idx + '].SpecCustomFieldCode"]').val());
            var specCustomFieldName = ReturnValue($(this).find('input[type="text"][name="ListMst_SpecCustomField[' + idx + '].SpecCustomFieldName"]').val());
            var objSpecCustomField = {};

            objSpecCustomField.FlagActive = flagActive;
            objSpecCustomField.SpecCustomFieldCode = specCustomFieldCode;
            objSpecCustomField.SpecCustomFieldName = specCustomFieldName;

            lstMst_SpecCustomField.push(objSpecCustomField);
        });
        
        var modelCur = JSON.stringify(lstMst_SpecCustomField);
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