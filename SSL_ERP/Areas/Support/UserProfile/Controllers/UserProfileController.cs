using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Ticket;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.UserProfile;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Services.UserProfile;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using StackExchange.Exceptional;
using System.Security.Claims;

namespace SSL_ERP.Areas.Support.UserProfile.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDb;
        private readonly SSL.CS.SSL.Common.Models.DbConfig _dbConfig;
        private readonly ITicketService _ticketService;
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(ApplicationDbContext applicationDb,
              UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SSL.CS.SSL.Common.Models.DbConfig dbConfig, ITicketService ticketService, IUserProfileService userProfileService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDb = applicationDb;
            _dbConfig = dbConfig;
            _ticketService = ticketService;
            _userProfileService = userProfileService;

        }

        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View("~/Areas/Support/UserProfile/Views/Index.cshtml");
            }
        }
        public IActionResult Varification(string id)
        {

            SSL.Ticket.SSL.Ticket.Models.UserProfile vm = new SSL.Ticket.SSL.Ticket.Models.UserProfile();
            vm.UserId = id;
            vm.Operation = "update";
            return View("~/Areas/Support/UserProfile/Views/Varification.cshtml", vm);
        }
        public ActionResult Create()
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.Clear();
                SSL.Ticket.SSL.Ticket.Models.UserProfile vm = new SSL.Ticket.SSL.Ticket.Models.UserProfile();
                vm.Operation = "add";
                return View("~/Areas/Support/UserProfile/Views/CreateEdit.cshtml", vm);
            }
        }
        public ActionResult LoginCreate()
        {
            ModelState.Clear();
            SSL.Ticket.SSL.Ticket.Models.UserProfile vm = new SSL.Ticket.SSL.Ticket.Models.UserProfile();
            vm.Operation = "add";
            return View("~/Areas/Support/UserProfile/Views/LoginCreateEdit.cshtml", vm);
        }
        public IActionResult Edit(string id)
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var user = _userManager.Users.SingleOrDefault(x => x.Id == id);
                SSL.Ticket.SSL.Ticket.Models.UserProfile vm = new SSL.Ticket.SSL.Ticket.Models.UserProfile();
                vm.UserId = user.UserName;
                vm.UserName = user.FullName;
                vm.Operation = "update";
                if (User.IsInRole("Admin"))
                {
                    vm.IsAdmin = true;
                }

                return View("~/Areas/Support/UserProfile/Views/ChangePassword.cshtml", vm);
            }

        }

        public IActionResult Forget()
        {
            //var user = _userManager.Users.SingleOrDefault(x => x.Id == id);
            SSL.Ticket.SSL.Ticket.Models.UserProfile vm = new SSL.Ticket.SSL.Ticket.Models.UserProfile();

            vm.Operation = "update";
            vm.UserId = "";
            vm.Password = "";
            if (User.IsInRole("Admin"))
            {
                vm.IsAdmin = true;
            }

            return View("~/Areas/Support/UserProfile/Views/ForgetPassword.cshtml", vm);

        }

        public IActionResult _index(string userName, string fullName)
        {
            bool isAdmin = User.IsInRole("Admin");
            var user = User.FindFirst(ClaimTypes.UserData);
            string id = string.Empty;
            if (user != null)
            {
                id = user.Value;
            }

            var search = Request.Form["search[value]"].FirstOrDefault();
            var users = _userManager.Users.ToList();

            if (!isAdmin)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    users = users.Where(x => x.Id == id).ToList();
                }
            }

            if (!string.IsNullOrEmpty(userName))
            {
                userName = userName.Trim();
                users = users.Where(x => x.UserName == userName).ToList();
            }
            if (!string.IsNullOrEmpty(fullName))
            {
                fullName = fullName.Trim();
                users = users.Where(x => x.FullName == fullName).ToList();
            }

            var result = users.Count(); // Get the total number of records
            var namesList = users.ToList();
            List<ApplicationUser> data = namesList;

            string draw = Request.Form["draw"].ToString();

            return Ok(new { data, draw, recordsTotal = result, recordsFiltered = result });
        }


        public IActionResult _indexbkp(string userName)
        {
            var search = Request.Form["search[value]"].FirstOrDefault();
            var users = _userManager.Users;

            if (!string.IsNullOrEmpty(userName))
            {
                users = users.Where(u => u.UserName.Contains(userName));
            }
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.UserName.Contains(search));
            }

            var result = users.Count(); // Get the total number of records
            var namesList = users.ToList();
            List<ApplicationUser> data = namesList;

            string draw = Request.Form["draw"].ToString();

            return Ok(new { data, draw, recordsTotal = result, recordsFiltered = result });
        }

        public async Task<ActionResult> CreateEditAsync(SSL.Ticket.SSL.Ticket.Models.UserProfile model)
        {
            ResultModel<SSL.Ticket.SSL.Ticket.Models.UserProfile> result = new ResultModel<SSL.Ticket.SSL.Ticket.Models.UserProfile>();
            try
            {
                //string SageDbName = _dbConfig.SageDbName;
                string SageDbName = "DBLDAT";
                //string DbName = _dbConfig.DbName;
                string DbName = "SSLAuditDB";
                var claims = new List<Claim>
                            {
                                new Claim("Database", DbName),
                                new Claim("SageDatabase", SageDbName),
                             };
                if (model.Operation == "update")
                {
                    var user = await _userManager.FindByNameAsync(model.UserId);

                    if (user == null)
                    {
                        result.Message = "User not found.";
                        return Ok(result);
                    }
                    if (string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.ConfirmPassword))
                    {
                        // Update the user profile without changing the password
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;
                        user.SageUserName = model.SageUserName;
                        user.FullName = model.UserName;

                        var updateResult = await _userManager.UpdateAsync(user);

                        TUserProfile userProfile = new TUserProfile();

                        userProfile.FullName = model.UserName;
                        userProfile.Email = model.Email;
                        userProfile.LogId = model.UserId;
                        userProfile.Password = user.PasswordHash;
                        userProfile.AuthId = user.Id;
                        userProfile.LastUpdateBy = User.Identity.Name;

                        //---------Update User Profile------------//
                        _userProfileService.Update(userProfile);

                        if (!updateResult.Succeeded)
                        {
                            result.Message = "Failed to update user profile.";
                            return Ok(result);
                        }

                        result.Status = Status.Success;
                        result.Message = "User profile updated successfully.";
                        result.Data = model;
                        return Ok(result);
                    }

                    if (User.IsInRole("Admin"))
                    {
                        string token = "";
                        if (user != null)
                        {
                            token = await _userManager.GeneratePasswordResetTokenAsync(user);

                            var changePasswordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);

                            if (!changePasswordResult.Succeeded)
                            {
                                result.Message = "Failed to change the password.";
                                return Ok(result);
                            }
                        }
                    }
                    else
                    {
                        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
                        TUserProfile userProfile = new TUserProfile();

                        userProfile.FullName = model.UserName;
                        userProfile.Email = model.Email;
                        userProfile.LogId = model.UserId;

                        userProfile.Password = model.Password;
                        userProfile.AuthId = user.Id;
                        userProfile.LastUpdateBy = User.Identity.Name;
                        //---------Update User Profile------------//
                        _userProfileService.Update(userProfile);

                        if (!changePasswordResult.Succeeded)
                        {
                            result.Message = "Failed to change the password.";
                            return Ok(result);
                        }
                    }

                    result.Status = Status.Success;
                    result.Message = "Password successfully updated.";
                    result.Data = model;

                    return Ok(result);
                }
                else
                {

                    if (model.Password != model.ConfirmPassword)
                    {
                        result.Message = "Passwords do not match.";
                        return Ok(result);
                    }

                    var _user = new ApplicationUser { UserName = model.UserId, FullName = model.UserName, ProfileName = model.UserName, PhoneNumber = model.PhoneNumber, Email = model.Email, SageUserName = "-", Designation = "-", PFNo = 10, IsPushAllow = true };


                    var _result = await _userManager.CreateAsync(_user, model.Password);

                    TUserProfile userProfile = new TUserProfile();
                    userProfile.FullName = model.UserName;
                    userProfile.Email = model.Email;
                    userProfile.LogId = model.UserId;
                    userProfile.DepartmentId = model.DepartmentId;
                    userProfile.ClientId = model.ClientId;
                    userProfile.Password = model.Password;
                    userProfile.AuthId = _user.Id;
                    userProfile.CreatedBy = User.Identity.Name;

                    //---------Insert User Profile------------//
                    _userProfileService.Insert(userProfile);


                    if (!_result.Succeeded)
                    {
                        foreach (var error in _result.Errors)
                        {
                            result.Message = error.Description;
                            return Ok(result);
                        }
                    }
                    var userClaimsresult = await _userManager.AddClaimsAsync(_user, claims);

                    if (!userClaimsresult.Succeeded)
                    {
                        result.Message = "Failed to add claims.";
                        return Ok(result);
                    }

                    // Ensure the default role exists
                    await EnsureRoleExistsAsync(DefaultRoles.User);
                    // Assign the default role to the new user
                    var addRolesResult = await _userManager.AddToRoleAsync(_user, DefaultRoles.User);
                    if (!addRolesResult.Succeeded)
                    {
                        result.Message = "Failed to assign default role.";
                        return Ok(result);
                    }
                    // Assign the default role to the new user

                    result.Status = Status.Success;
                    result.Message = "Successfully Saved";
                    result.Data = model;

                }
            }
            catch (Exception ex)
            {
                ex.LogAsync(ControllerContext.HttpContext);
            }

            return Ok(result);
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role: {roleName}");
                }
            }
        }

        public ActionResult Profile(string id)
        {
            string userName = User.Identity.Name;
            if (userName == "0" || userName == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var user = _userManager.Users.SingleOrDefault(x => x.Id == id);
                SSL.Ticket.SSL.Ticket.Models.UserProfile vm = new SSL.Ticket.SSL.Ticket.Models.UserProfile();
                vm.UserId = user.UserName;
                vm.UserName = user.FullName;
                vm.PhoneNumber = user.PhoneNumber;
                vm.SageUserName = user.SageUserName;
                vm.Email = user.Email;
                vm.Operation = "update";
                return View("/Areas/Support/UserProfile/Views/EditProfile.cshtml", vm);
            }

        }

        [HttpPost]
        public JsonResult GetUserProfileGrid(GridOptions options)
        {
            var res = _ticketService.GetUserProfileGrid(options);
            var erst = Json(res);
            return erst;
        }

        [HttpGet]

        public IActionResult GetVerifiedById(string userId, string verification)
        {
            bool isVerified = _userProfileService.VerifyUser(userId, verification);

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(verification))
            {
                // Return bad request if either parameter is missing
                return BadRequest("Invalid userId or verification code.");
            }

            // Check if the user exists and the verification code matches

            TUserProfile userProfile = new TUserProfile();
            userProfile.LogId = userId;
            if (isVerified)
            {
                _userProfileService.UpdateUserProfile(userProfile);
                // Return true if the user is verified
                return Json(true);
            }
            else
            {
                // Return false if the verification fails
                return Json(false);
            }
        }
    }
}
