using System;

namespace Xrm.Test1.DataModel.Repositories.Common
{
    public interface IRootRepository : IDisposable
    {
        IReadAcces GetReadAcces();
        IWriteAcces GetWriteAcces();
    }
}