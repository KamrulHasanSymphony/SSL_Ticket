namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class AttendanceVM : RegularVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Intime { get; set; }
        public string Outtime { get; set; }
        public string CurrentTime{ get; set; }

        public string Operation { get; set; }


    }
}
