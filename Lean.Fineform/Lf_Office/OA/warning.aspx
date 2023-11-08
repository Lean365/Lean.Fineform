<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="warning.aspx.cs" Inherits="Lean.Fineform.Lf_Office.OA.warning" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .f-window.f-window-maximized {
            margin: 50px;
            border-width: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />

        <f:Window ID="Window1" Width="650px" Height="450px" Icon="TagBlue" Title=""
            EnableCollapse="false" runat="server" EnableResize="true" IconUrl="~/Lf_Resources/menu/warning.png"
            IsModal="false" AutoScroll="true" BodyPadding="10px"
            EnableMaximize="false" Maximized="false" EnableClose="false">
            <Content>
                <%--<f:TextArea runat="server" ID="TextArea1" EmptyText="" Label="" CssStyle="" Width="400px"
                    AutoGrowHeight="true" AutoGrowHeightMin="300" AutoGrowHeightMax="300">
                </f:TextArea>--%>
                <p style="color: red">
                    <div style="color: red">
                        <h4>注意事项</h4>
                        <ol>
                            <li>此联系人搜索数据库中包含的所有信息都是保密的。 </li>
                            <li>请严格遵守相关制度。（就业规则、个人信息管理规程及其他相关规则），同意不得擅自向第三方泄漏。</li>

                        </ol>
                    </div>

                </p>
                <p style="color: red">
                    <div style="color: red">
                         <h4>注意事項</h4>
                        <ol>
                            <li>この連絡先検索データベースに収録されている内容は、すべて社外秘（ティアックグループ外秘）となります。 </li>
                            <li>本連絡先検索にて取得した情報の取扱には細心の注意を払い、就業規則・個人情報管理規程その他関連規則を遵守し、会社に無断で第三者に漏洩しないことに同意いただける場合に限り、本連絡先検索を利用することができます。</li>

                        </ol>
                    </div>
                </p>

            </Content>
            <Toolbars>

                <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Right">
                    <Items>
                        <f:Button Text="<%$ Resources:GlobalResource,sys_Iagree%>" ID="Btn_Agree" runat="server" OnClick="Btn_Agree_Click" CssStyle="color:red">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
    </form>
    <script>
        function alertAndRedirect(message, redirectUrl) {
            F.alert({
                
                message: message,
                ok: function () {
                    window.location.href = redirectUrl;
                }
            });
        }

    </script>
</body>
</html>
