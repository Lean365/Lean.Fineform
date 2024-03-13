using System;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_sap : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //GetDeleteScript  这个函数看官网示例中没有，我也是找了好久在网上搜到的也一并写出吧。

        private void LoadData()
        {
        }

        #endregion Page_Load
    }
}