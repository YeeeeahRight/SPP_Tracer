using TracerLib.Model;

namespace TracerLib.Logic
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }
}