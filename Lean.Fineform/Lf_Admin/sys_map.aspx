<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_map.aspx.cs" Inherits="LeanFine.Lf_Admin.sys_map" %>

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
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_menu" EnablePostBack="true" IconAlign="Top" IconFontClass="fab fa-buromobelexperte" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Menu%>" OnClick="Btn_Adm_menu_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_online" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Online%>" OnClick="Btn_Adm_online_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_config" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-cogs" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Config%>" OnClick="Btn_Adm_config_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_log" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-file-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Log%>" OnClick="Btn_Adm_log_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_operatelog" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-file-code" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Work_Log%>" OnClick="Btn_Adm_operatelog_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_Institution" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-building" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Company%>" OnClick="Btn_Adm_Institution_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_dept" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-bars" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Dept%>" OnClick="Btn_Adm_dept_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_dept_user" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-plus" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_DeptUser%>" OnClick="Btn_Adm_dept_user_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_title" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-graduate" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Title%>" OnClick="Btn_Adm_title_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_title_user" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-cog" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_TitleUser%>" OnClick="Btn_Adm_title_user_Click">
                                </f:Button>
                            </div>
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_user" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-users" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_User%>" OnClick="Btn_Adm_user_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_changepassword" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-exchange-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_ChangePassword%>" OnClick="Btn_Adm_changepassword_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_role" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-shield" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Role%>" OnClick="Btn_Adm_role_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_role_user" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-lock" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_RoleUser%>" OnClick="Btn_Adm_role_user_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_power" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-circle" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_Power%>" OnClick="Btn_Adm_power_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Adm_role_power" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-user-check" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sys_PowerRole%>" OnClick="Btn_Adm_role_power_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_bpm_todolist" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-toggle-on" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_BPM_Todo%>" OnClick="Btn_bpm_todolist_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_bpm_apply" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-tasks" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_BPM_Apply%>" OnClick="Btn_bpm_apply_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_bpm_desgin" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-sitemap" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_BPM_Desgin%>" OnClick="Btn_bpm_desgin_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_bpm_template" EnablePostBack="true" IconAlign="Top" IconFontClass="fab fa-elementor" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_BPM_Template%>" OnClick="Btn_bpm_template_Click">
                                </f:Button>
                            </div>
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_bpm_signature" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-signature" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_BPM_Signature%>" OnClick="Btn_bpm_signature_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_em_event" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-list" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_EM_My%>" OnClick="Btn_em_event_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_em_schedule" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-calendar-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_EM_MGT%>" OnClick="Btn_em_schedule_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_finance" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-chart-bar" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_SalesData%>" OnClick="Btn_Fico_finance_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_budget" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-yen-sign" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_MGT%>" OnClick="Btn_Fico_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_period" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-window-restore" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Period%>" OnClick="Btn_Fico_period_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_subject" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-coins" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Accounts%>" OnClick="Btn_Fico_subject_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_expense" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-compress" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Expenses%>" OnClick="Btn_sys_Tab_Fico_Expense_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_labor" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-code-branch" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Labor%>" OnClick="Btn_Fico_labor_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_workovertime" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-calendar-week" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Overtime%>" OnClick="Btn_Fico_workovertime_Click">
                                </f:Button>
                            </div>
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_asset" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-wallet" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Asset%>" OnClick="Btn_Fico_asset_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_Countersignature" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-pen-nib" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Countersignature%>" OnClick="Btn_Fico_Countersignature_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Fico_audit" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-stamp" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Fico_Audit%>" OnClick="Btn_Fico_audit_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Mm_material" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-leaf" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Mm_MGT%>" OnClick="Btn_Mm_material_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Mm_material_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-newspaper" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Mm_Inventory%>" OnClick="Btn_Mm_material_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_Pp_line" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-sitemap" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Line%>" OnClick="Btn_Pp_Pp_line_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_Pp_order" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-folder-open" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_MO%>" OnClick="Btn_Pp_Pp_order_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_Pp_manhour" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-clock" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Manhours%>" OnClick="Btn_Pp_Pp_manhour_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_Pp_Reason" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-window-restore" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_ReasonType%>" OnClick="Btn_Pp_Pp_Reason_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_qm_acceptcat" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-info" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_InspectionType%>" OnClick="Btn_Pp_qm_acceptcat_Click">
                                </f:Button>
                            </div>
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_Pp_Transport" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-shipping-fast" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sd_TransportationMethods%>" OnClick="Btn_Pp_Pp_Transport_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_Pp_efficiency" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-percent" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_efficiency%>" OnClick="Btn_Pp_Pp_efficiency_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-exchange-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Ec_Carryout%>" OnClick="Btn_Ec_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_sys_Status_Ec__Lot" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-vector-square" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Ec_Putinto%>" OnClick="Btn_Pp_sys_Status_Ec__Lot_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_view" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_TE%>" OnClick="Btn_Ec_view_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_pd" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map-marked-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_PM%>" OnClick="Btn_Ec_pd_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_pm" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_PD%>" OnClick="Btn_Ec_pm_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_qc" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map-marked-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_IQC%>" OnClick="Btn_Ec_qc_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_mm" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_MM%>" OnClick="Btn_Ec_Mm_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_p2d" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map-marked-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_P2D%>" OnClick="Btn_Ec_p2d_Click">
                                </f:Button>
                            </div>
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_p1d" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_P1D%>" OnClick="Btn_Ec_p1d_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_qa" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map-marked-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,co_Dept_QA%>" OnClick="Btn_Ec_qa_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_balance" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-map" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Ec_OldStock%>" OnClick="Btn_Ec_balance_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_sap" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-list" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Ec_SAP_Data%>" OnClick="Btn_Ec_sap_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Ec_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-search" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Ec_Query%>" OnClick="Btn_Ec_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_daily" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-list-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_OPH%>" OnClick="Btn_Pp_daily_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_daily_actual_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-building" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_OPH_Actual%>" OnClick="Btn_Pp_daily_actual_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_output_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-search" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_OPH_Query%>" OnClick="Btn_Pp_output_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_output_order_finish" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-toilet-paper" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_ProducingProgress%>" OnClick="Btn_Pp_output_order_finish_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_output_opt" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-th-list" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_OPH_Report%>" OnClick="Btn_Pp_output_opt_Click">
                                </f:Button>
                            </div>
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_pp_defect" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-bug" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_pp_defect_Production%>" OnClick="Btn_pp_defect_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_pp_defect_order_totalled" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-th-list" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_pp_defect_MOStatistics%>" OnClick="Btn_pp_defect_order_totalled_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_pp_defect_lot_finished" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-th-large" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_pp_defect_LotStatistics%>" OnClick="Btn_pp_defect_lot_finished_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_pp_defect_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-search" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_pp_defect_Query%>" OnClick="Btn_pp_defect_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_times_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-search" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_Times_Query%>" OnClick="Btn_Pp_times_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Qm_fqc" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-check-double" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_FQC%>" OnClick="Btn_Qm_fqc_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Qm_fqc_notice" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-sticky-note" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_FQC_UnqualifiedNotice%>" OnClick="Btn_Qm_fqc_notice_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Qm_fqc_action" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-window-maximize" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_FQC_AnalysisStrategy%>" OnClick="Btn_Qm_fqc_action_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Qm_fqc_count" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-drafting-compass" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_FQC_PassReport%>" OnClick="Btn_Qm_fqc_count_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Qm_fqc_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-search" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_FQC_Query%>" OnClick="Btn_Qm_fqc_Query_Click">
                                </f:Button>
                            </div>
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Button_New_Qm_Rework_cost" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-retweet" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_QC_Rework%>" OnClick="Btn_sys_Button_New_Qm_Rework_cost_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Button_New_Qm_Operation_cost" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-route" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_QC_Operation%>" OnClick="Btn_sys_Button_New_Qm_Operation_cost_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Button_New_Qm_Waste_cost" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-trash-alt" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Qm_QC_Waste%>" OnClick="Btn_sys_Button_New_Qm_Waste_cost_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Sd_forecast" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-building" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Sd_PlannedOrder%>" OnClick="Btn_Sd_forecast_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_inbound_scan" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-clipboard-list" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_InboundSerial%>" OnClick="Btn_Pp_inbound_scan_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_outbound_scan" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-outdent" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_OutboundSerial%>" OnClick="Btn_Pp_outbound_scan_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_inbound_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-indent" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_InboundQuery%>" OnClick="Btn_Pp_inbound_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Pp_outbound_query" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-search" runat="server"
                                    Text="<%$ Resources:GlobalResource,menu_Pp_OutboundQuery%>" OnClick="Btn_Pp_outbound_Query_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_Charts" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-chart-bar" runat="server"
                                    Text="<%$ Resources:GlobalResource,sys_Charts%>" OnClick="Btn_sys_Charts_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_sys_help" EnablePostBack="true" IconAlign="Top" IconFontClass="fas fa-question" runat="server"
                                    Text="<%$ Resources:GlobalResource,sys_Help%>" OnClick="Btn_sys_help_Click">
                                </f:Button>
                            </div>

                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-8 align-self-center">
                        </div>
                        <!-- Force next columns to break to new line at md breakpoint and up -->
                        <div class="w-100" style="margin-bottom: 20px"></div>
                    </div>
                </f:ContentPanel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
