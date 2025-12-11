using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.EntarnalNotes;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Ticket;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;
using System;
using System.Text;

namespace SSL_ERP.Areas.Support.Tickets.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ApplicationDbContext _applicationDb;
        private readonly KendoGrid<object> _kendoGrid;
        private readonly ITktEnternalNoteService _noteService;


        public TicketController(ITicketService ticketService, ApplicationDbContext appDbContext,KendoGrid<object> kendoGrid, ITktEnternalNoteService noteService)
        {
            _ticketService = ticketService;
            _applicationDb = appDbContext;
            _kendoGrid = kendoGrid;
            _noteService = noteService;
        }

        public IActionResult Index(string Self)
        {
            string userName = "0";
            if (Self == "s")
            {
                userName = User.Identity.Name;
            }
            T_TicketVm ticketMaster = new T_TicketVm()
            {
                Operation = "add",
                AssigneeUserId = userName

            };
            return View("~/Areas/Support/Tickets/Views/Tickets/Index.cshtml", ticketMaster);
        }
        public IActionResult Create()
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null) {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                T_TicketVm ticketMaster = new T_TicketVm();
                ticketMaster.CreateDate = DateTime.Now;
                ticketMaster.Operation = "add";

                return View("~/Areas/Support/Tickets/Views/Tickets/Create.cshtml", ticketMaster);
            }
            
        }


        [HttpPost]
        public ActionResult CreateEdit(T_TicketVm masterObj)
        {
            ResultModel<T_TicketVm> result = new ResultModel<T_TicketVm>();
            try
            {
                string userName = User.Identity.Name;

                if (userName == "0" || userName == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    masterObj.CreatedBy = userName;

                    PeramModel pm = new PeramModel();


                    if (masterObj.Operation == "update")
                    {
                        //string userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        masterObj.UpdateBy = userName;
                        result = _ticketService.Update(masterObj);

                        IndexModel index = new IndexModel();

                        index.OrderName = "Id";
                        index.startRec = 0;
                        index.pageSize = 100;

                        string[] conditionalFields = new[] { "" };
                        string?[] conditionalValue = new[] { "" };


                        int i = 0;
                        bool isChange = true;

                        if (result.Status == Status.Fail)
                        {
                            Exception ex = new Exception();
                            throw result.Exception;
                        }
                        return Ok(result);
                    }
                    else
                    {

                        string create = User.Identity.Name;
                        masterObj.CreatedBy = create;

                        result = _ticketService.Insert(masterObj);

                        if (result.Status == Status.Fail)
                        {
                            Exception ex = new Exception();
                            //_logger.LogError(ex, "An error occurred in the Index action.");
                            throw result.Exception;
                        }
                        return Ok(result);
                    }
                }
                    
            }
            catch (Exception ex)
            {
                ex.LogAsync(ControllerContext.HttpContext);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult SaveTicket(T_TicketVm objTicket)
        {
            var res = _ticketService.Insert(objTicket);
            return Json(res);
        }

        public ActionResult Edit(string id)
        {
            string userName = User.Identity.Name;

            try
            {
                if (userName == "0" || userName == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ResultModel<List<T_TicketVm>> result =
                    _ticketService.GetAll(new[] { "Id" }, new[] { id.ToString() });

                    T_TicketVm data = result.Data.FirstOrDefault();
                    data.Operation = "update";
                    data.Description = data.Description;
                    //data.Description = DecodeFromBase64(data.Description);
                    //data.Id = id;
                    //ResultModel<List<SupportVM>> details = _supportService.GetAll(new[] { "Id" }, new[] { id.ToString() });      
                    //return View("~/Areas/Tools/Views/Support/Create.cshtml", data);
                    return View("~/Areas/Support/Tickets/Views/Tickets/Create.cshtml", data);
                }
                    
            }
            catch (Exception e)
            {
                e.LogAsync(ControllerContext.HttpContext);
                return RedirectToAction("~/Areas/Support/Tickets/Views/Tickets/Create.cshtml");
            }

        }


        [HttpPost]
        public JsonResult GetAllTicketMData(GridOptions options, string AssigneeUserId)
        {
            
            var res = _ticketService.GetAllTicketMData(options, AssigneeUserId);

            var gridEntity = new GridEntity<T_TicketVm>
            {
                Items = res.Items,
                TotalCount = res.TotalCount,
                Columnses = res.Columnses
            };

            return Json(gridEntity);
        }

        public IActionResult GetAllStackHolder()
        {
            try
            {
                var stack = _ticketService.GetAllStackHolder();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public IActionResult GetAllClient()
        {
            try
            {
                var stack = _ticketService.GetAllClient();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public IActionResult GetAllticketSourceData()
        {
            try
            {
                var stack = _ticketService.GetAllticketSourceData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }


        public IActionResult GetAllProductsData()
        {
            try
            {
                var stack = _ticketService.GetAllProductsData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public IActionResult GetAllDepartmentData()
        {
            try
            {
                var dep = _ticketService.GetAllDepartmentData();
                return Json(dep);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }
        public IActionResult GetAllTicketTypeData()
        {
            try
            {
                var type = _ticketService.GetAllTicketTypeData();
                return Json(type);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public static string DecodeFromBase64(string base64EncodedData)
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(bytes);
        }
        public JsonResult GetAllEnternalNoteData(GridOptions options, int? id)
        {
            var res = _ticketService.GetAllEnternalNoteData(options, id);
            var erst = Json(res);

            return erst;
        }
        public JsonResult GetInternalById(int id)
        {
            try
            {
                var res = _ticketService.GetInternalById(id);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        [HttpPost]
        public IActionResult SaveEnternalNotes(T_TicketInternalNotesVM objNote)
        {
            string userName = User.Identity.Name;
            ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
            objNote.AssigneeUserId = userName;

            var res = _noteService.Insert(objNote);
            return Json(res);
        }

        public IActionResult SaveEnternalActiveNotes(T_TicketInternalNotesVM objActNote)
        {
            var res = _noteService.InsertActive(objActNote);
            return Json(res);
        }
    }
}
