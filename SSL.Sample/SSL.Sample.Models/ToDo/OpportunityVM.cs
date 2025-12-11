using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class OpportunityVM : RegularVM
    {
        public int Id { get; set; }
        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        public string Attachment { get; set; }
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public string Operation { get; set; }
    }
}
