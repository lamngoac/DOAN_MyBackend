//calls the keep alive handler every 600,000 miliseconds, which is 10 minutes
var keepAlive = {
    refresh: function () {
        $.ajax({
            type: 'post',
            data: { firstName: 'idocNet', lastName: 'idocNet' },
            url: '/Handlers/KeepAlive.ashx',
            dataType: 'json',
            //global: false,
            beforeSend: function () {
                if ($("#show-loading").length > 0) {
                    $("#show-loading").remove();
                }
            }
        }).done(function (objResult) {
            //alert(objResult.toString());
            //debugger;
            //doneFunction(objResult);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            //failFunction(jqXHR, textStatus, errorThrown);
        }).always(function () {
            //alwaysFunction();
        });
        //$.get('/keep-alive.ashx');
        setTimeout(keepAlive.refresh, 600000); // 10 minutes
    }
}; $(document).ready(keepAlive.refresh());