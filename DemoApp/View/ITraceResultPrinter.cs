using TracerLib.Model;
using TracerLib.Serialization;

namespace Application.View
{
    public interface ITraceResultPrinter
    {
        void Print(TraceResult traceResult);

        void SetTraceSerializer(ITraceSerializer traceSerializer);
    }
}