using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Models;
using System.ComponentModel.DataAnnotations;


namespace SSL_ERP.Models;

public class LoginResource
{
    public LoginResource()
    {
        CompanyInfos = new List<CompanyInfo>();
    }

    [Required]
    public string UserName { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "MIN_PASSWORD_LENGTH")]
    public string Password { get; set; }

    public string? Message { get; set; }
    public string? returnUrl { get; set; }


    [Required]
    public string CompanyName { get; set; }

    public List<CompanyInfo> CompanyInfos { get; set; }
}