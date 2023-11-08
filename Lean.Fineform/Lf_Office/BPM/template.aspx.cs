using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lean.Fineform.Lf_Office.BPM
{
    public partial class template : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreBpmTemplateView";
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}