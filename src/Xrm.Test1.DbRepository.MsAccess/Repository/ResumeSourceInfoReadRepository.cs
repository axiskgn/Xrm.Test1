using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DataModel.ResumeSource;
using Xrm.Test1.DbRepository.MsAccess.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Repository
{
    public class ResumeSourceInfoReadRepository:IResumeSourceInfoReadRepository, IMsAccesRepository
    {
        private readonly OleDbConnection _connection;
        private readonly IDictionaryReadRepository _dictionaryRepository;
        private readonly IResumeSourceInfoFactory _factory;

        public ResumeSourceInfoReadRepository(OleDbConnection connection, IDictionaryReadRepository dictionaryRepository, IResumeSourceInfoFactory factory)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dictionaryRepository == null) throw new ArgumentNullException("dictionaryRepository");
            if (factory == null) throw new ArgumentNullException("factory");
            _connection = connection;
            _dictionaryRepository = dictionaryRepository;
            _factory = factory;
        }

        public IList<IResumeSourceInfo> GetAll()
        {
            var sourceTypes = _dictionaryRepository.GetAll<IResumeSource>();

            var result = new List<IResumeSourceInfo>();
            // CREATE TABLE ResumeSourceInfo(id COUNTER, sourceType int, inSourceId int, link string)
            using ( var cmd = new OleDbCommand(
                "SELECT id, sourceType, inSourceId, link FROM ResumeSourceInfo", _connection))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr != null && dr.Read())
                    {
                        var id = (int) dr["id"];
                        var sourceType = (int) dr["sourceType"];
                        var inSourceId = (int) dr["inSourceId"];
                        var link = (string) dr["link"];
                        var obj = _factory.CreateObject(id, inSourceId, sourceTypes.First(t => t.Id == sourceType), link);
                        result.Add(obj);
                    }
                }
            }
            return result;
        }

        public void Dispose()
        {
            
        }

        public bool Check(IList<string> tableList)
        {
            return true;
        }
    }
}
