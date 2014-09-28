using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DataModel.Repositories.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Common
{
    public class ReadAcces:IReadAcces
    {
        private readonly IRepositoryCreator _repositoryCreator;

        private readonly List<IDisposable> _forDisposable = new List<IDisposable>();

        public ReadAcces(IRepositoryCreator repositoryCreator)
        {
            if (repositoryCreator == null) throw new ArgumentNullException("repositoryCreator");
            _repositoryCreator = repositoryCreator;
        }

        public void Dispose()
        {
            foreach (var disposable in _forDisposable)
            {
                disposable.Dispose();
            }
            _forDisposable.Clear();
        }

        private T CreateRepository<T>()
        {
            var repo = _repositoryCreator.CreateReadRepository<T>(this);
            _forDisposable.Add((IDisposable) repo);
            return repo;
        }

        public IDictionaryReadRepository DictionaryRepository
        {
            get
            {
                return CreateRepository<IDictionaryReadRepository>();
            }
        }

        public IPersonReadRepository PersonRepository
        {
            get
            {
                return CreateRepository<IPersonReadRepository>();
            }
        }

        public IResumeReadRepository ResumeRepository
        {
            get
            {
                return CreateRepository<IResumeReadRepository>();
            }
        }

        public IResumeSourceInfoReadRepository ResumeSourceInfo
        {
            get
            {
                return CreateRepository<IResumeSourceInfoReadRepository>();
            }
        }
    }
}