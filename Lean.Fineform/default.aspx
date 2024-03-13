<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="LeanFine._default" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.GlobalResource.sys_LeanName%></title>
    <script type="text/javascript">

        // 本页面一定是顶层窗口，不会嵌在IFrame中
        if (top.window != window) {
            top.window.location.href = "./default.aspx";
        }

        // 将 localhost 转换为 localhost/default.aspx
        if (window.location.href.indexOf('/default.aspx') < 0) {
            window.location.href = "./default.aspx";
        }

    </script>
    <style>
        .login-image {
            position: absolute;
            border-right: solid 1px #ddd;
            margin-bottom: 0 !important;
            align-content: center;
        }

            .login-image img {
                width: 200px;
                height: 200px;
                padding: 10px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"></f:PageManager>
        <f:Window ID="Window1" runat="server" EnableClose="False" WindowPosition="GoldenSection"
            Layout="HBox" Width="500px" BoxConfigAlign="Center" BoxConfigPosition="Center" Icon="key">

            <Items>
                <f:Image ID="imageLogin" ImageUrl="~/Lf_Resources/images/login/Lean_Logo.png" runat="server"
                    CssClass="login-image">
                </f:Image>
                <%--<f:ContentPanel CssClass="login-image" BodyPadding="10px" ShowBorder="true" ShowHeader="false" runat="server">
                    <i class="f-icon f-icon-key"></i>
                </f:ContentPanel>--%>
                <f:SimpleForm ID="SimpleForm1" LabelAlign="Top" BoxFlex="1" runat="server"
                    BodyPadding="10px 10px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        
                        <f:DropDownList ID="DDLCulture" Label="<%$ Resources:GlobalResource,sys_Language%>" Required="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLCulture_SelectedIndexChanged">
                            <f:ListItem Text="简体中文" Value="zh-CN" />
                            <f:ListItem Text="English" Value="en-US" />
                            <f:ListItem Text="日本語" Value="ja-JP" />
                        </f:DropDownList>
                        <f:TextBox ID="tbxUserName" FocusOnPageLoad="true" runat="server" Label="<%$ Resources:GlobalResource,sys_Login_Account%>" Required="true"
                            ShowRedStar="true" Text="">
                        </f:TextBox>
                        <f:TextBox ID="tbxPassword" TextMode="Password" runat="server" Required="true" ShowRedStar="true"
                            Label="<%$ Resources:GlobalResource,sys_Login_Password%>" Text="">
                        </f:TextBox>

                    </Items>
                </f:SimpleForm>
            </Items>
            <Toolbars>
                <f:Toolbar runat="server" Position="Bottom">
                    <Items>

                        <f:ToolbarText ID="tbText" CssStyle="Color:#2F4F4F;font-family:Arial, Helvetica, sans-serif;font-weight:bold" Text="TxtVersion" runat="server"></f:ToolbarText>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSubmit" Icon="LockOpen" Type="Submit" runat="server" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click" Text="<%$ Resources:GlobalResource,sys_Login%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
    </form>

</body>
</html>
