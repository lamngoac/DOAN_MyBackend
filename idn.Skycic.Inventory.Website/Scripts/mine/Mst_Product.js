var ListDataInformation = [];
var ListDataAttribute = [];

function Mst_Product() {
    this.ActionType = '';
    this.Image_Message = '';
    this.Confirm_Message = '';
    this.ajaxSettings = function () {
        var Type = '';
        var DataType = '';
        var Url = '';
    };
    this.formatNumber = function () {
        //$('.UPBuy').number(true, 2);
        //$('.UPSell').number(true, 2);
        //$('.QtyMaxSt').number(true, 2);
        //$('.QtyMinSt').number(true, 2);
        //$('.QtyEffSt').number(true, 2);
        //$('.fieldNumber').number(true, 2);
            
          var tableName = 'Mst_Product';
            var upBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPBuy');
            var upSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
            var qtyMaxStFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'QtyMaxSt');
            var qtyMinStFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'QtyMinSt');
            var qtyEffStFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'QtyEffSt');
       

        var tableName1 = 'Prd_BOM';
        var fieldNumberFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName1, 'Qty');

        // số lượng
        var QtyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName1, 'Qty');
                
        $('.UPBuy').number(true, upBuyFormat);
        $('.UPSell').number(true, upSellFormat);
        $('.QtyMaxSt').number(true, qtyMaxStFormat);
        $('.QtyMinSt').number(true, qtyMinStFormat);
        $('.QtyEffSt').number(true, qtyEffStFormat);
         $('.fieldNumber').number(true, fieldNumberFormat);



        //số lượng 

        $('.bom-qty').number(true, QtyFormat);
        //Giá bán - BOM Hàng thành phần
        $('.bom-upsell').number(true, upSellFormat);
        //Giá mua - BOM Hàng thành phần
        $('.bom-upbuy').number(true, upBuyFormat);
        //Tổng giá mua - BOM Hàng thành phần
        $('.bom-sum-buy-amount').number(true, upBuyFormat);
        //Tổng giá bán - BOM Hàng thành phần
        $('.bom-sum-sell-amount').number(true, upSellFormat);


    };
    this.checkForm = function () {
        var productCodeUser = commonUtils.returnValueTextOrNull('#ProductCodeUser');
        var productName = commonUtils.returnValueTextOrNull('#ProductName');
        var prdGrpName = commonUtils.returnValueTextOrNull('#ProductGrpName');
        var brandName = commonUtils.returnValueTextOrNull('#BrandName');
        var productType = commonUtils.returnValueText('#ProductType');
        //debugger;

        var listError = [];
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ProductType', 'has-error-fix', 'Chưa chọn loại hàng!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ProductCodeUser', 'has-error-fix', 'Mã hàng không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ProductName', 'has-error-fix', 'Tên hàng không được trống!');
        listError = commonUtils.checkElementIsNullOrEmpty_AddListError(listError, '#ProductGrpCode', 'has-error-fix', 'Chưa chọn nhóm hàng!');

        //listError = commonUtils.checkElementAddListErrorCode(listError, '#ProductCodeUser', 'has-error-fix', 'Mã hàng không được chứa ký tự đặc biệt!', productCodeUser);
        //listError = commonUtils.checkElementAddListErrorCode(listError, '#ProductGrpName', 'has-error-fix', 'Nhóm hàng không được chứa ký tự đặc biệt!', prdGrpName);

        var rowsgetbom = $("tbody#table-tbodyID.GetPrd tr.trdata").length;
        if (rowsgetbom > 0) {
            var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
            if (trArr !== null && trArr.length > 0) {
                for (var i = 0; i < trArr.length; i++) {
                    var trCur = trArr[i];
                    if (trCur !== null && trCur !== undefined) {
                        var idx = $(trCur).attr('idx');
                        var temDtlCur = {};
                        //debugger;
                        temDtlCur.ProductType = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].ProductType"]').val();
                        temDtlCur.ProductName = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].ProductName"]').val();
                        if (temDtlCur.ProductType === productType && productType === "COMBO") {
                            listError = commonUtils.checkElementIsSame_AddListError(listError, '#ProductType', 'has-error-fix', 'Loại sản phẩm là combo thì hàng thành phần không chứa combo ' + temDtlCur.ProductName + '!');
                        }
                    }
                }
            }
        }
        //debugger;
        if (productType == "COMBO" && rowsgetbom < 1) {
            listError = commonUtils.checkElementIsSame_AddListError(listError, '#ProductType', 'has-error-fix', 'Danh sách hàng thành phần trống!');
        }
        if (commonUtils.checkElementExists('#FlagSerial')) {
            var flagserial = '0';
            var inputFlagActive = $('#FlagSerial');
            if (inputFlagActive !== undefined && inputFlagActive !== null) {
                if (inputFlagActive.prop('checked')) {
                    flagserial = '1';
                }
            }
        }
        let trArrunit = $('#tbody-unit');
        if (trArrunit.find('tr').length !== null && trArrunit.find('tr').length > 0 && flagserial === "1") {
            listError = commonUtils.checkElementIsSame_AddListError(listError, '#tbody-unit', 'has-error-fix', 'Cờ quản lý Serial không có đơn vị quy đổi!');

        }



        //check thông tin bổ sung
        var rows = $("#table-information tr.trdata").length;
        if (rows > 0) {
            $('#table-information tr.trdata').each(function () {
                var trArr = $(this);
                var select = $(trArr).find('td.td-select select.select-information ');

                if (select !== undefined && select !== null) {
                    var attributeCode = $(select).val();
                    if (!commonUtils.isNullOrEmpty(attributeCode)) {

                    } else {

                        objToastr = { ToastrType: 'error', ToastrMessage: 'Chưa chọn thông tin bổ sung' };
                        listError.push(objToastr);

                        $(select).focus();
                    }
                }
            })


        }


        //check thuộc tính
        var rows = $('tbody#tbody-attribute tr.trdata').length;
        if (rows > 0) {
            $('tbody#tbody-attribute tr.trdata').each(function () {
                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-select select.select-attribute');
                    if (select !== undefined && select !== null) {
                        var attributeCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(attributeCode)) {

                        } else {
                            var listToastr = [];
                            objToastr = { ToastrType: 'error', ToastrMessage: 'Chưa chọn thuộc tính' };
                            listError.push(objToastr);

                            $(select).focus();
                        }
                    }


                }
            });
        }

        if (listError !== undefined && listError !== null && listError.length > 0) {
            commonUtils.showToastr(listError);
            return false;
        }
        return true;
    };
    //this.showPopupWarning = function () {
    //    ShowPopupWarning();
    //};
    this.change_Element = function (thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {
                $(thiz).attr('edit', '1');
            }, 100);
        }
    };
    this.blur_Element = function (thiz) {

        if (thiz !== undefined && thiz !== null) {
            $(thiz).attr('edit', '0');
            if (commonUtils.checkElementExists(thiz)) {
                var productCodeUser = commonUtils.returnValueText(thiz);
                if (!commonUtils.isNullOrEmpty(productCodeUser)) {
                    var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                    if (rows > 0) {
                        var id = $(thiz).attr('id');
                        update_Product_Info(id);
                    } else {
                        gen_Products();
                    }
                } else {
                    // không thực hiện hành động nào
                }
            }
        }
    };

    function update_Product_Info(id) {
        // cập nhật mã sản phẩm
        // cập nhật giá mua
        // cập nhật giá bán
        // cập nhật đơn vị cơ bản
        var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
        if (rows > 0) {
            var productCodeUser = commonUtils.returnValueText('#ProductCodeUser');
            var uPBuy = commonUtils.returnValueTextOrNull('#UPBuy');
            var uPSell = commonUtils.returnValueTextOrNull('#UPSell');
            //var productBarCode = commonUtils.returnValueText('#ProductBarCode');
            //var productName = commonUtils.returnValueText('#ProductName');
            if (!commonUtils.isNullOrEmpty(id)) {

                var unitCode = '';
                var flagSell_Root = '0';
                var flagBuy_Root = '0';
                if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('Unit_Root')) {
                    unitCode = commonUtils.returnValueTextOrNull('#Unit_Root');
                } else if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('FlagSell_Root')) {
                    if (commonUtils.checkElementExists('#FlagSell_Root')) {
                        var inputFlagSell_Root = $('#FlagSell_Root');
                        if (inputFlagSell_Root !== undefined && inputFlagSell_Root !== null) {
                            if (inputFlagSell_Root.prop('checked')) {
                                flagSell_Root = '1';
                            }
                        }
                    }
                }
                else if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('FlagSell_Root')) {
                    if (commonUtils.checkElementExists('#FlagBuy_Root')) {
                        var inputFlagBuy_Root = $('#FlagBuy_Root');
                        if (inputFlagBuy_Root !== undefined && inputFlagBuy_Root !== null) {
                            if (inputFlagBuy_Root.prop('checked')) {
                                flagBuy_Root = '1';
                            }
                        }
                    }
                }
                $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').each(function () {
                    var tr = $(this);
                    if (tr !== undefined && tr !== null) {
                        var productCodeUserSub = productCodeUser;
                        var idx = $(tr).attr('idx');
                        if (idx !== '0') {
                            productCodeUserSub += ('-' + idx);
                        }
                        var productLevelSys = $(tr).attr('product-level-sys');
                        if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('ProductCodeUser')) {
                            // cập nhật mã sản phẩm

                            //$(tr).find('input[name="List_Mst_Product[' + idx + '].ProductName"]').val(productName);
                            $(tr).find('input[name="List_Mst_Product[' + idx + '].ProductCodeUser"]').val(productCodeUserSub);
                            $(tr).find('input[name="List_Mst_Product[' + idx + '].ProductBarCode"]').val(productCodeUserSub);
                            //$(tr).find('input[name="List_Mst_Product[' + idx + '].ProductBarCode"]').val(productBarCode);
                        }
                        else if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('UPBuy')) {
                            // cập nhật giá mua cho sản phẩm root hoặc base
                            if (!commonUtils.isNullOrEmpty(productLevelSys)) {
                                if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') || commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPBuy"]').val(uPBuy);
                                }
                            }
                        }
                        else if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('UPSell')) {
                            // cập nhật giá bán cho sản phẩm root hoặc base
                            if (!commonUtils.isNullOrEmpty(productLevelSys)) {
                                if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') || commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPSell"]').val(uPSell);
                                }
                            }
                        }
                        else if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('Unit_Root')) {
                            // cập nhật đơn vị cơ bản cho sản phẩm root hoặc base
                            if (!commonUtils.isNullOrEmpty(productLevelSys)) {
                                if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') || commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UnitCode"]').val(unitCode);
                                }
                            }
                        }
                        else if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('FlagSell_Root')) {
                            // cập nhật cờ được bán cho sản phẩm root hoặc base
                            if (!commonUtils.isNullOrEmpty(productLevelSys)) {
                                if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') || commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].CanBeSold"]').val(flagSell_Root);
                                }
                            }
                        }
                        else if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('FlagBuy_Root')) {
                            // cập nhật cờ được mua cho sản phẩm root hoặc base
                            if (!commonUtils.isNullOrEmpty(productLevelSys)) {
                                if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') || commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].CanBePurchased"]').val(flagBuy_Root);
                                }
                            }
                        }
                    }
                });
            }
        }
    }

    function update_Product_Unit(thiz, productLevel) {
        if (thiz !== undefined && thiz !== null) {
            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
            if (rows > 0) {
                $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').each(function () {
                    var tr = $(this);
                    if (tr !== undefined && tr !== null) {
                        var idx = $(tr).attr('idx');
                        var productLevelSys = $(tr).attr('product-level-sys');
                        //if (!commonUtils.isNullOrEmpty(productLevel)) {

                        //} else {

                        //}
                        $(tr).find('input[name="List_Mst_Product[' + idx + '].ProductName"]').val(productName);
                        $(tr).find('input[name="List_Mst_Product[' + idx + '].ProductCodeUser"]').val(productCodeUser);
                        $(tr).find('input[name="List_Mst_Product[' + idx + '].ProductBarCode"]').val(productBarCode);
                    }
                });
            }
        }
    }

    // Start Unit
    this.change_UnitName_Root = function (thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {
                $(thiz).attr('edit', '1');
                var _value = $(thiz).val();
                var rows = $('tbody#tbody-unit tr.trdata').length;
                if (rows > 0) {
                    if (commonUtils.isNullOrEmpty(_value)) {
                        var listToastr = [];
                        objToastr = { ToastrType: 'error', ToastrMessage: 'Vui lòng nhập đơn vị cơ bản' };
                        listToastr.push(objToastr);
                        commonUtils.showToastr(listToastr);
                        var id = $(thiz).attr('id');
                        commonUtils.setFocus(id);
                    }
                }


            }, 100);
        }
    };

    this.blur_UnitName_Root = function (thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {
                $(thiz).attr('edit', '0');
                var id = $(thiz).attr('id');
                var _value = $(thiz).val();
                var rows = $('tbody#tbody-unit tr.trdata').length;
                if (rows > 0) {
                    if (commonUtils.isNullOrEmpty(_value)) {
                        var listToastr = [];
                        objToastr = { ToastrType: 'error', ToastrMessage: 'Vui lòng nhập đơn vị cơ bản' };
                        listToastr.push(objToastr);
                        commonUtils.showToastr(listToastr);

                        commonUtils.setFocus(id);
                    } else {
                        if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('Unit_Root')) {
                            var tb = $('#divListOfTheSameTypeItems').attr('tb');
                            if (tb === 'attribute') {
                                // chưa gen products theo đơn vị cơ bản
                                // => gen products
                                gen_Products();
                            } else {
                                update_Product_Info(id);
                            }
                        } else {
                            update_Product_Info(id);
                        }

                    }
                } else {
                    if (!commonUtils.isNullOrEmpty(_value)) {
                        if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('Unit_Root')) {
                            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                            if (rows > 0) {
                                update_Product_Info(id);
                            } else {
                                gen_Products();
                            }
                            //var tb = $('#divListOfTheSameTypeItems').attr('tb');
                            //if (tb === 'attribute') {
                            //    // chưa gen products theo đơn vị cơ bản
                            //    // => gen products
                            //    gen_Products();
                            //} else {
                            //    update_Product_Info(id);
                            //}
                        } else {
                            update_Product_Info(id);
                        }
                    } else {
                        var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                        if (rows > 0) {
                            var listToastr = [];
                            objToastr = { ToastrType: 'error', ToastrMessage: 'Vui lòng nhập đơn vị cơ bản' };
                            listToastr.push(objToastr);
                            commonUtils.showToastr(listToastr);

                            commonUtils.setFocus(id);
                        }
                    }
                }

            }, 100);
        }
    };

    this.change_FlagSell_Root = function (thiz) {
        if (thiz !== undefined && thiz !== null) {
            var flagCanBeSold = '0';
            if ($(thiz).prop('checked')) {
                flagCanBeSold = '1';
                $(thiz).val('1');
            }
            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
            if (rows > 0) {

                $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').each(function () {
                    var tr = $(this);
                    if (tr !== undefined && tr !== null) {
                        var idx = $(tr).attr('idx');
                        var productLevelSys = $(tr).attr('product-level-sys');
                        // cập nhật cờ được bán cho sản phẩm root hoặc base
                        if (!commonUtils.isNullOrEmpty(productLevelSys)) {
                            if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') || commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                                $(tr).find('input[name="List_Mst_Product[' + idx + '].CanBeSold"]').val(flagCanBeSold);
                            }
                        }
                    }
                });

            }
        }
    };
    this.change_FlagBuy_Root = function (thiz) {
        if (thiz !== undefined && thiz !== null) {
            var flagCanBePurchased = '0';
            if ($(thiz).prop('checked')) {
                flagCanBePurchased = '1';
                $(thiz).val('1');
            }
            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
            if (rows > 0) {

                $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').each(function () {
                    var tr = $(this);
                    if (tr !== undefined && tr !== null) {
                        var idx = $(tr).attr('idx');
                        var productLevelSys = $(tr).attr('product-level-sys');
                        if (!commonUtils.isNullOrEmpty(productLevelSys)) {
                            if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') || commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                                $(tr).find('input[name="List_Mst_Product[' + idx + '].CanBePurchased"]').val(flagCanBePurchased);
                            }
                        }
                    }
                });

            }
        }
    };

    this.addRow_Unit = function () {
        var unitName = commonUtils.returnValueText('#Unit_Root');
        if (commonUtils.isNullOrEmpty(unitName)) {
            var listToastr = [];
            objToastr = { ToastrType: 'error', ToastrMessage: 'Chưa nhập đơn vị cơ bản' };
            listToastr.push(objToastr);
            commonUtils.showToastr(listToastr);
        } else {
            if (commonUtils.checkElementExists('#tbody-unit')) {
                commonUtils.removeClassCss('table#table-unit thead.display-none', 'display-none');
                var $tbody = $('#tbody-unit');
                var buyPrice = commonUtils.returnValueFloat('#UPBuy');
                var sellPrice = commonUtils.returnValueFloat('#UPSell');

                var exQty = 1.0;

                var date = new Date();
                var randomCur = date.getTime();
                var jqnumber = 'jqnumber' + randomCur;
                var exQtyCss = 'extqty-' + jqnumber;
                var buyPriceCss = 'buyprice-' + jqnumber;
                var sellPriceCss = 'sellprice-' + jqnumber;
                var date = new Date();
                var randomCur = date.getTime();
                var $tr = $('<tr>');
                $tr.attr('class', 'trdata');
                $tr.attr('id', randomCur);
                $tr.attr('idx', '');
                $tr.attr('tr-new', '1'); // 1: là row vừa mới tạo => chưa gen products; 0: rows đã được tạo dùng để cập nhật lại lưới hàng hóa
                var $tdEvent = $('<td>');
                var $tdUnitName = $('<td>'); // Tên đơn vị
                var $tdExQty = $('<td>'); // Số lượng quy đổi
                var $tdBuyPrice = $('<td>'); // Giá mua
                var $tdSellPrice = $('<td>'); // Giá bán
                var $tdCanBeSold = $('<td>'); // Được bán
                var $tdCanBePurchased = $('<td>'); // Được mua
                // td1: thêm nút xóa
                $tdEvent.attr('class', 'cell-25 td-event-delete');
                var $aDelete = $('<a>');
                $aDelete.attr('href', 'javascript:;');
                $aDelete.attr('title', 'Xóa');
                //$aDelete.attr('class', 'btn btn-nc-button');
                $aDelete.click(function (e) {
                    deleteRow_Unit($aDelete);
                });

                var $iDelete = $('<i>');
                $iDelete.attr('class', 'fas fa-trash-alt');
                $aDelete.append($iDelete);
                $tdEvent.append($aDelete);
                // td2: tên đơn vị
                $tdUnitName.attr('class', 'td-unit-name');
                var $divUnitName = $('<div>');
                $divUnitName.attr('class', 'form-group no-margin div-unit-name');
                var $inputUnitName = $('<input>');
                $inputUnitName.attr('type', 'text');
                $inputUnitName.attr('class', 'col-12 input-unit-name');
                $inputUnitName.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                $inputUnitName.change(function (e) {
                    change_UnitName($inputUnitName);
                });
                $inputUnitName.blur(function (e) {
                    blur_UnitName($inputUnitName);
                });
                $divUnitName.append($inputUnitName);
                $tdUnitName.append($divUnitName);
                // td3: số lượng quy đổi
                $tdExQty.attr('class', 'td-ex-qty');
                var $divExtQty = $('<div>');
                $divExtQty.attr('class', 'form-group no-margin div-ex-qty');
                var $inputExtQty = $('<input>');
                $inputExtQty.attr('type', 'text');
                $inputExtQty.attr('class', 'col-12 text-right input-ex-qty ' + exQtyCss);
                $inputExtQty.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                $inputExtQty.attr('value', exQty);
                $inputExtQty.change(function (e) {
                    change_ExtQty($inputExtQty);
                });
                $inputExtQty.blur(function (e) {
                    blur_ExtQty($inputExtQty);
                });
                $divExtQty.append($inputExtQty);
                $tdExQty.append($divExtQty);
                // td4: giá mua
                $tdBuyPrice.attr('class', 'td-buy-price');
                var $divBuyPrice = $('<div>');
                $divBuyPrice.attr('class', 'form-group no-margin div-buy-price');
                var $inputBuyPrice = $('<input>');
                $inputBuyPrice.attr('type', 'text');
                $inputBuyPrice.attr('class', 'col-12 text-right input-buy-price ' + buyPriceCss);
                $inputBuyPrice.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                //$inputBuyPrice.attr('value', exQty * buyPrice);
                $inputBuyPrice.attr('value', '0');
                $inputBuyPrice.change(function (e) {
                    change_BuyPrice($inputBuyPrice);
                });
                $inputBuyPrice.blur(function (e) {
                    blur_BuyPrice($inputBuyPrice);
                });
                $divBuyPrice.append($inputBuyPrice);
                $tdBuyPrice.append($divBuyPrice);
                // td5: giá bán
                $tdSellPrice.attr('class', 'td-sell-price');
                var $divSellPrice = $('<div>');
                $divSellPrice.attr('class', 'form-group no-margin div-sell-price');
                var $inputSellPrice = $('<input>');
                $inputSellPrice.attr('type', 'text');
                $inputSellPrice.attr('class', 'col-12 text-right input-sell-price ' + sellPriceCss);
                $inputSellPrice.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                //$inputSellPrice.attr('value', exQty * sellPrice);
                $inputSellPrice.attr('value', '0');
                $inputSellPrice.change(function (e) {
                    change_SellPrice($inputSellPrice);
                });
                $inputSellPrice.blur(function (e) {
                    blur_SellPrice($inputSellPrice);
                });
                $divSellPrice.append($inputSellPrice);
                $tdSellPrice.append($divSellPrice);
                // td6: được bán
                $tdCanBeSold.attr('class', 'td-can-be-sold');
                var $inputCanBeSold = $('<input>');
                $inputCanBeSold.attr('type', 'checkbox');
                $inputCanBeSold.attr('class', 'input-can-be-sold');
                if ($('input[name="FlagSell_Root"]:checked').length > 0) {
                    $inputCanBeSold.attr('checked', 'checked');
                }
                $inputCanBeSold.change(function (e) {
                    change_CanBeSold($inputCanBeSold);
                });
                var $spanCanBeSold = $('<span>');
                $spanCanBeSold.attr('class', '');
                $spanCanBeSold.text('   Can be sold');
                $tdCanBeSold.append($inputCanBeSold);
                $tdCanBeSold.append($spanCanBeSold);
                // td7: được mua
                $tdCanBePurchased.attr('class', 'td-can-be-purchased');
                var $inputCanBePurchased = $('<input>');
                $inputCanBePurchased.attr('type', 'checkbox');
                $inputCanBePurchased.attr('class', 'input-can-be-purchased');
                if ($('input[name="FlagBuy_Root"]:checked').length > 0) {
                    $inputCanBePurchased.attr('checked', 'checked');
                }
                $inputCanBePurchased.change(function (e) {
                    change_CanBePurchased($inputCanBePurchased);
                });
                var $spanCanBePurchased = $('<span>');
                $spanCanBePurchased.attr('class', '');
                $spanCanBePurchased.text('   Can be purchased');
                $tdCanBePurchased.append($inputCanBePurchased);
                $tdCanBePurchased.append($spanCanBePurchased);
                //
                $tr.append($tdEvent);
                $tr.append($tdUnitName);
                $tr.append($tdExQty);
                $tr.append($tdBuyPrice);
                $tr.append($tdSellPrice);
                $tr.append($tdCanBeSold);
                $tr.append($tdCanBePurchased);
                $tbody.append($tr);
                commonUtils.updateTableTrNotShowIdx($('#tbody-unit tr.trdata'), true);

                //format number
                var tableName = 'Mst_Product';
                var tableName1 = 'Prd_BOM';
                var upSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
                var upBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPBuy');
                var qtyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName1, 'Qty');


                $('.' + exQtyCss).number(true, qtyFormat);
                $('.' + buyPriceCss).number(true, upBuyFormat);
                $('.' + sellPriceCss).number(true, upSellFormat);

            };
        }
    };
    this.addRow_Unit_Update = function () {
        var unitName = commonUtils.returnValueText('#Unit_Root');
        if (commonUtils.isNullOrEmpty(unitName)) {
            var listToastr = [];
            objToastr = { ToastrType: 'error', ToastrMessage: 'Chưa nhập đơn vị cơ bản' };
            listToastr.push(objToastr);
            commonUtils.showToastr(listToastr);
        } else {
            if (commonUtils.checkElementExists('#tbody-unit')) {
                commonUtils.removeClassCss('table#table-unit thead.display-none', 'display-none');
                var $tbody = $('#tbody-unit');
                var exQty = 1.0;
                var date = new Date();
                var randomCur = date.getTime();
                var jqnumber = 'jqnumber' + randomCur;
                var exQtyCss = 'extqty-' + jqnumber;
                var buyPriceCss = 'buyprice-' + jqnumber;
                var sellPriceCss = 'sellprice-' + jqnumber;
                var date = new Date();
                var randomCur = date.getTime();
                var $tr = $('<tr>');
                $tr.attr('class', 'trdata');
                $tr.attr('id', randomCur);
                $tr.attr('idx', '');
                $tr.attr('tr-new', '1'); // 1: là row vừa mới tạo => chưa gen products; 0: rows đã được tạo dùng để cập nhật lại lưới hàng hóa
                var $tdEvent = $('<td>');
                var $tdUnitName = $('<td>'); // Tên đơn vị
                var $tdExQty = $('<td>'); // Số lượng quy đổi
                var $tdBuyPrice = $('<td>'); // Giá mua
                var $tdSellPrice = $('<td>'); // Giá bán
                var $tdCanBeSold = $('<td>'); // Được bán
                var $tdCanBePurchased = $('<td>'); // Được mua
                // td1: thêm nút xóa
                $tdEvent.attr('class', 'cell-25 td-event-delete');
                var $aDelete = $('<a>');
                $aDelete.attr('href', 'javascript:;');
                $aDelete.attr('title', 'Xóa');
                //$aDelete.attr('class', 'btn btn-nc-button');
                $aDelete.click(function (e) {
                    deleteRow_Unit($(this));
                });

                var $iDelete = $('<i>');
                $iDelete.attr('class', 'fas fa-trash-alt');
                $aDelete.append($iDelete);
                $tdEvent.append($aDelete);
                // td2: tên đơn vị
                $tdUnitName.attr('class', 'td-unit-name');
                var $divUnitName = $('<div>');
                $divUnitName.attr('class', 'form-group no-margin div-unit-name');
                var $inputUnitName = $('<input>');
                $inputUnitName.attr('type', 'text');
                $inputUnitName.attr('class', 'col-12 input-unit-name');
                $inputUnitName.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                $inputUnitName.change(function (e) {
                    change_UnitName($(this));
                });
                $inputUnitName.blur(function (e) {
                    blur_UnitName_Update($(this));
                });
                $divUnitName.append($inputUnitName);
                $tdUnitName.append($divUnitName);
                // td3: số lượng quy đổi
                $tdExQty.attr('class', 'td-ex-qty');
                var $divExtQty = $('<div>');
                $divExtQty.attr('class', 'form-group no-margin div-ex-qty');
                var $inputExtQty = $('<input>');
                $inputExtQty.attr('type', 'text');
                $inputExtQty.attr('class', 'col-12 text-right input-ex-qty ' + exQtyCss);
                $inputExtQty.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                $inputExtQty.attr('value', exQty);
                $inputExtQty.change(function (e) {
                    change_ExtQty($(this));
                });
                $inputExtQty.blur(function (e) {
                    blur_ExtQty($(this));
                });
                $divExtQty.append($inputExtQty);
                $tdExQty.append($divExtQty);
                // td4: giá mua
                $tdBuyPrice.attr('class', 'td-buy-price');
                var $divBuyPrice = $('<div>');
                $divBuyPrice.attr('class', 'form-group no-margin div-buy-price');
                var $inputBuyPrice = $('<input>');
                $inputBuyPrice.attr('type', 'text');
                $inputBuyPrice.attr('class', 'col-12 text-right input-buy-price ' + buyPriceCss);
                $inputBuyPrice.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                //$inputBuyPrice.attr('value', exQty * buyPrice);
                $inputBuyPrice.attr('value', '0');
                $inputBuyPrice.change(function (e) {
                    change_BuyPrice($(this));
                });
                $inputBuyPrice.blur(function (e) {
                    blur_BuyPrice($(this));
                });
                $divBuyPrice.append($inputBuyPrice);
                $tdBuyPrice.append($divBuyPrice);
                // td5: giá bán
                $tdSellPrice.attr('class', 'td-sell-price');
                var $divSellPrice = $('<div>');
                $divSellPrice.attr('class', 'form-group no-margin div-sell-price');
                var $inputSellPrice = $('<input>');
                $inputSellPrice.attr('type', 'text');
                $inputSellPrice.attr('class', 'col-12 text-right input-sell-price ' + sellPriceCss);
                $inputSellPrice.attr('edit', '0'); // 1: đã bị thay đổi giá trị ngược lại 0: là chưa thay đổi giá trị
                //$inputSellPrice.attr('value', exQty * sellPrice);
                $inputSellPrice.attr('value', '0');
                $inputSellPrice.change(function (e) {
                    change_SellPrice($(this));
                });
                $inputSellPrice.blur(function (e) {
                    blur_SellPrice($(this));
                });
                $divSellPrice.append($inputSellPrice);
                $tdSellPrice.append($divSellPrice);
                // td6: được bán
                $tdCanBeSold.attr('class', 'td-can-be-sold');
                var $inputCanBeSold = $('<input>');
                $inputCanBeSold.attr('type', 'checkbox');
                $inputCanBeSold.attr('class', 'input-can-be-sold');
                $inputCanBeSold.change(function (e) {
                    change_CanBeSold($(this));
                });
                var $spanCanBeSold = $('<span>');
                $spanCanBeSold.attr('class', '');
                $spanCanBeSold.text('   Can be sold');
                $tdCanBeSold.append($inputCanBeSold);
                $tdCanBeSold.append($spanCanBeSold);
                // td7: được mua
                $tdCanBePurchased.attr('class', 'td-can-be-purchased');
                var $inputCanBePurchased = $('<input>');
                $inputCanBePurchased.attr('type', 'checkbox');
                $inputCanBePurchased.attr('class', 'input-can-be-purchased');
                $inputCanBePurchased.change(function (e) {
                    change_CanBePurchased($(this));
                });
                var $spanCanBePurchased = $('<span>');
                $spanCanBePurchased.attr('class', '');
                $spanCanBePurchased.text('   Can be purchased');
                $tdCanBePurchased.append($inputCanBePurchased);
                $tdCanBePurchased.append($spanCanBePurchased);
                //
                $tr.append($tdEvent);
                $tr.append($tdUnitName);
                $tr.append($tdExQty);
                $tr.append($tdBuyPrice);
                $tr.append($tdSellPrice);
                $tr.append($tdCanBeSold);
                $tr.append($tdCanBePurchased);
                $tbody.append($tr);
                commonUtils.updateTableTrNotShowIdx($('#tbody-unit tr.trdata'), true);

                //format number
                var tableName = 'Mst_Product';
                var tableName1 = 'Prd_BOM';
                var upSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
                var upBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPBuy');
                var qtyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName1, 'Qty');

                $('.' + exQtyCss).number(true, qtyFormat);
                $('.' + buyPriceCss).number(true, upBuyFormat);
                $('.' + sellPriceCss).number(true, upSellFormat);

            };
        }
    };
    function deleteRow_Unit(thiz) {
        var tr = $(thiz).parent().parent();
        if (tr !== undefined && tr !== null) {
            $(tr).remove();
        }
        // gen products
        var rows = $('tbody#tbody-unit tr.trdata').length;
        if (rows > 0) {

        } else {
            if (!$('#table-unit thead').hasClass('display-none')) {
                $('#table-unit thead').addClass('display-none');
            }
        }
        gen_Products();
    }
    function change_UnitName(thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {
                $(thiz).attr('edit', '1');
            }, 100);
        }
    }
    function blur_UnitName(thiz) {
        if (thiz !== undefined && thiz !== null) {

            var tr = $(thiz).parent().parent().parent();
            var id = $(tr).attr('id');
            var trNew = $(tr).attr('tr-new');
            if (trNew === '1') {
                // trường hợp thêm dòng mới => gen products
                gen_Products();
                $(tr).attr('tr-new', '0');
            }
            else if (trNew === '0') {
                // trường hợp này đơn vị đã được gen products
                // => chỉ cập nhật lại đơn vị
                var edit = $(thiz).attr('edit');
                if (edit === '1') {
                    $(thiz).attr('edit', '0');
                    // gen products
                    var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                    if (rows > 0) {
                        var unitCode = $(thiz).val();
                        $('.tbody-products').find('tr[product-level-sys="L2PRD"]').each(function () {

                            var tr = $(this);
                            if (tr !== undefined && tr !== null) {
                                var idx = $(tr).attr('idx');
                                var unitId = $(tr).attr('unit-id');
                                if (id === unitId) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UnitCode"]').val(unitCode);
                                }
                            }
                        });
                    }
                }
            }
        }
    }
    function change_ExtQty(thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {
                $(thiz).attr('edit', '1');
                var exQty = $(thiz).val();
                if (!commonUtils.isNullOrEmpty(exQty)) {

                } else {
                    //$(tr).find('input[name="List_Mst_Product[' + idx + '].UnitCode"]').val(unitCode);
                }
            }, 100);
        }
    }

    function blur_ExtQty(thiz) {
        if (thiz !== undefined && thiz !== null) {

            var tr = $(thiz).parent().parent().parent();
            var id = $(tr).attr('id');
            var trNew = $(tr).attr('tr-new');
            if (trNew === '1') {
                // trường hợp thêm dòng mới => gen products
                //gen_Products();
                //$(tr).attr('tr-new', '0');
            }
            else if (trNew === '0') {
                // trường hợp này đơn vị đã được gen products
                // => chỉ cập nhật lại đơn vị
                var edit = $(thiz).attr('edit');
                //if (edit === '1')
                if (true) {
                    $(thiz).attr('edit', '0');
                    // gen products
                    var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                    if (rows > 0) {
                        var exQty = $(thiz).val();
                        var uPBuy = commonUtils.returnValueTextOrNull('#UPBuy');
                        var uPSell = commonUtils.returnValueTextOrNull('#UPSell');
                        var buyPrice = null;
                        var sellPrice = null;
                        if (!commonUtils.isNullOrEmpty(exQty)) {
                            if (!commonUtils.isNullOrEmpty(uPBuy)) {
                                buyPrice = commonUtils.parseFloat(exQty) * commonUtils.parseFloat(uPBuy);
                            }
                            if (!commonUtils.isNullOrEmpty(uPSell)) {
                                sellPrice = commonUtils.parseFloat(exQty) * commonUtils.parseFloat(uPSell);
                            }
                        }

                        var trCur = $(thiz).parent().parent().parent();
                        var inputBuyPrice = $(trCur).find('input.input-buy-price');
                        if (inputBuyPrice !== undefined && inputBuyPrice !== null) {
                            $(inputBuyPrice).val(buyPrice);
                        }
                        var inputSellPrice = $(trCur).find('input.input-sell-price');
                        if (inputSellPrice !== undefined && inputSellPrice !== null) {
                            $(inputSellPrice).val(sellPrice);
                        }
                        $('.tbody-products').find('tr[product-level-sys="L2PRD"]').each(function () {
                            var tr = $(this);
                            if (tr !== undefined && tr !== null) {
                                var idx = $(tr).attr('idx');
                                var unitId = $(tr).attr('unit-id');
                                if (id === unitId) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPBuy"]').val(buyPrice);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPSell"]').val(sellPrice);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].ValConvert"]').val(exQty);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPBuy"]').number(true, 2);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPSell"]').number(true, 2);
                                }
                            }
                        });
                        $('.tbody-products').find('tr[product-level-sys="BASEPRD"]').each(function () {
                            var tr = $(this);
                            if (tr !== undefined && tr !== null) {
                                var idx = $(tr).attr('idx');
                                var unitId = $(tr).attr('unit-id');
                                if (id === unitId) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPBuy"]').val(buyPrice);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPSell"]').val(sellPrice);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].ValConvert"]').val(exQty);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPBuy"]').number(true, 2);
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPSell"]').number(true, 2);
                                }
                            }
                        });
                    }
                }
            }
        }
    }
    function change_BuyPrice(thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {
                $(thiz).attr('edit', '1');
            }, 100);
        }
    }
    function blur_BuyPrice(thiz) {
        if (thiz !== undefined && thiz !== null) {

            var tr = $(thiz).parent().parent().parent();
            var id = $(tr).attr('id');
            var trNew = $(tr).attr('tr-new');
            if (trNew === '1') {
                // trường hợp thêm dòng mới => gen products
                //gen_Products();
                //$(tr).attr('tr-new', '0');
            }
            else if (trNew === '0') {
                // trường hợp này đơn vị đã được gen products
                // => chỉ cập nhật lại đơn vị
                var edit = $(thiz).attr('edit');
                //if (edit === '1')
                if (true) {
                    $(thiz).attr('edit', '0');
                    // gen products
                    var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                    if (rows > 0) {
                        var uPBuy = $(thiz).val();
                        $('.tbody-products').find('tr[product-level-sys="L2PRD"]').each(function () {

                            var tr = $(this);
                            if (tr !== undefined && tr !== null) {
                                var idx = $(tr).attr('idx');
                                var unitId = $(tr).attr('unit-id');
                                if (id === unitId) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPBuy"]').val(uPBuy);
                                }
                            }
                        });
                    }
                }
            }
        }
    }
    function change_SellPrice(thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {
                $(thiz).attr('edit', '1');
            }, 100);
        }
    }
    function blur_SellPrice(thiz) {
        if (thiz !== undefined && thiz !== null) {

            var tr = $(thiz).parent().parent().parent();
            var id = $(tr).attr('id');
            var trNew = $(tr).attr('tr-new');
            if (trNew === '1') {
                // trường hợp thêm dòng mới => gen products
                //gen_Products();
                //$(tr).attr('tr-new', '0');
            }
            else if (trNew === '0') {
                // trường hợp này đơn vị đã được gen products
                // => chỉ cập nhật lại đơn vị
                var edit = $(thiz).attr('edit');
                //if (edit === '1')
                if (true) {
                    $(thiz).attr('edit', '0');
                    // gen products
                    var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                    if (rows > 0) {
                        var uPSell = $(thiz).val();
                        $('.tbody-products').find('tr[product-level-sys="L2PRD"]').each(function () {

                            var tr = $(this);
                            if (tr !== undefined && tr !== null) {
                                var idx = $(tr).attr('idx');
                                var unitId = $(tr).attr('unit-id');
                                if (id === unitId) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UPSell"]').val(uPSell);
                                }
                            }
                        });
                    }
                }
            }
        }
    }
    function change_CanBeSold(thiz) {
        if (thiz !== undefined && thiz !== null) {
            var flagCanBeSold = '0';
            if ($(thiz).prop('checked')) {
                flagCanBeSold = '1';
            }
            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
            if (rows > 0) {
                var trCur = $(thiz).parent().parent();
                var id = $(trCur).attr('id');
                $('.tbody-products').find('tr[product-level-sys="L2PRD"]').each(function () {

                    var tr = $(this);
                    if (tr !== undefined && tr !== null) {
                        var idx = $(tr).attr('idx');
                        var unitId = $(tr).attr('unit-id');
                        if (id === unitId) {
                            $(tr).find('input[name="List_Mst_Product[' + idx + '].CanBeSold"]').val(flagCanBeSold);
                        }
                    }
                });
            }
        }
    }
    function change_CanBePurchased(thiz) {
        if (thiz !== undefined && thiz !== null) {
            var flagCanBePurchased = '0';
            if ($(thiz).prop('checked')) {
                flagCanBePurchased = '1';
            }
            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
            if (rows > 0) {
                var trCur = $(thiz).parent().parent();
                var id = $(trCur).attr('id');
                $('.tbody-products').find('tr[product-level-sys="L2PRD"]').each(function () {

                    var tr = $(this);
                    if (tr !== undefined && tr !== null) {
                        var idx = $(tr).attr('idx');
                        var unitId = $(tr).attr('unit-id');
                        if (id === unitId) {
                            $(tr).find('input[name="List_Mst_Product[' + idx + '].CanBePurchased"]').val(flagCanBePurchased);
                        }
                    }
                });
            }
        }
    }

    // sự kiện của màn hình updata
    this.blur_UnitName_Root_Update = function (thiz) {
        if (thiz !== undefined && thiz !== null) {
            setTimeout(function () {

                $(thiz).attr('edit', '1');
                var id = $(thiz).attr('id');
                var _value = $(thiz).val();
                var rows = $('tbody#tbody-unit tr.trdata').length;
                if (rows > 0) {
                    if (commonUtils.isNullOrEmpty(_value)) {
                        var listToastr = [];
                        objToastr = { ToastrType: 'error', ToastrMessage: 'Vui lòng nhập đơn vị cơ bản' };
                        listToastr.push(objToastr);
                        commonUtils.showToastr(listToastr);

                        commonUtils.setFocus(id);
                    } else {
                        if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('Unit_Root')) {
                            var tb = $('#divListOfTheSameTypeItems').attr('tb');
                            if (tb === 'attribute') {
                                // chưa gen products theo đơn vị cơ bản
                                // => gen products
                                var _listUnit = listUnit();
                                genProducts_ByUnit(_listUnit);
                            } else {
                                update_Product_Info(id);
                            }
                        } else {
                            update_Product_Info(id);
                        }

                    }
                } else {
                    if (!commonUtils.isNullOrEmpty(_value)) {
                        if (commonUtils.toUpperCase(id) === commonUtils.toUpperCase('Unit_Root')) {
                            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                            if (rows > 0) {
                                update_Product_Info(id);
                            } else {
                                var _listUnit = listUnit();
                                genProducts_ByUnit(_listUnit);
                            }
                            //var tb = $('#divListOfTheSameTypeItems').attr('tb');
                            //if (tb === 'attribute') {
                            //    // chưa gen products theo đơn vị cơ bản
                            //    // => gen products
                            //    gen_Products();
                            //} else {
                            //    update_Product_Info(id);
                            //}
                        } else {
                            update_Product_Info(id);
                        }
                    } else {
                        var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                        if (rows > 0) {
                            var listToastr = [];
                            objToastr = { ToastrType: 'error', ToastrMessage: 'Vui lòng nhập đơn vị cơ bản' };
                            listToastr.push(objToastr);
                            commonUtils.showToastr(listToastr);

                            commonUtils.setFocus(id);
                        }
                    }
                }

            }, 100);
        }
    };
    function blur_UnitName_Update(thiz) {
        if (thiz !== undefined && thiz !== null) {

            var tr = $(thiz).parent().parent().parent();
            var id = $(tr).attr('id');
            var trNew = $(tr).attr('tr-new');
            if (trNew === '1') {
                // trường hợp thêm dòng mới => gen products
                var _listUnit = listUnit();
                genProducts_ByUnit(_listUnit);
                $(tr).attr('tr-new', '0');
            }
            else if (trNew === '0') {
                // trường hợp này đơn vị đã được gen products
                // => chỉ cập nhật lại đơn vị
                var edit = $(thiz).attr('edit');
                if (edit === '1') {
                    $(thiz).attr('edit', '0');
                    // gen products
                    var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
                    if (rows > 0) {
                        var unitCode = $(thiz).val();
                        $('.tbody-products').find('tr[product-level-sys="L2PRD"]').each(function () {

                            var tr = $(this);
                            if (tr !== undefined && tr !== null) {
                                var idx = $(tr).attr('idx');
                                var unitId = $(tr).attr('unit-id');
                                if (id === unitId) {
                                    $(tr).find('input[name="List_Mst_Product[' + idx + '].UnitCode"]').val(unitCode);
                                }
                            }
                        });
                    } else {
                        //var $tbody = $('tbody.tbody-products');
                        //$tbody.empty();
                    }
                }
            }
        }
    }
    function listUnit() {
        var _listUnit = [];
        var objUnitRoot = {
            UnitCode: commonUtils.returnValueText('#Unit_Root'),
            UnitName: commonUtils.returnValueText('#Unit_Root'),
            ExQty: 1,
            UPBuy: commonUtils.returnValueText('#UPBuy'),
            UPSell: commonUtils.returnValueText('#UPSell'),
            CanBeSold: '0', // 1: tích chọn; 0: không tích chọn // được bán
            CanBePurchased: '0', // 1: tích chọn; 0: không tích chọn // được mua
            Id: '' // id của tr
        };
        var inputCanBeSold = $('#FlagSell_Root');
        if (inputCanBeSold !== undefined && inputCanBeSold !== null) {
            if (inputCanBeSold.prop('checked')) {
                objUnitRoot.CanBeSold = '1';
            }
        }
        var inputCanBePurchased = $('#FlagBuy_Root');
        if (inputCanBePurchased !== undefined && inputCanBePurchased !== null) {
            if (inputCanBePurchased.prop('checked')) {
                objUnitRoot.CanBePurchased = '1';
            }
        }
        if (!commonUtils.isNullOrEmpty(objUnitRoot.UnitCode)) {
            _listUnit.push(objUnitRoot);
        }

        var rows = $('tbody#tbody-unit tr.trdata').length;
        var rootUnit = commonUtils.returnValueText('#Unit_Root');

        if (rows > 0) {
            $('tbody#tbody-unit tr.trdata').each(function () {
                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var unitName = '';
                    var exQty = '';
                    var uPBuy = '';
                    var uPSell = '';
                    var canBeSold = '';
                    var canBePurchased = '';
                    var id = $(tr).attr('id');

                    var tdUnitName = $(tr).find('td.td-unit-name');
                    if (tdUnitName !== undefined && tdUnitName !== null) {
                        var inputTextUnitName = $(tdUnitName).find('div.div-unit-name input.input-unit-name');
                        unitName = commonUtils.returnValue($(inputTextUnitName).val());
                        if (unitName.trim() === rootUnit.trim()) {
                            //debugger;
                            var listError = [];
                            listError = commonUtils.checkElementIsSame_AddListError(listError, '#Unit_Root', 'has-error-fix', 'Đơn vị khác không được trùng đơn vị cơ bản!');
                            if (listError !== undefined && listError !== null && listError.length > 0) {
                                commonUtils.showToastr(listError);
                                return false;
                            }
                        }
                    }
                    if (!commonUtils.isNullOrEmpty(unitName)) {
                        var tdExQty = $(tr).find('td.td-ex-qty');
                        if (tdExQty !== undefined && tdExQty !== null) {
                            var inputTextExQty = $(tdExQty).find('div.div-ex-qty input.input-ex-qty');
                            exQty = commonUtils.returnValue($(inputTextExQty).val());
                            if (commonUtils.isNullOrEmpty(exQty)) {
                                exQty = '0';
                            }
                        }

                        var tdUPBuy = $(tr).find('td.td-buy-price');
                        if (tdUPBuy !== undefined && tdUPBuy !== null) {
                            var inputTextUPBuy = $(tdUPBuy).find('div.div-buy-price input.input-buy-price');
                            uPBuy = commonUtils.returnValue($(inputTextUPBuy).val());
                            if (commonUtils.isNullOrEmpty(uPBuy)) {
                                uPBuy = '0';
                            }
                        }

                        var tdUPSell = $(tr).find('td.td-sell-price');
                        if (tdUPSell !== undefined && tdUPSell !== null) {
                            var inputTextUPSell = $(tdUPSell).find('div.div-sell-price input.input-sell-price');
                            uPSell = commonUtils.returnValue($(inputTextUPSell).val());
                            if (commonUtils.isNullOrEmpty(uPSell)) {
                                uPSell = '0';
                            }
                        }

                        var tdCanBeSold = $(tr).find('td.td-can-be-sold');
                        if (tdCanBeSold !== undefined && tdCanBeSold !== null) {
                            var inputCheckBoxCanBeSold = $(tdCanBeSold).find('input.input-can-be-sold');
                            if (inputCheckBoxCanBeSold.prop('checked')) {
                                canBeSold = '1';
                            } else {
                                canBeSold = '0';
                            }
                        }

                        var tdCanBePurchased = $(tr).find('td.td-can-be-purchased');
                        if (tdCanBePurchased !== undefined && tdCanBePurchased !== null) {
                            var inputCheckBoxCanBePurchased = $(tdCanBePurchased).find('input.input-can-be-purchased');
                            if (inputCheckBoxCanBePurchased.prop('checked')) {
                                canBePurchased = '1';
                            } else {
                                canBePurchased = '0';
                            }
                        }

                        var objUnit = {
                            UnitCode: unitName,
                            UnitName: unitName,
                            ExQty: exQty,
                            UPBuy: uPBuy,
                            UPSell: uPSell,
                            CanBeSold: canBeSold, // 1: tích chọn; 0: không tích chọn // được bán
                            CanBePurchased: canBePurchased, // 1: tích chọn; 0: không tích chọn // được mua
                            Id: id
                        };

                        _listUnit.push(objUnit);
                    }
                }
            });
        }


        return _listUnit;
    }
    // End Unit
    // Start Attribute
    this.addRow_Attribute = function () {
        debugger
        if (commonUtils.checkElementExists('#tbody-attribute')) {
            var $tbody = $('#tbody-attribute');
            var $tr = $('<tr>');
            $tr.attr('class', 'trdata');
            $tr.attr('idx', '');
            var $tdEvent = $('<td>');
            var $tdSelect = $('<td>');
            var $tdInputText = $('<td>');
            // td1: thêm nút xóa
            $tdEvent.attr('class', 'cell-25 td-event-delete');
            var $aDelete = $('<a>');
            $aDelete.attr('href', 'javascript:;');
            $aDelete.attr('title', 'Xóa');
            //$aDelete.attr('class', 'btn btn-nc-button');
            $aDelete.click(function (e) {
                deleteRow_Attribute($(this));
            });

            var $iDelete = $('<i>');
            $iDelete.attr('class', 'fas fa-trash-alt');
            $aDelete.append($iDelete);
            $tdEvent.append($aDelete);

            // td2: thuộc tính
            $tdSelect.attr('class', 'td-select');
            var $divSelect = $('<div>');
            $divSelect.attr('class', 'form-group no-margin div-select-attribute');
            var $select = $('<select>');
            $select.attr('class', 'col-10 select-attribute');
            $select.attr('select-value', '');
            $select.attr('showpopup-control-adddata', '#ShowPopupAddAttribute');
            $select.attr('showpopup-control-updatedata', '#ShowPopupUpdateAttribute');
            $select.change(function (e) {
                change_Attribute($select);
            });

            var $optgroup = $('<optgroup>');

            var $optionFirst = $('<option>');
            //$optionFirst.attr('disabled', 'disabled');
            $optionFirst.attr('opt', 'null');
            $optionFirst.attr('value', '');
            $optionFirst.text('--Lựa chọn--');
            $optgroup.append($optionFirst);
            debugger
            if (ListDataAttribute !== undefined && ListDataAttribute !== null) {
                //debugger
                for (var i = 0; i < ListDataAttribute.length; i++) {
                    var item = ListDataAttribute[i];
                    var attributeCode = commonUtils.returnValue(item.AttributeCode);
                    var attributeName = commonUtils.returnValue(item.AttributeName);

                    var $option = $('<option>');
                    $option.attr('value', attributeCode);
                    $option.text(attributeName);

                    $optgroup.append($option);
                }
            }

            $select.append($optgroup);

            var $optionadd = $('<option>');
            $optionadd.attr('value', "ADDDATA");
            //$optionadd.text("Thêm/Sửa");
            $select.append($optionadd);

            $divSelect.append($select);
            // nút thêm thuộc tính
            var $aAddAttr = $('<a>');
            $aAddAttr.attr('class', 'btn btn-nc-button btn-add-or-edit');
            $aAddAttr.attr('title', 'Thêm thuộc tính');
            $aAddAttr.click(function (e) {
                showPopup_Attribute($aAddAttr);
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
            $inputText.attr('class', 'col-12 value-attribute');
            $inputText.attr('data-role', 'tagsinput');
            $inputText.attr('placeholder', 'Enter an input and press Enter');
            $inputText.change(function (e) {
                change_InputTextAttribute($inputText);
            });
            $divInputText.append($inputText);
            $tdInputText.append($divInputText);


            $tr.append($tdEvent);
            $tr.append($tdSelect);
            $tr.append($tdInputText);
            $tbody.append($tr);

            $('.select-attribute').select2();
            $(".value-attribute").tagsinput();

            commonUtils.updateTableTrNotShowIdx($('#tbody-attribute tr.trdata'), true);
        }
    };
    function deleteRow_Attribute(thiz) {
        var tr = $(thiz).parent().parent();
        if (tr !== undefined && tr !== null) {
            $(tr).remove();
        }
        var rows = $('tbody#tbody-attribute tr.trdata').length;
        if (rows > 0) {
            gen_Products();
        } else {
            var $tbody = $('#tbody-attribute');
            $tbody.empty();
            // tạm thời set hàng hóa null
            var $div = $('#divListOfTheSameTypeItems');
            gen_Products();
        }
        // gen products
    };
    function deleteRow_Attribute_update(thiz) {
        var tr = $(thiz).parent().parent();
        if (tr !== undefined && tr !== null) {
            $(tr).remove();
        }
        var rows = $('tbody#tbody-attribute tr.trdata').length;
        if (rows > 0) {
            var _listUnit = listUnit();
            genProducts_ByUnit(_listUnit);
            //gen_Products();
        } else {
            var $tbody = $('#tbody-attribute');
            $tbody.empty();
            // tạm thời set hàng hóa null
            var $div = $('#divListOfTheSameTypeItems');
            //$div.empty();
        }
        // gen products
    };
    function showPopup_Attribute(thiz) {
        var trIdx = $(thiz).closest('tr.trdata').attr('idx');

        ShowPopupAddAttribute(trIdx);
    };
    function showPopup_Edit_Attribute(thiz) {
        var attributeCode = $(thiz).siblings('select.select-attribute').attr('select-value');
        var attributeName = $(thiz).siblings('select.select-attribute').find('option[value = "' + attributeCode + '"]').text();
        // Show popup update attribute
        ShowPopupUpdateAttribute(attributeCode, attributeName);
    };
    function change_Attribute(thiz) {
        if (thiz !== undefined && thiz !== null) {
            var $divSelectAttribute = $(thiz).parent();
            var _value = commonUtils.returnValue($(thiz).val());
            if (!commonUtils.isNullOrEmpty(_value)) {
                // gen products
                var isCheckExists = false;
                var tr = $(thiz).parent().parent().parent();
                var idx = $(tr).attr('idx');
                var rows = $('tbody#tbody-attribute tr.trdata').length;
                if (rows > 0) {
                    $('tbody#tbody-attribute tr.trdata').each(function () {
                        var trCur = $(this);
                        if (trCur !== undefined && trCur !== null) {
                            var idxCur = $(trCur).attr('idx');
                            if (idx !== idxCur) {
                                var select = $(trCur).find('td.td-select select.select-attribute');
                                if (select !== undefined && select !== null) {
                                    var attributeCode = commonUtils.returnValue($(select).val());
                                    if (attributeCode === _value) {
                                        var listToastr = [];
                                        objToastr = { ToastrType: 'error', ToastrMessage: 'Thuộc tính đã tồn tại trong bảng hàng hóa' };
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
                    var $aEditAttr = $divSelectAttribute.find('a.btn-add-or-edit');
                    if ($aEditAttr !== undefined && $aEditAttr !== null) {
                        $aEditAttr.empty();
                        $aEditAttr.attr('title', 'Sửa thuộc tính');
                        $aEditAttr.off('click');
                        $aEditAttr.on('click', function (e) {
                            showPopup_Edit_Attribute($aEditAttr);
                        });
                        var $iEditAttr = $('<i>');
                        $iEditAttr.attr('class', 'fas fa-pencil-alt');
                        $aEditAttr.append($iEditAttr);
                    }
                    var $option = $(thiz).find('option[opt="null"]');
                    if ($option !== undefined && $option !== null) {
                        $option.attr('disabled', 'disabled');
                    }

                    var inputText = $(tr).find('td.td-input-text input.value-attribute');
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
        }

    }
    function change_InputTextAttribute(thiz) {
        //debugger;
        if (thiz !== undefined && thiz !== null) {
            var _value = commonUtils.returnValue($(thiz).val());
            if (!commonUtils.isNullOrEmpty(_value)) {

                var tr = $(thiz).parent().parent().parent();
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-select select.select-attribute');
                    if (select !== undefined && select !== null) {
                        var attributeCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(attributeCode)) {
                            gen_Products();
                        } else {
                            var listToastr = [];
                            objToastr = { ToastrType: 'error', ToastrMessage: 'Chưa chọn thuộc tính' };
                            listToastr.push(objToastr);
                            commonUtils.showToastr(listToastr);
                            $(select).focus();
                        }
                    }
                }
            } else {
                gen_Products();
            }
        }
    }

    function listAttribute() {
        var _listAttribute = [];
        //var objAttribute = {
        //    AttributeCode: '',
        //    ListData: [],
        //};
        var rows = $('tbody#tbody-attribute tr.trdata').length;
        if (rows > 0) {
            $('tbody#tbody-attribute tr.trdata').each(function () {
                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-select select.select-attribute');
                    if (select !== undefined && select !== null) {
                        var attributeCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(attributeCode)) {
                            // trường hợp có chọn thuộc tính
                            // kiểm tra có giá trị của thuộc tính không
                            var inputText = $(tr).find('td.td-input-text input.value-attribute');
                            if (inputText !== undefined && inputText !== null) {
                                var _value = $(inputText).val();
                                if (!commonUtils.isNullOrEmpty(_value)) {
                                    var arrData = _value.split(',');
                                    if (arrData !== undefined && arrData !== null && arrData.length > 0) {
                                        var lisData = [];
                                        for (var i = 0; i < arrData.length; i++) {
                                            var _val = commonUtils.returnValue(arrData[i]);
                                            if (!commonUtils.isNullOrEmpty(_val)) {
                                                lisData.push(_val);
                                            }
                                        }
                                        if (lisData !== undefined && lisData !== null && lisData.length > 0) {
                                            var objAttribute = {
                                                AttributeCode: attributeCode,
                                                ListData: lisData
                                            };
                                            _listAttribute.push(objAttribute);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            });
        }
        return _listAttribute;
    }

    function listAttributeNew() {
        var _listAttribute = [];
        //var objAttribute = {
        //    ListAttribute: [],
        //};
        var rows = $('tbody#tbody-attribute tr.trdata').length;
        if (rows > 0) {
            $('tbody#tbody-attribute tr.trdata').each(function () {
                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-select select.select-attribute');
                    if (select !== undefined && select !== null) {
                        var attributeCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(attributeCode)) {
                            // trường hợp có chọn thuộc tính
                            // kiểm tra có giá trị của thuộc tính không
                            var inputText = $(tr).find('td.td-input-text input.value-attribute');
                            if (inputText !== undefined && inputText !== null) {
                                var _value = $(inputText).val();
                                if (!commonUtils.isNullOrEmpty(_value)) {
                                    var arrData = _value.split(',');
                                    if (arrData !== undefined && arrData !== null && arrData.length > 0) {
                                        var listAttribute = [];
                                        for (var i = 0; i < arrData.length; i++) {
                                            var _val = commonUtils.returnValue(arrData[i]);
                                            if (!commonUtils.isNullOrEmpty(_val)) {
                                                var objAttribute = {
                                                    AttributeCode: attributeCode,
                                                    AttributeName: _val
                                                };
                                                listAttribute.push(objAttribute);
                                            }
                                        }
                                        if (listAttribute !== undefined && listAttribute !== null && listAttribute.length > 0) {
                                            _listAttribute.push(listAttribute);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            });
        }
        return _listAttribute;
    }
    // Start Attribute


    //Open Information
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
            $tdEvent.attr('class', 'cell-25 td-event-delete');
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
            $select.attr('showpopup-control-adddata', '#ShowPopupAddInformation');
            $select.attr('showpopup-control-updatedata', '#ShowPopupUpdateInformation');
            $select.change(function (e) {
                change_Information($select);
            });

            var $optgroup = $('<optgroup>');

            var $optionFirst = $('<option>');
            //$optionFirst.attr('disabled', 'disabled');
            $optionFirst.attr('opt', 'null');
            $optionFirst.attr('value', '');
            $optionFirst.text('--Lựa chọn--');
            $optgroup.append($optionFirst);

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

                    $optgroup.append($option);
                }
            }

            $select.append($optgroup);

            var $optionadd = $('<option>');
            $optionadd.attr('value', "ADDDATA");
            //$optionadd.text("Thêm/Sửa");
            $select.append($optionadd);

            $divSelect.append($select);
            // nút thêm thông tin
            var $aAddAttr = $('<a>');
            $aAddAttr.attr('class', 'btn btn-nc-button btn-add-or-edit');
            $aAddAttr.attr('title', 'Thêm thông tin');
            $aAddAttr.click(function (e) {

                showPopup_Information($aAddAttr);
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
            debugger
            $inputText.change(function (e) {
                change_InputTextInformation($inputText);
            });
            $divInputText.append($inputText);
            $tdInputText.append($divInputText);


            $tr.append($tdEvent);
            $tr.append($tdSelect);
            $tr.append($tdInputText);
            $tbody.append($tr);

            $('.select-information').select2();
            //$(".value-information").tagsinput();

            commonUtils.updateTableTrNotShowIdx($('#tbody-information tr.trdata'), true);
        }
    };

    function change_InputTextInformation(thiz) {
        debugger;
        if (thiz !== undefined && thiz !== null) {
            var _value = commonUtils.returnValue($(thiz).val());
            if (!commonUtils.isNullOrEmpty(_value)) {

                var tr = $(thiz).parent().parent().parent();
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-select select.select-information ');
                    if (select !== undefined && select !== null) {
                        var attributeCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(attributeCode)) {
                            gen_Products();
                        } else {
                            var listToastr = [];
                            objToastr = { ToastrType: 'error', ToastrMessage: 'Chưa chọn thông tin bổ sung' };
                            listToastr.push(objToastr);
                            commonUtils.showToastr(listToastr);
                            $(select).focus();
                        }
                    }
                }
            } else {
                gen_Products();
            }
        }
    }

    function deleteRow_Information(thiz) {
        var tr = $(thiz).parent().parent();
        if (tr !== undefined && tr !== null) {
            $(tr).remove();
        }
        var rows = $('tbody#tbody-information tr.trdata').length;
        if (rows > 0) {
            gen_Products();
        } else {
            var $tbody = $('#tbody-information');
            $tbody.empty();
            // tạm thời set hàng hóa null
            var $div = $('#divListOfTheSameTypeItems');
            //$div.empty();
        }
        // gen products
    }
    function deleteRow_Information_update(thiz) {
        var tr = $(thiz).parent().parent();
        if (tr !== undefined && tr !== null) {
            $(tr).remove();
        }
        var rows = $('tbody#tbody-information tr.trdata').length;
        if (rows > 0) {
            var _listUnit = listUnit();
            genProducts_ByUnit(_listUnit);
            //gen_Products();
        } else {
            var $tbody = $('#tbody-information');
            $tbody.empty();
            // tạm thời set hàng hóa null
            var $div = $('#divListOfTheSameTypeItems');
            //$div.empty();
        }
        // gen products
    }
    function showPopup_Information(thiz) {
        var trIdx = $(thiz).closest('tr.trdata').attr('idx');
        ShowPopupAddInformation(trIdx);
    }
    function showPopup_Edit_Information(thiz) {
        var code = $(thiz).siblings('select.select-information').attr('select-value');
        var title = $(thiz).siblings('select.select-information').find('option[value = "' + code + '"]').text();
        var controltype = $(thiz).siblings('select.select-information').attr('select-controltype');
        var datatype = $(thiz).siblings('select.select-information').attr('select-datatype');
        // Show popup update information
        //debugger
        ShowPopupUpdateInformation(code, title, controltype, datatype);
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
                        //debugger
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
                    //if (_value !== "ADDDATA" && _value !== "UPDATEDATA") {
                    //    $(thiz).children('option').val('UPDATEDATA');
                    //}
                    var $aEditAttr = $divSelectInformation.find('a.btn-add-or-edit');
                    if ($aEditAttr !== undefined && $aEditAttr !== null) {
                        $aEditAttr.empty();
                        $aEditAttr.attr('title', 'Sửa thông tin');
                        $aEditAttr.off('click');
                        $aEditAttr.on('click', function (e) {
                            showPopup_Edit_Information($aEditAttr);
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

    function listInformation() {
        var _listInformation = [];
        //var objInformation = {
        //    InformationCode: '',
        //    ListData: [],
        //};
        var rows = $('tbody#tbody-information tr.trdata').length;
        if (rows > 0) {
            $('tbody#tbody-information tr.trdata').each(function () {
                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-input-title select.select-information');
                    if (select !== undefined && select !== null) {
                        var informationCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(informationCode)) {
                            // trường hợp có chọn thuộc tính
                            // kiểm tra có giá trị của thuộc tính không
                            var inputText = $(tr).find('td.td-input-text input.value-information');
                            if (inputText !== undefined && inputText !== null) {
                                var _value = $(inputText).val();
                                if (!commonUtils.isNullOrEmpty(_value)) {
                                    var arrData = _value.split(',');
                                    if (arrData !== undefined && arrData !== null && arrData.length > 0) {
                                        var lisData = [];
                                        for (var i = 0; i < arrData.length; i++) {
                                            var _val = commonUtils.returnValue(arrData[i]);
                                            if (!commonUtils.isNullOrEmpty(_val)) {
                                                lisData.push(_val);
                                            }
                                        }
                                        if (lisData !== undefined && lisData !== null && lisData.length > 0) {
                                            var objInformation = {
                                                InformationCode: informationCode,
                                                ListData: lisData
                                            };
                                            _listInformation.push(objInformation);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            });
        }
        return _listInformation;
    }

    function listInformationNew() {
        var _listInformation = [];
        //var objInformation = {
        //    ListInformation: [],
        //};
        var rows = $('tbody#tbody-information tr.trdata').length;
        if (rows > 0) {
            $('tbody#tbody-information tr.trdata').each(function () {
                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-input-title select.select-information');
                    if (select !== undefined && select !== null) {
                        var informationCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(informationCode)) {
                            // trường hợp có chọn thuộc tính
                            // kiểm tra có giá trị của thuộc tính không
                            var inputText = $(tr).find('td.td-input-text input.value-information');
                            if (inputText !== undefined && inputText !== null) {
                                var _value = $(inputText).val();
                                if (!commonUtils.isNullOrEmpty(_value)) {
                                    var arrData = _value.split(',');
                                    if (arrData !== undefined && arrData !== null && arrData.length > 0) {
                                        var listInformation = [];
                                        for (var i = 0; i < arrData.length; i++) {
                                            var _val = commonUtils.returnValue(arrData[i]);
                                            if (!commonUtils.isNullOrEmpty(_val)) {
                                                var objInformation = {
                                                    InformationCode: informationCode,
                                                    informationTitle: _val
                                                };
                                                listInformation.push(objInformation);
                                            }
                                        }
                                        if (listInformation !== undefined && listInformation !== null && listInformation.length > 0) {
                                            _listInformation.push(listInformation);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            });
        }
        return _listAttribute;
    }
    //Close Information


    this.setListOfTheSameTypeItems_Null = function () {
        var $divListOfTheSameTypeItems = $('#divListOfTheSameTypeItems');
        if ($divListOfTheSameTypeItems !== undefined && $divListOfTheSameTypeItems !== null) {
            $divListOfTheSameTypeItems.empty();
        }
    };

    function gen_Products() {
        //debugger;
        var _listAttribute = listAttributeNew();
        //var _listAttribute = listAttribute();
        var _listUnit = listUnit();
        // Trường hợp có đơn vị
        if (_listUnit.length > 0) {
            if (_listAttribute.length > 0) {
                genProducts_By_Unit_AttributeNew(_listAttribute, _listUnit)
                //genProducts_By_Unit_Attribute(_listAttribute, _listUnit);
            } else {
                // gen products theo đơn vị
                genProducts_ByUnit(_listUnit);
            }
        } else {
            // Trường hợp không có đơn vị
            // Nếu có thuộc tính => gen products theo thuộc tính
            if (_listAttribute.length > 0) {
                genProducts_ByAttribute(_listAttribute);
            }
        }
    }

    function genProducts_By_Unit_AttributeNew(_listAttribute, _listUnit) {
        var iLengthAttribute = _listAttribute.length;
        var iLengthUnit = _listUnit.length;
        if (iLengthAttribute > 0 && iLengthUnit > 0) {
            var myArr = [];
            for (var i = 0; i < iLengthAttribute; i++) {
                if (i === 0) {

                    if (_listAttribute[i] != undefined && _listAttribute[i] != null && _listAttribute[i].length > 0) {
                        for (var _i = 0; _i < _listAttribute[i].length; _i++) {
                            var myArrSub = [];
                            myArrSub.push(_listAttribute[i][_i]);
                            myArr.push(myArrSub);
                        }

                    }
                } else {
                    myArr = returnArray_Object(myArr, _listAttribute[i]);
                }
            }
            if (myArr.length > 0) {
                var listCss = [];
                var $divListOfTheSameTypeItems = $('#divListOfTheSameTypeItems');
                $divListOfTheSameTypeItems.empty();
                $divListOfTheSameTypeItems.attr('tb', 'attr-unit')
                // danh sách hàng hóa cùng loại
                var $table = $('<table>');
                $table.attr('class', 'table-products col-12');
                var $thead = $('<thead>');
                $thead.attr('class', 'thead-css');
                var $trThead = $('<tr>');
                $trThead.attr('class', 'trdata');
                $trThead.attr('idx', '0');
                // nút xóa
                var $thThead_Delete = $('<th>');
                $thThead_Delete.attr('class', 'text-center');
                // hàng hóa
                var $thThead_ProductName = $('<th>');
                $thThead_ProductName.attr('class', 'text-center');
                $thThead_ProductName.text('Hàng hóa');
                // mã hàng 
                var $thThead_ProductCodeUser = $('<th>');
                $thThead_ProductCodeUser.attr('class', 'text-center');
                $thThead_ProductCodeUser.text('Mã hàng');
                // mã vạch
                var $thThead_ProductBarCode = $('<th>');
                $thThead_ProductBarCode.attr('class', 'text-center');
                $thThead_ProductBarCode.text('Mã vạch');
                // đơn vị
                var $thThead_UnitCode = $('<th>');
                $thThead_UnitCode.attr('class', 'text-center');
                $thThead_UnitCode.text('Đơn vị');
                // giá mua 
                var $thThead_UPBuy = $('<th>');
                $thThead_UPBuy.attr('class', 'text-center');
                $thThead_UPBuy.text('Giá mua');
                // giá bán 
                var $thThead_UPSell = $('<th>');
                $thThead_UPSell.attr('class', 'text-center');
                $thThead_UPSell.text('Giá bán');

                $trThead.append($thThead_Delete);
                $trThead.append($thThead_ProductName);
                $trThead.append($thThead_ProductCodeUser);
                $trThead.append($thThead_ProductBarCode);
                $trThead.append($thThead_UnitCode);
                $trThead.append($thThead_UPBuy);
                $trThead.append($thThead_UPSell);

                $thead.append($trThead);

                $table.append($thead);
                var $tbody = $('<tbody>');
                $tbody.attr('class', 'tbody-products');
                var productCodeUser = commonUtils.returnValueText('#ProductCodeUser');
                var productName = commonUtils.returnValueText('#ProductName');
                var productCode = commonUtils.returnValueText('#ProductCode');
                var productCodeBase = commonUtils.returnValueText('#ProductCodeBase');
                var productCodeRoot = commonUtils.returnValueText('#ProductCodeRoot');

                var uPBuy = null;
                var uPSell = null;
                var valConvert = null;
                var unitCode = '';
                var idx = 0;
                var unitId = '';
                for (var j = 0; j < myArr.length; j++) {


                    var date = new Date();
                    var randomCur = date.getTime();
                    var jqnumber = 'jqnumber' + randomCur;
                    var uPBuyCss = 'upbuy-' + jqnumber;
                    var uPSellCss = 'upsell-' + jqnumber;
                    listCss.push(uPBuyCss);
                    listCss.push(uPSellCss);
                    var productInit = randomCur;
                    var listAttributeSub = [];
                    var productNameSub = '';
                    var arrAttribute = myArr[j].slice();
                    if (arrAttribute !== undefined && arrAttribute !== null) {
                        for (var l = 0; l < arrAttribute.length; l++) {
                            var objAttribute = arrAttribute[l];
                            if (l > 0) {
                                productNameSub += ',';
                            }
                            productNameSub += commonUtils.returnValue(objAttribute.AttributeName);
                            listAttributeSub.push(objAttribute);
                        }
                    }
                    //var productNameSub = commonUtils.returnValueOrNull(myArr[j]);

                    if (!commonUtils.isNullOrEmpty(productName)) {
                        productNameSub = productName + '-' + productNameSub;
                    }

                    for (var k = 0; k < _listUnit.length; k++) {
                        var productCodeUserSub = productCodeUser;
                        if (!commonUtils.isNullOrEmpty(productCodeUserSub)) {
                            if (idx >= 1) {
                                productCodeUserSub += ('-' + idx);
                            }
                        }
                        unitId = commonUtils.returnValue(_listUnit[k].Id);
                        unitCode = commonUtils.returnValue(_listUnit[k].UnitCode);
                        //unitCode = commonUtils.returnValue(_listUnit[k].UnitName);
                        var productNameSub1 = productNameSub + '-' + unitCode;
                        uPBuy = commonUtils.returnValueOrNull(_listUnit[k].UPBuy);
                        uPSell = commonUtils.returnValueOrNull(_listUnit[k].UPSell);
                        valConvert = commonUtils.returnValueOrNull(_listUnit[k].ExQty);
                        var canBeSold = commonUtils.returnValueOrNull(_listUnit[k].CanBeSold);
                        var canBePurchased = commonUtils.returnValueOrNull(_listUnit[k].CanBePurchased);

                        var $tr = $('<tr>');
                        $tr.attr('class', 'trdata');
                        $tr.attr('idx', idx);
                        $tr.attr('product-base', productInit);
                        $tr.attr('unit-id', unitId);
                        if (j === 0) {
                            if (k === 0) {
                                $tr.attr('product-level-sys', 'ROOTPRD');
                            } else {
                                $tr.attr('product-level-sys', 'L2PRD');
                            }
                        } else {
                            if (k === 0) {
                                $tr.attr('product-level-sys', 'BASEPRD');
                            } else {
                                $tr.attr('product-level-sys', 'L2PRD');
                            }
                        }

                        // nút xóa
                        var $td_Delete = $('<td>');
                        $td_Delete.attr('class', 'cell-50');
                        var $aDelete = $('<a>');
                        $aDelete.attr('href', 'javascript:;');
                        $aDelete.attr('title', 'Xóa');
                        $aDelete.click(function (e) {
                            deleteRow_Product($(this));
                        });
                        var $iDelete = $('<i>');
                        $iDelete.attr('class', 'fas fa-trash-alt');
                        $aDelete.append($iDelete);
                        $td_Delete.append($aDelete);
                        // hàng hóa
                        var $td_ProductName = $('<td>');
                        $td_ProductName.attr('class', 'td-product-name');
                        var $div_ProductName = $('<div>');
                        $div_ProductName.attr('class', 'form-group no-margin div-product-name');
                        var $input_ProductName = $('<input>');
                        $input_ProductName.attr('type', 'text');
                        //$input_ProductName.attr('id', 'ProductName_' + idx);
                        $input_ProductName.attr('name', 'List_Mst_Product[' + idx + '].ProductName');
                        $input_ProductName.attr('class', 'col-12 input-product-name');
                        //$input_ProductName.attr('value', productNameSub);
                        $input_ProductName.attr('value', productNameSub1);
                        $input_ProductName.attr('placeholder', 'Tên hàng tự động');
                        $div_ProductName.append($input_ProductName);
                        $td_ProductName.append($div_ProductName);
                        // mã hàng
                        var $td_ProductCodeUser = $('<td>');
                        $td_ProductCodeUser.attr('class', 'td-product-code-user');
                        var $div_ProductCodeUser = $('<div>');
                        $div_ProductCodeUser.attr('class', 'form-group no-margin div-product-code-user');
                        var $input_ProductCodeUser = $('<input>');
                        $input_ProductCodeUser.attr('type', 'text');
                        //$input_ProductCodeUser.attr('id', 'ProductCodeUser_' + idx);
                        $input_ProductCodeUser.attr('name', 'List_Mst_Product[' + idx + '].ProductCodeUser');
                        $input_ProductCodeUser.attr('class', 'col-12 input-product-code-user');
                        $input_ProductCodeUser.attr('value', productCodeUserSub);
                        $input_ProductCodeUser.attr('placeholder', 'Mã hàng tự động');

                        //// thêm giá trị
                        var $input_ProductCode = $('<input>');
                        $input_ProductCode.attr('type', 'hidden');
                        //$input_ProductCode.attr('id', 'ProductCode_' + idx);
                        $input_ProductCode.attr('name', 'List_Mst_Product[' + idx + '].ProductCode');
                        $input_ProductCode.attr('class', 'col-12 input-product-code');
                        $input_ProductCode.attr('value', productCode);

                        var $input_ProductCodeBase = $('<input>');
                        $input_ProductCodeBase.attr('type', 'hidden');
                        //$input_ProductCodeBase.attr('id', 'ProductCodeBase_' + idx);
                        $input_ProductCodeBase.attr('name', 'List_Mst_Product[' + idx + '].ProductCodeBase');
                        $input_ProductCodeBase.attr('class', 'col-12 input-product-code-base');
                        $input_ProductCodeBase.attr('value', productCodeBase);

                        var $input_ProductCodeRoot = $('<input>');
                        $input_ProductCodeRoot.attr('type', 'hidden');
                        //$input_ProductCodeRoot.attr('id', 'ProductCodeRoot_' + idx);
                        $input_ProductCodeRoot.attr('name', 'List_Mst_Product[' + idx + '].ProductCodeRoot');
                        $input_ProductCodeRoot.attr('class', 'col-12 input-product-code-root');
                        $input_ProductCodeRoot.attr('value', productCodeRoot);

                        var $input_CanBeSold = $('<input>');
                        $input_CanBeSold.attr('type', 'hidden');
                        $input_CanBeSold.attr('name', 'List_Mst_Product[' + idx + '].CanBeSold');
                        $input_CanBeSold.attr('class', 'col-12 input-can-be-sold');
                        $input_CanBeSold.attr('value', canBeSold);

                        var $input_CanBePurchased = $('<input>');
                        $input_CanBePurchased.attr('type', 'hidden');
                        $input_CanBePurchased.attr('name', 'List_Mst_Product[' + idx + '].CanBePurchased');
                        $input_CanBePurchased.attr('class', 'col-12 input-can-be-purchased');
                        $input_CanBePurchased.attr('value', canBePurchased);

                        var $input_ValConvert = $('<input>');
                        $input_ValConvert.attr('type', 'hidden');
                        $input_ValConvert.attr('name', 'List_Mst_Product[' + idx + '].ValConvert');
                        $input_ValConvert.attr('class', 'col-12 input-val-convert');
                        $input_ValConvert.attr('value', valConvert);

                        if (listAttributeSub != undefined && listAttributeSub !== null) {
                            for (var m = 0; m < listAttributeSub.length; m++) {
                                var objAttributeSub = listAttributeSub[m];
                                var attributeCodeSub = commonUtils.returnValue(objAttributeSub.AttributeCode);
                                var attributeNameSub = commonUtils.returnValue(objAttributeSub.AttributeName);
                                var $input_AttributeSub = $('<input>');
                                $input_AttributeSub.attr('type', 'hidden');
                                $input_AttributeSub.attr('name', 'List_Mst_Product[' + m + '].AttributeSub');
                                $input_AttributeSub.attr('class', 'col-12 input-attribute-sub');
                                $input_AttributeSub.attr('iidx', m);
                                $input_AttributeSub.attr('value', attributeCodeSub);
                                $input_AttributeSub.attr('data', attributeNameSub);
                                $div_ProductCodeUser.append($input_AttributeSub);
                            }
                        }

                        $div_ProductCodeUser.append($input_ProductCodeUser);
                        //$div_ProductCodeUser.append($input_ProductCode);
                        //$div_ProductCodeUser.append($input_ProductCodeBase);
                        //$div_ProductCodeUser.append($input_ProductCodeRoot);

                        $div_ProductCodeUser.append($input_ValConvert);
                        $div_ProductCodeUser.append($input_CanBeSold);
                        $div_ProductCodeUser.append($input_CanBePurchased);
                        $td_ProductCodeUser.append($div_ProductCodeUser);
                        // mã vạch
                        var $td_ProductBarCode = $('<td>');
                        $td_ProductBarCode.attr('class', 'td-product-bar-code');
                        var $div_ProductBarCode = $('<div>');
                        $div_ProductBarCode.attr('class', 'form-group no-margin div-product-bar-code');
                        var $input_ProductBarCode = $('<input>');
                        $input_ProductBarCode.attr('type', 'text');
                        //$input_ProductBarCode.attr('id', 'ProductBarCode_' + idx);
                        $input_ProductBarCode.attr('name', 'List_Mst_Product[' + idx + '].ProductBarCode');
                        $input_ProductBarCode.attr('class', 'col-12 input-product-bar-code');
                        $input_ProductBarCode.attr('value', productCodeUserSub);
                        $div_ProductBarCode.append($input_ProductBarCode);
                        $td_ProductBarCode.append($div_ProductBarCode);
                        // đơn vị
                        var $td_UnitCode = $('<td>');
                        $td_UnitCode.attr('class', 'td-unit-code');
                        var $div_UnitCode = $('<div>');
                        $div_UnitCode.attr('class', 'form-group no-margin div-unit-code');
                        var $input_UnitCode = $('<input>');
                        $input_UnitCode.attr('type', 'text');
                        //$input_UnitCode.attr('id', 'UnitCode_' + idx);
                        $input_UnitCode.attr('name', 'List_Mst_Product[' + idx + '].UnitCode');
                        $input_UnitCode.attr('class', 'col-12 disabled-fix input-unit-code');
                        //$input_UnitCode.attr('value', '');
                        $input_UnitCode.attr('value', unitCode);
                        $div_UnitCode.append($input_UnitCode);
                        $td_UnitCode.append($div_UnitCode);
                        // giá mua
                        var $td_UPBuy = $('<td>');
                        $td_UPBuy.attr('class', 'td-upbuy');
                        var $div_UPBuy = $('<div>');
                        $div_UPBuy.attr('class', 'form-group no-margin div-upbuy');
                        var $input_UPBuy = $('<input>');
                        $input_UPBuy.attr('type', 'text');
                        $input_UPBuy.attr('id', uPBuyCss);
                        $input_UPBuy.attr('name', 'List_Mst_Product[' + idx + '].UPBuy');
                        $input_UPBuy.attr('class', 'col-12 text-right input-upbuy ' + uPBuyCss);
                        $input_UPBuy.attr('value', uPBuy);


                        var tableName = 'Mst_Product';
                        var UPSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');


                        $('.' + uPSellCss).number(true, UPSellFormat);

                        //$input_UPBuy.focus(function (e) {
                        //    //$('.' + uPBuyCss).number(true, 3);
                        //    //$(this).number(false, 3);
                        //    //$('#' + uPBuyCss).number(true, 3);
                        //});
                        $div_UPBuy.append($input_UPBuy);
                        $td_UPBuy.append($div_UPBuy);
                        // giá bán
                        var $td_UPSell = $('<td>');
                        $td_UPSell.attr('class', 'td-upsell');
                        var $div_UPSell = $('<div>');
                        $div_UPSell.attr('class', 'form-group no-margin div-upsell');
                        var $input_UPSell = $('<input>');
                        $input_UPSell.attr('type', 'text');
                        //$input_UPSell.attr('id', 'UPSell_' + idx);
                        $input_UPSell.attr('name', 'List_Mst_Product[' + idx + '].UPSell');
                        $input_UPSell.attr('class', 'col-12 text-right input-upsell ' + uPSellCss);
                        $input_UPSell.attr('value', uPSell);
                        $div_UPSell.append($input_UPSell);
                        $td_UPSell.append($div_UPSell);

                        var tableName = 'Mst_Product';
                        var UPBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');


                        $('.' + uPBuyCss).number(true, UPBuyFormat);


                        $tr.append($td_Delete);
                        $tr.append($td_ProductName);
                        $tr.append($td_ProductCodeUser);
                        $tr.append($td_ProductBarCode);
                        $tr.append($td_UnitCode);
                        $tr.append($td_UPBuy);
                        $tr.append($td_UPSell);

                        $tbody.append($tr);
                        //$('.' + uPBuyCss).number(true, 2);
                        //$('.' + uPSellCss).number(true, 2);
                        idx++;
                    }
                }
                $table.append($tbody);
                $divListOfTheSameTypeItems.append($table);


                debugger
                //format
                var tableName = 'Ord_OrderSR';
                var UPBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPBuy');
                var UPSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');

                $('.input-upbuy').number(true, UPBuyFormat);
                $('.input-upsell').number(true, UPSellFormat);

                if (listCss !== undefined && listCss !== null) {
                    for (var n = 0; n < listCss.length; n++) {
                        var css = '' + listCss[n];
                        $(css).number(true, 2);
                    }
                }
            }
        }
    }

    function genProducts_By_Unit_Attribute(_listAttribute, _listUnit) {
        var iLengthAttribute = _listAttribute.length;
        var iLengthUnit = _listUnit.length;
        if (iLengthAttribute > 0 && iLengthUnit > 0) {
            var myArr = [];
            for (var i = 0; i < iLengthAttribute; i++) {
                if (i === 0) {
                    myArr = _listAttribute[i].ListData;
                } else {
                    myArr = returnArray(myArr, _listAttribute[i].ListData)
                }
            }
            if (myArr.length > 0) {
                var $divListOfTheSameTypeItems = $('#divListOfTheSameTypeItems');
                $divListOfTheSameTypeItems.empty();
                $divListOfTheSameTypeItems.attr('tb', 'attr-unit')
                // danh sách hàng hóa cùng loại
                var $table = $('<table>');
                $table.attr('class', 'table-products col-12');
                var $thead = $('<thead>');
                $thead.attr('class', 'thead-css');
                var $trThead = $('<tr>');
                $trThead.attr('class', 'trdata');
                $trThead.attr('idx', '0');
                // nút xóa
                var $thThead_Delete = $('<th>');
                $thThead_Delete.attr('class', 'text-center');
                // hàng hóa
                var $thThead_ProductName = $('<th>');
                $thThead_ProductName.attr('class', 'text-center');
                $thThead_ProductName.text('Hàng hóa');
                // mã hàng 
                var $thThead_ProductCodeUser = $('<th>');
                $thThead_ProductCodeUser.attr('class', 'text-center');
                $thThead_ProductCodeUser.text('Mã hàng');
                // mã vạch
                var $thThead_ProductBarCode = $('<th>');
                $thThead_ProductBarCode.attr('class', 'text-center');
                $thThead_ProductBarCode.text('Mã vạch');
                // đơn vị
                var $thThead_UnitCode = $('<th>');
                $thThead_UnitCode.attr('class', 'text-center');
                $thThead_UnitCode.text('Đơn vị');
                // giá mua 
                var $thThead_UPBuy = $('<th>');
                $thThead_UPBuy.attr('class', 'text-center');
                $thThead_UPBuy.text('Giá mua');
                // giá bán 
                var $thThead_UPSell = $('<th>');
                $thThead_UPSell.attr('class', 'text-center');
                $thThead_UPSell.text('Giá bán');

                $trThead.append($thThead_Delete);
                $trThead.append($thThead_ProductName);
                $trThead.append($thThead_ProductCodeUser);
                $trThead.append($thThead_ProductBarCode);
                $trThead.append($thThead_UnitCode);
                $trThead.append($thThead_UPBuy);
                $trThead.append($thThead_UPSell);

                $thead.append($trThead);

                $table.append($thead);
                var $tbody = $('<tbody>');
                $tbody.attr('class', 'tbody-products');
                var productCodeUser = commonUtils.returnValueText('#ProductCodeUser');
                var productName = commonUtils.returnValueText('#ProductName');
                var productCode = commonUtils.returnValueText('#ProductCode');
                var productCodeBase = commonUtils.returnValueText('#ProductCodeBase');
                var productCodeRoot = commonUtils.returnValueText('#ProductCodeRoot');

                var uPBuy = null;
                var uPSell = null;
                var valConvert = null;
                var unitCode = '';
                var idx = 0;
                for (var j = 0; j < myArr.length; j++) {


                    var date = new Date();
                    var randomCur = date.getTime();
                    var productInit = randomCur;
                    var productNameSub = commonUtils.returnValueOrNull(myArr[j]);

                    if (!commonUtils.isNullOrEmpty(productName)) {
                        productNameSub = productName + '-' + productNameSub;
                    }

                    for (var k = 0; k < _listUnit.length; k++) {
                        var productCodeUserSub = productCodeUser;
                        if (!commonUtils.isNullOrEmpty(productCodeUserSub)) {
                            if (idx >= 1) {
                                productCodeUserSub += ('-' + idx);
                            }
                        }
                        unitCode = commonUtils.returnValue(_listUnit[k].UnitCode);
                        //unitCode = commonUtils.returnValue(_listUnit[k].UnitName);
                        uPBuy = commonUtils.returnValueOrNull(_listUnit[k].UPBuy);
                        uPSell = commonUtils.returnValueOrNull(_listUnit[k].UPSell);
                        valConvert = commonUtils.returnValueOrNull(_listUnit[k].ExQty);
                        var canBeSold = commonUtils.returnValueOrNull(_listUnit[k].CanBeSold);
                        var canBePurchased = commonUtils.returnValueOrNull(_listUnit[k].CanBePurchased);

                        var $tr = $('<tr>');
                        $tr.attr('class', 'trdata');
                        $tr.attr('idx', idx);
                        $tr.attr('product-base', productInit);
                        if (j === 0) {
                            if (k === 0) {
                                $tr.attr('product-level-sys', 'ROOTPRD');
                            } else {
                                $tr.attr('product-level-sys', 'L2PRD');
                            }
                        } else {
                            if (k === 0) {
                                $tr.attr('product-level-sys', 'BASEPRD');
                            } else {
                                $tr.attr('product-level-sys', 'L2PRD');
                            }
                        }

                        // nút xóa
                        var $td_Delete = $('<td>');
                        $td_Delete.attr('class', 'cell-50');
                        var $aDelete = $('<a>');
                        $aDelete.attr('href', 'javascript:;');
                        $aDelete.attr('title', 'Xóa');
                        $aDelete.click(function (e) {
                            deleteRow_Product($(this));
                        });
                        var $iDelete = $('<i>');
                        $iDelete.attr('class', 'fas fa-trash-alt');
                        $aDelete.append($iDelete);
                        $td_Delete.append($aDelete);
                        // hàng hóa
                        var $td_ProductName = $('<td>');
                        $td_ProductName.attr('class', 'td-product-name');
                        var $div_ProductName = $('<div>');
                        $div_ProductName.attr('class', 'form-group no-margin div-product-name');
                        var $input_ProductName = $('<input>');
                        $input_ProductName.attr('type', 'text');
                        //$input_ProductName.attr('id', 'ProductName_' + idx);
                        $input_ProductName.attr('name', 'List_Mst_Product[' + idx + '].ProductName');
                        $input_ProductName.attr('class', 'col-12 input-product-name');
                        $input_ProductName.attr('value', productNameSub);
                        $input_ProductName.attr('placeholder', 'Tên hàng tự động');
                        $div_ProductName.append($input_ProductName);
                        $td_ProductName.append($div_ProductName);
                        // mã hàng
                        var $td_ProductCodeUser = $('<td>');
                        $td_ProductCodeUser.attr('class', 'td-product-code-user');
                        var $div_ProductCodeUser = $('<div>');
                        $div_ProductCodeUser.attr('class', 'form-group no-margin div-product-code-user');
                        var $input_ProductCodeUser = $('<input>');
                        $input_ProductCodeUser.attr('type', 'text');
                        //$input_ProductCodeUser.attr('id', 'ProductCodeUser_' + idx);
                        $input_ProductCodeUser.attr('name', 'List_Mst_Product[' + idx + '].ProductCodeUser');
                        $input_ProductCodeUser.attr('class', 'col-12 input-product-code-user');
                        $input_ProductCodeUser.attr('value', productCodeUserSub);
                        $input_ProductCodeUser.attr('placeholder', 'Mã hàng tự động');

                        //// thêm giá trị
                        var $input_ProductCode = $('<input>');
                        $input_ProductCode.attr('type', 'hidden');
                        //$input_ProductCode.attr('id', 'ProductCode_' + idx);
                        $input_ProductCode.attr('name', 'List_Mst_Product[' + idx + '].ProductCode');
                        $input_ProductCode.attr('class', 'col-12 input-product-code');
                        $input_ProductCode.attr('value', productCode);

                        var $input_ProductCodeBase = $('<input>');
                        $input_ProductCodeBase.attr('type', 'hidden');
                        //$input_ProductCodeBase.attr('id', 'ProductCodeBase_' + idx);
                        $input_ProductCodeBase.attr('name', 'List_Mst_Product[' + idx + '].ProductCodeBase');
                        $input_ProductCodeBase.attr('class', 'col-12 input-product-code-base');
                        $input_ProductCodeBase.attr('value', productCodeBase);

                        var $input_ProductCodeRoot = $('<input>');
                        $input_ProductCodeRoot.attr('type', 'hidden');
                        //$input_ProductCodeRoot.attr('id', 'ProductCodeRoot_' + idx);
                        $input_ProductCodeRoot.attr('name', 'List_Mst_Product[' + idx + '].ProductCodeRoot');
                        $input_ProductCodeRoot.attr('class', 'col-12 input-product-code-root');
                        $input_ProductCodeRoot.attr('value', productCodeRoot);

                        var $input_CanBeSold = $('<input>');
                        $input_CanBeSold.attr('type', 'hidden');
                        $input_CanBeSold.attr('name', 'List_Mst_Product[' + idx + '].CanBeSold');
                        $input_CanBeSold.attr('class', 'col-12 input-can-be-sold');
                        $input_CanBeSold.attr('value', canBeSold);

                        var $input_CanBePurchased = $('<input>');
                        $input_CanBePurchased.attr('type', 'hidden');
                        $input_CanBePurchased.attr('name', 'List_Mst_Product[' + idx + '].CanBePurchased');
                        $input_CanBePurchased.attr('class', 'col-12 input-can-be-purchased');
                        $input_CanBePurchased.attr('value', canBePurchased);

                        var $input_ValConvert = $('<input>');
                        $input_ValConvert.attr('type', 'hidden');
                        $input_ValConvert.attr('name', 'List_Mst_Product[' + idx + '].ValConvert');
                        $input_ValConvert.attr('class', 'col-12 input-val-convert');
                        $input_ValConvert.attr('value', valConvert);

                        $div_ProductCodeUser.append($input_ProductCodeUser);
                        //$div_ProductCodeUser.append($input_ProductCode);
                        //$div_ProductCodeUser.append($input_ProductCodeBase);
                        //$div_ProductCodeUser.append($input_ProductCodeRoot);

                        $div_ProductCodeUser.append($input_CanBeSold);
                        $div_ProductCodeUser.append($input_CanBePurchased);
                        $td_ProductCodeUser.append($div_ProductCodeUser);
                        // mã vạch
                        var $td_ProductBarCode = $('<td>');
                        $td_ProductBarCode.attr('class', 'td-product-bar-code');
                        var $div_ProductBarCode = $('<div>');
                        $div_ProductBarCode.attr('class', 'form-group no-margin div-product-bar-code');
                        var $input_ProductBarCode = $('<input>');
                        $input_ProductBarCode.attr('type', 'text');
                        //$input_ProductBarCode.attr('id', 'ProductBarCode_' + idx);
                        $input_ProductBarCode.attr('name', 'List_Mst_Product[' + idx + '].ProductBarCode');
                        $input_ProductBarCode.attr('class', 'col-12 input-product-bar-code');
                        $input_ProductBarCode.attr('value', productCodeUserSub);
                        $div_ProductBarCode.append($input_ProductBarCode);
                        $td_ProductBarCode.append($div_ProductBarCode);
                        // đơn vị
                        var $td_UnitCode = $('<td>');
                        $td_UnitCode.attr('class', 'td-unit-code');
                        var $div_UnitCode = $('<div>');
                        $div_UnitCode.attr('class', 'form-group no-margin div-unit-code');
                        var $input_UnitCode = $('<input>');
                        $input_UnitCode.attr('type', 'text');
                        //$input_UnitCode.attr('id', 'UnitCode_' + idx);
                        $input_UnitCode.attr('name', 'List_Mst_Product[' + idx + '].UnitCode');
                        $input_UnitCode.attr('class', 'col-12 disabled-fix input-unit-code');
                        //$input_UnitCode.attr('value', '');
                        $input_UnitCode.attr('value', unitCode);
                        $div_UnitCode.append($input_UnitCode);
                        $td_UnitCode.append($div_UnitCode);
                        // giá mua
                        var $td_UPBuy = $('<td>');
                        $td_UPBuy.attr('class', 'td-upbuy');
                        var $div_UPBuy = $('<div>');
                        $div_UPBuy.attr('class', 'form-group no-margin div-upbuy');
                        var $input_UPBuy = $('<input>');
                        $input_UPBuy.attr('type', 'text');
                        //$input_UPBuy.attr('id', 'UPBuy_' + idx);
                        $input_UPBuy.attr('name', 'List_Mst_Product[' + idx + '].UPBuy');
                        $input_UPBuy.attr('class', 'col-12 text-right input-upbuy');
                        $input_UPBuy.attr('value', uPBuy);
                        $div_UPBuy.append($input_UPBuy);
                        $td_UPBuy.append($div_UPBuy);
                        // giá bán
                        var $td_UPSell = $('<td>');
                        $td_UPSell.attr('class', 'td-upsell');
                        var $div_UPSell = $('<div>');
                        $div_UPSell.attr('class', 'form-group no-margin div-upsell');
                        var $input_UPSell = $('<input>');
                        $input_UPSell.attr('type', 'text');
                        //$input_UPSell.attr('id', 'UPSell_' + idx);
                        $input_UPSell.attr('name', 'List_Mst_Product[' + idx + '].UPSell');
                        $input_UPSell.attr('class', 'col-12 text-right input-upsell');
                        $input_UPSell.attr('value', uPSell);
                        $div_UPSell.append($input_UPSell);
                        $td_UPSell.append($div_UPSell);


                        $tr.append($td_Delete);
                        $tr.append($td_ProductName);
                        $tr.append($td_ProductCodeUser);
                        $tr.append($td_ProductBarCode);
                        $tr.append($td_UnitCode);
                        $tr.append($td_UPBuy);
                        $tr.append($td_UPSell);

                        $tbody.append($tr);

                        idx++;
                    }
                }
                $table.append($tbody);
                $divListOfTheSameTypeItems.append($table);
            }
        }
    }

    function genProducts_ByUnit(_listUnit) {
        var iLengthUnit = _listUnit.length;
        if (iLengthUnit > 0) {

            if (_listUnit.length > 0) {
                if (_listUnit.length === 1) {
                    var unitCode = commonUtils.returnValueText('#Unit_Root');
                    $('#divListOfTheSameTypeItems').empty();
                } else {

                    var listCss = [];
                    var $divListOfTheSameTypeItems = $('#divListOfTheSameTypeItems');
                    $divListOfTheSameTypeItems.empty();
                    $divListOfTheSameTypeItems.attr('tb', 'attribute');
                    // danh sách hàng hóa cùng loại
                    var $table = $('<table>');
                    $table.attr('class', 'table-products col-12');
                    var $thead = $('<thead>');
                    $thead.attr('class', 'thead-css');
                    var $trThead = $('<tr>');
                    $trThead.attr('class', 'trdata');
                    $trThead.attr('idx', '0');
                    // nút xóa
                    var $thThead_Delete = $('<th>');
                    $thThead_Delete.attr('class', 'text-center');
                    // hàng hóa
                    var $thThead_ProductName = $('<th>');
                    $thThead_ProductName.attr('class', 'text-center');
                    $thThead_ProductName.text('Hàng hóa');
                    // mã hàng 
                    var $thThead_ProductCodeUser = $('<th>');
                    $thThead_ProductCodeUser.attr('class', 'text-center');
                    $thThead_ProductCodeUser.text('Mã hàng');
                    // mã vạch
                    var $thThead_ProductBarCode = $('<th>');
                    $thThead_ProductBarCode.attr('class', 'text-center');
                    $thThead_ProductBarCode.text('Mã vạch');
                    // đơn vị
                    var $thThead_UnitCode = $('<th>');
                    $thThead_UnitCode.attr('class', 'text-center');
                    $thThead_UnitCode.text('Đơn vị');
                    // giá mua 
                    var $thThead_UPBuy = $('<th>');
                    $thThead_UPBuy.attr('class', 'text-center');
                    $thThead_UPBuy.text('Giá mua');
                    // giá bán 
                    var $thThead_UPSell = $('<th>');
                    $thThead_UPSell.attr('class', 'text-center');
                    $thThead_UPSell.text('Giá bán');

                    $trThead.append($thThead_Delete);
                    $trThead.append($thThead_ProductName);
                    $trThead.append($thThead_ProductCodeUser);
                    $trThead.append($thThead_ProductBarCode);
                    $trThead.append($thThead_UnitCode);
                    $trThead.append($thThead_UPBuy);
                    $trThead.append($thThead_UPSell);

                    $thead.append($trThead);

                    $table.append($thead);
                    var $tbody = $('<tbody>');
                    $tbody.attr('class', 'tbody-products');
                    var productCode = commonUtils.returnValueText('#ProductCode');
                    var productCodeBase = commonUtils.returnValueText('#ProductCodeBase');
                    var productCodeRoot = commonUtils.returnValueText('#ProductCodeRoot');
                    var productCodeUser = commonUtils.returnValueText('#ProductCodeUser');
                    var productName = commonUtils.returnValueText('#ProductName');
                    var uPBuy = commonUtils.returnValueTextOrNull('#UPBuy');
                    var uPSell = commonUtils.returnValueTextOrNull('#UPSell');
                    var unitCode = commonUtils.returnValueText('#Unit_Root');
                    var productBarCode = commonUtils.returnValueText('#ProductBarCode');
                    var productInit = '';
                    for (var j = 0; j < _listUnit.length; j++) {
                        var date = new Date();
                        var randomCur = date.getTime();
                        var jqnumber = 'jqnumber' + randomCur;
                        var uPBuyCss = 'upbuy-' + jqnumber;
                        var uPSellCss = 'upsell-' + jqnumber;
                        listCss.push(uPBuyCss);
                        listCss.push(uPSellCss);
                        if (commonUtils.isNullOrEmpty(productInit)) {
                            productInit = randomCur;
                        }

                        var listAttributeSub = [];
                        var productNameSub = '';


                        var unitNameSub = '';
                        unitNameSub = commonUtils.returnValue(_listUnit[j].UnitName);
                        if (!commonUtils.isNullOrEmpty(productName)) {
                            //if (j >= 1) {
                            //    productNameSub += ('-' + j);
                            //}
                            productNameSub = productName + productNameSub;
                        }

                        var productNameSub1 = productNameSub + '-' + unitNameSub;
                        var productCodeUserSub = productCodeUser;
                        if (!commonUtils.isNullOrEmpty(productCodeUserSub)) {
                            if (j >= 1) {
                                productCodeUserSub += ('-' + j);
                            }
                        }
                        unitId = commonUtils.returnValue(_listUnit[j].Id);
                        unitCode = commonUtils.returnValue(_listUnit[j].UnitCode);
                        uPBuy = commonUtils.returnValueOrNull(_listUnit[j].UPBuy);
                        uPSell = commonUtils.returnValueOrNull(_listUnit[j].UPSell);
                        valConvert = commonUtils.returnValueOrNull(_listUnit[j].ExQty);
                        var canBeSold = commonUtils.returnValueOrNull(_listUnit[j].CanBeSold);
                        var canBePurchased = commonUtils.returnValueOrNull(_listUnit[j].CanBePurchased);
                        var $tr = $('<tr>');
                        $tr.attr('class', 'trdata');
                        $tr.attr('idx', j);
                        $tr.attr('unit-id', unitId);
                        $tr.attr('product-base', productInit);
                        if (j === 0) {
                            $tr.attr('product-level-sys', 'ROOTPRD');
                        } else {
                            $tr.attr('product-level-sys', 'L2PRD');
                        }
                        // nút xóa
                        var $td_Delete = $('<td>');
                        $td_Delete.attr('class', 'cell-50');
                        var $aDelete = $('<a>');
                        $aDelete.attr('href', 'javascript:;');
                        $aDelete.attr('title', 'Xóa');
                        $aDelete.click(function (e) {
                            deleteRow_Product($(this));
                        });
                        var $iDelete = $('<i>');
                        $iDelete.attr('class', 'fas fa-trash-alt');
                        $aDelete.append($iDelete);
                        $td_Delete.append($aDelete);
                        // hàng hóa
                        var $td_ProductName = $('<td>');
                        $td_ProductName.attr('class', 'td-product-name');
                        var $div_ProductName = $('<div>');
                        $div_ProductName.attr('class', 'form-group no-margin div-product-name');
                        var $input_ProductName = $('<input>');
                        $input_ProductName.attr('type', 'text');
                        //$input_ProductName.attr('id', 'ProductName_' + j);
                        $input_ProductName.attr('name', 'List_Mst_Product[' + j + '].ProductName');
                        $input_ProductName.attr('class', 'col-12 input-product-name');
                        //$input_ProductName.attr('value', productNameSub);
                        $input_ProductName.attr('value', productNameSub1);
                        $input_ProductName.attr('placeholder', 'Tên hàng tự động');
                        $div_ProductName.append($input_ProductName);
                        $td_ProductName.append($div_ProductName);
                        // mã hàng
                        var $td_ProductCodeUser = $('<td>');
                        $td_ProductCodeUser.attr('class', 'td-product-code-user');
                        var $div_ProductCodeUser = $('<div>');
                        $div_ProductCodeUser.attr('class', 'form-group no-margin div-product-code-user');
                        var $input_ProductCodeUser = $('<input>');
                        $input_ProductCodeUser.attr('type', 'text');
                        //$input_ProductCodeUser.attr('id', 'ProductCodeUser_' + j);
                        $input_ProductCodeUser.attr('name', 'List_Mst_Product[' + j + '].ProductCodeUser');
                        $input_ProductCodeUser.attr('class', 'col-12 input-product-code-user');
                        $input_ProductCodeUser.attr('value', productCodeUserSub);
                        $input_ProductCodeUser.attr('placeholder', 'Mã hàng tự động');

                        //// thêm giá trị
                        var $input_ProductCode = $('<input>');
                        $input_ProductCode.attr('type', 'hidden');
                        //$input_ProductCode.attr('id', 'ProductCode_' + j);
                        $input_ProductCode.attr('name', 'List_Mst_Product[' + j + '].ProductCode');
                        $input_ProductCode.attr('class', 'col-12 input-product-code');
                        $input_ProductCode.attr('value', productCode);

                        var $input_ProductCodeBase = $('<input>');
                        $input_ProductCodeBase.attr('type', 'hidden');
                        //$input_ProductCodeBase.attr('id', 'ProductCodeBase_' + j);
                        $input_ProductCodeBase.attr('name', 'List_Mst_Product[' + j + '].ProductCodeBase');
                        $input_ProductCodeBase.attr('class', 'col-12 input-product-code-base');
                        $input_ProductCodeBase.attr('value', productCodeBase);

                        var $input_ProductCodeRoot = $('<input>');
                        $input_ProductCodeRoot.attr('type', 'hidden');
                        //$input_ProductCodeRoot.attr('id', 'ProductCodeRoot_' + j);
                        $input_ProductCodeRoot.attr('name', 'List_Mst_Product[' + j + '].ProductCodeRoot');
                        $input_ProductCodeRoot.attr('class', 'col-12 input-product-code-root');
                        $input_ProductCodeRoot.attr('value', productCodeRoot);

                        var $input_CanBeSold = $('<input>');
                        $input_CanBeSold.attr('type', 'hidden');
                        $input_CanBeSold.attr('name', 'List_Mst_Product[' + j + '].CanBeSold');
                        $input_CanBeSold.attr('class', 'col-12 input-can-be-sold');
                        $input_CanBeSold.attr('value', canBeSold);

                        var $input_CanBePurchased = $('<input>');
                        $input_CanBePurchased.attr('type', 'hidden');
                        $input_CanBePurchased.attr('name', 'List_Mst_Product[' + j + '].CanBePurchased');
                        $input_CanBePurchased.attr('class', 'col-12 input-can-be-purchased');
                        $input_CanBePurchased.attr('value', canBePurchased);

                        var $input_ValConvert = $('<input>');
                        $input_ValConvert.attr('type', 'hidden');
                        $input_ValConvert.attr('name', 'List_Mst_Product[' + j + '].ValConvert');
                        $input_ValConvert.attr('class', 'col-12 input-val-convert');
                        $input_ValConvert.attr('value', valConvert);

                        if (listAttributeSub != undefined && listAttributeSub !== null) {
                            for (var m = 0; m < listAttributeSub.length; m++) {
                                var objAttributeSub = listAttributeSub[m];
                                var attributeCodeSub = commonUtils.returnValue(objAttributeSub.AttributeCode);
                                var attributeNameSub = commonUtils.returnValue(objAttributeSub.AttributeName);
                                var $input_AttributeSub = $('<input>');
                                $input_AttributeSub.attr('type', 'hidden');
                                $input_AttributeSub.attr('name', 'List_Mst_Product[' + m + '].AttributeSub');
                                $input_AttributeSub.attr('class', 'col-12 input-attribute-sub');
                                $input_AttributeSub.attr('iidx', m);
                                $input_AttributeSub.attr('value', attributeCodeSub);
                                $input_AttributeSub.attr('data', attributeNameSub);
                                $div_ProductCodeUser.append($input_AttributeSub);
                            }
                        }

                        $div_ProductCodeUser.append($input_ProductCodeUser);
                        //$div_ProductCodeUser.append($input_ProductCode);
                        //$div_ProductCodeUser.append($input_ProductCodeBase);
                        //$div_ProductCodeUser.append($input_ProductCodeRoot);

                        $div_ProductCodeUser.append($input_ValConvert);
                        $div_ProductCodeUser.append($input_CanBeSold);
                        $div_ProductCodeUser.append($input_CanBePurchased);
                        $td_ProductCodeUser.append($div_ProductCodeUser);
                        // mã vạch
                        var $td_ProductBarCode = $('<td>');
                        $td_ProductBarCode.attr('class', 'td-product-bar-code');
                        var $div_ProductBarCode = $('<div>');
                        $div_ProductBarCode.attr('class', 'form-group no-margin div-product-bar-code');
                        var $input_ProductBarCode = $('<input>');
                        $input_ProductBarCode.attr('type', 'text');
                        //$input_ProductBarCode.attr('id', 'ProductBarCode_' + j);
                        $input_ProductBarCode.attr('name', 'List_Mst_Product[' + j + '].ProductBarCode');
                        $input_ProductBarCode.attr('class', 'col-12 input-product-bar-code');
                        $input_ProductBarCode.attr('value', productCodeUserSub);
                        $div_ProductBarCode.append($input_ProductBarCode);
                        $td_ProductBarCode.append($div_ProductBarCode);
                        // đơn vị
                        var $td_UnitCode = $('<td>');
                        $td_UnitCode.attr('class', 'td-unit-code');
                        var $div_UnitCode = $('<div>');
                        $div_UnitCode.attr('class', 'form-group no-margin div-unit-code');
                        var $input_UnitCode = $('<input>');
                        $input_UnitCode.attr('type', 'text');
                        //$input_UnitCode.attr('id', 'UnitCode_' + j);
                        $input_UnitCode.attr('name', 'List_Mst_Product[' + j + '].UnitCode');
                        $input_UnitCode.attr('class', 'col-12 input-unit-code');
                        //$input_UnitCode.attr('value', '');
                        $input_UnitCode.attr('value', unitCode);
                        $div_UnitCode.append($input_UnitCode);
                        $td_UnitCode.append($div_UnitCode);
                        // giá mua
                        var $td_UPBuy = $('<td>');
                        $td_UPBuy.attr('class', 'td-upbuy');
                        var $div_UPBuy = $('<div>');
                        $div_UPBuy.attr('class', 'form-group no-margin div-upbuy');
                        var $input_UPBuy = $('<input>');
                        $input_UPBuy.attr('type', 'text');
                        //$input_UPBuy.attr('id', 'UPBuy_' + j);
                        $input_UPBuy.attr('name', 'List_Mst_Product[' + j + '].UPBuy');
                        $input_UPBuy.attr('class', 'col-12 text-right input-upbuy ' + uPBuyCss);
                        $input_UPBuy.attr('value', uPBuy);
                        $div_UPBuy.append($input_UPBuy);
                        $td_UPBuy.append($div_UPBuy);
                        // giá bán
                        var $td_UPSell = $('<td>');
                        $td_UPSell.attr('class', 'td-upsell');
                        var $div_UPSell = $('<div>');
                        $div_UPSell.attr('class', 'form-group no-margin div-upsell');
                        var $input_UPSell = $('<input>');
                        $input_UPSell.attr('type', 'text');
                        //$input_UPSell.attr('id', 'UPSell_' + j);
                        $input_UPSell.attr('name', 'List_Mst_Product[' + j + '].UPSell');
                        $input_UPSell.attr('class', 'col-12 text-right input-upsell ' + uPSellCss);
                        $input_UPSell.attr('value', uPSell);
                        $div_UPSell.append($input_UPSell);
                        $td_UPSell.append($div_UPSell);


                        $tr.append($td_Delete);
                        $tr.append($td_ProductName);
                        $tr.append($td_ProductCodeUser);
                        $tr.append($td_ProductBarCode);
                        $tr.append($td_UnitCode);
                        $tr.append($td_UPBuy);
                        $tr.append($td_UPSell);

                        $tbody.append($tr);
                        //$('.' + uPBuyCss).number(true, 2);
                        //$('.' + uPSellCss).number(true, 2);

                        var tableName = 'Ord_OrderSR';
                        var UPBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPBuy');
                        var UPSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');

                        $('.' + uPBuyCss).number(true, UPBuyFormat);
                        $('.' + uPSellCss).number(true, UPSellFormat);
                    }
                    $table.append($tbody);
                    $divListOfTheSameTypeItems.append($table);


                    debugger
                    var tableName = 'Ord_OrderSR';
                    var UPBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
                    var UPSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');

                    $('.input-upbuy').number(true, UPBuyFormat);
                    $('.input-upsell').number(true, UPSellFormat);

                }
            }
        }
    }

    function genProducts_ByUnit_update(_listUnit) {

        var iLengthUnit = _listUnit.length;
        if (iLengthUnit > 0) {

            if (_listUnit.length > 0) {
                var listCss = [];
                var $divListOfTheSameTypeItems = $('#divListOfTheSameTypeItems');
                $divListOfTheSameTypeItems.empty();
                $divListOfTheSameTypeItems.attr('tb', 'attribute');
                // danh sách hàng hóa cùng loại
                var $table = $('<table>');
                $table.attr('class', 'table-products col-12');
                var $thead = $('<thead>');
                $thead.attr('class', 'thead-css');
                var $trThead = $('<tr>');
                $trThead.attr('class', 'trdata');
                $trThead.attr('idx', '0');
                // nút xóa
                var $thThead_Delete = $('<th>');
                $thThead_Delete.attr('class', 'text-center');
                // hàng hóa
                var $thThead_ProductName = $('<th>');
                $thThead_ProductName.attr('class', 'text-center');
                $thThead_ProductName.text('Hàng hóa');
                // mã hàng 
                var $thThead_ProductCodeUser = $('<th>');
                $thThead_ProductCodeUser.attr('class', 'text-center');
                $thThead_ProductCodeUser.text('Mã hàng');
                // mã vạch
                var $thThead_ProductBarCode = $('<th>');
                $thThead_ProductBarCode.attr('class', 'text-center');
                $thThead_ProductBarCode.text('Mã vạch');
                // đơn vị
                var $thThead_UnitCode = $('<th>');
                $thThead_UnitCode.attr('class', 'text-center');
                $thThead_UnitCode.text('Đơn vị');
                // giá mua 
                var $thThead_UPBuy = $('<th>');
                $thThead_UPBuy.attr('class', 'text-center');
                $thThead_UPBuy.text('Giá mua');
                // giá bán 
                var $thThead_UPSell = $('<th>');
                $thThead_UPSell.attr('class', 'text-center');
                $thThead_UPSell.text('Giá bán');

                $trThead.append($thThead_Delete);
                $trThead.append($thThead_ProductName);
                $trThead.append($thThead_ProductCodeUser);
                $trThead.append($thThead_ProductBarCode);
                $trThead.append($thThead_UnitCode);
                $trThead.append($thThead_UPBuy);
                $trThead.append($thThead_UPSell);

                $thead.append($trThead);

                $table.append($thead);
                var $tbody = $('<tbody>');
                $tbody.attr('class', 'tbody-products');
                var productCode = commonUtils.returnValueText('#ProductCode');
                var productCodeBase = commonUtils.returnValueText('#ProductCodeBase');
                var productCodeRoot = commonUtils.returnValueText('#ProductCodeRoot');
                var productCodeUser = commonUtils.returnValueText('#ProductCodeUser');
                var productName = commonUtils.returnValueText('#ProductName');
                var uPBuy = commonUtils.returnValueTextOrNull('#UPBuy');
                var uPSell = commonUtils.returnValueTextOrNull('#UPSell');
                var unitCode = commonUtils.returnValueText('#Unit_Root');
                var productInit = '';
                for (var j = 0; j < _listUnit.length; j++) {
                    var date = new Date();
                    var randomCur = date.getTime();
                    var jqnumber = 'jqnumber' + randomCur;
                    var uPBuyCss = 'upbuy-' + jqnumber;
                    var uPSellCss = 'upsell-' + jqnumber;
                    listCss.push(uPBuyCss);
                    listCss.push(uPSellCss);
                    if (commonUtils.isNullOrEmpty(productInit)) {
                        productInit = randomCur;
                    }
                    var listAttributeSub = [];
                    var productNameSub = '';
                    if (!commonUtils.isNullOrEmpty(productName)) {
                        productNameSub = productName + '-' + productNameSub;
                    }
                    var productCodeUserSub = productCodeUser;
                    if (!commonUtils.isNullOrEmpty(productCodeUserSub)) {
                        if (j >= 1) {
                            productCodeUserSub += ('-' + j);
                        }
                    }
                    unitCode = commonUtils.returnValue(_listUnit[j].UnitCode);
                    uPBuy = commonUtils.returnValueOrNull(_listUnit[j].UPBuy);
                    uPSell = commonUtils.returnValueOrNull(_listUnit[j].UPSell);
                    valConvert = commonUtils.returnValueOrNull(_listUnit[j].ExQty);
                    var canBeSold = commonUtils.returnValueOrNull(_listUnit[j].CanBeSold);
                    var canBePurchased = commonUtils.returnValueOrNull(_listUnit[j].CanBePurchased);
                    var $tr = $('<tr>');
                    $tr.attr('class', 'trdata');
                    $tr.attr('idx', j);
                    $tr.attr('product-base', productInit);
                    if (j === 0) {
                        $tr.attr('product-level-sys', 'ROOTPRD');
                    } else {
                        $tr.attr('product-level-sys', 'L2PRD');
                    }
                    // nút xóa
                    var $td_Delete = $('<td>');
                    $td_Delete.attr('class', 'cell-50');
                    var $aDelete = $('<a>');
                    $aDelete.attr('href', 'javascript:;');
                    $aDelete.attr('title', 'Xóa');
                    $aDelete.click(function (e) {
                        deleteRow_Product($(this));
                    });
                    var $iDelete = $('<i>');
                    $iDelete.attr('class', 'fas fa-trash-alt');
                    $aDelete.append($iDelete);
                    $td_Delete.append($aDelete);
                    // hàng hóa
                    var $td_ProductName = $('<td>');
                    $td_ProductName.attr('class', 'td-product-name');
                    var $div_ProductName = $('<div>');
                    $div_ProductName.attr('class', 'form-group no-margin div-product-name');
                    var $input_ProductName = $('<input>');
                    $input_ProductName.attr('type', 'text');
                    //$input_ProductName.attr('id', 'ProductName_' + j);
                    $input_ProductName.attr('name', 'List_Mst_Product[' + j + '].ProductName');
                    $input_ProductName.attr('class', 'col-12 input-product-name');
                    $input_ProductName.attr('value', productNameSub);
                    $input_ProductName.attr('placeholder', 'Tên hàng tự động');
                    $div_ProductName.append($input_ProductName);
                    $td_ProductName.append($div_ProductName);
                    // mã hàng
                    var $td_ProductCodeUser = $('<td>');
                    $td_ProductCodeUser.attr('class', 'td-product-code-user');
                    var $div_ProductCodeUser = $('<div>');
                    $div_ProductCodeUser.attr('class', 'form-group no-margin div-product-code-user');
                    var $input_ProductCodeUser = $('<input>');
                    $input_ProductCodeUser.attr('type', 'text');
                    //$input_ProductCodeUser.attr('id', 'ProductCodeUser_' + j);
                    $input_ProductCodeUser.attr('name', 'List_Mst_Product[' + j + '].ProductCodeUser');
                    $input_ProductCodeUser.attr('class', 'col-12 input-product-code-user');
                    $input_ProductCodeUser.attr('value', productCodeUserSub);
                    $input_ProductCodeUser.attr('placeholder', 'Mã hàng tự động');

                    //// thêm giá trị
                    var $input_ProductCode = $('<input>');
                    $input_ProductCode.attr('type', 'hidden');
                    //$input_ProductCode.attr('id', 'ProductCode_' + j);
                    $input_ProductCode.attr('name', 'List_Mst_Product[' + j + '].ProductCode');
                    $input_ProductCode.attr('class', 'col-12 input-product-code');
                    $input_ProductCode.attr('value', productCode);

                    var $input_ProductCodeBase = $('<input>');
                    $input_ProductCodeBase.attr('type', 'hidden');
                    //$input_ProductCodeBase.attr('id', 'ProductCodeBase_' + j);
                    $input_ProductCodeBase.attr('name', 'List_Mst_Product[' + j + '].ProductCodeBase');
                    $input_ProductCodeBase.attr('class', 'col-12 input-product-code-base');
                    $input_ProductCodeBase.attr('value', productCodeBase);

                    var $input_ProductCodeRoot = $('<input>');
                    $input_ProductCodeRoot.attr('type', 'hidden');
                    //$input_ProductCodeRoot.attr('id', 'ProductCodeRoot_' + j);
                    $input_ProductCodeRoot.attr('name', 'List_Mst_Product[' + j + '].ProductCodeRoot');
                    $input_ProductCodeRoot.attr('class', 'col-12 input-product-code-root');
                    $input_ProductCodeRoot.attr('value', productCodeRoot);

                    var $input_CanBeSold = $('<input>');
                    $input_CanBeSold.attr('type', 'hidden');
                    $input_CanBeSold.attr('name', 'List_Mst_Product[' + j + '].CanBeSold');
                    $input_CanBeSold.attr('class', 'col-12 input-can-be-sold');
                    $input_CanBeSold.attr('value', canBeSold);

                    var $input_CanBePurchased = $('<input>');
                    $input_CanBePurchased.attr('type', 'hidden');
                    $input_CanBePurchased.attr('name', 'List_Mst_Product[' + j + '].CanBePurchased');
                    $input_CanBePurchased.attr('class', 'col-12 input-can-be-purchased');
                    $input_CanBePurchased.attr('value', canBePurchased);

                    var $input_ValConvert = $('<input>');
                    $input_ValConvert.attr('type', 'hidden');
                    $input_ValConvert.attr('name', 'List_Mst_Product[' + j + '].ValConvert');
                    $input_ValConvert.attr('class', 'col-12 input-val-convert');
                    $input_ValConvert.attr('value', valConvert);

                    if (listAttributeSub != undefined && listAttributeSub !== null) {
                        for (var m = 0; m < listAttributeSub.length; m++) {
                            var objAttributeSub = listAttributeSub[m];
                            var attributeCodeSub = commonUtils.returnValue(objAttributeSub.AttributeCode);
                            var attributeNameSub = commonUtils.returnValue(objAttributeSub.AttributeName);
                            var $input_AttributeSub = $('<input>');
                            $input_AttributeSub.attr('type', 'hidden');
                            $input_AttributeSub.attr('name', 'List_Mst_Product[' + m + '].AttributeSub');
                            $input_AttributeSub.attr('class', 'col-12 input-attribute-sub');
                            $input_AttributeSub.attr('iidx', m);
                            $input_AttributeSub.attr('value', attributeCodeSub);
                            $input_AttributeSub.attr('data', attributeNameSub);
                            $div_ProductCodeUser.append($input_AttributeSub);
                        }
                    }

                    $div_ProductCodeUser.append($input_ProductCodeUser);
                    //$div_ProductCodeUser.append($input_ProductCode);
                    //$div_ProductCodeUser.append($input_ProductCodeBase);
                    //$div_ProductCodeUser.append($input_ProductCodeRoot);

                    $div_ProductCodeUser.append($input_ValConvert);
                    $div_ProductCodeUser.append($input_CanBeSold);
                    $div_ProductCodeUser.append($input_CanBePurchased);
                    $td_ProductCodeUser.append($div_ProductCodeUser);
                    // mã vạch
                    var $td_ProductBarCode = $('<td>');
                    $td_ProductBarCode.attr('class', 'td-product-bar-code');
                    var $div_ProductBarCode = $('<div>');
                    $div_ProductBarCode.attr('class', 'form-group no-margin div-product-bar-code');
                    var $input_ProductBarCode = $('<input>');
                    $input_ProductBarCode.attr('type', 'text');
                    //$input_ProductBarCode.attr('id', 'ProductBarCode_' + j);
                    $input_ProductBarCode.attr('name', 'List_Mst_Product[' + j + '].ProductBarCode');
                    $input_ProductBarCode.attr('class', 'col-12 input-product-bar-code');
                    $input_ProductBarCode.attr('value', productCodeUserSub);
                    $div_ProductBarCode.append($input_ProductBarCode);
                    $td_ProductBarCode.append($div_ProductBarCode);
                    // đơn vị
                    var $td_UnitCode = $('<td>');
                    $td_UnitCode.attr('class', 'td-unit-code');
                    var $div_UnitCode = $('<div>');
                    $div_UnitCode.attr('class', 'form-group no-margin div-unit-code');
                    var $input_UnitCode = $('<input>');
                    $input_UnitCode.attr('type', 'text');
                    //$input_UnitCode.attr('id', 'UnitCode_' + j);
                    $input_UnitCode.attr('name', 'List_Mst_Product[' + j + '].UnitCode');
                    $input_UnitCode.attr('class', 'col-12 input-unit-code');
                    //$input_UnitCode.attr('value', '');
                    $input_UnitCode.attr('value', unitCode);
                    $div_UnitCode.append($input_UnitCode);
                    $td_UnitCode.append($div_UnitCode);
                    // giá mua
                    var $td_UPBuy = $('<td>');
                    $td_UPBuy.attr('class', 'td-upbuy');
                    var $div_UPBuy = $('<div>');
                    $div_UPBuy.attr('class', 'form-group no-margin div-upbuy');
                    var $input_UPBuy = $('<input>');
                    $input_UPBuy.attr('type', 'text');
                    //$input_UPBuy.attr('id', 'UPBuy_' + j);
                    $input_UPBuy.attr('name', 'List_Mst_Product[' + j + '].UPBuy');
                    $input_UPBuy.attr('class', 'col-12 text-right input-upbuy ' + uPBuyCss);
                    $input_UPBuy.attr('value', uPBuy);
                    $div_UPBuy.append($input_UPBuy);
                    $td_UPBuy.append($div_UPBuy);
                    // giá bán
                    var $td_UPSell = $('<td>');
                    $td_UPSell.attr('class', 'td-upsell');
                    var $div_UPSell = $('<div>');
                    $div_UPSell.attr('class', 'form-group no-margin div-upsell');
                    var $input_UPSell = $('<input>');
                    $input_UPSell.attr('type', 'text');
                    //$input_UPSell.attr('id', 'UPSell_' + j);
                    $input_UPSell.attr('name', 'List_Mst_Product[' + j + '].UPSell');
                    $input_UPSell.attr('class', 'col-12 text-right input-upsell ' + uPSellCss);
                    $input_UPSell.attr('value', uPSell);
                    $div_UPSell.append($input_UPSell);
                    $td_UPSell.append($div_UPSell);


                    $tr.append($td_Delete);
                    $tr.append($td_ProductName);
                    $tr.append($td_ProductCodeUser);
                    $tr.append($td_ProductBarCode);
                    $tr.append($td_UnitCode);
                    $tr.append($td_UPBuy);
                    $tr.append($td_UPSell);

                    $tbody.append($tr);
                    $('.' + uPBuyCss).number(true, 2);
                    $('.' + uPSellCss).number(true, 2);
                }
                $table.append($tbody);
                $divListOfTheSameTypeItems.append($table);

            }
        }
    }

    function genProducts_ByAttribute(_listAttribute) {

        var iLengthAttribute = _listAttribute.length;
        if (iLengthAttribute > 0) {
            var myArr = [];
            for (var i = 0; i < iLengthAttribute; i++) {
                if (i === 0) {
                    if (_listAttribute[i] != undefined && _listAttribute[i] != null && _listAttribute[i].length > 0) {
                        for (var _i = 0; _i < _listAttribute[i].length; _i++) {
                            var myArrSub = [];
                            myArrSub.push(_listAttribute[i][_i]);
                            myArr.push(myArrSub);
                        }
                    }
                } else {
                    myArr = returnArray_Object(myArr, _listAttribute[i]);
                }
            }

            if (myArr.length > 0) {
                var listCss = [];
                var $divListOfTheSameTypeItems = $('#divListOfTheSameTypeItems');
                $divListOfTheSameTypeItems.empty();
                $divListOfTheSameTypeItems.attr('tb', 'attribute');
                // danh sách hàng hóa cùng loại
                var $table = $('<table>');
                $table.attr('class', 'table-products col-12');
                var $thead = $('<thead>');
                $thead.attr('class', 'thead-css');
                var $trThead = $('<tr>');
                $trThead.attr('class', 'trdata');
                $trThead.attr('idx', '0');
                // nút xóa
                var $thThead_Delete = $('<th>');
                $thThead_Delete.attr('class', 'text-center');
                // hàng hóa
                var $thThead_ProductName = $('<th>');
                $thThead_ProductName.attr('class', 'text-center');
                $thThead_ProductName.text('Hàng hóa');
                // mã hàng 
                var $thThead_ProductCodeUser = $('<th>');
                $thThead_ProductCodeUser.attr('class', 'text-center');
                $thThead_ProductCodeUser.text('Mã hàng');
                // mã vạch
                var $thThead_ProductBarCode = $('<th>');
                $thThead_ProductBarCode.attr('class', 'text-center');
                $thThead_ProductBarCode.text('Mã vạch');
                // đơn vị
                var $thThead_UnitCode = $('<th>');
                $thThead_UnitCode.attr('class', 'text-center display-none');
                $thThead_UnitCode.text('Đơn vị');
                // giá mua 
                var $thThead_UPBuy = $('<th>');
                $thThead_UPBuy.attr('class', 'text-center');
                $thThead_UPBuy.text('Giá mua');
                // giá bán 
                var $thThead_UPSell = $('<th>');
                $thThead_UPSell.attr('class', 'text-center');
                $thThead_UPSell.text('Giá bán');

                $trThead.append($thThead_Delete);
                $trThead.append($thThead_ProductName);
                $trThead.append($thThead_ProductCodeUser);
                $trThead.append($thThead_ProductBarCode);
                $trThead.append($thThead_UnitCode);
                $trThead.append($thThead_UPBuy);
                $trThead.append($thThead_UPSell);

                $thead.append($trThead);

                $table.append($thead);
                var $tbody = $('<tbody>');
                $tbody.attr('class', 'tbody-products');
                var productCode = commonUtils.returnValueText('#ProductCode');
                var productCodeBase = commonUtils.returnValueText('#ProductCodeBase');
                var productCodeRoot = commonUtils.returnValueText('#ProductCodeRoot');
                var productCodeUser = commonUtils.returnValueText('#ProductCodeUser');
                var productName = commonUtils.returnValueText('#ProductName');
                var uPBuy = commonUtils.returnValueTextOrNull('#UPBuy');
                var uPSell = commonUtils.returnValueTextOrNull('#UPSell');
                var unitCode = commonUtils.returnValueText('#Unit_Root');
                for (var j = 0; j < myArr.length; j++) {
                    var date = new Date();
                    var randomCur = date.getTime();
                    var jqnumber = 'jqnumber' + randomCur;
                    var uPBuyCss = 'upbuy-' + jqnumber;
                    var uPSellCss = 'upsell-' + jqnumber;
                    listCss.push(uPBuyCss);
                    listCss.push(uPSellCss);
                    var productInit = randomCur;
                    var listAttributeSub = [];
                    var productNameSub = '';
                    var arrAttribute = myArr[j].slice();
                    if (arrAttribute !== undefined && arrAttribute !== null) {
                        for (var l = 0; l < arrAttribute.length; l++) {
                            var objAttribute = arrAttribute[l];
                            if (l > 0) {
                                productNameSub += ',';
                            }
                            productNameSub += commonUtils.returnValue(objAttribute.AttributeName);
                            listAttributeSub.push(objAttribute);
                        }
                    }
                    if (!commonUtils.isNullOrEmpty(productName)) {
                        productNameSub = productName + '-' + productNameSub;
                    }
                    var productCodeUserSub = productCodeUser;
                    if (!commonUtils.isNullOrEmpty(productCodeUserSub)) {
                        if (j >= 1) {
                            productCodeUserSub += ('-' + j);
                        }
                    }
                    var $tr = $('<tr>');
                    $tr.attr('class', 'trdata');
                    $tr.attr('idx', j);
                    $tr.attr('product-base', productInit);
                    if (j === 0) {
                        $tr.attr('product-level-sys', 'ROOTPRD');
                    } else {
                        $tr.attr('product-level-sys', 'BASEPRD');
                    }
                    // nút xóa
                    var $td_Delete = $('<td>');
                    $td_Delete.attr('class', 'cell-50');
                    var $aDelete = $('<a>');
                    $aDelete.attr('href', 'javascript:;');
                    $aDelete.attr('title', 'Xóa');
                    $aDelete.click(function (e) {
                        deleteRow_Product($(this));
                    });
                    var $iDelete = $('<i>');
                    $iDelete.attr('class', 'fas fa-trash-alt');
                    $aDelete.append($iDelete);
                    $td_Delete.append($aDelete);
                    // hàng hóa
                    var $td_ProductName = $('<td>');
                    $td_ProductName.attr('class', 'td-product-name');
                    var $div_ProductName = $('<div>');
                    $div_ProductName.attr('class', 'form-group no-margin div-product-name');
                    var $input_ProductName = $('<input>');
                    $input_ProductName.attr('type', 'text');
                    //$input_ProductName.attr('id', 'ProductName_' + j);
                    $input_ProductName.attr('name', 'List_Mst_Product[' + j + '].ProductName');
                    $input_ProductName.attr('class', 'col-12 input-product-name');
                    $input_ProductName.attr('value', productNameSub);
                    $input_ProductName.attr('placeholder', 'Tên hàng tự động');
                    $div_ProductName.append($input_ProductName);
                    $td_ProductName.append($div_ProductName);
                    // mã hàng
                    var $td_ProductCodeUser = $('<td>');
                    $td_ProductCodeUser.attr('class', 'td-product-code-user');
                    var $div_ProductCodeUser = $('<div>');
                    $div_ProductCodeUser.attr('class', 'form-group no-margin div-product-code-user');
                    var $input_ProductCodeUser = $('<input>');
                    $input_ProductCodeUser.attr('type', 'text');
                    //$input_ProductCodeUser.attr('id', 'ProductCodeUser_' + j);
                    $input_ProductCodeUser.attr('name', 'List_Mst_Product[' + j + '].ProductCodeUser');
                    $input_ProductCodeUser.attr('class', 'col-12 input-product-code-user');
                    $input_ProductCodeUser.attr('value', productCodeUserSub);
                    $input_ProductCodeUser.attr('placeholder', 'Mã hàng tự động');

                    //// thêm giá trị
                    var $input_ProductCode = $('<input>');
                    $input_ProductCode.attr('type', 'hidden');
                    //$input_ProductCode.attr('id', 'ProductCode_' + j);
                    $input_ProductCode.attr('name', 'List_Mst_Product[' + j + '].ProductCode');
                    $input_ProductCode.attr('class', 'col-12 input-product-code');
                    $input_ProductCode.attr('value', productCode);

                    var $input_ProductCodeBase = $('<input>');
                    $input_ProductCodeBase.attr('type', 'hidden');
                    //$input_ProductCodeBase.attr('id', 'ProductCodeBase_' + j);
                    $input_ProductCodeBase.attr('name', 'List_Mst_Product[' + j + '].ProductCodeBase');
                    $input_ProductCodeBase.attr('class', 'col-12 input-product-code-base');
                    $input_ProductCodeBase.attr('value', productCodeBase);

                    var $input_ProductCodeRoot = $('<input>');
                    $input_ProductCodeRoot.attr('type', 'hidden');
                    //$input_ProductCodeRoot.attr('id', 'ProductCodeRoot_' + j);
                    $input_ProductCodeRoot.attr('name', 'List_Mst_Product[' + j + '].ProductCodeRoot');
                    $input_ProductCodeRoot.attr('class', 'col-12 input-product-code-root');
                    $input_ProductCodeRoot.attr('value', productCodeRoot);

                    var $input_CanBeSold = $('<input>');
                    $input_CanBeSold.attr('type', 'hidden');
                    $input_CanBeSold.attr('name', 'List_Mst_Product[' + j + '].CanBeSold');
                    $input_CanBeSold.attr('class', 'col-12 input-can-be-sold');
                    $input_CanBeSold.attr('value', '0');

                    var $input_CanBePurchased = $('<input>');
                    $input_CanBePurchased.attr('type', 'hidden');
                    $input_CanBePurchased.attr('name', 'List_Mst_Product[' + j + '].CanBePurchased');
                    $input_CanBePurchased.attr('class', 'col-12 input-can-be-purchased');
                    $input_CanBePurchased.attr('value', '0');

                    var $input_ValConvert = $('<input>');
                    $input_ValConvert.attr('type', 'hidden');
                    $input_ValConvert.attr('name', 'List_Mst_Product[' + j + '].ValConvert');
                    $input_ValConvert.attr('class', 'col-12 input-val-convert');
                    $input_ValConvert.attr('value', '');

                    if (listAttributeSub != undefined && listAttributeSub !== null) {
                        for (var m = 0; m < listAttributeSub.length; m++) {
                            var objAttributeSub = listAttributeSub[m];
                            var attributeCodeSub = commonUtils.returnValue(objAttributeSub.AttributeCode);
                            var attributeNameSub = commonUtils.returnValue(objAttributeSub.AttributeName);
                            var $input_AttributeSub = $('<input>');
                            $input_AttributeSub.attr('type', 'hidden');
                            $input_AttributeSub.attr('name', 'List_Mst_Product[' + m + '].AttributeSub');
                            $input_AttributeSub.attr('class', 'col-12 input-attribute-sub');
                            $input_AttributeSub.attr('iidx', m);
                            $input_AttributeSub.attr('value', attributeCodeSub);
                            $input_AttributeSub.attr('data', attributeNameSub);
                            $div_ProductCodeUser.append($input_AttributeSub);
                        }
                    }

                    $div_ProductCodeUser.append($input_ProductCodeUser);
                    //$div_ProductCodeUser.append($input_ProductCode);
                    //$div_ProductCodeUser.append($input_ProductCodeBase);
                    //$div_ProductCodeUser.append($input_ProductCodeRoot);

                    $div_ProductCodeUser.append($input_ValConvert);
                    $div_ProductCodeUser.append($input_CanBeSold);
                    $div_ProductCodeUser.append($input_CanBePurchased);
                    $td_ProductCodeUser.append($div_ProductCodeUser);
                    // mã vạch
                    var $td_ProductBarCode = $('<td>');
                    $td_ProductBarCode.attr('class', 'td-product-bar-code');
                    var $div_ProductBarCode = $('<div>');
                    $div_ProductBarCode.attr('class', 'form-group no-margin div-product-bar-code');
                    var $input_ProductBarCode = $('<input>');
                    $input_ProductBarCode.attr('type', 'text');
                    //$input_ProductBarCode.attr('id', 'ProductBarCode_' + j);
                    $input_ProductBarCode.attr('name', 'List_Mst_Product[' + j + '].ProductBarCode');
                    $input_ProductBarCode.attr('class', 'col-12 input-product-bar-code');
                    $input_ProductBarCode.attr('value', productCodeUserSub);
                    $div_ProductBarCode.append($input_ProductBarCode);
                    $td_ProductBarCode.append($div_ProductBarCode);
                    // đơn vị
                    var $td_UnitCode = $('<td>');
                    $td_UnitCode.attr('class', 'display-none td-unit-code');
                    var $div_UnitCode = $('<div>');
                    $div_UnitCode.attr('class', 'form-group no-margin div-unit-code');
                    var $input_UnitCode = $('<input>');
                    $input_UnitCode.attr('type', 'text');
                    //$input_UnitCode.attr('id', 'UnitCode_' + j);
                    $input_UnitCode.attr('name', 'List_Mst_Product[' + j + '].UnitCode');
                    $input_UnitCode.attr('class', 'col-12 input-unit-code');
                    //$input_UnitCode.attr('value', '');
                    $input_UnitCode.attr('value', unitCode);
                    $div_UnitCode.append($input_UnitCode);
                    $td_UnitCode.append($div_UnitCode);
                    // giá mua
                    var $td_UPBuy = $('<td>');
                    $td_UPBuy.attr('class', 'td-upbuy');
                    var $div_UPBuy = $('<div>');
                    $div_UPBuy.attr('class', 'form-group no-margin div-upbuy');
                    var $input_UPBuy = $('<input>');
                    $input_UPBuy.attr('type', 'text');
                    //$input_UPBuy.attr('id', 'UPBuy_' + j);
                    $input_UPBuy.attr('name', 'List_Mst_Product[' + j + '].UPBuy');
                    $input_UPBuy.attr('class', 'col-12 text-right input-upbuy ' + uPBuyCss);
                    $input_UPBuy.attr('value', uPBuy);
                    $div_UPBuy.append($input_UPBuy);
                    $td_UPBuy.append($div_UPBuy);
                    // giá bán
                    var $td_UPSell = $('<td>');
                    $td_UPSell.attr('class', 'td-upsell');
                    var $div_UPSell = $('<div>');
                    $div_UPSell.attr('class', 'form-group no-margin div-upsell');
                    var $input_UPSell = $('<input>');
                    $input_UPSell.attr('type', 'text');
                    //$input_UPSell.attr('id', 'UPSell_' + j);
                    $input_UPSell.attr('name', 'List_Mst_Product[' + j + '].UPSell');
                    $input_UPSell.attr('class', 'col-12 text-right input-upsell ' + uPSellCss);
                    $input_UPSell.attr('value', uPSell);
                    $div_UPSell.append($input_UPSell);
                    $td_UPSell.append($div_UPSell);


                    $tr.append($td_Delete);
                    $tr.append($td_ProductName);
                    $tr.append($td_ProductCodeUser);
                    $tr.append($td_ProductBarCode);
                    $tr.append($td_UnitCode);
                    $tr.append($td_UPBuy);
                    $tr.append($td_UPSell);

                    $tbody.append($tr);
                    //$('.' + uPBuyCss).number(true, 2);
                    //$('.' + uPSellCss).number(true, 2);
                    debugger
                    var tableName = 'Mst_Product';
                    var UPBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
                    $('.' + uPBuyCss).number(true, UPBuyFormat);


                    var UPSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
                    $('.' + uPSellCss).number(true, UPSellFormat);

                }
                $table.append($tbody);
                $divListOfTheSameTypeItems.append($table);


                var tableName = 'Mst_Product';
                var UPBuyFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
                $('.input-upbuy').number(true, UPBuyFormat);


                var UPSellFormat = objMst_ColumnConfig.returnValueColumnFormat_V2(tableName, 'UPSell');
                $('.input-upsell').number(true, UPSellFormat);

            }
        }
    }

    function deleteRow_Product(thiz) {

        if (thiz !== undefined && thiz !== null) {
            var tr = $(thiz).parent().parent();
            var idx = $(tr).attr('idx');
            var productLevelSys = $(tr).attr('product-level-sys');
            if (commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('ROOTPRD') ||
                commonUtils.toUpperCase(productLevelSys) === commonUtils.toUpperCase('BASEPRD')) {
                var productBase = $(tr).attr('product-base');
                //= "' + fieldId + '"
                //var ab =  $(trCur).find('td[fieldid = "' + fieldId + '"]')
                $('.tbody-products').find('tr[product-base="' + productBase + '"]').each(function () {

                    $(this).remove();
                });
            } else {
                $(tr).remove();
            }

            var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
            if (rows > 0) {

                var tbody = $('tbody.tbody-products'); //children('tr:first');
                var trFirst = $(tbody).children('tr.trdata:first');
                if (trFirst !== undefined && trFirst !== null) {
                    $(trFirst).attr('product-level-sys', 'ROOTPRD');
                }
                commonUtils.updateTableTrNotShowIdx($('tbody.tbody-products tr.trdata'), true);
            }
        }
    }

    function returnArray(arr1, arr2) {
        var myArr = [];
        var iLengthArr1 = arr1.length;
        var iLengthArr2 = arr2.length;
        for (i = 0; i < iLengthArr1; i++) {
            var _value1 = arr1[i];
            for (var j = 0; j < iLengthArr2; j++) {
                var _value2 = arr2[j];
                var data = _value1 + '-' + _value2;
                myArr.push(data);
            }
        }
        return myArr;
    }

    function returnArray_Object(arr1, arr2) {

        // object ={AttributeCode: '', AttributeName:''};
        // myArr = [ [mảng 1], [mảng 2]]
        var myArr = [];
        var iLengthArr1 = arr1.length;
        var iLengthArr2 = arr2.length;
        for (i = 0; i < iLengthArr1; i++) {
            for (var j = 0; j < iLengthArr2; j++) {
                //var arrsub = [];
                //var objarr1 = jQuery.extend(true, {}, arr1[i]);
                //arrsub.push(objarr1);
                var arrsub = arr1[i].slice();

                var attributeCode = commonUtils.returnValue(arr2[j].AttributeCode);
                var attributeName = commonUtils.returnValue(arr2[j].AttributeName);
                if (!commonUtils.isNullOrEmpty(attributeCode) && !commonUtils.isNullOrEmpty(attributeName)) {
                    var objAttribute = {
                        AttributeCode: attributeCode,
                        AttributeName: attributeName
                    };

                    arrsub.push(objAttribute);
                    myArr.push(arrsub);
                }
            }
        }
        return myArr;
    }

    this.getListAttribute = function () {
        var _listAttribute = [];
        //var objAttribute = {
        //    ProductCode: '',
        //    AttributeCode: '',
        //    AttributeValue: '',
        //};
        var rows = $('tbody#tbody-attribute tr.trdata').length;
        if (rows > 0) {
            var productCode = commonUtils.returnValueText('#ProductCode');
            var orgID = commonUtils.returnValueText('#OrgID');
            var networkID = commonUtils.returnValueText('#NetworkID');
            $('tbody#tbody-attribute tr.trdata').each(function () {

                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var select = $(tr).find('td.td-select select.select-attribute');
                    if (select !== undefined && select !== null) {
                        var attributeCode = $(select).val();
                        if (!commonUtils.isNullOrEmpty(attributeCode)) {
                            // trường hợp có chọn thuộc tính
                            // kiểm tra có giá trị của thuộc tính không
                            var inputText = $(tr).find('td.td-input-text input.value-attribute');
                            if (inputText !== undefined && inputText !== null) {
                                var _value = $(inputText).val();
                                if (!commonUtils.isNullOrEmpty(_value)) {
                                    var arrData = _value.split(',');
                                    if (arrData !== undefined && arrData !== null && arrData.length > 0) {
                                        for (var i = 0; i < arrData.length; i++) {

                                            var objAttribute = {};
                                            var _val = commonUtils.returnValue(arrData[i]);
                                            if (!commonUtils.isNullOrEmpty(_val)) {
                                                objAttribute.OrgID = orgID;
                                                objAttribute.NetworkID = networkID;
                                                objAttribute.ProductCode = productCode;
                                                objAttribute.AttributeCode = attributeCode;
                                                objAttribute.AttributeValue = _val;

                                                _listAttribute.push(objAttribute);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            });
        }
        return _listAttribute;
    };
    this.getListUnit = function () {
        var _listUnit = [];
        //var objUnit = {
        //    ProductCode: '',
        //    AttributeCode: '',
        //    AttributeValue: '',
        //};
        return _listUnit;
    };

    function IsNullOrEmpty(value) {
        if (value !== undefined && value !== null && value.toString().trim().length > 0) {
            return false;
        }
        return true;
    }

    function CheckElementExists(element) {
        if (!IsNullOrEmpty(element)) {
            if ($(element).length > 0) {
                return true;
            }
        }
        return false;
    }
    this.getData = function (flagRedirect) {
        debugger
        var token = $('#manageForm input[name=__RequestVerificationToken]').val();
        //debugger;
        var productType = commonUtils.returnValueText('#ProductType');
        var orgID = commonUtils.returnValueText('#OrgID');
        var networkID = commonUtils.returnValueText('#NetworkID');
        var productName = commonUtils.returnValueText('#ProductName');
        var productCodeUser = commonUtils.returnValueText('#ProductCodeUser');
        var productBarCode = commonUtils.returnValueText('#ProductBarCode');
        if (productBarCode === null || productBarCode === "") {
            productBarCode = productCodeUser;
        }
        var productGrpCode = commonUtils.returnValueText('#ProductGrpCode');
        var brandCode = commonUtils.returnValueText('#BrandCode');
        var uPBuy = commonUtils.returnValueTextOrNull('#UPBuy');
        var uPSell = commonUtils.returnValueTextOrNull('#UPSell');
        var qtyMinSt = commonUtils.returnValueTextOrNull('#QtyMinSt');
        var qtyEffSt = commonUtils.returnValueTextOrNull('#QtyEffSt');
        var qtyMaxSt = commonUtils.returnValueTextOrNull('#QtyMaxSt');
        var remark = commonUtils.returnValueText('#Remark');
        var productNameEN = commonUtils.returnValueText('#ProductNameEN');
        var productStd = commonUtils.returnValueText('#ProductStd');
        var productOrigin = commonUtils.returnValueText('#ProductOrigin');
        var productExpiry = commonUtils.returnValueText('#ProductExpiry');
        var productQuyCach = commonUtils.returnValueText('#ProductQuyCach');
        var productMnfUrl = commonUtils.returnValueText('#ProductMnfUrl');
        //var remark = $('textarea#Remark').val();
        var productIntro = tinyMCE.get('ProductIntro').getContent();
        var productUserGuide = tinyMCE.get('ProductUserGuide').getContent();
        var productDrawing = tinyMCE.get('ProductDrawing').getContent();
        var vatRateCode = commonUtils.returnValueText('#VATRateCode');


        debugger
        // truong dong
        if (CheckElementExists('#CustomField1')) {
            var CustomField1 = $('#CustomField1').val();
        }
        if (CheckElementExists('#CustomField2')) {
            var CustomField2 = $('#CustomField2').val();
        }
        if (CheckElementExists('#CustomField3')) {
            var CustomField3 = $('#CustomField3').val();
        }
        if (CheckElementExists('#CustomField4')) {
            var CustomField4 = $('#CustomField4').val();
        }
        if (CheckElementExists('#CustomField5')) {
            var CustomField5 = $('#CustomField5').val();
        }

        //var home = document.getElementById("iframe");
        //var img = home.
        //var innerDoc = (home.contentDocument) ? home.contentDocument : home.contentWindow.document;
        //var img = $(home).find('img').attr;
        ////var innerDoc = home.contentDocument || home.contentWindow.document;
        ////var img = home('img').attr;
        //$(img).each(function () {
        //    var x = window.location.origin;
        //    $(this).attr("src", x + "/" + $(this).attr('src')); 
        //});

        if (commonUtils.checkElementExists('#FlagSerial')) {
            var flagserial = '0';
            var inputFlagActive = $('#FlagSerial');
            if (inputFlagActive !== undefined && inputFlagActive !== null) {
                if (inputFlagActive.prop('checked')) {
                    flagserial = '1';
                }
            }
        }
        if (commonUtils.checkElementExists('#FlagLot')) {
            var flaglot = '0';
            var inputFlagActive = $('#FlagLot');
            if (inputFlagActive !== undefined && inputFlagActive !== null) {
                if (inputFlagActive.prop('checked')) {
                    flaglot = '1';
                }
            }
        }
        var unitCode = null;
        var valConvert = null;
        var canBeSold = null;
        var canBePurchased = null;

        if (!commonUtils.isNullOrEmpty(this.ActionType)) {
            if (this.ActionType === 'edit') {

            }
        }
        var Lst_Mst_Product = [];
        var Lst_Prd_Attribute = [];
        var Lst_Prd_BOM = [];
        var lstPrdDynamicField = "";
        //var lstPrdDynamicField = "{ "a": "b", "c": "d" }";

        var Lst_Mst_ProductImages = [];
        var Lst_Mst_ProductFiles = [];
        //List DynamicField
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

        //Lst_Prd_Attribute = this.getListAttribute();
        //List Bom

        var rowsgetbom = $("tbody#table-tbodyID.GetPrd tr.trdata").length;

        if (rowsgetbom > 0) {
            var trArr = $('tbody#table-tbodyID.GetPrd tr.trdata');
            if (trArr !== null && trArr.length > 0) {
                for (var i = 0; i < trArr.length; i++) {
                    var trCur = trArr[i];
                    if (trCur !== null && trCur !== undefined) {
                        var idx = $(trCur).attr('idx');
                        var temDtlCur = {};
                        temDtlCur.ProductCode = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].ProductCode"]').val();
                        temDtlCur.OrgID = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].OrgID"]').val();
                        temDtlCur.mp_UPSell = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].mp_UPSell"]').val();
                        temDtlCur.mp_UPBuy = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].mp_UPBuy"]').val();
                        temDtlCur.ProductType = $(trCur).find('input[type="hidden"][name="ListBOM[' + idx + '].ProductType"]').val();
                        temDtlCur.OrgIDParent = orgID;
                        temDtlCur.ProductCodeParent = "";
                        temDtlCur.Qty = document.getElementById("qtybuysell_" + temDtlCur.ProductCode).value;
                        Lst_Prd_BOM.push(temDtlCur);
                    }
                }
            }
        }

        //else {
        //    
        //}
        //list images
        var listImages = $('.list-images-slide').find('img');
        listImages.each(function () {
            //
            var imageSrc = commonUtils.returnValueOrNull($(this).attr('src'));
            var imageType = commonUtils.returnValueOrNull($(this).attr('data-type'));
            var imageName = commonUtils.returnValueOrNull($(this).attr('data-file-name'));
            //var imageBase64 = commonUtils.returnValueOrNull($(this).attr('data-base64'));
            var strReplace = "data:" + imageType + ";base64,";
            imageSrc = imageSrc.replace(strReplace, "");
            var imageIdx = commonUtils.returnValueOrNull($(this).attr('data-idx'));

            if (imageSrc != null && imageName != null) {
                var objImage = {
                    Idx: imageIdx,
                    ProductImageName: imageName,
                    ProductImageSpec: imageSrc
                };
                Lst_Mst_ProductImages.push(objImage);
            }
        });
        //

        //list files
        var listFiles = $('#tbody-CrCt_FileUpload').find('tr');
        listFiles.each(function () {
            //
            var fileIdx = commonUtils.returnValueOrNull($(this).attr('idx'));
            var fileName = commonUtils.returnValueOrNull($(this).attr('productfilename'));
            var filePath = $(this).find('input[name="Lst_CrCt_FileUpload[' + fileIdx + '].ProductFilePath"]').val();
            var fileType = $(this).find('input[name="Lst_CrCt_FileUpload[' + fileIdx + '].ProductFileType"]').val();
            var strReplace = "data:" + fileType + ";base64,";
            filePath = filePath.replace(strReplace, "");

            var objMst_ProductFile = {
                Idx: fileIdx,
                ProductFileName: fileName,
                ProductFileDesc: filePath,
            };
            Lst_Mst_ProductFiles.push(objMst_ProductFile);
        });
        var rows = $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').length;
        if (rows > 0) {
            $('div#divListOfTheSameTypeItems table.table-products tbody.tbody-products tr.trdata').each(function () {

                var tr = $(this);
                if (tr !== undefined && tr !== null) {
                    var idx = $(tr).attr('idx');
                    var productLevelSys = $(tr).attr('product-level-sys');
                    productName = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].ProductName"]').val());
                    productCodeUser = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].ProductCodeUser"]').val());
                    productBarCode = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].ProductBarCode"]').val());
                    if (productBarCode === null || productBarCode === "") {
                        productBarCode = productCodeUser;
                    }
                    valConvert = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].ValConvert"]').val());
                    unitCode = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].UnitCode"]').val());
                    uPBuy = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].UPBuy"]').val());
                    uPSell = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].UPSell"]').val());
                    canBeSold = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].CanBeSold"]').val());
                    canBePurchased = commonUtils.returnValueOrNull($(tr).find('input[name="List_Mst_Product[' + idx + '].CanBePurchased"]').val());

                    var objMst_Product = {};
                    objMst_Product.OrgID = orgID;
                    objMst_Product.NetworkID = networkID;
                    objMst_Product.ProductCode = null;
                    objMst_Product.ProductLevelSys = productLevelSys;
                    objMst_Product.ProductCodeUser = productCodeUser;
                    objMst_Product.BrandCode = brandCode;
                    objMst_Product.ProductType = productType;
                    objMst_Product.ProductGrpCode = productGrpCode;
                    objMst_Product.ProductName = productName;
                    objMst_Product.ProductNameEN = productName;
                    objMst_Product.ProductBarCode = productBarCode;
                    objMst_Product.ProductCodeNetwork = null;
                    objMst_Product.ProductCodeBase = null;
                    objMst_Product.ProductCodeRoot = null;
                    objMst_Product.ProductImagePathList = null;
                    objMst_Product.ProductFilePathList = null;
                    objMst_Product.UnitCode = unitCode;
                    objMst_Product.ValConvert = valConvert;
                    objMst_Product.FlagSell = canBeSold; // cờ bán
                    objMst_Product.FlagBuy = canBePurchased; // cờ mua
                    objMst_Product.UPBuy = uPBuy;
                    objMst_Product.UPSell = uPSell;
                    objMst_Product.QtyMaxSt = qtyMaxSt;
                    objMst_Product.QtyMinSt = qtyMinSt;
                    objMst_Product.QtyEffSt = qtyEffSt;
                    objMst_Product.ListOfPrdDynamicFieldValue = lstPrdDynamicField; // danh sách trường động
                    objMst_Product.Remark = remark;
                    // Trang tập hợp dữ liệu gán ở đây
                    // dữ liệu bảng hàng hóa gán vào listBOM
                    objMst_Product.FlagSerial = flagserial;
                    objMst_Product.FlagLot = flaglot;
                    objMst_Product.ProductStd = productStd;
                    objMst_Product.ProductExpiry = productExpiry;
                    objMst_Product.ProductQuyCach = productQuyCach;
                    objMst_Product.ProductMnfUrl = productMnfUrl;
                    objMst_Product.ProductIntro = productIntro;
                    objMst_Product.ProductUserGuide = productUserGuide;
                    objMst_Product.ProductDrawing = productDrawing;
                    objMst_Product.ProductOrigin = productOrigin;
                    objMst_Product.VATRateCode = vatRateCode;
                    objMst_Product.CustomField1 = CustomField1;
                    objMst_Product.CustomField2 = CustomField2;
                    objMst_Product.CustomField3 = CustomField3;
                    objMst_Product.CustomField4 = CustomField4;
                    objMst_Product.CustomField5 = CustomField5;
                    objMst_Product.FlagFG = '0';
                    //objMst_Product.Lst_Prd_BOM = Lst_Prd_BOM;
                    //

                    // danh sách thuộc tính sản phẩm
                    var _listAttribute = [];

                    $(tr).find('input.input-attribute-sub').each(function () {

                        var inputCur = $(this);
                        var iidx = $(inputCur).attr('iidx');
                        //var attributeCodeSub = commonUtils.returnValueOrNull($(inputCur).find('input[name="List_Mst_Product[' + iidx + '].AttributeSub"]').val());
                        //var attributeNameSub = commonUtils.returnValueOrNull($(inputCur).find('input[name="List_Mst_Product[' + iidx + '].AttributeSub"]').attr('data'));
                        var attributeCodeSub = commonUtils.returnValueOrNull($(inputCur).val());
                        var attributeValueSub = commonUtils.returnValueOrNull($(inputCur).attr('data'));
                        var attributeSub = {
                            AttributeCode: attributeCodeSub,
                            AttributeValue: attributeValueSub
                        };
                        _listAttribute.push(attributeSub)
                    });
                    objMst_Product.Lst_Prd_Attribute = _listAttribute;
                    Lst_Mst_Product.push(objMst_Product);
                }
            });
        } else {
            canBeSold = commonUtils.returnValueOrNull($('tr.unitcode-root').find('#FlagSell_Root').val());
            canBePurchased = commonUtils.returnValueOrNull($('tr.unitcode-root').find('#FlagBuy_Root').val());
            unitCode = commonUtils.returnValueText('#Unit_Root');
            var objMst_Product = {};
            objMst_Product.OrgID = orgID;
            objMst_Product.NetworkID = networkID;
            objMst_Product.ProductCode = null;
            objMst_Product.ProductLevelSys = 'ROOTPRD';
            objMst_Product.ProductCodeUser = productCodeUser;
            objMst_Product.BrandCode = brandCode;
            objMst_Product.ProductType = productType;
            objMst_Product.ProductGrpCode = productGrpCode;
            objMst_Product.ProductName = productName;
            objMst_Product.ProductNameEN = productNameEN;
            objMst_Product.ProductBarCode = productBarCode;
            objMst_Product.ProductCodeNetwork = null;
            objMst_Product.ProductCodeBase = null;
            objMst_Product.ProductCodeRoot = null;
            objMst_Product.ProductImagePathList = null;
            objMst_Product.ProductFilePathList = null;
            objMst_Product.UnitCode = unitCode;
            objMst_Product.ValConvert = 1;
            objMst_Product.FlagSell = canBeSold; // cờ bán
            objMst_Product.FlagBuy = canBePurchased; // cờ mua
            objMst_Product.UPBuy = uPBuy;
            objMst_Product.UPSell = uPSell;
            objMst_Product.QtyMaxSt = qtyMaxSt;
            objMst_Product.QtyMinSt = qtyMinSt;
            objMst_Product.QtyEffSt = qtyEffSt;
            objMst_Product.ListOfPrdDynamicFieldValue = lstPrdDynamicField; // danh sách trường động
            objMst_Product.Remark = remark;
            objMst_Product.FlagFG = '0';
            // Trang tập hợp dữ liệu gán ở đây
            // dữ liệu bảng hàng hóa gán vào listBOM
            objMst_Product.FlagSerial = flagserial;
            objMst_Product.FlagLot = flaglot;
            objMst_Product.ProductStd = productStd;
            objMst_Product.ProductExpiry = productExpiry;
            objMst_Product.ProductQuyCach = productQuyCach;
            objMst_Product.ProductMnfUrl = productMnfUrl;
            objMst_Product.ProductIntro = productIntro;
            objMst_Product.ProductUserGuide = productUserGuide;
            objMst_Product.ProductDrawing = productDrawing;
            objMst_Product.ProductOrigin = productOrigin;
            objMst_Product.VATRateCode = vatRateCode;

            objMst_Product.CustomField1 = CustomField1;
            objMst_Product.CustomField2 = CustomField2;
            objMst_Product.CustomField3 = CustomField3;
            objMst_Product.CustomField4 = CustomField4;
            objMst_Product.CustomField5 = CustomField5;

            //
            //objMst_Product.FlagActive = "1";
            //objMst_Product.Lst_Prd_BOM = Lst_Prd_BOM;
            //

            Lst_Mst_Product.push(objMst_Product);
        }
        var tem = {};
        tem.Lst_Mst_ProductUI_Create = Lst_Mst_Product;
        //tem.Lst_Prd_Attribute = Lst_Prd_Attribute;
        tem.Lst_Mst_ProductImages = Lst_Mst_ProductImages;
        tem.Lst_Mst_ProductFiles = Lst_Mst_ProductFiles;
        tem.Lst_Prd_BOM = Lst_Prd_BOM;
        tem.FlagRedirect = flagRedirect;
        var modelCur = commonUtils.returnJSONValue(tem);
        var data = {
            __RequestVerificationToken: token,
            model: modelCur,
        };
        return data;
    };
    this.saveData = function (flagRedirect) {

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
                doneFunction(objResult, flagRedirect);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                failFunction(jqXHR, textStatus, errorThrown);
            }).always(function () {
                alwaysFunction();
            });
        }
    };
    function doneFunction(objResult, flagRedirect) {
        if (objResult.Success) {
            if (!commonUtils.isNullOrEmpty(objResult.Messages)) {
                var listToastr = [];
                objToastr = { ToastrType: 'success', ToastrMessage: objResult.Messages };
                listToastr.push(objToastr);
                commonUtils.showToastr(listToastr);
            }
            if (flagRedirect === 0) {
                if (!commonUtils.isNullOrEmpty(objResult.RedirectUrl)) {
                    commonUtils.window_location_href(objResult.RedirectUrl);
                }
            }
            if (flagRedirect === 1) {
                window.location.href = window.location.href;
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
};