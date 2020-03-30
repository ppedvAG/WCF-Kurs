using HalloWCF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloWCF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new CalculatorSoapClient();
            var result = client.Add((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            label1.Text = $"Result: {result}";
        }
    }
}
