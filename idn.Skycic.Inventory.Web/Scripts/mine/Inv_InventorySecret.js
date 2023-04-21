function Inv_InventorySecret() {
    this.ajaxSettingsInit = function () {
        var _ajaxSettings = {
            Type: '',
            DataType: '',
            Url: '',
        };
        this.ajaxSettings = _ajaxSettings;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };

    // Start Export Excel
    this.getDataExportExcel = function (listSearchInput) {
        var data = {};
        var token = '';
        if (commonUtils.checkElementExists('#' + this.Id_FormMain)) {
            token = $('#' + this.Id_FormMain + ' input[name=__RequestVerificationToken]').val();
        } else {
            var token = $('input[name=__RequestVerificationToken]').val();
        }
        if (!IsNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }

        if (listSearchInput !== undefined && listSearchInput !== null && listSearchInput.length > 0) {
            var iCount = listSearchInput.length;
            for (var i = 0; i < iCount; i++) {
                if (listSearchInput[i] != null) {
                    if (!IsNullOrEmpty(listSearchInput[i].Key)) {
                        var key = ReturnValue(listSearchInput[i].Key);
                        var value = ReturnValue(listSearchInput[i].Value);
                        data[key] = value;
                    }
                }
            }
        }

        return data;
    };
    this.exportExcel = function (listSearchInput) {
        var _ajaxSettings = this.ajaxSettings;
        if (listSearchInput === undefined || listSearchInput === null || listSearchInput.length === 0) {
            listSearchInput = [];
        }
        var dataInput = this.getDataExportExcel(listSearchInput);
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionExport(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionExport(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionExport();
        });
    }
    function doneFunctionExport(objResult) {
        if (objResult.Success) {
            var title = '';
            if (!commonUtils.isNullOrEmpty(objResult.Title)) {
                title = commonUtils.returnValue(objResult.Title);
                commonUtils.showAlert(title);
            }
            if (!commonUtils.isNullOrEmpty(objResult.CheckSuccess) && !commonUtils.isNullOrEmpty(objResult.strUrl)) {
                var checkSuccess = commonUtils.returnValue(objResult.CheckSuccess);
                var strUrl = commonUtils.returnValue(objResult.strUrl);
                if (checkSuccess === '1' && !commonUtils.isNullOrEmpty(strUrl)) {
                    commonUtils.window_location_href(strUrl);
                }
            }
        } else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
            var strUrl = commonUtils.returnValue(objResult.strUrl);
            if (!commonUtils.isNullOrEmpty(strUrl)) {
                commonUtils.window_location_href(strUrl);
            }
        }
    }
    function failFunctionExport(jqXHR, textStatus, errorThrown) {

    }
    function alwaysFunctionExport() {

    }
    // End Export Excel

    return this;
}