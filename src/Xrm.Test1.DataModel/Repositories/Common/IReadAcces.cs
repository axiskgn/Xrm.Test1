using System;

namespace Xrm.Test1.DataModel.Repositories.Common
{
    public interface IReadAcces: IDisposable
    {
        IDictionaryReadRepository DictionaryRepository { get; }

        IPersonReadRepository PersonRepository { get; }

        IResumeReadRepository ResumeRepository { get; }

        IResumeSourceInfoReadRepository ResumeSourceInfo { get; }
    }
}