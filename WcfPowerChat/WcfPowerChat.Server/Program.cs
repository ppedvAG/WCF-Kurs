using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            tcpBind.MaxReceivedMessageSize = int.MaxValue;
            tcpBind.Security.Mode = SecurityMode.Transport;

            var tcpBind2 = new NetTcpBinding();
            tcpBind2.MaxReceivedMessageSize = int.MaxValue;
            tcpBind2.Security.Mode = SecurityMode.TransportWithMessageCredential;
            tcpBind2.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            var httpBind = new WSDualHttpBinding();
            httpBind.Security.Mode = WSDualHttpSecurityMode.Message;
            httpBind.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;


            host.AddServiceEndpoint(typeof(IServer), tcpBind, "net.tcp://localhost:1");
            host.AddServiceEndpoint(typeof(IServer), tcpBind2, "net.tcp://localhost:3");

            host.AddServiceEndpoint(typeof(IServer), httpBind, "http://localhost:2");

            host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindByThumbprint, "b84271924d3e48b2c77d4be7bf6062c100a73d90");

        
            host.Open();
            Console.WriteLine("Server gestartet");
            Console.ReadLine();
            host.Close();


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}


