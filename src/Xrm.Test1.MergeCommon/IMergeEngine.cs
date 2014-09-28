using Xrm.Test1.RawDataCommon.DataModel;

namespace Xrm.Test1.MergeCommon
{
    public interface IMergeEngine
    {
        void AddResume(IResumeRaw resumeRaw, string sourceName);

        int NewPersonCount { get; }
        int NewResumeCount { get; }
    }
}