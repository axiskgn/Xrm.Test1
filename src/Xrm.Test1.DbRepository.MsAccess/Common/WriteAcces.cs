using System;
using System.Collections.Generic;
using System.Threading;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DataModel.Repositories.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Common
{
    public class WriteAcces:IWriteAcces
    {
        private readonly object _writeAccesLocker;
        private readonly IRepositoryCreator _repositoryCreator;
        private readonly List<IDisposable> _forDisposable = new List<IDisposable>();

        public WriteAcces(object writeAccesLocker, IRepositoryCreator repositoryCreator)
        {
            if (writeAccesLocker == null) throw new ArgumentNullException("writeAccesLocker");
            if (repositoryCreator == null) throw new ArgumentNullException("repositoryCreator");
            _writeAccesLocker = writeAccesLocker;
            _repositoryCreator = repositoryCreator;
        }

        public void Dispose()
        {
            foreach (var disposable in _forDisposable)
            {
                disposable.Dispose();
            }
            _forDisposable.Clear();
            Monitor.Exit(_writeAccesLocker);
        }

        private T CreateRepository<T>()
        {
            var repo = _repositoryCreator.CreateWriteRepository<T>(this);
            _forDisposable.Add((IDisposable)repo);
            return repo;
        }

        public IDictionaryWriteRepository DictionaryRepository
        {
            get
            {
                return CreateRepository<IDictionaryWriteRepository>();
            }
        }

        public IPersonWriteRepository PersonRepository
        {
            get
            {
                return CreateRepository<IPersonWriteRepository>();
            }
        }

        public IResumeWriteRepository ResumeRepository
        {
            get
            {
                return CreateRepository<IResumeWriteRepository>();
            }
        }

        public IResumeSourceInfoWriteRepository ResumeSourceInfo
        {
            get
            {
                return CreateRepository<IResumeSourceInfoWriteRepository>();
            }
        }
    }
}
