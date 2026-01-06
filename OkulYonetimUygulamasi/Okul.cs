using System;
using System.Collections.Generic;
using System.Linq;

namespace OkulYonetimUygulamasi
{
    internal class Okul
    {
        public List<Ogrenci> Ogrenciler = new List<Ogrenci>();

        public Ogrenci OgrenciBul(int no)
        {
            return Ogrenciler.FirstOrDefault(a => a.No == no);
        }

        public bool OgrenciVarMi(int no)
        {
            return Ogrenciler.Any(a => a.No == no);
        }

        public int MusaitNoBul(int istenenNo)
        {
            int no = istenenNo;
            while (OgrenciVarMi(no))
                no++;
            return no;
        }

        public Ogrenci OgrenciEkle(int no, string ad, string soyad, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)
        {
            Ogrenci o = new Ogrenci();
            o.No = no;
            o.Ad = ad;
            o.Soyad = soyad;
            o.DogumTarihi = dogumTarihi;
            o.Cinsiyet = cinsiyet;
            o.Sube = sube;

            Ogrenciler.Add(o);
            return o;
        }

        public bool OgrenciSil(int no)
        {
            var o = OgrenciBul(no);
            if (o == null) return false;
            Ogrenciler.Remove(o);
            return true;
        }

        public void NotEkle(int no, string ders, int not)
        {
            Ogrenci o = OgrenciBul(no);
            if (o != null)
            {
                o.Notlar.Add(new DersNotu(ders, not));
            }
        }

        public void KitapEkle(int no, string kitapAdi)
        {
            var o = OgrenciBul(no);
            if (o != null)
                o.Kitaplar.Add(kitapAdi);
        }

        public void AdresGuncelle(int no, string il, string ilce, string mahalle)
        {
            var o = OgrenciBul(no);
            if (o != null)
            {
                if (o.Adresi == null) o.Adresi = new Adres();
                o.Adresi.Il = il;
                o.Adresi.Ilce = ilce;
                o.Adresi.Mahalle = mahalle;
            }
        }
    }
}
