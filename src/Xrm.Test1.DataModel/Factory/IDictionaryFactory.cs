using Xrm.Test1.DataModel.Dictionary.Common;

namespace Xrm.Test1.DataModel.Factory
{
    public interface IDictionaryFactory
    {
        /// <summary>
        /// Создание элемента словаря
        /// </summary>
        /// <typeparam name="T">Тип элемента словаря</typeparam>
        /// <param name="id">Идентификатор элемента словаря</param>
        /// <param name="name">Название элемента словаря</param>
        /// <returns>Созданный элемент словаря</returns>
        T CreateDictionaryItem<T>(int id, string name) where T : IDictionaryItem;
    }
}