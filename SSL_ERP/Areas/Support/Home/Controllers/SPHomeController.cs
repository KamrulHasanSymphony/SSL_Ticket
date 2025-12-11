using Microsoft.AspNetCore.Mvc;

namespace SSL_ERP.Areas.Support.Home.Controllers
{
    public class SPHomeController : Controller
    {
        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View("~/Areas/Support/Home/Views/Index.cshtml");
            }

        }
    }
}
