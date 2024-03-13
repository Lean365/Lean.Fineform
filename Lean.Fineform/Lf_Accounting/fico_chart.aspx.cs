using System;

namespace LeanFine.Lf_Accounting
{
    public partial class Fico_chart : PageBase
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

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}