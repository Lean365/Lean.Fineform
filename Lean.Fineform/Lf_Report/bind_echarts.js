
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



var PrimarycolorList = [
    '#ff7f50', '#87cefa', '#da70d6', '#32cd32', '#6495ed',
    '#ff69b4', '#ba55d3', '#cd5c5c', '#ffa500', '#40e0d0',
    '#1e90ff', '#ff6347', '#7b68ee', '#d0648a', '#ffd700',
    '#6b8e23', '#4ea397', '#3cb371', '#b8860b', '#7bd9a5',
    '#4150d8', '#28bf7e', '#ed7c2f', '#f2a93b', '#f9cf36',
    '#4a5bdc', '#4cd698', '#f4914e', '#fcb75b', '#ffe180',
    '#b6c2ff', '#96edc1'];
var SecondarycolorList = [
    '#229aff', '#fcbf5d', '#ff686b', '#a3f29d', '#00b1f2',
    '#fff225', '#ff93c3', '#23e8c7', '#fca04e', '#4cccff',
    '#b8f049', '#c593ff', '#0be6c1', '#fbd65b', '#73e3ff',
    '#ff937c', '#6aeaa4', '#388bff', '#fce75d', '#ffbab3',
    '#ff5662', '#4fe995', '#d9bfff', '#ffc43d', '#6b8dfe',
    '#fc5bb4', '#26d8ef', '#8ca1ff', '#b5e2e2', '#ffa69e',
    '#d0b0ff', '#F7393C'];
var SparecolorList = [
    '#929fff', '#9de0ff', '#ffa897', '#af87fe', '#7dc3fe',
    '#bb60b2', '#433e7c', '#f47a75', '#009db2', '#024b51',
    '#0780cf', '#765005', '#e75840', '#26ccd8', '#3685fe',
    '#9977ef', '#f5616f', '#f7b13f', '#f9e264', '#50c48f',
    '#9370DB', '#87CEEB', '#F5F5DC', '#F5DEB3', '#BC8F8F',
    '#8B636C', '#8B7500'];
var AuxiliarycolorList = [
    '#229aff', '#fcbf5d', '#ff686b', '#a3f29d', '#00b1f2',
    '#fff225', '#ff93c3', '#23e8c7', '#fca04e', '#4cccff',
    '#b8f049', '#c593ff', '#0be6c1', '#fbd65b', '#73e3ff',
    '#ff937c', '#6aeaa4', '#388bff', '#fce75d', '#ffbab3',
    '#ff5662', '#4fe995', '#d9bfff', '#ffc43d', '#6b8dfe',
    '#fc5bb4', '#26d8ef', '#8ca1ff', '#b5e2e2', '#ffa69e',
    '#d0b0ff', "#2F9323", "#D9B63A", "#2E2AA4", "#9F2E61",
    "#4D670C", "#BF675F", "#1F814A", "#357F88", "#673509",
    "#310937", "#1B9637", "#F7393C"];


var TreecolorList = [
    '#272727', '#2F0000', '#600030', '#460046', '#28004D',
    '#3C3C3C', '#4D0000', '#820041', '#5E005E', '#3A006F',
    '#4F4F4F', '#600000', '#9F0050', '#750075', '#4B0091',
    '#5B5B5B', '#750000', '#BF0060', '#930093', '#5B00AE',
    '#6C6C6C', '#930000', '#D9006C', '#AE00AE', '#6F00D2',
    '#7B7B7B', '#AE0000', '#F00078', '#D200D2', '#8600FF',
    '#8E8E8E', '#CE0000', '#FF0080', '#E800E8', '#921AFF',
    '#9D9D9D', '#EA0000', '#FF359A', '#FF00FF', '#9F35FF',
    '#ADADAD', '#FF0000', '#FF60AF', '#FF44FF', '#B15BFF',
    '#BEBEBE', '#FF2D2D', '#FF79BC', '#FF77FF', '#BE77FF',
    '#d0d0d0', '#FF5151', '#FF95CA', '#FF8EFF', '#CA8EFF',
    '#E0E0E0', '#ff7575', '#ffaad5', '#ffa6ff', '#d3a4ff',
    '#000079', '#FF9797', '#FFC1E0', '#FFBFFF', '#DCB5FF',
    '#000093', '#FFB5B5', '#FFD9EC', '#FFD0FF', '#E6CAFF',
    '#0000C6', '#FFD2D2', '#FFECF5', '#FFE6FF', '#F1E1FF',
    '#0000C6', '#000079', '#003E3E', '#006030', '#006000',
    '#0000E3', '#003D79', '#005757', '#01814A', '#007500',
    '#2828FF', '#004B97', '#007979', '#019858', '#009100',
    '#4A4AFF', '#005AB5', '#009393', '#01B468', '#00A600',
    '#6A6AFF', '#0066CC', '#00AEAE', '#02C874', '#00BB00',
    '#7D7DFF', '#0072E3', '#00CACA', '#02DF82', '#00DB00',
    '#9393FF', '#0080FF', '#00E3E3', '#02F78E', '#00EC00',
    '#AAAAFF', '#2894FF', '#00FFFF', '#1AFD9C', '#28FF28',
    '#B9B9FF', '#46A3FF', '#4DFFFF', '#4EFEB3', '#53FF53',
    '#CECEFF', '#66B3FF', '#80FFFF', '#7AFEC6', '#79FF79',
    '#DDDDFF', '#84C1FF', '#A6FFFF', '#96FED1', '#93FF93',
    '#ECECFF', '#97CBFF', '#BBFFFF', '#ADFEDC', '#A6FFA6',
    '#467500', '#ACD6FF', '#CAFFFF', '#C1FFE4', '#BBFFBB',
    '#548C00', '#C4E1FF', '#D9FFFF', '#D7FFEE', '#CEFFCE',
    '#64A600', '#D2E9FF', '#ECFFFF', '#E8FFF5', '#DFFFDF',
    '#73BF00', '#424200', '#5B4B00', '#844200', '#642100',
    '#82D900', '#5B5B00', '#796400', '#9F5000', '#842B00',
    '#8CEA00', '#737300', '#977C00', '#BB5E00', '#A23400',
    '#9AFF02', '#8C8C00', '#AE8F00', '#D26900', '#BB3D00',
    '#A8FF24', '#A6A600', '#C6A300', '#EA7500', '#D94600',
    '#B7FF4A', '#C4C400', '#D9B300', '#FF8000', '#F75000',
    '#C2FF68', '#E1E100', '#EAC100', '#FF9224', '#FF5809',
    '#CCFF80', '#F9F900', '#FFD306', '#FFA042', '#FF8040',
    '#D3FF93', '#FFFF37', '#FFDC35', '#FFAF60', '#FF8F59',
    '#DEFFAC', '#FFFF6F', '#FFE153', '#FFBB77', '#FF9D6F',
    '#E8FFC4', '#FFFF93', '#FFE66F', '#FFC78E', '#FFAD86',
    '#EFFFD7', '#FFFFAA', '#FFED97', '#FFD1A4', '#FFBD9D',
    '#613030', '#FFFFB9', '#FFF0AC', '#FFDCB9', '#FFCBB3',
    '#743A3A', '#FFFFCE', '#FFF4C1', '#FFE4CA', '#FFDAC8',
    '#804040', '#FFFFDF', '#FFF8D7', '#FFEEDD', '#FFE6D9',
    '#984B4B', '#616130', '#336666', '#484891', '#6C3365',
    '#AD5A5A', '#707038', '#3D7878', '#5151A2', '#7E3D76',
    '#B87070', '#808040', '#408080', '#5A5AAD', '#8F4586',
    '#C48888', '#949449', '#4F9D9D', '#7373B9', '#9F4D95',
    '#CF9E9E', '#A5A552', '#5CADAD', '#8080C0', '#AE57A4',
    '#D9B3B3', '#AFAF61', '#6FB7B7', '#9999CC', '#B766AD',
    '#E1C4C4', '#B9B973', '#81C0C0', '#A6A6D2', '#C07AB8',
    '#EBD6D6', '#C2C287', '#95CACA', '#B8B8DC', '#CA8EC2',
    '#F2E6E6', '#CDCD9A', '#A3D1D1', '#C7C7E2', '#D2A2CC',
    '#EBD3E8', '#D6D6AD', '#B3D9D9', '#D8D8EB', '#DAB1D5',
    '#F3F3FA', '#DEDEBE', '#C4E1E1', '#E6E6F2', '#E2C2DE',
]
////生成不同的颜色代码
//function randomRgbColor() { //随机生成RGB颜色
//    var r = Math.floor(Math.random() * 256); //随机生成256以内r值
//    var g = Math.floor(Math.random() * 256); //随机生成256以内g值
//    var b = Math.floor(Math.random() * 256); //随机生成256以内b值
//    return 'rgb(${r},${g},${b})'; //返回rgb(r,g,b)格式颜色
//}

//function color16() {//十六进制颜色随机
//    var r = Math.floor(Math.random() * 256);
//    var g = Math.floor(Math.random() * 256);
//    var b = Math.floor(Math.random() * 256);
//    var color = '#' + r.toString(16) + g.toString(16) + b.toString(16);
//    return color;
//}
//debugger;
//var colorGroup = createColorCode('#' + Math.floor(Math.random() * 16777215).toString(16), AidercolorList);
////生成不同的颜色代码
//function createColorCode(code, colorArr) {
//    if ($.inArray(code, colorArr) == -1 && code.length > 6) {
//        colorArr[0] = code;
//    } else {
//        code = '#' + Math.floor(Math.random() * 16777215).toString(16);
//        createColorCode(code, colorArr);
//    }
//    return colorArr;
//}

var randomRGBA = randomRgbaColor();
function randomRgbaColor() {//rgb颜色随机
    var r = Math.floor(Math.random() * 256); //随机生成256以内r值
    var g = Math.floor(Math.random() * 256); //随机生成256以内g值
    var b = Math.floor(Math.random() * 256); //随机生成256以内b值
    var alpha = 0.2;//Math.random().toFixed(1); //随机生成1以内a值
    var rgba = 'rgba(' + r + ',' + g + ',' + b + ',' + alpha + ')';
    return rgba;
}
function LineChart(name, value) {
    var myChart = echarts.init(document.getElementById('Line'));
    // 指定图表的配置项和数据

    var option = {
        title: {
            text: '汽车销量统计',
            subtext: '纯属虚构',
            x: 'center'
        },
        tooltip: {},
        xAxis: {
            data: name          //横坐标的值name
        },
        yAxis: {},
        series: [{
            name: '销量',
            type: 'line',
            data: value         //纵坐标的值value
        }]
    };
    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);

}
function RadarChart(name, value, data) {
    var myChart = echarts.init(document.getElementById('Radar'));
    option = {
        title: {
            text: '汽车销量统计(纯属虚构)',
            subtext: '纯属虚构',
            x: 'right',
            y: 'bottom'
        },
        tooltip: {
            trigger: 'item',
            backgroundColor: 'rgba(0,0,250,0.2)'
        },
        legend: {
            data: name
        },
        visualMap: {
            color: ['red', 'yellow']
        },
        radar: {
            indicator: (function () {
                var indicator = [];
                for (var i = 0; i < name.length; i++) {
                    indicator.push({
                        text: name[i],
                        max: 4000       //限制最大数数值
                    })
                }
                return indicator;
            })()
        },
        series: (function () {
            var series = [];
            for (var i = 0; i < data.length; i++) {
                series.push({
                    type: 'radar',
                    symbol: 'none',
                    itemStyle: {
                        normal: {
                            lineStyle: {
                                width: 1
                            }
                        },
                        emphasis: {
                            areaStyle: { color: 'rgba(0,250,0,0.3)' }
                        }
                    },

                    data: [
                        {
                            value: value,
                            name: name[i]
                        }
                    ]
                });
            }
            return series;
        })()
    };
    myChart.setOption(option);
}







