using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using TracerLib.Model;

namespace TracerLib.Logic
{
    public class Tracer : ITracer
    {
        private readonly ConcurrentDictionary<int, ThreadTracer> _threadsTraceMap = new ();

        public void StartTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (!_threadsTraceMap.TryGetValue(threadId, out var threadTracer))
            {
                threadTracer = _threadsTraceMap.GetOrAdd(threadId, new ThreadTracer());
            }
            threadTracer.StartTrace();
        }

        public void StopTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (_threadsTraceMap.TryGetValue(threadId, out var threadTracer))
            {
                threadTracer.StopTrace(new StackTrace().GetFrame(1));
            }
        }

        public TraceResult GetTraceResult()
        {
            var traceResult = new TraceResult();
            foreach (var (threadId, threadTracer) in _threadsTraceMap)
            {
                var threadInfo = new ThreadInfo
                {
                    Id = threadId,
                    Methods = threadTracer.MethodsInfo.ToList()
                };
                traceResult.ThreadsInfo.Add(threadInfo);
            }
            return traceResult;
        }
        
    }
}