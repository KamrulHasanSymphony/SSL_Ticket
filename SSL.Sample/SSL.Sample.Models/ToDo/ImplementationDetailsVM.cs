using System;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ImplementationDetailsVM
    {
        public int Id { get; set; }
        public int ImplementationId { get; set; }

        public int TaskId { get; set; }

        public int SubTaskId { get; set; }

        public int AssignId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }


        public string TaskName { get; set; }

        public string SubTaskName { get; set; }

        public string AssignIds { get; set; }


        public string AssignedName { get; set; }

        public string Duration { get; set; }

        public string CompletionStatus { get; set; }

        public string Description { get; set; }

        public object CreatedBy { get; set; }

        public object CreatedAt { get; set; }

        public object CreatedFrom { get; set; }

        public object Operation { get; set; }
    }
}
