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
            var result = traceResult.ToString();
            if (_traceSerializer != null)
            {
                result = _traceSerializer.Serialize(traceResult);
            }

            Console.WriteLine(result);
        }
        
        public void SetTraceSerializer(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
    }
}