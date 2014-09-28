using System.Collections.Generic;
using Xrm.Test1.DataModel.ResumeSource;

namespace Xrm.Test1.DataModel.Repositories
{
    public interface IResumeSourceInfoReadRepository
    {
        IList<IResumeSourceInfo> GetAll();
    }
}