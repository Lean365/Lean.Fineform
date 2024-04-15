<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_daily_sub_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_daily_sub_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" EnableFStateValidation="false" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="生产日报SUB">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveClose%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ForceFit="true" ShowBorder="false" ShowHeader="false" AllowCellEditing="true" ClicksToEdit="1" EnableCheckBoxSelect="true"
                    DataKeyNames="ID" AllowSorting="true" SortField="ID" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                    OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand" EnableMultiSelect="false">
                    <Columns>
                        <%--<f:RowNumberField />--%>
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                        <f:BoundField DataField="Prolinename" SortField="Prolinename" Width="80px" HeaderText="班组" />
                        <f:BoundField DataField="Prodate" SortField="Prodate" Width="80px" HeaderText="日期" />
                        <%--<f:BoundField DataField="Proorder" SortField="Proorder" Width="120px" HeaderText="生产工单" />--%>
                        <f:BoundField DataField="Promodel" SortField="Promodel" Width="100px" HeaderText="机种" />
                        <%--<f:BoundField DataField="Prohbn" SortField="Prohbn" Width="120px" HeaderText="生产物料" />--%>
                        <f:BoundField DataField="Prolot" SortField="Prolot" Width="100px" HeaderText="LOT" />

                        <%--<f:BoundField DataField="Prost" SortField="Prost" Width="120px" HeaderText="生产工时" />--%>
                        <%--<f:BoundField DataField="Proshort" SortField="Proshort" Width="120px" HeaderText="生产点数" />--%>
                        <%--<f:BoundField DataField="Prorate" SortField="Prorate" Width="120px" HeaderText="工时汇率" />--%>

                        <f:RenderField Width="180px" ColumnID="Propcbatype" DataField="Propcbatype" HeaderText="板别" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlPropcbatype" runat="server" EnableEdit="True" ForceSelection="false">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="Propcbaside" DataField="Propcbaside" HeaderText="多面板" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlPropcbaside" runat="server" EnableEdit="True" ForceSelection="false">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>


                        <f:RenderField Width="80px" ColumnID="Prorealqty" DataField="Prorealqty" HeaderText="生产实绩" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProrealqty" FocusOnPageLoad="true" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server" AutoPostBack="true">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="Propcbastated" DataField="Propcbastated" HeaderText="完成情况" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlPropcbastated" runat="server" EnableEdit="True" ForceSelection="false">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>

                        <%--多选--%>
                        <f:RenderField Width="150px" ColumnID="Probadcou" DataField="Probadcou" HeaderText="未达成原因" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProbadcou" runat="server" EnableMultiSelect="true" EnableEdit="True" ForceSelection="false">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <%--<f:RenderField Width="150px" ColumnID="Probadmemo" DataField="Probadmemo" HeaderText="未达成备注" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProbadmemo" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>--%>
                        <f:RenderField Width="100px" ColumnID="Propcbserial" DataField="Propcbserial" HeaderText="序列号" FieldType="String">
                            <Editor>
                                <f:TextBox ID="numPropcbserial" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Protime" DataField="Protime" HeaderText="生产工数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProtime" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Prohandoffnum" DataField="Prohandoffnum" HeaderText="切换次数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProhandoffnum" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Prohandofftime" DataField="Prohandofftime" HeaderText="切换时间" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProhandofftime" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Prodowntime" DataField="Prodowntime" HeaderText="切停机时间" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProdowntime" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Prolosstime" DataField="Prolosstime" HeaderText="损失工数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProlosstime" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Promaketime" DataField="Promaketime" HeaderText="投入工数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numPromaketime" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <%--<f:BoundField DataField="Prorealtime" SortField="Prorealtime" Width="120px" HeaderText="实绩工数" />--%>
                        <%--<f:BoundField DataField="Parent_ID" SortField="Parent_ID"  Width="80px" HeaderText="Parent_ID"/>--%>
                        <%--<f:BoundField DataField="Prostdcapacity" SortField="Prostdcapacity" Width="80px" HeaderText="标准产能" />--%>
                    </Columns>
                    <Toolbars>
                        <f:Toolbar runat="server" Position="Bottom">
                            <Items>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Grid>
                <f:Panel Height="150px" ShowHeader="false" BodyPadding="10px"
                    ShowBorder="true" runat="server">
                    <Items>
                        <f:Label ID="MemoText" runat="server" EncodeText="false">
                        </f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
