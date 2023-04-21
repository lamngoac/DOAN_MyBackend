function Mst_Organization() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.checkForm = function () {
        var listError = [];
        //listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#OrgID', 'has-error-fix', 'Mã tổ chức không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#NNTFullName', 'has-error-fix', 'Tên tổ chức không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#AreaCode', 'has-error-fix', 'Vùng thị trường không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ContactName', 'has-error-fix', 'Người liên hệ không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ContactEmail', 'has-error-fix', 'Email liên hệ không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#MST', 'has-error-fix', 'Mã số thuế không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#PresentBy', 'has-error-fix', 'Người đại diện không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#NNTPosition', 'has-error-fix', 'Chức vụ không được trống!');

        if (!commonUtils.isNullOrEmpty(commonUtils.returnValueTextOrNull('#ContactEmail'))) {
            var contactEmail = $('#ContactEmail').val();
            if (!commonUtils.isNullOrEmpty(contactEmail)) {
                if (!commonUtils.validateEmail(contactEmail)) {
                    var objToastr = {
                        ToastrType: 'error',
                        ToastrMessage: 'Email liên hệ không đúng định dạng!'
                    };
                    listError.push(objToastr);
                }
            }
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
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
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        //var orgid = commonUtils.returnValueText('#OrgID');
        var nntfullname = commonUtils.returnValueText('#NNTFullName');
        //Tên viết tắt
        var dlcode = commonUtils.returnValueText('#DLCode');
        var areacode = commonUtils.returnValueText('#AreaCode');
        var mst = commonUtils.returnValueText('#MST');
        var presentby = commonUtils.returnValueText('#PresentBy');
        var nntposition = commonUtils.returnValueText('#NNTPosition');
        var presentidno = commonUtils.returnValueText('#PresentIDNo');
        var presentidtype = commonUtils.returnValueText('#PresentIDType');
        var nntaddress = commonUtils.returnValueText('#NNTAddress');
        var districtcode = commonUtils.returnValueText('#DistrictCode');
        var provincecode = commonUtils.returnValueText('#ProvinceCode');
        var website = commonUtils.returnValueText('#Website');
        //var contactemail = commonUtils.returnValueText('#ContactEmail');
        var nntphone = commonUtils.returnValueText('#NNTPhone');
        var nntmobile = commonUtils.returnValueText('#NNTMobile');
        var nntfax = commonUtils.returnValueText('#NNTFax');
        var contactname = commonUtils.returnValueText('#ContactName');
        var contactemail = commonUtils.returnValueText('#ContactEmail');
        var contactphone = commonUtils.returnValueText('#ContactPhone');
        var accno = commonUtils.returnValueText('#AccNo');
        var accholder = commonUtils.returnValueText('#AccHolder');
        var bankname = commonUtils.returnValueText('#BankName');
        var canumber = commonUtils.returnValueText('#CANumber');
        var caorg = commonUtils.returnValueText('#CAOrg');
        var caeffdtimeutcstart = commonUtils.returnValueText('#CAEffDTimeUTCStart');
        var caeffdtimeutcend = commonUtils.returnValueText('#CAEffDTimeUTCEnd');

        debugger
        var orgID_MA = commonUtils.returnValueText('#OrgID_MA');

        var objMst_Organization = new Object();
        //objMst_Organization.OrgID = orgid;
        objMst_Organization.NNTFullName = nntfullname;
        //Tên viết tắt
        objMst_Organization.DLCode = dlcode;
        objMst_Organization.AreaCode = areacode;
        objMst_Organization.MST = mst;
        objMst_Organization.PresentBy = presentby;
        objMst_Organization.NNTPosition = nntposition;
        objMst_Organization.PresentIDNo = presentidno;
        objMst_Organization.PresentIDType = presentidtype;
        objMst_Organization.NNTAddress = nntaddress;
        objMst_Organization.DistrictCode = districtcode;
        objMst_Organization.ProvinceCode = provincecode;
        objMst_Organization.Website = website;
        //objMst_Organization.ContactEmail = contactemail;
        objMst_Organization.NNTPhone = nntphone;
        objMst_Organization.NNTMobile = nntmobile;
        objMst_Organization.NNTFax = nntfax;
        objMst_Organization.ContactName = contactname;
        objMst_Organization.ContactEmail = contactemail;
        objMst_Organization.ContactPhone = contactphone;
        objMst_Organization.AccNo = accno;
        objMst_Organization.AccHolder = accholder;
        objMst_Organization.BankName = bankname;
        objMst_Organization.CANumber = canumber;
        objMst_Organization.CAOrg = caorg;
        objMst_Organization.CAEffDTimeUTCStart = caeffdtimeutcstart;
        objMst_Organization.CAEffDTimeUTCEnd = caeffdtimeutcend;
        objMst_Organization.OrgID_MA = orgID_MA;

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
                    objMst_Organization.FlagActive = flagActive;
                }
            }
        }
        var modelCur = commonUtils.returnJSONValue(objMst_Organization);
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



    this.loadOS_Inos_Org = function (thiz) {
        var _ajaxSettings = this.ajaxSettings;

        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var orgId = commonUtils.returnValueText('#OrgID');


        if (!commonUtils.isNullOrEmpty(orgId)) {
            var dataInput = {
                __RequestVerificationToken: token,
                orgId: orgId
            };
            $.ajax({
                type: _ajaxSettings.Type,
                data: dataInput,
                url: _ajaxSettings.Url,
                dataType: _ajaxSettings.DataType,
                beforeSend: function () {
                }
            }).done(function (objResult) {
                if (objResult.Success) {
                    if (objResult.Data !== null && objResult.Data !== undefined) {
                        $("#NNTFullName").val(objResult.Data.Name);
                        $("#ContactName").val(objResult.Data.ContactName);
                        $("#ContactEmail").val(objResult.Data.Email);
                        $("#ContactPhone").val(objResult.Data.PhoneNo);
                        if (objResult.Data.BizType != null) {
                            $("#BizType").val(objResult.Data.BizType.Id).trigger('change');
                        }
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        }
    }

    this.deleteData = function (objMst_Organization) {
        var orgid = commonUtils.returnValue(objMst_Organization.OrgID);
        var mst = commonUtils.returnValue(objMst_Organization.MST);
        
        if (!commonUtils.isNullOrEmpty(orgid) && !commonUtils.isNullOrEmpty(mst)) {
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
                        debugger
                        var token = $('input[name=__RequestVerificationToken]').val();
                        var dataInput = {
                            mst: mst,
                            orgid: orgid
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
    this.getSysUser = function (groupcode) {
        var _ajaxSettings = this.ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();

        if (!commonUtils.isNullOrEmpty(groupcode)) {
            $.ajax({
                url: _ajaxSettings.Url,
                data: {
                    __RequestVerificationToken: token
                    , groupcode: groupcode
                },
                type: _ajaxSettings.Type,
                dataType: _ajaxSettings.DataType,
                traditional: true,
                success: function (data) {
                    if (data.Success) {
                        $("#ShowPopup").addClass('show');
                        $("#ShowPopup").css('display', 'block');
                        $("#ShowPopup").html(data.Html);
                        $('#ShowPopup').modal({
                            backdrop: false,
                            keyboard: true,
                        });

                    } else {
                        _showErrorMsg123("Lỗi!", data.Detail);
                    }

                }
            });
        }
        else {
            commonUtils.showAlert('Chưa chọn nhóm người dùng!');
            return;
        }
    };
    this.saveUserInGroup = function () {
        var token = $('#manageFormPopupOfSysGroup input[name=__RequestVerificationToken]').val();
        var rows = $("tbody#table-tbodyID-popup tr.trdata").length;
        var _ajaxSettings = this.ajaxSettings;

        if (rows > 0) {
            var groupcode = $('#GroupCodePU').val();
            if (!commonUtils.isNullOrEmpty(groupcode)) {
                var trArr = $('tbody#table-tbodyID-popup tr.trdata').has('input[name="ckbpopup"]:checked');
                var tem = new Object();
                var sysGroup = new Object();
                sysGroup.GroupCode = groupcode;

                tem.Sys_Group = sysGroup;

                var ListSysUserInGroup = [];
                if (trArr !== undefined && trArr !== null && trArr.length > 0) {

                    for (var i = 0; i < trArr.length; i++) {
                        var trCur = trArr[i];
                        if (trCur != null && trCur != undefined) {
                            var idx = $(trCur).attr('idx');
                            var userCodeCur = $(trCur).find('input[type="hidden"][name="ListSysUser[' + idx + '].UserCode"]').val();

                            var temSysUserInGroup = {};
                            temSysUserInGroup.UserCode = userCodeCur;
                            temSysUserInGroup.GroupCode = groupcode;

                            ListSysUserInGroup.push(temSysUserInGroup);
                        }
                    }
                }
                tem.Lst_Sys_UserInGroup = ListSysUserInGroup;
                var modelCur = JSON.stringify(tem);

                $.ajax({
                    type: _ajaxSettings.Type,
                    url: _ajaxSettings.Url,
                    data: {
                        __RequestVerificationToken: token
                        , model: modelCur
                    },
                    traditional: true,
                    //contentType: "application/json; charset=utf-8",
                    dataType: _ajaxSettings.DataType,
                    success: function (data) {
                        if (data.Success) {
                            alert(data.Title);

                            var rowsMaster = $("tbody#tbodyID tr.trdata").length;
                            if (rowsMaster > 0) {
                                $('.selected').removeClass('selected');
                                $("tbody#tbodyID tr.trdata").each(function () {
                                    var tr = $(this);
                                    if (tr !== undefined && tr !== null) {
                                        var groupcodeCur = tr.attr('groupcode');
                                        if (groupcodeCur !== undefined && groupcodeCur !== null && groupcodeCur.toString().trim().length > 0) {
                                            if (groupcodeCur.toUpperCase().trim() === groupcode.toUpperCase().trim()) {
                                                $(tr).addClass('selected');
                                                return;
                                            }
                                        }
                                    }

                                });

                            }
                            GetDetailData(groupcode);
                            ClosePopup();

                        }
                        else {
                            if (data.Detail != null) {
                                _showErrorMsg123("Lỗi!", data.Detail);
                            }
                        }
                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        debugger;
                        if (textStatus == 'Unauthorized') {
                            alert('custom message. Error: ' + errorThrown);
                        } else {
                            //alert('custom message. Error2: ' + errorThrown);
                        }
                    }
                });
            }
            else {
                alert('Mã nhóm người dùng trống!');
                return false;
            }
        }
        else {
            alert('Lưới danh sách người dùng trống!');
            return false;
        }
    };
    this.getDetailData = function (groupcode) {
        var _ajaxSettings = this.ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        $.ajax({
            url: _ajaxSettings.Url,
            data: {
                __RequestVerificationToken: token
                , groupcode: groupcode
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
            traditional: true,
            success: function (data) {
                if (data.Success) {
                    $("#divDtl").html(data.Html);
                } else {
                    _showErrorMsg123("Lỗi!", data.Detail);
                }

            }
        });
    };
    this.getSysObject = function (groupcode) {
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var _ajaxSettings = this.ajaxSettings;
        $.ajax({
            url: _ajaxSettings.Url,
            data: {
                __RequestVerificationToken: token
                , groupcode: groupcode
            },
            type: _ajaxSettings.Type,
            dataType: _ajaxSettings.DataType,
            traditional: true,
            success: function (data) {
                if (data.Success) {
                    $("#ShowPopup").addClass('show');
                    $("#ShowPopup").css('display', 'block');
                    $('#ShowPopup').modal({
                        backdrop: false,
                        keyboard: true,
                    });
                    $("#ShowPopup").html(data.Html);
                } else {
                    _showErrorMsg123("Lỗi!", data.Detail);
                }

            }
        });
    };
    this.saveObjectInGroup = function () {
        var _ajaxSettings = this.ajaxSettings;
        var rows = $("tbody#table-tbodyID-popup tr.trdata").length;
        var token = $('#manageFormPopupOfSysGroup input[name=__RequestVerificationToken]').val();
        if (rows > 0) {
            var groupcode = $('#GroupCodePU').val();
            if (!commonUtils.isNullOrEmpty(groupcode)) {
                var tem = new Object();
                var sysGroup = new Object();
                sysGroup.GroupCode = groupcode;
                tem.Sys_Group = sysGroup;

                var ListSysObject = [];
                var trArr = $('tbody#table-tbodyID-popup tr.trdata').has('input[name="ckbpopup"]:checked');
                if (trArr !== undefined && trArr !== null && trArr.length > 0) {

                    for (var i = 0; i < trArr.length; i++) {
                        var trCur = trArr[i];
                        if (trCur != null && trCur != undefined) {
                            var idx = $(trCur).attr('idx');
                            var objectCodeCur = $(trCur).find('input[type="hidden"][name="ListSysObject[' + idx + '].ObjectCode"]').val();
                            var so_ObjectTypeCur = $(trCur).find('input[type="hidden"][name="ListSysObject[' + idx + '].so_ObjectType"]').val();
                            var temSysObject = {};
                            temSysObject.ObjectCode = objectCodeCur;
                            temSysObject.GroupCode = groupcode;
                            temSysObject.so_ObjectType = so_ObjectTypeCur;
                            ListSysObject.push(temSysObject);
                        }
                    }
                }
                tem.Lst_Sys_Access = ListSysObject;

                var modelCur = JSON.stringify(tem);
                $.ajax({
                    type: _ajaxSettings.Type,
                    url: _ajaxSettings.Url,
                    data: {
                        __RequestVerificationToken: token
                        , model: modelCur
                    },
                    traditional: true,
                    //contentType: "application/json; charset=utf-8",
                    dataType: _ajaxSettings.DataType,
                    success: function (data) {
                        if (data.Success) {
                            alert(data.Title);
                            ClosePopup();
                        }
                        else {
                            if (data.Detail != null) {
                                _showErrorMsg123("Lỗi!", data.Detail);
                            }
                        }
                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        if (textStatus == 'Unauthorized') {
                            alert('custom message. Error: ' + errorThrown);
                        } else {
                            //alert('custom message. Error2: ' + errorThrown);
                        }
                    }
                });
            }
            else {
                alert('Mã nhóm người dùng trống!');
                return false;
            }
        }
        else {
            alert('Lưới danh sách module trống!');
            return false;
        }
    };
    this.loadDistrict = function () {
        var _ajaxSettings = this.ajaxSettings;
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        var provincecode = commonUtils.returnValueText('#ProvinceCode');
        var dataInput = {
            __RequestVerificationToken: token,
            provincecode: provincecode
        };
        $.ajax({
            type: _ajaxSettings.Type,
            data: dataInput,
            url: _ajaxSettings.Url,
            dataType: _ajaxSettings.DataType,
            beforeSend: function () {
            }
        }).done(function (objResult) {
            doneFunctionLoadData(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            alwaysFunction();
        });
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
    function doneFunctionLoadData(objResult) {
        if (objResult.Success) {
            $('#DistrictCode').html(objResult.Html);
        } else {
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