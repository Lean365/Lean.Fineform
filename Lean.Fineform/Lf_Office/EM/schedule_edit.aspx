<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schedule_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Office.EM.schedule_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Lf_Resources/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Lf_Resources/ueditor/themes/default/css/ueditor.min.css" />
    <script type="text/javascript" src="../Lf_Resources/ueditor/ckeditor.js"></script>
    <script src="/Lf_Resources/js/jquery.min.js" type="text/javascript"></script>
    <script src="/Lf_Resources/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Lf_Resources/spectrum/spectrum.css" />
    <style>
        .mycolor .f-field-body {
            display: inline-block !important;
            width: 150px;
            margin-right: 5px;
        }

        .mycolor .sp-replacer {
            border-width: 0;
            padding: 0;
            position: absolute;
            top: 50%;
            margin-top: -10px;
        }

        .mycolor .sp-dd {
            display: none;
        }

        .mycolor .sp-preview {
            margin-right: 0;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveClose%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label runat="server" ID="attypename" Label="事件类型">
                                </f:Label>
                                <f:TextBox runat="server" ID="attitle" Label="标题">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:TriggerBox ID="atsdate" Required="true" ShowRedStar="True" Label="日期和时间" EmptyText="请选择日期和时间" TriggerIcon="Date"
                                    EnablePostBack="false" runat="server" OnClientTriggerClick="atsdateTriggerClick();">
                                </f:TriggerBox>
                                <f:TriggerBox ID="atedate" Required="true" ShowRedStar="True" Label="日期和时间" EmptyText="请选择日期和时间" TriggerIcon="Date"
                                    EnablePostBack="false" runat="server" OnClientTriggerClick="atedateTriggerClick();">
                                </f:TriggerBox>


                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>

                                <f:TextBox runat="server" ID="atbgcolor" CssClass="mycolor" Text="#DC143C" Required="true" ShowRedStar="true" Label="背景">
                                </f:TextBox>

                                <f:TextBox runat="server" ID="attextcolor" CssClass="mycolor" Text="#FFF8DC" Required="true" ShowRedStar="true" Label="文本颜色">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="atcontent" Label="内容" ShowRedStar="True" Required="True">
                                </f:TextArea>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>

                                <f:TextBox runat="server" ID="aturl" Label="事件URL" ShowRedStar="True">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="atcolor" CssClass="mycolor" Text="#00008B" Required="true" ShowRedStar="true" Label="背景和边框">
                                </f:TextBox>

                                <f:TextBox runat="server" ID="atbdcolor" CssClass="mycolor" Text="#006400" Required="true" ShowRedStar="true" Label="边框颜色">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:TextBox ID="Remark" runat="server" Label="备注">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>


                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>
    <script type="text/javascript">
        var sid ='<%=atsdate.ClientID%>';
        // Extjs v6.2不再支持 tbxMyBox.onTriggerClick 的写法，请使用 OnClientTriggerClick
        function atsdateTriggerClick() {

            var tbxMyBox = F(sid);

            WdatePicker(
                {
                    el: sid + '-inputEl',
                    dateFmt: 'yyyy-MM-dd HH:mm:ss',
                    onpicked: function () {
                        // 确认选择后，执行触发器输入框的客户端验证
                        tbxMyBox.validate();
                    }
                });
        }
        var eid = '<%=atedate.ClientID%>';
        // Extjs v6.2不再支持 tbxMyBox.onTriggerClick 的写法，请使用 OnClientTriggerClick
        function atedateTriggerClick() {

            var tbxMyBox = F(eid);

            WdatePicker(
                {
                    el: eid + '-inputEl',
                    dateFmt: 'yyyy-MM-dd HH:mm:ss',
                    onpicked: function () {
                        // 确认选择后，执行触发器输入框的客户端验证
                        tbxMyBox.validate();
                    }
                });
        }

    </script>
    <script src="/Lf_Resources/spectrum/spectrum.js" type="text/javascript"></script>
    <script src="/Lf_Resources/spectrum/i18n/jquery.spectrum-zh-cn.js" type="text/javascript"></script>
    <script type="text/javascript">
        var attextcolorClientID = '<%= attextcolor.ClientID %>';

        F.ready(function () {

            var tbxMyBox = F(attextcolorClientID);

            tbxMyBox.bodyEl.spectrum({
                preferredFormat: "hex3",
                showInput: true,
                showPalette: true,
                palette: [
                    ["#000", "#444", "#666", "#999", "#ccc", "#eee", "#f3f3f3", "#fff"],
                    ["#f00", "#f90", "#ff0", "#0f0", "#0ff", "#00f", "#90f", "#f0f"],
                    ["#f4cccc", "#fce5cd", "#fff2cc", "#d9ead3", "#d0e0e3", "#cfe2f3", "#d9d2e9", "#ead1dc"],
                    ["#ea9999", "#f9cb9c", "#ffe599", "#b6d7a8", "#a2c4c9", "#9fc5e8", "#b4a7d6", "#d5a6bd"],
                    ["#e06666", "#f6b26b", "#ffd966", "#93c47d", "#76a5af", "#6fa8dc", "#8e7cc3", "#c27ba0"],
                    ["#c00", "#e69138", "#f1c232", "#6aa84f", "#45818e", "#3d85c6", "#674ea7", "#a64d79"],
                    ["#900", "#b45f06", "#bf9000", "#38761d", "#134f5c", "#0b5394", "#351c75", "#741b47"],
                    ["#600", "#783f04", "#7f6000", "#274e13", "#0c343d", "#073763", "#20124d", "#4c1130"]
                ]
            });


        });
    </script>
    <script type="text/javascript">
        var atbgcolorClientID = '<%= atbgcolor.ClientID %>';

        F.ready(function () {

            var tbxMyBox = F(atbgcolorClientID);

            tbxMyBox.bodyEl.spectrum({
                preferredFormat: "hex3",
                showInput: true,
                showPalette: true,
                palette: [
                    ["#000", "#444", "#666", "#999", "#ccc", "#eee", "#f3f3f3", "#fff"],
                    ["#f00", "#f90", "#ff0", "#0f0", "#0ff", "#00f", "#90f", "#f0f"],
                    ["#f4cccc", "#fce5cd", "#fff2cc", "#d9ead3", "#d0e0e3", "#cfe2f3", "#d9d2e9", "#ead1dc"],
                    ["#ea9999", "#f9cb9c", "#ffe599", "#b6d7a8", "#a2c4c9", "#9fc5e8", "#b4a7d6", "#d5a6bd"],
                    ["#e06666", "#f6b26b", "#ffd966", "#93c47d", "#76a5af", "#6fa8dc", "#8e7cc3", "#c27ba0"],
                    ["#c00", "#e69138", "#f1c232", "#6aa84f", "#45818e", "#3d85c6", "#674ea7", "#a64d79"],
                    ["#900", "#b45f06", "#bf9000", "#38761d", "#134f5c", "#0b5394", "#351c75", "#741b47"],
                    ["#600", "#783f04", "#7f6000", "#274e13", "#0c343d", "#073763", "#20124d", "#4c1130"]
                ]
            });


        });
    </script>
    <script type="text/javascript">
        var atcolorClientID = '<%= atcolor.ClientID %>';

        F.ready(function () {

            var tbxMyBox = F(atcolorClientID);

            tbxMyBox.bodyEl.spectrum({
                preferredFormat: "hex3",
                showInput: true,
                showPalette: true,
                palette: [
                    ["#000", "#444", "#666", "#999", "#ccc", "#eee", "#f3f3f3", "#fff"],
                    ["#f00", "#f90", "#ff0", "#0f0", "#0ff", "#00f", "#90f", "#f0f"],
                    ["#f4cccc", "#fce5cd", "#fff2cc", "#d9ead3", "#d0e0e3", "#cfe2f3", "#d9d2e9", "#ead1dc"],
                    ["#ea9999", "#f9cb9c", "#ffe599", "#b6d7a8", "#a2c4c9", "#9fc5e8", "#b4a7d6", "#d5a6bd"],
                    ["#e06666", "#f6b26b", "#ffd966", "#93c47d", "#76a5af", "#6fa8dc", "#8e7cc3", "#c27ba0"],
                    ["#c00", "#e69138", "#f1c232", "#6aa84f", "#45818e", "#3d85c6", "#674ea7", "#a64d79"],
                    ["#900", "#b45f06", "#bf9000", "#38761d", "#134f5c", "#0b5394", "#351c75", "#741b47"],
                    ["#600", "#783f04", "#7f6000", "#274e13", "#0c343d", "#073763", "#20124d", "#4c1130"]
                ]
            });


        });
    </script>
    <script type="text/javascript">
        var atbdcolorClientID = '<%= atbdcolor.ClientID %>';

        F.ready(function () {

            var tbxMyBox = F(atbdcolorClientID);

            tbxMyBox.bodyEl.spectrum({
                preferredFormat: "hex3",
                showInput: true,
                showPalette: true,
                palette: [
                    ["#000", "#444", "#666", "#999", "#ccc", "#eee", "#f3f3f3", "#fff"],
                    ["#f00", "#f90", "#ff0", "#0f0", "#0ff", "#00f", "#90f", "#f0f"],
                    ["#f4cccc", "#fce5cd", "#fff2cc", "#d9ead3", "#d0e0e3", "#cfe2f3", "#d9d2e9", "#ead1dc"],
                    ["#ea9999", "#f9cb9c", "#ffe599", "#b6d7a8", "#a2c4c9", "#9fc5e8", "#b4a7d6", "#d5a6bd"],
                    ["#e06666", "#f6b26b", "#ffd966", "#93c47d", "#76a5af", "#6fa8dc", "#8e7cc3", "#c27ba0"],
                    ["#c00", "#e69138", "#f1c232", "#6aa84f", "#45818e", "#3d85c6", "#674ea7", "#a64d79"],
                    ["#900", "#b45f06", "#bf9000", "#38761d", "#134f5c", "#0b5394", "#351c75", "#741b47"],
                    ["#600", "#783f04", "#7f6000", "#274e13", "#0c343d", "#073763", "#20124d", "#4c1130"]
                ]
            });


        });
    </script>
</body>
</html>

