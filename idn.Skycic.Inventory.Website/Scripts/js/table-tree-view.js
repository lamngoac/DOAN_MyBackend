function TreeTable($table) {
    var
        //$table = $('.tree-table'),
        rows = $table.find('tr');
    //For từng dòng tr
    rows.each(function (index, row) {
        //debugger;
        var
            $row = $(row),
            //lấy ra thuộc thính data-root của dòng hiện tại
            $dataRoot = $row.attr('data-root'),
            //div chứa các product base có root trùng với thuộc tính data-root của dòng hiện tại
            children = $table.find('tr[class*="tr-details-' + $dataRoot +'"]');

        if (children.length) {
            //ẩn div base
            children.hide();
            //xét sự kiện click trên các dòng
            $row.on('click', function (e) {
                //debugger;
                var $target = $(e.target);
                //lấy ra phần tử cha bao bên ngoài
                var $parent = $target.closest('tr');
                //kiểm tra xem có chứa class child-none hay k để ẩn/hiện div chứa các product base
                if ($parent.hasClass('child-none')) {
                    $parent
                        .removeClass('child-none')
                        .addClass('child-show');
                    children.show();
                } else {
                    $parent
                        .removeClass('child-show')
                        .addClass('child-none');

                    children.hide();
                }
            });
        }
        
    });
    
    reverseHide = function (table, element) {
        var
            $element = $(element),
            id = $element.data('id'),
            children = table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            children.each(function (i, e) {
                reverseHide(table, e);
            });

            //$element
            //    .find('.glyphicon-chevron-down')
            //    .removeClass('glyphicon-chevron-down')
            //    .addClass('glyphicon-chevron-right');

            children.hide();
        }
    };
};