<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YF_Materials_view.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.MM.YF_Materials_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .highlight {
            font-weight: bold;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ActiveTabIndex="0"
            runat="server">
            <Tabs>
                <f:Tab Title="H100" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ID="FormRow2" runat="server">
                                    <Items>
                                        <f:Label ID="HMB001" Label="品号物料" runat="server" />
                                        <f:Label ID="HMB002" Label="物料品名" runat="server" />
                                        <f:Label ID="HMB005" Label="物料类别" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow4" runat="server">
                                    <Items>
                                        <f:Label ID="HMB003" Label="制造品名" runat="server" />
                                        <f:Label ID="HMB080" Label="制造商号" runat="server" />
                                        <f:Label ID="HMB110" Label="制造品号" runat="server" />
                                    </Items>
                                </f:FormRow>


                                <f:FormRow ID="FormRow10" runat="server">
                                    <Items>
                                        <f:Label ID="HMB007" Label="生产状态" runat="server" />
                                        <f:Label ID="HMB015" Label="重量单位" runat="server" />
                                        <f:Label ID="HMB014" Label="单位净重" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow11" runat="server">
                                    <Items>
                                        <f:Label ID="HMB009" Label="商品描述" runat="server" />
                                        <f:Label ID="HMB017" Label="主要仓库" runat="server" />
                                        <f:Label ID="HMB019" Label="存货管理" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow14" runat="server">
                                    <Items>
                                        <f:Label ID="HMB032" Label="主供应商" runat="server" />
                                        <f:Label ID="HMB025" Label="品号属性" runat="server" />
                                        <f:Label ID="HMB028" Label="备注说明" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow15" runat="server">
                                    <Items>
                                        <f:Label ID="HMB067" Label="采购人员" CssClass="highlight" runat="server" />
                                        <f:Label ID="HMB036" Label="前置天数" runat="server" />
                                        <f:Label ID="HMB039" Label="最低补量" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow16" runat="server">
                                    <Items>
                                        <f:Label ID="HMB048" Label="进价原币" runat="server" />
                                        <f:Label ID="HMB049" Label="进价单价" runat="server" />
                                        <f:Label ID="HMB050" Label="进价单价" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow18" runat="server">
                                    <Items>
                                        <f:Label ID="HUDF01" Label="海关品名" runat="server" />
                                        <f:Label ID="HUDF02" Label="海关规格" runat="server" />
                                        <f:Label ID="HUDF04" Label="海关编码" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow19" runat="server">
                                    <Items>
                                        <f:Label ID="HMB068" Label="工作中心" runat="server" />
                                        <f:Label ID="HUDF05" Label="最新业者" CssClass="highlight" runat="server" />
                                        <f:Label ID="HUDF51" Label="最新核价" CssClass="highlight" runat="server" />
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Tab>
                <f:Tab Title="C100" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:Form ID="Form3" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:Label ID="CMB001" Label="品号物料" runat="server" />
                                        <f:Label ID="CMB002" Label="物料品名" runat="server" />
                                        <f:Label ID="CMB005" Label="物料类别" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow23" runat="server">
                                    <Items>
                                        <f:Label ID="CMB003" Label="制造品名" runat="server" />
                                        <f:Label ID="CMB080" Label="制造商号" runat="server" />
                                        <f:Label ID="CMB110" Label="制造品号" runat="server" />
                                    </Items>
                                </f:FormRow>


                                <f:FormRow ID="FormRow3" runat="server">
                                    <Items>
                                        <f:Label ID="CMB007" Label="生产状态" runat="server" />
                                        <f:Label ID="CMB015" Label="重量单位" runat="server" />
                                        <f:Label ID="CMB014" Label="单位净重" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow5" runat="server">
                                    <Items>
                                        <f:Label ID="CMB009" Label="商品描述" runat="server" />
                                        <f:Label ID="CMB017" Label="主要仓库" runat="server" />
                                        <f:Label ID="CMB019" Label="存货管理" runat="server" />


                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow6" runat="server">
                                    <Items>
                                        <f:Label ID="CMB032" Label="主供应商" runat="server" />
                                        <f:Label ID="CMB025" Label="品号属性" runat="server" />
                                        <f:Label ID="CMB028" Label="备注说明" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow7" runat="server">
                                    <Items>
                                        <f:Label ID="CMB067" Label="采购人员" CssClass="highlight" runat="server" />
                                        <f:Label ID="CMB036" Label="前置天数" runat="server" />
                                        <f:Label ID="CMB039" Label="最低补量" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow9" runat="server">
                                    <Items>
                                        <f:Label ID="CMB048" Label="进价原币" runat="server" />
                                        <f:Label ID="CMB049" Label="进价单价" runat="server" />
                                        <f:Label ID="CMB050" Label="进价单价" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow12" runat="server">
                                    <Items>
                                        <f:Label ID="CUDF01" Label="海关品名" runat="server" />
                                        <f:Label ID="CUDF02" Label="海关规格" runat="server" />
                                        <f:Label ID="CUDF04" Label="海关编码" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow13" runat="server">
                                    <Items>
                                        <f:Label ID="CMB068" Label="工作中心" runat="server" />
                                        <f:Label ID="CUDF05" Label="最新业者" CssClass="highlight" runat="server" />
                                        <f:Label ID="CUDF51" Label="最新核价" runat="server" />
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>

                    </Items>
                </f:Tab>

            </Tabs>
        </f:TabStrip>

    </form>
</body>
</html>
