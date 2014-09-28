using System.Collections.Generic;
using Xrm.Test1.RawDataCommon.DataModel;

namespace Xrm.Test1.RawDataCommon.DataSources
{
    public interface IRawDataSources
    {
        void Start();
        IList<IResumeRaw> GetNextDataBlock();
    }
}