<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_power.aspx.cs" Inherits="LeanFine.Lf_Admin.role_power" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        ul.powers {
            margin: 0;
            padding: 0;
        }

            ul.powers li {
                margin: 5px 15px 5px 0;
                display: inline-block;
                min-width: 150px;
            }

                ul.powers li input {
                    vertical-align: middle;
                }

                ul.powers li label {
                    margin-left: 5px;
                }

        /* 自动换行，放置权限列表过长 */
        .f-grid-cell.f-grid-cell-Powers .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server"></f:PageManager>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" Width="200px" Position="Left" Layout="Fit" BodyPadding="5px" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" ShowBorder="true" ShowHeader="false" EnableCheckBoxSelect="false" DataKeyNames="ID" AllowSorting="true" OnSort="Grid1_Sort" SortField="Name" SortDirection="DESC" AllowPaging="false" EnableMultiSelect="false" OnRowClick="Grid1_RowClick" EnableRowClickEvent="true">
                            <Columns>
                                <f:RowNumberField></f:RowNumberField>
                                <f:BoundField DataField="Name" SortField="Name" ExpandUnusedSpace="true" HeaderText="角色名称"></f:BoundField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="Fit" BodyPadding="5px 5px 5px 0" runat="server">
                    <Items>
                        <f:Grid ID="Grid2" runat="server" ShowBorder="true" ShowHeader="false" EnableMultiSelect="true" EnableCheckBoxSelect="false" DataKeyNames="ModuleId,ModuleName" AllowSorting="true" OnSort="Grid2_Sort" OnRowDataBound="Grid2_RowDataBound" SortField="GroupName" SortDirection="DESC" AllowPaging="false">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" ItemSpace="16" runat="server">
                                    <Items>
                                        <f:RadioButton ID="rbtnAll" Label="" Checked="true" GroupName="MyRadioGroup2"
                                            Text="<%$ Resources:GlobalResource, sys_Status_All %>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                        </f:RadioButton>
                                        <f:RadioButton ID="rbtnView" Label="" GroupName="MyRadioGroup2"
                                            Text="<%$ Resources:GlobalResource, sys_Button_View %>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                        </f:RadioButton>
                                        <f:RadioButton ID="rbtnNew" GroupName="MyRadioGroup2"
                                            Text="<%$ Resources:GlobalResource,sys_Button_New%>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                        </f:RadioButton>
                                        <f:RadioButton ID="rbtnEdit" GroupName="MyRadioGroup2"
                                            Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                        </f:RadioButton>
                                        <f:RadioButton ID="rbtnDelete" GroupName="MyRadioGroup2"
                                            Text="<%$ Resources:GlobalResource,sys_Button_Delete%>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                        </f:RadioButton>
                                        <f:RadioButton ID="rbtnPrint" GroupName="MyRadioGroup2"
                                            Text="<%$ Resources:GlobalResource,sys_Button_Print%>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                        </f:RadioButton>
                                        <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
                                        </f:ToolbarSeparator>
                                        <f:Button ID="btnSelectAll" IconUrl="~/Lf_Resources/menu/btn_all.png" EnablePostBack="false" runat="server" Text="全选">
                                        </f:Button>
                                        <f:Button ID="btnUnSelectAll" IconUrl="~/Lf_Resources/menu/btn_unall.png" EnablePostBack="false" runat="server" Text="反选">
                                        </f:Button>
                                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                        </f:ToolbarSeparator>
                                        <f:Button ID="btnGroupUpdate" IconUrl="~/Lf_Resources/menu/btn_batch.png" runat="server" Text="更新当前角色的权限" OnClick="btnGroupUpdate_Click">
                                        </f:Button>
                                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                        </f:ToolbarSeparator>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField></f:RowNumberField>
                                <f:BoundField DataField="GroupName" SortField="GroupName" HeaderText="分组名称" Width="120px"></f:BoundField>
                                <f:TemplateField ExpandUnusedSpace="true" ColumnID="Powers" HeaderText="权限列表">
                                    <ItemTemplate>
                                        <asp:CheckBoxList ID="ddlPowers" CssClass="powers" RepeatLayout="UnorderedList" RepeatDirection="Vertical" runat="server">
                                        </asp:CheckBoxList>
                                    </ItemTemplate>
                                </f:TemplateField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <f:Menu ID="Menu2" runat="server">
            <f:MenuButton ID="menuSelectRows" EnablePostBack="false" runat="server" Text="全选行">
            </f:MenuButton>
            <f:MenuButton ID="menuUnselectRows" EnablePostBack="false" runat="server" Text="取消行">
            </f:MenuButton>
        </f:Menu>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="600px" OnClose="Window1_Close">
        </f:Window>
    </form>
    <script>
        var grid2ID = '<%= Grid2.ClientID %>';
        var btnSelectAll = '<%= btnSelectAll.ClientID %>';
        var btnUnSelectAll = '<%= btnUnSelectAll.ClientID %>';

        var menuID = '<%= Menu2.ClientID %>';
        var menuSelectRows = '<%= menuSelectRows.ClientID %>';
        var menuUnselectRows = '<%= menuUnselectRows.ClientID %>';


        F.ready(function () {
            var grid = F(grid2ID), gridEl = grid.el; //$(grid.el.dom);
            var checkboxSelector = '.powers input[type=checkbox]',
                selectedRowSelector = '.f-grid-row-selected',
                selectedRowCheckboxSelector = selectedRowSelector + ' ' + checkboxSelector;


            F(grid2ID).on('beforerowcontextmenu', function (event, rowId, rowIndex) {
                F(menuID).show();
                return false;
            });


            function selectCheckbox(checked) {
                var selectedRows = gridEl.find(selectedRowSelector);
                if (selectedRows.length) {
                    gridEl.find(selectedRowCheckboxSelector).prop('checked', checked);
                } else {
                    gridEl.find(checkboxSelector).prop('checked', checked);
                }
            }

            F(menuSelectRows).on('click', function () {
                selectCheckbox(true);
            });

            F(menuUnselectRows).on('click', function () {
                selectCheckbox(false);
            });


            F(btnSelectAll).on('click', function () {
                selectCheckbox(true);
            });
            F(btnUnSelectAll).on('click', function () {
                selectCheckbox(false);
            });

        });

    </script>
</body>
</html>
