/* !
 * Echarts图表封装模板
 * 版 本 20200416.008(https://github.com/davischeng)
 * Copyright 2020 LeanCloud.Inc
 * 创建人：Davis.Cheng
 * 商业授权&遵循License: GNU GPL 3.0.
 * 描  述：图表类封装
 * https://github.com/davischeng/oneCube/blob/master/licenses.txt
 * Date: 2020-04-16T16:01Z
 */
document.write("<script language=javascript src='/Lf_Report//define_echart.js'></script >");
//(注：有时你引用的文件还可能需要引用其他的js,我们需要将需要的那个js文件也以同样的方法引用进来)

function BindmyChart_SD_Regin(name, value, data) {
    var myChart = echarts.init(document.getElementById('ReginBar'));
    //debugger;
    //统计总数量
    var total_datas = 0;
    for (var i = 0; i < data.length; i++) {
        total_datas += parseInt(data[i].value)
    }
    // 指定图表的配置项和数据
    var option = {
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px">名称</td>';
                        series.forEach(function (item) {
                            tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                        });
                        var table = '<table border="1" style="user-select: text;color:#FF9D6F;margin-left:20px;border-collapse:collapse;font-size:14px;text-align:center"><tbody><tr>' + tdHeads + '</tr>';
                        var tdBodys = '';
                        for (var i = 0, l = axisData.length; i < l; i++) {
                            for (var j = 0; j < series.length; j++) {
                                if (typeof (series[j].data[i]) == 'object') {
                                    tdBodys += '<td>' + series[j].data[i].value + '</td>';
                                } else {
                                    tdBodys += '<td>' + series[j].data[i] + '</td>';
                                }
                            }
                            table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                            tdBodys = '';
                        }
                        table += '</tbody></table>';
                        return table;
                    }
                },  //数据视图
                magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: '出货仕向地统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(Total:' + total_datas + 'PCS)',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        grid: [{
            top: 50,
            width: '50%',
            bottom: 15,
            left: 10,
            containLabel: true
        }],
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: -20
            }
        },
        //y轴的数据
        yAxis: [{
            type: "value",
            name: "销量",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            splitLine: {
                show: true,
                lineStyle: {
                    width: 1,
                    color: "#d0d0d0"
                }
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true
            },
            axisLabel: {
                show: true,
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        },
        ],
        series: [{
            name: '销量',
            type: 'bar',
            radius: [20, 110],
            center: ['25%', '50%'],
            data: value,

            itemStyle: {
                normal: {
                    label: {
                        show: true,//是否展示
                    },
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = PrimarycolorList;
                        return colorList[params.dataIndex]
                    }
                },//表示堆叠柱状图填充的颜色
            }
        },
        {
            name: '仕向地',
            type: 'pie',
            radius: [30, 110],
            center: ['75%', '50%'],
            data: data,
            roseType: 'radius',
            label: {
                normal: {
                    formatter: ['{c|{c}台({d}%)}', '{b|{b}}'].join('\n'),
                    show: true,
                    position: 'outside',
                    rich: {
                        c: {
                            color: '#003366',
                        },
                        b: {
                            color: '#4cabce',
                        },
                    },
                }
            },
            labelLine: {
                length: 10,
                length2: 20,
                show: true,
                color: '#e5323e'
            },
            itemStyle: {
                borderRadius: 5,
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        },

        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    myChart.resize();
}
function BindmyChart_SD_Destination(name, value, data) {
    var DestmyChart = echarts.init(document.getElementById('DestBar'));
    //debugger;
    var total_datas = 0;
    for (var i = 0; i < data.length; i++) {
        total_datas += parseInt(data[i].value)
    }
    // 指定图表的配置项和数据
    var option = {
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px">名称</td>';
                        series.forEach(function (item) {
                            tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                        });
                        var table = '<table border="1" style="user-select: text;color:#FF9D6F;margin-left:20px;border-collapse:collapse;font-size:14px;text-align:center"><tbody><tr>' + tdHeads + '</tr>';
                        var tdBodys = '';
                        for (var i = 0, l = axisData.length; i < l; i++) {
                            for (var j = 0; j < series.length; j++) {
                                if (typeof (series[j].data[i]) == 'object') {
                                    tdBodys += '<td>' + series[j].data[i].value + '</td>';
                                } else {
                                    tdBodys += '<td>' + series[j].data[i] + '</td>';
                                }
                            }
                            table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                            tdBodys = '';
                        }
                        table += '</tbody></table>';
                        return table;
                    }
                },  //数据视图
                magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: '出货目的地统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(Total:' + total_datas + 'PCS)',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        grid: [{
            top: 50,
            width: '50%',
            bottom: 15,
            left: 10,
            containLabel: true
        }],
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: -20
            }
        },
        //y轴的数据
        yAxis: [{
            type: "value",
            name: "销量",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            splitLine: {
                show: true,
                lineStyle: {
                    width: 1,
                    color: "#d0d0d0"
                }
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true
            },
            axisLabel: {
                show: true,
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        },
        ],
        series: [{
            name: '销量',
            type: 'bar',
            radius: [20, 110],
            center: ['25%', '50%'],
            data: value,
            itemStyle: {
                normal: {
                    label: {
                        show: true,//是否展示
                    },
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = SecondarycolorList;
                        return colorList[params.dataIndex]
                    }
                },//表示堆叠柱状图填充的颜色
            }
        },
        {
            name: '目的地',
            type: 'pie',
            radius: [30, 110],
            center: ['75%', '50%'],
            //radius: '80%',
            //roseType: 'radius',
            //zlevel: 10,
            //startAngle: 100,
            data: data,
            roseType: 'radius',
            label: {
                normal: {
                    formatter: ['{c|{c}台({d}%)}', '{b|{b}}'].join('\n'),
                    show: true,
                    position: 'outside',
                    rich: {
                        c: {
                            color: '#003366',
                        },
                        b: {
                            color: '#4cabce',
                        },
                    },
                }
            },
            labelLine: {
                length: 10,
                length2: 20,
                show: true,
                color: '#e5323e'
            },
            itemStyle: {
                borderRadius: 5,
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }
        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    DestmyChart.setOption(option);
    DestmyChart.resize();
}
function BindmyChart_SD_BuCode(name, value, data) {
    var DestmyChart = echarts.init(document.getElementById('BuBar'));
    //debugger;
    var total_datas = 0;
    for (var i = 0; i < data.length; i++) {
        total_datas += parseInt(data[i].value)
    }
    // 指定图表的配置项和数据
    var option = {
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px">名称</td>';
                        series.forEach(function (item) {
                            tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                        });
                        var table = '<table border="1" style="user-select: text;color:#FF9D6F;margin-left:20px;border-collapse:collapse;font-size:14px;text-align:center"><tbody><tr>' + tdHeads + '</tr>';
                        var tdBodys = '';
                        for (var i = 0, l = axisData.length; i < l; i++) {
                            for (var j = 0; j < series.length; j++) {
                                if (typeof (series[j].data[i]) == 'object') {
                                    tdBodys += '<td>' + series[j].data[i].value + '</td>';
                                } else {
                                    tdBodys += '<td>' + series[j].data[i] + '</td>';
                                }
                            }
                            table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                            tdBodys = '';
                        }
                        table += '</tbody></table>';
                        return table;
                    }
                },  //数据视图
                magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: 'BU别统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(Total:' + total_datas + 'PCS)',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        grid: [{
            top: 50,
            width: '50%',
            bottom: 15,
            left: 10,
            containLabel: true
        }],
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: -20
            }
        },
        //y轴的数据
        yAxis: [{
            type: "value",
            name: "销量",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            splitLine: {
                show: true,
                lineStyle: {
                    width: 1,
                    color: "#d0d0d0"
                }
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true
            },
            axisLabel: {
                show: true,
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        },
        ],
        series: [{
            name: '销量',
            type: 'bar',
            radius: [20, 110],
            center: ['25%', '50%'],
            data: value,
            itemStyle: {
                normal: {
                    label: {
                        show: true,//是否展示
                    },
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = PrimarycolorList;
                        return colorList[params.dataIndex]
                    }
                },//表示堆叠柱状图填充的颜色
            }
        },
        {
            name: 'BU别',
            type: 'pie',
            radius: [30, 110],
            center: ['75%', '50%'],
            //radius: '80%',
            //roseType: 'radius',
            //zlevel: 10,
            //startAngle: 100,
            data: data,
            roseType: 'radius',

            label: {
                normal: {
                    formatter: ['{c|{c}台({d}%)}', '{b|{b}}'].join('\n'),
                    show: true,
                    position: 'outside',
                    rich: {
                        c: {
                            color: '#003366',
                        },
                        b: {
                            color: '#4cabce',
                        },
                    },
                }
            },
            labelLine: {
                length: 10,
                length2: 20,
                show: true,
                color: '#e5323e'
            },
            itemStyle: {
                borderRadius: 5,
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }
        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    DestmyChart.setOption(option);
    DestmyChart.resize();
}
function BindmyChart_SD_FyJPStats(name, value1, value2) {
    var FyStatsChart = echarts.init(document.getElementById('FyJPBar'));
    //debugger;
    //var total_datas = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_datas += parseInt(data[i].value1)
    //}
    // 指定图表的配置项和数据
    var option = {
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px">名称</td>';
                        series.forEach(function (item) {
                            tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                        });
                        var table = '<table border="1" style="user-select: text;color:#FF9D6F;margin-left:20px;border-collapse:collapse;font-size:14px;text-align:center"><tbody><tr>' + tdHeads + '</tr>';
                        var tdBodys = '';
                        for (var i = 0, l = axisData.length; i < l; i++) {
                            for (var j = 0; j < series.length; j++) {
                                if (typeof (series[j].data[i]) == 'object') {
                                    tdBodys += '<td>' + series[j].data[i].value + '</td>';
                                } else {
                                    tdBodys += '<td>' + series[j].data[i] + '</td>';
                                }
                            }
                            table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                            tdBodys = '';
                        }
                        table += '</tbody></table>';
                        return table;
                    }
                },  //数据视图
                magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: 'Fiscal Year Bu-Sales',
            subtext: '按日本财年统计',// 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(Total:' + total_datas + 'PCS)',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        //图例内容以及位置
        legend: {
            //加载图例内容，这里json.a是对应的数组元素
            show: true,
            x: 'left'
        },
        //x轴的数据
        xAxis: {
            data: name
        },
        //y轴的数据
        //y轴的数据
        yAxis: [{
            type: "value",
            name: "销量",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            splitLine: {
                show: true,
                lineStyle: {
                    width: 1,
                    color: "#d0d0d0"
                }
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true
            },
            axisLabel: {
                show: true,
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        },
        {
            type: "value",
            name: "金额",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            position: "right",
            splitLine: {
                show: true
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true,
                lineStyle: {
                    color: "#396A87",
                    width: 2
                }
            },
            axisLabel: {
                show: true,
                formatter: "{value}", //右侧Y轴文字显示
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        }
        ],
        series: [
            {
                name: '销量',
                type: 'bar',
                stack: 'PLAN',//表示在哪一列
                barGap: 0,
                data: value1,
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                        },
                        color: function (params) {
                            // build a color map as your need.
                            var colorList = PrimarycolorList;
                            return colorList[params.dataIndex]
                        }
                    },//表示堆叠柱状图填充的颜色
                }
            },
            {
                name: '金额',
                type: 'line',
                //stack: 'PLAN',//表示在哪一列
                yAxisIndex: 1,
                areaStyle: {},
                data: value2,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 35,
                label: {
                    normal: {
                        show: true,
                        position: 'inside',
                        textStyle: {
                            color: '#blue',
                        },
                        //百分比格式
                        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                    }
                },
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },

                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(219, 50, 51, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(219, 50, 51, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        ///  通过params.value拿到对应的data里面的数据
                        color: function (params) {
                            if (params.value < 112) {
                                return "#ed1941";
                            } else if (params.value >= 112) {
                                return "#1d953f";
                            }
                            return "#225a1f";
                        },
                        //color: 'rgb(219,50,51)',
                        borderColor: randomRGBA,
                        //borderColor: 'rgba(72,209,204,0.2)',
                        borderWidth: 12
                    }
                },
            },

        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    FyStatsChart.setOption(option);
    FyStatsChart.resize();
}
function BindmyChart_SD_FyBuJPStats(name, value, data) {
    var FyStatsChart = echarts.init(document.getElementById('FyBuJPBar'));
    //debugger;
    //统计总数量
    var total_datas = 0;
    for (var i = 0; i < data.length; i++) {
        total_datas += parseInt(data[i].value)
    }
    // 指定图表的配置项和数据
    var option = {
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px">名称</td>';
                        series.forEach(function (item) {
                            tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                        });
                        var table = '<table border="1" style="user-select: text;color:#FF9D6F;margin-left:20px;border-collapse:collapse;font-size:14px;text-align:center"><tbody><tr>' + tdHeads + '</tr>';
                        var tdBodys = '';
                        for (var i = 0, l = axisData.length; i < l; i++) {
                            for (var j = 0; j < series.length; j++) {
                                if (typeof (series[j].data[i]) == 'object') {
                                    tdBodys += '<td>' + series[j].data[i].value + '</td>';
                                } else {
                                    tdBodys += '<td>' + series[j].data[i] + '</td>';
                                }
                            }
                            table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                            tdBodys = '';
                        }
                        table += '</tbody></table>';
                        return table;
                    }
                },  //数据视图
                magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: 'Fiscal Year Bu-Sales',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年 Sales:' + total_datas + 'CNY',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        grid: [{
            top: 50,
            width: '50%',
            bottom: 15,
            left: 10,
            containLabel: true
        }],
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: -20
            }
        },

        //y轴的数据
        yAxis: [{
            type: "value",
            name: "金额",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            splitLine: {
                show: true,
                lineStyle: {
                    width: 1,
                    color: "#d0d0d0"
                }
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true
            },
            axisLabel: {
                show: true,
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        },
        ],
        series: [{
            name: 'Sales',
            type: 'bar',
            radius: [20, 110],
            center: ['25%', '50%'],
            data: value,

            itemStyle: {
                normal: {
                    label: {
                        show: true,//是否展示
                    },
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = PrimarycolorList;
                        return colorList[params.dataIndex]
                    }
                },//表示堆叠柱状图填充的颜色
            }
        },
        {
            name: 'Fiscal Year Bu-Sales',
            type: 'pie',
            radius: [30, 110],
            center: ['75%', '50%'],
            data: data,
            roseType: 'radius',
            label: {
                normal: {
                    formatter: ['{c|{c}CNY({d}%)}', '{b|{b}}'].join('\n'),
                    show: true,
                    position: 'outside',
                    rich: {
                        c: {
                            color: '#003366',
                        },
                        b: {
                            color: '#4cabce',
                        },
                    },
                }
            },
            labelLine: {
                length: 10,
                length2: 20,
                show: true,
                color: '#e5323e'
            },
            itemStyle: {
                borderRadius: 5,
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        },

        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    FyStatsChart.setOption(option);
    FyStatsChart.resize();
}
function BindmyChart_SD_FyCNStats(name, value1, value2) {
    var FyStatsChart = echarts.init(document.getElementById('FyCNBar'));
    //debugger;
    //var total_datas = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_datas += parseInt(data[i].value1)
    //}
    // 指定图表的配置项和数据
    var option = {
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px">名称</td>';
                        series.forEach(function (item) {
                            tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                        });
                        var table = '<table border="1" style="user-select: text;color:#FF9D6F;margin-left:20px;border-collapse:collapse;font-size:14px;text-align:center"><tbody><tr>' + tdHeads + '</tr>';
                        var tdBodys = '';
                        for (var i = 0, l = axisData.length; i < l; i++) {
                            for (var j = 0; j < series.length; j++) {
                                if (typeof (series[j].data[i]) == 'object') {
                                    tdBodys += '<td>' + series[j].data[i].value + '</td>';
                                } else {
                                    tdBodys += '<td>' + series[j].data[i] + '</td>';
                                }
                            }
                            table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                            tdBodys = '';
                        }
                        table += '</tbody></table>';
                        return table;
                    }
                },  //数据视图
                magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: 'Natural Year Bu-Sales',
            subtext: '按自然年度统计',// 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(Total:' + total_datas + 'PCS)',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        //图例内容以及位置
        legend: {
            //加载图例内容，这里json.a是对应的数组元素
            show: true,
            x: 'left'
        },
        //x轴的数据
        xAxis: {
            data: name
        },
        //y轴的数据
        //y轴的数据
        yAxis: [{
            type: "value",
            name: "销量",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            splitLine: {
                show: true,
                lineStyle: {
                    width: 1,
                    color: "#d0d0d0"
                }
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true
            },
            axisLabel: {
                show: true,
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        },
        {
            type: "value",
            name: "金额",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            position: "right",
            splitLine: {
                show: true
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true,
                lineStyle: {
                    color: "#396A87",
                    width: 2
                }
            },
            axisLabel: {
                show: true,
                formatter: "{value}", //右侧Y轴文字显示
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        }
        ],
        series: [
            {
                name: '销量',
                type: 'bar',
                stack: 'PLAN',//表示在哪一列
                barGap: 0,
                data: value1,
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                        },
                        color: function (params) {
                            // build a color map as your need.
                            var colorList = PrimarycolorList;
                            return colorList[params.dataIndex]
                        }
                    },//表示堆叠柱状图填充的颜色
                }
            },
            {
                name: '金额',
                type: 'line',
                //stack: 'PLAN',//表示在哪一列
                yAxisIndex: 1,
                areaStyle: {},
                data: value2,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 35,
                label: {
                    normal: {
                        show: true,
                        position: 'inside',
                        textStyle: {
                            color: '#blue',
                        },
                        //百分比格式
                        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                    }
                },
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },

                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(219, 50, 51, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(219, 50, 51, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        ///  通过params.value拿到对应的data里面的数据
                        color: function (params) {
                            if (params.value < 98) {
                                return "#ed1941";
                            } else if (params.value = 110) {
                                return "#1d953f";
                            }
                            return "#225a1f";
                        },
                        //color: 'rgb(219,50,51)',
                        borderColor: randomRGBA,
                        //borderColor: 'rgba(72,209,204,0.2)',
                        borderWidth: 12
                    }
                },
            },

        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    FyStatsChart.setOption(option);
    FyStatsChart.resize();
}
function BindmyChart_SD_FyBuCNStats(name, value, data) {
    var FyStatsChart = echarts.init(document.getElementById('FyBuCNBar'));
    //debugger;
    //统计总数量
    var total_datas = 0;
    for (var i = 0; i < data.length; i++) {
        total_datas += parseInt(data[i].value)
    }
    // 指定图表的配置项和数据
    var option = {
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px">名称</td>';
                        series.forEach(function (item) {
                            tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                        });
                        var table = '<table border="1" style="user-select: text;color:#FF9D6F;margin-left:20px;border-collapse:collapse;font-size:14px;text-align:center"><tbody><tr>' + tdHeads + '</tr>';
                        var tdBodys = '';
                        for (var i = 0, l = axisData.length; i < l; i++) {
                            for (var j = 0; j < series.length; j++) {
                                if (typeof (series[j].data[i]) == 'object') {
                                    tdBodys += '<td>' + series[j].data[i].value + '</td>';
                                } else {
                                    tdBodys += '<td>' + series[j].data[i] + '</td>';
                                }
                            }
                            table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                            tdBodys = '';
                        }
                        table += '</tbody></table>';
                        return table;
                    }
                },  //数据视图
                magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: 'Natural Year Bu-Sales',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年 Sales:' + total_datas + 'CNY',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        grid: [{
            top: 50,
            width: '50%',
            bottom: 15,
            left: 10,
            containLabel: true
        }],
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: -20
            }
        },
        //y轴的数据
        yAxis: [{
            type: "value",
            name: "金额",
            nameTextStyle: {
                color: "#396A87",
                fontSize: 14
            },
            splitLine: {
                show: true,
                lineStyle: {
                    width: 1,
                    color: "#d0d0d0"
                }
            },
            axisTick: {
                show: true
            },
            axisLine: {
                show: true
            },
            axisLabel: {
                show: true,
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        },
        ],
        series: [{
            name: 'Sales',
            type: 'bar',
            radius: [20, 110],
            center: ['25%', '50%'],
            data: value,

            itemStyle: {
                normal: {
                    label: {
                        show: true,//是否展示
                    },
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = PrimarycolorList;
                        return colorList[params.dataIndex]
                    }
                },//表示堆叠柱状图填充的颜色
            }
        },
        {
            name: 'Natural Year Bu-Sales',
            type: 'pie',
            radius: [30, 110],
            center: ['75%', '50%'],
            data: data,
            roseType: 'radius',
            label: {
                normal: {
                    formatter: ['{c|{c}元({d}%)}', '{b|{b}}'].join('\n'),
                    show: true,
                    position: 'outside',
                    rich: {
                        c: {
                            color: '#003366',
                        },
                        b: {
                            color: '#4cabce',
                        },
                    },
                }
            },
            labelLine: {
                length: 10,
                length2: 20,
                show: true,
                color: '#e5323e'
            },
            itemStyle: {
                borderRadius: 5,
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        },

        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    FyStatsChart.setOption(option);
    FyStatsChart.resize();
}