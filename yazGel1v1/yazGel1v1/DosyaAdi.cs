using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazGel1v1
{
    public partial class DosyaAdi : Form
    {
        public DosyaAdi()
        {
            InitializeComponent();
        }

        public static string dosyaAditext;
        private void button1_Click(object sender, EventArgs e)
        {
            dosyaAditext = textBox1.Text;
            this.Close();
        }
    }
}
