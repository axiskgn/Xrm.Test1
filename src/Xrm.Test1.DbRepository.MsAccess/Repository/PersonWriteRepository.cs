using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DbRepository.MsAccess.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Repository
{
    public class PersonWriteRepository: IPersonWriteRepository, IMsAccesRepository
    {
        private readonly OleDbConnection _connection;
        private readonly IPersonFactory _personFactory;

        public PersonWriteRepository(OleDbConnection connection, IPersonFactory personFactory)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (personFactory == null) throw new ArgumentNullException("personFactory");
            _connection = connection;
            _personFactory = personFactory;
        }

        public IPerson Insert(string fio, DateTime birthday, ISex sex, IList<string> photos, bool isSmoke, bool isDriver,
            IMaritalStatus maritialStatus)
        {
            int newId;
            using (var cmd = new OleDbCommand(
                "INSERT INTO Person (fio, birthday, sex, isSmoke, isDriver, maritialStatus) VALUES (@fio, @birthday, @sex, @isSmoke, @isDriver, @maritialStatus)"
                , _connection))
            {

                cmd.Parameters.Add("@fio", OleDbType.VarChar).Value = fio;
                cmd.Parameters.Add("@birthday", OleDbType.Date).Value = birthday;
                cmd.Parameters.Add("@sex", OleDbType.Integer).Value = sex.Id;
                cmd.Parameters.Add("@isSmoke", OleDbType.Boolean).Value = isSmoke;
                cmd.Parameters.Add("@isDriver", OleDbType.Boolean).Value = isDriver;
                cmd.Parameters.Add("@maritialStatus", OleDbType.Integer).Value = maritialStatus.Id;
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT @@IDENTITY";
                newId = (int) cmd.ExecuteScalar();

                foreach (var photo in photos)
                {
                    using (var cmdP = new OleDbCommand(
                            "INSERT INTO PersonPhotos (idPerson, photoLink) VALUES (@idPerson, @photoLink)", _connection))
                    {
                        cmdP.Parameters.Add("@idPerson", OleDbType.Integer).Value = newId;
                        cmdP.Parameters.Add("@photoLink", OleDbType.VarChar).Value = photo;
                        cmdP.ExecuteNonQuery();
                    }
                }
            }

            return _personFactory.CreateEntity(newId, fio, birthday, sex, photos, isSmoke, isDriver, maritialStatus);
        }

        public IPerson AddPhoto(IPerson entity, string newPhoto)
        {
            using (var cmdP = new OleDbCommand("INSERT INTO PersonPhotos (idPerson, photoLink) VALUES (@idPerson, @photoLink)",_connection))
            {
                cmdP.Parameters.Add("@idPerson", OleDbType.Integer).Value = entity.Id;
                cmdP.Parameters.Add("@photoLink", OleDbType.VarChar).Value = newPhoto;
                cmdP.ExecuteNonQuery();
            }
            var newList = new List<string>(entity.Photos) {newPhoto};
            return _personFactory.CreateEntity(entity.Id, entity.Fio, entity.Birthday, entity.Sex, newList,
                entity.IsSmoke, entity.IsDriver, entity.MaritalStatus);
        }

        public void Dispose()
        {

        }

        public bool Check(IList<string> tableList)
        {

            if (!tableList.Contains("Person"))
            {
                using (var command = new OleDbCommand(@"
CREATE TABLE Person(id COUNTER, fio string, birthday datetime, sex int, isSmoke bit, isDriver bit, maritialStatus int)", _connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (!tableList.Contains("PersonPhotos"))
            {
                using (var command = new OleDbCommand(@"
CREATE TABLE PersonPhotos(id COUNTER, idPerson int, photoLink string)", _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}