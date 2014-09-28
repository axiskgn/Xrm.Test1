using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Factory;

namespace Xrm.Test1.DbRepository.MsAccess.Helpers
{
    public class DictionaryTypeHelper
    {
        private readonly OleDbConnection _connection;
        private readonly IDictionaryFactory _dictionaryFactory;

        public DictionaryTypeHelper(OleDbConnection connection, IDictionaryFactory dictionaryFactory)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dictionaryFactory == null) throw new ArgumentNullException("dictionaryFactory");
            _connection = connection;
            _dictionaryFactory = dictionaryFactory;
        }

        public IList<IDictionaryType> GetAll()
        {
            var result = new List<IDictionaryType>();

            using (var cmd = new OleDbCommand("SELECT * FROM DictionaryType", _connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var id = (int) reader["id"];
                            var name = (string) reader["name"];
                            var obj = _dictionaryFactory.CreateDictionaryItem<IDictionaryType>(id, name);
                            result.Add(obj);
                        }
                    }
                }
            }
            return result;
        }

        public IDictionaryType GetItem(int id)
        {
            using (var cmd = new OleDbCommand("SELECT * FROM DictionaryType WHERE id=@id ", _connection))
            {
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var name = (string) reader["name"];
                            var obj = _dictionaryFactory.CreateDictionaryItem<IDictionaryType>(id, name);
                            return obj;
                        }
                    }
                }
            }
            return null;
        }

        public IDictionaryType Insert(int id, string name)
        {
            using (var cmd = new OleDbCommand("INSERT INTO DictionaryType (id, name) VALUES (@id,@name)", _connection))
            {
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = name;
                cmd.ExecuteNonQuery();
            }
            return _dictionaryFactory.CreateDictionaryItem<IDictionaryType>(id,name);
        }

    }
}
