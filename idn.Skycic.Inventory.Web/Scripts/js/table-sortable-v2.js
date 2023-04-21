
var addEventClick = function (thiz, _columns) {
    var tableId = $(thiz).attr('id');
    $('table' + '#' + tableId + ' tr th').each(function () {
        var th = $(this);
        if (th !== undefined && th !== null) {
            var title = $(th).text();
            if (title !== undefined && title !== null && title.toString().trim().length > 0) {
                title = title.toString().trim();
            } else {
                title = '';
            }
            $(th).html('');
            var $divMenu = $('<div>');
            $divMenu.attr('class', 'divRoot');
            var $divdropdown = $('<div>');
            $divdropdown.attr('class', 'classdropdown');
            $divdropdown.attr('data-toggle', 'dropdown');
            $divdropdown.text(title);
            $divMenu.append($divdropdown);
            $(th).append($divMenu);
            var fieldId = $(th).attr('fieldId');
            if (fieldId !== undefined && fieldId !== null && fieldId.toString().trim().length > 0) {
                fieldId = fieldId.toString().toLocaleUpperCase().trim();
                if (_columns !== undefined && _columns !== null) {
                    for (var key in _columns) {
                        key = key.toString().toLocaleUpperCase().trim();
                        if (fieldId === key) {
                            var attr = $(this).attr('filterroot');
                            if (typeof attr !== typeof undefined && attr !== false) {
                                // ...
                            } else {
                                $(th).attr('filterroot', 'false');
                            }
                            //$(th).click(function () {
                            //    addDropdown($divMenu, _columns);
                            //});

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
    debugger;
    if (thiz !== undefined && thiz !== null) {
        var th = $(thiz).parent();
        var tableId = $(th).parent().parent().parent().attr('id');
        var tableId_Data = tableId + '-tbody';
        var listData = [];
        var listDataOld = [];
        var isulshowdata = false;

        var filterRoot = $(th).attr('filterroot');
        if (filterRoot === undefined || filterRoot === null || filterRoot.toString().trim().length === 0) {
            filterRoot = 'false';
        }
        var fieldId = $(th).attr('fieldid');
        if (fieldId !== undefined && fieldId !== null && fieldId.toString().trim().length > 0) {
            fieldId = fieldId.toString().toLocaleUpperCase().trim();

            var ulshowdata = $(th).find('ul.ulshowdata');
            if (ulshowdata !== undefined && ulshowdata !== null) {
                var liCount = $('th[fieldid="' + fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]');
                debugger;
                if (liCount !== undefined && liCount !== null) {//cbitem
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

            $('table' + '#' + tableId_Data + ' td[fieldid]').each(function () {
                var tdCur = $(this);
                if (tdCur !== undefined && tdCur !== null) {
                    var _fieldId = $(tdCur).attr('fieldid');
                    if (_fieldId !== undefined && _fieldId !== null && _fieldId.toString().trim().length > 0) {
                        _fieldId = _fieldId.toString().toLocaleUpperCase().trim();
                        if (fieldId === _fieldId) {
                            $(tdCur).attr('filterroot', 'true');
                            var trCur = $(tdCur).parent();
                            if (trCur !== undefined && trCur !== null) {
                                if (!$(trCur).hasClass('display-none')) {
                                    var _value = $(tdCur).text();
                                    if (_value !== undefined && _value !== null && _value.toString().trim().length > 0) {
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
                                            var obj = {
                                                Data: _value,
                                                IsChecked: true
                                            };
                                            listData.push(obj);
                                            //listData.push(_value);
                                        }
                                    }
                                }
                            }
                        } else {
                            $(tdCur).attr('filterroot', 'false');
                        }
                    }
                }
            });


            //if (filterRoot.toString().toLocaleLowerCase().trim() === 'true') {
            //    $('table' + '#' + tableId_Data + ' td[fieldid]').each(function () {
            //        var tdCur = $(this);
            //        if (tdCur !== undefined && tdCur !== null) {
            //            var _fieldId = $(tdCur).attr('fieldid');
            //            if (_fieldId !== undefined && _fieldId !== null && _fieldId.toString().trim().length > 0) {
            //                _fieldId = _fieldId.toString().toLocaleUpperCase().trim();
            //                if (fieldId === _fieldId) {
            //                    $(tdCur).attr('filterroot', 'true');
            //                    var _value = $(tdCur).text();
            //                    if (_value !== undefined && _value !== null && _value.toString().trim().length > 0) {
            //                        _value = _value.toString().trim();
            //                        var dataExists = false;
            //                        for (var j = 0; j < listData.length; j++) {
            //                            var _data = listData[j];
            //                            if (_value === _data) {
            //                                dataExists = true;
            //                                break;
            //                            }
            //                        }
            //                        if (!dataExists) {
            //                            listData.push(_value);
            //                        }
            //                    }
            //                } else {
            //                    $(tdCur).attr('filterroot', 'false');
            //                }
            //            }
            //        }
            //    });
            //} else {
            //    $('table' + '#' + tableId_Data + ' td[fieldid]').each(function () {
            //        var tdCur = $(this);
            //        if (tdCur !== undefined && tdCur !== null) {
            //            var _fieldId = $(tdCur).attr('fieldid');
            //            if (_fieldId !== undefined && _fieldId !== null && _fieldId.toString().trim().length > 0) {
            //                _fieldId = _fieldId.toString().toLocaleUpperCase().trim();
            //                if (fieldId === _fieldId) {
            //                    $(tdCur).attr('filterroot', 'true');
            //                    var trCur = $(tdCur).parent();
            //                    if (trCur !== undefined && trCur !== null) {
            //                        if (!$(trCur).hasClass('display-none')) {
            //                            var _value = $(tdCur).text();
            //                            if (_value !== undefined && _value !== null && _value.toString().trim().length > 0) {
            //                                _value = _value.toString().trim();
            //                                var dataExists = false;
            //                                for (var j = 0; j < listData.length; j++) {
            //                                    var _data = listData[j];
            //                                    if (_value === _data) {
            //                                        dataExists = true;
            //                                        break;
            //                                    }
            //                                }
            //                                if (!dataExists) {
            //                                    listData.push(_value);
            //                                }
            //                            }
            //                        }
            //                    }
            //                } else {
            //                    $(tdCur).attr('filterroot', 'false');
            //                }
            //            }
            //        }
            //    });
            //}
        }
        debugger;
        if (listData !== undefined && listData !== null && listData.length > 0) {

            var ulshowdata = $(th).find('ul.ulshowdata');
            if (ulshowdata === undefined || ulshowdata === null || ulshowdata.length === 0) {
                var $div = $('<div>');
                //$div.trigger('click');
                $div.click(function (e) {
                    e.stopPropagation();
                    //e.preventDefault();
                });
                $div.attr('class', 'dropdown-menu');
                $div.attr('role', 'menu');
                var $ul = $('<ul>');
                $ul.attr('class', 'ulshowdata');
                //$ul.click(function (e) {
                //    e.stopPropagation();
                //    e.preventDefault();
                //});
                var $liSortAsc = $('<li>');
                $liSortAsc.text('Sắp xếp ASC');
                $liSortAsc.attr('class', 'abc def');
                $liSortAsc.click(function (e) {
                    //e.stopPropagation();
                    sortDataAsc();
                });
                $ul.append($liSortAsc);
                var $liSortDesc = $('<li>');
                $liSortDesc.text('Sắp xếp DESC');
                $liSortDesc.attr('class', 'abc def');
                $liSortDesc.click(function (e) {
                    //e.stopPropagation();
                    sortDataDesc();
                });
                $ul.append($liSortDesc);
                if (listData !== undefined && listData !== null && listData.length > 0) {
                    var $liCheckAll = $('<li>');
                    $liCheckAll.attr('class', 'abc def');
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
                    $divInputCheckAllTitle.text(' Check all');
                    $liCheckAll.append($divInputCheckAll);
                    $liCheckAll.append($divInputCheckAllTitle);

                    $ul.append($liCheckAll);
                    var $liFilter = $('<li>');
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
                            //$inputCheck.prop('checked', true);
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
                var $a = $('<a>');
                $a.attr('href', 'javascript:;');
                $a.attr('class', 'btn-filter-ok');
                //$a.attr('onclick', 'filterData(this, _columns)');
                $a.click(function (e) {
                    filterData($a, _columns);
                });
                $a.text('OK');
                $liButton.append($a);
                // add button OK hay cancel ở đây
                $ul.append($liButton);
                //return $ul;
                $div.append($ul);
                $(thiz).append($div);
            } else {
                $(ulshowdata).empty();
                var $ul = ulshowdata;
                //$ul.click(function (e) {
                //    e.stopPropagation();
                //    e.preventDefault();
                //});
                $ul.attr('class', 'ulshowdata');
                var $liSortAsc = $('<li>');
                $liSortAsc.text('Sắp xếp ASC');
                $liSortAsc.attr('class', 'abc def');
                $liSortAsc.click(function (e) {
                    //e.stopPropagation();
                    sortDataAsc();
                });
                $ul.append($liSortAsc);
                var $liSortDesc = $('<li>');
                $liSortDesc.text('Sắp xếp DESC');
                $liSortDesc.attr('class', 'abc def');
                $liSortDesc.click(function (e) {
                    //e.stopPropagation();
                    sortDataDesc();
                });
                $ul.append($liSortDesc);
                if (listData !== undefined && listData !== null && listData.length > 0) {
                    var $liCheckAll = $('<li>');
                    $liCheckAll.attr('class', 'abc def');
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
                    $divInputCheckAllTitle.text(' Check all');
                    $liCheckAll.append($divInputCheckAll);
                    $liCheckAll.append($divInputCheckAllTitle);

                    $ul.append($liCheckAll);
                    var $liFilter = $('<li>');
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
                            //$inputCheck.prop('checked', true);
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
                var $a = $('<a>');
                $a.attr('href', 'javascript:;');
                $a.click(function (e) {
                    filterData($a, _columns);
                });
                $a.text('OK');
                $liButton.append($a);
                // add button OK hay cancel ở đây
                $ul.append($liButton);

            }


        }
    }
};

var checkAll = function (thiz, inputcheckbox) {
    // inputcheckbox: '.table-tbody input.ace'
    // inputcheckbox: '#table-tbodyID input.sl_ace'
    // inputcheckbox: '.table-tbody input.chked'
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
    debugger;
    var ulshowdata = $(thiz).parent().parent();
    var th = $(ulshowdata).parent().parent().parent();
    if (ulshowdata !== undefined && ulshowdata !== null && th !== undefined && th !== null) {
        var fieldId = $(th).attr('fieldid');

        if (fieldId !== undefined && fieldId !== null && fieldId.toString().trim().length > 0) {
            $(th).attr('filterroot', 'true');
            var listData = [];
            var columns = jQuery.extend(true, {}, _columns);
            //if (columns !== undefined && columns !== null && columns.count > 0) {
            //    for (var key in columns) {
            //        key = key.toString().toLocaleUpperCase().trim();
            //        columns[key] = [];
            //    }
            //}

            // tập hợp dữ liệu filter
            var tableId = $(th).parent().parent().parent().attr('id');
            $('table' + '#' + tableId + ' tr th').each(function () {
                var thCur = $(this);
                var _fieldId = $(thCur).attr('fieldid');
                if (_fieldId !== undefined && _fieldId !== null && _fieldId.toString().trim().length > 0) {
                    _fieldId = _fieldId.toString().toLocaleUpperCase().trim();
                    if (_columns !== undefined && _columns !== null) {
                        for (var key in _columns) {
                            key = key.toString().toLocaleUpperCase().trim();

                            columns[key] = [];
                            var ulc = $('th[fieldid="' + key + '"]').find('ul.ulc');
                            if (ulc !== undefined && ulc !== null) {
                                var liCount = $('th[fieldid="' + key + '"] ul.ulc li.liitem').has('input[type=checkbox]:checked');
                                if (liCount !== undefined && liCount !== null) {
                                    for (var i = 0; i < liCount.length; i++) {
                                        var liCur = liCount[i];
                                        if (liCur !== undefined && liCur !== null) {
                                            var desc = $(liCur).attr('desc');
                                            columns[key].push(desc);
                                        }
                                    }
                                }
                            }

                            //if (fieldId === key) {
                            //    columns[key] = [];
                            //    var ulc = $('th[fieldid="' + fieldId + '"]').find('ul.ulc');
                            //    if (ulc !== undefined && ulc !== null) {
                            //        var liCount = $('th[fieldid="' + fieldId + '"] ul.ulc li.liitem').has('input[type=checkbox]:checked');
                            //        if (liCount !== undefined && liCount !== null) {
                            //            for (var i = 0; i < liCount.length; i++) {
                            //                var liCur = liCount[i];
                            //                if (liCur !== undefined && liCur !== null) {
                            //                    var desc = $(liCur).attr('desc');
                            //                    columns[key].push(desc);
                            //                }
                            //            }
                            //        }
                            //    }

                            //}
                        }
                    }
                }
            });


            debugger;
            // filter table
            if (columns !== undefined && columns !== null) {
                debugger;
                var table = $(th).parent().parent().parent();
                if (table !== undefined && table !== null) {
                    var tableId = $(table).attr('id');
                    var tableId_Data = tableId + '-tbody';
                    if (tableId_Data !== undefined && tableId_Data !== null && tableId_Data.toString().trim().length > 0) {
                        var rows = $('table#' + tableId_Data + ' tbody tr.trdata').length;
                        if (rows > 0) {
                            $('table#' + tableId_Data + ' tbody tr.trdata').each(function () {
                                var tr = $(this);
                                var idx = $(tr).attr('idx');
                                debugger;
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
                                }


                                //var tdByFieldId = $(tr).find('td[fieldid = "' + fieldId + '"]');
                                //if (tdByFieldId !== undefined && tdByFieldId !== null) {
                                //    var data = $(tdByFieldId).text();
                                //    if (data !== undefined && data !== null && data.toString().trim().length > 0) {
                                //        data = data.toString().trim();
                                //    } else {
                                //        data = '';
                                //    }
                                //    var flagFilter = false;
                                //    for (var j = 0; j < listData.length; j++) {
                                //        var _data = listData[j].toString().trim();
                                //        if (data === _data) {
                                //            flagFilter = true;
                                //            break;
                                //        }
                                //    }
                                //    if (!flagFilter) {
                                //        // không tìm thấy data => ẩn row
                                //        if (!$(tr).hasClass('display-none')) {
                                //            $(tr).addClass('display-none');
                                //        }
                                //    } else {
                                //        // tìm thấy => kiểm tra có class 'display-none'; nếu có class 'display-none' => remove
                                //        if ($(tr).hasClass('display-none')) {
                                //            $(tr).removeClass('display-none');
                                //        }
                                //    }
                                //}
                            });
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
        //$(th).click(function () {
        //    addDropdown(divRoot, _columns);
        //});
    }
};

var sortDataAsc = function (e) {
    debugger;
    //e.stopPropagation();
    alert('Asc!');
};

var sortDataDesc = function (e) {
    debugger;
    //e.stopPropagation();
    alert('Desc!');
};


var addHtmlDropdown = function (listdata) {

    var $ul = $('<ul>');
    var $liSort = $('<li>');
    $liSort.text('sắp xếp');
    $liSort.attr('class', 'abc def');
    $ul.append($liSort);
    if (listdata !== undefined && listdata !== null && listdata.length > 0) {
        var $liCheckAll = $('<li>');
        $liCheckAll.text('check all');
        $liCheckAll.attr('class', 'abc def');
        $ul.append($liCheckAll);
        var $liFilter = $('<li>');
        var $ulFilterData = $('<ul>');
        for (var i = 0; i < listdata.length; i++) {
            var _data = listdata[i];
            if (_data !== undefined && _data !== null && _data.toString().trim().length > 0) {
                _data = _data.toString().trim();
                var $liData = $('<li>');
                $liData.text(_data);
                $liData.attr('class', 'abc def');
                $ulFilterData.append($liData);
            }
        }
        $liFilter.append($ulFilterData);
        $ul.append($liFilter);
    }

    var $liButton = $('<li>');
    // add button OK hay cancel ở đây
    $ul.append($liButton);
    return $ul;
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
            return this;

        }
    });
}(jQuery));