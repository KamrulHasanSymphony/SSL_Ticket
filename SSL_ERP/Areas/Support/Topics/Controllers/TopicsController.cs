using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Topics;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;

namespace SSL_ERP.Areas.Support.Topics.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly ITopicsServices _topicsService;

        public TopicsController(ApplicationDbContext applicationDb, ITopicsServices topicsService)
        {
            _applicationDb = applicationDb;
            _topicsService = topicsService;
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
                return View("~/Areas/Support/Topics/Views/Index.cshtml");
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
                T_TopicsVM Master = new T_TopicsVM();
                Master.Operation = "add";

                return View("~/Areas/Support/Topics/Views/CreateEdit.cshtml", Master);
            }

        }

        public ActionResult CreateEdit(T_TopicsVM master)
        {

            string userName = User.Identity.Name;

            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ResultModel<T_TopicsVM> result = new ResultModel<T_TopicsVM>();
                try
                {



                    if (master.Operation == "update")
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.UpdateBy = user.UserName;


                        result = _topicsService.Update(master);

                        return Ok(result);
                    }
                    else
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.CreateBy = user.UserName;
                        result = _topicsService.Insert(master);

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
                    ResultModel<List<T_TopicsVM>> result =
                    _topicsService.GetAll(new[] { "Id" }, new[] { id.ToString() });
                    T_TopicsVM clientsInfo = result.Data.FirstOrDefault();
                    clientsInfo.Operation = "update";
                    clientsInfo.Id = id;

                    return View("~/Areas/Support/Topics/Views/CreateEdit.cshtml", clientsInfo);
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
            var res = _topicsService.GetGridData(options);
            var erst = Json(res);
            return erst;
        }
    }
}
