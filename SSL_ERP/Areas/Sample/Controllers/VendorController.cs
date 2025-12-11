using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Product;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorService;
using SSL.Sample.SSL.Sample.Models;

namespace SSL_ERP.Areas.Sample.Controllers
{
    public class VendorController : Controller
    {

        
        private readonly IVendorServices _VendorService;

        public VendorController(IVendorServices vendorService)
        {
            _VendorService = vendorService;
        }
        public IActionResult VendorIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveVendor(AVendorVM objVendor)
        {
            var res = _VendorService.Insert(objVendor);
            return Json(res);
        }

        public IActionResult UpdateVendor(AVendorVM objVendor)
        {
            var res = _VendorService.Update(objVendor);

            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Vendors", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok("File uploaded successfully.");
            }
            else
            {
                return BadRequest("File not selected or invalid.");
            }
        }
    }
}
