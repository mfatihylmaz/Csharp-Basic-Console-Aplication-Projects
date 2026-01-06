using System;
using System.Collections.Generic;
using System.Linq;

namespace OkulYonetimUygulamasi
{
    internal class Ogrenci
    {
        public int No { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }

        public float Ortalama
        {
            get
            {
                if (Notlar == null || Notlar.Count == 0) return 0;
                return (float)Notlar.Average(a => a.Not);
            }
        }

        public SUBE Sube { get; set; }
        public CINSIYET Cinsiyet { get; set; }

        public Adres Adresi { get; set; } = new Adres();

        public List<DersNotu> Notlar { get; set; } = new List<DersNotu>();
        public List<string> Kitaplar { get; set; } = new List<string>();
    }

    public enum SUBE
    {
        Empty, A, B, C
    }

    public enum CINSIYET
    {
        Empty, Kiz, Erkek
    }
}
