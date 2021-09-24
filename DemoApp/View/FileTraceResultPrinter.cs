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
            File.WriteAllText(_fileName, _traceSerializer.Serialize(traceResult));
        }

        public void SetTraceSerializer(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
    }
}