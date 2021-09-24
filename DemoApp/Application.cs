using System.Collections.Generic;
using System.Threading;
using Application.View;
using TracerLib.Serialization;
using TracerLib.Logic;
using TracerLib.Model;

namespace Application
{
    public class Application
    {
        private static void Main(string[] args)
        {
            var tracer = new Tracer();
            TestEntryClass testEntryClass = new(tracer);
            
            testEntryClass.TestEntryMethod();

            TraceResult traceResult = tracer.GetTraceResult();
            ITraceSerializer xmlSerializer = new XmlTraceSerializer();
            ITraceSerializer jsonSerializer = new JsonTraceSerializer();
            
            ITraceResultPrinter consoleTraceResultPrinter = new ConsoleTraceResultPrinter(xmlSerializer);
            consoleTraceResultPrinter.Print(traceResult);
            consoleTraceResultPrinter.SetTraceSerializer(jsonSerializer);
            consoleTraceResultPrinter.Print(traceResult);
            
            ITraceResultPrinter fileTraceResultPrinter = new FileTraceResultPrinter(xmlSerializer, "result");
            fileTraceResultPrinter.Print(traceResult);
            fileTraceResultPrinter.SetTraceSerializer(jsonSerializer);
            fileTraceResultPrinter.Print(traceResult);
        }
    }

    internal class TestEntryClass
    {
        private readonly ITracer _tracer;
        private readonly InnerTestClass _innerTestClass;
        public TestEntryClass(ITracer tracer)
        {
            _tracer = tracer;
            _innerTestClass = new InnerTestClass(_tracer);
        }

        public void TestEntryMethod()
        {
            _tracer.StartTrace();

            var events = new List<WaitHandle>();

            for (var i = 0; i < 2; i++)
            {   
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(
                    _ =>
                    {
                        _innerTestClass.TestMethod_1();
                        resetEvent.Set();
                    });
                events.Add(resetEvent);
            }
            
            _innerTestClass.TestMethod_1();
            _innerTestClass.TestMethod_1();
            
            
            WaitHandle.WaitAll(events.ToArray());
            _tracer.StopTrace();
        }
    }

    internal class InnerTestClass
    {
        private readonly ITracer _tracer;

        public InnerTestClass(ITracer tracer)
        {
            _tracer = tracer;
        }
        
        public void TestMethod_1()
        {
            _tracer.StartTrace();

            for (var i = 0; i < 2; i++)
            {
                TestMethod_2();
            }
            
            _tracer.StopTrace();
        }
        
        private void TestMethod_2()
        {
            _tracer.StartTrace();

            for (int i = 0; i < 3; i++)
            {
                TestMethod_3();
            }
            
            _tracer.StopTrace();
        }
        
        private void TestMethod_3()
        {
            _tracer.StartTrace();

            for (var i = 0; i < 999999; i++)
            {
                var garbage = 1 + i;
            }
            
            _tracer.StopTrace();
        }
    }
}