using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.Factory;

namespace Xrm.Test1.ConsoleTest.DataModel.ObjectFactory
{
    public class PersonFactory : IPersonFactory
    {
        public IPerson CreateEntity(int id, string fio, DateTime birthday, ISex sex, IList<string> photos, bool isSmoke, bool isDriver,
            IMaritalStatus maritialStatus)
        {
            return new Person(id,fio,birthday,sex,photos,isSmoke,isDriver,maritialStatus);
        }

        private class Person:IPerson
        {
            public Person(int id,string fio, DateTime birthday, ISex sex, IList<string> photos, bool isSmoke, bool isDriver, IMaritalStatus maritalStatus)
            {
                Id = id;
                Fio = fio;
                Birthday = birthday;
                Sex = sex;
                Photos = photos;
                IsSmoke = isSmoke;
                IsDriver = isDriver;
                MaritalStatus = maritalStatus;
            }

            public int Id { get; private set; }
            public string Fio { get; private set; }
            public DateTime Birthday { get; private set; }
            public ISex Sex { get; private set; }
            public IList<string> Photos { get; private set; }
            public bool IsSmoke { get; private set; }
            public bool IsDriver { get; private set; }
            public IMaritalStatus MaritalStatus { get; private set; }
        }
    }
}
