using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lc_MM
{
    public partial class material_query : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMaterialView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        private void LoadData()
        {
            //Publisher.Text = GetIdentityName();
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

            BindData();
        }

        private void BindData()
        {
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                //IQueryable<Mm_Material> q = DB.Mm_Materials; //.Include(u => u.Dept);

                var q = from a in DB.Mm_Materials
                        select a;

                // 在用户名称中搜索

                q = q.Where(u => u.MatItem.Contains(searchText)); //|| u.CreateDate.Contains(searchText));

                var qs = q.ToList();
                if (qs.Any())
                {
                    isDate.Text = qs[0].isDate;
                    Plnt.Text = qs[0].Plnt;
                    //MatItem.Text = "<span style='color:red;font-weight:bold;'>"+ qs[0].MatItem +"</span>";
                    MatItem.Text = qs[0].MatItem;
                    Industry.Text = qs[0].Industry;
                    MatType.Text = qs[0].MatType;
                    MatDescription.Text = qs[0].MatDescription;
                    BaseUnit.Text = qs[0].BaseUnit;
                    ProHierarchy.Text = qs[0].ProHierarchy;
                    MatGroup.Text = qs[0].MatGroup;
                    PurGroup.Text = qs[0].PurGroup;
                    PurType.Text = qs[0].PurType;
                    SpecPurType.Text = qs[0].SpecPurType;
                    BulkMat.Text = qs[0].BulkMat;
                    Moq.Text = qs[0].Moq.ToString();
                    RoundingVal.Text = qs[0].RoundingVal.ToString();
                    LeadTime.Text = qs[0].LeadTime.ToString();
                    ProDays.Text = qs[0].ProDays.ToString();
                    IsCheck.Text = qs[0].IsCheck;
                    ProfitCenter.Text = qs[0].ProfitCenter;
                    DiffCode.Text = qs[0].DiffCode;
                    isLot.Text = qs[0].isLot;
                    MPN.Text = qs[0].MPN;
                    Mfrs.Text = qs[0].Mfrs;
                    EvaluationType.Text = qs[0].EvaluationType;
                    MovingAvg.Text = qs[0].MovingAvg.ToString();
                    Currency.Text = qs[0].Currency;
                    PriceControl.Text = qs[0].PriceControl;
                    PriceUnit.Text = qs[0].PriceUnit.ToString();
                    SLoc.Text = qs[0].SLoc;
                    ESLoc.Text = qs[0].ESLoc;
                    LocPosition.Text = qs[0].LocPosition;
                    Inventory.Text = qs[0].Inventory.ToString();
                    LocEol.Text = qs[0].LocEol;
                }
            }
        }

        #endregion Page_Load

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindData();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindData();
        }

        #endregion Events
    }
}