function Mst_Sys_Config() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';


    this.getData = function () {
        debugger
        var token = $('input[name=__RequestVerificationToken]').val();

        var listMst_Sys_Config = [];
        var tem = {};

        for (var i = 0; i < 4; i++) {
            debugger
            var sysConfigID = $('input[name="Lst_Mst_Sys_Config[' + i + '].SysConfigID"]').val();
            //var flagActive = $('input[name="Lst_Mst_Sys_Config[' + i + '].FlagActive"]').val();

            var flagActive = '0';
            var inputflagActive = $('#flagshow-' + i);



            if (inputflagActive !== undefined && inputflagActive) {
                if (inputflagActive.prop('checked')) {
                    flagActive = '1';
                }
            }



            
            var obj = {
                SysConfigID: sysConfigID,
                FlagActive: flagActive
            };
            listMst_Sys_Config.push(obj);
        };
        tem.Lst_Mst_Sys_Config = listMst_Sys_Config;
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
        //if (this.checkForm()) {
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
        //}

    };

    function doneFunction(objResult) {
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                //commonUtils.showAlert(objResult.Messages[0]);

                var listToastr = [];
                objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages[0] };
                listToastr.push(objToastr);
                commonUtils.showToastr(listToastr);
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