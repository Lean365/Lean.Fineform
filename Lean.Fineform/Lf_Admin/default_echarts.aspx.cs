using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
namespace Fine.Lf_Admin
{
    public partial class default_echarts : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreKitOutput";
            }
        }

        #endregion

        public static string DivText;
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

           if(DateTime.Now>=d1)
            {
                DPend.SelectedDate = DateTime.Now;//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            }
           else
            {
                DPend.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            }

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            BindData();
        }
        private void BindData()
        {
            string selectYearAreaStart = DPend.SelectedDate.Value.ToString("yyyyMM");
            string fy;
            var fm = (from a in DB.Fico_Periods
                     where a.Btfm.Contains(selectYearAreaStart)
                     select a).ToList();

            if (fm.Any())
            {
                fy = fm[0].Btfy.Substring(2,4).ToString();




                var langstr = Session["PreferredCulture"];
                if (Session["PreferredCulture"] != null)//先判断百session是否存在 （sessionname表示你度session的名称）
                {
                    string LangStr = Session["PreferredCulture"] as string;
                    //object o = Session["PreferredCulture"];//获取session，session里面存放的是object，使回用的时候要转成自己的存放的session的数据类答型。
                    if (LangStr.ToLower().Contains("zh"))
                    {
                        var q = (from a in DB.Adm_Corpkpis
                                 where a.CorpAbbrName.CompareTo("DTA") == 0
                                 where a.CorpAnnual.CompareTo(fy) == 0
                                 select a).ToList();
                        if (q.Any())
                        {
                            this.TargetText.InnerHtml = q[0].CorpTarget_CN.Replace("\r\n", "<br/>");
                            
                            //DivText = q[0].CorpTarget_CN.Replace("\r\n ", " <br/> ");
                        }
                        var qf = (from a in DB.Adm_Formulas
                                  where a.FormulaType.CompareTo("P") == 0
                                  //where a.CorpAnnual.CompareTo(selectYearAreaStart) == 0
                                  select a).ToList();
                        if (qf.Any())
                        {
                            this.FormulaText.InnerHtml = qf[0].Formula_CN.Replace("\r\n", "<br/>"); 

                            //DivText = q[0].CorpTarget_CN.Replace("\r\n ", " <br/> ");
                        }

                    }
                    if (LangStr.ToLower().Contains("ja"))
                    {
                        var q = (from a in DB.Adm_Corpkpis
                                 where a.CorpAbbrName.CompareTo("DTA") == 0
                                 where a.CorpAnnual.CompareTo(fy) == 0
                                 select a).ToList();
                        if (q.Any())
                        {
                            this.TargetText.InnerHtml = q[0].CorpTarget_JA.Replace("\r\n", "<br/>"); 

                            //DivText = q[0].CorpTarget_CN.Replace("\r\n ", " <br/> ");
                        }
                        var qf = (from a in DB.Adm_Formulas
                                  where a.FormulaType.CompareTo("P") == 0
                                  //where a.CorpAnnual.CompareTo(selectYearAreaStart) == 0
                                  select a).ToList();
                        if (qf.Any())
                        {
                            this.FormulaText.InnerHtml = qf[0].Formula_JA.Replace("\r\n", "<br/>"); 

                            //DivText = q[0].CorpTarget_CN.Replace("\r\n ", " <br/> ");
                        }
                    }
                    if (LangStr.ToLower().Contains("en"))
                    {
                        var q = (from a in DB.Adm_Corpkpis
                                 where a.CorpAbbrName.CompareTo("DTA") == 0
                                 where a.CorpAnnual.CompareTo(fy) == 0
                                 select a).ToList();
                        if (q.Any())
                        {
                            this.TargetText.InnerHtml = q[0].CorpTarget_EN.Replace("\r\n", "<br/>"); 

                            //DivText = q[0].CorpTarget_CN.Replace("\r\n ", " <br/> ");
                        }
                        var qf = (from a in DB.Adm_Formulas
                                  where a.FormulaType.CompareTo("P") == 0
                                  //where a.CorpAnnual.CompareTo(selectYearAreaStart) == 0
                                  select a).ToList();
                        if (qf.Any())
                        {
                            this.FormulaText.InnerHtml = qf[0].Formula_EN.Replace("\r\n", "<br/>");

                            //DivText = q[0].CorpTarget_CN.Replace("\r\n ", " <br/> ");
                        }
                    }
                }
            }





        }
        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {

                BindData();
                //PageContext.RegisterStartupScript("<script>updateChartInTabStrip();</script>");

                //如果没有就如下代码
                // PageContext.RegisterStartupScript("<script language='javascript'>updateChartInTabStrip();</script>");
            }
        }


    }

}