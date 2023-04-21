var commonUtils = new function () {
    this.isNullOrEmpty = function(value) {
        if (value !== undefined && value !== null && value.toString().trim().length > 0) {
            return false;
        }
        return true;
    };
    this.isNumber = function (number) {
        // commonUtils.isNumber('a') => true; (không là số)
        // commonUtils.isNumber(1.5.5) => true; (không là số)
        // commonUtils.isNumber(1,5) => true; (không là số) (dấu '.' mới hợp lệ (là phần phân cách thập phân))
        // commonUtils.isNumber(1111.555) => false; (là số)
        var check = false;
        if (!this.isNullOrEmpty(number)) {
            if (!isNaN(number)) {
                check = true; // là số
            }
        }
        return check;
        //return /^-?[\d.]+(?:e-?\d+)?$/.test(number);
    };
    this.addClassCss = function (idorclass, classcss) {
        if (!this.isNullOrEmpty(classcss)) {
            if (!$(idorclass).hasClass(classcss)) {
                $(idorclass).addClass(classcss);
            }
        }
    };
    this.removeClassCss = function (idorclass, classcss) {
        if (!this.isNullOrEmpty(classcss)) {
            if ($(idorclass).hasClass(classcss)) {
                $(idorclass).removeClass(classcss);
            }
        }
    };
    this.checkElementUndefinedOrNull = function (element) {
        if (element !== undefined && element !== null) {
            return false;
        }
        return true;
    };
    this.checkDate = function (date) {
        if (this.isNullOrEmpty(date)) {
            return false;
        }
        var _date = new Date(date);
        var check = (_date instanceof Date);
        return check;
    };
    this.checkElementExists = function (element) {
        if (!this.isNullOrEmpty(element)) {
            if ($(element).length > 0) {
                return true;
            }
        }
        return false;
    };
    this.checkElementIsNullOrEmpty = function (idorclass, classcss, message) {
        var check = true;
        var _value = '';
        if ($(idorclass).length > 0) {
            _value = $(idorclass).val();
            if (this.isNullOrEmpty(_value)) {
                check = false;
                AddClassCss(idorclass, classcss);
                $(idorclass).focus();
                if (!this.isNullOrEmpty(message)) {
                    alert(message);
                }
                return false;
            }
        }
        if (check) {
            this.removeClassCss(idorclass, classcss);
        }
        return check;
    };
    this.checkElementIsDate = function (idorclass, classcss, message) {
        var check = true;
        _value = $(idorclass).val();
        check = this.checkDate(_value);
        if (!check) {
            AddClassCss(idorclass, classcss);
            $(idorclass).focus();
            if (!this.isNullOrEmpty(message)) {
                alert(message);
            }
            return false;
        }
        else {
            RemoveClassCss(idorclass, classcss);
        }
        return check;
    };
    this.checkElementIsNumber = function (idorclass, classcss, message) {
        var check = true;
        _value = $(idorclass).val();
        if (!this.isNumber(_value)) {
            check = false;
            this.addClassCss(idorclass, classcss);
            $(idorclass).focus();
            if (!this.isNullOrEmpty(message)) {
                alert(message);
            }
            return false;
        }
        if (check) {
            this.removeClassCss(idorclass, classcss);
        }
        return check;
    };
    this.checkElementIsNumber_GE_Zero = function (idorclass, classcss, message) {
        // Is greater than or equal to
        var check = true;
        _value = $(idorclass).val();
        if (this.isNumber(_value)) {
            if (parseFloat(_value.toString().trim()) < 0) {
                check = false;
            }
        } else {
            check = false;
        }
        if (check) {
            this.removeClassCss(idorclass, classcss);
        } else {
            this.addClassCss(idorclass, classcss);
            $(idorclass).focus();
            if (!this.isNullOrEmpty(message)) {
                alert(message);
            }
        }
        return check;
    };
    this.checkIsNumber_IsGreater_Zero = function (idorclass, classcss, message) {
        var check = true;
        _value = $(idorclass).val();
        if (this.isNumber(_value)) {
            if (parseFloat(_value.toString().trim()) <= 0) {
                check = false;
            }
        } else {
            check = false;
        }
        if (check) {
            this.removeClassCss(idorclass, classcss);
        } else {
            this.addClassCss(idorclass, classcss);
            $(idorclass).focus();
            if (!this.isNullOrEmpty(message)) {
                alert(message);
            }
        }
        return check;
    };
    this.checkIsNumber_IsGreaterOrSame_Zero = function (idorclass, classcss, message) {
        var check = true;
        _value = $(idorclass).val();
        if (this.isNumber(_value)) {
            if (parseFloat(_value.toString().trim()) < 0) {
                check = false;
            }
        } else {
            check = false;
        }
        if (check) {
            this.removeClassCss(idorclass, classcss);
        } else {
            this.addClassCss(idorclass, classcss);
            $(idorclass).focus();
            if (!this.isNullOrEmpty(message)) {
                alert(message);
            }
        }
        return check;
    };
    this.returnJSONValue = function (data) {
        var value = null;
        if (!this.isNullOrEmpty(data)) {
            value = JSON.stringify(data);
        }
        return value;
    };
    this.returnValueOrNull = function (data) {
        var value = null;
        if (!this.isNullOrEmpty(data)) {
            value = data.toString().trim();
        }
        return value;
    };
    this.returnValue = function (data) {
        var value = '';
        if (!this.isNullOrEmpty(data)) {
            value = data.toString().trim();
        }
        return value;
    };
    this.returnValueTextOrNull = function (element) {
        var value = null;
        if (this.checkElementExists(element)) {
            var _value = $(element).val();
            if (!this.isNullOrEmpty(_value)) {
                value = _value.toString().trim();
            }
        }
        return value;
    };
    this.returnValueText = function (element) {
        var value = '';
        if (this.checkElementExists(element)) {
            var _value = $(element).val();
            if (!this.isNullOrEmpty(_value)) {
                value = _value.toString().trim();
            }
        }
        return value;
    };
    this.returnValueInt = function (element) {
        var value = 0;
        if (this.checkElementExists(element)) {
            var _value = $(element).val();
            if (!this.isNullOrEmpty(_value) && this.isNumber(_value)) {
                value = parseInt(_value);
            }
        }
        return value;
    };
    this.returnValueFloat = function (element) {
        var value = 0.0;
        if (this.checkElementExists(element)) {
            var _value = $(element).val();
            if (!this.isNullOrEmpty(_value) && this.isNumber(_value)) {
                value = parseFloat(_value);
            }
        }
        return value;
    };
    this.returnValueMinOrMax = function () {
        if (parseInt(value) < min || isNaN(parseInt(value)))
            return min;
        else if (parseInt(value) > max)
            return max;
        else return value;
    };
    this.parseInt = function(number) {
        var value = 0;
        if (!this.isNullOrEmpty(number) && this.isNumber(number)) {
            value = parseInt(number);
        } 
        return value;
    };
    this.parseFloat = function (number) {
        var value = 0.0;
        if (!this.isNullOrEmpty(number) && this.isNumber(number)) {
            value = parseFloat(number);
        }
        return value;
    };
    this.totalParseInt = function (element1, element2) {
        var total = 0;
        var value1 = 0;
        var value2 = 0;
        value1 = this.returnValueInt(element1);
        value2 = this.returnValueInt(element2);
        total = value1 + value2;
        return total;
    };
    this.totalParseFloat = function (element1, element2) {
        var total = 0.0;
        var value1 = 0.0;
        var value2 = 0.0;
        value1 = this.returnValueFloat(element1);
        value2 = this.returnValueFloat(element2);
        total = value1 + value2;
        return total;
    };
    this.lamTronSo = function(number, scale) {
        //number: số
        //scale: làm tròn bao nhiêu số sau dấu thập phân
        var _number = this.parseFloat(number.toString());
        var _scale = this.parseFloat(scale.toString());

        if (!("" + _number).includes("e")) {
            return +(Math.round(_number + "e+" + _scale) + "e-" + _scale);
        } else {
            var arr = ("" + _number).split("e");
            var sig = "";
            if (+arr[1] + _scale > 0) {
                sig = "+";
            }
            var i = +arr[0] + "e" + sig + (+arr[1] + _scale);
            var j = Math.round(i);
            var k = +(j + "e-" + _scale);
            return k;
        }
    };
    this.toLowerCase = function(data) {
        var value = '';
        if (!this.isNullOrEmpty(data)) {
            value = data.toString().trim().toLowerCase();
        }
        return value;
    };
    this.toUpperCase = function (data) {
        var value = '';
        if (!this.isNullOrEmpty(data)) {
            value = data.toString().trim().toUpperCase();
        }
        return value;
    };
    this.locDau = function (thiz) {
        // using : onkeyup="commonUtils.locDau(this);"
        var str;
        if (eval(thiz)) {
            str = eval(thiz).value;
        } else {
            str = thiz;
        }

        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ |ặ|ẳ|ẵ/g, "a");

        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ |Ặ|Ẳ|Ẵ/g, "A");

        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");

        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");

        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");

        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");

        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ |ợ|ở|ỡ/g, "o");

        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ |Ợ|Ở|Ỡ/g, "O");

        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");

        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");

        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");

        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");

        str = str.replace(/đ/g, "d");

        str = str.replace(/Đ/g, "D");

        //str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\\|\||\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
        //str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\\|\||\<|\>|\?|\/|,|\:|\;|\'| |\"|\&|\#|\$|\`|\[|\]|~|$|_/g, "-"); // cho phép nhập dấu ., các ký tự ko cho phép -> -
        //str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\\|\||\<|\>|\?|\/|,|\:|\;|\'| |\"|\&|\#|\$|\`|\[|\]|~|$|/g, ""); // cho phép nhập dấu ., các ký tự ko cho phép -> -
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\\|\||\<|\>|\?|,|\:|\;|\'| |\"|\&|\#|\$|\`|\[|\]|~|$|/g, ""); // cho phép nhập dấu ., các ký tự ko cho phép -> -
        /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */

        str = str.replace(/-+-/g, "-");//thay thế 2- thành 1- 
        str = str.replace(/_+_/g, "_");//thay thế 2 _ thành 1 _
        str = str.replace(/\.+\./g, ".");//thay thế 2 . thành 1 .

        str = str.replace(/^\-+/g, ""); //cắt bỏ ký tự - ở đầu
        str = str.replace(/^\_+/g, ""); //cắt bỏ ký tự _ ở đầu
        str = str.replace(/^\.+/g, ""); //cắt bỏ ký tự . ở đầu

        //str = str.replace(/^\-+|\-+$/g, ""); //
        //str = str.replace(/\-/g, "");
        //cắt bỏ ký tự - ở đầu và cuối chuỗi 

        eval(thiz).value = str.trim();

    };
    this.reverse = function(chuoi) {
        // Đảo ngược chuỗi
        var _chuoi = '';
        for (var i = chuoi.length - 1; i >= 0; i--)
            _chuoi += chuoi[i];
        return _chuoi;
    };
    this.catChuoi = function (chuoi, soluongkytu) {
        var subchuoi = '';
        if (!IsNullOrEmpty(chuoi)) {
            chuoi = chuoi.toString().trim();
            if (chuoi.length <= soluongkytu) {
                subchuoi = chuoi;
            } else {
                var indexOf = chuoi.lastIndexOf(" ", soluongkytu);
                if (indexOf > 0) {
                    subchuoi = chuoi.substring(0, indexOf).trim() + '...';
                } else {
                    subchuoi = chuoi.substring(0, soluongkytu).trim() + '...';
                }
            }
        }
        return subchuoi;
    };
    this.blockSpecialChar = function (e) {
        // không cho nhập ký tự đặc biệt
        // <input type="text" name="txtName"  onkeypress="return commonUtils.blockSpecialChar(event)"/>
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k === 8 || k === 32 || (k >= 48 && k <= 57));
    };
    this.allLetter = function (inputtxt) {
        // Chỉ cho nhập ký tự
        // <input type="text" id="inputText" name="inputText"/>
        // allLetter('#inputText');
        var letters = /^[A-Za-z]+$/;
        if ($(inputtxt).val().match(letters)) {
            return true;
        }
        else {
            return false;
        }
    };
    this.replaceAll = function (chuoi, chuoicanthaythe, chuoithaythe) {
        //var chuoi = 'Báo điện -tử-dân trí baodientudantri-https://dantri.com.vn';
        //var chuoicanthaythe = "/";
        //var chuoithaythe = " // ";
        var patt = new RegExp(chuoicanthaythe, "g");
        return  chuoi.replace(patt, chuoithaythe);
    };
    this.replaceAll_Arrays_ChuoiCanThayThe = function (chuoi, chuoicanthaythe, chuoithaythe) {
        //var chuoi = 'Báo điện -tử-dân trí baodientudantri-https://dantri.com.vn';
        //var chuoicanthaythe = ["/", "-"];
        //var chuoithaythe = ["//", " – "];
        if (chuoicanthaythe !== null && chuoicanthaythe.length > 0) {
            for (var i = 0; i < chuoicanthaythe.length; i++) {
                var _chuoicanthaythe = chuoicanthaythe[i];
                var _chuoithaythe = chuoithaythe[i];
                var patt = new RegExp(_chuoicanthaythe, "g");
                chuoi = chuoi.replace(patt, _chuoithaythe);
            }
        }
        return chuoi;
    };
    this.formatNumber = function (number, scale) {
        //number: số
        //scale: lấy bao số phần thập phân
        // using fnumber = formatNumber(parseFloat(number), parseInt(scale));

        var _number = number.toFixed(scale) + '';
        var x = _number.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        if (scale === 0) {
            return x1; //10,000
        } else {
            return x1 + x2; //10,000.05
        }
    };
    this.isNumberKey = function (evt) {
        //<input id="txtChar" onKeyPress="return isNumberKey(event)" type="text" name="txtChar">
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    };
    this.validateMobile = function (phoneno) {
        // Số di động
        // true: hợp lệ
        var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        if (!IsNullOrEmpty(phoneno)) {
            phoneno = phoneno.toString().trim();
            var phone = phoneno.replace('(84)', '0');
            phone = phoneno.replace('+84', '0');
            phone = phoneno.replace('0084', '0');
            phone = phoneno.replace(/ /g, '');
            var validateMobile = vnf_regex.test(phone);
            return validateMobile;
            //return vnf_regex.test(phoneno);
        } else {
            return false;
        }
    };
    this.validateEmail = function (email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    };
    this.validateHhMm = function(time) {
        var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(time);
        if (isValid) {
            return true;
        } else {
            return false;
        }
    };
    this.dateTimeSubtractDays = function (first, second) {
        var timeFirst = new Date(first).getTime();
        var timeSecond = new Date(second).getTime();
        var diff = timeFirst - timeSecond;
        var days = Math.floor(diff / (1000 * 60 * 60 * 24));
        return days;
    };
    this.dateTimeAddDays = function (first, second) {
        var date = new Date(first);
        date.setDate(date.getDate() + second);
        return date;
    };
    this.compareTwoDates = function (first, second) {
        // first > second => return true 
        var timeFirst = new Date(first).getTime();
        var timeSecond = new Date(second).getTime();
        if (timeFirst > timeSecond) {
            return true;
        } else {
            return false;
        }
    };
    this.compareTwoDates_GE = function(first, second) {
        // Is greater than or equal to
        // first >= second => return true 
        var timeFirst = new Date(first).getTime();
        var timeSecond = new Date(second).getTime();
        if (timeFirst >= timeSecond) {
            return true;
        } else {
            return false;
        }
    };
    this.totalMonths = function (enddate, startdate) {
        var totalMonths;
        var startdateCur = new Date(startdate);
        var enddateCur = new Date(enddate);
        var startMonthCur = startdateCur.getMonth();
        var startYearCur = startdateCur.getFullYear();
        var endMonthCur = enddateCur.getMonth();
        var endYearCur = enddateCur.getFullYear();
        totalMonths = (endYearCur - startYearCur) * 12;
        //totalMonths -= startMonthCur + 1;
        totalMonths -= (startMonthCur + 1);
        totalMonths += (endMonthCur + 1);
        return totalMonths <= 0 ? 0 : totalMonths;
        //return totalMonths;
    };
    this.setDefaultValueZero = function (thiz) {
        setTimeout(function () {
            var valueCur = $(thiz).val();
            if (IsNullOrEmpty(valueCur)) {
                $(thiz).val('0');
            }
        });
    };
    this.setValueElement = function (thiz, data) {
        if (!this.checkElementUndefinedOrNull(thiz)) {
            //if (!this.isNullOrEmpty(data)) {
            //    var value = this.returnValue(data);
            //    $(thiz).val(value);
            //}
           
            $(thiz).val(data);
        }
    };
    this.setValueAttributeElement = function (thiz, attribute, data) {
        if (!this.checkElementUndefinedOrNull(thiz)) {
            if (!this.isNullOrEmpty(attribute)) {
                //var value = this.returnValue(data);
                $(thiz).attr(attribute, data);
            }
        }
    };
    this.getValueAttributeElement = function (thiz, attribute) {
        var value = '';
        if (!this.checkElementUndefinedOrNull(thiz)) {
            if (!this.isNullOrEmpty(attribute)) {
                var data = $(thiz).attr(attribute);
                value = this.returnValue(data);
            }
        }
        return value;
    };
    this.checkAll_CheckBox = function (thiz, inputcheckbox) {
        // inputcheckbox: '.table-tbody input.ace'
        // inputcheckbox: '#table-tbodyID input.sl_ace'
        // inputcheckbox: '.table-tbody input.chked'
        var c_all = false;
        if ($(thiz).is(":checked")) {
            c_all = true;
        }
        $(inputcheckbox).prop("checked", c_all);
    };
    this.checkBox = function (thiz) {
        var c_all = false;
        if ($(thiz).is(":checked")) {
            c_all = true;
        }
        if (!c_all) {
            $(".table-thead input.ace").prop("checked", c_all);
        }
        else {
            var rows = $(".table-tbody tbody tr.trdata").length;
            if (rows > 0) {
                //var trArr = $('.table-tbody tbody tr.trdata').has('input[class="ace"]:checked');
                var trArr = $('.table-tbody tbody tr.trdata').has('input[type=checkbox]:checked');
                if (trArr !== null) {
                    var trLength = trArr.length;
                    if (trLength === rows) {
                        $(".table-thead input.ace").prop("checked", c_all);
                    }
                    else {
                        $(".table-thead input.ace").prop("checked", !c_all);
                    }
                }
                else {
                    $(".table-thead input.ace").prop("checked", !c_all);
                }
            }
        }
    };
    this.changeCheckBox = function (thiz) {
        var checkBox = $(thiz);
        if (checkBox !== null && checkBox !== undefined) {
            if (checkBox.is(':checked')) {
                checkBox.prop('checked', true);
                checkBox.val(true);
            } else {
                checkBox.prop('checked', false);
                checkBox.val(false);
            }
        }
    };
    this.checkFileExcelImport = function (thiz, e) {
        var checkFile = false;
        var fileName = e.target.files[0].name;
        if (fileName !== undefined && fileName !== null && fileName.trim().length > 0) {
            var _index = fileName.lastIndexOf('.');
            if (_index !== undefined && _index !== null && _index > 0) {
                var fileType = fileName.substring(_index + 1, fileName.length).toLowerCase();
                if (fileType === 'xls' || fileType.toLowerCase() === 'xlsx') {
                    checkFile = true;
                }
            }
        }
        if (!checkFile) {
            alert("File excel Import không hợp lệ!");
            $(thiz).val('');
            return false;
        }
        return true;
    };
    this.checkFileJsonImport = function (thiz, e) {
        var checkFile = false;
        var fileName = e.target.files[0].name;
        if (fileName !== undefined && fileName !== null && fileName.trim().length > 0) {
            var _index = fileName.lastIndexOf('.');
            if (_index !== undefined && _index !== null && _index > 0) {
                var fileType = fileName.substring(_index + 1, fileName.length).toLowerCase();
                if (fileType === 'json') {
                    checkFile = true;
                }
            }
        }
        if (!checkFile) {
            alert("File Import không hợp lệ!");
            $(thiz).val('');
            return false;
        }
        return true;
    };
    this.readURL = function (input) {
        var thumbimages = "#" + $(input).attr("inputid");
        if (input.files && input.files[0]) {
            var name = input.files[0].name;
            if (!name.match(/(?:gif|jpg|jpeg|png|bmp|GIF|JPG|JPEG|PNG|BMP)$/)) {
                alert("File upload phải thuộc các định dạng sau: \" gif | jpg | png | bmp \"!");
                return false;
            } else {
                var reader = new window.FileReader();
                reader.onload = function (e) {
                    $(thumbimages).attr('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
                $(thumbimages).show();
            }
        }
        else {
            $(thumbimages).attr('src', input.value);
            $(thumbimages).show();
        }
        $('.filename').text($(".uploadfile").val());
        return false;
    };
    this.readFileURL = function (input) {
        var thumbfile = "#" + $(input).attr("inputid");
        if (input.files && input.files[0]) {
            var name = input.files[0].name;
            if (!name.match(/(?:doc|docx|xls|xlsx|ppt|ppts|pps|ppsx|pptx|mdb|pdf|psd|gif|jpg|jpeg|png|bmp|rar|zip|html|htm|xml)$/)) {
                alert("File upload phải thuộc các định dạng sau: \" doc | docx | xls | xlsx | ppt | ppts | pps | ppsx | pptx | mdb | pdf | psd | gif | jpg | jpeg | png | bmp | rar | zip | html | htm | xml \"!");
                return false;
            } else {
                var reader = new window.FileReader();
                reader.onload = function (e) {
                    if (name.match(/(?:gif|jpg|jpeg|png|bmp|GIF|JPG|JPEG|PNG|BMP)$/)) {
                        $(thumbfile).attr('src', e.target.result);
                    } else {
                        $(thumbfile).attr('src', '/Content/assets/avatars/profile-pic.jpg');
                    }
                };
                reader.readAsDataURL(input.files[0]);
                $(thumbfile).show();
            }
        }
        else {
            $(thumbfile).attr('src', input.value);
            $(thumbfile).show();
        }
        $('.filename').text($(".uploadfile").val());
        return false;
    };
    this.getUrlParameter = function (strparam) {
        // using: http://dummy.com/?technology=jquery&blog=jquerybyexample
        // var tech = getUrlParameter('technology');
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;
        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] === strparam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };
    this.updateTableTrIdx = function ($selector, displayIdx) {
        var idx = 0;
        $selector.each(function () {
            var tr = $(this);
            var odx = tr.attr('idx');
            if (odx !== undefined) {
                if (displayIdx === true) {
                    var ftd = tr.find('td').eq(0).text(idx + 1);
                }
                odx = odx * 1;
                tr.find('input[name*="[' + odx + ']"]').each(function () {
                    var name = $(this).attr('name');
                    var nname = name.replace('[' + odx + ']', '[' + idx + ']');
                    $(this).attr('name', nname);
                });
                tr.find('textarea[name*="[' + odx + ']"]').each(function () {
                    var name = $(this).attr('name');
                    var nname = name.replace('[' + odx + ']', '[' + idx + ']');
                    $(this).attr('name', nname);
                });
            }
            tr.attr('idx', idx);
            idx++;
        });
    };
    this.updateTableTrNotShowIdx = function ($selector, displayIdx) {
        var idx = 0;
        $selector.each(function () {
            var tr = $(this);
            var odx = tr.attr('idx');
            if (odx !== undefined) {
                if (displayIdx === true) {
                    //var ftd = tr.find('td').eq(0).text(idx + 1);
                }
                odx = odx * 1;
                tr.find('input[name*="[' + odx + ']"]').each(function () {
                    var name = $(this).attr('name');
                    var nname = name.replace('[' + odx + ']', '[' + idx + ']');
                    $(this).attr('name', nname);
                });
                tr.find('textarea[name*="[' + odx + ']"]').each(function () {
                    var name = $(this).attr('name');
                    var nname = name.replace('[' + odx + ']', '[' + idx + ']');
                    $(this).attr('name', nname);
                });
            }
            tr.attr('idx', idx);
            idx++;
        });
    };
    this.getHtmlFromTemplate = function ($t, data, extData) {
        var html = $t.html();
        for (var key in data) {
            var s = '==' + key + '=='
            html = html.replace(new RegExp(s, 'g'), data[key]);
        }
        if (extData !== undefined) {
            for (var keyext in extData) {
                var sext = '==' + keyext + '=='
                html = html.replace(new RegExp(sext, 'g'), extData[keyext]);
            }
        }
        return html;
    };
    this.setAdminCurrentTag = function (parents, parentText) {
        for (var i = 0; i < parents.length; i++) {
            var childLi = "";
            if (i !== 0) {
                childLi += "<li>";
            } else {
                childLi += "<li class=\"breadcrumb-remove-content\">";
            }
            if (parents[i] !== null && parents[i] !== undefined && parents[i].toString().trim().length > 0 && parents[i].toString().trim() !== '#') {
                childLi += "<a href='" + parents[i] + "'\>" + parentText[i] + "</a>";

            } else {
                childLi += "<span style=\"color: ##3b81ab;\">" + parentText[i] + "</span>";
            }
            childLi += "</li>";
            //alert(parents[i] + "ABC" + parentText[i]);
            $('ul.breadcrumb').append(childLi);
        }
    };
    this.setAdminCurrentUrl = function (url) {
        $('.nav-list a[href="' + url + '"]').each(function () {
            $(this).closest('li').addClass('active');
            $(this).closest('li').closest('ul').show();
            $(this).closest('li').closest('ul').closest('li').addClass('active open');

            $(this).closest('li').closest('ul').closest('li').closest('ul').closest('li').addClass('active open');
        });

        //Thêm mới cập nhật menu ngang 07/06/2019
        $('.nav.navbar-nav a[href="' + url + '"]').each(function () {
            $(this).closest('li').addClass('active');
            $(this).closest('li').closest('ul').show();
            $(this).closest('li').closest('ul').closest('li').addClass('active open');

            $(this).closest('li').closest('ul').closest('li').closest('ul').closest('li').addClass('active open');
        });
    };
    this.closeModalPopup = function (modalPopup) {
        $('#' + modalPopup).modal("hide");
        $('#' + modalPopup).html('');
    };
    this.setFocus = function (element) {
        // setFocus
        // <input type="text" id="inputText" name="inputText"/>
        // setFocus('inputText');
        window.setTimeout(function () {
            document.getElementById(element).focus();
        }, 0);
    };
    this.goBack = function () {
        window.history.back();
    };
    this.window_location_href = function (url) {
        window.location.href = url;
    }
    this.showAlert = function (message) {
        alert(message);
    };
    this.showErrorMessage = function () {

    };
}

