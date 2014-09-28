using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.ResumeSource;

namespace Xrm.Test1.DataModel.Repositories
{
    public interface IResumeSourceInfoWriteRepository
    {
        IResumeSourceInfo Insert(int resumeSourceId, IResumeSource source, string link);
    }
}