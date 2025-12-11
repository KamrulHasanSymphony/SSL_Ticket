namespace SSL.Sample.Models
{
    public class TestHeaderVM : BaseModel
    {

        public string GLAccount { get; set; }
        public string TransDate { get; set; }

        public List<TestDetailVM> TestDetails { get; set; }

        public TestHeaderVM()
        {
            TestDetails = new List<TestDetailVM>();
            Audit = new Audit();
        }

    }
}