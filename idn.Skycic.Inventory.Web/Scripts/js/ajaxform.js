function processAjaxForm(form, onSumitSuccess) {
    var ajaxFormParam = {
        async: true,
        dataType: 'json',
        beforeSend: function () {
            
            var form = $(this);

            form.find("[type=submit]").addClass("disabled").attr("disabled", true);
        },
        beforeSubmit: function (arr, $form, options) {
            
            if (!$form.valid()) {
                return false;
            }
        },
        success: function (response, statusText, xhr, $form) {
            
            var form = $form;
            if (response === undefined) {
                return false;
            }

            var responseData = response;

            if (typeof (responseData) === 'string') {

                responseData = $.parseJSON($(responseData).text());
            }


            
            if (typeof onSumitSuccess !== 'undefined' && $.isFunction(onSumitSuccess)) {
                
                onSumitSuccess.call(this, responseData, statusText, xhr, $form)
            }
            else
                processAjaxFormResult(responseData, statusText, xhr, $form);
        },
        //// 2017-09-23: comment showError file loading.js
        //error: function (response, statusText, xhr) {
        //    
        //    var form = $(this);
        //    form.find("[type=submit]").removeClass('disabled').removeAttr('disabled');
        //    var responseData = response;

        //    if (typeof (responseData) === 'string') {

        //        responseData = $.parseJSON($(responseData).text());
        //    }
        //    showErrorDialog(responseData.responseText);
        //}
    };



    form.submit(function () {
        // reset all the validation messer elements. (Change for textContent form.)
        form.find('.field-validation-error').empty();

        if (!form.valid()) {

            if (form.find('.tabs').length) {
                var selector = 'input.input-validation-error,select.input-validation-error';
                form.find(selector).parents('div.tab-content')
						.each(function () {
						    var tab = $(this);
						    var li = $('a[href="#' + tab.attr('id') + '"]')
							.hide().show('pulsate', {}, 100)
							.show('highlight', {}, 200)
							.show('pulsate', {}, 300)
							.show('highlight', {}, 400);
						});
            }
        }
    });


    if (!form.hasClass('no-ajax')) {
        
        form.ajaxForm(ajaxFormParam);
        form.submit(function () {

        });
    }



}

function processAjaxFormResult(response) {
    if (response.Success === true) {
        if (response.Messages && response.Messages.length > 0) {
            alert(response.Messages[0]);
        }

        if (response.RedirectUrl) {
            window.location.href = response.RedirectUrl;
        }
        else if (response.RedirectToOpener) {
            window.location.href = window.location.href;
        }
    }

    else {
        if (response.IsServiceException === true) {
            //showErrorDialog(response.Detail);
            _showErrorMsg123("THÔNG BÁO", response.Detail);
        }
        else {
            var msgStr = '';
            for (var i = 0; i < response.Messages.length; i++) {
                msgStr += response.Messages[i] + '\r\n';
            }

            //var validator = form.validate();
            //var errors = [];
            for (var j = 0; i < response.FieldErrors.length; j++) {
                var obj = {};
                obj[response.FieldErrors[j].FieldName] = response.FieldErrors[j].ErrorMessage;
                validator.showErrors(obj);
            }
            if (msgStr.length > 0) {
                alert(msgStr);
            }
            //showErrorDialog(msgStr);

        }
    }
}

function showErrorDialog(opt) {
    
    var div = $('<div style="display:none; "></div>').appendTo('body');

    var options = {
        dialogClass: 'ui-jqdialog',
        title: 'Lỗi!',
        width: 650,
        height: 500,
        modal: true,
        buttons: [
					{
					    text: "Đóng",
					    click: function () {

					        $(this).dialog("close");

					    }
					},
        ],

        close: function () { div.dialog('destroy').remove(); },
        ready: function () { },
        error: function () { },

    };

    div.html(opt);
    $.extend(true, options, opt);

    var dialog = div.dialog({
        width: options.width,
        dialogClass: options.dialogClass,
        height: options.height,
        title: options.title,
        autoOpen: false,
        modal: options.modal,
        close: function () { options.close(); },

        buttons: options.buttons

    });

    div.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');

    dialog.dialog("open");

}

function showErrorDialogZZZ(err, opt) {

    var div = $('<div style="display:none; "></div>').appendTo('body');

    var options = {
        dialogClass: 'ui-jqdialog',
        title: 'Lỗi!',
        width: 650,
        height: 500,
        modal: true,
        buttons: [
					{
					    text: "Đóng",
					    click: function () {

					        $(this).dialog("close");

					    }
					},
        ],

        close: function () { div.dialog('destroy').remove(); },
        ready: function () { },
        error: function () { },

    };


    var strErr = '';
    if (err !== undefined && err.toString().trim().length > 0) {
        strErr = err.replaceAllMyString("ZZZ", "'");
        strErr = strErr.replaceAllMyString("TZBAZT", "<");
        strErr = strErr.replaceAllMyString("TZBACZT", ">");
    }
    div.html(strErr);
    $.extend(true, options, opt);

    var dialog = div.dialog({
        width: options.width,
        dialogClass: options.dialogClass,
        height: options.height,
        title: options.title,
        autoOpen: false,
        modal: options.modal,
        close: function () { options.close(); },

        buttons: options.buttons

    });

    div.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');

    dialog.dialog("open");

}

/////////////////////////////////////

function showAjaxFormDialog(url, title, opt) {
    
    var div = $('<div style="display:none; "></div>').appendTo('body');
    var options = {


        dialogClass: 'ui-jqdialog',
        width: 650,
        height: 500,
        modal: true,
        buttons: [
					{
					    text: "Đóng",
					    click: function () {
					        $(this).dialog("close");
					    }
					},
					{
					    text: "Lưu",
					    "class": 'submit',
					    click: function () {
					        div.find('form').eq(0).submit();
					    }
					}
        ],

        close: function () { },
        ready: function () { },
        success: function () { },
    };

    $.extend(true, options, opt);

    var dialog = div.dialog({
        width: options.width,
        dialogClass: options.dialogClass,
        height: options.height,
        title: title,
        autoOpen: false,
        modal: options.modal,
        close: function () { options.close(); div.remove(); },

        buttons: options.buttons

    });

    dialog.dialog("open");

    $.ajax(
	 {
	     type: "GET",
	     url: url,
	     dataType: "html",

	     success: function (data, status) {
	         if (data.length > 0) {
	             div.html(data);
	             var form = div.find('form');
	             if (form.length > 0) {
	                 processAjaxForm(form.eq(0), options.success);
	             }

	             options.ready();
	         }
	     },
	     error: function (e) {

	     }
	 });

    return dialog;

}

function showAjaxDialog(url, title, opt) {
    
    var options = {
        dialogClass: 'ui-jqdialog',
        width: 650,
        height: 500,
        modal: true,
        buttons: [
					{
					    text: "Đóng",
					    click: function () {

					        $(this).dialog("close");


					    }
					},
        ],

        close: function () { },
        ready: function () { },
        error: function () { },

    };


    var div = $('<div style="display:none; "></div>').appendTo('body');

    $.extend(true, options, opt);

    var dialog = div.dialog({
        width: options.width,
        dialogClass: options.dialogClass,
        height: options.height,
        title: title,
        autoOpen: false,
        modal: options.modal,
        close: function () { options.close(); },

        buttons: options.buttons

    });


    div.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');

    dialog.dialog("open");


    $.ajax(
	 {
	     type: "GET",
	     url: url,
	     dataType: "html",

	     success: function (data, status) {
	         if (data.length > 0) {
	             div.html(data);
	             options.ready();
	         }
	     },
	     error: function (e) {
	         options.error(e);
	     }
	 });

    return dialog;

}