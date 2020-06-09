using System;
using System.IO;
using System.Text;
using SignLib.Certificates;
using SignLib.Pdf;
using Softveloper.IO;

namespace ConsoleNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var ps = new PdfSignature("serial number");
            ps.LoadPdfDocument("./source.pdf");

            var height = 40;
            var page = ps.DocumentProperties.DocumentPageSize(ps.DocumentProperties.NumberOfPages);
            var width = 80;

            ps.DigitalSignatureCertificate = DigitalCertificate.LoadCertificate("./cert.p12", "123456");
            ps.FontFile = "./THSarabunITù.ttf";
            ps.SignatureAdvancedPosition = new System.Drawing.Rectangle(50, (page.Y - 50) - height, width, height);
            ps.SignatureImage = System.IO.File.ReadAllBytes("./graphic.png");
            ps.SignatureImageType = SignatureImageType.ImageAsBackground;
            ps.SignaturePage = ps.DocumentProperties.NumberOfPages;
            // ps.SignaturePosition = SignaturePosition.TopRight;
            ps.SignatureText = $"(๑) ความคิดเห็น{Environment.NewLine}เห็นควรอนุมัติ{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}ใครสักฅน ที่เกิดมาเพื่อผูกพันธ์{Environment.NewLine}ผู้อำนวยการสร้าง{Environment.NewLine}วันที่ ๒๗ กุมภาพันธ์ ๒๕๖๓ เวลา ๐๐:๐๐ น.";
            ps.SigningReason = "I approve this document";
            Filerectory.CreateDirectory("./temps");
            File.WriteAllBytes($"./temps/{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.pdf", ps.ApplyDigitalSignature());
        }
    }
}
