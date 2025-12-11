namespace SSL.Ticket.SSL.Ticket.Models.Tasks
{
    public class T_TaskAssignesVM
    {
        public int Id { get; set; }

        public int? T_TaskId { get; set; }

        public int? T_TicketId { get; set; }

        public string AssigneeUserId { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
