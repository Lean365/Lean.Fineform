using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.PP.poor
{
    public partial class p2d_defect_order_totalled_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DDefectEdit";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string mysql, userid, ConnStr;
        public static int rowID, delrowID, editrowID, totalSum;
        public static string Prolot, Prolinename, Prodate, Prorealqty, Probadnote, Proorder, Probadreason, Pronobadqty, Proorderqty, Promodel, Promodelqty, Probadqty, Probadtotal, Probadamount, Prongdept;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            userid = GetIdentityName();

            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            BindData();

            //UpdateQty();
            //DefDate.SelectedDate = DateTime.Now.AddDays(-1);
        }

        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Guid id = Guid.Parse(GetQueryValue("GUID"));

            var current = (from a in DB.Pp_Defect_Totals
                           where a.GUID == (id)
                           select a).ToList();

            if (!current.Any())
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            DefDate.Text = current[0].Prodate;
            // 选中当前节点的父节点
            prolinename.Text = current[0].Prolinename;

            prolot.Text = current[0].Prolot;

            promodel.Text = current[0].Promodel;

            proorderqty.Text = current[0].Proorderqty.ToString();

            proorder.Text = current[0].Proorder;

            var rqty = (from a in DB.Pp_P1d_OutputSubs
                        where a.Proorder == proorder.Text
                        group a by a.Proorder into g
                        select new
                        {
                            rqty = g.Sum(a => a.Prorealqty)
                        }).ToList();
            if (rqty.Any())
            {
                prorealqty.Text = rqty[0].rqty.ToString();
            }
            else
            {
                prorealqty.Text = current[0].Prorealqty.ToString();
            }

            if (current[0].Pronobadqty != 0)
            {
                pronobadqty.Text = current[0].Pronobadqty.ToString();
            }
            else
            {
                pronobadqty.Text = current[0].Prorealqty.ToString();
            }

            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);

            //修改前日志
            string BeforeModi = current[0].Prodate + "," + current[0].Prolinename + "," + current[0].Prolot + "," + current[0].Prorealqty + "," + current[0].Proorder + "," + current[0].Pronobadqty;
            string OperateType = "修改";
            string OperateNotes = "beEdit生产不良*" + BeforeModi + " *beEdit生产不良 的订单记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "系统管理", "不具合修改", OperateNotes);
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (Decimal.Parse(pronobadqty.Text) > Decimal.Parse(prorealqty.Text))
            {
                Alert.ShowInTop("无不良台数不能大于生产台数");
                return;
            }
            if (Decimal.Parse(pronobadqty.Text) <= Decimal.Parse(prorealqty.Text))
            {
                UpdateDefectQty();
                UpdateOqty();
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        private void UpdateOqty()
        {
            var q = (from a in DB.Pp_P1d_Defects
                     where a.Prolot == prolot.Text
                     select a).ToList();
            if (q.Any())
            {
                for (int i = 0; i < q.Count(); i++)
                {
                    int iid = q[i].ID;
                    Pp_P1d_Defect item = DB.Pp_P1d_Defects
                     .Where(u => u.ID == iid).FirstOrDefault();
                    item.Pronobadqty = int.Parse(pronobadqty.Text);//无不良台数更新
                    item.ModifyDate = DateTime.Now;
                    item.Modifier = GetIdentityName();
                    DB.SaveChanges();
                }
            }
        }

        public void UpdateDefectQty()
        {
            string dd = DefDate.Text.Substring(0, 6);
            var q =
                from p in DB.Pp_P1d_OutputSubs
                        .Where(s => s.Prodate.Substring(0, 6).CompareTo(dd) == 0)
                    //    .Where(s => s.Prodate.Contains(dd))
                    //    .Where(s => s.Prolinename.Contains(pl))
                    //.Where(s => s.Prolot.Contains(lt))
                orderby p.Prodate descending
                group p by new
                {
                    p.Prolot,
                    // p.Prodate,
                    //p.Prolinename,
                }

                    into g
                select new
                {
                    //Prodate = g.Key.Prodate,
                    // Prolinename = g.Key.Prolinename,
                    Prolot = g.Key.Prolot,
                    Prorealqty = g.Sum(p => p.Prorealqty),
                };

            //判断查询是否为空
            var qs = q.ToList();
            if (qs.Any())
            {
                for (int i = 0; i < qs.Count; i++)
                {
                    string lt = qs[i].Prolot;

                    var up = (from a in DB.Pp_P1d_Defects
                              where a.Prolot == lt
                              select a).ToList();
                    if (up.Any())
                    {
                        //for遍历
                        for (int f = 0; f < up.Count; f++)
                        {
                            //dd = qs[i].Prodate;
                            //pl = qs[i].Prolinename;

                            int iid = up[f].ID;
                            //批量更新
                            Pp_P1d_Defect current = DB.Pp_P1d_Defects
                                .Where(u => u.ID == iid).FirstOrDefault();
                            if (current != null)
                            {
                                current.Prorealqty = qs[0].Prorealqty;
                                //current.Prongmatter = qs[0].Prorealqty.ToString();
                                current.Modifier = GetIdentityName();
                                current.ModifyDate = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        #endregion Page_Load

        #region Events

        private void SaveItem()//新增生产日报单头
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_Defect_Total item = DB.Pp_Defect_Totals

                .Where(u => u.GUID == (id)).FirstOrDefault();
            //liclass();
            //ngclass();
            //ngcode();
            //item.ID = GetQueryIntValue("id");

            //item.Prongdept = Prongdept.SelectedItem.Text;

            //item.Proclassmatter = proclassmatter.SelectedItem.Text;
            //item.Prongclass = nclass;
            //item.Prongmatter = prongmatter.SelectedItem.Text;
            //item.Prongcode = ncode;

            //item.Probadqty = int.Parse(probadqty.Text);
            ////item.Probadtotal =0;
            ////item.Prongbdel = false;
            //item.Probadnote = Probadnote.Text;

            //// 添加所有用户

            //item.Remark = remark.Text;
            item.Prorealqty = int.Parse(prorealqty.Text);
            item.Pronobadqty = int.Parse(pronobadqty.Text);
            item.Prodirectrate = (decimal)int.Parse(pronobadqty.Text) / int.Parse(prorealqty.Text);

            item.Modifier = GetIdentityName();
            item.ModifyDate = DateTime.Now;

            //DB.Probads.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = prolot.Text.Trim() + "," + prolot.Text + "," + DefDate.Text + "," + prolinename.Text + "," + prorealqty.Text + "," + pronobadqty.Text + "," + promodel.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit生产不良* " + ModifiedText + "*afEdit生产不良 的订单记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "系统管理", "不具合修改", OperateNotes);
        }

        #endregion Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                SaveItem();
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
    }
}