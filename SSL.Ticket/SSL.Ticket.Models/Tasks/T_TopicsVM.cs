using System.ComponentModel.DataAnnotations;

namespace SSL.Ticket.SSL.Ticket.Models.Tasks
{
    public class T_TopicsVM
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        [Display(Name = "Name")]

        public string Name { get; set; }
        public string TopicName { get; set; }
        [Display(Name = "Product")]
        public int T_ProductId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public DateTime? CreateOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool? TopicIsActive { get; set; }
        public string Operation { get; set; }
        public string ErrorMsg { get; set; }
    }
}
