using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciYonetimUygulamasi
{
    internal class Ogrenci
    {
        public string Ad;
        public string Soyad;
        public string Sube;
        public int No;

        public int MatematikNotu;
        public int FenNotu;
        public int SosyalNotu;
        public int TurkceNotu;

        public float NotOrtalamasıYazdır(int not1, int not2, int not3, int not4)
        {
            float ortalama = (not1 + not2 + not3 + not4)/4;
            return ortalama;
        }
    }


}
