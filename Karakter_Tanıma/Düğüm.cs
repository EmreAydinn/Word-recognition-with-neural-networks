using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karakter_Tanıma
{
    public class Düğüm
    {
        public Düğüm Önceki;            // boşuna olabilir.

        // girişler A,B,C,D,E
        public double[] A = {0,1,1,1,0,1,0,0,0,1,1,0,0,0,1,1,1,1,1,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1 };
        public double[] B = {1,1,1,1,0,1,0,0,0,1,1,0,0,0,1,1,1,1,1,0,1,0,0,0,1,1,0,0,0,1,1,1,1,1,0 };
        public double[] C = {0,1,1,1,1,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,1,1,1,1 };
        public double[] D = {1,1,1,0,0,1,0,0,1,0,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,1,0,1,1,1,0,0 };
        public double[] E = {1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,1,1,1,1,0,1,0,0,0,0,1,0,0,0,0,1,1,1,1,1 };

        // beklenen değerler
        public double[] beklenenA = { 1, 0, 0, 0, 0 };
        public double[] beklenenB = { 0, 1, 0, 0, 0 };
        public double[] beklenenC = { 0, 0, 1, 0, 0 };
        public double[] beklenenD = { 0, 0, 0, 1, 0 };
        public double[] beklenenE = { 0, 0, 0, 0, 1 };

        public double öğrenme = 0.5;
        public double momentum = 0.8;

        public double[,] giriş_ara_ağırlık = new double[35,10];
        public double[,] çıkış_ara_ağırlık = new double[10,5];

        public double[,] giriş_ara_ağırlık_önceki_değişimi = new double[35, 10];
        public double[,] çıkış_ara_ağırlık_önceki_değişimi = new double[10, 5];

        public double[] aradeğer = new double[10];      // ara karmandaki proses elemanların çıkış değerleri.

        public double[] aradeğer_hata = new double[10];     // // ara karmandaki proses elemanların hata değerleri.

        public double ToplamHata = 0;
        public double[] yardımcılar = new double[10];

        public double[] çıkışlar = new double[5];           // çıktı değerleri  
        public double[] çıkış_hataları = new double[5];          // çıktı hata değerleri

        public double[] bias_giriş = new double[10];
        public double[] bias_çıkış = new double[5];

        public double[] bias_giriş_ağırlık_değişimi = new double[10];
        public double[] bias_çıkış_ağırlık_değişimi = new double[5];

        Random rnd = new Random();

        // giriş_ara_ağırlık_önceki_değişimi ve çıkış_ara_ağırlık_önceki_değişimine ve bias değişimlerine başlangıç olarak 0 atandı.
        //#region propertyler         
        //public double[,] Giriş_ara_ağırlık_önceki_değişimi
        //{
        //    get
        //    {
        //        return giriş_ara_ağırlık_önceki_değişimi;
        //    }

        //    set
        //    {
        //        for (int i = 0; i < 35; i++)
        //        {
        //            for (int j = 0; j < 10; j++)
        //            {
        //                giriş_ara_ağırlık_önceki_değişimi[i, j] = 0;
        //            }
        //        }
        //    }
        //}

        //public double[,] Çıkış_ara_ağırlık_önceki_değişimi
        //{
        //    get
        //    {
        //        return çıkış_ara_ağırlık_önceki_değişimi;
        //    }

        //    set
        //    {
        //        for (int i = 0; i < 10; i++)
        //        {
        //            for (int j = 0; j < 5; j++)
        //            {
        //                çıkış_ara_ağırlık_önceki_değişimi[i, j] = 0;
        //            }
        //        }
        //    }
        //}

        //public double[] Bias_giriş_ağırlık_değişimi
        //{
        //    get
        //    {
        //        return bias_giriş_ağırlık_değişimi;
        //    }

        //    set
        //    {
        //        for (int i = 0; i < 10; i++)
        //        {
        //            bias_giriş_ağırlık_değişimi[i] = 0;
        //        }
        //    }
        //}

        //public double[] Bias_çıkış_ağırlık_değişimi
        //{
        //    get
        //    {
        //        return bias_çıkış_ağırlık_değişimi;
        //    }

        //    set
        //    {
        //        for (int i = 0; i < 5; i++)
        //        {
        //            bias_çıkış_ağırlık_değişimi[i] = 0;
        //        }
        //    }
        //}

        //#endregion                      // 

        public void ilkdeğeratama()     // başlangıç rastgele değer atanması işlemleri burada yapıldı
        {
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j <10; j++)
                {
                    giriş_ara_ağırlık[i, j] = rnd.NextDouble();
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    çıkış_ara_ağırlık[i, j] = rnd.NextDouble();
                }
            }
            for (int i = 0; i < 10; i++)
            {
                bias_giriş[i] = rnd.NextDouble();
            }
            for (int i = 0; i < 5; i++)
            {
                bias_çıkış[i] = rnd.NextDouble();
            }
        }

        public double sigmoid_hesabı(double değer)
        {
            return 1 / (1 + Math.Exp(-değer));
        }

        public void net_ve_hata_hesapla(double[] dizi, double[] beklenen)
        {
            ToplamHata = 0;
            for (int i = 0; i < 10; i++)
            {
                aradeğer[i] = 0;
                aradeğer_hata[i] = 0;
                yardımcılar[i] = 0;
            }
            for (int i = 0; i < 5; i++)
            {
                çıkışlar[i] = 0;
                çıkış_hataları[i] = 0;
            }


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 35; j++)
                {
                    aradeğer[i] = aradeğer[i] + (dizi[j] * giriş_ara_ağırlık[j,i]);
                }
                aradeğer[i] = aradeğer[i] + (bias_giriş[i] * 1);
            }

            for (int i = 0; i < 10; i++)
            {
                aradeğer[i] = sigmoid_hesabı(aradeğer[i]);
            }


            for (int i = 0; i < 5; i++)     // çıkışların neti hesaplandı.
            {
                for (int j = 0; j < 10; j++)
                {
                    çıkışlar[i] = çıkışlar[i] + (aradeğer[j] * çıkış_ara_ağırlık[j, i]) ;
                }
                çıkışlar[i] = çıkışlar[i] + (bias_çıkış[i] * 1);
            }
            for (int i = 0; i < 5; i++)     // sigmoidden geçirildi.
            {
                çıkışlar[i] = sigmoid_hesabı(çıkışlar[i]);
            }


            for (int i = 0; i < 5; i++)     // çıkışların hataları hesaplandı.
            {
                çıkış_hataları[i] = (beklenen[i] - çıkışlar[i]) * çıkışlar[i] * (1 - çıkışlar[i]);
            }
          
            for (int i = 0; i < 5; i++)         // belki sıkıntı olabilir.
            {
                for (int j = 0; j < 10; j++)
                {
                    yardımcılar[j] = yardımcılar[j] + (çıkış_hataları[i] * çıkış_ara_ağırlık[j, i]);
                    
                }
            }
            for (int i = 0; i < 10; i++)                     // ara çıkışların hataları hesaplandı.
            {
                aradeğer_hata[i] = aradeğer[i] * (1 - aradeğer[i]) * yardımcılar[i];
            }

            for (int i = 0; i < 5; i++)
            {
                ToplamHata = ToplamHata + (çıkış_hataları[i] * çıkış_hataları[i]);
            }

            //ToplamHata = ToplamHata + (aradeğer1_hata * aradeğer1_hata);
            //ToplamHata = ToplamHata + (aradeğer2_hata * aradeğer2_hata);

            ToplamHata = Math.Sqrt(ToplamHata);                                         // Toplam Hata Hesaplandı.
        }

        public void Ağırlıkları_hesapla (double[] dizi)
        {
            // çıkış ağırlıklarının değişim miktarı hesaplandı.
            for (int i = 0; i < 5; i++)    
            {
                for (int j = 0; j < 10; j++)
                {
                    çıkış_ara_ağırlık_önceki_değişimi[j, i] = (öğrenme * aradeğer[j] * çıkış_hataları[i]) + (momentum * çıkış_ara_ağırlık_önceki_değişimi[j, i]);
                }
            }
           
            for (int i = 0; i < 5; i++)     // biasda hata var biasgirişleri ve çıkışları işleme katılmamalı.
            {
                bias_çıkış_ağırlık_değişimi[i] = (öğrenme * bias_çıkış[i] * çıkış_hataları[i]) + (momentum * bias_çıkış_ağırlık_değişimi[i]);
            }

            // giriş ağırlıklarının değişim miktarı hesaplandı.
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    giriş_ara_ağırlık_önceki_değişimi[i, j] = (öğrenme * dizi[i] * aradeğer_hata[j]) + (momentum * giriş_ara_ağırlık_önceki_değişimi[i, j]);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                bias_giriş_ağırlık_değişimi[i] = (öğrenme * bias_giriş[0] * aradeğer_hata[i]) + (momentum * bias_giriş_ağırlık_değişimi[i]);
            }
        }

        public void yeni_değerler()
        {
            // giriş ve çıkış ağırlıklarına yeni değerler atandı.
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    giriş_ara_ağırlık[i, j] = giriş_ara_ağırlık[i, j] + giriş_ara_ağırlık_önceki_değişimi[i,j];
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    çıkış_ara_ağırlık[i, j] = çıkış_ara_ağırlık[i, j] + çıkış_ara_ağırlık_önceki_değişimi[i, j];
                }
            }

            // giriş ve çıkış bias larına yeni değerler atandı.
            for (int i = 0; i < 10; i++)
            {
                bias_giriş[i] = bias_giriş[i] + bias_giriş_ağırlık_değişimi[i];
            }
            for (int i = 0; i < 5; i++)
            {
                bias_çıkış[i] = bias_çıkış[i] + bias_çıkış_ağırlık_değişimi[i];
            }
        }
    }
}
