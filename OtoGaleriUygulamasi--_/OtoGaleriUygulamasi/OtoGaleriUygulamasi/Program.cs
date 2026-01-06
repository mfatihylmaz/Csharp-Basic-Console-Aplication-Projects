using System;
using System.Numerics;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi
{
    internal class Program
    {
        static Galeri OtoGaleri = new Galeri();

        static bool devamMi = true;

        static void Main(string[] args)
        {
            // Kullanıcı ile etkileşime gireceğimiz bütün kodlar bu sınıfta yazılacak.
            Uygulama();
        }
        static void Uygulama()
        {
            SahteVeriEkle();
            Menu();
            // SecimAl()
            // switch-case
            while (devamMi)
            {
                string giris = SecimAl();

                switch (giris)
                {
                    case "K":
                    case "1":
                        ArabaKirala();
                        break;
                    case "T":
                    case "2":
                        ArabaTeslimAl();
                        break;
                    case "R":
                    case "3":
                        KiradakileriListele();
                        break;

                    case "M":
                    case "4":
                        GaleridekileriListele();
                        break;
                    case "A":
                    case "5":
                        TumArabalariListele();
                        break;
                    case "I":
                    case "6":
                        KiralamaIptali();
                        break;
                    case "Y":
                    case "7":
                        ArabaEkle();
                        break;
                    case "S":
                    case "8":
                        ArabaSil();
                        break;
                    case "G":
                    case "9":
                        BilgileriGoster();
                        break;
                    case "X":
                        break;


                    default:
                        Console.WriteLine("Geçersiz seçim yaptınız. Lütfen tekrar deneyiniz.");
                        break;
                }

                Console.WriteLine();
            }
        }
        static void Menu()
        {
            Console.WriteLine("Galeri Otomasyon                     ");
            Console.WriteLine("1 - Araba Kirala(K)                  ");
            Console.WriteLine("2 - Araba Teslim Al(T)               ");
            Console.WriteLine("3 - Kiradaki Arabaları Listele(R)    ");
            Console.WriteLine("4 - Galerideki Arabaları Listele(M)  ");
            Console.WriteLine("5 - Tüm Arabaları Listele(A)         ");
            Console.WriteLine("6 - Kiralama İptali(I)               ");
            Console.WriteLine("7 - Araba Ekle(Y)                    ");
            Console.WriteLine("8 - Araba Sil(S)                     ");
            Console.WriteLine("9 - Bilgileri Göster(G)              \n");

        }
        static void TumArabalariListele()
        {
            if (OtoGaleri.Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride hiç araba yok. Dükkani Kapat");
                return;
            }
            else
            {
                Console.WriteLine("Plaka                Marka        K. Bedeli        Araba Tipi           K. Sayısı         Durum ");
                Console.WriteLine("----------------------------------------------------------------------------------------------------- \n");
                foreach (Araba item in OtoGaleri.Arabalar)
                {
                    Console.WriteLine(item.Plaka.ToString().PadRight(21) + item.Marka.PadRight(13) + item.KiralamaBedeli.ToString().PadRight(17) + item.AracTipi.PadRight(21) + item.KiralanmaSayisi.ToString().PadRight(18) + item.Durum);
                }
            }

        }
        static void ArabaKirala()
        {
            Console.WriteLine("Seçim 1 ya da K için:  \n-Araba Kirala-\n");
            int plakavarmi = 0;
            bool kiradami = false;
            string plaka;

            while (true)
            {
                Console.Write("Kiralanacak arabanın plakası: ");
                plaka = PlakaAl__2();
                if (plaka == "X") return;

                foreach (Araba item in OtoGaleri.Arabalar)
                {
                    if (item.Plaka == plaka)
                    {
                        plakavarmi++;
                        if (item.Durum == "Kirada")
                        {
                            Console.WriteLine("Araba şu anda kirada. Farklı araba seçiniz. ");
                            kiradami = true;
                        }
                    }
                }
                if (plakavarmi == 0 && plaka != null)
                {
                    Console.WriteLine("Bu plakaya sahip araba galeride yok. Tekrar deneyin.");
                }

                // kiralanacak arabanın plakası doğru girilmediği sürece tekrar plaka istenecek
                if (plakavarmi > 0 && !kiradami && plaka != null)
                {

                    while (true)
                    {
                        int sure = 0;
                        Console.Write("Kiralama süresi: ");
                        string giris = Console.ReadLine().ToUpper();
                        if (giris == "X") return;
                        if (!(int.TryParse(giris, out sure)))
                        {
                            Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                            continue;
                        }
                        else
                        {

                            OtoGaleri.ArabaKirala(plaka, sure);
                            Console.WriteLine(plaka + "  plakalı araba " + sure + " saatliğine kiralandı.\n");
                            return;
                        }
                    }

                }
                kiradami = false;
            }
        }
        static void ArabaTeslimAl()
        {
            // Araba teslim alma işlemleri burada yapılacak

            Console.WriteLine("Seçim 2 ya da T için:  \n-Araba Teslim Al-\n");
            int plakavarmi = 0;
            bool galerideMi = false;
            string plaka;

            while (true)
            {
                Console.Write("Teslim edilecek arabanın plakası: ");
                plaka = PlakaAl__2();
                if (plaka == "X") return;

                foreach (Araba item in OtoGaleri.Arabalar)
                {
                    if (item.Plaka == plaka)
                    {
                        plakavarmi++;
                        if (item.Durum == "Galeride")
                        {
                            Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                            galerideMi = true;
                        }
                    }
                }
                if (plakavarmi == 0 && plaka != null)
                {
                    Console.WriteLine("Bu plakaya sahip araba galeride yok. Tekrar deneyin.");
                }

                // kiralanacak arabanın plakası doğru girilmediği sürece tekrar plaka istenecek
                if (plakavarmi > 0 && !galerideMi && plaka != null)
                {
                    OtoGaleri.ArabaTeslimAl(plaka);
                    Console.WriteLine("Araba galeride beklemeye alındı.");
                    break;
                }
                galerideMi = false;
                plakavarmi = 0;
            }
        }
        static void KiralamaIptali()
        {
            // Kiralama iptali işlemleri burada yapılacak
            Console.WriteLine("- Kiralama İptali -\n");

            string plaka;
            Araba secilenAraba = null;
            int kirada_sayısı = 0;
            while (true)
            {
                foreach (Araba a in OtoGaleri.Arabalar)
                {
                    if (a.Durum == "Kirada")
                    {
                        kirada_sayısı++;
                    }
                }
                if (kirada_sayısı == 0)
                {
                    Console.WriteLine("Kirada araba yok.");
                    return;

                }
                Console.Write("Kiralaması iptal edilecek arabanın plakası: ");
                plaka = PlakaAl__2();

                if (plaka == "X") return;

                foreach (Araba a in OtoGaleri.Arabalar)
                {
                    if (a.Plaka == plaka)
                    {
                        secilenAraba = a;
                        break;
                    }
                }

                if (secilenAraba == null)
                {
                    //HataliGirisArtirVeKontrol();
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    continue;
                }

                if (secilenAraba.Durum == "Galeride")
                {
                    //HataliGirisArtirVeKontrol();
                    Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                    continue;
                }

                break;
            }

            OtoGaleri.KiralamaIptal(plaka);
            Console.WriteLine("İptal gerçekleştirildi.");
        }
        static void KiradakileriListele()
        {
            // kiradaki araçları listeleme işlemleri burada yapılacak
            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kiralanmış hiç araba yok. Müşteri bul");
                return;
            }
            else
            {
                Console.WriteLine("Plaka                Marka        K. Bedeli        Araba Tipi           K. Sayısı         Durum ");
                Console.WriteLine("----------------------------------------------------------------------------------------------------- \n");
                foreach (Araba item in OtoGaleri.Arabalar)
                {
                    if (item.Durum == "Kirada")
                    {
                        Console.WriteLine(item.Plaka.ToString().PadRight(21) + item.Marka.PadRight(13) + item.KiralamaBedeli.ToString().PadRight(17) + item.AracTipi.PadRight(21) + item.KiralanmaSayisi.ToString().PadRight(18) + item.Durum);
                    }
                }
            }

        }
        static void GaleridekileriListele()
        {
            // Galerideki araçları listeleme işlemleri burada yapılacak
            if (OtoGaleri.GaleridekiAracSayisi == 0)
            {
                Console.WriteLine("Galeride hiç araba yok.");
                return;
            }
            else
            {
                Console.WriteLine("Plaka                Marka        K. Bedeli        Araba Tipi           K. Sayısı         Durum ");
                Console.WriteLine("----------------------------------------------------------------------------------------------------- \n");
                foreach (Araba item in OtoGaleri.Arabalar)
                {
                    if (item.Durum == "Galeride")
                    {
                        Console.WriteLine(item.Plaka.ToString().PadRight(21) + item.Marka.PadRight(13) + item.KiralamaBedeli.ToString().PadRight(17) + item.AracTipi.PadRight(21) + item.KiralanmaSayisi.ToString().PadRight(18) + item.Durum);
                    }
                }
            }
        }
        static void BilgileriGoster()
        {
            Console.WriteLine("- Galeri Bilgileri -");

            Console.WriteLine("Toplam araba sayısı: " + OtoGaleri.ToplamAracSayisi);
            Console.WriteLine("Kiradaki araba sayısı: " + OtoGaleri.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen araba sayısı: " + OtoGaleri.GaleridekiAracSayisi);
            Console.WriteLine("Toplam araba kiralama süresi: " + OtoGaleri.ToplamAracKiralamaSuresi);
            Console.WriteLine("Toplam araba kiralama adedi: " + OtoGaleri.ToplamAracKiralamaAdeti);
            Console.WriteLine("Ciro: " + OtoGaleri.Ciro);
        }
        static void ArabaSil()
        {
            // Araba silme işlemleri burada yapılacak
            Console.WriteLine("- Araba Sil -\n");

            if (OtoGaleri.Arabalar.Count == 0)
            {
                Console.WriteLine("Silinecek araç yok.");
                return;
            }

            string plaka;
            Araba secilenAraba = null;
            bool _flag = false;

            while (true)
            {
                Console.Write("Silinmek istenen araba plakasını girin: ");
                plaka = PlakaAl__3();

                if (plaka == "X") return;

                secilenAraba = null;
                _flag = false;
                foreach (Araba a in OtoGaleri.Arabalar)
                {
                    if (a.Plaka == plaka)
                    {
                        secilenAraba = a;
                        _flag = true;
                        break;
                    }
                }

                if (secilenAraba == null && plaka != "Emre Bayrakcı")
                {

                    //HataliGirisArtirVeKontrol();
                    //Console.WriteLine(plaka);
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    continue;


                }
                if (_flag)
                {
                    if (secilenAraba.Durum == "Kirada")
                    {
                        //HataliGirisArtirVeKontrol();
                        Console.WriteLine("Araba kirada olduğu için silme işlemi gerçekleştirilemedi.");

                        continue;
                    }
                    break;
                }

            }
            if (_flag)
            {
                OtoGaleri.Arabalar.Remove(secilenAraba);
                Console.WriteLine("Araba silindi.");
            }
        }
        static void ArabaEkle()
        {
            Console.WriteLine("- Araba Ekle -\n");
            string plaka = PlakaAl();

            string marka = MarkaAl();
            string arabaTipi = "";

            float kiralamaBedeli = 0;
            Console.Write("Kiralama Bedeli: ");
            while (true)
            {
                if (!(float.TryParse(Console.ReadLine(), out kiralamaBedeli)))
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }
                else break;
            }

            Console.WriteLine("Araba Tipi: \nSUV için 1\nHatchback için 2\nSedan için 3\n");
            while (true)
            {
                Console.Write("Araba tipi: ");
                arabaTipi = Console.ReadLine();
                if (arabaTipi == "1" || arabaTipi == "2" || arabaTipi == "3")
                {
                    if (arabaTipi == "1")
                    {
                        arabaTipi = "SUV";
                        break;
                    }
                    else if (arabaTipi == "2")
                    {
                        arabaTipi = "Hatchback";
                        break;
                    }
                    else if (arabaTipi == "3")
                    {
                        arabaTipi = "Sedan";
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }
            }
            OtoGaleri.ArabaEkle(plaka, marka, kiralamaBedeli, arabaTipi);
            Console.WriteLine("\nAraba başarılı bir şekilde eklendi.");
        }
        static string MarkaAl()
        {
            while (true)
            {
                Console.Write("Araba Markası: ");
                string marka = Console.ReadLine();
                marka = marka.Trim().ToUpper();


                bool sayiVarMi = false;
                foreach (char c in marka)
                {
                    if (char.IsDigit(c))
                    {
                        sayiVarMi = true;
                    }
                }

                if (string.IsNullOrWhiteSpace(marka) || sayiVarMi)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }
                else return marka;
            }

        }
        static string SecimAl()
        {
            string karakterler = "1234567890KTRMAIYSGX";

            int sayac = 0;

            while (true)
            {
                sayac++;
                Console.Write("Seçiminiz: ");
                string giris = Console.ReadLine().ToUpper();
                int index = karakterler.IndexOf(giris);

                Console.WriteLine();

                if (index >= 0)
                {
                    return giris;
                }
                else
                {
                    if (sayac == 10)
                    {
                        Console.WriteLine("Üzgünüm sizi anlayamıyorum. Program sonlandırılıyor.");
                        //Environment.Exit(0);
                        devamMi = false;
                    }
                    Console.WriteLine("Hatalı işlem gerçekleştirildi.Tekrar deneyin.");
                }
                Console.WriteLine();
            }



        }
        static string PlakaAl()
        {
            while (true)
            {
                Console.Write("Plaka: ");
                string plaka = Console.ReadLine().ToUpper();
                if (plaka == "X") return "X";
                if (OtoGaleri.PlakaKontrol(plaka))
                {
                    return plaka;
                }
                else
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                }
            }

        }
        static string PlakaAl__2()
        {
            string plaka = Console.ReadLine().ToUpper();
            if (plaka == "X") return "X";
            if (OtoGaleri.PlakaKontrol(plaka))
            {
                return plaka;
            }
            else
            {
                Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                return null;
            }
        }
        static string PlakaAl__3()
        {
            string plaka = Console.ReadLine().ToUpper();
            if (plaka == "X") return "X";
            if (OtoGaleri.PlakaKontrol(plaka))
            {
                return plaka;
            }
            else
            {
                Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                return "Emre Bayrakcı";
            }
        }
        static void SahteVeriEkle()
        {
            OtoGaleri.ArabaEkle("34ABC34", "Toyota", 500, "Sedan");
            OtoGaleri.ArabaEkle("35DEF35", "Honda", 600, "SUV");
            OtoGaleri.ArabaEkle("36GHI36", "Ford", 550, "Hatchback");

        }
        static void Notlar()
        {
            //case "M":
            //case "4":

            // Environment.Exit(0);               
            // devamMi = false;
            // break;
        }

    }
}



