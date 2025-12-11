namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class UserClientDetails
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int UserId { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateFrom { get; set; }

        public string ClientName { get; set; }

        public string UserName { get; set; }
    }
}