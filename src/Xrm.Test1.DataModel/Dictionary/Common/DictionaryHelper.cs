using System;
using System.Collections.Generic;
using System.Linq;

namespace Xrm.Test1.DataModel.Dictionary.Common
{

    /// <summary>
    /// Утилитарный класс работы со словарями
    /// </summary>
    public class DictionaryHelper
    {
        /// <summary>
        /// Получение списка интерфейсов наследованных от базового для словарей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetTypesNeedForGeneration()
        {
            var typeName = typeof(IDictionaryItem).Name;

            return (AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes().Where(t => t.IsInterface),
                    (assembly, type) => new { assembly, type })
                .Where(@t1 => @t1.type.GetInterface(typeName) != null)
                .Select(@t1 => @t1.type)).ToList();
        }
    }
}
