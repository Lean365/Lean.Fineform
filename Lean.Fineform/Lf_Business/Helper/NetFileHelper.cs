using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Runtime.InteropServices;
using System.Configuration;

namespace LeanFine.Lf_Business.Helper
{
    /// <summary>
    /// 远程文件处理帮助类
    /// </summary>
    public static class NetFileHelper
    {
        #region 文件路径处理

        /// <summary>
        /// 获取文件完整路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>完整的文件路径</returns>
        public static string GetFullFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            // 如果是绝对路径，直接返回
            if (Path.IsPathRooted(filePath))
                return filePath;

            // 如果是相对路径，转换为绝对路径
            return Path.Combine(HttpContext.Current.Server.MapPath("~/"), filePath.TrimStart('~', '/'));
        }

        /// <summary>
        /// 获取文件URL
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件URL</returns>
        public static string GetFileUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            // 如果是网络URL，直接返回
            if (filePath.StartsWith("http://") || filePath.StartsWith("https://"))
                return filePath;

            // 如果是相对路径，转换为URL
            return VirtualPathUtility.ToAbsolute(filePath);
        }

        #endregion

        #region 文件存在性检查

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件是否存在</returns>
        public static bool FileExists(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                string fullPath = GetFullFilePath(filePath);
                return File.Exists(fullPath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查远程文件是否存在
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <returns>文件是否存在</returns>
        public static bool RemoteFileExists(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                request.Timeout = 5000;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 文件下载

        /// <summary>
        /// 下载文件到客户端
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">下载时的文件名</param>
        public static void DownloadFile(string filePath, string fileName = null)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            try
            {
                string fullPath = GetFullFilePath(filePath);
                if (!File.Exists(fullPath))
                    return;

                if (string.IsNullOrEmpty(fileName))
                    fileName = Path.GetFileName(fullPath);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = GetContentType(filePath);
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                HttpContext.Current.Response.TransmitFile(fullPath);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw new Exception("文件下载失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 下载远程文件到客户端
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="fileName">下载时的文件名</param>
        public static void DownloadRemoteFile(string url, string fileName = null)
        {
            if (string.IsNullOrEmpty(url))
                return;

            try
            {
                if (string.IsNullOrEmpty(fileName))
                    fileName = Path.GetFileName(url);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = GetContentType(url);
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));

                using (WebClient client = new WebClient())
                {
                    byte[] fileBytes = client.DownloadData(url);
                    HttpContext.Current.Response.BinaryWrite(fileBytes);
                }

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw new Exception("远程文件下载失败: " + ex.Message);
            }
        }

        #endregion

        #region 文件预览

        /// <summary>
        /// 在浏览器中预览文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void PreviewFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            try
            {
                string fullPath = GetFullFilePath(filePath);
                if (!File.Exists(fullPath))
                    return;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = GetContentType(filePath);
                HttpContext.Current.Response.TransmitFile(fullPath);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw new Exception("文件预览失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 在浏览器中预览远程文件
        /// </summary>
        /// <param name="url">文件URL</param>
        public static void PreviewRemoteFile(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = GetContentType(url);

                using (WebClient client = new WebClient())
                {
                    byte[] fileBytes = client.DownloadData(url);
                    HttpContext.Current.Response.BinaryWrite(fileBytes);
                }

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw new Exception("远程文件预览失败: " + ex.Message);
            }
        }

        #endregion

        #region 文件信息获取

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小（字节）</returns>
        public static long GetFileSize(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return 0;

            try
            {
                string fullPath = GetFullFilePath(filePath);
                if (File.Exists(fullPath))
                {
                    FileInfo fileInfo = new FileInfo(fullPath);
                    return fileInfo.Length;
                }
            }
            catch
            {
                return 0;
            }

            return 0;
        }

        /// <summary>
        /// 获取远程文件大小
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <returns>文件大小（字节）</returns>
        public static long GetRemoteFileSize(string url)
        {
            if (string.IsNullOrEmpty(url))
                return 0;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                request.Timeout = 5000;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.ContentLength;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件扩展名</returns>
        public static string GetFileExtension(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            return Path.GetExtension(filePath).ToLower();
        }

        /// <summary>
        /// 获取文件MIME类型
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MIME类型</returns>
        public static string GetContentType(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return "application/octet-stream";

            string extension = GetFileExtension(filePath);
            return GetContentTypeByExtension(extension);
        }

        /// <summary>
        /// 根据扩展名获取MIME类型
        /// </summary>
        /// <param name="extension">文件扩展名</param>
        /// <returns>MIME类型</returns>
        private static string GetContentTypeByExtension(string extension)
        {
            switch (extension.ToLower())
            {
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".ppt":
                    return "application/vnd.ms-powerpoint";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".txt":
                    return "text/plain";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".zip":
                    return "application/zip";
                case ".rar":
                    return "application/x-rar-compressed";
                case ".7z":
                    return "application/x-7z-compressed";
                default:
                    return "application/octet-stream";
            }
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                string fullPath = GetFullFilePath(filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>是否复制成功</returns>
        public static bool CopyFile(string sourcePath, string targetPath)
        {
            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(targetPath))
                return false;

            try
            {
                string sourceFullPath = GetFullFilePath(sourcePath);
                string targetFullPath = GetFullFilePath(targetPath);

                if (File.Exists(sourceFullPath))
                {
                    // 确保目标目录存在
                    string targetDir = Path.GetDirectoryName(targetFullPath);
                    if (!Directory.Exists(targetDir))
                        Directory.CreateDirectory(targetDir);

                    File.Copy(sourceFullPath, targetFullPath, true);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        #endregion

        #region 文件列表

        /// <summary>
        /// 获取目录下的文件列表
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <returns>文件列表</returns>
        public static List<FileInfo> GetFiles(string directoryPath, string searchPattern = "*.*")
        {
            List<FileInfo> files = new List<FileInfo>();

            if (string.IsNullOrEmpty(directoryPath))
                return files;

            try
            {
                string fullPath = GetFullFilePath(directoryPath);
                if (Directory.Exists(fullPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(fullPath);
                    files.AddRange(dir.GetFiles(searchPattern));
                }
            }
            catch
            {
                // 忽略异常，返回空列表
            }

            return files;
        }

        #endregion

        #region 文件大小格式化

        /// <summary>
        /// 格式化文件大小显示
        /// </summary>
        /// <param name="fileSize">文件大小（字节）</param>
        /// <returns>格式化后的文件大小字符串</returns>
        public static string FormatFileSize(long fileSize)
        {
            if (fileSize < 1024)
                return fileSize + " B";
            else if (fileSize < 1024 * 1024)
                return (fileSize / 1024.0).ToString("F1") + " KB";
            else if (fileSize < 1024 * 1024 * 1024)
                return (fileSize / (1024.0 * 1024.0)).ToString("F1") + " MB";
            else
                return (fileSize / (1024.0 * 1024.0 * 1024.0)).ToString("F1") + " GB";
        }

        #endregion

        // 网络共享连接统一对外接口
        public static bool ConnectToShare(string uncPath, string username, string password)
        {
            return SharedFolderHelper.ConnectToShare(uncPath, username, password);
        }

        public static bool DisconnectFromShare(string uncPath)
        {
            return SharedFolderHelper.DisconnectFromShare(uncPath);
        }
    }

    public static class SharedFolderHelper
    {
        // 连接共享文件夹（Windows API方式）
        [DllImport("mpr.dll", CharSet = CharSet.Auto)]
        private static extern int WNetAddConnection2(ref NETRESOURCE netResource, string password, string username, int flags);

        // 断开共享连接
        [DllImport("mpr.dll", CharSet = CharSet.Auto)]
        private static extern int WNetCancelConnection2(string lpName, int dwFlags, bool fForce);

        // 网络资源结构体
        [StructLayout(LayoutKind.Sequential)]
        private struct NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        /// <summary>
        /// 连接共享文件夹
        /// </summary>
        public static bool ConnectToShare(string uncPath, string username, string password)
        {
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo("net", $"use {uncPath} {password} /user:{username}")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = System.Diagnostics.Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    System.Diagnostics.Debug.WriteLine($"[net use 输出]: {output}");
                    if (output.Contains("命令成功完成"))
                    {
                        System.Diagnostics.Debug.WriteLine("[DEBUG] net use 连接成功");
                        return true;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[DEBUG] net use 连接失败");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[net use 异常]: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 断开共享连接
        /// </summary>
        public static bool DisconnectFromShare(string uncPath)
        {
            return WNetCancelConnection2(uncPath, 1, true) == 0;
        }

        /// <summary>
        /// 获取共享文件夹中的文件列表（支持递归）
        /// </summary>
        public static List<FileInfo> GetFilesFromUncPath(string uncPath, bool recursive = true)
        {
            var files = new List<FileInfo>();
            try
            {
                var dir = new DirectoryInfo(uncPath);
                files.AddRange(dir.GetFiles());
                if (recursive)
                {
                    files.AddRange(dir.GetDirectories()
                        .SelectMany(d => GetFilesFromUncPath(d.FullName, recursive)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"访问失败: {ex.Message}");
            }
            return files;
        }
    }
}
