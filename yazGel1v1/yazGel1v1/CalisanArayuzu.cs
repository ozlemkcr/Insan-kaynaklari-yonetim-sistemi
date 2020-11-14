using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liste;
namespace yazGel1v1
{
    public partial class CalisanArayuzu : Form
    {
        public CalisanArayuzu()
        {
            InitializeComponent();
        }

        private void CalisanArayuzu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void verileriSil_Click(object sender, EventArgs e)
        {
            
            AgacListesiStaticClass.agaclistesi.delete(adiLabel.Text,telefonLabel.Text);
            AgacListesiStaticClass.agaclistesi.textiGuncelle();
            KullaniciAdiDosyaIslemleri.kullaniciAdiSifreSil(telefonLabel.Text);
            
            MessageBox.Show("Sistemden cikildi!");
            this.Close();


        }

     
        private void kisiBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            ;
            AgacListesiStaticClass.telefonNo = telefonLabel.Text;
            AgacListesiStaticClass.sart = "1";
            AgacListesiStaticClass.kisiAdi = adiLabel.Text;
            EkleGuncelle form = new EkleGuncelle();
            form.ShowDialog();
            
        }

        
        private void verileriArayuzeBas()
        {
            egitimBilgisiDataGrid.Rows.Clear();
            isyeriBilgileriDataGrid.Rows.Clear();
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(AgacListesiStaticClass.telefonNo,"telefon","inOrder");
            Liste.TreeNode node = AgacListesiStaticClass.agaclistesi.cekilecekNode;

            adiLabel.Text = node.kisiAdiSoyadi;
            adresiLabel.Text = node.kisiAdresi;
            telefonLabel.Text = node.kisiTelefonu;
            dogumTarihiLabel.Text = node.kisiDogumTarihi;
            yabanciDilLabel.Text = node.kisiYabanciDil;
            mailLabel.Text = node.kisiMail;
            ehliyetLabel.Text = node.kisiEhliyet;

            for (int i = 0; i < node.kisiEgitimListesi.count(); i++)
            {
                egitimBilgisiDataGrid.Rows.Add(
                    node.kisiEgitimListesi.egitimListesiDugum(i).okulAdi,
                    node.kisiEgitimListesi.egitimListesiDugum(i).okulturu,
                    node.kisiEgitimListesi.egitimListesiDugum(i).bolum,
                    node.kisiEgitimListesi.egitimListesiDugum(i).baslangicTarihi,
                    node.kisiEgitimListesi.egitimListesiDugum(i).bitisTarihi,
                    node.kisiEgitimListesi.egitimListesiDugum(i).notOrtalamasi
                    );
            }
            for (int i = 0; i < node.kisiIsyeriBilgileriListesi.count(); i++)
            {
                isyeriBilgileriDataGrid.Rows.Add(
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdi,
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdresi,
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).gorevi,
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).suresi

                    );
            }
        }

        private void CalisanArayuzu_Activated(object sender, EventArgs e)
        {
            verileriArayuzeBas();
        }

    }  
}
