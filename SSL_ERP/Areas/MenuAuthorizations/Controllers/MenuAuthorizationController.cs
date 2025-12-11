using Microsoft.AspNetCore.Mvc;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;

namespace SSL_ERP.Areas.MenuAuthorizations.Controllers
{
    public class MenuAuthorizationController : Controller
    {
        public IActionResult Index()
        {

            UserRoleVM userRole = new UserRoleVM()
            {
                Operation = "add"                
            };
            return View("~/Areas/MenuAuthorizations/Views/Role.cshtml", userRole);
        }
    }
}
