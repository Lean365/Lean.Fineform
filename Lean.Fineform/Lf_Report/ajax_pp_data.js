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
//生产实绩
function AjaxData_Pp_Actual() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_actual.ashx",
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
                BindmyChart_Pp_Actual(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//前一天生产实绩

function AjaxData_Pp_Lastday_Actual() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_last_actual.ashx",
        data: {
            TransDate: "DTA"
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
                BindmyChart_Pp_Last_Actual(name, value1, value2, value3, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//生产不良
function AjaxData_Pp_Defect() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_defect.ashx",
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
                var value4 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value4.push(list[i].value4);
                }
                BindmyChart_Pp_Defect(name, value1, value2, value4, data);          //调用封装好的DefectmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//未达成原因
function AjaxData_Pp_Reason() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_reason.ashx",
        data: {
            TransDate: TransDates
        },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                //var list = eval(data);
                //var name = [];
                //var value = [];
                //for (var i = 0; i < list.length; i++) {
                //    name.push(list[i].name);
                //    value.push(list[i].value);
                //}
                BindmyChart_Pp_Reason(data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//直行率
function AjaxData_Pp_Direct() {
   // debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_defect.ashx",
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
                var value3 = [];
                var value4 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                }
                BindmyChart_Pp_Direct(name, value3, value4);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//达成率
function AjaxData_Pp_Achieve() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_achieve.ashx",
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
                var value4 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                }
                BindmyChart_Pp_Achieve(name, value3, value4);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//生产进度
function AjaxData_Pp_Progress() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    //准备数据
    $.ajax({
        url: "/Lf_Report/Pp_moprogress.ashx",
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
                var value4 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                }
                BindmyChart_Pp_Progress(name, value3, value4, data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//班组损失工数
function AjaxData_Pp_LineLosstime() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    //准备数据
    $.ajax({
        url: "/Lf_Report/Pp_losstime_line.ashx",
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
                var value4 = [];
                var value5 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                    value5.push(list[i].value5);
                }
                BindmyChart_Pp_LineLoss(name, value1, value2, value3, value4, value5, data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//损失工数
function AjaxData_Pp_Losstime() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    //准备数据
    $.ajax({
        url: "/Lf_Report/Pp_losstime.ashx",
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
                var value4 = [];
                var value5 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                    value5.push(list[i].value5);
                }
                BindmyChart_Pp_Loss(name, value1, value2, value3, value4, value5, data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//停线原因
function AjaxData_Pp_Linestop() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    //准备数据
    $.ajax({
        url: "/Lf_Report/Pp_reason_stopline.ashx",
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
                BindmyChart_Pp_Linestop(name, value, data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//机种达成率
function AjaxData_Pp_ModelAchieve() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_achieve_model.ashx",
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
                var value4 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                }
                BindmyChart_Pp_ModelAchieve(name, value3, value4);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//机种达成率
function AjaxData_Pp_YearAchieve() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_achieve_year.ashx",
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
                var value4 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);

                }
                BindmyChart_Pp_YearAchieve(name, value1, value2, value3,data);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}

//生产批次追踪
function AjaxData_Pp_Tracking_Lot() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_tracking_lot.ashx",
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
                var value4 = [];
                var value5 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                    value5.push(list[i].value5);

                }
                BindmyChart_Pp_Ttacking_Lot(name, value1, value2, value3, value4, value5, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//批次OPH追踪
function AjaxData_Pp_Tracking_OPH() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/Pp_tracking_lot.ashx",
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
                var value4 = [];
                var value5 = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value1.push(list[i].value1);
                    value2.push(list[i].value2);
                    value3.push(list[i].value3);
                    value4.push(list[i].value4);
                    value5.push(list[i].value5);

                }
                BindmyChart_Pp_Ttacking_OPH(name, value3, value4, value5, data);          //调用封装好的ActualmyChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}