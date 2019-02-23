using System;

namespace OtelCadirRezervasyonu2
{
    /*
     * 1) Operator overloading (static polymorphism) calismasi icin: 
     *     RezervasyonYap methodu 3 farkli sekilde implement edilir, 1. olarak parametresiz, 
     *     ikinci olarak iki tarih parametreli, ucuncu olarak baslangic tarihi ve integer gun parametreli
     * 2) Dynamic polymorphism calismasi icin: 
     *     Abstract hale getirilmis Rezervasyon class'indan inherit ederek ButikOtelRezervasyonu ve CadirRezervasyonu class'lari eklenir
     * 3) Menu kodu polymorphism uygulanmis olan class'i kullanarak yazilir
     * 4) Inherit edilen class'larin davranisini farklilastirmak icin yeni metodlar eklenir.
     *     KiralananYerTipi
     *     UygulamaAdi
     *     YanYanaIkiYerBirdenRezervasyonYapilabilirMi
     * 5) Base class'taki RezervasyonYap metoduna buyuk cadir rezervasyonu yapabilmek icin bir parametre eklenir 
     * 6) Polymorphism ornegini pekistirmek icin iki farkli tip icin farkli fiyat hesaplama metodlari eklenir.
     * 7) CadirYeri icin buyuk ve kucuk yer rezervasyonu tek fonksiyonda birlestirilir.
     * 8) Program her basladiginda iki tipten birisini rasgele secerek baslamasi saglanir.
     * 9) Yat limani rezervasyonu class'i eklenir.
     * 10) Ertesi gunu temizlik icin bos birakma ozelligi de temel sinifa eklenecek bir metod ile belirlenir.
     *     Bu fonksiyonun default implementation'inda "yapilacak" olarak ayarlanir.
     *     Yat limani uygulamasinda ise temizlik icin ertesi gun bos birakilacak ayari "hayir" olarak ayarlanir.
     */

    class ButikOtelRezervasyonu : Rezervasyon
    {
        protected override string KiralananYerTipi()
        {
            return "Oda";
        }
        public override string UygulamaAdi()
        {
            return "Butik Otel Rezervasyonu";
        }
        public override bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi()
        {
            return false;
        }
        public override int Fiyat(int gun, bool ciftYer)
        {
            // gunluk 100 TL, 5 gunden fazla ise gunluk 90 TL.
            return gun * (gun > 5 ? 90 : 100);
        }
    }

    class CadirRezervasyonu : Rezervasyon
    {
        protected override string KiralananYerTipi()
        {
            return "Yer";
        }
        public override string UygulamaAdi()
        {
            return "Cadir Yeri Rezervasyonu";
        }
        public override bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi()
        {
            return true;
        }
        public override int Fiyat(int gun, bool ciftYer)
        {
            // kucuk cadir icin tek yer gunluk 100 TL. 
            // buyuk cadir icin cift yer gunluk 160 TL.
            return gun * (ciftYer ? 160 : 100);
        }
    }

    class YatLimaniRezervasyonu : Rezervasyon
    {
        protected override string KiralananYerTipi()
        {
            return "Yer";
        }
        public override string UygulamaAdi()
        {
            return "Yat Limani Rezervasyonu";
        }
        public override bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi()
        {
            return true;
        }
        public override int Fiyat(int gun, bool ciftYer)
        {
            // gunluk 1000 TL. cift yer icin iki kati.
            return gun * 1000 * (ciftYer ? 2 : 1);
        }
        protected override bool ErtesiGunTemizlikIcinBosBirakilacakMi()
        {
            // Yat limani uygulamasinda temizlik icin ertesi gunun bos birakilmasina gerek yoktur.
            return false;
        }
    }

    abstract class Rezervasyon
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

        protected abstract string KiralananYerTipi();
        public abstract string UygulamaAdi();
        public abstract bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi();
        public abstract int Fiyat(int gun, bool ciftYer);
        protected virtual bool ErtesiGunTemizlikIcinBosBirakilacakMi()
        {
            // Bu fonksiyonun default implementation'inda "temizlik icin ertesi gun bos birakilacak" olarak ayarlanir.
            return true;
        }

        public void RasgeleDoldur()
        {
            rezervasyonDurumu[0, 0] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 1] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[0, 5] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 6] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[1, 7] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[1, 8] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[2, 9] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[2, 10] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[5, 15] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 16] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[8, 20] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[8, 21] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[0, 27] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 28] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[9, 29] = RezervasyonEnum.Dolu;

            rezervasyonDurumu[4, 0] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
            rezervasyonDurumu[5, 1] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 2] = (ErtesiGunTemizlikIcinBosBirakilacakMi() ? RezervasyonEnum.Temizlik : RezervasyonEnum.Bos);
        }
        public void BugunkuBosOdalar()
        {
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)
            {
                if (rezervasyonDurumu[i, 0] == RezervasyonEnum.Bos)
                {
                    if (ErtesiGunTemizlikIcinBosBirakilacakMi() == false
                        || rezervasyonDurumu[i, 1] == RezervasyonEnum.Bos)
                    {
                        // bos oda bulundu
                        bosOdaYok = false;
                        Console.WriteLine(i + 1);
                    }
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Bugun icin bos " + KiralananYerTipi().ToLower() + " yok");
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
                Console.Write(KiralananYerTipi() + " {0:00}", i + 1); //Oda01, Oda02 satir basliklari icin
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
            Console.Write("Doluluk % ");
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
        // bugun icin tek gunluk rezervasyon yapar
        public void RezervasyonYap(bool yanYanaIkiRezervasyon = false) 
        {
            RezervasyonYap(DateTime.Today, DateTime.Today, yanYanaIkiRezervasyon);
        }
        // baslangic tarihinden itibaren verilen gun sayisi kadar rezervasyon yapar
        public void RezervasyonYap(DateTime basTarihi, int gun, bool yanYanaIkiRezervasyon = false) 
        {
            RezervasyonYap(basTarihi, basTarihi.AddDays(gun), yanYanaIkiRezervasyon);
        }
        // verilen iki tarih arasinda rezervasyon yapar
        public void RezervasyonYap(DateTime date1, DateTime date2, bool yanYanaIkiRezervasyon = false) 
        {
            if (yanYanaIkiRezervasyon == true && YanYanaIkiYerBirdenRezervasyonYapilabilirMi() == false)
            {
                Console.WriteLine("Ayni rezervasyonda yan yana iki yer ayrilmasina izin verilmiyor");
                return;
            }
            if (date1 < DateTime.Today)
            {
                Console.WriteLine("Baslangic tarihi bugunden kucuk olamaz");
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
                // yerleri ikiser ikiser kontrol ederken, hep bir sonraki yere de bak.
                // son yerden bir onceki yere kadar devam et.
                if (yanYanaIkiRezervasyon && i == (odaSayisi - 1))
                    break;

                bool odaMusait = true;
                // yan yana iki yer isteniyorsa, ayni anda yandaki yeri de kontrol et.
                for (int k = 0; k <= (yanYanaIkiRezervasyon ? 1 : 0); k++)
                {
                    for (int j = gun1; j <= gun2; j++)
                    {
                        if (rezervasyonDurumu[i + k, j] != RezervasyonEnum.Bos)
                        {
                            odaMusait = false;
                            break;
                        }
                    }
                    // temizlik islemi icin bir sonraki gunun de bos olmasini kontrol et
                    if (ErtesiGunTemizlikIcinBosBirakilacakMi())
                    {
                        if (gun2 < (gunSayisi - 1))
                        {
                            if (rezervasyonDurumu[i + k, gun2 + 1] != RezervasyonEnum.Bos)
                                odaMusait = false;
                        }
                    }
                }
                if (odaMusait)
                {
                    bosOdaYok = false;
                    for (int j = gun1; j <= gun2; j++)
                    {
                        for (int k = 0; k <= (yanYanaIkiRezervasyon ? 1 : 0); k++) // sonraki yeri de rezerve et.
                            rezervasyonDurumu[i + k, j] = RezervasyonEnum.Dolu;
                    }
                    // bitis tarihi son gun degilse, sonraki gunu temizlik icin ayir
                    if (ErtesiGunTemizlikIcinBosBirakilacakMi())
                    {
                        if (gun2 < (gunSayisi - 1))
                        {
                            for (int k = 0; k <= (yanYanaIkiRezervasyon ? 1 : 0); k++) // sonraki yeri de rezerve et.
                                rezervasyonDurumu[i + k, gun2 + 1] = RezervasyonEnum.Temizlik;
                        }
                    }
                    Console.WriteLine("{0} " 
                        + (yanYanaIkiRezervasyon ? "ve {1} ":"")
                        + "numarali " + KiralananYerTipi().ToLower() + " sizin icin ayrildi", i + 1, i + 2);
                    // Fiyat hesaplamasini goster
                    Console.WriteLine("Toplam fiyat : {1} TL, {0} gun.", 
                        (date2-date1).Days + 1, 
                        this.Fiyat((date2-date1).Days + 1, yanYanaIkiRezervasyon));
                    break;
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Istediginiz tarihte bos " + KiralananYerTipi().ToLower() + " yok");
        }
        public void GunSonuIslemi()
        {
            for (int i = 0; i < odaSayisi; i++)
            {
                for (int j = 0; j < gunSayisi - 1; j++)
                {
                    rezervasyonDurumu[i, j] = rezervasyonDurumu[i, j + 1];
                }
                // son gun dolu ise gunleri kaydirdiktan sonra ertesi gunu temizlik icin ayir.
                if (rezervasyonDurumu[i, gunSayisi - 2] == RezervasyonEnum.Dolu
                    && ErtesiGunTemizlikIcinBosBirakilacakMi())
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
            Rezervasyon rezervasyon;
            // her calistiginda uc tipten birisini rasgele sec.
            int a = new Random().Next(100);
            if (a < 33)
            {
                rezervasyon = new ButikOtelRezervasyonu();
            }
            else if (a < 67)
            {
                rezervasyon = new CadirRezervasyonu();
            }
            else
            {
                rezervasyon = new YatLimaniRezervasyonu();
            }

            rezervasyon.RasgeleDoldur();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("        "+rezervasyon.UygulamaAdi());
                Console.WriteLine("   1- Bugunku bos yerleri goster");
                Console.WriteLine("   2- 30 gunluk doluluk durumu");
                Console.WriteLine("   3- Gun bazinda doluluk oranlari");
                Console.WriteLine("   4- Bugun icin hizli rezervasyon");
                Console.WriteLine("   5- Iki tarih arasi rezervasyon");
                char gunSonuIslemi = '6';
                if (rezervasyon.YanYanaIkiYerBirdenRezervasyonYapilabilirMi())
                {
                    Console.WriteLine("   6- Bugun icin hizli rezervasyon (yan yana iki yer)");
                    Console.WriteLine("   7- Iki tarih arasi rezervasyon (yan yana iki yer)");
                    gunSonuIslemi = '8';
                }
                Console.WriteLine("   {0}- Gun sonu islemi", gunSonuIslemi);

                char c = Console.ReadKey().KeyChar;
                switch (c)
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
                            Console.WriteLine("Bugun icin hizli rezervasyon");
                            rezervasyon.RezervasyonYap();
                            break;
                        }
                    case '5':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Iki tarih arasi rezervasyon");
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
                            rezervasyon.RezervasyonYap(date1, date2);
                            break;
                        }
                    case '6':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugun icin hizli rezervasyon (yan yana iki yer)");
                            rezervasyon.RezervasyonYap(yanYanaIkiRezervasyon: true);
                            break;
                        }
                    case '7':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Iki tarih arasi rezervasyon (yan yana iki yer)");
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
                            rezervasyon.RezervasyonYap(date1, date2, yanYanaIkiRezervasyon: true);
                            break;
                        }
                    default:
                        {
                            if (c == gunSonuIslemi)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Gun sonu islemi");
                                rezervasyon.GunSonuIslemi();
                            }
                            break;
                        }
                }
            }
        }
    }
}
