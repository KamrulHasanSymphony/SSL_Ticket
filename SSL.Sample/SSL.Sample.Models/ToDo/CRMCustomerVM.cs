using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class CRMCustomerVM : RegularVM
    {
        public int Id { get; set; }

        [Display(Name = "Client Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string BIN { get; set; }
        public string TIN { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [Display(Name = "PO Date")]
        public string PODate { get; set; }
        [Display(Name = "MMC Start Date")]
        public string MMCStartDate { get; set; }
        public string Notes { get; set; }
        public bool IsClient { get; set; }
        public bool MMCStart { get; set; }

        public List<CRMContactPersonVM> CRMContactPersonVMs { get; set; }
        public List<CRMDocumentVM> CRMDocumentVMs { get; set; }
        public List<CRMServiceChargeVM> CRMServiceChargeVMs { get; set; }

        //public string Operation { get; set; }

        public string FilterType { get; set; }
    }
}
