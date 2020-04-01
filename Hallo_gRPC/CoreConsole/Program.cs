using Grpc.Net.Client;
using Hallo_gRPC;
using System;

namespace CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var result = client.SayHello(new HelloRequest() { Name = "Fred", Zahl = 12 });

            Console.WriteLine(result.Message);


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
