namespace SSL.Ticket.SSL.Ticket.Models
{
    public class ReportModel
    {
        public string Id { set; get; }
        public string? Code { set; get; }

        public string FromDate { set; get; }

        public string ToDate { set; get; }

        public string? BranchId { set; get; }
        public string? BranchName { set; get; }
        public string? TransactionType { set; get; }
        public string? PaymentCode { set; get; }

        public string BatchNum { set; get; }

        public string? IsReceived { set; get; }

        public string? BankNo { set; get; }
    }
}
