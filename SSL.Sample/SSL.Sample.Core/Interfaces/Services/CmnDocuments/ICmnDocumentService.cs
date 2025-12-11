using Microsoft.AspNetCore.Http;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Services.CmnDocuments
{
    public interface ICmnDocumentService : IBaseService<CmnDocument>
    {
        //ResultModel<CmnDocument> CreateUpdateDocument(CmnDocument objDocument);
        object Insert(List<IFormFile> files);
    }
}
