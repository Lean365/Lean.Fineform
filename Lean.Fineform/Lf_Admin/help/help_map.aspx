<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="help_map.aspx.cs" Inherits="Fine.Lf_Admin.help.help_map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <!-- reference your copy Font Awesome here (from our CDN or by hosting yourself) -->
    <link href="~/Lf_Resources/fontawesome/css/fontawesome.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/fontawesome.min.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/brands.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/solid.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/svg-with-js.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/v4-shims.css" rel="stylesheet" />
    <link href="~/Lf_Resources/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Lf_Resources/css/map.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" IsFluid="false" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            BodyPadding="1px" ShowHeader="false" Title="" AutoScroll="false" Margin="1px">
            <Items>
                <f:ContentPanel runat="server" ShowHeader="false">
                    <div class="w-100" style="margin-bottom: 20px"></div>
                    <div class="container">
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-4 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Help_Htmlcolor" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/htmlcolor.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,sys_Help_Htmlcolor%>" OnClick="Btn_sys_Help_Htmlcolor_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Help_Encryption" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/encryption.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,sys_Help_Encryption%>" OnClick="Btn_sys_Help_Encryption_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Help_FontIcon" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/fonticon.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,sys_Help_FontIcon%>" OnClick="Btn_sys_Help_FontIcon_Click">
                                </f:Button>
                            </div>

                        </div>
                    </div>
                    <div class="w-100" style="margin-bottom: 20px"></div>
                    <div class="container">
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-4 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Help_Random" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/random.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,sys_Help_Random%>" OnClick="Btn_sys_Help_Random_Click">
                                </f:Button>
                            </div>
                            <%--<div class="w-100" style="margin-bottom: 20px"></div>--%>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Help_Wiki" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/world.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,sys_Help_Wiki%>" OnClick="Btn_sys_Help_Wiki_Click">
                                </f:Button>
                            </div>
                            <%--<div class="w-100" style="margin-bottom: 20px"></div>--%>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Help_Manual" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/help.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Help%>" OnClick="Btn_sys_Help_Manual_Click">
                                </f:Button>
                            </div>
                            <%--<div class="w-100" style="margin-bottom: 20px"></div>--%>
                        </div>
                    </div>
                </f:ContentPanel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
