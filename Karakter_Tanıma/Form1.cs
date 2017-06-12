using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karakter_Tanıma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double[] GelenDizi = new double[35];
        Düğüm d1 = new Düğüm();
        private void buttoneğit_Click(object sender, EventArgs e)
        {
            //double ToplamHata = 0;
            d1.ilkdeğeratama();
            //listBox1.Items.Clear();
            int i = 0;
            do
            {
                d1.net_ve_hata_hesapla(d1.A, d1.beklenenA);
                d1.Ağırlıkları_hesapla(d1.A);
                d1.yeni_değerler();
                //ToplamHata += d1.ToplamHata*d1.ToplamHata;
                //listBox1.Items.Add(d1.ToplamHata);

                d1.net_ve_hata_hesapla(d1.B, d1.beklenenB);
                d1.Ağırlıkları_hesapla(d1.B);
                d1.yeni_değerler();
                //ToplamHata += d1.ToplamHata * d1.ToplamHata;
                // listBox1.Items.Add(d1.ToplamHata);

                d1.net_ve_hata_hesapla(d1.C, d1.beklenenC);
                d1.Ağırlıkları_hesapla(d1.C);
                d1.yeni_değerler();
                //ToplamHata += d1.ToplamHata * d1.ToplamHata;
                // listBox1.Items.Add(d1.ToplamHata);

                d1.net_ve_hata_hesapla(d1.D, d1.beklenenD);
                d1.Ağırlıkları_hesapla(d1.D);
                d1.yeni_değerler();
               // ToplamHata += d1.ToplamHata * d1.ToplamHata;
                // listBox1.Items.Add(d1.ToplamHata);

                d1.net_ve_hata_hesapla(d1.E, d1.beklenenE);
                d1.Ağırlıkları_hesapla(d1.E);
                d1.yeni_değerler();
                i++;
                //ToplamHata += d1.ToplamHata * d1.ToplamHata;
               // ToplamHata = Math.Sqrt(ToplamHata);
                // listBox1.Items.Add(d1.ToplamHata);
            } while (d1.ToplamHata > (double)(numericUpDown1.Value));

            label1.Text = i.ToString();

            MessageBox.Show("Eğtim Tamamlandı");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.BackColor == Color.White)
            {
                btn.BackColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }

        private void buttonTanımla_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            int i = 0;
            foreach (Control item in groupBox1.Controls)
            {
                if (item is Button)
                {
                    if (item.BackColor == Color.Black)
                    {
                        GelenDizi[i] = 1;
                    }
                    else
                    {
                        GelenDizi[i] = 0;
                    }
                    i++;
                }
            }
            i = 0;
            for (int j = 0; j < 35; j++)
            {
                label1.Text += GelenDizi[j].ToString() + ",";
            }    

            d1.net_ve_hata_hesapla(GelenDizi, d1.beklenenA);
            //d1.Ağırlıkları_hesapla(GelenDizi);
            //d1.yeni_değerler();

            labelA.Text = d1.çıkışlar[0].ToString();
           
            d1.net_ve_hata_hesapla(GelenDizi, d1.beklenenB);
            //d1.Ağırlıkları_hesapla(GelenDizi);
            //d1.yeni_değerler();

            labelB.Text = d1.çıkışlar[1].ToString();

            d1.net_ve_hata_hesapla(GelenDizi, d1.beklenenC);
            //d1.Ağırlıkları_hesapla(GelenDizi);
            //d1.yeni_değerler();

            labelC.Text = d1.çıkışlar[2].ToString();

            d1.net_ve_hata_hesapla(GelenDizi, d1.beklenenD);
            //d1.Ağırlıkları_hesapla(GelenDizi);
            //d1.yeni_değerler();

            labelD.Text = d1.çıkışlar[3].ToString();

            d1.net_ve_hata_hesapla(GelenDizi, d1.beklenenE);
            //d1.Ağırlıkları_hesapla(GelenDizi);
            //d1.yeni_değerler();

            labelE.Text = d1.çıkışlar[4].ToString();

        }
    }
}
