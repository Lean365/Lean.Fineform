/* !
 * Echarts图表封装模板
 * 版 本 20200416.008(https://github.com/davischeng)
 * Copyright 2020 LeanCloud.Inc
 * 创建人：Davis.Cheng
 * 商业授权&遵循License: GNU GPL 3.0.
 * 描  述：Ajax异步交互式数据更新
 * https://github.com/davischeng/oneCube/blob/master/licenses.txt
 * Date: 2020-04-16T16:01Z
 */

//出货仕向地
function AjaxData_Sd_Regin() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    //debugger;
    $.ajax({
        url: "/Lf_Report/shipment_regin.ashx",
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
                BindmyChart_SD_Regin(name, value, data);          //调用封装好的ReginmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//出货目的地
function AjaxData_Sd_Destination() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/shipment_location.ashx",
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
                BindmyChart_SD_Destination(name, value, data);          //调用封装好的DestmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//出货BUCode
function AjaxData_Sd_BuCode() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/shipment_bucode.ashx",
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
                BindmyChart_SD_BuCode(name, value, data);          //调用封装好的ReginmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//年度统计
function AjaxData_Sd_FyJPStats() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/sd_fyjp.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            //debugger;
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
                BindmyChart_SD_FyJPStats(name, value1, value2, data);          //调用封装好的ReginmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//年度统计
function AjaxData_Sd_FyCNStats() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/sd_fycn.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            //debugger;
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
                BindmyChart_SD_FyCNStats(name, value1, value2, data);          //调用封装好的ReginmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//年度统计
function AjaxData_Sd_FyBuJPStats() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/sd_fybujp.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            //debugger;
            if (data) {
                var list = eval(data);
                var name = [];
                var value = [];
                //var value2 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value.push(list[i].value);
                    // value2.push(list[i].value2);
                }
                BindmyChart_SD_FyBuJPStats(name, value, data);          //调用封装好的ReginmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//年度统计
function AjaxData_Sd_FyBuCNStats() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/sd_fybucn.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            //debugger;
            if (data) {
                var list = eval(data);
                var name = [];
                var value = [];
                //var value2 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value.push(list[i].value);
                    //value2.push(list[i].value2);
                }
                BindmyChart_SD_FyBuCNStats(name, value, data);          //调用封装好的ReginmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}