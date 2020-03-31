using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using WcfPowerChat.Contracts;

namespace WcfPowerChat.Server
{
    class PowerChat : IServer
    {
        static Dictionary<string, IClient> users = new Dictionary<string, IClient>();

        public void Login(string name)
        {
            Console.WriteLine($"Login: {name} ");
            var cb = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.ContainsKey(name))
                cb.LoginResponse(false, $"Der {name} ist bereits in verwendung");
            else
            {
                users.Add(name, cb);
                cb.LoginResponse(true, $"Willkommen {name}");
            }

            ExecuteForAllClient(x => x.ShowUsers(users.Select(y => y.Key)));
        }

        public void Logout()
        {
            var caller = users.FirstOrDefault(x => x.Value == OperationContext.Current.GetCallbackChannel<IClient>());

            Console.WriteLine($"Logout: {caller.Key}");

            users.Remove(caller.Key);

            caller.Value.LogoutResponse(true, "bye bye");

            ExecuteForAllClient(x => x.ShowUsers(users.Select(y => y.Key)));
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
            var caller = users.FirstOrDefault(x => x.Value == OperationContext.Current.GetCallbackChannel<IClient>());

            Console.WriteLine($"SendText: ({caller.Key}) {text}");
            ExecuteForAllClient(x => x.ShowText($"[{DateTime.Now:T}] <{caller.Key}> {text}"));

        }

        private void ExecuteForAllClient(Action<IClient> action)
        {
            foreach (var item in users.ToList())
            {
                try
                {
                    action.Invoke(item.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler: {ex.Message}");
                    users.Remove(item.Key);
                    ExecuteForAllClient(x => x.ShowUsers(users.Select(y => y.Key)));
                }
            }
        }
    }
}
