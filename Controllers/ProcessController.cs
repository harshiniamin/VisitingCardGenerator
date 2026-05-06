using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Visitingcardgenerator.Data;
using System.Drawing;
using System.IO;

namespace Visitingcardgenerator.Controllers
{
    public class ProcessController : Controller
    {
        private readonly AppDbContext db;

        public ProcessController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Index(DateTime? fromDate, DateTime? toDate, string status)
        {
            var data = db.UploadHistory.AsQueryable();

            if (fromDate.HasValue)
                data = data.Where(x => x.UploadDate >= fromDate.Value);

            if (toDate.HasValue)
                data = data.Where(x => x.UploadDate <= toDate.Value);

            if (!string.IsNullOrEmpty(status))
                data = data.Where(x => x.Status == status);

            return View(data.OrderByDescending(x => x.UploadDate).ToList());
        }

        public IActionResult ProcessFile(int id)
        {
            var upload = db.UploadHistory.FirstOrDefault(x => x.Id == id);

            if (upload == null)
                return NotFound();

            // Update status
            upload.Status = "Processed";
            db.SaveChanges();

            // Redirect to Preview (IMPORTANT)
            return RedirectToAction("Preview", new { id = id });
        }



        public IActionResult Preview(int id)
        {
            var customers = db.CustomerInfo
                              .Where(x => x.UploadId == id)
                              .ToList();

            foreach (var c in customers)
            {
                string vCard = $"BEGIN:VCARD\n" +
                               $"VERSION:3.0\n" +
                               $"FN:{c.FullName}\n" +
                               $"ORG:{c.Designation}\n" +
                               $"TEL:{c.Phone}\n" +
                               $"EMAIL:{c.Email}\n" +
                               $"ADR:{c.Address}\n" +
                               $"END:VCARD";

                var qrGenerator = new QRCoder.QRCodeGenerator();
                var qrData = qrGenerator.CreateQrCode(vCard, QRCoder.QRCodeGenerator.ECCLevel.Q);

                var qrCode = new QRCoder.PngByteQRCode(qrData);
                byte[] qrBytes = qrCode.GetGraphic(20);

                c.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(qrBytes);
            }

            return View(customers);
        }
    }
}