<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_power_batch.aspx.cs" Inherits="LeanFine.Lf_Admin.role_power_batch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" Layout="Region" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            runat="server">
            <Items>

                <f:Panel BodyPadding="5px" ShowBorder="false" Layout="Region"
                    ShowHeader="false" runat="server">
                    <Items>

                        <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                            RegionPercent="30%" Title="角色用户ID" ShowBorder="true" ShowHeader="false"
                            BodyPadding="10px" IconFont="_PullLeft">
                            <Items>
                                <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea runat="server" ID="TextArea1"
                                            Height="530px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%">
                                        </f:TextArea>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center"
                            Title="将源角色权限复制到目标角色批量更新" ShowBorder="true" ShowHeader="false" BodyPadding="10px" IconFont="_RoundPlus">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>

                                        <f:ToolbarFill ID="ToolbarFill4" runat="server"></f:ToolbarFill>
                                        <f:Button ID="BtnBatch" IconUrl="~/Lf_Resources/icon/add.png" EnableAjax="false" DisableControlBeforePostBack="false" RegionPosition="Center" RegionSplit="true" RegionSplitWidth="100%"
                                            runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Update%>" OnClick="BtnBatch_Click" CssClass="marginr">
                                        </f:Button>
                                        <f:ToolbarFill ID="ToolbarFill5" runat="server"></f:ToolbarFill>

                                    </Items>
                                </f:Toolbar>

                            </Toolbars>
                            <Items>
                                <f:SimpleForm ID="SimpleForm3" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel runat="server" ID="panelRightRegion" RegionPosition="Right" RegionSplit="true" EnableCollapse="true"
                            RegionPercent="30%" Title="权限ID" ShowBorder="true" ShowHeader="false" BodyPadding="10px" IconFont="_PullRight">
                            <Items>
                                <f:SimpleForm ID="SimpleForm2" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea runat="server" ID="TextArea2"
                                            Height="530px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%">
                                        </f:TextArea>

                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>


                    </Items>
                </f:Panel>

            </Items>
        </f:Panel>
    </form>
</body>
</html>
