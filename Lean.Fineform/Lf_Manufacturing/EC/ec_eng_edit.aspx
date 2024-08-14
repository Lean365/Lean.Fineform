<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="ec_eng_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.ec_eng_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-editMm {
            background-color: #b3e19d;
            color: #fff;
        }

        .f-grid-row .f-grid-cell-editPcba {
            background-color: #f3d19e;
            color: #fff;
        }

        .f-grid-row .f-grid-cell-editAssy {
            background-color: #a0cfff;
            color: #fff;
        }

        .f-grid-row .f-grid-cell-editPe {
            background-color: #fab6b6;
            color: #fff;
        }

        .f-grid-row .f-grid-cell-editPcba a,
        .f-grid-row .f-grid-cell-editPcba a:hover {
            color: #fff;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AjaxTimeout="3600" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" TabPosition="Top" ActiveTabIndex="0" AutoScroll="true"
            runat="server">
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
                <f:Tab Title="设变信息" BodyPadding="10px" runat="server">
                    <Items>
                        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server" Title="技术部">

                            <Items>
                                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                                    Title="SimpleForm">
                                    <Rows>
                                        <f:FormRow ID="FormRow2" runat="server">
                                            <Items>
                                                <f:Label runat="server" Label="发行日期" ID="Ec_issuedate" ShowRedStar="True">
                                                </f:Label>
                                                <f:Label runat="server" ID="Ec_no" Label="设变号码" ShowRedStar="True">
                                                </f:Label>
                                                <f:Label ID="Ec_model" runat="server" Label="机种名" ShowRedStar="True" Text="-">
                                                </f:Label>
                                                <f:Label ID="Ec_title" runat="server" Label="标题" ShowRedStar="True" Text="-">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow1" runat="server">
                                            <Items>
                                                <f:Label ID="mReason" runat="server" Label="设变(主)" ShowRedStar="True" Text="-">
                                                </f:Label>
                                                <f:Label ID="sReason" runat="server" Label="设变(次)" ShowRedStar="True" Text="-">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <%--                                        <f:FormRow ID="FormRow9" runat="server">
                                            <Items>
                                                <f:RadioButtonList ID="IsSopUpdate" Label="SOP确认" ColumnNumber="2" runat="server"
                                                    AutoPostBack="true" OnSelectedIndexChanged="IsSopUpdate_SelectedIndexChanged"
                                                    ShowRedStar="true" Required="true">
                                                    <f:RadioItem Text="是" Value="1" Selected="true" />
                                                    <f:RadioItem Text="否" Value="0" />
                                                </f:RadioButtonList>
                                                <f:RadioButtonList ID="IsManage" Label="制二课管理" ColumnNumber="2" runat="server"
                                                    AutoPostBack="true" OnSelectedIndexChanged="IsManage_SelectedIndexChanged"
                                                    ShowRedStar="true" Required="true">
                                                    <f:RadioItem Text="是" Value="1" />
                                                    <f:RadioItem Text="否" Value="0" Selected="true" />
                                                </f:RadioButtonList>
                                            </Items>
                                        </f:FormRow>--%>
                                        <f:FormRow ID="FormRow4" runat="server">
                                            <Items>
                                                <f:Label ID="Ec_detailstent" runat="server" Label="设变内容" Height="150px">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow7" runat="server">
                                            <Items>
                                                <f:DropDownList runat="server" ID="Ec_distinction" Label="管理区分" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>">
                                                </f:DropDownList>
                                                <f:DropDownList runat="server" ID="Ec_leader" Label="技术担当" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>">
                                                </f:DropDownList>
                                                <f:Label runat="server" ID="oldEc_documents" Label="附件" Text="-"></f:Label>
                                                <f:FileUpload runat="server" ID="Ec_documents" ButtonText="更新附件" EmptyText="SAP设变PDF" Label="更新附件"
                                                    ShowRedStar="true">
                                                </f:FileUpload>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow runat="server">
                                            <Items>
                                                <f:DropDownList runat="server" ID="ddlPbook" Label="编码" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" AutoPostBack="True" OnSelectedIndexChanged="ddlPbook_SelectedIndexChanged">
                                                </f:DropDownList>
                                                <f:TextBox ID="Ec_letterno" Label="技联NO" ShowRedStar="true" runat="server" EmptyText="9999" RegexPattern="ALPHA_NUMERIC" MaxLength="9999">
                                                </f:TextBox>
                                                <f:Label runat="server" ID="oldEc_letterdoc" Label="附件" Text="-"></f:Label>
                                                <f:FileUpload runat="server" ID="Ec_letterdoc" ButtonText="更新附件" EmptyText="更新附件" Label="更新附件"
                                                    ShowRedStar="true">
                                                </f:FileUpload>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow runat="server">
                                            <Items>
                                                <f:DropDownList runat="server" ID="ddlp" Label="编码" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>">
                                                    <f:ListItem Text="P-" Value="1" Selected="true" />
                                                </f:DropDownList>
                                                <f:TextBox ID="Ec_eppletterno" Label="P番联络书" ShowRedStar="true" runat="server" EmptyText="9999" RegexPattern="ALPHA_NUMERIC" MaxLength="9999">
                                                </f:TextBox>
                                                <f:Label runat="server" ID="oldEc_eppletterdoc" Label="附件" Text="-"></f:Label>
                                                <f:FileUpload runat="server" ID="Ec_eppletterdoc" ButtonText="更新附件" EmptyText="更新附件" Label="更新附件"
                                                    ShowRedStar="true">
                                                </f:FileUpload>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow runat="server">
                                            <Items>
                                                <f:DropDownList runat="server" ID="ddlptcj" Label="编码" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>">
                                                    <f:ListItem Text="P-" Value="1" Selected="true" />
                                                </f:DropDownList>
                                                <f:TextBox ID="Ec_teppletterno" Label="P番(TCJ)" ShowRedStar="True" runat="server" EmptyText="9999" RegexPattern="ALPHA_NUMERIC" MaxLength="9999">
                                                </f:TextBox>
                                                <f:Label runat="server" ID="oldEc_teppletterdoc" Label="附件" Text="-"></f:Label>
                                                <f:FileUpload runat="server" ID="Ec_teppletterdoc" ButtonText="选择P番PDF" EmptyText="选择附件" Label="添加PDF"
                                                    ShowRedStar="true">
                                                </f:FileUpload>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow3" runat="server">
                                            <Items>
                                                <f:HtmlEditor ID="Ec_details" runat="server" Editor="UMEditor" BasePath="~/Lf_Resources/umeditor/" Label="翻译说明" Height="150px" Text="-">
                                                </f:HtmlEditor>
                                            </Items>
                                        </f:FormRow>


                                        <f:FormRow ID="FormRow5" runat="server">
                                            <Items>
                                                <f:TextBox ID="Ec_lossamount" Label="仕损金额" runat="server" Text="-">
                                                </f:TextBox>
                                                <f:Label ID="Ec_status" runat="server" Label="状态">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="FormRow6" runat="server">
                                            <Items>
                                                <f:Label ID="MemoText" runat="server" EncodeText="false">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
                <f:Tab Title="物料确认" BodyPadding="10px" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                            EnableCheckBoxSelect="false" ForceFit="false" ColumnWidth="100px"
                            DataKeyNames="Ec_no,Ec_model,Ec_bomitem" AllowSorting="true" OnSort="Grid1_Sort" SortField="Ec_bomitem"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" EnableTextSelection="true"
                            OnPageIndexChange="Grid1_PageIndexChange" OnRowDataBound="Grid1_RowDataBound"
                            OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                        <f:DropDownList ID="DDL_Item" AutoPostBack="true" OnSelectedIndexChanged="DDL_Item_SelectedIndexChanged"
                                            runat="server">
                                        </f:DropDownList>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                                </f:ToolbarText>
                                <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                                    runat="server">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="50" Value="50" />
                                    <f:ListItem Text="100" Value="100" />
                                </f:DropDownList>
                            </PageItems>
                            <Columns>
                                <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,co_Dept_MM%>" Width="100px" Icon="TableEdit" ColumnID="editMm" CommandName="EditMm" Text="<%$ Resources:GlobalResource,sys_Button_Unrelated%>" ConfirmTarget="Top" ConfirmText="确实定要设为与<部管课无关>吗？" />
                                <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,co_Dept_P2D%>" Width="100px" Icon="TableEdit" ColumnID="editPcba" CommandName="EditPcba" Text="<%$ Resources:GlobalResource,sys_Button_Unrelated%>" ConfirmTarget="Top" ConfirmText="确定要设为与<制二课无关>吗？" />
                                <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,co_Dept_P1D%>" Width="100px" Icon="TableEdit" ColumnID="editAssy" CommandName="EditAssy" Text="<%$ Resources:GlobalResource,sys_Button_Unrelated%>" ConfirmTarget="Top" ConfirmText="确定要设为与<组立无关>吗？" />
                                <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,co_Dept_PE%>" Width="100px" Icon="TableEdit" ColumnID="editPe" CommandName="EditPe" Text="<%$ Resources:GlobalResource,menu_Pp_Sop%>" ConfirmTarget="Top" ConfirmText="确定不用更新SOP吗？" />
                                <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Width="100px" Icon="delete" ColumnID="editDel" CommandName="EditDel" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>" />
                                <f:BoundField DataField="IsManage" SortField="IsManage" Width="120px" HeaderText="管理" />
                                <f:BoundField DataField="IsMmManage" SortField="IsMmManage" Width="120px" HeaderText="部管" />
                                <f:BoundField DataField="IsPcbaManage" SortField="IsPcbaManage" Width="120px" HeaderText="制二" />
                                <f:BoundField DataField="IsAssyManage" SortField="IsAssyManage" Width="120px" HeaderText="组立" />
                                <f:RowNumberField Width="35px" EnablePagingNumber="true" />
                                <f:BoundField DataField="Ec_no" ColumnID="Ec_no" SortField="Ec_no" EnableLock="true" Width="100px" HeaderText="设变号码" />
                                <f:BoundField DataField="Ec_model" ColumnID="Ec_model" SortField="Ec_model" EnableLock="true" Width="150px" HeaderText="机种" />
                                <f:BoundField DataField="Ec_bomitem" ColumnID="Ec_bomitem" SortField="Ec_bomitem" EnableLock="true" Width="150px" HeaderText="完成品" />
                                <f:BoundField DataField="Ec_bomsubitem" ColumnID="Ec_bomsubitem" SortField="Ec_bomsubitem" EnableLock="true" Width="150px" HeaderText="上阶品号" />
                                <f:BoundField DataField="Ec_olditem" ColumnID="Ec_olditem" SortField="Ec_olditem" EnableLock="true" Width="150px" HeaderText="旧品号" />
                                <f:BoundField DataField="Ec_oldtext" ColumnID="Ec_oldtext" SortField="Ec_oldtext" EnableLock="true" Width="100px" HeaderText="品名" />
                                <f:BoundField DataField="Ec_oldqty" ColumnID="Ec_oldqty" SortField="Ec_oldqty" EnableLock="true" Width="100px" HeaderText="数量" />
                                <f:BoundField DataField="Ec_oldset" ColumnID="Ec_oldset" SortField="Ec_oldset" EnableLock="true" Width="100px" HeaderText="位置" />
                                <f:BoundField DataField="Ec_newitem" ColumnID="Ec_newitem" SortField="Ec_newitem" EnableLock="true" Width="150px" HeaderText="新品号" />
                                <f:BoundField DataField="Ec_newtext" ColumnID="Ec_newtext" SortField="Ec_newtext" DataToolTipField="Ec_newtext" EnableLock="true" Width="100px" HeaderText="品名" />
                                <f:BoundField DataField="Ec_newqty" ColumnID="Ec_newqty" SortField="Ec_newqty" EnableLock="true" Width="100px" HeaderText="数量" />
                                <f:BoundField DataField="Ec_newset" ColumnID="Ec_newset" SortField="Ec_newset" EnableLock="true" Width="100px" HeaderText="位置" />
                                <f:BoundField DataField="Ec_bomno" ColumnID="Ec_bomno" SortField="Ec_bomno" EnableLock="true" Width="100px" HeaderText="番号" />
                                <f:BoundField DataField="Ec_procurement" ColumnID="Ec_procurement" SortField="Ec_procurement" EnableLock="true" Width="100px" HeaderText="采购类别" />
                                <f:BoundField DataField="Ec_location" ColumnID="Ec_location" SortField="Ec_location" EnableLock="true" Width="100px" HeaderText="存储位置" />
                                <f:BoundField DataField="IsCheck" ColumnID="IsCheck" SortField="IsCheck" EnableLock="true" Width="100px" HeaderText="QC检查" />



                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Tab>
            </Tabs>

        </f:TabStrip>
    </form>

</body>
</html>

