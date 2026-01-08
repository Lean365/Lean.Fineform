/* !
 * Echarts图表封装模板
 * 版 本 20200416.008(https://github.com/Lean365)
 * Copyright 2020 Lean365.Inc
 * 创建人：Davis.Ching
 * 商业授权&遵循License: GNU GPL 3.0.
 * 描  述：图表类封装
 * https://github.com/Lean365
 * Date: 2020-04-16T16:01Z
 */
document.write("<script language=javascript src='/Lf_Report//define_echart.js'></script >");
//(注：有时你引用的文件还可能需要引用其他的js,我们需要将需要的那个js文件也以同样的方法引用进来)

function BindChartData_Expense_Contrast(name, value1, value2) {
    var PassChart = echarts.init(document.getElementById('ContrastBar'));

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
                        var axisData = opt.yAxis[0].data;
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
            text: '费用分析统计',
            subtext: 'DTA:月度费用分析',// + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 4) + '月',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            //     formatter: function(params){return Math.max(params.value,-params.value)}

            formatter: function (params) {
                return params[0].name +
                    "<br>计划费用：" + params[0].value + "CNY" +
                    "<br>实际费用：" + params[1].value + "CNY" +
                    "<br>差异%：" + (((parseFloat(params[1].value) * -1) - parseFloat(params[0].value)) / parseFloat(params[0].value) * 100).toFixed(2) + "%";
            }
            //formatter: '{b}<br />差异:{c1}/({c0}-{c1})%<br />{a0}: {c0}<br />{a1}: {c1}',　　　　//这是关键，在需要的地方加上就行了
        },
        legend: {
            show: true,
            x: 'left'
        },
        //grid: [{
        //    top: 50,
        //    width: '50%',
        //    bottom: '45%',
        //    left: 10,
        //    containLabel: true
        //}, {
        //    top: '55%',
        //    width: '50%',
        //        bottom: '5%',
        //    left: 10,
        //    containLabel: true
        //}],
        //xAxis: [{
        //    type: 'value',
        //    //max: builderJson.all,
        //    splitLine: {
        //        show: false
        //    }
        //}, {
        //    type: 'value',
        //    //max: builderJson.all,
        //    gridIndex: 1,
        //    splitLine: {
        //        show: false
        //    }
        //}],
        //yAxis: [{
        //    type: 'category',
        //    data: name,
        //    axisLabel: {
        //        interval: 0,
        //        rotate: 30
        //    },
        //    splitLine: {
        //        show: false
        //    }
        //}, {
        //    gridIndex: 1,
        //    type: 'category',
        //        data: name,
        //    axisLabel: {
        //        interval: 0,
        //        rotate: 30
        //    },
        //    splitLine: {
        //        show: false
        //    }
        //}],
        xAxis: {
            type: 'value',
            axisLabel: {
                formatter: function (value) {
                    if ((value / (1000)) > 10) {
                        return (value / (1000)) + " KCNY";
                    } else if ((value / (1000)) < -10) {
                        return (value / (1000)) + " KCNY";
                    }

                    else {
                        return value + " CNY";
                    }
                }
            }
        },
        yAxis: {
            data: name,
            axisLabel: {
                interval: 0,
                rotate: 20
            }
        },
        series: [

            {
                name: '计划',
                type: 'bar',
                barGap: 0,
                //z: 3,
                data: value1,
                //silent: true,
                stack: 'Exp',
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                            position: 'right',
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
                name: '实际',
                type: 'bar',
                //z: 3,
                barGap: 0,
                data: value2,
                //silent: true,
                //xAxisIndex: 1,
                //yAxisIndex: 1,
                stack: 'Exp',
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                            position: 'left'
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
            //{
            //    name: '合格率',
            //    type: 'line',
            //    //yAxisIndex: 1,
            //    areaStyle: {},
            //    data: value3,
            //    // smooth: true, //是否平滑
            //    showAllSymbol: true,
            //    // symbol: 'image://./static/images/guang-circle.png',
            //    symbol: 'circle',
            //    symbolSize: 35,
            //    label: {
            //        normal: {
            //            show: true,
            //            position: 'inside',
            //            textStyle: {
            //                color: '#fff',
            //            },
            //            //百分比格式
            //            formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
            //        }

            //    },
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
            //    itemStyle: {
            //        normal: {
            //            color: 'rgb(219,50,51)',
            //            borderColor: 'rgba(219,50,51,0.2)',
            //            borderWidth: 12
            //        }
            //    },
            //},
        ]
    };
    PassChart.setOption(option);
    PassChart.resize();
}
function BindChartData_Expense_DeptRequests(name, value2, data) {
    var PassChart = echarts.init(document.getElementById('RequisitionBar'));
    var total_Amount = 0;
    for (var i = 0; i < data.length; i++) {
        total_Amount += parseFloat(data[i].value2)
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
            text: '辅助材料费用统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月(辅助材料出库总费用:' + total_Amount.toFixed(2) + 'CNY)',
            x: 'center'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />{a}: {c}CNY',　　　　//这是关键，在需要的地方加上就行了
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
        yAxis: {},
        series: [
            {
                name: '费用',
                type: 'bar',
                barGap: 0,
                data: value2,
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
            //    name: '验退',
            //    type: 'bar',
            //    data: value1,
            //    itemStyle: {
            //        normal: {
            //            label: {
            //                show: true,//是否展示
            //            },
            //            color: function (params) {
            //                // build a color map as your need.
            //                var colorList = [
            //                    '#003D79', '#004B97', '#005AB5', '#0066CC', '#0072E3',
            //                    '#0080FF', '#2894FF', '#46A3FF', '#66B3FF', '#84C1FF',
            //                ];
            //                return colorList[params.dataIndex]
            //            }
            //        },//表示堆叠柱状图填充的颜色
            //    }
            //},
            //{
            //    name: '合格率',
            //    type: 'line',
            //    //yAxisIndex: 1,
            //    areaStyle: {},
            //    data: value3,
            //    // smooth: true, //是否平滑
            //    showAllSymbol: true,
            //    // symbol: 'image://./static/images/guang-circle.png',
            //    symbol: 'circle',
            //    symbolSize: 35,
            //    label: {
            //        normal: {
            //            show: true,
            //            position: 'inside',
            //            textStyle: {
            //                color: '#fff',
            //            },
            //            //百分比格式
            //            formatter: '{c}%'　　　　//这是关键，在需要的地方加上就行了
            //        }

            //    },
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
            //    itemStyle: {
            //        normal: {
            //            color: 'rgb(219,50,51)',
            //            borderColor: 'rgba(219,50,51,0.2)',
            //            borderWidth: 12
            //        }
            //    },
            //},
        ]
    };
    PassChart.setOption(option);
    PassChart.resize();
}
function BindChartData_Expense_Actual(name, value, data) {
    var myChart = echarts.init(document.getElementById('ActualExs'));
    //debugger;
    //统计总数量
    var total_datas = 0;
    for (var i = 0; i < data.length; i++) {
        total_datas += parseFloat(data[i].value)
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
            text: '月度费用统计',
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(Total:' + total_datas + 'CNY)',
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
                rotate: -0
            }
        },
        yAxis: {},
        series: [{
            name: '费用',
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
            name: '费用',
            type: 'pie',
            radius: [30, 110],
            center: ['75%', '50%'],
            //color: [
            //    '#DC143C', '#000080', '#2E8B57', '#FF4500', "#BA55D3"
            //    ],
            data: data,
            roseType: 'area',
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
function BindChartData_Expense_Tree(data) {
    var TreemyChart = echarts.init(document.getElementById('TreeBar'));
    TreemyChart.showLoading();
    let BindTree = [];
    BindTree.push(trans_tree(data));
    function trans_tree(jsonData) {
        //temp为临时对象，将json数据按照id值排序.
        var result = [], temp = {}, len = jsonData.length

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
                result.push(list);
            }
        }
        return result;
    }
    // 指定图表的配置项和数据
    var option = {
        type: "tree",
        //backgroundColor: "#000",
        toolbox: { //工具栏
            show: true,
            iconStyle: {
                borderColor: "#03ceda"
            },
            feature: {
                restore: {},
                saveAsImage: { show: true },
            }
        },
        tooltip: {//提示框
            trigger: "item",
            triggerOn: "mousemove",
            backgroundColor: "rgba(1,70,86,1)",
            borderColor: "rgba(0,246,255,1)",
            borderWidth: 0.5,
            textStyle: {
                fontSize: 10
            }
        },
        //series: [
        //    {
        //        type: 'tree',
        //        name: '成本要素',
        //        data: data,

        //        top: '2%',
        //        left: '2%',
        //        bottom: '2%',
        //        right: '2%',

        //        orient: 'BT',

        //        expandAndCollapse: true,

        //        label: {
        //            position: 'bottom',
        //            rotate: 90,
        //            horizontalAlign: 'middle',
        //            align: 'right'
        //        },

        //        leaves: {
        //            label: {
        //                position: 'top',
        //                rotate: 90,
        //                horizontalAlign: 'middle',
        //                align: 'left'
        //            }
        //        },

        //        expandAndCollapse: true,

        //        animationDuration: 550,
        //        animationDurationUpdate: 750
        //    }
        //]
        series: [
            {
                type: "tree",
                name: '成本要素',
                hoverAnimation: true, //hover样式
                data: data,
                left: '2%',
                right: '2%',
                top: '2%',
                bottom: '2%',

                //symbol: 'emptyCircle',

                //orient: 'BT',
                layout: "radial",
                symbol: "circle",
                symbolSize: 10,
                nodePadding: 20,
                animationDurationUpdate: 750,
                expandAndCollapse: true, //子树折叠和展开的交互，默认打开
                initialTreeDepth: 2,
                roam: true, //是否开启鼠标缩放和平移漫游。scale/move/true
                focusNodeAdjacency: true,
                itemStyle: {
                    borderWidth: 1,
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = TreecolorList;
                        return colorList[params.dataIndex]
                    }
                },
                //label: {
                //    position: 'bottom',
                //    rotate: 90,
                //    horizontalAlign: 'middle',
                //    align: 'right'
                //},

                //leaves: {
                //    label: {
                //        position: 'top',
                //        rotate: 90,
                //        horizontalAlign: 'middle',
                //        align: 'left'
                //    }
                //},
                lineStyle: {
                    width: 1,
                    curveness: 0.5,
                }
            }
        ]
        //series: {
        //    type: 'sunburst',
        //    highlightPolicy: 'ancestor',
        //    data: data,
        //    radius: [0, '95%'],
        //    sort: null,
        //    levels: [{}, {
        //        r0: '15%',
        //        r: '35%',
        //        itemStyle: {
        //            borderWidth: 2
        //        },
        //        label: {
        //            rotate: 'tangential'
        //        }
        //    }, {
        //        r0: '35%',
        //        r: '70%',
        //        label: {
        //            align: 'right'
        //        }
        //    }, {
        //        r0: '70%',
        //        r: '72%',
        //        label: {
        //            position: 'outside',
        //            padding: 3,
        //            silent: false
        //        },
        //        itemStyle: {
        //            borderWidth: 3
        //        }
        //    }]
        //}
    };
    TreemyChart.setOption(option);
    TreemyChart.resize();
    TreemyChart.hideLoading();
}

//材料费比率
function BindChartData_CostingMaterialcost(name, value1, value2, value3, data) {
    //获取隐藏控件中的值
    //图表展示
    var myChart = echarts.init(document.getElementById('MaterialcostChart'));
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
                saveAsImage: { show: true }// 保存为图片
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
    myChart.setOption(option);
    myChart.resize();
}
//需求量
function BindChartData_CostingNeedQty(name, value1, value2, value3, data) {
    //获取隐藏控件中的值
    //图表展示
    var myChart = echarts.init(document.getElementById('NeedQtyChart'));
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
                saveAsImage: { show: true }// 保存为图片
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
    myChart.setOption(option);
    myChart.resize();
}
//营业毛利
function BindChartData_CostingGrossOperatingMargin(name, value1, value2, value3, data) {
    //获取隐藏控件中的值
    //图表展示
    var myChart = echarts.init(document.getElementById('GrossOperatingMarginChart'));
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
                saveAsImage: { show: true }// 保存为图片
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
    myChart.setOption(option);
    myChart.resize();
}
//FC
function BindChartData_CostingComparedForecast(name, value1, value2, value3, data) {
    //获取隐藏控件中的值
    //图表展示
    var myChart = echarts.init(document.getElementById('ComparedForecastChart'));
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
                saveAsImage: { show: true }// 保存为图片
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
    myChart.setOption(option);
    myChart.resize();
}

//成本
function BindChartData_CostingProductCost(name, value1, value2, value3, data) {
    //获取隐藏控件中的值
    //图表展示
    var myChart = echarts.init(document.getElementById('ProductCostChart'));
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
                saveAsImage: { show: true }// 保存为图片
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
    myChart.setOption(option);
    myChart.resize();
}
//在库
function BindChartData_CostingInvAMT(name, value1, value2, data) {
    var myChart = echarts.init(document.getElementById('InvAmtChart'));
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
                saveAsImage: { show: true }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: '月库存统计',//正标题
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(库存:' + total_PlanQty + 'PCS;' + ',金额:' + total_ActualQty + 'CNY)',//副标题
            x: 'center'//标题水平方向位置
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
            name: "库存",
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
                name: '库存',
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
    myChart.setOption(option);
    myChart.resize();
}

function BindChartData_CostingInvAMTbu(name, value1, value2, data) {
    var myChart = echarts.init(document.getElementById('buInvAmtChart'));
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
                saveAsImage: { show: true }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: '月库存统计',//正标题
            subtext: 'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(库存:' + total_PlanQty + 'PCS;' + ',金额:' + total_ActualQty + 'CNY)',//副标题
            x: 'center'//标题水平方向位置
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
            name: "库存",
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
                name: '库存',
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
    myChart.setOption(option);
    myChart.resize();
}

function BindChartData_CostingMonthlyInvAMT(name, value1, value2, data) {
    var myChart = echarts.init(document.getElementById('AnnualInvAmtChart'));
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
                saveAsImage: { show: true }// 保存为图片
            }
        },
        //图表标题
        title: {
            text: '年度库存分析',//正标题
            subtext: '单位:万元',//'DTA:' + TransDates.substring(0, 4) + '年' + TransDates.substring(4, 6) + '月' + '(库存:' + total_PlanQty + 'PCS;' + ',金额:' + total_ActualQty + 'CNY)',//副标题
            x: 'center'//标题水平方向位置
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            },
            formatter: '{b}<br />{a0}: {c0}万元',　　　　//这是关键，在需要的地方加上就行了
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
            name: "库存",
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
                name: '金额',
                type: 'bar',
                stack: 'PLAN',//表示在哪一列
                barGap: 0,
                data: value2,
                itemStyle: {
                    normal: {
                        label: {
                            show: true,//是否展示
                            position: 'top',
                        },
                        color: function (params) {
                            // build a color map as your need.
                            var colorList = PrimarycolorList;
                            return colorList[params.dataIndex]
                        }
                    },//表示堆叠柱状图填充的颜色
                }
            }

        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    myChart.resize();
}