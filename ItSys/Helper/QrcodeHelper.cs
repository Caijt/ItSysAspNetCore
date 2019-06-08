using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QRCoder;

namespace ItSys.Helper
{
    public class QrcodeHelper
    {
        public static byte[] CreateQrcode(string url, int size=5)
        {
            var generator = new QRCodeGenerator();
            var qrcodeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCode qrcode = new QRCode(qrcodeData);
            var img = qrcode.GetGraphic(size, Color.Black, Color.White, false);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
