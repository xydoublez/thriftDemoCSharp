using System;
using System.Net;
using System.Net.Sockets;
using Thrift.Protocol;
using Thrift.Transport;
using static msunsoft.service.calculator;

namespace thriftDemoClient
{
    static class Program
    {
   
        static void Main()
        {
            /*
             * thrift的使用中一般是一个Server对应一个Processor和一个Transport，如果有多个服务的话，那必须要启动多个Server，
             * 占用多个端口，这种方式显然不是我们想要的，所以thrift为我们提供了复用端口的方式，
             * 通过监听一个端口就可以提供多种服务，
             * 这种方式需要用到两个类：TMultiplexedProcessor和TMultiplexedProtocol。
             
             */
            IPAddress ip = IPAddress.Parse("192.168.43.1");

            using (TcpClient tcpClient = new System.Net.Sockets.TcpClient())
            {
                tcpClient.Connect(ip, 25001);
                using (TTransport transport = new TSocket(tcpClient))
                {
                    TTransport frameTransport = new TFramedTransport(transport);
                    
                    TProtocol protocol = new TCompactProtocol(frameTransport);
                    //如果服务端使用TMultiplexedProcessor接收处理，客户端必须用TMultiplexedProtocol并且指定serviceName和服务端的一致
                    TMultiplexedProtocol multiplexedProtocol = new TMultiplexedProtocol(protocol, "test-server-rpc$com.msunsoft.service.calculator$2.0");
                    Client client = new Client(multiplexedProtocol);
                    //transport.Open();
                    Console.WriteLine(client.add(1, 2));
                }
            }

           
            Console.ReadLine();
        }
    }
}
