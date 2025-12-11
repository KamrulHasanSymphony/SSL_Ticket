using Microsoft.AspNetCore.Http;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.ExcellUpload
{
    public interface IExcellUploadService : IBaseService<T_TicketVm>
    {        
        //Task<(bool Success, string Message)> ProcessExcelFileAsync(IFormFile file);
    }
}
