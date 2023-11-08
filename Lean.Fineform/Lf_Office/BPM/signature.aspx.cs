using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lean.Fineform.Lf_Office.BPM
{
    public partial class signature : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreBpmSignatureView";
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 限制范围的方法一
                //TimePicker1.MinTime = new DateTime(2000, 1, 1, 8, 30, 0);
                //TimePicker1.MaxTime = new DateTime(2000, 1, 1, 20, 30, 0);
                //TimePicker1.SelectedDate = new DateTime(2000, 1, 1, 10, 30, 0);

                // 限制范围的方法二
                TimeStart.MinTimeText = "8:30";
                TimeStart.MaxTimeText = "20:30";
                //TimePicker1.Text = "10:30";

                TimeEnd.MinTimeText = "9:00";
                TimeEnd.MaxTimeText = "22:00";


                dpkStart.MinDate = DateTime.Now;
                dpkEnd.MinDate = DateTime.Now;
            }
        }

    }
}