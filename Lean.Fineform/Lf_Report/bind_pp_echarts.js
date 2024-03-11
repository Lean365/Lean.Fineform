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


function BindmyChart_Pp_Actual(name, value1, value2, value3, data) {
    //获取隐藏控件中的值
    //图表展示
    var ActualmyChart = echarts.init(document.getElementById('ActualBar'));
    //debugger;
    //统计总数量
    var total_PlanQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_PlanQty += parseFloat(data[i].value1)
    }
    var total_ActualQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_ActualQty += parseInt(data[i].value2)
    }
    var total_AchievingRate = Math.round(total_ActualQty / total_PlanQty * 10000) / 100.00 + "%";// 小数点后两位百分比

    //debugger;
    // 指定图表的配置项和数据
    var option = {
        //工具箱配置
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                //  缩放
                dataZoom: {
                    yAxisIndex: 'none'
                },
                // 数据视图
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px;color:#FF9D6F">名称</td>';
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
                restore: { show: true },// 还原，复位原始图表
                saveAsImage: {
                    show: true,
                    title: 'IMG',
                    name: 'DTA:'  + '_班组生产统计',    
                },// 保存为图片
            }
        },
        //图表标题
        title: {
            text: '班组生产统计',//正标题
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(计划:' + total_PlanQty.toFixed(2) + 'PCS;' + ',实绩:' + total_ActualQty + 'PCS;达成率:' + total_AchievingRate + ')',//副标题
            x: 'center'//标题水平方向位置
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />达成率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
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
            name: "达成率%",
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
        //图表数据
        series: [
            {
                name: '计划',
                type: 'bar',
                //stack: 'PLAN',//表示在哪一列
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
                name: '实绩',
                type: 'bar',
                //stack: 'Actual',//表示在哪一列
                data: value2,
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
                name: '达成率',
                type: 'line',
                //stack: 'PLAN',//表示在哪一列
                yAxisIndex: 1,
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
                        name: 'FY2024目标',
                        yAxis: 112,
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
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    ActualmyChart.setOption(option);
    ActualmyChart.resize();
}

function BindmyChart_Pp_Last_Actual(name, value1, value2, value3, data) {
    //获取隐藏控件中的值
    //图表展示
    var ActualLastmyChart = echarts.init(document.getElementById('Actual_Last'));
    //debugger;
    //统计总数量
    var total_PlanQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_PlanQty += parseInt(data[i].value1)
    }
    var total_ActualQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_ActualQty += parseInt(data[i].value2)
    }
    var total_AchievingRate = Math.round(total_ActualQty / total_PlanQty * 10000) / 100.00 + "%";// 小数点后两位百分比

    debugger;
    // 指定图表的配置项和数据
    var option = {
        //工具箱配置
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                //  缩放
                dataZoom: {
                    yAxisIndex: 'none'
                },
                // 数据视图
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
                restore: { show: true },// 还原，复位原始图表
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_班组生产统计',    
                }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: '班组生产统计',//正标题
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(计划:' + total_PlanQty + 'PCS;' + ',实绩:' + total_ActualQty + 'PCS;达成率:' + total_AchievingRate + ')',//副标题
            x: 'center'//标题水平方向位置
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />达成率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
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
        yAxis: {},
        //图表数据
        series: [
            {
                name: '计划',
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
                name: '实绩',
                type: 'bar',
                stack: 'Actual',//表示在哪一列
                data: value2,
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
                name: '达成率',
                type: 'line',
                stack: 'PLAN',//表示在哪一列
                //yAxisIndex: 1,
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
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    ActualLastmyChart.setOption(option);
    ActualLastmyChart.resize();
}
function BindmyChart_Pp_Defect(name, value1, value2, value4, data) {
    var DefectmyChart = echarts.init(document.getElementById('DefectBar'));
    //debugger;
    //统计总数量
    var total_NobadQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_NobadQty += parseInt(data[i].value1)
    }
    var total_BadQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_BadQty += parseInt(data[i].value2)
    }
    var total_AchievingRate = Math.round(total_BadQty / (total_NobadQty + total_BadQty) * 10000) / 100.00 + "%";// 小数点后两位百分比
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
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_生产不良集计',  
                }
            }
        },
        title: {
            text: '生产不良集计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(无不良台数:' + total_NobadQty + 'PCS;' + ',不良台数:' + total_BadQty + 'PCS;不良率:' + total_AchievingRate + ')',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                lineStyle: {
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0,
                            color: 'rgba(0, 255, 233,0)'
                        }, {
                            offset: 0.5,
                            color: 'rgba(255, 255, 255,1)',
                        }, {
                            offset: 1,
                            color: 'rgba(0, 255, 233,0)'
                        }],
                        global: false
                    }
                },
            },
            formatter: '{b}<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        legend: {
            show: true,
            x: 'left'
        },
        xAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: -20
            }

        },
        yAxis: {},
        series: [
        //    {
        //    name: '无不良',
        //    type: 'bar',
        //    //barGap: 0,
        //    stack: 'LOTSUM',//表示在哪一列
        //    data: value1,
        //    itemStyle: {
        //        normal: {
        //            label: {
        //                show: true,//是否展示
        //                position: 'inside',
        //            },
        //            color: function (params) {
        //                // build a color map as your need.
        //                var colorList = PrimarycolorList;
        //                return colorList[params.dataIndex]
        //            }
        //        },//表示堆叠柱状图填充的颜色
        //    }
        //},
        {
            name: '不良台数',
            type: 'bar',
            stack: 'LOTSUM',//表示在哪一列
            data: value2,
            itemStyle: {
                normal: {
                    label: {
                        show: true,//是否展示
                        //position: 'Top',
                    },
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = SecondarycolorList;
                        return colorList[params.dataIndex]
                    }
                },//表示堆叠柱状图填充的颜色
            }
        },
            //{
            //    name: '不良率',
            //    type: 'line',
            //    areaStyle: {},
            //    stack: 'LOTSUM',//表示在哪一列
            //    data: value4,
            //    // smooth: true, //是否平滑
            //    showAllSymbol: true,
            //    // symbol: 'image://./static/images/guang-circle.png',
            //    symbol: 'circle',
            //    symbolSize: 25,
            //    lineStyle: {
            //        normal: {
            //            color: "#00ca95",
            //            shadowColor: 'rgba(0, 0, 0, .3)',
            //            shadowBlur: 0,
            //            shadowOffsetY: 5,
            //            shadowOffsetX: 5,
            //        },
            //    },
            //    label: {
            //        normal: {
            //            show: true,
            //            position: 'top',
            //            textStyle: {
            //                color: '#00ca95',
            //            },
            //            //百分比格式
            //            formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
            //        }
            //    },

            //    itemStyle: {
            //        color: "#00ca95",
            //        borderColor: "#fff",
            //        borderWidth: 3,
            //        shadowColor: 'rgba(0, 0, 0, .3)',
            //        shadowBlur: 0,
            //        shadowOffsetY: 2,
            //        shadowOffsetX: 2,
            //    },
            //    tooltip: {
            //        show: true
            //    },
            //    areaStyle: {
            //        normal: {
            //            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
            //                offset: 0,
            //                color: 'rgba(0,202,149,0.3)'
            //            },
            //            {
            //                offset: 1,
            //                color: 'rgba(0,202,149,0)'
            //            }
            //            ], false),
            //            shadowColor: 'rgba(0,202,149, 0.9)',
            //            shadowBlur: 20
            //        }
            //    },
            //}
        ]
    };
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    DefectmyChart.setOption(option);
    DefectmyChart.resize();
}
function  BindmyChart_Pp_Reason(data) {
    var myChart = echarts.init(document.getElementById('ReasonSunburst'));
    //debugger;
    //拼接JSON数据为TREE结构
    var BindTree = [];
    trans_tree(data);
    function trans_tree(jsonData) {
        //temp为临时对象，将json数据按照id值排序.
        var temp = {}, len = jsonData.length

        for (var i = 0; i < len; i++) {
            // 以id作为索引存储元素，可以无需遍历直接快速定位元素
            temp[jsonData[i]['id']] = jsonData[i]
        }
        for (var j = 0; j < len; j++) {
            var list = jsonData[j]
            // 临时变量里面的当前元素的父元素，即pid的值，与找对应id值
            var sonlist = temp[list['pid']]
            // 如果存在父元素，即如果有pid属性
            if (sonlist) {
                // 如果父元素没有children键
                if (!sonlist['children']) {
                    // 设上父元素的children键
                    sonlist['children'] = []
                }
                // 给父元素加上当前元素作为子元素
                sonlist['children'].push(list)
            }
            // 不存在父元素，意味着当前元素是一级元素
            else {
                BindTree.push(list);
            }
        }
        return BindTree;
    }
    //文字折行
    var getChars = function (e) {
        var newStr = " ";
        var start, end;
        var name_len = e.name.length;    　　　　　　　　　　　　   //每个内容名称的长度
        var max_name = 4;    　　　　　　　　　　　　　　　　　　//每行最多显示的字数
        var new_row = Math.ceil(name_len / max_name); 　　　　// 最多能显示几行，向上取整比如2.1就是3行
        if (name_len > max_name) { 　　　　　　　　　　　　　　  //如果长度大于每行最多显示的字数
            for (var i = 0; i < new_row; i++) { 　　　　　　　　　　　   //循环次数就是行数
                var old = '';    　　　　　　　　　　　　　　　　    //每次截取的字符
                start = i * max_name;    　　　　　　　　　　     //截取的起点
                end = start + max_name;    　　　　　　　　　  //截取的终点
                if (i == new_row - 1) {    　　　　　　　　　　　　   //最后一行就不换行了
                    old = e.name.substring(start);
                } else {
                    old = e.name.substring(start, end) + "\n";
                }
                newStr += old; //拼接字符串
            }
        } else {                                          //如果小于每行最多显示的字数就返回原来的字符串
            newStr = e.name;
        }
        return newStr;
    }
    //var getdata = genData(data);
    option = {
        toolbox: {
            show: true,
            orient: 'horizontal',
            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },//  缩放
                dataView: { readOnly: true },   //  数据视图
                //magicType: { type: ['line', 'bar'] },   ///　　折线  直方图切换
                restore: {}, // 重置
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_原因分析',  
                } // 导出图片
            }
        },
        title: {
            text: '原因分析',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月',
            x: 'left'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} "
        },
        legend: {
            type: 'scroll',
            orient: 'horizontal',
            right: 10,
            top: 20,
            bottom: 20,
            data: name,
            //selected: getdata.selected
        },
        series: {
            type: 'sunburst',
            highlightPolicy: 'ancestor',
            data: BindTree,
            radius: [0, '95%'],
            sort: null,
            levels: [
                {},//第一层
                {//第二层
                    r0: '15%',
                    r: '35%',
                    itemStyle: {
                        borderWidth: 2
                    },
                    label: {
                        //formatter: getChars,           //调用get
                        //align: 'right',
                        rotate: 'tangential'
                    }
                }, {//第三层
                    r0: '35%',
                    r: '70%',
                    label: {
                        //formatter: getChars,          //调用get
                        position: 'outside',
                    }
                },]
        }

    };
    //选中数据
    //function genData(data) {
    //    var selected = {};
    //    var list = eval(data);
    //    for (i = 0; i < list.length; i++) {
    //        var key_value = list[i].value;
    //        if (key_value >= 5) {
    //            selected[list[i].name] = true;
    //        } else {
    //            selected[list[i].name] = false;
    //        }
    //    };
    //    return {
    //        selected: selected
    //    };
    //}

    myChart.setOption(option);
    myChart.resize();
}
function  BindmyChart_Pp_Direct(name, value3, value4) {
    var AchievemyChart = echarts.init(document.getElementById('DirectLine'));
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
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_Lot直行率统计',  
                }
            }
        },
        title: {
            text: 'LOT直行率统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'line'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />{a0}: {c0}%<br />{a1}: {c1}%',　　　　//这是关键，在需要的地方加上就行了
        },
        grid: {
            top: '15%',
            left: '5%',
            right: '5%',
            bottom: '10%',
            // containLabel: true
        },
        legend: {
            show: true,
            x: 'left'
        },
        xAxis: [
            {
                //left:'5%',
                type: 'category',
                axisLine: {
                    show: true
                },
                splitArea: {
                    // show: true,
                    color: '#f00',
                    lineStyle: {
                        color: '#f00'
                    },
                },
                axisLabel: {
                    show: true,
                    color: '#003366',
                    interval: 0,
                    rotate: 20
                },
                splitLine: {
                    show: true
                },
                boundaryGap: false,
                data: name
            }
        ],
        yAxis: [
            {
                type: 'value',
                min: 0,
                // max: 140,
                splitNumber: 4,
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(255,255,255,0.1)'
                    }
                },
                axisLine: {
                    show: true,
                },
                axisLabel: {
                    show: true,
                    formatter: '{value} %',
                    margin: 20,
                    textStyle: {
                        color: '#003366',

                    },
                },
                axisTick: {
                    show: true,
                },
            }
        ],
        series: [
            {
                name: '直行率',
                type: 'line',
                //yAxisIndex: 1,
                areaStyle: {},
                data: value3,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 25,
                lineStyle: {
                    normal: {
                        color: "#6c50f3",
                        shadowColor: 'rgba(0, 0, 0, .3)',
                        shadowBlur: 0,
                        shadowOffsetY: 5,
                        shadowOffsetX: 5,
                    },
                },
                label: {
                    normal: {
                        show: true,
                        position: 'top',
                        textStyle: {
                            color: '#6c50f3',
                        },
                        //百分比格式
                        formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                    }

                },
                itemStyle: {
                    color: "#6c50f3",
                    borderColor: "#fff",
                    borderWidth: 3,
                    shadowColor: 'rgba(0, 0, 0, .3)',
                    shadowBlur: 0,
                    shadowOffsetY: 2,
                    shadowOffsetX: 2,
                },
                tooltip: {
                    show: true
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(108,80,243,0.3)'
                        },
                        {
                            offset: 1,
                            color: 'rgba(108,80,243,0)'
                        }
                        ], false),
                        shadowColor: 'rgba(108,80,243, 0.9)',
                        shadowBlur: 20
                    }
                },
            },
            {
                name: '不良率',
                type: 'line',
                areaStyle: {},
                //yAxisIndex:1,
                data: value4,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 25,
                lineStyle: {
                    normal: {
                        color: "#00ca95",
                        shadowColor: 'rgba(0, 0, 0, .3)',
                        shadowBlur: 0,
                        shadowOffsetY: 5,
                        shadowOffsetX: 5,
                    },
                },
                label: {
                    normal: {
                        show: true,
                        position: 'top',
                        textStyle: {
                            color: '#00ca95',
                        },
                        //百分比格式
                        formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                    }
                },

                itemStyle: {
                    color: "#00ca95",
                    borderColor: "#fff",
                    borderWidth: 3,
                    shadowColor: 'rgba(0, 0, 0, .3)',
                    shadowBlur: 0,
                    shadowOffsetY: 2,
                    shadowOffsetX: 2,
                },
                tooltip: {
                    show: true
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(0,202,149,0.3)'
                        },
                        {
                            offset: 1,
                            color: 'rgba(0,202,149,0)'
                        }
                        ], false),
                        shadowColor: 'rgba(0,202,149, 0.9)',
                        shadowBlur: 20
                    }
                },
            }
        ]
    };
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    AchievemyChart.setOption(option);
    AchievemyChart.resize();
}
function  BindmyChart_Pp_Achieve(name, value3, value4) {
    var AchievemyChart = echarts.init(document.getElementById('AchieveLine'));
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
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_月达成率统计',  
                }
            }
        },
        title: {
            text: '月达成率统计',
            subtext: 'DTA 年度达成率', //+ TransDates,
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'line'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />{a0}: {c0}%<br />{a1}: {c1}%',　　　　//这是关键，在需要的地方加上就行了
        },
        grid: {
            top: '10%',
            left: '10%',
            right: '10%',
            bottom: '10%',
            // containLabel: true
        },
        legend: {
            show: true,
            x: 'left'
        },
        xAxis: [
            {
                //left:'5%',
                type: 'category',
                axisLine: {
                    show: true
                },
                splitArea: {
                    // show: true,
                    color: '#f00',
                    lineStyle: {
                        color: '#f00'
                    },
                },
                axisLabel: {
                    show: true,
                    color: '#003366',
                    interval: 0,
                    rotate: 20
                },
                splitLine: {
                    show: true
                },
                boundaryGap: false,
                data: name
            }
        ],
        yAxis: [
            {
                type: 'value',
                min: 0,
                // max: 140,
                splitNumber: 4,
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(255,255,255,0.1)'
                    }
                },
                axisLine: {
                    show: true,
                },
                axisLabel: {
                    show: true,
                    formatter: '{value} %',
                    margin: 20,
                    textStyle: {
                        color: '#003366',

                    },
                },
                axisTick: {
                    show: true,
                },
            }
        ],
        series: [
            {
                name: '达成率',
                type: 'line',
                areaStyle: {},
                //yAxisIndex:1,
                data: value4,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 30,
                label: {
                    normal: {
                        show: true,
                        position: 'inside',
                        textStyle: {
                            color: '#FFFFF0',
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
                            color: 'rgba(0, 136, 212, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(0, 136, 212, 0)'
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
                        borderWidth: 12
                    }
                },
                markLine: {
                    label: {
                        show: true,
                        fontSize: 9,
                        formatter: '{b}:{c}%'
                    },
                    lineStyle: {
                        type: 'dashed',//dashed虚线，solid实绩
                        width: 2
                    },
                    data: [{
                        name: 'FY2024目标',
                        yAxis: 112,
                        itemStyle: {
                            normal: {
                                
                                color: '#ff6d9d'
                            }
                        }
                    }]
                }
            }
        ]
    };
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    AchievemyChart.setOption(option);
    AchievemyChart.resize();
}


function  BindmyChart_Pp_Progress(name, value3, value4, data) {
    //获取隐藏控件中的值
    //图表展示
    var ProgressmyChart = echarts.init(document.getElementById('ProgressBar'));
    //debugger;
    //重新组合JSON数据
    var datas = [];
    var list = eval(data);
    for (var i = 0; i < list.length; i++) {
        var item = {
            value: list[i].value1.toFixed(0),
            value1: list[i].value2,
        }
        datas.push(item);
    }
    //统计总数量
    //var total_ActualQty = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_ActualQty += parseInt(data[i].value2)
    //}
    //var total_AchievingRate = Math.round(total_ActualQty / total_PlanQty * 10000) / 100.00 + "%";// 小数点后两位百分比

    //debugger;
    // 指定图表的配置项和数据
    var option = {

        //工具箱配置
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                //  缩放
                dataZoom: {
                    yAxisIndex: 'none'
                },
                // 数据视图
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
                restore: { show: true },// 还原，复位原始图表
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_生产进度',  
                }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: 'LOT生产进度',//正标题
            //subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(计划:' + total_PlanQty + 'PCS;' + ',实绩:' + total_ActualQty + 'PCS;达成率:' + total_AchievingRate + ')',//副标题
            x: 'center'//标题水平方向位置
        },
        grid: {
            left: "5%",
            right: "5%",
            top: "10%",
            bottom: "10%",
            containLabel: true
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: function (params) {
                for (x in params) {
                    return params[x].name + "<br/>生产数量:" + params[x].data.value1 + "<br/>占比:" + params[x].data.value + "%";
                }

            }
            //formatter: '{b}<br />达成率:{c2}%<br />{a2}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了

            //formatter: function (v) {
            //    return "批次数量：" + value3 + "<br/>生产数量:" + v.data.value1 + "<br/>占比:" + v.data.value + "%";
            //},
        },
        //图例内容以及位置
        legend: {
            //加载图例内容，这里json.a是对应的数组元素
            show: true,
            x: 'left'
        },
        //x轴的数据
        xAxis: [{
            splitLine: {
                show: false
            }, //去除网格线
            type: "category",
            axisLabel: {
                rotate: 90,
                textStyle: {
                    fontSize: 12,
                    color: "#000"
                }
            },
            data: name

        },
        {
            show: true,
            data: value3,
            axisLabel: {

                textStyle: {
                    fontSize: 8,
                    color: "#000"
                }
            },
            axisLine: {
                show: false
            },
            splitLine: {
                show: false
            },
            axisTick: {
                show: false
            }
        }
        ],

        //y轴的数据
        yAxis: {

            show: false,
            splitLine: {
                show: false
            }, //去除网格线
            type: "value",
            min: 0,
            max: 100
        },

        //图表数据
        series: [{
            name: "LOT数量",
            type: "bar",
            tooltip: {
                show: false
            },
            data: value4,
            barGap: "-100%",
            // barWidth: "60px",
            itemStyle: {
                normal: {
                    color: "rgb(240,240,240)"
                },
                emphasis: {
                    color: "rgb(240,240,240)"
                }
            }
        },
        {
            name: "生产数量",
            type: "bar",
            itemStyle: {
                color: function (params) {
                    // build a color map as your need.
                    var colorList = TreecolorList;
                    return colorList[params.dataIndex]
                }
                //color: {
                //    type: "linear",
                //    x: 0,
                //    y: 0,
                //    x2: 0,
                //    y2: 1,
                //    colorStops: [{
                //        offset: 0,
                //        color: "rgba(41, 121, 255, 1)" // 0% 处的颜色
                //    },
                //    {
                //        offset: 1,
                //        color: "rgba(0, 192, 255, 1)" // 100% 处的颜色
                //    }
                //    ],
                //    globalCoord: false // 缺省为 false
                //}
            },
            label: {
                normal: {
                    show: true,
                    fontSize: 8,
                    position: "inside",
                    formatter: function (v) {
                        return v.data.value1 + '\n\n' + v.data.value + '%';
                    }
                }
            },
            data: datas,
            z: 9,
            // barWidth: "60px"
        }
        ],
    };
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    ProgressmyChart.setOption(option);
    ProgressmyChart.resize();
}
// 把数组整理成树形结构

// 获取所有层级中节点数最大的层级的节点数(tempNodeNum)，总层数(tempclassesNum)
function  BindmyChart_Pp_LineLoss(name, value1, value2, value3, value4, value5, data) {
    //获取隐藏控件中的值
    //图表展示
    var LossmyChart = echarts.init(document.getElementById('LossTimeBar'));
    //debugger;
    var total_spendtime = 0;
    for (var i = 0; i < data.length; i++) {
        total_spendtime += parseFloat(data[i].value3)
    }
    var total_LossTime = 0;
    for (var i = 0; i < data.length; i++) {
        total_LossTime += parseFloat(data[i].value2)
    }
    var total_WorkTime = 0;
    for (var i = 0; i < data.length; i++) {
        total_WorkTime += parseFloat(data[i].value1)
    }
    var total_LossRate = (parseFloat(total_LossTime) / parseFloat(total_spendtime)) * 100;


    //debugger;
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
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_损失工数统计',  
                }
            }
        },
        title: {
            text: '损失工数统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月(生产工数:' + total_WorkTime + 'MIN,损失工数:' + total_LossTime + 'MIN,工时损失率:' + total_LossRate.toFixed(2) + '%)',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />工时损失率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        grid: {
            left: '5%',
            right: '4%',
            bottom: '5%',
            top: '15%',
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
            name: "分钟",
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
            name: "工时损失率%",
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
            //{
            //    name: '投入工数',
            //    type: 'bar',
            //    barGap: 0,
            //    data: value3,
            //    stack: 'LOSS',//表示在哪一列
            //    itemStyle: {
            //        normal: {
            //            label: {
            //                show: true,//是否展示
            //            },
            //            color: function (params) {
            //                // build a color map as your need.
            //                //var colorList = [
            //                //    '#84C1FF', '#66B3FF', '#46A3FF', '#2894FF', '#0080FF',
            //                //    '#0072E3', '#0066CC', '#005AB5', '#004B97', '#003D79',
            //                //];
            //                var colorList = PrimarycolorList;
            //                return colorList[params.dataIndex]
            //            }
            //        },//表示堆叠柱状图填充的颜色
            //    }
            //},
            {
                name: '实际工数',
                type: 'bar',
                data: value1,
                stack: 'LOSS',//表示在哪一列
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
                name: '损失工数',
                type: 'bar',
                data: value2,
                stack: 'LOSS',//表示在哪一列
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                        },
                        color: function (params) {
                            // build a color map as your need.
                            var colorList = SparecolorList;
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
                name: '工时损失率',
                type: 'line',
                yAxisIndex: 1,
                //stack: 'LOSS',
                areaStyle: {},
                data: value5,
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
                            if (params.value < 112) {
                                return "#FF0000";
                            } else if (params.value >= 112) {
                                return "#1d953f";

                            }
                            return "#225a1f";
                        },
                        //color: 'rgb(219,50,51)',
                        borderColor: randomRGBA,
                        borderWidth: 12
                    }
                },
            },
        ]
    };
    LossmyChart.setOption(option);
    LossmyChart.resize();
}
function  BindmyChart_Pp_Loss(name, value1, value2, value3, value4, value5, data) {
    //获取隐藏控件中的值
    //图表展示
    var LossmyChart = echarts.init(document.getElementById('LossBar'));
    //debugger;
    //var total_spendtime = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_spendtime += parseFloat(data[i].value2)
    //}
    //var total_LossTime = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_LossTime += parseFloat(data[i].value2)
    //}
    //var total_WorkTime = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_WorkTime += parseFloat(data[i].value1)
    //}
    //var total_LossRate = (parseFloat(total_LossTime) / parseFloat(total_spendtime)) * 100;


    //debugger;
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
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_年度损失工数分析',  
                }
            }
        },
        title: {
            text: '损失工数统计',
            subtext: 'DTA年度损失工数分析',// + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月(生产工数:' + total_WorkTime + ',损失工数:' + total_LossTime + ',工时损失率:' + total_LossRate.toFixed(2) + '%)',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />工时损失率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        grid: {
            left: '5%',
            right: '4%',
            bottom: '5%',
            top: '15%',
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
        //Y轴显示
        yAxis: [{
            type: "value",
            name: "分钟",
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
            name: "工时损失率%",
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
            //{
            //    name: '投入工数',
            //    type: 'bar',
            //    barGap: 0,
            //    data: value3,
            //    stack: 'LOSS',//表示在哪一列
            //    itemStyle: {
            //        normal: {
            //            label: {
            //                show: true,//是否展示
            //            },
            //            color: function (params) {
            //                // build a color map as your need.
            //                //var colorList = [
            //                //    '#84C1FF', '#66B3FF', '#46A3FF', '#2894FF', '#0080FF',
            //                //    '#0072E3', '#0066CC', '#005AB5', '#004B97', '#003D79',
            //                //];
            //                var colorList = PrimarycolorList;
            //                return colorList[params.dataIndex]
            //            }
            //        },//表示堆叠柱状图填充的颜色
            //    }
            //},
            {
                name: '实际工数',
                type: 'bar',
                data: value1,
               stack: 'LOSS',//表示在哪一列
                itemStyle: {
                    normal: {
                        label: {
                            position: 'inside',
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
                name: '损失工数',
                type: 'bar',
                data: value2,
                stack: 'LOSS',//表示在哪一列
                itemStyle: {
                    normal: {
                        label: {
                            position: ['+4', '-40'],
                            show: true,//是否展示
                            formatter: [
                                '-',
                                '{c}',

                            ].join(''),
                        },

                        color: function (params) {
                            // build a color map as your need.
                            var colorList = SparecolorList;
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
                name: '工时损失率',
                type: 'line',
                yAxisIndex: 1,
                //stack: 'LOSS',
                areaStyle: {},
                data: value5,
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
                            if (params.value < 112) {
                                return "#FF0000";
                            } else if (params.value >= 112) {
                                return "#1d953f";

                            }
                            return "#225a1f";
                        },
                        //color: 'rgb(219,50,51)',
                        borderColor: randomRGBA,
                        borderWidth: 12
                    }
                },
            },
        ]
    };
    LossmyChart.setOption(option);
    LossmyChart.resize();
}
function  BindmyChart_Pp_Linestop(name, value, data) {
    var DestmyChart = echarts.init(document.getElementById('LineStopBar'));
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
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_生产线停线统计',  
                }
            }
        },
        title: {
            text: '生产线停线统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(Reason Top 5-Total:' + total_datas + 'MIN)',
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
        yAxis: {},
        series: [{
            name: '停线时间',
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
            name: '停线原因',
            type: 'pie',
            radius: [0, '30%'],
            center: ['75%', '50%'],
            //radius: '80%',
            //roseType: 'radius',
            //zlevel: 10,
            //startAngle: 100,
            data: data,
            label: {
                normal: {
                    formatter: ['{c|{c}MIN({d}%)}', '{b|{b}}'].join('\n'),
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
                length: 30,
                length2: 100,
                show: true,
                color: '#e5323e'
            },
            itemStyle: {

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
function BindmyChart_Pp_ModelAchieve(name, value3, value4) {
    var AchievemyChart = echarts.init(document.getElementById('ModelBar'));
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
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_机种达成率统计',
}
            }
        },
        title: {
            text: '机种达成率统计',
            subtext: '大于目标112%的10个机种，小于目标的后10个机种', //+ TransDates,
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'line'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />{a0}: {c0}%<br />{a1}: {c1}%',　　　　//这是关键，在需要的地方加上就行了
        },
        grid: {
            top: '10%',
            left: '10%',
            right: '10%',
            bottom: '10%',
            // containLabel: true
        },
        legend: {
            show: true,
            x: 'left',
            let:'50%'
        },
        xAxis: [
            {

                //left:'5%',
                type: 'category',
                axisLine: {
                    show: true
                },
                splitArea: {
                    // show: true,
                    color: '#f00',
                    lineStyle: {
                        color: '#f00'
                    },
                },
                axisLabel: {
                    //fontSize: '100%',
                    show: true,
                    color: '#003366',
                    interval: 0,
                    rotate: 30,

                },
                splitLine: {
                    show: true
                },
                boundaryGap: false,
                data: name
            }
        ],
        yAxis: [
            {
                type: 'value',
                min: 0,
                // max: 140,
                splitNumber: 4,
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(255,255,255,0.1)'
                    }
                },
                axisLine: {
                    show: true,
                },
                axisLabel: {
                    show: true,
                    formatter: '{value} %',
                    margin: 20,
                    textStyle: {
                        color: '#003366',

                    },
                },
                axisTick: {
                    show: true,
                },
            }
        ],
        series: [

            {
                name: '达成率',
                type: 'line',
                areaStyle: {},
                //yAxisIndex:1,
                data: value4,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 30,
                label: {
                    normal: {
                        fontSize: '100%',
                        show: true,
                        position: 'inside',
                        textStyle: {
                            color: '#FFFFF0',
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
                            color: 'rgba(0, 136, 212, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(0, 136, 212, 0)'
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
                        name: 'FY2024目标',
                        yAxis: 112,
                        itemStyle: {
                            normal: {
                                color: '#ff6d9d'
                            }
                        }
                    }]
                }
            }
        ]
    };
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    AchievemyChart.setOption(option);
    AchievemyChart.resize();
}
function BindmyChart_Pp_YearAchieve(name, value1, value2, value3,data) {
    //获取隐藏控件中的值
    //图表展示
    var ActualmyChart = echarts.init(document.getElementById('YearBar'));
    //debugger;
    //统计总数量
    var total_PlanQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_PlanQty += parseFloat(data[i].value1)
    }
    var total_ActualQty = 0;
    for (var i = 0; i < data.length; i++) {
        total_ActualQty += parseInt(data[i].value2)
    }
    var total_AchievingRate = Math.round(total_ActualQty / total_PlanQty * 10000) / 100.00 + "%";// 小数点后两位百分比

    //debugger;
    // 指定图表的配置项和数据
    var option = {
        //工具箱配置
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                //  缩放
                dataZoom: {
                    yAxisIndex: 'none'
                },
                // 数据视图
                dataView: {
                    show: true,
                    title: '数据视图',
                    optionToContent: function (opt) {
                        var axisData = opt.xAxis[0].data;
                        var series = opt.series;
                        var tdHeads = '<td  style="padding:0 10px;color:#FF9D6F">名称</td>';
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
                restore: { show: true },// 还原，复位原始图表
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_年度生产统计',                }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: '年度生产统计',//正标题
            //subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(计划:' + total_PlanQty.toFixed(2) + 'PCS;' + ',实绩:' + total_ActualQty + 'PCS;达成率:' + total_AchievingRate + ')',//副标题
            x: 'center'//标题水平方向位置
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />达成率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
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
            name: "达成率%",
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
        //图表数据
        series: [
            {
                name: '计划',
                type: 'bar',
                //stack: 'PLAN',//表示在哪一列
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
                name: '实绩',
                type: 'bar',
                //stack: 'Actual',//表示在哪一列
                data: value2,
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
                name: '达成率',
                type: 'line',
                //stack: 'PLAN',//表示在哪一列
                yAxisIndex: 1,
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
                        name: 'FY2024目标',
                        yAxis: 112,
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
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    ActualmyChart.setOption(option);
    ActualmyChart.resize();
}
function BindmyChart_Pp_Ttacking_Lot(name, value1, value2, value3, value4, value5, data) {
    //获取隐藏控件中的值
    //图表展示
    var ActualmyChart = echarts.init(document.getElementById('TrackingTime'));
    //debugger;
    //统计总数量
    //var total_PlanQty = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_PlanQty += parseFloat(data[i].value1)
    //}
    //var total_ActualQty = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_ActualQty += parseInt(data[i].value2)
    //}
    //var total_AchievingRate = Math.round(total_ActualQty / total_PlanQty * 10000) / 100.00 + "%";// 小数点后两位百分比

    //debugger;
    // 指定图表的配置项和数据
    var option = {
        //工具箱配置
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                //  缩放
                dataZoom: {
                    yAxisIndex: 'none'
                },
                // 数据视图
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
                restore: { show: true },// 还原，复位原始图表
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_Process分析',   
                }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: 'Process分析',//正标题
            //subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(计划:' + total_PlanQty.toFixed(2) + 'PCS;' + ',实绩:' + total_ActualQty + 'PCS;达成率:' + total_AchievingRate + ')',//副标题
            x: 'center'//标题水平方向位置
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            //formatter: '{b}<br />达成率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
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
        yAxis: {},
        //图表数据
        series: [
            {
                name: 'ProcessQty',
                type: 'bar',
                stack: 'Qty',//表示在哪一列
                barGap: 0,
                data: value3,
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
            //{
            //    name: '实绩',
            //    type: 'bar',
            //    stack: 'Actual',//表示在哪一列
            //    data: value2,
            //    itemStyle: {
            //        normal: {
            //            label: {
            //                show: true,//是否展示
            //            },
            //            color: function (params) {
            //                // build a color map as your need.
            //                var colorList = SecondarycolorList;
            //                return colorList[params.dataIndex]
            //            }
            //        },//表示堆叠柱状图填充的颜色
            //    }
            //},
            {
                name: 'Max_Sec',
                type: 'line',
                stack: 'PLAN',//表示在哪一列
                step: 'end',
                //yAxisIndex: 1,
                areaStyle: {},
                data: value1,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 5,
                //label: {
                //    normal: {
                //        show: true,
                //        position: 'inside',
                //        textStyle: {
                //            color: '#fff',
                //        },
                //        //百分比格式
                //        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                //    }

                //},
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
                //itemStyle: {
                //    normal: {
                //        ///  通过params.value拿到对应的data里面的数据
                //        color: function (params) {
                //            if (params.value < 98) {
                //                return "#ed1941";
                //            } else if (params.value = 110) {
                //                return "#1d953f";

                //            }
                //            return "#225a1f";
                //        },
                //        //color: 'rgb(219,50,51)',
                //        borderColor: randomRGBA,
                //        //borderColor: 'rgba(72,209,204,0.2)',
                //        borderWidth: 12
                //    }
                //},
            },
            {
                name: 'Avg_Sec',
                type: 'line',
                step: 'middle',
                stack: 'PLAN',//表示在哪一列
                //yAxisIndex: 1,
                areaStyle: {},
                data: value5,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 5,
                //label: {
                //    normal: {
                //        show: true,
                //        position: 'inside',
                //        textStyle: {
                //            color: '#fff',
                //        },
                //        //百分比格式
                //        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                //    }

                //},
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
                //itemStyle: {
                //    normal: {
                //        ///  通过params.value拿到对应的data里面的数据
                //        color: function (params) {
                //            if (params.value < 98) {
                //                return "#ed1941";
                //            } else if (params.value = 110) {
                //                return "#1d953f";

                //            }
                //            return "#225a1f";
                //        },
                //        //color: 'rgb(219,50,51)',
                //        borderColor: randomRGBA,
                //        //borderColor: 'rgba(72,209,204,0.2)',
                //        borderWidth: 12
                //    }
                //},
            },
            {
                name: 'Min_Sec',
                type: 'line',
                step: 'start',
                stack: 'PLAN',//表示在哪一列
                //yAxisIndex: 1,
                areaStyle: {},
                data: value2,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 5,
                //label: {
                //    normal: {
                //        show: true,
                //        position: 'inside',
                //        textStyle: {
                //            color: '#fff',
                //        },
                //        //百分比格式
                //        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                //    }

                //},
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
                //itemStyle: {
                //    normal: {
                //        ///  通过params.value拿到对应的data里面的数据
                //        color: function (params) {
                //            if (params.value < 98) {
                //                return "#ed1941";
                //            } else if (params.value = 110) {
                //                return "#1d953f";

                //            }
                //            return "#225a1f";
                //        },
                //        //color: 'rgb(219,50,51)',
                //        borderColor: randomRGBA,
                //        //borderColor: 'rgba(72,209,204,0.2)',
                //        borderWidth: 12
                //    }
                //},
            },
        ]
    };
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    ActualmyChart.setOption(option);
    ActualmyChart.resize();
}

function BindmyChart_Pp_Ttacking_OPH(name, value3, value4, value5, data) {
    //获取隐藏控件中的值
    //图表展示
    var ActualmyChart = echarts.init(document.getElementById('TrackingOPH'));
    //debugger;
    //统计总数量
    //var total_PlanQty = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_PlanQty += parseFloat(data[i].value1)
    //}
    //var total_ActualQty = 0;
    //for (var i = 0; i < data.length; i++) {
    //    total_ActualQty += parseInt(data[i].value2)
    //}
    //var total_AchievingRate = Math.round(total_ActualQty / total_PlanQty * 10000) / 100.00 + "%";// 小数点后两位百分比

    //debugger;
    // 指定图表的配置项和数据
    var option = {
        //工具箱配置
        toolbox: {
            show: true,
            orient: 'horizontal',

            feature: {
                //  缩放
                dataZoom: {
                    yAxisIndex: 'none'
                },
                // 数据视图
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
                restore: { show: true },// 还原，复位原始图表
                saveAsImage: {
                    show: true,
                    title: 'save as image',
                    name: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月_Process分析',   
                }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: 'Process分析',//正标题
            //subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(计划:' + total_PlanQty.toFixed(2) + 'PCS;' + ',实绩:' + total_ActualQty + 'PCS;达成率:' + total_AchievingRate + ')',//副标题
            x: 'center'//标题水平方向位置
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            //formatter: '{b}<br />达成率:{c2}%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
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
        yAxis: {},
        //图表数据
        series: [
            {
                name: 'ProcessQty',
                type: 'bar',
                stack: 'Qty',//表示在哪一列
                barGap: 0,
                data: value3,
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
            //{
            //    name: '实绩',
            //    type: 'bar',
            //    stack: 'Actual',//表示在哪一列
            //    data: value2,
            //    itemStyle: {
            //        normal: {
            //            label: {
            //                show: true,//是否展示
            //            },
            //            color: function (params) {
            //                // build a color map as your need.
            //                var colorList = SecondarycolorList;
            //                return colorList[params.dataIndex]
            //            }
            //        },//表示堆叠柱状图填充的颜色
            //    }
            //},
            {
                name: 'Std_Devp',
                type: 'line',
                stack: 'PLAN',//表示在哪一列
                step: 'end',
                //yAxisIndex: 1,
                areaStyle: {},
                data: value4,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 5,
                //label: {
                //    normal: {
                //        show: true,
                //        position: 'inside',
                //        textStyle: {
                //            color: '#fff',
                //        },
                //        //百分比格式
                //        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                //    }

                //},
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
                //itemStyle: {
                //    normal: {
                //        ///  通过params.value拿到对应的data里面的数据
                //        color: function (params) {
                //            if (params.value < 98) {
                //                return "#ed1941";
                //            } else if (params.value = 110) {
                //                return "#1d953f";

                //            }
                //            return "#225a1f";
                //        },
                //        //color: 'rgb(219,50,51)',
                //        borderColor: randomRGBA,
                //        //borderColor: 'rgba(72,209,204,0.2)',
                //        borderWidth: 12
                //    }
                //},
            },
            //{
            //    name: 'Avg_Sec',
            //    type: 'line',
            //    step: 'middle',
            //    stack: 'PLAN',//表示在哪一列
            //    //yAxisIndex: 1,
            //    areaStyle: {},
            //    data: value5,
            //    // smooth: true, //是否平滑
            //    showAllSymbol: true,
            //    // symbol: 'image://./static/images/guang-circle.png',
            //    symbol: 'circle',
            //    symbolSize: 5,
            //    //label: {
            //    //    normal: {
            //    //        show: true,
            //    //        position: 'inside',
            //    //        textStyle: {
            //    //            color: '#fff',
            //    //        },
            //    //        //百分比格式
            //    //        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
            //    //    }

            //    //},
            //    lineStyle: {
            //        normal: {
            //            width: 1
            //        }
            //    },
            //    areaStyle: {
            //        normal: {
            //            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
            //                offset: 0,
            //                color: 'rgba(219, 50, 51, 0.3)'
            //            }, {
            //                offset: 0.8,
            //                color: 'rgba(219, 50, 51, 0)'
            //            }], false),
            //            shadowColor: 'rgba(0, 0, 0, 0.1)',
            //            shadowBlur: 10
            //        }
            //    },
            //    //itemStyle: {
            //    //    normal: {
            //    //        ///  通过params.value拿到对应的data里面的数据
            //    //        color: function (params) {
            //    //            if (params.value < 98) {
            //    //                return "#ed1941";
            //    //            } else if (params.value = 110) {
            //    //                return "#1d953f";

            //    //            }
            //    //            return "#225a1f";
            //    //        },
            //    //        //color: 'rgb(219,50,51)',
            //    //        borderColor: randomRGBA,
            //    //        //borderColor: 'rgba(72,209,204,0.2)',
            //    //        borderWidth: 12
            //    //    }
            //    //},
            //},
            {
                name: 'Avg_Sec',
                type: 'line',
                step: 'start',
                stack: 'PLAN',//表示在哪一列
                //yAxisIndex: 1,
                areaStyle: {},
                data: value5,
                // smooth: true, //是否平滑
                showAllSymbol: true,
                // symbol: 'image://./static/images/guang-circle.png',
                symbol: 'circle',
                symbolSize: 5,
                //label: {
                //    normal: {
                //        show: true,
                //        position: 'inside',
                //        textStyle: {
                //            color: '#fff',
                //        },
                //        //百分比格式
                //        //formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
                //    }

                //},
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
                //itemStyle: {
                //    normal: {
                //        ///  通过params.value拿到对应的data里面的数据
                //        color: function (params) {
                //            if (params.value < 98) {
                //                return "#ed1941";
                //            } else if (params.value = 110) {
                //                return "#1d953f";

                //            }
                //            return "#225a1f";
                //        },
                //        //color: 'rgb(219,50,51)',
                //        borderColor: randomRGBA,
                //        //borderColor: 'rgba(72,209,204,0.2)',
                //        borderWidth: 12
                //    }
                //},
            },
        ]
    };
    //debugger;
    // 使用刚指定的配置项和数据显示图表。
    ActualmyChart.setOption(option);
    ActualmyChart.resize();
}