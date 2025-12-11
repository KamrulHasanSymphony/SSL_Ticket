namespace SSL.Common.SSL.Common.Core.Interfaces
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