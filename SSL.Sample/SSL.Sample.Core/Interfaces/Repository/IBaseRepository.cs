using SSL.CS.SSL.Common.Models;
using SSL.Sample.Models;

namespace SSL.Sample.Core.Interfaces.Repository
{
    public interface IBaseRepository<TModel> where TModel: class, new()
    {
        string GetSettingsValue(string[] conditionalFields, string[] conditionalValue);

        List<TModel> GetAll(string[] conditionalFields , string[] conditionalValue, PeramModel vm = null);
        List<TModel> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null);

        int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null);

      
        int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue);

        TModel Insert(TModel model);
        TModel Update(TModel model);

        int Delete(string tableName,  string[] conditionalFields, string[] conditionalValue);

        int Archive(string tableName,  string[] conditionalFields, string[] conditionalValue);

        bool CheckExists(string tableName, string[] conditionalFields,string[] conditionalValue);
        string GetSingleValeByID(string tableName, string ReturnFields, string[] conditionalFields, string[] conditionalValue);

        bool CheckPostStatus(string tableName, string[] conditionalFields, string[] conditionalValue);
        bool CheckPushStatus(string tableName, string[] conditionalFields, string[] conditionalValue);
    }
}
