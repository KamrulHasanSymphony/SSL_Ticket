using SSL.Sample.SSL.Sample.Models.ToDo;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class TicketHistoryVM :  RegularVM
    {

        public int Id { get; set; }
        public string TicketId { get; set; }
        public string TicketTitle { get; set; }
        public string TicketCode { get; set; }
        public string StartDate { get; set; }
        public string HoldDate { get; set; }
        public string EndDate { get; set; }
        public string TotalDurationTime { get; set; }
        public string Status { get; set; }
    }
}
