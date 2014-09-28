using Xrm.Test1.DataModel.Dictionary;

namespace Xrm.Test1.DataModel.ResumeSource
{
    public interface IResumeSourceInfo
    {
        int Id { get; }
        int ResumeSourceId { get; } 
        IResumeSource Source { get; }
        string Link { get; }
    }
}