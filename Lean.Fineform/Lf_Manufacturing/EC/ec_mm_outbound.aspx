﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_Mm_outbound.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.EC.ec_Mm_outbound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="UserManage">
            <Items>
                <f:Form ID="Form3" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="Ec_no" Label="设变号码" EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="Ec_no_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>



                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:RadioButtonList ID="rblAuto" Label="<%$ Resources:GlobalResource,sys_Status_mm%>" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="rblAuto_SelectedIndexChanged">
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_Common%>" Value="com" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,co_Dept_MM%>" Value="mm" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,co_Dept_P2D%>" Value="p2d" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_Notoutbound%>" Value="0" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_All%>" Value="all" Selected="true" />
                                </f:RadioButtonList>
                                <f:Button ID="btnChangeOutboundItems" IconUrl="~/Lf_Resources/menu/confirm.png" EnablePostBack="false" runat="server"
                                    Text="重新设置物料管理部门">
                                    <Menu runat="server">
                                        <f:MenuButton ID="btnOutboundmm" IconUrl="~/Lf_Resources/menu/change/dept_mm.png" OnClick="btnOutboundMm_Click" runat="server" Text="选中记录设为部管课管理">
                                        </f:MenuButton>
                                        <f:MenuButton ID="btnOutboundp2d" IconUrl="~/Lf_Resources/menu/change/dept_p2d.png" OnClick="btnOutboundp2d_Click" runat="server" Text="选中记录设为制二课管理">
                                        </f:MenuButton>
                                        <f:MenuButton ID="btnOutboundCommon" IconUrl="~/Lf_Resources/menu/change/dept_all.png" OnClick="btnOutboundCommon_Click" runat="server" Text="选中记录设为共通管理">
                                        </f:MenuButton>
                                        <f:MenuButton ID="btnOutboundnommp2d" IconUrl="~/Lf_Resources/menu/change/dept_no.png" OnClick="btnOutboundnommp2d_Click" runat="server"
                                            Text="选中记录部管制二不管理">
                                        </f:MenuButton>
                                        <f:MenuButton ID="btnNotOutboundItems" IconUrl="~/Lf_Resources/menu/change/dept_no.png" OnClick="btnNotOutboundItems_Click" runat="server"
                                            Text="选中记录不管理">
                                        </f:MenuButton>

                                    </Menu>
                                </f:Button>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>

                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" EnableTextSelection="true"
                    DataKeyNames="GUID,Ec_no,Ec_entrydate" AllowSorting="true" OnSort="Grid1_Sort" SortField="Ec_entrydate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" />
                        <f:BoundField DataField="Ec_no" SortField="Ec_no" Width="120px" HeaderText="设变号码" />
                        <f:BoundField DataField="isConfirm" SortField="isConfirm" Width="120px" HeaderText="管理否" />
                        <%--<f:CheckBoxField DataField="isConfirm" SortField="isConfirm" HeaderText="管理否" RenderAsStaticField="true"
                            Width="80px" />--%>
                        <f:BoundField DataField="Ec_model" SortField="Ec_model" Width="180px" HeaderText="机种名称" />
                        <f:BoundField DataField="Ec_newitem" SortField="Ec_newitem" Width="180px" HeaderText="新物料" />
                        <f:BoundField DataField="Ec_newtext" SortField="Ec_newtext" Width="180px" HeaderText="物料描述" />

                        <f:BoundField DataField="Ec_procurement" SortField="Ec_procurement" Width="120px" HeaderText="采购" />
                        <f:BoundField DataField="Ec_location" SortField="Ec_location" Width="120px" HeaderText="仓库" />
                        <f:BoundField DataField="isCheck" SortField="isCheck" Width="120px" HeaderText="检查" />
                        <f:BoundField DataField="Ec_bomitem" SortField="Ec_bomitem" Width="180px" HeaderText="上阶物料" />
                        <f:BoundField DataField="Ec_bomsubitem" SortField="Ec_bomsubitem" Width="180px" HeaderText="子物料" />
                        <f:BoundField DataField="Ec_olditem" SortField="Ec_olditem" Width="180px" HeaderText="旧物料" />


                        <f:BoundField DataField="Ec_entrydate" SortField="Ec_entrydate" Width="120px" HeaderText="登录日期" />
                        <f:BoundField DataField="Ec_leader" SortField="Ec_leader" Width="120px" HeaderText="技术担当" />
                        <f:BoundField DataField="Ec_distinction" SortField="Ec_distinction" Width="120px" HeaderText="管理区分" />



                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="600px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>

