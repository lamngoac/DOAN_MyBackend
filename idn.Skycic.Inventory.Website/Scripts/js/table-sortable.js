var addEventSearch = function (thiz) {
    var tableId = $(thiz).attr('id');
    var textSearchId = tableId + '-search';
    var tableId_Data = tableId + '-tbody';
    if ($('#' + textSearchId).length) {
        var $table = $('#' + tableId_Data);
        var $textbox = $('#' + textSearchId);
        $('#' + textSearchId).keyup(function () {
            searchData($textbox, $table);
        });
    }
};

var addEvenToggle = function (thiz) {
    //Xóa phần tử trong ul#togglecolumn
    $('#togglecolumn').html('');
    $(thiz).find('th').each(function () {
        //Đầu vào: Bảng #dynamic-table-thead có th có các thuộc tính fieldId, fieldName, fieldActive
        //Đầu ra: Tùy chọn ẩn hiện các cột trong bảng #dynamic-table-thead

        //B1: lấy các thuộc tính fieldId, fieldName, fieldActive
        var fieldId = $(this).attr('fieldId');
        var title = $(this).attr('fieldName');
        if (title !== undefined && title !== null && title.toString().trim().length > 0) {
            title = title.toString().trim();
        } else {
            title = '';
        }
        
        var fieldActive = $(this).attr('fieldActive'); //0 : hiển thị, 1 : không hiển thị

        //B2: Sinh popup tùy chỉnh các cột trong bảng #dynamic-table-thead
        var $input = $('<input>');
        $input.attr('type', 'checkbox');
        $input.attr('name', fieldId);
        $input.attr('onclick', "togglecolumn('" + fieldId + "')"); //Tạo sự kiện ẩn hiện nếu click vào ô input
        if (fieldActive === '0') { //Nếu trạng thái bằng 0 cho hiện cột và checked ô input tương ứng
            $input.attr('checked', 'checked');
            $('[fieldId="' + fieldId + '"]').show();
        }
        else { //Ngược lại ẩn cột và không checked ô input tương ứng
            $('[fieldId="' + fieldId + '"]').hide();
        }
        var $spaninput = $('<span>');
        $spaninput.append($input);
        $spaninput.css('margin-right', '10px');

        var $spantext = $('<span>');
        $spantext.text(title);

        var $li = $('<li>');
        $li.css('line-height', '19px');
        $li.css('margin-bottom', '18px');
        $li.append($spaninput);
        $li.append($spantext);
        
        $('#togglecolumn').append($li);
    });
};



//Sự kiện ẩn hiện cột có fieldId tương ứng
function togglecolumn(fieldId) {
    // Lưu cột được chọn vào cookies
    saveSelectedToCookie(fieldId);
    $('[fieldId="' + fieldId + '"]').toggle();
    var fieldActive = $('[fieldId="' + fieldId + '"]').attr('fieldActive');
    if (fieldActive === '0') {
        $('[fieldId="' + fieldId + '"]').attr('fieldActive', '1');
    }
    else {
        $('[fieldId="' + fieldId + '"]').attr('fieldActive', '0');
    }
}

var searchData = function (thiz, table) {
    if (thiz !== undefined && thiz !== null && table !== undefined && table !== null) {
        var datafilter = $(thiz).val();
        if (datafilter !== undefined && datafilter !== null && datafilter.toString().trim()) {
            datafilter = datafilter.toUpperCase().trim();
        }
        var tableid = $(table).attr('id');
        var rex = new RegExp($(thiz).val(), 'i');
        $('table#' + tableid + ' tbody tr').hide();
        $('table#' + tableid + ' tbody tr').filter(function () {
            var dataOfRow = $(this).text();
            if (dataOfRow !== undefined && dataOfRow !== null && dataOfRow.toString().trim().length > 0) {
                dataOfRow = dataOfRow.toUpperCase().trim();
            }
            if (dataOfRow.toUpperCase().indexOf(datafilter) > -1) {
                return true;
            }
            else {
                return false;
            }
        }).show();
    }
}

var addEventClick = function (thiz, _columns) {
    var tableId = $(thiz).attr('id');
    $('table' + '#' + tableId + ' tr.trthead').attr('filterroot', '');
    $('table' + '#' + tableId + ' tr.trthead').attr('bfilter', '0');
    $('table' + '#' + tableId + ' tr.trthead th').each(function () {
        
        var th = $(this);
        if (th !== undefined && th !== null) {
            var title = $(th).attr('fieldname');
            if (title !== undefined && title !== null && title.toString().trim().length > 0) {
                title = title.toString().trim();
            } else {
                title = '';
            }
            
            var fieldId = $(th).attr('fieldId');
            if (fieldId !== undefined && fieldId !== null && fieldId.toString().trim().length > 0) {
                fieldId = fieldId.toString().toLocaleUpperCase().trim();
                if (_columns !== undefined && _columns !== null) {
                    for (var key in _columns) {
                        key = key.toString().toLocaleUpperCase().trim();
                        if (fieldId === key) {
                            $(th).html('');
                            var $divMenu = $('<div>');
                            $divMenu.attr('class', 'divRoot');
                            var $divdropdown = $('<div>');
                            $divdropdown.attr('class', 'classdropdown');
                            $divdropdown.attr('data-toggle', 'dropdown');
                            $divdropdown.text(title);
                            $divMenu.append($divdropdown);
                            $(th).append($divMenu);
                            $(th).attr('thfilter', false);
                            $divdropdown.click(function () {
                                addDropdown($divMenu, _columns);
                            });
                        }
                    }
                }
            }
        }
    });
};

var addDropdown = function (thiz, _columns) {
    if (thiz !== undefined && thiz !== null) {
        var th = $(thiz).parent();
        var trthead = $(th).parent();
        var trfilterroot = $(trthead).attr('filterroot');
        var tableId = $(trthead).parent().parent().attr('id');
        var tableId_Data = tableId + '-tbody';
        var listData = [];
        var fieldId = $(th).attr('fieldid');
        var sortbycolumn = '';
        var sortdirection = '';
        if (fieldId !== undefined && fieldId !== null && fieldId.toString().trim().length > 0) {
            fieldId = fieldId.toString().toLocaleUpperCase().trim();
            sortbycolumn = $('table#' + tableId_Data).attr('sortbycolumn');
            sortdirection = $('table#' + tableId_Data).attr('sortdirection');
            if (typeof sortbycolumn !== typeof undefined && sortbycolumn !== false) {
                if (sortbycolumn !== undefined && sortbycolumn !== null && sortbycolumn.toString().trim().length > 0) {
                    sortbycolumn = sortbycolumn.toString().trim();
                } else {
                    sortbycolumn = '';
                }
            } else {
                sortbycolumn = '';
            }
            if (typeof sortdirection !== typeof undefined && sortdirection !== false) {
                if (sortdirection !== undefined && sortdirection !== null && sortdirection.toString().trim().length > 0) {
                    sortdirection = sortdirection.toString().trim();
                } else {
                    sortdirection = '';
                }
            } else {
                sortdirection = '';
            }
            var bfilter = $(trthead).attr('bfilter');
            if (bfilter === '0' || fieldId === trfilterroot) {
                var ulshowdata = $(th).find('ul.ulshowdata');
                if (ulshowdata !== undefined && ulshowdata !== null) {
                    var liCount = $('th[fieldid="' + fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]');
                    if (liCount !== undefined && liCount !== null) { //cbitem
                        for (var i = 0; i < liCount.length; i++) {
                            var liCur = liCount[i];
                            if (liCur !== undefined && liCur !== null) {
                                var inputcb = $(liCur).find('input.cbitem[type=checkbox]');
                                if (inputcb !== undefined && inputcb !== null) {
                                    var ischecked = $(inputcb).prop('checked');
                                    var desc = $(inputcb).attr('desc');
                                    if (desc !== undefined && desc !== null && desc.toString().trim().length > 0) {
                                        desc = desc.toString().trim();
                                    } else {
                                        desc = '';
                                    }
                                    var obj = {
                                        Data: desc,
                                        IsChecked: ischecked
                                    };
                                    listData.push(obj);
                                }
                            }
                        }
                    }

                }
            }
            if ($('table#' + tableId_Data).hasClass('i-table-filter')) {
                // đã filter
                $('table' + '#' + tableId_Data + ' tr.trdata').each(function () {
                    var trCur = $(this);
                    if (trCur !== undefined && trCur !== null) {
                        var tdByFieldId = $(trCur).find('td[fieldid = "' + fieldId + '"]');
                        if (tdByFieldId !== undefined && tdByFieldId !== null) {
                            if ($(trCur).hasClass('i-filter')) {
                                if (!$(trCur).hasClass('display-none')) {
                                    var _value = $(tdByFieldId).text();
                                    if (_value !== undefined && _value.toString().trim().length > 0) {
                                        _value = _value.toString().trim();
                                        var dataExists = false;
                                        for (var j = 0; j < listData.length; j++) {
                                            var obj = listData[j];
                                            var _data = obj.Data;
                                            if (_value === _data) {
                                                dataExists = true;
                                                listData[j].IsChecked = true;
                                                break;
                                            }
                                        }
                                        if (!dataExists) {
                                            var obj = {};
                                            obj.Data = _value;
                                            obj.IsChecked = true;
                                            listData.push(obj);
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            } else {
                // chưa filter
                $('table' + '#' + tableId_Data + ' tr.trdata').each(function () {
                    var trCur = $(this);
                    if (trCur !== undefined && trCur !== null) {
                        var tdByFieldId = $(trCur).find('td[fieldid = "' + fieldId + '"]');
                        if (tdByFieldId !== undefined && tdByFieldId !== null) {
                            var _value = $(tdByFieldId).text();
                            if (_value !== undefined && _value.toString().trim().length > 0) {
                                _value = _value.toString().trim();
                                var dataExists = false;
                                for (var j = 0; j < listData.length; j++) {
                                    var obj = listData[j];
                                    var _data = obj.Data;
                                    if (_value === _data) {
                                        dataExists = true;
                                        break;
                                    }
                                }
                                if (!dataExists) {
                                    var obj = {};
                                    obj.Data = _value;
                                    if (!$(trCur).hasClass('display-none')) {
                                        obj.IsChecked = true;
                                    } else {
                                        obj.IsChecked = false;
                                    }
                                    listData.push(obj);
                                }
                            }
                        }
                    }
                });
            }
        }

        if (listData !== undefined && listData !== null && listData.length > 0) {
            var ulshowdata = $(th).find('ul.ulshowdata');
            if (ulshowdata === undefined || ulshowdata === null || ulshowdata.length === 0) {
                var $div = $('<div>');
                $div.click(function (e) {
                    e.stopPropagation();
                });
                $div.attr('class', 'dropdown-menu');
                $div.attr('role', 'menu');
                var $ul = $('<ul>');
                $ul.attr('class', 'ulshowdata');
                var $liSortAsc = $('<li>');
                $liSortAsc.append('<i class="fas fa-sort-amount-up"></i>');
                $liSortAsc.text(' Sort A to Z');
                $liSortAsc.attr('class', 'ascsort');
                $liSortAsc.click(function (e) {
                    $('.classdropdown').attr('name', '');
                    $(thiz).find('.classdropdown').attr('name', 'icon-asc');
                    sortDataAsc($liSortDesc);
                });
                $ul.append($liSortAsc);
                var $liSortDesc = $('<li>');
                $liSortDesc.text(' Sort Z to A');
                $liSortDesc.attr('class', 'descsort');
                $liSortDesc.click(function (e) {
                    $('.classdropdown').attr('name', '');
                    $(thiz).find('.classdropdown').attr('name', 'icon-desc');
                    sortDataDesc($liSortDesc);
                });

                //if (sortbycolumn === fieldId) {
                //    if (sortdirection === sortDirection.ASC) {
                //        $liSortAsc.attr('class', 'ascsort disabled-fix');
                //    }
                //    else if (sortdirection === sortDirection.DESC) {
                //        $liSortDesc.attr('class', 'descsort disabled-fix');
                //    }
                //}


                //$ul.append($liSortAsc);
                $ul.append($liSortDesc);
                if (listData !== undefined && listData !== null && listData.length > 0) {
                    var $liFilterTitle = $('<li>');
                    $liFilterTitle.attr('class', 'filtertitle');
                    $liFilterTitle.text(' Filter');
                    $ul.append($liFilterTitle);

                    var $liCheckAll = $('<li>');
                    $liCheckAll.attr('class', 'licheckall');
                    var $divInputCheckAll = $('<span>');
                    var $inputCheckAll = $('<input>');
                    $inputCheckAll.attr('type', 'checkbox');
                    $inputCheckAll.attr('class', 'cball');
                    $inputCheckAll.prop('checked', true);
                    $inputCheckAll.click(function (e) {
                        var inputcheckbox = 'th[fieldid="' + fieldId + '"] ul.ulc input.cbitem';
                        checkAll($inputCheckAll, inputcheckbox);
                    });
                    $divInputCheckAll.append($inputCheckAll);
                    var $divInputCheckAllTitle = $('<span>');
                    $divInputCheckAllTitle.text('Tất cả');
                    $liCheckAll.append($divInputCheckAll);
                    $liCheckAll.append($divInputCheckAllTitle);

                    $ul.append($liCheckAll);
                    var $liFilter = $('<li>');
                    $liFilter.attr('class', 'listulc');
                    var $ulFilterData = $('<ul>');
                    $ulFilterData.attr('class', 'ulc');
                    for (var i = 0; i < listData.length; i++) {
                        var obj = listData[i];
                        var _data = obj.Data;
                        if (_data !== undefined && _data !== null && _data.toString().trim().length > 0) {
                            _data = _data.toString().trim();
                            var $liData = $('<li>');
                            $liData.attr('idx', i);
                            $liData.attr('desc', _data);
                            $liData.attr('class', 'liitem');
                            var $divInputCheck = $('<span>');
                            var $inputCheck = $('<input>');
                            $inputCheck.attr('type', 'checkbox');
                            $inputCheck.attr('class', 'cbitem');
                            $inputCheck.attr('desc', _data);
                            $inputCheck.prop('checked', obj.IsChecked);
                            $inputCheck.click(function (e) {
                                var inputcheckall = 'th[fieldid="' + fieldId + '"] ul.ulshowdata input.cball';
                                var inputcheckbox = 'th[fieldid="' + fieldId + '"] ul.ulc li.liitem';
                                checkBox($inputCheck, inputcheckall, inputcheckbox);
                            });
                            $divInputCheck.append($inputCheck);
                            var $divInputCheckTitle = $('<span>');
                            $divInputCheckTitle.text(' ' + _data);
                            $liData.append($divInputCheck);
                            $liData.append($divInputCheckTitle);
                            $ulFilterData.append($liData);
                        }
                    }
                    $liFilter.append($ulFilterData);
                    $ul.append($liFilter);
                }
                var $liButton = $('<li>');
                $liButton.attr('class', 'li-btn-filter-ok');
                var $a = $('<a>');
                $a.attr('href', 'javascript:;');
                $a.attr('class', 'btn btn-nc-button');
                $a.css('float', 'right');
                $a.click(function (e) {
                    $('.classdropdown').attr('name', '');
                    $(thiz).find('.classdropdown').attr('name', 'icon-filter');
                    filterData($a, _columns);
                });
                $a.text('OK');
                $liButton.append($a);
                // add button OK hay cancel ở đây
                $ul.append($liButton);
                $div.append($ul);
                $(thiz).append($div);
            } else {
                $(ulshowdata).empty();
                var $ul = ulshowdata;
                $ul.attr('class', 'ulshowdata');
                var $liSortAsc = $('<li>');
                $liSortAsc.text(' Sort A to Z');
                $liSortAsc.attr('class', 'ascsort');
                $liSortAsc.click(function (e) {
                    $('.classdropdown').attr('name', '');
                    $(thiz).find('.classdropdown').attr('name', 'icon-asc');
                    sortDataAsc($liSortDesc);
                });
                //$ul.append($liSortAsc);
                var $liSortDesc = $('<li>');
                $liSortDesc.text(' Sort Z to A');
                $liSortDesc.attr('class', 'descsort');
                $liSortDesc.click(function (e) {
                    $('.classdropdown').attr('name', '');
                    $(thiz).find('.classdropdown').attr('name', 'icon-desc');
                    sortDataDesc($liSortDesc);
                });

                if (sortbycolumn === fieldId) {
                    if (sortdirection === sortDirection.ASC) {
                        $liSortAsc.attr('class', 'ascsort disabled-fix');
                    }
                    else if (sortdirection === sortDirection.DESC) {
                        $liSortDesc.attr('class', 'descsort disabled-fix');
                    }
                }
                $ul.append($liSortAsc);
                $ul.append($liSortDesc);
                if (listData !== undefined && listData !== null && listData.length > 0) {
                    var $liFilterTitle = $('<li>');
                    $liFilterTitle.attr('class', 'filtertitle');
                    $liFilterTitle.text(' Filter');
                    $ul.append($liFilterTitle);

                    var $liCheckAll = $('<li>');
                    $liCheckAll.attr('class', 'licheckall');
                    var $divInputCheckAll = $('<span>');
                    var $inputCheckAll = $('<input>');
                    $inputCheckAll.attr('type', 'checkbox');
                    $inputCheckAll.attr('class', 'cball');
                    $inputCheckAll.prop('checked', true);
                    $inputCheckAll.click(function (e) {
                        var inputcheckbox = 'th[fieldid="' + fieldId + '"] ul.ulc input.cbitem';
                        checkAll($inputCheckAll, inputcheckbox);
                    });
                    $divInputCheckAll.append($inputCheckAll);
                    var $divInputCheckAllTitle = $('<span>');
                    $divInputCheckAllTitle.text('Tất cả');
                    $liCheckAll.append($divInputCheckAll);
                    $liCheckAll.append($divInputCheckAllTitle);
                    $ul.append($liCheckAll);
                    var $liFilter = $('<li>');
                    $liFilter.attr('class', 'listulc');
                    var $ulFilterData = $('<ul>');
                    $ulFilterData.attr('class', 'ulc');
                    for (var i = 0; i < listData.length; i++) {
                        var obj = listData[i];
                        var _data = obj.Data;
                        if (_data !== undefined && _data !== null && _data.toString().trim().length > 0) {
                            _data = _data.toString().trim();
                            var $liData = $('<li>');
                            $liData.attr('idx', i);
                            $liData.attr('desc', _data);
                            $liData.attr('class', 'liitem');
                            var $divInputCheck = $('<span>');
                            var $inputCheck = $('<input>');
                            $inputCheck.attr('type', 'checkbox');
                            $inputCheck.attr('class', 'cbitem');
                            $inputCheck.attr('desc', _data);
                            $inputCheck.prop('checked', obj.IsChecked);
                            $inputCheck.click(function (e) {
                                var inputcheckall = 'th[fieldid="' + fieldId + '"] ul.ulshowdata input.cball';
                                var inputcheckbox = 'th[fieldid="' + fieldId + '"] ul.ulc li.liitem';
                                checkBox($inputCheck, inputcheckall, inputcheckbox);
                            });
                            $divInputCheck.append($inputCheck);
                            var $divInputCheckTitle = $('<span>');
                            $divInputCheckTitle.text(' ' + _data);
                            $liData.append($divInputCheck);
                            $liData.append($divInputCheckTitle);
                            $ulFilterData.append($liData);
                        }
                    }
                    $liFilter.append($ulFilterData);
                    $ul.append($liFilter);
                }
                var $liButton = $('<li>');
                $liButton.attr('class', 'li-btn-filter-ok');
                var $a = $('<a>');
                $a.attr('class', 'btn btn-nc-button');
                $a.attr('href', 'javascript:;');
                $a.css('float', 'right');
                $a.click(function (e) {
                    $('.classdropdown').attr('name', '');
                    $(thiz).find('.classdropdown').attr('name', 'icon-filter');
                    filterData($a, _columns);
                });
                $a.text('OK');
                $liButton.append($a);
                // add button OK hay cancel ở đây
                $ul.append($liButton);
            }
        }
        //a.btn.btn-nc-button
        var iLiCountCheckBoxChecked = 0;
        var _ulshowdata = $(th).find('ul.ulshowdata');
        if (_ulshowdata !== undefined && _ulshowdata !== null) {
            var liCountCheckBox = $('th[fieldid="' + fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]');
            var liCountCheckBoxChecked = $('th[fieldid="' + fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]:checked');
            var liCheckAll = $('th[fieldid="' + fieldId + '"] ul.ulshowdata li.licheckall').find('input[type=checkbox][class=cball]');
            if (liCountCheckBoxChecked !== undefined && liCountCheckBoxChecked !== null) {
                iLiCountCheckBoxChecked = liCountCheckBoxChecked.length;
            }
            if (liCheckAll !== undefined && liCheckAll !== null) {
                if (liCountCheckBox !== undefined && liCountCheckBox !== null
                    && liCountCheckBox.length === iLiCountCheckBoxChecked) {
                    $(liCheckAll).prop('checked', true);
                } else {
                    $(liCheckAll).prop('checked', false);
                }
            }
        }
        var btnOK = $('th[fieldid="' + fieldId + '"] ul.ulshowdata li.li-btn-filter-ok').find('a.btn.btn-nc-button');
        if (btnOK !== undefined && btnOK !== null) {
            if (iLiCountCheckBoxChecked > 0) {
                if ($(btnOK).hasClass('display-none')) {
                    $(btnOK).removeClass('display-none');
                }
            } else {
                if (!$(btnOK).hasClass('display-none')) {
                    $(btnOK).addClass('display-none');
                }
            }
        }
    }
};

var checkAll = function (thiz, inputcheckbox) {
    var c_all = false;
    if ($(thiz).is(":checked")) {
        c_all = true;
    }
    $(inputcheckbox).prop("checked", c_all);
    var btnFilterOK = $(thiz).parent().parent().parent().find('a.btn-filter-ok');
    if (btnFilterOK !== undefined && btnFilterOK !== null) {
        if (c_all) {
            if ($(btnFilterOK).hasClass('disabled-fix')) {
                $(btnFilterOK).removeClass('disabled-fix');
            }
        } else {
            if (!$(btnFilterOK).hasClass('disabled-fix')) {
                $(btnFilterOK).addClass('disabled-fix');
            }
        }
    }

};

var checkBox = function (thiz, inputcheckall, inputcheckbox) {
    var c_all = false;
    var showbuttonfilter = false;
    $(inputcheckall).prop("checked", c_all);
    var liCount = $(inputcheckbox).length;
    if (liCount > 0) {
        var _liCount = $(inputcheckbox).has('input[type=checkbox]:checked');
        if (_liCount !== null) {
            showbuttonfilter = true;
            if (_liCount.length > 0) {
                if (_liCount.length === liCount) {
                    c_all = true;
                    $(inputcheckall).prop("checked", c_all);
                } else {
                    c_all = false;
                    $(inputcheckall).prop("checked", c_all);
                }
            } else {
                showbuttonfilter = false;
            }
        }
    }

    var btnFilterOK = $(inputcheckall).parent().parent().parent().find('a.btn-filter-ok');
    if (btnFilterOK !== undefined && btnFilterOK !== null) {
        if (showbuttonfilter) {
            if ($(btnFilterOK).hasClass('disabled-fix')) {
                $(btnFilterOK).removeClass('disabled-fix');
            }
        } else {
            if (!$(btnFilterOK).hasClass('disabled-fix')) {
                $(btnFilterOK).addClass('disabled-fix');
            }
        }
    }
};

var filterData = function (thiz, _columns) {
    var ulshowdata = $(thiz).parent().parent();
    var th = $(ulshowdata).parent().parent().parent();
    if (ulshowdata !== undefined && ulshowdata !== null && th !== undefined && th !== null) {
        var fieldId = $(th).attr('fieldid');

        if (fieldId !== undefined && fieldId !== null && fieldId.toString().trim().length > 0) {
            var flagFilter = false;
            // có thể check checkall để xác định
            var ulcfilter = $('th[fieldid="' + fieldId + '"]').find('ul.ulc');
            if (ulcfilter !== undefined && ulcfilter !== null) {
                var liCountCheckBox = $('th[fieldid="' + fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]');
                var liCountCheckBoxChecked = $('th[fieldid="' + fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]:checked');

                if (liCountCheckBox !== undefined && liCountCheckBox !== null && liCountCheckBoxChecked !== undefined && liCountCheckBoxChecked !== null) {
                    if (liCountCheckBox.length === liCountCheckBoxChecked.length) {
                        flagFilter = false;
                    } else {
                        flagFilter = true;

                    }
                }
            }
            $(th).attr('thfilter', flagFilter);

            var columns = jQuery.extend(true, {}, _columns);
            var trthead = $(th).parent();
            var filterroot = $(trthead).attr('filterroot');
            var isRoot = false;
            var tablefilter = '';
            if (typeof filterroot !== typeof undefined && filterroot !== false) {
                if (filterroot !== undefined && filterroot !== null && filterroot.toString().trim().length > 0) {
                    filterroot = filterroot.toString().trim();
                } else {
                    filterroot = fieldId; // set là filterroot
                    $(trthead).attr('filterroot', fieldId);
                }
            } else {
                filterroot = fieldId; // set là filterroot
                $(trthead).attr('filterroot', fieldId);
            }
            if (filterroot === fieldId) {
                isRoot = true;
                if (!flagFilter) {
                    $(trthead).attr('filterroot', '');
                }
            }
            if (isRoot) {
                // dùng để reset điều kiện filter sau này
                // 1: filterroot lần đầu tiên, reset lại điều kiện filter
                // 2: filterroot nhiều hơn 2 lần liên tiếp, reset lại điều kiện filter
                // 0: không reset lại điều kiện filter
                var bfilter = $(trthead).attr('bfilter');
                if (bfilter === undefined || bfilter === null) {
                    $(trthead).attr('bfilter', '1');
                } else {
                    if (bfilter === '0') {
                        $(trthead).attr('bfilter', '1');
                    }
                    else if (bfilter === '1') {
                        $(trthead).attr('bfilter', '2');
                    }
                }
            } else {
                $(trthead).attr('bfilter', '0');
            }

            // tập hợp dữ liệu filter
            if (columns !== undefined && columns !== null) {
                for (var key in columns) {
                    columns[key] = [];
                }
            }
            var tableId = $(th).parent().parent().parent().attr('id');
            $('table' + '#' + tableId + ' tr th').each(function () {
                var thCur = $(this);
                var thfilter = $(thCur).attr('thfilter');
                var _fieldId = $(thCur).attr('fieldid');
                if (_fieldId !== undefined && _fieldId !== null && _fieldId.toString().trim().length > 0) {
                    _fieldId = _fieldId.toString().toLocaleUpperCase().trim();
                    if (columns !== undefined && columns !== null) {
                        if (isRoot) {
                            columns[_fieldId] = [];
                            var ulc = $('th[fieldid="' + _fieldId + '"]').find('ul.ulc');
                            if (ulc !== undefined && ulc !== null) {
                                var liCount = $('th[fieldid="' + _fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]:checked');
                                if (liCount !== undefined && liCount !== null) {
                                    for (var i = 0; i < liCount.length; i++) {
                                        var liCur = liCount[i];
                                        if (liCur !== undefined && liCur !== null) {
                                            var desc = $(liCur).attr('desc');
                                            columns[_fieldId].push(desc);
                                        }
                                    }
                                }
                            }
                        } else {
                            if (thfilter !== undefined && thfilter !== null && thfilter.toString().toLocaleLowerCase().trim() === 'true') {
                                columns[_fieldId] = [];
                                var ulc = $('th[fieldid="' + _fieldId + '"]').find('ul.ulc');
                                if (ulc !== undefined && ulc !== null) {
                                    var liCount = $('th[fieldid="' + _fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]:checked');
                                    if (liCount !== undefined && liCount !== null) {
                                        for (var i = 0; i < liCount.length; i++) {
                                            var liCur = liCount[i];
                                            if (liCur !== undefined && liCur !== null) {
                                                var desc = $(liCur).attr('desc');
                                                columns[_fieldId].push(desc);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            });
            
            // filter table
            if (columns !== undefined && columns !== null) {
                var table = $(th).parent().parent().parent();
                if (table !== undefined && table !== null) {
                    var tableId = $(table).attr('id');
                    var tableId_Data = tableId + '-tbody';
                    if (tableId_Data !== undefined && tableId_Data !== null && tableId_Data.toString().trim().length > 0) {
                        var rows = $('table#' + tableId_Data + ' tbody tr.trdata').length;
                        if (rows > 0) {
                            if (!$('table#' + tableId_Data).hasClass('i-table-filter')) {
                                $('table#' + tableId_Data).addClass('i-table-filter');
                            }



                            if (isRoot) {
                                tablefilter = 'table#' + tableId_Data + ' tbody tr.trdata';
                                $(tablefilter).each(function () {
                                    var tr = $(this);
                                    var flagFilter = false;
                                    var listData = columns[fieldId];
                                    var tdByFieldId = $(tr).find('td[fieldid = "' + fieldId + '"]');
                                    if (tdByFieldId !== undefined && tdByFieldId !== null) {
                                        var data = $(tdByFieldId).text();
                                        if (data !== undefined &&
                                            data !== null &&
                                            data.toString().trim().length > 0) {
                                            data = data.toString().trim();
                                        } else {
                                            data = '';
                                        }
                                        if (listData !== undefined && listData !== null && listData.length > 0) {

                                            for (var j = 0; j < listData.length; j++) {
                                                var _data = listData[j].toString().trim();
                                                if (data === _data) {
                                                    flagFilter = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (!flagFilter) {
                                        if (!$(tr).hasClass('display-none')) {
                                            $(tr).addClass('display-none');
                                        }
                                    } else {
                                        if ($(tr).hasClass('display-none')) {
                                            $(tr).removeClass('display-none');
                                        }

                                        if (!$(tr).hasClass('i-filter')) {
                                            $(tr).addClass('i-filter');
                                        }
                                    }
                                });
                            }
                            else {
                                tablefilter = 'table#' + tableId_Data + ' tbody tr.i-filter';
                                $(tablefilter).each(function () {
                                    var tr = $(this);
                                    var showRow = true;
                                    for (var key in columns) {
                                        if (showRow) {
                                            key = key.toString().toLocaleUpperCase().trim();
                                            var listData = columns[key];
                                            var tdByFieldId = $(tr).find('td[fieldid = "' + key + '"]')
                                            if (tdByFieldId !== undefined && tdByFieldId !== null) {
                                                var data = $(tdByFieldId).text();
                                                if (data !== undefined &&
                                                    data !== null &&
                                                    data.toString().trim().length > 0) {
                                                    data = data.toString().trim();
                                                } else {
                                                    data = '';
                                                }
                                                if (listData !== undefined && listData !== null && listData.length > 0) {
                                                    var _flagFilter = false;
                                                    for (var j = 0; j < listData.length; j++) {
                                                        var _data = listData[j].toString().trim();
                                                        if (data === _data) {
                                                            _flagFilter = true;
                                                            break;
                                                        }
                                                    }
                                                    if (!_flagFilter) {
                                                        // không tìm thấy => thoát luôn ko tìm kiếm nữa
                                                        showRow = false;
                                                    }
                                                }
                                            }
                                        } //else {
                                        //    break;
                                        //}
                                    }

                                    if (!showRow) {
                                        // không tìm thấy data => ẩn row
                                        if (!$(tr).hasClass('display-none')) {
                                            $(tr).addClass('display-none');
                                        }
                                    } else {
                                        // tìm thấy => kiểm tra có class 'display-none'; nếu có class 'display-none' => remove
                                        if ($(tr).hasClass('display-none')) {
                                            $(tr).removeClass('display-none');
                                        }

                                        if (!$(tr).hasClass('i-filter')) {
                                            $(tr).addClass('i-filter');
                                        }
                                    }

                                });
                            }
                            if (isRoot && !flagFilter) {
                                if ($('table#' + tableId_Data).hasClass('i-table-filter')) {
                                    $('table#' + tableId_Data).removeClass('i-table-filter');
                                    $(trthead).attr('bfilter', '0');
                                    // reset lại điều kiện lọc
                                    for (var key in columns) {
                                        var _ulshowdata = $('th[fieldid="' + key + '"]').find('ul.ulshowdata');
                                        if (_ulshowdata !== undefined && _ulshowdata !== null) {
                                            $(_ulshowdata).empty();
                                        }
                                    }

                                }
                                $('table#' + tableId_Data + ' tbody tr.trdata').removeClass('i-filter');
                            }
                        }
                    }
                }
            }

        }
    }
    var divdropdownmenu = $(ulshowdata).parent();
    if (divdropdownmenu !== undefined && divdropdownmenu !== null) {
        if ($(divdropdownmenu).hasClass('show')) {
            $(divdropdownmenu).removeClass('show');
        }
        var divRoot = $(divdropdownmenu).parent();
        if (divRoot !== undefined && divRoot !== null) {
            if ($(divRoot).hasClass('show')) {
                $(divRoot).removeClass('show');
            }
            var divclassdropdown = $(divRoot).find('div.classdropdown');
            if (divclassdropdown !== undefined && divclassdropdown !== null) {
                $(divclassdropdown).attr('aria-expanded', false);
            }
        }
    }
};
var sortDirection = {
    ASC: 'ASC',
    DESC: 'DESC'
};
var sortDataAsc = function (thiz) {
    var ulshowdata = $(thiz).parent();
    var th = $(ulshowdata).parent().parent().parent();
    if (th !== undefined && th !== null) {
        var tableId = $(th).parent().parent().parent().attr('id');
        var tableId_Data = tableId + '-tbody';
        var fieldid = $(th).attr('fieldid');
        var sorttype = $(th).attr('sorttype');
        var sortfomart = $(th).attr('sortfomart');
        if (sorttype === 'D') {
            if (typeof sortbycolumn !== typeof undefined && sortbycolumn !== false) {
                if (sortfomart !== undefined && sortfomart !== null && sortfomart.toString().trim().length > 0) {
                    sortfomart = sortfomart.toString().trim();
                } else {
                    sortfomart = 'yyyy-MM-dd';
                }
            } else {
                sortfomart = 'yyyy-MM-dd';
            }
        } else {
            sortfomart = '';
        }
        if (fieldid !== undefined && fieldid !== null && fieldid.toString().trim().length > 0) {
            var isSort = false;
            var sortbycolumn = $('#' + tableId_Data).attr('sortbycolumn');
            if (typeof sortbycolumn !== typeof undefined && sortbycolumn !== false) {
                if (sortbycolumn !== undefined && sortbycolumn !== null && sortbycolumn.toString().trim().length > 0) {
                    sortbycolumn = sortbycolumn.toString().trim();
                    if (sortbycolumn === fieldid) {
                        // trường hợp đang sắp xếp theo fieldid
                        // => chỉ cần đảo ngược lại
                        isSort = true;
                    } else {
                        // chưa được sắp xếp hoặc đang sắp xếp theo column khác
                        // => tiến hành sắp xếp lại
                        isSort = false;
                        $('#' + tableId_Data).attr('sortbycolumn', fieldid);
                    }
                } else {
                    // chưa được sắp xếp hoặc đang sắp xếp theo column khác
                    // => tiến hành sắp xếp lại
                    isSort = false;
                    $('#' + tableId_Data).attr('sortbycolumn', fieldid);
                }
            } else {
                // chưa được sắp xếp hoặc đang sắp xếp theo column khác
                // => tiến hành sắp xếp lại
                isSort = false;
                $('#' + tableId_Data).attr('sortbycolumn', fieldid);
            }
            $('#' + tableId_Data).attr('sortdirection', sortDirection.ASC);
            // gọi hàm sắp xếp

            var objSort = {
                SortDirection: sortDirection.ASC,
                SortType: sorttype,
                SortFomart: sortfomart
            };
            sortData(tableId_Data, fieldid, objSort);
            var liasc = $('th[fieldid = "' + fieldid + '"] ul.ulshowdata').find('li.ascsort');
            if (liasc !== undefined && liasc !== null) {
                if (!$(liasc).hasClass('disabled-fix')) {
                    $(liasc).addClass('disabled-fix');
                }
            }
            var lidesc = $('th[fieldid = "' + fieldid + '"] ul.ulshowdata').find('li.descsort');
            if (lidesc !== undefined && lidesc !== null) {
                if ($(lidesc).hasClass('disabled-fix')) {
                    $(lidesc).removeClass('disabled-fix');
                }
            }
        }
    }
    var divdropdownmenu = $(ulshowdata).parent();
    if (divdropdownmenu !== undefined && divdropdownmenu !== null) {
        if ($(divdropdownmenu).hasClass('show')) {
            $(divdropdownmenu).removeClass('show');
        }
        var divRoot = $(divdropdownmenu).parent();
        if (divRoot !== undefined && divRoot !== null) {
            if ($(divRoot).hasClass('show')) {
                $(divRoot).removeClass('show');
            }
            var divclassdropdown = $(divRoot).find('div.classdropdown');
            if (divclassdropdown !== undefined && divclassdropdown !== null) {
                $(divclassdropdown).attr('aria-expanded', false);
            }
        }
    }
};

var sortDataDesc = function (thiz) {
    var ulshowdata = $(thiz).parent();
    var th = $(ulshowdata).parent().parent().parent();
    if (th !== undefined && th !== null) {
        var tableId = $(th).parent().parent().parent().attr('id');
        var tableId_Data = tableId + '-tbody';
        var fieldid = $(th).attr('fieldid');
        var sorttype = $(th).attr('sorttype');
        var sortfomart = $(th).attr('sortfomart');
        if (sorttype === 'D') {
            if (typeof sortbycolumn !== typeof undefined && sortbycolumn !== false) {
                if (sortfomart !== undefined && sortfomart !== null && sortfomart.toString().trim().length > 0) {
                    sortfomart = sortfomart.toString().trim();
                } else {
                    sortfomart = 'yyyy-MM-dd';
                }
            } else {
                sortfomart = 'yyyy-MM-dd';
            }
        } else {
            sortfomart = '';
        }
        if (fieldid !== undefined && fieldid !== null && fieldid.toString().trim().length > 0) {
            var isSort = false;
            var sortbycolumn = $('#' + tableId_Data).attr('sortbycolumn');
            if (typeof sortbycolumn !== typeof undefined && sortbycolumn !== false) {
                if (sortbycolumn !== undefined && sortbycolumn !== null && sortbycolumn.toString().trim().length > 0) {
                    sortbycolumn = sortbycolumn.toString().trim();
                    if (sortbycolumn === fieldid) {
                        // trường hợp đang sắp xếp theo fieldid
                        // => chỉ cần đảo ngược lại
                        isSort = true;
                    } else {
                        // chưa được sắp xếp hoặc đang sắp xếp theo column khác
                        // => tiến hành sắp xếp lại
                        isSort = false;
                        $('#' + tableId_Data).attr('sortbycolumn', fieldid);
                    }
                } else {
                    // chưa được sắp xếp hoặc đang sắp xếp theo column khác
                    // => tiến hành sắp xếp lại
                    isSort = false;
                    $('#' + tableId_Data).attr('sortbycolumn', fieldid);
                }
            } else {
                // chưa được sắp xếp hoặc đang sắp xếp theo column khác
                // => tiến hành sắp xếp lại
                isSort = false;
                $('#' + tableId_Data).attr('sortbycolumn', fieldid);
            }
            $('#' + tableId_Data).attr('sortdirection', sortDirection.DESC);
            // gọi hàm sắp xếp
            var objSort = {
                SortDirection: sortDirection.DESC,
                SortType: sorttype,
                SortFomart: sortfomart
            };
            sortData(tableId_Data, fieldid, objSort);
            var liasc = $('th[fieldid = "' + fieldid + '"] ul.ulshowdata').find('li.ascsort');
            if (liasc !== undefined && liasc !== null) {
                if ($(liasc).hasClass('disabled-fix')) {
                    $(liasc).removeClass('disabled-fix');
                }
            }
            var lidesc = $('th[fieldid = "' + fieldid + '"] ul.ulshowdata').find('li.descsort');
            if (lidesc !== undefined && lidesc !== null) {
                if (!$(lidesc).hasClass('disabled-fix')) {
                    $(lidesc).addClass('disabled-fix');
                }
            }
        }
    }
    var divdropdownmenu = $(ulshowdata).parent();
    if (divdropdownmenu !== undefined && divdropdownmenu !== null) {
        if ($(divdropdownmenu).hasClass('show')) {
            $(divdropdownmenu).removeClass('show');
        }
        var divRoot = $(divdropdownmenu).parent();
        if (divRoot !== undefined && divRoot !== null) {
            if ($(divRoot).hasClass('show')) {
                $(divRoot).removeClass('show');
            }
            var divclassdropdown = $(divRoot).find('div.classdropdown');
            if (divclassdropdown !== undefined && divclassdropdown !== null) {
                $(divclassdropdown).attr('aria-expanded', false);
            }
        }
    }
};

var sortData = function (tableid, sortbycolumn, objsort) {
    var sortdirection = sortDirection.ASC;
    var datatype = 'T';
    var dateformat = '';
    if (objsort !== undefined && objsort !== null) {
        if (objsort.SortDirection !== undefined &&
            objsort.SortDirection !== null &&
            objsort.SortDirection.toString().trim().length > 0) {
            sortdirection = objsort.SortDirection.toString().trim();
        }
        if (objsort.SortType !== undefined &&
            objsort.SortType !== null &&
            objsort.SortType.toString().trim().length > 0) {
            datatype = objsort.SortType.toString().trim();
        }
        if (objsort.SortFomart !== undefined &&
            objsort.SortFomart !== null &&
            objsort.SortFomart.toString().trim().length > 0) {
            dateformat = objsort.SortFomart.toString().trim();
        }
        if (datatype === 'D') {
            if (dateformat === undefined || dateformat === null) {
                dateformat = 'yyyy-MM-dd';
            }
        }
    }
    var rows = $('table#' + tableid + ' tbody tr.trdata').length;
    if (rows > 0) {
        var tableCur = document.getElementsByClassName(tableid)[0];
        var tbodyOfTable = tableCur.getElementsByTagName("tbody")[0];
        var rowsOfTable = tbodyOfTable.getElementsByTagName("tr");
        var arrayOfRows = new Array();
        datatype = datatype.toUpperCase();
        dateformat = dateformat.toLowerCase();
        var tdsort = 'table#' + tableid + ' tbody tr.trdata td[fieldid = "' + sortbycolumn + '"]';
        var i = 0;
        $(tdsort).each(function () {
            var td = $(this);
            var celltext = $(td).text().replace(/<[^>]*>/g, "");
            if (celltext !== undefined && celltext !== null && celltext.toString().trim().length > 0) {
                celltext = celltext.toString().trim();
            }
            arrayOfRows[i] = new Object;
            arrayOfRows[i].oldIndex = i;
            if (datatype === 'D') {
                arrayOfRows[i].value = getDateSortingKey(dateformat, celltext);
            }
            else if (datatype === 'N') {
                var re = /[^\.\-\+\d]/g;
                arrayOfRows[i].value = celltext.replace(re, "").toLowerCase();
            } else {
                arrayOfRows[i].value = celltext.toLowerCase();
            }
            i++;
        });
        switch (datatype) {
            case "N": arrayOfRows.sort(compareRowOfNumbers); break;
            case "D": arrayOfRows.sort(compareRowOfNumbers); break;
            default: arrayOfRows.sort(localeCompareRowOfText);
        }

        if (sortdirection === sortDirection.DESC) {
            arrayOfRows.reverse();
        }

        var newTableBody = document.createElement("tbody");
        for (var i = 0, len = arrayOfRows.length; i < len; i++) {
            newTableBody.appendChild(rowsOfTable[arrayOfRows[i].oldIndex].cloneNode(true));
        }
        tableCur.replaceChild(newTableBody, tbodyOfTable);
    }
}

var sortData_Old = function (tableid, sortbycolumn, issort, datatype, dateformat) {
    var rows = $('table#' + tableid + ' tbody tr.trdata').length;
    if (rows > 0) {
        var tableCur = document.getElementsByClassName(tableid)[0];
        var tbodyOfTable = tableCur.getElementsByTagName("tbody")[0];
        var rowsOfTable = tbodyOfTable.getElementsByTagName("tr");
        var arrayOfRows = new Array();
        datatype = datatype.toUpperCase();
        dateformat = dateformat.toLowerCase();

        var tdsort = 'table#' + tableid + ' tbody tr.trdata td[fieldid = "' + sortbycolumn + '"]';
        var i = 0;
        $(tdsort).each(function () {
            var td = $(this);
            var celltext = $(td).text().replace(/<[^>]*>/g, "");
            if (celltext !== undefined && celltext !== null && celltext.toString().trim().length > 0) {
                celltext = celltext.toString().trim();
            }
            arrayOfRows[i] = new Object;
            arrayOfRows[i].oldIndex = i;
            if (datatype === 'D') {
                arrayOfRows[i].value = getDateSortingKey(dateformat, celltext);
            }
            else if (datatype === 'N') {
                var re = /[^\.\-\+\d]/g;
                arrayOfRows[i].value = celltext.replace(re, "").toLowerCase();
            } else {
                arrayOfRows[i].value = celltext.toLowerCase();
            }
            i++;
        });

        if (issort) {
            arrayOfRows.reverse();
        }
        else {
            switch (datatype) {
                case "N": arrayOfRows.sort(compareRowOfNumbers); break;
                case "D": arrayOfRows.sort(compareRowOfNumbers); break;
                default: arrayOfRows.sort(localeCompareRowOfText);
            }
        }


        var newTableBody = document.createElement("tbody");
        for (var i = 0, len = arrayOfRows.length; i < len; i++) {
            newTableBody.appendChild(rowsOfTable[arrayOfRows[i].oldIndex].cloneNode(true));
        }
        tableCur.replaceChild(newTableBody, tbodyOfTable);
    }
}

var compareRowOfText = function (a, b) {
    var aval = a.value;
    var bval = b.value;
    return (aval == bval ? 0 : (aval > bval ? 1 : -1));
}

var localeCompareRowOfText = function (a, b) {
    var aval = a.value;
    var bval = b.value;
    var result = aval.localeCompare(bval);
    return result;
}

var compareRowOfNumbers = function (a, b) {
    var aval = /\d/.test(a.value) ? parseFloat(a.value) : 0;
    var bval = /\d/.test(b.value) ? parseFloat(b.value) : 0;
    return (aval == bval ? 0 : (aval > bval ? 1 : -1));
}

var getDateSortingKey = function (format, text) {
    if (format.length < 1) { return ""; }
    format = format.toLowerCase();
    format = format.replace(/^[^a-z0-9]*/, "");
    format = format.replace(/[^a-z0-9]*$/, "");
    format = format.replace(/[^a-z0-9]+/g, ",");
    var arrFormat = format.split(",");

    text = text.toLowerCase();
    text = text.replace(/^[^a-z0-9]*/, "");
    text = text.replace(/[^a-z0-9]*$/, "");
    if (text.length < 1) { return ""; }
    text = text.replace(/[^a-z0-9]+/g, ",");
    var date = text.split(",");
    if (date.length < 3) { return ""; }
    var d = 0, m = 0, y = 0;
    if (arrFormat === null || arrFormat.length == 0) { return ""; }
    for (var i = 0; i < arrFormat.length; i++) {
        var ts = arrFormat[i];
        if (ts == "dd") { d = date[i]; }
        else if (ts == "mm") { m = date[i]; }
        else if (ts == "yyyy") { y = date[i]; }
    }
    d = d.replace(/^0/, "");
    if (d < 10) { d = "0" + d; }
    if (/[a-z]/.test(m)) {
        m = m.substr(0, 3);
        switch (m) {
            case "jan": m = String(1); break;
            case "feb": m = String(2); break;
            case "mar": m = String(3); break;
            case "apr": m = String(4); break;
            case "may": m = String(5); break;
            case "jun": m = String(6); break;
            case "jul": m = String(7); break;
            case "aug": m = String(8); break;
            case "sep": m = String(9); break;
            case "oct": m = String(10); break;
            case "nov": m = String(11); break;
            case "dec": m = String(12); break;
            default: m = String(0);
        }
    }
    m = m.replace(/^0/, "");
    if (m < 10) { m = "0" + m; }
    y = parseInt(y);
    if (y < 100) { y = parseInt(y) + 2000; }
    return "" + String(y) + "" + String(m) + "" + String(d) + "";
};

(function ($) {
    $.fn.extend({
        table_Sortable: function (options) {
            // Đặt các giá trị mặc định
            var defaults = {
                //mouseover_color: 'red',
                //mouseover_size: '30px',
                //mouseout_color: 'white',
                //mouseout_size: '15px'
            };

            var settings = $.extend({}, defaults, options);
            var _columns = options.Columns;
            // Add sự kiện click
            addEventClick(this, _columns);
            // Add sự kiện search
            addEventSearch(this);
            // Add sự kiện Toggle column
            addEvenToggle(this);
            return this;

        }
    });
}(jQuery));