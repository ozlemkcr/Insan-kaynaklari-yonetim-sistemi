using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Liste;
namespace yazGel1v1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AgacListesiStaticClass.agaclistesi.okut();
            


        }

        private void giris_Click(object sender, EventArgs e)
        {

            if (KullaniciAdiDosyaIslemleri.girisKontrol(kullaniciAdi.Text,sifre.Text))
            {
                CalisanArayuzu form = new CalisanArayuzu();
                AgacListesiStaticClass.telefonNo = kullaniciAdi.Text;
                form.ShowDialog();  
                
            }
            else
            {
                MessageBox.Show("Kullanici adi sifre hatali");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AgacListesiStaticClass.sart = "0";
            EkleGuncelle form = new EkleGuncelle();
            form.ShowDialog();
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ElemanAra form = new ElemanAra();
            form.ShowDialog();
        }
    }
}
