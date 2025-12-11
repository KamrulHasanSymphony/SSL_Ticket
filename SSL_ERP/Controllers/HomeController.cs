using Microsoft.AspNetCore.Mvc;
using SSL_ERP.Persistence;

namespace SSL_ERP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        //private readonly IScreenService _IScreenService;

        public HomeController(
            ApplicationDbContext context
        )
        {
            _context = context;
        }


        //[Authorize]
        public IActionResult Index()
        
        {
            return View();
            //return RedirectToAction();
        }

        public IActionResult KGrid()
        {
            return View();
            //return View("~/Areas/Sample/Views/SampleView/Index.cshtml");
        }

        public IActionResult Login()
        {
            return RedirectToAction("Dashboard");
        }
        public ViewResult Dashboard()
        {
            return View();
        }

       


        // [ChildActionOnly]
        //public PartialViewResult GetSideBar(string roleID)
        //{
        //    return PartialView("_leftSideBar", _IScreenService.GetAll(null, null));
        //}




        // [HttpGet]
        //public async Task<ActionResult<List<Color>>> GetColors()
        //{
        //    try
        //    {
        //        var resultModel = _colorService.GetAll();


        //        if (resultModel.Status == Status.Success)
        //        {
        //            return resultModel.Data;
        //        }

        //        return BadRequest();

        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest(e);
        //    }

        //}


        //[HttpGet]
        //public async Task<ActionResult<List<Color>>> Errors()
        //{
        //    try
        //    {
        //        string userName = User.Identity.Name;

        //        var user = _context.Users.FirstOrDefault(x => x.UserName == userName);

        //        throw new NotImplementedException();

        //    }
        //    catch (Exception e)
        //    {
        //        await e
        //            .AddLogData("E1", "myException")
        //            .LogAsync(ControllerContext.HttpContext);

        //        return BadRequest();
        //    }

        //}

        //[Authorize(Roles = "Admin")]
        //public async Task Exceptions()
        //{
        //    await ExceptionalMiddleware.HandleRequestAsync(HttpContext).ConfigureAwait(false);
        //}
    }
}