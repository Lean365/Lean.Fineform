<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_manhour_new.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.Master.Pp_manhour_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"  />
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
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                   
                                 <f:DatePicker runat="server"  Label="更新日期"  FocusOnPageLoad="true"   ID="Prodate" ShowRedStar="True"  DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" LabelAlign="Right" Required="True" Readonly="True">
                                </f:DatePicker>                      
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items> 
                                 <f:Label runat="server"  Label="生产工厂"   ID="Proplnt" ShowRedStar="true" LabelAlign="Right" Text="C100">
                                </f:Label>                                                                                              
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TextBox runat="server"  Label="生产物料" ID="Proitem" ShowRedStar="true" LabelAlign="Right" >
                                </f:TextBox>
                                 <f:TextBox runat="server" ID="Protext" Label="物料文本" ShowRedStar="True" LabelAlign="Right">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:TextBox runat="server"  Label="生产机种"   ID="Promodel" ShowRedStar="True" LabelAlign="Right">
                                </f:TextBox>  

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:TextBox runat="server"  Label="工作中心"   ID="Prowcname" ShowRedStar="true"   LabelAlign="Right" >
                                </f:TextBox>
                                <f:TextBox runat="server"  Label="工作文本"   ID="Prowctext" ShowRedStar="True" LabelAlign="Right">
                                </f:TextBox> 

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:NumberBox runat="server"  Label="Short数"   ID="Proshort" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                                <f:Label runat="server"  Label="单位"   ID="Propset" ShowRedStar="True"  LabelAlign="Right" Text="P">
                                </f:Label>


                                                                      
                            </Items>
                        </f:FormRow>
                            <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:NumberBox runat="server"  Label="换算汇率"   ID="Prorate" NoDecimal="false" NoNegative="True" DecimalPrecision="5" Required="True" ShowRedStar="True" LabelAlign="Right" RegexPattern="NUMBER" EmptyText="0">
                                </f:NumberBox>
                                                                      
                            </Items>
                        </f:FormRow>                    
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                                <f:NumberBox runat="server" ID="Prost" Label="ST" ShowRedStar="True" NoDecimal="false" NoNegative="True" MinValue="0" MaxValue="999" DecimalPrecision="2" Required="True" LabelAlign="Right">
                                </f:NumberBox>
                                 <f:Label runat="server"  Label="单位"   ID="Proset" ShowRedStar="True"  LabelAlign="Right" Text="MIN">
                                </f:Label>
                                                                      
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                 <f:Label runat="server" ID="Prodesc" Label="仕向地" ShowRedStar="True" LabelAlign="Right" >
                                </f:Label>                                                       
                                <f:TextBox ID="Remark" runat="server" Label="备注" LabelAlign="Right">
                                </f:TextBox>                                                                                                                         
                            </Items>
                        </f:FormRow>                        
                  </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>
        
</body>
</html>


