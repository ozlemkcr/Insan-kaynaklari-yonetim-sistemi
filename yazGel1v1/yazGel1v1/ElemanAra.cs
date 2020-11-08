using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liste;
namespace yazGel1v1
{
    public partial class ElemanAra : Form
    {
        public ElemanAra()
        {
            InitializeComponent();
        }

        string renk="beyaz";
        private void ElemanAra_Load(object sender, EventArgs e)
        {
            herseyiListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AgacListesiStaticClass.agaclistesi.sart2 = 0;
            AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Clear();
            elemanAraDataGrid.Rows.Clear();
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu("", "");
           
            if (minimumDeneyimTextbox.Text != "")
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(minimumDeneyimTextbox.Text,"minimumDeneyim");
            }
            if (maksimumYasTextBox.Text != "")
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(maksimumYasTextBox.Text, "maximumYas");
            }
            if (ehliyetComboBox.Text != "")
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(ehliyetComboBox.Text, "ehliyet");
            }
            if (ingilizceBilenlerCheckBox.Checked == true)
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu("ingilizce", "ingilizceBilenler");
            }
            if (birdenFazlaDilCheckBox.Checked == true)
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu("", "birdenFazlaDilBilenler");
            }
            if (deneyimsizKisilerCheckbox.Checked == true)
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu("", "deneyimsizKisiler");
            }
            if (lisansTuru.Text != "")
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(lisansTuru.Text.ToLower(), "ogrenimDerecesi");
            }
            if (searcbar.Text != "")
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(searcbar.Text, "searchbar");
            }
            
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);
                if (renk == "beyaz")
                {
                    renk = "gri";
                }
                else
                {
                    renk = "beyaz";
                }
            }
            //searcBarListele();
            //ehliyetListele();
            //minimumDeneyimListele();
            //maksimumYasListele();
            //ingilizceBilenlerListele();
            //herseyiListele();
            //deneyimsizKisiler();
            //ogrenimDerecesi();

        }
        private void herseyiListele()
        {
           
            
        }
        private void dataGrideVerileriBas(Liste.TreeNode node)
        {
            
            string buyuk;
            int count;
            if (node.kisiEgitimListesi.count() < node.kisiIsyeriBilgileriListesi.count())
            {
                buyuk = "isyeri";
                count = node.kisiEgitimListesi.count();
            }
            else
            {
                buyuk = "egitim";
                count = node.kisiIsyeriBilgileriListesi.count();
            }

            if (node.kisiIsyeriBilgileriListesi.count() == 0)
            {
                elemanAraDataGrid.Rows.Add(
               node.kisiAdiSoyadi,
               node.kisiAdresi,
               node.kisiTelefonu,
               node.kisiMail,
               node.kisiDogumTarihi,
               node.kisiYabanciDil,
               node.kisiEhliyet,
               node.kisiEgitimListesi.egitimListesiDugum(0).okulAdi,
               node.kisiEgitimListesi.egitimListesiDugum(0).okulturu,
               node.kisiEgitimListesi.egitimListesiDugum(0).bolum,
               node.kisiEgitimListesi.egitimListesiDugum(0).baslangicTarihi,
               node.kisiEgitimListesi.egitimListesiDugum(0).bitisTarihi,
               node.kisiEgitimListesi.egitimListesiDugum(0).notOrtalamasi,
               "-",
               "-",
               "-",
               "-"
               );
            }
            else
            {
                elemanAraDataGrid.Rows.Add(
                node.kisiAdiSoyadi,
                node.kisiAdresi,
                node.kisiTelefonu,
                node.kisiMail,
                node.kisiDogumTarihi,
                node.kisiYabanciDil,
                node.kisiEhliyet,
                node.kisiEgitimListesi.egitimListesiDugum(0).okulAdi,
                node.kisiEgitimListesi.egitimListesiDugum(0).okulturu,
                node.kisiEgitimListesi.egitimListesiDugum(0).bolum,
                node.kisiEgitimListesi.egitimListesiDugum(0).baslangicTarihi,
                node.kisiEgitimListesi.egitimListesiDugum(0).bitisTarihi,
                node.kisiEgitimListesi.egitimListesiDugum(0).notOrtalamasi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(0).isyeriAdi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(0).isyeriAdresi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(0).gorevi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(0).suresi
                );
            }
            
            if (renk == "gri")
            {
                elemanAraDataGrid.Rows[elemanAraDataGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 252);
            }
            
               


            for (int i = 1; i < count; i++)
            {
                elemanAraDataGrid.Rows.Add(
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                node.kisiEgitimListesi.egitimListesiDugum(i).okulAdi,
                node.kisiEgitimListesi.egitimListesiDugum(i).okulturu,
                node.kisiEgitimListesi.egitimListesiDugum(i).bolum,
                node.kisiEgitimListesi.egitimListesiDugum(i).baslangicTarihi,
                node.kisiEgitimListesi.egitimListesiDugum(i).bitisTarihi,
                node.kisiEgitimListesi.egitimListesiDugum(i).notOrtalamasi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdresi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).gorevi,
                node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).suresi
                );
                if (renk == "gri")
                {
                    elemanAraDataGrid.Rows[elemanAraDataGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 252);
                }
            }

            if (buyuk=="egitim")
            {
                if (count == 0)
                {
                    count++;
                    for (int i = count; i < node.kisiEgitimListesi.count(); i++)
                    {
                        elemanAraDataGrid.Rows.Add(
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        node.kisiEgitimListesi.egitimListesiDugum(i).okulAdi,
                        node.kisiEgitimListesi.egitimListesiDugum(i).okulturu,
                        node.kisiEgitimListesi.egitimListesiDugum(i).bolum,
                        node.kisiEgitimListesi.egitimListesiDugum(i).baslangicTarihi,
                        node.kisiEgitimListesi.egitimListesiDugum(i).bitisTarihi,
                        node.kisiEgitimListesi.egitimListesiDugum(i).notOrtalamasi,
                        "-",
                        "-",
                        "-",
                        "-"
                        );
                        if (renk == "gri")
                        {
                            elemanAraDataGrid.Rows[elemanAraDataGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 252);
                        }
                    }
                }
                

            }
            else
            {
                for (int i = count; i < node.kisiIsyeriBilgileriListesi.count(); i++)
                {
                    elemanAraDataGrid.Rows.Add(
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdi,
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdresi,
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).gorevi,
                    node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).suresi
               
                    );
                    if (renk == "gri")
                    {
                        elemanAraDataGrid.Rows[elemanAraDataGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 252);
                    }
                }
            }

            
        }
        
        private void searcBarListele(string sart)
        {
            
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(searcbar.Text, "searchbar");
            
            for(int i =0; i<AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);

            }

        }
        private void minimumDeneyimListele()
        {
            
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(minimumDeneyimTextbox.Text, "minimumDeneyim");
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);
            }
        }
        private void maksimumYasListele()
        {
            
            if (maksimumYasTextBox.Text != "")
            {
                AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(maksimumYasTextBox.Text, "maximumYas");
                for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
                {
                    dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);

                }
            }

        }
        private void ehliyetListele()
        {
            
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(ehliyetComboBox.Text,"ehliyet");
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);
            }
        }
        private void ingilizceBilenlerListele()
        {
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu("ingilizce", "ingilizceBilenler");
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);
            }
        }
        private void birdenFazlaDilBilenler()
        {
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu("", "birdenFazlaDilBilenler");
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);
            }
        }
        private void deneyimsizKisiler()
        {
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu("", "deneyimsizKisiler");
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);
            }
        }
        private void ogrenimDerecesi()
        {
            if (AgacListesiStaticClass.agaclistesi.temp.Count == 0)
            {

            }
            AgacListesiStaticClass.agaclistesi.filtrelemeFonksiyonu(lisansTuru.Text.ToLower(), "ogrenimDerecesi");
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.filtrelenmisTreeNodeListesi[i]);
            }
        }
    }
}
