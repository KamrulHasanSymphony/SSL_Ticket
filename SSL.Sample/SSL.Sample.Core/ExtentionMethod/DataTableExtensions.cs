using Newtonsoft.Json;
using System.Data;
namespace SSL.Sample.Core.ExtentionMethod
{
    public static class DataTableExtensions
    {
        public static string GetCommaSeparated(this DataTable source, string columnName)
        {
            var list = source.AsEnumerable().Select(r => r[columnName].ToString());
            string value = string.Join("','", list);

            return "'" + value + "'";
        }


        public static List<T> ToList<T>(this DataTable source)
        {
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(source));
        }

        public static T ToModel<T>(this DataTable source) where T : class, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(source)).FirstOrDefault();
        }
    }
}
