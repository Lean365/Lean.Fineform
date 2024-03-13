<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="LeanFine.main" %>

<!DOCTYPE html>
<html>
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="res/css/main.css" rel="stylesheet" />
    <title><%= Resources.GlobalResource.sys_LeanName%></title>
    
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="regionPanel" runat="server" />
        <f:Timer ID="Timer1" Interval="60" Enabled="false" OnTick="Timer1_Tick" EnableAjaxLoading="false" runat="server">
        </f:Timer>
        <f:Panel ID="regionPanel" Layout="Region" CssClass="mainpanel" ShowBorder="false" ShowHeader="false" runat="server">
            <Items>
                <f:Panel ID="topPanel" runat="server" Layout="Column" RegionPosition="Top" RegionSplit="false" EnableCollapse="false"
                    Title="" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                    <Content>
                        <div id="header">
                            <img src="./Lf_Resources/images/login/Lean_Icon.png" class="logo" alt="Logo"/>
                            <asp:Label ID="linkSystemTitle" CssClass="title" runat="server" NavigateUrl="~/main.aspx"></asp:Label>
                        </div>
                    </Content>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Bottom" runat="server" Height="25px">
                            <Items>
                               
                                <f:Label ID="txtUName" runat="server" LabelAlign="Left" CssStyle="font-size:12px;color: #000033;"></f:Label>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Label ID="linkSlogantext" runat="server" Width="600px" LabelAlign="Left" CssStyle="font-size:14px;color: #000033;"></f:Label>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                <f:Button runat="server" ID="btnUserName" CssClass="userpicaction" Text="" IconUrl="~/Lf_Resources/images/my_face.jpg" IconAlign="Left" 
                                    CssStyle="font-size:12px;color: #000033;"  EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false">
                                    <Menu runat="server">
                                        <f:MenuButton ID="btnHelp" EnablePostBack="false" Icon="Help" Text="<%$ Resources:GlobalResource,sys_Help%>" runat="server">
                                        </f:MenuButton>
                                        <f:MenuSeparator runat="server">
                                        </f:MenuSeparator>
                                        <f:MenuButton runat="server" IconFont="PowerOff" Text="<%$ Resources:GlobalResource,sys_Exit%>" ConfirmText="<%$ Resources:GlobalResource,sys_ExitConfirmText%>" OnClick="btnExit_Click">
                                        </f:MenuButton>
                                    </Menu>
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>

                <f:Panel ID="leftPanel" CssClass="leftregion bgpanel"
                    EnableCollapse="true" Width="200px" RegionSplit="true" RegionSplitIcon="false" RegionSplitWidth="3px"
                    ShowHeader="false" Title="<%$ Resources:GlobalResource,sys_Menu%>" Layout="Fit" RegionPosition="Left" runat="server">
                </f:Panel>
                <f:TabStrip ID="mainTabStrip" CssClass="centerregion" RegionPosition="Center" ShowInkBar="true" EnableTabCloseMenu="true" ShowBorder="true" runat="server">
                    <Tabs>
                        <f:Tab ID="Tab1" Title="<%$ Resources:GlobalResource,sys_Home%>" EnableIFrame="true" IFrameUrl="~/Lf_Admin/default.aspx"
                            Icon="House" runat="server">
                        </f:Tab>
                    </Tabs>
                    <Tools>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="_Refresh" CssClass="tabtool" ToolTip="<%$ Resources:GlobalResource,sys_Windows_Refresh%>" ID="toolRefresh">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolRefreshClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="_Maximize" CssClass="tabtool" ToolTip="<%$ Resources:GlobalResource,sys_Windows_Maxsize%>" ID="toolMaximize">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolMaximizeClick" />
                            </Listeners>
                        </f:Tool>
                    </Tools>
                </f:TabStrip>
                <f:Panel ID="Panel1" runat="server" Layout="Column" RegionPosition="Bottom" RegionSplit="false" EnableCollapse="false" Height="30px"
                    Title="" ShowBorder="false" ShowHeader="false" BodyPadding="0px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>

                                <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                                <f:Label ID="txtCrght" runat="server" CssStyle="font-size:14px;color: #000033;"></f:Label>

                                <f:ToolbarFill ID="ToolbarFill4" runat="server"></f:ToolbarFill>
                                <f:Label ID="txtDtime" runat="server" CssStyle="font-size:12px;color: #0099FF;"></f:Label>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
            </Items>
        </f:Panel>

        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" EnableIFrame="true"
            EnableResize="true" EnableMaximize="true" IFrameUrl="about:blank" Width="800px"
            Height="500px">
        </f:Window>
    </form>
    <script>

        var mainTabStripClientID = '<%= mainTabStrip.ClientID %>';
        var topPanelClientID = '<%= topPanel.ClientID %>';
        var leftPanelClientID = '<%= leftPanel.ClientID %>';

        // 下载源代码
        function onDownloadClick(event) {
            window.open('http://FineUIPro.com/bbs/forum.php?mod=viewthread&tid=21482', '_blank');
        }

        // 点击标题栏工具图标 - 刷新
        function onToolRefreshClick(event) {
            var mainTabStrip = F(mainTabStripClientID);

            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab.iframe) {
                var iframeWnd = activeTab.getIFrameWindow();
                iframeWnd.location.reload();
            }
        }

        // 点击标题栏工具图标 - 最大化
        function onToolMaximizeClick(event) {
            var topPanel = F(topPanelClientID);
            var leftPanel = F(leftPanelClientID);

            var currentTool = this;
            F.noAnimation(function () {
                if (currentTool.iconFont === 'f-iconfont-maximize') {
                    currentTool.setIconFont('f-iconfont-restore');

                    leftPanel.collapse();
                    topPanel.collapse();
                } else {
                    currentTool.setIconFont('f-iconfont-maximize');

                    leftPanel.expand();
                    topPanel.expand();
                }
            });
        }

        // 添加示例标签页（通过href在树中查找）
        // href: 选项卡对应的网址
        // actived: 是否激活选项卡（默认为true）
        function addExampleTabByHref(href, actived) {
            var leftPanel = F(leftPanelClientID);
            var treeMenu = leftPanel.getItem(0);

            F.addMainTabByHref(F(mainTabStripClientID), treeMenu, href, actived);
        }


        // 添加示例标签页
        // tabOptions: 选项卡参数
        // tabOptions.id： 选项卡ID
        // tabOptions.iframeUrl: 选项卡IFrame地址 
        // tabOptions.title： 选项卡标题
        // tabOptions.icon： 选项卡图标
        // tabOptions.createToolbar： 创建选项卡前的回调函数（接受tabOptions参数）
        // tabOptions.refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
        // tabOptions.iconFont： 选项卡图标字体
        // tabOptions.activeIt： 是否激活当前添加的选项卡
        // actived: 是否激活选项卡（默认为true）
        function addExampleTab(tabOptions, actived) {
            //调试
            //debugger;
            if (typeof (tabOptions) === 'string') {
                tabOptions = {
                    id: arguments[0],
                    iframeUrl: arguments[1],
                    title: arguments[2],
                    icon: arguments[3],
                    createToolbar: arguments[4],
                    refreshWhenExist: arguments[5],
                    iconFont: arguments[6]
                };
            }

            F.addMainTab(F(mainTabStripClientID), tabOptions, actived);
        }


        // 移除选中标签页
        function removeActiveTab() {
            var mainTabStrip = F(mainTabStripClientID);

            var activeTab = mainTabStrip.getActiveTab();
            activeTab.hide();
        }

        // 获取当前激活选项卡的ID
        function getActiveTabId() {
            var mainTabStrip = F(mainTabStripClientID);

            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab) {
                return activeTab.id;
            }
            return '';
        }

        // 激活选项卡，并刷新其中的内容，示例：表格控件->杂项->在新标签页中打开（关闭后刷新父选项卡）
        function activeTabAndRefresh(tabId) {
            var mainTabStrip = F(mainTabStripClientID);
            var targetTab = mainTabStrip.getTab(tabId);
            var oldActiveTabId = getActiveTabId();

            if (targetTab) {
                mainTabStrip.activeTab(targetTab);
                targetTab.refreshIFrame();

                // 删除之前的激活选项卡
                mainTabStrip.removeTab(oldActiveTabId);
            }
        }

        // 激活选项卡，并刷新其中的内容，示例：表格控件->杂项->在新标签页中打开（关闭后更新父选项卡中的表格）
        function activeTabAndUpdate(tabId, param1) {
            var mainTabStrip = F(mainTabStripClientID);
            var targetTab = mainTabStrip.getTab(tabId);
            var oldActiveTabId = getActiveTabId();

            if (targetTab) {
                mainTabStrip.activeTab(targetTab);
                targetTab.getIFrameWindow().updatePage(param1);

                // 删除之前的激活选项卡
                mainTabStrip.removeTab(oldActiveTabId);
            }
        }


        // 移除选中标签页
        function removeActiveTab() {
            var mainTabStrip = F(mainTabStripClientID);

            var activeTab = mainTabStrip.getActiveTab();
            activeTab.hide();
        }


        F.ready(function () {

            var mainTabStrip = F(mainTabStripClientID);
            var leftPanel = F(leftPanelClientID);
            var treeMenu = leftPanel.getItem(0);

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // updateHash: 切换Tab时，是否更新地址栏Hash值（默认值：true）
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame（默认值：false）
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame（默认值：false）
            // maxTabCount: 最大允许打开的选项卡数量
            // maxTabMessage: 超过最大允许打开选项卡数量时的提示信息
            F.initTreeTabStrip(treeMenu, mainTabStrip, {
                maxTabCount: 10,
                maxTabMessage: '请先关闭一些选项卡（最多允许打开 10 个）！'
            });


        });

    </script>
</body>
</html>

