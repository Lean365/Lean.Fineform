using FineUIPro;
using LeanFine.Lf_Business.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.TL
{
    public partial class liaison_remote : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTlView";
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
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDdlModel();
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                // 获取远程文件列表
                DataTable fileTable = GetRemoteFileList();

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = fileTable.Rows.Count;

                if (Grid1.RecordCount != 0)
                {
                    // 应用搜索过滤
                    DataTable filteredTable = ApplySearchFilter(fileTable);

                    // 应用分页
                    DataTable pagedTable = ApplyPaging(filteredTable);

                    Grid1.DataSource = pagedTable;
                    Grid1.DataBind();
                }
                else
                {
                    Grid1.DataSource = "";
                    Grid1.DataBind();
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
                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }

        /// <summary>
        /// 获取远程文件列表
        /// </summary>
        /// <returns>文件信息数据表</returns>
        private DataTable GetRemoteFileList()
        {
            DataTable table = new DataTable();

            // 添加列
            table.Columns.Add("FileID", typeof(string));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("FilePath", typeof(string));
            table.Columns.Add("FileType", typeof(string));
            table.Columns.Add("FileSize", typeof(long));
            table.Columns.Add("FileExists", typeof(bool));
            table.Columns.Add("LastModified", typeof(DateTime));
            table.Columns.Add("FileCategory", typeof(string));

            // 从网络共享获取文件信息
            List<RemoteFileInfo> fileList = GetRemoteFileListFromSource();

            foreach (var fileInfo in fileList)
            {
                DataRow row = table.NewRow();
                row["FileID"] = fileInfo.FileID;
                row["FileName"] = fileInfo.FileName;
                row["FilePath"] = fileInfo.FilePath;
                row["FileType"] = fileInfo.FileType;
                row["FileSize"] = fileInfo.FileSize;
                row["FileExists"] = fileInfo.FileExists;
                row["LastModified"] = fileInfo.LastModified;
                row["FileCategory"] = fileInfo.FileCategory;
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// 从数据源获取远程文件列表
        /// </summary>
        /// <returns>远程文件信息列表</returns>
        private List<RemoteFileInfo> GetRemoteFileListFromSource()
        {
            List<RemoteFileInfo> fileList = new List<RemoteFileInfo>();

            // 从配置的网络共享路径获取文件
            string[] remotePaths = GetRemoteFilePaths();
            System.Diagnostics.Debug.WriteLine($"开始处理 {remotePaths.Length} 个文件路径");

            foreach (string path in remotePaths)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    System.Diagnostics.Debug.WriteLine($"处理路径: {path}");

                    // 检查是否为网络共享路径
                    if (IsUncPath(path))
                    {
                        System.Diagnostics.Debug.WriteLine($"检测到UNC路径: {path}");
                        // 处理网络共享路径
                        ProcessUncPath(path, fileList);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"检测到本地/远程路径: {path}");
                        // 检查本地文件是否存在
                        bool fileExists = NetFileHelper.FileExists(path) || NetFileHelper.RemoteFileExists(path);

                        if (fileExists)
                        {
                            System.Diagnostics.Debug.WriteLine($"文件存在: {path}");
                            RemoteFileInfo fileInfo = new RemoteFileInfo
                            {
                                FileID = Guid.NewGuid().ToString(),
                                FileName = Path.GetFileName(path),
                                FilePath = path,
                                FileType = NetFileHelper.GetContentType(path),
                                FileSize = NetFileHelper.FileExists(path)
                                    ? NetFileHelper.GetFileSize(path)
                                    : NetFileHelper.GetRemoteFileSize(path),
                                FileExists = true,
                                LastModified = DateTime.Now,
                                FileCategory = GetFileCategory(path)
                            };
                            fileList.Add(fileInfo);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"文件不存在: {path}");
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine($"最终获取到 {fileList.Count} 个文件信息");
            return fileList;
        }

        /// <summary>
        /// 检查是否为UNC路径（网络共享路径）
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否为UNC路径</returns>
        private bool IsUncPath(string path)
        {
            return !string.IsNullOrEmpty(path) && path.StartsWith(@"\\");
        }

        /// <summary>
        /// 处理UNC路径（网络共享路径）
        /// </summary>
        /// <param name="uncPath">UNC路径</param>
        /// <param name="fileList">文件列表</param>
        private void ProcessUncPath(string uncPath, List<RemoteFileInfo> fileList)
        {
            try
            {
                // 获取网络共享的用户名和密码
                string username = GetNetworkShareUsername();
                string password = GetNetworkSharePassword();

                // 连接网络共享
                if (NetFileHelper.ConnectToShare(uncPath, username, password))
                {
                    try
                    {
                        // 获取文件信息
                        if (File.Exists(uncPath))
                        {
                            var fileInfo = new FileInfo(uncPath);
                            RemoteFileInfo remoteFileInfo = new RemoteFileInfo
                            {
                                FileID = Guid.NewGuid().ToString(),
                                FileName = fileInfo.Name,
                                FilePath = uncPath,
                                FileType = NetFileHelper.GetContentType(uncPath),
                                FileSize = fileInfo.Length,
                                FileExists = true,
                                LastModified = fileInfo.LastWriteTime,
                                FileCategory = GetFileCategory(uncPath)
                            };
                            fileList.Add(remoteFileInfo);
                        }
                    }
                    finally
                    {
                        // 断开网络共享连接
                        NetFileHelper.DisconnectFromShare(uncPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop("加载网络共享数据时发生错误，请稍后重试。", MessageBoxIcon.Error + ex.Message);// 记录错误但不中断处理
            }
        }

        /// <summary>
        /// 获取网络共享用户名
        /// </summary>
        /// <returns>用户名</returns>
        private string GetNetworkShareUsername()
        {
            return ConfigurationManager.AppSettings["NetworkShareUsername"];
        }

        /// <summary>
        /// 获取网络共享密码
        /// </summary>
        /// <returns>密码</returns>
        private string GetNetworkSharePassword()
        {
            return ConfigurationManager.AppSettings["NetworkSharePassword"];
        }

        /// <summary>
        /// 获取远程文件路径列表
        /// </summary>
        /// <returns>文件路径数组</returns>
        private string[] GetRemoteFilePaths()
        {
            List<string> paths = new List<string>();

            // 从配置文件获取远程文件路径和子目录
            string shareRoot = ConfigurationManager.AppSettings["NetworkSharePath"];
            string subPath = ConfigurationManager.AppSettings["NetworkShareSubPath"];
            System.Diagnostics.Debug.WriteLine($"配置的网络共享路径: {shareRoot}");
            System.Diagnostics.Debug.WriteLine($"配置的子目录: {subPath}");

            if (!string.IsNullOrEmpty(shareRoot) && !string.IsNullOrEmpty(subPath))
            {
                System.Diagnostics.Debug.WriteLine($"处理网络共享目录: {shareRoot} 子目录: {subPath}");
                string[] uncFiles = GetUncDirectoryFiles(shareRoot, subPath);
                paths.AddRange(uncFiles);
                System.Diagnostics.Debug.WriteLine($"从网络共享获取到 {uncFiles.Length} 个文件");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("未配置网络共享路径或子目录");
            }

            System.Diagnostics.Debug.WriteLine($"总共获取到 {paths.Count} 个文件路径");
            return paths.ToArray();
        }

        /// <summary>
        /// 获取网络共享目录下的所有文件
        /// </summary>
        /// <param name="shareRoot">共享根目录</param>
        /// <param name="subPath">子目录</param>
        /// <returns>文件路径数组</returns>
        private string[] GetUncDirectoryFiles(string shareRoot, string subPath)
        {
            List<string> files = new List<string>();
            System.Diagnostics.Debug.WriteLine($"开始处理网络共享目录: {shareRoot} 子目录: {subPath}");
            string username = GetNetworkShareUsername();
            string password = GetNetworkSharePassword();
            bool connected = NetFileHelper.ConnectToShare(shareRoot, username, password);
            System.Diagnostics.Debug.WriteLine($"连接结果: {connected}");
            if (connected)
            {
                try
                {
                    string targetPath = Path.Combine(shareRoot, subPath);
                    var fileInfos = NetFileHelper.GetFiles(targetPath, "*.*");
                    files.AddRange(fileInfos.Select(f => f.FullName));
                    System.Diagnostics.Debug.WriteLine($"从网络共享目录获取到 {files.Count} 个文件");
                    for (int i = 0; i < Math.Min(10, files.Count); i++)
                    {
                        System.Diagnostics.Debug.WriteLine($"示例文件 {i + 1}: {files[i]}");
                    }
                }
                finally
                {
                    NetFileHelper.DisconnectFromShare(shareRoot);
                }
            }
            return files.ToArray();
        }

        /// <summary>
        /// 根据文件路径获取文件分类
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件分类</returns>
        private string GetFileCategory(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return "未知";

            string extension = NetFileHelper.GetFileExtension(filePath);

            if (extension == ".pdf")
                return "PDF文档";
            else if (extension == ".doc" || extension == ".docx")
                return "Word文档";
            else if (extension == ".xls" || extension == ".xlsx")
                return "Excel表格";
            else if (extension == ".ppt" || extension == ".pptx")
                return "PowerPoint演示";
            else if (extension == ".dwg" || extension == ".dxf")
                return "CAD图纸";
            else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                return "图片文件";
            else if (extension == ".zip" || extension == ".rar" || extension == ".7z")
                return "压缩文件";
            else if (extension == ".txt" || extension == ".log")
                return "文本文件";
            else
                return "其他文件";
        }

        /// <summary>
        /// 应用搜索过滤
        /// </summary>
        /// <param name="table">原始数据表</param>
        /// <returns>过滤后的数据表</returns>
        private DataTable ApplySearchFilter(DataTable table)
        {
            DataTable filteredTable = table.Clone();

            // 文本搜索过滤
            string searchText = ttbSearchMessage.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                var rows = table.Select($"FileName LIKE '%{searchText}%' OR FileCategory LIKE '%{searchText}%'");
                foreach (var row in rows)
                {
                    filteredTable.ImportRow(row);
                }
                table = filteredTable;
                filteredTable = table.Clone();
            }

            // 分类过滤
            if (this.DDLModel.SelectedIndex != 0 && this.DDLModel.SelectedIndex != -1)
            {
                string selectedCategory = DDLModel.SelectedItem.Text;
                if (selectedCategory != "全部")
                {
                    var rows = table.Select($"FileCategory = '{selectedCategory}'");
                    foreach (var row in rows)
                    {
                        filteredTable.ImportRow(row);
                    }
                    return filteredTable;
                }
            }

            // 如果没有分类过滤，返回所有数据
            if (string.IsNullOrEmpty(searchText))
            {
                return table;
            }

            return filteredTable;
        }

        /// <summary>
        /// 应用分页
        /// </summary>
        /// <param name="table">原始数据表</param>
        /// <returns>分页后的数据表</returns>
        private DataTable ApplyPaging(DataTable table)
        {
            int pageSize = Grid1.PageSize;
            int pageIndex = Grid1.PageIndex;
            int startIndex = pageIndex * pageSize;

            DataTable pagedTable = table.Clone();
            var rows = table.AsEnumerable().Skip(startIndex).Take(pageSize);

            foreach (var row in rows)
            {
                pagedTable.ImportRow(row);
            }

            return pagedTable;
        }

        #endregion Page_Load

        #region Events

        private void BindDdlModel()
        {
            // 获取文件分类列表
            var categories = new[] {
                "全部",
                "PDF文档",
                "Word文档",
                "Excel表格",
                "PowerPoint演示",
                "CAD图纸",
                "图片文件",
                "压缩文件",
                "文本文件",
                "其他文件"
            };

            DDLModel.DataTextField = "Category";
            DDLModel.DataValueField = "Category";
            DDLModel.DataSource = categories.Select(c => new { Category = c });
            DDLModel.DataBind();

            // 选中根节点
            this.DDLModel.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 纯浏览页面，不需要编辑和删除功能
        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        // 移除此方法，因为页面中没有rblEnableStatus控件

        protected void DDLModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            // 纯浏览页面，不需要编辑和删除功能
            // 只保留文件下载和预览功能
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            // 刷新数据
            BindGrid();
        }

        #endregion Events


    }

    /// <summary>
    /// 远程文件信息类
    /// </summary>
    public class RemoteFileInfo
    {
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public bool FileExists { get; set; }
        public DateTime LastModified { get; set; }
        public string FileCategory { get; set; }
    }
}