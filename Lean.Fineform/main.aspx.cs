using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine
{
    public partial class main : PageBase
    {
        #region Page_Init

        protected void Page_Init(object sender, EventArgs e)
        {
            // 工具栏上的帮助菜单
            JArray ja = JArray.Parse(ConfigHelper.HelpList);
            foreach (JObject jo in ja)
            {
                MenuButton menuItem = new MenuButton();
                menuItem.EnablePostBack = false;
                menuItem.Text = jo.Value<string>("Text");
                switch (menuItem.Text)
                {
                    case "日程":
                        menuItem.Text = global::Resources.GlobalResource.sys_Help_Calendar;
                        break;

                    case "科学计算器":
                        menuItem.Text = global::Resources.GlobalResource.sys_Help_Calculator;
                        break;

                    case "系统帮助":
                        menuItem.Text = global::Resources.GlobalResource.sys_Help_Manual;
                        break;

                    case "随机数":
                        menuItem.Text = "随机数";
                        break;

                    case "加解密":
                        menuItem.Text = "加解密";
                        break;
                }
                menuItem.Icon = IconHelper.String2Icon(jo.Value<string>("Icon"), true);
                menuItem.OnClientClick = String.Format("addExampleTab('{0}','{1}','{2}')", jo.Value<string>("ID"), ResolveUrl(jo.Value<string>("URL")), jo.Value<string>("Text"));

                btnHelp.Menu.Items.Add(menuItem);
            }

            // 用户可见的菜单列表
            List<Adm_Menu> menus = ResolveUserMenuList();
            if (menus == null)
            {
                Response.Write(global::Resources.GlobalResource.sys_Msg_PowerAuth_Error);
                Response.End();

                return;
            }

            if (menus.Count == 0)
            {
                Response.Write(global::Resources.GlobalResource.sys_Msg_PowerAuth_Error);
                Response.End();

                return;
            }

            //// 注册客户端脚本，服务器端控件ID和客户端ID的映射关系
            //JObject ids = GetClientIDS(regionPanel, topPanel, mainTabStrip);
            //ids.Add("userName", GetIdentityName());
            //ids.Add("userIP", Request.UserHostAddress);
            //ids.Add("onlineUserCount", GetOnlineCount());

            if (ConfigHelper.MenuType == "accordion")
            {
                Accordion accordionMenu = InitAccordionMenu(menus);
                //ids.Add("treeMenu", accordionMenu.ClientID);
                //ids.Add("menuType", "accordion");
            }
            else
            {
                Tree treeMenu = InitTreeMenu(menus);
                //ids.Add("treeMenu", treeMenu.ClientID);
                //ids.Add("menuType", "menu");
            }

            //string idsScriptStr = String.Format("window.DATA={0};", ids.ToString(Newtonsoft.Json.Formatting.None));
            //PageContext.RegisterStartupScript(idsScriptStr);
        }

        //private JObject GetClientIDS(params ControlBase[] ctrls)
        //{
        //    JObject jo = new JObject();
        //    foreach (ControlBase ctrl in ctrls)
        //    {
        //        jo.Add(ctrl.ID, ctrl.ClientID);
        //    }

        //    return jo;
        //}

        #region InitAccordionMenu

        /// <summary>
        /// 创建手风琴菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Accordion InitAccordionMenu(List<Adm_Menu> menus)
        {
            Accordion accordionMenu = new Accordion();
            accordionMenu.ID = "accordionMenu";
            accordionMenu.EnableFill = true;
            accordionMenu.ShowBorder = false;
            accordionMenu.ShowHeader = false;
            leftPanel.Items.Add(accordionMenu);

            foreach (var menu in menus.Where(m => m.Parent == null))
            {
                AccordionPane accordionPane = new AccordionPane();
                accordionPane.Title = menu.Name;
                accordionPane.Layout = LayoutType.Fit;
                accordionPane.ShowBorder = false;
                accordionPane.BodyPadding = "2px 0 0 0";

                Tree innerTree = new Tree();
                innerTree.ShowBorder = false;
                innerTree.ShowHeader = false;
                innerTree.EnableIcons = true;
                innerTree.AutoScroll = true;

                // 生成树
                int nodeCount = ResolveMenuTree(menus, menu, innerTree.Nodes);
                if (nodeCount > 0)
                {
                    accordionPane.Items.Add(innerTree);
                    accordionMenu.Items.Add(accordionPane);
                }
            }

            return accordionMenu;
        }

        #endregion InitAccordionMenu

        #region InitTreeMenu

        /// <summary>
        /// 创建树菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Tree InitTreeMenu(List<Adm_Menu> menus)
        {
            Tree treeMenu = new Tree();
            treeMenu.ID = "treeMenu";
            //treeMenu.EnableArrows = true;
            treeMenu.ShowBorder = false;
            treeMenu.ShowHeader = false;
            treeMenu.EnableIcons = true;
            treeMenu.AutoScroll = true;
            leftPanel.Items.Add(treeMenu);

            // 生成树
            ResolveMenuTree(menus, null, treeMenu.Nodes);

            // 展开第一个树节点
            treeMenu.Nodes[0].Expanded = true;

            return treeMenu;
        }

        /// <summary>
        /// 生成菜单树
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentMenuId"></param>
        /// <param name="nodes"></param>
        private int ResolveMenuTree(List<Adm_Menu> menus, Adm_Menu parentMenu, FineUIPro.TreeNodeCollection nodes)
        {
            int count = 0;
            foreach (var menu in menus.Where(m => m.Parent == parentMenu))
            {
                FineUIPro.TreeNode node = new FineUIPro.TreeNode();
                nodes.Add(node);
                count++;

                switch (menu.Name)
                {
                    case "系统管理":
                        node.Text = global::Resources.GlobalResource.menu_Sys_MGT; break;
                    case "系统菜单":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Menu; break;
                    case "在线统计":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Online; break;
                    case "系统配置":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Config; break;
                    case "登录日志":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Log; break;
                    case "工作日志":
                        node.Text = global::Resources.GlobalResource.menu_Work_Log; break;
                    case "公司部门":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Dpets; break;
                    case "公司信息":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Company; break;
                    case "数据字典":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Dict; break;
                    case "公司目标":
                        node.Text = global::Resources.GlobalResource.menu_Adm_Target; break;
                    case "部门管理":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Dept; break;
                    case "部门用户":
                        node.Text = global::Resources.GlobalResource.menu_Sys_DeptUser; break;
                    case "职称管理":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Title; break;
                    case "职称用户":
                        node.Text = global::Resources.GlobalResource.menu_Sys_TitleUser; break;
                    case "用户角色权限":
                        node.Text = global::Resources.GlobalResource.menu_Sys_UserRolePower; break;
                    case "用户管理":
                        node.Text = global::Resources.GlobalResource.menu_Sys_User; break;
                    case "密码修改":
                        node.Text = global::Resources.GlobalResource.menu_Sys_ChangePassword; break;
                    case "角色管理":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Role; break;
                    case "角色用户":
                        node.Text = global::Resources.GlobalResource.menu_Sys_RoleUser; break;
                    case "权限管理":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Power; break;
                    case "角色权限":
                        node.Text = global::Resources.GlobalResource.menu_Sys_PowerRole; break;
                    case "批量更新":
                        node.Text = global::Resources.GlobalResource.menu_Sys_Update; break;

                    case "财务管理":
                        node.Text = global::Resources.GlobalResource.menu_Fico_MGT; break;

                    case "费用管理":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Expenses; break;

                    case "预算管理":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Budget; break;
                    case "财务期间":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Period; break;
                    case "会计科目":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Titles; break;
                    case "实际科目":
                        node.Text = global::Resources.GlobalResource.menu_Fico_RealTitles; break;
                    case "费用预算":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Expenses; break;
                    case "人工预算":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Labor; break;
                    case "加班预算":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Overtime; break;
                    case "资产预算":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Asset; break;
                    case "签拟单":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Countersignature; break;
                    case "预算审核":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Audit; break;

                    case "成本分析":
                        node.Text = global::Resources.GlobalResource.menu_Fico_Costing; break;
                    case "费用分析":
                        node.Text = global::Resources.GlobalResource.menu_Fico_ExpensesAnalysis; break;

                    case "物料管理":
                        node.Text = global::Resources.GlobalResource.menu_Mm_MGT; break;
                    case "物料":
                        node.Text = global::Resources.GlobalResource.menu_Mm_Master; break;
                    case "机种":
                        node.Text = global::Resources.GlobalResource.menu_Mm_ModelRegion; break;
                    case "查询":
                        node.Text = global::Resources.GlobalResource.menu_Mm_Query; break;
                    case "易飞数据":
                        node.Text = global::Resources.GlobalResource.menu_YF_Query; break;

                    case "生产管理": node.Text = global::Resources.GlobalResource.menu_Pp_MGT; break;

                    case "信息": node.Text = global::Resources.GlobalResource.menu_Pp_MasterData; break;
                    case "班组管理": node.Text = global::Resources.GlobalResource.menu_Pp_Line; break;
                    case "生产订单": node.Text = global::Resources.GlobalResource.menu_Pp_MO; break;
                    case "标准工时": node.Text = global::Resources.GlobalResource.menu_Pp_Manhours; break;
                    case "机种仕向": node.Text = global::Resources.GlobalResource.menu_Mm_ModelRegion; break;
                    case "原因类别": node.Text = global::Resources.GlobalResource.menu_Pp_ReasonType; break;
                    case "停线类别": node.Text = global::Resources.GlobalResource.menu_Pp_LineStopType; break;
                    case "不具合类别": node.Text = global::Resources.GlobalResource.menu_pp_defect_Type; break;
                    case "检验类别": node.Text = global::Resources.GlobalResource.menu_Qm_InspectionType; break;
                    case "验收类别": node.Text = global::Resources.GlobalResource.menu_Qm_AcceptanceType; break;
                    case "运输方式": node.Text = global::Resources.GlobalResource.menu_Sd_TransportationMethods; break;
                    case "稼动率": node.Text = global::Resources.GlobalResource.menu_Pp_efficiency; break;
                    case "技术联络": node.Text = global::Resources.GlobalResource.menu_Pp_TL_Liaison; break;
                    case "技联": node.Text = global::Resources.GlobalResource.menu_Pp_TL; break;
                    case "技联查询": node.Text = global::Resources.GlobalResource.menu_Pp_TL_List; break;
                    case "设变": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_MGT; break;
                    case "设变实施": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_Carryout; break;
                    case "投入批次": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_Putinto; break;
                    case "SOP": node.Text = global::Resources.GlobalResource.menu_Pp_Sop; break;
                    case "SOP确认": node.Text = global::Resources.GlobalResource.menu_Pp_Sop_Query; break;
                    case "组立": node.Text = global::Resources.GlobalResource.menu_Pp_Sop_ASY; break;
                    case "PCBA": node.Text = global::Resources.GlobalResource.menu_Pp_Sop_PCBA; break;

                    case "物料确认": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_Outbound; break;
                    case "设变报表": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_Report; break;
                    case "SAP设变": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_SAP_Data; break;
                    case "旧品管制": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_OldStock; break;
                    case "设变查询": node.Text = global::Resources.GlobalResource.menu_Pp_Ec_Query; break;

                    case "追溯": node.Text = global::Resources.GlobalResource.menu_Pp_Tracking; break;
                    case "批次追溯": node.Text = global::Resources.GlobalResource.menu_pp_tracking_lot; break;
                    case "标准工程": node.Text = global::Resources.GlobalResource.menu_Pp_Tracking_Process; break;

                    case "分析图表": node.Text = global::Resources.GlobalResource.menu_Rpt_pptackingcharts; break;

                    case "制一生产": node.Text = global::Resources.GlobalResource.menu_Pp_Manufacture_P1D; break;
                    case "制二生产": node.Text = global::Resources.GlobalResource.menu_Pp_Manufacture_P2D; break;
                    case "切换记录": node.Text = global::Resources.GlobalResource.menu_Pp_P2d_SwitchNote; break;
                    case "改修不良": node.Text = global::Resources.GlobalResource.menu_Pp_P1d_Repair_Defect; break;
                    case "Epp不良": node.Text = global::Resources.GlobalResource.menu_Pp_P1d_Epp_Defect; break;
                    case "改修生产": node.Text = global::Resources.GlobalResource.menu_Pp_P1d_Repair_Output; break;
                    case "生产日报": node.Text = global::Resources.GlobalResource.menu_Pp_OPH; break;
                    case "EPP生产": node.Text = global::Resources.GlobalResource.menu_Pp_OPHEpp; break;
                    case "实绩查询": node.Text = global::Resources.GlobalResource.menu_Pp_OPH_Actual; break;
                    case "OPH查询": node.Text = global::Resources.GlobalResource.menu_Pp_OPH_Query; break;
                    case "生产进度": node.Text = global::Resources.GlobalResource.menu_Pp_ProducingProgress; break;
                    case "OPH报表": node.Text = global::Resources.GlobalResource.menu_Pp_OPH_Report; break;
                    case "看板管理": node.Text = global::Resources.GlobalResource.menu_Pp_Kanban; break;
                    case "SMT点数": node.Text = global::Resources.GlobalResource.menu_Pp_Short; break;
                    case "SMT日报": node.Text = global::Resources.GlobalResource.menu_Pp_Smt; break;
                    case "制一不良": node.Text = global::Resources.GlobalResource.menu_Pp_P1d_Defect; break;
                    case "制二不良": node.Text = global::Resources.GlobalResource.menu_Pp_P2d_Defect; break;
                    case "工程检查": node.Text = global::Resources.GlobalResource.menu_Pp_Inspection; break;
                    case "生产不良": node.Text = global::Resources.GlobalResource.menu_Pp_Defect; break;
                    case "修理记录": node.Text = global::Resources.GlobalResource.menu_Pp_Repair; break;

                    case "工单统计": node.Text = global::Resources.GlobalResource.menu_pp_defect_MOStatistics; break;
                    case "LOT集计": node.Text = global::Resources.GlobalResource.menu_pp_defect_LotStatistics; break;
                    case "不具合查询": node.Text = global::Resources.GlobalResource.menu_pp_defect_Query; break;

                    case "工数": node.Text = global::Resources.GlobalResource.menu_Pp_Times; break;
                    case "制一": node.Text = global::Resources.GlobalResource.menu_Pp_Times_P1D; break;
                    case "制二": node.Text = global::Resources.GlobalResource.menu_Pp_Times_P2D; break;
                    case "图表": node.Text = global::Resources.GlobalResource.menu_Rpt; break;
                    case "生产图表": node.Text = global::Resources.GlobalResource.menu_Rpt_ppcharts; break;

                    case "质量管理": node.Text = global::Resources.GlobalResource.menu_Qm_MGT; break;
                    case "品质": node.Text = global::Resources.GlobalResource.menu_Qm_QC; break;
                    case "成品入库检验": node.Text = global::Resources.GlobalResource.menu_Qm_FQC; break;
                    case "不合格通知书": node.Text = global::Resources.GlobalResource.menu_Qm_FQC_UnqualifiedNotice; break;
                    case "分析对策报告": node.Text = global::Resources.GlobalResource.menu_Qm_FQC_AnalysisStrategy; break;
                    case "合格率报表": node.Text = global::Resources.GlobalResource.menu_Qm_FQC_PassReport; break;
                    case "检验查询": node.Text = global::Resources.GlobalResource.menu_Qm_FQC_Query; break;

                    case "成本":
                        node.Text = global::Resources.GlobalResource.menu_Qm_QC_Cost; break;
                    case "改修对应":
                        node.Text = global::Resources.GlobalResource.menu_Qm_QC_Rework; break;
                    case "品质业务":
                        node.Text = global::Resources.GlobalResource.menu_Qm_QC_Operation; break;
                    case "废弃事故":
                        node.Text = global::Resources.GlobalResource.menu_Qm_QC_Waste; break;
                    case "直接工资率":
                        node.Text = global::Resources.GlobalResource.menu_Qm_QC_Wages; break;
                    case "客诉":
                        node.Text = global::Resources.GlobalResource.menu_Qm_ComplaintMGT; break;
                    case "客诉信息":
                        node.Text = global::Resources.GlobalResource.menu_Qm_Complaint; break;

                    case "品质图表":
                        node.Text = global::Resources.GlobalResource.menu_Rpt_qmcharts; break;

                    case "销售管理":
                        node.Text = global::Resources.GlobalResource.menu_Sd_MGT; break;
                    case "销售数据":
                        node.Text = global::Resources.GlobalResource.menu_Sd_SlaesData; break;
                    case "订单管理": node.Text = global::Resources.GlobalResource.menu_Sd_SO; break;

                    case "计划SO": node.Text = global::Resources.GlobalResource.menu_Sd_PlannedOrder; break;

                    case "出货管理": node.Text = global::Resources.GlobalResource.menu_Sd_Shipment_MGT; break;
                    case "入库扫描": node.Text = global::Resources.GlobalResource.menu_Sd_InboundSerial; break;
                    case "出库扫描": node.Text = global::Resources.GlobalResource.menu_Sd_OutboundSerial; break;
                    case "入库查询": node.Text = global::Resources.GlobalResource.menu_Sd_InboundQuery; break;
                    case "出库查询": node.Text = global::Resources.GlobalResource.menu_Sd_OutboundQuery; break;

                    case "客户管理": node.Text = global::Resources.GlobalResource.menu_Sd_CustomerMGT; break;
                    case "客户信息": node.Text = global::Resources.GlobalResource.menu_Sd_Customer; break;

                    case "销售图表": node.Text = global::Resources.GlobalResource.menu_Rpt_sdcharts; break;

                    case "日常办公": node.Text = global::Resources.GlobalResource.menu_Oa_MGT; break;
                    case "通讯录": node.Text = global::Resources.GlobalResource.menu_Oa_MGT_Contacts; break;

                    case "报关课": node.Text = global::Resources.GlobalResource.co_Dept_CD; break;
                    case "财务课": node.Text = global::Resources.GlobalResource.co_Dept_GA; break;
                    case "总经室": node.Text = global::Resources.GlobalResource.co_Dept_GD; break;
                    case "受检课": node.Text = global::Resources.GlobalResource.co_Dept_IQC; break;
                    case "技术课": node.Text = global::Resources.GlobalResource.co_Dept_TE; break;
                    case "生管课": node.Text = global::Resources.GlobalResource.co_Dept_PM; break;
                    case "总务课": node.Text = global::Resources.GlobalResource.co_Dept_LD; break;
                    case "部管课": node.Text = global::Resources.GlobalResource.co_Dept_MM; break;

                    case "制一课": node.Text = global::Resources.GlobalResource.co_Dept_P1D; break;
                    case "制二课": node.Text = global::Resources.GlobalResource.co_Dept_P2D; break;
                    case "制技课": node.Text = global::Resources.GlobalResource.co_Dept_PE; break;
                    case "采购课": node.Text = global::Resources.GlobalResource.co_Dept_PD; break;
                    case "品管课": node.Text = global::Resources.GlobalResource.co_Dept_QA; break;
                    case "电脑课": node.Text = global::Resources.GlobalResource.co_Dept_IT; break;
                    case "OEM": node.Text = global::Resources.GlobalResource.co_Dept_OEM; break;

                    default:
                        break;
                }

                node.IconUrl = menu.ImageUrl;
                if (!String.IsNullOrEmpty(menu.NavigateUrl))
                {
                    node.EnableClickEvent = false;
                    node.NavigateUrl = ResolveUrl(menu.NavigateUrl);
                    //node.OnClientClick = String.Format("addTab('{0}','{1}','{2}')", node.NodeID, ResolveUrl(menu.NavigateUrl), node.Text.Replace("'", ""));
                }

                if (menu.IsTreeLeaf)
                {
                    node.Leaf = true;

                    // 如果是叶子节点，但是不是超链接，则是空目录，删除
                    if (String.IsNullOrEmpty(menu.NavigateUrl))
                    {
                        nodes.Remove(node);
                        count--;
                    }
                }
                else
                {
                    //node.SingleClickExpand = true;

                    int childCount = ResolveMenuTree(menus, menu, node.Nodes);

                    // 如果是目录，但是计算的子节点数为0，可能目录里面的都是空目录，则要删除此父目录
                    if (childCount == 0 && String.IsNullOrEmpty(menu.NavigateUrl))
                    {
                        nodes.Remove(node);
                        count--;
                    }
                }
            }

            return count;
        }

        #endregion InitTreeMenu

        #region ResolveUserMenuList

        // 获取用户可用的菜单列表
        private List<Adm_Menu> ResolveUserMenuList()
        {
            // 当前登陆用户的权限列表
            List<string> rolePowerNames = GetRolePowerNames();
            if (rolePowerNames.Count != 0)
            {
                // 当前用户所属角色可用的菜单列表
                List<Adm_Menu> menus = new List<Adm_Menu>();

                foreach (Adm_Menu menu in MenuHelper.Adm_Menus)
                {
                    // 如果此菜单不属于任何模块，或者此用户所属角色拥有对此模块的权限
                    if (menu.ViewPower == null || rolePowerNames.Contains(menu.ViewPower.Name))
                    {
                        menus.Add(menu);
                    }
                }

                return menus;
            }
            else
            {
                return null;
            }
        }

        #endregion ResolveUserMenuList

        #endregion Page_Init

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1.Enabled = true;
                //string strlang = Session["PreferredCulture"].ToString().ToLower().Trim() ;
                //Response.Redirect(Request.Url.PathAndQuery);
                LoadData();
            }
        }

        private void LoadData()
        {
            string uid = GetIdentityName();
            string uname = "";
            var q = (from a in DB.Adm_Users
                     where a.Name.CompareTo(uid) == 0
                     select a).ToList();
            if (q.Any())
            {
                uname = q[0].ChineseName;
            }
            string uip = NetHelper.GetIP4Address();

            this.txtUName.Text = "Welcome:" + uname + "[" + uip + "] Online:" + GetOnlineCount();

            this.txtDtime.Text = DateTime.Now.ToString();
            var qc = (from a in DB.Adm_Institutions
                          //join b in DB.ProSapEngChanges on a.Proecnno equals b.D_SAP_ZPABD_Z001
                      where a.ShortName == "DTA"
                      select new
                      {
                          a.FullName,
                          a.Slogan,
                      }).ToList();
            if (qc.Any())
            {
                this.txtCrght.Text = "CopyRight© 2015-" + DateTime.Now.ToString("yyyy") + qc[0].FullName;

                linkSlogantext.Text = global::Resources.GlobalResource.sys_Slogan;
            }

            System.Web.UI.WebControls.Label link = topPanel.FindControl("linkSystemTitle") as System.Web.UI.WebControls.Label;
            if (link != null)
            {
                //System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                link.Text = String.Format(" v{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());// ConfigHelper.Title;
            }

            //linkSystemTitle.Text = String.Format("OneCube v{0}", GetProductVersion());

            btnUserName.Text = uname;
        }

        #endregion Page_Load

        #region Events

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hOnline = (Hashtable)Application["Online"];
                if (hOnline != null)
                {
                    if ((hOnline[Session.SessionID] != null))
                    {
                        hOnline.Remove(Session.SessionID);
                        Application.Lock();
                        Application["Online"] = hOnline;
                        Application.UnLock();
                    }
                }
                FormsAuthentication.SignOut();
                //Session.Abandon();

                FormsAuthentication.RedirectToLoginPage();
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);
            }
        }

        #endregion Events

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //显示时间右下角
            //txtDtime.Text = DateTime.Now.ToString();
        }
    }
}