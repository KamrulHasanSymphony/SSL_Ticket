using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.Task
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common;
        private Session session;

        public TaskService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork;
            _common = common;
        }

        long maxSizeInBytes = 2 * 1024 * 1024;  
        string saveDirectory = "wwwroot\\files";
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf", ".xls", ".xlsx", ".docx" };


               

        public ResultModel<List<T_TasksVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    var records = context.Repositories.TaskRepository.GetAll(conditionalFields, conditionalValue);
                    context.SaveChanges();

                    return new ResultModel<List<T_TasksVM>>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.DataLoaded,
                        Data = records
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<List<T_TasksVM>>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.DataLoadedFailed,
                        Exception = e
                    };
                }

            }
        }

        public ResultModel<List<T_TasksVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TasksVM> Insert(T_TasksVM model)
        {

            List<string> savedPaths = new List<string>();


            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                //savedPaths = _fileService.UploadFiles(model.Attachments, saveDirectory, allowedExtensions, maxSizeInBytes).Result;
                savedPaths = UploadFiles(model.Attachments, saveDirectory, allowedExtensions, maxSizeInBytes).Result;

                string CodeGroup = "TS";
                string CodeName = "Task";
                //string code = context.Repositories.TicketRepository.GenerateCode(CodeGroup, CodeName, Convert.ToInt32(1));
                string code = context.Repositories.TaskRepository.NewGenerateCode(model.TaskId);
                model.Code = code;

                T_TasksVM master = context.Repositories.TaskRepository.Insert(model);


                foreach (string savedPath in savedPaths)
                {
                    AuditIssueAttachments auditIssueAttachment = new AuditIssueAttachments
                    {                      
                        FileName = Path.GetFileName(savedPath),
                        T_TaskId = model.Id
                    };

                    auditIssueAttachment = context.Repositories.TaskRepository.InsertAttachments(auditIssueAttachment);
                    //auditIssueAttachment = InsertAttachments(auditIssueAttachment);
                    master.AttachmentsList.Add(auditIssueAttachment);
                }

                context.SaveChanges();
                return new ResultModel<T_TasksVM>()
                {
                    Status = Status.Success,
                    Message = MessageModel.InsertSuccess,
                    Data = master,
                    Success = true
                };

            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public ResultModel<T_TasksVM> Update(T_TasksVM model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    List<string> savedPaths = new List<string>();
                    savedPaths = UploadFiles(model.Attachments, saveDirectory, allowedExtensions, maxSizeInBytes).Result;

                    //string CodeGroup = "TS";
                    //string CodeName = "Task";
                    //if(model.Code == null || model.Code == "")
                    //{
                    //    string code = context.Repositories.TicketRepository.GenerateCode(CodeGroup, CodeName, Convert.ToInt32(1));
                    //    model.Code = code;
                    //}

                    //string taskCode = context.Repositories.TaskRepository.NewGenerateCode(model.TaskId);
                    //model.Code = taskCode;


                    T_TasksVM master = context.Repositories.TaskRepository.Update(model);
                    var attachmentData = context.Repositories.TaskRepository.GetAttachmentsById(model.Id);

                    if (attachmentData.Count == 0) {
						foreach (string savedPath in savedPaths)
						{
							AuditIssueAttachments auditIssueAttachment = new AuditIssueAttachments
							{
								FileName = Path.GetFileName(savedPath),
								T_TaskId = model.Id
							};

							auditIssueAttachment = context.Repositories.TaskRepository.InsertAttachments(auditIssueAttachment);
							//auditIssueAttachment = context.Repositories.TaskRepository.UpdateAttachments(auditIssueAttachment);
							//auditIssueAttachment = InsertAttachments(auditIssueAttachment);
							master.AttachmentsList.Add(auditIssueAttachment);
						}
					}
                    else
                    {
						foreach (string savedPath in savedPaths)
						{
							AuditIssueAttachments auditIssueAttachment = new AuditIssueAttachments
							{
								FileName = Path.GetFileName(savedPath),
								T_TaskId = model.Id
							};

							//auditIssueAttachment = context.Repositories.TaskRepository.InsertAttachments(auditIssueAttachment);
							auditIssueAttachment = context.Repositories.TaskRepository.UpdateAttachments(auditIssueAttachment);
							//auditIssueAttachment = InsertAttachments(auditIssueAttachment);
							master.AttachmentsList.Add(auditIssueAttachment);
						}
					}
					

                    context.SaveChanges();


                    return new ResultModel<T_TasksVM>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<T_TasksVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }

        public ResultModel<T_TasksVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<T_TicketVm> GetAllTicket()
        {
            try
            {
                return _common.Select_Data_List<T_TicketVm>("Select_Task_Dropdown", "get_all_ticket_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TopicsVM> GetAllTopicData()
        {
            try
            {
                return _common.Select_Data_List<T_TopicsVM>("Select_Task_Dropdown", "get_all_Topic_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TaskTypeVm> GetAllTaskTypeData()
        {
            try
            {
                return _common.Select_Data_List<T_TaskTypeVm>("Select_Task_Dropdown", "get_all_tasktype_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_RatingsVm> GetAllRatingData()
        {
            try
            {
                return _common.Select_Data_List<T_RatingsVm>("Select_Task_Dropdown", "get_all_Rating_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_PrioritiesVM> GetAllPriorityData()
        {
            try
            {
                return _common.Select_Data_List<T_PrioritiesVM>("Select_Task_Dropdown", "get_all_Priorities_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_StatusVM> GetAllStatusData()
        {
            try
            {
                return _common.Select_Data_List<T_StatusVM>("Select_Task_Dropdown", "get_all_status_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TaskInternalNotesVM> GetAllEnternalNoteData(GridOptions options, int? id)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var detail = new GridEntity<T_TaskInternalNotesVM>();
                    detail = KendoGrid<T_TaskInternalNotesVM>.GetGridData_5(options, "sp_Select_Enternal_Note_Grid", "get_Note_summary", "Id", id.ToString());
                    return detail;
                }                    
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TaskInternalNotesVM> GetInternalNoteByTaskId(GridOptions options, int? taskId)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var detail = new GridEntity<T_TaskInternalNotesVM>();
                    detail = KendoGrid<T_TaskInternalNotesVM>.GetGridData_5(options, "sp_Select_Enternal_Note_Grid", "get_Note_summary", "Id", taskId.ToString());
                    return detail;
                }
                    
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TaskCollaborationsVM> GetAllCollaborationData(GridOptions options, int? id)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var collab = new GridEntity<T_TaskCollaborationsVM>();
                    collab = KendoGrid<T_TaskCollaborationsVM>.GetGridData_5(options, "sp_Select_Collaboration_Grid", "get_collaboration_summary", "Id", id.ToString());
                    return collab;
                }
                    
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TaskCollaborationsVM> GetCollaborationByTaskId(GridOptions options, int? taskId)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var collab = new GridEntity<T_TaskCollaborationsVM>();
                    collab = KendoGrid<T_TaskCollaborationsVM>.GetGridData_5(options, "sp_Select_Collaboration_Grid", "get_collaboration_summary", "Id", taskId.ToString());
                    return collab;
                }
               
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TaskAssignesVM> GetAllAssigneeData(GridOptions options, int? id)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var assignee = new GridEntity<T_TaskAssignesVM>();
                    assignee = KendoGrid<T_TaskAssignesVM>.GetGridData_5(options, "sp_Select_Assigne_Grid", "get_assigne_summary", "Id", id.ToString());
                    return assignee;
                }
               
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<T_TasksVM> InsertActive(T_TasksVM model)
        {
            throw new NotImplementedException();
        }

        public GridEntity<T_TaskTimesVM> GetAllTaskTimeData(GridOptions options, int? id)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var assignee = new GridEntity<T_TaskTimesVM>();
                    assignee = KendoGrid<T_TaskTimesVM>.GetGridData_5(options, "sp_Select_TaskTime_Grid", "get_time_summary", "StartTime", id.ToString());
                    return assignee;
                }
                   
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TaskAssignesVM> GetAssigneById(int id)
        {
            try
            {
                return _common.Select_Data_List<T_TaskAssignesVM>("GetById", "get_assign_by_id", id.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TaskCollaborationsVM> GetCollaborationById(int id)
        {
            try
            {
                return _common.Select_Data_List<T_TaskCollaborationsVM>("GetById", "get_collaboration_by_id", id.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TaskInternalNotesVM> GetInternalById(int id)
        {
            try
            {
                return _common.Select_Data_List<T_TaskInternalNotesVM>("GetById", "get_internal_by_id", id.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TasksVM> GetAllTTaskMData(GridOptions options, int? ticketId, string AssigneeUserId)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TasksVM>();
                    support = KendoGrid<T_TasksVM>.GetGridData_5(options, "sp_Select_TTaskMain_Grid", "get_ttask_summary", "ts.TaskId", ticketId.ToString(), AssigneeUserId);
                    return support;
                }
                 
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public GridEntity<T_TasksVM> GetAllTaskOpenData(GridOptions options, int? ticketId, string AssigneeUserId)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TasksVM>();
                    support = KendoGrid<T_TasksVM>.GetGridData_5(options, "sp_Select_TaskOpen_Grid", "get_taskopen_summary", "ts.Id", ticketId.ToString(), AssigneeUserId);
                    return support;
                }
               
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public GridEntity<T_TasksVM> GetTodayTaskData(GridOptions options, string AssigneeUserId)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TasksVM>();
                    support = KendoGrid<T_TasksVM>.GetGridData_5(options, "sp_Select_TodayTask_Grid", "get_tasktoday_summary", "ts.TaskId", AssigneeUserId);
                    return support;
                }
                  
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public GridEntity<T_TasksVM> GetPendingTaskData(GridOptions options, string AssigneeUserId)
        {
            try 
            {
                using (var context = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TasksVM>();
                    support = KendoGrid<T_TasksVM>.GetGridData_5(options, "sp_Select_PendingTask_Grid", "get_pendingtasktoday_summary", "ts.TaskId", AssigneeUserId);
                    return support;
                }
                  
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        //public GridEntity<T_TasksVM> GetAllPendingTaskData(GridOptions options)
        //{
        //    try
        //    {
        //        using (var context = _unitOfWork.Create())
        //        {
        //            var support = new GridEntity<T_TasksVM>();
        //            support = KendoGrid<T_TasksVM>.GetGridData_5(options, "sp_Select_AllPendingTask_Grid", "get_allpendingtask_summary", "ts.TaskId");
        //            return support;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
        //}


        public GridEntity<T_TasksVM> GetAllPendingTaskData(GridOptions options, string assigneeUserId = null)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TasksVM>();
                    support = KendoGrid<T_TasksVM>.GetGridData_5(
                        options,
                        "sp_Select_AllPendingTask_Grid",
                        "get_allpendingtask_summary",
                        "ts.TaskId",
                        assigneeUserId
                    );
                    return support;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public List<T_TaskTimesVM> GetTimeByTask(int taskId)
        {
            try
            {
                return _common.Select_Data_List<T_TaskTimesVM>("GetById", "get_time_by_id", taskId.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<AuditIssueAttachments> DeleteAttachments(int id)
        {
            using var context = _unitOfWork.Create();
            try
            {
                var count = context.Repositories.TaskRepository.Delete(
                    TableName.T_TicketFiles,
                    new[] { "Id" }, new[] { id.ToString() }
                );
                context.SaveChanges();
                return new ResultModel<AuditIssueAttachments>()
                {
                    Status = Status.Success,
                    Message = MessageModel.DeleteSuccess,
                    EffectedRows = count
                };
            }
            catch (Exception e)
            {

                context.RollBack();

                return new ResultModel<AuditIssueAttachments>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    EffectedRows = 0
                };
            }
        }



        public async Task<List<string>> UploadFiles(IList<IFormFile>? files, string saveDirectory, string[] allowedExtensions = null, long? maxSizeInBytes = null)
        {
            var savedPaths = new List<string>();

            if (files is null)
                return savedPaths;

            foreach (var file in files)
            {

                if (file != null)
                {

                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (allowedExtensions != null && (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension)))
                    {
                        throw new Exception($"Invalid file extension. Allowed extensions are: {string.Join(", ", allowedExtensions)}");
                    }


                    if (maxSizeInBytes.HasValue && file.Length > maxSizeInBytes.Value)
                    {
                        throw new Exception($"File size exceeds limit of {maxSizeInBytes.Value / 1024 / 1024}MB");
                    }


                    string guid = Guid.NewGuid().ToString();

                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);


                    var newFileName = $"{fileName}_shp_{guid}{extension}";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), saveDirectory, newFileName);
                    savedPaths.Add(filePath);

                    await using var fileStream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }
            }

            return savedPaths;
        }

        public ResultModel<List<AuditIssueAttachments>> GetAllAttachments(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using var context = _unitOfWork.Create();
            try
            {

                List<AuditIssueAttachments> records = context.Repositories.TaskRepository.GetAllAttachments(conditionalFields, conditionalValue);

                context.SaveChanges();

                return new ResultModel<List<AuditIssueAttachments>>()
                {
                    Status = Status.Success,
                    Message = MessageModel.DeleteSuccess,
                    Data = records
                };
            }
            catch (Exception e)
            {

                context.RollBack();

                return new ResultModel<List<AuditIssueAttachments>>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    Exception = e

                };
            }            
        }

        public List<T_TasksVM> GetSchedulerData(string userName)
        {
            try
            {
                return _common.Select_Data_List<T_TasksVM>("GetSchedulerData", "get_scheduler_data",userName);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TasksVM> GetAllSchedulerData()
        {
            try
            {
                return _common.Select_Data_List<T_TasksVM>("GetAllSchedulerData", "get_scheduler_all_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TaskTimesVM> GetStackHolderById(int selectedId)
        {
            try
            {
                return _common.Select_Data_List<T_TaskTimesVM>("GetById", "get_stackholder_by_id", selectedId.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TasksVM> GetAllStartedTaskData(GridOptions options, string? assigneeUserId)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TasksVM>();
                    support = KendoGrid<T_TasksVM>.GetGridData_5(options, "sp_Select_TaskOpen_Grid", "get_taskopen_summary", "ts.TaskId", assigneeUserId);
                    return support;
                }
                 
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TasksVM> GetTaskAssignee(int id , string? assigneeUserId)
        {
            try
            {
                return _common.Select_Data_List<T_TasksVM>("GetById", "get_assigne_by_id", id.ToString(),assigneeUserId);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_ClientsVm> GetAllClient()
        {
            try
            {
                return _common.Select_Data_List<T_ClientsVm>("Select_Dropdown", "get_all_clients_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TicketVm> GetTicketByTicketId(string selectedId)
        {
            try
            {
                return _common.Select_Data_List<T_TicketVm>("GetById", "get_ticket_by_id", selectedId);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
