using System;

namespace Xrm.Test1.DataModel.Dictionary.Common
{

    /// <summary>
    /// Атрибут для внесения метаданных в интерфейсы словарей
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class DictionaryIdTypeAttribute:Attribute
    {

        public DictionaryIdTypeAttribute(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Идентификатор словаря
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Название словаря
        /// </summary>
        public string Name { get; private set; }
    }
}
