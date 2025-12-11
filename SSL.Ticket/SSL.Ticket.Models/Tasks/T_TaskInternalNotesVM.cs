namespace SSL.Ticket.SSL.Ticket.Models.Tasks
{
    public class T_TaskInternalNotesVM
    {
        public int Id { get; set; }

        public int? T_TaskId { get; set; }

        public string ShortNote { get; set; }

        public string Description { get; set; }

        public string AssigneeUserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
