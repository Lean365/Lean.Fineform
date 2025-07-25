# NetFileHelper 远程文件处理帮助类使用说明

## 概述

`NetFileHelper` 是一个用于处理远程文件显示、下载、预览等功能的静态帮助类。它提供了完整的文件操作功能，支持本地文件和远程文件的处理。

## 主要功能

### 1. 文件路径处理
- `GetFullFilePath(string filePath)` - 获取文件完整路径
- `GetFileUrl(string filePath)` - 获取文件URL

### 2. 文件存在性检查
- `FileExists(string filePath)` - 检查本地文件是否存在
- `RemoteFileExists(string url)` - 检查远程文件是否存在

### 3. 文件下载
- `DownloadFile(string filePath, string fileName)` - 下载本地文件
- `DownloadRemoteFile(string url, string fileName)` - 下载远程文件

### 4. 文件预览
- `PreviewFile(string filePath)` - 预览本地文件
- `PreviewRemoteFile(string url)` - 预览远程文件

### 5. 文件信息获取
- `GetFileSize(string filePath)` - 获取文件大小
- `GetRemoteFileSize(string url)` - 获取远程文件大小
- `GetFileExtension(string filePath)` - 获取文件扩展名
- `GetContentType(string filePath)` - 获取文件MIME类型

### 6. 文件操作
- `DeleteFile(string filePath)` - 删除文件
- `CopyFile(string sourcePath, string targetPath)` - 复制文件

### 7. 文件列表
- `GetFiles(string directoryPath, string searchPattern)` - 获取目录下的文件列表

## 使用示例

### 1. 在ASPX页面中使用

```aspx
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="example.aspx.cs" Inherits="YourNamespace.example" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件显示示例</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:Grid ID="Grid1" runat="server">
            <Columns>
                <!-- 文件显示列 -->
                <f:TemplateField HeaderText="文件" Width="200px">
                    <ItemTemplate>
                        <div>
                            <div><strong><%# Eval("FileName") %></strong></div>
                            <div>
                                <a href="example.aspx?action=download&filePath=<%# Server.UrlEncode(Eval("FilePath").ToString()) %>" target="_blank">下载</a>
                                <a href="example.aspx?action=preview&filePath=<%# Server.UrlEncode(Eval("FilePath").ToString()) %>" target="_blank">预览</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </f:TemplateField>
            </Columns>
        </f:Grid>
    </form>
</body>
</html>
```

### 2. 在代码后台处理文件操作

```csharp
using LeanFine.Lf_Business.Helper;

public partial class example : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 检查是否是文件操作请求
        string action = Request.QueryString["action"];
        if (!string.IsNullOrEmpty(action))
        {
            HandleFileAction(action);
            return;
        }

        if (!IsPostBack)
        {
            LoadData();
        }
    }

    /// <summary>
    /// 处理文件操作请求
    /// </summary>
    private void HandleFileAction(string action)
    {
        try
        {
            string filePath = Request.QueryString["filePath"];
            string fileName = Request.QueryString["fileName"];

            if (string.IsNullOrEmpty(filePath))
            {
                Alert.ShowInTop("文件路径为空！");
                return;
            }

            switch (action.ToLower())
            {
                case "download":
                    // 检查文件是否存在
                    if (NetFileHelper.FileExists(filePath))
                    {
                        NetFileHelper.DownloadFile(filePath, fileName);
                    }
                    else if (NetFileHelper.RemoteFileExists(filePath))
                    {
                        NetFileHelper.DownloadRemoteFile(filePath, fileName);
                    }
                    else
                    {
                        Alert.ShowInTop("文件不存在或无法访问！");
                    }
                    break;

                case "preview":
                    // 检查文件是否存在
                    if (NetFileHelper.FileExists(filePath))
                    {
                        NetFileHelper.PreviewFile(filePath);
                    }
                    else if (NetFileHelper.RemoteFileExists(filePath))
                    {
                        NetFileHelper.PreviewRemoteFile(filePath);
                    }
                    else
                    {
                        Alert.ShowInTop("文件不存在或无法访问！");
                    }
                    break;

                default:
                    Alert.ShowInTop("无效的操作类型！");
                    break;
            }
        }
        catch (Exception ex)
        {
            Alert.ShowInTop("文件操作失败: " + ex.Message);
        }
    }
}
```

### 3. 直接调用NetFileHelper方法

```csharp
// 检查文件是否存在
if (NetFileHelper.FileExists("~/uploads/document.pdf"))
{
    // 文件存在，执行下载
    NetFileHelper.DownloadFile("~/uploads/document.pdf", "文档.pdf");
}

// 检查远程文件是否存在
if (NetFileHelper.RemoteFileExists("http://example.com/file.pdf"))
{
    // 远程文件存在，执行下载
    NetFileHelper.DownloadRemoteFile("http://example.com/file.pdf", "远程文件.pdf");
}

// 获取文件信息
long fileSize = NetFileHelper.GetFileSize("~/uploads/document.pdf");
string contentType = NetFileHelper.GetContentType("~/uploads/document.pdf");

// 预览文件
NetFileHelper.PreviewFile("~/uploads/document.pdf");
```

## 支持的文件类型

NetFileHelper 支持以下文件类型的MIME类型识别：

- **PDF文件**: `.pdf` → `application/pdf`
- **Word文档**: `.doc`, `.docx` → `application/msword`, `application/vnd.openxmlformats-officedocument.wordprocessingml.document`
- **Excel表格**: `.xls`, `.xlsx` → `application/vnd.ms-excel`, `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`
- **PowerPoint**: `.ppt`, `.pptx` → `application/vnd.ms-powerpoint`, `application/vnd.openxmlformats-officedocument.presentationml.presentation`
- **文本文件**: `.txt` → `text/plain`
- **图片文件**: `.jpg`, `.jpeg`, `.png`, `.gif`, `.bmp` → `image/jpeg`, `image/png`, `image/gif`, `image/bmp`
- **压缩文件**: `.zip`, `.rar`, `.7z` → `application/zip`, `application/x-rar-compressed`, `application/x-7z-compressed`

## 注意事项

1. **权限检查**: 在使用文件操作功能前，请确保有相应的权限检查
2. **异常处理**: 所有文件操作都包含异常处理，建议在使用时添加适当的错误处理
3. **路径安全**: 确保文件路径的安全性，避免路径遍历攻击
4. **文件大小**: 对于大文件，建议添加文件大小限制
5. **网络超时**: 远程文件操作设置了5秒超时，可根据需要调整

## 错误处理

NetFileHelper 提供了完善的错误处理机制：

- 文件不存在时返回相应的错误信息
- 网络连接失败时抛出异常
- 权限不足时返回错误提示
- 文件格式不支持时给出相应提示

## 扩展功能

可以根据需要扩展以下功能：

1. **文件上传**: 添加文件上传功能
2. **文件转换**: 添加文件格式转换功能
3. **文件压缩**: 添加文件压缩功能
4. **批量操作**: 添加批量文件操作功能
5. **进度显示**: 添加文件操作进度显示功能 