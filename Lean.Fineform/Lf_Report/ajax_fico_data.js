/* !
 * Echarts图表封装模板
 * 版 本 20200416.008(https://github.com/Lean365)
 * Copyright 2020 LeanCloud.Inc
 * 创建人：Davis.Ching
 * 商业授权&遵循License: GNU GPL 3.0.
 * 描  述：Ajax异步交互式数据更新
 * https://github.com/Lean365
 * Date: 2020-04-16T16:01Z
 */

//费用对比
function AjaxData_Expense_Contrast() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        //相对路径【网站根目录可以用"/"开始】
        url: "/Lf_Report/fico_expense_contrast.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_Expense_Contrast(name, value1, value2);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//费用请求
function AjaxData_Expense_DeptRequests() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/FICO_Expense_deptrequests.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_Expense_DeptRequests(name, value2, data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}

function AjaxData_Expense_Actual() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        //url: "/Lf_Report/FICO_Expense_actual.ashx",

        url: "/Lf_Report/fico_expense_actual.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value = [];

                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value.push(list[i].value);
                }
                BindChartData_Expense_Actual(name, value, data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//成本要素
function AjaxData_Expense_Tree() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    //准备数据
    $.ajax({
        url: "/Lf_Report/fico_subjects_tree.ashx",
        //data: {
        //    TransDate: TransDates
        //},
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value = [];

                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value.push(list[i].value);
                }
                BindChartData_Expense_Tree(data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}

function AjaxData_CostingMaterialcost() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/actual_chart.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_CostingMaterialcost(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//MRP需求量
function AjaxData_CostingNeedQty() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/actual_chart.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_CostingNeedQty(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}

function AjaxData_CostingGrossOperatingMargin() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/actual_chart.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_CostingGrossOperatingMargin(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//FC
function AjaxData_CostingComparedForecast() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/actual_chart.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_CostingComparedForecast(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//
function AjaxData_CostingbuAmount() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/actual_chart.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_CostingbuAmount(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//成本
function AjaxData_CostingProductCost() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/actual_chart.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];
                var value3 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                }
                BindChartData_CostingProductCost(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//在库
function AjaxData_CostingInvAMT() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/fico_costing_invamt.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];

                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                }
                BindChartData_CostingInvAMT(name, value1, value2, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//在库BU区分
function AjaxData_CostingInvAMTbu() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/fico_costing_invamt_bu.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];

                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                }
                BindChartData_CostingInvAMTbu(name, value1, value2, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//月在库统计
function AjaxData_CostingMonthlyInvAMT() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/fico_costing_monthlyinvamt.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];
                var value1 = [];
                var value2 = [];

                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                }
                BindChartData_CostingMonthlyInvAMT(name, value1, value2, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}