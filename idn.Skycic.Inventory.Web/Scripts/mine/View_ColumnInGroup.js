function View_ColumnInGroup() {
    this.ActionType = '';
    this.checkForm = function () {
        if (!CheckIsNullOrEmpty('#role', 'has-error-fix', 'Chưa chọn nhóm!')) {
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
        var role = $('#role').val();
        if (!IsNullOrEmpty(role)) {
            var token = $('#manageForm input[name=__RequestVerificationToken]').val();

            var tem = new Object();
            var objView_ColumnInGroup = new Object();
            objView_ColumnInGroup.GroupViewCode = role;
            var ListView_ColumnInGroup = [];

            $("tbody#tbodyleft tr.trdata").each(function () {
                var trCur = $(this);
                var idx = trCur.attr('idx');
                var columnviewcode = trCur.find('input[type="hidden"][name="ListColumnViewCode[' + idx + '].ColumnViewCode"]').val();
                //var columnviewname = trCur.find('input[type="hidden"][name="ListColumnViewCode[' + idx + '].ColumnViewName"]').val();
                var temView_ColumnInGroup = {};
                temView_ColumnInGroup.GroupViewCode = role.trim();
                if (!IsNullOrEmpty(columnviewcode)) {
                    temView_ColumnInGroup.ColumnViewCode = columnviewcode.trim();
                }
                else {
                    temView_ColumnInGroup.ColumnViewCode = '';
                }
                ListView_ColumnInGroup.push(temView_ColumnInGroup);
            });
            tem.Lst_View_ColumnInGroup = ListView_ColumnInGroup;
            tem.View_ColumnInGroup = objView_ColumnInGroup;

            var modelCur = JSON.stringify(tem);
            var data = {
                __RequestVerificationToken: token,
                model: modelCur,
            };
            return data;
        } else {
            $('#role').focus();
            alert('Mã Role trống!');
            return false;
        }
    };
    this.saveData = function () {
        var _ajaxSettings = this.ajaxSettings;
        var dataInput = this.getData();
        if (dataInput === false) {
            return false;
        } else {

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
        }

    };
    this.addAllLeft = function () {
        var rows = $("tbody#tbodyright tr.trdata").length;
        if (rows > 0) {
            var strHtml = '';
            $("tbody#tbodyright tr.trdata").each(function () {
                var trCur = $(this);
                var idx = trCur.attr('idx');
                var columnviewcode = trCur.find('input[type="hidden"][name="ListColumnNoViewCode[' + idx + '].ColumnViewCode"]').val();
                var columnviewname = trCur.find('input[type="hidden"][name="ListColumnNoViewCode[' + idx + '].ColumnViewName"]').val();

                strHtml += getHtmlFromTemplate($('#rowtemplateLeft'), {
                    columnviewcode: columnviewcode
                    , columnviewname: columnviewname
                    , idx: 999999
                });

                var oldItem = $('#tbodyleft').find('tr[columnviewcode="' + columnviewcode + '"]');
                if (oldItem != undefined && oldItem.length > 0) {
                    var strMess = "Mã cột: " + columnviewcode + " đã tồn tại bên bảng 'Trường thông tin không hiển thị'!";
                    alert(strMess);
                    return false;
                }

            });
            $('#tbodyleft').append(strHtml);

            updateTableTrNotShowIdx($('#tbodyleft tr'), true);
            $('#tbodyright').html('');

        }
        return true;
    };
    this.addAllRight = function () {
        var rows = $("tbody#tbodyleft tr.trdata").length;
        if (rows > 0) {
            var strHtml = '';
            $("tbody#tbodyleft tr.trdata").each(function () {
                var trCur = $(this);
                var idx = trCur.attr('idx');
                var columnviewcode = trCur.find('input[type="hidden"][name="ListColumnViewCode[' + idx + '].ColumnViewCode"]').val();
                var columnviewname = trCur.find('input[type="hidden"][name="ListColumnViewCode[' + idx + '].ColumnViewName"]').val();

                strHtml += getHtmlFromTemplate($('#rowtemplateRight'), {
                    columnviewcode: columnviewcode
                    , columnviewname: columnviewname
                    , idx: 999999
                });

                var oldItem = $('#tbodyright').find('tr[columnviewcode="' + columnviewcode + '"]');
                if (oldItem != undefined && oldItem.length > 0) {
                    var strMess = "Mã cột: " + columnviewcode + " đã tồn tại bên bảng 'Trường thông tin không hiển thị'!";
                    alert(strMess);
                    return false;
                }

            });
            $('#tbodyright').append(strHtml);

            updateTableTrNotShowIdx($('#tbodyright tr'), true);
            $('#tbodyleft').html('');

        }
        return true;
    };
    this.addLeft = function () {
        var rows = $("tbody#tbodyright tr.trdata").length;
        if (rows > 0) {
            $("tbody#tbodyright tr.trdata").each(function () {
                var trCur = $(this);
                if (trCur.hasClass('selectedd')) {
                    var idx = trCur.attr('idx');

                    var columnviewcode = trCur.find('input[type="hidden"][name="ListColumnNoViewCode[' + idx + '].ColumnViewCode"]').val();
                    var columnviewname = trCur.find('input[type="hidden"][name="ListColumnNoViewCode[' + idx + '].ColumnViewName"]').val();

                    var strHtml = getHtmlFromTemplate($('#rowtemplateLeft'), {
                        columnviewcode: columnviewcode
                        , columnviewname: columnviewname
                        , idx: 999999
                    });

                    var oldItem = $('#tbodyleft').find('tr[columnviewcode="' + columnviewcode + '"]');
                    if (oldItem != undefined && oldItem.length > 0) {
                        var strMess = "Mã cột: " + columnviewcode + " đã tồn tại bên bảng 'Trường thông tin không hiển thị'!";
                        alert(strMess);
                        return false;
                    }
                    else {
                        $('#tbodyleft').append(strHtml);
                        trCur.remove();
                        updateTableTrNotShowIdx($('#tbodyleft tr'), true);
                        updateTableTrNotShowIdx($('#tbodyright tr'), true);

                    }

                }
            });
        }
        return true;
    };
    this.addRight = function () {
        var rows = $("tbody#tbodyleft tr.trdata").length;
        if (rows > 0) {
            $("tbody#tbodyleft tr.trdata").each(function () {
                var trCur = $(this);
                if (trCur.hasClass('selectedd')) {
                    var idx = trCur.attr('idx');
                    var columnviewcode = trCur.find('input[type="hidden"][name="ListColumnViewCode[' + idx + '].ColumnViewCode"]').val();
                    var columnviewname = trCur.find('input[type="hidden"][name="ListColumnViewCode[' + idx + '].ColumnViewName"]').val();

                    var strHtml = getHtmlFromTemplate($('#rowtemplateRight'), {
                        columnviewcode: columnviewcode
                        , columnviewname: columnviewname
                        , idx: 999999
                    });

                    var oldItem = $('#tbodyright').find('tr[columnviewcode="' + columnviewcode + '"]');
                    if (oldItem != undefined && oldItem.length > 0) {
                        var strMess = "Mã cột: " + columnviewcode + " đã tồn tại bên bảng 'Trường thông tin không hiển thị'!";
                        alert(strMess);
                        return false;
                    }
                    else {
                        $('#tbodyright').append(strHtml);
                        trCur.remove();
                        updateTableTrNotShowIdx($('#tbodyleft tr'), true);
                        updateTableTrNotShowIdx($('#tbodyright tr'), true);

                    }


                }
            });
        }
        return true;
    };
    this.selectedRowRight = function (thiz) {
        $('#tbodyright tr').removeClass('selectedd');
        $(thiz).addClass('selectedd');
        var rows = $("tbody#tbodyleft tr.trdata").length;
        if (rows > 0) {
            $('#tbodyleft tr').removeClass('selectedd');
        }
        if ($('#AddRight').length) {
            if (!$('#AddRight').hasClass('disabled')) {
                $('#AddRight').addClass('disabled')
            }
        }
        if ($('#AddLeft').length) {
            if ($('#AddLeft').hasClass('disabled')) {
                $('#AddLeft').removeClass('disabled')
            }
        }
    };
    this.selectedRowLeft = function (thiz) {
        $('#tbodyleft tr').removeClass('selectedd');
        $(thiz).addClass('selectedd');
        var rows = $("tbody#tbodyright tr.trdata").length;
        if (rows > 0) {
            $('#tbodyright tr').removeClass('selectedd');
        }
        if ($('#AddRight').length) {
            if ($('#AddRight').hasClass('disabled')) {
                $('#AddRight').removeClass('disabled')
            }
        }
        if ($('#AddLeft').length) {
            if (!$('#AddLeft').hasClass('disabled')) {
                $('#AddLeft').addClass('disabled')
            }
        }
    };
    this.showButton = function () {
        var rowsLeft = $("tbody#tbodyleft tr.trdata").length;
        if (rowsLeft > 0) {
            if ($('#AddAllRight').length) {
                if ($('#AddAllRight').hasClass('disabled')) {
                    $('#AddAllRight').removeClass('disabled')
                }
            }
        }
        else {
            if (!$('#AddRight').hasClass('disabled')) {
                $('#AddRight').addClass('disabled')
            }
            if ($('#AddAllRight').length) {
                if (!$('#AddAllRight').hasClass('disabled')) {
                    $('#AddAllRight').addClass('disabled')
                }
            }
        }

        var rowsRight = $("tbody#tbodyright tr.trdata").length;
        if (rowsRight > 0) {
            if ($('#AddAllLeft').length) {
                if ($('#AddAllLeft').hasClass('disabled')) {
                    $('#AddAllLeft').removeClass('disabled')
                }
            }
        }
        else {
            if ($('#AddLeft').length) {
                if (!$('#AddLeft').hasClass('disabled')) {
                    $('#AddLeft').addClass('disabled')
                }
            }
            if ($('#AddAllLeft').length) {
                if (!$('#AddAllLeft').hasClass('disabled')) {
                    $('#AddAllLeft').addClass('disabled')
                }
            }
        }
    };
    function doneFunction(objResult) {
        if (objResult.Success) {
            if (!IsNullOrEmpty(objResult.Messages)) {
                alert(objResult.Messages[0]);
            }
            if (!IsNullOrEmpty(objResult.RedirectUrl)) {
                window.location.href = objResult.RedirectUrl;
            }
        }
        else {
            if (!IsNullOrEmpty(objResult.Detail)) {
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