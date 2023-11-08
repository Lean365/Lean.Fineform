<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lot_process.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.PP.tracking.lot_process" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" EnableFStateValidation="false" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="生产日报">
            <Items>
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DropDownList ID="DDLModel" Label="<%$ Resources:GlobalResource,Query_Model%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLModel_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" 
                    EnableCheckBoxSelect="true" ForceFit="true" EnableTextSelection="true"
                    DataKeyNames="Pro_Item" AllowSorting="true" OnSort="Grid1_Sort" SortField="Pro_Item,Pro_Process"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnList" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnList_Click" CssClass="marginr">
                                </f:Button>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" runat="server" Icon="TabAdd" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                            <f:ListItem Text="5000" Value="5000" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>

                        <f:BoundField DataField="Pro_Item" SortField="Pro_Item" Width="80px" HeaderText="物料" />
                        
                        <f:BoundField DataField="Pro_Region" SortField="Pro_Region" Width="100px" HeaderText="仕向" />
                        <f:BoundField DataField="Pro_Model" SortField="Pro_Model" Width="100px" HeaderText="机种" />
                        <f:BoundField DataField="Pro_Manhour" SortField="Pro_Manhour" Width="80px" HeaderText="工时" />
                        <f:BoundField DataField="Pro_Process" SortField="Pro_Process" Width="80px" HeaderText="处理工位" />
                        <f:BoundField DataField="Pro_Tractime" SortField="Pro_Tractime" Width="80px" HeaderText="标准时间" />


                    </Columns>

                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="1200px"
            Height="700px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>

