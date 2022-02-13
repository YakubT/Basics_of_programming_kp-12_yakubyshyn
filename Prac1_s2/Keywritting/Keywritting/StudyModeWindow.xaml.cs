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
    /// Логика взаимодействия для StudyModeWindow.xaml
    /// </summary>
    public partial class StudyModeWindow : Window
    {
        int knt = 3;
        const int len = 8;
        const string str = "fyfnjksq";
        List<List<double>> lmas;
        List <double> tmp;
        short f;
        const double n = 7;
        const double tt = 2.57;//n = 7, n' = 6, n'-1=5 при альфа = 0,05 
        public StudyModeWindow()
        {
            InitializeComponent();
            lmas = new List<List<double>>();
            tmp = new List<double>();
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem l = (ListBoxItem) cb.Items[cb.SelectedIndex];
            lab1.Content = "залишилось спроб: " + l.Content.ToString();
            knt = int.Parse(l.Content.ToString());
        }
        Stopwatch stopWatch = new Stopwatch();
        private void wrtinf()
        {
            StreamWriter sw = new StreamWriter("times.txt");
            for (int i=0;i<lmas.Count;i++)
            {
                for (int j = 0; j < lmas[i].Count - 1; j++)
                    sw.Write(lmas[i][j]+" ");
                sw.WriteLine(lmas[i][lmas[i].Count-1]);
            }
            sw.Close();
        }
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void tb_KeyUp(object sender, KeyEventArgs e)
        {
            if (f>0)
                tb.Text = "";
            if (f==2)
            MessageBox.Show("Спроба не зараховується, Ви допустилися помилки");
            else
                if (f==1)
                MessageBox.Show("Спроба записана");
            else
                if (f==3)
                MessageBox.Show("Інтервали часу містять грубу помилку, введіть фразу ще раз");
            f = 0;


        }

        private bool Stuard(List<double> l)
        {
            double M = 0;
            for (int i = 0; i < l.Count; i++)
                M += l[i];
            M /= n - 1;
            for (int i=0;i<l.Count;i++)
            {
                double Mi = M - l[i] / (n - 1);
                double Skv = 0;
                for (int j=0;j<l.Count;j++)
                {
                    if (i == j)
                        continue;
                    Skv += (l[j] - Mi) * (l[j] - Mi) / (n - 2);

                }
                double S = Math.Sqrt(Skv);
                double tp = Math.Abs((l[i] - Mi) / S);
                if (tp > tt)
                    return false;
            }
            return true;
        }
        private void tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (knt != 0)
            {

                //MessageBox.Show(e.Key.ToString());
                if (str[tb.Text.Length].ToString() == e.Key.ToString().ToLower())
                {
                    if (tb.Text.Length == 0)
                    {
                        stopWatch.Restart();
                        f = 0;
                    }
                    else
                    {
                        stopWatch.Stop();
                        tmp.Add(stopWatch.Elapsed.TotalSeconds);
                        stopWatch.Restart();
                    }
                    if (tb.Text.Length == len - 1)
                    {
                        if (!Stuard(tmp))
                        {
                            
                            tmp.Clear();
                            f = 3;
                            return;
                        }

                        lmas.Add(tmp);
                        tmp = new List<double>();
                        knt--;

                        f = 1;
                        lab1.Content = "залишилось спроб: " + knt.ToString();
                        if (knt == 0)
                        {
                            lab1.Content = "залишилось спроб: " + knt.ToString();
                            if (knt == 0)
                            {
                                wrtinf();
                                tmp.Clear();

                                ListBoxItem l = (ListBoxItem)cb.Items[cb.SelectedIndex];
                                lab1.Content = "залишилось спроб: " + l.Content.ToString();
                                knt = int.Parse(l.Content.ToString());
                            }
                        }
                    }
                }
                else
                {


                    tmp.Clear();
                    f = 2;

                }
            }
        }
    }
}
