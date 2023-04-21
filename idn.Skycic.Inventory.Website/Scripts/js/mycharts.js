function pieChart(chartID, type = 'pie', title, seriesname, data) {
    Highcharts.setOptions({
        colors: ['#ffbc75', '#58acdb', '#24CBE5', '#50B432', '#ED561B', '#DDDF00', '#64E572', '#FF9655', '#FFF263', '#6AF9C4']
        //colors: ['#95ceff', '#6AF9C4', '#64E572', '#58acdb', '#24CBE5', '#50B432', '#ED561B', '#DDDF00', '#FF9655', '#FFF263']
    });
    Highcharts.chart(chartID, {
        chart: {
            type: type,
            options3d: {
                enabled: true,
                alpha: 45,
                beta: 0
            }
        },
        title: {
            text: title
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                depth: 35,
                dataLabels: {
                    enabled: true,
                    format: '{point.name}',
                    color: '#000000',
                    connectorColor: '#000000',

                    formatter: function () {

                        return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';

                    }
                }
            }
        },
        series: [{
            type: type,
            name: seriesname,
            data: data
        }],
        exporting: {
            enabled: false
        }
    });
}

function pieChart1(chartID, type = 'pie', title, seriesname, data) {
    // Make monochrome colors and set them as default for all pies
    //Highcharts.getOptions().plotOptions.pie.colors = (function () {
    //    var colors = [],
    //        base = Highcharts.getOptions().colors[0],
    //        i;

    //    for (i = 0; i < 35; i += 1) {
    //        // Start out with a darkened base color (negative brighten), and end
    //        // up with a much brighter color
    //        colors.push(Highcharts.Color(base).brighten((i - 3) / 7).get());
    //    }
    //    return colors;
    //}());

    Highcharts.setOptions({
        //colors: ['#58acdb', '#24CBE5', '#50B432', '#ED561B', '#DDDF00', '#64E572', '#FF9655', '#FFF263', '#6AF9C4']
        colors: ['#50B432', '#6AF9C4', '#64E572', '#58acdb', '#24CBE5', '#ED561B', '#DDDF00', '#FF9655', '#FFF263']
    });

    // Build the chart
    Highcharts.chart(chartID, {
        chart: {
            //plotBackgroundColor: null,
            //plotBorderWidth: null,
            //plotShadow: false,
            type: type,
            options3d: {
                enabled: true,
                alpha: 45,
                beta: 0
            }
        },
        title: {
            text: title
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                depth: 35,
                dataLabels: {
                    enabled: true,
                    format: '{point.name}',
                    color: '#000000',
                    connectorColor: '#000000',
                    //style: {
                    //    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    //},
                    formatter: function () {

                        return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';

                    }
                }
            }
        },
        //plotOptions: {
        //    pie: {
        //        allowPointSelect: true,
        //        cursor: 'pointer',
        //        dataLabels: {
        //            enabled: true,
        //            format: '<b>{point.name}</b>: {point.percentage:.2f} %',
        //            style: {
        //                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
        //            }
        //        }
        //    }
        //},
        series: [{
            type: type,
            name: seriesname,
            data: data,
            colorByPoint: true,
        }]
    });

    
}

function columnChart3D(chartID, type = 'column', title, subtitle, categories, fontSize, yAxistitle, seriesname, data) {
    Highcharts.chart(chartID,{
        chart: {
            type: type,
            options3d: {
                enabled: true,
                alpha: 15,
                beta: 15,
                viewDistance: 25,
                depth: 40
            }
        },
        title: {
            text: title
        },
        xAxis: {
            categories: categories,
            labels: {
                skew3d: true,
                style: {
                    fontSize: fontSize
                }
            }
        },
        yAxis: {
            allowDecimals: false,
            min: 0,
            title: {
                text: yAxistitle,
                skew3d: true
            }
        },
        //tooltip: {
        //    headerFormat: '<b>{point.key}</b><br>',
        //    pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} / {point.stackTotal}'
        //},
        subtitle: {
            text: subtitle
        },
        plotOptions: {
            column: {
                stacking: 'normal',
                depth: 150
            }
        },
        series: [{
            type: type,
            name: seriesname,
            data: data,
        }]
    });
}

function stackedColumnChart1(chartID, type = 'column', title, categories, fontSize, yAxistitle, data) {

    Highcharts.theme = {
        colors: ['#7cb5ec', '#f7a35c', '#90ee7e', '#7798BF', '#aaeeee', '#ff0066',
            '#eeaaee', '#55BF3B', '#DF5353', '#7798BF', '#aaeeee'],
        chart: {
            backgroundColor: null,
            style: {
                fontFamily: 'Dosis, sans-serif'
            }
        },
        title: {
            style: {
                fontSize: '16px',
                fontWeight: 'bold',
                textTransform: 'uppercase'
            }
        },
        tooltip: {
            borderWidth: 0,
            backgroundColor: 'rgba(219,219,216,0.8)',
            shadow: false
        },
        legend: {
            itemStyle: {
                fontWeight: 'bold',
                fontSize: '13px'
            }
        },
        xAxis: {
            gridLineWidth: 1,
            labels: {
                style: {
                    fontSize: '12px'
                }
            }
        },
        yAxis: {
            minorTickInterval: 'auto',
            title: {
                text: 'Doanh thu (VND)',
                align: "high",
                textAlign: "left",
                rotation: 0,
                offset: 0,
                margin: 0,
                y: -20,
                x: -60,
            },
            labels: {
        formatter: function() {
            var ret,
                numericSymbols = [' Nghìn', ' Triệu', ' Tỷ', ' Nghìn Tỷ', ' Triệu Tỷ'],
                i = numericSymbols.length;
            if(this.value >=1000) {
                while (i-- && ret === undefined) {
                    multi = Math.pow(1000, i + 1);
                    if (this.value >= multi && numericSymbols[i] !== null) {
                        ret = (this.value / multi) + numericSymbols[i];
                    }
                }
            }
            return (ret ? ret : this.value);
        },
                
        style: {
            fontSize: '12px'
        },
    },
        },
        plotOptions: {
            candlestick: {
                lineColor: '#404048'
            }
        },


        // General
        background2: '#F0F0EA',
        lang:{
            thousandsSep: ','
        }
    };

    // Apply the theme
    Highcharts.setOptions(Highcharts.theme);


    //debugger;
    Highcharts.chart(chartID, {
        chart: {
            type: type
        },
        title: {
            text: title
        },
        xAxis: {
            categories: categories
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Doanh thu (VND)'
            },
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                }
            }
        },
        legend: {
            //align: 'right',
            //x: -30,
            //verticalAlign: 'top',
            //y: 25,
            //floating: true,
            //backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            //borderColor: '#CCC',
            //borderWidth: 1,
            //shadow: false
            enabled: false,
        },
        tooltip: {
            headerFormat: '<b>{point.x}</b><br/>',
            pointFormat: '{series.name}: {point.y} VND'
            //<br/>Total: {point.stackTotal} VND
        },
        plotOptions: {
            column: {
                stacking: '',
                dataLabels: {
                    enabled: false,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                },
                pointPadding: 0.1,
                borderWidth: 0,
            },
            series: {
                pointWidth: 35
            }
        },
        series: data    ,
        exporting: {
            enabled: false
        }
    });    
}

function stackedColumnChart(chartID, type = 'column', title, categories, fontSize, yAxistitle, data)
{
    Highcharts.chart(chartID, {
        chart: {
            type: type
        },
        title: {
            text: title
        },
        xAxis: {
            //categories: ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
            categories: categories,
            //labels: {
            //    style: {
            //        fontSize: fontSize
            //    }
            //}
        },
        yAxis: {
            min: 0,
            title: {
                text: yAxistitle
            },
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                }
            }
        },
        legend: {
            align: 'right',
            x: -30,
            verticalAlign: 'top',
            y: 25,
            floating: true,
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            borderColor: '#CCC',
            borderWidth: 1,
            shadow: false
        },
        tooltip: {
            headerFormat: '<b>{point.x}</b><br/>',
            pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
        },
        plotOptions: {
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                }
            }
        },
        series: data
        //series: [{
        //    name: 'John',
        //    data: [5, 3, 4, 7, 2]
        //}, {
        //    name: 'Jane',
        //    data: [2, 2, 3, 2, 1]
        //}, {
        //    name: 'Joe',
        //    data: [3, 4, 4, 2, 5]
        //}]
    });
}































//function pieChart(chartID, type = 'pie', title, seriesname, data) {
//    Highcharts.chart(chartID, {
//        chart: {
//            type: 'pie',
//            options3d: {
//                enabled: true,
//                alpha: 45,
//                beta: 0
//            }
//        },
//        title: {
//            text: title
//        },
//        tooltip: {
//            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
//        },
//        plotOptions: {
//            pie: {
//                allowPointSelect: true,
//                cursor: 'pointer',
//                depth: 35,
//                dataLabels: {
//                    enabled: true,
//                    format: '{point.name}'
//                }
//            }
//        },
//        series: [{
//            type: 'pie',
//            name: seriesname,
//            data: [
//                {
//                    name: 'Thành Công EC',
//                    y: 1,
//                },
//                {
//                    name: 'Quê Việt',
//                    y: 18,
//                },
//                {
//                    name: 'Cổ loa',
//                    y: 13,
//                    sliced: true,
//                    selected: true,
//                },
//                {
//                    name: 'HTX Thành Công',
//                    y: 22,
//                },
//                {
//                    name: 'Phúc Thịnh 1',
//                    y: 8,
//                },
//                {
//                    name: 'Phúc Thịnh 2',
//                    y: 8,
//                },
//                {
//                    name: 'Phúc Thịnh 3',
//                    y: 15,
//                },
//                {
//                    name: 'Phúc Thịnh 4',
//                    y: 7,
//                },
//                {
//                    name: 'Phúc Thịnh 5',
//                    y: 8,
//                },
//            ]
//        }]
//    });
    //}

    /*2019-10-18*/
function stackedColumnChart3(chartID, type = 'column', title,unit, titleUnit, categories, series) {
    Highcharts.theme = {
        //xét các màu cho các cột theo thứ tự 0,1,
        colors: ['#4F81BD', '#C0504D'],
        chart: {
            backgroundColor: null,
            style: {
                fontFamily: 'Dosis, sans-serif'
            }
        },
        plotOptions: {
            column: {
                colorByPoint: false,//áp dụng cho tất cả các cột hay không
            }
        },        
        lang:{
            thousandsSep: ','
        },
        title: {
            style: {
                fontSize: '16px',
                fontWeight: 'bold',
                textTransform: 'uppercase'
            }
        }
    };
    Highcharts.setOptions(Highcharts.theme);

    Highcharts.chart(chartID, {
        chart: {
            type: 'column',
        },
        title: {
            text: title/*'Doanh thu các tháng trong năm'*/
        },
        xAxis: {
            categories: categories,
            crosshair: true,
            gridLineWidth: 1,
            title:{
                text: titleUnit,
                align:'high',
                textAlign: "right",
                offset: 7,
                y: 0,
                x: 30,
                layout: 'vertical',
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: unit,
                align: "high",
                textAlign: "left",
                rotation: 0,
                offset: 0,
                margin: 0,
                y: -20,
                x: -60,
                layout: 'vertical',
            },
            labels: {
                formatter: function() {
                    //var ret,
                    //    numericSymbols = [' Nghìn', ' Triệu', ' Tỷ', ' Nghìn Tỷ', ' Triệu Tỷ'],
                    //    i = numericSymbols.length;
                    //if(this.value >=1000) {
                    //    while (i-- && ret === undefined) {
                    //        multi = Math.pow(1000, i + 1);
                    //        if (this.value >= multi && numericSymbols[i] !== null) {
                    //            ret = (this.value / multi) + numericSymbols[i];
                    //        }
                    //    }
                    //}
                    //return (ret ? ret : this.value);
                    return (this.value);
                }
            },
        },
        legend: {
            align: 'right',
            x: 0,
            verticalAlign: 'top',
            y: 0,
            floating: false,
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            borderColor: '#CCC',
            borderWidth: 0,
            shadow: false,
            layout: 'vertical',
            symbolRadius: 0,
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y} </b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                groupPadding: 0.1
            },
            series: {
                pointWidth: 20
            }
        },
        series: series,
        exporting: {
            enabled: false
        }
    });
}

function stackedLineChart(chartID, title, unit, titleUnit, categories, series) {
    //Highcharts.theme = {
    //    //xét các màu cho các cột theo thứ tự 0,1,
    //    colors: ['#4F81BD', '#C0504D'],
    //    chart: {
    //        backgroundColor: null,
    //        style: {
    //            fontFamily: 'Dosis, sans-serif'
    //        }
    //    },
    //    plotOptions: {
    //        column: {
    //            colorByPoint: false,//áp dụng cho tất cả các cột hay không
    //        }
    //    },
    //    lang: {
    //        thousandsSep: ','
    //    },
    //    title: {
    //        style: {
    //            fontSize: '16px',
    //            fontWeight: 'bold',
    //            textTransform: 'uppercase'
    //        }
    //    }
    //};
    //Highcharts.setOptions(Highcharts.theme);

    Highcharts.chart(chartID, {
        chart: {
            type: 'line',
        },
        title: {
            text: title/*'Doanh thu các tháng trong năm'*/
        },
        xAxis: {
            categories: categories
        },
        yAxis: {            
            title: {
                text: unit,
                align: "high",
                textAlign: "left",
                rotation: 0,
                offset: 0,
                margin: 0,
                y: -20,
                x: -60,
                layout: 'vertical',
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: false
            }
        },
        series: series,
        exporting: {
            enabled: false
        }
    });
}