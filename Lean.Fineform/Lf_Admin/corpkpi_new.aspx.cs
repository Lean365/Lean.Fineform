using FineUIPro;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Admin
{
    public partial class corpkpi_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreCorpkpiNew";
            }
        }

        #endregion ViewPower

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BindDDLCorp();
            BindDDLYear();

            this.ddlCorpAnnual.SelectedValue = DateTime.Now.AddYears(1).ToString("yyyy");
        }

        #region BindDDL

        private void BindDDLCorp()
        {
            IQueryable<Adm_Institution> q = DB.Adm_Institutions;
            //q = q.Where(u => u.EnName.ToString().Contains("25"));

            // 绑定到下拉列表（启用模拟树功能）

            ddlCorpAbbrName.DataTextField = "EnName";
            ddlCorpAbbrName.DataValueField = "EnName";
            ddlCorpAbbrName.DataSource = q;
            ddlCorpAbbrName.DataBind();

            this.ddlCorpAbbrName.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void BindDDLYear()
        {
            int selectYearAreaStart = int.Parse(DateTime.Now.AddYears(0).ToString("yyyy"));
            int selectYearAreaEnd = int.Parse(DateTime.Now.AddYears(10).ToString("yyyy"));

            var q = from a in DB.Adm_TheDates
                    where a.TheYear.CompareTo(selectYearAreaStart) >= 0
                    where a.TheYear.CompareTo(selectYearAreaEnd) <= 0
                    orderby a.TheYear
                    select new
                    {
                        a.TheYear
                    };

            // q.Distinct().OrderBy(i => i.TheYear);

            var qs = q.Select(E => new { E.TheYear }).ToList().Distinct();
            // 绑定到下拉列表（启用模拟树功能）

            ddlCorpAnnual.DataTextField = "TheYear";
            ddlCorpAnnual.DataValueField = "TheYear";
            ddlCorpAnnual.DataSource = qs;
            ddlCorpAnnual.DataBind();
            this.ddlCorpAnnual.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion BindDDL

        #endregion Page_Load

        #region Events

        private void SaveItem()
        {
            Adm_Corpkpi item = new Adm_Corpkpi();
            item.GUID = Guid.NewGuid();
            item.CorpAbbrName = ddlCorpAbbrName.SelectedItem.Text.Trim();
            item.CorpAnnual = ddlCorpAnnual.SelectedItem.Text.Trim();
            item.CorpTarget_CN = tbxCorpTarget_CN.Text.Trim();
            item.CorpTarget_EN = tbxCorpTarget_EN.Text.Trim();
            item.CorpTarget_JA = tbxCorpTarget_JA.Text.Trim();

            item.isDeleted = 0;
            item.Remark = "";
            item.CreateDate = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Adm_Corpkpis.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = ddlCorpAbbrName.SelectedItem.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Contectext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "目标管理", "目标信息新增", OperateNotes);
        }

        private void CheckData()
        {
            if (this.ddlCorpAbbrName.SelectedIndex == -1 || this.ddlCorpAbbrName.SelectedIndex == 0)
            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Reselect);
                return;
            }
            if (this.ddlCorpAnnual.SelectedIndex == -1 || this.ddlCorpAnnual.SelectedIndex == 0)
            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Reselect);
                return;
            }

            ////判断修改内容
            //int id = GetQueryIntValue("id");
            //proLine current = DB.proLines.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string checkdata1 = current.linename;

            //if (this.linename.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //    alert.IconFont = IconFont.Warning;
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}
            //判断重复
            string InputData = ddlCorpAbbrName.Text.Trim() + ddlCorpAnnual.Text.Trim();

            Adm_Corpkpi redata = DB.Adm_Corpkpis.Where(u => u.CorpAbbrName + u.CorpAnnual == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("目标信息,目标< " + InputData + ">已经存在！修改即可");
                return;
            }
            else
            {
                SaveItem();

                //Alert.ShowInTop("添加成功！", String.Empty, ActiveWindow.GetHidePostBackReference());
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            CheckData();
        }

        #endregion Events
    }
}