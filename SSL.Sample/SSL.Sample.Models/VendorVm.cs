using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models
{
    public class VendorVm
    {

        [Display(Name = "Active Status")]
        public string ActiveStatus { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public int BranchId { get; set; }
        [Display(Name = "Business Code")]
        public string BusinessCode { get; set; }
        [Display(Name = "Business Type")]
        public string BusinessType { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Display(Name = "Person Designation")]
        public string ContactPersonDesignation { get; set; }
        [Display(Name = "Person Email")]
        public string ContactPersonEmail { get; set; }
        [Display(Name = "Person Telephone")]
        public string ContactPersonTelephone { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool ExportAll { get; set; }
        [Display(Name = "Fax No")]
        public string FaxNo { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
        public string Info5 { get; set; }
        [Display(Name = "Is Active")]
        public string IsActive { get; set; }

        [Display(Name = "Is Register")]
        public string IsRegister { get; set; }
        [Display(Name = "Is Turnover")]
        public string IsTurnover { get; set; }
        public string IsVDSWithHolder { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedOn { get; set; }
        [Display(Name = "NID No")]
        public string NIDNo { get; set; }
        public string Operation { get; set; }
        public string SearchField { get; set; }
        public string SearchValue { get; set; }
        public string SelectTop { get; set; }
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }
        [Display(Name = "Date From")]
        public string StartDateFrom { get; set; }
        [Display(Name = "Start Date from")]
        public string StartDateTime { get; set; }
        [Display(Name = "Start Date To")]
        public string StartDateTo { get; set; }
        [Display(Name = "Telephone No")]
        public string TelephoneNo { get; set; }
        [Display(Name = "TIN No")]
        public string TINNo { get; set; }
        [Display(Name = "VATRegistration No")]
        public string VATRegistrationNo { get; set; }
        [Display(Name = "VDS Percent")]
        public decimal VDSPercent { get; set; }
        [Display(Name = "Vendor Code")]
        public string VendorCode { get; set; }
        [Display(Name = "Vendor Group Name")]
        public string VendorGroup { get; set; }
        [Display(Name = "Vendor Group")]
        public string VendorGroupID { get; set; }
        public int VendorID { get; set; }
        public List<string> VendorIDs { get; set; }
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }
        [Display(Name = "Country")]
        public string CountryName { get; set; }
    }
}
