using SSL.Sample.SSL.Sample.Models.ToDo;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class TaskHistoryVM : RegularVM
    {
        public int Id { get; set; }
        public string TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskCode { get; set; }
        public string StartDate { get; set; }
        public string HoldDate { get; set; }
        public string EndDate { get; set; }
        public string TotalDurationTime { get; set; }
        public string Status { get; set; }

    }
}
