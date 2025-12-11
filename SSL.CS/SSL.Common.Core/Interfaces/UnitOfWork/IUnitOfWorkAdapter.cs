using SSL.Common.SSL.Common.Core.Interfaces.UnitOfWork;

namespace SSL.CS.SSL.Common.Core.Interfaces.UnitOfWork

{
    public interface IUnitOfWorkAdapter : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }
        void SaveChanges();
        void RollBack();
    }
}
