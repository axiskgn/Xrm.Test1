using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;

namespace Xrm.Test1.DataModel.Repositories
{

    /// <summary>
    /// Редактирование не предполагается (только добавление фотографий)
    /// </summary>
    public interface IPersonWriteRepository
    {
        IPerson Insert(string fio, DateTime birthday, ISex sex, IList<string> photos, bool isSmoke,
            bool isDriver, IMaritalStatus maritialStatus);

        IPerson AddPhoto(IPerson entity, string newPhoto);
    }
}