function OS_PrdCenter_Mst_Spec() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#SpecCode', 'has-error-fix', 'Mã hàng hóa không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#SpecName', 'has-error-fix', 'Tên hàng hóa không được trống!"')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#ModelCode', 'has-error-fix', 'Chưa chọn Model!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#SpecType1', 'has-error-fix', 'Chưa chọn loại sản phẩm!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#DefaultUnitCode', 'has-error-fix', 'Chưa chọn đơn vị thường dùng!')) {
            return false;
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

        var objMst_Spec = new Object();

        var specCode = commonUtils.returnValueTextOrNull('#SpecCode');
        var specName = commonUtils.returnValueTextOrNull('#SpecName');
        var modelCode = commonUtils.returnValueTextOrNull('#ModelCode');
        var specType1 = commonUtils.returnValueTextOrNull('#SpecType1');
        var specType2 = commonUtils.returnValueTextOrNull('#SpecType2');
        var color = commonUtils.returnValueTextOrNull('#Color');
        var defaultUnitCode = commonUtils.returnValueTextOrNull('#DefaultUnitCode');
        var standardUnitCode = defaultUnitCode;//ReturnValueText('#StandardUnitCode');
        var specDesc = commonUtils.returnValueTextOrNull('#SpecDesc');
        var org = commonUtils.returnValueTextOrNull('#OrgID');

        var tem = new Object();
        tem.SpecCode = specCode;
        tem.SpecName = specName;
        tem.ModelCode = modelCode;
        tem.SpecType1 = specType1;
        tem.SpecType2 = specType2;
        tem.Color = color;
        tem.DefaultUnitCode = defaultUnitCode;
        tem.StandardUnitCode = standardUnitCode;
        tem.SpecDesc = specDesc;
        tem.OrgID = org;

        var flagHasSerial = '0';
        var inputFlagHasSerial = $('#FlagHasSerial');
        if (inputFlagHasSerial !== undefined && inputFlagHasSerial !== null) {
            if (inputFlagHasSerial.prop('checked')) {
                flagHasSerial = '1';
            }
        }
        tem.FlagHasSerial = flagHasSerial;
        var flagHasLOT = '0';
        var inputFlagHasLOT = $('#FlagHasLOT');
        if (inputFlagHasLOT !== undefined && inputFlagHasLOT !== null) {
            if (inputFlagHasLOT.prop('checked')) {
                flagHasLOT = '1';
            }
        }
        tem.FlagHasLOT = flagHasLOT;
        var flagActive = '0';
        var inputFlagActive = $('#FlagActive');
        if (inputFlagActive !== undefined && inputFlagActive !== null) {
            if (inputFlagActive.prop('checked')) {
                flagActive = '1';
            }
        }
        tem.FlagActive = flagActive;

        if (commonUtils.checkElementExists('#CUSTOMFIELD1')) {
            var customField1 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD1');
            tem.CustomField1 = customField1;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD2')) {
            var customField2 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD2');
            tem.CustomField2 = customField2;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD3')) {
            var customField3 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD3');
            tem.CustomField3 = customField3;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD4')) {
            var customField4 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD4');
            tem.CustomField4 = customField4;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD5')) {
            var customField5 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD5');
            tem.CustomField5 = customField5;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD6')) {
            var customField6 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD6');
            tem.CustomField6 = customField6;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD7')) {
            var customField7 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD7');
            tem.CustomField7 = customField7;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD8')) {
            var customField8 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD8');
            tem.CustomField8 = customField8;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD9')) {
            var customField9 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD9');
            tem.CustomField9 = customField9;
        }
        if (commonUtils.checkElementExists('#CUSTOMFIELD10')) {
            var customField10 = commonUtils.returnValueTextOrNull('#CUSTOMFIELD10');
            tem.CustomField10 = customField10;
        }
        if (ListImages !== undefined && ListImages !== null && ListImages.length > 0) {
            objMst_Spec.ListImages = ListImages;
        }
        if (ListFiles !== undefined && ListFiles !== null && ListFiles.length > 0) {
            objMst_Spec.ListFiles = ListFiles;
        }
        objMst_Spec.Mst_Spec = tem;

        var modelCur = commonUtils.returnJSONValue(objMst_Spec);
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
    this.deleteData = function (objOS_PrdCenter_Mst_Spec) {
        var speccode = commonUtils.returnValue(objOS_PrdCenter_Mst_Spec.SpecCode);
        var specname = commonUtils.returnValue(objOS_PrdCenter_Mst_Spec.SpecName);
        if (!commonUtils.isNullOrEmpty(speccode) && !commonUtils.isNullOrEmpty(specname)) {
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
                            speccode: speccode,
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
    this.removeImages = function (thiz) {
        var pathRoot = document.location.protocol + '//' + document.location.host;
        var imgCur = $(thiz).parent().find('img');
        var idx = $(imgCur).attr('idximg');
        //var flagprimaryimage = $(imgCur).attr('flagprimaryimage');
        var divthumb = $('.divthumb[idx="' + idx + '"]');
        if (divthumb != undefined && divthumb !== null && divthumb.length > 0) {
            var divthumbNext = $(divthumb).next();
            var divthumbPrev = $(divthumb).prev();
            $(divthumb).remove();
            // remove ảnh trong List
            if (ListImages !== undefined && ListImages !== null && ListImages.length > 0) {
                ListImages = $.grep(ListImages, function (e) { return e.Id.toString().trim() !== idx.toString().trim() });
            } else {
                ListImages = [];
            }
            var idivthumb = $('#listImagesThumb .divthumb').length;
            if (idivthumb > 0) {
                var src = '';
                var idximg = '';
                var flagprimaryimageCur = '';
                if (divthumbNext !== undefined && divthumbNext !== null && divthumbNext.length > 0) {
                    if (!$(divthumbNext).hasClass('div-thumb-active')) {
                        $(divthumbNext).addClass('div-thumb-active');
                    }
                    src = $(divthumbNext).children('img').attr('src');
                    idximg = $(divthumbNext).children('img').attr('idximg');
                    flagprimaryimageCur = $(divthumbNext).children('img').attr('flagprimaryimage');
                    $('#imgView').attr('src', src);
                    $('#imgView').attr('idximg', idximg);
                    $('#imgView').attr('flagprimaryimage', flagprimaryimageCur);
                } else {
                    if (divthumbPrev !== undefined && divthumbPrev !== null && divthumbPrev.length > 0) {
                        if (!$(divthumbPrev).hasClass('div-thumb-active')) {
                            $(divthumbPrev).addClass('div-thumb-active');
                        }
                        src = $(divthumbPrev).children('img').attr('src');
                        idximg = $(divthumbPrev).children('img').attr('idximg');
                        flagprimaryimageCur = $(divthumbPrev).children('img').attr('flagprimaryimage');
                        $('#imgView').attr('src', src);
                        $('#imgView').attr('idximg', idximg);
                        $('#imgView').attr('flagprimaryimage', flagprimaryimageCur);
                    }
                }

                if (flagprimaryimageCur === '1') {
                    $('#FlagPrimaryImage').prop('checked', true);
                } else {
                    $('#FlagPrimaryImage').prop('checked', false);
                }

            } else {
                $('#imgView').attr('src', pathRoot + '/Images/DK_TVAN.png');
                $('#imgView').attr('idximg', '');
                $('#imgView').attr('flagprimaryimage', '');
            }
        }
    };
    this.selectedImages = function (thiz) {
        var divCur = $(thiz);
        $(".divthumb").each(function () {
            var divthumbCur = $(this);
            idx = $(divthumbCur).attr('idx');
            if ($(divthumbCur).hasClass('div-thumb-active')) {
                $(divthumbCur).removeClass('div-thumb-active');
            }
            if (!$(divthumbCur).hasClass('div-thumb')) {
                $(divthumbCur).addClass('div-thumb');
            }
        });
        if (!$(divCur).hasClass('div-thumb-active')) {
            $(divCur).addClass('div-thumb-active');
        }

        if ($(divCur).hasClass('div-thumb')) {
            $(divCur).removeClass('div-thumb');
        }
        var imgCur = $(divCur).children('img');
        var src = imgCur.attr('src');
        var idximg = imgCur.attr('idximg');
        var flagprimaryimage = imgCur.attr('flagprimaryimage');
        $('#imgView').attr('src', src);
        $('#imgView').attr('idximg', idximg);
        $('#imgView').attr('flagprimaryimage', flagprimaryimage);
        if (flagprimaryimage === '1') {
            $('#FlagPrimaryImage').prop('checked', true);
        } else {
            $('#FlagPrimaryImage').prop('checked', false);
        }
    };
    this.setPrimaryImages = function (thiz) {
        var checkBox = $(thiz);
        if (checkBox !== null && checkBox !== undefined) {
            if (checkBox.is(':checked')) {
                checkBox.prop('checked', true);
                checkBox.val('1');
                var imgCur = $(thiz).parent().parent().parent().find('img');
                var idx = $(imgCur).attr('idximg');
                $(imgCur).attr('flagprimaryimage', '1');

                $("#listImagesThumb div.divthumb").each(function () {
                    debugger;
                    var _imgCur = $(this).find('img');
                    if (_imgCur !== undefined && _imgCur !== null && _imgCur.length > 0) {
                        var idxCur = $(this).attr('idx');
                        if (idxCur.toString().trim() === idx.toString().trim()) {
                            $(_imgCur).attr('flagprimaryimage', '1');
                        } else {
                            $(_imgCur).attr('flagprimaryimage', '0');
                        }
                    }
                });
                if (ListImages !== undefined && ListImages !== null && ListImages.length > 0) {
                    for (var i = 0; i < ListImages.length; i++) {
                        var objImages = ListImages[i];
                        if (objImages.Id.toString().trim() === idx.toString().trim()) {
                            objImages.FlagPrimaryImage = '1';
                        } else {
                            objImages.FlagPrimaryImage = '0';
                        }
                    }
                }
            } else {
                checkBox.prop('checked', false);
                checkBox.val('0');
                var imgCur = $(thiz).parent().parent().parent().find('img');
                var idx = $(imgCur).attr('idximg');
                $(imgCur).attr('flagprimaryimage', '0');
                $("#listImagesThumb div.divthumb").each(function () {
                    var _imgCur = $(this).find('img');
                    if (_imgCur !== undefined && _imgCur !== null && _imgCur.length > 0) {
                        $(_imgCur).attr('flagprimaryimage', '0');
                    }
                });
            }
        }
    };
    this.handleImageFileSelect = function (thiz) {
        if (event.target == undefined) return;
        var listFile = event.target.files;
        totalFiles = listFile.length;
        currentTotalFiles = 0;
        fileTypeError = false;
        fileSizeError = false;
        if (totalFiles > 0) {
            for (var i = 0; i < listFile.length; i++) {
                if (ImgId > 0) {
                    ImgId = (ImgId + 1);
                } else {
                    ImgId = (ImgId + i);
                }
                var file = listFile[i];
                var name = file.name;
                if (!name.match(/(?:doc|docx|xls|xlsx|ppt|ppts|pps|ppsx|pptx|mdb|pdf|psd|gif|jpg|jpeg|png|bmp|rar|zip|html|htm|xml)$/)) {
                    alert("File upload phải thuộc các định dạng sau: \" doc | docx | xls | xlsx | ppt | ppts | pps | ppsx | pptx | mdb | pdf | psd | gif | jpg | jpeg | png | bmp | rar | zip | html | htm | xml \"!");
                    return false;
                } else {
                    renderImageFileInfo(file, ImgId);
                }

            }
        }
        $('#fileImage').val('');
        //document.getElementById("fileImage").value = null;
    };
    this.renderImageFileInfo = function (file, id) {
        var strHtml = '';
        var name = file.name;
        var size = file.size;
        var type = file.type;
        var fileReader = new FileReader();

        fileReader.onload = function (event) {
            var base64 = event.target.result;
            var objImages = new Object();
            objImages.Id = id;
            objImages.Name = name;
            objImages.Size = size;
            objImages.Type = type;
            objImages.Src = base64;
            objImages.Status = 'N';
            var strReplace = "data:" + type + ";base64,";
            objImages.Base64 = base64.replace(strReplace, "");
            var flagprimaryimage = "0";
            if (ListImages !== undefined && ListImages !== null && ListImages.length > 0) {
                flagprimaryimage = "0";
                strHtml += '<div idx="' + id + '" class="text-center divthumb div-thumb" onclick="SelectedImages(this)">';
                strHtml += '<img idximg="' + id + '" class="img-thumb" src="' + objImages.Src + '" flagprimaryimage="' + flagprimaryimage + '">';
                strHtml += '</div>';
                objImages.FlagPrimaryImage = flagprimaryimage;
            } else {
                flagprimaryimage = "1";
                strHtml += '<div idx="' + id + '" class="text-center divthumb div-thumb-active" onclick="SelectedImages(this)">';
                strHtml += '<img idximg="' + id + '" class="img-thumb" src="' + objImages.Src + '" flagprimaryimage="' + flagprimaryimage + '">';
                strHtml += '</div>';
                objImages.FlagPrimaryImage = flagprimaryimage;
                $('#imgView').attr('src', objImages.Src);
                $('#imgView').attr('idximg', id);
                $('#imgView').attr('flagprimaryimage', flagprimaryimage);
                if (flagprimaryimage === '1') {
                    $('#FlagPrimaryImage').prop('checked', true);
                } else {
                    $('#FlagPrimaryImage').prop('checked', false);
                }

            }
            ListImages.push(objImages);
            $('#listImagesThumb').append(strHtml);
        }
        fileReader.readAsDataURL(file);
    };
    this.checkError = function () {
        currentTotalFiles++;

        if (currentTotalFiles === totalFiles) {
            if (fileSizeError) {
                console.log('Error on file size!');
            } else if (fileTypeError) {
                console.log('Error on file type');
            }
        }
    };
    this.handleFileSelect = function (event) {
        debugger;
        var listFiles = event.target.files;
        totalFiles = listFiles.length;
        currentTotalFiles = 0;
        fileTypeError = false;
        fileSizeError = false;
        if (totalFiles > 0) {
            for (var i = 0; i < listFiles.length; i++) {
                if (FileId > 0) {
                    FileId = (FileId + 1);
                } else {
                    FileId = (FileId + i);
                }
                var file = listFiles[i];
                var name = file.name;
                if (!name.match(/(?:doc|docx|xls|xlsx|ppt|ppts|pps|ppsx|pptx|mdb|pdf|psd|gif|jpg|jpeg|png|bmp|rar|zip|html|htm|xml)$/)) {
                    alert("File upload phải thuộc các định dạng sau: \" doc | docx | xls | xlsx | ppt | ppts | pps | ppsx | pptx | mdb | pdf | psd | gif | jpg | jpeg | png | bmp | rar | zip | html | htm | xml \"!");
                    return false;
                } else {
                    renderFileInfo(file, FileId);
                }

            }
        }
        $('#fileInput').val('');
        //document.getElementById("fileInput").value = null;
    };
    this.renderFileInfo = function (file, id) {
        var name = file.name;
        var size = file.size;
        var type = file.type;
        var fileReader = new FileReader();

        fileReader.onload = function (event) {
            var base64 = event.target.result;
            var objFile = new Object();
            objFile.Id = id;
            objFile.Name = name;
            objFile.Size = size;
            objFile.Type = type;
            objFile.Src = base64;
            objFile.Status = 'N';
            var strReplace = "data:" + type + ";base64,";
            objFile.Base64 = base64.replace(strReplace, "");
            var strHtml = getHtmlFromTemplate($('#rowtemplateFileUpload'), {
                specfilename: name
                , specfilepath: objFile.Src
                , specfiletype: type
                , specfiledesc: ''
                , idx: 999999
            });

            var oldItem = $('#tbody-FileUpload').find('tr[specfilename="' + name + '"]');
            if (oldItem != undefined && oldItem.length > 0) {
                var strMess = "File: " + name + " đã tồn tại!";
                alert(strMess);
            }
            else {
                $('#tbody-FileUpload').append(strHtml);
                updateTableTrNotShowIdx($('#tbody-FileUpload tr'), true);
            }
            ListFiles.push(objFile);
        }

        fileReader.readAsDataURL(file);
    };
    this.deleteFile = function (thiz) {
        debugger;
        if (thiz !== undefined && thiz !== null) {
            if (thiz !== undefined && thiz !== null) {
                var trCur = $(thiz).parent().parent().parent();
                var idx = $(trCur).attr('idx');
                var specfilename = trCur.attr('specfilename');
                var pathImg = document.location.protocol + "//" + document.location.host + '/Images/exclamation-circle-solid.svg';
                bootbox.confirm({
                    title: "<img id=\"exclamation-circle\" style=\"width: 35px; float: left; margin-right: 10px\" src=\"" + pathImg + "\"> THÔNG BÁO",
                    message: "Đồng ý xóa tệp tin đính kèm: '" + specfilename + "' ?",
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
                            trCur.remove();
                            updateTableTrNotShowIdx($('#tbody-FileUpload tr'), true);
                        }
                    }
                });
            }
            else {
                alert("Chưa chọn bản ghi cần xóa!");
                return;
            }
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
}