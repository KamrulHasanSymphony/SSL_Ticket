using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Ticket;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.ExcellUpload;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Product;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.ExcellUpload
{
    public class ExcellUploadService : IExcellUploadService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ITicketRepository _ticketRepository;
        //private readonly ITaskRepository _taskRepository;

        public ExcellUploadService(IUnitOfWork unitOfWork, ITicketRepository ticketRepository, ITaskRepository taskRepository)
        {
            _unitOfWork = unitOfWork;
            //_ticketRepository = ticketRepository;
            //_taskRepository = taskRepository;
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketVm> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TicketVm>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TicketVm>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketVm> Insert(T_TicketVm model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketVm> InsertActive(T_TicketVm model)
        {
            throw new NotImplementedException();
        }

        //public async Task<(bool Success, string Message)> ProcessExcelFileAsync(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return (false, "No file selected");

        //    try
        //    {
        //        using var stream = new MemoryStream();
        //        await file.CopyToAsync(stream);
        //        using var package = new ExcelPackage(stream);

        //        // 1️⃣ Tickets Sheet (first sheet)
        //        var ticketSheet = package.Workbook.Worksheets[0];
        //        int ticketRowCount = ticketSheet.Dimension.Rows;
        //        var ticketMap = new Dictionary<string, int>(); // Map TicketCode -> TicketId

        //        for (int row = 2; row <= ticketRowCount; row++) // skip header
        //        {
        //            var ticketVm = new T_TicketVm
        //            {
        //                CompanyId = int.Parse(ticketSheet.Cells[row, 1].Text),
        //                Code = ticketSheet.Cells[row, 2].Text,
        //                TaskCode = ticketSheet.Cells[row, 3].Text,
        //                Title = ticketSheet.Cells[row, 4].Text,
        //                Description = ticketSheet.Cells[row, 5].Text,
        //                T_ClientId = string.IsNullOrEmpty(ticketSheet.Cells[row, 6].Text) ? null : (int?)int.Parse(ticketSheet.Cells[row, 6].Text),
        //                StackHolderUserId = ticketSheet.Cells[row, 7].Text,
        //                T_RatingId = string.IsNullOrEmpty(ticketSheet.Cells[row, 8].Text) ? null : (int?)int.Parse(ticketSheet.Cells[row, 8].Text),
        //                T_TopicId = string.IsNullOrEmpty(ticketSheet.Cells[row, 9].Text) ? null : (int?)int.Parse(ticketSheet.Cells[row, 9].Text),
        //                T_PriorityId = string.IsNullOrEmpty(ticketSheet.Cells[row, 10].Text) ? null : (int?)int.Parse(ticketSheet.Cells[row, 10].Text),
        //                T_StatusId = string.IsNullOrEmpty(ticketSheet.Cells[row, 11].Text) ? null : (int?)int.Parse(ticketSheet.Cells[row, 11].Text),
        //                T_ProductId = string.IsNullOrEmpty(ticketSheet.Cells[row, 12].Text) ? null : (int?)int.Parse(ticketSheet.Cells[row, 12].Text),
        //                T_SourceId = string.IsNullOrEmpty(ticketSheet.Cells[row, 13].Text) ? null : (int?)int.Parse(ticketSheet.Cells[row, 13].Text),
        //                CreateDate = string.IsNullOrEmpty(ticketSheet.Cells[row, 14].Text) ? (DateTime?)null : DateTime.Parse(ticketSheet.Cells[row, 14].Text),
        //                DueDate = string.IsNullOrEmpty(ticketSheet.Cells[row, 15].Text) ? (DateTime?)null : DateTime.Parse(ticketSheet.Cells[row, 15].Text),
        //                CreatedBy = ticketSheet.Cells[row, 16].Text,
        //                CreateOn = DateTime.Now,
        //                IsComplete = false,
        //                DepartmentId = string.IsNullOrEmpty(ticketSheet.Cells[row, 17].Text) ? 0 : int.Parse(ticketSheet.Cells[row, 17].Text),
        //                TicketTypeId = string.IsNullOrEmpty(ticketSheet.Cells[row, 18].Text) ? 0 : int.Parse(ticketSheet.Cells[row, 18].Text)
        //            };

        //            // Insert Ticket
        //            var insertedTicket = _ticketRepository.Insert(ticketVm);
        //            ticketMap[ticketVm.Code] = insertedTicket.Id;
        //        }

        //        // 2️⃣ Tasks Sheet (second sheet)
        //        var taskSheet = package.Workbook.Worksheets[1];
        //        int taskRowCount = taskSheet.Dimension.Rows;

        //        for (int row = 2; row <= taskRowCount; row++)
        //        {
        //            var ticketCode = taskSheet.Cells[row, 4].Text; // Column with Ticket Code
        //            if (!ticketMap.ContainsKey(ticketCode)) continue;

        //            var taskVm = new T_TasksVM
        //            {
        //                CompanyId = int.Parse(taskSheet.Cells[row, 1].Text),
        //                Code = taskSheet.Cells[row, 2].Text,
        //                Title = taskSheet.Cells[row, 3].Text,
        //                Description = taskSheet.Cells[row, 5].Text,
        //                T_TicketId = ticketMap[ticketCode],
        //                T_SourceId = int.Parse(taskSheet.Cells[row, 6].Text),
        //                T_TopicId = int.Parse(taskSheet.Cells[row, 7].Text),
        //                T_PriorityId = int.Parse(taskSheet.Cells[row, 8].Text),
        //                T_StatusId = int.Parse(taskSheet.Cells[row, 9].Text),
        //                ProgressInPercent = 0,
        //                StartDate = DateTime.Parse(taskSheet.Cells[row, 10].Text),
        //                StartTime = TimeSpan.Parse(taskSheet.Cells[row, 11].Text).ToString(@"hh\:mm\:ss"),
        //                RequiredTime = int.Parse(taskSheet.Cells[row, 12].Text),
        //                CreatedOn = DateTime.Now,
        //                CreatedBy = taskSheet.Cells[row, 13].Text,
        //                IsComplete = false,
        //                DepartmentId = int.Parse(taskSheet.Cells[row, 14].Text),
        //                TaskTypeId = int.Parse(taskSheet.Cells[row, 15].Text)
        //            };

        //            // Insert Task
        //            _taskRepository.Insert(taskVm);
        //        }

        //        return (true, "Tickets and Tasks uploaded successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, "Error processing Excel file: " + ex.Message);
        //    }
        //}

        public ResultModel<T_TicketVm> Update(T_TicketVm model)
        {
            throw new NotImplementedException();
        }
    }
}
