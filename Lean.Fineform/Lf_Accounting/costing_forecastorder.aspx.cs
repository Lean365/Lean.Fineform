using System;

namespace LeanFine.Lf_Accounting
{
    public partial class costing_forecastorder : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFICOChart";
            }
        }

        #endregion ViewPower

        #region Page_Init

        // 注意：动态创建的代码需要放置于Page_Init（不是Page_Load），这样每次构造页面时都会执行

        #endregion Page_Init

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
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
        }

        #endregion Page_Load
    }
}