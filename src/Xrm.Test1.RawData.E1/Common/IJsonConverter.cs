using System.Collections.Generic;
using System.IO;
using Xrm.Test1.RawDataCommon.DataModel;

namespace Xrm.Test1.RawData.E1.Common
{
    public interface IJsonConverter
    {
        IList<IResumeRaw> Convert(Stream stream);
    }
}