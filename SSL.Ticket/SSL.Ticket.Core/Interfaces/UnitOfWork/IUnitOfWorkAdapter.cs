namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkAdapter : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }
        void SaveChanges();
        void RollBack();
    }
}
