using SSL.CS.SSL.Common.Models;

namespace SSL.Common.SSL.Common.Core.Interfaces.Services
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
