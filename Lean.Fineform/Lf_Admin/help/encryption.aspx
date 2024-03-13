<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="encryption.aspx.cs" Inherits="LeanFine.Lf_Admin.help.encryption" %>

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
            <Items>
                <f:Panel ID="Panel2" Title="Character" Height="570px" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    RegionPercent="30%" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                    <Items>
                        <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server"
                            BodyPadding="0px" Title="SimpleForm">
                            <Items>
                                <f:TextArea ID="TextArea1" runat="server" RegionSplit="true" RegionSplitWidth="100%"
                                    Height="560px" FocusOnPageLoad="true">
                                </f:TextArea>
                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel3" Title="Algorithms" Height="570px" RegionPosition="Center" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                    <Items>
                        <f:SimpleForm ID="SimpleForm2" ShowBorder="false" ShowHeader="false" runat="server"
                            BodyPadding="0px" Title="SimpleForm">
                            <Items>

                                <f:Button ID="Button1" Text="AES加密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button1_Click"></f:Button>
                                <f:Button ID="Button2" Text="AES解密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button2_Click"></f:Button>
                                <f:Button ID="Button3" Text="DES加密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button3_Click"></f:Button>
                                <f:Button ID="Button4" Text="DES解密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button4_Click"></f:Button>
                                <f:Button ID="Button5" Text="MD5加密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button5_Click"></f:Button>
                                <f:Button ID="Button6" Text="MD5解密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button6_Click"></f:Button>
                                <f:Button ID="Button7" Text="SHA1" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button7_Click"></f:Button>
                                <f:Button ID="Button8" Text="SHA384" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button8_Click"></f:Button>
                                <f:Button ID="Button9" Text="SHA256" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button9_Click"></f:Button>
                                <f:Button ID="Button10" Text="SHA512" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button10_Click"></f:Button>
                                <f:Button ID="Button11" Text="GUID" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button11_Click"></f:Button>

                                <f:Button ID="Button18" Text="base64加密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button18_Click"></f:Button>
                                <f:Button ID="Button19" Text="base64解密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true" OnClick="Button19_Click"></f:Button>
                                <f:Button ID="Button22" Text="Rabbit加密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true"></f:Button>
                                <f:Button ID="Button23" Text="Rabbit解密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true"></f:Button>
                                <f:Button ID="Button24" Text="TripleDES加密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true"></f:Button>
                                <f:Button ID="Button25" Text="TripleDES解密" Width="150px" RegionSplit="true" RegionSplitWidth="100%" runat="server" EnablePostBack="true"></f:Button>
                            </Items>
                        </f:SimpleForm>

                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="Encryption" Height="570px" RegionPosition="Right" RegionSplit="true" EnableCollapse="true"
                    RegionPercent="30%" runat="server" CssStyle="margin-bottom:5px;"
                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                    <Items>
                        <f:SimpleForm ID="SimpleForm3" ShowBorder="false" ShowHeader="false" runat="server"
                            BodyPadding="0px" Title="SimpleForm">
                            <Items>
                                <f:TextArea ID="TextArea2" runat="server" RegionPercent="30%" Height="560px"></f:TextArea>
                            </Items>
                        </f:SimpleForm>

                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
