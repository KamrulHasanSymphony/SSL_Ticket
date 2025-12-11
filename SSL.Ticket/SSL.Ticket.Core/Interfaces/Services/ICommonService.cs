

using SSL.Ticket.SSL.Ticket.Models;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services
{
    public interface ICommonService
    {
        //IList<UserBranch> GetBranch();
       
        //bank
        
        IList<CommonDropDown> GetAllHeaders();
        IList<CommonDropDown> EntryTypes();
        IList<CommonDropDown> DocumentType();  
        List<CommonDropDown> ApplyMethod();
        List<CommonDropDown> TransactionType();    
        List<CommonDropDown> OrderBy();
         
 

    }
}
