using System.IO;

namespace Xrm.Test1.Utils.Common
{
    public interface IDataGetter
    {
        Stream GetDataStream(string source);
    }
}