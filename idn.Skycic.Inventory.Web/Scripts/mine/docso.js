function DocSoJS(){
	this.arrSo = ["không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"];
	
    this.Doc3So = function (so, isDau) {
        var str1So = "0", str2So = "0", str3So = "0";
        if (so === "") return "";
        var ketQua = "";
        if (so === "000") return "";
        str1So = parseInt(so.substring(0, 1));
        str2So = parseInt(so.substring(1, 2));
        str3So = parseInt(so.substring(2, 3));
        if (isDau !== true || str1So !== 0) ketQua = this.arrSo[str1So] + " trăm";
        if (str2So !== 0) {
            if (str2So === 1) ketQua += " mười";
            else ketQua += " " + this.arrSo[str2So] + " mươi";
        }
        else if (isDau !== true || str1So !== 0) if (str3So !== 0) ketQua += " linh";
        if (str3So === 1) {
            if (str2So === 0 || str2So === 1) ketQua += " một";
            else ketQua += " mốt";
        }
        else if (str3So === 4) {
            if (str2So === 0 || str2So === 1) ketQua += " bốn";
            else ketQua += " tư";
        }
        else if (str3So === 5) {
            if (str2So === 0) ketQua += " năm";
            else ketQua += " lăm";
        }
        else {
            if (str3So !== 0) {
                if (str2So === 0 || str2So === 1) ketQua += " " + this.arrSo[str3So];
                else ketQua += " " + this.arrSo[str3So];
            }
        }
        return ketQua;
    };

    this.Doc9So = function (so, isDau) {
        if (so === "") return "";
        var ketQua = "";
        var ketQuaTempt = "";
        var strSo = "";           
        ketQuaTempt = this.Doc3So(so.substring(0, 3), isDau);
        
        if (ketQuaTempt !== "") {
            ketQua = ketQuaTempt + " triệu";
            isDau = false;
        }
        ketQuaTempt = this.Doc3So(so.substring(3, 6), isDau);
        
        if (ketQuaTempt !== "") {
            ketQua += " " + ketQuaTempt + " nghìn";
            isDau = false;
        }
        ketQua += " " + this.Doc3So(so.substring(6, 9), isDau);
        
        return ketQua.trim();
    };


    // Truyền kiểu string
    this.DocSo = function (so) {
        if (so === "0") return "không";
        so = parseInt(so).toString();
        var ketQua = "";
        var ketQuaTempt = "";       
        while (so.length < 9) {
            so = "0" + so;
        }
        var slTy = Math.round(so.length / 9);

        // Truong hop > 9 so
        var sochusoht = so.length - slTy * 9;
        var solanso0 = 9 - sochusoht;
        
        if (sochusoht !== 0) {
            for (var st = 0; st < solanso0; st++) {
                so = "0" + so;
            }
            slTy += 1;
        }
        //
        for (var i = 0; i < slTy; i++)
        {        
            if (i === 0) ketQuaTempt = this.Doc9So(so.substring(i * 9, 9), true);
            else {
                var lastIndex = (i + 1) * 9;
                ketQuaTempt = this.Doc9So(so.substring(i * 9, lastIndex), false);
            }
            if (ketQuaTempt.length > 0) {
                ketQua += " " + ketQuaTempt;
                for (var j = slTy - 1; j > i; j--) ketQua += " tỷ";
            }
        }
        ketQua = ketQua.trim();
        var fstketQua = ketQua.substring(0, 1);
        return fstketQua.toUpperCase() + ketQua.substring(1) + " đồng ./.";
    };	
}