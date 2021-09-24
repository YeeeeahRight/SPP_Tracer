using System;
using System.IO;
using TracerLib.Model;
using TracerLib.Serialization;

namespace Application.View
{
    public class FileTraceResultPrinter : ITraceResultPrinter
    {
        private ITraceSerializer _traceSerializer { get; set; }
        private String _fileName;
        
        public FileTraceResultPrinter(ITraceSerializer traceSerializer, String fileName)
        {
            _traceSerializer = traceSerializer;
            _fileName = fileName;
        }
        
        public void Print(TraceResult traceResult)
        {
            _fileName += _traceSerializer.GetFileExtension();
            var result = traceResult.ToString();
            if (_traceSerializer != null)
            {
                result = _traceSerializer.Serialize(traceResult);
            }
            File.WriteAllText(_fileName, result);
        }

        public void SetTraceSerializer(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
    }
}