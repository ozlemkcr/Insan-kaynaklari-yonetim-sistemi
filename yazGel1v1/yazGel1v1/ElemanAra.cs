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
            elemanAraDataGrid.Rows.Clear();
            searcBarListele();
            ehliyetListele();
            //minimumDeneyimListele();
            //maksimumYasListele();


        }
        private void herseyiListele()
        {
            listelemeFonksiyonu("hersey");
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
                for(int i = count; i<node.kisiEgitimListesi.count(); i++)
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
                    "",
                    "",
                    "",
                    ""
                    );
                    if (renk == "gri")
                    {
                        elemanAraDataGrid.Rows[elemanAraDataGrid.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 252);
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
        private void listelemeFonksiyonu(string sart)
        {
            
            Liste.TreeNode node;
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.count(); i++)
            {
                if (renk == "beyaz")
                {
                    renk = "gri";
                }
                else
                {
                    renk = "beyaz";
                }

                if (sart == "kisiyeGore")
                {
                    AgacListesiStaticClass.agaclistesi.kisiIsmineGoreDugumDondur(textBox1.Text);
                    
                }
                else
                {
                    AgacListesiStaticClass.agaclistesi.treeDugumDondur(i);
                    node = AgacListesiStaticClass.agaclistesi.cekilecekNode;
                    dataGrideVerileriBas(node);
                }
                
                

            }
        }
        private void searcBarListele()
        {
            elemanAraDataGrid.Rows.Clear();
            AgacListesiStaticClass.agaclistesi.kisiIsmineGoreDugumDondur(textBox1.Text);
            for(int i =0; i<AgacListesiStaticClass.agaclistesi.treeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.treeNodeListesi[i]);

            }

        }
        private void minimumDeneyimListele()
        {
            elemanAraDataGrid.Rows.Clear();
            AgacListesiStaticClass.agaclistesi.DeneyimeGoreDugumDondur(Int32.Parse(minimumDeneyimTextbox.Text));
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.treeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.treeNodeListesi[i]);
            }
        }
        private void maksimumYasListele()
        {
            elemanAraDataGrid.Rows.Clear();
            if (maksimumYasTextBox.Text != "")
            {
                AgacListesiStaticClass.agaclistesi.YasaGoreDugumDondur(Int32.Parse(maksimumYasTextBox.Text));
                for (int i = 0; i < AgacListesiStaticClass.agaclistesi.treeNodeListesi.Count; i++)
                {
                    dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.treeNodeListesi[i]);

                }
            }

        }

        private void ehliyetListele()
        {
            elemanAraDataGrid.Rows.Clear();
            AgacListesiStaticClass.agaclistesi.ehliyetDugumDondur(ehilyetComboBox.Text);
            for (int i = 0; i < AgacListesiStaticClass.agaclistesi.treeNodeListesi.Count; i++)
            {
                dataGrideVerileriBas(AgacListesiStaticClass.agaclistesi.treeNodeListesi[i]);
            }
        }
    }
}
