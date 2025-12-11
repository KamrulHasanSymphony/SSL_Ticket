using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Product;
using SSL.Sample.SSL.Sample.Models;

namespace SSL_ERP.Areas.Sample.Controllers
{
    public class XProductController : Controller
    {
        private readonly IProductService _productService;

        public XProductController(IProductService productService)
        {
            _productService = productService;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllProductData(GridOptions options)
        {
            var res = _productService.GetAllProductData(options);
            var erst = Json(res);

            return erst;
        }

        [HttpPost]
        public IActionResult SaveProduct(AProductVM objProduct)
        {
            var res = _productService.Insert(objProduct);

            return Json(res);
        }
        public JsonResult GetCategory()
        {
            var res = _productService.GetAllCategoryData();
            return Json(res);
        }
        public JsonResult GetUom()
        {
            var res = _productService.GetAllUomData();
            return Json(res);
        }

        public IActionResult UpdateProduct(AProductVM objVendor)
        {
            var res = _productService.Update(objVendor);

            return Json(res);
        }

        public IActionResult DeleteProduct(int itemNo)
        {
            var res = _productService.Delete(itemNo);

            return Json(res, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
    }
}
