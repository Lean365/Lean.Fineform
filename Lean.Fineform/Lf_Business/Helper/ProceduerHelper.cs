using System;

using FineUIPro;
using System.Linq;


//使用Hashtable时，必须引入这个命名空间
namespace Fine
{
    public class ProceduerHelper : PageBase
    {
        /// <summary>
        /// 数据库存储过程，计算年月日
        /// </summary>
        public static string DateProcedure = " CREATE PROCEDURE[dbo].[GetCalendarByYear]" +
        " @Year varchar(4)" +
        " AS" +
        " BEGIN" +

        " DECLARE @WeekString varchar(12)," +
        "   @dDate DATETIME," +
        "   @adddays int," +
        "   @tempDate varchar(6)" +


         " SELECT @adddays = 1 " +


         "SELECT @tempDate = DATENAME(YY,cast(cast(@Year as varchar) as DATETIME)) FROM Adm_TheDate " +
        " IF @tempDate = '' OR @tempDate IS NULL" +
        " BEGIN" +
        "  SELECT @tempDate = DATENAME(YY, @Year) " +
        " END" +


        " SELECT @dDate = cast(@tempDate + '-01' + '-01' as DATETIME) " +


        " INSERT INTO Adm_TheDate(TheYear, TheMonth, TheDay, TheWeekDay, TheWeeks, TheMonths, IsWorkDay,TheDatetime, Guid, UDF01,UDF02,UDF03,UDF04,UDF05,UDF06,UDF51,UDF52,UDF53,UDF54,UDF55,UDF56,isDelete,Creator, CreateTime)" +
        " SELECT DATENAME(yy, convert(varchar(20), dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01'), 120)) AS TheYear" +
        " , DateName(month, convert(varchar(20), dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01'), 120)) AS TheMonth" +
        " , DateName(day, convert(varchar(20), dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01'), 120)) AS TheDay" +
        "  , DateName(WEEKDAY, dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01')) AS TheWeekDay" +
        "    ,  DateName(yy, convert(varchar(20), dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01'), 120))+'W'+DateName(week, dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01')) AS TheWeeks" +
        " , DATEPART(month, convert(varchar(20), dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01'), 120)) AS TheMonths" +
        "  , CASE WHEN datename(WEEKDAY, dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01')) IN('星期六', '星期日') THEN 0 ELSE 1 END AS IsWorkDay" +
        " ,convert(varchar(20),dateadd(dd,number,convert(varchar(5),@dDate,120)+'01-01'),120) as TheDatetime " +
            "  , NEWID() AS Guid" +
        " ,''as UDF01,''as UDF02,''as UDF03,''as UDF04,''as UDF05,''as UDF06,0 as UDF51,0 as UDF52,0 as UDF53,0 as UDF54,0 as UDF55,0 as UDF56, 0 as isDelete, @Year+'Admin' AS Creator" +
        " , GETDATE() AS CreateTime" +
        " FROM master..spt_values" +
        " WHERE type = 'P'" +
        " AND dateadd(dd, number, convert(varchar(5), @dDate, 120) + '01-01') <= dateadd(dd, -1, convert(varchar(5), dateadd(YEAR, 1, @dDate), 120) + '01-01')" +
        "END";

    }
}