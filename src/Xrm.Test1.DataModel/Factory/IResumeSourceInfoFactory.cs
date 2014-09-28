using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.ResumeSource;

namespace Xrm.Test1.DataModel.Factory
{
    public interface IResumeSourceInfoFactory
    {
        IResumeSourceInfo CreateObject(int id, int resumeSourceId, IResumeSource source, string link);
    }
}