using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DataModel.ResumeSource;
using Xrm.Test1.DbRepository.MsAccess.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Repository
{
    public class ResumeSourceInfoWriteRepository:IResumeSourceInfoWriteRepository, IMsAccesRepository
    {
        private readonly OleDbConnection _connection;
        private readonly IResumeSourceInfoFactory _factory;

        public ResumeSourceInfoWriteRepository(OleDbConnection connection, IResumeSourceInfoFactory factory)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (factory == null) throw new ArgumentNullException("factory");
            _connection = connection;
            _factory = factory;
        }

        public IResumeSourceInfo Insert(int resumeSourceId, IResumeSource source, string link)
        {
            int newId;
            using ( var cmd = new OleDbCommand(
                        "INSERT INTO ResumeSourceInfo (sourceType, inSourceId, link) VALUES (@sourceType, @inSourceId, @link)",
                        _connection))
            {
                cmd.Parameters.Add("@sourceType", OleDbType.Integer).Value = source.Id;
                cmd.Parameters.Add("@inSourceId", OleDbType.Integer).Value = resumeSourceId;
                cmd.Parameters.Add("@link", OleDbType.VarChar).Value = link;
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT @@IDENTITY";
                newId = (int) cmd.ExecuteScalar();
            }

            return _factory.CreateObject(newId, resumeSourceId, source, link);
        }

        public void Dispose()
        {
            
        }

        public bool Check(IList<string> tableList)
        {
            if (!tableList.Contains("ResumeSourceInfo"))
            {
                using (var command = new OleDbCommand(@"
CREATE TABLE ResumeSourceInfo(id COUNTER, sourceType int, inSourceId int, link string)", _connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
