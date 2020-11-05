using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace yazGel1v1
{
    class KullaniciAdiDosyaIslemleri
    {
        static string  dosya_yolu = @"C:\kullaniciAdiSifre.txt";
        static List<string> kullaniciAdiSifre = new List<string>();
        private static void kullaniciAdiSifreDosyayaYaz()
        {
            File.Delete(dosya_yolu);
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.Append, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);

            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            for (int i = 0; i < kullaniciAdiSifre.Count; i++)
            {
                sw.WriteLine(kullaniciAdiSifre[i]);
            }
            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();
            kullaniciAdiSifre.Clear();
        }
        public static void kullaniciAdiSifreEkle(string kullaniciAdi,string sifre)
        {
            kullaniciAdiSifreDosyadanOku();
            kullaniciAdiSifre.Add(kullaniciAdi);
            kullaniciAdiSifre.Add(sifre);
            kullaniciAdiSifreDosyayaYaz();
        }
        public static void kullaniciAdiSifreSil(string telefon)
        {
            kullaniciAdiSifreDosyadanOku();
            for (int i = 0; i < kullaniciAdiSifre.Count; i++)
            {
                if (kullaniciAdiSifre[i] == telefon)
                {
                    kullaniciAdiSifre.RemoveAt(i);
                    kullaniciAdiSifre.RemoveAt(i);
                    break;

                }
            }
            kullaniciAdiSifreDosyayaYaz();
        }
        public static  void kullaniciAdiSifreDosyadanOku()
        {
            kullaniciAdiSifre.Clear();
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string yazi = sr.ReadLine();
            while (yazi != null)
            {
                kullaniciAdiSifre.Add(yazi);
                yazi = sr.ReadLine();
            }
           
            //Veriyi tampon bölgeden dosyaya aktardık.
            sr.Close();
            fs.Close();
        }
        public static void kullaniciAdiGuncelle(string guncellenecekTelefonNumarasi,string deger)
        {
            kullaniciAdiSifreDosyadanOku();
            for (int i =0; i<kullaniciAdiSifre.Count; i++)
            {
                if (kullaniciAdiSifre[i] == guncellenecekTelefonNumarasi)
                {
                    kullaniciAdiSifre[i] = deger;
                    break;
                }
            }
            kullaniciAdiSifreDosyayaYaz();
        

        }

        public static bool girisKontrol(string kullaniciAdi,string sifre)
        {
            kullaniciAdiSifreDosyadanOku();
            for (int i = 0; i < kullaniciAdiSifre.Count; i = i + 2)
            {
                if (kullaniciAdi == kullaniciAdiSifre[i] && sifre == kullaniciAdiSifre[i + 1])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
