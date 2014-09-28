using System.IO;
using System.Net;
using Xrm.Test1.Utils.Common;

namespace Xrm.Test1.Utils.DataGetter
{
    public class HttpDataGetter : IDataGetter
    {
        private static int _num = 0;

        public Stream GetDataStream(string source)
        {
            var request = WebRequest.Create(source);
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            return stream;
            //_num++;
            //var file = new FileStream(string.Format("d:\\{0}.dump",_num), FileMode.OpenOrCreate);
            
            //stream.CopyTo(file);

            //file.Close();
            //stream.Close();
            //file = new FileStream(string.Format("d:\\{0}.dump", _num), FileMode.Open);


            //return file;
        }
    }
}
