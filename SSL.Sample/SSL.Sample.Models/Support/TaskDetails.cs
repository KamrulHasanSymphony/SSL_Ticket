using SSL.Sample.SSL.Sample.Models.ToDo;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class TaskDetails : RegularVM
    {

        public int Id { get; set; }
        public string TaskId { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string DueDate { get; set; }
        public bool IsActive { get; set; }
      
    }
}
