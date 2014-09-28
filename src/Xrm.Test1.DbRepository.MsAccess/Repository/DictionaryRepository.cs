using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using Xrm.Test1.DataModel.Dictionary.Common;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DbRepository.MsAccess.Common;
using Xrm.Test1.DbRepository.MsAccess.Helpers;

namespace Xrm.Test1.DbRepository.MsAccess.Repository
{
    public class DictionaryRepository : IDictionaryReadRepository, IDictionaryWriteRepository, IMsAccesRepository
    {
        private readonly OleDbConnection _connection;
        private readonly IDictionaryFactory _dictionaryFactory;
        private DictionaryTypeHelper _helper;

        public DictionaryRepository(OleDbConnection connection, IDictionaryFactory dictionaryFactory)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dictionaryFactory == null) throw new ArgumentNullException("dictionaryFactory");
            _connection = connection;
            _dictionaryFactory = dictionaryFactory;
            _helper = new DictionaryTypeHelper(connection,dictionaryFactory);
        }
        
        public IList<T> GetAll<T>() where T : IDictionaryItem
        {
            var result = new List<T>();

            using (var cmd = new OleDbCommand("SELECT * FROM Dictionary WHERE type = @type", _connection))
            {
                cmd.Parameters.Add("@type", OleDbType.Integer).Value = GetDictionaryTypeId(typeof (T));
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr != null && dr.Read())
                    {
                        var id = (int) dr["id"];
                        var name = (string) dr["name"];
                        result.Add(_dictionaryFactory.CreateDictionaryItem<T>(id, name));
                    }
                }
            }
            return result;
        }

        public T Insert<T>(string name) where T : IDictionaryItem
        {
            int newId;
            using (var cmd = new OleDbCommand("INSERT INTO Dictionary (type, name) VALUES (@type, @name)", _connection))
            {
                cmd.Parameters.Add("@type", OleDbType.Integer).Value = GetDictionaryTypeId(typeof (T));
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = name;
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT @@IDENTITY";
                newId = (int) cmd.ExecuteScalar();
            }
            return _dictionaryFactory.CreateDictionaryItem<T>(newId, name);
        }

        private static bool _isChecked = false;

        public bool Check(IList<string> tableList)
        {
            // да тут можно было ещё один уровень изоляции навешать, но для данного приложения лишнее

            if (!_isChecked)
            {
                if (!tableList.Contains("DictionaryType"))
                {
                    using (var command = new OleDbCommand(@"
CREATE TABLE DictionaryType(id int, name string)", _connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                if (!tableList.Contains("Dictionary"))
                {
                    using (var command = new OleDbCommand(@"
CREATE TABLE Dictionary(id COUNTER, type int, name string)", _connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                var goodDictionaryTypes = _helper.GetAll();

                var helper = new DictionaryHelper();
                var dictionaryInterfacesList = helper.GetTypesNeedForGeneration();
                foreach (var dictionaryType in dictionaryInterfacesList)
                {
                    var attrList = dictionaryType.GetCustomAttributes(typeof (DictionaryIdTypeAttribute), true);

                    foreach (var attr in attrList)
                    {
                        var okAtr = attrList.First() as DictionaryIdTypeAttribute;
                        if (okAtr != null)
                        {
                            if (goodDictionaryTypes.All(t => t.Id != okAtr.Id))
                            {
                                _helper.Insert(okAtr.Id, okAtr.Name);
                            }
                        }
                    }
                }
                _isChecked = true;
            }
            return true;
        }

        private int GetDictionaryTypeId(Type type)
        {
            var attrList = type.GetCustomAttributes(typeof(DictionaryIdTypeAttribute), true);

            if (!attrList.Any())
            {
                throw new Exception("Класс не содержит описания типа словаря");
            }
            var okAtr = attrList.First() as DictionaryIdTypeAttribute;
            if (okAtr != null)
            {
                return  okAtr.Id;
            }
            throw new Exception("Fatal error");
        }

        public void Dispose()
        {
            
        }
    }
}
