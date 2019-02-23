using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadirYeriRezervasyonu1
{
    /*
     * Rezervasyon class'indan inherit ederek CadirRezervasyonu class'i eklendi
     */

    /*
     * 1) main() methoddaki menude "oda" kelimelerini "yer" ile degistirin, ana basligi "Cadir Rezervasyonu" olarak degistirin.
     * 2) static method ornegi olarak TarihCetveliniYazdir methodu hazirlayiniz ve
     * DolulukDurumlari methodlarinda(2-30 gunluk doluluk durumu, 3-Gun bazinda doluluk oranlari)  kullanin.
     * 3) yeni GunBazindaDolulukOranlari methodu tanimlayip, menuden cagrilmasini saglayin(Icerisinde TarihCetveliniYazdir methodunu kullanin.)
     * 
     * 4) Rezervasyon sinifindan CadirRezervasyonu sinifini turetip, main() method'da Rezervasyon class'i yerine bu yeni class'i kullanin.
     * 
     * - Kucuk cadir rezervasyonu icin 1 yer ayrilir.
     * - Buyuk cadir rezervasyonu icin yan yana 2 yer ayrilir.
     * 
     * Menudeki (bugun ya da iki tarih arasi) rezervasyon islemlerini kucuk ve buyuk cadir icin ayri ayri tanimlayin.
     * Kucuk cadir rezervasyonu islemleri icin Rezervasyon sinifindaki methodlari kullanin.
     * Buyuk cadir rezervasyonu islemleri icin Rezervasyon sinifindaki methodlari kopyalayarak, CadirRezervasyonu sinifina ekleyip, 
     *             yan yana iki yeri birden kontrol edecek sekilde degistirin.
     * Rezervasyon sinifindaki Write() yapilan "Oda" kelimesini parametrik hale getirin, 
     *             Rezervasyon sinifi tek basina kullanildiginda "Oda",
     *             CadirRezervasyonu sinifi kullanildiginda "Yer" yazilmasini saglayin.
     * 
     */

    class CadirRezervasyonu : Rezervasyon
    {
        public CadirRezervasyonu()
        {
            kiralanan = "Yer";
        }
        public void BugunIcinHizliRezervasyonBuyukCadir()
        {
            IkiTarihArasiRezervasyonBuyukCadir(DateTime.Today, DateTime.Today);
        }
        public void IkiTarihArasiRezervasyonBuyukCadir(DateTime date1, DateTime date2)
        {
            if (date1 < DateTime.Today)
            {
                Console.WriteLine("Baslangic tarih bugunden kucuk olamaz");
                return;
            }
            if (date2 < date1)
            {
                Console.WriteLine("Bitis tarihi baslangic tarihinden kucuk olamaz");
                return;
            }
            if ((date1 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Baslangic tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            if ((date2 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Bitis tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            int gun1 = (date1 - DateTime.Today).Days;
            int gun2 = (date2 - DateTime.Today).Days;
            bool bosOdaYok = true;
            // yerleri ikiser ikiser kontrol et. o yuzden son yerden bir oncesine kadar kontrol et.
            for (int i = 0; i < odaSayisi - 1; i++)
            {
                bool odaMusait = true;
                for (int k = 0; k <= 1; k++) // sonraki odayi da kontrol et.
                {
                    for (int j = gun1; j <= gun2; j++)
                    {
                        if (rezervasyonDurumu[i + k, j] != RezervasyonEnum.Bos)
                        {
                            odaMusait = false;
                            break;
                        }
                    }
                    if ((gun2 + 1) < gunSayisi) // temizlik islemi icin bir sonraki gunun de bos olmasini kontrol ediyor
                    {
                        if (rezervasyonDurumu[i + k, gun2 + 1] != RezervasyonEnum.Bos)
                            odaMusait = false;
                    }
                }
                if (odaMusait)
                {
                    bosOdaYok = false;
                    for (int j = gun1; j <= gun2; j++)
                    {
                        for (int k = 0; k <= 1; k++) // sonraki odayi da rezerve et.
                            rezervasyonDurumu[i + k, j] = RezervasyonEnum.Dolu;
                    }
                    if ((gun2 + 1) < gunSayisi)
                    {
                        for (int k = 0; k <= 1; k++) // sonraki odayi da rezerve et.
                        {
                            rezervasyonDurumu[i + k, gun2 + 1] = RezervasyonEnum.Temizlik;
                        }
                    }
                    Console.WriteLine("{0} ve {1} numarali " + kiralanan.ToLower() + " sizin icin ayrildi", i + 1, i + 2);
                    break;
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Istediginiz tarihte bos " + kiralanan.ToLower() + " yok");
        }
    }

    class Rezervasyon
    {
        protected const int odaSayisi = 10;
        protected const int gunSayisi = 30;
        protected enum RezervasyonEnum
        {
            Bos = 0,
            Dolu = 1,
            Temizlik = 2
        };
        protected RezervasyonEnum[,] rezervasyonDurumu = new RezervasyonEnum[odaSayisi, gunSayisi];

        protected string kiralanan = "Oda";

        public void RasgeleDoldur()
        {
            rezervasyonDurumu[0, 0] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 1] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[0, 5] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 6] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[1, 7] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[1, 8] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[2, 9] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[2, 10] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[5, 15] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 16] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[8, 20] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[8, 21] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[0, 27] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 28] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[9, 29] = RezervasyonEnum.Dolu;

            rezervasyonDurumu[4, 0] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[5, 1] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 2] = RezervasyonEnum.Temizlik;
        }
        public void BugunkuBosOdalar()
        {
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)
            {
                if (rezervasyonDurumu[i, 0] == RezervasyonEnum.Bos
                    && rezervasyonDurumu[i, 1] == RezervasyonEnum.Bos)
                {
                    bosOdaYok = false;
                    Console.WriteLine(i + 1);
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Bugun icin bos " + kiralanan.ToLower() + " yok");
        }
        static void TarihCetveliniYazdir(string onKisim)
        {
            Console.Write(onKisim);
            for (int j = 0; j < gunSayisi; j++)
            {
                Console.Write(" {0:00}", DateTime.Today.AddDays(j).Day);
            }
            Console.WriteLine();
        }
        public void AylikDolulukDurumu()
        {
            TarihCetveliniYazdir("      ");

            for (int i = 0; i < odaSayisi; i++)
            {
                Console.Write(kiralanan + " {0:00}", i + 1); //Oda01, Oda02 satir basliklari icin
                for (int j = 0; j < gunSayisi; j++)
                {
                    if (rezervasyonDurumu[i, j] == RezervasyonEnum.Bos)
                        Console.Write(" - ");
                    else if (rezervasyonDurumu[i, j] == RezervasyonEnum.Dolu)
                        Console.Write(" D ");
                    else
                        Console.Write(" x ");
                }
                Console.WriteLine();
            }
        }
        public void GunBazindaDolulukOranlari()
        {
            TarihCetveliniYazdir(" -------- ");
            Console.Write(       "Doluluk % ");
            int doluOdaSayisi = 0;

            for (int j = 0; j < gunSayisi; j++)
            {
                doluOdaSayisi = 0;
                for (int i = 0; i < odaSayisi; i++)
                {
                    if (rezervasyonDurumu[i, j] == RezervasyonEnum.Dolu)
                    {
                        doluOdaSayisi++;
                    }
                }
                Console.Write("{0,3}", (int)(100f * doluOdaSayisi / odaSayisi));
            }
            Console.WriteLine();
        }
        public void BugunIcinHizliRezervasyon()
        {
            IkiTarihArasiRezervasyon(DateTime.Today, DateTime.Today);
        }
        public void IkiTarihArasiRezervasyon(DateTime date1, DateTime date2)
        {
            if (date1 < DateTime.Today)
            {
                Console.WriteLine("Baslangic tarih bugunden kucuk olamaz");
                return;
            }
            if (date2 < date1)
            {
                Console.WriteLine("Bitis tarihi baslangic tarihinden kucuk olamaz");
                return;
            }
            if ((date1 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Baslangic tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            if ((date2 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Bitis tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            int gun1 = (date1 - DateTime.Today).Days;
            int gun2 = (date2 - DateTime.Today).Days;
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)
            {
                bool odaMusait = true;
                for (int j = gun1; j <= gun2; j++)
                {
                    if (rezervasyonDurumu[i, j] != RezervasyonEnum.Bos)
                    {
                        odaMusait = false;
                        break;
                    }
                }
                if ((gun2 + 1) < gunSayisi)
                {
                    if (rezervasyonDurumu[i, gun2 + 1] != RezervasyonEnum.Bos)
                        odaMusait = false;
                }
                if (odaMusait)
                {
                    bosOdaYok = false;
                    for (int j = gun1; j <= gun2; j++)
                    {
                        rezervasyonDurumu[i, j] = RezervasyonEnum.Dolu;
                    }
                    if ((gun2 + 1) < gunSayisi)
                        rezervasyonDurumu[i, gun2 + 1] = RezervasyonEnum.Temizlik;
                    Console.WriteLine("{0} numarali " + kiralanan.ToLower() + " sizin icin ayrildi", i + 1);
                    break;
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Istediginiz tarihte bos " + kiralanan.ToLower() + " yok");
        }
        public void GunSonuIslemi()
        {
            for (int i = 0; i < odaSayisi; i++)
            {
                for (int j = 0; j < gunSayisi - 1; j++)
                {
                    rezervasyonDurumu[i, j] = rezervasyonDurumu[i, j + 1];
                }
                if (rezervasyonDurumu[i, gunSayisi - 2] == RezervasyonEnum.Dolu)
                    rezervasyonDurumu[i, gunSayisi - 1] = RezervasyonEnum.Temizlik;
                else
                    rezervasyonDurumu[i, gunSayisi - 1] = RezervasyonEnum.Bos;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CadirRezervasyonu rezervasyon = new CadirRezervasyonu();
            rezervasyon.RasgeleDoldur();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("        Cadir Yeri Rezervasyonu");
                Console.WriteLine("1-Bugunku bos yerleri goster");
                Console.WriteLine("2-30 gunluk doluluk durumu");
                Console.WriteLine("3-Gun bazinda doluluk oranlari");
                Console.WriteLine("4-Bugun icin hizli rezervasyon (kucuk cadir)");
                Console.WriteLine("5-Iki tarih arasi rezervasyon (kucuk cadir)");
                Console.WriteLine("6-Bugun icin hizli rezervasyon (buyuk cadir)");
                Console.WriteLine("7-Iki tarih arasi rezervasyon (buyuk cadir)");
                Console.WriteLine("8-Gun sonu islemi");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugunku bos yerler");
                            rezervasyon.BugunkuBosOdalar();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine();
                            Console.WriteLine("30 gunluk doluluk durumu");
                            rezervasyon.AylikDolulukDurumu();
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Gun bazinda doluluk oranlari");
                            rezervasyon.GunBazindaDolulukOranlari();
                            break;
                        }
                    case '4':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugun icin hizli rezervasyon (kucuk cadir)");
                            rezervasyon.BugunIcinHizliRezervasyon();
                            break;
                        }
                    case '5':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Iki tarih arasi rezervasyon (kucuk cadir)");
                            DateTime date1 = DateTime.Today;
                            DateTime date2 = DateTime.Today;
                            try
                            {
                                Console.Write("Rezervasyon baslangic tarihi (gg/aa/yyyy): ");
                                string baslangicTarihi = Console.ReadLine();
                                date1 = Convert.ToDateTime(baslangicTarihi);

                                Console.Write("Rezervasyon bitis tarihi (gg/aa/yyyy): ");
                                string bitisTarihi = Console.ReadLine();
                                date2 = Convert.ToDateTime(bitisTarihi);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Tarih formatina dikkat ediniz");
                            }
                            rezervasyon.IkiTarihArasiRezervasyon(date1, date2);
                            break;
                        }
                    case '6':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugun icin hizli rezervasyon (buyuk cadir)");
                            rezervasyon.BugunIcinHizliRezervasyonBuyukCadir();
                            break;
                        }
                    case '7':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Iki tarih arasi rezervasyon (buyuk cadir)");
                            DateTime date1 = DateTime.Today;
                            DateTime date2 = DateTime.Today;
                            try
                            {
                                Console.Write("Rezervasyon baslangic tarihi (gg/aa/yyyy): ");
                                string baslangicTarihi = Console.ReadLine();
                                date1 = Convert.ToDateTime(baslangicTarihi);

                                Console.Write("Rezervasyon bitis tarihi (gg/aa/yyyy): ");
                                string bitisTarihi = Console.ReadLine();
                                date2 = Convert.ToDateTime(bitisTarihi);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Tarih formatina dikkat ediniz");
                            }
                            rezervasyon.IkiTarihArasiRezervasyonBuyukCadir(date1, date2);
                            break;
                        }
                    case '8':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Gun sonu islemi");
                            rezervasyon.GunSonuIslemi();
                            break;
                        }
                }
            }
        }
    }
}
