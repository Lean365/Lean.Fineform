using FineUIPro;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace LeanFine
{
    public class QrcodeHelper
    {
        /// <summary>
        /// 二维码处理类
        /// </summary>
        public class QRCodeHandler
        {
            /// <summary>
            /// 创建二维码
            /// </summary>
            /// <param name="QRString">二维码字符串</param>
            /// <param name="QRCodeEncodeMode">二维码编码(Byte、AlphaNumeric、Numeric)</param>
            /// <param name="QRCodeScale">二维码尺寸(Version为0时，1：26x26，每加1宽和高各加25</param>
            /// <param name="QRCodeVersion">二维码密集度0-40</param>
            /// <param name="QRCodeErrorCorrect">二维码纠错能力(L：7% M：15% Q：25% H：30%)</param>
            /// <param name="filePath">保存路径</param>
            /// <param name="hasLogo">是否有logo(logo尺寸50x50，QRCodeScale>=5，QRCodeErrorCorrect为H级)</param>
            /// <param name="logoFilePath">logo路径</param>
            /// <returns></returns>
            public bool CreateQRCode(string QRString, string QRCodeEncodeMode, short QRCodeScale, int QRCodeVersion, string QRCodeErrorCorrect, string filePath, bool hasLogo, string logoFilePath)
            {
                bool result = true;

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

                switch (QRCodeEncodeMode)
                {
                    case "Byte":
                        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                        break;

                    case "AlphaNumeric":
                        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                        break;

                    case "Numeric":
                        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                        break;

                    default:
                        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                        break;
                }

                qrCodeEncoder.QRCodeScale = QRCodeScale;
                qrCodeEncoder.QRCodeVersion = QRCodeVersion;

                switch (QRCodeErrorCorrect)
                {
                    case "L":
                        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                        break;

                    case "M":
                        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                        break;

                    case "Q":
                        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                        break;

                    case "H":
                        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                        break;

                    default:
                        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                        break;
                }

                try
                {
                    System.Drawing.Image image = qrCodeEncoder.Encode(QRString, System.Text.Encoding.UTF8);
                    System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                    image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fs.Close();

                    if (hasLogo)
                    {
                        System.Drawing.Image copyImage = System.Drawing.Image.FromFile(logoFilePath);
                        Graphics g = Graphics.FromImage(image);
                        int x = image.Width / 2 - copyImage.Width / 2;
                        int y = image.Height / 2 - copyImage.Height / 2;
                        g.DrawImage(copyImage, new Rectangle(x, y, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                        g.Dispose();

                        image.Save(filePath);
                        copyImage.Dispose();
                    }
                    image.Dispose();
                }
                catch (Exception Message)
                {
                    Alert.ShowInTop("使用无效的类:" + Message);
                    result = false;
                }
                return result;
            }
        }

        /// <summary>
        /// 生成并保存二维码图片的方法
        /// </summary>
        /// <param name="str">输入的内容</param>
        public static void CreateQRImg(string QRString, string QRpath, FineUIPro.Image QRimage)
        {
            Bitmap bt;
            string enCodeString = QRString;
            //生成设置编码实例
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置二维码的规模，默认4
            qrCodeEncoder.QRCodeScale = 4;
            //设置二维码的版本，默认7
            qrCodeEncoder.QRCodeVersion = 7;
            //设置错误校验级别，默认中等
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片
            bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
            //二维码图片的名称
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss");
            //保存二维码图片在photos路径下
            bt.Save(QRpath + filename + ".jpg");
            //图片控件要显示的二维码图片路径
            QRimage.ImageUrl = "~/photos/" + filename + ".jpg";
        }

        /// <summary>
        /// 删除文件夹以及文件
        /// </summary>
        /// <param name="directoryPath"> 文件夹路径 </param>
        /// <param name="fileName"> 文件名称 </param>
        public static void DeleteDirectory(string directoryPath, string fileName)
        {
            //删除文件
            for (int i = 0; i < Directory.GetFiles(directoryPath).ToList().Count; i++)
            {
                if (Directory.GetFiles(directoryPath)[i] == fileName)
                {
                    File.Delete(fileName);
                }
            }

            //删除文件夹
            for (int i = 0; i < Directory.GetDirectories(directoryPath).ToList().Count; i++)
            {
                if (Directory.GetDirectories(directoryPath)[i] == fileName)
                {
                    Directory.Delete(fileName, true);
                }
            }
        }
    }
}