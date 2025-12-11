using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Product;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;

namespace SSL_ERP.Areas.Support.Product.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly IProductServices _productService;

        public ProductController(ApplicationDbContext applicationDb, IProductServices productService)
        {
            _applicationDb = applicationDb;
            _productService = productService;
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
                return View("~/Areas/Support/Product/Views/Index.cshtml");
            }
        }

        public IActionResult Create()
        {

            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                T_ProductsVM Master = new T_ProductsVM();
                Master.Operation = "add";

                return View("~/Areas/Support/Product/Views/CreateEdit.cshtml", Master);
            }

        }

        public ActionResult CreateEdit(T_ProductsVM master)
        {

            string userName = User.Identity.Name;

            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ResultModel<T_ProductsVM> result = new ResultModel<T_ProductsVM>();
                try
                {



                    if (master.Operation == "update")
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.UpdateBy = user.UserName;


                        result = _productService.Update(master);

                        return Ok(result);
                    }
                    else
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.CreateBy = user.UserName;
                        result = _productService.Insert(master);

                        return Ok(result);
                    }


                }
                catch (Exception ex)
                {
                    ex.LogAsync(ControllerContext.HttpContext);
                }

                return Ok(result);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                string userName = User.Identity.Name;
                if (userName == "0" || userName == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ResultModel<List<T_ProductsVM>> result =
                    _productService.GetAll(new[] { "Id" }, new[] { id.ToString() });
                    T_ProductsVM clientsInfo = result.Data.FirstOrDefault();
                    clientsInfo.Operation = "update";
                    clientsInfo.Id = id;

                    return View("~/Areas/Support/Product/Views/CreateEdit.cshtml", clientsInfo);
                }
            }
            catch (Exception e)
            {
                e.LogAsync(ControllerContext.HttpContext);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult GetGridData(GridOptions options)
        {
            var res = _productService.GetGridData(options);
            var erst = Json(res);
            return erst;
        }
    }
}
