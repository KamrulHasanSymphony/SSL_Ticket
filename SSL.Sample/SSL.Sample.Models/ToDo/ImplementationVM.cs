using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ImplementationVM
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ProductId { get; set; }

        public int CoordinatorId { get; set; }
        public bool IsActive { get; set; }

        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedFrom { get; set; }

        public string LastUpdateAt { get; set; }

        public string LastUpdateBy { get; set; }

        public string LastUpdateFrom { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Display(Name = "Product Name")]

        public string ProductName { get; set; }


        public List<ImplementationDetailsVM> Details { get; set; }

        public string Operation { get; set; }

        public string CoordinatorName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string CompletionStatus { get; set; }

        public string Text { get; set; }

        public string Progress { get; set; }

        public string Duration { get; set; }
    }


}