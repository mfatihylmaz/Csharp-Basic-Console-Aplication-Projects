using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi
{
    internal class Galeri
    {
        // bu sınıf içinde galeri ile ilgili kodlar yazılacak
        // Galeriye ilişkin herhnagi bir verinin değiştirilmesi gerektiğinde ilgili kodlar bu sınıfta yazılacak

        public List<Araba> Arabalar = new List<Araba>();

        public int ToplamAracSayisi
        {
            get
            {
                return this.Arabalar.Count;
            }
        }
        public int KiradakiAracSayisi
        {
            get
            {
                int adet = 0;

                foreach (Araba item in Arabalar)
                {
                    if (item.Durum == "Kirada")
                    {
                        adet++;
                    }
                }

                return adet;
            }
        }

        public int GaleridekiAracSayisi
        {
            get
            {
                return this.ToplamAracSayisi - this.KiradakiAracSayisi;
            }
        }

        public int ToplamAracKiralamaSuresi
        {
            get
            {
                int toplam = 0;

                foreach (Araba item in Arabalar)
                {
                    toplam += item.ToplamKiralanmaSuresi;
                }

                return toplam;
            }
        }

        public int ToplamAracKiralamaAdeti
        {
            get
            {
                int toplam = 0;
                foreach (Araba item in Arabalar)
                {
                    toplam += item.KiralanmaSayisi;
                }
                return toplam;
            }
        }

        public int Ciro
        {
            get
            {
                int toplam = 0;
                foreach (Araba item in Arabalar)
                {
                    toplam += item.KiralanmaSayisi * (int)item.KiralamaBedeli;
                }
                return toplam;
            }
        }


        public void ArabaKirala(string plaka, int sure)
        {
            // bu plakaya ait arabanın bulunması lazım

            Araba a = null;

            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                }
            }

            if (a != null)
            {
                a.Durum = "Kirada";
                //a.KiralanmaSayisi++;
                //a.ToplamKiralanmaSuresi += sure;
                a.KiralamaSureleri.Add(sure);
            }


        }
        public void ArabaTeslimAl(string plaka)
        {
            // bu plakaya ait arabanı bul durumunu değiştir

            Araba a = null;

            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                }
            }
            if (a != null)
            {
                a.Durum = "Galeride";
            }


        }

        public bool KiralamaIptal(string plaka)
        {
            // arabayı bul
            // a.KiralamaSureleri.RemoveAt()
            // KiralamaSureleri ndeki en son elamanı listeden çıkarıyoruz.
            //Araba a = ArabaBul(plaka);
            Araba a = null;
            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                }
            }

            if (a == null) return false;

            if (a.KiralamaSureleri.Count == 0) return false;

            int sonIndex = a.KiralamaSureleri.Count - 1;
            a.KiralamaSureleri.RemoveAt(sonIndex);

            a.Durum = "Galeride";
            return true;

        }

        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, string aTipi)
        {
            // paramatreden aldığımız bilgiler ile yeni bir araba oluşmalı.
            // bu oluşan araba Arabalar listesine eklenecek

            Araba a = new Araba(plaka, marka, kiralamaBedeli, aTipi);
            this.Arabalar.Add(a);
        }

        public void ArabaSil(string plaka)
        {
            // arabayı bul
            // bulduğumuz arabayı listeden çıkar
        }
        public void BilgileriGoster()
        {
            Console.WriteLine("Toplam Araç Sayısı: " + this.ToplamAracSayisi);
            Console.WriteLine("Kiradaki Araç Sayısı: " + this.KiradakiAracSayisi);
            Console.WriteLine("Galerideki Araç Sayısı: " + (this.ToplamAracSayisi - this.KiradakiAracSayisi));
            Console.WriteLine("Toplam Araç Kiralama Süresi: " + this.ToplamAracKiralamaSuresi);
            Console.WriteLine("Toplam Araç Kiralama Adeti: " + this.ToplamAracKiralamaAdeti);
            Console.WriteLine("Ciro: " + this.Ciro + " TL");
        }

        public bool PlakaKontrol(string plaka)
        {
            // Geçerli Türk plakası örnekleri kabul edilir:
            // "34 A 1234", "34 AB 123", "34 ABC 12", boşluklar olmadan da: "34AB1234"
            // İl kodu: 01..81, harfler: 1-3 (Türkçe büyük harfler dahil), rakamlar: 1-4
            if (string.IsNullOrWhiteSpace(plaka))
            {
                return false;
            }

            string p = plaka.Trim().ToUpperInvariant();

            // İl kodu 01-81, 1-3 harf (A-Z ve Türkçe harfler), 1-4 rakam. Boşluk opsiyonel.
            string pattern = @"^(0[1-9]|[1-7][0-9]|8[0-1])\s?[A-ZÇĞİÖŞÜ]{1,3}\s?\d{1,4}$";

            return Regex.IsMatch(p, pattern, RegexOptions.CultureInvariant);
        }

        static bool PlakaGecerliMi(string plaka)
        {
            if (string.IsNullOrWhiteSpace(plaka))
                return false;

            plaka = plaka.Replace(" ", "").ToUpper();

            int index = 0;

            // 1️⃣ İl kodunu oku (1 veya 2 hane)
            if (!char.IsDigit(plaka[index]))
                return false;

            string ilKoduStr = plaka[index].ToString();
            index++;

            if (index < plaka.Length && char.IsDigit(plaka[index]))
            {
                ilKoduStr += plaka[index];
                index++;
            }

            if (!int.TryParse(ilKoduStr, out int ilKodu))
                return false;

            if (ilKodu < 1 || ilKodu > 81)
                return false;

            // 2️⃣ Harfleri oku (1–3 harf)
            string harfler = "";

            while (index < plaka.Length && char.IsLetter(plaka[index]))
            {
                char c = plaka[index];

                if (c < 'A' || c > 'Z' || c == 'Q' || c == 'W' || c == 'X')
                    return false;

                harfler += c;
                index++;
            }

            if (harfler.Length < 1 || harfler.Length > 3)
                return false;

            // 3️⃣ Sayıları oku (2–4 rakam)
            string sayilar = "";

            while (index < plaka.Length && char.IsDigit(plaka[index]))
            {
                sayilar += plaka[index];
                index++;
            }

            if (sayilar.Length < 2 || sayilar.Length > 4)
                return false;

            // Fazladan karakter varsa geçersiz
            if (index != plaka.Length)
                return false;

            return true;
        }

    }

}
