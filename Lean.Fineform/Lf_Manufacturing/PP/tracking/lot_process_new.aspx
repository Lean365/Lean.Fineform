<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lot_process_new.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.PP.tracking.lot_process_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-hlfNew {
            background-color: #b200ff;
            color: #fff;
        }

            .f-grid-row .f-grid-cell-hlfNew a,
            .f-grid-row .f-grid-cell-hlfNew a:hover {
                color: #fff;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="SAP ECN_Master">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>

                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <%--<f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>--%>
                        <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>"
                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                            OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                        </f:TwinTriggerBox>
                        <f:Button ID="btnClose" ValidateForms="SimpleForm1" Icon="SystemClose" OnClick="btnClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>

                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="false" ForceFit="true" EnableTextSelection="true"
                    DataKeyNames="Pro_Item" AllowSorting="true" OnSort="Grid1_Sort" SortField="Pro_Item"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">

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
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" HeaderText="序号" />
                        <f:BoundField DataField="Pro_Item" SortField="Pro_Item" MinWidth="200px" MaxWidth="200px" Width="200px" HeaderText="物料" />
                        <f:BoundField DataField="Pro_Model" SortField="Pro_Model" MinWidth="200px" MaxWidth="200px" Width="200px" HeaderText="机种" />
                        <f:BoundField DataField="Pro_Region" SortField="Pro_Region" HtmlEncode="false" MinWidth="200px" MaxWidth="200px" Width="200px" HeaderText="仕向" />
                        <f:BoundField DataField="Pro_Manhour" SortField="Pro_Manhour" MinWidth="200px" MaxWidth="200px" Width="200px" HeaderText="ST" />

                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_List%>" ColumnID="hlfNew" MinWidth="200px" MaxWidth="200px"
                            Width="200px" CommandName="EcnAdd" Text="<%$ Resources:GlobalResource,sys_Button_New_EC%>" />


                    </Columns>
                </f:Grid>
            </Items>

        </f:Panel>
        <%--<f:Label ID="labResult" EncodeText="false" runat="server"> </f:Label>--%>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="1000px" Title="新增"
            Height="800px" OnClose="Window1_Close">
        </f:Window>

    </form>
</body>
</html>

