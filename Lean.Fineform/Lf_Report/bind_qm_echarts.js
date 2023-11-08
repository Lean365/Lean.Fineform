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


function BindmyChart_Qm_Pass(name, value1, value2, value3) {
    var PassChart = echarts.init(document.getElementById('PassLine'));
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
                magicType: { type: ['line', 'bar', 'stack'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: 'LOT合格率统计',
            subtext: 'DTA:月度合格率',// + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 4) + '月',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />合格率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        grid: {
            left: '10%',
            right: '10%',
            bottom: '10%',
            top: '10%',
            containLabel: true
        },
        legend: {
            show: true,
            x: 'left'
        },
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: 20
            }
        },
        yAxis: [{
            type: "value",
            name: "台数",
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
            name: "合格率%",
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
                formatter: "{value} %", //右侧Y轴文字显示
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        }
        ],
        series: [
            {
                name: '验收',
                type: 'bar',
                barGap: 0,
                stack: 'PASS',//表示在哪一列
                data: value2,
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                        },
                        color: function (params) {
                            // build a color map as your need.
                            //var colorList = [
                            //    '#84C1FF', '#66B3FF', '#46A3FF', '#2894FF', '#0080FF',
                            //    '#0072E3', '#0066CC', '#005AB5', '#004B97', '#003D79',
                            //];
                            var colorList = PrimarycolorList;
                            return colorList[params.dataIndex]
                        }
                    },//表示堆叠柱状图填充的颜色
                }
            },
            {
                name: '验退',
                type: 'bar',
                data: value1,
                stack: 'PASS',//表示在哪一列
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                        },
                        color: function (params) {
                            // build a color map as your need.
                            var colorList = SecondarycolorList;
                            //var colorList = [
                            //    '#003D79', '#004B97', '#005AB5', '#0066CC', '#0072E3',
                            //    '#0080FF', '#2894FF', '#46A3FF', '#66B3FF', '#84C1FF',
                            //];
                            return colorList[params.dataIndex]
                        }
                    },//表示堆叠柱状图填充的颜色
                }
            },
            {
                name: '合格率',
                type: 'line',
                yAxisIndex: 1,
                //barGap: 20,
                //stack: 'PASS',
                areaStyle: {},
                data: value3,
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
                            color: '#fff',
                        },
                        //百分比格式
                        formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
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
                                return "#FF0000";
                            }
                            return "#225a1f";
                        },
                        //color: 'rgb(219,50,51)',
                        borderColor: randomRGBA,
                        borderWidth: 12
                    }
                },
                markLine: {
                    label: {
                        fontSize: 9,
                        show: true,
                        formatter: '{b}：{c}%'
                    },
                    lineStyle: {
                        type: 'dashed',//dashed虚线，solid实绩
                        width: 2
                    },
                    data: [{
                        name: '年度目标',
                        yAxis: 98,
                        itemStyle: {
                            normal: {
                                color: '#ff6d9d'
                            }
                        }
                    }]
                }
            },

        ]
    };
    PassChart.setOption(option);
    PassChart.resize();
}
function BindmyChart_Qm_LotPass(name, value1, value2, value3) {
    var PassChart = echarts.init(document.getElementById('LotpassLine'));
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
                magicType: { type: ['line', 'bar', 'stack'] },   ///　　折线  直方图切换
                //restore: {}, // 重置
                //saveAsImage: {} // 导出图片
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: 'LOT合格率统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />合格率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        grid: {
            left: '10%',
            right: '10%',
            bottom: '10%',
            top: '10%',
            containLabel: true
        },
        legend: {
            show: true,
            x: 'left'
        },
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: 20
            }
        },
        yAxis: [{
            type: "value",
            name: "台数",
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
            name: "合格率%",
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
                formatter: "{value} %", //右侧Y轴文字显示
                textStyle: {
                    color: "#396A87",
                    fontSize: 14
                }
            }
        }
        ],
        series: [
            {
                name: '验收',
                type: 'bar',
                barGap: 0,
                data: value2,
                stack: 'PASS',//表示在哪一列
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                        },
                        color: function (params) {
                            // build a color map as your need.
                            //var colorList = [
                            //    '#84C1FF', '#66B3FF', '#46A3FF', '#2894FF', '#0080FF',
                            //    '#0072E3', '#0066CC', '#005AB5', '#004B97', '#003D79',
                            //];
                            var colorList = PrimarycolorList;
                            return colorList[params.dataIndex]
                        }
                    },//表示堆叠柱状图填充的颜色
                }
            },
            {
                name: '验退',
                type: 'bar',
                data: value1,
                stack: 'PASS',//表示在哪一列
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                        },
                        color: function (params) {
                            // build a color map as your need.
                            var colorList = SecondarycolorList;
                            //var colorList = [
                            //    '#003D79', '#004B97', '#005AB5', '#0066CC', '#0072E3',
                            //    '#0080FF', '#2894FF', '#46A3FF', '#66B3FF', '#84C1FF',
                            //];
                            return colorList[params.dataIndex]
                        }
                    },//表示堆叠柱状图填充的颜色
                }
            },
            {
                name: '合格率',
                type: 'line',
                yAxisIndex: 1,
                //stack: 'PASS',
                areaStyle: {},
                data: value3,
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
                            color: '#fff',
                        },
                        //百分比格式
                        formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
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
                                return "#FF0000";
                            }
                            return "#225a1f";
                        },
                        //color: 'rgb(219,50,51)',
                        borderColor: randomRGBA,
                        borderWidth: 12
                    }
                },
                markLine: {
                    label: {
                        fontSize: 9,
                        show: true,
                        formatter: '{b}：{c}%'
                    },
                    lineStyle: {
                        type: 'dashed',//dashed虚线，solid实绩
                        width: 2
                    },
                    data: [{
                        name: '年度目标',
                        yAxis: 98,
                        itemStyle: {
                            normal: {
                                color: '#ff6d9d'
                            }
                        }
                    }]
                }
            },
        ]
    };
    PassChart.setOption(option);
    PassChart.resize();
}