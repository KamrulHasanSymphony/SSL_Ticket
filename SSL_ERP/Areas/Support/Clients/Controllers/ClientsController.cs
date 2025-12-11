using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Clients;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Company;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using SSL.Ticket.SSL.Ticket.Services.Company;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;

namespace SSL_ERP.Areas.Support.Clients.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly IClientsService _clientService;

        public ClientsController(ApplicationDbContext applicationDb, IClientsService clientService)
        {
            _applicationDb = applicationDb ?? throw new ArgumentNullException(nameof(applicationDb));
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
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
                return View("~/Areas/Support/Clients/Views/Index.cshtml");
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
                T_ClientsVm Master = new T_ClientsVm();
                Master.Operation = "add";

                return View("~/Areas/Support/Clients/Views/CreateEdit.cshtml", Master);
            }

        }

        public ActionResult CreateEdit(T_ClientsVm master)
        {

            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ResultModel<T_ClientsVm> result = new ResultModel<T_ClientsVm>();
                try
                {



                    if (master.Operation == "update")
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.UpdateBy = user.UserName;


                        result = _clientService.Update(master);

                        return Ok(result);
                    }
                    else
                    {

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        master.CreateBy = user.UserName;
                        result = _clientService.Insert(master);

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
                    ResultModel<List<T_ClientsVm>> result =
                    _clientService.GetAll(new[] { "Id" }, new[] { id.ToString() });
                    T_ClientsVm clientsInfo = result.Data.FirstOrDefault();
                    clientsInfo.Operation = "update";
                    clientsInfo.Id = id;

                    return View("~/Areas/Support/Clients/Views/CreateEdit.cshtml", clientsInfo);
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
            var res = _clientService.GetGridData(options);
            var erst = Json(res);
            return erst;
        }
    }
}
