<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schedule_full.aspx.cs" Inherits="Lean.Fineform.Lf_Office.EM.schedule_full" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type='text/css'>
        body {
            margin-top: 40px;
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
        }

        #calendar {
            width: 900px;
            margin: 0 auto;
        }
        /* css for timepicker */
        .ui-timepicker-div dl {
            text-align: left;
        }

            .ui-timepicker-div dl dt {
                height: 25px;
            }

            .ui-timepicker-div dl dd {
                margin: -25px 0 10px 65px;
            }

        .style1 {
            width: 100%;
        }

        /* table fields alignment*/
        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 10px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 10px;
        }

        .textarea {
            border: 1px solid #bbb;
            width: 550px;
            height: 80px;
        }

        em {
            color: red;
        }

        strong {
            color: red;
        }
    </style>
    <link href="/Lf_Resources//fullcalendar/lib/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="/Lf_Resources//fullcalendar/lib/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="/Lf_Resources/fullcalendar/demos/css/mainstructure.css" rel="stylesheet" type="text/css" />
    <link href="/Lf_Resources/fullcalendar/demos/css/maincontent.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' type='text/css' href='/Lf_Resources/fullcalendar/demos/cupertino/theme.css' />
    <%--    <link rel='stylesheet' type='text/css' href='/res//fullcalendar/fullcalendar.min.css' />--%>
    <link rel='stylesheet' type='text/css' href='/Lf_Resources//fullcalendar/fullcalendar.css' />
    <link rel='stylesheet' type='text/css' href='/Lf_Resources//fullcalendar/fullcalendar.print.css' media='print' />

    <script src="/Lf_Resources//fullcalendar/lib/moment.min.js" type="text/javascript"></script>
    <script src="/Lf_Resources//fullcalendar/lib/jquery.min.js" type="text/javascript"></script>
    <script src="/Lf_Resources//fullcalendar/lib/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Lf_Resources//fullcalendar/lib/jquery.text.js" type="text/javascript"></script>
    <script src="/Lf_Resources//fullcalendar/lib/jquery.length.js" type="text/javascript"></script>

    <script src="/Lf_Resources/fullcalendar/dist/jquery-ui-timepicker-addon.js"></script>
    <script src="/Lf_Resources/fullcalendar/dist/jquery-ui-timepicker-addon.min.js"></script>
    <script src="/Lf_Resources/fullcalendar/dist/jquery-ui-sliderAccess.js"></script>
    <%--    <script src="/res/fullcalendar/lib/datepicker-zh.js" type="text/javascript"></script>--%>
    <link href="/Lf_Resources/fullcalendar/dist/jquery-ui-timepicker-addon.css" rel="stylesheet" />

    <%--    <script src="/res//fullcalendar/fullcalendar.min.js" type="text/javascript"></script>--%>
    <script src='/Lf_Resources/fullcalendar/fullcalendar.js' type="text/javascript"></script>
    <script src='/Lf_Resources/fullcalendar/locale-all.js'></script>




    <script type="text/javascript">

        var type = navigator.appName;
        if (type == "Netscape") {
            var ilang = navigator.language.toLowerCase();//获取浏览器配置语言，支持非IE浏览器
        } else {
            var ilang = navigator.userLanguage.toLowerCase();//获取浏览器配置语言，支持IE5+ == navigator.systemLanguage
        };

        if (ilang.indexOf("cn")) {
            var ilang = "zh-cn"

        }
        else if (ilang.indexOf("ja")) {
            var ilang = "ja"
        }

        else if (ilang.indexOf("tw")) {
            var ilang = "zh-tw"
        }
        else {
            var ilang = "en-gb"
        }

        $(function () {

            $(".remindAuto").textRemindAuto();
            $(".borderChange").textRemindAuto({ chgClass: "border" });
            $("#textColorChg").textRemindAuto({
                focusColor: "red"
            });

        });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#attitle").keydown(function () {
                var max = 20;
                var tianzi = max - $("#attitle").val().length;
                $("em").text(tianzi);
                if ($("em").text() <= 0) {
                    $("em").text("0");
                    var val = $("#attitle").val().substring(0, 20);
                    $("#attitle").val(val);
                }
            });
            $("#details").keydown(function () {
                var max = 200;
                var tianzi = max - $("#details").val().length;
                $("strong").text(tianzi);
                if ($("strong").text() <= 0) {
                    $("strong").text("0");
                    var val = $("#details").val().substring(0, 200);
                    $("#details").val(val);
                }
            });
            var startDateTextBox = $('#atsdate');
            var endDateTextBox = $('#atedate');
            //开始时间
            startDateTextBox.datetimepicker({

                dateFormat: 'yy-mm-dd', timeFormat: 'HH:mm', hourMin: 6, hourMax: 23, hourGrid: 3, minuteGrid: 10, timeText: 'Time', hourText: 'Hour', minuteText: 'Minute', timeOnlyTitle: 'Time Change',
                onClose: function (dateText, inst) {
                    if (endDateTextBox.val() != '') {
                        var testStartDate = startDateTextBox.datetimepicker('getDate');
                        var testEndDate = endDateTextBox.datetimepicker('getDate');
                        if (testStartDate > testEndDate)
                            endDateTextBox.datetimepicker('setDate', testStartDate);
                    }
                    else {
                        endDateTextBox.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    endDateTextBox.datetimepicker('option', 'minDate', startDateTextBox.datetimepicker('getDate'));
                }
            });
            //结束时间
            endDateTextBox.datetimepicker({
                dateFormat: 'yy-mm-dd', timeFormat: 'HH:mm', hourMin: 8, hourMax: 23, hourGrid: 3, minuteGrid: 10, timeText: 'Time', hourText: 'Hour', minuteText: 'Minute', timeOnlyTitle: 'Time Change',
                onClose: function (dateText, inst) {

                    if (startDateTextBox.val() != '') {
                        var testStartDate = startDateTextBox.datetimepicker('getDate');
                        var testEndDate = endDateTextBox.datetimepicker('getDate');
                        if (testStartDate > testEndDate)
                            startDateTextBox.datetimepicker('setDate', testEndDate);
                    }
                    else {
                        startDateTextBox.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    startDateTextBox.datetimepicker('option', 'maxDate', endDateTextBox.datetimepicker('getDate'));
                }
            });
            //$("#atsdate").datetimepicker(),
            //$("#atedate").datetimepicker(),
            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            $("#addhelper").hide();

            $('#calendar').fullCalendar({
                locale: ilang,//依据判断的语言显示,ilang是IE判断后返回的值，返回值可以出现"zh-cn"或"zh-CN"或"zh"等不同的语言可以做一个判断，自自解决
                //theme: true,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay,listWeek'
                },
                //通过JSON动态传送事件显示到日历,试了好多方法，这个显示最好
                eventSources:
                    [ // 数据源
                        {
                            url: '/Lf_Office/EM/event_view.ashx',
                            type: 'POST',
                        }
                        // 可以追加其它数据源如有的话...
                    ],
                weekNumbers: true,     //是否在视图左边显示这是第多少周，默认false  
                navLinks: true, // “xx周”是否可以被点击，默认false，如果为true则周视图“周几”被点击之后进入日视图
                editable: true,////支持Event日程拖动修改，默认false 
                eventLimit: true, // 事件过多显示More字样
                selectOverlap: false,//确定用户是否被允许选择被事件占用的时间段，默认true可占用时间段  
                //日期点击后弹出的jq ui的框，添加日程记录
                dayClick: function (date, allDay, jsEvent, view) {

                    var selectdate = date.format("YYYY-MM-DD HH:mm");//选择当前日期的时间转换
                    $("#atsdate").datetimepicker('setDate', selectdate);//给时间空间赋值
                    $("#atedate").datetimepicker('setDate', selectdate);//给时间空间赋值
                    //开始日期：除当前选择日期外，其它日期禁用
                    $("#atsdate").datetimepicker('option', 'minDate', selectdate);//给时间空间赋值
                    //开始日期为当前日期时，其它日期禁用
                    $("#atsdate").datetimepicker('option', 'maxDate', selectdate);//给时间空间赋值

                    //结束日期：选择日期之前的全部禁用，确保结束日期大于等于开始日期
                    $('#atedate').datetimepicker('option', 'minDate', selectdate);//给时间空间赋值

                    $("#reservebox").dialog(
                        {
                            autoOpen: false,
                            height: 500,
                            width: 400,
                            title: '事件新增(Event New)' + selectdate,
                            modal: true,
                            //position: "center",
                            draggable: false,
                            beforeClose: function (event, ui) {
                                //$.validationEngine.closePrompt("#meeting");
                                //$.validationEngine.closePrompt("#start");
                                //$.validationEngine.closePrompt("#end");
                            },
                            timeFormat: 'HH:mm{ - HH:mm}',//24小时制
                            buttons:
                            {
                                "关闭(close)": function () {
                                    $(this).dialog("close");
                                },
                                "新增(New)": function () {
                                    var flag = true;
                                    //返回FullCalendar已经存储到客户端的CalEvents对象数组
                                    var events = $('#calendar').fullCalendar('clientEvents', function (event) {
                                        var eventStart = event.start.format('YYYY-MM-DD HH:mm');
                                        var eventEnd = event.end.format('YYYY-MM-DD HH:mm') ? event.end.format('YYYY-MM-DD HH:mm') : null;
                                        var theDate = $("#atsdate").val();
                                        // Make sure the event starts on or before date and ends afterward
                                        // Events that have no end date specified (null) end that day, so check if start = date
                                        return (eventStart <= theDate && (eventEnd >= theDate) && !(eventStart < theDate && (eventEnd == theDate))) || (eventStart == theDate && (eventEnd === null));
                                    });
                                    if (events.length != 0) {
                                        //for (i in events) {

                                        //    var currentStart = events[i].start.format('YYYY-MM-DD HH:mm');
                                        //    if (events[i].end == null) {
                                        //        var currentEnd = currentStart + defaultEventMinutes * 60 * 1000;
                                        //    } else {
                                        //        var currentEnd = events[i].end.format('YYYY-MM-DD HH:mm');
                                        //    }
                                        //    /** 
                                        //     * 对应的事件的起始时间>当前事件的结束时间 
                                        //     */
                                        //    //if(!(array[i].start >= event.end || array[i].end <= event.start  )){  

                                        //    if ($("#atsdate").val() > currentStart && $("#atsdate").val() < currentEnd) {//  
                                        //        console.log('开始时间在其他日程之间');
                                        //        flag = true;
                                        //    }
                                        //    if ($("#atedate").val() > currentStart && $("#atedate").val() < currentEnd) {//  
                                        //        console.log('结束时间在其他日程之间');
                                        //        flag = true;
                                        //    }
                                        //    if ($("#atsdate").val() == currentStart || $("#atedate").val() == currentEnd) {
                                        //        console.log('//开始时间或者结束时间等于别人的时间');
                                        //        flag = true;
                                        //    }
                                        //    if ($("#atsdate").val() < currentStart && $("#atedate").val() > currentStart) {
                                        //        console.log('其他日程在当前的日期中间');
                                        //        flag = true;
                                        //    }
                                        //}
                                        flag = true;
                                    }
                                    else {
                                        flag = false;
                                    }

                                    if (flag) {
                                        $("#atsdate").css("color", "blue");
                                        $("#atedate").css("color", "blue");
                                        //$("#atsdate").css("font-color", "red");
                                        $('#atsdate').not(this).css('border', '1px solid red');
                                        $('#atedate').not(this).css('border', '1px solid red');
                                        //$("#atsdate").css("background-color", "red");
                                        //$("#atedate").css("background-color", "red");
                                        alert("时间有冲突，请重新录入起始时间" + $("#atsdate").val());
                                        return;
                                    }
                                    else {
                                        //debugger;
                                        //判断输入数据为空，不新增
                                        if ($("#atsdate").val() != '') {
                                            if ($("#atedate").val() != '') {
                                                if ($("#attitle").val() != '') {
                                                    if ($("#details").val() != '') {
                                                        var startdatestr = $("#atsdate").val(); //开始时间
                                                        var enddatestr = $("#atedate").val(); //结束时间
                                                        var attitle = $("#attitle").val(); //标题
                                                        var content = $("#details").val(); //内容 
                                                        var attype = $("#attype").val(); //事件类别 

                                                        var id2;
                                                        var startdate = $.fullCalendar.moment(startdatestr);//时间和日期拼接
                                                        var enddate = $.fullCalendar.moment(enddatestr);
                                                        var schdata = { title: attitle, description: content, attype: attype, start: startdatestr, end: enddatestr };
                                                        //debugger;
                                                        $.ajax(
                                                            {
                                                                type: "POST", //使用post方法访问后台
                                                                url: "/Lf_Office/EM/event_new.ashx", //要访问的后台地址
                                                                data: schdata, //要发送的数据
                                                                success: function (data) {
                                                                    //对话框里面的数据提交完成，data为操作结果
                                                                    id2 = data;
                                                                    var schdata2 = { title: attitle, description: content, attype: attype, start: startdatestr, end: enddatestr, id: id2 };
                                                                    $('#calendar').fullCalendar('renderEvent', schdata2, true);
                                                                    $("#atsdate").val(''); //开始时间
                                                                    $("#atedate").val(''); //结束时间
                                                                    $("#attitle").val(''); //标题
                                                                    $("#details").val(''); //内容 
                                                                    // $("#attype").val(''); //事件类别 
                                                                }
                                                            }
                                                        );
                                                    }
                                                }
                                            }
                                        }
                                        $(this).dialog("close");
                                    }
                                }
                            }
                        });
                    $("#reservebox").dialog("open");
                    return false;
                },
                //titleFormat: "yyyyMMMMdddd",
                loading: function (bool) {
                    if (bool) $('#loading').show();
                    else $('#loading').hide();
                },
                eventAfterRender: function (event, element, view) {//数据绑定上去后添加相应信息在页面上
                    var fstart = $.fullCalendar.moment(event.start).format("HH:mm");;
                    var fend = $.fullCalendar.moment(event.end).format("HH:mm");;
                    //element.html('<a href=#><div>Time: ' + fstart + "-" +  fend + '</div><div>Room:' + event.confname + '</div><div style=color:#E5E5E5>Host:' +  event.fullname + "</div></a>");
                    var confbg = '';
                    if (event.attype == "1") {
                        confbg = confbg + '<span class="fc-event-bg"></span>';
                    }
                    else if (event.attype == "2") {
                        confbg = confbg + '<span class="fc-event-bg"></span>';
                    }
                    //} else if (event.attitle == 3) {
                    //    confbg = confbg + '<span class="fc-event-bg"></span>';
                    //} else if (event.attitle == 4) {
                    //    confbg = confbg + '<span class="fc-event-bg"></span>';
                    //} else if (event.attitle == 5) {
                    //    confbg = confbg + '<span class="fc-event-bg"></span>';
                    //} else if (event.attitle == 6) {
                    //    confbg = confbg + '<span class="fc-event-bg"></span>';
                    //} else {
                    //    confbg = confbg + '<span class="fc-event-bg"></span>';
                    //}
                    //  var titlebg = '<span class="fc-event-conf" style="background:' + event.confcolor + '">' + event.confshortname + '</span>';
                    //                if (event.repweeks > 0) {
                    //                    titlebg = titlebg + '<span class="fc-event-conf" style="background:#fff;top:0;right:15;color:#3974BC;font-weight:bold">R</span>';
                    //                }
                    if (view.name == "month") {//按月份
                        var evtcontent = '<div class="fc-event-vert"><a>';
                        evtcontent = evtcontent + confbg;
                        evtcontent = evtcontent + '<span class="fc-event-titlebg">' + fstart + " - " + fend + event.title + '</span>';
                        //                    evtcontent = evtcontent + '<span>Room: ' + event.confname + '</span>';
                        //                    evtcontent = evtcontent + '<span>Host: ' + event.fullname + '</span>';
                        //  evtcontent = evtcontent + '</a><div class="ui-resizable-handle ui-resizable-e"></div></div>';
                        element.html(evtcontent);
                    } else if (view.name == "agendaWeek") {//按周
                        var evtcontent = '<a>';
                        evtcontent = evtcontent + confbg;
                        evtcontent = evtcontent + '<span class="fc-event-time">' + fstart + "-" + fend + '</span>';
                        evtcontent = evtcontent + '<span>' + event.title + '</span>';
                        //evtcontent = evtcontent + '<span>' +  event.fullname + '</span>';
                        //  evtcontent = evtcontent + '</a><span class="ui-icon ui-icon-arrowthick-2-n-s"><div class="ui-resizable-handle ui-resizable-s"></div></span>';
                        element.html(evtcontent);
                    } else if (view.name == "agendaDay") {//按日
                        var evtcontent = '<a>';
                        evtcontent = evtcontent + confbg;
                        evtcontent = evtcontent + '<span class="fc-event-time">' + fstart + " - " + fend + '</span>';
                        //              evtcontent = evtcontent + '<span>Room: ' + event.confname + '</span>';
                        //                evtcontent = evtcontent + '<span>Host: ' + event.fullname + '</span>';
                        //                    evtcontent = evtcontent + '<span>Topic: ' + event.topic + '</span>';
                        // evtcontent = evtcontent + '</a><span class="ui-icon ui-icon-arrow-2-n-s"><div class="ui-resizable-handle ui-resizable-s"></div></span>';
                        element.html(evtcontent);
                    }
                },
                timeFormat: 'H(:mm)',
                eventMouseover: function (calEvent, jsEvent, view) {
                    var fstart = $.fullCalendar.moment(calEvent.start).format("YYYY-MM-DD HH:mm");
                    var fend = $.fullCalendar.moment(calEvent.end).format("YYYY-MM-DD HH:mm");;
                    $(this).attr('title', fstart + " - " + fend + " " + "标题(Title)" + " : " + calEvent.title);
                    $(this).css('font-weight', 'normal');
                    $(this).tooltip({
                        effect: 'toggle',
                        cancelDefault: true
                    });
                },
                eventClick: function (event) {
                    var fstart = $.fullCalendar.moment(event.start).format("YYYY-MM-DD HH:mm");
                    var fend = $.fullCalendar.moment(event.end).format("YYYY-MM-DD HH:mm");
                    //  var schdata = { sid: event.sid, deleted: 1, uid: event.uid };
                    var selectdate = $.fullCalendar.moment(event.start);
                    $("#atsdate").val(fstart);
                    $("#atedate").val(fend);
                    //$("#atedate").datetimepicker('setDate', event.end);
                    $("#attitle").val(event.title); //标题
                    $("#details").val(event.description); //内容 
                    $("#attype").val(event.attype); //事件类别 
                    $("#reservebox").dialog({
                        autoOpen: false,
                        height: 450,
                        width: 400,
                        title: '事件编辑(Event Editor)',
                        modal: true,
                        //position: "center",
                        draggable: false,
                        beforeClose: function (event, ui) {
                            //$.validationEngine.closePrompt("#meeting");
                            //$.validationEngine.closePrompt("#start");
                            //$.validationEngine.closePrompt("#end");
                            $("#atsdate").val(''); //开始时间
                            $("#atedate").val(''); //结束时间
                            $("#attitle").val(''); //标题
                            $("#details").val(''); //内容 
                            $("#attype").val(''); //事件类别 
                        },
                        timeFormat: 'HH:mm{ - HH:mm}',
                        buttons: {
                            "删除(Del)": function () {
                                var aa = window.confirm("警告：确定要删除记录，删除后无法恢复！");
                                if (aa) {
                                    var para = { id: event.id };
                                    $.ajax({
                                        type: "POST", //使用post方法访问后台
                                        url: "/Lf_Office/EM/event_del.ashx", //要访问的后台地址
                                        data: para, //要发送的数据
                                        success: function (data) {
                                            //对话框里面的数据提交完成，data为操作结果
                                            $('#calendar').fullCalendar('removeEvents', event.id);
                                        }
                                    });
                                }
                                $(this).dialog("close");
                            },
                            "编辑(Edit)": function () {
                                var startdatestr = $("#atsdate").val(); //开始时间
                                var enddatestr = $("#atedate").val(); //结束时间
                                var attitle = $("#attitle").val(); //标题
                                var content = $("#details").val(); //内容 
                                var attype = $("#attype").val(); //事件类别 
                                var startdate = $.fullCalendar.moment(startdatestr).format("YYYY-MM-DD HH:mm");
                                var enddate = $.fullCalendar.moment.parseZone(enddatestr).format("YYYY-MM-DD HH:mm");
                                event.title = attitle;
                                event.attype = attitle;
                                event.start = startdate;
                                event.end = enddate;
                                event.description = content;
                                var id2;
                                var schdata = { title: attitle, description: content, attype: attype, start: startdatestr, end: enddatestr, id: event.id };
                                $.ajax({
                                    type: "POST", //使用post方法访问后台
                                    url: "/Lf_Office/EM/event_edit.ashx", //要访问的后台地址
                                    data: schdata, //要发送的数据
                                    success: function (data) {
                                        //对话框里面的数据提交完成，data为操作结果
                                        var schdata2 = { title: attitle, description: content, attype: attype, start: startdatestr, end: enddatestr, id: event.id };
                                        $('#calendar').fullCalendar('updateEvent', event);
                                    }
                                });
                                $(this).dialog("close");
                            }
                        }
                    });
                    $("#reservebox").dialog("open");
                    return false;
                },
                //            events: "../../sr/AccessDate.ashx"
                events: []
            });

            $("#selecteddate").datepicker({
                dateFormat: 'yy-mm-dd',
                beforeShow: function (input, instant) {
                    setTimeout(
                        function () {
                            $('#ui-datepicker-div').css("z-index", 15);
                        }, 100
                    );
                }
            });
            //$("#selectdate").click(function () {
            //    var selectdstr = $("#selecteddate").val();
            //    var selectdate = $.fullCalendar.parseDate(selectdstr, "yyyy-mm-dd");
            //    $('#calendar').fullCalendar('gotoDate', selectdate.getFullYear(), selectdate.getMonth(), selectdate.getDate());
            //});
            // conference function
            // $("#calendar .fc-header-left table td:eq(0)").before('<td><div class="ui-state-default ui-corner-left ui-corner-right" id="selectmeeting"><a><span id="selectdate" class="ui-icon ui-icon-search" style="float: left;padding-left: 5px; padding-top:1px"></span>meeting room</a></div></td><td><span class="fc-header-space"></span></td>');
        });
    </script>
</head>
<body>
    <div id="wrap">
        <div id='calendar'>
        </div>
        <div id="reserveinfo" title="Details">
            <div id="revtitle">
            </div>
            <div id="revdesc">
            </div>
        </div>
        <div class="box" style="display: none" id="reservebox" title="Event">
            <form id="reserveformID" method="post">
                <div class="sysdesc">
                    &nbsp;
                </div>
                <div class="rowElem">
                    <label>
                        事件类型(EventType):</label>
                    <select name="start" id="attype" required="required" style="width: 200px">
                        <option value="1" selected="selected">公开日程</option>
                        <option value="2">个人日程</option>
                    </select>
                </div>
                <div class="rowElem">
                    <label>
                        事件标题(EventTitle):</label>
                    <input class="remindAuto borderChange" id="attitle" name="attitle" value="请输入4到20个字符" required="required" size="20" style="width: 200px" />
                    <p style="text-align: left; font-size: 16px;"><em>20</em></p>
                </div>
                <div class="rowElem">
                    <label>
                        开始时间(StartTime):</label>
                    <input class="remindAuto borderChange" id="atsdate" name="atsdate" required="required" style="width: 200px" />
                </div>
                <div class="rowElem">
                    <label>
                        结束时间(EndTime):</label>
                    <input class="remindAuto borderChange" id="atedate" name="atedate" required="required" style="width: 200px" />
                </div>
                <div class="rowElem">
                    <label>
                        事件内容(EventDetails):</label>
                    <textarea class="textarea remindAuto borderChange" id="details" rows="4" cols="50" name="details" required="required" maxlength="200" style="width: 350px">请输入6到200字符</textarea>
                    <p style="text-align: center; font-size: 16px;"><strong>200</strong></p>
                </div>

                <div id="addhelper" class="ui-widget">
                    <div style="padding-bottom: 5px; padding-left: 5px; padding-right: 5px; padding-top: 5px"
                        class="ui-state-error ui-corner-all">
                        <div id="addresult">
                        </div>
                    </div>
                </div>
            </form>
        </div>

    </div>


</body>
</html>
