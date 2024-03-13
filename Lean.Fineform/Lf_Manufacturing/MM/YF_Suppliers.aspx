<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YF_Suppliers.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.MM.YF_Suppliers" %>

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
                <f:Tab Title="H100" BodyPadding="10px" Layout="VBox" runat="server">
                    <Items>
                        <f:Form ID="Form4" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <f:DropDownList runat="server" ID="H_Code" Label="<%$ Resources:GlobalResource,Query_Supplier%>" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" ShowRedStar="True" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="H_Code_SelectedIndexChanged" TabIndex="2">
                                        </f:DropDownList>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Form>
                        <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>

                                <f:FormRow ID="FormRow2" runat="server">
                                    <Items>
                                        <f:Label ID="H_MA001" Label="编号" runat="server" />
                                        <f:Label ID="H_MA002" Label="简称" runat="server" />
                                        <f:Label ID="H_MA012" Label="负责人" runat="server" />


                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow4" runat="server">
                                    <Items>
                                        <f:Label ID="H_MA008" Label="TEL" runat="server" />
                                        <f:Label ID="H_MA009" Label="TEL" runat="server" />
                                        <f:Label ID="H_MA010" Label="FAX" runat="server" />
                                    </Items>
                                </f:FormRow>


                                <f:FormRow ID="FormRow10" runat="server">
                                    <Items>
                                        <f:Label ID="H_MA013" Label="联系人" runat="server" />
                                        <f:Label ID="H_MA048" Label="联系人" runat="server" />
                                        <f:Label ID="H_MA049" Label="联系人" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow11" runat="server">
                                    <Items>
                                        <f:Label ID="H_MA003" Label="全称" runat="server" />
                                        <f:Label ID="H_MA014" Label="地址" runat="server" />
                                        <f:Label ID="H_MA015" Label="地址" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow14" runat="server">
                                    <Items>
                                        <f:Label ID="H_MA011" Label="MAIL" runat="server" />
                                        <f:Label ID="H_MA021" Label="币种" runat="server" />
                                        <f:Label ID="H_MA023" Label="交易" runat="server" />


                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow15" runat="server">
                                    <Items>
                                        <f:Label ID="H_MA024" Label="结算" runat="server" />
                                        <f:Label ID="H_MA025" Label="条件" runat="server" />
                                        <f:Label ID="H_MA028" Label="账号" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow16" runat="server">
                                    <Items>
                                        <f:Label ID="H_MA047" Label="采购" runat="server" />
                                        <f:Label ID="H_MA051" Label="账单" runat="server" />
                                        <f:Label ID="H_MA055" Label="付款条件" runat="server" />
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Tab>
                <f:Tab Title="C100" BodyPadding="10px" Layout="VBox" runat="server">
                    <Items>
                        <f:Form ID="Form5" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <f:DropDownList runat="server" ID="C_Code" Label="供应商" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" ShowRedStar="True" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="C_Code_SelectedIndexChanged" TabIndex="2">
                                        </f:DropDownList>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Form>
                        <f:Form ID="Form3" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:Label ID="C_MA001" Label="编号" runat="server" />
                                        <f:Label ID="C_MA002" Label="简称" runat="server" />
                                        <f:Label ID="C_MA012" Label="负责人" runat="server" />


                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow3" runat="server">
                                    <Items>
                                        <f:Label ID="C_MA008" Label="TEL" runat="server" />
                                        <f:Label ID="C_MA009" Label="TEL" runat="server" />
                                        <f:Label ID="C_MA010" Label="FAX" runat="server" />
                                    </Items>
                                </f:FormRow>


                                <f:FormRow ID="FormRow5" runat="server">
                                    <Items>
                                        <f:Label ID="C_MA013" Label="联系人" runat="server" />
                                        <f:Label ID="C_MA048" Label="联系人" runat="server" />
                                        <f:Label ID="C_MA049" Label="联系人" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow6" runat="server">
                                    <Items>
                                        <f:Label ID="C_MA003" Label="全称" runat="server" />
                                        <f:Label ID="C_MA014" Label="地址" runat="server" />
                                        <f:Label ID="C_MA015" Label="地址" runat="server" />
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow7" runat="server">
                                    <Items>
                                        <f:Label ID="C_MA011" Label="MAIL" runat="server" />
                                        <f:Label ID="C_MA021" Label="币种" runat="server" />
                                        <f:Label ID="C_MA023" Label="交易" runat="server" />


                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow9" runat="server">
                                    <Items>
                                        <f:Label ID="C_MA024" Label="结算" runat="server" />
                                        <f:Label ID="C_MA025" Label="条件" runat="server" />
                                        <f:Label ID="C_MA028" Label="账号" runat="server" />

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow12" runat="server">
                                    <Items>
                                        <f:Label ID="C_MA047" Label="采购" runat="server" />
                                        <f:Label ID="C_MA051" Label="账单" runat="server" />
                                        <f:Label ID="C_MA055" Label="付款条件" runat="server" />
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
