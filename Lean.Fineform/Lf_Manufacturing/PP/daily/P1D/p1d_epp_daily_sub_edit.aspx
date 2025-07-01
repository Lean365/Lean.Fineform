<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_epp_daily_sub_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.p1d_epp_daily_sub_edit" %>

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
                    DataKeyNames="ID" AllowSorting="true" SortField="Prostime" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                    OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand" EnableMultiSelect="false">
                    <Columns>
                        <%--<f:RowNumberField />--%>
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                        <f:BoundField DataField="Prostime" SortField="Prostime" Width="80px" HeaderText="开始时间" />
                        <f:BoundField DataField="Proetime" SortField="Proetime" Width="80px" HeaderText="结束时间" />
                        <f:BoundField DataField="Prolot" SortField="Prolot" Width="120px" HeaderText="生产LOT" />


                        <f:RenderField Width="80px" ColumnID="Prorealqty" DataField="Prorealqty" HeaderText="生产实绩" FieldType="Double">
                            <Editor>
                                <f:NumberBox runat="server" ID="numProrealqty" FocusOnPageLoad="true" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="true" DecimalPrecision="2" Required="true" ShowRedStar="true" Increment="0.01" AutoPostBack="true">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>

                        <f:RenderField Width="100px" ColumnID="Prostopcou" DataField="Prostopcou" HeaderText="停线原因" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProstopcou" runat="server" EnableMultiSelect="true">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="Prostopmemo" DataField="Prostopmemo" HeaderText="停线备注" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProstopmemo" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Prolinestopmin" DataField="Prolinestopmin" HeaderText="停线时间" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProlinestopmin" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <%--多选--%>
                        <f:RenderField Width="150px" ColumnID="Probadcou" DataField="Probadcou" HeaderText="未达成原因"  FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProbadcou" runat="server" EnableMultiSelect="true">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="Probadmemo" DataField="Probadmemo" HeaderText="未达成备注" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProbadmemo" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Prolinemin" DataField="Prolinemin" HeaderText="生产工数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProlinemin" NoDecimal="true" NoNegative="true" MinValue="0" MaxValue="9999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:BoundField DataField="Prorealtime" SortField="Prorealtime" Width="120px" HeaderText="实绩工数" />
                        <f:BoundField DataField="Prolinename" SortField="Prolinename" Width="80px" HeaderText="班组" />
                        <%--<f:BoundField DataField="Parent_ID" SortField="Parent_ID"  Width="80px" HeaderText="Parent_ID"/>--%>
                        <f:BoundField DataField="Prostdcapacity" SortField="Prostdcapacity" Width="80px" HeaderText="标准产能" />
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
