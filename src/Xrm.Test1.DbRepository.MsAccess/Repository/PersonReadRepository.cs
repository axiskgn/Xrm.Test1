using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DbRepository.MsAccess.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Repository
{
    public class PersonReadRepository:IPersonReadRepository, IMsAccesRepository
    {
        private readonly OleDbConnection _connection;
        private readonly IPersonFactory _personFactory;
        private readonly IDictionaryReadRepository _dictionaryReadRepository;

        public PersonReadRepository(OleDbConnection connection, IPersonFactory personFactory, IDictionaryReadRepository dictionaryReadRepository)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (personFactory == null) throw new ArgumentNullException("personFactory");
            if (dictionaryReadRepository == null) throw new ArgumentNullException("dictionaryReadRepository");
            _connection = connection;
            _personFactory = personFactory;
            _dictionaryReadRepository = dictionaryReadRepository;
        }

        public IList<IPerson> GetAll()
        {
            var sexItems = _dictionaryReadRepository.GetAll<ISex>();
            var maritialStatusItems = _dictionaryReadRepository.GetAll<IMaritalStatus>();

            //PersonPhotos(id COUNTER, idPerson int, photoLink string)
            var photoList = new Dictionary<int, List<string>>();
            using (var cmd1 = new OleDbCommand("SELECT idPerson, photoLink FROM PersonPhotos", _connection))
            {
                using (var dr1 = cmd1.ExecuteReader())
                {
                    while (dr1 != null && dr1.Read())
                    {
                        var idPerson = (int) dr1["idPerson"];
                        var link = (string) dr1["photoLink"];
                        if (!photoList.ContainsKey(idPerson))
                        {
                            photoList.Add(idPerson, new List<string>());
                        }
                        photoList[idPerson].Add(link);
                    }
                }
            }
            var result = new List<IPerson>();
            // (id COUNTER, fio string, birthday datetime, sex int, isSmoke bit, isDriver bit, maritialStatus int)
            using ( var cmd = new OleDbCommand(@"SELECT id, fio, birthday, sex, isSmoke, isDriver, maritialStatus FROM Person",
                        _connection))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr != null && dr.Read())
                    {
                        var id = (int) dr["id"];
                        var fio = (string) dr["fio"];
                        var birthday = (DateTime) dr["birthday"];
                        var sex = (int) dr["sex"];
                        var isSmoke = (bool) dr["isSmoke"];
                        var isDriver = (bool) dr["isDriver"];
                        var maritialStatus = (int) dr["maritialStatus"];

                        var photos = new List<string>();
                        if (photoList.ContainsKey(id))
                        {
                            photos = photoList[id];
                        }

                        result.Add(_personFactory.CreateEntity(id, fio, birthday,
                            sexItems.FirstOrDefault(t => t.Id == sex),
                            photos, isSmoke, isDriver,
                            maritialStatusItems.FirstOrDefault(t => t.Id == maritialStatus)));
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