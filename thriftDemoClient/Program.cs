using System;
using Thrift.Protocol;
using Thrift.Transport;
using static msunsoft.service.calculator;

namespace thriftDemoClient
{
    static class Program
    {
   
        static void Main()
        {
            TTransport transport = new TSocket("localhost", 25000);
            TProtocol protocol = new TBinaryProtocol(transport);
            Client client = new Client(protocol);
            transport.Open();
            Console.WriteLine(client.add(1, 2));
            Console.ReadLine();
        }
    }
}
