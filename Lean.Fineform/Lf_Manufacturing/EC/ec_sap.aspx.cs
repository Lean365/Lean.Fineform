﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;using System.Data.Entity.Validation;

using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.IO;

using System.Text;

namespace Fine.Lf_Manufacturing.EC
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

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //GetDeleteScript  这个函数看官网示例中没有，我也是找了好久在网上搜到的也一并写出吧。


        private void LoadData()
        {


        }


        #endregion



    }
}

