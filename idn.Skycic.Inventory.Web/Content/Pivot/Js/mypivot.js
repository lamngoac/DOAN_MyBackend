  $(document).ready(function () {
        var dttk = $('#dttk option:selected').text();
        var view = $('#tamnhin option:selected').text();
        $('#tenbc').text(dttk + "-" + view);
        DrawPivot();

        $('#tamnhin').change(function () {
            debugger;
            var view = $('#tamnhin').val();
            if (view == "thang" || view == "quy") {
                $('#dennam').prop("disabled", true);
                $('#dennam').val("");
            }
            else {
                $('#dennam').prop("disabled", false);
            }
            //DrawPivot();
        })

        $('#dttk').change(function () {
            $('#loaidttk').html("");
            $('#btnLayDL').click();
        });
    });

    function DrawPivot() {
        debugger;
        $('#chart2').html("");
        $('#tbl_chugiai').css("display","none");

        var s1 = JSON.parse($('#KHCX').val());
        var s2 = JSON.parse($('#KHTN').val());

        if (s1.length == 0 || s2.length == 0) return;
        var view = $('#tamnhin').val();
        var ticks = [];
        if (view == "thang") {
            for (var i = 1; i <= 12; i++) {
                if (i < 10) {
                    ticks.push("Tháng 0" + i);
                }
                else {
                    ticks.push("Tháng " + i);
                }
            }
        }
        else if (view == "quy") {
            ticks = ["Quý I", "Quý II", "Quý III", "Quý IV"]
        }
        else {
            var nambd = $("#tunam").val();
            var namkt = $('#dennam').val();
            if ((nambd != undefined && namkt != undefined) && (nambd != "" && namkt != "")) {
                if ($.isNumeric(nambd) == true && $.isNumeric(namkt) == true) {
                    var bd = parseFloat(nambd);
                    var kt = parseFloat(namkt);
                    if (bd > kt) {
                        alert("Từ năm phải <= đến năm.");
                    }
                    else {
                        for (var i = bd; i <= kt; i++) {
                            ticks.push("Năm " + i);
                        }
                    }
                }
                else {
                    alert("Từ năm, đến năm phải là số.");
                    return;
                }
            }
        }

        if (ticks.length > 0) {
            plot2 = $.jqplot('chart2', [s1, s2], {
                seriesDefaults: {
                    renderer: $.jqplot.BarRenderer,
                    pointLabels: { show: true }
                },
                axes: {
                    xaxis: {
                        renderer: $.jqplot.CategoryAxisRenderer,
                        ticks: ticks
                    }
                }
            });

            $('#chart2').bind('jqplotDataHighlight',
                function (ev, seriesIndex, pointIndex, data) {
                    $('#info2').html('series: ' + seriesIndex + ', point: ' + pointIndex + ', data: ' + data);
                }
            );

            $('#chart2').bind('jqplotDataUnhighlight',
                function (ev) {
                    $('#info2').html('Nothing');
                }
            );

            $('#tbl_chugiai').css("display", "");
        }
    }



    function CheckDL() {
        debugger;
        var tunam = $('#tunam').val();
        if (tunam == "" || $.isNumeric(tunam) == false) {
            alert("Từ năm chưa được nhập hoặc nhập sai định dạng.");
            return;
        }

        var view = $('#tamnhin').val();
        if (view == "nam") {
            var dennam = $('#dennam').val();
            if (dennam == "" || $.isNumeric(dennam) == false) {
                alert("Đến năm chưa được nhập hoặc nhập sai định dạng.");
                return;
            }
        }
        $('#btnLayDL').click();
    }