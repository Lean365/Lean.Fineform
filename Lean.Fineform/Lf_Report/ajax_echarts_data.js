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


function AjaxDestLineChart() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "ashx/Echarts.ashx",
        data: { cmd: "bar" },
        cache: false,
        async: false,
        dataType: "json",
        success: function (data) {
            if (data) {
                var list = eval(data);
                var name = [];      //Line的数据需要object类型，所以创建name和value数组存放数据
                var value = [];
                for (var i = 0; i < list.length; i++) {
                    name.push(list[i].name);
                    value.push(list[i].value);
                }
                LineChart(name, value);     //调用封装好的Line    
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
function AjaxDestRadarChart() {
    //-----------上下都是设置样式的可以无视掉，这里才是核心--------------------
    $.ajax({
        url: "ashx/Echarts.ashx",
        data: { cmd: "bar" },
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
                RadarChart(name, value, data);          //调用封装好的RadarChart
            }
        },
        error: function (msg) {
            alert("Relevant information is not available.无相关信息！");
        }
    });
    //---------------------------------------------------------------------------
}
// debugger;




