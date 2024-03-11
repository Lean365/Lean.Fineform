<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="random.aspx.cs" Inherits="Fine.Lf_Admin.help.random" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" Layout="Region" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:NumberBox ID="Numtrand" runat="server" Label="位数" MaxValue="14" MinValue="0"
                            NoDecimal="true" NoNegative="True" Required="True" ShowRedStar="True">
                        </f:NumberBox>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:TextBox ID="Txtrand" runat="server" Label="字符" MaxLength="4" MinLength="0"></f:TextBox>
                        <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="Btn1" Text="指定位数" runat="server" OnClick="Btn1_Click">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="Btn2" Text="包含字符" runat="server" OnClick="Btn2_Click">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator4" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="Btn3" Text="所有条件" runat="server" OnClick="Btn3_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Panel BodyPadding="5px" ShowBorder="false" Layout="Region"
                    ShowHeader="false" runat="server">
                    <Items>
                        <f:Panel ID="Panel2" Title="Character(4Bit)" Height="530px" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    RegionPercent="15%" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                            <Items>
                                <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea ID="Txtrandom1" runat="server" Height="520px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%"></f:TextArea>

                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel4" Title="Character(6Bit)" Height="540px" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    RegionPercent="15%" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                            <Items>
                                <f:SimpleForm ID="SimpleForm2" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea ID="Txtrandom2" runat="server" Height="530px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%"></f:TextArea>

                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel9" Title="Character(8Bit)" Height="540px" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    RegionPercent="15%" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                            <Items>
                                <f:SimpleForm ID="SimpleForm3" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea ID="Txtrandom3" runat="server" Height="530px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%"></f:TextArea>

                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel10" Title="Character(10Bit)" Height="540px" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    RegionPercent="15%" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                            <Items>
                                <f:SimpleForm ID="SimpleForm4" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea ID="Txtrandom4" runat="server" Height="530px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%"></f:TextArea>

                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel3" Title="Character(12Bit)" Height="540px" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    RegionPercent="15%" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                            <Items>
                                <f:SimpleForm ID="SimpleForm5" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea ID="Txtrandom5" runat="server" Height="530px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%"></f:TextArea>

                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>


                    </Items>
                </f:Panel>


            </Items>
        </f:Panel>
    </form>
</body>
</html>
