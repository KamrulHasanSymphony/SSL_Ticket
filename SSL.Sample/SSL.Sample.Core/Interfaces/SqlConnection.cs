namespace SSL.Sample.Core.Interfaces
{
    internal class SqlConnection
    {
        private object connectionString;

        public SqlConnection(object connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}