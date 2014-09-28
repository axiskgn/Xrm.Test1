using System;

namespace Xrm.Test1.DataModel.Repositories.Common
{
    public interface IWriteAcces : IDisposable
    {
         IDictionaryWriteRepository DictionaryRepository { get; }

         IPersonWriteRepository PersonRepository { get; }   

         IResumeWriteRepository ResumeRepository { get; }

         IResumeSourceInfoWriteRepository ResumeSourceInfo { get; }
    }
}