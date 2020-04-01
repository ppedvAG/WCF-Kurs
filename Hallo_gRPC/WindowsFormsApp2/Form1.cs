using Grpc.Net.Client;
using Hallo_gRPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var result = client.SayHello(new HelloRequest() { Name = textBox1.Text, Zahl = (int)numericUpDown1.Value });


            listBox1.Items.Add(result);

            Console.WriteLine(result.Message);

        }
    }
}
