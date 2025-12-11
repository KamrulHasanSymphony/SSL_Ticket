using System.ComponentModel.DataAnnotations;

namespace SSL.Sample.SSL.Sample.Models
{
    public class PurchaseDetails
    {
        public int Id { get; set; }

        public int? APurchaseHeaderId { get; set; }

        [Display(Name = "Product")]
        public int? AProductId { get; set; }
        [Display(Name = "Quantity")]
        public decimal? Quantity { get; set; }
        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }
        [Display(Name = "Sub-Total")]
        public decimal? SubTotal { get; set; }
        [Display(Name = "VAT Rate")]
        public decimal? VATRate { get; set; }
        [Display(Name = "VAT Amount")]
        public decimal? VATAmount { get; set; }
        [Display(Name = "Total")]
        public decimal? Total { get; set; }
        [Display(Name = "UOM")]
        public string? UOM { get; set; }
        [Display(Name = "UOMn")]
        public string? UOMn { get; set; }
        [Display(Name = "UOMc")]
        public decimal? UOMc { get; set; }
    }
}
