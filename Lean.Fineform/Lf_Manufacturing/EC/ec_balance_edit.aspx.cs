using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;


namespace Fine.Lf_Manufacturing.EC
{
    public partial class ec_balance_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcPMEdit";
            }
        }

        #endregion

        #region Page_Load
        public static string strecn, stritem,  strmodel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadData();
            }
        }

        private void LoadData()
        {
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            //BindDroplist();


            //获取通过窗体传递的值
            string strtransmit = GetQueryValue("Ec_no");
            //转字符串组
            string[] strgroup = strtransmit.Split(',');

            strecn = strgroup[0].ToString().Trim();
            strmodel = strgroup[1].ToString().Trim();
            stritem = strgroup[2].ToString().Trim();




            BindData();


        }
        #endregion

        #region Events

        private void BindData()
        {
            var q = (from item in DB.Pp_EcBalances
                    join a in DB.Pp_SapMaterials on item.Ec_olditem equals a.D_SAP_ZCA1D_Z002
                    where item.Ec_no.Contains(strecn)
                    where item.Ec_olditem.Contains(stritem)
                    where item.Ec_model.Contains(strmodel)
                    select new
                    {
  
                        item.Ec_olditem,
                        item.Ec_oldqty,
                        item.Ec_poqty,
                        item.Ec_balanceqty,
                        item.Ec_newitem,
                        item.Ec_precess,
                        item.Ec_note,
                        item.Ec_no,
                        item.Ec_issuedate,
                        item.Ec_status,
                        item.Ec_model,
                        item.Ec_item,
                        item.isEnd,
                        a.D_SAP_ZCA1D_Z033,

                    }).ToList();
            if(q.Any())
            {
                Ec_balancedate.SelectedDate = DateTime.Now;
                Ec_no.Text = q[0].Ec_no;
                Ec_oldqty.Text = q[0].Ec_oldqty.ToString();
                Ec_poqty.Text = q[0].Ec_poqty.ToString();
                Ec_balanceqty.Text = q[0].Ec_balanceqty.ToString();
                NowQty.Text = q[0].Ec_no;
                Ec_precess.Text = q[0].Ec_precess;
                Ec_note.Text = q[0].Ec_note;
                Ec_issuedate.Text = q[0].Ec_issuedate;
                Ec_model.Text = q[0].Ec_model;
                Ec_item.Text = q[0].Ec_item;
                Ec_olditem.Text = q[0].Ec_olditem;
                Ec_newitem.Text = q[0].Ec_newitem;
            }


        }


        //字段赋值，保存

        private void SaveItem()//修改设变平衡表
        {

          var q=  (from item in DB.Pp_EcBalances

             where item.Ec_no.Contains(strecn)
             where item.Ec_olditem.Contains(stritem)
             where item.Ec_model.Contains(strmodel)
             select new
             {
                 item.GUID,
                 item.Ec_olditem,
                 item.Ec_oldqty,
                 item.Ec_poqty,
                 item.Ec_balanceqty,
                 item.Ec_newitem,
                 item.Ec_newqty,
                 item.Ec_precess,
                 item.Ec_note,
                 item.Ec_no,
                 item.Ec_issuedate,
                 item.Ec_status,
                 item.Ec_model,
                 item.Ec_item,
                 item.isEnd,
                 item.Creator,
                 item.CreateTime,
                 item.isDelete,
             }).ToList();
            List<Pp_EcBalance> UpdateList = (from item in q
                                              select new Pp_EcBalance
                                              {
                                                  GUID = item.GUID,
                                                  Ec_balancedate= Ec_balancedate.SelectedDate.Value.ToString("yyyyMMdd"),
                                                  Ec_olditem = item.Ec_olditem,
                                                  Ec_oldqty = item.Ec_oldqty,
                                                  Ec_poqty = Decimal.Parse(Ec_poqty.Text),
                                                  Ec_balanceqty = Decimal.Parse(Ec_balanceqty.Text),
                                                  Ec_newitem = item.Ec_newitem,
                                                  Ec_newqty = item.Ec_newqty,
                                                  Ec_precess = Ec_precess.Text,
                                                  Ec_note = Ec_note.Text,
                                                  Ec_no = item.Ec_no,
                                                  Ec_issuedate = item.Ec_issuedate,
                                                  Ec_status = item.Ec_status,
                                                  Ec_model = item.Ec_model,
                                                  Ec_item = item.Ec_item,
                                                  isEnd = item.isEnd,
                                                  Creator = item.Creator,
                                                  CreateTime = item.CreateTime,
                                                  isDelete = item.isDelete,
                                                  Modifier = GetIdentityName(),
                                                  ModifyTime = DateTime.Now,
                                              }).ToList();

            DB.BulkUpdate(UpdateList);
            DB.BulkSaveChanges();





        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatorTools.IsNumber(Ec_balanceqty.Text) || !ValidatorTools.IsNumber(Ec_oldqty.Text) || !ValidatorTools.IsNumber(Ec_poqty.Text))
                {
                    if (decimal.Parse(Ec_balanceqty.Text) > decimal.Parse(Ec_oldqty.Text) + decimal.Parse(Ec_poqty.Text))
                    {
                        Alert.ShowInTop("结余 " + Ec_balanceqty.Text + " 不能大于发行在库+PO残！");
                        return;
                    }
                }
                SaveItem();

                InsNetOperateNotes();
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region NetOperateNotes
        private void InsNetOperateNotes()
        {


            //修改日志
            string Newtext = Ec_issuedate.Text + Ec_no.Text;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit生管* " + Newtext + " *Edit生管 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "旧品在库修改", OperateNotes);
        }
        #endregion


    }
}