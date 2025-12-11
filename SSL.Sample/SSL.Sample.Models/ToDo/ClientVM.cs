using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ClientVM : RegularVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Display(Name = "Full Address")]
        public string FullAddress { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        [Display(Name = "TAX No")]
        public string TAXNo { get; set; }
        [Display(Name = "VAT No")]
        public string VATNo { get; set; }
        [Display(Name = "No. of Employees")]
        public int NumberOfEmployees { get; set; }
        public string Attachment { get; set; }

        public bool IsClient { get; set; }

        public string Operation { get; set; }
        [Display(Name = "Product Name")]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        [Display(Name = "Priority Status")]
        public string PriorityStatus { get; set; }
        public bool IsQualified { get; set; }
        public string Reference { get; set; }
        public string Industry { get; set; }
        public string Qualified { get; set; }




        public List<ClientContactPersonVM> detailClientContactPersonVMs { get; set; }
    }
}
