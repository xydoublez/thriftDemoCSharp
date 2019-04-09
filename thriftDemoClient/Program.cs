using Consul;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Thrift.Protocol;
using Thrift.Transport;
using static com.msunsoft.service.calculator;

namespace thriftDemoClient
{
    static class Program
    {
   
        static void Main()
        {

            using (var consul = new Consul.ConsulClient(c =>
            {
                c.Address = new Uri("http://127.0.0.1:8500");
            }))
            {
                //获取在Consul注册的全部服务
                var services = consul.Agent.Services().Result.Response;
                
                foreach (var s in services.Values)
                {
                    Console.WriteLine($"ID={s.ID},Service={s.Service},Addr={s.Address},Port={s.Port}");
                }
                var service = consul.Agent.Services().Result.Response.Values.Where(
                    p => p.Service.Equals("test-server-rpc", StringComparison.OrdinalIgnoreCase)).First();
                

                /*
             * thrift的使用中一般是一个Server对应一个Processor和一个Transport，如果有多个服务的话，那必须要启动多个Server，
             * 占用多个端口，这种方式显然不是我们想要的，所以thrift为我们提供了复用端口的方式，
             * 通过监听一个端口就可以提供多种服务，
             * 这种方式需要用到两个类：TMultiplexedProcessor和TMultiplexedProtocol。
             
             */
                IPAddress ip = IPAddress.Parse(service.Address);

                using (TcpClient tcpClient = new System.Net.Sockets.TcpClient())
                {
                    tcpClient.Connect(ip, service.Port);
                    using (TTransport transport = new TSocket(tcpClient))
                    {
                        TTransport frameTransport = new TFramedTransport(transport);

                        TProtocol protocol = new TCompactProtocol(frameTransport);
                        //如果服务端使用TMultiplexedProcessor接收处理，客户端必须用TMultiplexedProtocol并且指定serviceName和服务端的一致
                        TMultiplexedProtocol multiplexedProtocol = new TMultiplexedProtocol(protocol, service.Service+ "$com.msunsoft.service.calculator$2.0");
                        Client client = new Client(multiplexedProtocol);
                        //transport.Open();
                        Console.WriteLine(client.add(1, 2));
                    }
                }

            }


            Console.ReadLine();
        }
    }
}
