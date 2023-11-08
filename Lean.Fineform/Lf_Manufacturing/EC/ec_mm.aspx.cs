using System;
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

namespace Lean.Fineform.Lf_Manufacturing.EC
{
    public partial class ec_mm : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcMMView";
            }
        }

        #endregion

        #region Page_Load
        public static String mysql, tracestr, strecnno;

        public int selID;
        protected void Page_Load(object sender, EventArgs e)
        {

            //numBEbudgetmoney.Attributes.Add("Value", "0名");
            //numBEbudgetmoney.Attributes.Add("OnFocus", "if(this.value=='0') {this.value=''}");
            //numBEbudgetmoney.Attributes.Add("OnBlur", "if(this.value==''){this.value='0'}");
            if (!IsPostBack)
            {
                LoadData();

                //btnDel.OnClientClick = Grid2.GetNoSelectionAlertReference(" 请至少选择一项！") + GetDeleteScript(Grid2); // 删除选中行客户端脚本定义 .就这一句就可以了。（和官网的一至）


            }
        }


        private void LoadData()
        {
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            BindGrid1();

            // 默认选中第一个角色
            Grid2.SelectedRowIndex = 0;
            // 每页记录数
            Grid2.PageSize = ConfigHelper.PageSize;

            BindGrid2();
        }



        private void BindGrid1()
        {
            try
            {
                if (rbtnFirstAuto.Checked)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            where b.isDelete == 0
                            where string.IsNullOrEmpty(b.Ec_mmdate)
                            where !string.IsNullOrEmpty(b.Ec_iqcdate)// != "" 
                            where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" 

                            where b.isConfirm==1||b.isConfirm==2
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                //a.Ec_mmdate,
                                b.Ec_model,
                                b.Ec_bomitem,
                                b.Ec_bomsubitem,
                                b.Ec_olditem,
                                b.Ec_newitem,
                                a.Ec_issuedate,

                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_bomitem.ToString().Contains(searchText) || u.Ec_bomsubitem.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }
                    //q=q.Where(x =>(x.Ec_mmdate ?? string.Empty)== string.Empty);
                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).ToList().Distinct();

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(q);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, q);
                    //}


                    Grid1.DataSource = qs;
                    Grid1.DataBind();



                    Grid1.SelectedRowIndex = 0;
                }
                else if (rbtnSecondAuto.Checked)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            where b.isDelete == 0
                            where !string.IsNullOrEmpty(b.Ec_mmdate)
                            where !string.IsNullOrEmpty(b.Ec_iqcdate)// != "" 
                            where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" 

                            where b.isConfirm == 1 || b.isConfirm == 2
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                //a.Ec_mmdate,
                                b.Ec_model,
                                b.Ec_bomitem,
                                b.Ec_bomsubitem,
                                b.Ec_olditem,
                                b.Ec_newitem,
                                a.Ec_issuedate,

                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_bomitem.ToString().Contains(searchText) || u.Ec_bomsubitem.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).ToList().Distinct();
                    //// 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(q);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, q);
                    //}


                    Grid1.DataSource = qs;
                    Grid1.DataBind();



                    Grid1.SelectedRowIndex = 0;
                }
                else if (rbtnThirdAuto.Checked)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            where b.isDelete == 0
                            //where string.IsNullOrEmpty(b.Ec_mmdate)
                            //where !string.IsNullOrEmpty(b.Ec_iqcdate)// != "" 
                           //where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" 

                            where b.isConfirm == 1 || b.isConfirm == 2
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                //a.Ec_mmdate,
                                b.Ec_model,
                                b.Ec_bomitem,
                                b.Ec_bomsubitem,
                                b.Ec_olditem,
                                b.Ec_newitem,
                                a.Ec_issuedate,

                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_bomitem.ToString().Contains(searchText) || u.Ec_bomsubitem.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).ToList().Distinct();

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(q);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, q);
                    //}


                    Grid1.DataSource = qs;
                    Grid1.DataBind();



                    Grid1.SelectedRowIndex = 0;
                }
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);

            }
        }




        private void BindGrid2()
        {
            if (Grid1.SelectedRowIndex < 0)
            {
                Grid2.DataSource = "";

                Grid2.DataBind();
                return;
            }



            if (!String.IsNullOrEmpty(strecnno))
            {
                if (rbtnFirstAuto.Checked)
                {



                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              
                              where a.Ec_no.Contains(strecnno)
                              where b.isDelete == 0
                              where string.IsNullOrEmpty(b.Ec_mmdate)
                              where !string.IsNullOrEmpty(b.Ec_iqcdate)// != "" 
                              where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" 
                              //where a.Ec_newitem != "0"
                              where b.isConfirm == 1 || b.isConfirm == 2
                              select new
                              {
                                  a.Ec_documents,
                                  a.Ec_letterno,
                                  a.Ec_letterdoc,
                                  a.Ec_eppletterno,
                                  a.Ec_eppletterdoc,
                                  a.Ec_teppletterno,
                                  a.Ec_teppletterdoc,
                                  Ec_mmdate = b.Ec_mmdate == null ? "" : b.Ec_mmdate,
                                  b.Ec_mmlot,
                                  b.Ec_mmlotno,
                                  b.Ec_mmnote,
                                  b.Ec_model,
                                  b.Ec_newitem,
                                  b.Ec_no,
                                  b.Ec_olditem,
                                  b.Ec_pmclot,
                                  

                              };
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_mmdate,
                        E.Ec_mmlot,
                        E.Ec_mmlotno,
                        E.Ec_mmnote,
                        E.Ec_model,
                        E.Ec_newitem,
                        E.Ec_no,
                        E.Ec_olditem,
                        E.Ec_pmclot,
                        
                    }).Distinct();

                    

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (GridHelper.GetTotalCount(q) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);
                        // 3.绑定到Grid
                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }
                if (rbtnSecondAuto.Checked)
                {


                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              where a.Ec_no.Contains(strecnno)
                              //where a.Ec_newitem != "0"
                              where !string.IsNullOrEmpty(b.Ec_mmdate)
                              where !string.IsNullOrEmpty(b.Ec_iqcdate)// != "" 
                              where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" 
                              where b.isDelete == 0
                              where b.isConfirm == 1 || b.isConfirm == 2
                              select new
                              {
                                  a.Ec_documents,
                                  a.Ec_letterno,
                                  a.Ec_letterdoc,
                                  a.Ec_eppletterno,
                                  a.Ec_eppletterdoc,
                                  a.Ec_teppletterno,
                                  a.Ec_teppletterdoc,
                                  Ec_mmdate = b.Ec_mmdate == null ? "" : b.Ec_mmdate,
                                  b.Ec_mmlot,
                                  b.Ec_mmlotno,
                                  b.Ec_mmnote,
                                  b.Ec_model,
                                  b.Ec_newitem,
                                  b.Ec_no,
                                  b.Ec_olditem,
                                  b.Ec_pmclot,
                                  

                              };
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_mmdate,
                        E.Ec_mmlot,
                        E.Ec_mmlotno,
                        E.Ec_mmnote,
                        E.Ec_model,
                        E.Ec_newitem,
                        E.Ec_no,
                        E.Ec_olditem,
                        E.Ec_pmclot,
                        
                    }).Distinct();

                    

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (GridHelper.GetTotalCount(q) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);
                        // 3.绑定到Grid
                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }
                if (rbtnThirdAuto.Checked)
                {


                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              where a.Ec_no.Contains(strecnno)
                              where b.isDelete == 0                             
                              where b.isConfirm == 1 || b.isConfirm == 2
                              select new
                              {
                                  a.Ec_documents,
                                  a.Ec_letterno,
                                  a.Ec_letterdoc,
                                  a.Ec_eppletterno,
                                  a.Ec_eppletterdoc,
                                  a.Ec_teppletterno,
                                  a.Ec_teppletterdoc,
                                  Ec_mmdate = b.Ec_mmdate == null ? "" : b.Ec_mmdate,
                                  b.Ec_mmlot,
                                  b.Ec_mmlotno,
                                  b.Ec_mmnote,
                                  b.Ec_model,
                                  b.Ec_newitem,
                                  b.Ec_no,
                                  b.Ec_olditem,
                                  b.Ec_pmclot,


                              };
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_mmdate,
                        E.Ec_mmlot,
                        E.Ec_mmlotno,
                        E.Ec_mmnote,
                        E.Ec_model,
                        E.Ec_newitem,
                        E.Ec_no,
                        E.Ec_olditem,
                        E.Ec_pmclot,
                        
                    }).Distinct();

                    

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (GridHelper.GetTotalCount(q) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);
                        // 3.绑定到Grid
                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }

            }
            else
            {
                // 3.绑定到Grid
                Grid2.DataSource = "";
                Grid2.DataBind();
            }
        }




        #endregion

        #region Events
        protected void ttbSearchEcnsub_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.ShowTrigger1 = true;
            BindGrid1();
            BindGrid2();
        }

        protected void ttbSearchEcnsub_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.Text = String.Empty;
            ttbSearchEcnsub.ShowTrigger1 = false;
            BindGrid1();
            BindGrid2();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageindex1 = Grid1.PageIndex;
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid1.PageIndex > Grid1.PageCount - 1)
            {
                Grid1.PageIndex = Grid1.PageCount - 1;
            }

            BindGrid1();
            int pageindex2 = Grid1.PageIndex;
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid2.PageIndex > Grid2.PageCount - 1)
            {
                Grid2.PageIndex = Grid2.PageCount - 1;
            }

            BindGrid2();
        }


        #endregion

        #region Grid1 Events

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            BindGrid2();
        }

        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {

            object[] keys = Grid1.DataKeys[e.RowIndex];            
            strecnno = keys[1].ToString();
            BindGrid2();
        }

        #endregion

        #region Grid2 Events

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithLinkButtonField("CoreBudgetDelete", Grid2, "deleteField");

            // 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript(Grid1);
            CheckPowerWithLinkButtonField("CoreEcMMEdit", Grid2, "editField");
        }

        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            //参数传递
            var q = from a in DB.Pp_Ecs
                    where a.Ec_no.Contains(strecnno)
                    select new
                    {

                        a.Ec_distinction,


                    };

            var qs = q.ToList();
            //ID,Ec_no,Ec_model,Ec_bomitem,Ec_olditem,Ec_newitem
            object[] keys = Grid2.DataKeys[e.RowIndex];


            if (keys[0] == null)
            {
                tracestr = ",";
            }
            else
            {
                tracestr = keys[0].ToString() + ",";
            }

            if (keys[1] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[1].ToString() + ",";
            }
            if (keys[2] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[2].ToString() + ",";
            }
            if (keys[3] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[3].ToString() + ",";
            }
            if (keys[4] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[4].ToString() + ",";
            }
            if (keys[5] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[5].ToString() + "," + qs[0].Ec_distinction.ToString();
            }

            PageContext.RegisterStartupScript(Window3.GetShowReference("~/Lf_Manufacturing/EC/ec_Mm_edit.aspx?Ec_no=" + tracestr + "&type=1") + Window3.GetMaximizeReference());
            //选中ID
            //int DelID = Convert.ToInt32(Grid2.DataKeys[e.RowIndex][0]);//GetSelectedDataKeyID(Grid2);

            BindGrid2();

        }

        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {

        }

        #endregion

        #region Other Events

        protected void rbtnAuto_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            string checkedValue = String.Empty;
            if (rbtnFirstAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            else if (rbtnSecondAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            else if (rbtnThirdAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            strecnno = "";
        }
        #endregion

        #region NetOperateNotes

        #endregion

        protected void Window3_Close(object sender, EventArgs e)
        {
            BindGrid1();
            BindGrid2();
        }


    }
}

