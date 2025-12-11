namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ImplementationDetailStatus
    {
        public int Id { get; set; }


        public string ImplementationId { get; set; }

        public string ImplementationDetailId { get; set; }

        public string Description { get; set; }

        public string Operation { get; set; }

        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateFrom { get; set; }

        public string WorkingDate { get; set; }

        public string Attachment { get; set; }

        public System.Collections.Generic.List<ImplementationDetailStatus> Details { get; set; }
    }
}