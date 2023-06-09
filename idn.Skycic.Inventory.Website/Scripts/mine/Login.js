﻿var _location = window.location;
var _arrInvoice = window.location.href.split("/");

function User() {
    this.UserCode = '';
    this.UserPassword = '';

    this.setInfo = function (username, password) {
        this.UserCode = username;
        this.UserPassword = password;
    };

    this.checkFormLogin = function () {
        if (!commonUtils.checkElementIsNullOrEmpty('#UserCodeLogin', 'has-error-fix', 'Tên truy cập không được trống!')) {
            return false;
        }
        if (!commonUtils.checkElementIsNullOrEmpty('#PasswordLogin', 'has-error-fix', 'Mật khẩu không được trống!')) {
            return false;
        }
        return true;
    };

    this.submitForm = function (idForm, attrAction, attrData) {
        $(idForm).attr(attrAction, attrData).submit();
    };
    // Phải return this thì mới tạo mới được đối tượng
    return this;
}

var MyBase64 = {


    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",


    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = commonUtils.reverse(input);
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