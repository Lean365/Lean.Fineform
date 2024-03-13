<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_p1d.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.ec_p1d" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <style type="text/css">
        .f-grid-row .f-grid-cell-Ec_pmclot {
            font-size:75%;
            background-color: #66CCCC;
            color: #fff;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" EnableFStateValidation="false"></f:PageManager>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
                        <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:RadioButton ID="rbtnFirstAuto" Label="" Checked="true" GroupName="MyRadioGroup2"
                            Text="<%$ Resources:GlobalResource, sys_Status_Pp_EC_Unenforced %>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                        </f:RadioButton>
                        <f:RadioButton ID="rbtnSecondAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,sys_Status_Pp_EC_Implemented%>" runat="server"
                            OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                        </f:RadioButton>
                        <f:RadioButton ID="rbtnThirdAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,sys_Status_Pp_EC_All%>" runat="server"
                            OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                        </f:RadioButton>
                        <%--<f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>--%>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:TwinTriggerBox ID="ttbSearchEcnsub" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>" Width="300px"
                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchEcnsub_Trigger2Click"
                            OnTrigger1Click="ttbSearchEcnsub_Trigger1Click">
                        </f:TwinTriggerBox>


                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" Width="120px" Position="Left" Layout="Fit" BodyPadding="5px" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" ShowBorder="true" ShowHeader="false" EnableCheckBoxSelect="false" DataKeyNames="ID,Ec_no,Ec_model," AllowSorting="true"
                            OnSort="Grid1_Sort" SortField="Ec_issuedate" SortDirection="DESC" AllowPaging="false" EnableMultiSelect="false" EnableRowDoubleClickEvent="False" 
                            EnableRowClickEvent="true" OnRowClick="Grid1_RowClick">

                            <Columns>

                                <f:BoundField DataField="Ec_no" SortField="Ec_no" DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="设变"></f:BoundField>

                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Left" BodyPadding="5px 5px 5px 0" runat="server">
                    <Items>
                        <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                            EnableCheckBoxSelect="false" ForceFit="true" DataKeyNames="GUID,Ec_no,Ec_model,Ec_bomitem,Ec_olditem,Ec_newitem" AllowSorting="true" OnSort="Grid2_Sort" SortField="Ec_no"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid2_PreDataBound" AllowCellEditing="true" ClicksToEdit="1" EnableTextSelection="true"
                            OnPreRowDataBound="Grid2_PreRowDataBound" OnRowCommand="Grid2_RowCommand" OnPageIndexChange="Grid2_PageIndexChange">
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
                                <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                                    Icon="TableEdit" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                                    CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                                <f:BoundField DataField="Ec_p1ddate" SortField="Ec_p1ddate " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="生产日期"></f:BoundField>
                                <f:BoundField DataField="Ec_p1dline" SortField="Ec_p1dline " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="生产班组"></f:BoundField>
                                <f:BoundField DataField="Ec_p1dlot" SortField="Ec_p1dlot " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="生产批次"></f:BoundField>
                                <f:BoundField DataField="Ec_p1dlotsn" SortField="Ec_p1dlotsn " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="注意事项"></f:BoundField>
                                <%--<f:BoundField DataField="Ec_p1dnote" SortField="Ec_p1dnote "  DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="备注说明"></f:BoundField>--%>
                                <%--<f:BoundField DataField="Ec_pmclot" SortField="Ec_pmclot "  DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="预定批次"></f:BoundField>--%>
                                <f:BoundField DataField="Ec_pmclot" ColumnID="Ec_pmclot" SortField="Ec_pmclot " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="预定批次"></f:BoundField>
                                <f:HyperLinkField DataTextField="Ec_no" ColumnID="Ec_no" Width="100px" HeaderText="设变号码" MinWidth="100px" DataNavigateUrlFields="Ec_documents" />

                                <f:BoundField DataField="Ec_model" SortField="Ec_model " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="机种"></f:BoundField>
                                <f:BoundField DataField="Ec_bomitem" SortField="Ec_bomitem " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="上阶物料"></f:BoundField>
                                <%--<f:BoundField DataField="Ec_olditem" SortField="Ec_olditem " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="旧品号"></f:BoundField>--%>
                                <%--<f:BoundField DataField="Ec_newitem" SortField="Ec_newitem " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="新品号"></f:BoundField>--%>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <%--        <f:Window ID="Window1" IconUrl="~/Lf_Resources/icon/lock_key.png" runat="server" Hidden="true"
            WindowPosition="Center" IsModal="true" Title="审核" EnableMaximize="true"
            EnableResize="true" Target="Self" EnableIFrame="true"
            Height="400px" Width="400px" OnClose="Window1_Close">
        </f:Window>
        <f:Window ID="Window2" IconUrl="~/Lf_Resources/icon/lock_delete.png" runat="server" Hidden="true"
            WindowPosition="Center" IsModal="true" Title="弃审" EnableMaximize="true"
            EnableResize="true" Target="Self" EnableIFrame="true"
            Height="400px" Width="400px" OnClose="Window2_Close">
        </f:Window>--%>
        <f:Window ID="Window3" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px" OnClose="Window3_Close">
        </f:Window>
    </form>
</body>
</html>
