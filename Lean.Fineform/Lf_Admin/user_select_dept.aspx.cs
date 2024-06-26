﻿using System;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class user_select_dept : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreDeptView";
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
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            _deptID = GetQueryIntValue("ids");

            // 绑定列表
            BindGrid();

            // 初始化选中项，放在表格数据绑定之后
            if (_selectedRowIndex != -1)
            {
                Grid1.SelectedRowIndex = _selectedRowIndex;
            }
        }

        // 当前部门ID
        private int _deptID;

        // 用来在表格渲染时记录选中的行索引
        private int _selectedRowIndex = -1;

        private void BindGrid()
        {
            Grid1.DataSource = DeptHelper.Adm_Depts;
            Grid1.DataBind();
        }

        #endregion Page_Load

        #region Events

        //protected void btnSaveClose_Click(object sender, EventArgs e)
        //{
        //    int selectedRowIndex = Grid1.SelectedRowIndex;

        //    string deptID = Grid1.DataKeys[selectedRowIndex][0].ToString();
        //    string deptName = Grid1.DataKeys[selectedRowIndex][1].ToString();

        //    //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(deptID, deptName)
        //    //     + ActiveWindow.GetHideReference());

        //    PageContext.RegisterStartupScript(String.Format("selectDept({0},{1});", JsHelper.Enquote(deptName), JsHelper.Enquote(deptID)));
        //}

        protected void Grid1_RowDataBound(object sender, FineUIPro.GridRowEventArgs e)
        {
            // 行绑定后，确定应该选择哪一行
            Adm_Dept dept = e.DataItem as Adm_Dept;
            if (dept != null && _deptID == dept.ID)
            {
                _selectedRowIndex = e.RowIndex;
            }
        }

        #endregion Events
    }
}