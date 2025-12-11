using SSL.Sample.SSL.Sample.Models.ToDo;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class CollaborationVM : RegularVM
    {


        public int Id { get; set; }
        public string CollaborationDetails { get; set; }
        public string CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string CollaborateBy { get; set; }
        public string TicketId { get; set; }
        public string TaskId { get; set; }

        public int ContactPersonId { get; set; }
        public string TransactionDateTime { get; set; }


    }
}
