using Xrm.Test1.DataModel.Dictionary.Common;

namespace Xrm.Test1.DataModel.Repositories
{

    /// <summary>
    /// Репозиторий на запись
    /// </summary>
    public interface IDictionaryWriteRepository
    {
        T Insert<T>(string name) where T : IDictionaryItem;  
    }
}