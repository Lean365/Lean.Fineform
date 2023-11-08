<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Yf_EC.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.MM.Yf_EC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" EnableFStateValidation="false"></f:PageManager>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" Width="120px" Position="Left" Layout="Fit" BodyPadding="5px" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" ShowBorder="true" ShowHeader="false" EnableCheckBoxSelect="false" DataKeyNames="TA002,TA003" AllowSorting="true"
                            OnSort="Grid1_Sort" SortField="TA003" SortDirection="DESC" AllowPaging="false" EnableMultiSelect="false" EnableRowDoubleClickEvent="False"
                            EnableRowClickEvent="true" OnRowClick="Grid1_RowClick">

                            <Columns>

                                <f:BoundField DataField="TA002" SortField="TA002" Width="110px" HeaderText="设变"></f:BoundField>

                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Left" BodyPadding="5px 5px 5px 0" runat="server">
                    <Items>
                        <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                            EnableCheckBoxSelect="false" ForceFit="true" DataKeyNames="TB002" AllowSorting="true" OnSort="Grid2_Sort" SortField="TB002"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid2_PreDataBound" 
                            OnPreRowDataBound="Grid2_PreRowDataBound" OnRowCommand="Grid2_RowCommand" OnPageIndexChange="Grid2_PageIndexChange">

                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <f:TwinTriggerBox ID="ttbSearchEcnsub" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>" Width="300px"
                                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchEcnsub_Trigger2Click"
                                            OnTrigger1Click="ttbSearchEcnsub_Trigger1Click">
                                        </f:TwinTriggerBox>
                                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                        </f:ToolbarSeparator>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                                </f:ToolbarText>
                                <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged" runat="server">
                                    <f:ListItem Text="10" Value="10"></f:ListItem>
                                    <f:ListItem Text="20" Value="20"></f:ListItem>
                                    <f:ListItem Text="50" Value="50"></f:ListItem>
                                    <f:ListItem Text="100" Value="100"></f:ListItem>
                                </f:DropDownList>
                            </PageItems>
                            <Columns>
                                <f:BoundField DataField="TB002" SortField="TB002 " MinWidth="100px" HeaderText="设变"></f:BoundField>
                                <f:BoundField DataField="TB003" SortField="TB003 " MinWidth="60px" HeaderText="序号"></f:BoundField>
                                <f:BoundField DataField="TB004" SortField="TB004 " MinWidth="100px" HeaderText="主件"></f:BoundField>
                                <f:RenderField DataField="TB008" SortField="TB008 " MinWidth="60px" HeaderText="用量" FieldType="Double"></f:RenderField>
                                <f:BoundField DataField="TB104" SortField="TB104 " MinWidth="100px" HeaderText="原件"></f:BoundField>
                                <f:RenderField DataField="TB108" SortField="TB108 " MinWidth="60px" HeaderText="用量" FieldType="Double"></f:RenderField>
                                <f:BoundField DataField="TA005" SortField="TA005 " MinWidth="160px" HeaderText="原因"></f:BoundField>
                                <f:BoundField DataField="TA003" SortField="TA003 " MinWidth="80px" HeaderText="日期"></f:BoundField>
                            </Columns>
                        </f:Grid>

                        <f:Grid ID="Grid3" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                            EnableCheckBoxSelect="false" ForceFit="true" DataKeyNames="TC002" AllowSorting="true" OnSort="Grid3_Sort" SortField="TC002"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid3_PreDataBound"  
                            OnPreRowDataBound="Grid3_PreRowDataBound" OnRowCommand="Grid3_RowCommand" OnPageIndexChange="Grid3_PageIndexChange">
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator4" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText2" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                                </f:ToolbarText>
                                <f:DropDownList ID="ddlGridPageSize2" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize2_SelectedIndexChanged" runat="server">
                                    <f:ListItem Text="10" Value="10"></f:ListItem>
                                    <f:ListItem Text="20" Value="20"></f:ListItem>
                                    <f:ListItem Text="50" Value="50"></f:ListItem>
                                    <f:ListItem Text="100" Value="100"></f:ListItem>
                                </f:DropDownList>
                            </PageItems>
                            <Columns>
                                <%--<f:BoundField DataField="TC002" SortField="TC002 " Width="100px" HeaderText="设变"></f:BoundField>--%>
                                <f:BoundField DataField="TC003" SortField="TC003 " Width="80px" HeaderText="序号"></f:BoundField>
                                <f:BoundField DataField="TC004" SortField="TC004 " Width="80px" HeaderText="BOM序号"></f:BoundField>
                                <f:BoundField DataField="TC005" SortField="TC005 " Width="120px" HeaderText="元件"></f:BoundField>
                                <f:RenderField  DataField="TC008" SortField="TC008 " Width="80px" HeaderText="用量" FieldType="Double"></f:RenderField>
                                <f:RenderField  DataField="TC009" SortField="TC009 " Width="80px" HeaderText="底数" FieldType="Double"></f:RenderField>
                                <f:BoundField DataField="TC011" SortField="TC011 " Width="100px" HeaderText="工艺"></f:BoundField>
                                <f:BoundField DataField="TC021" SortField="TC021 " Width="100px" HeaderText="工艺"></f:BoundField>
                                <f:BoundField DataField="TC013" SortField="TC013 " Width="100px" HeaderText="生效"></f:BoundField>
                                <f:BoundField DataField="TC018" SortField="TC018 " Width="100px" HeaderText="原因"></f:BoundField>
                                <f:BoundField DataField="TC104" SortField="TC104 " Width="80px" HeaderText="原序号"></f:BoundField>
                                <f:BoundField DataField="TC105" SortField="TC105 " Width="120px" HeaderText="原元件"></f:BoundField>
                                <f:RenderField DataField="TC108" SortField="TC108 " Width="80px" HeaderText="原用量" FieldType="Double"></f:RenderField>
                                <f:RenderField DataField="TC109" SortField="TC109 " Width="80px" HeaderText="原底数" FieldType="Double"></f:RenderField>
                                <f:BoundField DataField="TC111" SortField="TC111 " Width="100px" HeaderText="原工艺"></f:BoundField>
                                <f:BoundField DataField="TC121" SortField="TC121 " Width="100px" HeaderText="工艺"></f:BoundField>
                                <f:BoundField DataField="TC113" SortField="TC113 " Width="100px" HeaderText="原生效"></f:BoundField>

                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
</body>
</html>


