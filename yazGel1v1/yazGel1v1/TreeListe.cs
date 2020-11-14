using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Xml;
using yazGel1v1;

namespace Liste
{
    class TreeListe
    {
        string dosya_yolu = Environment.CurrentDirectory + @"\ikveriTabani.txt";
        TreeNode root;
        int id = 0;
        int sayac;
        int sayac2;
        public TreeNode cekilecekNode;
        public List<TreeNode> filtrelenmisTreeNodeListesi = new List<TreeNode>();
        public List<TreeNode> temp = new List<TreeNode>();
        public List<TreeNode> yeniListe = new List<TreeNode>();

        public void ekle(string kisiAdiSoyadi_, string kisiAdresi_, string kisiTelefonu_, string kisiMail_, string kisiDogumTarihi_, string kisiYabanciDil_, string kisiEhliyet_, EgitimBilgileriListe kisiEgitimListesi_, IsyeriBilgileriListesi kisiIsyeriBilgileriListesi)
        {
            string id = idDondur();
            TreeNode eklenecek = new TreeNode( id, kisiAdiSoyadi_,  kisiAdresi_,  kisiTelefonu_,  kisiMail_,  kisiDogumTarihi_,  kisiYabanciDil_,  kisiEhliyet_,kisiEgitimListesi_,kisiIsyeriBilgileriListesi);
            if (root == null)
            {
                root = eklenecek;
            }
            else
            {
                TreeNode temp = root;
                TreeNode temp2;
                while (true)
                {
                    temp2 = temp;
                    if (string.Compare(temp.kisiAdiSoyadi, kisiAdiSoyadi_) == -1)
                    {
                        temp = temp.sag;
                        if (temp == null)
                        {
                            temp2.sag = eklenecek;
                            break;
                        }
                    }
                    else
                    {
                        temp = temp.sol;
                        if (temp == null)
                        {
                            temp2.sol = eklenecek;
                            break;
                        }
                    }
                }
            }
            

        }
        private void print(TreeNode node)
        {
            
            if (node != null)
            {
                print(node.sol);
                sayac++;
                print(node.sag);
            }
        }
        /////// tüm liseleme
        private void dugumListeleIndexIcin(TreeNode node,int aranan)
        {
            if (node != null)
            {
                dugumListeleIndexIcin(node.sol,aranan);
                if(aranan == sayac)
                {
                    cekilecekNode = node;
                   
                }
                sayac++;
                dugumListeleIndexIcin(node.sag,aranan);
            }
            
        }
        public void treeDugumDondur(int aranan)
        {
            sayac = 0;
            dugumListeleIndexIcin(root,aranan);
        }
        public string turkceMetniIngilizceyeCevir(string metin)
        {

            char[] türkcekarakterler = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] ingilizce = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };
            for (int i = 0; i < türkcekarakterler.Length; i++)
            {

                metin = metin.Replace(türkcekarakterler[i], ingilizce[i]);

            }
            return metin;
        }
        private bool ehliyetKontrolEt(string textinIcindeki,string nodeunIcindeki)
        {
            int sayax = 0;
            List<string> textinIcindekiListesi = new List<string>();
            List<string> nodeunIcindekiListesi = new List<string>();

            string temp = "";
            //texinIcindekileri listeye ekler
            for(int i=0; i<textinIcindeki.Length; i++)
            {
                
                if (textinIcindeki[i] == ',' )
                {
                    textinIcindekiListesi.Add(temp);
                    temp = "";
                }
                else
                {
                    temp = temp + textinIcindeki[i];
                }
                
            }
            //son elemanı ekler
            textinIcindekiListesi.Add(temp);
            temp = "";

            //nodun ıcındekılerı lısteye ekler
            for (int i = 0; i < nodeunIcindeki.Length; i++)
            {
                
                if (nodeunIcindeki[i] == ',' )
                {
                    nodeunIcindekiListesi.Add(temp);
                    temp = "";
                }
                else
                {
                    temp = temp + nodeunIcindeki[i];
                }
                
            }
            //son elemanı ekler
            nodeunIcindekiListesi.Add(temp); 
            
            //karsılastırma yapar
            for (int i =0; i<textinIcindekiListesi.Count; i++)
            {
                for(int j=0; j<nodeunIcindekiListesi.Count; j++)
                {
                    if (textinIcindekiListesi[i] == nodeunIcindekiListesi[j])
                    {
                        sayax++;
                    }
                }
            }

            //sayacla textınlıstesının elemansayısı esıtse true doner
            if (textinIcindekiListesi.Count() == sayax)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void filtrele(TreeNode node,string aranan,string sart)
        {

            if (sart == "telefon")
            {
                if (node.kisiTelefonu == aranan)
                {
                    cekilecekNode = node;
                }
            }
            else if (sart == "searchbar")
            {
                if (node.kisiAdiSoyadi == aranan)
                {
                    temp.Add(node);
                }
            }
            else if (sart == "minimumDeneyim")
            {

                sayac2 = 0;
                //Deneyimleri toplayacak dongu
                for (int i = 0; i < node.kisiIsyeriBilgileriListesi.count(); i++)
                {
                    sayac2 = sayac2 + Convert.ToInt32(node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).suresi);

                }
                //Aranandan buyukse veya esitse listeye atilacak
                if (sayac2 >= Convert.ToInt32(aranan))

                {
                    temp.Add(node);
                }
            }
            else if (sart == "maximumYas")
            {
                if (Convert.ToInt32(node.kisiDogumTarihi) >= 2020 - Convert.ToInt32(aranan))
                {
                    temp.Add(node);
                }
            }
            else if (sart == "ehliyet")
            {
                if (ehliyetKontrolEt(aranan, node.kisiEhliyet))
                {
                    temp.Add(node);
                }
            }
            else if (sart == "ingilizceBilenler")
            {
                if (node.kisiYabanciDil.ToLower().Contains("ingilizce"))
                {
                    temp.Add(node);
                }

            }
            else if (sart == "birdenFazlaDilBilenler")
            {
                int virgulsayaci = 0;
                if (turkceMetniIngilizceyeCevir(node.kisiYabanciDil.ToLower()).Contains("turkce"))
                {

                    for (int i = 0; i < node.kisiYabanciDil.Length; i++)
                    {
                        if (node.kisiYabanciDil[i] == ',')
                        {
                            virgulsayaci++;
                        }
                    }
                    if (virgulsayaci > 1)
                    {
                        temp.Add(node);
                    }
                }
                else
                {
                    for (int i = 0; i < node.kisiYabanciDil.Length; i++)
                    {
                        if (node.kisiYabanciDil[i] == ',')
                        {
                            virgulsayaci++;
                        }
                    }
                    if (virgulsayaci > 0)
                    {
                        temp.Add(node);
                    }
                }
                //if (node.kisiTelefonu == aranan)
                //{
                //    cekilecekNode = node;
                //}
            }
            else if (sart == "deneyimsizKisiler")
            {
                if (node.kisiIsyeriBilgileriListesi.count() == 0)
                {
                    temp.Add(node);
                }
            }
            else if (sart == "ogrenimDerecesi")
            {
                for (int i = 0; i < node.kisiEgitimListesi.count(); i++)
                {
                    if (turkceMetniIngilizceyeCevir(node.kisiEgitimListesi.egitimListesiDugum(i).okulturu.ToLower()) == turkceMetniIngilizceyeCevir(aranan.ToLower()))
                    {
                        temp.Add(node);
                    }
                }
            }
            else
            {
                temp.Add(node);
            }

        }
        private TreeNode DugumDondur(TreeNode node, string aranan,string sart,string siralamaBicimi)
        {
            if (node != null)
            {
                if (siralamaBicimi=="inOrder")
                {
                    DugumDondur(node.sol, aranan, sart, siralamaBicimi);
                    filtrele(node, aranan, sart);
                    DugumDondur(node.sag, aranan, sart, siralamaBicimi);

                }else if (siralamaBicimi == "preOrder")
                {

                    filtrele(node, aranan, sart);
                    DugumDondur(node.sol, aranan, sart, siralamaBicimi);
                    DugumDondur(node.sag, aranan, sart, siralamaBicimi);

                }
                else
                {
                    
                    DugumDondur(node.sol, aranan, sart, siralamaBicimi);
                    DugumDondur(node.sag, aranan, sart, siralamaBicimi);
                    filtrele(node, aranan, sart);

                }
                
            }
            return null;
        }
        public int sart2;
        public void filtrelemeFonksiyonu(string aranan, string sart,string siralamaBicimi)
        {
            //butona bastiginda clear atilacak
            temp.Clear();
            
            DugumDondur(root,aranan,sart, siralamaBicimi);
            if (sart2 == 0)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    filtrelenmisTreeNodeListesi.Add(temp[i]);
                }
                sart2 = 1;
            }
            else
            {
                yeniListe.Clear();
                for (int j = 0; j < temp.Count; j++)
                {
                    for (int i = 0; i < filtrelenmisTreeNodeListesi.Count; i++)
                    {
                    
                        if (temp[j] == filtrelenmisTreeNodeListesi[i])
                        {
                            yeniListe.Add(temp[j]);
                        }

                    }
                }
                filtrelenmisTreeNodeListesi.Clear();
                for(int i=0; i<yeniListe.Count; i++)
                {
                    filtrelenmisTreeNodeListesi.Add(yeniListe[i]);
                }
                
                
                

            }
        }      
        public void listele()
        {
            print(root);
        }

        private string idDondur()
        {
            id++;
            return "kisi" + id;
        }

        private TreeNode minVal(TreeNode node)
        {
            while (node.sol != null)
            {
                node = node.sol;
            }
            return node;
        }

        private TreeNode sil(TreeNode node, string kisiAdiSoyadi_, string kisiTelefonu)
        {

            if (node == null) ///node null ise geriye null değer döndürecek yani root
            {
                return node;
            }
            
            if (string.Compare(node.kisiAdiSoyadi, kisiAdiSoyadi_) == 1)  ////silinecek veri node daki değerden küçükse ife girecek
            {
                
                node.sol = sil(node.sol, kisiAdiSoyadi_,kisiTelefonu); ////node un solundaki değer için fonksiyonu tekrar döndürüyor
            }
            else if (string.Compare(node.kisiAdiSoyadi, kisiAdiSoyadi_) == -1)  ////silinecek veri node daki değerden büyükse ife girecek
            {
                node.sag = sil(node.sag, kisiAdiSoyadi_,kisiTelefonu) ; ////node un sağındaki değer için fonksiyonu tekrar döndürüyor
            }
            else  //// silinecek veri node daki değere eşitse else e giriyor
            {
                if(node.kisiTelefonu != kisiTelefonu)
                {
                    node.sol=sil(node.sol, kisiAdiSoyadi_, kisiTelefonu);
                }
                else
                {
                    if (node.sol == null) ///solunda çocuk yoksa sağdaki değeri döndürür oda boşsa fonku bitirir. Buradaki işlem hiç çocuğu yoksa da uygulanır
                    {
                        return node.sag;
                    }
                    else if (node.sag == null)
                    {
                        return node.sol;
                    }
                    else
                    {
                        kopyala(node, minVal(node.sag)); /// düğümde iki tane çocuk varsa sağındaki ağaçtan minimum değeri kendine eşitler (düğümü değil değeri)
                        node.sag = sil(node.sag, node.kisiAdiSoyadi, kisiTelefonu);  ///
                    }
                }
                


            }
            return node;

        }
        private void kopyala(TreeNode kopyalanan, TreeNode minvalue)
        {
            kopyalanan.kisiAdiSoyadi = minvalue.kisiAdiSoyadi;
            kopyalanan.kisiAdresi = minvalue.kisiAdresi;
            kopyalanan.kisiTelefonu = minvalue.kisiTelefonu;
            kopyalanan.kisiMail = minvalue.kisiMail;
            kopyalanan.kisiDogumTarihi = minvalue.kisiDogumTarihi;
            kopyalanan.kisiYabanciDil = minvalue.kisiYabanciDil;
            kopyalanan.kisiEhliyet = minvalue.kisiEhliyet;


        }
        
        public void delete(string kisiAdi,string telefon)
        {
           
           root= sil(root,kisiAdi,telefon);
            
            
        }

        private void Write(TreeNode node)
        {
            if (node != null)
            {
                Write(node.sol);


                FileStream fs = new FileStream(dosya_yolu, FileMode.Append, FileAccess.Write);

                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("*********KISI BILGILERI*********");
                sw.WriteLine(" ");
                sw.WriteLine("Kisi Adi Soyadi: " + node.kisiAdiSoyadi);
                sw.WriteLine("Kisi Adresi: " + node.kisiAdresi);
                sw.WriteLine("Kisi Telefonu: " + node.kisiTelefonu);
                sw.WriteLine("Kisi Mail: " + node.kisiMail);
                sw.WriteLine("Kisi DogumTarihi: " + node.kisiDogumTarihi);
                sw.WriteLine("Kisi Yabanci Dil: " + node.kisiYabanciDil);
                sw.WriteLine("Kisi Ehliyet: " + node.kisiEhliyet);
                sw.WriteLine(" ");
                sw.WriteLine("--------Egitim Bilgileri--------");
                for (int i = 0; i<node.kisiEgitimListesi.count(); i++)
                {
                    sw.WriteLine(" ");
                    sw.WriteLine("Okul Adi: "+node.kisiEgitimListesi.egitimListesiDugum(i).okulAdi);
                    sw.WriteLine("Okul Turu: "+ node.kisiEgitimListesi.egitimListesiDugum(i).okulturu);
                    sw.WriteLine("Bolumu: " + node.kisiEgitimListesi.egitimListesiDugum(i).bolum);
                    sw.WriteLine("Baslangic Tarihi: " + node.kisiEgitimListesi.egitimListesiDugum(i).baslangicTarihi);
                    sw.WriteLine("Bitis Tarihi: " + node.kisiEgitimListesi.egitimListesiDugum(i).bitisTarihi);
                    sw.WriteLine("Not Ortalamasi: " + node.kisiEgitimListesi.egitimListesiDugum(i).notOrtalamasi);
                    sw.WriteLine("......");
                }
                sw.WriteLine("+++++++++Isyeri Bilgileri+++++++++");
                for (int i =0; i<node.kisiIsyeriBilgileriListesi.count(); i++)
                {
                    sw.WriteLine(" ");
                    sw.WriteLine("Isyeri Adi: " + node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdi);
                    sw.WriteLine("Isyeri Adresi: " + node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).isyeriAdresi);
                    sw.WriteLine("Gorevi: " + node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).gorevi);
                    sw.WriteLine("Calisma suresi: " + node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).suresi);
                    sw.WriteLine("......");
                }


                sw.WriteLine("_____________________________________________________________________");
                sw.Flush();

                sw.Close();
                fs.Close();
                Write(node.sag);
               
            }
            
        }

        private void Read(TreeNode node)
        {
            
            string kisiAdi = " ",adresi = " ", telefonu = " ", mail = " ", dogumtarihi = " ", yabancidil = " ", ehliyet = " ";
            string isyeriadi = " ", isyeriAdresi = " ", gorevi = " ", calisma = " ";
            string okuladi = " ", okulturu = " ", bolumu = " ", baslangic = " ", bitis = " ", notortalamasi = " ";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Read);
          
            StreamReader sw = new StreamReader(fs);
            
            string satir = sw.ReadLine();
            while (satir != null)
            {
                
               
                if (satir[0] == '*')
                {
                    
                    

                    for (int i = 0; i < 8; i++)
                    {
                        satir = sw.ReadLine();
                        if (satir.Contains("Kisi Adi"))
                        {
                            kisiAdi = stringAyikla(satir);
                        }
                        else if (satir.Contains("Kisi Adresi"))
                        {
                            adresi = stringAyikla(satir);
                        }
                        else if (satir.Contains("Kisi Telefonu"))
                        {
                            telefonu = stringAyikla(satir);
                        }
                        else if (satir.Contains("Kisi Mail"))
                        {
                            mail = stringAyikla(satir);
                        }
                        else if (satir.Contains("Kisi DogumTarihi"))
                        {
                            dogumtarihi = stringAyikla(satir);
                        }
                        else if (satir.Contains("Kisi Yabanci Dil"))
                        {
                            yabancidil = stringAyikla(satir);
                        }
                        else if (satir.Contains("Kisi Ehliyet"))
                        {
                            ehliyet = stringAyikla(satir);
                        }
                    }
                }
                else if (satir[0] == '-')
                {
                    satir = sw.ReadLine();
                    EgitimBilgileriListe egitimliste = new EgitimBilgileriListe();

                    while (true)
                    {

                        satir = sw.ReadLine();
                        if (satir.Contains("Okul Adi"))
                        {
                            okuladi = stringAyikla(satir);
                        }
                        else if (satir.Contains("Okul Turu"))
                        {
                            okulturu = stringAyikla(satir);
                        }
                        else if (satir.Contains("Bolumu"))
                        {
                            bolumu = stringAyikla(satir);
                        }
                        else if (satir.Contains("Baslangic"))
                        {
                            baslangic = stringAyikla(satir);
                        }
                        else if (satir.Contains("Bitis"))
                        {
                            bitis = stringAyikla(satir);
                        }
                        else if (satir.Contains("Not"))
                        {
                            notortalamasi = stringAyikla(satir);
                        }
                        else if (satir[0] == '.')
                        {

                            egitimliste.ekle(okuladi, okulturu, bolumu, baslangic, bitis, notortalamasi);
                        }
                        else if (satir[0] == '+')
                        {

                            satir = sw.ReadLine();
                            IsyeriBilgileriListesi isyeriliste = new IsyeriBilgileriListesi();
                            if (satir[0] == '_')
                            {
                                ekle(kisiAdi, adresi, telefonu, mail, dogumtarihi, yabancidil, ehliyet, egitimliste, isyeriliste);
                                break;
                            }

                            
                            while (true)
                            {
                                satir = sw.ReadLine();
                                if (satir.Contains("Isyeri Adi"))
                                {
                                    isyeriadi = stringAyikla(satir);
                                }
                                else if (satir.Contains("Isyeri Adresi"))
                                {
                                    isyeriAdresi = stringAyikla(satir);
                                }
                                else if (satir.Contains("Gorevi"))
                                {
                                    gorevi = stringAyikla(satir);
                                }
                                else if (satir.Contains("Calisma suresi"))
                                {
                                    calisma = stringAyikla(satir);
                                }

                                else if (satir[0] == '.')
                                {

                                    isyeriliste.ekle(isyeriadi, isyeriAdresi, gorevi, calisma);
                                }
                                else if (satir[0] == '_')
                                {
                                    
                                    ekle(kisiAdi, adresi, telefonu, mail, dogumtarihi, yabancidil, ehliyet, egitimliste, isyeriliste);
                                    
                                    break;
                                }

                            }
                            break;
                        }
                    }
                   

                }

                satir = sw.ReadLine();
            }
            
            
            sw.Close();
            fs.Close();
        }
        public void okut()
        {
            Read(root);
        }
        
        public void textiGuncelle()
        {
            File.Delete(dosya_yolu);
            Write(root);
        }
        private string stringAyikla(string deger)
        {
            string donecek = "";
            int i;
            for (i = 0; i < deger.Length; i++)
            {
                if (deger[i] == ':')
                {
                    break;
                }
            }
            i++;
            i++;
            for (int j=i; j < deger.Length; j++)
            {
                donecek = donecek + deger[j];
            }
            return donecek;

        }
        public int count()
        {
            sayac = 0;
            print(root);
            return sayac;
        }
       
    }

   

}
