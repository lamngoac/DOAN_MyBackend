function GetCertificateInfo() {
    var tem = new Object();
    tem.FullName = ReturnValueText('#FullName');
    var modelCur = ReJSONValue(tem);
    var urlCur = "";
    urlCur = "http://localhost:12888/api/Signature/GetCertificateInfo";

    $.ajax({
        type: "post",
        data: modelCur,
        url: urlCur,
        dataType: 'json',
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        traditional: true,
        success: function (result) {
            if (result.Success) {
                var cert = result.Data;
                if (cert !== null && cert !== undefined) {
                    var serialNumber = ReturnValue(cert.SerialNumber);
                    var fullNameStep2 = ReturnValue(cert.Subject.CN);
                    var cAOrg = ReturnValue(cert.Issuer.CN);
                    var cAStartEff = ReturnValue(cert.NotBefore);
                    var cAEndEff = ReturnValue(cert.NotAfter);

                    $('#CANumber').val(serialNumber);
                    $('#FullNameStep2').val(fullNameStep2);
                    $('#CAOrg').val(cAOrg);
                    $('#CAStartEff').val(cAStartEff);
                    $('#CAEndEff').val(cAEndEff);
                    $('#ContactEmailStep2').val(ReturnValueText('#ContactEmail'));
                    $('#ContactPhoneStep2').val(ReturnValueText('#ContactPhone'));
                }
            } else {
                var exception = result.Exception;
                if (exception !== null && exception !== undefined) {
                    if (!IsNullOrEmpty(exception.ErrorDetail)) {
                        _showErrorMsg123("Lỗi!", exception.ErrorDetail);
                    } else {
                        if (!IsNullOrEmpty(exception.ErrorMessage)) {
                            alert(exception.ErrorMessage);
                        } else {
                            alert("Có lỗi xảy ra xin vui lòng thử lại!");
                        }
                    }
                } else {
                    alert("Có lỗi xảy ra xin vui lòng thử lại!");
                }
                //
            }
        }, error: function (xhr, textStatus, errorThrown) {
            var mess = xhr.responseJSON;
            alert("Bạn chưa cài chương trình ký số điện tử!");
        }
    }, function (err) {
        alert("Có lỗi xảy ra xin vui lòng thử lại!");
    });
}