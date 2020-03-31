using AdonisUI;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows;
using WcfPowerChat.Contracts;

namespace WcfPowerChat.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IClient
    {
        public MainWindow()
        {
            InitializeComponent();
            //   SetUi(false);
        }

        public void LoginResponse(bool ok, string msg)
        {
            if (ok)
            {
                ShowText($"Login OK ({msg})");
            }
            else
            {
                MessageBox.Show($"Login fehlgeschlagen:{msg}");
            }
            SetUi(ok);
        }

        private void SetUi(bool ok)
        {
            this.Dispatcher.Invoke(() =>
            {
                nameTb.IsEnabled = !ok;
                loginBtn.IsEnabled = !ok;
                logoutBtn.IsEnabled = ok;

                chatTb.IsEnabled = ok;
                sendBtn.IsEnabled = ok;
                sendImageBtn.IsEnabled = ok;
                sendFileBtn.IsEnabled = ok;
            });

        }

        public void LogoutResponse(bool ok, string msg)
        {
            if (ok)
                ShowText($"Logout OK ({msg})");
            else
                MessageBox.Show($"Login fehlgeschlagen:{msg}");

            SetUi(false);

        }

        public void ShowFile(Stream file)
        {
            throw new System.NotImplementedException();
        }

        public void ShowImage(Stream image)
        {
            throw new System.NotImplementedException();
        }

        public void ShowText(string text)
        {
            chatTb.Dispatcher.Invoke(() => chatLb.Items.Insert(0, text));
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var tcpBind = new NetTcpBinding();

            var cf = new DuplexChannelFactory<IServer>(new InstanceContext(this), tcpBind, new EndpointAddress("net.tcp://localhost:1"));
            server = cf.CreateChannel();

            server.Login(nameTb.Text);
        }

        IServer server = null;

        private void SendText(object sender, RoutedEventArgs e)
        {
            server.SendText(chatTb.Text);
            chatTb.Clear();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            server.Logout();
        }

        public void ShowUsers(IEnumerable<string> users)
        {
            userLb.Dispatcher.Invoke(() => userLb.ItemsSource = users);
        }
    }
}
