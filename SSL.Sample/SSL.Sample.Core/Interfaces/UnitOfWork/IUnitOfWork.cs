namespace SSL.Sample.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUnitOfWorkAdapter Create();

        IUnitOfWorkAdapter CreateAuth();


    }
}
