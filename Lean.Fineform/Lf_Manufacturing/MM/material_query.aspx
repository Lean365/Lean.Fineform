<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="material_query.aspx.cs" Inherits="LeanFine.Lc_MM.material_query" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .customlabel span {
            color: red;
            font-weight: bold;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" Height="150px" ShowBorder="true" TabPosition="Top" ActiveTabIndex="0"
            runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Layout="Fit">
                    <Items>
                        <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Items%>"
                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                            OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                        </f:TwinTriggerBox>

                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Tabs>
                <f:Tab Title="基本信息" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:Panel ID="pnl_Main" runat="server" BodyPadding="3px"
                            ShowBorder="false" ShowHeader="false" Layout="Fit">
                            <Items>
                                <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                                    <Rows>

                                        <f:FormRow ID="FormRow2" runat="server" BoxFlex="1">
                                            <Items>
                                                <f:Label ID="isDate" Label="更新日期" runat="server" />
                                                <f:Label ID="Plnt" Label="工厂" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow1" runat="server">
                                            <Items>
                                                <f:Label ID="MatItem" EncodeText="false" CssClass="customlabel" Label="物料号" runat="server" />
                                                <f:Label ID="Industry" Label="物料行业" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow3" runat="server">
                                            <Items>
                                                <f:Label ID="MatType" Label="物料类型" runat="server" />
                                                <f:Label ID="MatDescription" Label="物料描述" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow4" runat="server">
                                            <Items>
                                                <f:Label ID="BaseUnit" Label="单位" runat="server" />
                                                <f:Label ID="ProHierarchy" Label="产品阶层" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow5" runat="server">
                                            <Items>
                                                <f:Label ID="MatGroup" Label="物料组" runat="server" />
                                            </Items>

                                        </f:FormRow>

                                    </Rows>
                                </f:Form>

                            </Items>
                        </f:Panel>

                    </Items>
                </f:Tab>
                <f:Tab Title="采购信息" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:Panel ID="Panel1" runat="server" BodyPadding="3px"
                            ShowBorder="false" ShowHeader="false" Layout="Fit">
                            <Items>
                                <f:Form ID="Form3" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow ID="FormRow6" runat="server">
                                            <Items>
                                                <f:Label ID="PurGroup" EncodeText="false" CssClass="customlabel" Label="采购组" runat="server" />
                                                <f:Label ID="PurType" EncodeText="false" CssClass="customlabel" Label="采购类型" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow7" runat="server">
                                            <Items>
                                                <f:Label ID="SpecPurType" Label="特殊采购" runat="server" />
                                                <f:Label ID="BulkMat" Label="散装物料" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow8" runat="server">
                                            <Items>
                                                <f:Label ID="Moq" EncodeText="false" CssClass="customlabel" Label="MOQ" runat="server" />
                                                <f:Label ID="RoundingVal" Label="舍入值" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow9" runat="server">
                                            <Items>
                                                <f:Label ID="LeadTime" EncodeText="false" CssClass="customlabel" Label="Lead Time" runat="server" />
                                                <f:Label ID="isCheck" Label="检验库存" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow11" runat="server">
                                            <Items>
                                                <f:Label ID="MPN" Label="制造商零件" runat="server" />
                                                <f:Label ID="Mfrs" Label="制造商" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>

                            </Items>
                        </f:Panel>

                    </Items>
                </f:Tab>
                <f:Tab Title="MRP" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:Panel ID="Panel2" runat="server" BodyPadding="3px"
                            ShowBorder="false" ShowHeader="false" Layout="Fit">
                            <Items>
                                <f:Form ID="Form4" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow ID="FormRow10" runat="server">
                                            <Items>
                                                <f:Label ID="isLot" Label="批次管理" runat="server" />
                                                <f:Label ID="SLoc" Label="生产仓储" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow15" runat="server">
                                            <Items>
                                                <f:Label ID="ESLoc" EncodeText="false" CssClass="customlabel" Label="采购仓储" runat="server" />
                                                <f:Label ID="LocPosition" Label="库存仓位" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow16" runat="server">
                                            <Items>
                                                <f:Label ID="Inventory" CssClass="customlabel" Label="前一天库存" runat="server" Text="0" />
                                                <f:Label ID="LocEol" EncodeText="false" CssClass="customlabel" Label="生产停止" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow17" runat="server">
                                            <Items>
                                                <f:Label ID="ProDays" Label="自制天数" runat="server" />

                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>

                            </Items>
                        </f:Panel>

                    </Items>
                </f:Tab>
                <f:Tab Title="成本会计" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:Panel ID="Panel3" runat="server" BodyPadding="3px"
                            ShowBorder="false" ShowHeader="false" Layout="Fit">
                            <Items>
                                <f:Form ID="Form5" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow ID="FormRow12" runat="server">
                                            <Items>
                                                <f:Label ID="ProfitCenter" EncodeText="false" CssClass="customlabel" Label="成本中心" runat="server" />
                                                <f:Label ID="DiffCode" Label="差异码" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow13" runat="server">
                                            <Items>
                                                <f:Label ID="EvaluationType" Label="评估类" runat="server" />
                                                <f:Label ID="MovingAvg" EncodeText="false" CssClass="customlabel" Label="移动价格" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow14" runat="server">
                                            <Items>
                                                <f:Label ID="Currency" Label="币种" runat="server" />
                                                <f:Label ID="PriceControl" Label="价格控制" runat="server" />
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow18" runat="server">
                                            <Items>
                                                <f:Label ID="PriceUnit" Label="价格单位" runat="server" />

                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>

                            </Items>
                        </f:Panel>

                    </Items>
                </f:Tab>

            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>
