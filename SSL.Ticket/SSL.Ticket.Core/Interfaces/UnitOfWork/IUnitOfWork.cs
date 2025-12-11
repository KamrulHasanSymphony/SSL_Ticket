namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUnitOfWorkAdapter Create();

        IUnitOfWorkAdapter CreateAuth();


    }
}
