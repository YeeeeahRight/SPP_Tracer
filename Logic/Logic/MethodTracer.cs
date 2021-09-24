using System.Collections.Concurrent;
using System.Diagnostics;
using MethodInfo = TracerLib.Model.MethodInfo;

namespace TracerLib.Logic
{
    public class MethodTracer
    {
        public Stopwatch Stopwatch { get; } = new ();
        public ConcurrentStack<MethodInfo> Methods { get; } = new ();

        public void AddMethod(MethodInfo methodInfo)
        {
            Methods.Push(methodInfo);
        }
    }
}