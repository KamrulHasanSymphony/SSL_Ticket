using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.ExcellUpload;
using SSL_ERP.Persistence;

namespace SSL_ERP.Areas.Support.ExcellUpload.Controllers
{
    [Area("Support")]
    public class ExcellUploadController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly IExcellUploadService _excellUploadService;

        public ExcellUploadController(ApplicationDbContext applicationDb, IExcellUploadService excellUploadService)
        {
            _applicationDb = applicationDb ?? throw new ArgumentNullException(nameof(applicationDb));
            _excellUploadService = excellUploadService ?? throw new ArgumentNullException(nameof(excellUploadService));
        }

        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View("~/Areas/Support/ExcellUpload/Views/Index.cshtml");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadExcelFile(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return Json(new { success = false, message = "No file selected" });

        //    try
        //    {
        //        // You can call your service to process the Excel file
        //        var result = await _excellUploadService.ProcessExcelFileAsync(file);

        //        if (result.Success)
        //        {
        //            return Json(new { success = true, message = result.Message });
        //        }
        //        else
        //        {
        //            return Json(new { success = false, message = result.Message });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Error uploading file: " + ex.Message });
        //    }
        //}
    }
}
