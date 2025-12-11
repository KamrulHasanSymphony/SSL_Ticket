using SSL.Sample.SSL.Sample.Models.ToDo;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class OrganizationVM : RegularVM
    {
        public string Id { set; get; }
        public string Organization { set; get; }

        public string ClientOrOrganization { get; set; }
        public string ConcernPerson { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }


        public string Operation { get; set; }
    }
}
