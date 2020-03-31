using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfPowerChat.Contracts;

namespace WcfPowerChat.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Power Chat Server ***");

            var host = new ServiceHost(typeof(PowerChat));
            
            var tcpBind = new NetTcpBinding();
            host.AddServiceEndpoint(typeof(IServer), tcpBind, "net.tcp://localhost:1");

            host.Open();
            Console.WriteLine("Server gestartet");
            Console.ReadLine();
            host.Close();


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
