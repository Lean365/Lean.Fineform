<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_badcategory_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.Master.Pp_badcategory_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>" >
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveClose%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                     Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label runat="server" ID="ngclass" Label="不良类别" ShowRedStar="True" >
                                </f:Label>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="cn_classmatter"  FocusOnPageLoad="true" Label="名称CN" Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="en_classmatter"  Label="名称EN"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="jp_classmatter"  Label="名称JP"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="ngcode" Label="不良代码" ShowRedStar="True" >
                                </f:Label>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="cn_ngmatter"    Label="名称CN"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="en_ngmatter"    Label="名称EN"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="jp_ngmatter"    Label="名称JP"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="analysisclass"    Label="分析代码"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow11" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="jp_class"    Label="分析类别"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow12" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="jp_ng"    Label="分析名称"  Required="True" ShowRedStar="True" >
                                </f:TextBox>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                             
                                <f:TextBox ID="remark" runat="server" Label="备注">
                                </f:TextBox>                                                                                                                         
                            </Items>
                        </f:FormRow>

                        
                  </Rows>
                </f:Form>
        <f:Label ID="labResult" EncodeText="false" runat="server">
        </f:Label>
            </Items>
        </f:Panel>
        <f:HiddenField ID="hfSelectedDept" runat="server">
        </f:HiddenField>

        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>
        
</body>
</html>
