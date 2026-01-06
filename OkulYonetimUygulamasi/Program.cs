using System;
using System.Globalization;
using System.Linq;

namespace OkulYonetimUygulamasi
{
    internal class Program
    {
        static Okul Okul = new Okul();

        static void Main(string[] args)
        {
            SahteVeriGir();
            Uygulama();
        }

        static void Uygulama()
        {
            Menu();
            bool baslangıc = false;

            while (true)
            {
                if (baslangıc) Console.WriteLine("\nMenüyü tekrar listelemek için \"liste\", çıkış yapmak için \"çıkış\" yazın.");
                Console.WriteLine();
                baslangıc = true;
                Console.Write("Yapmak istediğiniz işlemi seçiniz: ");
                string secim = Console.ReadLine();
                if (secim == null) secim = "";
                secim = secim.Trim().ToLower();

                if (secim == "çıkış")
                    Environment.Exit(0);

                if (secim == "liste")
                {
                    Menu();
                    continue;
                }

                if (!int.TryParse(secim, out int islemNo))
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }

                Console.WriteLine();

                switch (islemNo)
                {
                    case 1: TumOgrencileriListele(); break;
                    case 2: SubeyeGoreListele(); break;
                    case 3: CinsiyeteGoreListele(); break;
                    case 4: TarihtenSonraDoganlariListele(); break;
                    case 5: IllereGoreSiralaListele(); break;
                    case 6: OgrencininNotlariniListele(); break;
                    case 7: OgrencininKitaplariniListele(); break;
                    case 8: OkuldakiEnYuksekNotlu5(); break;
                    case 9: OkuldakiEnDusukNotlu3(); break;
                    case 10: SubedekiEnYuksekNotlu5(); break;
                    case 11: SubedekiEnDusukNotlu3(); break;
                    case 12: OgrenciNotOrtGoster(); break;
                    case 13: SubeNotOrtGoster(); break;
                    case 14: OgrencininSonKitabiGoster(); break;
                    case 15: OgrenciEkle(); break;
                    case 16: OgrenciGuncelle(); break;
                    case 17: OgrenciSil(); break;
                    case 18: OgrenciAdresGir(); break;
                    case 19: OgrenciKitapGir(); break;
                    case 20: NotGir(); break;
                    default:
                        Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                        break;
                }
            }
        }

        static void Menu()
        {
            Console.WriteLine("\n------ Okul Yönetim Uygulamasi -----");
            Console.WriteLine();
            Console.WriteLine("1 - Bütün öğrencileri listele");
            Console.WriteLine("2 - Şubeye göre öğrencileri listele");
            Console.WriteLine("3 - Cinsiyetine göre öğrencileri listele");
            Console.WriteLine("4 - Şu tarihten sonra doğan öğrencileri listele");
            Console.WriteLine("5 - İllere göre sıralayarak öğrencileri listele");
            Console.WriteLine("6 - Öğrencinin tüm notlarını listele");
            Console.WriteLine("7 - Öğrencinin okuduğu kitapları listele");
            Console.WriteLine("8 - Okuldaki en yüksek notlu 5 öğrenciyi listele");
            Console.WriteLine("9 - Okuldaki en düşük notlu 3 öğrenciyi listele");
            Console.WriteLine("10 - Şubedeki en yüksek notlu 5 öğrenciyi listele");
            Console.WriteLine("11 - Şubedeki en düşük notlu 3 öğrenciyi listele");
            Console.WriteLine("12 - Öğrencinin not ortalamasını gör");
            Console.WriteLine("13 - Şubenin not ortalamasını gör");
            Console.WriteLine("14 - Öğrencinin okuduğu son kitabı gör");
            Console.WriteLine("15 - Öğrenci ekle");
            Console.WriteLine("16 - Öğrenci güncelle");
            Console.WriteLine("17 - Öğrenci sil");
            Console.WriteLine("18 - Öğrencinin adresini gir");
            Console.WriteLine("19 - Öğrencinin okuduğu kitabı gir");
            Console.WriteLine("20 - Öğrencinin notunu gir");
            Console.WriteLine();
            Console.WriteLine("Çıkış yapmak için \"çıkış\" yazıp \"enter\"a basın.");
        }

        // ----------------- ORTAK YARDIMCILAR -----------------

        static string IlkHarfBuyukYap(string metin)
        {
            if (string.IsNullOrWhiteSpace(metin)) return "";
            metin = metin.Trim().ToLower(new CultureInfo("tr-TR"));
            TextInfo ti = new CultureInfo("tr-TR").TextInfo;
            return ti.ToTitleCase(metin);
        }

        static int OgrenciNoAl(string baslik)
        {
            while (true)
            {
                Console.Write(baslik);
                string giris = Console.ReadLine();
                if (giris == null) giris = "";
                giris = giris.Trim();

                if (int.TryParse(giris, out int no))
                    return no;

                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
            }
        }

        static Ogrenci OgrenciSec()
        {
            while (true)
            {
                int no = OgrenciNoAl("Öğrencinin numarasi: ");
                var o = Okul.OgrenciBul(no);
                if (o == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bu numarada bir öğrenci yok.Tekrar deneyin.");
                    Console.ResetColor();
                    continue;
                }
                return o;
            }
        }

        static SUBE SubeAl(string baslik)
        {
            while (true)
            {
                Console.Write(baslik);
                string s = Console.ReadLine();
                if (s == null) s = "";
                s = s.Trim().ToUpper();

                if (s == "A") return SUBE.A;
                if (s == "B") return SUBE.B;
                if (s == "C") return SUBE.C;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                Console.ResetColor();
            }
        }

        static CINSIYET CinsiyetAl(string baslik)
        {
            while (true)
            {
                Console.Write(baslik);
                string s = Console.ReadLine();
                if (s == null) s = "";
                s = s.Trim().ToUpper();

                if (s == "K") return CINSIYET.Kiz;
                if (s == "E") return CINSIYET.Erkek;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                Console.ResetColor();
            }
        }

        static DateTime TarihAl(string baslik)
        {
            while (true)
            {
                Console.Write(baslik);
                string giris = Console.ReadLine();
                if (giris == null) giris = "";
                giris = giris.Trim();

                // Ekranda 10.10.2000 gibi görünüyor
                if (DateTime.TryParse(giris, new CultureInfo("tr-TR"), DateTimeStyles.None, out DateTime tarih))
                    return tarih;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                Console.ResetColor();
            }
        }

        static void TabloBaslikYaz(bool sehirKolonlu)
        {
            if (!sehirKolonlu)
            {
                Console.WriteLine("Şube".PadRight(6) + "No".PadRight(6) + "Adı Soyadı".PadRight(25) + "Not Ort.".PadRight(12) + "Okuduğu Kitap Say.");
                Console.WriteLine(new string('-', 75));
            }
            else
            {
                Console.WriteLine("Şube".PadRight(6) + "No".PadRight(6) + "Adı Soyadı".PadRight(25) + "Şehir".PadRight(12) + "Semt");
                Console.WriteLine(new string('-', 75));
            }
        }

        static void OgrenciSatirYaz(Ogrenci o)
        {
            Console.WriteLine(
                o.Sube.ToString().PadRight(6) +
                o.No.ToString().PadRight(6) +
                (o.Ad + " " + o.Soyad).PadRight(25) +
                o.Ortalama.ToString("0.##", new CultureInfo("tr-TR")).PadRight(12) +
                o.Kitaplar.Count.ToString()
            );
        }

        // ----------------- 1..20 İŞLEMLER -----------------

        static void TumOgrencileriListele()
        {
            Console.WriteLine("1-Bütün Öğrencileri Listele " + new string('-', 60));
            Console.WriteLine();
            TabloBaslikYaz(false);

            foreach (var o in Okul.Ogrenciler.OrderBy(a => a.No).ThenBy(a => a.Sube))
                OgrenciSatirYaz(o);
        }

        static void SubeyeGoreListele()
        {
            Console.WriteLine("2-Şubeye Göre Öğrencileri Listele " + new string('-', 50));
            Console.WriteLine();
            SUBE sube = SubeAl("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            Console.WriteLine();

            TabloBaslikYaz(false);

            var liste = Okul.Ogrenciler.Where(a => a.Sube == sube).OrderBy(a => a.No).ToList();
            foreach (var o in liste)
                OgrenciSatirYaz(o);
        }

        static void CinsiyeteGoreListele()
        {
            Console.WriteLine("3-Cinsiyete Göre Öğrencileri Listele " + new string('-', 45));
            Console.WriteLine();
            CINSIYET c = CinsiyetAl("Listelemek istediğiniz cinsiyeti girin (K/E): ");
            Console.WriteLine();

            TabloBaslikYaz(false);

            var liste = Okul.Ogrenciler.Where(a => a.Cinsiyet == c).OrderBy(a => a.No).ToList();
            foreach (var o in liste)
                OgrenciSatirYaz(o);
        }

        static void TarihtenSonraDoganlariListele()
        {
            Console.WriteLine("4-Doğum Tarihine Göre Öğrencileri Listele " + new string('-', 42));
            Console.WriteLine();
            DateTime t = TarihAl("Hangi tarihten sonraki öğrencileri listelemek istersiniz: ");
            Console.WriteLine();

            var liste = Okul.Ogrenciler.Where(a => a.DogumTarihi > t).OrderBy(a => a.No).ToList();
            if (liste.Count > 0)
            {
                TabloBaslikYaz(false);
                foreach (var o in liste)
                    OgrenciSatirYaz(o);
            }
            else
            {
                Console.WriteLine("Listelenecek ögrenci yok.");
            }

        }

        static void IllereGoreSiralaListele()
        {
            Console.WriteLine("5-Illere Göre Öğrencileri Listele " + new string('-', 50));
            Console.WriteLine();
            TabloBaslikYaz(true);

            var liste = Okul.Ogrenciler
                .OrderBy(a => (a.Adresi?.Il ?? ""))
                .ThenBy(a => (a.Adresi?.Ilce ?? ""))
                .ThenBy(a => a.No)
                .ToList();

            foreach (var o in liste)
            {
                string sehir = o.Adresi?.Il ?? "";
                string semt = o.Adresi?.Ilce ?? "";
                Console.WriteLine(
                    o.Sube.ToString().PadRight(6) +
                    o.No.ToString().PadRight(6) +
                    (o.Ad + " " + o.Soyad).PadRight(25) +
                    sehir.PadRight(12) +
                    semt
                );
            }
        }

        static void OgrencininNotlariniListele()
        {
            Console.WriteLine("6-Öğrencinin notlarını görüntüle " + new string('-', 45));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();
            Console.WriteLine("Dersin Adı".PadRight(18) + "Notu");
            Console.WriteLine(new string('-', 18));

            foreach (var n in o.Notlar.OrderBy(a => a.DersAdi))
                Console.WriteLine(n.DersAdi.PadRight(18) + n.Not);
        }

        static void OgrencininKitaplariniListele()
        {
            Console.WriteLine("7-Öğrencinin okuduğu kitapları listele " + new string('-', 40));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();
            Console.WriteLine("Okuduğu Kitaplar");
            Console.WriteLine(new string('-', 18));

            foreach (var k in o.Kitaplar)
                Console.WriteLine(k);
        }

        static void OkuldakiEnYuksekNotlu5()
        {
            Console.WriteLine("8-Okuldaki en başarılı 5 öğrenciyi listele " + new string('-', 35));
            Console.WriteLine();
            TabloBaslikYaz(false);

            var liste = Okul.Ogrenciler.OrderByDescending(a => a.Ortalama).Take(5).ToList();
            foreach (var o in liste)
                OgrenciSatirYaz(o);
        }

        static void OkuldakiEnDusukNotlu3()
        {
            Console.WriteLine("9-Okuldaki en basarısız 3 ögrenciyi listele " + new string('-', 30));
            Console.WriteLine();
            TabloBaslikYaz(false);

            var liste = Okul.Ogrenciler.OrderBy(a => a.Ortalama).Take(3).ToList();
            foreach (var o in liste)
                OgrenciSatirYaz(o);
        }

        static void SubedekiEnYuksekNotlu5()
        {
            Console.WriteLine("10-Şubedeki en başarılı 5 öğrenciyi listele " + new string('-', 33));
            SUBE sube = SubeAl("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            Console.WriteLine();

            TabloBaslikYaz(false);

            var liste = Okul.Ogrenciler.Where(a => a.Sube == sube)
                .OrderByDescending(a => a.Ortalama)
                .Take(5)
                .ToList();

            foreach (var o in liste)
                OgrenciSatirYaz(o);
        }

        static void SubedekiEnDusukNotlu3()
        {
            Console.WriteLine("11-Şubedeki en düşük notlu 3 öğrenciyi listele " + new string('-', 28));
            SUBE sube = SubeAl("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            Console.WriteLine();

            TabloBaslikYaz(false);

            var liste = Okul.Ogrenciler.Where(a => a.Sube == sube)
                .OrderBy(a => a.Ortalama)
                .Take(3)
                .ToList();

            foreach (var o in liste)
                OgrenciSatirYaz(o);
        }

        static void OgrenciNotOrtGoster()
        {
            Console.WriteLine("12-Öğrencinin Not Ortalamasını Gör " + new string('-', 35));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();
            Console.WriteLine("Öğrencinin not ortalaması: " + o.Ortalama.ToString("0.##", new CultureInfo("tr-TR")));
        }

        static void SubeNotOrtGoster()
        {
            Console.WriteLine("13-Şubenin Not Ortalamasını Gör " + new string('-', 35));

            SUBE sube = SubeAl("Bir şube seçin (A/B/C): ");
            Console.WriteLine();

            var liste = Okul.Ogrenciler.Where(a => a.Sube == sube).ToList();
            float ort = 0;
            if (liste.Count > 0) ort = (float)liste.Average(a => a.Ortalama);

            Console.WriteLine($"{sube} şubesinin not ortalaması: {ort.ToString("0.##", new CultureInfo("tr-TR"))}");
        }

        static void OgrencininSonKitabiGoster()
        {
            Console.WriteLine("14-Öğrencinin okuduğu son kitabı listele " + new string('-', 33));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();
            Console.WriteLine("Öğrencinin Okuduğu Son Kitap");
            Console.WriteLine(new string('-', 27));

            if (o.Kitaplar.Count == 0)
                Console.WriteLine("Kayıtlı kitap yok.");
            else
                Console.WriteLine(o.Kitaplar.Last());
        }

        static void OgrenciEkle()
        {
            Console.WriteLine("15-Öğrenci Ekle " + new string('-', 55));
            Console.WriteLine();

            int istenenNo = OgrenciNoAl("Öğrencinin numarasi: ");

            Console.Write("Öğrencinin adı: ");
            string ad = IlkHarfBuyukYap(Console.ReadLine());

            Console.Write("Öğrencinin soyadı: ");
            string soyad = IlkHarfBuyukYap(Console.ReadLine());

            DateTime dt = TarihAl("Öğrencinin doğum tarihi: ");
            CINSIYET c = CinsiyetAl("Öğrencinin cinsiyeti (K/E): ");
            SUBE sube = SubeAl("Öğrencinin şubesi (A/B/C): ");

            int gercekNo = Okul.MusaitNoBul(istenenNo);
            Okul.OgrenciEkle(gercekNo, ad, soyad, dt, c, sube);

            Console.WriteLine();
            Console.WriteLine($"{gercekNo} numaralı öğrenci sisteme başarılı bir şekilde eklenmiştir.");

            if (gercekNo != istenenNo)
                Console.WriteLine($"Sistemde {istenenNo} numaralı öğrenci olduğu için verdiğiniz öğrenci no {gercekNo} olarak değiştirildi.");
        }

        static void OgrenciGuncelle()
        {
            Console.WriteLine("16-Öğrenci Güncelle " + new string('-', 50));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.Write("Öğrencinin adı: (Enter) ");
            string ad = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ad))
                o.Ad = IlkHarfBuyukYap(ad);

            Console.Write("Öğrencinin soyadı: (Enter) ");
            string soyad = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(soyad))
                o.Soyad = IlkHarfBuyukYap(soyad);

            Console.Write("Öğrencinin doğum tarihi: (Enter) ");
            string dtStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dtStr))
            {
                if (DateTime.TryParse(dtStr, new CultureInfo("tr-TR"), DateTimeStyles.None, out DateTime dt))
                    o.DogumTarihi = dt;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    Console.ResetColor();
                }
            }

            o.Cinsiyet = CinsiyetAl("Öğrencinin cinsiyeti (K/E): ");
            o.Sube = SubeAl("Öğrencinin şubesi (A/B/C): ");

            Console.WriteLine();
            Console.WriteLine("Öğrenci güncellendi.");
        }

        static void OgrenciSil()
        {
            Console.WriteLine("17-Öğrenci sil " + new string('-', 60));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();

            while (true)
            {
                Console.Write("Öğrenciyi silmek istediğinize emin misiniz (E/H): ");
                string s = Console.ReadLine();
                if (s == null) s = "";
                s = s.Trim().ToUpper();

                if (s == "E")
                {
                    Okul.OgrenciSil(o.No);
                    Console.WriteLine("Öğrenci başarılı bir şekilde silindi.");
                    return;
                }
                if (s == "H")
                {
                    Console.WriteLine("İşlem iptal edildi.");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                Console.ResetColor();
            }
        }

        static void OgrenciAdresGir()
        {
            Console.WriteLine("18-Öğrencinin Adresini Gir " + new string('-', 45));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();

            Console.Write("Il: ");
            string il = IlkHarfBuyukYap(Console.ReadLine());

            Console.Write("Ilce: ");
            string ilce = IlkHarfBuyukYap(Console.ReadLine());

            Console.Write("Mahalle: ");
            string mahalle = IlkHarfBuyukYap(Console.ReadLine());

            Okul.AdresGuncelle(o.No, il, ilce, mahalle);

            Console.WriteLine();
            Console.WriteLine("Bilgiler sisteme girilmiştir.");
        }

        static void OgrenciKitapGir()
        {
            Console.WriteLine("19-Öğrencinin okuduğu kitabı gir " + new string('-', 45));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();

            Console.Write("Eklenecek Kitabın Adı: ");
            string kitap = IlkHarfBuyukYap(Console.ReadLine());

            Okul.KitapEkle(o.No, kitap);

            Console.WriteLine("Bilgiler sisteme girilmiştir.");
        }

        static void NotGir()
        {
            Console.WriteLine("20-Not Gir " + new string('-', 58));
            var o = OgrenciSec();

            Console.WriteLine();
            Console.WriteLine("Öğrencinin Adı Soyadı: " + o.Ad + " " + o.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + o.Sube);
            Console.WriteLine();

            Console.Write("Not eklemek istediğiniz ders: ");
            string ders = IlkHarfBuyukYap(Console.ReadLine());

            int adet;
            while (true)
            {
                Console.Write("Eklemek istediğiniz not adedi: ");
                string giris = Console.ReadLine();
                if (giris == null) giris = "";
                giris = giris.Trim();

                if (int.TryParse(giris, out adet) && adet > 0)
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                Console.ResetColor();
            }

            for (int i = 1; i <= adet; i++)
            {
                int not;
                while (true)
                {
                    Console.Write(i + ". Notu girin: ");
                    string g = Console.ReadLine();
                    if (g == null) g = "";
                    g = g.Trim();

                    if (int.TryParse(g, out not) && not >= 0 && not <= 100)
                        break;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    Console.ResetColor();
                }

                Okul.NotEkle(o.No, ders, not);
            }

            Console.WriteLine();
            Console.WriteLine("Bilgiler sisteme girilmiştir.");
        }

        // ----------------- SAHTE VERİ -----------------

        static void SahteVeriGir()
        {
            // Çıktılardaki isimlere benzer örnekler
            Okul.OgrenciEkle(1, "Elif", "Selçuk", new DateTime(2001, 2, 3), CINSIYET.Kiz, SUBE.A);
            Okul.OgrenciEkle(2, "Betül", "Yılmaz", new DateTime(2000, 6, 10), CINSIYET.Kiz, SUBE.B);
            Okul.OgrenciEkle(3, "Hakan", "Çelik", new DateTime(2002, 1, 20), CINSIYET.Erkek, SUBE.C);
            Okul.OgrenciEkle(4, "Kerem", "Akay", new DateTime(2000, 12, 5), CINSIYET.Erkek, SUBE.A);
            Okul.OgrenciEkle(5, "Hatice", "Çınar", new DateTime(2001, 11, 1), CINSIYET.Kiz, SUBE.B);
            Okul.OgrenciEkle(6, "Selim", "İleri", new DateTime(2002, 4, 15), CINSIYET.Erkek, SUBE.B);
            Okul.OgrenciEkle(10, "Selda", "Kavak", new DateTime(2001, 9, 9), CINSIYET.Kiz, SUBE.B);

            // Adres
            Okul.AdresGuncelle(1, "Ankara", "Çankaya", "Kızılay");
            Okul.AdresGuncelle(2, "Ankara", "Keçiören", "Etlik");
            Okul.AdresGuncelle(3, "İzmir", "Karşıyaka", "Bostanlı");
            Okul.AdresGuncelle(4, "Ankara", "Çankaya", "Bahçelievler");
            Okul.AdresGuncelle(5, "İstanbul", "Kadıköy", "Moda");
            Okul.AdresGuncelle(6, "Ankara", "Çankaya", "Dikmen");
            Okul.AdresGuncelle(10, "Ankara", "Keçiören", "Aktepe");

            // Notlar (çıktıda dersler alfabetik listeleniyor)
            Okul.NotEkle(1, "Türkçe", 42);
            Okul.NotEkle(1, "Matematik", 33);
            Okul.NotEkle(1, "Fen", 82);
            Okul.NotEkle(1, "Sosyal", 65);

            Okul.NotEkle(2, "Matematik", 60);
            Okul.NotEkle(2, "Türkçe", 55);

            Okul.NotEkle(3, "Matematik", 90);
            Okul.NotEkle(3, "Fen", 85);

            Okul.NotEkle(4, "Türkçe", 57);
            Okul.NotEkle(5, "Türkçe", 49);
            Okul.NotEkle(6, "Türkçe", 78);
            Okul.NotEkle(10, "Türkçe", 53);

            // Kitap
            Okul.KitapEkle(1, "Bülbülü Öldürmek");
            Okul.KitapEkle(2, "Kürk Mantolu Madonna");
            Okul.KitapEkle(3, "1984");
            Okul.KitapEkle(4, "Simyacı");
            Okul.KitapEkle(5, "Saatleri Ayarlama Enstitüsü");
            Okul.KitapEkle(6, "Tutunamayanlar");
        }
    }
}
