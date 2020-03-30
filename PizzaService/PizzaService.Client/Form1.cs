using PizzaService.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaService.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //    cf = new ChannelFactory<IPizzaService>(new NetTcpBinding(), "net.tcp://localhost:1");
            cf = new ChannelFactory<IPizzaService>(new BasicHttpBinding(), "http://localhost.fiddler:2");
            //cf = new ChannelFactory<IPizzaService>(new WSHttpBinding(), "http://localhost:3");
            //cf = new ChannelFactory<IPizzaService>(new NetNamedPipeBinding(), "net.pipe://localhost");
            checkedListBox1.Format += (s, e) => e.Value = $"{((Pizza)e.ListItem).Name} ({((Pizza)e.ListItem).Preis:c})";
        }

        ChannelFactory<IPizzaService> cf;
        private void button1_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();

            IPizzaService client = cf.CreateChannel();

            foreach (var item in client.GetPizzaListe())
            {
                checkedListBox1.Items.Add(item);
            }
            ICommunicationObject com = client as ICommunicationObject;
            com.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var best = new Bestellung() { Bestelldatum = dateTimePicker1.Value };
            best.Besteller = "Fred";
            best.Pizzen = checkedListBox1.CheckedItems.Cast<Pizza>().ToList();

            IPizzaService client = cf.CreateChannel();
            try
            {
                client.SendBestellung(best);
            }
            catch (FaultException<BestellError> err)
            {
                MessageBox.Show(err.Reason.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IPizzaService client = cf.CreateChannel();
            dataGridView1.DataSource = client.GetBestellungen();
        }
    }
}
