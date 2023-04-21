function goBack() {
    window.history.back();
}

function setAdminCurrentTag(parents, parentText) {
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
}

function setAdminCurrentUrl(url) {
    $('.nav-pills a[href="' + url + '"]').each(function () {
        $(this).closest('li').addClass('active');
        $(this).closest('li').closest('ul').show();
        $(this).closest('li').closest('ul').closest('li').addClass('menu-open active open');

        $(this).closest('li').closest('ul').closest('li').closest('ul').closest('li').addClass('menu-open active open');
    });

    //Thêm mới cập nhật menu ngang 07/06/2019
    $('.nav.navbar-nav a[href="' + url + '"]').each(function () {
        $(this).closest('li').addClass('active');
        $(this).closest('li').closest('ul').show();
        $(this).closest('li').closest('ul').closest('li').addClass('active open');

        $(this).closest('li').closest('ul').closest('li').closest('ul').closest('li').addClass('active open');
    });
}

function getHtmlFromTemplate($t, data, extData) {
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
}

function updateTableTrNotShowIdx($selector, displayIdx) {
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
}

function updateTableTrIdx($selector, displayIdx) {
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

            // Lan update 2019-10-09
            tr.find('textarea[name*="[' + odx + ']"]').each(function () {
                var name = $(this).attr('name');

                var nname = name.replace('[' + odx + ']', '[' + idx + ']');
                $(this).attr('name', nname);
            });
        }

        tr.attr('idx', idx);
        idx++;

    });
}

// using: http://dummy.com/?technology=jquery&blog=jquerybyexample
// var tech = getUrlParameter('technology');
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

var formatNumberIDN = function (num) {
    var str = num.toString().replace("$", ""), parts = false, output = [], i = 1, formatted = null;
    if (str.indexOf(",") > 0) {
        parts = str.split(",");
        str = parts[0];
    }
    str = str.split("").reverse();
    for (var j = 0, len = str.length; j < len; j++) {
        if (str[j] !== ",") {
            output.push(str[j]);
            if (i % 3 === 0 && j < (len - 1)) {
                output.push(",");
            }
            i++;
        }
    }
    formatted = output.reverse().join("");
    //return ("" + formatted + ((parts) ? "," + parts[1].substr(0, 5) : "")); // 1,154.12345
    return formatted;
};

var formatNumericIDN = function (num) {
    var str = num.toString().replace("$", ""), parts = false, output = [], i = 1, formatted = null;
    if (str.indexOf(",") > 0) {
        parts = str.split(",");
        str = parts[0];
    }
    str = str.split("").reverse();
    for (var j = 0, len = str.length; j < len; j++) {
        if (str[j] !== ",") {
            output.push(str[j]);
            if (i % 3 === 0 && j < (len - 1)) {
                output.push(",");
            }
            i++;
        }
    }
    formatted = output.reverse().join("");
    return ("" + formatted + ((parts) ? "," + parts[1].substr(0, 2) : "")); // 1,154.12
    //return formatted;
};

// using value = formatNumber(parseFloat(strValue));
function formatNumber(num) {
    var number = num.toFixed(2) + '';
    var x = number.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2; //10,000.05
}

// using value = formatNumberInteger(parseInt(strValue));
function formatNumberInteger(num) {
    var number = num.toFixed(2) + '';
    var x = number.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1;// 10,000
}


// Replaces all instances of the given substring.
// using var str = strChuoi.idnReplaceAll(',', '');
String.prototype.idnReplaceAll = function(
    strTarget, // The substring you want to replace
    strSubString // The string you want to replace in.
) {
    var strText = this;
    var intIndexOfMatch = strText.indexOf(strTarget);

    // Keep looping while an instance of the target string
    // still exists in the string.
    while (intIndexOfMatch !== -1) {
        // Relace out the current instance.
        strText = strText.replace(strTarget, strSubString);

        // Get the index of any next matching substring.
        intIndexOfMatch = strText.indexOf(strTarget);
    }

    // Return the updated string with ALL the target strings
    // replaced out with the new substring.
    return (strText);
};


// using var str = strChuoi.replaceAllMyString("ZZZ", "'");
String.prototype.replaceAllMyString = function (token, newToken, ignoreCase) {
    var _token;
    var str = this + "";
    var i = -1;

    if (typeof token === "string") {

        if (ignoreCase) {

            _token = token.toLowerCase();

            while ((
                i = str.toLowerCase().indexOf(
                    _token, i >= 0 ? i + newToken.length : 0
                )) !== -1
            ) {
                str = str.substring(0, i) +
                    newToken +
                    str.substring(i + token.length);
            }

        } else {
            return this.split(token).join(newToken);
        }

    }
    return str;
};

function isNumber(n) {
    return /^-?[\d.]+(?:e-?\d+)?$/.test(n);
}

function ChangeCheckBox(thiz) {
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
}

function checkFileJsonImport(thiz, e) {
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
}

function checkFileExcelImport(thiz, e) {
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
        alert("File Excel Import không hợp lệ!");
        $(thiz).val('');
        return false;
    }
    return true;
}

function ExportExcelTemplate(url, token) {
    if (url !== null && url !== undefined && token !== null && token !== undefined)
    {
        $.ajax({
            url: url,
            data: { __RequestVerificationToken: token },
            type: 'post',
            dataType: 'JSON',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            traditional: true,
            success: function (data) {
                try {
                    var getData = data;
                    if (getData.Success === false || getData.Success === 'false') {
                        _showErrorMsg123("Lỗi!", getData.Detail);
                    } else {
                        alert(getData.Title);
                        if (getData.CheckSuccess === "1") {
                            if (getData.strUrl !== null && getData.strUrl.length > 0) {
                                window.location = getData.strUrl;
                            }
                        }
                    }
                } catch (e) {
                    console.log(e.message);
                }
            }
        });
    }
    
}

function ExportExcel(url, token) {
    if (url !== null && url !== undefined && token !== null && token !== undefined) {
        $.ajax({
            url: url,
            data: { __RequestVerificationToken: token },
            type: 'post',
            dataType: 'JSON',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            traditional: true,
            success: function (data) {
                try {
                    var getData = data;
                    if (getData.Success === false || getData.Success === 'false') {
                        _showErrorMsg123("Lỗi!", getData.Detail);
                    } else {
                        alert(getData.Title);
                        if (getData.CheckSuccess === "1") {
                            if (getData.strUrl !== null && getData.strUrl.length > 0) {
                                window.location = getData.strUrl;
                            }

                        }

                    }
                } catch (e) {
                    console.log(e.message);
                }
            }
        });
    }
    
}

function TotalMonths(enddate, startdate) {
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
    return totalMonths;
    //return totalMonths <= 0 ? 0 : totalMonths;
}

function DateTimeSubtractDays(first, second) {
    var timeFirst = new Date(first).getTime();
    var timeSecond = new Date(second).getTime();
    var diff = timeFirst - timeSecond;
    var days = Math.floor(diff / (1000 * 60 * 60 * 24));
    return days;
}

function DateTimeAddDays(first, second) {
    var date = new Date(first);
    date.setDate(date.getDate() + second);
    return date;
}

// first > second => return true 
function CompareTwoDates(first, second) {
    var timeFirst = new Date(first).getTime();
    var timeSecond = new Date(second).getTime();
    if (timeFirst > timeSecond) {
        return true;
    } else {
        return false;
    }
}

// first >= second => return true 
function CompareToDate(first, second) {
    if ((new Date(first).getTime() >= new Date(second).getTime())) {
        return true;
    } else {
        return false;
    }
}

function CheckDate(date) {
    if (date === undefined || date === null || date.toString().trim().length === 0)
    {
        return false;
    }
    var _date = new Date(date);
    var check = (_date instanceof Date);
    return check;
}

function CheckAll(thiz) {
    var c_all = false;
    if ($(thiz).is(":checked")) {
        c_all = true;
    }
    $(".table-tbody input.cl-check").prop("checked", c_all);
}

function CheckAlltbodyID(thiz) {
    var c_all = false;
    if ($(thiz).is(":checked")) {
        c_all = true;
    }
    $("#table-tbodyID input.sl_ace").prop("checked", c_all);
}

function CheckAllNotCheckDisabled(thiz) {
    var c_all = false;
    if ($(thiz).is(":checked")) {
        c_all = true;
    }
    $(".table-tbody input.chked").prop("checked", c_all);
}

function CheckBox(thiz) {
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
                if (trLength === rows)
                {
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
}

function ValidateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function validateHhMm(strValue) {
    var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(strValue);
    if (isValid) {
        return true;
    } else {
        return false;
    }
}

// đảo ngược chuỗi
function reverse(s) {
    var o = '';
    for (var i = s.length - 1; i >= 0; i--)
        o += s[i];
    return o;
}

// <input type="text" name="folderName"  onkeypress="return blockSpecialChar(event)"/>
function blockSpecialChar(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k === 8 || k === 32 || (k >= 48 && k <= 57));
}

// using : onkeyup="locdau(this);" 
function locdau(obj) {
    
    var str;

    if (eval(obj))

        str = eval(obj).value;

    else str = obj;

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

    eval(obj).value = str.trim();

}

function catchuoi(chuoi, soluongkytu) {
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
}

function SetDefaultValueZero(thiz) {
    setTimeout(function () {
        var valueCur = $(thiz).val();
        if (IsNullOrEmpty(valueCur)) {
            $(thiz).val('0');
        }
    });
}

function readURL(input) {    
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
}

function readfileURL(input) {
    
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
}

// 2.5 -> 3
// 2.45 -> 2
function LamTronSo(number) {
    var _number = parseFloat(number.toString());
    var numbercur = Math.round(_number);
    //var x = Math.round(_number * 1000) / 1000;
    return numbercur;
}
//number: số
//scale: làm tròn bao nhiêu số sau dấu thập phân
function LamTronSo_Scale(number, scale) {
    var _number = parseFloat(number.toString());
    var _scale = parseFloat(scale.toString());
    var numbercur = roundNumberV1(_number, _scale);
    return numbercur;
}
//number: số
//scale: làm tròn bao nhiêu số sau dấu thập phân
function roundNumberV1(number, scale) {
    if (!("" + number).includes("e")) {
        return +(Math.round(number + "e+" + scale) + "e-" + scale);
    } else {
        var arr = ("" + number).split("e");
        var sig = "";
        if (+arr[1] + scale > 0) {
            sig = "+";
        }
        var i = +arr[0] + "e" + sig + (+arr[1] + scale);
        var j = Math.round(i);
        var k = +(j + "e-" + scale);
        return k;
    }
}

// Số di động
// true: hợp lệ
function ValidateMobile(phoneno) {
    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    if (!IsNullOrEmpty(phoneno)) {
        phoneno = phoneno.toString().trim();
        var phone = phoneno.replace('(+84)', '0');
        phone = phoneno.replace('+84', '0');
        phone = phoneno.replace('0084', '0');
        phone = phoneno.replace(/ /g, '');
        var validateMobile = vnf_regex.test(phone);
        return validateMobile;
        //return vnf_regex.test(phoneno);
    } else {
        return false;
    }
}

//<input id="txtChar" onKeyPress="return isNumberKey(event)" type="text" name="txtChar">
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

var MyBase64 = {


    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",


    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = reverse(input);
        input = MyBase64._utf8_encode(input);
        
        
        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output + this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) + this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

        }

        return output;
    },

    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    }

}