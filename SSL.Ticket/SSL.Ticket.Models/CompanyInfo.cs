using System.ComponentModel.DataAnnotations;

namespace SSL.Ticket.SSL.Ticket.Models
{
    public class CompanyInfo
    {
        public int CompanyID { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Legal Name")]
        public string CompanyLegalName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Telephone No")]
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        [Display(Name = "Contact Person")]

        public string ContactPerson { get; set; }
        [Display(Name = "Contact Person Designation")]
        public string ContactPersonDesignation { get; set; }
        [Display(Name = "Contact Phone")]

        public string ContactPhone { get; set; }
        [Display(Name = "Contact Person Telephone")]

        public string ContactPersonTelephone { get; set; }
        [Display(Name = "Contact Person Email")]
        public string ContactPersonEmail { get; set; }
        public string? TINNo { get; set; }
        public string? BIN { get; set; }
        [Display(Name = "Vat Registration No")]
        public string? VatRegistrationNo { get; set; }
        public string Comments { get; set; }
        public bool ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedOn { get; set; }
        public string Operation { get; set; }
        public string ErrorMsg { get; set; }
    }
}
