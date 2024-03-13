using System;

namespace LeanFine.Lf_Manufacturing.PP.daily
{
    public partial class p1d_output_opt : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DOutputView";
            }
        }

        #endregion ViewPower

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}