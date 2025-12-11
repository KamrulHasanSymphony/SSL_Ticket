using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.CmnDocuments;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Purchase;
using SSL.Sample.SSL.Sample.Models;
using System.Data.SqlClient;
using System.Text.Json;

namespace SSL_ERP.Areas.Sample.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ICmnDocumentService _documentService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public PurchaseController(IPurchaseService purchaseService, ICmnDocumentService documentService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _purchaseService = purchaseService;
            _documentService = documentService;
            _environment = environment;

        }
        public IActionResult Index()
        {
            return View("~/Areas/Sample/Views/Purchase/Index.cshtml");
        }


        [HttpGet]
        public IActionResult GetVendor()
        {
            try
            {
                var res = _purchaseService.GetAllVendorData();
                return Json(res);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine(ex.ToString());
                return Json(new { error = "An error occurred while fetching vendor data." });
            }
        }

        public IActionResult GetAllProductData()
        {
            try
            {
                var products = _purchaseService.GetAllProductData();
                return Json(products); // Ensure the response is in JSON format
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public IActionResult GetAllUOMData()
        {
            try
            {
                var uom = _purchaseService.GetAllUOMData();
                return Json(uom); // Ensure the response is in JSON format
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public JsonResult GetUomByUOM(string vuom)
        {
            try
            {
                var res = _purchaseService.GetUomByUOM(vuom);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [HttpPost]
        public IActionResult SavePurchase(PurchaseHeaderVM objPurchase)
        {
            var res = _purchaseService.Insert(objPurchase);

            return Json(res);
        }

        public IActionResult UpdatePurchase(PurchaseHeaderVM objPurchase)
        {
            var res = _purchaseService.Update(objPurchase);

            return Json(res);
        }
        public JsonResult GetAllPurchaseByMasterId(int purchaseMasterId)
        {
            try
            {
                var res = _purchaseService.GetAllPurchaseByMasterId(purchaseMasterId);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public JsonResult GetVendorByVendorId(int vendorID)
        {
            try
            {
                var res = _purchaseService.GetVendorByVendorId(vendorID);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }



        [HttpPost]
        public JsonResult GetAllPurchaseData(GridOptions options)
        {
            var res = _purchaseService.GetAllPurchaseData(options);
            var erst = Json(res);

            return erst;
        }

        public JsonResult GetAllPurchaseDetailData(GridOptions options, int purchaseId)
        {
            var res = _purchaseService.GetAllPurchaseDetailData(options, purchaseId);
            var erst = Json(res);

            return erst;
        }
        public JsonResult GetAllVendorData(GridOptions options)
        {
            var res = _purchaseService.GetAllVendorData(options);
            var erst = Json(res);

            return erst;
        }

        public JsonResult GetPurchaseById(string checkedPurchaseIds)
        {
            try
            {
                var res = _purchaseService.GetPurchaseById(checkedPurchaseIds);
                string jsonString = JsonSerializer.Serialize(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/uploads", file.FileName); // Adjust the path as needed
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save file path to database
                    SaveFilePathToDatabase(filePath);
                }
            }

            return RedirectToAction("Index"); // Redirect to a suitable action
        }

        private void SaveFilePathToDatabase(string filePath)
        {
            using (var connection = new SqlConnection(""))
            {
                string query = "INSERT INTO Documents (FilePath) VALUES (@FilePath)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilePath", filePath);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle exceptions here
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Remove(string fileName)
        {
            try
            {
                // Delete the file from the server
                string path = Path.Combine(_environment.WebRootPath, "DocumentFile", fileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                // You can perform additional processing or database operations here

                // Return JSON response indicating success
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}
