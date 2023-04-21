// idntable v0.0.1
// idocNet jsc
(function (window, $, undefined) {
    'use strict';

    $.fn.idnTable = function (opts) {
        /*
		 * constants
		 *-----------*/





        /*
		 * variables
		 *-----------*/


        var $tbHead, $tbData, $txtSearch, $togglecolumn, $cookieId;


        /*
		 * private methods
		 *-----------*/

        /// Lay gia tri cua Cell
        /// Edit ham nay de lay nhieu kieu gia tri khac

        var getCellValue = function ($td, dataType) {

            if (dataType == undefined) dataType = 'T';

            var val = $td.text();

            if (val.length > 0) {
                val = val.trim();
            }
            else
                val = '';

            if (dataType == 'N') {

                if (val.length > 0) {
                    var v = parseFloat(val.replace(/,/g, ""));

                    //$td.text(v);

                    return v;
                }
                else return undefined;


            }
            return val;
        }

        // so sanh 2 gia tri
        // 0: bang, 1: lon hon, -1: nho hon
        // edit ham nay de so sanh nhieu kieu du lieu khac
        var compareValue = function (val1, val2, dataType) {

            if (val1 == undefined || val2 == undefined) {

                return 0;
            }


            if (dataType == undefined) dataType = 'T';

            //if (dataType == 'N') {
            //    var fval1 = parseFloat(val1);
            //    var fval2 = parseFloat(val2);


            //    if (fval1 == fval2) return 0;

            //    else if (fval1 < fval2) return -1;
            //    else return 1;
            //}

            if (val1 == val2) return 0;
            else if (val1 < val2) return -1;
            else return 1;
        }


        var getThInfo = function ($th) {
            var fieldId = $th.attr('fieldid');
            var fieldName = $th.attr('fieldname');

            var dataType = $th.attr('idn-data-type');
            if (dataType == undefined) dataType = 'T';
            var enableSort = $th.attr('idn-data-sort') == '1';
            var enableFilter = $th.attr('idn-data-filter') == '1';

            var item = { id: fieldId, name: fieldName, dataType: dataType, enableSort: enableSort, enableFilter: enableFilter };

            var listColumnsShow = [];
            if ($cookieId !== undefined && $cookieId !== null) {
                var viewId = $cookieId.val();
                if (viewId !== undefined && viewId !== null && viewId.toString().trim().length > 0) {
                    var cookieValue = getCookie(viewId);
                    if (cookieValue !== undefined && cookieValue !== null && cookieValue.toString().trim().length > 0) {
                        var columnArray = cookieValue.split('|');
                        if (columnArray !== undefined && columnArray !== null && columnArray.length > 0) {
                            var listColumnsShow = [];
                            for (var j = 0; j < columnArray.length; j++) {
                                if (columnArray[j] !== undefined && columnArray[j] !== null && columnArray[j].toString().trim().length > 0)
                                    listColumnsShow.push(columnArray[j]);
                            }
                        }
                    }
                }
            }
            if (listColumnsShow !== undefined && listColumnsShow !== null && listColumnsShow.length > 0) {
                var isShow = false;
                for (var i = 0; i < listColumnsShow.length; i++) {
                    if (item.id === listColumnsShow[i]) {
                        isShow = true;
                    }
                }
                if (isShow) {
                    if ($th.hasClass('idn-hidden')) {
                        $th.removeClass('idn-hidden');
                    }
                    item.show = true;
                }
                else {
                    if (!$th.hasClass('idn-hidden')) {
                        $th.addClass('idn-hidden');
                    }
                    item.show = false;
                }
            }
            else {
                if ($th.hasClass('idn-hidden')) {
                    item.show = false;
                }
                else {
                    item.show = true;
                }
            }

            return item;
        };



        var initDropdown = function ($th) {
            var colInfo = getThInfo($th);
            if (!colInfo.enableFilter && !colInfo.enableSort) {
                return false;
            }
            var title = $th.attr('fieldName');
            if (title == 'CONTROL') title = '';
            if (title == undefined) {
                title = $th.text().trim();
                $th.attr('fieldName', title);
            }

            $th.empty();
            var $divRoot = $('<div/>').appendTo($th);

            $divRoot.addClass('divRoot');

            var $dropdown = $('<div class="classdropdown" data-toggle="dropdown"/>').appendTo($divRoot);
            $dropdown.html(title);


            var $ddmenu = $('<div class="dropdown-menu" role="menu" x-placement="bottom-start"/>').appendTo($divRoot);

            var $ulshowdata = $('<ul class="ulshowdata"/>').appendTo($ddmenu);

            if (colInfo.enableSort) {
                var $ascsort = $('<li class="ascsort"> Sort A to Z</li>').appendTo($ulshowdata);
                var $descsort = $('<li class="descsort"> Sort Z to A</li>').appendTo($ulshowdata);

                $ascsort.click(function () {
                    var th = $(this).closest('th');
                    sortData(th, 1);
                });

                $descsort.click(function () {
                    var th = $(this).closest('th');
                    sortData(th, -1);
                });
            }
            if (colInfo.enableFilter) {
                initFilterItems($th);

                $divRoot.on('show.bs.dropdown', function () {

                    var $ulshowdata = $(this).find('ul.ulshowdata').eq(0);

                    var $cball = $ulshowdata.find('input.cball').eq(0);
                    var c_all = true;

                    $ulshowdata.find('.ulc input.cbitem').each(function () {

                        var ck = $(this);
                        var on = ck.attr('on') == 'true';
                        if (on) {
                            ck.prop('checked', true);
                        }
                        else {
                            ck.prop('checked', false);
                            c_all = false;
                        }

                    });

                    if (c_all) {
                        $cball.prop('checked', true);
                    }
                    else {
                        $cball.prop('checked', false);
                    }

                });
            }
        };

        var initFilterItems = function ($th) {

            var colInfor = getThInfo($th);
            var fieldId = colInfor.id;

            var $ulshowdata = $th.find('ul.ulshowdata').eq(0);


            $('<li class="filtertitle"> Filter</li>').appendTo($ulshowdata);
            $('<li class="licheckall"><span><input type="checkbox" checked class="cball"></span><span>Tất cả</span></li>').appendTo($ulshowdata);
            $('<li class="listulc"><ul class="ulc"></ul></li>').appendTo($ulshowdata);
            $('<li class="li-btn-filter-ok"><a href="javascript:;" class="btn btn-nc-button" style="float: right;">OK</a></li>').appendTo($ulshowdata);

            var $ulc = $th.find('li.listulc ul.ulc').eq(0);
            $ulc.empty();

            $tbData.find('td[fieldid="' + fieldId + '"]').each(function () {
                var $td = $(this);

                var val = getCellValue($td, colInfor.dataType);

                var $ex = undefined;
                $ulc.find('.cbitem').each(function () {

                    var itemval = $(this).attr('val');

                    if (compareValue(itemval, val, colInfor.dataType) == 0) {
                        $ex = $(this);
                    }

                });

                if ($ex == undefined) {

                    var $li = $('<li class="liitem"></li>').appendTo($ulc);
                    var $span1 = $('<span/>').appendTo($li);
                    var $span2 = $('<span/>').appendTo($li);
                    var $ck = $('<input type="checkbox" checked on="true" class="cbitem"/>').appendTo($span1);

                    $ck.attr('val', val);
                    $span2.text(val);

                    $ck.click(function () {


                        if ($(this).is(':checked')) {
                            var c_all = true;
                            $(this).closest('ul.ulshowdata').find('.ulc input.cbitem').each(function () {
                                if (!$(this).is(':checked')) {
                                    c_all = false;
                                }
                            });

                            if (c_all) {
                                //check all checked
                                $(this).closest('ul.ulshowdata').find('input.cball').eq(0).prop('checked', true);
                            }
                        }
                        else {

                            //check all uncheck
                            $(this).closest('ul.ulshowdata').find('input.cball').eq(0).prop('checked', false);

                        }
                    });
                }


            });


            var $cball = $ulshowdata.find('input.cball').eq(0);


            $cball.click(function () {
                var checked = $(this).is(':checked');
                $(this).closest('ul.ulshowdata').find('.ulc input.cbitem').each(function () {

                    var ck = $(this);
                    if (checked) {
                        ck.prop('checked', true);
                    }
                    else {
                        ck.prop('checked', false);
                    }

                });
            });


            var $btnOk = $ulshowdata.find('a.btn-nc-button').eq(0);

            $btnOk.click(function () {

                $(this).closest('ul.ulshowdata').find('.ulc input.cbitem').each(function () {

                    var ck = $(this);
                    var checked = ck.is(':checked');
                    if (checked) {
                        ck.attr('on', 'true');
                    }
                    else {
                        ck.attr('on', 'false');
                    }

                });

                calcFilterByColumn($(this).closest('th').eq(0));

                redisplayRows();

            });

        };


        //doi cho 2 tr cho nhau
        var swapTr = function (tr1, tr2) {

            var bfor2 = tr2.prev();
            tr2.insertBefore(tr1);
            tr1.insertAfter(bfor2);

        };
        //sap xep
        // 1: asc, -1: desc
        var sortData = function ($th, dir) {
            var colInfo = getThInfo($th);
            var fieldId = colInfo.id;

            var trList = $tbData.find('tr.data-item');

            for (var i = 0; i < trList.length - 1; ++i) {

                var trI = $tbData.find('tr.data-item').eq(i);
                var tdI = trI.find('td[fieldid="' + fieldId + '"]');



                if (tdI != undefined && tdI.length > 0) {

                    var valI = getCellValue(tdI.eq(0), colInfo.dataType);
                    for (var j = i + 1; j < trList.length; ++j) {

                        var trJ = $tbData.find('tr.data-item').eq(j);

                        var tdJ = trJ.find('td[fieldid="' + fieldId + '"]');

                        if (tdJ != undefined && tdJ.length > 0) {

                            var valJ = getCellValue(tdJ.eq(0), colInfo.dataType);


                            if (compareValue(valI, valJ, colInfo.dataType) == dir) {

                                swapTr(trI, trJ);
                                trI = trJ;
                                valI = valJ;
                            }
                        }
                    }
                }

            }

            $tbHead.find('.classdropdown.icon-desc').removeClass('icon-desc');
            $tbHead.find('.classdropdown.icon-asc').removeClass('icon-asc');

            if (dir == 1)
                $th.find('.classdropdown').addClass('icon-asc');
            else
                $th.find('.classdropdown').addClass('icon-desc');

        };

        var initColums = function () {

            $tbHead.find('.trthead th').each(function () {
                var $th = $(this);

                var fieldId = $th.attr('fieldid');



                var isHidden = $th.attr('fieldactive') == '0';

                initDropdown($th);
            });
            redisplayColumns();
        };

        var redisplayColumns = function () {
            var listColumnsShow = [];
            $tbHead.find('.trthead th').each(function () {
                var $th = $(this);

                var fieldId = $th.attr('fieldid');

                if ($th.hasClass('idn-hidden')) {

                    $tbData.find('td[fieldid="' + fieldId + '"]').addClass('idn-hidden');
                }
                else {
                    $tbData.find('td[fieldid="' + fieldId + '"]').removeClass('idn-hidden');
                    listColumnsShow.push(fieldId);
                }


            });


            //co the phai sua lai cho checkbox
			///luu  idList show vao cookies

            if ($cookieId !== undefined && $cookieId !== null) {
                var viewId = $cookieId.val();

                if (viewId !== undefined && viewId !== null && viewId.toString().trim().length > 0) {

                    //var cookieValue = getCookie(viewId);
                    var strColumnsShow = '';
                    if (listColumnsShow != undefined && listColumnsShow !== null && listColumnsShow.length > 0) {
                        for (var j = 0; j < listColumnsShow.length; j++) {
                            strColumnsShow += listColumnsShow[j];
                            if (j != (listColumnsShow.length - 1)) {
                                strColumnsShow += '|';
                            }
                        }
                        setCookie(viewId, strColumnsShow, 10);
                    }


                    viewId = viewId.toString().trim();
                    // Danh sách cookie
                    var allCookies = document.cookie;
                    if (allCookies !== undefined && allCookies !== null && allCookies.length > 0) {
                        var cookieArray = allCookies.split(';');
                        if (cookieArray !== undefined && cookieArray !== null && cookieArray.length > 0) {
                            for (var i = 0; i < cookieArray.length; i++) {
                                var cookieCur = cookieArray[i];
                                var cookieArrayCur = cookieCur.split('=');

                                var cookieName = cookieArrayCur[0];
                                var cookieValue = cookieArrayCur[1];
                                if (cookieName !== undefined && cookieName !== null && cookieName.toString().trim().length > 0) {
                                    cookieName = cookieName.toString().trim();
                                    if (cookieName === viewId) {
                                        var strColumnsShow = '';
                                        if (listColumnsShow != undefined && listColumnsShow !== null && listColumnsShow.length > 0) {
                                            for (var j = 0; j < listColumnsShow.length; j++) {
                                                strColumnsShow += listColumnsShow[j];
                                                if (j != (listColumnsShow.length - 1)) {
                                                    strColumnsShow += '|';
                                                }
                                            }
                                        }
                                        cookieArray[i] = cookieName + '=' + strColumnsShow;
                                        setCookie(cookieName, strColumnsShow, 10);
                                    }
                                }
                                
                            }
                        }
                    }
                }
            }

        };

        //calulate filter theo cot
        var calcFilterByColumn = function ($th) {

            var colInfo = getThInfo($th);
            var fieldId = colInfo.id;

            var offValues = [];
            $th.find('.ulc input.cbitem').each(function () {

                var ck = $(this);
                var on = $(this).attr('on') == 'true'
                if (!on) {
                    offValues.push(ck.attr('val'));
                }

            });


            $tbData.find('tr.data-item td[fieldid="' + fieldId + '"]').each(function () {
                var $td = $(this);

                $td.removeClass('ft_off');

                var val = getCellValue($td, colInfo.dataType);
                for (var i = 0; i < offValues.length; ++i) {
                    if (compareValue(val, offValues[i], colInfo.dataType) == 0) {
                        $td.addClass('ft_off');
                        break;
                    }
                }


            });

            if (offValues.length > 0) {
                $th.find('.classdropdown').addClass('icon-filter');
            }
            else {
                $th.find('.classdropdown').removeClass('icon-filter');
            }


        };

        //an hien cac dong theo filter / search
        var redisplayRows = function () {

            
            $tbData.find('tr.data-item').each(function () {

                var $tr = $(this);
                if ($tr.find('td.ft_off').length > 0 || $tr.find('td.se_off').length > 0) {

                    
                    $tr.addClass('idn-hidden');
                }
                else {
                    $tr.removeClass('idn-hidden');
                }
            });

            if(opts.onRedisplay)
            {
                opts.onRedisplay();
            }
            

        };

        var initTxtSearch = function () {
            $txtSearch.unbind('keyup.idn').bind('keyup.idn', function () {
                var kw = $(this).val();
                
                search(kw);
            });
        };

        var initColumnToggle = function () {
            var colList = getAllColumns();
            $togglecolumn.html('');
            for (var i = 0; i < colList.length; ++i) {
                var col = colList[i];
                var fieldId = col.id;
                var title = col.name;

                if (title !== undefined && title !== null && title.toString().trim().length > 0) {
                    title = title.toString().trim();
                } else {
                    title = '';
                }

                var fieldActive = col.show;

                //B2: Sinh popup tùy chỉnh các cột trong bảng #dynamic-table-thead
                var $input = $('<input>');
                $input.attr('type', 'checkbox');
                $input.attr('name', fieldId);

                if (fieldActive === true) { //Nếu trạng thái bằng 0 cho hiện cột và checked ô input tương ứng
                    $input.attr('checked', 'checked');

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

                $togglecolumn.append($li);
                $input.click(function () {
                    if ($(this).is(':checked')) {
                        showColumns([$(this).attr('name')], false);
                    }
                    else {
                        hideColumns([$(this).attr('name')], false);
                    }
                });
            }
        };

        /*
		 * public method
		 *-----------*/
        var search = function (kw) {
            if (kw == undefined || kw.length == 0) {

                $tbData.find('tr.data-item td.se_off').removeClass('se_off');

            }
            else {
                kw = kw.toLocaleLowerCase();
                $tbData.find('tr.data-item').each(function () {

                    var $tr = $(this);

                    var text = $tr.text();

                    text = text.toLocaleLowerCase();

                    if (text.indexOf(kw) < 0) {
                        $tr.find('td').eq(0).addClass('se_off');
                    }
                    else {
                        $tr.find('td').eq(0).removeClass('se_off');
                    }


                });
            }

            redisplayRows();
        };

        var showColumns = function (idList, hideOthers) {
            $tbHead.find('.trheadfirst th').each(function () {
                var $th = $(this);
                var show = false;
                for (var i = 0; i < idList.length; ++i) {
                    if ($th.attr('fieldid') == idList[i]) {
                        show = true;
                        break;
                    }
                }

                if (show) {
                    $th.removeClass('idn-hidden');
                }

                else if (hideOthers) {
                    $th.addClass('idn-hidden');
                }
            });

            $tbHead.find('.trthead th').each(function () {
                var $th = $(this);
                var show = false;
                for (var i = 0; i < idList.length; ++i) {
                    if ($th.attr('fieldid') == idList[i]) {
                        show = true;
                        break;
                    }
                }

                if (show) {
                    $th.removeClass('idn-hidden');
                }

                else if (hideOthers) {
                    $th.addClass('idn-hidden');
                }
            });

            redisplayColumns();

        };
        var hideColumns = function (idList, showOthers) {
            $tbHead.find('.trheadfirst th').each(function () {
                var $th = $(this);
                var hide = false;
                for (var i = 0; i < idList.length; ++i) {
                    if ($th.attr('fieldid') == idList[i]) {
                        hide = true;
                        break;
                    }
                }

                if (hide) {
                    $th.addClass('idn-hidden');
                }

                else if (showOthers) {
                    $th.removeClass('idn-hidden');
                }
            });
            $tbHead.find('.trthead th').each(function () {
                var $th = $(this);
                var hide = false;
                for (var i = 0; i < idList.length; ++i) {
                    if ($th.attr('fieldid') == idList[i]) {
                        hide = true;
                        break;
                    }
                }

                if (hide) {
                    $th.addClass('idn-hidden');
                }

                else if (showOthers) {
                    $th.removeClass('idn-hidden');
                }
            });
            redisplayColumns();

        };
        var getAllColumns = function () {
            var list = [];
            $tbHead.find('.trthead th').each(function () {
                var $th = $(this);

                list.push(getThInfo($th));


            });

            return list;
        };

        var getColumnInfo = function (fieldId) {
            var $th = $tbHead.find('th[fieldid="' + fieldId + '"]').eq(0);

            return getThInfo($th);
        };


        /// goi ham khi fill data moi vao table data
        var reload = function () {
            redisplayColumns();
            $tbHead.find('.trthead th').each(function () {
                calcFilterByColumn($(this));


            });

            redisplayRows();
        };

        /*
         * Cookie
         *-----------*/

        var getCookie = function (cname) {
            var allCookies = document.cookie;
            if (allCookies !== undefined && allCookies !== null && allCookies.length > 0) {
                var cookieArray = allCookies.split(';');
                if (cookieArray !== undefined && cookieArray !== null && cookieArray.length > 0) {
                    for (var i = 0; i < cookieArray.length; i++) {
                        var cookieCur = cookieArray[i];
                        var cookieArrayCur = cookieCur.split('=');

                        var cookieName = cookieArrayCur[0];
                        var cookieValue = cookieArrayCur[1];
                        if (cookieName !== undefined && cookieName !== null && cookieName.toString().trim().length > 0) {
                            cookieName = cookieName.toString().trim();
                            if (cookieName === cname) {
                                if (cookieValue !== undefined && cookieValue !== null && cookieValue.toString().trim().length > 0) {
                                    return cookieValue;
                                }
                            }
                        }
                    }
                }
            }
            return "";
        };

        var setCookie = function (cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        };
        /*
		 * run
		 *-----------*/
        opts = opts || {};

        //var settings = $.extend({}, defaults, options);

        $tbHead = $(this);
        var dtid = $tbHead.attr('idn-datatable-id');
        $tbData = $('#' + dtid);
        if (opts.cookieId != undefined) {
            $cookieId = $(opts.cookieId);
        }

        if (opts.searchTextbox != undefined) {
            $txtSearch = $(opts.searchTextbox);
            initTxtSearch();
        }

        if (opts.columnToggleButton != undefined) {
            $togglecolumn = $(opts.columnToggleButton);
            initColumnToggle();
        }


        initColums();
        //neu co cookie:
        //- lay cookie => idList(cookie theo cookieID trong opts)
        //- showColumns(idList hien, hideOthers = true), dong thoi set check / uncheck cua togglecolumn checkbox
        if ($cookieId !== undefined && $cookieId !== null) {
            var viewId = $cookieId.val();

            if (viewId !== undefined && viewId !== null && viewId.toString().trim().length > 0) {
                viewId = viewId.toString().trim();

                var cookieValue = getCookie(viewId);
                if (cookieValue !== undefined && cookieValue !== null && cookieValue.toString().trim().length > 0) {
                    var columnArray = cookieValue.split('|');
                    if (columnArray !== undefined && columnArray !== null && columnArray.length > 0) {
                        var listColumnsShow = [];
                        for (var j = 0; j < columnArray.length; j++) {
                            if (columnArray[j] !== undefined && columnArray[j] !== null && columnArray[j].toString().trim().length > 0)
                                listColumnsShow.push(columnArray[j]);
                        }
                    }
                    if (listColumnsShow !== undefined && listColumnsShow !== null && listColumnsShow.length > 0) {
                        showColumns(listColumnsShow, true);
                    }
                    
                }
            }
        }

        ////////
        return {

            search: search,
            showColumns: showColumns,
            hideColumns: hideColumns,
            getColumnInfo: getColumnInfo,
            getAllColumns: getAllColumns,
            reload: reload,
        };
    };


})(window, jQuery);
