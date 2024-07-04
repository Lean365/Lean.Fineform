using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using FineUIPro;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LeanFine
{
    public class ExportHelper
    {
        #region ================适用于单表头================

        public static DataTable GetGridDataTable(Grid grid)
        {
            DataTable dt = new DataTable();
            DataColumn dc;//创建列
            DataRow dr;       //创建行
                              //构造列
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = grid.Columns[i].HeaderText;
                dt.Columns.Add(dc);
            }
            //构造行
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                dr = dt.NewRow();
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    dr[j] = grid.Rows[i].Values[j].ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        #endregion ================适用于单表头================

        /// <summary>
        /// 由DataSet导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel工作表</returns>
        private static Stream ExportDataSetToExcel(DataSet sourceDs, string sheetName)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            string[] sheetNames = sheetName.Split(',');
            for (int i = 0; i < sheetNames.Length; i++)
            {
                XSSFSheet sheet = workbook.CreateSheet(sheetNames[i]) as XSSFSheet;
                XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
                // handling header.
                foreach (DataColumn column in sourceDs.Tables[i].Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in sourceDs.Tables[i].Rows)
                {
                    XSSFRow dataRow = sheet.CreateRow(rowIndex) as XSSFRow;

                    foreach (DataColumn column in sourceDs.Tables[i].Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }

                    rowIndex++;
                }
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            workbook = null;
            return ms;
        }

        /// <summary>
        /// 由DataSet导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="fileName">指定Excel工作表名称</param>
        /// <returns>Excel工作表</returns>
        public static void ExportDataSetToExcel(DataSet sourceDs, string fileName, string sheetName)
        {
            MemoryStream ms = ExportDataSetToExcel(sourceDs, sheetName) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
            ms.Close();
            ms = null;
        }

        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <returns>Excel工作表</returns>
        private static Stream ExportDataTableToExcel(DataTable sourceTable, string sheetName)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            XSSFSheet sheet = workbook.CreateSheet(sheetName) as XSSFSheet;
            XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
            // handling header.
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = 1;

            foreach (DataRow row in sourceTable.Rows)
            {
                XSSFRow dataRow = sheet.CreateRow(rowIndex) as XSSFRow;

                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }

        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="fileName">指定Excel工作表名称</param>
        /// <returns>Excel工作表</returns>
        public static void ExportDataTableToExcel(DataTable sourceTable, string fileName, string sheetName)
        {
            MemoryStream ms = ExportDataTableToExcel(sourceTable, sheetName) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
            ms.Close();
            ms = null;
        }

        //导出2007格式文件For EPPLUS类
        public static void EpplustoXLSXfile(DataTable mydt, string mybname, string myfname)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname);
                //配置文件属性
                wb.Properties.Title = mybname;
                wb.Properties.Subject = "Data";
                wb.Properties.Category = "Excel";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = mybname;
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                //赋值单元格
                ws.Cells[1, 2].Value = mybname;
                //pck.Save();
                ws.Cells[2, 2].Value = "Date";
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 3].Value = DateTime.Now.ToString("yyyy-MM-dd");
                ws.Cells[2, 3].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["A6"].LoadFromDataTable(mydt, true);
                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
                ws.View.FreezePanes(7, 19);
                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        public static void EpplustoXLSXfiles(DataTable mydt, string mybname, string myfname, string mytitle, string update)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                int fistr = mybname.IndexOf("_");
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname.Substring(fistr + 1, mybname.Length - fistr - 1));
                //配置文件属性
                wb.Properties.Title = mybname;
                wb.Properties.Subject = "Data";
                wb.Properties.Category = "Excel";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = mybname;
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                //赋值单元格

                ws.Cells[1, 1].Value = "DTA OPH " + mytitle + " Report";
                ws.Cells[1, 2].Style.Font.Size = 12;//字体大小
                ws.Cells[1, 1].Style.Font.Bold = true;//字体为粗体

                ws.Cells[1, 7].Value = "1時間当たりの生産量";
                ws.Cells[1, 7].Style.Font.Size = 8;
                ws.Cells[2, 1].Value = "Date";
                ws.Cells[2, 2].Value = update;
                ws.Cells[2, 2].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 4].Value = "Company:";
                ws.Cells[2, 5].Value = "DongGuan TEAC Electronics Co.,Ltd";
                ws.Cells[2, 14].Value = "稼働率:";
                ws.Cells[2, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 14].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 14].Style.Font.Size = 10;
                ws.Cells[2, 15].Value = "85.00%";
                ws.Cells[2, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 15].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 15].Style.Font.Size = 10;
                ws.Cells[3, 15].Value = "Program Designer : DTA EDP Davis.Ching " + update.Substring(0, 4);
                ws.Cells[3, 15].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[3, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[3, 15].Style.Font.Size = 6;
                ws.Cells[4, 7].Value = "OPH入力実績(集計条件：ACTUALQTY>0)";
                ws.Cells[4, 7].Style.Font.Color.SetColor(Color.DarkSeaGreen);
                ws.Cells[4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells[4, 7].Style.Font.Size = 10;

                ws.Cells[5, 1, 5, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                ws.Cells[5, 15].Value = "ACT_ST = ACTUALTIME*DIRECTWORKER/ACTUALQTY*0.85";
                ws.Cells[5, 15].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[5, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[5, 15].Style.Font.Size = 6;

                ws.Cells["A6"].LoadFromDataTable(mydt, true);

                ws.Cells[6, 1, 6, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
                ws.View.FreezePanes(7, 19);
                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        //按班组导出Line Output Report
        public static void LineOutput_XlsxFile(DataTable mydt, string mybname, string myfname, string update)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                Color borderColor = Color.FromArgb(155, 155, 155);
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname);
                //配置文件属性
                wb.Properties.Category = "Report";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = "OPH";
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.Subject = "Lean Manufacturing";
                wb.Properties.Title = "DTA Lines Output Report";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                var aTableRange = ws.Cells[1, 1, 1, 15];
                aTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                var bTableRange = ws.Cells[2, 1, 2, 15];
                bTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                var cTableRange = ws.Cells[3, 1, 3, 15];
                cTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                var dTableRange = ws.Cells[5, 1, 5, 15];
                dTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                //赋值单元格
                ws.Cells[1, 1].Value = "DTA Lines Output Report";
                ws.Cells[1, 2].Style.Font.Size = 12;//字体大小
                ws.Cells[1, 1].Style.Font.Bold = true;//字体为粗体

                ws.Cells[1, 7].Value = "1時間当たりの生産量";
                ws.Cells[1, 7].Style.Font.Size = 8;
                ws.Cells[2, 1].Value = "Date";
                ws.Cells[2, 2].Value = update;
                ws.Cells[2, 2].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 4].Value = "Company:";
                ws.Cells[2, 5].Value = "DongGuan TEAC Electronics Co.,Ltd";
                ws.Cells[2, 14].Value = "稼働率:";
                ws.Cells[2, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 14].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 14].Style.Font.Size = 10;
                ws.Cells[2, 15].Value = "85.00%";
                ws.Cells[2, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 15].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 15].Style.Font.Size = 10;
                ws.Cells[3, 15].Value = "Program Designer : DTA EDP Davis.Ching " + update.Substring(0, 4);
                ws.Cells[3, 15].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[3, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[3, 15].Style.Font.Size = 6;
                ws.Cells[4, 7].Value = "OPH入力実績(集計条件：ACTUALQTY>0)";
                ws.Cells[4, 7].Style.Font.Color.SetColor(Color.DarkSeaGreen);
                ws.Cells[4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells[4, 7].Style.Font.Size = 10;
                ws.Cells[5, 1, 5, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                ws.Cells[5, 15].Value = "ACT_ST = ACTUALTIME*DIRECTWORKER/ACTUALQTY*0.85";
                ws.Cells[5, 15].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[5, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[5, 15].Style.Font.Size = 6;

                ws.Cells["A6"].LoadFromDataTable(mydt, true);
                ws.Cells[6, 1, 6, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //pck.Save();
                ws.View.FreezePanes(7, 16);
                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        //按班组导出Rework Output Report
        public static void ReworkLine_XlsxFile(DataTable mydt, string mybname, string myfname, string update)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname);
                //配置文件属性
                wb.Properties.Category = "Rework Report";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = "OPH";
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.Subject = "Lean Manufacturing";
                wb.Properties.Title = "DTA Rework by Lines Output Report";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                //赋值单元格
                var aTableRange = ws.Cells[1, 1, 1, 15];
                aTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                var bTableRange = ws.Cells[2, 1, 2, 15];
                bTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                var cTableRange = ws.Cells[3, 1, 3, 15];
                cTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                var dTableRange = ws.Cells[5, 1, 5, 15];
                dTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                //赋值单元格
                ws.Cells[1, 1].Value = "DTA Rework by Lines Output Report";
                ws.Cells[1, 2].Style.Font.Size = 12;//字体大小
                ws.Cells[1, 1].Style.Font.Bold = true;//字体为粗体

                ws.Cells[1, 7].Value = "1時間当たりの生産量";
                ws.Cells[1, 7].Style.Font.Size = 8;
                ws.Cells[2, 1].Value = "Date";
                ws.Cells[2, 2].Value = update;
                ws.Cells[2, 2].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 4].Value = "Company:";
                ws.Cells[2, 5].Value = "DongGuan TEAC Electronics Co.,Ltd";
                ws.Cells[2, 14].Value = "稼働率:";
                ws.Cells[2, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 14].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 14].Style.Font.Size = 10;
                ws.Cells[2, 15].Value = "85.00%";
                ws.Cells[2, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 15].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 15].Style.Font.Size = 10;
                ws.Cells[3, 15].Value = "Program Designer : DTA EDP Davis.Ching " + update.Substring(0, 4);
                ws.Cells[3, 15].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[3, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[3, 15].Style.Font.Size = 6;
                ws.Cells[4, 7].Value = "OPH入力実績(集計条件：ACTUALQTY>0)";
                ws.Cells[4, 7].Style.Font.Color.SetColor(Color.DarkSeaGreen);
                ws.Cells[4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells[4, 7].Style.Font.Size = 10;
                ws.Cells[5, 1, 5, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                ws.Cells[5, 15].Value = "ACT_ST = ACTUALTIME*DIRECTWORKER/ACTUALQTY*0.85";
                ws.Cells[5, 15].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[5, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[5, 15].Style.Font.Size = 6;
                ws.Cells["A6"].LoadFromDataTable(mydt, true);
                ws.Cells[6, 1, 6, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //pck.Save();
                ws.View.FreezePanes(7, 16);
                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        //按班组导出Model Output Report
        public static void ModelOutput_XlsxFile(DataTable mydt, string mybname, string myfname, string update)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname);
                //配置文件属性
                wb.Properties.Category = "Model Output Report";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = "OPH";
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.Subject = "Lean Manufacturing";
                wb.Properties.Title = "DTA Model Output Report";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                //赋值单元格
                ws.Cells[1, 2].Value = "DTA Model Output Report";
                ws.Cells[1, 2].Style.Font.Size = 12;//字体大小
                ws.Cells[1, 1].Style.Font.Bold = true;//字体为粗体
                ws.Cells[1, 7].Value = "1時間当たりの生産量";
                ws.Cells[1, 7].Style.Font.Size = 8;
                //ws.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                var aTableRange = ws.Cells[1, 1, 1, 11];
                aTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                aTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[2, 1].Value = "Date";
                ws.Cells[2, 2].Value = update;
                ws.Cells[2, 2].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 4].Value = "Company:";
                ws.Cells[2, 5].Value = "DongGuan TEAC Electronics Co.,Ltd";
                ws.Cells[2, 10].Value = "稼働率:";
                ws.Cells[2, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 10].Style.Font.Size = 10;
                ws.Cells[2, 11].Value = "85.00%";
                ws.Cells[2, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 11].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 11].Style.Font.Size = 10;
                //ws.Cells[2, 10, 2, 11].Merge = true;//合并单元格
                var bTableRange = ws.Cells[2, 1, 2, 11];
                bTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[3, 11].Value = "Program Designer : DTA EDP Davis.Ching " + update.Substring(0, 4);
                ws.Cells[3, 11].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[3, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[3, 11].Style.Font.Size = 6;
                var cTableRange = ws.Cells[3, 1, 3, 11];
                cTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells[4, 7].Value = "OPH入力実績(集計条件：ACTUALQTY>0)";
                ws.Cells[4, 7].Style.Font.Color.SetColor(Color.DarkSeaGreen);
                ws.Cells[4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[4, 7].Style.Font.Size = 10;
                //cTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //cTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                ws.Cells[5, 11].Value = "ACT_ST = ACTUALTIME*DIRECTWORKER/ACTUALQTY*0.85";
                ws.Cells[5, 11].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[5, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[5, 11].Style.Font.Size = 6;
                var dTableRange = ws.Cells[5, 1, 5, 11];
                dTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                //dTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //dTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A6"].LoadFromDataTable(mydt, true);
                ws.Cells[6, 1, 6, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //pck.Save();

                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
                ws.View.FreezePanes(7, 12);
                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        //按班组导出Rework Model Report
        public static void ReworkModel_XlsxFile(DataTable mydt, string mybname, string myfname, string update)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname);
                //配置文件属性
                wb.Properties.Category = "Rework Report";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = "OPH";
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.Subject = "Lean Manufacturing";
                wb.Properties.Title = "DTA Rework by Models Output Report";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                //赋值单元格
                ws.Cells[1, 2].Value = "DTA Rework by Models Output Report";
                ws.Cells[1, 2].Style.Font.Size = 12;//字体大小
                ws.Cells[1, 1].Style.Font.Bold = true;//字体为粗体
                ws.Cells[1, 7].Value = "1時間当たりの生産量";
                ws.Cells[1, 7].Style.Font.Size = 8;
                //ws.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                var aTableRange = ws.Cells[1, 1, 1, 11];
                aTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                aTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[2, 1].Value = "Date";
                ws.Cells[2, 2].Value = update;
                ws.Cells[2, 2].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 4].Value = "Company:";
                ws.Cells[2, 5].Value = "DongGuan TEAC Electronics Co.,Ltd";
                ws.Cells[2, 10].Value = "稼働率:";
                ws.Cells[2, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 10].Style.Font.Size = 10;
                ws.Cells[2, 11].Value = "85.00%";
                ws.Cells[2, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 11].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 11].Style.Font.Size = 10;
                //ws.Cells[2, 10, 2, 11].Merge = true;//合并单元格
                var bTableRange = ws.Cells[2, 1, 2, 11];
                bTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[3, 11].Value = "Program Designer : DTA EDP Davis.Ching " + update.Substring(0, 4);
                ws.Cells[3, 11].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[3, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[3, 11].Style.Font.Size = 6;
                var cTableRange = ws.Cells[3, 1, 3, 11];
                cTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells[4, 7].Value = "OPH入力実績(集計条件：ACTUALQTY>0)";
                ws.Cells[4, 7].Style.Font.Color.SetColor(Color.DarkSeaGreen);
                ws.Cells[4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[4, 7].Style.Font.Size = 10;
                //cTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //cTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[5, 11].Value = "ACT_ST = ACTUALTIME*DIRECTWORKER/ACTUALQTY*0.85";
                ws.Cells[5, 11].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[5, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[5, 11].Style.Font.Size = 6;
                var dTableRange = ws.Cells[5, 1, 5, 11];
                dTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //dTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //dTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A6"].LoadFromDataTable(mydt, true);
                ws.Cells[6, 1, 6, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //pck.Save();
                ws.View.FreezePanes(7, 12);
                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        //检查集计表 Inspection Report
        public static void Inspection_XlsxFile(DataTable mydt, string mybname, string myfname, string update)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname);
                //.Properties.Category = "类别"
                //.Properties.Author = "作者"
                //.Properties.Comments = "备注"
                //.Properties.Company = "公司"
                //.Properties.Keywords = "关键字"
                //.Properties.Manager = "管理者"
                //.Properties.Status = "状态"
                //.Properties.Subject = "主题"
                //.Properties.Title = "标题"
                //.Properties.LastModifiedBy = "最后一次修改日期"
                //.Properties.Application = "应用"
                //配置文件属性
                wb.Properties.Category = "Inspection Details";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = "Details;Report";
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.Subject = "Lean Manufacturing";
                wb.Properties.Title = "DTA Inspection Details Report";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                //赋值单元格
                ws.Cells[1, 2].Value = "DTA Inspection Details Report";
                ws.Cells[1, 2].Style.Font.Size = 12;//字体大小
                ws.Cells[1, 1].Style.Font.Bold = true;//字体为粗体
                ws.Cells[1, 7].Value = "1時間当たりの生産量";
                ws.Cells[1, 7].Style.Font.Size = 8;
                //ws.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                var aTableRange = ws.Cells[1, 1, 1, 23];
                aTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                aTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[2, 1].Value = "Date";
                ws.Cells[2, 2].Value = update;
                ws.Cells[2, 2].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 4].Value = "Company:";
                ws.Cells[2, 5].Value = "DongGuan TEAC Electronics Co.,Ltd";
                ws.Cells[2, 22].Value = "稼働率:";
                ws.Cells[2, 22].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 22].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 22].Style.Font.Size = 10;
                ws.Cells[2, 23].Value = "85.00%";
                ws.Cells[2, 23].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 23].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 23].Style.Font.Size = 10;
                //ws.Cells[2, 10, 2, 11].Merge = true;//合并单元格
                var bTableRange = ws.Cells[2, 1, 2, 23];
                bTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[3, 23].Value = "Program Designer : DTA EDP Davis.Ching " + update.Substring(0, 4);
                ws.Cells[3, 23].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[3, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[3, 23].Style.Font.Size = 6;
                var cTableRange = ws.Cells[3, 1, 3, 23];
                cTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells[4, 7].Value = "OPH入力実績(集計条件：ACTUALQTY>0)";
                ws.Cells[4, 7].Style.Font.Color.SetColor(Color.DarkSeaGreen);
                ws.Cells[4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[4, 7].Style.Font.Size = 10;
                //cTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //cTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[5, 23].Value = "ACT_ST = ACTUALTIME*DIRECTWORKER/ACTUALQTY*0.85";
                ws.Cells[5, 23].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[5, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[5, 23].Style.Font.Size = 6;
                var dTableRange = ws.Cells[5, 1, 5, 23];
                dTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //dTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //dTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A6"].LoadFromDataTable(mydt, true);
                ws.Cells[6, 1, 6, 23].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //pck.Save();
                ws.View.FreezePanes(7, 24);
                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        //修理集计表 Manufacturing Report
        public static void Manufacturing_XlsxFile(DataTable mydt, string mybname, string myfname, string update)
        {
            // If you are a commercial business and have
            // purchased commercial licenses use the static property
            // LicenseContext of the ExcelPackage class :
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorkbook wb = pck.Workbook;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(mybname);
                //配置文件属性
                wb.Properties.Category = "DTA Repair Details Report";
                wb.Properties.Author = "Davis.Ching";
                wb.Properties.Comments = "Lean365 Inc.";
                wb.Properties.Company = "DTA";
                wb.Properties.Keywords = "Defect;Report";
                wb.Properties.Manager = "Davis.Ching";
                wb.Properties.Status = "Normal";
                wb.Properties.Subject = "Lean Manufacturing";
                wb.Properties.Title = "DTA Repair Details Report";
                wb.Properties.LastModifiedBy = "Davis.Ching";

                //赋值单元格
                ws.Cells[1, 2].Value = "DTA Repair Details Report";
                ws.Cells[1, 2].Style.Font.Size = 12;//字体大小
                ws.Cells[1, 1].Style.Font.Bold = true;//字体为粗体
                ws.Cells[1, 7].Value = "1時間当たりの生産量";
                ws.Cells[1, 7].Style.Font.Size = 8;
                //ws.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                var aTableRange = ws.Cells[1, 1, 1, 18];
                aTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                aTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //aTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[2, 1].Value = "Date";
                ws.Cells[2, 2].Value = update;
                ws.Cells[2, 2].Style.Font.Color.SetColor(Color.Red); //字体颜色
                ws.Cells[2, 9].Value = "Company:";
                ws.Cells[2, 10].Value = "DongGuan TEAC Electronics Co.,Ltd";
                ws.Cells[2, 17].Value = "稼働率:";
                ws.Cells[2, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 17].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 17].Style.Font.Size = 10;
                ws.Cells[2, 18].Value = "85.00%";
                ws.Cells[2, 18].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[2, 18].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));//设置单元格背景色
                ws.Cells[2, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[2, 18].Style.Font.Size = 10;
                //ws.Cells[2, 10, 2, 11].Merge = true;//合并单元格
                var bTableRange = ws.Cells[2, 1, 2, 18];
                bTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //bTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[3, 18].Value = "Program Designer : DTA EDP Davis.Ching " + update.Substring(0, 4);
                ws.Cells[3, 18].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[3, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[3, 18].Style.Font.Size = 6;
                var cTableRange = ws.Cells[3, 1, 3, 18];
                cTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells[4, 9].Value = "OPH入力実績(集計条件：ACTUALQTY>0)";
                ws.Cells[4, 9].Style.Font.Color.SetColor(Color.DarkSeaGreen);
                ws.Cells[4, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[4, 9].Style.Font.Size = 10;
                //cTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //cTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[5, 18].Value = "ACT_ST = ACTUALTIME*DIRECTWORKER/ACTUALQTY*0.85";
                ws.Cells[5, 18].Style.Font.Color.SetColor(Color.DarkGray);
                ws.Cells[5, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[5, 18].Style.Font.Size = 6;
                var dTableRange = ws.Cells[5, 1, 5, 18];
                dTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //dTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //dTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A6"].LoadFromDataTable(mydt, true);
                ws.Cells[6, 1, 6, 18].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                //pck.Save();
                ws.View.FreezePanes(7, 19);
                //Example how to Format Column 1 as numeric
                //using (ExcelRange col = ws.Cells[2, 3, 2 + mydt.Rows.Count, 3])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //integer (not really needed unless you need to round numbers, Excel with use default cell properties)
                //ws.Cells["C2:C125"].Style.Numberformat.Format = "0";
                ws.Column(3).Style.Numberformat.Format = "0.00";//设置列宽
                //integer without displaying the number 0 in the cell
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#";

                ////number with 1 decimal place
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.0";

                ////number with 2 decimal places
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0.00";

                ////number with 2 decimal places and thousand separator
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";

                ////number with 2 decimal places and thousand separator and money symbol
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";

                ////percentage (1 = 100%, 0.01 = 1%)
                //ws.Cells["A1:A25"].Style.Numberformat.Format = "0%";
                //foreach (var dc in dateColumns)
                //{
                //    sheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "###,##%";
                //}
                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                //写到客户端（下载）
                HttpContext.Current.Response.Clear();
                //asp.net输出的Excel文件名
                //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
                string fileName = HttpUtility.UrlEncode(myfname);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                //ep.SaveAs(Response.OutputStream);    第二种方式
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 根据数据类型设置不同类型的cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="obj"></param>
        private static string GetValue(string cellValue, string type)
        {
            object value = string.Empty;
            switch (type)
            {
                case "System.String"://字符串类型
                    value = cellValue;
                    break;

                case "System.DateTime"://日期类型
                    System.DateTime dateV;
                    System.DateTime.TryParse(cellValue, out dateV);
                    value = dateV;
                    break;

                case "System.Boolean"://布尔型
                    bool boolV = false;
                    bool.TryParse(cellValue, out boolV);
                    value = boolV;
                    break;

                case "System.Int16"://整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV = 0;
                    int.TryParse(cellValue, out intV);
                    value = intV;
                    break;

                case "System.Decimal"://浮点型
                case "System.Double":
                    double doubV = 0;
                    double.TryParse(cellValue, out doubV);
                    value = doubV;
                    break;

                case "System.DBNull"://空值处理
                    value = string.Empty;
                    break;

                default:
                    value = string.Empty;
                    break;
            }
            return value.ToString();
        }

        /// <summary>
        /// 将多个DataTable数据导入到excel中
        /// </summary>
        /// <param name="dts">要导入的数据集合</param>
        /// <param name="strExcelFileName">定义Excel文件名</param>
        /// <param name="indexType">给个 1 就行</param>
        public static bool TableListToExcels(List<DataTable> dts, DataTable main, string strExcelFileName, int indexType)
        {
            bool fs = false;

            XSSFWorkbook workbook = new XSSFWorkbook();
            //Create Excel的属性中的来源以及说明等
            POIXMLProperties props = workbook.GetProperties();
            props.CoreProperties.Creator = "Davis.Ching";
            props.CoreProperties.Created = DateTime.Now;
            props.CoreProperties.Category = "生产管理";
            if (!props.CustomProperties.Contains("Lean365"))
                props.CustomProperties.AddProperty("Lean365", "Lean365");

            props.CoreProperties.Modified = DateTime.Now;

            props.CoreProperties.Keywords = "Lean365";

            props.CoreProperties.Subject = "生产不良集计";
            props.CoreProperties.Title = "DTA精益生产系统";
            props.CoreProperties.LastModifiedByUser = "Davis.Ching";

            DataSet set = new DataSet();
            foreach (DataTable dt in dts)
            {
                //Sheet名称
                //.Replace("[", "(").Replace("]", "）").Replace("/", "&").Replace("?", "_").Replace("*", "_").Replace(":", "_").Replace("\\", "&")
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet(dt.TableName.Replace("[", "(").Replace("]", "）").Replace("/", "&").Replace("?", "_").Replace("*", "_").Replace(":", "_").Replace("\\", "&"));
                //sheet.DefaultColumnWidth = 100 * 256;
                //sheet.DefaultRowHeight = 30 * 20;

                sheet.PrintSetup.PaperSize = 9;

                //sheet.SetMargin(MarginType.LeftMargin, (double)0.5);
                //sheet.SetMargin(MarginType.RightMargin, (double)0.5);
                //sheet.SetMargin(MarginType.BottomMargin, (double)0.5);
                //sheet.SetMargin(MarginType.TopMargin, (double)0.5);
                //sheet.FitToPage = false;
                //默认格式，边框
                XSSFCellStyle DefStyles = (XSSFCellStyle)workbook.CreateCellStyle();
                DefStyles.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                DefStyles.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                DefStyles.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                DefStyles.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //下边框没有
                XSSFCellStyle BotStyles = (XSSFCellStyle)workbook.CreateCellStyle();
                BotStyles.BorderBottom = NPOI.SS.UserModel.BorderStyle.Hair;
                BotStyles.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                BotStyles.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                BotStyles.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                //表内容样式
                XSSFCellStyle ContStyles = (XSSFCellStyle)workbook.CreateCellStyle();
                ContStyles.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                ContStyles.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                ContStyles.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                ContStyles.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                //字体
                IFont font9 = workbook.CreateFont();
                font9.IsBold = false;
                font9.FontHeightInPoints = 9;
                ContStyles.SetFont(font9);
                //XSSFDataFormat intFormat = workbook.CreateDataFormat() as XSSFDataFormat;

                //ContStyles.DataFormat = intFormat.GetFormat("0");
                //表内数字样式
                XSSFCellStyle IntStyles = (XSSFCellStyle)workbook.CreateCellStyle();
                IntStyles.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                IntStyles.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                IntStyles.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                IntStyles.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                //字体
                IFont intfont9 = workbook.CreateFont();
                intfont9.IsBold = false;
                intfont9.FontHeightInPoints = 9;
                IntStyles.SetFont(intfont9);
                XSSFDataFormat intFormat = workbook.CreateDataFormat() as XSSFDataFormat;

                IntStyles.DataFormat = intFormat.GetFormat("0");

                //标题文字，字体加粗，大小12，居中
                ICellStyle Centerstyle = workbook.CreateCellStyle();
                Centerstyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                //defstyle.BottomBorderColor = IndexedColors.Black.Index;
                Centerstyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                //defstyle.LeftBorderColor = IndexedColors.Green.Index;
                Centerstyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                //defstyle.RightBorderColor = IndexedColors.Blue.Index;
                Centerstyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                Centerstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                Centerstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                IFont font20 = workbook.CreateFont();
                font20.IsBold = false;
                font20.FontHeightInPoints = 18;
                Centerstyle.SetFont(font20);

                //标题文字，字体，大小12，居中
                ICellStyle Rightstyles = workbook.CreateCellStyle();
                Rightstyles.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                //defstyle.BottomBorderColor = IndexedColors.Black.Index;
                Rightstyles.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                //defstyle.LeftBorderColor = IndexedColors.Green.Index;
                Rightstyles.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                //defstyle.RightBorderColor = IndexedColors.Blue.Index;
                Rightstyles.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                Rightstyles.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                Rightstyles.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //字体
                IFont fontCont9 = workbook.CreateFont();
                fontCont9.IsBold = false;
                fontCont9.FontHeightInPoints = 9;
                Rightstyles.SetFont(fontCont9);
                //string sorder = dt.TableName.Substring(0, dt.TableName.Length - 2).Substring(dt.TableName.Substring(0, dt.TableName.Length - 2).Length-6, 6);
                //string slot = dt.TableName.Substring(0, dt.TableName.Length - 2).Substring(0,dt.TableName.Substring(0, dt.TableName.Length - 2).Length - 6);
                var qs = from g in main.AsEnumerable()
                         select g;
                //判断字符的位置并截取完整的生产批次
                string llot = dt.TableName.Substring(0, dt.TableName.IndexOf("_"));
                var q =
                    (from g in main.AsEnumerable()
                     where g.Field<string>("Prolot") == llot
                     //where g.Field<string>("Proorder") == sorder
                     select g).ToList();

                if (q.Any())
                {
                    int rowCout = dt.Rows.Count;
                    int PageCount = (int)Math.Ceiling(rowCout / 47.00);

                    if (PageCount == 0)
                    {
                        //创建表格为58行，6列
                        for (int i = 0; i < 53; i++)
                        {
                            XSSFRow row = (XSSFRow)sheet.CreateRow(i);
                            for (int j = 0; j < 10; j++)
                            {
                                XSSFCell cell = (XSSFCell)sheet.GetRow(i).CreateCell(j);
                                cell.CellStyle = DefStyles;
                            }
                            sheet.GetRow(i).Height = 16 * 20;
                        }

                        //处理表头
                        for (int i = 0; i < 2; i++)//i表示第一行的列数
                        {
                            sheet.GetRow(0).Height = 20 * 20;
                            sheet.GetRow(1).Height = 20 * 20;
                            sheet.GetRow(2).Height = 16 * 20;
                            sheet.GetRow(3).Height = 16 * 20;
                            sheet.GetRow(4).Height = 16 * 20;
                            sheet.GetRow(5).Height = 16 * 20;
                            sheet.GetRow(48).Height = 20 * 20;
                            sheet.GetRow(49).Height = 16 * 20;
                            sheet.GetRow(50).Height = 16 * 20;
                            sheet.GetRow(51).Height = 16 * 20;
                            sheet.GetRow(52).Height = 16 * 20;

                            if (i == 1)//将第一行第一列、第二行第一列合并
                            {
                                //插入图片
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, 1));
                                XSSFCell cell = (XSSFCell)sheet.GetRow(0).GetCell(1);
                                HttpRuntime.AppDomainAppPath.ToString();

                                IDrawing patriarch = sheet.CreateDrawingPatriarch();
                                //create the anchor
                                XSSFClientAnchor anchor = new XSSFClientAnchor(500, 200, 0, 0, 0, 0, 1, 1);
                                anchor.AnchorType = AnchorType.MoveDontResize;
                                //load the picture and get the picture index in the workbook
                                //first picture
                                int imageId = LoadImage(HttpRuntime.AppDomainAppPath.ToString() + "/Lf_Resources/images/Flogo.png", workbook);
                                XSSFPicture picture = (XSSFPicture)patriarch.CreatePicture(anchor, imageId);
                                //Reset the image to the original size.
                                picture.Resize();   //Note: Resize will reset client anchor you set.
                                picture.LineStyle = LineStyle.DashDotGel;

                                //将图片文件读入一个字符串
                                //byte[] bytes = File.ReadAllBytes(HttpRuntime.AppDomainAppPath.ToString() + "/Lf_Resources/images/Flogo.png");
                                //int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);

                                //NPOI.SS.UserModel.IDrawing patriarch = sheet.CreateDrawingPatriarch();

                                //// 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
                                //XSSFClientAnchor anchor = new XSSFClientAnchor(1023, 0, 1023, 0, 0, 0, 1, 1);
                                ////把图片插到相应的位置
                                //XSSFPicture pict = (XSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                                //pict.Resize();
                                cell.CellStyle = Centerstyle;

                                //合并第一行第三列和第四列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 2, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row1cell3 = (XSSFCell)sheet.GetRow(0).GetCell(2);
                                row1cell3.SetCellValue("文书名");
                                row1cell3.CellStyle = BotStyles;

                                //合并第二行第三列和第四列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row2cell3 = (XSSFCell)sheet.GetRow(1).GetCell(2);
                                row2cell3.SetCellValue("不良集计");
                                row2cell3.CellStyle = Centerstyle;

                                //合并第一行第五列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 8, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row1cell5 = (XSSFCell)sheet.GetRow(0).GetCell(8);
                                row1cell5.SetCellValue("发行元：DTA制一课");
                                row1cell5.CellStyle = Rightstyles;

                                //合并第二行第五列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 8, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row2cell5 = (XSSFCell)sheet.GetRow(1).GetCell(8);
                                row2cell5.SetCellValue("发行日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                                row2cell5.CellStyle = Rightstyles;

                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(48, 52, 0, 6));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell0 = (XSSFCell)sheet.GetRow(48).GetCell(0);
                                //row50cell5.SetCellValue("承认");
                                row50cell0.CellStyle = Rightstyles;

                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(49, 52, 7, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell5 = (XSSFCell)sheet.GetRow(48).GetCell(7);
                                row50cell5.SetCellValue("承  认");
                                row50cell5.CellStyle = Rightstyles;

                                //合并第50行第一列和第三列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(49, 52, 8, 8));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell6 = (XSSFCell)sheet.GetRow(48).GetCell(8);
                                row50cell6.SetCellValue("确  认");
                                row50cell6.CellStyle = Rightstyles;
                                //合并第50行第四列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(49, 52, 9, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                //XSSFTextbox txt = item as XSSFTextbox;

                                XSSFCell row50cell7 = (XSSFCell)sheet.GetRow(48).GetCell(9);
                                row50cell7.SetCellValue("担  当");
                                row50cell7.CellStyle = Rightstyles;
                                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(50, 52, 9, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                //XSSFTextbox txt = item as XSSFTextbox;

                                //XSSFCell row50cell8 = (XSSFCell)sheet.GetRow(49).GetCell(9);
                                //row50cell8.SetCellValue("页码");
                                //row50cell8.CellStyle = DefStyles;
                            }
                        }
                        for (int f = 5; f < 48; f++)
                        {
                            //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 1, 2));
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 4, 8));
                            //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 1, 2));
                        }
                        sheet.GetRow(2).GetCell(0).SetCellValue("机种");
                        //合并2,3
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 1, 2));
                        sheet.GetRow(2).GetCell(1).SetCellValue(q[0].Field<string>("Promodel"));

                        //sheet.CreateRow(1).CreateCell(0).SetCellValue("机种");//创建第一行/创建第一单元格/设置第一单元格的内容[可以分开创建，但必须先创建行才能创建单元格不然报错]
                        //sheet.GetRow(1).CreateCell(1).SetCellValue(strmodel);//获取第一行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(3).GetCell(0).SetCellValue("批次");//创建第二行/创建第一单元格/设置第一单元格的内容
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 1, 2));
                        sheet.GetRow(3).GetCell(1).SetCellValue(q[0].Field<string>("Prolot"));//获取第二行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(4).GetCell(0).SetCellValue("期间");//创建第三行/创建第一单元格/设置第一单元格的内容
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 1, 2));
                        sheet.GetRow(4).GetCell(1).SetCellValue(q[0].Field<string>("Prodate"));//获取第三行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(2).GetCell(3).SetCellValue("班组");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                        sheet.GetRow(2).GetCell(4).SetCellValue(q[0].Field<string>("Prolinename"));
                        sheet.GetRow(3).GetCell(3).SetCellValue("直行率");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 4, 5));
                        sheet.GetRow(3).GetCell(4).SetCellValue(q[0].Field<string>("Prodirectrate"));

                        sheet.GetRow(4).GetCell(3).SetCellValue("不良率");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 4, 5));
                        sheet.GetRow(4).GetCell(4).SetCellValue(q[0].Field<string>("Probadrate"));
                        //sheet.GetRow(3).GetCell(3).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 6, 8));
                        sheet.GetRow(2).GetCell(6).SetCellValue("生产数量");

                        sheet.GetRow(2).GetCell(9).SetCellValue(q[0].Field<Int32>("Prorealqty"));
                        //sheet.GetRow(1).GetCell(5).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 6, 8));
                        sheet.GetRow(3).GetCell(6).SetCellValue("无不良台数");

                        sheet.GetRow(3).GetCell(9).SetCellValue(q[0].Field<Int32>("Pronobadqty"));
                        //sheet.GetRow(2).GetCell(5).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 6, 8));
                        sheet.GetRow(4).GetCell(6).SetCellValue("不良总件数");

                        sheet.GetRow(4).GetCell(9).SetCellValue(q[0].Field<Int32>("Probadtotal"));
                        //sheet.GetRow(3).GetCell(5).SetCellType(CellType.Numeric);
                        //sheet.Header.Left = "TEAC";
                        //sheet.Header.Right = "DTA-04-Z038-B";
                        ////sheet.Header.Center = "不良集计";
                        sheet.Footer.Center = "页码：&P / &N";//&P当前页码&N总页码数
                        //sheet.Footer.Left = "配布:品管课";
                        //sheet.Footer.Center = "承认：            确认：            担当：            ";
                        sheet.Footer.Right = "DTA-04-Z038-C";
                        //用column name 作为列名
                        int icolIndex = 0;

                        sheet.GetRow(5).GetCell(0).SetCellValue("区分");
                        sheet.GetRow(5).GetCell(1).SetCellValue("不良症状");
                        sheet.GetRow(5).GetCell(3).SetCellValue("不良个所");
                        sheet.GetRow(5).GetCell(4).SetCellValue("不良原因");
                        sheet.GetRow(5).GetCell(9).SetCellValue("件数");

                        XSSFRow headerRow = (XSSFRow)sheet.GetRow(5);
                        foreach (DataColumn item in dt.Columns)
                        {
                            switch (icolIndex)
                            {
                                case 0:
                                    XSSFCell cellA = (XSSFCell)headerRow.GetCell(icolIndex + 0);
                                    cellA.SetCellValue(item.ColumnName);
                                    cellA.CellStyle = DefStyles;
                                    break;

                                case 1:
                                    XSSFCell cellB = (XSSFCell)headerRow.GetCell(icolIndex + 0);
                                    cellB.SetCellValue(item.ColumnName);
                                    cellB.CellStyle = DefStyles;
                                    break;

                                case 2:
                                    XSSFCell cellC = (XSSFCell)headerRow.GetCell(icolIndex + 1);
                                    cellC.SetCellValue(item.ColumnName);
                                    cellC.CellStyle = DefStyles;
                                    break;

                                case 3:
                                    XSSFCell cellD = (XSSFCell)headerRow.GetCell(icolIndex + 1);
                                    cellD.SetCellValue(item.ColumnName);
                                    cellD.CellStyle = DefStyles;
                                    break;

                                case 4:
                                    XSSFCell cellE = (XSSFCell)headerRow.GetCell(icolIndex + 5);
                                    cellE.SetCellValue(item.ColumnName);
                                    cellE.CellStyle = DefStyles;
                                    break;
                                    //case 5:
                                    //    XSSFCell cellF = (XSSFCell)headerRow.GetCell(icolIndex + 4);
                                    //    cellF.SetCellValue(item.ColumnName);
                                    //    cellF.CellStyle = DefStyles;
                                    //    break;
                            }

                            icolIndex++;
                        }
                        if (indexType == 6)
                        {
                            //建立内容行
                            int iRowIndex = 6;
                            int iCellIndex = 0;
                            foreach (DataRow Rowitem in dt.Rows)
                            {
                                if (iRowIndex < 49)
                                {
                                    XSSFRow DataRow = (XSSFRow)sheet.GetRow(iRowIndex);
                                    foreach (DataColumn Colitem in dt.Columns)
                                    {
                                        switch (iCellIndex)
                                        {
                                            case 0:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellA = (XSSFCell)DataRow.GetCell(iCellIndex + 0);
                                                string typeA = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellA.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeA));

                                                cellA.CellStyle = ContStyles;
                                                break;

                                            case 1:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellB = (XSSFCell)DataRow.GetCell(iCellIndex + 0);
                                                string typeB = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellB.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeB));
                                                cellB.CellStyle = ContStyles;
                                                break;

                                            case 2:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellC = (XSSFCell)DataRow.GetCell(iCellIndex + 1);
                                                string typeC = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellC.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeC));
                                                cellC.CellStyle = ContStyles;
                                                break;

                                            case 3:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellD = (XSSFCell)DataRow.GetCell(iCellIndex + 1);
                                                string typeD = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellD.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeD));
                                                cellD.CellStyle = ContStyles;
                                                break;

                                            case 4:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellE = (XSSFCell)DataRow.GetCell(iCellIndex + 5);
                                                string typeE = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellE.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeE));
                                                cellE.CellStyle = IntStyles;
                                                break;
                                                //case 5:
                                                //    //从第二列开始
                                                //    //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                //    XSSFCell cellF = (XSSFCell)DataRow.GetCell(iCellIndex + 4);
                                                //    string typeF = Rowitem[Colitem].GetType().FullName.ToString();
                                                //    cellF.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeF));
                                                //    cellF.CellStyle = ContStyles;
                                                //    break;
                                        }

                                        iCellIndex++;
                                    }
                                    iCellIndex = 0;
                                    iRowIndex++;
                                }
                            }
                        }

                        //自适应列宽
                        //AutoColumnWidth(sheet);
                        //sheet.trackAllColumnsForAutoSizing();
                        for (int i = 0; i < icolIndex; i++)
                        {
                            // 调整每一列宽度
                            sheet.AutoSizeColumn(i);
                            // 解决自动设置列宽中文失效的问题
                            //sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) * 17 / 10);
                        }
                    }
                    if (PageCount == 1)
                    {
                        //创建表格为58行，6列
                        for (int i = 0; i < 53; i++)
                        {
                            XSSFRow row = (XSSFRow)sheet.CreateRow(i);
                            for (int j = 0; j < 10; j++)
                            {
                                XSSFCell cell = (XSSFCell)sheet.GetRow(i).CreateCell(j);
                                cell.CellStyle = DefStyles;
                            }
                            sheet.GetRow(i).Height = 16 * 20;
                        }

                        //处理表头
                        for (int i = 0; i < 2; i++)//i表示第一行的列数
                        {
                            sheet.GetRow(0).Height = 20 * 20;
                            sheet.GetRow(1).Height = 20 * 20;
                            sheet.GetRow(2).Height = 16 * 20;
                            sheet.GetRow(3).Height = 16 * 20;
                            sheet.GetRow(4).Height = 16 * 20;
                            sheet.GetRow(5).Height = 16 * 20;
                            sheet.GetRow(48).Height = 20 * 20;
                            sheet.GetRow(49).Height = 16 * 20;
                            sheet.GetRow(50).Height = 16 * 20;
                            sheet.GetRow(51).Height = 16 * 20;
                            sheet.GetRow(52).Height = 16 * 20;

                            if (i == 1)//将第一行第一列、第二行第一列合并
                            {
                                //插入图片
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, 1));
                                XSSFCell cell = (XSSFCell)sheet.GetRow(0).GetCell(1);
                                HttpRuntime.AppDomainAppPath.ToString();

                                IDrawing patriarch = sheet.CreateDrawingPatriarch();
                                //create the anchor
                                XSSFClientAnchor anchor = new XSSFClientAnchor(500, 200, 0, 0, 0, 0, 1, 1);
                                anchor.AnchorType = AnchorType.MoveDontResize;
                                //load the picture and get the picture index in the workbook
                                //first picture
                                int imageId = LoadImage(HttpRuntime.AppDomainAppPath.ToString() + "/Lf_Resources/images/Flogo.png", workbook);
                                XSSFPicture picture = (XSSFPicture)patriarch.CreatePicture(anchor, imageId);
                                //Reset the image to the original size.
                                picture.Resize();   //Note: Resize will reset client anchor you set.
                                picture.LineStyle = LineStyle.DashDotGel;

                                //将图片文件读入一个字符串
                                //byte[] bytes = File.ReadAllBytes(HttpRuntime.AppDomainAppPath.ToString() + "/Lf_Resources/images/Flogo.png");
                                //int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);

                                //NPOI.SS.UserModel.IDrawing patriarch = sheet.CreateDrawingPatriarch();

                                //// 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
                                //XSSFClientAnchor anchor = new XSSFClientAnchor(1023, 0, 1023, 0, 0, 0, 1, 1);
                                ////把图片插到相应的位置
                                //XSSFPicture pict = (XSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                                //pict.Resize();
                                cell.CellStyle = Centerstyle;

                                //合并第一行第三列和第四列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 2, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row1cell3 = (XSSFCell)sheet.GetRow(0).GetCell(2);
                                row1cell3.SetCellValue("文书名");
                                row1cell3.CellStyle = BotStyles;

                                //合并第二行第三列和第四列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row2cell3 = (XSSFCell)sheet.GetRow(1).GetCell(2);
                                row2cell3.SetCellValue("不良集计");
                                row2cell3.CellStyle = Centerstyle;

                                //合并第一行第五列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 8, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row1cell5 = (XSSFCell)sheet.GetRow(0).GetCell(8);
                                row1cell5.SetCellValue("发行元：DTA制一课");
                                row1cell5.CellStyle = Rightstyles;

                                //合并第二行第五列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 8, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row2cell5 = (XSSFCell)sheet.GetRow(1).GetCell(8);
                                row2cell5.SetCellValue("发行日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                                row2cell5.CellStyle = Rightstyles;

                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(48, 52, 0, 6));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell0 = (XSSFCell)sheet.GetRow(48).GetCell(0);
                                //row50cell5.SetCellValue("承认");
                                row50cell0.CellStyle = Rightstyles;

                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(49, 52, 7, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell5 = (XSSFCell)sheet.GetRow(48).GetCell(7);
                                row50cell5.SetCellValue("承  认");
                                row50cell5.CellStyle = Rightstyles;

                                //合并第50行第一列和第三列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(49, 52, 8, 8));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell6 = (XSSFCell)sheet.GetRow(48).GetCell(8);
                                row50cell6.SetCellValue("确  认");
                                row50cell6.CellStyle = Rightstyles;
                                //合并第50行第四列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(49, 52, 9, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                //XSSFTextbox txt = item as XSSFTextbox;

                                XSSFCell row50cell7 = (XSSFCell)sheet.GetRow(48).GetCell(9);
                                row50cell7.SetCellValue("担  当");
                                row50cell7.CellStyle = Rightstyles;
                                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(50, 52, 9, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                //XSSFTextbox txt = item as XSSFTextbox;

                                //XSSFCell row50cell8 = (XSSFCell)sheet.GetRow(49).GetCell(9);
                                //row50cell8.SetCellValue("页码");
                                //row50cell8.CellStyle = DefStyles;
                            }
                        }
                        for (int f = 5; f < 48; f++)
                        {
                            //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 1, 2));
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 4, 8));
                            //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 1, 2));
                        }
                        sheet.GetRow(2).GetCell(0).SetCellValue("机种");
                        //合并2,3
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 1, 2));
                        sheet.GetRow(2).GetCell(1).SetCellValue(q[0].Field<string>("Promodel"));

                        //sheet.CreateRow(1).CreateCell(0).SetCellValue("机种");//创建第一行/创建第一单元格/设置第一单元格的内容[可以分开创建，但必须先创建行才能创建单元格不然报错]
                        //sheet.GetRow(1).CreateCell(1).SetCellValue(strmodel);//获取第一行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(3).GetCell(0).SetCellValue("批次");//创建第二行/创建第一单元格/设置第一单元格的内容
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 1, 2));
                        sheet.GetRow(3).GetCell(1).SetCellValue(q[0].Field<string>("Prolot"));//获取第二行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(4).GetCell(0).SetCellValue("期间");//创建第三行/创建第一单元格/设置第一单元格的内容
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 1, 2));
                        sheet.GetRow(4).GetCell(1).SetCellValue(q[0].Field<string>("Prodate"));//获取第三行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(2).GetCell(3).SetCellValue("班组");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                        sheet.GetRow(2).GetCell(4).SetCellValue(q[0].Field<string>("Prolinename"));
                        sheet.GetRow(3).GetCell(3).SetCellValue("直行率");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 4, 5));
                        sheet.GetRow(3).GetCell(4).SetCellValue(q[0].Field<string>("Prodirectrate"));

                        sheet.GetRow(4).GetCell(3).SetCellValue("不良率");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 4, 5));
                        sheet.GetRow(4).GetCell(4).SetCellValue(q[0].Field<string>("Probadrate"));
                        //sheet.GetRow(3).GetCell(3).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 6, 8));
                        sheet.GetRow(2).GetCell(6).SetCellValue("生产数量");

                        sheet.GetRow(2).GetCell(9).SetCellValue(q[0].Field<Int32>("Prorealqty"));
                        //sheet.GetRow(1).GetCell(5).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 6, 8));
                        sheet.GetRow(3).GetCell(6).SetCellValue("无不良台数");

                        sheet.GetRow(3).GetCell(9).SetCellValue(q[0].Field<Int32>("Pronobadqty"));
                        //sheet.GetRow(2).GetCell(5).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 6, 8));
                        sheet.GetRow(4).GetCell(6).SetCellValue("不良总件数");

                        sheet.GetRow(4).GetCell(9).SetCellValue(q[0].Field<Int32>("Probadtotal"));
                        //sheet.GetRow(3).GetCell(5).SetCellType(CellType.Numeric);
                        //sheet.Header.Left = "TEAC";
                        //sheet.Header.Right = "DTA-04-Z038-B";
                        ////sheet.Header.Center = "不良集计";
                        sheet.Footer.Center = "页码：&P / &N";//&P当前页码&N总页码数
                        //sheet.Footer.Left = "配布:品管课";
                        //sheet.Footer.Center = "承认：            确认：            担当：            ";
                        sheet.Footer.Right = "DTA-04-Z038-C";
                        //用column name 作为列名
                        int icolIndex = 0;
                        XSSFRow headerRow = (XSSFRow)sheet.GetRow(5);

                        foreach (DataColumn item in dt.Columns)
                        {
                            switch (icolIndex)
                            {
                                case 0:
                                    XSSFCell cellA = (XSSFCell)headerRow.GetCell(icolIndex + 0);
                                    cellA.SetCellValue(item.ColumnName);
                                    cellA.CellStyle = DefStyles;
                                    break;

                                case 1:
                                    XSSFCell cellB = (XSSFCell)headerRow.GetCell(icolIndex + 0);
                                    cellB.SetCellValue(item.ColumnName);
                                    cellB.CellStyle = DefStyles;
                                    break;

                                case 2:
                                    XSSFCell cellC = (XSSFCell)headerRow.GetCell(icolIndex + 1);
                                    cellC.SetCellValue(item.ColumnName);
                                    cellC.CellStyle = DefStyles;
                                    break;

                                case 3:
                                    XSSFCell cellD = (XSSFCell)headerRow.GetCell(icolIndex + 1);
                                    cellD.SetCellValue(item.ColumnName);
                                    cellD.CellStyle = DefStyles;
                                    break;

                                case 4:
                                    XSSFCell cellE = (XSSFCell)headerRow.GetCell(icolIndex + 5);
                                    cellE.SetCellValue(item.ColumnName);
                                    cellE.CellStyle = DefStyles;
                                    break;
                                    //case 5:
                                    //    XSSFCell cellF = (XSSFCell)headerRow.GetCell(icolIndex + 4);
                                    //    cellF.SetCellValue(item.ColumnName);
                                    //    cellF.CellStyle = DefStyles;
                                    //    break;
                            }

                            icolIndex++;
                        }
                        if (indexType == 6)
                        {
                            //建立内容行
                            int iRowIndex = 6;
                            int iCellIndex = 0;
                            foreach (DataRow Rowitem in dt.Rows)
                            {
                                if (iRowIndex < 49)
                                {
                                    XSSFRow DataRow = (XSSFRow)sheet.GetRow(iRowIndex);
                                    foreach (DataColumn Colitem in dt.Columns)
                                    {
                                        switch (iCellIndex)
                                        {
                                            case 0:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellA = (XSSFCell)DataRow.GetCell(iCellIndex + 0);
                                                string typeA = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellA.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeA));

                                                cellA.CellStyle = ContStyles;
                                                break;

                                            case 1:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellB = (XSSFCell)DataRow.GetCell(iCellIndex + 0);
                                                string typeB = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellB.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeB));
                                                cellB.CellStyle = ContStyles;
                                                break;

                                            case 2:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellC = (XSSFCell)DataRow.GetCell(iCellIndex + 1);
                                                string typeC = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellC.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeC));
                                                cellC.CellStyle = ContStyles;
                                                break;

                                            case 3:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellD = (XSSFCell)DataRow.GetCell(iCellIndex + 1);
                                                string typeD = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellD.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeD));
                                                cellD.CellStyle = ContStyles;
                                                break;

                                            case 4:
                                                //从第二列开始
                                                //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                XSSFCell cellE = (XSSFCell)DataRow.GetCell(iCellIndex + 5);
                                                string typeE = Rowitem[Colitem].GetType().FullName.ToString();
                                                cellE.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeE));
                                                cellE.CellStyle = IntStyles;
                                                break;
                                                //case 5:
                                                //    //从第二列开始
                                                //    //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                                //    XSSFCell cellF = (XSSFCell)DataRow.GetCell(iCellIndex + 4);
                                                //    string typeF = Rowitem[Colitem].GetType().FullName.ToString();
                                                //    cellF.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeF));
                                                //    cellF.CellStyle = ContStyles;
                                                //    break;
                                        }

                                        iCellIndex++;
                                    }
                                    iCellIndex = 0;
                                    iRowIndex++;
                                }
                            }
                        }

                        //自适应列宽
                        //AutoColumnWidth(sheet);
                        //sheet.trackAllColumnsForAutoSizing();
                        for (int i = 0; i < icolIndex; i++)
                        {
                            // 调整每一列宽度
                            sheet.AutoSizeColumn(i);
                            // 解决自动设置列宽中文失效的问题
                            //sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) * 17 / 10);
                        }
                    }
                    if (PageCount > 1)
                    {
                        //创建表格为数据行53行，加表头表尾10行，10列
                        for (int i = 0; i < rowCout + 15; i++)
                        {
                            XSSFRow row = (XSSFRow)sheet.CreateRow(i);
                            for (int j = 0; j < 10; j++)
                            {
                                XSSFCell cell = (XSSFCell)sheet.GetRow(i).CreateCell(j);
                                cell.CellStyle = DefStyles;
                            }
                            sheet.GetRow(i).Height = 16 * 20;
                        }

                        //处理表头
                        for (int i = 0; i < 2; i++)//i表示第一行的列数
                        {
                            //表头
                            sheet.GetRow(0).Height = 20 * 20;
                            sheet.GetRow(1).Height = 20 * 20;
                            sheet.GetRow(2).Height = 16 * 20;
                            sheet.GetRow(3).Height = 16 * 20;
                            sheet.GetRow(4).Height = 16 * 20;
                            sheet.GetRow(5).Height = 16 * 20;
                            //表尾
                            sheet.GetRow(rowCout + 15 - 5).Height = 20 * 20;
                            sheet.GetRow(rowCout + 15 - 4).Height = 16 * 20;
                            sheet.GetRow(rowCout + 15 - 3).Height = 16 * 20;
                            sheet.GetRow(rowCout + 15 - 2).Height = 16 * 20;
                            sheet.GetRow(rowCout + 15 - 1).Height = 16 * 20;

                            if (i == 1)//将第一行第一列、第二行第一列合并
                            {
                                //插入图片
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, 1));
                                XSSFCell cell = (XSSFCell)sheet.GetRow(0).GetCell(1);
                                HttpRuntime.AppDomainAppPath.ToString();

                                IDrawing patriarch = sheet.CreateDrawingPatriarch();
                                //create the anchor
                                XSSFClientAnchor anchor = new XSSFClientAnchor(500, 200, 0, 0, 0, 0, 1, 1);
                                anchor.AnchorType = AnchorType.MoveDontResize;
                                //load the picture and get the picture index in the workbook
                                //first picture
                                int imageId = LoadImage(HttpRuntime.AppDomainAppPath.ToString() + "/Lf_Resources/images/Flogo.png", workbook);
                                XSSFPicture picture = (XSSFPicture)patriarch.CreatePicture(anchor, imageId);
                                //Reset the image to the original size.
                                picture.Resize();   //Note: Resize will reset client anchor you set.
                                picture.LineStyle = LineStyle.DashDotGel;

                                //将图片文件读入一个字符串
                                //byte[] bytes = File.ReadAllBytes(HttpRuntime.AppDomainAppPath.ToString() + "/Lf_Resources/images/Flogo.png");
                                //int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);

                                //NPOI.SS.UserModel.IDrawing patriarch = sheet.CreateDrawingPatriarch();

                                //// 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
                                //XSSFClientAnchor anchor = new XSSFClientAnchor(1023, 0, 1023, 0, 0, 0, 1, 1);
                                ////把图片插到相应的位置
                                //XSSFPicture pict = (XSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                                //pict.Resize();
                                cell.CellStyle = Centerstyle;

                                //合并第一行第三列和第四列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 2, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row1cell3 = (XSSFCell)sheet.GetRow(0).GetCell(2);
                                row1cell3.SetCellValue("文书名");
                                row1cell3.CellStyle = BotStyles;

                                //合并第二行第三列和第四列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row2cell3 = (XSSFCell)sheet.GetRow(1).GetCell(2);
                                row2cell3.SetCellValue("不良集计");
                                row2cell3.CellStyle = Centerstyle;

                                //合并第一行第五列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 8, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row1cell5 = (XSSFCell)sheet.GetRow(0).GetCell(8);
                                row1cell5.SetCellValue("发行元：DTA制一课");
                                row1cell5.CellStyle = Rightstyles;

                                //合并第二行第五列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 8, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row2cell5 = (XSSFCell)sheet.GetRow(1).GetCell(8);
                                row2cell5.SetCellValue("发行日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                                row2cell5.CellStyle = Rightstyles;

                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowCout + 15 - 5, rowCout + 15 - 1, 0, 6));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell0 = (XSSFCell)sheet.GetRow(rowCout + 15 - 5).GetCell(0);
                                //row50cell5.SetCellValue("承认");
                                row50cell0.CellStyle = Rightstyles;

                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowCout + 15 - 4, rowCout + 15 - 1, 7, 7));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell5 = (XSSFCell)sheet.GetRow(rowCout + 15 - 5).GetCell(7);
                                row50cell5.SetCellValue("承  认");
                                row50cell5.CellStyle = Rightstyles;

                                //合并第50行第一列和第三列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowCout + 15 - 4, rowCout + 15 - 1, 8, 8));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                XSSFCell row50cell6 = (XSSFCell)sheet.GetRow(rowCout + 15 - 5).GetCell(8);
                                row50cell6.SetCellValue("确  认");
                                row50cell6.CellStyle = Rightstyles;
                                //合并第50行第四列和第六列
                                //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowCout + 15 - 4, rowCout + 15 - 1, 9, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                //XSSFTextbox txt = item as XSSFTextbox;

                                XSSFCell row50cell7 = (XSSFCell)sheet.GetRow(rowCout + 15 - 5).GetCell(9);
                                row50cell7.SetCellValue("担  当");
                                row50cell7.CellStyle = Rightstyles;
                                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(50, 52, 9, 9));
                                //sheet.GetRow(0).CreateCell(0).SetCellValue("不良集计");
                                //XSSFTextbox txt = item as XSSFTextbox;

                                //XSSFCell row50cell8 = (XSSFCell)sheet.GetRow(49).GetCell(9);
                                //row50cell8.SetCellValue("页码");
                                //row50cell8.CellStyle = DefStyles;
                            }
                        }
                        for (int f = 5; f < rowCout + 10; f++)
                        {
                            //CellRangeAddress有4个参数：起始行号，终止行号， 起始列号，终止列号
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 1, 2));
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 4, 8));
                            //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(f, f, 1, 2));
                        }
                        sheet.GetRow(2).GetCell(0).SetCellValue("机种");
                        //合并2,3
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 1, 2));
                        sheet.GetRow(2).GetCell(1).SetCellValue(q[0].Field<string>("Promodel"));

                        //sheet.CreateRow(1).CreateCell(0).SetCellValue("机种");//创建第一行/创建第一单元格/设置第一单元格的内容[可以分开创建，但必须先创建行才能创建单元格不然报错]
                        //sheet.GetRow(1).CreateCell(1).SetCellValue(strmodel);//获取第一行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(3).GetCell(0).SetCellValue("批次");//创建第二行/创建第一单元格/设置第一单元格的内容
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 1, 2));
                        sheet.GetRow(3).GetCell(1).SetCellValue(q[0].Field<string>("Prolot"));//获取第二行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(4).GetCell(0).SetCellValue("期间");//创建第三行/创建第一单元格/设置第一单元格的内容
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 1, 2));
                        sheet.GetRow(4).GetCell(1).SetCellValue(q[0].Field<string>("Prodate"));//获取第三行/创建第二单元格/设置第二单元格的内容

                        sheet.GetRow(2).GetCell(3).SetCellValue("班组");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                        sheet.GetRow(2).GetCell(4).SetCellValue(q[0].Field<string>("Prolinename"));
                        sheet.GetRow(3).GetCell(3).SetCellValue("直行率");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 4, 5));
                        sheet.GetRow(3).GetCell(4).SetCellValue(q[0].Field<string>("Prodirectrate"));

                        sheet.GetRow(4).GetCell(3).SetCellValue("不良率");
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 4, 5));
                        sheet.GetRow(4).GetCell(4).SetCellValue(q[0].Field<string>("Probadrate"));
                        //sheet.GetRow(3).GetCell(3).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 6, 8));
                        sheet.GetRow(2).GetCell(6).SetCellValue("生产数量");

                        sheet.GetRow(2).GetCell(9).SetCellValue(q[0].Field<Int32>("Prorealqty"));
                        //sheet.GetRow(1).GetCell(5).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 6, 8));
                        sheet.GetRow(3).GetCell(6).SetCellValue("无不良台数");

                        sheet.GetRow(3).GetCell(9).SetCellValue(q[0].Field<Int32>("Pronobadqty"));
                        //sheet.GetRow(2).GetCell(5).SetCellType(CellType.Numeric);
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 6, 8));
                        sheet.GetRow(4).GetCell(6).SetCellValue("不良总件数");

                        sheet.GetRow(4).GetCell(9).SetCellValue(q[0].Field<Int32>("Probadtotal"));
                        //sheet.GetRow(3).GetCell(5).SetCellType(CellType.Numeric);

                        //sheet.Header.Left = "TEAC";
                        //sheet.Header.Right = "DTA-04-Z038-B";
                        ////sheet.Header.Center = "不良集计";
                        sheet.Footer.Center = "页码：&P / &N";//&P当前页码&N总页码数
                        //sheet.Footer.Left = "配布:品管课";
                        //sheet.Footer.Center = "承认：            确认：            担当：            ";
                        sheet.Footer.Right = "DTA-04-Z038-C";

                        //用column name 作为列名
                        int icolIndex = 0;
                        XSSFRow headerRow = (XSSFRow)sheet.GetRow(5);

                        foreach (DataColumn item in dt.Columns)
                        {
                            switch (icolIndex)
                            {
                                case 0:
                                    XSSFCell cellA = (XSSFCell)headerRow.GetCell(icolIndex + 0);
                                    cellA.SetCellValue(item.ColumnName);
                                    cellA.CellStyle = DefStyles;
                                    break;

                                case 1:
                                    XSSFCell cellB = (XSSFCell)headerRow.GetCell(icolIndex + 0);
                                    cellB.SetCellValue(item.ColumnName);
                                    cellB.CellStyle = DefStyles;
                                    break;

                                case 2:
                                    XSSFCell cellC = (XSSFCell)headerRow.GetCell(icolIndex + 1);
                                    cellC.SetCellValue(item.ColumnName);
                                    cellC.CellStyle = DefStyles;
                                    break;

                                case 3:
                                    XSSFCell cellD = (XSSFCell)headerRow.GetCell(icolIndex + 1);
                                    cellD.SetCellValue(item.ColumnName);
                                    cellD.CellStyle = DefStyles;
                                    break;

                                case 4:
                                    XSSFCell cellE = (XSSFCell)headerRow.GetCell(icolIndex + 5);
                                    cellE.SetCellValue(item.ColumnName);
                                    cellE.CellStyle = DefStyles;
                                    break;
                                    //case 5:
                                    //    XSSFCell cellF = (XSSFCell)headerRow.GetCell(icolIndex + 4);
                                    //    cellF.SetCellValue(item.ColumnName);
                                    //    cellF.CellStyle = DefStyles;
                                    //    break;
                            }

                            icolIndex++;
                        }
                        if (indexType == 6)
                        {
                            //建立内容行
                            int iRowIndex = 6;
                            int iCellIndex = 0;
                            foreach (DataRow Rowitem in dt.Rows)
                            {
                                XSSFRow DataRow = (XSSFRow)sheet.GetRow(iRowIndex);
                                foreach (DataColumn Colitem in dt.Columns)
                                {
                                    switch (iCellIndex)
                                    {
                                        case 0:
                                            //从第二列开始
                                            //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                            XSSFCell cellA = (XSSFCell)DataRow.GetCell(iCellIndex + 0);
                                            string typeA = Rowitem[Colitem].GetType().FullName.ToString();
                                            cellA.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeA));

                                            cellA.CellStyle = ContStyles;
                                            break;

                                        case 1:
                                            //从第二列开始
                                            //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                            XSSFCell cellB = (XSSFCell)DataRow.GetCell(iCellIndex + 0);
                                            string typeB = Rowitem[Colitem].GetType().FullName.ToString();
                                            cellB.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeB));
                                            cellB.CellStyle = ContStyles;
                                            break;

                                        case 2:
                                            //从第二列开始
                                            //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                            XSSFCell cellC = (XSSFCell)DataRow.GetCell(iCellIndex + 1);
                                            string typeC = Rowitem[Colitem].GetType().FullName.ToString();
                                            cellC.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeC));
                                            cellC.CellStyle = ContStyles;
                                            break;

                                        case 3:
                                            //从第二列开始
                                            //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                            XSSFCell cellD = (XSSFCell)DataRow.GetCell(iCellIndex + 1);
                                            string typeD = Rowitem[Colitem].GetType().FullName.ToString();
                                            cellD.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeD));
                                            cellD.CellStyle = ContStyles;
                                            break;

                                        case 4:
                                            //从第二列开始
                                            //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                            XSSFCell cellE = (XSSFCell)DataRow.GetCell(iCellIndex + 5);
                                            string typeE = Rowitem[Colitem].GetType().FullName.ToString();
                                            cellE.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeE));
                                            cellE.CellStyle = IntStyles;
                                            break;
                                            //case 5:
                                            //    //从第二列开始
                                            //    //XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex + 1);
                                            //    XSSFCell cellF = (XSSFCell)DataRow.GetCell(iCellIndex + 4);
                                            //    string typeF = Rowitem[Colitem].GetType().FullName.ToString();
                                            //    cellF.SetCellValue(GetValue(Rowitem[Colitem].ToString(), typeF));
                                            //    cellF.CellStyle = ContStyles;
                                            //    break;
                                    }

                                    iCellIndex++;
                                }
                                iCellIndex = 0;
                                iRowIndex++;
                            }
                        }

                        //自适应列宽
                        //AutoColumnWidth(sheet);
                        //sheet.trackAllColumnsForAutoSizing();
                        for (int i = 0; i < icolIndex; i++)
                        {
                            // 调整每一列宽度
                            sheet.AutoSizeColumn(i);
                            // 解决自动设置列宽中文失效的问题
                            //sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) * 17 / 10);
                        }
                    }
                }

                sheet.RepeatingRows = new CellRangeAddress(0, 5, -1, -1);
                sheet.PrintSetup.Scale = 97;
                sheet.PrintSetup.FitWidth = 1;
                sheet.PrintSetup.FitHeight = 0;
                AddRengionBorder(sheet, workbook, 0, 0, 0, 4);
            }

            //发送到客户端
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            //HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", HttpUtility.UrlEncode("WS" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8)));

            //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + strExcelFileName);
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strExcelFileName);
            //HttpContext.Current.Response.AddHeader("Content-Length", ms.Length.ToString());
            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            HttpContext.Current.Response.ContentType = "application/octet-stream;charset=utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.Flush();

            HttpContext.Current.Response.End();
            workbook = null;
            ms.Close();
            ms.Dispose();

            return fs;
        }

        public static bool TableListToExcel(List<DataTable> dts, DataTable main, string strExcelFileName, int indexType)
        {
            bool fs = false;

            XSSFWorkbook workbook = new XSSFWorkbook();
            DataSet set = new DataSet();
            foreach (DataTable dt in dts)
            {
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet(dt.TableName);
                //sheet.DefaultColumnWidth = 100 * 256;
                //sheet.DefaultRowHeight = 30 * 20;

                sheet.PrintSetup.PaperSize = 9;

                //sheet.SetMargin(MarginType.LeftMargin, (double)0.5);
                //sheet.SetMargin(MarginType.RightMargin, (double)0.5);
                //sheet.SetMargin(MarginType.BottomMargin, (double)0.5);
                //sheet.SetMargin(MarginType.TopMargin, (double)0.5);
                //sheet.FitToPage = false;
                XSSFCellStyle cellStyles = (XSSFCellStyle)workbook.CreateCellStyle();
                //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text来看
                //cellStyle.DataFormat = XSSFDataFormat.;
                //cellStyles.DataFormat = XSSFDataFormat.GetBuiltinFormat("0");

                cellStyles.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyles.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyles.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyles.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //XSSFFont cellfonts = workbook.CreateFont() as XSSFFont;
                //    cellfonts.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
                //cellStyles.SetFont(cellfonts);

                var q = (from g in main.AsEnumerable()
                         where g.Field<string>("Prolot") == dt.TableName.Substring(0, dt.TableName.Length - 2)

                         select g).ToList();

                XSSFCellStyle DefStyles = (XSSFCellStyle)workbook.CreateCellStyle();
                DefStyles.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                DefStyles.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                DefStyles.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                DefStyles.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                DefStyles.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                ////字体
                //XSSFFont headerfont = workbook.CreateFont() as XSSFFont;

                //headerfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                //DefStyles.SetFont(headerfont);

                //用column name 作为列名
                int icolIndex = 0;
                XSSFRow headerRow = (XSSFRow)sheet.CreateRow(0);

                foreach (DataColumn item in dt.Columns)
                {
                    XSSFCell cell = (XSSFCell)headerRow.CreateCell(icolIndex); ;
                    cell.SetCellValue(item.ColumnName);
                    cell.CellStyle = DefStyles;
                    icolIndex++;
                }

                XSSFCellStyle cellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text来看
                //cellStyle.DataFormat = XSSFDataFormat.;
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //XSSFFont cellfont = workbook.CreateFont() as XSSFFont;
                //    cellfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
                //cellStyle.SetFont(cellfont);
                if (indexType == 6)
                {
                    //建立内容行
                    int iRowIndex = 1;
                    int iCellIndex = 0;
                    foreach (DataRow Rowitem in dt.Rows)
                    {
                        XSSFRow DataRow = (XSSFRow)sheet.CreateRow(iRowIndex);
                        foreach (DataColumn Colitem in dt.Columns)
                        {
                            XSSFCell cell = (XSSFCell)DataRow.CreateCell(iCellIndex);
                            string type = Rowitem[Colitem].GetType().FullName.ToString();
                            cell.SetCellValue(GetValue(Rowitem[Colitem].ToString(), type));
                            cell.CellStyle = cellStyle;

                            iCellIndex++;
                        }
                        iCellIndex = 0;
                        iRowIndex++;
                    }
                    //自适应列宽
                    //AutoColumnWidth(sheet, 0);
                    //for (int i = 0; i < icolIndex; i++)
                    //{
                    //    sheet.AutoSizeColumn(i);
                    //}

                    //设置自适应宽度
                    for (int columnNum = 0; columnNum <= 10; columnNum++)
                    {
                        int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                        for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                        {
                            XSSFRow currentRow = (XSSFRow)sheet.GetRow(rowNum);
                            if (currentRow.GetCell(columnNum) != null)
                            {
                                XSSFCell currentCell = (XSSFCell)currentRow.GetCell(columnNum);
                                int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                                if (columnWidth < length)
                                {
                                    columnWidth = length;
                                }
                            }
                        }
                        sheet.SetColumnWidth(columnNum, columnWidth * 200);
                    }
                }

                AddRengionBorder(sheet, workbook, 0, 0, 0, 1);
            }
            //发送到客户端
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            //HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", HttpUtility.UrlEncode("WS" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8)));

            //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + strExcelFileName);
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strExcelFileName);
            //HttpContext.Current.Response.AddHeader("Content-Length", ms.Length.ToString());
            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            HttpContext.Current.Response.ContentType = "application/octet-stream;charset=utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.Flush();

            HttpContext.Current.Response.End();
            workbook = null;
            ms.Close();
            ms.Dispose();

            return fs;
        }

        /// <summary>
        /// NPOI自适应列宽
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cols"></param>
        public static void AutoColumnWidth(XSSFSheet Sheet)
        {
            //for (int col = 1; col <= cols; col++)
            //{
            //    Sheet.AutoSizeColumn(col);//自适应宽度，但是其实还是比实际文本要宽
            //    int columnWidth = Sheet.GetColumnWidth(col) / 256;//获取当前列宽度
            //    for (int rowIndex = 1; rowIndex <= Sheet.LastRowNum; rowIndex++)
            //    {
            //        XSSFRow row = (XSSFRow)Sheet.GetRow(rowIndex)  ;
            //        XSSFCell cell = (XSSFCell)row.GetCell(col);
            //        int contextLength = Encoding.UTF8.GetBytes(cell.ToString()).Length;//获取当前单元格的内容宽度
            //        columnWidth = columnWidth < contextLength ? contextLength : columnWidth;

            //    }
            //    Sheet.SetColumnWidth(col, columnWidth * 200);//

            //}

            //设置自适应宽度
            for (int columnNum = 0; columnNum <= 10; columnNum++)
            {
                int columnWidth = Sheet.GetColumnWidth(columnNum) / 256;
                for (int rowNum = 1; rowNum <= Sheet.LastRowNum; rowNum++)
                {
                    XSSFRow currentRow = (XSSFRow)Sheet.GetRow(rowNum);
                    if (currentRow.GetCell(columnNum) != null)
                    {
                        XSSFCell currentCell = (XSSFCell)currentRow.GetCell(columnNum);
                        int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                        if (columnWidth < length)
                        {
                            columnWidth = length;
                        }
                    }
                }
                Sheet.SetColumnWidth(columnNum, columnWidth * 256);
            }
        }

        /// <summary>
                /// 加范围边框
                /// </summary>
                /// <param name="firstRow">起始行</param>
                /// <param name="lastRow">结束行</param>
                /// <param name="firstCell">起始列</param>
                /// <param name="lastCell">结束列</param>
                /// <returns></returns>
        public static void AddRengionBorder(XSSFSheet sheet1, XSSFWorkbook workbook2, int firstRow, int lastRow, int firstCell, int lastCell)
        {
            try
            {
                //HSSFCellStyle Style = (HSSFCellStyle)workbook2.CreateCellStyle();
                for (int i = firstRow; i < lastRow; i++)
                {
                    for (int n = firstCell; n < lastCell; n++)
                    {
                        XSSFCell cell;
                        cell = (XSSFCell)sheet1.GetRow(i).GetCell(n);
                        if (cell == null)
                        {
                            cell = (XSSFCell)sheet1.GetRow(i).CreateCell(n);
                            cell.SetCellValue(" ");
                        }
                        XSSFCellStyle Style = (XSSFCellStyle)workbook2.CreateCellStyle();
                        ////为首行加上方边框
                        if (i == firstRow)
                        {
                            Style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                        }
                        //为末行加下方边框
                        if (i == lastRow - 1)
                        {
                            Style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                        }
                        //为首列加左边框
                        if (n == firstCell)
                        {
                            Style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                        }
                        //为末列加右边框
                        if (n == lastCell - 1)
                        {
                            Style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                        }
                        cell.CellStyle = Style;
                    }
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

        public static int LoadImage(string path, IWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            return wb.AddPicture(buffer, PictureType.JPEG);
        }
    }
}