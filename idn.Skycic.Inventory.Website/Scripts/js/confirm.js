function idnConfirm(msg, funcYes, opts) {

    opts = opts || {};

    var settings = $.extend({}, {
        title: 'System',
        yesBtnLabel:'Yes',
        noBtnLabel:'No',
        no:undefined,
    }, opts);
    

    var $dl = $('#globalConfirmDlg');
    if ($dl == undefined || $dl.length == 0) {
        var html = '<div class="modal fade" id="globalConfirmDlg" tabindex="-1" role="dialog">'
            
           + '<div class="modal-dialog" >'
     + '<div class="modal-content">'
       + '<div class="modal-header">'
         + '<h5 class="modal-title title">System</h5>'
         + '<button type="button" class="close" data-dismiss="modal" aria-label="Close">'
           + '<span aria-hidden="true">&times;</span>'
         + '</button>'
       + '</div>'
       + '<div class="modal-body">'
         + '<p class="msg"></p>'
       + '</div>'
       + '<div class="modal-footer">'
         + '<button type="button" class="btn btn-primary" data-dismiss="modal">Yes</button>'
         + '<button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>'
       + '</div>'
       + '</div>'
     + '</div>'
 + '</div>';
        $dl = $(html).appendTo('body');
    }
    $dl.find('.title').text(settings.title);
    $dl.find('.msg').text(msg);
    $dl.modal('toggle');

    $dl.find('.btn-primary').text(settings.yesBtnLabel).unbind('click.confirm').bind('click.confirm', function () {

        if (funcYes != undefined) {
            funcYes();
        }
    });

    $dl.find('.btn-secondary').text(settings.noBtnLabel).unbind('click.confirm').bind('click.confirm', function () {
        if (settings.no != undefined) {
            settings.no();
        }
    });

}