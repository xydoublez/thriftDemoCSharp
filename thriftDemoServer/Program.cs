
using System;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using thriftDemoServer;

namespace testThriftIFace
{
    static class Program
    {
   
        static void Main()
        {
            TServerSocket serverTransport = new TServerSocket(25000, 0, false);
            msunsoft.service.calculator.Processor processor = new msunsoft.service.calculator.Processor(new Server());
            TServer server = new TSimpleServer(processor, serverTransport);
            Console.WriteLine("Starting server on port 25000 ...");
            server.Serve();

        }
    }
}
