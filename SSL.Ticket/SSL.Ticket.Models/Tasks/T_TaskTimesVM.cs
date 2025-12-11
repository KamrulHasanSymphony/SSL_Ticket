namespace SSL.Ticket.SSL.Ticket.Models.Tasks
{
    public class T_TaskTimesVM
    {
        public int Id { get; set; }

        public int? T_TaskId { get; set; }
        public string AssigneeUserId { get; set; }
        public int? T_TicketId { get; set; }

        public string Comments { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? StopTime { get; set; }

        public string Duration { get; set; }

        public string StartedBy { get; set; }

        public string StartStatus { get; set; }
        public string IsStackHolder { get; set; }
    }
}
