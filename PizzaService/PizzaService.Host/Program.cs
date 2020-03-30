using PizzaService.Contracts;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PizzaService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Pizza Service HOST ***");

            var host = new ServiceHost(typeof(PService));
            host.AddServiceEndpoint(typeof(IPizzaService), new NetTcpBinding(), "net.tcp://localhost:1");
            host.AddServiceEndpoint(typeof(IPizzaService), new BasicHttpBinding(), "http://localhost:2");
            host.AddServiceEndpoint(typeof(IPizzaService), new WSHttpBinding(), "http://localhost:3");
            host.AddServiceEndpoint(typeof(IPizzaService), new NetNamedPipeBinding(), "net.pipe://localhost");


            var smb = new ServiceMetadataBehavior();
            smb.HttpGetUrl = new Uri("http://localhost:3/mex");
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);


            host.Open();
            Console.WriteLine("Host gestartet");
            Console.ReadLine();

            host.Close();
            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }


}

