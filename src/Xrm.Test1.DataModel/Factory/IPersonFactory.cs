using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;

namespace Xrm.Test1.DataModel.Factory
{

    /// <summary>
    /// Фабрика для человека
    /// </summary>
    public interface IPersonFactory
    {
        /// <summary>
        /// Создание сущности человек (персона)
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="fio">ФИО</param>
        /// <param name="birthday">дата рождения</param>
        /// <param name="sex">пол</param>
        /// <param name="photos">ссылки на фото</param>
        /// <param name="isSmoke">признак курения</param>
        /// <param name="isDriver">права</param>
        /// <param name="maritialStatus">семейное положение</param>
        /// <returns></returns>
        IPerson CreateEntity(int id, string fio, DateTime birthday, ISex sex, IList<string> photos, bool isSmoke,
            bool isDriver, IMaritalStatus maritialStatus);
    }
}