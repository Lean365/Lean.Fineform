using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fine
{
    public class StringHelper
    {
        #region 替换特殊字符  
        public static string StringClear(string strMessage)
        {
            string[] aryReg = { "'", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "-", "_", ";", "||", "[", "]", "&", "/", "-", "|", " ", };
            for (int i = 0; i < aryReg.Length; i++)
            {
                strMessage = strMessage.Replace(aryReg[i], string.Empty);
            }
            strMessage = strMessage.ToUpper();
            return strMessage;
        }
        public static string FilterSpecial(string str)//特殊字符过滤函数
        {
            if (str == "") //如果字符串为空，直接返回。
            {

                return str;
            }
            else
            {
                //str = str.Replace("'", "");
                //str = str.Replace("<", "");
                //str = str.Replace(">", "");
                //str = str.Replace("%", "");
                //str = str.Replace("'delete", "");
                //str = str.Replace("''", "");
                str = str.Replace("\\", "/");
                //str = str.Replace("\r\n", "**");//换行符
                //str = str.Replace("\r", "*");//换行符
                //str = str.Replace("\n", "*");//换行符
                //str = str.Replace(",", "");
                //str = str.Replace(".", "");
                //str = str.Replace(">=", "");
                //str = str.Replace("=<", "");
                //str = str.Replace("-", "");
                //str = str.Replace("_", "");
                //str = str.Replace(";", "");
                //str = str.Replace("||", "");
                //str = str.Replace("[", "");
                //str = str.Replace("]", "");
                //str = str.Replace("&", "");
                //str = str.Replace("/", "");
                //str = str.Replace("-", "");
                //str = str.Replace("|", "");
                //str = str.Replace("?", "");
                //str = str.Replace(">?", "");
                //str = str.Replace("?<", "");
                //str = str.Replace(" ", "");
                str = str.Replace("'", ",");
                //以下是全角中文标点
                str = str.Replace("～", "~");
                str = str.Replace("｀", "`");
                str = str.Replace("！", "!");
                str = str.Replace("＠", "@");
                str = str.Replace("＃", "#");
                str = str.Replace("＄", "$");
                str = str.Replace("％", "%");
                str = str.Replace("＾", "^");
                str = str.Replace("＆", "&");
                str = str.Replace("＊", "*");
                str = str.Replace("（", "(");
                str = str.Replace("）", ")");
                str = str.Replace("＿", "_");
                str = str.Replace("＋", "+");
                str = str.Replace("－", "-");
                str = str.Replace("＝", "=");
                str = str.Replace("［", "[");
                str = str.Replace("］", "]");
                str = str.Replace("｛", "{");
                str = str.Replace("｝", "}");
                str = str.Replace("＼", "*");
                str = str.Replace("｜", "|");
                str = str.Replace("＂", "\"");
                str = str.Replace("＇", ",");
                str = str.Replace("：", ":");
                str = str.Replace("／", "/");
                str = str.Replace("？", "?");
                str = str.Replace("＞", ">");
                str = str.Replace("＜", "<");
                str = str.Replace("·", ".");
                str = str.Replace("！", "!");
                str = str.Replace("@", "@");
                str = str.Replace("#", "#");
                str = str.Replace("￥", "$");
                str = str.Replace("%", "%");
                str = str.Replace("……", "...");
                str = str.Replace("&", "&");
                str = str.Replace("*", "*");
                str = str.Replace("（", "(");
                str = str.Replace("）", ")");
                str = str.Replace("——", "_");
                str = str.Replace("+", "+");
                str = str.Replace("-", "-");
                str = str.Replace("=", "=");
                str = str.Replace("、", ",");
                str = str.Replace("|", "|");
                str = str.Replace("】", "]");
                str = str.Replace("【", "[");
                str = str.Replace("{", "{");
                str = str.Replace("}", "}");
                str = str.Replace("‘", ",");
                str = str.Replace("’", ",");
                str = str.Replace("“", "\"");
                str = str.Replace("”", "\"");
                str = str.Replace(";", ";");
                str = str.Replace("：", ":");
                str = str.Replace("》", ">");
                str = str.Replace("《", "<");
                str = str.Replace("？", "?");
                str = str.Replace("。", ".");
                str = str.Replace("，", ",");

                str = str.ToUpper();
                return str;
            }
        }
        #endregion
        #region 全角转换半角以及半角转换为全角  
        ///转全角的函数(SBC case)  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248  
        public static string ToSBC(string input)
        {
            input = input.ToUpper();
            // 半角转全角：  
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 32)
                {
                    array[i] = (char)12288;
                    continue;
                }
                if (array[i] < 127)
                {
                    array[i] = (char)(array[i] + 65248);
                }
            }
            return new string(array);
        }

        ///转半角的函数(DBC case)  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248//   
        public static string ToDBC(string input)
        {
            input = input.ToUpper();
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }

            return new string(array);
        }
        #endregion
        #region 字符大小写转换  
        public static string strToUpper(string str)
        {
            str = str.ToUpper();
            return str;
        }
        public static string strToLower(string str)
        {
            str = str.ToLower();
            return str;
        }
        #endregion
        #region 金额大小写转换 
        public static string MoneySmallToBig(string par)
        {
            String[] Scale = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
            String[] Base = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            String Temp = par;
            string result = null;
            int index = Temp.IndexOf(".", 0, Temp.Length);//判断是否有小数点
            if (index != -1)
            {
                Temp = Temp.Remove(Temp.IndexOf("."), 1);
                for (int i = Temp.Length; i > 0; i--)
                {
                    int Data = Convert.ToInt16(Temp[Temp.Length - i]);
                    result += Base[Data - 48];
                    result += Scale[i - 1];
                }
            }
            else
            {
                for (int i = Temp.Length; i > 0; i--)
                {
                    int Data = Convert.ToInt16(Temp[Temp.Length - i]);
                    result += Base[Data - 48];
                    result += Scale[i + 1];
                }
            }
            return result;
        }
        #endregion
        /// <summary> 
        /// 转换人民币大小金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string CNYcurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /**/
        /// <summary> 
        /// 一个重载，将字符串先转换成数字在调用CmycurD(decimal num) 
        /// </summary> 
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param> 
        /// <returns></returns> 
        public static string CNYcurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CNYcurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }

    }
}