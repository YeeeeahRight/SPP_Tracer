using System;
using TracerLib.Model;
using TracerLib.Serialization;

namespace Application.View
{
    public class ConsoleTraceResultPrinter : ITraceResultPrinter
    {
        private ITraceSerializer _traceSerializer;
        
        public ConsoleTraceResultPrinter(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
        
        public void Print(TraceResult traceResult)
        {
            Console.WriteLine(_traceSerializer.Serialize(traceResult));
        }
        
        public void SetTraceSerializer(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
    }
}