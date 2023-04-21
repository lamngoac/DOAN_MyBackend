function InvFTempPrint() {
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
    this.checkForm = function () {
        debugger;
        var printtempdody = $('#divView').find('object').attr('data-body');
        if (printtempdody === null || printtempdody === "") {
            alert("Bạn chưa chọn mẫu in để lưu");
            return false;
        }

        //var logoFilePath = document.getElementById('LogoFilePath').value;
        //var backgroundFilePath = document.getElementById('BackgroundFilePath').value;
        //if ((logoFilePath === undefined || logoFilePath === "") && (backgroundFilePath === undefined || backgroundFilePath === "")) {
        //    alert("Bạn chưa chọn logo để lưu");
        //    return false;
        //}

        return true;
    };
    
    this.getData = function () {
        debugger;        
        var imageLogo = $('#ImagePath').attr('image-src');
        if (imageLogo === "/Images/no-image.jpg") {
            imageLogo = "";
        }
        
        var imageTypeLogo = $('#ImagePath').attr('image-type');
        var imageNameLogo = $('#ImagePath').attr('image-name');
        var strReplace = "data:" + imageTypeLogo + ";base64,";
        imageLogo = imageLogo.replace(strReplace, "");

        var imageBackground = $('#ImagePathBackground').attr('image-src');
        if (imageBackground === "/Images/no-image.jpg") {
            imageBackground = "";
        }
        var imageTypeBgr = $('#ImagePathBackground').attr('image-type');
        var imageNameBgr = $('#ImagePathBackground').attr('image-name');
        strReplace = "data:" + imageTypeBgr + ";base64,";
        imageBackground = imageBackground.replace(strReplace, "");

        var token = $('#manageFormtem input[name=__RequestVerificationToken]').val();
                
        //var nntName = commonUtils.returnValueText('#NNTName');
        var tempPrintType = commonUtils.returnValueText('#TempPrintType');
        var iF_TempPrintNo = commonUtils.returnValueText('#IF_TempPrintNo');
        var printtempdody = $('#divView').find('object').attr('data-body');        

        var objInvF_TempPrint = new Object();
        objInvF_TempPrint.IF_TempPrintNo = iF_TempPrintNo;
        objInvF_TempPrint.TempPrintType = tempPrintType;
        objInvF_TempPrint.TempPrintBody = printtempdody;        
        objInvF_TempPrint.LogoFilePathBase64 = imageLogo;
        objInvF_TempPrint.FlagUpdloadLogoFilePathBase64 = imageLogo.length > 0 ? '1' : '0';
        objInvF_TempPrint.LogoFileName = imageNameLogo !== undefined ? imageNameLogo.replace(' ', '') : null;
        objInvF_TempPrint.BackgroundFilePathBase64 = imageBackground;
        objInvF_TempPrint.FlagUpdloadBackgroundFilePathBase64 = imageBackground.length > 0 ? '1' : '0';
        objInvF_TempPrint.BackgroundFileName = imageNameBgr !== undefined ? imageNameBgr.replace(' ', '') : null;

        var modelCur = commonUtils.returnJSONValue(objInvF_TempPrint);
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

            let eTempPrintType = document.getElementById('TempPrintType');
            GetTempPrint(eTempPrintType);
            $('#ImagePath').attr('image-src', '');
            $('#ImagePath').attr('src', '/Images/no-image.jpg');
            $('#ImagePathBackground').attr('image-src', '');
            $('#ImagePathBackground').attr('src', '/Images/no-image.jpg');
            $('#LogoFilePath').val('');
            $('#BackgroundFilePath').val('');
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    
    function doneFunctionIn(objResult) {
        if (objResult.Success) {
            var title = '';
            if (!commonUtils.isNullOrEmpty(objResult.Title)) {
                title = commonUtils.returnValue(objResult.Title);
                commonUtils.showAlert(title);
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
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    function doneFunctionPreview(objResult) {
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                commonUtils.window_location_href(objResult.RedirectUrl);
            }
            if (objResult.Model.PrintTempBody != null && objResult.Model.PrintTempBody != undefined) {
                debugger;
                $('#Viewtem').find('object').attr('data', objResult.Model.PrintTempBody);
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