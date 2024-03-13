using System;

namespace LeanFine.Lf_Manufacturing.SD
{
    public partial class Sd_chart : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreSDChart";
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
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);
            //大于15号

            DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);

            if (DateTime.Now >= d1)
            {
                DPend.SelectedDate = DateTime.Now;//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            }
            else
            {
                DPend.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                getdate();
                //BindGrid();
                //PageContext.RegisterStartupScript("<script>updateChartInTabStrip();</script>");

                //如果没有就如下代码
                // PageContext.RegisterStartupScript("<script language='javascript'>updateChartInTabStrip();</script>");
            }
        }

        private string getdate()
        {
            string strDate = DPend.SelectedDate.Value.ToString("yyyyMM");
            return strDate;
        }
    }
}