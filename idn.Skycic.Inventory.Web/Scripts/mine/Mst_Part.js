function Mst_Part() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#PartCode', 'has-error-fix', 'Mã sản phẩm không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#PartName', 'has-error-fix', 'Tên sản phẩm không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#PartType', 'has-error-fix', 'Chưa chọn loại sản phẩm!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#PMType', 'has-error-fix', 'Chưa chọn nhóm sản phẩm!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#PartUnitCodeStd', 'has-error-fix', 'Chưa chọn đơn vị chuẩn!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#PartUnitCodeDefault', 'has-error-fix', 'Chưa chọn đơn vị thường dùng!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#QtyMinSt', 'has-error-fix', 'Tồn kho tối thiểu không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNullOrEmpty('#QtyMinSt', 'has-error-fix', 'Tồn kho tối thiểu không là số!')) {
                return false;
            }
            if (!commonUtils.checkIsNumber_IsGreaterOrSame_Zero('#QtyMinSt', 'has-error-fix', 'Tồn kho tối thiểu >= 0!')) {
                return false;
            }
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#QtyEffSt', 'has-error-fix', 'Tồn kho tối ưu không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNullOrEmpty('#QtyEffSt', 'has-error-fix', 'Tồn kho tối ưu không là số!')) {
                return false;
            }
            if (!commonUtils.checkIsNumber_IsGreaterOrSame_Zero('#QtyEffSt', 'has-error-fix', 'Tồn kho tối ưu >= 0!')) {
                return false;
            }
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#QtyMaxSt', 'has-error-fix', 'Tồn kho tối đa không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNullOrEmpty('#QtyMaxSt', 'has-error-fix', 'Tồn kho tối đa không là số!')) {
                return false;
            }
            if (!commonUtils.checkIsNumber_IsGreaterOrSame_Zero('#QtyMaxSt', 'has-error-fix', 'Tồn kho tối đa >= 0!')) {
                return false;
            }
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#UPIn', 'has-error-fix', 'Giá nhập không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNullOrEmpty('#UPIn', 'has-error-fix', 'Giá nhập không là số!')) {
                return false;
            }
            if (!commonUtils.checkIsNumber_IsGreaterOrSame_Zero('#UPIn', 'has-error-fix', 'Giá nhập >= 0!')) {
                return false;
            }
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#UPOut', 'has-error-fix', 'Giá bán không được trống!')) {
            return false;
        } else {
            if (!commonUtils.checkElementIsNullOrEmpty('#UPOut', 'has-error-fix', 'Giá bán không là số!')) {
                return false;
            }
            if (!commonUtils.checkIsNumber_IsGreaterOrSame_Zero('#UPOut', 'has-error-fix', 'Giá bán >= 0!')) {
                return false;
            }
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
        var partcode = commonUtils.returnValueText('#PartCode');
        var partbarcode = commonUtils.returnValueText('#PartCode');
        var partnamefs = commonUtils.returnValueText('#PartNameFS');
        var partname = commonUtils.returnValueText('#PartName');
        var parttype = commonUtils.returnValueText('#PartType');
        var pmtype = commonUtils.returnValueText('#PMType');
        var partunitcodestd = commonUtils.returnValueText('#PartUnitCodeStd');
        var partunitcodedefault = commonUtils.returnValueText('#PartUnitCodeDefault');
        var qtyminst = commonUtils.returnValueText('#QtyMinSt');
        var qtyeffst = commonUtils.returnValueText('#QtyEffSt');
        var qtymaxst = commonUtils.returnValueText('#QtyMaxSt');
        var upin = commonUtils.returnValueText('#UPIn');
        var upout = commonUtils.returnValueText('#UPOut');
        var qtyeffmonth = commonUtils.returnValueText('#QtyEffMonth');
        var remarkforeffused = commonUtils.returnValueText('#RemarkForEffUsed');
        var partorgin = commonUtils.returnValueText('#PartOrigin');
        var partdesc = commonUtils.returnValueText('#PartDesc');
        var partcomponents = commonUtils.returnValueText('#PartComponents');
        var instructionforuse = commonUtils.returnValueText('#InstructionForUse');
        var partstorage = commonUtils.returnValueText('#PartStorage');
        var urlmnfsequence = commonUtils.returnValueText('#UrlMnfSequence');
        var partintroduction = commonUtils.returnValueText('#PartIntroduction');
        var mnfstandard = commonUtils.returnValueText('#MnfStandard');
        var partstyle = commonUtils.returnValueText('#PartStyle');
        var filepath = commonUtils.returnValueText('#FilePath');
        var imagepath = commonUtils.returnValueText('#ImagePath');
        
        if (commonUtils.checkElementExists('#FlagBOM')) {
            var flagBom = '0';
            var inputFlagBOM = $('#FlagBOM');
            if (inputFlagBOM !== undefined && inputFlagBOM !== null) {
                if (inputFlagBOM.prop('checked')) {
                    flagBom = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagVirtual')) {
            var flagVirtual = '0';
            var inputFlagVirtual = $('#FlagVirtual');
            if (inputFlagVirtual !== undefined && inputFlagVirtual !== null) {
                if (inputFlagVirtual.prop('checked')) {
                    flagVirtual = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagInputLot')) {
            var flagInputLot = '0';
            var inputFlagInputLot = $('#FlagInputLot');
            if (inputFlagInputLot !== undefined && inputFlagInputLot !== null) {
                if (inputFlagInputLot.prop('checked')) {
                    flagInputLot = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagInputSerial')) {
            var flagInputSerial = '0';
            var inputflagInputSerial = $('#FlagInputSerial');
            if (inputflagInputSerial !== undefined && inputflagInputSerial !== null) {
                if (inputflagInputSerial.prop('checked')) {
                    flagInputSerial = '1';
                }
            }
        }
        var objMst_Part = new Object();
        objMst_Part.PartCode = partcode;
        objMst_Part.PartBarCode = partbarcode;
        objMst_Part.PartNameFS = partnamefs;
        objMst_Part.PartName = partname;
        objMst_Part.PartType = parttype;
        objMst_Part.PMType = pmtype;
        objMst_Part.PartUnitCodeStd = partunitcodestd;
        objMst_Part.PartUnitCodeDefault = partunitcodedefault;
        objMst_Part.QtyMinSt = qtyminst;
        objMst_Part.QtyEffSt = qtyeffst;
        objMst_Part.QtyMaxSt = qtymaxst;
        objMst_Part.UPIn = upin;
        objMst_Part.UPOut = upout;
        objMst_Part.QtyEffMonth = 0;
        objMst_Part.RemarkForEffUsed = remarkforeffused;
        objMst_Part.PartOrigin = partorgin;
        objMst_Part.PartDesc = partdesc;
        objMst_Part.PartComponents = partcomponents;
        objMst_Part.InstructionForUse = instructionforuse;
        objMst_Part.PartStorage = partstorage;
        objMst_Part.UrlMnfSequence = urlmnfsequence;
        objMst_Part.PartIntroduction = partintroduction;
        objMst_Part.MnfStandard = mnfstandard;
        objMst_Part.PartStyle = partstyle;
        objMst_Part.FlagBOM = flagBom;
        objMst_Part.FlagVirtual = flagVirtual;
        objMst_Part.FlagInputLot = flagInputLot;
        objMst_Part.FlagInputSerial = flagInputSerial;
        objMst_Part.FilePath = filepath;
        objMst_Part.ImagePath = imagepath;

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
                    objMst_Part.FlagActive = flagActive;
                }
            }
        }
        
        var modelCur = commonUtils.returnJSONValue(objMst_Part);
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
    this.deleteData = function (objMst_Part) {
        var partcode = commonUtils.returnValue(objMst_Part.PartCode);
        var partname = commonUtils.returnValue(objMst_Part.PartName);
        if (!commonUtils.isNullOrEmpty(partcode) && !commonUtils.isNullOrEmpty(partname)) {
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
                            partcode: partcode,
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
    //images
    this.handleImageFileSelect = function (thiz, idInput, idImage) {
        if (event.target == undefined) return;
        var listFile = event.target.files;
        totalFiles = listFile.length;
        currentTotalFiles = 0;
        fileTypeError = false;
        fileSizeError = false;
        if (totalFiles > 0) {
            var file = listFile[0];
            var name = file.name;
            if (!name.match(/(?:doc|docx|xls|xlsx|ppt|ppts|pps|ppsx|pptx|mdb|pdf|psd|gif|jpg|jpeg|png|bmp|rar|zip|html|htm|xml)$/)) {
                alert("File upload phải thuộc các định dạng sau: \" doc | docx | xls | xlsx | ppt | ppts | pps | ppsx | pptx | mdb | pdf | psd | gif | jpg | jpeg | png | bmp | rar | zip | html | htm | xml \"!");
                return false;
            } else {
                renderImageFileInfo(file, idImage);
            }
        }
        //$('#fileImage').val('');
        $(idInput).val('');
        //document.getElementById("fileImage").value = null;
    };
    this.renderImageFileInfo = function (file, idImage) {
        var strHtml = '';
        var name = file.name;
        var size = file.size;
        var type = file.type;
        var fileReader = new FileReader();
        
        fileReader.onload = function (event) {
            var base64 = event.target.result;
            var objImages = new Object();
            objImages.Name = name;
            objImages.Size = size;
            objImages.Type = type;
            objImages.Src = base64;
            objImages.Status = 'N';
            var strReplace = "data:" + type + ";base64,";
            objImages.Base64 = base64.replace(strReplace, "");
            //$('#imgView').attr('src', objImages.Src);
            $(idImage).attr('src', objImages.Src);
        }
        fileReader.readAsDataURL(file);
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
}