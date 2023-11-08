<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acctitle.aspx.cs" Inherits="Lean.Fineform.Lf_Accounting.acctitle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="YiFei Data">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                    DataKeyNames="Bacode" AllowSorting="true" SortField="Bacode" EnableTextSelection="true"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange"
                    OnSort="Grid1_Sort"
                    OnRowDataBound="Grid1_RowDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:RadioButton ID="rbtnFirstAuto" Label="" Checked="true" GroupName="MyRadioGroup2" Text="TCJ" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnSecondAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="DTA" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnThirdAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="TAC" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
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
                        </f:DropDownList>

                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="35px" HeaderText="序号" />
                        <f:BoundField DataField="Bacode" SortField="Bacode" Width="100px" HeaderText="科目" />
                        <f:BoundField DataField="Bacnname" SortField="Bacnname" Width="100px" HeaderText="DTA" />
                        <f:BoundField DataField="Bajpname" SortField="Bajpname" Width="100px" HeaderText="TCJ" />
                        <f:BoundField DataField="Baenname" SortField="Baenname" Width="100px" HeaderText="TAC" />



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

