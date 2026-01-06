using System;
using System.Collections.Generic;

namespace OgrenciYonetimUygulamasi
{
    internal class Program
    {
        static List<Ogrenci> ogrenciler = new List<Ogrenci>(); // global değişken

        static void Main(string[] args)
        {
            Uygulama();
            test();


        }
        static void test()
        {
            Ogrenci o1 = new Ogrenci();
            o1.Ad = "Fatih";
            o1.Soyad = "Yılmaz";
            o1.No = 10;
            o1.MatematikNotu = 100;
            o1.FenNotu = 90;
            o1.SosyalNotu = 80;
            o1.TurkceNotu = 70;
            Console.WriteLine(o1.NotOrtalamasıYazdır(o1.MatematikNotu, o1.SosyalNotu, o1.FenNotu, o1.TurkceNotu));
        }
        static void Uygulama()
        {
            SahteVeriEkle();
            Menu();

            while (true)
            {
                Console.Write("Seçiminiz : ");
                string giris = Console.ReadLine().ToUpper();

                switch (giris)
                {
                    case "E":
                    case "1":
                        OgrenciEkle();
                        break;
                    case "L":
                    case "2":
                        OgrenciListele();
                        break;
                    case "S":
                    case "3":
                        OgrenciSil();
                        break;
                    case "X":
                    case "4":
                        // Çıkış
                        break;
                    default:
                        Console.WriteLine("Hatalı giriş yapıldı, tekrar deneyin!");
                        break;

                }

                Console.WriteLine();
            }



        }
        static void OgrenciEkle()
        {
            Ogrenci o = new Ogrenci();


            Console.WriteLine("1- Öğrenci Ekle ----------");
            Console.WriteLine("Öğrencinin");

            Console.Write("No: ");
            o.No = int.Parse(Console.ReadLine());
            Console.Write("Adı: ");
            o.Ad = Console.ReadLine();
            Console.Write("Soyadı: ");
            o.Soyad = Console.ReadLine();
            Console.Write("Şubesi: ");
            o.Sube = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Öğrenciyi kaydetmek istediğinize emin misiniz? (E/H)  ");
            string secim = Console.ReadLine().ToUpper();

            if (secim == "E")
            {
                ogrenciler.Add(o);
                Console.WriteLine("Öğrenci eklendi.");
            }
            else
            {
                Console.WriteLine("Öğrenci eklenmedi.");
            }

            Console.WriteLine();

        }
        static void OgrenciListele()
        {
            Console.WriteLine("2- Öğrenci Listele-----------");
            Console.WriteLine();
            Console.WriteLine("Şube    No    Ad Soyad");
            Console.WriteLine("---------------------------------- ");

            foreach (Ogrenci item in ogrenciler)
            {
                Console.WriteLine(item.Sube + "       " + item.No + "     " + item.Ad + " " + item.Soyad);
            }


        }
        static void OgrenciSil()
        {
            Console.WriteLine("3- Öğrenci Sil ----------");
            Console.WriteLine("Silmek istediğiniz öğrencinin");

            Console.Write("No: ");
            int no = int.Parse(Console.ReadLine());

            Ogrenci ogr = null;

            foreach (Ogrenci item in ogrenciler)
            {
                if (item.No == no)
                {
                    ogr = item;
                    break;
                }

            }

            if (ogr != null)
            {
                Console.WriteLine("Adı: " + ogr.Ad);
                Console.WriteLine("Soyadı: " + ogr.Soyad);
                Console.WriteLine("Şubesi: " + ogr.Sube);
                Console.WriteLine();
                Console.Write("Öğrenciyi silmek istediğinize emin misiniz? (E/H)  ");

                string secim = Console.ReadLine();

                if (secim == "E")
                {
                    ogrenciler.Remove(ogr);
                    Console.WriteLine("Öğrenci silindi");
                }
                else
                {
                    // silinmedi
                }
            }
            else
            {
                // Böyle bir öğrenci bulunamadı.
            }



        }
        static void Menu()
        {
            Console.WriteLine("Öğrenci Yönetim Uygulaması");
            Console.WriteLine("1 - Öğrenci Ekle(E)       ");
            Console.WriteLine("2 - Öğrenci Listele(L)    ");
            Console.WriteLine("3 - Öğrenci Sil(S)        ");
            Console.WriteLine("4 - Çıkış(X)              ");
            Console.WriteLine();
        }
        static void SahteVeriEkle()
        {
            Ogrenci o1 = new Ogrenci();
            o1.Ad = "Veli";
            o1.Soyad = "Gündüz";
            o1.No = 1;
            o1.Sube = "A";
            ogrenciler.Add(o1);

            Ogrenci o2 = new Ogrenci();
            o2.Ad = "Ali";
            o2.Soyad = "Yılmaz";
            o2.No = 2;
            o2.Sube = "B";
            ogrenciler.Add(o2);

            Ogrenci o3 = new Ogrenci();
            o3.Ad = "Ayşe";
            o3.Soyad = "Yıldız";
            o3.No = 3;
            o3.Sube = "C";
            ogrenciler.Add(o3);
        }

    }
}
