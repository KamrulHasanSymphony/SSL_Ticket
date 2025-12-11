namespace SSL.CS.SSL.Common.Models
{
    public class UserManuInfo
    {
        public int Id { get; set; }
        public string? Modul { get; set; }
        public string? Node { get; set; }
        public string? Url { get; set; }
        public string? ActionName { get; set; }
        public string? ControllerName { get; set; }
        public bool? IsActive { get; set; }



    }

    public class SubmanuList
    {
        public int Id { get; set; }
        public string? Node { get; set; }
        public string? Url { get; set; }
        public string? ActionName { get; set; }
        public bool IsActive { get; set; }
        public bool IsInsert { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }

    }
}
