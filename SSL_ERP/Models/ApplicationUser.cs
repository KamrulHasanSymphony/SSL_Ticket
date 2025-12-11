using Microsoft.AspNetCore.Identity;

namespace SSL_ERP.Models;

public class ApplicationUser : IdentityUser
{
    public string SageUserName { get; set; }
    public string FullName { get; set; }
    public string ProfileName { get; set; }
    public bool IsPushAllow { get; set; }
    public string? Designation { get; set; }
    public int? PFNo { get; set; }



}