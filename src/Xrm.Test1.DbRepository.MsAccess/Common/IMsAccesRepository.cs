using System;
using System.Collections.Generic;

namespace Xrm.Test1.DbRepository.MsAccess.Common
{
    public interface IMsAccesRepository:IDisposable
    {
        bool Check(IList<string> tableList);
    }
}