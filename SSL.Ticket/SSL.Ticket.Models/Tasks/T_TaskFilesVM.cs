namespace SSL.Ticket.SSL.Ticket.Models.Tasks
{
    public class T_TaskFilesVM
    {
        public int Id { get; set; }

        public int? T_TaskId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
