using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Product;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorGroup;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorService;
using SSL.Sample.SSL.Sample.Models;

namespace SSL_ERP.Areas.Sample.Controllers
{
    public class SampleController : Controller
    {
        private readonly IVendorServices _VendorService;
        private readonly IVendorGroup _VendorGroup;
        //private readonly IProductService _productService;
        public SampleController(IVendorServices vendorService, IVendorGroup vendorGroup/*, IProductService productService*/)
        {
            _VendorService = vendorService;
            _VendorGroup = vendorGroup;
            //_productService= productService;
        }


        //public SampleController(IVendorServices vendorService)
        //{
        //    _VendorService = vendorService;
        //}

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Dashboard()
        {
            return View();
        }

        //[HttpPost]
        //public JsonResult GetAllTestData(GridOptions options)
        //{
        //    var res = _productService.GetAllProductData(options);
        //    var erst = Json(res);

        //    return erst;
        //}



        public IActionResult Test()
        {
            return View();
            //return View("~/Areas/Sample/Views/SampleView/Index.cshtml");
        }

        public IActionResult VendorGroup()
        {
            return View("~/Areas/Sample/Views/Vendor/Index.cshtml");
        }

        //[HttpPost]
        //public IActionResult SaveVendor(AVendorVM objVendor)
        //{
        //    var res = _VendorService.Insert(objVendor);
        //    if (res.Message == "Saved Successfully")
        //    {
        //        return Ok(new { message = "Saved Successfully" });
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = "Save Unsuccessfully" });
        //    }
        //}

        [HttpPost]
        public IActionResult SaveVendorGroup(VendorGroupVm objVendorGroup)

        {
            var res = _VendorGroup.Insert(objVendorGroup);

            return Json(res, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllTestData(GridOptions options)        
        
        {            
            var res = _VendorService.GetAllTestData(options);
            var erst = Json(res);
            
            return erst;
        }

        //public IActionResult UpdateVendor(AVendorVM objVendor)
        //{
        //    var res = _VendorService.Update(objVendor);

        //    return Json(res, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        //}

        public IActionResult UpdateVendorGroup(VendorGroupVm objVendorGroup)
        {
            var res = _VendorGroup.Update(objVendorGroup);

            return Json(res, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public IActionResult DeleteVendor(int vendorId)
        {
            var res = _VendorService.Delete(vendorId);

            return Json(res, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public IActionResult DeleteVendorGroup(int vendorGroupId)
        {
            var res = _VendorGroup.Delete(vendorGroupId);

            return Json(res, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public JsonResult VendorName()
        {
            var res = _VendorService.GetAllVendorData();
            return Json(res);
            //var result = _companyInfoService.GetAll(null, null);
            //return Ok(result);
        }
        public JsonResult Country(int Id)
        {
            var res = _VendorService.GetAllCountryData(Id);
            return Json(res);
            //var result = _companyInfoService.GetAll(null, null);
            //return Ok(result);
        }
    }
}
