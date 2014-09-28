using Xrm.Test1.DataModel.Repositories.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Common
{
    public interface IRepositoryCreator
    {
        T CreateWriteRepository<T>(IWriteAcces wa);
        T CreateReadRepository<T>(IReadAcces ra);
    }
}