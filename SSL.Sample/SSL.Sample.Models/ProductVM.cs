using System.ComponentModel.DataAnnotations;

namespace SSL.Sample.SSL.Sample.Models
{
    public class ProductVM
    {
        //public ProductVM();
        [Display(Name = "Active Status")]

        public string ActiveStatus { get; set; }
        public decimal AITRate { get; set; }
        public decimal ATVRate { get; set; }
        [Display(Name = "Banderol")]

        public string Banderol { get; set; }
        public int BOMId { get; set; }
        public int BranchId { get; set; }
        public string ButtonName { get; set; }
        [Display(Name = "Category")]

        public string CategoryID { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public decimal CDRate { get; set; }
        public decimal CnFRate { get; set; }
        public string Comments { get; set; }
        public decimal CostPrice { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string DARNo { get; set; }
        public List<ProductNameVM> Details { get; set; }
        public bool ExportAll { get; set; }
        public decimal FixedVATAmount { get; set; }
        public string GenericName { get; set; }
        public decimal HPSRate { get; set; }
        [Display(Name = "HSCode No")]
        public string HSCodeNo { get; set; }
        public string isActive { get; set; }
        public string IsChild { get; set; }
        public string IsCodeUpdate { get; set; }
        public string IsConfirmed { get; set; }
        public string IsExempted { get; set; }
        public string IsExpireDate { get; set; }
        public string IsFixedAIT { get; set; }
        public bool IsFixedAITChecked { get; set; }
        public string IsFixedAT { get; set; }
        public bool IsFixedATChecked { get; set; }
        public string IsFixedCD { get; set; }
        public bool IsFixedCDChecked { get; set; }
        public string IsFixedOtherSD { get; set; }
        public string IsFixedRD { get; set; }
        public bool IsFixedRDChecked { get; set; }
        public string IsFixedSD { get; set; }
        public bool IsFixedSDChecked { get; set; }
        public string IsFixedVAT { get; set; }
        public string IsFixedVAT1 { get; set; }
        public bool IsFixedVAT1Checked { get; set; }
        public bool IsFixedVatM { get; set; }
        public string IsFixedVATRebate { get; set; }
        public string IsHouseRent { get; set; }
        public string IsRaw { get; set; }
        public string IsSample { get; set; }
        public string IsTransport { get; set; }
        public string IsVDS { get; set; }
        public string IsZeroVAT { get; set; }
        [Display(Name = "Item No")]
        public int ItemNo { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedOn { get; set; }
        public string MasterProductItemNo { get; set; }
        [Display(Name = "NBR Price")]
        public decimal NBRPrice { get; set; }
        public string NonStock { get; set; }
        [Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }
        [Display(Name = "Opening TotalCost")]
        public decimal OpeningTotalCost { get; set; }
        public string Operation { get; set; }
        public string Option1 { get; set; }
        public decimal Packetprice { get; set; }
        [Display(Name = "Code")]
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public List<string> ProductIDs { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        public List<ProductStockVM> ProductStocks { get; set; }
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }
        public decimal RDRate { get; set; }
        public decimal RebatePercent { get; set; }
        public string ReportType { get; set; }
        public string[] retResult { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal SD { get; set; }
        public decimal SDRate { get; set; }
        public string SearchField { get; set; }
        public string SearchItemNo { get; set; }
        public string SearchProductCode { get; set; }
        public string SearchValue { get; set; }
        public string SelectTop { get; set; }
        [Display(Name = "Serial No")]
        public string SerialNo { get; set; }
        public string ShortName { get; set; }
        public decimal Stock { get; set; }
        public string TargetId { get; set; }
        [Display(Name = "TDS Code")]
        public string TDSCode { get; set; }
        [Display(Name = "Toll Charge")]
        public decimal TollCharge { get; set; }
        [Display(Name = "Toll Product")]
        public string TollProduct { get; set; }
        public string TotalCount { get; set; }
        public string Trading { get; set; }
        public decimal TradingMarkUp { get; set; }
        public decimal TradingSaleSD { get; set; }
        public decimal TradingSaleVATRate { get; set; }
        public decimal TransactionHoldDate { get; set; }
        public string TransactionType { get; set; }
        public decimal TVARate { get; set; }
        public decimal TVBRate { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "UOM")]
        public string UOM { get; set; }
        public string UOM2 { get; set; }
        public decimal UOMConversion { get; set; }
        public decimal VATRate { get; set; }
        public decimal VATRate2 { get; set; }
        public decimal VATRate3 { get; set; }
        public decimal VDSRate { get; set; }
        public decimal Volume { get; set; }
        public string VolumeUnit { get; set; }
        public decimal WastageTotalValue { get; set; }
    }
    public class ProductNameVM
    {
        //public ProductNameVM();

        public int Id { get; set; }
        public string ItemNo { get; set; }
        public string ProductName { get; set; }
    }
    public class ProductStockVM
    {
        //public ProductStockVM();

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string Comments { get; set; }
        public decimal CurrentStock { get; set; }
        public string ItemNo { get; set; }
        public int StockId { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal StockValue { get; set; }
        public decimal WastageTotalQuantity { get; set; }
    }
}
