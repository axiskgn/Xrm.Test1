using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DataModel.Repositories.Common;
using Xrm.Test1.DbRepository.MsAccess.Properties;
using Xrm.Test1.DbRepository.MsAccess.Repository;

namespace Xrm.Test1.DbRepository.MsAccess.Common
{
    public class MsAccesRootRepository : IRootRepository, IRepositoryCreator
    {
        private readonly IDictionaryFactory _dictionaryFactory;
        private readonly IPersonFactory _personFactory;
        private readonly IResumeSourceInfoFactory _resumeSourceInfoFactory;
        private readonly IResumeFactory _resumeFactory;

        private readonly OleDbConnection _connection;

        private readonly Dictionary<Type, Func<OleDbConnection, IReadAcces, IMsAccesRepository>> _repositoryReadCollection = new Dictionary<Type, Func<OleDbConnection, IReadAcces, IMsAccesRepository>>();
        private readonly Dictionary<Type, Func<OleDbConnection, IWriteAcces, IMsAccesRepository>> _repositoryWriteCollection = new Dictionary<Type, Func<OleDbConnection, IWriteAcces, IMsAccesRepository>>();

        public MsAccesRootRepository(string dbPath, IDictionaryFactory dictionaryFactory, IPersonFactory personFactory, IResumeSourceInfoFactory resumeSourceInfoFactory, IResumeFactory resumeFactory)
        {
            if (dictionaryFactory == null) throw new ArgumentNullException("dictionaryFactory");
            if (personFactory == null) throw new ArgumentNullException("personFactory");
            if (resumeSourceInfoFactory == null) throw new ArgumentNullException("resumeSourceInfoFactory");
            if (resumeFactory == null) throw new ArgumentNullException("resumeFactory");
            if (string.IsNullOrEmpty(dbPath)) throw new ArgumentNullException("dbPath");

            _dictionaryFactory = dictionaryFactory;
            _personFactory = personFactory;
            _resumeSourceInfoFactory = resumeSourceInfoFactory;
            _resumeFactory = resumeFactory;

            if (!File.Exists(dbPath))
            {
                CreateDb(dbPath);
            }

            _connection = CreateConnect(dbPath);

            InitRepoCollection();

            var tableList = GetTableList(_connection);
            using (var wa = GetWriteAcces())
            {
                foreach (var funcCreateRepo in _repositoryWriteCollection)
                {
                    using (var repo = funcCreateRepo.Value(_connection, wa))
                    {
                        repo.Check(tableList);
                    }
                }
            }
        }

        private void InitRepoCollection()
        {
            _repositoryReadCollection.Clear();
            _repositoryWriteCollection.Clear();

            _repositoryReadCollection.Add(typeof(IDictionaryReadRepository),
                (connection, ra) => new DictionaryRepository(connection, _dictionaryFactory));

            _repositoryReadCollection.Add(typeof(IPersonReadRepository),
                (connection, ra) => new PersonReadRepository(connection, _personFactory, ra.DictionaryRepository));

            _repositoryReadCollection.Add(typeof (IResumeSourceInfoReadRepository),
                (connection, ra) =>
                    new ResumeSourceInfoReadRepository(connection, ra.DictionaryRepository, _resumeSourceInfoFactory));

            _repositoryReadCollection.Add(typeof (IResumeReadRepository),
                (connection, ra) =>
                    new ResumeReadRepository(connection, ra.DictionaryRepository, ra.PersonRepository,
                        ra.ResumeSourceInfo, _resumeFactory));



            _repositoryWriteCollection.Add(typeof (IDictionaryWriteRepository),
                (connection, wa) => new DictionaryRepository(connection, _dictionaryFactory));

            _repositoryWriteCollection.Add(typeof(IPersonWriteRepository),
                (connection, wa) => new PersonWriteRepository(connection, _personFactory));

            _repositoryWriteCollection.Add(typeof(IResumeSourceInfoWriteRepository),
                (connection, wa) => new ResumeSourceInfoWriteRepository(connection, _resumeSourceInfoFactory));

            _repositoryWriteCollection.Add(typeof(IResumeWriteRepository),
                (connection, wa) => new ResumeWriteRepository(connection, _resumeFactory));
        }

        private IList<string> GetTableList(OleDbConnection connection)
        {
            var tmp = connection.GetSchema("Tables", new string[] { null, null, null });
            var tableList = new List<string>();
            foreach (DataRow row in tmp.Rows)
            {
                var tn = row["TABLE_NAME"].ToString();
                tableList.Add(tn);
            }
            return tableList;
        }

        private OleDbConnection CreateConnect(string dbPath)
        {
            var connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", dbPath);
            var connection = new OleDbConnection(connStr);

            connection.Open();
            return connection;
        }

        private void CreateDb(string dbFileName)
        {
            var buff = Resources.empty;
            if ((buff) != null)
            {
                using (var fs = new FileStream(dbFileName, FileMode.Create))
                {
                    //Записываем 
                    fs.Write(buff, 0, buff.Length);
                    //Сохраняем данные из буффера на диск
                    fs.Flush();
                }
            }
        }

        public IReadAcces GetReadAcces()
        {
            return new ReadAcces(this);
        }

        private readonly object _writeAccesLocker = new object();

        public IWriteAcces GetWriteAcces()
        {
            if (Monitor.TryEnter(_writeAccesLocker))
            {
                return new WriteAcces(_writeAccesLocker, this);
            }
            return null;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public T CreateWriteRepository<T>(IWriteAcces wa)
        {
            var type = typeof (T);
            if (_repositoryWriteCollection.ContainsKey(type))
            {
                return (T)_repositoryWriteCollection[type](_connection, wa);
            }
            return default(T);
        }

        public T CreateReadRepository<T>(IReadAcces ra)
        {
            var type = typeof(T);
            if (_repositoryReadCollection.ContainsKey(type))
            {
                return (T)_repositoryReadCollection[type](_connection, ra);
            }
            return default(T);
        }
    }
}