using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

using AspNet = System.Web.UI.WebControls;

namespace LeanFine.Lf_Admin
{
    public partial class role_power : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreRolePowerView";
            }
        }

        #endregion ViewPower

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
            // 权限检查
            CheckPowerWithButton("CoreRolePowerEdit", btnGroupUpdate);
            //CheckPowerWithButton("CoreRolePowerEdit", btnViewSelect);
            //btnBatch.OnClientClick = Window1.GetShowReference("~/Lf_Admin/role_power_batch.aspx", "批量更新");
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            BindGrid();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            // 每页记录数
            Grid2.PageSize = ConfigHelper.PageSize;
            BindGrid2();
        }

        private void BindGrid()
        {
            IQueryable<Adm_Role> q = DB.Adm_Roles;

            // 排列
            q = Sort<Adm_Role>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        private Dictionary<string, bool> _currentRolePowers = new Dictionary<string, bool>();

        private void BindGrid2()
        {
            try
            {
                int roleId = GetSelectedDataKeyID(Grid1);
                if (roleId == -1)
                {
                    Grid2.DataSource = null;
                    Grid2.DataBind();
                }
                else
                {
                    // 当前选中角色拥有的权限列表
                    _currentRolePowers.Clear();

                    Adm_Role role = DB.Adm_Roles.Include(r => r.Powers).Where(r => r.ID == roleId).FirstOrDefault();
                    foreach (var power in role.Powers)
                    {
                        string powerName = power.Name;
                        if (!_currentRolePowers.ContainsKey(powerName))
                        {
                            _currentRolePowers.Add(powerName, true);
                        }
                    }

                    if (rbtnAll.Checked)
                    {
                        var q = DB.Adm_Powers.GroupBy(p => p.GroupName);
                        if (Grid2.SortField == "GroupName")
                        {
                            if (Grid2.SortDirection == "ASC")
                            {
                                q = q.OrderBy(g => g.Key);
                            }
                            else
                            {
                                q = q.OrderByDescending(g => g.Key);
                            }
                        }

                        var powers = q.Select(g => new
                        {
                            GroupName = g.Key,
                            Powers = g
                        });

                        Grid2.DataSource = powers;
                        Grid2.DataBind();
                    }
                    if (rbtnView.Checked)
                    {
                        var q = DB.Adm_Powers.Where(w => w.Title.Contains("浏览")).GroupBy(p => p.GroupName);
                        if (Grid2.SortField == "GroupName")
                        {
                            if (Grid2.SortDirection == "ASC")
                            {
                                q = q.OrderBy(g => g.Key);
                            }
                            else
                            {
                                q = q.OrderByDescending(g => g.Key);
                            }
                        }

                        var powers = q.Select(g => new
                        {
                            GroupName = g.Key,
                            Powers = g
                        });

                        Grid2.DataSource = powers;
                        Grid2.DataBind();
                    }
                    if (rbtnNew.Checked)
                    {
                        var q = DB.Adm_Powers.Where(w => w.Title.Contains("新增")).GroupBy(p => p.GroupName);
                        if (Grid2.SortField == "GroupName")
                        {
                            if (Grid2.SortDirection == "ASC")
                            {
                                q = q.OrderBy(g => g.Key);
                            }
                            else
                            {
                                q = q.OrderByDescending(g => g.Key);
                            }
                        }

                        var powers = q.Select(g => new
                        {
                            GroupName = g.Key,
                            Powers = g
                        });

                        Grid2.DataSource = powers;
                        Grid2.DataBind();
                    }
                    if (rbtnEdit.Checked)
                    {
                        var q = DB.Adm_Powers.Where(w => w.Title.Contains("编辑")).GroupBy(p => p.GroupName);
                        if (Grid2.SortField == "GroupName")
                        {
                            if (Grid2.SortDirection == "ASC")
                            {
                                q = q.OrderBy(g => g.Key);
                            }
                            else
                            {
                                q = q.OrderByDescending(g => g.Key);
                            }
                        }

                        var powers = q.Select(g => new
                        {
                            GroupName = g.Key,
                            Powers = g
                        });

                        Grid2.DataSource = powers;
                        Grid2.DataBind();
                    }
                    if (rbtnDelete.Checked)
                    {
                        var q = DB.Adm_Powers.Where(w => w.Title.Contains("删除")).GroupBy(p => p.GroupName);
                        if (Grid2.SortField == "GroupName")
                        {
                            if (Grid2.SortDirection == "ASC")
                            {
                                q = q.OrderBy(g => g.Key);
                            }
                            else
                            {
                                q = q.OrderByDescending(g => g.Key);
                            }
                        }

                        var powers = q.Select(g => new
                        {
                            GroupName = g.Key,
                            Powers = g
                        });

                        Grid2.DataSource = powers;
                        Grid2.DataBind();
                    }
                    if (rbtnPrint.Checked)
                    {
                        var q = DB.Adm_Powers.Where(w => w.GroupName.Contains("通用")).GroupBy(p => p.GroupName);
                        if (Grid2.SortField == "GroupName")
                        {
                            if (Grid2.SortDirection == "ASC")
                            {
                                q = q.OrderBy(g => g.Key);
                            }
                            else
                            {
                                q = q.OrderByDescending(g => g.Key);
                            }
                        }

                        var powers = q.Select(g => new
                        {
                            GroupName = g.Key,
                            Powers = g
                        });

                        Grid2.DataSource = powers;
                        Grid2.DataBind();
                    }
                }
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
        }

        #endregion Page_Load

        #region Grid1 Events

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            BindGrid2();
        }

        protected void Grid1_RowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            BindGrid2();
        }

        #endregion Grid1 Events

        #region Grid2 Events

        protected void Grid2_RowDataBound(object sender, FineUIPro.GridRowEventArgs e)
        {
            AspNet.CheckBoxList ddlPowers = (AspNet.CheckBoxList)Grid2.Rows[e.RowIndex].FindControl("ddlPowers");

            IGrouping<string, Adm_Power> powers = e.DataItem.GetType().GetProperty("Powers").GetValue(e.DataItem, null) as IGrouping<string, Adm_Power>;

            foreach (Adm_Power power in powers.ToList())
            {
                AspNet.ListItem item = new AspNet.ListItem();
                item.Value = power.ID.ToString();
                item.Text = power.Title;
                item.Attributes["data-qtip"] = power.Name;

                if (_currentRolePowers.ContainsKey(power.Name))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }

                ddlPowers.Items.Add(item);
            }
        }

        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }

        protected void btnGroupUpdate_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreRolePowerEdit"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            int roleId = GetSelectedDataKeyID(Grid1);
            if (roleId == -1)
            {
                return;
            }

            // 当前角色新的权限列表
            List<int> newPowerIDs = new List<int>();
            for (int i = 0; i < Grid2.Rows.Count; i++)
            {
                AspNet.CheckBoxList ddlPowers = (AspNet.CheckBoxList)Grid2.Rows[i].FindControl("ddlPowers");
                foreach (AspNet.ListItem item in ddlPowers.Items)
                {
                    if (item.Selected)
                    {
                        newPowerIDs.Add(Convert.ToInt32(item.Value));
                    }
                }
            }

            Adm_Role role = DB.Adm_Roles.Include(r => r.Powers).Where(r => r.ID == roleId).FirstOrDefault();

            ReplaceEntities<Adm_Power>(role.Powers, newPowerIDs.ToArray());

            DB.SaveChanges();

            Alert.ShowInTop("当前角色的权限更新成功！");
        }

        protected void btnViewSelect_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreRolePowerEdit"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            int roleId = GetSelectedDataKeyID(Grid1);
            if (roleId == -1)
            {
                return;
            }

            // 当前角色新的权限列表
            //List<int> newPowerIDs = new List<int>();
            //for (int i = 0; i < Grid2.Rows.Count; i++)
            //{
            //    AspNet.CheckBoxList ddlPowers = (AspNet.CheckBoxList)Grid2.Rows[i].FindControl("ddlPowers");
            //    foreach (AspNet.ListItem item in ddlPowers.Items)
            //    {
            //        if (item.Selected)
            //        {
            //            newPowerIDs.Add(Convert.ToInt32(item.Value));
            //        }
            //    }
            //}

            //Adm_Role role = DB.Adm_Roles.Include(r => r.Powers).Where(r => r.ID == roleId).FirstOrDefault();

            //ReplaceEntities<Adm_Power>(role.Powers, newPowerIDs.ToArray());

            //DB.SaveChanges();

            //Alert.ShowInTop("当前角色的权限更新成功！");
        }

        #endregion Grid2 Events

        protected void rbtnAuto_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }
            if (rbtnAll.Checked)
            {
                BindGrid2();
            }
            if (rbtnView.Checked)
            {
                BindGrid2();
            }
            else if (rbtnNew.Checked)
            {
                BindGrid2();
            }
            else if (rbtnEdit.Checked)
            {
                BindGrid2();
            }
            else if (rbtnDelete.Checked)
            {
                BindGrid2();
            }
            else if (rbtnPrint.Checked)
            {
                BindGrid2();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}