using System.ComponentModel.DataAnnotations;

namespace SSL.Sample.SSL.Sample.Models
{
    public class AProductVM
    {
        public int ProductId { get; set; }
        [Display(Name = "Code")]
        public string ProductCode { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Display(Name = "Is Active")]
        public bool? IsActive { get; set; }
        [Display(Name = "Date")]
        public DateTime? OpeningDate { get; set; }
        [Display(Name = "Quantity")]
        public int? OpeningQuantity { get; set; }
        [Display(Name = "UOM")]
        public string UOM { get; set; }
        [Display(Name = "VAT Rate")]
        public decimal VATRate { get; set; }
    }
}
