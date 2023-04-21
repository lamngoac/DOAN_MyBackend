var ListDataInformation = [];
function Mst_Customer() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.loadMst_District = function (thiz) {
        debugger;
        var provincecode = $(thiz).val();
        if (!commonUtils.isNullOrEmpty(provincecode)) {
            var _ajaxSettings = this.ajaxSettings;
            var token = $('#manageForm input[name=__RequestVerificationToken]').val();
            $.ajax({
                type: _ajaxSettings.Type,
                data: {
                    __RequestVerificationToken: token
                    , provincecode: provincecode
                },
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                doneFunctionLoadDistrict(objResult);
            }).fail(function (jqXHR, textStatus, errorThrown) {
            }).always(function () {
            });
        } else {
            $('#DistrictCode').html('');
        }
    };
    function doneFunctionLoadDistrict(objResult) {
        debugger
        if (objResult.Success) {
            $("#DistrictCode").html(objResult.Html);
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    this.loadMst_Ward = function (thiz) {
        var districtcode = $(thiz).val();
        var provincecode = '';
        if (!commonUtils.isNullOrEmpty(districtcode)) {
            var inputDistrictCode = $('#DistrictCode');
            if (inputDistrictCode !== undefined && inputDistrictCode !== null) {
                var op = $(inputDistrictCode).find(":selected");

                if (op != undefined) {
                    provincecode = op.attr('provincecode');
                }
            }
            if (!commonUtils.isNullOrEmpty(provincecode) && !commonUtils.isNullOrEmpty(districtcode)) {
                var _ajaxSettings = this.ajaxSettings;
                var token = $('#manageForm input[name=__RequestVerificationToken]').val();
                $.ajax({
                    type: _ajaxSettings.Type,
                    data: {
                        __RequestVerificationToken: token
                        , provincecode: provincecode
                        , districtcode: districtcode
                    },
                    url: _ajaxSettings.Url,
                    dataType: _ajaxSettings.DataType,
                    beforeSend: function () {
                    }
                }).done(function (objResult) {
                    doneFunctionLoadWard(objResult);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                }).always(function () {
                });
            }
        } else {
            $('#WardCode').html('');
        }
    };
    function doneFunctionLoadWard(objResult) {
        debugger
        if (objResult.Success) {
            $("#WardCode").html(objResult.Html);
        }
        else {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                commonUtils.showAlert(objResult.Messages[0]);
            }
            if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                _showErrorMsg123('Lỗi!', objResult.Detail);
            }
        }
    };
    this.toggle = function (thiz) {
        var shodetail = $(thiz).find('.showdetail');
        var idx = shodetail.attr('value');
        if (idx == 1) {
            shodetail.attr('src', '/Images/More.svg');
            shodetail.attr('value', '0');
        }
        else {
            shodetail.attr('src', '/Images/Hide.svg');
            shodetail.attr('value', '1');
        }

        $(thiz).parent().find('.detail-toggle').toggle();
    };
    this.checkForm = function() {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerCode', 'has-error-fix', 'Mã khách hàng không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerName', 'has-error-fix', 'Tên khách hàng không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#CustomerGrpCode', 'has-error-fix', 'Nhóm khách hàng không được trống!');
        var contactEmail = commonUtils.returnValueText('#ContactEmail');
        if (!commonUtils.isNullOrEmpty(contactEmail)) {
            if (!commonUtils.validateEmail(contactEmail)) {
                var objToastr = {
                    ToastrType: 'error',
                    ToastrMessage: 'Email liên hệ không hợp lệ!'
                };
                listError.push(objToastr);
            }
        }
        var invoiceEmailSend = commonUtils.returnValueText('#InvoiceEmailSend');
        if (!commonUtils.isNullOrEmpty(invoiceEmailSend)) {
            if (!commonUtils.validateEmail(invoiceEmailSend)) {
                var objToastr = {
                    ToastrType: 'error',
                    ToastrMessage: 'Email nhận HĐ không hợp lệ!'
                };
                listError.push(objToastr);
            }
        }
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.blur_Element = function (thiz) {
        // Set giá trị Init
        // Người mua hàng
        var invoiceCustomerName = commonUtils.returnValueText('#InvoiceCustomerName');
        if (commonUtils.isNullOrEmpty(invoiceCustomerName)) {
            $('#InvoiceCustomerName').val(commonUtils.returnValueText('#ContactName'));
        }
        // Tên tổ chức
        var invoiceOrgName = commonUtils.returnValueText('#InvoiceOrgName');
        if (commonUtils.isNullOrEmpty(invoiceOrgName)) {
            $('#InvoiceOrgName').val(commonUtils.returnValueText('#CustomerName'));
        }
        // Địa chỉ 
        var invoiceCustomerAddress = commonUtils.returnValueText('#InvoiceCustomerAddress');
        if (commonUtils.isNullOrEmpty(invoiceCustomerAddress)) {
            $('#InvoiceCustomerAddress').val(commonUtils.returnValueText('#CustomerAddress'));
        }
        // Email nhận hóa đơn
        var invoiceEmailSend = commonUtils.returnValueText('#InvoiceEmailSend');
        if (commonUtils.isNullOrEmpty(invoiceEmailSend)) {
            $('#InvoiceEmailSend').val(commonUtils.returnValueText('#ContactEmail'));
        }
    }
    this.showPopup = function (_input) {
        if (_input !== undefined && _input !== null) {
            var id = $(_input).attr('id');
            if (!commonUtils.isNullOrEmpty(id)) {

                if (id === 'ShowPopupAddCustomerGroup') {
                    $('#CustomerGrpName').val('');
                    $("#CustomerGrpCodeParent").val($("#CustomerGrpCodeParent option:first").val());
                    $('#CustomerGrpCodeParent').trigger('change');
                } else if (id === 'ShowPopupAddCustomerSource') {
                    $('#CustomerSourceName').val('');
                    $("#CustomerSourceCodeParent").val($("#CustomerSourceCodeParent option:first").val());
                    $('#CustomerSourceCodeParent').trigger('change');
                } else if (id === 'ShowPopupAddArea') {
                    $('#AreaName').val('');
                    $("#AreaCodeParent").val($("#AreaCodeParent option:first").val());
                    $('#AreaCodeParent').trigger('change');
                }

                $('#' + id).modal({
                    backdrop: false,
                    keyboard: true,
                });
                $('#' + id).modal('show');
            }
        }
    };
    this.closePopup = function (_input) {
        close_Popup(_input);
    }

    function close_Popup(_input) {
        if (_input !== undefined && _input !== null) {
            var id = $(_input).attr('id');
            if (!commonUtils.isNullOrEmpty(id)) {
                $('#' + id).modal('hide');
                $('#' + id).on('hidden.bs.modal', function () {
                    $('#' + id + ' form')[0].reset();
                });
                if (id === 'ShowPopupAddCustomerGroup') {
                    $('#CustomerGrpName').val('');
                    $('#CustomerGrpDesc').val('');
                    $("#CustomerGrpCodeParent").val($("#CustomerGrpCodeParent option:first").val());
                    $('#CustomerGrpCodeParent').trigger('change');
                    $('#div-cusgrp-avatar').css('background-image', 'none');
                    $('#CustomerGrpAvatarImage').val('');
                    $('#CustomerGrpAvatarImage').attr('name', '');
                    $('#CustomerGrpAvatarImage').attr('image-name', '');
                    $('#CustomerGrpAvatarImage').attr('image-type', '');
                } else if (id === 'ShowPopupAddCustomerSource') {
                    $('#CustomerSourceName').val('');
                    $("#CustomerSourceCodeParent").val($("#CustomerSourceCodeParent option:first").val());
                    $('#CustomerSourceCodeParent').trigger('change');
                } else if (id === 'ShowPopupAddArea') {
                    $('#AreaName').val('');
                    $("#AreaCodeParent").val($("#AreaCodeParent option:first").val());
                    $('#AreaCodeParent').trigger('change');
                }
            }
        }
    }
    this.checkPopupCustomerGroup = function() {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupAddCustomerGroup #CustomerGrpName', 'has-error-fix', 'Tên nhóm khách hàng không được trống!');
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupAddCustomerGroup #CustomerGrpCodeParent', 'has-error-fix', 'Chưa chọn nhóm khách hàng cha!');
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.addCustomerGroup = function() {
        if (this.checkPopupCustomerGroup()) {
            var _ajaxSettings = this.ajaxSettings;
            var token = $('#manageFormShowPopupAddCustomerGroup input[name=__RequestVerificationToken]').val();
            var customerGrpName = commonUtils.returnValue($('#manageFormShowPopupAddCustomerGroup #CustomerGrpName').val());
            var customerGrpCodeParent = commonUtils.returnValue($('#manageFormShowPopupAddCustomerGroup #CustomerGrpCodeParent').val());
            var customerGrpDesc = commonUtils.returnValue($('#manageFormShowPopupAddCustomerGroup #CustomerGrpDesc').val());
            debugger;
            var ListImages = [];
            var inputImage = $('input#CustomerGrpAvatarImage');
            if ($(imageBase64) !== undefined && $(imageBase64) !== null) {
                var imageType = commonUtils.returnValueOrNull($(inputImage).attr('image-type'));
                var imageName = commonUtils.returnValueOrNull($(inputImage).attr('image-name'));
                var imageBase64 = commonUtils.returnValueOrNull($(inputImage).val());
                if (!commonUtils.isNullOrEmpty(imageBase64)) {
                    var strReplace = "data:" + imageType + ";base64,";
                    imageBase64 = imageBase64.replace(strReplace, "");
                    var objMst_CustomerGroupImages = {};
                    objMst_CustomerGroupImages.ImageName = imageName;
                    objMst_CustomerGroupImages.ImageSpec = imageBase64;
                    
                    ListImages.push(objMst_CustomerGroupImages);
                }
            }

            var tem = {};
            tem.CustomerGrpName = customerGrpName;
            tem.CustomerGrpCodeParent = customerGrpCodeParent;
            tem.CustomerGrpDesc = customerGrpDesc;
            var modelCur = commonUtils.returnJSONValue(tem);
            var modelimagesCur = commonUtils.returnJSONValue(ListImages);
            var data = {
                __RequestVerificationToken: token,
                model: modelCur,
                modelimages: modelimagesCur
            };
            var dataInput = data;
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                debugger;
                if (objResult.Success) {
                    close_Popup($('#ShowPopupAddCustomerGroup'));
                    $("#CustomerGrpCode").html(objResult.Html);
                    $("#CustomerGrpCodeParent").html(objResult.Html);
                }
                else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunctionAddInformation(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunctionAddInformation();
            });
        }
    };
    this.checkPopupCustomerSource = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupAddCustomerSource #CustomerSourceName', 'has-error-fix', 'Tên nguồn khách hàng không được trống!');
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupAddCustomerSource #CustomerSourceCodeParent', 'has-error-fix', 'Chưa chọn nguồn khách hàng cha!');
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.addCustomerSource = function () {
        if (this.checkPopupCustomerSource()) {
            debugger;
            var _ajaxSettings = this.ajaxSettings;
            var token = $('#manageFormShowPopupAddCustomerSource input[name=__RequestVerificationToken]').val();
            var customerSourceName = commonUtils.returnValue($('#manageFormShowPopupAddCustomerSource #CustomerSourceName').val());
            var customerSourceCodeParent = commonUtils.returnValue($('#manageFormShowPopupAddCustomerSource #CustomerSourceCodeParent').val());
            var customerSourceDesc = commonUtils.returnValue($('#manageFormShowPopupAddCustomerSource #CustomerSourceDesc').val());
            var ListImages = [];
            var inputImage = $('input#CustomerSrcAvatarImage');
            if ($(imageBase64) !== undefined && $(imageBase64) !== null) {
                var imageType = commonUtils.returnValueOrNull($(inputImage).attr('image-type'));
                var imageName = commonUtils.returnValueOrNull($(inputImage).attr('image-name'));
                var imageBase64 = commonUtils.returnValueOrNull($(inputImage).val());
                if (!commonUtils.isNullOrEmpty(imageBase64)) {
                    var strReplace = "data:" + imageType + ";base64,";
                    imageBase64 = imageBase64.replace(strReplace, "");
                    var objMst_CustomerSourceImages = {};
                    objMst_CustomerSourceImages.ImageName = imageName;
                    objMst_CustomerSourceImages.ImageSpec = imageBase64;

                    ListImages.push(objMst_CustomerSourceImages);
                }
            }
            var tem = {};
            tem.CustomerSourceName = customerSourceName;
            tem.CustomerSourceCodeParent = customerSourceCodeParent;
            tem.CustomerSourceDesc = customerSourceDesc;
            var modelCur = commonUtils.returnJSONValue(tem);
            var modelimagesCur = commonUtils.returnJSONValue(ListImages);
            var data = {
                __RequestVerificationToken: token,
                model: modelCur,
                modelimages: modelimagesCur
            };
            var dataInput = data;
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                debugger;
                if (objResult.Success) {
                    close_Popup($('#ShowPopupAddCustomerSource'));
                    $("#CustomerSourceCode").html(objResult.Html);
                    $("#CustomerSourceCodeParent").html(objResult.Html);
                }
                else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunctionAddInformation(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunctionAddInformation();
            });
        }
    };
    this.checkPopupArea = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupAddArea #AreaName', 'has-error-fix', 'Tên khu vực không được trống!');
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupAddArea #AreaCodeParent', 'has-error-fix', 'Chưa chọn khu vực cha!');
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.addArea = function () {
        if (this.checkPopupArea()) {
            var _ajaxSettings = this.ajaxSettings;
            var token = $('#manageFormShowPopupAddArea input[name=__RequestVerificationToken]').val();
            var areaName = commonUtils.returnValue($('#manageFormShowPopupAddArea #AreaName').val());
            var areaCodeParent = commonUtils.returnValue($('#manageFormShowPopupAddArea #AreaCodeParent').val());
            var tem = {};
            tem.AreaName = areaName;
            tem.AreaCodeParent = areaCodeParent;
            var modelCur = commonUtils.returnJSONValue(tem);
            var data = {
                __RequestVerificationToken: token,
                model: modelCur,
            };
            var dataInput = data;
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                debugger;
                if (objResult.Success) {
                    close_Popup($('#ShowPopupAddArea'));
                    $("#AreaCode").html(objResult.Html);
                    $("#AreaCodeParent").html(objResult.Html);
                }
                else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunctionAddInformation(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunctionAddInformation();
            });
        }
    };
    // Open Info
    this.getValue = function() {
        var value = commonUtils.returnValueText('#ShowPopupAddInformation #InformationTitle');
        if (!commonUtils.isNullOrEmpty(value)) {
            var gen = commonUtils.genCode(value);
            $('#ShowPopupAddInformation #InformationCode').val(gen);
        }
    };
    this.addRow_Information = function () {
        if (commonUtils.checkElementExists('#tbody-information')) {
            var $tbody = $('#tbody-information');
            var $tr = $('<tr>');
            $tr.attr('class', 'trdata');
            $tr.attr('idx', '');
            var $tdEvent = $('<td>');
            var $tdSelect = $('<td>');
            var $tdInputText = $('<td>');
            // td1: thêm nút xóa
            $tdEvent.attr('class', 'cell-50 td-event-delete');
            var $aDelete = $('<a>');
            $aDelete.attr('href', 'javascript:;');
            $aDelete.attr('title', 'Xóa');
            //$aDelete.attr('class', 'btn btn-nc-button');
            $aDelete.click(function (e) {
                deleteRow_Information($(this));
            });

            var $iDelete = $('<i>');
            $iDelete.attr('class', 'fas fa-trash-alt');
            $aDelete.append($iDelete);
            $tdEvent.append($aDelete);

            // td2: thuộc tính
            $tdSelect.attr('class', 'td-select');
            var $divSelect = $('<div>');
            $divSelect.attr('class', 'form-group no-margin div-select-information');
            var $select = $('<select>');
            $select.attr('class', 'col-10 select-information');
            $select.attr('select-value', '');
            $select.attr('select-datatype', '');
            $select.attr('select-controltype', '');
            $select.change(function (e) {
                change_Information($(this));
            });
            var $optionFirst = $('<option>');
            //$optionFirst.attr('disabled', 'disabled');
            $optionFirst.attr('opt', 'null');
            $optionFirst.attr('value', '');
            $optionFirst.text('--Lựa chọn--');
            $select.append($optionFirst);

            if (ListDataInformation !== undefined && ListDataInformation !== null) {
                for (var i = 0; i < ListDataInformation.length; i++) {
                    var item = ListDataInformation[i];
                    var informationCode = commonUtils.returnValue(item.Code);
                    var informationTitle = commonUtils.returnValue(item.Title);
                    var informationDataType = commonUtils.returnValue(item.DataType);
                    var informationControlType = commonUtils.returnValue(item.ControlType);

                    var $option = $('<option>');
                    $option.attr('value', informationCode);
                    $option.attr('datatype', informationDataType);
                    $option.attr('controltype', informationControlType);
                    $option.text(informationTitle);

                    $select.append($option);
                }
            }

            $divSelect.append($select);
            // nút thêm thông tin
            var $aAddAttr = $('<a>');
            $aAddAttr.attr('class', 'btn btn-nc-button btn-add-or-edit');
            $aAddAttr.attr('title', 'Thêm thông tin');
            $aAddAttr.click(function (e) {

                showPopup_Information($(this));
            });
            var $iAddAttr = $('<i>');
            $iAddAttr.attr('class', 'fa fa-plus');
            $aAddAttr.append($iAddAttr);
            $divSelect.append($aAddAttr);
            $tdSelect.append($divSelect);

            // td3: giá trị thuộc tính
            $tdInputText.attr('class', 'td-input-text');
            var $divInputText = $('<div>');
            $divInputText.attr('class', 'form-group no-margin div-input-text');
            var $inputText = $('<input>');
            $inputText.attr('type', 'text');
            $inputText.attr('class', 'col-12 value-information');
            $inputText.attr('data-role', 'tagsinput');
            $inputText.attr('placeholder', 'Enter an input and press Enter');

            $divInputText.append($inputText);
            $tdInputText.append($divInputText);

            $tr.append($tdEvent);
            $tr.append($tdSelect);
            $tr.append($tdInputText);
            $tbody.append($tr);

            $('.select-information').select2();
            $(".value-information").tagsinput();

            commonUtils.updateTableTrNotShowIdx($('#tbody-information tr.trdata'), true);
        }
    };
    this.deleteRowInformation = function (thiz) {
        deleteRow_Information(thiz);
    };
    function deleteRow_Information(thiz) {
        var tr = $(thiz).parent().parent();
        if (tr !== undefined && tr !== null) {
            $(tr).remove();
        }
        
    }
    function closePopupAddInformation() {
        $('#ShowPopupAddInformation').modal('hide');
        $('#ShowPopupAddInformation').on('hidden.bs.modal', function () {
            $(this).find('#manageFormShowPopupAddInformation').trigger('reset');
        });
    }
    this.closePopup_AddInformation = function() {
        closePopupAddInformation();
    };
    function closePopupUpdateInformation() {
        $('#ShowPopupUpdateInformation').modal('hide');
        $('#ShowPopupUpdateInformation').on('hidden.bs.modal', function () {
            $('#ShowPopupUpdateInformation form')[0].reset();
        });
    }
    this.closePopup_UpdateInformation = function() {
        closePopupUpdateInformation();
    }
    function showPopup_Information(thiz) {
        
        var idx = $(thiz).closest('tr.trdata').attr('idx');
        $('#ShowPopupAddInformation #InformationCode').attr('idx', idx);
        $('#ShowPopupAddInformation').modal({
            backdrop: false,
            keyboard: true,
        });
        $('#ShowPopupAddInformation').modal('show');
        
    }

    this.showPopupEdit_Information = function (thiz) {
        showPopup_Edit_Information(thiz)
    };
    function showPopup_Edit_Information(thiz) {
        debugger;
        var code = $(thiz).siblings('select.select-information').attr('select-value');
        var title = $(thiz).siblings('select.select-information').find('option[value = "' + code + '"]').text();
        var controlType = $(thiz).siblings('select.select-information').attr('select-controltype');
        var dataType = $(thiz).siblings('select.select-information').attr('select-datatype');
        // Show popup update information
        $('#ShowPopupUpdateInformation #InformationCodeEdit').val(code);
        $('#ShowPopupUpdateInformation #InformationTitleEdit').val(title);
        $('#ShowPopupUpdateInformation #InformationControlTypeEdit').find('option[value="' + controlType + '"]').attr('selected', 'selected');
        $('#ShowPopupUpdateInformation #InformationPrdDynamicFieldDataTypesEdit').find('option[value="' + dataType + '"]').attr('selected', 'selected');
        $('#ShowPopupUpdateInformation').modal({
            backdrop: false,
            keyboard: true,
        });
        $('#ShowPopupUpdateInformation').modal('show');
        
    }
    function change_Information(thiz) {
        if (thiz !== undefined && thiz !== null) {
            var $divSelectInformation = $(thiz).parent();
            var _value = commonUtils.returnValue($(thiz).val());
            var datatype = commonUtils.returnValue($(thiz).find('option:selected').attr('datatype'));
            var controltype = commonUtils.returnValue($(thiz).find('option:selected').attr('controltype'));
            if (!commonUtils.isNullOrEmpty(_value)) {
                // gen products
                var isCheckExists = false;
                var tr = $(thiz).parent().parent().parent();
                var idx = $(tr).attr('idx');
                var rows = $('tbody#tbody-information tr.trdata').length;
                if (rows > 0) {
                    $('tbody#tbody-information tr.trdata').each(function () {
                        debugger
                        var trCur = $(this);
                        if (trCur !== undefined && trCur !== null) {
                            var idxCur = $(trCur).attr('idx');
                            if (idx !== idxCur) {
                                var select = $(trCur).find('td.td-select select.select-information');
                                if (select !== undefined && select !== null) {
                                    var informationCode = commonUtils.returnValue($(select).val());
                                    if (informationCode === _value) {
                                        var listToastr = [];
                                        objToastr = { ToastrType: 'error', ToastrMessage: 'Thuộc tính đã tồn tại trong bảng thông tin' };
                                        listToastr.push(objToastr);
                                        commonUtils.showToastr(listToastr);
                                        isCheckExists = true;
                                        return false;
                                    }
                                }

                            }
                        }
                    });
                }


                if (!isCheckExists) {
                    var $aEditAttr = $divSelectInformation.find('a.btn-add-or-edit');
                    if ($aEditAttr !== undefined && $aEditAttr !== null) {
                        $aEditAttr.empty();
                        $aEditAttr.attr('title', 'Sửa thông tin');
                        $aEditAttr.off('click');
                        $aEditAttr.on('click', function (e) {
                            showPopup_Edit_Information($(this));
                        });
                        var $iEditAttr = $('<i>');
                        $iEditAttr.attr('class', 'fas fa-pencil-alt');
                        $aEditAttr.append($iEditAttr);
                    }
                    var $option = $(thiz).find('option[opt="null"]');
                    if ($option !== undefined && $option !== null) {
                        $option.attr('disabled', 'disabled');
                    }

                    var inputText = $(tr).find('td.td-input-text input.value-information');
                    if (inputText !== undefined && inputText !== null) {
                        var _valueInputText = commonUtils.returnValue($(inputText).val());
                        if (!commonUtils.isNullOrEmpty(_valueInputText)) {
                            // gen products
                        }
                    }

                } else {
                    var $optionSelected = $(thiz).find('option:selected');
                    $optionSelected.removeAttr('selected');
                    $('#mySelect option:selected').removeAttr('selected');
                    // gán bằng giá trị đã chọn lần trước nếu có
                    //_value = $(thiz).attr('select-value');
                    _value = '';
                    $(thiz).val(_value);
                }


            } else {
                // xóa product theo thuộc tính đã chọn lúc trước nếu có

            }

            $(thiz).attr('select-value', _value);
            $(thiz).attr('select-datatype', datatype);
            $(thiz).attr('select-controltype', controltype);
        }

    }
    this.checkFormAddInformation = function() {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupAddInformation #InformationTitle', 'has-error-fix', 'Tên thông tin bổ sung không được trống!');
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.checkFormEditInformation = function () {
        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#manageFormShowPopupUpdateInformation #InformationTitleEdit', 'has-error-fix', 'Tên thông tin bổ sung không được trống!');
        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    this.addInformation = function () {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        if (this.checkFormAddInformation()) {
            debugger;
            var token = $('#manageFormShowPopupAddInformation input[name=__RequestVerificationToken]').val();
            var code = commonUtils.returnValue($('#InformationCode').val());
            var title = commonUtils.returnValue($('#InformationTitle').val());
            var controlType = commonUtils.returnValue($('#InformationControlType').val());
            var dataType = commonUtils.returnValue($('#InformationPrdDynamicFieldDataTypes').val());
            var tem = {};
            tem.Code = code;
            tem.Title = title;
            tem.ControlType = controlType;
            tem.DataType = dataType;
            var modelCur = commonUtils.returnJSONValue(tem);
            var data = {
                __RequestVerificationToken: token,
                model: modelCur,
            };
            var dataInput = data;
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                debugger
                if (objResult.Success) {
                    debugger
                    closePopupAddInformation();
                    ListDataInformation.push({ Code: code, Title: title });
                    $('.select-information').each(function () {
                        debugger
                        let selected = commonUtils.returnValue($('#InformationCode').attr('idx')) === $(this).closest('tr.trdata').attr('idx') ? 'selected' : '';
                        $(this).append('<option value="' + code + ' " ' + selected + '>' + title + '</option>');
                        $(this).select2();
                    })

                    var $divSelectInformation = $('.select-information').closest('tr.trdata[idx="' + commonUtils.returnValue($('#InformationCode').attr('idx')) + '"]')
                    var $aEditAttr = $divSelectInformation.find('a.btn-add-or-edit');
                    $aEditAttr.empty();
                    $aEditAttr.attr('title', 'Sửa thông tin');
                    $aEditAttr.off('click');
                    $aEditAttr.on('click', function (e) {
                        var attributeCode = $($aEditAttr).siblings('select.select-information').attr('select-value');
                        var attributeName = $($aEditAttr).siblings('select.select-information').find('option[value = "' + code + '"]').text();
                        // Show popup update attribute
                        //showPopup_Edit_Information(attributeCode, attributeName);
                        showPopup_Edit_Information($(this));
                    });
                    var $iEditAttr = $('<i>');
                    $iEditAttr.attr('class', 'fas fa-pencil-alt');
                    $aEditAttr.append($iEditAttr);
                }
                else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunctionAddInformation(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunctionAddInformation();
            });
        }
        
    };
    this.editInformation = function () {
        debugger;
        var _ajaxSettings = this.ajaxSettings;
        if (this.checkFormEditInformation()) {
            debugger;
            var token = $('#manageFormShowPopupUpdateInformation input[name=__RequestVerificationToken]').val();
            var code = commonUtils.returnValue($('#manageFormShowPopupUpdateInformation #InformationCodeEdit').val());
            var title = commonUtils.returnValue($('#manageFormShowPopupUpdateInformation #InformationTitleEdit').val());
            var controlType = commonUtils.returnValue($('#manageFormShowPopupUpdateInformation #InformationControlTypeEdit').val());
            var dataType = commonUtils.returnValue($('#manageFormShowPopupUpdateInformation #InformationPrdDynamicFieldDataTypesEdit').val());
            var tem = {};
            tem.Code = code;
            tem.Title = title;
            tem.ControlType = controlType;
            tem.DataType = dataType;
            var modelCur = commonUtils.returnJSONValue(tem);
            var data = {
                __RequestVerificationToken: token,
                model: modelCur,
            };
            var dataInput = data;
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                if (objResult.Success) {
                    closePopupUpdateInformation();
                    debugger;
                    ListDataInformation.find(element => element.Code === code).Title = title;
                    $('.select-information').each(function() {
                        $(this).find('option[value="' + code + '"]').text(title);
                        $(this).select2();
                    });
                }
                else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunctionAddInformation(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunctionAddInformation();
            });
        }

    };
    function failFunctionAddInformation(jqXHR, textStatus, errorThrown) {
    }
    function alwaysFunctionAddInformation() {
    }

    //
    this.showPopupWarning = function (flagRedirect) {
        $('#Confirm').attr('data-flag', flagRedirect);
        $('#ShowPopupWarning').modal({
            backdrop: false,
            keyboard: true,
        });
        $('#ShowPopupWarning').modal('show');
    };
    
    this.closePopupWarning = function() {
        closePopup_Warning();
    };

    function closePopup_Warning() {
        $('#ShowPopupWarning').modal('hide');
        $('#ShowPopupWarning').on('hidden.bs.modal', function () {
            $(this).find('#manageFormShowPopupWarning').trigger('reset');
        });
    }

    this.closePopupCustomerCheck = function() {
        closePopup_CustomerCheck();
    };

    function closePopup_CustomerCheck() {
        $("#lblMessage").text('');
        $('#ShowPopupCustomerCheck').modal('hide');
        $('#ShowPopupCustomerCheck').on('hidden.bs.modal', function () {
            $(this).find('#manageFormShowPopupCustomerCheck').trigger('reset');
        });
    }
    this.onConfirm = function (thiz) {
        debugger;
        var flag = $(thiz).attr('data-flag');
        var ajaxSettings = {};
        ajaxSettings.Type = 'post';
        ajaxSettings.DataType = 'json';
        ajaxSettings.Url = '@Url.Action("Create", "Mst_Customer")';
        this.ajaxSettings = ajaxSettings;
        this.saveData(flag);
    };
    this.customerCheck = function (flagRedirect) {
        debugger
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataUpdate(flagRedirect);
        if (this.checkForm()) {
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                async: false,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                debugger;
                if (objResult.Success) {
                    if (objResult.Action === '1') {
                        // đóng popupcheck mã khách hàng
                        closePopup_Warning();
                        // Show Popup cảnh báo
                        var customerCode = commonUtils.returnValue(objResult.CustomerCode);
                        var messages = commonUtils.returnValue(objResult.Messages);
                        showPopupCustomerCheck(flagRedirect, customerCode, messages);
                    } else {
                        // thực hiện gọi hàm lưu luôn
                        SaveData(flagRedirect); // Create
                    }
                    
                }
                else {
                    if (!commonUtils.isNullOrEmpty(objResult.Detail)) {
                        _showErrorMsg123('Lỗi!', objResult.Detail);
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        }
    };

    function showPopupCustomerCheck(flagRedirect, customercode, message) {
        $('#CustomerCode_CustomerCheck').val(customercode);
        $('#lblMessage').text(message);
        $('#ConfirmCustomerCheck').attr('data-flag', flagRedirect);
        $('#CancelCustomerCheck').attr('data-flag', flagRedirect);
        $('#ShowPopupCustomerCheck').modal({
            backdrop: false,
            keyboard: true,
        });
        $('#ShowPopupCustomerCheck').modal('show');
    }
    this.getData = function (flagRedirect) {
        var objMst_Customer = {};
        var customerType = $('input[name="ckb-customertype"]:checked').val();
        objMst_Customer.CustomerType = customerType;
        var customerGender = $('input[name="ckb-customergender"]:checked').val();
        objMst_Customer.CustomerType = customerType;
        objMst_Customer.CustomerGender = customerGender;
        objMst_Customer.CustomerCodeSys = commonUtils.returnValueTextOrNull('#CustomerCodeSys');
        objMst_Customer.CustomerCode = commonUtils.returnValueTextOrNull('#CustomerCode');
        objMst_Customer.CustomerName = commonUtils.returnValueTextOrNull('#CustomerName');
        objMst_Customer.CustomerGrpCode = commonUtils.returnValueTextOrNull('#CustomerGrpCode');
        objMst_Customer.CustomerSourceCode = commonUtils.returnValueTextOrNull('#CustomerSourceCode');
        objMst_Customer.CustomerPhoneNo = commonUtils.returnValueTextOrNull('#CustomerPhoneNo');
        objMst_Customer.CustomerMobilePhone = commonUtils.returnValueTextOrNull('#CustomerMobilePhone');
        objMst_Customer.CustomerAddress = commonUtils.returnValueTextOrNull('#CustomerAddress');
        objMst_Customer.ProvinceCode = commonUtils.returnValueTextOrNull('#ProvinceCode');
        objMst_Customer.DistrictCode = commonUtils.returnValueTextOrNull('#DistrictCode');
        objMst_Customer.WardCode = commonUtils.returnValueTextOrNull('#WardCode');
        objMst_Customer.AreaCode = commonUtils.returnValueTextOrNull('#AreaCode');
        objMst_Customer.RepresentName = commonUtils.returnValueTextOrNull('#RepresentName');
        objMst_Customer.RepresentPosition = commonUtils.returnValueTextOrNull('#RepresentPosition');
        objMst_Customer.GovIDCardNo = commonUtils.returnValueTextOrNull('#GovIDCardNo');
        objMst_Customer.GovIDType = commonUtils.returnValueTextOrNull('#GovIDType');
        objMst_Customer.BankAccountNo = commonUtils.returnValueTextOrNull('#BankAccountNo');
        objMst_Customer.BankName = commonUtils.returnValueTextOrNull('#BankName');
        objMst_Customer.ContactName = commonUtils.returnValueTextOrNull('#ContactName');
        objMst_Customer.ContactPhone = commonUtils.returnValueTextOrNull('#ContactPhone');
        objMst_Customer.ContactEmail = commonUtils.returnValueTextOrNull('#ContactEmail');
        objMst_Customer.Fax = commonUtils.returnValueTextOrNull('#Fax');
        objMst_Customer.CustomerDateOfBirth = commonUtils.returnValueTextOrNull('#CustomerDateOfBirth');
        objMst_Customer.Facebook = commonUtils.returnValueTextOrNull('#Facebook');
        objMst_Customer.InvoiceCustomerName = commonUtils.returnValueTextOrNull('#InvoiceCustomerName');
        objMst_Customer.InvoiceOrgName = commonUtils.returnValueTextOrNull('#InvoiceOrgName');
        objMst_Customer.MST = commonUtils.returnValueTextOrNull('#MST');
        objMst_Customer.InvoiceCustomerAddress = commonUtils.returnValueTextOrNull('#InvoiceCustomerAddress');
        objMst_Customer.InvoiceEmailSend = commonUtils.returnValueTextOrNull('#InvoiceEmailSend');
        objMst_Customer.Remark = commonUtils.returnValueTextOrNull('#Remark');
        var flagDealer = '';
        var flagSupplier = '';
        var flagEndUser = '';
        var flagBank = '';
        var flagInsurrance = '';
        if (commonUtils.checkElementExists('#FlagActive')) {
            var flagActive = '0';
            var inputFlagActive = $('#FlagActive');
            if (inputFlagActive !== undefined && inputFlagActive !== null) {
                if (inputFlagActive.prop('checked')) {
                    flagActive = '1';
                }
            }
            objMst_Customer.FlagActive = flagActive;
        }
        if (commonUtils.checkElementExists('#FlagDealer')) {
            flagDealer = '0';
            var inputFlagDealer = $('#FlagDealer');
            if (inputFlagDealer !== undefined && inputFlagDealer !== null) {
                if (inputFlagDealer.prop('checked')) {
                    flagDealer = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagSupplier')) {
            flagSupplier = '0';
            var inputFlagSupplier = $('#FlagSupplier');
            if (inputFlagSupplier !== undefined && inputFlagSupplier !== null) {
                if (inputFlagSupplier.prop('checked')) {
                    flagSupplier = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagEndUser')) {
            flagEndUser = '0';
            var inputFlagEndUser = $('#FlagEndUser');
            if (inputFlagEndUser !== undefined && inputFlagEndUser !== null) {
                if (inputFlagEndUser.prop('checked')) {
                    flagEndUser = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagBank')) {
            flagBank = '0';
            var inputFlagBank = $('#FlagBank');
            if (inputFlagBank !== undefined && inputFlagBank !== null) {
                if (inputFlagBank.prop('checked')) {
                    flagBank = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagInsurrance')) {
            flagInsurrance = '0';
            var inputFlagInsurrance = $('#FlagInsurrance');
            if (inputFlagInsurrance !== undefined && inputFlagInsurrance !== null) {
                if (inputFlagInsurrance.prop('checked')) {
                    flagInsurrance = '1';
                }
            }
        }
        objMst_Customer.FlagDealer = flagDealer;
        objMst_Customer.FlagSupplier = flagSupplier;
        objMst_Customer.FlagEndUser = flagEndUser;
        objMst_Customer.FlagBank = flagBank;
        objMst_Customer.FlagInsurrance = flagInsurrance;
        var inputImage = $('input#AvatarImage');
        if ($(imageBase64) !== undefined && $(imageBase64) !== null) {
            var imageType = commonUtils.returnValueOrNull($(inputImage).attr('image-type'));
            var imageName = commonUtils.returnValueOrNull($(inputImage).attr('image-name'));
            var imageBase64 = commonUtils.returnValueOrNull($(inputImage).val());
            if (!commonUtils.isNullOrEmpty(imageBase64)) {
                var strReplace = "data:" + imageType + ";base64,";
                imageBase64 = imageBase64.replace(strReplace, "");
                objMst_Customer.CustomerAvatarSpec = imageBase64;
                objMst_Customer.CustomerAvatarName = imageName;
            }
        }


        objMst_Customer.FlagCustomerAvatarPath = '0';
        //List DynamicField
        var lstPrdDynamicField = "";
        var rows = $("#table-information tr.trdata").length;

        if (rows > 0) {
            var trArr = $('#table-information tr.trdata');
            lstPrdDynamicField = lstPrdDynamicField.concat('{ ');
            for (var i = 0; i < trArr.length; i++) {
                var trCur = trArr[i];
                if (trCur !== null && trCur !== undefined) {
                    var idx = $(trCur).attr('idx');
                    var temPrd_DynamicFieldCur = {};
                    var code = $(trCur).find('.select-information').val();
                    var defaultvalue = $(trCur).find('.value-information').val().replace(',', ';');
                    if (i > 0) {
                        lstPrdDynamicField = lstPrdDynamicField.concat(',');
                    }
                    lstPrdDynamicField = lstPrdDynamicField.concat('"', code, '": "', defaultvalue, '"');

                }
            }
            lstPrdDynamicField = lstPrdDynamicField.concat(' }');
        }
        if (!commonUtils.isNullOrEmpty(lstPrdDynamicField)) {
            objMst_Customer.ListOfCustDynamicFieldValue = lstPrdDynamicField;
        }

        objMst_Customer.FlagRedirect = flagRedirect;
        var modelCur = commonUtils.returnJSONValue(objMst_Customer);
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var data = {
            __RequestVerificationToken: token,
            model: modelCur
        };
        return data;
    };



    this.getDataUpdate = function (flagRedirect) {
        debugger
        var objMst_Customer = {};
        var customerType = $('input[name="ckb-customertype"]:checked').val();
        objMst_Customer.CustomerType = customerType;
        var customerGender = $('input[name="ckb-customergender"]:checked').val();
        objMst_Customer.CustomerType = customerType;
        objMst_Customer.CustomerGender = customerGender;
        objMst_Customer.CustomerCodeSys = commonUtils.returnValueTextOrNull('#CustomerCodeSys');
        objMst_Customer.CustomerCode = commonUtils.returnValueTextOrNull('#CustomerCode');
        objMst_Customer.CustomerName = commonUtils.returnValueTextOrNull('#CustomerName');
        objMst_Customer.CustomerGrpCode = commonUtils.returnValueTextOrNull('#CustomerGrpCode');
        objMst_Customer.CustomerSourceCode = commonUtils.returnValueTextOrNull('#CustomerSourceCode');
        objMst_Customer.CustomerPhoneNo = commonUtils.returnValueTextOrNull('#CustomerPhoneNo');
        objMst_Customer.CustomerMobilePhone = commonUtils.returnValueTextOrNull('#CustomerMobilePhone');
        objMst_Customer.CustomerAddress = commonUtils.returnValueTextOrNull('#CustomerAddress');
        objMst_Customer.ProvinceCode = commonUtils.returnValueTextOrNull('#ProvinceCode');
        objMst_Customer.DistrictCode = commonUtils.returnValueTextOrNull('#DistrictCode');
        objMst_Customer.WardCode = commonUtils.returnValueTextOrNull('#WardCode');
        objMst_Customer.AreaCode = commonUtils.returnValueTextOrNull('#AreaCode');
        objMst_Customer.RepresentName = commonUtils.returnValueTextOrNull('#RepresentName');
        objMst_Customer.RepresentPosition = commonUtils.returnValueTextOrNull('#RepresentPosition');
        objMst_Customer.GovIDCardNo = commonUtils.returnValueTextOrNull('#GovIDCardNo');
        objMst_Customer.GovIDType = commonUtils.returnValueTextOrNull('#GovIDType');
        objMst_Customer.BankAccountNo = commonUtils.returnValueTextOrNull('#BankAccountNo');
        objMst_Customer.BankName = commonUtils.returnValueTextOrNull('#BankName');
        objMst_Customer.ContactName = commonUtils.returnValueTextOrNull('#ContactName');
        objMst_Customer.ContactPhone = commonUtils.returnValueTextOrNull('#ContactPhone');
        objMst_Customer.ContactEmail = commonUtils.returnValueTextOrNull('#ContactEmail');
        objMst_Customer.Fax = commonUtils.returnValueTextOrNull('#Fax');
        objMst_Customer.CustomerDateOfBirth = commonUtils.returnValueTextOrNull('#CustomerDateOfBirth');
        objMst_Customer.Facebook = commonUtils.returnValueTextOrNull('#Facebook');
        objMst_Customer.InvoiceCustomerName = commonUtils.returnValueTextOrNull('#InvoiceCustomerName');
        objMst_Customer.InvoiceOrgName = commonUtils.returnValueTextOrNull('#InvoiceOrgName');
        objMst_Customer.MST = commonUtils.returnValueTextOrNull('#MST');
        objMst_Customer.InvoiceCustomerAddress = commonUtils.returnValueTextOrNull('#InvoiceCustomerAddress');
        objMst_Customer.InvoiceEmailSend = commonUtils.returnValueTextOrNull('#InvoiceEmailSend');
        objMst_Customer.Remark = commonUtils.returnValueTextOrNull('#Remark');
        var flagDealer = '';
        var flagSupplier = '';
        var flagEndUser = '';
        var flagBank = '';
        var flagInsurrance = '';
        if (commonUtils.checkElementExists('#FlagActive')) {
            var flagActive = '0';
            var inputFlagActive = $('#FlagActive');
            if (inputFlagActive !== undefined && inputFlagActive !== null) {
                if (inputFlagActive.prop('checked')) {
                    flagActive = '1';
                }
            }
            objMst_Customer.FlagActive = flagActive;
        }
        if (commonUtils.checkElementExists('#FlagDealer')) {
            flagDealer = '0';
            var inputFlagDealer = $('#FlagDealer');
            if (inputFlagDealer !== undefined && inputFlagDealer !== null) {
                if (inputFlagDealer.prop('checked')) {
                    flagDealer = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagSupplier')) {
            flagSupplier = '0';
            var inputFlagSupplier = $('#FlagSupplier');
            if (inputFlagSupplier !== undefined && inputFlagSupplier !== null) {
                if (inputFlagSupplier.prop('checked')) {
                    flagSupplier = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagEndUser')) {
            flagEndUser = '0';
            var inputFlagEndUser = $('#FlagEndUser');
            if (inputFlagEndUser !== undefined && inputFlagEndUser !== null) {
                if (inputFlagEndUser.prop('checked')) {
                    flagEndUser = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagBank')) {
            flagBank = '0';
            var inputFlagBank = $('#FlagBank');
            if (inputFlagBank !== undefined && inputFlagBank !== null) {
                if (inputFlagBank.prop('checked')) {
                    flagBank = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagInsurrance')) {
            flagInsurrance = '0';
            var inputFlagInsurrance = $('#FlagInsurrance');
            if (inputFlagInsurrance !== undefined && inputFlagInsurrance !== null) {
                if (inputFlagInsurrance.prop('checked')) {
                    flagInsurrance = '1';
                }
            }
        }
        objMst_Customer.FlagDealer = flagDealer;
        objMst_Customer.FlagSupplier = flagSupplier;
        objMst_Customer.FlagEndUser = flagEndUser;
        objMst_Customer.FlagBank = flagBank;
        objMst_Customer.FlagInsurrance = flagInsurrance;
        var inputImage = $('input#AvatarImage');
        if ($(imageBase64) !== undefined && $(imageBase64) !== null) {
            var imageType = commonUtils.returnValueOrNull($(inputImage).attr('image-type'));
            var imageName = commonUtils.returnValueOrNull($(inputImage).attr('image-name'));
            var imageBase64 = commonUtils.returnValueOrNull($(inputImage).val());
            if (!commonUtils.isNullOrEmpty(imageBase64)) {
                var strReplace = "data:" + imageType + ";base64,";
                imageBase64 = imageBase64.replace(strReplace, "");
                objMst_Customer.CustomerAvatarSpec = imageBase64;
                objMst_Customer.CustomerAvatarName = imageName;
            }
        }


        objMst_Customer.FlagCustomerAvatarPath = '0';
        //List DynamicField
        debugger
        var lstPrdDynamicField = "";
        var rows = $("#table-informationupdate tr.trdata").length;

        if (rows > 0) {
            var trArr = $('#table-informationupdate tr.trdata');
            lstPrdDynamicField = lstPrdDynamicField.concat('{ ');
            for (var i = 0; i < trArr.length; i++) {
                var trCur = trArr[i];
                if (trCur !== null && trCur !== undefined) {
                    var idx = $(trCur).attr('idx');
                    var temPrd_DynamicFieldCur = {};
                    var code = $(trCur).find('.select-information').val();
                    var defaultvalue = $(trCur).find('.value-information').val().replace(',', ';');
                    if (i > 0) {
                        lstPrdDynamicField = lstPrdDynamicField.concat(',');
                    }
                    lstPrdDynamicField = lstPrdDynamicField.concat('"', code, '": "', defaultvalue, '"');

                }
            }
            lstPrdDynamicField = lstPrdDynamicField.concat(' }');
        }
        if (!commonUtils.isNullOrEmpty(lstPrdDynamicField)) {
            objMst_Customer.ListOfCustDynamicFieldValue = lstPrdDynamicField;
        }

        objMst_Customer.FlagRedirect = flagRedirect;
        var modelCur = commonUtils.returnJSONValue(objMst_Customer);
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var data = {
            __RequestVerificationToken: token,
            model: modelCur
        };
        return data;
    };

    this.saveData = function (flagRedirect) {
        debugger;
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData(flagRedirect);
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



    this.saveDataUpd = function (flagRedirect) {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getDataUpdate(flagRedirect);
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
                var listToastr = [];
                objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages };
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