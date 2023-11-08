<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="countersignature.aspx.cs" Inherits="Lean.Fineform.Lf_Accounting.countersignature" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="_form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Window1" runat="server" />
        <f:Window ID="Window1" runat="server" Title="流程审批" IsModal="false" EnableClose="false" EnableResize="true"
            Width="650px" BodyPadding="10px" AutoScroll="true">
            <Items>
                <f:Form ID="Form1" LabelAlign="Right" MessageTarget="Qtip"
                    ShowBorder="false" ShowHeader="false" runat="server">
                    <Items>
                        <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="申请信息" runat="server">
                            <Items>
                                <f:Form ID="FormApply" ShowBorder="false" ShowHeader="false"
                                    runat="server" LabelWidth="120" LabelAlign="Right" Layout="VBox" MessageTarget="Qtip">
                                    <Rows>
                                        <f:FormRow runat="server">
                                            <Items>
                                                <f:Label ID="lblUseDepName" Label="申请部门" Text="" runat="server"></f:Label>
                                                <f:Label ID="lblApplyName" Label="申请人" runat="server"></f:Label>                                        
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow runat="server">
                                            <Items>
                                                <f:NumberBox runat="server" ID="numPersonCount" Label="预算金额" EmptyText="请输入预算金额" NoNegative="true" NoDecimal="true" ShowRedStar="true" Required="true" />
                                                <f:NumberBox runat="server" ID="NumberBox1" Label="使用金额" EmptyText="请输入使用金额" NoNegative="true" NoDecimal="true" ShowRedStar="true" Required="true" />
                                                
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow runat="server">
                                            <Items>
                                                
                                                <f:TextBox runat="server" Label="理由" ID="TxtResaon"></f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow runat="server">
                                            <Items>
                                            <f:TextArea runat="server" Label="内容" ID="TextArea1" Height="60" />
                                            </Items>
                                        </f:FormRow>

                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:GroupPanel>
                        <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="流程信息" runat="server">
                            <Items>
                                <f:Grid ID="GridInfo" runat="server" ShowBorder="true" ShowHeader="false"
                                    DataKeyNames="ID" EnableColumnLines="true" EnableRowLines="true">
                                    <Columns>
                                        <f:RowNumberField runat="server" ID="XH" ColumnID="XH" HeaderText="序号" TextAlign="Right" HeaderTextAlign="Center" Width="50" />
                                        <f:BoundField runat="server" ID="ACTIVITYNAME" ColumnID="ACTIVITYNAME" DataField="ACTIVITYNAME" HeaderText="步骤" HeaderTextAlign="Center" TextAlign="Left" Width="100" />
                                        <f:BoundField runat="server" ID="OPTORNAME" ColumnID="OPTORNAME" DataField="OPTORNAME" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Left" Width="100" />
                                        <f:TemplateField runat="server" ID="OPTIONSTATE" ColumnID="OPTIONSTATE" HeaderText="办理状态" HeaderTextAlign="Center" TextAlign="Center" Width="100">
                                            <ItemTemplate>
                                                <%#Eval("OPTIONSTATE")==null?"已提交申请":(Eval("OPTIONSTATE").ToString()=="1"?"通过":"驳回") %>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:BoundField runat="server" ID="OPTIONREMARK" ColumnID="OPTIONREMARK" DataField="OPTIONREMARK" HeaderText="办理意见" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true" MinWidth="150px" />
                                        <f:BoundField runat="server" ID="CREATETIME" ColumnID="CREATETIME" DataField="CREATETIME" HeaderText="操作时间" HeaderTextAlign="Center" TextAlign="Left" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" Width="150" />
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:GroupPanel>
                        <f:GroupPanel ID="GroupPanel4" Layout="Anchor" Title="当前审批" runat="server">
                            <Items>
                                <f:RadioButtonList ID="RadioButtonList1" Label="意见" ColumnNumber="5" ShowLabel="false" runat="server">
                                    <f:RadioItem Text="同意" Value="1" Selected="true" />
                                    <f:RadioItem Text="驳回" Value="2" />
                                </f:RadioButtonList>
                                <f:TextArea runat="server" ID="txt_Remark" Label="备注" Text="同意" Height="80" />
                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right" Position="Bottom">
                    <Items>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSaveClose" ValidateForms="FormApply" Icon="SystemSaveClose" runat="server" Text="保存后关闭">
                        </f:Button>
                        <f:ToolbarSeparator runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSavePost" ValidateForms="FormApply" Icon="SystemSaveNew" runat="server" Text="保存后提交">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btn_Close" Icon="SystemClose" EnablePostBack="false" runat="server" Text="关闭">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>

    </form>
</body>
</html>
