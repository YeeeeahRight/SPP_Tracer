using TracerLib.Model;

namespace TracerLib.Serialization
{
    public interface ITraceSerializer
    {
        string Serialize(TraceResult traceResult);
        TraceResult Deserialize(string traceResult);
        string GetFileExtension();
    }
}