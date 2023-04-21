function Mst_TempPrintGroup() {
    this.Id_Modal = ''; // 'ImportExcelModal' 'ImportFileModal'
    this.Id_FormMain = ''; // 'manageForm'
    this.Id_FormImportFile = ''; // 'manageFormImportExcel' 'manageFormImportFile'
    this.Id_FileInput = ''; // '#file' <input type="file" name="file" id="file" />
    this.variablesInit = function (objVariablesInit) {
        if (objVariablesInit !== undefined && objVariablesInit !== null) {
            var id_Modal = '';
            var id_FormMain = '';
            var id_FormImportFile = '';
            var id_FileInput = '';
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_Modal)) {
                id_Modal = commonUtils.returnValue(objVariablesInit.Id_Modal);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FormMain)) {
                id_FormMain = commonUtils.returnValue(objVariablesInit.Id_FormMain);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FormImportFile)) {
                id_FormImportFile = commonUtils.returnValue(objVariablesInit.Id_FormImportFile);
            }
            if (!commonUtils.isNullOrEmpty(objVariablesInit.Id_FileInput)) {
                id_FileInput = commonUtils.returnValue(objVariablesInit.Id_FileInput);
            }
        }
        this.Id_Modal = id_Modal;
        this.Id_FormMain = id_FormMain;
        this.Id_FormImportFile = id_FormImportFile;
        this.Id_FileInput = id_FileInput;
    };
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
    this.checkFileImport = function (thiz, e) {
        var checkFile = false;
        var fileName = e.target.files[0].name;
        if (!commonUtils.isNullOrEmpty(fileName)) {
            var _index = fileName.lastIndexOf('.');
            if (_index !== undefined && _index !== null && _index > 0) {
                var fileType = commonUtils.toLowerCase(fileName.substring(_index + 1, fileName.length));
                if (fileType === 'rtmpl') {
                    checkFile = true;
                }
            }
        }
        if (!checkFile) {
            commonUtils.showAlert('File Import không hợp lệ!');
            $(thiz).val('');
            return false;
        }
        return true;
    };
    this.closeModalImportFile = function (importFileModal) {
        $('#' + importFileModal).modal("hide");
        $('#' + importFileModal).html('');
    };
    // Start Show Popup Import File
    this.getDataShowPopupImportFile = function () {
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
        return data;
    };
    this.showPopupImportFile = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataShowPopupImportFile();
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
            doneFunctionShowPopupImportFile(objResult, objVariablesInit);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionShowPopupImportFile(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionShowPopupImportFile();
        });
    };

    function doneFunctionShowPopupImportFile(objResult, objVariablesInit) {
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
    function failFunctionShowPopupImportFile(jqXHR, textStatus, errorThrown) {

    };
    function alwaysFunctionShowPopupImportFile() {

    };
    // End Show Popup Import File

    // Start Import File
    this.getFormDataImportFile = function () {
        debugger;
        var formData = new window.FormData($('#' + this.Id_FormImportFile)[0]);
        formData.append('tax_file', $('input[type=file]')[0].files[0]);

        var tempPrintType = commonUtils.returnValueText('#pTempPrintType');
        formData.append('tempPrintType', tempPrintType);

        return formData;
    };
    this.importFile = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getFormDataImportFile();
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
            doneFunctionImportFile(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunctionImportFile(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunctionImportFile();
        });
    };

    function doneFunctionImportFile(objResult) {
        if (objResult.Success) {
            if (objResult.Messages !== undefined && objResult.Messages !== null && objResult.Messages.length > 0) {
                if (!commonUtils.isNullOrEmpty(objResult.Messages[0])) {
                    var messages = commonUtils.returnValue(objResult.Messages[0]);
                    commonUtils.showAlert(messages);
                }
            }
            if (objResult.ObjectData !== undefined && objResult.ObjectData) {
                var objectData = objResult.ObjectData;
                var filePath = commonUtils.returnValue(objectData.FilePathImport);
                var urlFilePdf = commonUtils.returnValue(objectData.UrlFilePdf);
                if (!commonUtils.isNullOrEmpty(filePath)) {
                    commonUtils.setValueElement($('#FilePath'), filePath);
                }
                if (!commonUtils.isNullOrEmpty(urlFilePdf)) {
                    commonUtils.setValueAttributeElement($('#objPDF'), 'data', urlFilePdf);
                }
            }
            commonUtils.closeModalPopup('ImportFileModal');
            //commonUtils.window_location_href(window.location.href);
        } else {
            if (objResult.Messages !== undefined && objResult.Messages !== null && objResult.Messages.length > 0) {
                if (!commonUtils.isNullOrEmpty(objResult.Messages[0])) {
                    var messages = commonUtils.returnValue(objResult.Messages[0]);
                    commonUtils.showAlert(messages);
                }
            }
            else {
                if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                    _showErrorMsg123('Lỗi!', objResult.Detail);
                }
            }
        }
    };
    function failFunctionImportFile(jqXHR, textStatus, errorThrown) {

    };
    function alwaysFunctionImportFile() {
        setTimeout(function () {
            var checkElementExists = commonUtils.checkElementExists('html[dir="ltr"] #toolbarContainer')
            //commonUtils.showAlert('checkElementExists: ' + checkElementExists);
            if (checkElementExists) {

                $('html[dir="ltr"] #toolbarContainer').html('');
                $('html[dir="ltr"] #toolbarContainer').remove();
            }
        });
        
        
    };
    // End Import File
    this.checkForm = function() {
        if (!commonUtils.checkElementIsNullOrEmpty('#pTempPrintType', 'has-error-fix', 'Loại mẫu in không được trống!')) {
            return false;
        }
        return true;
    };
    this.checkRowsSelected = function () {
        debugger
        var checkRows = $('#table-tbodyID').find(".sl_ace");
        var checkRowsSelected = $('#table-tbodyID').find(".sl_ace:checked").length;
        if (checkRowsSelected === 0) {
            commonUtils.showAlert('Chưa có dữ liệu nào được chọn!');
            return false;
        }
        return true;
    };
    this.getData = function () {
        debugger;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        //var printTempCode = commonUtils.returnValueText('#PrintTempCode');        
        var tempPrintType = commonUtils.returnValueText('#pTempPrintType');
        //var filePath = commonUtils.returnValueText('#FilePath');
        
        var objMst_TempPrintGroup = new Object();        
        objMst_TempPrintGroup.TempPrintType = tempPrintType;

        var modelCur = commonUtils.returnJSONValue(objMst_TempPrintGroup);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };
    this.getDataRowsSelected = function () {
        var data = {};
        var token = $('input[name=__RequestVerificationToken]').val();
        
        var ListPrintTempCode = [];
        
        debugger;
        $('#table-tbodyID').find('.sl_ace:checked').each(function () {
            var tr = $(this).parent().parent().parent();
            if (!commonUtils.checkElementUndefinedOrNull(tr)) {
                var printtempcode = commonUtils.getValueAttributeElement(tr, 'printtempcode');
                var temtype = commonUtils.getValueAttributeElement(tr, 'itemtype');
                var idx = commonUtils.getValueAttributeElement(tr, 'idx');
                var objListPrintTempCode = new Object();
                if (!commonUtils.isNullOrEmpty(printtempcode)) {
                    objListPrintTempCode.PrintTempCode = printtempcode;
                    objListPrintTempCode.TempType = temtype;
                }
                ListPrintTempCode.push(objListPrintTempCode);
            }
        });
        var modelCur = commonUtils.returnJSONValue(ListPrintTempCode);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    }
    this.saveData = function () {
        debugger;
        var _ajaxSettings = this.ajaxSettings;       
        //var dataInput = this.getFormDataImportFile();
        var formData = new window.FormData($('#' + this.Id_FormImportFile)[0]);
        formData.append('tax_file', $('input[type=file]')[0].files[0]);

        var tempPrintType = commonUtils.returnValueText('#pTempPrintType');
        formData.append('tempPrintType', tempPrintType);

        if (this.checkForm()) {
            $.ajax({
                type: _ajaxSettings.Type,
                data: formData,
                url: _ajaxSettings.Url,
                //dataType: _ajaxSettings.DataType,
                processData: false,
                cache: false,
                contentType: false,
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
    this.deleteData = function (objMst_TempPrintGroup) {
        var printtempcode = commonUtils.returnValue(objMst_TempPrintGroup.PrintTempCode);
        if (!commonUtils.isNullOrEmpty(printtempcode)) {
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
                        var dataInput = {};
                        var ListPrintTempCode = [];
                        ListPrintTempCode.push(printtempcode);
                        var modelCur = '';
                        if (ListPrintTempCode !== undefined && ListPrintTempCode !== null && ListPrintTempCode.length > 0) {
                            modelCur = commonUtils.returnJSONValue(ListPrintTempCode);
                        }
                        dataInput.model = modelCur;
                        var token = $('input[name=__RequestVerificationToken]').val();
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
            commonUtils.showAlert('Mã mẫu tem trống!');
            return;
        }
    };
    this.deleteMultipleData = function () {
        if (this.checkRowsSelected()) {
            var _ajaxSettings = this.ajaxSettings;
            var dataInput = this.getDataRowsSelected();
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
        }
    };
    this.approvedData = function (objMst_TempPrintGroup) {
        var printtempcode = commonUtils.returnValue(objMst_TempPrintGroup.PrintTempCode);
        if (!commonUtils.isNullOrEmpty(printtempcode)) {
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
                        var dataInput = {};
                        var ListPrintTempCode = [];
                        ListPrintTempCode.push(printtempcode);
                        var modelCur = '';
                        if (ListPrintTempCode !== undefined && ListPrintTempCode !== null && ListPrintTempCode.length > 0) {
                            modelCur = commonUtils.returnJSONValue(ListPrintTempCode);
                        }
                        dataInput.model = modelCur;
                        var token = $('input[name=__RequestVerificationToken]').val();
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
            commonUtils.showAlert('Mã mẫu tem trống!');
            return;
        }
    };
    this.approvedMultipleData = function () {
        if (this.checkRowsSelected()) {
            var _ajaxSettings = this.ajaxSettings;
            var dataInput = this.getDataRowsSelected();
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
        }
    };
    this.cancelData = function (objMst_TempPrintGroup) {
        var printtempcode = commonUtils.returnValue(objMst_TempPrintGroup.PrintTempCode);
        if (!commonUtils.isNullOrEmpty(printtempcode)) {
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
                        var dataInput = {};
                        var ListPrintTempCode = [];
                        ListPrintTempCode.push(printtempcode);
                        var modelCur = '';
                        if (ListPrintTempCode !== undefined && ListPrintTempCode !== null && ListPrintTempCode.length > 0) {
                            modelCur = commonUtils.returnJSONValue(ListPrintTempCode);
                        }
                        dataInput.model = modelCur;
                        var token = $('input[name=__RequestVerificationToken]').val();
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
            commonUtils.showAlert('Mã mẫu tem trống!');
            return;
        }
    };
    this.cancelMultipleData = function () {
        if (this.checkRowsSelected()) {
            var _ajaxSettings = this.ajaxSettings;
            var dataInput = this.getDataRowsSelected();
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
};