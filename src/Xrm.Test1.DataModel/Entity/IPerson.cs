using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary;

namespace Xrm.Test1.DataModel.Entity
{
    public interface IPerson
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        int Id { get; }

        /// <summary>
        /// ФИО
        /// </summary>
        string Fio { get; } 

        /// <summary>
        /// Дата рождения
        /// </summary>
        DateTime Birthday { get; }

        /// <summary>
        /// Пол
        /// </summary>
        ISex Sex { get; }

        /// <summary>
        /// Ссылки на фотографии
        /// </summary>
        IList<String> Photos { get; }

        /// <summary>
        /// Курящий
        /// </summary>
        bool IsSmoke { get; }

        /// <summary>
        /// Есть ли водительское удостоверение
        /// </summary>
        bool IsDriver { get; }

        /// <summary>
        /// Семейное положение
        /// </summary>
        IMaritalStatus MaritalStatus { get; }
    }
}