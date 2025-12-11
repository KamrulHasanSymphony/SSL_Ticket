using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Services.Product
{
    public interface IProductService : IBaseService<AProductVM>
    {
        List<AProductVM> GetAllCategoryData();
        GridEntity<AProductVM> GetAllProductData(GridOptions options);
        List<UOMVm> GetAllUomData();
    }
}
