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
        }]
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
                style: {
                    textTransform: 'uppercase'
                }
            },
            labels: {
                style: {
                    fontSize: '12px'
                }
            }
        },
        plotOptions: {
            candlestick: {
                lineColor: '#404048'
            }
        },


        // General
        background2: '#F0F0EA'

    };

    // Apply the theme
    Highcharts.setOptions(Highcharts.theme);


    debugger;
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
                text: title
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