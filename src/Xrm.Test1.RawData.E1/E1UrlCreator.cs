using Xrm.Test1.Utils.Common;

namespace Xrm.Test1.RawData.E1
{
    public class E1UrlCreator : IUrlCreator
    {
        public string CreateUrl(int offsetPosition, int limit)
        {
            //http://rabota.e1.ru/api/v1/resumes/?limit=1&offset=0&search_key=d99fm2d&auth_token=&q=
            var result =
                string.Format(
                    "http://rabota.e1.ru/api/v1/resumes/?limit={0}&offset={1}&search_key=d99fm2d&auth_token=&q=",
                    limit, offsetPosition);

            return result;
        }
    }
}
