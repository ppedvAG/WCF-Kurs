using System;
using System.IO;
using WcfPowerChat.Contracts;

namespace WcfPowerChat.Server
{
    class PowerChat : IServer
    {
        public void Login(string name)
        {
            Console.WriteLine($"Login:{name}");
        }

        public void Logout()
        {
            Console.WriteLine($"Logout:");
        }

        public void SendFile(Stream file)
        {
            Console.WriteLine($"SendFile:");
        }

        public void SendImage(Stream image)
        {
            Console.WriteLine($"SendImage:");
        }

        public void SendText(string text)
        {
            Console.WriteLine($"SendText:");
        }
    }
}
