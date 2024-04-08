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
//合格率
function AjaxData_Qm_Pass() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/qm_pass.ashx",
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
                BindChartData_QM_Pass(name, value1, value2, value3);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
//批次合格率
function AjaxData_LotPass() {
    //debugger;
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "/Lf_Report/qm_pass_lot.ashx",
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
                BindChartData_QM_LotPass(name, value1, value2, value3);       // 圆形不同的是数据类型是这样的：{value:335, name:'直接访问'},{value:310, name:'邮件营销'},{value:234, name:'联盟广告'}
            }
        },
        error: function (msg) {
            alert(msg + "Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}