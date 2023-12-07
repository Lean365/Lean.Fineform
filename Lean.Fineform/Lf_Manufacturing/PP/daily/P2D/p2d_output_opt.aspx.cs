using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Lean.Fineform.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_output_opt : PageBase
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

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}