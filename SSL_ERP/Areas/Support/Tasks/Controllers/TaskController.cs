using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Assignee;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Collaboration;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.EntarnalNotes;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.TaskTime;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Ticket;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;
using System.Text;
using OfficeOpenXml;

namespace SSL_ERP.Areas.Support.Tasks.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IEnternalNoteService _noteService;
        private readonly ICollaborationService _collaborationService;
        private readonly IAssigneeService _assigneeService;
        private readonly ITaskTimeService _taskTimeService;
        private readonly ApplicationDbContext _applicationDb;
        private readonly ITicketService _ticketService;
        private readonly KendoGrid<object> _kendoGrid;



        public TaskController(ITaskService taskService, IEnternalNoteService noteService
            , ICollaborationService collaborationService, IAssigneeService assigneeService
            , ITaskTimeService taskTimeService
            , ApplicationDbContext applicationDb
            , ITicketService ticketService
            , KendoGrid<object> kendoGrid
            )
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
            _collaborationService = collaborationService ?? throw new ArgumentNullException(nameof(collaborationService));
            _assigneeService = assigneeService ?? throw new ArgumentNullException(nameof(collaborationService));
            _taskTimeService = taskTimeService ?? throw new ArgumentNullException(nameof(taskTimeService));
            _applicationDb = applicationDb ?? throw new ArgumentNullException(nameof(applicationDb));
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _kendoGrid = kendoGrid;
        }

        public IActionResult Index(int? id)
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                T_TasksVM taskMaster = new T_TasksVM()
                {
                    Operation = "add",
                    T_TicketId = id.Value
                };
                return View("~/Areas/Support/Tasks/Views/Index.cshtml", taskMaster);
            }

        }

        public IActionResult TaskIndex(int? id, string Self, string? ticketTitle, string? ticketCode)
        {

            string userName = (Self == "s") ? User.Identity.Name : "0";

            var taskMaster = new T_TasksVM
            {
                Operation = "add",
                T_TicketId = id.Value,
                AssigneeUserId = userName,
                TicketCode = (ticketTitle != null || ticketCode != null)
                             ? $"[{ticketCode}] {ticketTitle}"
                             : null
            };

            return View("~/Areas/Support/Tasks/Views/TaskIndex.cshtml", taskMaster);
        }
        public IActionResult StartedTask(int? id, string Self)
        {
            string userName = "0";
            if (Self == "s")
            {
                userName = User.Identity.Name;
            }


            T_TasksVM taskMaster = new T_TasksVM()
            {
                Operation = "add",
                T_TicketId = id.Value,
                AssigneeUserId = userName
            };
            return View("~/Areas/Support/Tasks/Views/StartedTask.cshtml", taskMaster);
        }
        public IActionResult TodayTask(int? id, string Self)
        {
            string userName = "0";
            if (Self == "s")
            {
                userName = User.Identity.Name;
            }


            T_TasksVM taskMaster = new T_TasksVM()
            {
                Operation = "add",
                T_TicketId = id.Value,
                AssigneeUserId = userName
            };
            return View("~/Areas/Support/Tasks/Views/TodayTask.cshtml", taskMaster);
        }

        public IActionResult PendingTask(int? id, string Self)
        {
            string userName = "0";
            if (Self == "s")
            {
                userName = User.Identity.Name;
            }


            T_TasksVM taskMaster = new T_TasksVM()
            {
                Operation = "add",
                T_TicketId = id.Value,
                AssigneeUserId = userName
            };
            return View("~/Areas/Support/Tasks/Views/PendingTask.cshtml", taskMaster);
        }
        public IActionResult AllPendingTask(int? id, string Self)
        {
            string userName = "0";
            if (Self == "s")
            {
                userName = User.Identity.Name;
            }


            T_TasksVM taskMaster = new T_TasksVM()
            {
                Operation = "add",
                T_TicketId = id.Value,
                AssigneeUserId = userName
            };
            return View("~/Areas/Support/Tasks/Views/AllPendingTask.cshtml", taskMaster);
        }
        public IActionResult Create(int? ticketId)
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ResultModel<List<T_TicketVm>> result =
                  _ticketService.GetAll(new[] { "Id" }, new[] { ticketId.ToString() });

                T_TicketVm data = result.Data.FirstOrDefault();

                T_TasksVM taskMaster = new T_TasksVM();
                taskMaster.Operation = "add";
                taskMaster.Code = data.Code;
                //taskMaster.StartDate = DateTime.Today;

                taskMaster.T_TicketId = ticketId;
                //taskMaster.Id = data.Id;
                taskMaster.TicketCode = "[" + data.Code.ToString() + " ] " + data.Title;
                taskMaster.T_PriorityId = data.T_PriorityId;
                taskMaster.T_TopicId = data.T_TopicId;
                taskMaster.T_StatusId = 1;
                taskMaster.DepartmentId = data.DepartmentId;
                taskMaster.T_SourceId = data.T_SourceId;


                taskMaster.ProgressInPercent = 0;
                taskMaster.RequiredTime = 0;
                taskMaster.StartDate = DateTime.Now;
                taskMaster.StartTime = DateTime.Now.ToString("HH:mm:ss");


                return View("~/Areas/Support/Tasks/Views/Create.cshtml", taskMaster);
            }


        }

        [HttpPost]
        public ActionResult CreateEdit(T_TasksVM masterObj)
        {
            ResultModel<T_TasksVM> result = new ResultModel<T_TasksVM>();
            try
            {

                string userName = User.Identity.Name;
                if (userName == "0" || userName == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    PeramModel pm = new PeramModel();


                    if (masterObj.Operation == "update")
                    {
                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
                        masterObj.UpdateBy = userName;
                        masterObj.UpdateOn = DateTime.Now;
                        result = _taskService.Update(masterObj);
                        //var assignee = _taskService.GetTaskAssignee(masterObj.Id, userName);
                        //if(assignee.Count > 0)
                        //{                        
                        //    result = _taskService.Update(masterObj);                        
                        //}
                        //else
                        //{
                        //    result.Message = "Add Assignee please!";
                        //}

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

                        userName = User.Identity.Name;
                        ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);

                        result = _taskService.Insert(masterObj);

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
        public IActionResult DeleteFile(string fileName, string id)
        {
            string saveDirectory = "wwwroot\\files";

            ResultModel<AuditIssueAttachments> result = new ResultModel<AuditIssueAttachments>
            {
                Message = "File could not be deleted"
            };

            try
            {
                var path = Path.Combine(saveDirectory, fileName);

                if (!System.IO.File.Exists(path)) return Ok(result);

                result = _taskService.DeleteAttachments(Convert.ToInt32(id.Replace("file-", "")));

                if (result.Status == Status.Success)
                {
                    System.IO.File.Delete(path);
                }


                return Ok(result);
            }
            catch (Exception e)
            {
                e.LogAsync(ControllerContext.HttpContext);

                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<IActionResult> DownloadFile(string filePath)
        {
            string saveDirectory = "wwwroot\\files";

            try
            {
                var path = Path.Combine(saveDirectory, filePath);
                var memory = new MemoryStream();
                await using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                var ext = Path.GetExtension(path).ToLowerInvariant();
                return File(memory, GetMimeType(ext), Path.GetFileName(path));
            }
            catch (Exception e)
            {
                e.LogAsync(ControllerContext.HttpContext);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult SaveTicket(T_TasksVM objTicket)
        {
            var res = _taskService.Insert(objTicket);
            return Json(res);
        }


        public ActionResult Edit(string Id, string? ticketTitle, string? ticketCode)
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

                    ResultModel<List<T_TasksVM>> result =
                    _taskService.GetAll(new[] { "ts.Id" }, new[] { Id.ToString() });

                    T_TasksVM data = result.Data.FirstOrDefault();
                    data.T_TicketId = data.T_TicketId;

                    data.AttachmentsList = _taskService.GetAllAttachments(new[] { "T_TaskId" }, new[] { Id.ToString() }).Data;

                    data.Operation = "update";
                    data.Code = data.TicketCode;
                    data.TicketCode = "[" + data.TicketCode.ToString() + " ] " + data.TicketTitle;
                    //data.StartDate = DateTime.Today.ToString("yyyy-MM-dd");
                    //taskMaster.Id = data.Id;
                    if (data.ProgressInPercent == null)
                    {
                        data.ProgressInPercent = 0;
                    }

                    if (data.RequiredTime == null)
                    {
                        data.RequiredTime = 0;
                    }
                    if (ticketTitle != null && ticketCode != null)
                    {
                        data.TicketCode = "[" + ticketCode.ToString() + " ] " + ticketTitle;
                    }
                    return View("~/Areas/Support/Tasks/Views/Create.cshtml", data);
                }
            }
            catch (Exception e)
            {
                e.LogAsync(ControllerContext.HttpContext);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public JsonResult GetAllTicketTaskMData(GridOptions options, int? ticketId, string AssigneeUserId)
        {
            var res = _taskService.GetAllTTaskMData(options, ticketId, AssigneeUserId);

            var gridEntity = new GridEntity<T_TasksVM>
            {
                Items = res.Items,
                TotalCount = res.TotalCount,
                Columnses = res.Columnses
            };

            return Json(gridEntity);
        }
        public static string DecodeFromBase64(string base64EncodedData)
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(bytes);
        }
        public IActionResult GetAllTicket()
        {
            try
            {
                var stack = _taskService.GetAllTicket();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }
        public IActionResult GetAllTopicData()
        {
            try
            {
                var stack = _taskService.GetAllTopicData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }
        public IActionResult GetAllRatingData()
        {
            try
            {
                var stack = _taskService.GetAllRatingData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }
        public IActionResult GetAllPriorityData()
        {
            try
            {
                var stack = _taskService.GetAllPriorityData();
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
                var stack = _taskService.GetAllStatusData();
                return Json(stack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        public JsonResult GetAllEnternalNoteData(GridOptions options, int? id)
        {
            var res = _taskService.GetAllEnternalNoteData(options, id);
            var erst = Json(res);

            return erst;
        }

        public JsonResult GetTodayTaskData(GridOptions options, string? assigneeUserId)
        {
            assigneeUserId = User.Identity.Name;
            var res = _taskService.GetTodayTaskData(options, assigneeUserId);
            var erst = Json(res);

            return erst;
        }
        public JsonResult GetAllTaskOpenData(GridOptions options, int? ticketId, string? assigneeUserId)
        {
            var res = _taskService.GetAllTaskOpenData(options, ticketId, assigneeUserId);
            var erst = Json(res);

            return erst;
        }

        public JsonResult GetAllStartedTaskData(GridOptions options, string? assigneeUserId)
        {
            assigneeUserId = User.Identity.Name;
            var res = _taskService.GetAllStartedTaskData(options, assigneeUserId);
            var erst = Json(res);

            return erst;
        }

        public JsonResult GetPendingTaskData(GridOptions options, string? assigneeUserId)
        {
            assigneeUserId = User.Identity.Name;
            var res = _taskService.GetPendingTaskData(options, assigneeUserId);
            var erst = Json(res);

            return erst;
        }
        //public JsonResult GetAllPendingTaskData(GridOptions options)
        //{
        //    var res = _taskService.GetAllPendingTaskData(options);
        //    var erst = Json(res);

        //    return erst;
        //}
        public JsonResult GetAllPendingTaskData(GridOptions options, string assigneeUserId = null)
        {
            var res = _taskService.GetAllPendingTaskData(options, assigneeUserId);
            return Json(res);
        }

        [HttpPost]
        public IActionResult SaveEnternalNotes(T_TaskInternalNotesVM objNote)
        {
            string userName = User.Identity.Name;
            ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
            objNote.AssigneeUserId = userName;

            var res = _noteService.Insert(objNote);
            return Json(res);
        }

        public JsonResult GetInternalNoteByTaskId(GridOptions options, int? taskId)
        {
            var res = _taskService.GetInternalNoteByTaskId(options, taskId);

            var erst = Json(res);

            return erst;
        }

        public JsonResult GetAllCollaborationData(GridOptions options, int? id)
        {
            var res = _taskService.GetAllCollaborationData(options, id);
            return Json(res);
        }
        public JsonResult GetAllAssigneeData(GridOptions options, int? id)
        {
            var res = _taskService.GetAllAssigneeData(options, id);
            return Json(res);
        }

        [HttpPost]
        public IActionResult SaveCollaboration(T_TaskCollaborationsVM objCLB)
        {
            string userName = User.Identity.Name;
            ApplicationUser? user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);
            objCLB.UserId = userName;

            var res = _collaborationService.Insert(objCLB);
            return Json(res);
        }

        public JsonResult GetCollaborationByTaskId(GridOptions options, int? taskId)
        {
            var res = _taskService.GetCollaborationByTaskId(options, taskId);

            var erst = Json(res);

            return erst;
        }

        public IActionResult SaveAssigne(T_TaskAssignesVM objAssigne)
        {

            var res = _assigneeService.Insert(objAssigne);
            return Json(res);
        }

        public IActionResult SaveEnternalActiveNotes(T_TaskInternalNotesVM objActNote)
        {
            var res = _noteService.InsertActive(objActNote);
            return Json(res);
        }

        public JsonResult GetAllTaskTimeData(GridOptions options, int? id)
        {
            var res = _taskService.GetAllTaskTimeData(options, id);
            return Json(res);
        }

        public IActionResult SaveStartTime(T_TaskTimesVM objStart)
        {
            string userName = User.Identity.Name;
            //T_TaskTimesVM taskTime = new T_TaskTimesVM();

            objStart.AssigneeUserId = userName;

            var res = _taskTimeService.Insert(objStart);
            return Json(res);
        }
        public IActionResult SaveStopTime(T_TaskTimesVM objStop)
        {
            var res = _taskTimeService.Update(objStop);
            return Json(res);
        }
        //public IActionResult SaveHoldTime(T_TaskTimesVM objHold)
        //{
        //    var res = _taskTimeService.Update(objHold);
        //    return Json(res);
        //}
        public IActionResult SaveCollabActiveNotes(T_TaskCollaborationsVM objActCol)
        {
            var res = _collaborationService.InsertActive(objActCol);
            return Json(res);
        }
        public IActionResult SaveAssigneActiveNotes(T_TaskAssignesVM objActAssign)
        {
            var res = _assigneeService.InsertActive(objActAssign);
            return Json(res);
        }

        public JsonResult GetAssigneById(int id)
        {
            try
            {
                var res = _taskService.GetAssigneById(id);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public JsonResult GetCollaborationById(int id)
        {
            try
            {
                var res = _taskService.GetCollaborationById(id);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public JsonResult GetInternalById(int id)
        {
            try
            {
                var res = _taskService.GetInternalById(id);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public JsonResult GetStackHolderById(string selectedId)
        {
            try
            {
                bool isStackHolder;
                string userName = User.Identity.Name;
                var data = _taskService.GetTicketByTicketId(selectedId);
                if (data[0].StackHolderUserId == userName)
                {
                    isStackHolder = true;
                }

                else
                {
                    isStackHolder = false;
                }


                return Json(isStackHolder);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public JsonResult GetTimeByTask(int taskId)
        {
            try
            {
                var res = _taskService.GetTimeByTask(taskId);
                var jres = Json(res);
                return Json(res);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files, string CustomField1, string CustomField2)
        {
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // You can create an instance of your view model to save file details to the database
                        T_TaskFilesVM fileData = new T_TaskFilesVM
                        {
                            FileName = file.FileName,
                            FilePath = filePath,
                            CreateDate = DateTime.Now,
                            IsActive = true
                        };


                        // Save fileData to the database here
                    }
                }
            }

            // Access custom fields
            var customField1 = CustomField1;
            var customField2 = CustomField2;

            return Json(new { success = true });
        }



        private string GetMimeType(string ext)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(ext, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        public IActionResult GetAllTaskTypeData()
        {
            try
            {
                var type = _taskService.GetAllTaskTypeData();
                return Json(type);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        [HttpGet]
        public IActionResult GetSchedulerData()
        {
            string userName = User.Identity.Name;

            var data = _taskService.GetSchedulerData(userName);
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetAllSchedulerData()
        {

            var data = _taskService.GetAllSchedulerData();
            return Json(data);
        }

        public IActionResult GetAllClient()
        {
            try
            {
                var clients = _taskService.GetAllClient();
                return Json(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching product data. Please try again later.");
            }
        }

        [HttpPost]
        public IActionResult UploadExcel(IFormFile excelFile, T_TasksVM ticketDefaults)
        {
            if (excelFile == null || excelFile.Length == 0)
                return Json(new { status = "Fail", message = "No file selected." });

            try
            {
                // Required for EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    stream.Position = 0;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        if (worksheet == null)
                            return Json(new { status = "Fail", message = "No worksheet found in Excel file." });

                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var title = worksheet.Cells[row, 1].Text?.Trim();
                            var description = worksheet.Cells[row, 2].Text?.Trim();
                            var status = worksheet.Cells[row, 3].Text?.Trim();
                            var priority = worksheet.Cells[row, 4].Text?.Trim();
                            var startDateStr = worksheet.Cells[row, 5].Text?.Trim();
                            var startTimeStr = worksheet.Cells[row, 6].Text?.Trim();

                            if (string.IsNullOrWhiteSpace(title))
                                continue; // Skip blank rows

                            // ✅ Validate Status
                            var validStatuses = new[] { "Open", "Closed" };
                            if (!validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
                            {
                                return Json(new
                                {
                                    status = "Fail",
                                    message = $"Row {row}: Correct Status please (must be 'Open' or 'Closed')."
                                });
                            }

                            // ✅ Validate Priority
                            var validPriorities = new[] { "Normal", "High", "Emergency", "Medium" };
                            if (!validPriorities.Contains(priority, StringComparer.OrdinalIgnoreCase))
                            {
                                return Json(new
                                {
                                    status = "Fail",
                                    message = $"Row {row}: Correct Priority please (must be 'Normal', 'High', 'Emergency', or 'Medium')."
                                });
                            }

                            // 🟩 Map Status
                            int statusId = status.Equals("Open", StringComparison.OrdinalIgnoreCase) ? 1 : 2;

                            // 🟧 Map Priority
                            int priorityId = priority switch
                            {
                                "Normal" => 1,
                                "High" => 2,
                                "Emergency" => 3,
                                "Medium" => 4,
                                _ => 1
                            };

                            // 🟨 Parse Date
                            DateTime? parsedStartDate = null;
                            if (DateTime.TryParse(startDateStr, out DateTime tempDate))
                                parsedStartDate = tempDate;

                            // 🟩 Build Task
                            var taskVm = new T_TasksVM
                            {
                                Operation = "add",
                                T_TicketId = ticketDefaults.T_TicketId,
                                T_SourceId = ticketDefaults.T_SourceId,
                                T_TopicId = ticketDefaults.T_TopicId,
                                T_PriorityId = priorityId,
                                DepartmentId = ticketDefaults.DepartmentId,
                                TaskTypeId = ticketDefaults.TaskTypeId,
                                Title = title,
                                Description = description,
                                T_StatusId = statusId,
                                ProgressInPercent = 0,
                                StartDate = parsedStartDate,
                                StartTime = startTimeStr,
                                RequiredTime = 0,
                                CreatedOn = DateTime.Now,
                                UpdateOn = DateTime.Now,
                                CreatedBy = User.Identity?.Name ?? "System",
                                UpdateBy = User.Identity?.Name ?? "System",
                                IsComplete = false,
                                IsSendEmail = false,
                                WorkingStatus = null
                            };

                            // 🟪 Insert Task
                            var result = _taskService.Insert(taskVm);
                            if (result.Status == Status.Fail)
                                throw result.Exception ?? new Exception($"Failed to insert task for row {row}");

                            // ✅ Insert Assignee
                            var assignesVM = new T_TaskAssignesVM
                            {
                                T_TaskId = result.Data.Id,
                                T_TicketId = result.Data.T_TicketId,
                                AssigneeUserId = User.Identity?.Name,
                                CreatedBy = User.Identity?.Name ?? "ERP",
                                CreatedOn = DateTime.Now.ToString()
                            };

                            var assigneeResult = _assigneeService.Insert(assignesVM);
                            if (assigneeResult.Status == Status.Fail)
                                throw assigneeResult.Exception ?? new Exception($"Failed to assign user for row {row}");
                        }
                    }
                }

                return Json(new { status = "Success", message = "Tasks created and assigned from Excel successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Fail", message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult DownloadExcelTemplate(int ticketId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Tasks");

                // Add headers
                worksheet.Cells[1, 1].Value = "Title";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "Status";
                worksheet.Cells[1, 4].Value = "Priority";
                worksheet.Cells[1, 5].Value = "Start Date";
                worksheet.Cells[1, 6].Value = "Start Time";

                // Add a static example row
                worksheet.Cells[2, 1].Value = "Task Upload";
                worksheet.Cells[2, 2].Value = "Task excell upload option need to develope.";
                worksheet.Cells[2, 3].Value = "Open";
                worksheet.Cells[2, 4].Value = "High";
                worksheet.Cells[2, 5].Value = "10/11/2025";
                worksheet.Cells[2, 6].Value = "13:30:00";

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"Task_Template.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

    }
}

