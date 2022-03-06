using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ConsultaCNPJ : Form
    {
        public ConsultaCNPJ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Search();
        }
        private async void Search()
        {
            ConsultaCnpjSerPro.ConsultaCNPJ consultaCNPJ = new ConsultaCnpjSerPro.ConsultaCNPJ();

            var cnpj = textBox1.Text.Trim();

            var dataCnpj = await consultaCNPJ.GetAPI(cnpj);

            if (dataCnpj.ni == null)
            {
                MessageBox.Show("No return");
            }
        }
    }
}
