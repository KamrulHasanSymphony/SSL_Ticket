namespace SSL.Ticket.SSL.Ticket.Models.Tasks
{
    public class T_PrioritiesVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreateOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
