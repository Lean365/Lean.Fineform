<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sap_header.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.dept.sap_header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <style>
        /* 限定[个人简介]列自动换行，其他列不自动换行 */
        .f-grid-cell.f-grid-cell-D_SAP_ZPABD_Z027 .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"  
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="设计变更ECN">
            <Items>
                <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="DPstart" ShowRedStar="True" OnTextChanged="DPstart_TextChanged">
                                </f:DatePicker>
                                <f:DatePicker ID="DPend" Readonly="false" Width="300px" CompareControl="DPstart" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"  ForceFit="true"                  
                    DataKeyNames="D_SAP_ZPABD_Z001" AllowSorting="true" SortField="D_SAP_ZPABD_Z005"  EnableTextSelection="true"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange"
                    EnableMultiSelect="false" EnableRowClickEvent="true" OnRowClick="Grid1_RowClick"
                    OnSort="Grid1_Sort"
                    OnRowDataBound="Grid1_RowDataBound" 
                    OnPreRowDataBound="Grid1_PreRowDataBound">
                    <%--<Toolbars>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>                                
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>

                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false" 
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">  </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>--%>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>" >
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
                        <f:BoundField DataField="D_SAP_ZPABD_Z001" ColumnID="D_SAP_ZPABD_Z001" SortField="D_SAP_ZPABD_Z001"  EnableLock="true" Width="80px" HeaderText="号码" />
                        <f:BoundField DataField="D_SAP_ZPABD_Z002" ColumnID="D_SAP_ZPABD_Z002" SortField="D_SAP_ZPABD_Z002"  EnableLock="true" Width="100px" HeaderText="机种" />
                        <f:BoundField DataField="D_SAP_ZPABD_Z003" ColumnID="D_SAP_ZPABD_Z003" SortField="D_SAP_ZPABD_Z003"  EnableLock="true" Width="100px" HeaderText="标题" />
                        <f:BoundField DataField="D_SAP_ZPABD_Z004" ColumnID="D_SAP_ZPABD_Z004" SortField="D_SAP_ZPABD_Z004"  EnableLock="true" Width="100px" HeaderText="状态" />
                        <f:BoundField DataField="D_SAP_ZPABD_Z005" ColumnID="D_SAP_ZPABD_Z005" SortField="D_SAP_ZPABD_Z005"  EnableLock="true" Width="100px" HeaderText="日期" />
                        <f:BoundField DataField="D_SAP_ZPABD_Z006" ColumnID="D_SAP_ZPABD_Z006" SortField="D_SAP_ZPABD_Z006"  EnableLock="true" Width="100px" HeaderText="担当" />
                        <f:BoundField DataField="D_SAP_ZPABD_Z007" ColumnID="D_SAP_ZPABD_Z007" SortField="D_SAP_ZPABD_Z007"  EnableLock="true" Width="100px" HeaderText="依赖" />
                        <f:BoundField DataField="D_SAP_ZPABD_Z026" ColumnID="D_SAP_ZPABD_Z026" SortField="D_SAP_ZPABD_Z026"  EnableLock="true" Width="100px" HeaderText="图纸" />
                        <%--<f:BoundField DataField="D_SAP_ZPABD_Z027" ColumnID="D_SAP_ZPABD_Z027" SortField="D_SAP_ZPABD_Z027"  EnableLock="true" Width="400px" HeaderText="内容" />--%>


                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
