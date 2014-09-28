using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.ResumeSource;

namespace Xrm.Test1.DataModel.SimpleFactory
{
    public class ResumeSourceInfoFactory : IResumeSourceInfoFactory
    {
        public IResumeSourceInfo CreateObject(int id, int resumeSourceId, IResumeSource source, string link)
        {
            return new ResumeSourceInfo(id, resumeSourceId, source, link);
        }

        private class ResumeSourceInfo : IResumeSourceInfo
        {
            public ResumeSourceInfo(int id, int resumeSourceId, IResumeSource source, string link)
            {
                Id = id;
                ResumeSourceId = resumeSourceId;
                Source = source;
                Link = link;
            }

            public int Id { get; private set; }
            public int ResumeSourceId { get; private set; }
            public IResumeSource Source { get; private set; }
            public string Link { get; private set; }
        }
    }
}
