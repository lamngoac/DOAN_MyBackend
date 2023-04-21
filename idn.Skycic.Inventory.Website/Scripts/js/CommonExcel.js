function CommonExcel() {
    this.Id_Modal = ''; // 'ImportExcelModal'
    this.Id_FormMain = ''; // 'manageForm'
    this.Id_FormImportExcel = ''; // 'manageFormImportExcel'
    this.Id_FileInput = ''; // '#file' <input type="file" name="file" id="file" />
    this.variablesInit = function (objVariablesInit) {
        if (objVariablesInit !== undefined && objVariablesInit !== null) {
            var id_Modal = '';
            var id_FormMain = '';
            var id_FormImportExcel = '';
            var id_FileInput = '';
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_Modal)) {
                id_Modal = commonUtils.returnValue(objVariablesInit.Id_Modal);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FormMain)) {
                id_FormMain = commonUtils.returnValue(objVariablesInit.Id_FormMain);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FormImportExcel)) {
                id_FormImportExcel = commonUtils.returnValue(objVariablesInit.Id_FormImportExcel);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FileInput)) {
                id_FileInput = commonUtils.returnValue(objVariablesInit.Id_FileInput);
            }
        }
        this.Id_Modal = id_Modal;
        this.Id_FormMain = id_FormMain;
        this.Id_FormImportExcel = id_FormImportExcel;
        this.Id_FileInput = id_FileInput;
    };
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.ajaxSettingsInit = function () {
        var _ajaxSettings = {
            Type: '',
            DataType: '',
            Url: '',
        };
        this.ajaxSettings = _ajaxSettings;
    };
    // Start Show Popup Import Excel
    this.getDataShowPopupImportExcel = function () {
        var data = {};
        var token = '';
        if (commonUtils.checkElementExists('#' + this.Id_FormMain)) {
            token = $('#' + this.Id_FormMain + ' input[name=__RequestVerificationToken]').val();
        } else {
            var token = $('input[name=__RequestVerificationToken]').val();
        }
        if (!commonUtils.isNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }
        return data;
    };
    this.showPopupImportExcel = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataShowPopupImportExcel();
        var objVariablesInit = {
            Id_Modal: this.Id_Modal,
        };
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionShowPopupImportExcel(objResult, objVariablesInit);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionShowPopupImportExcel(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionShowPopupImportExcel();
        });
    };
    function doneFunctionShowPopupImportExcel(objResult, objVariablesInit) {
        if (objResult.Success) {
            $('#' + objVariablesInit.Id_Modal).modal({
                backdrop: false,
                keyboard: true,
            });
            $('#' + objVariablesInit.Id_Modal).html(objResult.Html);
        } else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi', objResult.Detail);
            }
        }
    };
    function failFunctionShowPopupImportExcel(jqXHR, textStatus, errorThrown) {

    };
    function alwaysFunctionShowPopupImportExcel() {

    };
    // End Show Popup Import Excel

    // Start Import Excel
    this.getFormDataImportExcel = function (listSearchInput) {
        var formData = new window.FormData($('#' + this.Id_FormImportExcel)[0]);
        formData.append('tax_file', $('input[type=file]')[0].files[0]);
        if (listSearchInput !== undefined && listSearchInput !== null && listSearchInput.length > 0) {
            var iCount = listSearchInput.length;

            for (var i = 0; i < iCount; i++) {
                if (listSearchInput[i] != null) {
                    if (!commonUtils.isNullOrEmpty(listSearchInput[i].Key)) {
                        var key = commonUtils.returnValue(listSearchInput[i].Key);
                        var value = commonUtils.returnValue(listSearchInput[i].Value);
                        formData.append(key, value);
                    }
                }
            }
        }
        return formData;
    };
    this.importFileExcel = function (listSearchInput) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getFormDataImportExcel(listSearchInput);
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionImportFileExcel(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionImportFileExcel(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionImportFileExcel();
        });
    };

    this.importFileExcelAddHTML = function (listSearchInput, idElement) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getFormDataImportExcel(listSearchInput);
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionImportFileExcelAddHTML(objResult, idElement);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionImportFileExcel(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionImportFileExcel();
        });
    };
    //this.importFileExcel = function () {
    //    if (!commonUtils.isNullOrEmpty(this.Id_FileInput)) {
    //        var idFileInput = '#' + this.Id_FileInput;
    //        var file = $(idFileInput).val();
    //        if (file.length === 0) {
    //            alert("Nhập file cần import");
    //            $(idFileInput).val('');
    //            return false;
    //        } else {
    //            var checkFile = false;
    //            var _index = file.lastIndexOf('.');
    //            if (_index !== undefined && _index !== null && _index > 0) {
    //                var fileType = file.substring(_index + 1, file.length).toLowerCase();
    //                if (fileType === 'xls' || fileType.toLowerCase() === 'xlsx') {
    //                    checkFile = true;
    //                }
    //            }
    //            if (checkFile) {
    //                var _ajaxSettings = this.ajaxSettings;
    //                var dataInput = this.getFormDataImportExcel();
    //                $.ajax({
    //                    type: _ajaxSettings.Type,
    //                    data: dataInput,
    //                    url: _ajaxSettings.Url,
    //                    beforeSend: function () {
    //                    }
    //                }).done(function (objResult) {
    //                    doneFunctionImportFileExcel(objResult);
    //                }).fail(function (jqXHR, textStatus, errorThrown) {
    //                    failFunctionImportFileExcel(jqXHR, textStatus, errorThrown);
    //                }).always(function () {
    //                    alwaysFunctionImportFileExcel();
    //                });
    //            } else {
    //                alert("File excel import không hợp lệ!");
    //            }
    //        }
    //    }
    //};

    function doneFunctionImportFileExcel(objResult) {
        if (objResult.Success) {
            if (objResult.Messages !== undefined && objResult.Messages !== null && objResult.Messages.length > 0) {
                if (!commonUtils.isNullOrEmpty(objResult.Messages[0])) {
                    alert(objResult.Messages[0]);
                }
            }
            commonUtils.window_location_href(window.location.href);
        } else {
            if (objResult.Messages !== undefined && objResult.Messages !== null && objResult.Messages.length > 0) {
                if (!commonUtils.isNullOrEmpty(objResult.Messages[0])) {
                    commonUtils.showAlert(objResult.Messages[0]);
                }
            }
            else {
                if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                    _showErrorMsg123('Lỗi!', objResult.Detail);
                }
            }
        }
    }
    function doneFunctionImportFileExcelAddHTML(objResult, idElement) {
        if (objResult.Success) {
            $(idElement).html(objResult.Html);
            CloseModalImportExcel();
        } else {
            if (objResult.Messages !== undefined && objResult.Messages !== null && objResult.Messages.length > 0) {
                if (!commonUtils.isNullOrEmpty(objResult.Messages[0])) {
                    commonUtils.showAlert(objResult.Messages[0]);
                }
            }
            else {
                if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                    _showErrorMsg123('Lỗi!', objResult.Detail);
                }
            }
        }
    }
    function failFunctionImportFileExcel(jqXHR, textStatus, errorThrown) {

    }
    function alwaysFunctionImportFileExcel() {

    }
    // End Import Excel

    // Start Export Excel
    this.getDataExportExcel = function (listSearchInput) {
        var data = {};
        var token = '';
        if (commonUtils.checkElementExists('#' + this.Id_FormMain)) {
            token = $('#' + this.Id_FormMain + ' input[name=__RequestVerificationToken]').val();
        } else {
            var token = $('input[name=__RequestVerificationToken]').val();
        }
        if (!commonUtils.isNullOrEmpty(token)) {
            data.__RequestVerificationToken = token;
        }

        if (listSearchInput !== undefined && listSearchInput !== null && listSearchInput.length > 0) {
            var iCount = listSearchInput.length;
            var ListSearchInput = [];
            for (var i = 0; i < iCount; i++) {
                if (listSearchInput[i] != null) {
                    if (!commonUtils.isNullOrEmpty(listSearchInput[i].Key)) {
                        var key = commonUtils.returnValue(listSearchInput[i].Key);
                        //2020-08-06 update với trường hợp biến là array
                        if (Array.isArray(listSearchInput[i].Value)) {
                            data[key] = listSearchInput[i].Value;
                        } else {
                            var value = commonUtils.returnValue(listSearchInput[i].Value);
                            data[key] = value;
                        }
                        //Hue comment
                        //var objSearchInput = {
                        //    Key: key,
                        //    Value: value,
                        //};
                        //ListSearchInput.push(objSearchInput);
                        //data[key] = value;
                    }
                }
            }
            //Hue comment
            //if (ListSearchInput !== undefined && ListSearchInput !== null && ListSearchInput.length > 0) {
            //    var modelCur = JSON.stringify(ListSearchInput);
            //    data.model = modelCur;
            //}
        }

        return data;
    };
    this.exportExcel = function (listSearchInput) {
        debugger
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
            doneFunctionExportTemplate(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionExportTemplate(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionExportTemplate();
        });
    }
    function doneFunctionExportTemplate(objResult) {
        if (objResult.Success) {
            var title = '';
            if (!commonUtils.isNullOrEmpty(objResult.Title)) {
                title = commonUtils.returnValue(objResult.Title);
                commonUtils.showToastr(title);
            }
            if (!commonUtils.isNullOrEmpty(objResult.CheckSuccess) && !commonUtils.isNullOrEmpty(objResult.strUrl)) {
                var checkSuccess = commonUtils.returnValue(objResult.CheckSuccess);
                var strUrl = commonUtils.returnValue(objResult.strUrl);
                if (checkSuccess === '1') {
                    //window.location = strUrl;
                    commonUtils.window_location_href(strUrl);
                }
            }
        }
    }
    function failFunctionExportTemplate(jqXHR, textStatus, errorThrown) {

    }
    function alwaysFunctionExportTemplate() {

    }

    this.exportExcelSelected = function (listObjSelected) {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        if (listObjSelected === undefined || listObjSelected === null || listObjSelected.length === 0) {
            return;
        }
        var dataInput = { objlistselected: commonUtils.returnJSONValue(listObjSelected) };
        var token = $('input[name=__RequestVerificationToken]').val();        
        if (!commonUtils.isNullOrEmpty(token)) {
            dataInput.__RequestVerificationToken = token;
        }
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionExportTemplate(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionExportTemplate(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionExportTemplate();
        });
    }
    // End Export Excel
}