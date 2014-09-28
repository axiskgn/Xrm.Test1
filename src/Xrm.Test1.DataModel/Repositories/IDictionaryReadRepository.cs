using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary.Common;

namespace Xrm.Test1.DataModel.Repositories
{

    /// <summary>
    /// Репозиторий на чтение
    /// </summary>
    public interface IDictionaryReadRepository
    {
        IList<T> GetAll<T>() where T : IDictionaryItem; 
    }
}