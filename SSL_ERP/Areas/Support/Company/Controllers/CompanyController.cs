using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Core.Interfaces.Services.Company;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Company;
using SSL.Ticket.SSL.Ticket.Models;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;

namespace SSL_ERP.Areas.Support.Company.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly ICompanyService _companyService;

        public CompanyController(ApplicationDbContext applicationDb, ICompanyService companyService)
        {
            _applicationDb = applicationDb;
            _companyService = companyService;
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
                return View("~/Areas/Support/Company/Views/Index.cshtml");
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
                SSL.Ticket.SSL.Ticket.Models.CompanyInfo Master = new SSL.Ticket.SSL.Ticket.Models.CompanyInfo();
                Master.Operation = "add";

                return View("~/Areas/Support/Company/Views/CreateEdit.cshtml", Master);
            }
                
        }

        [HttpPost]
        public JsonResult GetGridData(GridOptions options)
        {
            var res = _companyService.GetGridData(options);
            var erst = Json(res);
            return erst;
        }
        public ActionResult CreateEdit(SSL.Ticket.SSL.Ticket.Models.CompanyInfo master)
        {

            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ResultModel<SSL.Ticket.SSL.Ticket.Models.CompanyInfo> result = new ResultModel<SSL.Ticket.SSL.Ticket.Models.CompanyInfo>();
                try
                {



                    if (master.Operation == "update")
                    {

                         userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.CreatedBy = user.UserName;


                        result = _companyService.Update(master);

                        return Ok(result);
                    }
                    else
                    {

                         userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.CreatedBy = user.UserName;
                        result = _companyService.Insert(master);

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
                    ResultModel<List<SSL.Ticket.SSL.Ticket.Models.CompanyInfo>> result =
                    _companyService.GetAll(new[] { "CompanyID" }, new[] { id.ToString() });
                    SSL.Ticket.SSL.Ticket.Models.CompanyInfo companyinfos = result.Data.FirstOrDefault();
                    companyinfos.Operation = "update";
                    companyinfos.CompanyID = id;

                    return View("~/Areas/Support/Company/Views/CreateEdit.cshtml", companyinfos);
                }
            }
            catch (Exception e)
            {
                e.LogAsync(ControllerContext.HttpContext);
                return RedirectToAction("Index");
            }
        }
    }
}
