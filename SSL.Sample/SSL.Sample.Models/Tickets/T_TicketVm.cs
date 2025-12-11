namespace SSL.Sample.SSL.Sample.Models.Tickets
{
    public class T_TicketVm
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? T_ClientId { get; set; }

        public string StackHolderUserId { get; set; }

        public int? T_ProductId { get; set; }

        public int? T_SourceId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreateOn { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public bool? IsComplete { get; set; }
        public string Operation { get; set; }
    }
}
