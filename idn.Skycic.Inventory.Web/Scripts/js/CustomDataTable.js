function dataTable(total, active, inactive) {
    var html2 = '<div style="display:inline-block;width:84%;height:50px;" id="item">';
    html2 += '<div id="left" style="float:left; margin-top:5px; font-size: 14px; font-weight: 600; "><span>Tất cả (' + total + ') &nbsp;&nbsp;|</span>';
    if (active !== null && active.toString().trim().length > 0)
    {
        html2 += '<span>&nbsp;Active (' + active + ')&nbsp;&nbsp;|</span>';
    }
    if (inactive !== null && inactive.toString().trim().length > 0)
    {
        html2 += '<span>&nbsp;Inactive (' + inactive + ')</span>';
    }
    
    html2 += '</div>';
    html2 += '<div id="right" style="float:right;">';
    html2 += '<div class="input-group" style="width:100px;">';
    html2 += '<input type="search" id="searchbox" placeholder="search" aria-controls="dynamic-table" style="border-radius:5px 0 0 5px !important"/>';
    html2 += '<span class="input-group-addon" style="position: inherit; left:-5px;border-radius:0 5px 5px 0 !important"><i class="glyphicon glyphicon-search" aria-hidden="true"></i></span>';
    html2 += '</div>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizePlus" style="margin-top:0"><b>A +</b></a>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizeMinus" style="margin-left: 5px;margin-top:0"><b>A -</b></a>';
    html2 += '</div>';
    html2 += '</div>';
    var check = true;

    var html = $('#htmlButton').html();
    jQuery(function ($) {
        //initiate dataTables plugin
        //var oTable1 =
            $('.table-cus')
                .dataTable({
                    "fnDrawCallback": function () {
                        if (check) {
                            var divParent = $("#dynamic-table_filter").parent();
                            divParent.attr('id', 'divleft');
                            $("#dynamic-table_length").before(html2);
                            divParent.html('');
                            check = false;
                        }
                        $('.table-cus th').css({ 'fontSize': "15px" });
                        $('.table-cus td').css({ 'fontSize': "14px" });
                        var min = 10;
                        var max = 30;
                        var reset = $('.table-cus th,.table-cus td').css('fontSize');
                        var elm = $('.table-cus th,.table-cus td');
                        var size = str_replace(reset, 'px', '');
                        $('a.fontSizePlus').click(function () {
                            
                            if (size <= max) {
                                size++;
                                elm.css({ 'fontSize': size });
                            }
                            return false;
                        });
                        $('a.fontSizeMinus').click(function () {
                            
                            if (size >= min) {
                                size--;
                                elm.css({ 'fontSize': size });
                            }
                            return false;
                        });
                        $('a.fontReset').click(function () {
                            elm.css({ 'fontSize': reset });
                        });
                        
                    },
                    bAutoWidth: false,
                    //"aoColumns": [
                    //    { "bSortable": false },
                    //    null, null, null, null, null, null, null, null, null, null, null, null,
                    //    { "bSortable": false }
                    //],
                    "aaSorting": [],
                    "lengthMenu": [[30, 50, 100, 150, 200, 250, 300, 400, 500, 1000, -1], [30, 50, 100, 150, 200, 250, 300, 400, 500, 1000, "All"]],
                    //"scrollY": 250,
                    "scrollX": true,
                    "iDisplayLength": 50,
                    //"searching": false,
                    "ordering": false,
                    "oLanguage": {
                        //"sEmptyTable": "Dữ liệu đang cập nhật",
                        "oPaginate": {
                            "sNext": '<i class="fa fa-chevron-right" aria-hidden="true"></i>',
                            "sPrevious": '<i class="fa fa-chevron-left" aria-hidden="true"></i>'
                        },
                        //sLengthMenu: "Hiển thị  _MENU_  dòng"
                    }
                });
        $("#item").parent().css({ "width": "100%", "display": "inline-block", "margin-bottom": "-20px", /*"background-color": "#ffffff"*/ });
        $(document).ready(function () {
            var dataTable = $('#dynamic-table').dataTable();
            $("#searchbox").keyup(function () {
                dataTable.fnFilter(this.value);
            });
            $("#searchbox").mouseleave().css("border", "1px solid #d5d5d5");
        });
    });
}

// use:(có nhiều bảng truyền lần lượt '', '1', '2'. Max: 4 table)
//dataTable('', '@MvcHtmlString.Create(totalRow.ToString(CultureInfo.InvariantCulture))','@MvcHtmlString.Create(totalRowActive.ToString(CultureInfo.InvariantCulture))', '@MvcHtmlString.Create(totalRowInactive.ToString(CultureInfo.InvariantCulture))');
//dataTable('1', '@MvcHtmlString.Create(totalRow.ToString(CultureInfo.InvariantCulture))', '@MvcHtmlString.Create(totalRowActive.ToString(CultureInfo.InvariantCulture))', '@MvcHtmlString.Create(totalRowInactive.ToString(CultureInfo.InvariantCulture))');
function dataTable(idtable, total, active, inactive) {

    var html2 = '<div style="display:inline-block;width:84%;height:50px;" id="item' + idtable + '">';
    html2 += '<div id="left' + idtable + '" style="float:left; margin-top:5px; font-size: 14px; font-weight: 600; "><span>Tất cả (' + total + ') &nbsp;&nbsp;|</span>';
    if (active !== null && active !== undefined && active.toString().trim().length > 0) {
        html2 += '<span>&nbsp;Active (' + active + ')&nbsp;&nbsp;|</span>';
    }
    if (inactive !== null && inactive !== undefined && inactive.toString().trim().length > 0) {
        html2 += '<span>&nbsp;Inactive (' + inactive + ')</span>';
    }

    html2 += '</div>';
    html2 += '<div id="right' + idtable + '" style="float:right;">';
    html2 += '<div class="input-group" style="width:100px;">';
    html2 += '<input type="search" id="searchbox' + idtable + '" placeholder="search" aria-controls="dynamic-table" style="border-radius:5px 0 0 5px !important"/>';
    html2 += '<span class="input-group-addon" style="position: inherit; left:-5px;border-radius:0 5px 5px 0 !important"><i class="glyphicon glyphicon-search" aria-hidden="true"></i></span>';
    html2 += '</div>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizePlus' + idtable + '" style="margin-top:0"><b>A +</b></a>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizeMinus' + idtable + '" style="margin-left: 5px;margin-top:0"><b>A -</b></a>';
    html2 += '</div>';
    html2 += '</div>';
    var check = true;

    var html = $('#htmlButton').html();
    jQuery(function ($) {
        //initiate dataTables plugin
        
            $('#dynamic-table' + idtable)
                .dataTable({
                    "fnDrawCallback": function () {
                        if (check) {
                            var divParent = $("#dynamic-table" + idtable + "_filter").parent();
                            divParent.attr('id', 'divleft' + idtable);
                            $("#dynamic-table" + idtable + "_length").before(html2);
                            divParent.html('');
                            check = false;
                        }
                        //'#dynamic-table' + idtable
                        $('#dynamic-table' + idtable + ' th').css({ 'fontSize': "15px" });
                        $('#dynamic-table' + idtable + ' td').css({ 'fontSize': "14px" });
                        var min = 10;
                        var max = 30;
                        var reset = $('#dynamic - table' + idtable + ' th,#dynamic-table' + idtable + ' td').css('fontSize');
                        var elm = $('#dynamic - table' + idtable + ' th,#dynamic-table' + idtable + ' td');
                        var size = str_replace(reset, 'px', '');
                        $('a.fontSizePlus' + idtable).click(function () {

                            if (size <= max) {
                                size++;
                                elm.css({ 'fontSize': size });
                            }
                            return false;
                        });
                        $('a.fontSizeMinus' + idtable).click(function () {

                            if (size >= min) {
                                size--;
                                elm.css({ 'fontSize': size });
                            }
                            return false;
                        });
                        $('a.fontReset').click(function () {
                            elm.css({ 'fontSize': reset });
                        });

                    },
                    bAutoWidth: false,
                    //"aoColumns": [
                    //    { "bSortable": false },
                    //    null, null, null, null, null, null, null, null, null, null, null, null,
                    //    { "bSortable": false }
                    //],
                    "aaSorting": [],
                    "lengthMenu": [[1, 50, 100, 150, 200, 250, 300, 400, 500, 1000, -1], [1, 50, 100, 150, 200, 250, 300, 400, 500, 1000, "All"]],
                    //"scrollY": 250,
                    "scrollX": true,
                    "iDisplayLength": 50,
                    //"searching": false,
                    "ordering": false,
                    "oLanguage": {
                        //"sEmptyTable": "Dữ liệu đang cập nhật",
                        "oPaginate": {
                            "sNext": '<i class="fa fa-chevron-right" aria-hidden="true"></i>',
                            "sPrevious": '<i class="fa fa-chevron-left" aria-hidden="true"></i>'
                        },
                        //sLengthMenu: "Hiển thị  _MENU_  dòng"
                    }
                });
        $("#item" + idtable).parent().css({ "width": "100%", "display": "inline-block", "margin-bottom": "-20px", /*"background-color": "#ffffff"*/ });
        $(document).ready(function () {
            var dataTable = $('#dynamic-table' + idtable).dataTable();
            $("#searchbox" + idtable).keyup(function () {
                dataTable.fnFilter(this.value);
            });
            $("#searchbox" + idtable).mouseleave().css("border", "1px solid #d5d5d5");
        });
    });
}

function dataTableHDDT(idtable, total) {
    var rowcur = 100;
    var page = Math.ceil(total / rowcur);
    var html2 = '<div style="display:inline-block;width:84%;height:50px;" id="item' + idtable + '">';
    html2 += '<div id="left' + idtable + '" style="float:left; margin-top:5px; font-size: 14px; font-weight: 600; "><span>Tổng số ' + total + ' kết quả trong ' + page + ' trang</span>';

    html2 += '</div>';
    html2 += '<div id="right' + idtable + '" style="float:right;">';
    html2 += '</div>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizePlus' + idtable + '" style="margin-top:0"><b>A +</b></a>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizeMinus' + idtable + '" style="margin-left: 5px;margin-top:0"><b>A -</b></a>';
    html2 += '</div>';
    html2 += '</div>';
    var check = true;

    var html = $('#htmlButton').html();
    jQuery(function ($) {
        //initiate dataTables plugin

        $('#dynamic-table' + idtable)
            .dataTable({
                "fnDrawCallback": function () {
                    if (check) {
                        var divParent = $("#dynamic-table" + idtable + "_filter").parent();
                        divParent.attr('id', 'divleft' + idtable);
                        $("#dynamic-table" + idtable + "_length").before(html2);
                        divParent.html('');
                        check = false;
                    }
                    //'#dynamic-table' + idtable
                    $('#dynamic-table' + idtable + ' th').css({ 'fontSize': "15px" });
                    $('#dynamic-table' + idtable + ' td').css({ 'fontSize': "14px" });
                    var min = 10;
                    var max = 30;
                    var reset = $('#dynamic - table' + idtable + ' th,#dynamic-table' + idtable + ' td').css('fontSize');
                    var elm = $('#dynamic - table' + idtable + ' th,#dynamic-table' + idtable + ' td');
                    var size = str_replace(reset, 'px', '');
                    $('a.fontSizePlus' + idtable).click(function () {

                        if (size <= max) {
                            size++;
                            elm.css({ 'fontSize': size });
                        }
                        return false;
                    });
                    $('a.fontSizeMinus' + idtable).click(function () {

                        if (size >= min) {
                            size--;
                            elm.css({ 'fontSize': size });
                        }
                        return false;
                    });
                    $('a.fontReset').click(function () {
                        elm.css({ 'fontSize': reset });
                    });

                },
                bAutoWidth: false,
                //"aoColumns": [
                //    { "bSortable": false },
                //    null, null, null, null, null, null, null, null, null, null, null, null,
                //    { "bSortable": false }
                //],
                "aaSorting": [],
                "lengthMenu": [[1, 50, 100, 150, 200, 250, 300, 400, 500, 1000, -1], [1, 50, 100, 150, 200, 250, 300, 400, 500, 1000, "All"]],
                //"scrollY": 250,
                "scrollX": true,
                "iDisplayLength": rowcur,
                //"searching": false,
                "ordering": false,
                "oLanguage": {
                    //"sEmptyTable": "Dữ liệu đang cập nhật",
                    "oPaginate": {
                        "sNext": '<i class="fa fa-chevron-right" aria-hidden="true"></i>',
                        "sPrevious": '<i class="fa fa-chevron-left" aria-hidden="true"></i>'
                    },
                    //sLengthMenu: "Hiển thị  _MENU_  dòng"
                }
            });
        $("#item" + idtable).parent().css({ "width": "100%", "display": "inline-block", "margin-bottom": "-20px", /*"background-color": "#ffffff"*/ });
        $(document).ready(function () {
            var dataTable = $('#dynamic-table' + idtable).dataTable();
            $("#searchbox" + idtable).keyup(function () {
                dataTable.fnFilter(this.value);
            });
            $("#searchbox" + idtable).mouseleave().css("border", "1px solid #d5d5d5");
        });
    });
}

function dataTableMng(idtable, total, active, inactive) {

    var html2 = '<div style="display:inline-block;width:79%;height:30px;" id="item' + idtable + '">';
    html2 += '<div id="left' + idtable + '" style="float:left; margin-top:5px; font-size: 14px; font-weight: 600; padding-left: 15px; "><span>Tất cả (' + total + ') &nbsp;&nbsp;|</span>';
    if (active !== null && active !== undefined && active.toString().trim().length > 0 && parseFloat(active) > 0) {
        html2 += '<span>&nbsp;Active (' + active + ')&nbsp;&nbsp;|</span>';
    }
    if (inactive !== null && inactive !== undefined && inactive.toString().trim().length > 0 && parseFloat(inactive) > 0) {
        html2 += '<span>&nbsp;Inactive (' + inactive + ')</span>';
    }

    html2 += '</div>';
    html2 += '<div id="right' + idtable + '" style="float:right;">';
    html2 += '<div class="input-group" style="width:100px;">';
    html2 += '<input type="search" id="searchbox' + idtable + '" placeholder="search" aria-controls="dynamic-table" style="border-radius:5px 0 0 5px !important"/>';
    html2 += '<span class="input-group-addon" style="position: inherit; left:-5px;border-radius:0 5px 5px 0 !important"><i class="glyphicon glyphicon-search" aria-hidden="true"></i></span>';
    html2 += '</div>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizePlus' + idtable + '" style="margin-top:0"><b>A +</b></a>';
    //html2 += '<a href="javascript:" class="btn mybtnButton fontSizeMinus' + idtable + '" style="margin-left: 5px;margin-top:0"><b>A -</b></a>';
    html2 += '</div>';
    html2 += '</div>';
    var check = true;

    var html = $('#htmlButton').html();
    jQuery(function ($) {
        //initiate dataTables plugin

        $('#dynamic-table' + idtable)
            .dataTable({
                "fnDrawCallback": function () {
                    if (check) {
                        var divParentInfo = $("#dynamic-table" + idtable + "_info").parent();
                        divParentInfo.html('');
                        //divParentInfo.remove();
                        //dataTables_empty
                        var tr = $(".dataTables_empty").parent().remove();
                        var divParent = $("#dynamic-table" + idtable + "_filter").parent();
                        divParent.attr('id', 'divleft' + idtable);
                        divParent.attr('class', '');
                        //divParent.attr('class', 'col-xs-5');
                        var divParentRight = $("#dynamic-table" + idtable + "_length").parent();
                        divParentRight.attr('id', 'divright' + idtable);
                        divParentRight.attr('class', '');
                        divParentRight.css('min-width', '700px');
                        divParentRight.css('width', '1085px');
                        divParentRight.html('');
                        var divParentRightParent = divParentRight.parent();
                        divParentRightParent.remove();
                        //divParentRight.attr('class', 'col-xs-7');
                        $("#dynamic-table" + idtable + "_length").before(html2);
                        divParent.html('');
                        check = false;
                    }
                    //'#dynamic-table' + idtable
                    $('#dynamic-table' + idtable + ' th').css({ 'fontSize': "15px" });
                    $('#dynamic-table' + idtable + ' td').css({ 'fontSize': "14px" });
                    var min = 10;
                    var max = 30;
                    var reset = $('#dynamic - table' + idtable + ' th,#dynamic-table' + idtable + ' td').css('fontSize');
                    var elm = $('#dynamic - table' + idtable + ' th,#dynamic-table' + idtable + ' td');
                    var size = str_replace(reset, 'px', '');
                    $('a.fontSizePlus' + idtable).click(function () {

                        if (size <= max) {
                            size++;
                            elm.css({ 'fontSize': size });
                        }
                        return false;
                    });
                    $('a.fontSizeMinus' + idtable).click(function () {

                        if (size >= min) {
                            size--;
                            elm.css({ 'fontSize': size });
                        }
                        return false;
                    });
                    $('a.fontReset').click(function () {
                        elm.css({ 'fontSize': reset });
                    });

                },
                bAutoWidth: false,
                //"aoColumns": [
                //    { "bSortable": false },
                //    null, null, null, null, null, null, null, null, null, null, null, null,
                //    { "bSortable": false }
                //],
                "aaSorting": [],
                "lengthMenu": [[10, 50, 100, 150, 200, 250, 300, 400, 500, 1000, -1], [10, 50, 100, 150, 200, 250, 300, 400, 500, 1000, "All"]],
                //"scrollY": 250,
                "scrollX": true,
                "iDisplayLength": 10,
                //"searching": false,
                "ordering": false,
                "oLanguage": {
                    //"sEmptyTable": "Dữ liệu đang cập nhật",
                    "oPaginate": {
                        "sNext": '<i class="fa fa-chevron-right" aria-hidden="true"></i>',
                        "sPrevious": '<i class="fa fa-chevron-left" aria-hidden="true"></i>'
                    },
                    //sLengthMenu: "Hiển thị  _MENU_  dòng"
                }
            });
        $("#item" + idtable).parent().css({ "width": "100%", "display": "inline-block", "margin-bottom": "-20px", /*"background-color": "#ffffff"*/ });
        $(document).ready(function () {
            var dataTable = $('#dynamic-table' + idtable).dataTable();
            $("#searchbox" + idtable).keyup(function () {
                dataTable.fnFilter(this.value);
            });
            $("#searchbox" + idtable).mouseleave().css("border", "1px solid #d5d5d5");
        });
    });
}

function str_replace(_haystack, needle, replacement) {

    if (_haystack === undefined || _haystack === null || _haystack.trim().length === 0) {
        _haystack = '14';
    }
    if (_haystack !== null && _haystack.toString().trim().length > 0)
    {
        var _value = _haystack.toString().trim();
        var temp = _value.split(needle);
        return temp.join(replacement);
    }
    
}

