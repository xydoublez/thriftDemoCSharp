
using System;
using System.Net;
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
            //TServerSocket serverTransport = new TServerSocket(new System.Net.Sockets.TcpListener(IPAddress.Parse("192.168.43.1"), 25000));
            TServerSocket serverTransport = new TServerSocket(25001, 0, false);
            //异步IO，需要使用TFramedTransport，它将分块缓存读取
            var tfactory = new TFramedTransport.Factory();
            //使用高密度二进制协议
            var pfactory = new TCompactProtocol.Factory();
            TMultiplexedProcessor processor = new TMultiplexedProcessor();
            msunsoft.service.calculator.Processor calcProcessor = new msunsoft.service.calculator.Processor(new Server());
            processor.RegisterProcessor("test-server-rpc$com.msunsoft.service.calculator$2.0", calcProcessor);
            TThreadedServer server = new TThreadedServer(processor, serverTransport,tfactory,pfactory);
            Console.WriteLine("Starting server on port 25001 ...");
            server.Serve();
            

        }
    }
}
