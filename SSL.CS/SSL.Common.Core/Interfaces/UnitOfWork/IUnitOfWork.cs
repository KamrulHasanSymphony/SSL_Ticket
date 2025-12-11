namespace SSL.CS.SSL.Common.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUnitOfWorkAdapter Create();

        IUnitOfWorkAdapter CreateAuth();


    }
}
