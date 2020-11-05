using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace Liste
{
    class TreeListe
    {
        string dosya_yolu = @"C:\ikveriTabani.txt";
        TreeNode root;
        int id = 0;
        int sayac;
        int sayac2;
        public TreeNode cekilecekNode;
        public List<TreeNode> treeNodeListesi = new List<TreeNode>();
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

        private TreeNode filtrelemeDolas(TreeNode node,  
            string kisiAdi, 
            string minimumDeneyim, 
            string maksimumYas, 
            string ehliyet, 
            bool ingilizceBilenler,
            bool birdenFazlaDil,
            bool deneyimsiz,
            bool lisans,
            bool yuksekLisans,
            bool doktora   )
        {
            if (node != null)
            {
                filtrelemeDolas(node.sol, kisiAdi, minimumDeneyim, maksimumYas, ehliyet, ingilizceBilenler, birdenFazlaDil, deneyimsiz, lisans, yuksekLisans, doktora);
                if ()
                filtrelemeDolas(node.sag, kisiAdi, minimumDeneyim, maksimumYas, ehliyet, ingilizceBilenler, birdenFazlaDil, deneyimsiz, lisans, yuksekLisans, doktora);
            }
            return null;
        }
        ///// telefon için düğüm döndür
        private TreeNode DugumDondur(TreeNode node, string aranan)
        {
            if (node != null)
            {
                DugumDondur(node.sol, aranan);
                if (node.kisiTelefonu == aranan)
                {
                    cekilecekNode = node;
                }
                DugumDondur(node.sag, aranan);
            }
            return null;
        }
        public TreeNode treeDugumDondur(string aranan)
        {
            return DugumDondur(root, aranan);
        }
        /////deneyime göre düğüm döndür
        private TreeNode DeneyimeGoreDugum(TreeNode node, int aranan)
        {
            if (node != null)
            {
                DeneyimeGoreDugum(node.sol, aranan);
                sayac2 = 0;
                for (int i = 0; i<node.kisiIsyeriBilgileriListesi.count();i++)
                {
                    sayac2 = sayac2 + Convert.ToInt32(node.kisiIsyeriBilgileriListesi.isyeribilgileriDugum(i).suresi) ;
                    
                }
                if (sayac2 >= aranan)
                {
                    treeNodeListesi.Add(node);
                }
                DeneyimeGoreDugum(node.sag, aranan);
            }
            return null;
        }
        public void DeneyimeGoreDugumDondur(int aranan)
        {
            treeNodeListesi.Clear();
            DeneyimeGoreDugum(root, aranan);
        }
        /////// isme(searche) göre düğüm döndür
        private TreeNode kisiIsmineGoreDugum(TreeNode node, string aranan)
        {
            if (node != null)
            {
                kisiIsmineGoreDugum(node.sol, aranan);
                if (node.kisiAdiSoyadi == aranan)
                {
                    treeNodeListesi.Add(node);
                }
                kisiIsmineGoreDugum(node.sag, aranan);
            }
            return null;
        }
        public void kisiIsmineGoreDugumDondur(string aranan)
        {
            treeNodeListesi.Clear();
            kisiIsmineGoreDugum(root, aranan);
        }
        //////Maksimum yaşa göre düğüm döndür
        private TreeNode YasaGoreDugum(TreeNode node, int aranan)
        {
            if (node != null)
            {
                YasaGoreDugum(node.sol, aranan);
                if (Convert.ToInt32(node.kisiDogumTarihi) >= 2020 - aranan)
                {
                    treeNodeListesi.Add(node);
                }
                YasaGoreDugum(node.sag, aranan);
            }
            return null;
        }
        public void YasaGoreDugumDondur(int aranan)
        {
            treeNodeListesi.Clear();
            YasaGoreDugum(root, aranan);
        }
        ///// ehilyet düğüm döndür
        private TreeNode ehliyetDugum(TreeNode node, string aranan)
        {
            if (node != null)
            {
                ehliyetDugum(node.sol, aranan);
                if (node.kisiEhliyet == aranan)
                {
                    treeNodeListesi.Add(node);
                }
                ehliyetDugum(node.sag, aranan);
            }
            return null;
        }
        public void ehliyetDugumDondur(string aranan)
        {
            treeNodeListesi.Clear();
            ehliyetDugum(root, aranan);
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
