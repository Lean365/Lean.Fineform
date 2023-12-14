using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using FineUIPro;
using System.Security;
using Lean.Fineform.Lf_Business.Models.YF;
namespace Lean.Fineform
{
    public class LeanDatabaseInitializer : DropCreateDatabaseIfModelChanges<LeanContext>  // DropCreateDatabaseAlways<BaseKitContext>  DropCreateDatabaseIfModelChanges<BaseKitContext>
    {
        [SecurityCritical]
        protected override void Seed(LeanContext context)
        {

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Pp_P1d_Output', RESEED, 910000001)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Pp_P1d_OutputSub', RESEED, 920000001)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Pp_P2d_Output', RESEED, 910000001)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Pp_P2d_OutputSub', RESEED, 920000001)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Qm_Outgoing', RESEED, 710000001)");
            // context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Qm_Complaint', RESEED, 720000001)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Pp_P1d_Defect', RESEED, 510000001)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Pp_P2d_Defect', RESEED, 520000001)");
            context.Database.ExecuteSqlCommand(ProceduerHelper.DateProcedure);




            GetAdm_Configs().ForEach(c => context.Adm_Configs.Add(c));
            GetAdm_Institutions().ForEach(c => context.Adm_Institutions.Add(c));
            GetAdm_Depts().ForEach(d => context.Adm_Depts.Add(d));
            GetAdm_Roles().ForEach(r => context.Adm_Roles.Add(r));
            GetAdm_Powers().ForEach(p => context.Adm_Powers.Add(p));
            GetAdm_Titles().ForEach(t => context.Adm_Titles.Add(t));
            GetAdm_Users().ForEach(u => context.Adm_Users.Add(u));
            GetoneEm_Event_Types().ForEach(u => context.Em_Event_Types.Add(u));

            GetPp_Lines().ForEach(t => context.Pp_Lines.Add(t));
            GetPp_DefectCodes().ForEach(t => context.Pp_DefectCodes.Add(t));
            GetPp_Worktimes().ForEach(t => context.Pp_Durations.Add(t));
            GetPp_Reasons().ForEach(t => context.Pp_Reasons.Add(t));
            GetQm_CheckTypes().ForEach(t => context.Qm_CheckTypes.Add(t));
            GetQm_DocNumbers().ForEach(t => context.Qm_DocNumbers.Add(t));
            GetPp_EcCategorys().ForEach(t => context.Pp_EcCategorys.Add(t));
            context.SaveChanges();

            // 添加菜单时需要指定ViewAdm_Power，所以上面需要先保存到数据库
            GetAdm_Menus(context).ForEach(m => context.Adm_Menus.Add(m));

            //插入年月日数据表

            for (int i = 1; i <= 100; i++)
            {

                int y = int.Parse(DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(-10).ToString("yyyy")) + i;
                context.Database.ExecuteSqlCommand("EXEC[GetCalendarByYear]" + y + "");

            }
            //填充生产赁率
            string InsertEfficiency = "INSERT INTO[dbo].[Pp_Efficiency]([GUID],[Proratedate],[Prorate],[Udf001],[Udf002],[Udf003],[Udf004],[Udf005]" +
                                        ",[Udf006],[isDelete],[Remark],[Creator],[CreateTime])SELECT newid(),cast([TheYear] as varchar)+RIGHT('0000000'+CONVERT(VARCHAR(50),cast([TheMonth] as varchar)),2),85," +
                                        "'','','',0,0,0,0,'admin',(select CONVERT(varchar, GETDATE(),120)),''  FROM [dbo].[Adm_TheDate] group by cast([TheYear] as varchar)+RIGHT('0000000'+CONVERT(VARCHAR(50),cast([TheMonth] as varchar)),2)";

            context.Database.ExecuteSqlCommand(InsertEfficiency);

            //更新部门

            //string UpdateDept = "update [dbo].[Adm_User] set DeptID=6 where Name='admin'";

            //context.Database.ExecuteSqlCommand(UpdateDept);
        }

        private static List<Adm_Menu> GetAdm_Menus(LeanContext context)
        {
            var Adm_Menus = new List<Adm_Menu> {
                new Adm_Menu
                {
                    Name = "系统管理",
                    SortIndex = 1,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/system.png",
                    ButtonName="Btn_LB_Adm_sys_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSysView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="系统菜单",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/menu.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/manu.png",
                            ButtonName="Btn_LB_Adm_menu",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreMenuView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="在线统计",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/online.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/online.png",
                            ButtonName="Btn_LB_Adm_online",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreOnlineView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="系统配置",
                            SortIndex=30,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/config.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/config.png",
                            ButtonName="Btn_LB_Adm_config",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreConfigView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="登录日志",
                            SortIndex=40,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/log.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/loginlog.png",
                            ButtonName="Btn_LB_Adm_log",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreLogView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="工作日志",
                            SortIndex=50,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/operatelog.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/worklog.png",
                            ButtonName="Btn_LB_Adm_operatelog",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreLogView").FirstOrDefault<Adm_Power>()
                        },
                    }
                },
                new Adm_Menu
                {
                    Name = "公司部门",
                    SortIndex = 2,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/Dept.png",
                    ButtonName="Btn_LB_Adm_dept_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreComDeptView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="公司信息",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/company.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/company.png",
                            ButtonName="Btn_LB_Adm_Institution",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreCompanyView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="部门管理",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/dept.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/deptm.png",
                            ButtonName="Btn_LB_Adm_dept",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreDeptView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="部门用户",
                            SortIndex=30,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/dept_user.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/deptuser.png",
                            ButtonName="Btn_LB_Adm_dept_user",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreDeptuserView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="职称管理",
                            SortIndex=40,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/title.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/title.png",
                            ButtonName="Btn_LB_Adm_title",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTitleView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="职称用户",
                            SortIndex=50,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/title_user.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/titleuser.png",
                            ButtonName="Btn_LB_Adm_title_user",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTitleUserView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="公司目标",
                            SortIndex=60,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/corpkpi.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/kpi.png",
                            ButtonName="Btn_LB_Adm_corpkpi",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreCorpkpiView").FirstOrDefault<Adm_Power>()
                        },

                    }
                },
                new Adm_Menu
                {
                    Name = "用户角色权限",
                    SortIndex = 3,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/User.png",
                    ButtonName="Btn_LB_Adm_"+"user_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreUserRolePowerView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="用户管理",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/user.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/user.png",
                            ButtonName="Btn_LB_Adm_"+"user",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreUserView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="密码修改",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/changepassword.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/userset.png",
                            ButtonName="Btn_LB_Adm_"+"changepassword",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreUserChangePassword").FirstOrDefault<Adm_Power>()
                        },

                        new Adm_Menu {
                            Name="角色管理",
                            SortIndex=30,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/role.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/rolem.png",
                            ButtonName="Btn_LB_Adm_"+"role",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreRoleView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="角色用户",
                            SortIndex=40,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/role_user.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/roleuser.png",
                            ButtonName="Btn_LB_Adm_"+"role_user",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreRoleUserView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="权限管理",
                            SortIndex=50,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/power.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/power.png",
                            ButtonName="Btn_LB_Adm_"+"power",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePowerView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="角色权限",
                            SortIndex=60,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/role_power.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/rolepower.png",
                            ButtonName="Btn_LB_Adm_"+"role_power",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreRolePowerView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="批量更新",
                            SortIndex=70,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Admin/role_power_batch.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/confirm.png",
                            ButtonName="Btn_LB_Adm_"+"role_power_batch",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreRolePowerView").FirstOrDefault<Adm_Power>()
                        },

                    }
                },
                new Adm_Menu
                {
                    Name = "流程管理",
                    SortIndex = 4,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/flow.png",
                    ButtonName="Btn_LB_Bpm_"+"bpm_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBPMView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu
                        {

                            Name = "待办事项",
                            SortIndex = 10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/BPM/todo.aspx",
                            ImageUrl = "~/Lf_Resources/menu/todo.png",
                            ButtonName="Btn_LB_Bpm_"+"todolist",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBpmTodoView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu
                        {

                            Name = "我的申请",
                            SortIndex = 20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/BPM/apply.aspx",
                            ImageUrl = "~/Lf_Resources/menu/apply.png",
                            ButtonName="Btn_LB_Bpm_"+"apply",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBpmApplyView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu
                        {

                            Name = "流程设计",
                            SortIndex = 30,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/BPM/desgin.aspx",
                            ImageUrl = "~/Lf_Resources/menu/desgin.png",
                            ButtonName="Btn_LB_Bpm_"+"desgin",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBpmDesginView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu
                        {

                            Name = "流程模板",
                            SortIndex = 40,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/BPM/template.aspx",
                            ImageUrl = "~/Lf_Resources/menu/template.png",
                            ButtonName="Btn_LB_Bpm_"+"template",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBpmTemplateView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu
                        {

                            Name = "签章管理",
                            SortIndex = 50,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/BPM/signature.aspx",
                            ImageUrl = "~/Lf_Resources/menu/signature.png",
                            ButtonName="Btn_LB_Bpm_"+"signature",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBpmSignatureView").FirstOrDefault<Adm_Power>()
                        },

                    }
                },
                new Adm_Menu
                {
                    Name = "日程事件",
                    SortIndex = 5,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/schedule.png",
                    ButtonName="Btn_LB_Em_"+"Em_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEMView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="我的日程",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/EM/event.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/myevent.png",
                            ButtonName="Btn_LB_Em_"+"event",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEventView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="日程管理",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/EM/schedule.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/event.png",
                            ButtonName="Btn_LB_Em_"+"schedule",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEventView").FirstOrDefault<Adm_Power>()
                        },
                    }
                },
                new Adm_Menu
                {
                    Name = "表单管理",
                    SortIndex = 6,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/form.png",
                    ButtonName="Btn_LB_Fm_"+"Fm_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFMView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="我的表单",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/FM/myform.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/myform.png",
                            ButtonName="Btn_LB_Fm_"+"myform",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoremyFormView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="表单设计",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/FM/formdesign.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/formanage.png",
                            ButtonName="Btn_LB_Fm_"+"formdesign",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFormDesginView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="表单类别",
                            SortIndex=30,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/FM/formtype.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/formtype.png",
                            ButtonName="Btn_LB_Fm_"+"formtype",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFormCategoryView").FirstOrDefault<Adm_Power>()
                        },
                    }
                },
                new Adm_Menu
                {
                    Name = "财务管理",
                    SortIndex = 7,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/finance.png",
                    ButtonName="Btn_LB_Fico_"+"Fico_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFICOView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                                new Adm_Menu {
                                    Name="财务期间",
                                    SortIndex=10,
                                    Remark = "二级菜单",
                                    NavigateUrl = "~/Lf_Accounting/fyperiod.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/period.png",
                                    ButtonName="Btn_LB_Fico_"+"period",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePeriodView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu {
                                    Name="会计科目",
                                    SortIndex=20,
                                    Remark = "二级菜单",
                                    NavigateUrl = "~/Lf_Accounting/acctitle.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/subject.png",
                                    ButtonName="Btn_LB_Fico_"+"subject",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSubjectView").FirstOrDefault<Adm_Power>()
                                },
                                                                new Adm_Menu {
                                    Name="签拟单",
                                    SortIndex=30,
                                    Remark = "二级菜单",
                                    NavigateUrl = "~/Lf_Accounting/Countersignature.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/Countersignature.png",
                                    ButtonName="Btn_LB_Fico_"+"Countersignature",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreCountersignatureView").FirstOrDefault<Adm_Power>()
                                },
                        new Adm_Menu {
                            Name="费用管理",
                            SortIndex=40,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Accounting/expense.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/expenses.png",
                            ButtonName="Btn_LB_Fico_"+"expense",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreExpensesView").FirstOrDefault<Adm_Power>(),

                        },
                        new Adm_Menu {
                            Name="预算管理",
                            SortIndex=50,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/budget.png",
                            ButtonName="Btn_LB_Fico_"+"budget",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBudgetView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu> {

                                new Adm_Menu {
                                    Name="费用预算",
                                    SortIndex=100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/bg_expense.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/expenses.png",
                                    ButtonName="Btn_LB_Fico_"+"expense",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreExpensesView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu {
                                    Name="人工预算",
                                    SortIndex=110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/bg_labor.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/labor.png",
                                    ButtonName="Btn_LB_Fico_"+"labor",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBudgetView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu {
                                    Name="加班预算",
                                    SortIndex=120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/bg_workovertime.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/workovertime.png",
                                    ButtonName="Btn_LB_Fico_"+"workovertime",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBudgetView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu {
                                    Name="资产预算",
                                    SortIndex=130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/bg_asset.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/asset.png",
                                    ButtonName="Btn_LB_Fico_"+"asset",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreBudgetView").FirstOrDefault<Adm_Power>()
                                },

                                new Adm_Menu {
                                    Name="预算审核",
                                    SortIndex=140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/audit.aspx",
                                    ImageUrl = "~/Lf_Resources/Menu/Audit.png",
                                    ButtonName="Btn_LB_Fico_"+"audit",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreAuditView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="图表",
                            SortIndex=60,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/kanban.png",
                            ButtonName="Btn_LB_Fico_"+"Sd_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFICOView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "费用分析",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/Fico_chart.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/chart.png",
                                    ButtonName="Btn_LB_Fico_"+"Fico_charts",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFICOChart").FirstOrDefault<Adm_Power>()
                                },
                                 new Adm_Menu
                                {

                                    Name = "成本分析",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/costing.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/costing.png",
                                    ButtonName="Btn_LB_Fico_"+"costing",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFICOChart").FirstOrDefault<Adm_Power>()
                                },
                                 new Adm_Menu
                                {

                                    Name = "实际科目",
                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Accounting/Fico_cost_element.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/acctitle.png",
                                    ButtonName="Btn_LB_Fico_"+"Fico_cost_element",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFICOChart").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                    }
                },
                new Adm_Menu
                {
                    Name = "物料管理",
                    SortIndex = 8,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/material.png",
                    ButtonName="Btn_LB_Mm_"+"Mm_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreMMView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="物料",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Manufacturing/MM/material.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/materialadmin.png",
                            ButtonName="Btn_LB_Mm_"+"material",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreMaterialView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="查询",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Manufacturing/MM/material_query.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/query.png",
                            ButtonName="Btn_LB_Mm_"+"material_query",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreMaterialView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="易飞数据",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Manufacturing/MM/Yf_Map.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/yf.png",
                            ButtonName="Btn_LB_Mm_"+"Yf_Map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreMaterialView").FirstOrDefault<Adm_Power>()
                        },
                    }
                },
                new Adm_Menu
                {
                    Name = "生产管理",
                    SortIndex = 9,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/pro.png",
                    ButtonName="Btn_LB_Pp_"+"pp_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="信息",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/info.png",
                            ButtonName="Btn_LB_Pp_"+"pp_info_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "班组管理",
                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Pp_line.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/lineteam.png",
                                    ButtonName="Btn_LB_Pp_"+"Pp_line",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreLineView").FirstOrDefault<Adm_Power>(),
                                },
                                 new Adm_Menu
                                {

                                    Name = "生产订单",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Pp_order.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_order.png",
                                    ButtonName="Btn_LB_Pp_"+"Pp_order",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreOrderView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "标准工时",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Pp_manhour.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_time.png",
                                    ButtonName="Btn_LB_Pp_"+"Pp_manhour",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreManhourView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "原因类别",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Pp_reason.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_rtype.png",
                                    ButtonName="Btn_LB_Pp_"+"Pp_reason",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreNotReachedView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "检验类别",

                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Qm_acceptcat.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_qtype.png",
                                    ButtonName="Btn_LB_Pp_"+"Qm_acceptcat",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreInspectCatView").FirstOrDefault<Adm_Power>(),
                                },


                                new Adm_Menu
                                {

                                    Name = "运输方式",

                                    SortIndex = 150,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Pp_transport.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_ttype.png",
                                    ButtonName="Btn_LB_Pp_"+"Pp_transport",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTransportView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "稼动率",

                                    SortIndex = 160,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Pp_efficiency.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_rate.png",
                                    ButtonName="Btn_LB_Pp_"+"Pp_efficiency",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreUtilizationView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "机种仕向",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/Master/Pp_models_region.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/model.png",
                                    ButtonName="Btn_LB_Pp_"+"Pp_models_region",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreModelsView").FirstOrDefault<Adm_Power>(),
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="技联",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/liaisonmgt.png",
                            ButtonName="Btn_LB_Pp_"+"liaison",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTlView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "技术联络",

                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/TL/liaison.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/liaison.png",
                                    ButtonName="Btn_LB_Pp_"+"liaison",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTlView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "技联查询",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/TL/liaison_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Pp_"+"liaison",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTlView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="SOP",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/sop.png",
                            ButtonName="Btn_LB_Pp_"+"sopinfo",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSopView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "SOP确认",

                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SOP/sop.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/sopinfo.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_sop",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSopView").FirstOrDefault<Adm_Power>()
                                },
                            new Adm_Menu
                                {

                                    Name = "组立",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SOP/pe_asy.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/sopasy.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_peng",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSopView").FirstOrDefault<Adm_Power>()
                                },
                            new Adm_Menu
                                {

                                    Name = "PCBA",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SOP/pe_pcba.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/soppcba.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_peng",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSopView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },

                        new Adm_Menu {
                            Name="设变",
                            SortIndex=40,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/proec.png",
                            ButtonName="Btn_LB_Pp_"+"ec_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {

                                new Adm_Menu
                                {
                                    Name = "设变实施",
                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/ecstatus.png",
                                    ButtonName="Btn_LB_Pp_"+"ec",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "投入批次",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_lot.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/lotstatus.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_lot",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcView").FirstOrDefault<Adm_Power>()
                                },

                                new Adm_Menu
                                {

                                    Name = "物料确认",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_Mm_outbound.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/confirm.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_eng_outbound",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "技术课",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_eng_view.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/te.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_eng_view",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcENGView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "采购课",

                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_pd.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/pd.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_pd",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcPDView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "生管课",

                                    SortIndex = 150,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_pm.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/pm.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_pm",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcPMView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "受检课",

                                    SortIndex = 160,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_qc.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/iqc.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_qc",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcIQCView").FirstOrDefault<Adm_Power>(),
                                },
                                new Adm_Menu
                                {

                                    Name = "部管课",

                                    SortIndex = 170,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_mm.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/mm.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_mm",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcMMView").FirstOrDefault<Adm_Power>(),
                                },

                                new Adm_Menu
                                {

                                    Name = "制二课",

                                    SortIndex = 180,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_p2d.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/p2d.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_p2d",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcP2DView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "制一课",

                                    SortIndex = 190,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_p1d.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/p1d.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_p1d",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcP1DView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "品管课",

                                    SortIndex = 200,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_qa.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/qa.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_qa",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcQAView").FirstOrDefault<Adm_Power>()
                                },


                                new Adm_Menu
                                {

                                    Name = "旧品管制",

                                    SortIndex = 210,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_balance.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/inventory.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_balance",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "SAP设变",

                                    SortIndex = 220,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_sap.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/sapdata.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_sap",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "设变查询",

                                    SortIndex = 230,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/EC/ec_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Pp_"+"ec_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreEcView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="制一生产",
                            SortIndex=50,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/proadmin.png",
                            ButtonName="Btn_LB_Pp_"+"daily_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {

                                new Adm_Menu
                                {

                                    Name = "生产日报",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P1D/p1d_daily.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_dreport.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_daily",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DOutputView").FirstOrDefault<Adm_Power>()
                                },

                                new Adm_Menu
                                {

                                    Name = "OPH查询",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P1D/p1d_output_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_output_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DOutputView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "生产进度",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P1D/p1d_output_order_finish.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/push.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_output_order_finish",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DOutputView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {
                                    Name = "OPH报表",
                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P1D/p1d_output_opt.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/reportl.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_output_opt",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DOutputView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {
                                    Name = "看板管理",
                                    SortIndex = 150,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P1D/p1d_data_kanban.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/kanban.png",
                                    ButtonName="Btn_LB_Pp_"+"output_data_line",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreKanbanView").FirstOrDefault<Adm_Power>()
                                },

                            }
                        },
                        new Adm_Menu {
                            Name="制一不良",
                            SortIndex=60,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/prodef.png",
                            ButtonName="Btn_LB_Pp_"+"def_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {

                                new Adm_Menu
                                {

                                    Name = "生产不良",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P1D/p1d_defect.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_bad.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_defect",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DDefectView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "工单统计",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P1D/p1d_defect_order_totalled.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_line.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_defect_order_totalled",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DDefectView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "LOT集计",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P1D/p1d_defect_lot_finished.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/report.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_defect_lot_finished",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DDefectView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "不具合查询",

                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P1D/p1d_defect_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Pp_"+"p1d_defect_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP1DDefectView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="制二生产",
                            SortIndex=70,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/proadmin.png",
                            ButtonName="Btn_LB_Pp_"+"daily_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {

                                new Adm_Menu
                                {

                                    Name = "生产日报",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P2D/p2d_daily.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_dreport.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_daily",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DOutputView").FirstOrDefault<Adm_Power>()
                                },

                                new Adm_Menu
                                {

                                    Name = "OPH查询",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P2D/p2d_output_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_output_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DOutputView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "生产进度",

                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P2D/p2d_output_order_finish.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/push.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_output_order_finish",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DOutputView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {
                                    Name = "OPH报表",
                                    SortIndex = 150,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P2D/p2d_output_opt.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/reportl.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_output_opt",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DOutputView").FirstOrDefault<Adm_Power>()
                                },

                                new Adm_Menu
                                {
                                    Name = "看板管理",
                                    SortIndex = 150,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/daily/P2D/p2d_data_kanban.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/kanban.png",
                                    ButtonName="Btn_LB_Pp_"+"output_data_line",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreKanbanView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="制二不良",
                            SortIndex=80,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/prodef.png",
                            ButtonName="Btn_LB_Pp_"+"def_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {

                                new Adm_Menu
                                {

                                    Name = "生产不良",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P2D/p2d_defect.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_bad.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_defect",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DDefectView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "工单统计",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P2D/p2d_defect_order_totalled.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_line.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_defect_order_totalled",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DDefectView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "LOT集计",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P2D/p2d_defect_lot_finished.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/report.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_defect_lot_finished",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DDefectView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "不具合查询",

                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/poor/P2D/p2d_defect_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Pp_"+"p2d_defect_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreP2DDefectView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="工数",
                            SortIndex=90,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/protime.png",
                            ButtonName="Btn_LB_Pp_"+"time_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {
                                    Name = "工数查询",
                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/timesheet/times_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Pp_"+"times_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTimeView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="追溯",
                            SortIndex=90,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/tracking.png",
                            ButtonName="Btn_LB_Pp_"+"lot_tracking",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {
                                    Name = "批次追溯",
                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/tracking/lot_tracking.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/tracking.png",
                                    ButtonName="Btn_LB_Pp_"+"lot_tracking",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTrackingView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {
                                    Name = "标准工程",
                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/tracking/lot_process.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/process.png",
                                    ButtonName="Btn_LB_Pp_"+"lot_process",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreTrackingView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {
                                    Name = "分析图表",
                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/pp_tracking_chart.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/chart.png",
                                    ButtonName="Btn_LB_Pp_"+"pp_tracking_chart",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPChart").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="图表",
                            SortIndex=100,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Report/rpt_map.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/kanban.png",
                            ButtonName="Btn_LB_RPT_"+"sys_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {
                                    Name = "生产图表",
                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/PP/pp_chart.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/chart.png",
                                    ButtonName="Btn_LB_Pp_"+"pp_chart",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CorePPChart").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                    }
                },
                new Adm_Menu
                {
                    Name = "质量管理",
                    SortIndex = 10,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/quality.png",
                    ButtonName="Btn_LB_Qm_"+"qm_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreQMView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="品质",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/qualityadmin.png",
                            ButtonName="Btn_LB_Qm_"+"fqc_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreQMView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "成品入库检验",

                                    SortIndex = 100,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/fqc/fqc.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_qa.png",
                                    ButtonName="Btn_LB_Qm_"+"fqc",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFqcView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "不合格通知书",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/fqc/fqc_notice.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_nopass.png",
                                    ButtonName="Btn_LB_Qm_"+"fqc_notice",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFqcNoticeView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "分析对策报告",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/fqc/fqc_action.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_analysis.png",
                                    ButtonName="Btn_LB_Qm_"+"fqc_action",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFqcActionView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "合格率报表",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/fqc/fqc_count.aspx",
                                    ImageUrl = "~/Lf_Resources/icon/tag_linepass.png",
                                    ButtonName="Btn_LB_Qm_"+"fqc_count",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFqcView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "检验查询",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/fqc/fqc_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/query.png",
                                    ButtonName="Btn_LB_Qm_"+"fqc_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreFqcView").FirstOrDefault<Adm_Power>()
                                },

                            }
                        },
                        new Adm_Menu {
                            Name="成本",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/qccost.png",
                            ButtonName="Btn_LB_Qm_"+"cost_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreQMView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "改修对应",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/cost/rework_cost.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/qcrework.png",
                                    ButtonName="Btn_LB_Qm_"+"rework_cost",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreReworkCostView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "品质业务",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/cost/operation_cost.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/qcbus.png",
                                    ButtonName="Btn_LB_Qm_"+"operation_cost",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreOperationCostView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "废弃事故",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/cost/waste_cost.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/qcscrap.png",
                                    ButtonName="Btn_LB_Qm_"+"waste_cost",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreWasteCostView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "直接工资率",

                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/cost/wagerate.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/wages.png",
                                    ButtonName="Btn_LB_Qm_"+"wagerate",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreWagesView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu {
                            Name="客诉",
                            SortIndex=30,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/complaint.png",
                            ButtonName="Btn_LB_Qm_"+"qm_complaint",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreQMView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "客诉信息",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/complaint/complaint.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/complaintinfo.png",
                                    ButtonName="Btn_LB_Qm_"+"qm_complaint",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreComplaintView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "品管课",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/complaint/complaint_qa.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/qa.png",
                                    ButtonName="Btn_LB_Qm_"+"qm_complaint",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreComplaintQAView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "制一课",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/complaint/complaint_p1d.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/dept/p1d.png",
                                    ButtonName="Btn_LB_Qm_"+"qm_complaint",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreComplaintP1DView").FirstOrDefault<Adm_Power>()
                                },

                            }
                        },
                        new Adm_Menu {
                            Name="图表",
                            SortIndex=40,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/kanban.png",
                            ButtonName="Btn_LB_Qm_"+"qm_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreQMView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "品质图表",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/QM/qm_chart.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/chart.png",
                                    ButtonName="Btn_LB_Qm_"+"qm_charts",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreQMView").FirstOrDefault<Adm_Power>()
                                },

                            }
                        },

                    }
                },
                new Adm_Menu
                {
                    Name = "销售管理",
                    SortIndex = 11,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/salesadmin.png",
                    ButtonName="Btn_LB_Sd_"+"Sd_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSDView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="销售数据",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Manufacturing/SD/salesmanage/sales.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/sales.png",
                            ButtonName="Btn_LB_Sd_"+"salesdata",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSDView").FirstOrDefault<Adm_Power>(),

                        },
                        new Adm_Menu {
                            Name="客户信息",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Manufacturing/SD/salesmanage/customer.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/cus.png",
                            ButtonName="Btn_LB_Sd_"+"customer",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreCustomerView").FirstOrDefault<Adm_Power>(),

                        },

                        new Adm_Menu {
                            Name="订单管理",
                            SortIndex=30,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/so.png",
                            ButtonName="Btn_LB_Sd_"+"somanage",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSDView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {
                                    Name = "计划SO",
                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SD/salesmanage/forecast.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/planso.png",
                                    ButtonName="Cube_SD"+"planso",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreForecastView").FirstOrDefault<Adm_Power>()
                                },
                            }
                        },
                        new Adm_Menu
                        {
                            Name = "出货管理",
                            SortIndex = 40,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/outgoing.png",
                            ButtonName="Btn_LB_Pp_"+"sm_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSDView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "入库扫描",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SD/shipment/inbound_scan.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/inbound.png",
                                    ButtonName="Btn_LB_Pp_"+"inbound_scan",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreInboundScanView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "出库扫描",

                                    SortIndex = 120,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SD/shipment/outbound_scan.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/outbound.png",
                                    ButtonName="Btn_LB_Pp_"+"outbound_scan",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreOutboundScanView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "入库查询",

                                    SortIndex = 130,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SD/shipment/inbound_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/inserialy.png",
                                    ButtonName="Btn_LB_Pp_"+"inbound_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreInboundScanView").FirstOrDefault<Adm_Power>()
                                },
                                new Adm_Menu
                                {

                                    Name = "出库查询",

                                    SortIndex = 140,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SD/shipment/outbound_query.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/outserialy.png",
                                    ButtonName="Btn_LB_Pp_"+"outbound_query",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreOutboundScanView").FirstOrDefault<Adm_Power>()
                                },


                            }
                        },

                        new Adm_Menu {
                            Name="图表",
                            SortIndex=50,
                            Remark = "二级菜单",
                            NavigateUrl = "",
                            ImageUrl = "~/Lf_Resources/Menu/kanban.png",
                            ButtonName="Btn_LB_Sd_"+"Sd_map",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSDView").FirstOrDefault<Adm_Power>(),
                            Children = new List<Adm_Menu>
                            {
                                new Adm_Menu
                                {

                                    Name = "销售图表",

                                    SortIndex = 110,
                                    Remark = "三级菜单",
                                    NavigateUrl = "~/Lf_Manufacturing/SD/Sd_chart.aspx",
                                    ImageUrl = "~/Lf_Resources/menu/chart.png",
                                    ButtonName="Btn_LB_Sd_"+"Sd_charts",
                                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreSDChart").FirstOrDefault<Adm_Power>()
                                },

                            }
                        },

                    }
                },

                new Adm_Menu
                {
                    Name = "日常办公",
                    SortIndex = 12,
                    Remark = "顶级菜单",
                    NavigateUrl = "",
                    ImageUrl = "~/Lf_Resources/Menu/OA.png",
                    ButtonName="Btn_LB_Oa_"+"oa_map",
                    ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreOAView").FirstOrDefault<Adm_Power>(),
                    Children = new List<Adm_Menu> {
                        new Adm_Menu {
                            Name="通讯录",
                            SortIndex=10,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/OA/warning.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/addr.png",
                            ButtonName="Btn_LB_Oa_"+"address",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreAddressView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="公告通知",
                            SortIndex=20,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/OA/notice.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/notice.png",
                            ButtonName="Btn_LB_Oa_"+"forecast",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreNoticeView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="会议预定",
                            SortIndex=30,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/OA/meeting.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/meeting.png",
                            ButtonName="Btn_LB_Oa_"+"meeting",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreMeetingView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="车辆预定",
                            SortIndex=40,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/OA/vehicles.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/vehicle.png",
                            ButtonName="Btn_LB_Oa_"+"vehicles",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreVehiclesView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="周报管理",
                            SortIndex=50,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/OA/weekly.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/weekly.png",
                            ButtonName="Btn_LB_Oa_"+"weekly",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreWeeklyView").FirstOrDefault<Adm_Power>()
                        },
                        new Adm_Menu {
                            Name="帮助台",
                            SortIndex=60,
                            Remark = "二级菜单",
                            NavigateUrl = "~/Lf_Office/OA/helpdesk.aspx",
                            ImageUrl = "~/Lf_Resources/Menu/helpdesk.png",
                            ButtonName="Btn_LB_Oa_"+"helpdesk",
                            ViewPower = context.Adm_Powers.Where(p => p.Name == "CoreHelpdeskView").FirstOrDefault<Adm_Power>()
                        },
                    }
                },
            };

            return Adm_Menus;
        }
        private static List<Adm_Title> GetAdm_Titles()
        {
            var Adm_Titles = new List<Adm_Title>()
            {
                new Adm_Title()
                {
                    Name = "总经理"
                },
                new Adm_Title()
                {
                    Name = "部门经理"
                },
                new Adm_Title()
                {
                    Name = "课长"
                },
                new Adm_Title()
                {
                    Name = "股长"
                },
                new Adm_Title()
                {
                    Name = "班长"
                },
                new Adm_Title()
                {
                    Name = "工程师"
                },
                new Adm_Title()
                {
                    Name = "事务员"
                }
            };

            return Adm_Titles;
        }
        private static List<Adm_Power> GetAdm_Powers()
        {
            var Adm_Powers = new List<Adm_Power>
            {
                new Adm_Power
                {
                    Name = "CoreSysView",
                    Title = "浏览系统地图",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Admin/sys_map.aspx",

                },
                new Adm_Power
                {
                    Name = "CoreSysChart",
                    Title = "浏览系统图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Admin/sys_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOAView",
                    Title = "浏览日常办公",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/OA/oa_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOAChart",
                    Title = "浏览办公图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/OA/oa_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePPView",
                    Title = "浏览生产管理",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Manufacturing/PP/pp_map.aspx",

                },
                new Adm_Power
                {
                    Name = "CorePPChart",
                    Title = "浏览生产图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Manufacturing/PP/pp_chart.aspx",

                },
                new Adm_Power
                {
                    Name = "CoreSDView",
                    Title = "浏览销售管理",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Manufacturing/SD/Sd_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSDChart",
                    Title = "浏览销售图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Manufacturing/SD/Sd_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFICOView",
                    Title = "浏览财务管理",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Accounting/Fico_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFICOChart",
                    Title = "浏览财务图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Accounting/Fico_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEMView",
                    Title = "浏览日程事件",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/EM/Em_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEMChart",
                    Title = "浏览日程图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/EM/Em_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFMView",
                    Title = "浏览表单管理",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/FM/Fm_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFMChart",
                    Title = "浏览表单图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/FM/Fm_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBPMView",
                    Title = "浏览流程管理",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/BPM/bpm_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBPMView",
                    Title = "浏览流程图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Office/BPM/bpm_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreQMView",
                    Title = "浏览品质管理",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Manufacturing/QM/qm_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreQMChart",
                    Title = "浏览品质图表",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Manufacturing/QM/qm_chart.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMMView",
                    Title = "浏览物料管理",
                    GroupName = "系统地图",
                    NavigateUrl="~/Lf_Manufacturing/MM/Mm_map.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUserView",
                    Title = "浏览用户列表",
                    GroupName = "用户管理",
                    NavigateUrl="~/Lf_Admin/user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUserNew",
                    Title = "新增用户",
                    GroupName = "用户管理",
                    NavigateUrl="~/Lf_Admin/user_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUserEdit",
                    Title = "编辑用户",
                    GroupName = "用户管理",
                    NavigateUrl="~/Lf_Admin/user_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUserDelete",
                    Title = "删除用户",
                    GroupName = "用户管理",
                    NavigateUrl="~/Lf_Admin/user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUserChangePassword",
                    Title = "修改密码",
                    GroupName = "用户管理",
                    NavigateUrl="~/Lf_Admin/changepassword.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRoleView",
                    Title = "浏览角色列表",
                    GroupName = "角色管理",
                    NavigateUrl="~/Lf_Admin/role.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRoleNew",
                    Title = "新增角色",
                    GroupName = "角色管理",
                    NavigateUrl="~/Lf_Admin/role_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRoleEdit",
                    Title = "编辑角色",
                    GroupName = "角色管理",
                    NavigateUrl="~/Lf_Admin/role_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRoleDelete",
                    Title = "删除角色",
                    GroupName = "角色管理",
                    NavigateUrl="~/Lf_Admin/role.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRoleUserView",
                    Title = "浏览角色用户列表",
                    GroupName = "角色用户",
                    NavigateUrl="~/Lf_Admin/role_user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRoleUserNew",
                    Title = "向角色添加用户",
                    GroupName = "角色用户",
                    NavigateUrl="~/Lf_Admin/role_user_addnew.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRoleUserDelete",
                    Title = "从角色中删除用户",
                    GroupName = "角色用户",
                    NavigateUrl="~/Lf_Admin/role_user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOnlineView",
                    Title = "浏览在线用户列表",
                    GroupName = "在线用户",
                    NavigateUrl="~/Lf_Admin/online.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreConfigView",
                    Title = "浏览全局配置参数",
                    GroupName = "系统参数",
                    NavigateUrl="~/Lf_Admin/config.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreConfigEdit",
                    Title = "修改全局配置参数",
                    GroupName = "系统参数",
                    NavigateUrl="~/Lf_Admin/config.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMenuView",
                    Title = "浏览菜单列表",
                    GroupName = "菜单管理",
                    NavigateUrl="~/Lf_Admin/menu.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMenuNew",
                    Title = "新增菜单",
                    GroupName = "菜单管理",
                    NavigateUrl="~/Lf_Admin/menu_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMenuEdit",
                    Title = "编辑菜单",
                    GroupName = "菜单管理",
                    NavigateUrl="~/Lf_Admin/menu_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMenuDelete",
                    Title = "删除菜单",
                    GroupName = "菜单管理",
                    NavigateUrl="~/Lf_Admin/menu.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLogView",
                    Title = "浏览日志列表",
                    GroupName = "日志管理",
                    NavigateUrl="~/Lf_Admin/log.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLogDelete",
                    Title = "删除日志",
                    GroupName = "日志管理",
                    NavigateUrl="~/Lf_Admin/log.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTitleView",
                    Title = "浏览职务列表",
                    GroupName = "职务管理",
                    NavigateUrl="~/Lf_Admin/title.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTitleNew",
                    Title = "新增职务",
                    GroupName = "职务管理",
                    NavigateUrl="~/Lf_Admin/title_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTitleEdit",
                    Title = "编辑职务",
                    GroupName = "职务管理",
                    NavigateUrl="~/Lf_Admin/title_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTitleDelete",
                    Title = "删除职务",
                    GroupName = "职务管理",
                    NavigateUrl="~/Lf_Admin/title_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTitleUserView",
                    Title = "浏览职务用户列表",
                    GroupName = "职务用户",
                    NavigateUrl="~/Lf_Admin/title_user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTitleUserNew",
                    Title = "向职务添加用户",
                    GroupName = "职务用户",
                    NavigateUrl="~/Lf_Admin/title_user_addnew.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTitleUserDelete",
                    Title = "从职务中删除用户",
                    GroupName = "职务用户",
                    NavigateUrl="~/Lf_Admin/title_user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCompanyView",
                    Title = "浏览公司列表",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/company.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCompanyNew",
                    Title = "新增公司信息",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/company_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCompanyEdit",
                    Title = "编辑公司信息",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/company_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCompanyDelete",
                    Title = "删除公司信息",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/company.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCorpkpiView",
                    Title = "浏览公司目标列表",
                    GroupName = "公司目标",
                    NavigateUrl="~/Lf_Admin/corpkpi.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCorpkpiNew",
                    Title = "新增公司目标",
                    GroupName = "公司目标",
                    NavigateUrl="~/Lf_Admin/corpkpi_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCorpkpiEdit",
                    Title = "编辑公司目标",
                    GroupName = "公司目标",
                    NavigateUrl="~/Lf_Admin/corpkpi_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCorpkpiDelete",
                    Title = "删除公司目标",
                    GroupName = "公司目标",
                    NavigateUrl="~/Lf_Admin/corpkpi.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreDeptView",
                    Title = "浏览部门列表",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/dept.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreDeptNew",
                    Title = "新增部门",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/dept_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreDeptEdit",
                    Title = "编辑部门",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/dept_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreDeptDelete",
                    Title = "删除部门",
                    GroupName = "公司部门",
                    NavigateUrl="~/Lf_Admin/dept.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreDeptUserView",
                    Title = "浏览部门用户列表",
                    GroupName = "部门用户",
                    NavigateUrl="~/Lf_Admin/dept_user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreDeptUserNew",
                    Title = "向部门添加用户",
                    GroupName = "部门用户",
                    NavigateUrl="~/Lf_Admin/dept_user_addnew.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreDeptUserDelete",
                    Title = "从部门中删除用户",
                    GroupName = "部门用户",
                    NavigateUrl="~/Lf_Admin/dept_user.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePowerView",
                    Title = "浏览权限列表",
                    GroupName = "权限管理",
                    NavigateUrl="~/Lf_Admin/power.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePowerNew",
                    Title = "新增权限",
                    GroupName = "权限管理",
                    NavigateUrl="~/Lf_Admin/power_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePowerEdit",
                    Title = "编辑权限",
                    GroupName = "权限管理",
                    NavigateUrl="~/Lf_Admin/power_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePowerDelete",
                    Title = "删除权限",
                    GroupName = "权限管理",
                    NavigateUrl="~/Lf_Admin/power.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRolePowerView",
                    Title = "浏览角色权限列表",
                    GroupName = "角色权限",
                    NavigateUrl="~/Lf_Admin/role_user.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreRolePowerEdit",
                    Title = "编辑角色权限",
                    GroupName = "角色权限",
                    NavigateUrl="~/Lf_Admin/role_power.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTodoView",
                    Title = "浏览待办列表",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/todo.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTodoNew",
                    Title = "新增待办流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/todo_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTodoEdit",
                    Title = "编辑待办流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/todo_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTodoDelete",
                    Title = "删除待办流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/todo.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmApplyView",
                    Title = "浏览审批列表",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/apply.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmApplyNew",
                    Title = "新增审批流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/apply_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmApplyEdit",
                    Title = "编辑审批流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/apply_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmApplyDelete",
                    Title = "删除审批流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/apply.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmDesginView",
                    Title = "浏览流程设计列表",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/desgin.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmDesginNew",
                    Title = "新增流程设计",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/desgin_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmDesginEdit",
                    Title = "编辑流程设计",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/desgin_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmDesginDelete",
                    Title = "删除流程设计",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/desgin.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmSignatureView",
                    Title = "浏览签章列表",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/signature.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmSignatureNew",
                    Title = "新增签章信息",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/signature_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmSignatureEdit",
                    Title = "编辑签章信息",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/signature_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmSignatureDelete",
                    Title = "删除签章信息",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/signature.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTemplateView",
                    Title = "浏览流程模板列表",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/template.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTemplateNew",
                    Title = "新增流程模板",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/template_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTemplateEdit",
                    Title = "编辑流程模板",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/template_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBpmTemplateDelete",
                    Title = "删除流程模板",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/template.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFlowView",
                    Title = "浏览流程列表",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/flow.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFlowNew",
                    Title = "新增流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/flow_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFlowEdit",
                    Title = "编辑流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/flow_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFlowDelete",
                    Title = "删除流程",
                    GroupName = "流程管理",
                    NavigateUrl="~/Lf_Office/BPM/flow.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFlowInport",
                    Title = "导入流程信息",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Office/BPM/flow_input.aspx",

                },
                new Adm_Power
                {
                    Name = "CoreEventView",
                    Title = "浏览日程列表",
                    GroupName = "日程管理",
                    NavigateUrl="~/Lf_Office/EM/schedule_full.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEventNew",
                    Title = "新增日程信息",
                    GroupName = "日程管理",
                    NavigateUrl="~/Lf_Office/EM/schedule_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEventEdit",
                    Title = "编辑日程信息",
                    GroupName = "日程管理",
                    NavigateUrl="~/Lf_Office/EM/schedule_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEventDelete",
                    Title = "删除日程信息",
                    GroupName = "日程管理",
                    NavigateUrl="~/Lf_Office/EM/schedule.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEventInport",
                    Title = "导入日程信息",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Office/EM/schedule_full.aspx",
                },
                new Adm_Power
                {
                    Name = "CoremyFormView",
                    Title = "浏览我的表单列表",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/myform.aspx",
                },
                new Adm_Power
                {
                    Name = "CoremyFormNew",
                    Title = "新增我的表单",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/myform_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoremyFormEdit",
                    Title = "编辑我的表单",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/myform_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoremyFormDelete",
                    Title = "删除我的表单",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/myform.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormDesginView",
                    Title = "浏览表单设计列表",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formdesgin.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormDesginNew",
                    Title = "新增表单设计",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formdesgin_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormDesginEdit",
                    Title = "编辑表单设计",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formdesgin_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormDesginDelete",
                    Title = "删除表单设计",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formdesgin.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormCategoryView",
                    Title = "浏览表单类别列表",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formcategory.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormCategoryNew",
                    Title = "新增表单类别",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formcategory_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormCategoryEdit",
                    Title = "编辑表单类别",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formcategory_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormCategoryDelete",
                    Title = "删除表单类别",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/formcategory.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormView",
                    Title = "浏览表单列表",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/form.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormNew",
                    Title = "新增表单信息",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/form_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormEdit",
                    Title = "编辑表单信息",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/form_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormDelete",
                    Title = "删除表单信息",
                    GroupName = "表单管理",
                    NavigateUrl="~/Lf_Office/FM/form.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFormInport",
                    Title = "导入表单信息",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Office/FM/form_input.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreCustomerView",
                    Title = "浏览客户信息列表",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/salesmanage/customer.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCustomerNew",
                    Title = "新增客户信息",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/salesmanage/customer_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCustomerEdit",
                    Title = "编辑客户信息",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/salesmanage/customer_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCustomerDelete",
                    Title = "删除客户信息",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/salesmanage/customer.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreForecastView",
                    Title = "浏览计划订单列表",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/forecast.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreForecastNew",
                    Title = "新增计划订单",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/forecast_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreForecastEdit",
                    Title = "编辑计划订单",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/forecast_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreForecastDelete",
                    Title = "删除计划订单",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/forecast.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreForecastInport",
                    Title = "导入计划订单",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/forecast_inport.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreInboundScanView",
                    Title = "浏览入库扫描列表",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/inbound_scan.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreInboundScanNew",
                    Title = "新增入库扫描",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/inbound_scan_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreInboundScanEdit",
                    Title = "编辑入库扫描",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/inbound_scan_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreInboundScanDelete",
                    Title = "删除入库扫描",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/inbound_scan.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOutboundScanView",
                    Title = "浏览出库扫描列表",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/outbound_scan.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOutboundScanNew",
                    Title = "新增出库扫描",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/outbound_scan_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOutboundScanEdit",
                    Title = "编辑出库扫描",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/outbound_scan_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOutboundScanDelete",
                    Title = "删除出库扫描",
                    GroupName = "销售管理",
                    NavigateUrl="~/Lf_Manufacturing/SD/shipment/outbound_scan.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreNoticeView",
                    Title = "浏览公告列表",
                    GroupName = "公告通知",
                    NavigateUrl="~/Lf_Office/OA/notice.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreNoticeNew",
                    Title = "新增公告信息",
                    GroupName = "公告通知",
                    NavigateUrl="~/Lf_Office/OA/notice_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreNoticeEdit",
                    Title = "编辑公告信息",
                    GroupName = "公告通知",
                    NavigateUrl="~/Lf_Office/OA/notice_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreNoticeDelete",
                    Title = "删除公告信息",
                    GroupName = "公告通知",
                    NavigateUrl="~/Lf_Office/OA/notice.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMeetingView",
                    Title = "浏览会议信息列表",
                    GroupName = "会议管理",
                    NavigateUrl="~/Lf_Office/OA/meeting.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMeetingNew",
                    Title = "新增会议信息",
                    GroupName = "会议管理",
                    NavigateUrl="~/Lf_Office/OA/meeting_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMeetingEdit",
                    Title = "编辑会议信息",
                    GroupName = "会议管理",
                    NavigateUrl="~/Lf_Office/OA/meeting_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMeetingDelete",
                    Title = "删除会议信息",
                    GroupName = "会议管理",
                    NavigateUrl="~/Lf_Office/OA/meeting.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreVehiclesView",
                    Title = "浏览车辆信息列表",
                    GroupName = "车辆管理",
                    NavigateUrl="~/Lf_Office/OA/vehicles.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreVehiclesNew",
                    Title = "新增车辆信息",
                    GroupName = "车辆管理",
                    NavigateUrl="~/Lf_Office/OA/vehicles_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreVehiclesEdit",
                    Title = "编辑车辆信息",
                    GroupName = "车辆管理",
                    NavigateUrl="~/Lf_Office/OA/vehicles_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreVehiclesDelete",
                    Title = "删除车辆信息",
                    GroupName = "车辆管理",
                    NavigateUrl="~/Lf_Office/OA/vehicles.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWeeklyView",
                    Title = "浏览周报信息列表",
                    GroupName = "周报管理",
                    NavigateUrl="~/Lf_Office/OA/weekly.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWeeklyNew",
                    Title = "新增周报信息",
                    GroupName = "周报管理",
                    NavigateUrl="~/Lf_Office/OA/weekly_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWeeklyEdit",
                    Title = "编辑周报信息",
                    GroupName = "周报管理",
                    NavigateUrl="~/Lf_Office/OA/weekly_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWeeklyDelete",
                    Title = "删除周报信息",
                    GroupName = "周报管理",
                    NavigateUrl="~/Lf_Office/OA/weekly.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreHelpdeskView",
                    Title = "浏览帮助工单列表",
                    GroupName = "帮助工单",
                    NavigateUrl="~/Lf_Office/OA/helpdesk.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreHelpdeskNew",
                    Title = "新增帮助工单",
                    GroupName = "帮助工单",
                    NavigateUrl="~/Lf_Office/OA/helpdesk_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreHelpdeskEdit",
                    Title = "编辑帮助工单",
                    GroupName = "帮助工单",
                    NavigateUrl="~/Lf_Office/OA/helpdesk_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreHelpdeskDelete",
                    Title = "删除帮助工单",
                    GroupName = "帮助工单",
                    NavigateUrl="~/Lf_Office/OA/helpdesk.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreAddressView",
                    Title = "浏览通讯录列表",
                    GroupName = "通讯录",
                    NavigateUrl="~/Lf_Office/OA/address.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreAddressNew",
                    Title = "新增通讯录",
                    GroupName = "通讯录",
                    NavigateUrl="~/Lf_Office/OA/address_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreAddressEdit",
                    Title = "编辑通讯录",
                    GroupName = "通讯录",
                    NavigateUrl="~/Lf_Office/OA/address_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreAddressDelete",
                    Title = "删除通讯录",
                    GroupName = "通讯录",
                    NavigateUrl="~/Lf_Office/OA/address.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreExpensesView",
                    Title = "浏览费用信息列表",
                    GroupName = "财务管理",
                    NavigateUrl="~/Lf_Accounting/expense.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreExpensesNew",
                    Title = "新增费用信息",
                    GroupName = "财务管理",
                    NavigateUrl="~/Lf_Accounting/expense.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreExpensesEdit",
                    Title = "编辑费用信息",
                    GroupName = "财务管理",
                    NavigateUrl="~/Lf_Accounting/expense.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreExpensesDelete",
                    Title = "删除费用信息",
                    GroupName = "财务管理",
                    NavigateUrl="~/Lf_Accounting/expense.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCostingView",
                    Title = "浏览成本信息列表",
                    GroupName = "财务管理",
                    NavigateUrl="~/Lf_Accounting/costing.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetView",
                    Title = "浏览预算信息列表",
                    GroupName = "预算管理",
                    NavigateUrl="~/Lf_Accounting/budget.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetNew",
                    Title = "新增预算信息",
                    GroupName = "预算管理",
                    NavigateUrl="~/Lf_Accounting/budget_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetEdit",
                    Title = "编辑预算信息",
                    GroupName = "预算管理",
                    NavigateUrl="~/Lf_Accounting/budget_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetInport",
                    Title = "导入预算信息",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetDelete",
                    Title = "删除预算信息",
                    GroupName = "预算管理",
                    NavigateUrl="~/Lf_Accounting/budget.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetGDView",
                    Title = "浏览预算信息GD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetGDNew",
                    Title = "新增预算信息GD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetGDEdit",
                    Title = "编辑预算信息GD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetGDDelete",
                    Title = "删除预算信息GD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetGDInport",
                    Title = "导入预算信息GD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetLDView",
                    Title = "浏览预算信息LD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetLDNew",
                    Title = "新增预算信息LD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetLDEdit",
                    Title = "编辑预算信息LD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetLDDelete",
                    Title = "删除预算信息LD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetLDInport",
                    Title = "导入预算信息LD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetITView",
                    Title = "浏览预算信息IT",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetITNew",
                    Title = "新增预算信息IT",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetITEdit",
                    Title = "编辑预算信息IT",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetITDelete",
                    Title = "删除预算信息IT",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetITInport",
                    Title = "导入预算信息IT",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetMMView",
                    Title = "浏览预算信息MM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetMMNew",
                    Title = "新增预算信息MM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetMMEdit",
                    Title = "编辑预算信息MM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetMMDelete",
                    Title = "删除预算信息MM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetMMInport",
                    Title = "导入预算信息MM",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPMView",
                    Title = "浏览预算信息PM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPMNew",
                    Title = "新增预算信息PM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPMEdit",
                    Title = "编辑预算信息PM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPMDelete",
                    Title = "删除预算信息PM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPMInport",
                    Title = "导入预算信息PM",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPDView",
                    Title = "浏览预算信息PD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPDNew",
                    Title = "新增预算信息PD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPDEdit",
                    Title = "编辑预算信息PD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPDDelete",
                    Title = "删除预算信息PD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPDInport",
                    Title = "导入预算信息PD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetENGView",
                    Title = "浏览预算信息ENG",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetENGNew",
                    Title = "新增预算信息ENG",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetENGEdit",
                    Title = "编辑预算信息ENG",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetENGDelete",
                    Title = "删除预算信息ENG",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetENGInport",
                    Title = "导入预算信息ENG",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetIQCView",
                    Title = "浏览预算信息IQC",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetIQCNew",
                    Title = "新增预算信息IQC",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetIQCEdit",
                    Title = "编辑预算信息IQC",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetIQCDelete",
                    Title = "删除预算信息IQC",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetIQCInport",
                    Title = "导入预算信息IQC",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetQAView",
                    Title = "浏览预算信息QA",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetQANew",
                    Title = "新增预算信息QA",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetQAEdit",
                    Title = "编辑预算信息QA",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetQADelete",
                    Title = "删除预算信息QA",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetQAInport",
                    Title = "导入预算信息QA",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPEView",
                    Title = "浏览预算信息PE",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPENew",
                    Title = "新增预算信息PE",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPEEdit",
                    Title = "编辑预算信息PE",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPEDelete",
                    Title = "删除预算信息PE",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetPEInport",
                    Title = "导入预算信息PE",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP1DView",
                    Title = "浏览预算信息P1D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP1DNew",
                    Title = "新增预算信息P1D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP1DEdit",
                    Title = "编辑预算信息P1D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP1DDelete",
                    Title = "删除预算信息P1D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP1DInport",
                    Title = "导入预算信息P1D",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP2DView",
                    Title = "浏览预算信息P2D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP2DNew",
                    Title = "新增预算信息P2D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP2DEdit",
                    Title = "编辑预算信息P2D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP2DDelete",
                    Title = "删除预算信息P2D",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetP2DInport",
                    Title = "导入预算信息P2D",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetCDView",
                    Title = "浏览预算信息CD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetCDNew",
                    Title = "新增预算信息CD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetCDEdit",
                    Title = "编辑预算信息CD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetCDDelete",
                    Title = "删除预算信息CD",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetCDInport",
                    Title = "导入预算信息CD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetOEMView",
                    Title = "浏览预算信息OEM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetOEMNew",
                    Title = "新增预算信息OEM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetOEMEdit",
                    Title = "编辑预算信息OEM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetOEMDelete",
                    Title = "删除预算信息OEM",
                    GroupName = "部门预算",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBudgetOEMInport",
                    Title = "导入预算信息OEM",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Accounting/budget_input.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreAuditView",
                    Title = "浏览审核信息列表",
                    GroupName = "预算审核",
                    NavigateUrl="~/Lf_Accounting/audit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreAuditNew",
                    Title = "新增审核信息",
                    GroupName = "预算审核",
                    NavigateUrl="~/Lf_Accounting/audit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreAuditEdit",
                    Title = "编辑审核信息",
                    GroupName = "预算审核",
                    NavigateUrl="~/Lf_Accounting/audit.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreAuditCancel",
                    Title = "取消审核信息",
                    GroupName = "预算审核",
                    NavigateUrl="~/Lf_Accounting/audit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreAuditDelete",
                    Title = "删除审核信息",
                    GroupName = "预算审核",
                    NavigateUrl="~/Lf_Accounting/audit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCountersignatureView",
                    Title = "浏览会签信息列表",
                    GroupName = "会签管理",
                    NavigateUrl="~/Lf_Accounting/countersignature.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCountersignatureNew",
                    Title = "新增会签信息",
                    GroupName = "会签管理",
                    NavigateUrl="~/Lf_Accounting/countersignature.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCountersignatureEdit",
                    Title = "编辑会签信息",
                    GroupName = "会签管理",
                    NavigateUrl="~/Lf_Accounting/countersignature.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreCountersignatureDelete",
                    Title = "删除会签信息",
                    GroupName = "会签管理",
                    NavigateUrl="~/Lf_Accounting/countersignature.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePeriodView",
                    Title = "浏览预算期间列表",
                    GroupName = "预算期间",
                    NavigateUrl="~/Lf_Accounting/period.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePeriodNew",
                    Title = "新增预算期间",
                    GroupName = "预算期间",
                    NavigateUrl="~/Lf_Accounting/period.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePeriodEdit",
                    Title = "编辑预算期间",
                    GroupName = "预算期间",
                    NavigateUrl="~/Lf_Accounting/period.aspx",
                },
                new Adm_Power
                {
                    Name = "CorePeriodDelete",
                    Title = "删除预算期间",
                    GroupName = "预算期间",
                    NavigateUrl="~/Lf_Accounting/period.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSubjectView",
                    Title = "浏览预算科目列表",
                    GroupName = "预算科目",
                    NavigateUrl="~/Lf_Accounting/subject.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSubjectNew",
                    Title = "新增预算科目",
                    GroupName = "预算科目",
                    NavigateUrl="~/Lf_Accounting/subject.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSubjectEdit",
                    Title = "编辑预算科目",
                    GroupName = "预算科目",
                    NavigateUrl="~/Lf_Accounting/subject.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSubjectDelete",
                    Title = "删除预算科目",
                    GroupName = "预算科目",
                    NavigateUrl="~/Lf_Accounting/subject.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcView",
                    Title = "浏览设变列表",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcNew",
                    Title = "新增设变权限",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcEdit",
                    Title = "编辑设变权限",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcDelete",
                    Title = "删除设变权限",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcInport",
                    Title = "导入设变权限",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTlView",
                    Title = "浏览技联列表",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/TL/liaison.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTlNew",
                    Title = "新增技联权限",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/liaison.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTlEdit",
                    Title = "编辑技联权限",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/liaison.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTlDelete",
                    Title = "删除技联权限",
                    GroupName = "设变管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/liaison.aspx",
                },
               new Adm_Power
                {
                    Name = "CoreEcGDView",
                    Title = "浏览设变信息GD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcGDNew",
                    Title = "新增设变信息GD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcGDEdit",
                    Title = "编辑设变信息GD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcGDDelete",
                    Title = "删除设变信息GD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcGDInport",
                    Title = "导入设变信息GD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcLDView",
                    Title = "浏览设变信息LD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcLDNew",
                    Title = "新增设变信息LD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcLDEdit",
                    Title = "编辑设变信息LD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcLDDelete",
                    Title = "删除设变信息LD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcLDInport",
                    Title = "导入设变信息LD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcITView",
                    Title = "浏览设变信息IT",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcITNew",
                    Title = "新增设变信息IT",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcITEdit",
                    Title = "编辑设变信息IT",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcITDelete",
                    Title = "删除设变信息IT",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcITInport",
                    Title = "导入设变信息IT",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMMView",
                    Title = "浏览设变信息MM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_mm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMMNew",
                    Title = "新增设变信息MM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_mm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMMEdit",
                    Title = "编辑设变信息MM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_Mm_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMMDelete",
                    Title = "删除设变信息MM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_mm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMMInport",
                    Title = "导入设变信息MM",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_mm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPMView",
                    Title = "浏览设变信息PM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPMNew",
                    Title = "新增设变信息PM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPMEdit",
                    Title = "编辑设变信息PM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pm_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPMDelete",
                    Title = "删除设变信息PM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPMInport",
                    Title = "导入设变信息PM",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pm.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPDView",
                    Title = "浏览设变信息PD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pd.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPDNew",
                    Title = "新增设变信息PD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pd.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPDEdit",
                    Title = "编辑设变信息PD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pd_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPDDelete",
                    Title = "删除设变信息PD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pd.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPDInport",
                    Title = "导入设变信息PD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_pd.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcENGView",
                    Title = "浏览设变信息ENG",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_view.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcENGNew",
                    Title = "新增设变信息ENG",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcENGEdit",
                    Title = "编辑设变信息ENG",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcENGDelete",
                    Title = "删除设变信息ENG",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_view.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcENGInport",
                    Title = "导入设变信息ENG",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcQAInport",
                    Title = "导入设变信息QA",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qa.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMatView",
                    Title = "浏览物料确认",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng_outbound.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMatNew",
                    Title = "新增物料确认",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng_outbound.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMatEdit",
                    Title = "编辑物料确认",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng_outbound.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMatDelete",
                    Title = "删除物料确认",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng_outbound.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcMatInport",
                    Title = "导入物料确认",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_eng_outbound.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcIQCView",
                    Title = "浏览设变信息IQC",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qc.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcIQCNew",
                    Title = "新增设变信息IQC",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qc.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcIQCEdit",
                    Title = "编辑设变信息IQC",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qc_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcIQCDelete",
                    Title = "删除设变信息IQC",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qc.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcIQCInport",
                    Title = "导入设变信息IQC",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qc.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcQAView",
                    Title = "浏览设变信息QA",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qa.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcQANew",
                    Title = "新增设变信息QA",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qa.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcQAEdit",
                    Title = "编辑设变信息QA",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qa_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcQADelete",
                    Title = "删除设变信息QA",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qa.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcQAInport",
                    Title = "导入设变信息QA",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_qa.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPEView",
                    Title = "浏览设变信息PE",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPENew",
                    Title = "新增设变信息PE",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPEEdit",
                    Title = "编辑设变信息PE",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPEDelete",
                    Title = "删除设变信息PE",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcPEInport",
                    Title = "导入设变信息PE",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP1DView",
                    Title = "浏览设变信息P1D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p1d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP1DNew",
                    Title = "新增设变信息P1D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p1d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP1DEdit",
                    Title = "编辑设变信息P1D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p1d_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP1DDelete",
                    Title = "删除设变信息P1D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p1d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP1DInport",
                    Title = "导入设变信息P1D",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p1d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP2DView",
                    Title = "浏览设变信息P2D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p2d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP2DNew",
                    Title = "新增设变信息P2D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p2d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP2DEdit",
                    Title = "编辑设变信息P2D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p2d_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP2DDelete",
                    Title = "删除设变信息P2D",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p2d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcP2DInport",
                    Title = "导入设变信息P2D",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec_p2d.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcCDView",
                    Title = "浏览设变信息CD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcCDNew",
                    Title = "新增设变信息CD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcCDEdit",
                    Title = "编辑设变信息CD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcCDDelete",
                    Title = "删除设变信息CD",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcCDInport",
                    Title = "导入设变信息CD",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcOEMView",
                    Title = "浏览设变信息OEM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcOEMNew",
                    Title = "新增设变信息OEM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcOEMEdit",
                    Title = "编辑设变信息OEM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcOEMDelete",
                    Title = "删除设变信息OEM",
                    GroupName = "部门设变",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcOEMInport",
                    Title = "导入设变信息OEM",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/EC/ec.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLineView",
                    Title = "浏览班组列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_line.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLineNew",
                    Title = "新增班组信息",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_line_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLineEdit",
                    Title = "编辑班组信息",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_line_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLineDelete",
                    Title = "删除班组信息",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_line.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcmanaView",
                    Title = "浏览管理区分列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Ec_admin.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcmanaNew",
                    Title = "新增管理区分",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Ec_admin_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcmanaEdit",
                    Title = "编辑管理区分",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Ec_admin_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreEcmanaDelete",
                    Title = "删除管理区分",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Ec_admin.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMaterialView",
                    Title = "浏览物料权限",
                    GroupName = "物料信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/material.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMaterialNew",
                    Title = "新增物料权限",
                    GroupName = "物料信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/material_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMaterialEdit",
                    Title = "编辑物料权限",
                    GroupName = "物料信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/material_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMaterialDelete",
                    Title = "删除物料权限",
                    GroupName = "物料信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/material.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreMaterialInport",
                    Title = "导入物料权限",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/MM/material.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreModelsView",
                    Title = "浏览机种列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/matmodel.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreModelsNew",
                    Title = "新增机种",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/matmodel_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreModelsEdit",
                    Title = "编辑机种",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/matmodel_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreModelsDelete",
                    Title = "删除机种",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/MM/matmodel.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreModelsInport",
                    Title = "导入机种",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/MM/matmodel.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOrderView",
                    Title = "浏览订单列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_order.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOrderNew",
                    Title = "新增订单",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_order_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOrderEdit",
                    Title = "编辑订单",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_order_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOrderDelete",
                    Title = "删除订单",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_order.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreManhourView",
                    Title = "浏览工时列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_manhour.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreManhourNew",
                    Title = "新增工时",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_manhour_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreManhourEdit",
                    Title = "编辑工时",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_manhour_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreManhourDelete",
                    Title = "删除工时",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_manhour.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreTimeView",
                    Title = "浏览工数列表",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/timesheet/times_query.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTimeNew",
                    Title = "新增工数",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/timesheet/times_query.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTimeEdit",
                    Title = "编辑工数",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/timesheet/times_query.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTimeDelete",
                    Title = "删除工数",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/timesheet/times_query.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreNotReachedView",
                    Title = "浏览未达成原因列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreNotReachedNew",
                    Title = "新增未达成原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreNotReachedEdit",
                    Title = "编辑未达成原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreNotReachedDelete",
                    Title = "删除未达成原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreLineStopView",
                    Title = "浏览停线原因列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLineStopdNew",
                    Title = "新增停线原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLineStopEdit",
                    Title = "编辑停线原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreLineStopDelete",
                    Title = "删除停线原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_reason.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreBadReasonView",
                    Title = "浏览不良原因列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_badcategory.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBadReasonNew",
                    Title = "新增不良原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_badcategory_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBadReasonEdit",
                    Title = "编辑不良原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_badcategory_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreBadReasonDelete",
                    Title = "删除不良原因",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_badcategory.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreUtilizationView",
                    Title = "浏览稼动率列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_efficiency.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUtilizationNew",
                    Title = "新增稼动率",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_efficiency_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUtilizationEdit",
                    Title = "编辑稼动率",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_efficiency_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreUtilizationDelete",
                    Title = "删除稼动率",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_efficiency.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreInspectCatView",
                    Title = "浏览检验类别列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Qm_acceptcat.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreInspectCatNew",
                    Title = "新增检验类别",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Qm_acceptcat_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreInspectCatEdit",
                    Title = "编辑检验类别",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Qm_acceptcat_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreInspectCatDelete",
                    Title = "删除检验类别",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Qm_acceptcat.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreTransportView",
                    Title = "浏览运输方式列表",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_transport.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTransportNew",
                    Title = "新增运输方式",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_transport_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTransportEdit",
                    Title = "编辑运输方式",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_transport_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreTransportDelete",
                    Title = "删除运输方式",
                    GroupName = "基本信息",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_transport.aspx",
                },
                                new Adm_Power
                {
                    Name = "CoreSopView",
                    Title = "浏览SOP信息",
                    GroupName = "SOP管理",
                    NavigateUrl="~/Lf_Manufacturing/SOP/sop.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSopNew",
                    Title = "新增SOP信息",
                    GroupName = "SOP管理",
                    NavigateUrl="~/Lf_Manufacturing/SOP/sop.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSopEdit",
                    Title = "编辑SOP信息",
                    GroupName = "SOP管理",
                    NavigateUrl="~/Lf_Manufacturing/SOP/sop.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreSopDelete",
                    Title = "删除SOP信息",
                    GroupName = "SOP管理",
                    NavigateUrl="~/Lf_Manufacturing/SOP/sop.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreP1DOutputView",
                    Title = "浏览生产日报列表",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P1D_daily.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP1DOutputNew",
                    Title = "新增生产日报",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P1D_daily_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP1DOutputEdit",
                    Title = "编辑生产日报",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P1D_daily_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP1DOutputDelete",
                    Title = "删除生产日报",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P1D_daily.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP1DOutputSubEdit",
                    Title = "编辑生产日报SUB",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P1D_daily_sub_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP1DDefectView",
                    Title = "浏览生产不良列表",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P1D_defect.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP1DDefectNew",
                    Title = "新增生产不良",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P1D_defect_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP1DDefectEdit",
                    Title = "编辑生产不良",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P1D_defect_edit.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreP1DDefectDelete",
                    Title = "删除生产不良",
                    GroupName = "制一生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P1D_defect.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP2DOutputView",
                    Title = "浏览生产日报列表",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P2D_daily.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP2DOutputNew",
                    Title = "新增生产日报",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P2D_daily_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP2DOutputEdit",
                    Title = "编辑生产日报",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P2D_daily_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP2DOutputDelete",
                    Title = "删除生产日报",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P2D_daily.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP2DOutputSubEdit",
                    Title = "编辑生产日报SUB",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/daily/P2D_daily_sub_edit.aspx",
                },

                 new Adm_Power
                {
                    Name = "CoreP2DDefectView",
                    Title = "浏览生产不良列表",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P2D_defect.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP2DDefectNew",
                    Title = "新增生产不良",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P2D_defect_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreP2DDefectEdit",
                    Title = "编辑生产不良",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P2D_defect_edit.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreP2DDefectDelete",
                    Title = "删除生产不良",
                    GroupName = "制二生产",
                    NavigateUrl="~/Lf_Manufacturing/PP/poor/P2D_defect.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreKanbanView",
                    Title = "浏览生产看板列表",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreKanbanNew",
                    Title = "新增生产看板",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban_new.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreKanbanEdit",
                    Title = "编辑产看板",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban_edit.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreKanbanDelete",
                    Title = "删除生产看板",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreTrackingView",
                    Title = "浏览批次可追溯列表",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreTrackingNew",
                    Title = "新增批次可追溯",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban_new.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreTrackingEdit",
                    Title = "编辑批次可追溯",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban_edit.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreTrackingDelete",
                    Title = "删除批次可追溯",
                    GroupName = "生产管理",
                    NavigateUrl="~/Lf_Manufacturing/Master/Pp_model_kanban.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreFqcActionView",
                    Title = "浏览品质对策列表",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_action.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcActionNew",
                    Title = "新增品质对策",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_action_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcActionEdit",
                    Title = "编辑品质对策",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_action_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcActionDelete",
                    Title = "删除品质对策",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_action.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcNoticeView",
                    Title = "浏览品质报告列表",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_notice.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcNoticeNew",
                    Title = "新增品质报告",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_notice_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcNoticeEdit",
                    Title = "编辑品质报告",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_notice_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcNoticeDelete",
                    Title = "删除品质报告",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_notice.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcView",
                    Title = "浏览入库检验列表",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcNew",
                    Title = "新增入库检验",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcEdit",
                    Title = "编辑入库检验",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreFqcDelete",
                    Title = "删除入库检验",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/fqc/fqc.aspx",
                },
                 new Adm_Power
                {
                    Name = "CoreComplaintView",
                    Title = "浏览客诉信息列表",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintNew",
                    Title = "新增客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintEdit",
                    Title = "编辑客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintDelete",
                    Title = "删除客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintQAView",
                    Title = "浏览客诉信息列表",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintQANew",
                    Title = "新增客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintQAEdit",
                    Title = "编辑客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintQADelete",
                    Title = "删除客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintP1DView",
                    Title = "浏览客诉信息列表",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintP1DNew",
                    Title = "新增客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintP1DEdit",
                    Title = "编辑客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreComplaintP1DDelete",
                    Title = "删除客诉信息",
                    GroupName = "品质管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/complaint/complaint.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWagesView",
                    Title = "浏览平均工资率列表",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Accounting/wagerate.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWagesNew",
                    Title = "新增平均工资率",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Accounting/wagerate_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWagesEdit",
                    Title = "编辑平均工资率",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Accounting/wagerate_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWagesDelete",
                    Title = "删除平均工资率",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Accounting/wagerate.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOperationCostView",
                    Title = "浏览品质业务列表",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/operation_cost.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOperationCostNew",
                    Title = "新增品质业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/operation_cost_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOperationCostEdit",
                    Title = "编辑品质业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/operation_cost_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreOperationCostDelete",
                    Title = "删除品质业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/operation_cost.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreReworkCostView",
                    Title = "浏览返修业务列表",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/rework_cost.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreReworkCostNew",
                    Title = "新增返修业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/rework_cost_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreReworkCostEdit",
                    Title = "编辑返修业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/rework_cost_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreReworkCostDelete",
                    Title = "删除返修业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/rework_cost.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWasteCostView",
                    Title = "浏览废弃业务列表",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/waste_cost.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWasteCostNew",
                    Title = "新增废弃业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/waste_cost_new.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWasteCostEdit",
                    Title = "编辑废弃业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/waste_cost_edit.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWasteCostDelete",
                    Title = "删除废弃业务",
                    GroupName = "品质业务",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/waste_cost.aspx",
                },
                new Adm_Power
                {
                    Name = "CoreWasteCostInport",
                    Title = "导入废弃业务",
                    GroupName = "导入管理",
                    NavigateUrl="~/Lf_Manufacturing/QM/cost/waste_input.aspx",
                },

                new Adm_Power
                {
                    Name = "CoreKitInput",
                    Title = "导入权限",
                    GroupName = "通用权限"
                },
                new Adm_Power
                {
                    Name = "CoreKitOutput",
                    Title = "输出权限",
                    GroupName = "通用权限"
                },
                new Adm_Power
                {
                    Name = "CoreKitPrint",
                    Title = "打印权限",
                    GroupName = "通用权限"
                },
                new Adm_Power
                {
                    Name = "CoreKitDesgin",
                    Title = "打印设计",
                    GroupName = "通用权限"
                },
                new Adm_Power
                {
                    Name = "CoreKitSetup",
                    Title = "打印维护",
                    GroupName = "通用权限"
                },


            };

            return Adm_Powers;
        }
        private static List<Adm_Role> GetAdm_Roles()
        {
            var roles = new List<Adm_Role>()
            {
                new Adm_Role()
                {

                    Name = "系统管理员",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "开发人员",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "总经理室",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "总务课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "财务课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "电脑课",
                    Remark = ""
                },

                new Adm_Role()
                {

                    Name = "OEM",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "制一设变",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "制一OPH",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "制二设变",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "制二OPH",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "技术课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "生管课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "部管课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "报关课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "采购课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "受检课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "品管课",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "制技课",
                    Remark = ""
                },

                new Adm_Role()
                {

                    Name = "TOS",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "TCJ",
                    Remark = ""
                },
                new Adm_Role()
                {

                    Name = "浏览",
                    Remark = ""
                },
            };

            return roles;
        }
        private static List<Adm_User> GetAdm_Users()
        {

            var Adm_Users = new List<Adm_User>
            {

            // 添加超级管理员
            new Adm_User
            {
                Name = "admin",
                Gender = "男",
                Password = PasswordUtil.CreateDbPassword("admin"),
                ChineseName = "超级管理员",
                Email = "itsup@teac.com.cn",
                Enabled = true,

                CreateTime = DateTime.Now


            },
        };
            return Adm_Users;
        }
        private static List<Adm_Dept> GetAdm_Depts()
        {
            var Adm_Depts = new List<Adm_Dept> {
                new Adm_Dept
                {

                    Name = "DTA",
                    SortIndex = 1,
                    Remark = "顶级部门",
                    Children = new List<Adm_Dept>
                    {
                        new Adm_Dept
                        {
                            Name = "总经理室",
                            SortIndex = 1,
                            Remark = "二级部门"
                        },
                         new Adm_Dept
                         {
                             Name = "总务部",
                             SortIndex = 2,
                             Remark = "二级部门",
                             Children = new List<Adm_Dept>
                             {
                                new Adm_Dept
                                {
                                    Name = "总务课",
                                    SortIndex = 1,
                                    Remark = "三级部门"
                                }
                            }
                        },
                        new Adm_Dept
                        {
                            Name = "财务课",
                            SortIndex = 3,
                            Remark = "二级部门"
                        },
                        new Adm_Dept
                        {
                            Name = "电脑课",
                            SortIndex = 4,
                            Remark = "二级部门"
                        },
                            new Adm_Dept
                            {

                                Name = "事业推进本部",
                                SortIndex = 6,
                                Remark = "二级部门",
                                Children = new List<Adm_Dept>
                                {
                                    new Adm_Dept
                                    {

                                        Name = "管理部",
                                        SortIndex = 1,
                                        Remark = "三级部门"
                                   ,
                                        Children = new List<Adm_Dept>
                                        {
                                            new Adm_Dept
                                            {

                                                Name = "报关课",
                                                SortIndex = 1,
                                                Remark = "四级部门"
                                            },
                                            new Adm_Dept
                                            {

                                                Name = "生管课",
                                                SortIndex = 2,
                                                Remark = "四级部门"
                                            },
                                            new Adm_Dept
                                            {

                                                Name = "部管课",
                                                SortIndex = 3,
                                                Remark = "四级部门"
                                            }
                                        }
                                    },
                                    new Adm_Dept
                                    {

                                        Name = "资材部",
                                        SortIndex = 1,
                                        Remark = "三级部门",
                                        Children = new List<Adm_Dept>
                                        {
                                            new Adm_Dept
                                            {

                                                Name = "采购课",
                                                SortIndex = 1,
                                                Remark = "四级部门"
                                            }
                                        }
                                    },
                                 }

                             },
                            new Adm_Dept
                            {
                                Name = "生产改善推进本部",
                                SortIndex = 6,
                                Remark = "二级部门",
                                Children = new List<Adm_Dept>
                                {
                                    new Adm_Dept
                                    {

                                        Name = "生产部",
                                        SortIndex = 1,
                                        Remark = "三级部门",
                                        Children = new List<Adm_Dept>
                                        {
                                            new Adm_Dept
                                            {

                                                Name = "制一课",
                                                SortIndex = 1,
                                                Remark = "四级部门"
                                            },
                                            new Adm_Dept
                                            {

                                                Name = "制二课",
                                                SortIndex = 2,
                                                Remark = "四级部门"
                                            },
                                            new Adm_Dept
                                            {

                                                Name = "制造技术课",
                                                SortIndex = 3,
                                                Remark = "四级部门"
                                            }

                                        }
                                    },
                                    new Adm_Dept
                                    {

                                        Name = "技术部",
                                        SortIndex = 2,
                                        Remark = "三级部门",
                                        Children = new List<Adm_Dept>
                                        {
                                            new Adm_Dept
                                            {
                                                Name = "技术课",
                                                SortIndex = 1,
                                                Remark = "四级部门"
                                            },
                                        }
                                    },
                                    new Adm_Dept
                                    {

                                        Name = "品保部",
                                        SortIndex = 3,
                                        Remark = "三级部门",
                                        Children = new List<Adm_Dept>
                                        {
                                            new Adm_Dept
                                            {
                                                Name = "受检课",
                                                SortIndex = 1,
                                                Remark = "四级部门"
                                            },
                                            new Adm_Dept
                                            {
                                                Name = "品管课",
                                                SortIndex = 2,
                                                Remark = "四级部门"
                                            },

                                        }
                                    },
                                },
                            },

                            new Adm_Dept
                            {
                                Name = "TCJ",
                                SortIndex = 7,
                                Remark = "二级部门"
                            },
                            new Adm_Dept
                            {
                                Name = "TOS",
                                SortIndex = 8,
                                Remark = "二级部门"
                            }
                        }
                    },
            };


            return Adm_Depts;
        }
        private static List<Adm_Config> GetAdm_Configs()
        {
            var Adm_Configs = new List<Adm_Config> {
                new Adm_Config
                {

                    ConfigKey= "Adm_Title",
                    ConfigValue= "LeanCloud（LeanKit）",
                    Remark = "网站的标题"
                },
                new Adm_Config
                {
                    ConfigKey= "PageSize",
                    ConfigValue= "20",
                    Remark = "表格每页显示的个数"
                },
                new Adm_Config
                {
                    ConfigKey= "Adm_MenuType",
                    ConfigValue= "tree",
                    Remark = "左侧菜单样式"
                },
                new Adm_Config
                {
                    ConfigKey= "Theme",
                    ConfigValue= "Cupertino",
                    Remark = "网站主题"
                },
                new Adm_Config
                {
                    ConfigKey= "HelpList",
                    ConfigValue= "[{\"Text\":\"日程\",\"Icon\":\"Calendar\",\"ID\":\"schedule\",\"URL\":\"~/Lf_Office/EM/schedule.aspx \"},{\"Text\":\"科学计算器\",\"Icon\":\"Calculator\",\"ID\":\"jisuanqi\",\"URL\":\"~/Lf_Admin/help/jisuanqi.htm\"},{\"Text\":\"系统帮助\",\"Icon\":\"Help\",\"ID\":\"help\",\"URL\":\"~/Lf_Admin/help/help_map.aspx\"}]",
                    Remark = "帮助下拉列表的JSON字符串"
                }
            };

            return Adm_Configs;
        }
        private static List<Adm_Institution> GetAdm_Institutions()
        {
            var comname = new List<Adm_Institution>()
            {
                new Adm_Institution()
                {
                    GUID=Guid.NewGuid(),
                    Category="外商独资",
                    EnName="DTA",
                    ShortName="DTA",
                    FullName="东莞蒂雅克电子有限公司",
                    EnFullName="DongGuan TEAC Electronics Co.,Ltd.",
                    Nature="电子制造业",
                    OuterPhone="+86 769 8554 8848",
                    InnerPhone="888",
                    Fax="+86 769 8554 4276",
                    Postalcode="823867",
                    Email="dta@abc.com.cn",
                    OrgCode="478512245555555555",
                    Corporate="yame rino",
                    ProvinceId="44",
                    CityId="441900000000",
                    CountyId="0",
                    TownId="441900119000",
                    VillageId="441900119014",
                    Address="上兴路1号",
                    EnAddress="NO.1 SHANGXINGLU,SHANGJIAO COMMUNITY,CHANGANTOWN,DONGGUAN CITY,GUANGDONG PROVINCE,CHINA",
                    WebAddress="www.teac.com.cn",
                    FoundedTime = new DateTime(1996, 3, 16, 00, 00, 00),
                    BusinessScope ="音响设备，专业板卡，数字激光唱机",
                    SortCode=1,
                    isDelete=0,
                    isEnabled=0,
                    Slogan="グロバール企業としてのプライドを持ちQCDSへの限りない挑戦。",
                    EnSlogan="グロバール企業としてのプライドを持ちQCDSへの限りない挑戦。",
                    JpSlogan="グロバール企業としてのプライドを持ちQCDSへの限りない挑戦。",
                    Remark="后台添加",
                    CreateTime  =DateTime.Now,
                    Creator="Admin"
                },


            };

            return comname;
        }
        private static List<Pp_DefectCode> GetPp_DefectCodes()
        {
            var Pp_DefectCodes = new List<Pp_DefectCode>()
            {
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="O",
                    cn_classmatter="OK",
                    en_classmatter="OK",
                    jp_classmatter="OK",
                    ngcode="1",
                    cn_ngmatter="OK",
                    en_ngmatter="OK",
                    jp_ngmatter="OK",
                    analysisclass="W",
                    jp_class="OK",
                    jp_ng="OK",
                    Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="P",
                    cn_classmatter="PCBA不良",
                    en_classmatter="PCB assembly NG",
                    jp_classmatter="PCBA不良",
                    ngcode="1",
                    cn_ngmatter="漏植",
                    en_ngmatter="Insert Leakage",
                    jp_ngmatter="挿し漏れ",
                    analysisclass="P",
                    jp_class="PCB assembly NG",
                    jp_ng="Insert Leakage",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="P",
                    cn_classmatter="PCBA不良",
                    en_classmatter="PCB assembly NG",
                    jp_classmatter="PCBA不良",
                    ngcode="2",
                    cn_ngmatter="误植",
                    en_ngmatter="Insert Mistake",
                    jp_ngmatter="誤挿し",
                    analysisclass="P",
                    jp_class="PCB assembly NG",
                    jp_ng="Insert Mistake",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="P",
                    cn_classmatter="PCBA不良",
                    en_classmatter="PCB assembly NG",
                    jp_classmatter="PCBA不良",
                    ngcode="3",
                    cn_ngmatter="短路",
                    en_ngmatter="Short",
                    jp_ngmatter="ショート",
                    analysisclass="P",
                    jp_class="PCB assembly NG",
                    jp_ng="Short",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="P",
                    cn_classmatter="PCBA不良",
                    en_classmatter="PCB assembly NG",
                    jp_classmatter="PCBA不良",
                    ngcode="4",
                    cn_ngmatter="开路",
                    en_ngmatter="Open",
                    jp_ngmatter="オープン",
                    analysisclass="P",
                    jp_class="PCB assembly NG",
                    jp_ng="Open",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="P",
                    cn_classmatter="PCBA不良",
                    en_classmatter="PCB assembly NG",
                    jp_classmatter="PCBA不良",
                    ngcode="5",
                    cn_ngmatter="焊接不良",
                    en_ngmatter="SolderiNG NG",
                    jp_ngmatter="半田不良",
                    analysisclass="P",
                    jp_class="PCB assembly NG",
                    jp_ng="SolderiNG NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="W",
                    cn_classmatter="作业不良",
                    en_classmatter="Main Line NG",
                    jp_classmatter="作業不良",
                    ngcode="2",
                    cn_ngmatter="误作业",
                    en_ngmatter="Work Mistake",
                    jp_ngmatter="誤作業",
                    analysisclass="W",
                    jp_class="Main Line NG",
                    jp_ng="Work Mistake",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="W",
                    cn_classmatter="作业不良",
                    en_classmatter="Main Line NG",
                    jp_classmatter="作業不良",
                    ngcode="3",
                    cn_ngmatter="锁付不良",
                    en_ngmatter="Screw NG",
                    jp_ngmatter="ネジ取付不良",
                    analysisclass="W",
                    jp_class="Main Line NG",
                    jp_ng="Screw NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="W",
                    cn_classmatter="作业不良",
                    en_classmatter="Main Line NG",
                    jp_classmatter="作業不良",
                    ngcode="4",
                    cn_ngmatter="调整不良",
                    en_ngmatter="Adjustment NG",
                    jp_ngmatter="調整不良",
                    analysisclass="W",
                    jp_class="Main Line NG",
                    jp_ng="Adjustment NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="W",
                    cn_classmatter="作业不良",
                    en_classmatter="Main Line NG",
                    jp_classmatter="作業不良",
                    ngcode="5",
                    cn_ngmatter="确认不良",
                    en_ngmatter="Confirmation NG",
                    jp_ngmatter="確認不良",
                    analysisclass="W",
                    jp_class="Main Line NG",
                    jp_ng="Confirmation NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="W",
                    cn_classmatter="作业不良",
                    en_classmatter="Main Line NG",
                    jp_classmatter="作業不良",
                    ngcode="6",
                    cn_ngmatter="外观部品作业损伤",
                    en_ngmatter="Damaged Material By Work",
                    jp_ngmatter="外観不良",
                    analysisclass="W",
                    jp_class="Main Line NG",
                    jp_ng="Damaged Material By Work",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="W",
                    cn_classmatter="作业不良",
                    en_classmatter="Main Line NG",
                    jp_classmatter="作業不良",
                    ngcode="1",
                    cn_ngmatter="漏作业",
                    en_ngmatter="Work Leakage",
                    jp_ngmatter="作業漏れ",
                    analysisclass="W",
                    jp_class="Main Line NG",
                    jp_ng="Work Leakage",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="K",
                    cn_classmatter="包裝不良",
                    en_classmatter="Pack NG",
                    jp_classmatter="梱包不良",
                    ngcode="1",
                    cn_ngmatter="漏装",
                    en_ngmatter="Assembly Leakage",
                    jp_ngmatter="入漏れ",
                    analysisclass="W",
                    jp_class="Pack NG",
                    jp_ng="Assembly Leakage",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="K",
                    cn_classmatter="包裝不良",
                    en_classmatter="Pack NG",
                    jp_classmatter="梱包不良",
                    ngcode="2",
                    cn_ngmatter="装错",
                    en_ngmatter="Assembly Mistake",
                    jp_ngmatter="入違い",
                    analysisclass="W",
                    jp_class="Pack NG",
                    jp_ng="Assembly Mistake",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="K",
                    cn_classmatter="包裝不良",
                    en_classmatter="Pack NG",
                    jp_classmatter="梱包不良",
                    ngcode="3",
                    cn_ngmatter="贴付不良",
                    en_ngmatter="Stick on NG",
                    jp_ngmatter="張り付け不良",
                    analysisclass="W",
                    jp_class="Pack NG",
                    jp_ng="Stick on NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="K",
                    cn_classmatter="包裝不良",
                    en_classmatter="Pack NG",
                    jp_classmatter="梱包不良",
                    ngcode="4",
                    cn_ngmatter="捆包材破损",
                    en_ngmatter="Damaged Pack Material",
                    jp_ngmatter="梱包材破損",
                    analysisclass="M",
                    jp_class="Pack NG",
                    jp_ng="Damaged Pack Material",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="M",
                    cn_classmatter="机心不良",
                    en_classmatter="MECHA",
                    jp_classmatter="メカ不良",
                    ngcode="1",
                    cn_ngmatter="动作不良",
                    en_ngmatter="Action NG",
                    jp_ngmatter="動作不良",
                    analysisclass="B",
                    jp_class="MECHA",
                    jp_ng="Action NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="M",
                    cn_classmatter="机心不良",
                    en_classmatter="MECHA",
                    jp_classmatter="メカ不良",
                    ngcode="2",
                    cn_ngmatter="性能不良",
                    en_ngmatter="Performance NG",
                    jp_ngmatter="性能不良",
                    analysisclass="B",
                    jp_class="MECHA",
                    jp_ng="Performance NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="M",
                    cn_classmatter="机心不良",
                    en_classmatter="MECHA",
                    jp_classmatter="メカ不良",
                    ngcode="3",
                    cn_ngmatter="寻迹不到",
                    en_ngmatter="Lead In NG",
                    jp_ngmatter="リードNG",
                    analysisclass="B",
                    jp_class="MECHA",
                    jp_ng="Lead In NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="M",
                    cn_classmatter="机心不良",
                    en_classmatter="MECHA",
                    jp_classmatter="メカ不良",
                    ngcode="4",
                    cn_ngmatter="异噪音",
                    en_ngmatter="Noise",
                    jp_ngmatter="ノイズ",
                    analysisclass="B",
                    jp_class="MECHA",
                    jp_ng="Noise",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="D",
                    cn_classmatter="設計不良",
                    en_classmatter="Design NG",
                    jp_classmatter="設計不良",
                    ngcode="2",
                    cn_ngmatter="机构设计不良",
                    en_ngmatter="Mechanism Design NG",
                    jp_ngmatter="機構設計不良",
                    analysisclass="M",
                    jp_class="Design NG",
                    jp_ng="Mechanism Design NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="D",
                    cn_classmatter="設計不良",
                    en_classmatter="Design NG",
                    jp_classmatter="設計不良",
                    ngcode="3",
                    cn_ngmatter="FW, Soft设计不良",
                    en_ngmatter="Firmware&Software Design NG",
                    jp_ngmatter="FW,ソフトウェア設計不良",
                    analysisclass="F",
                    jp_class="Design NG",
                    jp_ng="Firmware&Software Design NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="D",
                    cn_classmatter="設計不良",
                    en_classmatter="Design NG",
                    jp_classmatter="設計不良",
                    ngcode="1",
                    cn_ngmatter="回路设计不良",
                    en_ngmatter="Circuit Design NG",
                    jp_ngmatter="回路設計不良",
                    analysisclass="F",
                    jp_class="Design NG",
                    jp_ng="Circuit Design NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="B",
                    cn_classmatter="部品不良",
                    en_classmatter="Part NG",
                    jp_classmatter="部品不良",
                    ngcode="2",
                    cn_ngmatter="性能不良",
                    en_ngmatter="Performance NG",
                    jp_ngmatter="性能不良",
                    analysisclass="M",
                    jp_class="Part NG",
                    jp_ng="Performance NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="B",
                    cn_classmatter="部品不良",
                    en_classmatter="Part NG",
                    jp_classmatter="部品不良",
                    ngcode="3",
                    cn_ngmatter="尺寸不合",
                    en_ngmatter="Dimension NG",
                    jp_ngmatter="サイズが合わない",
                    analysisclass="M",
                    jp_class="Part NG",
                    jp_ng="Dimension NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="B",
                    cn_classmatter="部品不良",
                    en_classmatter="Part NG",
                    jp_classmatter="部品不良",
                    ngcode="4",
                    cn_ngmatter="印刷不良",
                    en_ngmatter="PrintiNG NG",
                    jp_ngmatter="印刷不良",
                    analysisclass="M",
                    jp_class="Part NG",
                    jp_ng="PrintiNG NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="B",
                    cn_classmatter="部品不良",
                    en_classmatter="Part NG",
                    jp_classmatter="部品不良",
                    ngcode="5",
                    cn_ngmatter="破损",
                    en_ngmatter="Damaged",
                    jp_ngmatter="破損",
                    analysisclass="M",
                    jp_class="Part NG",
                    jp_ng="Damaged",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="B",
                    cn_classmatter="部品不良",
                    en_classmatter="Part NG",
                    jp_classmatter="部品不良",
                    ngcode="6",
                    cn_ngmatter="变形",
                    en_ngmatter="Deform",
                    jp_ngmatter="変形",
                    analysisclass="M",
                    jp_class="Part NG",
                    jp_ng="Deform",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="B",
                    cn_classmatter="部品不良",
                    en_classmatter="Part NG",
                    jp_classmatter="部品不良",
                    ngcode="1",
                    cn_ngmatter="机能不良",
                    en_ngmatter="Function NG",
                    jp_ngmatter="機能不良",
                    analysisclass="F",
                    jp_class="Part NG",
                    jp_ng="Function NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                    },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="B",
                    cn_classmatter="部品不良",
                    en_classmatter="Part NG",
                    jp_classmatter="部品不良",
                    ngcode="7",
                    cn_ngmatter="外观不良",
                    en_ngmatter="Function NG",
                    jp_ngmatter="外観不良",
                    analysisclass="M",
                    jp_class="Part NG",
                    jp_ng="Function NG",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="1",
                    cn_ngmatter="学习",
                    en_ngmatter="Learning",
                    jp_ngmatter="勉強する",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now

                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="2",
                    cn_ngmatter="下机慢",
                    en_ngmatter="Production Slow",
                    jp_ngmatter="生産が遅い",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="3",
                    cn_ngmatter="测试慢",
                    en_ngmatter="Testing Slow",
                    jp_ngmatter="テストが遅い",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="4",
                    cn_ngmatter="清机中",
                    en_ngmatter="Cleaning",
                    jp_ngmatter="MOC端数生産",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="5",
                    cn_ngmatter="切换机种",
                    en_ngmatter="Model Change",
                    jp_ngmatter="切替機種",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="6",
                    cn_ngmatter="治具坏",
                    en_ngmatter="Jig Bad",
                    jp_ngmatter="治具が悪い",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="7",
                    cn_ngmatter="仪器坏",
                    en_ngmatter="Instrument Bad",
                    jp_ngmatter="装置が悪い",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_DefectCode()
                {
                    GUID=Guid.NewGuid(),
                    ngclass="S",
                    cn_classmatter="停线",
                    en_classmatter="LineStop",
                    jp_classmatter="一時の停止",
                    ngcode="8",
                    cn_ngmatter="部件不良",
                    en_ngmatter="Parts Bad",
                    jp_ngmatter="部品不具合",
                    analysisclass="S",
                    jp_class="Line Stop",
                    jp_ng="Line Stop",
                     Remark="程序后台添加",
                    Creator="admin",
                    CreateTime  =DateTime.Now
                }
            };
            return Pp_DefectCodes;
        }
        private static List<Pp_Line> GetPp_Lines()
        {
            var Pp_Lines = new List<Pp_Line>()
            {
               new Pp_Line()
                {
                   GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="AIA",
                    linename="自插A",
                    en_linename="Automatic Insertion A",
                    jp_linename="自挿A",
                    Remark="制二课",
                   isDelete =0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="AIB",
                    linename="自插B",
                    en_linename="Automatic Insertion B",
                    jp_linename="自挿B",
                    Remark="制二课",
                    isDelete =0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="AIC",
                    linename="自插C",
                    en_linename="Automatic Insertion C",
                    jp_linename="自挿C",
                    Remark="制二课",
                    isDelete =0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="AID",
                    linename="自插D",
                    en_linename="Automatic Insertion D",
                    jp_linename="自挿D",
                    Remark="制二课",
                    isDelete =0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="MIA",
                    linename="手插A",
                    en_linename="Manual Insertion A",
                    jp_linename="手插A",
                    Remark="制二课",
                    isDelete =0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="MIB",
                    linename="手插B",
                    en_linename="Manual Insertion B",
                    jp_linename="手插B",
                    Remark="制二课",
                    isDelete =0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="MIC",
                    linename="手插C",
                    en_linename="Manual Insertion C",
                    jp_linename="手插C",
                    Remark="制二课",
                    isDelete =0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="RA",
                    linename="修正A",
                    en_linename="Repair Line A",
                    jp_linename="修正A",
                    Remark="制二课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="RB",
                    linename="修正B",
                    en_linename="Repair Line B",
                    jp_linename="修正B",
                    Remark="制二课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="RC",
                    linename="修正C",
                    en_linename="Repair Line C",
                    jp_linename="修正C",
                    Remark="制二课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="SMT1",
                    linename="SMT1",
                    en_linename="SMT Line 1",
                    jp_linename="SMT1",
                    Remark="制二课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="P",
                    linecode="SMT2",
                    linename="SMT2",
                    en_linename="SMT Line 2",
                    jp_linename="SMT2",
                    Remark="制二课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="Q",
                    linecode="IQC",
                    linename="IQC",
                    en_linename="IQC CHECK",
                    jp_linename="受检课",
                    Remark="品保部",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now

                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="Q",
                    linecode="QA",
                    linename="QA",
                    en_linename="QA CHECK",
                    jp_linename="品管课",
                    Remark="品保部",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="1",
                    linename="1班",
                    en_linename="Assembly Line 1",
                    jp_linename="1班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="2",
                    linename="2班",
                    en_linename="Assembly Line 2",
                    jp_linename="2班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="3",
                    linename="3班",
                    en_linename="Assembly Line 3",
                    jp_linename="3班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="4",
                    linename="4班",
                    en_linename="Assembly Line 4",
                    jp_linename="4班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="5",
                    linename="5班",
                    en_linename="Assembly Line 5",
                    jp_linename="5班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="6",
                    linename="6班",
                    en_linename="Assembly Line 6",
                    jp_linename="6班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="7",
                    linename="7班",
                    en_linename="Assembly Line 7",
                    jp_linename="7班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="8",
                    linename="8班",
                    en_linename="Assembly Line 8",
                    jp_linename="8班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="9",
                    linename="9班",
                    en_linename="Assembly Line 9",
                    jp_linename="9班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="10",
                    linename="10班",
                    en_linename="Assembly Line 10",
                    jp_linename="10班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="11",
                    linename="11班",
                    en_linename="Assembly Line 11",
                    jp_linename="11班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="12",
                    linename="12班",
                    en_linename="Assembly Line 12",
                    jp_linename="12班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="13",
                    linename="13班",
                    en_linename="Assembly Line 13",
                    jp_linename="13班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="14",
                    linename="14班",
                    en_linename="Assembly Line 14",
                    jp_linename="14班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="15",
                    linename="15班",
                    en_linename="Assembly Line 15",
                    jp_linename="15班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                },
                new Pp_Line()
                {
                    GUID=Guid.NewGuid(),
                    lineclass="M",
                    linecode="16",
                    linename="加工班",
                    en_linename="Sub Line",
                    jp_linename="加工班",
                    Remark="制一课",isDelete=0,
                    Creator="admin",
                    CreateTime  =DateTime.Now
                }
            };
            return Pp_Lines;
        }
        private static List<Pp_Duration> GetPp_Worktimes()
        {
            string[] Stime = { "08:00", "09:00", "10:10", "11:10", "13:30", "14:30", "15:40", "16:40", "18:30", "19:30", "20:30", "21:30", "22:30", "09:00", "10:00", "11:10", "12:10", "14:30", "15:30", "16:40", "17:40", "19:30", "20:30", "21:30", "22:30", "23:30" };
            //string[] Etime = {  };

            var worktimes = new List<Pp_Duration>();

            for (int i = 0, scount = Stime.Length - 13; i < scount; i += 1)
            {
                string Prostime = Stime[i];
                string Proetime = Stime[i + 13];
                worktimes.Add(new Pp_Duration
                {
                    GUID = Guid.NewGuid(),
                    Prostime = Prostime,
                    Proetime = Proetime,

                    Remark = "程序后台添加",
                    isDelete = 0,
                    Creator = "admin",
                    CreateTime = DateTime.Now

                });
            }

            //    var Pp_Worktimes = new List<Pp_Worktime>()
            //{
            //    new Pp_Worktime()
            //    {
            //        GUID=Guid.NewGuid(),
            //        Prostime="8:00",
            //        Proetime="9:00",
            //        Remark="程序后台添加",
            //        Creator="admin",
            //        CreateTime  =DateTime.Now
            //    },

            //};
            return worktimes;

        }
        private static List<Pp_Reason> GetPp_Reasons()
        {
            var str = new List<Pp_Reason>()
            {



                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="L",
                        Reasoncntext="FCT",
                        Reasonentext="FCT",
                        Reasonjptext="FCT",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="L",
                        Reasoncntext="烧录",
                        Reasonentext="烧录",
                        Reasonjptext="烧录",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="L",
                        Reasoncntext="JTAG",
                        Reasonentext="JTAG",
                        Reasonjptext="JTAG",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },

                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="K",
                        Reasoncntext="检查完",
                        Reasonentext="检查完",
                        Reasonjptext="检查完",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="K",
                        Reasoncntext="检查中",
                        Reasonentext="检查中",
                        Reasonjptext="检查中",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="K",
                        Reasoncntext="测试完",
                        Reasonentext="测试完",
                        Reasonjptext="测试完",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="K",
                        Reasoncntext="测试中",
                        Reasonentext="测试中",
                        Reasonjptext="测试中",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="空焊",
                        Reasonentext="空焊",
                        Reasonjptext="空焊",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="连锡",
                        Reasonentext="连锡",
                        Reasonjptext="连锡",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="翘脚",
                        Reasonentext="翘脚",
                        Reasonjptext="翘脚",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="漏件",
                        Reasonentext="漏件",
                        Reasonjptext="漏件",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="位置偏移",
                        Reasonentext="位置偏移",
                        Reasonjptext="位置偏移",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="IC PIN 浮高",
                        Reasonentext="IC PIN 浮高",
                        Reasonjptext="IC PIN 浮高",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="IC PIN 竖立",
                        Reasonentext="IC PIN 竖立",
                        Reasonjptext="IC PIN 竖立",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="锡少",
                        Reasonentext="锡少",
                        Reasonjptext="锡少",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="翻面",
                        Reasonentext="翻面",
                        Reasonjptext="翻面",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="反面",
                        Reasonentext="反面",
                        Reasonjptext="反面",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="反向",
                        Reasonentext="反向",
                        Reasonjptext="反向",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="立碑",
                        Reasonentext="立碑",
                        Reasonjptext="立碑",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="发黄",
                        Reasonentext="发黄",
                        Reasonjptext="发黄",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="极性相违",
                        Reasonentext="极性相违",
                        Reasonjptext="极性相违",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="部品破损",
                        Reasonentext="部品破损",
                        Reasonjptext="部品破损",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="焊接不良",
                        Reasonentext="焊接不良",
                        Reasonjptext="焊接不良",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="红胶不良",
                        Reasonentext="红胶不良",
                        Reasonjptext="红胶不良",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="部品不良",
                        Reasonentext="部品不良",
                        Reasonjptext="部品不良",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="异物付着",
                        Reasonentext="异物付着",
                        Reasonjptext="异物付着",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="锡量过多",
                        Reasonentext="锡量过多",
                        Reasonjptext="锡量过多",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="错料",
                        Reasonentext="错料",
                        Reasonjptext="错料",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="基板不良",
                        Reasonentext="基板不良",
                        Reasonjptext="基板不良",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="底下有部品",
                        Reasonentext="底下有部品",
                        Reasonjptext="底下有部品",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="PCB不良",
                        Reasonentext="PCB不良",
                        Reasonjptext="PCB不良",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="多件",
                        Reasonentext="多件",
                        Reasonjptext="多件",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="生锡",
                        Reasonentext="生锡",
                        Reasonjptext="生锡",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="侧立",
                        Reasonentext="侧立",
                        Reasonjptext="侧立",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="H",
                        Reasoncntext="撞件",
                        Reasonentext="撞件",
                        Reasonjptext="撞件",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },

                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="正常",
                        Reasonentext="normal",
                        Reasonjptext="正常",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="学习中,新人员学习,开会",
                        Reasonentext="learning,meeting",
                        Reasonjptext="「新人」勉強する,週会",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="请假,旷工",
                        Reasonentext="ask for leave,absent without leave",
                        Reasonjptext="休暇する,欠勤する",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="切换机种,仕向",
                        Reasonentext="change model",
                        Reasonjptext="切替機種,仕向",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="ST差异大",
                        Reasonentext="labor time variance",
                        Reasonjptext="ST差異",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="组立慢,加工多,工程多,下机慢,作业困难,升级慢",
                        Reasonentext="assembly complicated",
                        Reasonjptext="組立を複雑する",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="坏机多,不良多",
                        Reasonentext="low accredited rate",
                        Reasonjptext="不具合",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="仪器设备,设置,调试,检查,故障,切换",
                        Reasonentext="instrument maintenance",
                        Reasonjptext="設備メンテナンス",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="部品不良,欠料",
                        Reasonentext="missing materials",
                        Reasonjptext="不良品,欠料",
                        Remark="程序后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="清机中,返工,改修",
                        Reasonentext="over again,revise",
                        Reasonjptext="手直し,やり直す",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="人员欠缺",
                        Reasonentext="understaffing",
                        Reasonjptext="人員不足",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="人员借调",
                        Reasonentext="temporarily transfer",
                        Reasonjptext="人員出向",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="测试慢,测试修理机",
                        Reasonentext="slow test speed",
                        Reasonjptext="テストが遅い",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="P",
                        Reasoncntext="其他",
                        Reasonentext="other",
                        Reasonjptext="その他",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="S",
                        Reasoncntext="开周会",
                        Reasonentext="开周会",
                        Reasonjptext="开周会",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {

                        GUID= Guid.NewGuid(),
                        Reasontype="S",
                        Reasoncntext="清洁",
                        Reasonentext="清洁",
                        Reasonjptext="清洁",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="欠料",
                        Reasonentext="欠料",
                        Reasonjptext="欠料",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                        Reasontype="S",
                        Reasoncntext="停电",
                        Reasonentext="停电",
                        Reasonjptext="停电",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="仪设",
                        Reasonentext="仪设",
                        Reasonjptext="仪设",
                        Remark="程序后台添加",
                    isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="切换机种",
                        Reasonentext="切换机种",
                        Reasonjptext="切换机种",
                        Remark="程序后台添加",
                    isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="切换停止时间",
                        Reasonentext="切换停止时间",
                        Reasonjptext="切换停止时间",
                        Remark="程序后台添加",
                    isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="学习",
                        Reasonentext="Learning",
                        Reasonjptext="トレニンーグする",
                        Remark="程序后台添加",
                    isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="开班会",
                        Reasonentext="开班会",
                        Reasonjptext="开班会",
                        Remark="程序后台添加",
                    isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="学习",
                        Reasonentext="学习",
                        Reasonjptext="学习",
                        Remark="程序后台添加",
                    isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="其他",
                        Reasonentext="Other",
                        Reasonjptext="その他",
                        Remark="程序后台添加",
                    isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },

                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="S",
                        Reasoncntext="组立",
                        Reasonentext="组立",
                        Reasonjptext="组立",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="组立",
                        Reasonentext="组立",
                        Reasonjptext="组立",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="SMT",
                        Reasonentext="SMT",
                        Reasonjptext="SMT",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="手插",
                        Reasonentext="手插",
                        Reasonjptext="手插",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="修正",
                        Reasonentext="修正",
                        Reasonjptext="修正",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="自插",
                        Reasonentext="自插",
                        Reasonjptext="自插",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="部品",
                        Reasonentext="部品",
                        Reasonjptext="部品",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="设计",
                        Reasonentext="设计",
                        Reasonjptext="设计",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="加工",
                        Reasonentext="加工",
                        Reasonjptext="加工",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_Reason()
                    {
                        GUID= Guid.NewGuid(),
                         Reasontype="D",
                        Reasoncntext="其他",
                        Reasonentext="其他",
                        Reasonjptext="其他",
                        Remark="程序后台添加",
                        isDelete =0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },


            };
            return str;
        }
        private static List<Pp_EcCategory> GetPp_EcCategorys()
        {
            var str = new List<Pp_EcCategory>()
            {
                new Pp_EcCategory()
                    {
                        GUID=Guid.NewGuid(),
                        Difftype=1,
                        Diffcnname="全仕向",
                        Udf001="",
                        Udf002="",
                        Udf003="",
                        Udf004=0,
                        Udf005=0,
                        Udf006=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_EcCategory()
                    {
                        GUID=Guid.NewGuid(),
                        Difftype=2,
                        Diffcnname="部管",
                        Udf001="",
                        Udf002="",
                        Udf003="",
                        Udf004=0,
                        Udf005=0,
                        Udf006=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_EcCategory()
                    {
                        GUID=Guid.NewGuid(),
                        Difftype=3,
                        Diffcnname="内部",
                        Udf001="",
                        Udf002="",
                        Udf003="",
                        Udf004=0,
                        Udf005=0,
                        Udf006=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Pp_EcCategory()
                    {
                        GUID=Guid.NewGuid(),
                        Difftype=4,
                        Diffcnname="技术",
                        Udf001="",
                        Udf002="",
                        Udf003="",
                        Udf004=0,
                        Udf005=0,
                        Udf006=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },

            };
            return str;
        }
        private static List<Qm_CheckType> GetQm_CheckTypes()
        {
            var strname = new List<Qm_CheckType>()
            {
                new Qm_CheckType()
                    {

                        GUID=Guid.NewGuid(),
                        Checktype="A",
                        Checkcntext="免检",
                        Checkentext="Exemption",
                        Checkjptext="PCBA半制品",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                        GUID=Guid.NewGuid(),
                         Checktype="A",
                        Checkcntext="抽检",
                        Checkentext="Randomly",
                        Checkjptext="制品",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                        GUID=Guid.NewGuid(),
                         Checktype="A",
                        Checkcntext="全检",
                        Checkentext="Full",
                        Checkjptext="特殊制品",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                         GUID=Guid.NewGuid(),
                         Checktype="B",
                        Checkcntext="允收",
                        Checkjptext="允收",
                        Checkentext="Accept",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                         GUID=Guid.NewGuid(),
                         Checktype="B",
                        Checkcntext="拒收",
                        Checkjptext="拒收",
                        Checkentext="Reject",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                         GUID=Guid.NewGuid(),
                         Checktype="B",
                        Checkcntext="特采",
                        Checkjptext="特采",
                        Checkentext="AOD",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                        GUID=Guid.NewGuid(),
                        Checktype="C",

                        Checkcntext="非常严重",
                        Checkjptext="特采",
                        Checkentext="AOD",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                         GUID=Guid.NewGuid(),
                         Checktype="C",

                        Checkcntext="严重",
                        Checkjptext="特采",
                        Checkentext="AOD",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                         GUID=Guid.NewGuid(),
                         Checktype="C",

                        Checkcntext="中等严重",
                        Checkjptext="特采",
                        Checkentext="AOD",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                         GUID=Guid.NewGuid(),
                         Checktype="C",

                        Checkcntext="不严重",
                        Checkjptext="特采",
                        Checkentext="AOD",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_CheckType()
                    {
                         GUID=Guid.NewGuid(),
                         Checktype="C",

                        Checkcntext="合格",
                        Checkjptext="特采",
                        Checkentext="AOD",
                        Remark="后台添加",
                        isDelete=0,
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
            };
            return strname;

        }
        private static List<Qm_DocNumber> GetQm_DocNumbers()
        {
            var str = new List<Qm_DocNumber>()
            {
                new Qm_DocNumber()
                    {
                        GUID=Guid.NewGuid(),
                        Doctype="A",
                        Docname="技术联络书编号",
                        Docnumber="DTS-",
                        isDelete=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_DocNumber()
                    {
                       GUID=Guid.NewGuid(),
                        Doctype="A",
                        Docname="技术联络书编号",
                        Docnumber="X-",
                        isDelete=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_DocNumber()
                    {
                       GUID=Guid.NewGuid(),
                        Doctype="A",
                        Docname="技术联络书编号",
                        Docnumber="TCJ-",
                        isDelete=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_DocNumber()
                    {
                        GUID=Guid.NewGuid(),
                        Doctype="B",
                        Docname="P番编号",
                        Docnumber="P-",
                         isDelete=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_DocNumber()
                    {
                        GUID=Guid.NewGuid(),
                        Doctype="C",
                        Docname="批次不合格通知书",
                        Docnumber="FormNo:DTA-04-Q010~B",
                         isDelete=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_DocNumber()
                    {
                        GUID=Guid.NewGuid(),
                        Doctype="C",
                        Docname="生产日报",
                        Docnumber="FormNo:DTA-04-O001~A",
                         isDelete=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },
                new Qm_DocNumber()
                    {
                        GUID=Guid.NewGuid(),
                        Doctype="C",
                        Docname="不良集计",
                        Docnumber="FormNo:DTA-04-Z038~A",

                         isDelete=0,
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        Creator="Admin"
                    },

            };
            return str;
        }
        private static List<Em_Event_Type> GetoneEm_Event_Types()
        {
            var str = new List<Em_Event_Type>()
            {
                new Em_Event_Type()
                    {
                        GUID=Guid.NewGuid(),
                        atcntype="A",
                        atcntypename="公司事务",
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        CreateUser="Admin"
                    },
                new Em_Event_Type()
                    {
                        GUID=Guid.NewGuid(),
                        atcntype="B",
                        atcntypename="私人事务",
                        Remark="批量导入",
                        CreateTime  =DateTime.Now,
                        CreateUser="Admin"
                    },

            };
            return str;


        }
    }
}