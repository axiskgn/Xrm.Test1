using System.Collections.Generic;
using Xrm.Test1.DataModel.Entity;

namespace Xrm.Test1.DataModel.Repositories
{
    public interface IPersonReadRepository
    {
        IList<IPerson> GetAll();
    }
}