using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Purchase;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Support;
using SSL.Sample.SSL.Sample.Models;
using SSL.Sample.SSL.Sample.Models.Support;
using SSL.Sample.SSL.Sample.Services.Purchase;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using SSL_ERP.Models;
using StackExchange.Exceptional;
using SSL_ERP.Persistence;
using SSL.Sample.SSL.Sample.Models.Tickets;

namespace SSL_ERP.Areas.Tools.Controllers
{
    public class SupportController : Controller
    {
        private readonly ISupportService _supportService;
        private readonly ApplicationDbContext _applicationDb;

        public SupportController(ISupportService supportService, ApplicationDbContext appDbContext)
        {
            _supportService = supportService;
            _applicationDb = appDbContext;
        }

        public IActionResult Index()
        {
            T_TicketVm ticketMaster = new T_TicketVm()
            {
                Operation = "add",

            };

            return View("~/Areas/Tools/Views/Tickets/Index.cshtml", ticketMaster);
        }



       

        public IActionResult Create()
        {
            T_TicketVm ticketMaster = new T_TicketVm();
                ticketMaster.Operation = "add";

            return View("~/Areas/Tools/Views/Tickets/Create.cshtml", ticketMaster);
        }


        [HttpPost]
        public ActionResult CreateEdit(T_TicketVm master)
        {
            ResultModel<T_TicketVm> result = new ResultModel<T_TicketVm>();
            try
            {
                
                PeramModel pm = new PeramModel();
               

                if (master.Operation == "update")
                {
                    string userName = User.Identity.Name;
                    ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);

                    result = _supportService.Update(master);

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
                       // _logger.LogError(ex, "An error occurred in the Index action.");
                        throw result.Exception;
                    }
                    return Ok(result);
                }
                else
                {

                    string userName = User.Identity.Name;
                    ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);

                    result = _supportService.Insert(master);

                    if (result.Status == Status.Fail)
                    {
                        Exception ex = new Exception();
                        //_logger.LogError(ex, "An error occurred in the Index action.");
                        throw result.Exception;
                    }
                    return Ok(result);
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
            var res = _supportService.Insert(objTicket);
            return Json(res);
        }

        public ActionResult Edit(string id)
        {
            try
            {

                ResultModel<List<T_TicketVm>> result =
                    _supportService.GetAll(new[] { "T.Id" }, new[] { id.ToString() });

                T_TicketVm data = result.Data.FirstOrDefault();
                data.Operation = "update";
                data.Description = DecodeFromBase64(data.Description);
                //data.Id = id;
                //ResultModel<List<SupportVM>> details = _supportService.GetAll(new[] { "Id" }, new[] { id.ToString() });      
                //return View("~/Areas/Tools/Views/Support/Create.cshtml", data);
                return View("~/Areas/Tools/Views/Support/create.cshtml", data);
            }
            catch (Exception e)
            {
                e.LogAsync(ControllerContext.HttpContext);
                return RedirectToAction("Index");
            }
        
        }


        [HttpPost]
        public JsonResult GetAllTicketMData(GridOptions options)
        {
            // Call the service method to get the data
            var res = _supportService.GetAllTicketMData(options);

            // Decode Base64 encoded descriptions
            foreach (var item in res.Items)
            {
                item.Description = DecodeFromBase64(item.Description);
            }

            // Wrap the data in the GridEntity structure
            var gridEntity = new GridEntity<T_TicketVm>
            {
                Items = res.Items,
                TotalCount = res.TotalCount,
                Columnses = res.Columnses
            };

            // Return the data as JSON
            return Json(gridEntity);
        }

        public IActionResult GetAllStackHolder()
        {
            try
            {
                var stack = _supportService.GetAllStackHolder();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public IActionResult GetAllOrganization()
        {
            try
            {
                var stack = _supportService.GetAllOrganization();
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
                var stack = _supportService.GetAllticketSourceData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public IActionResult GetAllticketTopicData()
        {
            try
            {
                var stack = _supportService.GetAllticketTopicData();
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
                var stack = _supportService.GetAllDepartmentData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }
        public IActionResult GetAllStatusData()
        {
            try
            {
                var stack = _supportService.GetAllStatusData();
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
                var stack = _supportService.GetAllProductsData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public IActionResult GetAllAssignToData()
        {
            try
            {
                var stack = _supportService.GetAllAssignToData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        [HttpGet("Tools/Support/GetAssignToByTicketId")]
        public IActionResult GetAssignToByTicketId(int id)
        {
            try
            {
                var res = _supportService.GetAssignToByTicketId(id);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public static string DecodeFromBase64(string base64EncodedData)
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
