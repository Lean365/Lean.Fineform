using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using FineUIPro;
namespace Lean.Fineform
{
    public class EncryptHelper
    {
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptString">AES密文</param>
        /// <param name="key">秘钥（44个字符）</param>
        /// <param name="ivString">向量（16个字符）</param>
        /// <returns></returns>
        public static string AES_Decrypt(string decryptString, string key, string ivString)
        {
            try
            {

                key = key.PadRight(32, ' ');
                RijndaelManaged aes = new RijndaelManaged();

                byte[] iv = Encoding.UTF8.GetBytes(ivString.Substring(0, 16));
                aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 32));
                aes.Mode = CipherMode.ECB;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;  //


                ICryptoTransform rijndaelDecrypt = aes.CreateDecryptor();
                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] xBuff = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(xBuff);
            }
            catch (Exception ex)
            {
                //Alert.ShowInTop(ex.Message.ToString(), MessageBoxIcon.Error);
                throw ex;

            }
        }



        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encriyptString">要被加密的字符串</param>
        /// <param name="key">秘钥（44个字符）</param>
        /// <param name="ivString">向量长度（16个字符）</param>
        /// <returns></returns>
        public static string AES_Encrypt(string encriyptString, string key, string ivString)
        {
            key = key.PadRight(32, ' ');
            SymmetricAlgorithm aes = new RijndaelManaged();

            byte[] iv = Encoding.UTF8.GetBytes(ivString.Substring(0, 16));


            aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            aes.Mode = CipherMode.ECB;
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7; //


            ICryptoTransform rijndaelEncrypt = aes.CreateEncryptor();
            byte[] inputData = Encoding.UTF8.GetBytes(encriyptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }




        #region Base64加密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <returns></returns>
        public static string EncodeBase64(string text)
        {
            //如果字符串为空，则返回
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            try
            {
                char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
                                            'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
                                            'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                                            '8','9','+','/','='};
                byte empty = (byte)0;
                ArrayList byteMessage = new ArrayList(Encoding.Default.GetBytes(text));
                StringBuilder outmessage;
                int messageLen = byteMessage.Count;
                int page = messageLen / 3;
                int use = 0;
                if ((use = messageLen % 3) > 0)
                {
                    for (int i = 0; i < 3 - use; i++)
                        byteMessage.Add(empty);
                    page++;
                }
                outmessage = new System.Text.StringBuilder(page * 4);
                for (int i = 0; i < page; i++)
                {
                    byte[] instr = new byte[3];
                    instr[0] = (byte)byteMessage[i * 3];
                    instr[1] = (byte)byteMessage[i * 3 + 1];
                    instr[2] = (byte)byteMessage[i * 3 + 2];
                    int[] outstr = new int[4];
                    outstr[0] = instr[0] >> 2;
                    outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                    if (!instr[1].Equals(empty))
                        outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                    else
                        outstr[2] = 64;
                    if (!instr[2].Equals(empty))
                        outstr[3] = (instr[2] & 0x3f);
                    else
                        outstr[3] = 64;
                    outmessage.Append(Base64Code[outstr[0]]);
                    outmessage.Append(Base64Code[outstr[1]]);
                    outmessage.Append(Base64Code[outstr[2]]);
                    outmessage.Append(Base64Code[outstr[3]]);
                }
                return outmessage.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Base64解密
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        public static string DecodeBase64(string text)
        {
            //如果字符串为空，则返回
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            //将空格替换为加号
            text = text.Replace(" ", "+");

            try
            {
                if ((text.Length % 4) != 0)
                {
                    return "包含不正确的BASE64编码";
                }
                if (!Regex.IsMatch(text, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
                {
                    return "包含不正确的BASE64编码";
                }
                string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
                int page = text.Length / 4;
                ArrayList outMessage = new ArrayList(page * 3);
                char[] message = text.ToCharArray();
                for (int i = 0; i < page; i++)
                {
                    byte[] instr = new byte[4];
                    instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                    instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                    instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                    instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                    byte[] outstr = new byte[3];
                    outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                    if (instr[2] != 64)
                    {
                        outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                    }
                    else
                    {
                        outstr[2] = 0;
                    }
                    if (instr[3] != 64)
                    {
                        outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                    }
                    else
                    {
                        outstr[2] = 0;
                    }
                    outMessage.Add(outstr[0]);
                    if (outstr[1] != 0)
                        outMessage.Add(outstr[1]);
                    if (outstr[2] != 0)
                        outMessage.Add(outstr[2]);
                }
                byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
                return Encoding.Default.GetString(outbyte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text">要加密的文本</param>
        /// <param name="sKey">秘钥</param>
        /// <returns></returns>
        public static string MD5Encrypt(string Text, string sKey = "cube")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            //md4j = ret.ToString();
            return ret.ToString();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string MD5Decrypt(string Text, string sKey = "cube")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        //默认密钥向量
        private static byte[] Keys = { 0x13, 0x24, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /**//// <summary>
            /// DES加密字符串
            /// </summary>
            /// <param name="encryptString">待加密的字符串</param>
            /// <param name="encryptKey">加密密钥,要求为8位</param>
            /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /**//// <summary>
            /// DES解密字符串
            /// </summary>
            /// <param name="decryptString">待解密的字符串</param>
            /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
            /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// 创建加密签名
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public static string CreateSignature(string userinfo)
        {
            var token = Guid.NewGuid().ToString("N").Substring(0, 8);
            return EncryptDES(userinfo, token);
        }

        /// <summary>
        /// 获取当前随机码
        /// </summary>
        /// <returns></returns>
        public static string GetCode()
        {
            Random rd = new Random();
            int shu = rd.Next(97, 122);
            char c = (char)shu;
            Random rdnumber = new Random();
            var val = rdnumber.Next(0, 99999);
            return c + val.ToString().PadLeft(5, '0');
        }
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetSHA256HashFromString(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            SHA256 sha = new SHA256CryptoServiceProvider();

            byte[] retVal = sha.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetSHA1HashFromString(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] retVal = sha.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// SHA384加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetSHA384HashFromString(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            SHA384 sha = new SHA384CryptoServiceProvider();

            byte[] retVal = sha.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetSHA512HashFromString(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            SHA512 sha = new SHA512CryptoServiceProvider();

            byte[] retVal = sha.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}