using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using FineUIPro;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Lean.Fineform.Lf_Manufacturing.PP.daily.P2D
{

    public partial class p2d_daily_edit : PageBase
    {
        public int SubOphID, Prorate;
        public static string lclass, OPHID, ConnStr, nclass, ncode;
        public static int rowID, delrowID, editrowID, totalSum;

        public static string userid, badSum;



        public int ParentID;
        
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DOutputEdit";
            }
        }

        #endregion

        #region Page_Load

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
            //BindDDLclass();
            //赋值
            BindData();



        }
        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Pp_P2d_Output current = DB.Pp_P2d_Outputs.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }


            proorder.Text = current.Proorder;
            prodate.Text = current.Prodate;
            // 选中当前节点的父节点
            prolinename.Text = current.Prolinename;
            prodirect.Text = current.Prodirect.ToString();
            proindirect.Text = current.Proindirect.ToString();
            prolot.Text = current.Prolot;
            prohbn.Text = current.Prohbn;
            prosn.Text = current.Prosn;
            promodel.Text = current.Promodel;
            prost.Text = current.Prost.ToString();
            //prosubst.Text = current.Prosubst.ToString();





            prostdcapacity.Text = current.Prostdcapacity.ToString();

            prolotqty.Text = current.Proorderqty.ToString();
            //prostime.Text = current.Prostime;
            //proetime.Text = current.Proetime;
            //proplanqty.Text = current.Proplanqty.ToString();
            //prorealqty.Text = current.Prorealqty.ToString();
            //proplantotal.Text=current.Proplantotal.ToString();
            //prorealtotal.Text = current.Prorealtotal.ToString();
            //prolinestop.Checked = current.Prolinestop;
            //prolinestopmin.Text = current.Prolinestopmin.ToString();
            //Probadnote.Text = current.Probadnote;
            //proratio.Text = current.Proratio.ToString();
            remark.Text = current.Remark;
            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);

            //修改前日志
            string BeforeModi = current.Prodate + "," + current.Prolinename + "," + current.Prohbn + "," + current.Promodel;
            string OperateType = "修改";
            string OperateNotes = "*beEdit生产OPH " + BeforeModi + " *beEdit生产OPH 的记录可能将被修改";

            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH修改", OperateNotes);

        }




        #endregion

        #region Events

        //DDL查询不良各类

        private void SaveItem()//新增生产日报单头
        {


            int id = GetQueryIntValue("id");
            Pp_P2d_Output item = DB.Pp_P2d_Outputs

                .Where(u => u.ID == id).FirstOrDefault();



            item.Prodirect = int.Parse(prodirect.Text);
            item.Proindirect = int.Parse(proindirect.Text);

            item.Prolot = prolot.Text;
            item.Prohbn = prohbn.Text;
            item.Prosn = prosn.Text;
            item.Promodel = promodel.Text;
            item.Prost = Decimal.Parse(prost.Text);



            //item.Prosubst = Decimal.Parse(prosubst.Text);
            item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);


            item.Remark = remark.Text;
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Proophs.Add(item);
            DB.SaveChanges();

            SaveSubItem();

            //修改后日志
            string ModifiedText = prolinename.Text.Trim() + "," + prodate.Text + "," + prolot.Text + "," + prohbn.Text + "," + promodel.Text;
            string OperateType = "修改";
            string OperateNotes = "*afEdit生产OPH " + ModifiedText + "*afEdit生产OPH 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH修改", OperateNotes);


        }

        private void OPHRate()//稼动率
        {
            Pp_Efficiency current = DB.Pp_Efficiencys

                .OrderByDescending(u => u.Proratedate).FirstOrDefault();


            Prorate = current.Prorate;
        }
        //新增新增生产日报单身
        /// <summary>
        /// 新增单身数据
        /// </summary>
        private void SaveSubItem()
        {

            //无须更新单身
            //updateSuboph();

        }
        private void updateSuboph()
        {
            int iid = GetQueryIntValue("id");
            var q = from a in DB.Pp_P2d_OutputSubs
                    where a.Parent == iid
                    select a;

            if (q.Any())
            {
                DB.Pp_P2d_OutputSubs

                    .Where(s => s.Parent == iid)
                    .Where(s => s.Prolinemin != 0)

                    .ToList()
                    .ForEach(x => {
                        x.Prodirect = int.Parse(prodirect.Text);
                        x.Proindirect = int.Parse(proindirect.Text);
                        x.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);
                        x.Prolinemin = int.Parse(prodirect.Text) * 60 - x.Prolinestopmin;
                        x.Prorealtime = int.Parse(prodirect.Text) * 60 - x.Prolinestopmin;
                        x.Modifier =GetIdentityName();
                        x.ModifyTime = DateTime.Now;
                    });
                DB.SaveChanges();
            }

            //修改后日志
            string ModifiedText = prodirect.Text.Trim() + "," + proindirect.Text + "," + prostdcapacity.Text;
            string OperateType = "修改";
            string OperateNotes = "*afEdit生产OPH_SUB " + ModifiedText + "*afEdit生产OPH_SUB 的记录已修改";

            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH修改", OperateNotes);


        }
        //新增新增生产日报单身

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (int.Parse(proindirect.Text) > int.Parse(prodirect.Text))
            {
                Alert.ShowInTop("间接人数大于直接人数！");


            }

            //保存OPH数据
            SaveItem();



            //保存不具合数据
            //EditDefectDataRow();
            //Alert.ShowInTop("数据保存成功！（表格数据已重新绑定）");


            //更新不具合总数量
            //BindTotal();



            //表格数据已重新绑定
            //BindGrid();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }
        #endregion
        //计算标准产能
        /// <summary>
        /// 标准产能计算
        /// </summary>
        private void HourQty()
        {

            decimal stdiff = (decimal.Parse(prost.Text));
            if (stdiff != 0)
            {
                OPHRate();
                decimal rate = (decimal)Prorate / 100;
                prostdcapacity.Text = ((decimal.Parse(prodirect.Text) * 60) / (decimal.Parse(prost.Text)) * rate).ToString("0.00");
            }

        }
        protected void prodirect_TextChanged(object sender, EventArgs e)
        {
            HourQty();
        }




    }
}
