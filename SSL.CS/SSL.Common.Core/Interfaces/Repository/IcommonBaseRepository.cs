namespace SSL.Common.SSL.Common.Core.Interfaces.Repository
{
    public interface IcommonBaseRepository<TModel> where TModel : class, new()
    {
        //bank
       
        List<TModel> GetAllHeaders();
        List<TModel> EntryTypes();
        List<TModel> DocumentType();


        List<TModel> ApplyMethod();
        List<TModel> OrderBy();
        List<TModel> TransactionType();
 
        List<TModel> GetAllPorts();
       


    }
}
