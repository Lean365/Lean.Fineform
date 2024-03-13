using FineUIPro;
using System;

namespace LeanFine.Lf_Admin
{
    public partial class sys_map : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreSysView";
            }
        }

        #endregion ViewPower

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            CheckPowerWithButton("CoreMenuView", Btn_Adm_menu);
            CheckPowerWithButton("CoreOnlineView", Btn_Adm_online);
            CheckPowerWithButton("CoreConfigView", Btn_Adm_config);
            CheckPowerWithButton("CoreLogView", Btn_Adm_log);
            CheckPowerWithButton("CoreLogView", Btn_Adm_operatelog);
            CheckPowerWithButton("CoreCompanyView", Btn_Adm_Institution);
            CheckPowerWithButton("CoreDeptView", Btn_Adm_dept);
            CheckPowerWithButton("CoreDeptUserView", Btn_Adm_dept_user);
            CheckPowerWithButton("CoreTitleView", Btn_Adm_title);
            CheckPowerWithButton("CoreTitleUserView", Btn_Adm_title_user);
            CheckPowerWithButton("CoreUserView", Btn_Adm_user);
            CheckPowerWithButton("CoreUserChangePassword", Btn_Adm_changepassword);
            CheckPowerWithButton("CoreRoleView", Btn_Adm_role);
            CheckPowerWithButton("CoreRoleUserView", Btn_Adm_role_user);
            CheckPowerWithButton("CorePowerView", Btn_Adm_power);
            CheckPowerWithButton("CoreRolePowerView", Btn_Adm_role_power);
            CheckPowerWithButton("CoreBpmTodoView", Btn_bpm_todolist);
            CheckPowerWithButton("CoreBpmApplyView", Btn_bpm_apply);
            CheckPowerWithButton("CoreBpmDesginView", Btn_bpm_desgin);
            CheckPowerWithButton("CoreBpmTemplateView", Btn_bpm_template);
            CheckPowerWithButton("CoreBpmSignatureView", Btn_bpm_signature);
            CheckPowerWithButton("CoreEventView", Btn_em_event);
            CheckPowerWithButton("CoreEventView", Btn_em_schedule);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_finance);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_budget);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_period);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_subject);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_expense);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_labor);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_workovertime);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_asset);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_Countersignature);
            CheckPowerWithButton("CoreBudgetView", Btn_Fico_audit);
            CheckPowerWithButton("CoreMaterialView", Btn_Mm_material);
            CheckPowerWithButton("CoreMaterialView", Btn_Mm_material_query);
            CheckPowerWithButton("CoreLineView", Btn_Pp_Pp_line);
            CheckPowerWithButton("CoreOrderView", Btn_Pp_Pp_order);
            CheckPowerWithButton("CoreManhourView", Btn_Pp_Pp_manhour);
            CheckPowerWithButton("CoreNotReachedView", Btn_Pp_Pp_Reason);
            CheckPowerWithButton("CoreInspectCatView", Btn_Pp_qm_acceptcat);
            CheckPowerWithButton("CoreTransportView", Btn_Pp_Pp_Transport);
            CheckPowerWithButton("CoreUtilizationView", Btn_Pp_Pp_efficiency);
            CheckPowerWithButton("CoreEcView", Btn_Ec_);
            CheckPowerWithButton("CoreEcView", Btn_Pp_sys_Status_Ec__Lot);
            CheckPowerWithButton("CoreEcView", Btn_Ec_view);
            CheckPowerWithButton("CoreEcView", Btn_Ec_pd);
            CheckPowerWithButton("CoreEcView", Btn_Ec_pm);
            CheckPowerWithButton("CoreEcView", Btn_Ec_qc);
            CheckPowerWithButton("CoreEcView", Btn_Ec_mm);
            CheckPowerWithButton("CoreEcView", Btn_Ec_p2d);
            CheckPowerWithButton("CoreEcView", Btn_Ec_p1d);
            CheckPowerWithButton("CoreEcView", Btn_Ec_qa);
            CheckPowerWithButton("CoreEcView", Btn_Ec_balance);
            CheckPowerWithButton("CoreEcView", Btn_Ec_sap);
            CheckPowerWithButton("CoreEcView", Btn_Ec_query);
            CheckPowerWithButton("CoreP2DOutputView", Btn_Pp_daily);
            CheckPowerWithButton("CoreP2DOutputView", Btn_Pp_daily_actual_query);
            CheckPowerWithButton("CoreP2DOutputView", Btn_Pp_output_query);
            CheckPowerWithButton("CoreP2DOutputView", Btn_Pp_output_order_finish);
            CheckPowerWithButton("CoreP2DOutputView", Btn_Pp_output_opt);
            CheckPowerWithButton("CoreP1DDefectView", Btn_pp_defect);
            CheckPowerWithButton("CoreP1DDefectView", Btn_pp_defect_order_totalled);
            CheckPowerWithButton("CoreP1DDefectView", Btn_pp_defect_lot_finished);
            CheckPowerWithButton("CoreP1DDefectView", Btn_pp_defect_query);
            CheckPowerWithButton("CoreP1DDefectView", Btn_Pp_times_query);
            CheckPowerWithButton("CoreFqcActionView", Btn_Qm_fqc);
            CheckPowerWithButton("CoreFqcActionView", Btn_Qm_fqc_notice);
            CheckPowerWithButton("CoreFqcActionView", Btn_Qm_fqc_action);
            CheckPowerWithButton("CoreFqcActionView", Btn_Qm_fqc_count);
            CheckPowerWithButton("CoreFqcActionView", Btn_Qm_fqc_query);
            CheckPowerWithButton("CoreReworkCostView", Btn_sys_Button_New_Qm_Rework_cost);
            CheckPowerWithButton("CoreOperationCostView", Btn_sys_Button_New_Qm_Operation_cost);
            CheckPowerWithButton("CoreWasteCostView", Btn_sys_Button_New_Qm_Waste_cost);
            CheckPowerWithButton("CoreForecastView", Btn_Sd_forecast);
            CheckPowerWithButton("CoreInboundScanView", Btn_Pp_inbound_scan);
            CheckPowerWithButton("CoreOutboundScanView", Btn_Pp_outbound_scan);
            CheckPowerWithButton("CoreInboundScanView", Btn_Pp_inbound_query);
            CheckPowerWithButton("CoreOutboundScanView", Btn_Pp_outbound_query);
            CheckPowerWithButton("CoreLogView", Btn_sys_help);
            CheckPowerWithButton("CoreKitOutput", Btn_sys_Charts);
            //btnConfig.OnClientClick = Window1.GetShowReference("~/Lf_Admin/dept_new.aspx", "新增部门");
        }

        #region event

        protected void Btn_Adm_menu_Click(object sender, EventArgs e)
        {
            // 添加示例标签页
            // tabOptions: 选项卡参数
            // tabOptions.id： 选项卡ID
            // tabOptions.iframeUrl: 选项卡IFrame地址
            // tabOptions.title： 选项卡标题
            // tabOptions.icon： 选项卡图标
            // tabOptions.createToolbar： 创建选项卡前的回调函数（接受tabOptions参数）
            // tabOptions.refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // tabOptions.iconFont： 选项卡图标字体
            // tabOptions.activeIt： 是否激活当前添加的选项卡
            // actived: 是否激活选项卡（默认为true）string menu_Sys_Menu= global::Resources.GlobalResource.;
            string menu_Sys_Menu = global::Resources.GlobalResource.menu_Sys_Menu;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Menu','/Cube_ADM/menu.aspx','" + menu_Sys_Menu + "', '/res/Menu/manu.png', '', true, ''); ");
        }

        protected void Btn_Adm_online_Click(object sender, EventArgs e)
        {
            string menu_Sys_Online = global::Resources.GlobalResource.menu_Sys_Online;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Online','/Cube_ADM/online.aspx','" + menu_Sys_Online + "', '/res/Menu/online.png', '', true, ''); ");
        }

        protected void Btn_Adm_config_Click(object sender, EventArgs e)
        {
            string menu_Sys_Config = global::Resources.GlobalResource.menu_Sys_Config;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Config','/Cube_ADM/config.aspx','" + menu_Sys_Config + "', '/res/Menu/config.png', '', true, ''); ");
        }

        protected void Btn_Adm_log_Click(object sender, EventArgs e)
        {
            string menu_Sys_Log = global::Resources.GlobalResource.menu_Sys_Log;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Log','/Cube_ADM/log.aspx','" + menu_Sys_Log + "', '/res/Menu/loginlog.png', '', true, ''); ");
        }

        protected void Btn_Adm_operatelog_Click(object sender, EventArgs e)
        {
            string menu_Work_Log = global::Resources.GlobalResource.menu_Work_Log;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Work_Log','/Cube_ADM/operatelog.aspx','" + menu_Work_Log + "', '/res/Menu/worklog.png', '', true, ''); ");
        }

        protected void Btn_Adm_Institution_Click(object sender, EventArgs e)
        {
            string menu_Sys_Company = global::Resources.GlobalResource.menu_Sys_Company;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Company','/Cube_ADM/company.aspx','" + menu_Sys_Company + "', '/res/Menu/company.png', '', true, ''); ");
        }

        protected void Btn_Adm_dept_Click(object sender, EventArgs e)
        {
            string menu_Sys_Dept = global::Resources.GlobalResource.menu_Sys_Dept;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Dept','/Cube_ADM/dept.aspx','" + menu_Sys_Dept + "', '/res/Menu/deptm.png', '', true, ''); ");
        }

        protected void Btn_Adm_dept_user_Click(object sender, EventArgs e)
        {
            string menu_Sys_DeptUser = global::Resources.GlobalResource.menu_Sys_DeptUser;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_DeptUser','/Cube_ADM/dept_user.aspx','" + menu_Sys_DeptUser + "', '/res/Menu/deptuser.png', '', true, ''); ");
        }

        protected void Btn_Adm_title_Click(object sender, EventArgs e)
        {
            string menu_Sys_Title = global::Resources.GlobalResource.menu_Sys_Title;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Title','/Cube_ADM/title.aspx','" + menu_Sys_Title + "', '/res/Menu/title.png', '', true, ''); ");
        }

        protected void Btn_Adm_title_user_Click(object sender, EventArgs e)
        {
            string menu_Sys_TitleUser = global::Resources.GlobalResource.menu_Sys_TitleUser;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_TitleUser','/Cube_ADM/title_user.aspx','" + menu_Sys_TitleUser + "', '/res/Menu/titleuser.png', '', true, ''); ");
        }

        protected void Btn_Adm_user_Click(object sender, EventArgs e)
        {
            string menu_Sys_User = global::Resources.GlobalResource.menu_Sys_User;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_User','/Cube_ADM/user.aspx','" + menu_Sys_User + "', '/res/Menu/user.png', '', true, ''); ");
        }

        protected void Btn_Adm_changepassword_Click(object sender, EventArgs e)
        {
            string menu_Sys_ChangePassword = global::Resources.GlobalResource.menu_Sys_ChangePassword;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_ChangePassword','/Cube_ADM/changepassword.aspx','" + menu_Sys_ChangePassword + "', '/res/Menu/userset.png', '', true, ''); ");
        }

        protected void Btn_Adm_role_Click(object sender, EventArgs e)
        {
            string menu_Sys_Role = global::Resources.GlobalResource.menu_Sys_Role;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Role','/Cube_ADM/role.aspx','" + menu_Sys_Role + "', '/res/Menu/rolem.png', '', true, ''); ");
        }

        protected void Btn_Adm_role_user_Click(object sender, EventArgs e)
        {
            string menu_Sys_RoleUser = global::Resources.GlobalResource.menu_Sys_RoleUser;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_RoleUser','/Cube_ADM/role_user.aspx','" + menu_Sys_RoleUser + "', '/res/Menu/roleuser.png', '', true, ''); ");
        }

        protected void Btn_Adm_power_Click(object sender, EventArgs e)
        {
            string menu_Sys_Power = global::Resources.GlobalResource.menu_Sys_Power;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Power','/Cube_ADM/power.aspx','" + menu_Sys_Power + "', '/res/Menu/power.png', '', true, ''); ");
        }

        protected void Btn_Adm_role_power_Click(object sender, EventArgs e)
        {
            string menu_Sys_PowerRole = global::Resources.GlobalResource.menu_Sys_PowerRole;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_PowerRole','/Cube_ADM/role_power.aspx','" + menu_Sys_PowerRole + "', '/res/Menu/rolepower.png', '', true, ''); ");
        }

        protected void Btn_bpm_todolist_Click(object sender, EventArgs e)
        {
            string menu_BPM_Todo = global::Resources.GlobalResource.menu_BPM_Todo;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_BPM_Todo','/Cube_BPM/todolist.aspx','" + menu_BPM_Todo + "', '/res/menu/todo.png', '', true, ''); ");
        }

        protected void Btn_bpm_apply_Click(object sender, EventArgs e)
        {
            string menu_BPM_Apply = global::Resources.GlobalResource.menu_BPM_Apply;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_BPM_Apply','/Cube_BPM/apply.aspx','" + menu_BPM_Apply + "', '/res/menu/apply.png', '', true, ''); ");
        }

        protected void Btn_bpm_desgin_Click(object sender, EventArgs e)
        {
            string menu_BPM_Desgin = global::Resources.GlobalResource.menu_BPM_Desgin;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_BPM_Desgin','/Cube_BPM/desgin.aspx','" + menu_BPM_Desgin + "', '/res/menu/desgin.png', '', true, ''); ");
        }

        protected void Btn_bpm_template_Click(object sender, EventArgs e)
        {
            string menu_BPM_Template = global::Resources.GlobalResource.menu_BPM_Template;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_BPM_Template','/Cube_BPM/template.aspx','" + menu_BPM_Template + "', '/res/menu/template.png', '', true, ''); ");
        }

        protected void Btn_bpm_signature_Click(object sender, EventArgs e)
        {
            string menu_BPM_Signature = global::Resources.GlobalResource.menu_BPM_Signature;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_BPM_Signature','/Cube_BPM/signature.aspx','" + menu_BPM_Signature + "', '/res/menu/signature.png', '', true, ''); ");
        }

        protected void Btn_em_event_Click(object sender, EventArgs e)
        {
            string menu_EM_My = global::Resources.GlobalResource.menu_EM_My;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_EM_My','/Cube_EM/event.aspx','" + menu_EM_My + "', '/res/Menu/myevent.png', '', true, ''); ");
        }

        protected void Btn_em_schedule_Click(object sender, EventArgs e)
        {
            string menu_EM_MGT = global::Resources.GlobalResource.menu_EM_MGT;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_EM_MGT','/Cube_EM/schedule.aspx','" + menu_EM_MGT + "', '/res/Menu/event.png', '', true, ''); ");
        }

        protected void Btn_Fico_finance_Click(object sender, EventArgs e)
        {
            string menu_Sd_SlaesData = global::Resources.GlobalResource.menu_Sd_SlaesData;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sd_SlaesData','/Cube_SD/shipment/salesdata.aspx','" + menu_Sd_SlaesData + "', '/res/Menu/financem.png', '', true, ''); ");
        }

        protected void Btn_Fico_Click(object sender, EventArgs e)
        {
            string menu_Fico_MGT = global::Resources.GlobalResource.menu_Fico_MGT;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_MGT','/Cube_FICO/budget.aspx','" + menu_Fico_MGT + "', '/res/Menu/budget.png', '', true, ''); ");
        }

        protected void Btn_Fico_period_Click(object sender, EventArgs e)
        {
            string menu_Fico_Period = global::Resources.GlobalResource.menu_Fico_Period;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Period','/Cube_FICO/period.aspx','" + menu_Fico_Period + "', '/res/Menu/period.png', '', true, ''); ");
        }

        protected void Btn_Fico_subject_Click(object sender, EventArgs e)
        {
            string menu_Fico_Titles = global::Resources.GlobalResource.menu_Fico_Titles;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Titles','/Cube_FICO/subject.aspx','" + menu_Fico_Titles + "', '/res/Menu/subject.png', '', true, ''); ");
        }

        protected void Btn_sys_Tab_Fico_Expense_Click(object sender, EventArgs e)
        {
            string menu_Fico_Expenses = global::Resources.GlobalResource.menu_Fico_Expenses;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Expenses','/Cube_FICO/expense.aspx','" + menu_Fico_Expenses + "', '/res/Menu/expenses.png', '', true, ''); ");
        }

        protected void Btn_Fico_labor_Click(object sender, EventArgs e)
        {
            string menu_Fico_Labor = global::Resources.GlobalResource.menu_Fico_Labor;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Labor','/Cube_FICO/labor.aspx','" + menu_Fico_Labor + "', '/res/Menu/labor.png', '', true, ''); ");
        }

        protected void Btn_Fico_workovertime_Click(object sender, EventArgs e)
        {
            string menu_Fico_Overtime = global::Resources.GlobalResource.menu_Fico_Overtime;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Overtime','/Cube_FICO/workovertime.aspx','" + menu_Fico_Overtime + "', '/res/Menu/workovertime.png', '', true, ''); ");
        }

        protected void Btn_Fico_asset_Click(object sender, EventArgs e)
        {
            string menu_Fico_Asset = global::Resources.GlobalResource.menu_Fico_Asset;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Asset','/Cube_FICO/asset.aspx','" + menu_Fico_Asset + "', '/res/Menu/asset.png', '', true, ''); ");
        }

        protected void Btn_Fico_Countersignature_Click(object sender, EventArgs e)
        {
            string menu_Fico_Countersignature = global::Resources.GlobalResource.menu_Fico_Countersignature;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Countersignature','/Cube_FICO/Countersignature.aspx','" + menu_Fico_Countersignature + "', '/res/Menu/Countersignature.png', '', true, ''); ");
        }

        protected void Btn_Fico_audit_Click(object sender, EventArgs e)
        {
            string menu_Fico_Audit = global::Resources.GlobalResource.menu_Fico_Audit;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Fico_Audit','/Cube_FICO/audit.aspx','" + menu_Fico_Audit + "', '/res/Menu/Audit.png', '', true, ''); ");
        }

        protected void Btn_Mm_material_Click(object sender, EventArgs e)
        {
            string menu_Mm_MGT = global::Resources.GlobalResource.menu_Mm_MGT;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Mm_MGT','/Cube_MM/material.aspx','" + menu_Mm_MGT + "', '/res/Menu/materialadmin.png', '', true, ''); ");
        }

        protected void Btn_Mm_material_Query_Click(object sender, EventArgs e)
        {
            string menu_Mm_Inventory = global::Resources.GlobalResource.menu_Mm_Inventory;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Mm_Inventory','/Cube_MM/material_query.aspx','" + menu_Mm_Inventory + "', '/res/Menu/query.png', '', true, ''); ");
        }

        protected void Btn_Pp_Pp_line_Click(object sender, EventArgs e)
        {
            string menu_Pp_Line = global::Resources.GlobalResource.menu_Pp_Line;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Line','/Cube_PP/baseinfo/Pp_line.aspx','" + menu_Pp_Line + "', '/res/menu/lineteam.png', '', true, ''); ");
        }

        protected void Btn_Pp_Pp_order_Click(object sender, EventArgs e)
        {
            string menu_Pp_MO = global::Resources.GlobalResource.menu_Pp_MO;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_MO','/Cube_PP/baseinfo/Pp_order.aspx','" + menu_Pp_MO + "', '/res/icon/tag_order.png', '', true, ''); ");
        }

        protected void Btn_Pp_Pp_manhour_Click(object sender, EventArgs e)
        {
            string menu_Pp_Manhours = global::Resources.GlobalResource.menu_Pp_Manhours;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Manhours','/Cube_PP/baseinfo/Pp_manhour.aspx','" + menu_Pp_Manhours + "', '/res/icon/tag_time.png', '', true, ''); ");
        }

        protected void Btn_Pp_Pp_Reason_Click(object sender, EventArgs e)
        {
            string menu_Pp_ReasonType = global::Resources.GlobalResource.menu_Pp_ReasonType;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_ReasonType','/Cube_PP/baseinfo/Pp_Reason.aspx','" + menu_Pp_ReasonType + "', '/res/icon/tag_rtype.png', '', true, ''); ");
        }

        protected void Btn_Pp_qm_acceptcat_Click(object sender, EventArgs e)
        {
            string menu_Qm_InspectionType = global::Resources.GlobalResource.menu_Qm_InspectionType;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_InspectionType','/Cube_PP/baseinfo/qm_acceptcat.aspx','" + menu_Qm_InspectionType + "', '/res/icon/tag_qtype.png', '', true, ''); ");
        }

        protected void Btn_Pp_Pp_Transport_Click(object sender, EventArgs e)
        {
            string menu_Sd_TransportationMethods = global::Resources.GlobalResource.menu_Sd_TransportationMethods;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sd_TransportationMethods','/Cube_PP/baseinfo/Pp_Transport.aspx','" + menu_Sd_TransportationMethods + "', '/res/icon/tag_ttype.png', '', true, ''); ");
        }

        protected void Btn_Pp_Pp_efficiency_Click(object sender, EventArgs e)
        {
            string menu_Pp_efficiency = global::Resources.GlobalResource.menu_Pp_efficiency;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_efficiency','/Cube_PP/baseinfo/Pp_efficiency.aspx','" + menu_Pp_efficiency + "', '/res/icon/tag_rate.png', '', true, ''); ");
        }

        protected void Btn_Ec_Click(object sender, EventArgs e)
        {
            string menu_Pp_Ec_Carryout = global::Resources.GlobalResource.menu_Pp_Ec_Carryout;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Ec_Carryout','/Cube_PP/designchange/ec.aspx','" + menu_Pp_Ec_Carryout + "', '/res/menu/ecstatus.png', '', true, ''); ");
        }

        protected void Btn_Pp_sys_Status_Ec__Lot_Click(object sender, EventArgs e)
        {
            string menu_Pp_Ec_Putinto = global::Resources.GlobalResource.menu_Pp_Ec_Putinto;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Ec_Putinto','/Cube_PP/designchange/sys_Status_Ec__Lot.aspx','" + menu_Pp_Ec_Putinto + "', '/res/menu/lotstatus.png', '', true, ''); ");
        }

        protected void Btn_Ec_view_Click(object sender, EventArgs e)
        {
            string co_Dept_TE = global::Resources.GlobalResource.co_Dept_TE;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_TE','/Cube_PP/designchange/ec_view.aspx','" + co_Dept_TE + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_pd_Click(object sender, EventArgs e)
        {
            string co_Dept_PM = global::Resources.GlobalResource.co_Dept_PM;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_PM','/Cube_PP/designchange/ec_pd.aspx','" + co_Dept_PM + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_pm_Click(object sender, EventArgs e)
        {
            string co_Dept_PD = global::Resources.GlobalResource.co_Dept_PD;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_PD','/Cube_PP/designchange/ec_pm.aspx','" + co_Dept_PD + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_qc_Click(object sender, EventArgs e)
        {
            string co_Dept_IQC = global::Resources.GlobalResource.co_Dept_IQC;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_IQC','/Cube_PP/designchange/ec_qc.aspx','" + co_Dept_IQC + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_Mm_Click(object sender, EventArgs e)
        {
            string co_Dept_MM = global::Resources.GlobalResource.co_Dept_MM;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_MM','/Cube_PP/designchange/ec_mm.aspx','" + co_Dept_MM + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_p2d_Click(object sender, EventArgs e)
        {
            string co_Dept_P2D = global::Resources.GlobalResource.co_Dept_P2D;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_P2D','/Cube_PP/designchange/ec_p2d.aspx','" + co_Dept_P2D + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_p1d_Click(object sender, EventArgs e)
        {
            string co_Dept_P1D = global::Resources.GlobalResource.co_Dept_P1D;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_P1D','/Cube_PP/designchange/ec_p1d.aspx','" + co_Dept_P1D + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_qa_Click(object sender, EventArgs e)
        {
            string co_Dept_QA = global::Resources.GlobalResource.co_Dept_QA;
            PageContext.RegisterStartupScript("top.addExampleTab('co_Dept_QA','/Cube_PP/designchange/ec_qa.aspx','" + co_Dept_QA + "', '/res/icon/tag_ecn.png', '', true, ''); ");
        }

        protected void Btn_Ec_balance_Click(object sender, EventArgs e)
        {
            string menu_Pp_Ec_OldStock = global::Resources.GlobalResource.menu_Pp_Ec_OldStock;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Ec_OldStock','/Cube_PP/designchange/ec_balance.aspx','" + menu_Pp_Ec_OldStock + "', '/res/menu/inventory.png', '', true, ''); ");
        }

        protected void Btn_Ec_sap_Click(object sender, EventArgs e)
        {
            string menu_Pp_Ec_SAP_Data = global::Resources.GlobalResource.menu_Pp_Ec_SAP_Data;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Ec_SAP_Data','/Cube_PP/designchange/ec_sap.aspx','" + menu_Pp_Ec_SAP_Data + "', '/res/menu/sapdata.png', '', true, ''); ");
        }

        protected void Btn_Ec_Query_Click(object sender, EventArgs e)
        {
            string menu_Pp_Ec_Query = global::Resources.GlobalResource.menu_Pp_Ec_Query;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Ec_Query','/Cube_PP/designchange/ec_query.aspx','" + menu_Pp_Ec_Query + "', '/res/menu/query.png', '', true, ''); ");
        }

        protected void Btn_Pp_daily_Click(object sender, EventArgs e)
        {
            string menu_Pp_OPH = global::Resources.GlobalResource.menu_Pp_OPH;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_OPH','/Cube_PP/manufacturing/daily/daily.aspx','" + menu_Pp_OPH + "', '/res/icon/tag_dreport.png', '', true, ''); ");
        }

        protected void Btn_Pp_daily_actual_Query_Click(object sender, EventArgs e)
        {
            string menu_Pp_OPH_Actual = global::Resources.GlobalResource.menu_Pp_OPH_Actual;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_OPH_Actual','/Cube_PP/manufacturing/daily/daily_actual_query.aspx','" + menu_Pp_OPH_Actual + "', '/res/menu/actualquery.png', '', true, ''); ");
        }

        protected void Btn_Pp_output_Query_Click(object sender, EventArgs e)
        {
            string menu_Pp_OPH_Query = global::Resources.GlobalResource.menu_Pp_OPH_Query;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_OPH_Query','/Cube_PP/manufacturing/daily/output_query.aspx','" + menu_Pp_OPH_Query + "', '/res/menu/query.png', '', true, ''); ");
        }

        protected void Btn_Pp_output_order_finish_Click(object sender, EventArgs e)
        {
            string menu_Pp_ProducingProgress = global::Resources.GlobalResource.menu_Pp_ProducingProgress;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_ProducingProgress','/Cube_PP/manufacturing/daily/output_order_finish.aspx','" + menu_Pp_ProducingProgress + "', '/res/menu/push.png', '', true, ''); ");
        }

        protected void Btn_Pp_output_opt_Click(object sender, EventArgs e)
        {
            string menu_Pp_OPH_Report = global::Resources.GlobalResource.menu_Pp_OPH_Report;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_OPH_Report','/Cube_PP/manufacturing/daily/output_opt.aspx','" + menu_Pp_OPH_Report + "', '/res/menu/reportl.png', '', true, ''); ");
        }

        protected void Btn_pp_defect_Click(object sender, EventArgs e)
        {
            string menu_pp_defect_Production = global::Resources.GlobalResource.menu_pp_defect_Production;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_pp_defect_Production','/Cube_PP/manufacturing/defective/defect.aspx','" + menu_pp_defect_Production + "', '/res/icon/tag_bad.png', '', true, ''); ");
        }

        protected void Btn_pp_defect_order_totalled_Click(object sender, EventArgs e)
        {
            string menu_pp_defect_MOStatistics = global::Resources.GlobalResource.menu_pp_defect_MOStatistics;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_pp_defect_MOStatistics','/Cube_PP/manufacturing/defective/defect_order_totalled.aspx','" + menu_pp_defect_MOStatistics + "', '/res/icon/tag_line.png', '', true, ''); ");
        }

        protected void Btn_pp_defect_lot_finished_Click(object sender, EventArgs e)
        {
            string menu_pp_defect_LotStatistics = global::Resources.GlobalResource.menu_pp_defect_LotStatistics;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_pp_defect_LotStatistics','/Cube_PP/manufacturing/defective/defect_lot_finished.aspx','" + menu_pp_defect_LotStatistics + "', '/res/menu/report.png', '', true, ''); ");
        }

        protected void Btn_pp_defect_Query_Click(object sender, EventArgs e)
        {
            string menu_pp_defect_Query = global::Resources.GlobalResource.menu_pp_defect_Query;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_pp_defect_Query','/Cube_PP/manufacturing/defective/defect_query.aspx','" + menu_pp_defect_Query + "', '/res/menu/query.png', '', true, ''); ");
        }

        protected void Btn_Pp_times_Query_Click(object sender, EventArgs e)
        {
            string menu_Pp_Times_Query = global::Resources.GlobalResource.menu_Pp_Times_Query;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_Times_Query','/Cube_PP/manufacturing/timesheet/times_query.aspx','" + menu_Pp_Times_Query + "', '/res/menu/query.png', '', true, ''); ");
        }

        protected void Btn_Qm_fqc_Click(object sender, EventArgs e)
        {
            string menu_Qm_FQC = global::Resources.GlobalResource.menu_Qm_FQC;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_FQC','/Cube_QM/fqc/fqc.aspx','" + menu_Qm_FQC + "', '/res/icon/tag_qa.png', '', true, ''); ");
        }

        protected void Btn_Qm_fqc_notice_Click(object sender, EventArgs e)
        {
            string menu_Qm_FQC_UnqualifiedNotice = global::Resources.GlobalResource.menu_Qm_FQC_UnqualifiedNotice;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_FQC_UnqualifiedNotice','/Cube_QM/fqc/fqc_notice.aspx','" + menu_Qm_FQC_UnqualifiedNotice + "', '/res/icon/tag_nopass.png', '', true, ''); ");
        }

        protected void Btn_Qm_fqc_action_Click(object sender, EventArgs e)
        {
            string menu_Qm_FQC_AnalysisStrategy = global::Resources.GlobalResource.menu_Qm_FQC_AnalysisStrategy;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_FQC_AnalysisStrategy','/Cube_QM/fqc/fqc_action.aspx','" + menu_Qm_FQC_AnalysisStrategy + "', '/res/icon/tag_analysis.png', '', true, ''); ");
        }

        protected void Btn_Qm_fqc_count_Click(object sender, EventArgs e)
        {
            string menu_Qm_FQC_PassReport = global::Resources.GlobalResource.menu_Qm_FQC_PassReport;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_FQC_PassReport','/Cube_QM/fqc/fqc_count.aspx','" + menu_Qm_FQC_PassReport + "', '/res/icon/tag_linepass.png', '', true, ''); ");
        }

        protected void Btn_Qm_fqc_Query_Click(object sender, EventArgs e)
        {
            string menu_Qm_FQC_Query = global::Resources.GlobalResource.menu_Qm_FQC_Query;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_FQC_Query','/Cube_QM/fqc/fqc_query.aspx','" + menu_Qm_FQC_Query + "', '/res/menu/query.png', '', true, ''); ");
        }

        protected void Btn_sys_Button_New_Qm_Rework_cost_Click(object sender, EventArgs e)
        {
            string menu_Qm_QC_Rework = global::Resources.GlobalResource.menu_Qm_QC_Rework;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_QC_Rework','/Cube_QM/cost/rework_cost.aspx','" + menu_Qm_QC_Rework + "', '/res/menu/qcrework.png', '', true, ''); ");
        }

        protected void Btn_sys_Button_New_Qm_Operation_cost_Click(object sender, EventArgs e)
        {
            string menu_Qm_QC_Operation = global::Resources.GlobalResource.menu_Qm_QC_Operation;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_QC_Operation','/Cube_QM/cost/operation_cost.aspx','" + menu_Qm_QC_Operation + "', '/res/menu/qcbus.png', '', true, ''); ");
        }

        protected void Btn_sys_Button_New_Qm_Waste_cost_Click(object sender, EventArgs e)
        {
            string menu_Qm_QC_Waste = global::Resources.GlobalResource.menu_Qm_QC_Waste;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Qm_QC_Waste','/Cube_QM/cost/waste_cost.aspx','" + menu_Qm_QC_Waste + "', '/res/menu/qcscrap.png', '', true, ''); ");
        }

        protected void Btn_Sd_forecast_Click(object sender, EventArgs e)
        {
            string menu_Sd_PlannedOrder = global::Resources.GlobalResource.menu_Sd_PlannedOrder;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sd_PlannedOrder','/Cube_SD/forecast.aspx','" + menu_Sd_PlannedOrder + "', '/res/Menu/planso.png', '', true, ''); ");
        }

        protected void Btn_Pp_inbound_scan_Click(object sender, EventArgs e)
        {
            string menu_Pp_InboundSerial = global::Resources.GlobalResource.menu_Sd_InboundSerial;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_InboundSerial','/Cube_PP/manufacturing/shipment/inbound_scan.aspx','" + menu_Pp_InboundSerial + "', '/res/menu/inbound.png', '', true, ''); ");
        }

        protected void Btn_Pp_outbound_scan_Click(object sender, EventArgs e)
        {
            string menu_Pp_OutboundSerial = global::Resources.GlobalResource.menu_Sd_OutboundSerial;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_OutboundSerial','/Cube_PP/manufacturing/shipment/outbound_scan.aspx','" + menu_Pp_OutboundSerial + "', '/res/menu/outbound.png', '', true, ''); ");
        }

        protected void Btn_Pp_inbound_Query_Click(object sender, EventArgs e)
        {
            string menu_Pp_InboundQuery = global::Resources.GlobalResource.menu_Sd_InboundQuery;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_InboundQuery','/Cube_PP/manufacturing/shipment/inbound_query.aspx','" + menu_Pp_InboundQuery + "', '/res/menu/inserialy.png', '', true, ''); ");
        }

        protected void Btn_Pp_outbound_Query_Click(object sender, EventArgs e)
        {
            string menu_Pp_OutboundQuery = global::Resources.GlobalResource.menu_Sd_OutboundQuery;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Pp_OutboundQuery','/Cube_PP/manufacturing/shipment/outbound_query.aspx','" + menu_Pp_OutboundQuery + "', '/res/menu/outserialy.png', '', true, ''); ");
        }

        protected void Btn_sys_Help_Manual_Click(object sender, EventArgs e)
        {
            string sys_Help_Manual = global::Resources.GlobalResource.menu_Sys_Help;
            PageContext.RegisterStartupScript("top.addExampleTab('sys_Help_Manual','/Lc_Docs/helper/Lc_Manual.pdf','" + sys_Help_Manual + "', '/res/icon/help.png', '', true, ''); ");
        }

        protected void Btn_sys_Charts_Click(object sender, EventArgs e)
        {
            string sys_Charts = global::Resources.GlobalResource.sys_Charts;
            PageContext.RegisterStartupScript("top.addExampleTab('sys_Charts','/Cube_RPT/cube_echarts.aspx','" + sys_Charts + "', '/res/menu/kanban.png', '', true, ''); ");
        }

        #endregion event
    }
}