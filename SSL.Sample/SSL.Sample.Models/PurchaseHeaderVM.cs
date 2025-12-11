using System.ComponentModel.DataAnnotations;

namespace SSL.Sample.SSL.Sample.Models
{
    public class PurchaseHeaderVM
    {
        public int PurchaseId { get; set; }
        [Display(Name = "Code")]

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
        [Display(Name = "Trans. Date")]
        public DateTime? TransactionDate { get; set; }
        [Display(Name = "Vendor")]
        public int? AVendorId { get; set; }
        public string VendorName { get; set; }
        [Display(Name = "Address")]
        public string VendorAddress { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
        [Display(Name = "Sub Total")]
        public decimal? SubTotal { get; set; }
        [Display(Name = "Subtotal VAT")]
        public decimal? SubtotalVAT { get; set; }
        [Display(Name = "Total")]
        public decimal? Total { get; set; }
        public List<PurchaseDetailVM> PurchaseHeaderDetails { get; set; }
        public List<string> IDs { get; set; }
    }
    public class PurchaseDetailVM
    {
        public int Id { get; set; }

        public int? APurchaseHeaderId { get; set; }        
        public string AProductId { get; set; }
        [Display(Name = "Product")]
        public string ProductName { get; set; }
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
        public string UOM { get; set; }
        public string? UOMn { get; set; }
        public decimal? UOMc { get; set; }
    }
}
