using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Product;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.TodayTaskSummary;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using SSL_ERP.ExtensionMethods;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;

namespace SSL_ERP.Areas.Support.TodayTaskSummary.Controllers
{
    public class TodayTaskSummaryController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly ITodayTaskSummaryServices _todayTaskSummaryServices;

        public TodayTaskSummaryController(ApplicationDbContext applicationDb, ITodayTaskSummaryServices todayTaskSummaryServices)
        {
            _applicationDb = applicationDb;
            _todayTaskSummaryServices = todayTaskSummaryServices;
        }

        public IActionResult Index()
        {
            TodayTaskSummaryVM vm = new TodayTaskSummaryVM();
            string userName = User.Identity.Name;

            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View("~/Areas/Support/TodayTaskSummary/Views/Index.cshtml");
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
                TodayTaskSummaryVM Master = new TodayTaskSummaryVM();
                Master.Operation = "add";

                return View("~/Areas/Support/TodayTaskSummary/Views/CreateEdit.cshtml", Master);
            }

        }

        public ActionResult CreateEdit(TodayTaskSummaryVM master)
        {

            string userName = User.Identity.Name;

            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ResultModel<TodayTaskSummaryVM> result = new ResultModel<TodayTaskSummaryVM>();
                try
                {



                    if (master.Operation == "update")
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.LastUpdatedBy = user.UserName;
                        master.LastUpdatedFrom = HttpContext.Connection.RemoteIpAddress.ToString();


                        result = _todayTaskSummaryServices.Update(master);

                        return Ok(result);
                    }
                    else
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.CreatedBy = user.UserName;
                        result = _todayTaskSummaryServices.Insert(master);

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
                    ResultModel<List<TodayTaskSummaryVM>> result =
                    _todayTaskSummaryServices.GetAll(new[] { "Id" }, new[] { id.ToString() });
                    TodayTaskSummaryVM clientsInfo = result.Data.FirstOrDefault();
                    clientsInfo.Operation = "update";
                    clientsInfo.Id = id;

                    return View("~/Areas/Support/TodayTaskSummary/Views/CreateEdit.cshtml", clientsInfo);
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
            string assigneeUserId = User.Identity.Name;
            var res = _todayTaskSummaryServices.GetGridData(options, assigneeUserId);
            var erst = Json(res);
            return erst;
        }

        public ActionResult MultiplePost(TodayTaskSummaryVM master)
        {
            ResultModel<TodayTaskSummaryVM> result = new ResultModel<TodayTaskSummaryVM>();

            try
            {

                string userName = User.Identity.Name;
                ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                master.PostedBy = user.UserName;
                master.PostedOn = DateTime.Now;
                master.PostedFrom = HttpContext.Connection.RemoteIpAddress.ToString();

                result = _todayTaskSummaryServices.MultiplePost(master);                

                return Ok(result);

            }
            catch (Exception ex)
            {
                result.Status = Status.Fail;
                result.Message = ex.Message;
                ex.LogAsync(ControllerContext.HttpContext);
            }

            return Ok(result);
        }
    }
}
