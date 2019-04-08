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
            IPAddress ip = IPAddress.Parse("192.168.43.1");
            using (TcpClient tcpClient = new System.Net.Sockets.TcpClient())
            {
                tcpClient.Connect(ip, 25000);
                using (TTransport transport = new TSocket(tcpClient))
                {

                    TProtocol protocol = new TBinaryProtocol(transport);
                    Client client = new Client(protocol);
                    //transport.Open();
                    Console.WriteLine(client.add(1, 2));
                }
            }

           
            Console.ReadLine();
        }
    }
}
