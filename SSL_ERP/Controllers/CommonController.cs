using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SSL.Common.SSL.Common.Core.Interfaces.Services;
using SSL.CS.SSL.Common.Core.Interfaces.Services.Company;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorService;
using SSL.Sample.SSL.Sample.Services.Vendor;
using SSL_ERP.ExtensionMethods;
using SSL_ERP.Persistence;

namespace SSL_ERP.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ICommonService _commonService;
        private readonly IVendorServices _VendorService;


        private readonly ICompanyInfoService _companyInfoService;


        private readonly ApplicationDbContext _applicationDb;

        //private readonly IBranchProfileService _branchProfileService;

        public CommonController(IVendorServices vendorService,ICommonService commonService, ApplicationDbContext applicationDb, ICompanyInfoService companyInfoService



            )
        {
            _VendorService = vendorService;
            _commonService = commonService;
            _applicationDb = applicationDb;
            _companyInfoService = companyInfoService;
        }


        //SSLAudit


        public JsonResult VendorName()
        {
            var res = _VendorService.GetAllVendorData();
            return Json(res);
            //var result = _companyInfoService.GetAll(null, null);
            //return Ok(result);
        }
        
    }
}
