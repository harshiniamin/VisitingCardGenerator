using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Visitingcardgenerator.Data;
using Visitingcardgenerator.Models;

namespace Visitingcardgenerator.Controllers
{
    public class UploadController : Controller
    {
        private readonly AppDbContext db;

        public UploadController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.Banks = db.Banks.ToList();

            ViewBag.Uploads = db.UploadHistory
                .OrderByDescending(x => x.UploadDate)
                .Take(10)
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult UploadExcel(IFormFile file, int bankId)
        {
            int recordCount = 0;

            if (file != null && file.Length > 0)
            {
                ExcelPackage.License.SetNonCommercialPersonal("Harshi");

                var bank = db.Banks.FirstOrDefault(b => b.BankId == bankId);

                var upload = new UploadHistory
                {
                    FileName = file.FileName,
                    UploadDate = DateTime.Now,
                    BankName = bank?.BankName ?? "Unknown",
                    TotalRecords = 0,
                    Status = "New"
                };

                db.UploadHistory.Add(upload);
                db.SaveChanges();

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var ws = package.Workbook.Worksheets[0];

                        for (int row = 2; row <= ws.Dimension.Rows; row++)
                        {
                            var customer = new CustomerInfo
                            {
                                FullName = ws.Cells[row, 1].Text,
                                Email = ws.Cells[row, 2].Text,
                                Designation = ws.Cells[row, 3].Text,
                                Phone = ws.Cells[row, 4].Text,
                                Address = ws.Cells[row, 5].Text,
                                BankId = bankId,
                                UploadId = upload.Id,
                                Status = "New"
                            };

                            db.CustomerInfo.Add(customer);
                            recordCount++;
                        }
                    }
                }

                upload.TotalRecords = recordCount;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Process");
        }
    }
}