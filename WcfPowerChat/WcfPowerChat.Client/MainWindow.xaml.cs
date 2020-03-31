using AdonisUI;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            this.Dispatcher.Invoke(() =>
            {
                var ms = new MemoryStream();
                image.CopyTo(ms);
                ms.Position = 0;

                var img = new Image();
                img.BeginInit();
                img.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                img.Stretch = Stretch.None;
                img.EndInit();

                chatLb.Items.Insert(0, img);
            });
        }

        public void ShowText(string text)
        {
            chatTb.Dispatcher.Invoke(() => chatLb.Items.Insert(0, text));
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var tcpBind = new NetTcpBinding();
            tcpBind.MaxReceivedMessageSize = int.MaxValue;

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

        private void SendImage(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Bilder|*.png|Alles|*.*"
            };

            if (dlg.ShowDialog().Value)
            {
                server.SendImage(File.OpenRead(dlg.FileName));
            }
        }
    }
}
