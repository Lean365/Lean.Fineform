<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="company_new.aspx.cs" Inherits="Fine.Lf_Admin.company_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" Height="350px" ShowBorder="true" TabPosition="Top"
            EnableTabCloseMenu="false" ActiveTabIndex="0" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
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
            <Tabs>
                <f:Tab Title="机构信息" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm1" ShowBorder="false"
                            ShowHeader="false" Title="SimpleForm1" LabelWidth="120px" runat="server">
                            <Items>
                                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="机构信息" runat="server">
                                    <Items>
                                        <f:Panel ID="Panel2" Layout="HBox" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:Label ID="Label1" runat="server" ShowRedStar="true" Label="机构类型"></f:Label>
                                                <f:DropDownList ID="DDLentCategory" runat="server" ShowLabel="false" Label="机构类型" LabelAlign="Right">
                                                    <f:ListItem Text="国家机关" Value="国家机关" />
                                                    <f:ListItem Text="企业" Value="企业" />
                                                    <f:ListItem Text="事业单位" Value="事业单位" />
                                                    <f:ListItem Text="社会团体" Value="社会团体" />
                                                    <f:ListItem Text="其他机构" Value="其他机构" />
                                                </f:DropDownList>
                                                <f:DropDownList ID="DDLentNature" runat="server" ShowLabel="false" Label="机构性质" LabelAlign="Right">
                                                    <f:ListItem Text="全民所有制" Value="全民所有制" />
                                                    <f:ListItem Text="集体所有制" Value="集体所有制" />
                                                    <f:ListItem Text="有限责任" Value="有限责任" />
                                                    <f:ListItem Text="股份有限" Value="股份有限" />
                                                    <f:ListItem Text="中外合资" Value="中外合资" />
                                                    <f:ListItem Text="中外合作" Value="中外合作" />
                                                    <f:ListItem Text="外资" Value="外资" />
                                                    <f:ListItem Text="合伙" Value="合伙" />
                                                    <f:ListItem Text="个人独资" Value="个人独资" />
                                                    <f:ListItem Text="私营" Value="私营" />
                                                </f:DropDownList>


                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel1"  ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="TXTentOrgCode" runat="server" Label="机构代码" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:DatePicker runat="server" Label="成立时间" ID="DPKentFoundedTime" ShowRedStar="True" DateFormatString="yyyy-MM-dd" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" LabelAlign="Right">
                                                </f:DatePicker>
                                                <f:TextBox ID="TXTentCorporate" runat="server" Label="法人" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:TextBox ID="TXTentabbrName" runat="server" Label="英文简称" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:TextBox ID="TXTentShortName" runat="server" Label="机构简称" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:TextBox ID="TXTentFullName_cn" runat="server" Label="机构名称cn" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:TextBox ID="TXTentFullName_en" runat="server" Label="机构名称en" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>

                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:GroupPanel>
                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:Tab>
                <f:Tab Title="<span class='highlight'>联系方式</span>" BodyPadding="10px"
                    runat="server">
                    <Items>
                        <f:GroupPanel ID="GroupPanel4" Layout="Anchor" Title="联系信息" runat="server">
                            <Items>
                                <f:Panel ID="Panel9" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TXTentOuterPhone" runat="server" Label="外线电话" ShowRedStar="True" Required="True" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="TXTentFax" runat="server" Label="传真" ShowRedStar="True" Required="True" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:NumberBox ID="TXTentInnerPhone" runat="server" Label="内线电话" NoDecimal="true" NoNegative="true" ShowRedStar="True" Required="True" RegexPattern="NUMBER" LabelAlign="Right">
                                        </f:NumberBox>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel6" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TXTentEmail" runat="server" Label="电子邮箱" ShowRedStar="True" Required="True" RegexPattern="EMAIL" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="TXTentWebUrl" runat="server" Label="WWW" ShowRedStar="True" Required="True" RegexPattern="URL" LabelAlign="Right">
                                        </f:TextBox>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:Tab>
                <f:Tab Title="联系地址" BodyPadding="10px" runat="server">
                    <Items>
                        <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="联系地址" runat="server">
                            <Items>
                                <f:Panel ID="Panel4" Layout="HBox" BoxConfigAlign="Stretch" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="ProvinceId" Label="省" BoxFlex="1" LabelWidth="50px" Required="true" ShowRedStar="true" runat="server" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="CityId" Label="市" BoxFlex="1" LabelWidth="50px" Required="true" ShowRedStar="true" runat="server" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="CountyId" Label="县" BoxFlex="1" LabelWidth="50px" Required="true" ShowRedStar="true" runat="server" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="TownId" Label="镇" BoxFlex="1" LabelWidth="50px" Required="true" ShowRedStar="true" runat="server" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="VillageId" Label="镇" BoxFlex="1" LabelWidth="50px" Required="true" ShowRedStar="true" runat="server" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="TXTentPostalcode" Label="邮政编码" Width="200px" Required="true" ShowRedStar="true" runat="server" LabelAlign="Right">
                                        </f:TextBox>
                                    </Items>
                                </f:Panel>
                                <f:TextArea  ID="TxtentAddress_cn" Label="详细地址" Required="true" ShowRedStar="true" runat="server" Height="100px" LabelAlign="Right">
                                </f:TextArea>
                                <f:TextArea ID="TxtentAddress_en" Label="详细地址" Required="true" ShowRedStar="true" runat="server" Height="100px" LabelAlign="Right">
                                </f:TextArea>
                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:Tab>
                <f:Tab Title="其它" BodyPadding="10px" runat="server">
                    <Items>
                        <f:GroupPanel ID="GroupPanel5" Layout="Anchor" Title="其它信息" runat="server">
                            <Items>
                                <f:TextArea ID="TXTentBusinessScope" runat="server" Label="经营范围" ShowRedStar="True" Required="True" Height="50px" LabelAlign="Right">
                                </f:TextArea>
                                <f:TextArea ID="TXTentSlogan_cn" runat="server" Label="口号" ShowRedStar="True" Required="True" Height="80px" LabelAlign="Right">
                                </f:TextArea>
                                <f:TextArea ID="TXTentSlogan_en" runat="server" Label="口号" ShowRedStar="True" Required="True" Height="80px" LabelAlign="Right">
                                </f:TextArea>
                                <f:TextArea ID="TXTentSlogan_ja" runat="server" Label="口号" ShowRedStar="True" Required="True" Height="80px" LabelAlign="Right">
                                </f:TextArea>
                                <f:NumberBox ID="NUMSortCode" runat="server" Label="排序" NoDecimal="false" NoNegative="True" DecimalPrecision="0" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:NumberBox>

                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>

        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>

</body>
</html>
