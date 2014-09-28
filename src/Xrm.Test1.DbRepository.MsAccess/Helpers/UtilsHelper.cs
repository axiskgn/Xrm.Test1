using System;
using System.Data.OleDb;

namespace Xrm.Test1.DbRepository.MsAccess.Helpers
{
    public class UtilsHelper
    {
        private readonly OleDbConnection _connection;

        public UtilsHelper(OleDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            _connection = connection;
        }

        public int GetLastId()
        {
            //cmd.CommandText = "SELECT @@IDENTITY";
            using (var cmd = new OleDbCommand("SELECT @@IDENTITY", _connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var id = (int) reader[0];
                            return id;
                        }
                    }
                }
            }
            return -1;
        }
         
    }
}