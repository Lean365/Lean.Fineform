using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.PP.poor
{
    public partial class p1d_defect_order_totalled_new : PageBase
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
        //public static string Prolot, Linename, Prodate, Prorealqty, Prodefectsymptom, Proorder, Prodefectcause, Prodzeroefects, Proorderqty, Promodel, Promodelqty, Probadqty, Probadtotal, Probadamount, Prodefectcategory;

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

            //BindData();

            //UpdateQty();
            //prodate.SelectedDate = DateTime.Now.AddDays(-1);
        }

        //查询LOT
        private void BindDdlprolot()
        {
            //查询LINQ去重复

            if (this.pikProdate.SelectedDate.Value.ToString("yyyyMMdd").Length == 8)
            {
                string Prodate = this.pikProdate.SelectedDate.Value.ToString("yyyyMMdd");
                string pline = this.ddlProlinename.SelectedItem.Text;
                var q = from p in DB.Pp_P1d_Outputs
                        where p.Prodate.Contains(Prodate) && p.Prolinename.Contains(pline)
                            && !(from d in DB.Pp_P1d_Defects
                                 where d.IsDeleted == 0
                                 where d.Prodate == Prodate
                                 where d.Prolinename == p.Prolinename
                                 select d.Proorder)//20220815修改之前是d.Prolots
                                 .Contains(p.Proorder)//20220815修改之前是p.Prolots
                        select new
                        {
                            Prolot = p.Prolot + "," + p.Proorder,
                        };

                var qs = q.Select(E => new { E.Prolot }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                ddlProlot.DataSource = qs;
                ddlProlot.DataTextField = "Prolot";
                ddlProlot.DataValueField = "Prolot";
                ddlProlot.DataBind();
                this.ddlProlot.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }

        //查询班组
        private void BindDdlLine()

        {
            if (pikProdate.SelectedDate.HasValue)
            {
                string Prodate = pikProdate.SelectedDate.Value.ToString("yyyyMMdd");

                //查询LINQ去重复
                var q = from a in DB.Pp_P1d_Outputs
                            //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
                            //where b.Proecnno == strecn
                        where a.Prodate.Contains(Prodate) && !(from d in DB.Pp_P1d_Defects
                                                               where d.IsDeleted == 0
                                                               where d.Prodate == Prodate
                                                               where d.Prolinename == a.Prolinename
                                                               select d.Prolot)
                                    .Contains(a.Prolot)//投入日期
                        select new
                        {
                            a.Prolinename
                        };

                var qs = q.Select(E => new { E.Prolinename }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                ddlProlinename.DataSource = qs;
                ddlProlinename.DataTextField = "Prolinename";
                ddlProlinename.DataValueField = "Prolinename";
                ddlProlinename.DataBind();
                this.ddlProlinename.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }

        #endregion Page_Load

        #region Events

        protected void prodate_TextChanged(object sender, EventArgs e)
        {
            //绑定DDL
            BindDdlLine();
        }
        protected void prolinename_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddlProlinename.SelectedIndex != 0 && ddlProlinename.SelectedIndex != -1)
            {
                lblProrealqty.Text = "";
                numProdzeroefects.Text = "";
                lblPromodel.Text = "";
                lblProorder.Text = "";
                lblProorderqty.Text = "";
                BindDdlprolot();
                if (ddlProlinename.SelectedItem.Text.Contains("班"))
                {
                    lblProdept.Text = "ASSY";
                }
                else
                {
                    lblProdept.Text = "PCBA";
                }
            }
            else
            {
                lblProrealqty.Text = "";
                numProdzeroefects.Text = "";
                lblPromodel.Text = "";
                lblProorder.Text = "";
                lblProorderqty.Text = "";
                lblProdept.Text = "";
                ddlProlot.Text = "";
                lblProitem.Text = "";
            }
        }

        protected void prolot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProlot.SelectedIndex != -1 && ddlProlot.SelectedIndex != 0 && ddlProlinename.SelectedIndex != -1 && ddlProlinename.SelectedIndex != 0)
            {
                string sdate = this.pikProdate.SelectedDate.Value.ToString("yyyyMMdd");
                string sline = ddlProlinename.SelectedItem.Text;
                string slot = ddlProlot.SelectedItem.Text.Substring(0, ddlProlot.SelectedItem.Text.IndexOf(","));
                string sorder = ddlProlot.SelectedItem.Text.Substring(ddlProlot.SelectedItem.Text.IndexOf(",") + 1, ddlProlot.SelectedItem.Text.Length - ddlProlot.SelectedItem.Text.IndexOf(",") - 1);

                var q_output = from a in DB.Pp_P1d_OutputSubs
                                   //join b in DB.Pp_P1d_OutputSubs on a.ID equals b.Parent.ID

                               where a.IsDeleted == 0
                               where a.Prodate == sdate
                               where a.Prolinename == sline
                               where a.Prolot == slot
                               where a.Proorder == sorder
                               select new
                               {
                                   a.Proorder,
                                   a.Prohbn,
                                   a.Promodel,
                                   a.Prodate,
                                   a.Prolinename,
                                   a.Prolot,
                                   a.Prorealqty
                               };
                var q = from p in q_output

                        group p by new
                        {
                            //order.Proorder,
                            p.Proorder,
                            p.Prohbn,
                            p.Promodel,
                            p.Prodate,
                            p.Prolinename,
                            p.Prolot,

                        }

                    into g
                        select new
                        {
                            g.Key.Proorder,
                            g.Key.Prohbn,
                            g.Key.Promodel,
                            g.Key.Prodate,
                            g.Key.Prolot,
                            Prorealqty = g.Sum(p => p.Prorealqty),
                        };
                var qs = q.ToList();
                if (qs.Any())
                {
                    lblProrealqty.Text = qs[0].Prorealqty.ToString();
                    numProdzeroefects.Text = qs[0].Prorealqty.ToString();
                    lblPromodel.Text = qs[0].Prohbn.ToString();
                    lblProitem.Text = qs[0].Promodel.ToString();
                    lblProorder.Text = sorder;

                    var orderqty = (from a in DB.Pp_Orders
                                    where a.Porderno == lblProorder.Text
                                    select a).ToList();
                    if (orderqty.Any())
                    {
                        lblProorderqty.Text = orderqty[0].Porderqty.ToString();
                        //promodelqty.Text = orderqty[0].Porderqty.ToString();
                    }
                    else
                    {
                        lblProorderqty.Text = "0";
                        //promodelqty.Text = "0";
                    }
                    //机种总数量不再需要
                    //var modelqty = (from a in DB.Pp_P1d_OutputSubs

                    //                where a.Prolot == slot

                    //                group a by new
                    //                {
                    //                    a.Prolot,
                    //                }
                    //                into g
                    //                select new
                    //                {
                    //                    Prolot = g.Key.Prolot,
                    //                    Promodelqty = g.Sum(a => a.Prorealqty),
                    //                }).ToList();
                    //if (modelqty.Any())
                    //{
                    //    promodelqty.Text = modelqty[0].Promodelqty.ToString();
                    //}

                    //proorderqty.Text = modelqty[0].Porderqty.ToString();

                    //BindGrid();
                }

                //ConnStr = "SELECT [prolinename],[Prodate],[Prolot],sum([Prorealqty])[Prorealqty] " +
                //            " FROM [dbo].[Pp_P1d_OutputSubs] where Prodate='" + this.pikProdate.SelectedDate.Value.ToString("yyyyMMdd") + "' and  [Prolot] = '" + prolot.SelectedItem.Text + "' and  [prolinename]='" + prolinename.SelectedItem.Text + "'" +
                //            "  group by[prolinename],[Prodate],[Prolot]";

                //SqlDataAdapter DAstr1 = new SqlDataAdapter(ConnStr, AppConn);
                //DataSet DSstr1 = new DataSet();
                //DAstr1.Fill(DSstr1);
                ////赋值给订单序列号字段[MC008]
                //if (JudgeHelper.IfExitData(DSstr1, 0))
                //{
                //    if (decimal.Parse(DSstr1.Tables[0].Rows[0][3].ToString()) >= 0)
                //    {
                //        prorealqty.Text = DSstr1.Tables[0].Rows[0][3].ToString();

                //        BindGrid();
                //    }
                //    else
                //    {
                //        Alert alert = new Alert();
                //        alert.Message = "ST为0,不能参与计算,请先确认！";
                //        alert.IconFont = IconFont.Warning;
                //        alert.Target = Target.Top;
                //        Alert.ShowInTop();
                //        return;
                //    }
                //}
                //else
                //{
                //    Alert alert = new Alert();
                //    alert.Message = "数据不能为空，请检查机种，LOT数量，ST！源数据在生产订单中，请确认！";
                //    alert.IconFont = IconFont.Warning;
                //    alert.Target = Target.Top;
                //    Alert.ShowInTop();
                //    return;
                //}
            }
        }

        //字段赋值，保存
        private void SaveItem()//新增订单不良数据
        {
            Pp_Defect_P1d_Order item = new Pp_Defect_P1d_Order();

            item.Prolot = ddlProlot.SelectedItem.Text.Substring(0, ddlProlot.SelectedItem.Text.IndexOf(","));
            item.Promodel = lblPromodel.Text;
            item.Proitem = lblProitem.Text;
            item.Prolinename = ddlProlinename.SelectedItem.Text;
            item.Prodate = pikProdate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Proorder = lblProorder.Text;
            item.Proorderqty = (int)decimal.Parse(lblProorderqty.Text);
            item.Prorealqty = int.Parse(lblProrealqty.Text);
            item.Prodzeroefects = int.Parse(numProdzeroefects.Text);
            item.Probadtotal = int.Parse(numProbadtotal.Text);
            item.Prodept = lblProdept.Text;
            item.GUID = Guid.NewGuid();
            // 添加所有用户

            item.Remark = ddlProlot.SelectedItem.Text + "," + lblPromodel.Text;
            item.CreateDate = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_Defect_P1d_Orders.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = this.pikProdate.SelectedDate.Value.ToString("yyyyMMdd") + "," + this.lblProitem.Text + "," + this.lblProorder.Text + "," + this.lblPromodel.Text.ToUpper();
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产不良", "订单不良数据新增", OperateNotes);
        }

        #endregion Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                DefectOrderValidateExists();

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
        /// <summary>
        /// 判断订单是否存在
        /// </summary>
        public void DefectOrderValidateExists()
        {
            string orderNo = lblProorder.Text.Trim();

            Pp_Defect_P1d_Order redata = DB.Pp_Defect_P1d_Orders.Where(u => u.Proorder == orderNo).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("不良信息,订单< " + orderNo + ">已经存在！更新即可");
                return;
            }
            //保存数据
            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }
        /// <summary>
        /// 判断批次是否存在
        /// </summary>

        /// <summary>
        /// 更新生产实际数据
        /// </summary>
        public void UpdateDefectQty()
        {
            string dd = pikProdate.Text.Substring(0, 6);
            var q =
                from p in DB.Pp_P1d_OutputSubs
                        .Where(s => s.Prodate.Substring(0, 6).CompareTo(dd) == 0)
                    //    .Where(s => s.Prodate.Contains(dd))
                    //    .Where(s => s.prolinename.Contains(pl))
                    //.Where(s => s.Prolot.Contains(lt))
                orderby p.Prodate descending
                group p by new
                {
                    p.Prolot,
                    // p.Prodate,
                    //p.prolinename,
                }

                    into g
                select new
                {
                    //Prodate = g.Key.Prodate,
                    // prolinename = g.Key.prolinename,
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
                            //pl = qs[i].prolinename;

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
        /// <summary>
        /// 更新无不良数据
        /// </summary>
        private void UpdateOqty()
        {
            var q = (from a in DB.Pp_P1d_Defects
                     where a.Prolot == ddlProlot.Text
                     select a).ToList();
            if (q.Any())
            {
                for (int i = 0; i < q.Count(); i++)
                {
                    int iid = q[i].ID;
                    Pp_P1d_Defect item = DB.Pp_P1d_Defects
                     .Where(u => u.ID == iid).FirstOrDefault();
                    item.Prodzeroefects = int.Parse(numProdzeroefects.Text);//无不良台数更新
                    item.ModifyDate = DateTime.Now;
                    item.Modifier = GetIdentityName();
                    DB.SaveChanges();
                }
            }
        }

        //public void Dept()
        //{
        //    string lineName = prolinename.Text.Trim();

        //    Adm_Dict redata = DB.Adm_Dicts.Where(u => u.DictLabel.Contains(lineName)) .FirstOrDefault();
        //    if(redata != null)
        //    {
        //        prodept.Text = redata.DictType;
        //    }
        //}
    }
}