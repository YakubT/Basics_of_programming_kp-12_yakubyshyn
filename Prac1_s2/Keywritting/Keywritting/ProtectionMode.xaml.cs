using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Keywritting
{
    /// <summary>
    /// Логика взаимодействия для ProtectionMode.xaml
    /// </summary>
    public partial class ProtectionMode : Window
    {
        int knt;
        const string str = "jmnmzkck";
        Stopwatch stopWatch = new Stopwatch();
        List<double> tmp= new List<double>();
        int f;
        int count_good;
        int countneodnor;
        int count_good_ident;
        int zag;
        List<List<double>> table;
        const int n = 7;
        const double Ft = 4.28;
        const double tt = 2.45;
        public ProtectionMode()
        {
            InitializeComponent();
            knt = 5;
            count_good = 0;
            countneodnor = 0;
            count_good_ident = 0;
            table = new List<List<double>>();
            zag = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
        
        private double D (List <double> l)
        {
            double M=0;
            foreach (double el in l)
                M += el;
            M /= l.Count;
            double D=0;
            foreach (double el in l)
                D += (el - M) * (el - M);
            D /= l.Count;
            return D;
        }
        private double M(List <double> l)
        {
            double M = 0;
            foreach (double el in l)
                M += el;
            M /= n;
            return M;
        }
        private bool readfromfile()
        {
            StreamReader fin;
            try
            {
                fin = new StreamReader("times.txt");
                while (!fin.EndOfStream)
                {
                    string[] smas = fin.ReadLine().Split();
                    table.Add(new List<double>());
                    for (int i=0;i<smas.Length;i++)
                    {
                        smas[i] = smas[i].Replace('.', ',');
                        table[table.Count - 1].Add(double.Parse(smas[i]));
                    }
                }
                fin.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Файлу з еталонними характеристиками нема, перейдіть в розділ навчання");
            }
            return false;
        }
        double proc;
        private void TB2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (knt != 0)
            {

                
                if (str[TB2.Text.Length].ToString() == e.Key.ToString().ToLower())
                {
                    if (TB2.Text.Length == 0)
                    {
                        tmp = new List<double>();
                        stopWatch.Restart();
                        f = 0;
                    }
                    else
                    {
                        stopWatch.Stop();
                        tmp.Add(stopWatch.Elapsed.TotalSeconds);
                        stopWatch.Restart();
                    }
                    if (TB2.Text.Length == str.Length - 1)
                    {
                        bool g = readfromfile();
                        if (!g)
                        {
                            f = 3;
                            tmp.Clear();
                            return;
                        }
                        double S2kv = D(tmp);
                        double cnt = 0;
                        for (int i=0;i<table.Count;i++)
                        {
                            
                           double S1kv = D(table[i]);
                            double Fp = Math.Max(S1kv, S2kv)/Math.Max(S1kv,S2kv);
                            if (Fp > Ft)
                                countneodnor++;
                            double Sxkv = S1kv * n / (n - 1);
                            double Sykv = S2kv * n / (n - 1);
                            //MessageBox.Show(Sxkv.ToString());
                            double S = Math.Sqrt((Sxkv+Sykv)*(n-1)/(2*n-1));
                            double tp= Math.Abs(M(table[i]) - M(tmp)) / (S * Math.Sqrt(2.0 / n));
                           // MessageBox.Show(tp.ToString());
                            if (tp <= tt)
                            {
                                count_good++;
                                cnt++;
                            }
                            zag++;
                        }
                         proc = (double) cnt / table.Count;
                        if (proc > 0.85)
                        {
                            count_good_ident++;
                            StreamWriter fout = new StreamWriter("times.txt", true);
                            for (int i = 0; i < tmp.Count - 1; i++)
                                fout.Write(tmp[i] + " ");
                            fout.WriteLine(tmp[tmp.Count-1]);
                            fout.Close();
                        }
                        knt--;
                        f = 1;
                        
                    }
                }
                else
                {
                    bool g = readfromfile();
                    if (!g)
                    {
                        f = 3;
                        tmp.Clear();
                        return;
                    }
                    zag += table.Count;
                    countneodnor += table.Count;
                   
                    tmp.Clear();
                    f = 2;
                    knt--;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem l = (ListBoxItem)CB.Items[CB.SelectedIndex];
            knt = int.Parse(l.Content.ToString());
            countneodnor = 0;
            count_good_ident = 0;
            count_good = 0;
            zag = 0;
        }

        private void TB2_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (f > 0)
                TB2.Text = "";
            if (f==1)
            {
                if (proc > 0.85)
                    MessageBox.Show("Вдала спроба (автентифікація пройшла)");
                else
                    MessageBox.Show("Невдала спроба (автентифікація не пройшла)");
                MessageBox.Show("Залишилось спроб: " + knt.ToString());
               

            }
                else
            if (f == 3)
                TB2.Text = "";
            else
            if (f==2)
            {
                MessageBox.Show("Ви допустилися помилки");
                MessageBox.Show("Залишилось спроб: " + knt.ToString());
                TB2.Text = "";
            }
            if (knt == 0)
            {
                lbP.Content = Math.Round(((double)count_good / zag),2).ToString();

                if (countneodnor > zag - countneodnor)
                    Ld.Content = "Дисперсії вибірок неоднорідні";
                else
                    Ld.Content = "Дисперсії вибірок однорідні";
                if (CHB.IsChecked == true)
                {
                    P1.Content = ((double.Parse(CB.Text.ToString()) - count_good_ident) / (double.Parse(CB.Text.ToString()))).ToString();
                }
                else
                    P2.Content = (count_good_ident/ (double.Parse(CB.Text.ToString()))).ToString();
                knt = int.Parse(CB.Text);
                count_good_ident = 0;
                count_good = 0;
                zag = 0;
                countneodnor = 0;
            }
            f = 0;
        }

        private void сhb_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TB2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
