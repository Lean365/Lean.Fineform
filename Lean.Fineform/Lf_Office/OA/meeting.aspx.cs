using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lean.Fineform.Lf_Office.OA
{
    public partial class meeting : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMeetingView";
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}