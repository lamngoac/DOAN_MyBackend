$(document).ready(function () {
	
	var qrCodeContent = $("#qrcodeContent").val();

	if (qrCodeContent != '') {
		$("#qrcodeTable").qrcode({
			text: qrCodeContent,
			typeNumber: 9,
			moduleCount: '53x53',
			errorCorrectLevel: 'L',
			width: 103,
			height: 103
		});
	}
 
});