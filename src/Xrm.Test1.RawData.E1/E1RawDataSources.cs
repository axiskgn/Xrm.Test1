using System;
using System.Collections.Generic;
using Xrm.Test1.RawData.E1.Common;
using Xrm.Test1.RawDataCommon.DataModel;
using Xrm.Test1.RawDataCommon.DataSources;
using Xrm.Test1.Utils.Common;

namespace Xrm.Test1.RawData.E1
{
    public class E1RawDataSources:IRawDataSources
    {
        private readonly IJsonConverter _converter;
        private readonly IDataGetter _dataGetter;
        private readonly IUrlCreator _urlCreator;

        public E1RawDataSources(IJsonConverter converter, IDataGetter dataGetter, IUrlCreator urlCreator)
        {
            if (converter == null) throw new ArgumentNullException("converter");
            if (dataGetter == null) throw new ArgumentNullException("dataGetter");
            if (urlCreator == null) throw new ArgumentNullException("urlCreator");
            _converter = converter;
            _dataGetter = dataGetter;
            _urlCreator = urlCreator;
        }

        private int _pos;
        private int _size=100;

        public void Start()
        {
            _pos = 0;
        }

        public IList<IResumeRaw> GetNextDataBlock()
        {
            if (_pos >= 1000)
            {
                return null;
            }

            var stream = _dataGetter.GetDataStream(_urlCreator.CreateUrl(_pos, _size));
            _pos += _size;
            var result = _converter.Convert(stream);
            return result;
        }
    }
}
