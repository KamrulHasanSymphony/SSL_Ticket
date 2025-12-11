using SSL.Sample.SSL.Sample.Core.Interfaces.UnitOfWork;

namespace SSL.Sample.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkAdapter : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }
        void SaveChanges();
        void RollBack();
    }
}
