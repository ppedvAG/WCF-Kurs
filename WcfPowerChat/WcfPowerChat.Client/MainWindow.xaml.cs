using AdonisUI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
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
            try
            {
                // var tcpBind = new NetTcpBinding();
                // tcpBind.MaxReceivedMessageSize = int.MaxValue;
                // tcpBind.Security.Mode = SecurityMode.Transport;
                //
                //
                // var cf = new DuplexChannelFactory<IServer>(new InstanceContext(this), tcpBind, new EndpointAddress("net.tcp://192.168.178.56:1"));
                // cf.Credentials.Windows.ClientCredential.UserName = "Fred";
                // cf.Credentials.Windows.ClientCredential.Password = "123456";


                 var tcpBind = new NetTcpBinding();
                 tcpBind.MaxReceivedMessageSize = int.MaxValue;
                 tcpBind.Security.Mode = SecurityMode.TransportWithMessageCredential;
                tcpBind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;



                var dual = new WSDualHttpBinding();
                dual.Security.Mode = WSDualHttpSecurityMode.Message;
                dual.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
                dual.Security.Message.NegotiateServiceCredential = true;

                //var cf = new DuplexChannelFactory<IServer>(new InstanceContext(this), dual, new EndpointAddress("http://192.168.178.56:2")); 


                EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity("RootCA");
                EndpointAddress address = new EndpointAddress(new Uri("net.tcp://DESKTOP-MFV9PIV:3"), identity);

                var cf = new DuplexChannelFactory<IServer>(new InstanceContext(this), tcpBind, address);
                cf.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "db56b195af62c2e65ae9243deac64eba8a34ed73");
                cf.Credentials.Windows.ClientCredential.UserName = "Fred";
                cf.Credentials.Windows.ClientCredential.Password = "123456";
                cf.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;


                server = cf.CreateChannel();

                server.Login(nameTb.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler:{ex.Message}");
                Debugger.Break();
            }
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
