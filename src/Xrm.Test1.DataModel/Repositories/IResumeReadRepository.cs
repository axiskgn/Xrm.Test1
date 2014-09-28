using System.Collections.Generic;
using Xrm.Test1.DataModel.Entity;

namespace Xrm.Test1.DataModel.Repositories
{
    public interface IResumeReadRepository
    {
        IList<IResume> GetAll();
    }
}