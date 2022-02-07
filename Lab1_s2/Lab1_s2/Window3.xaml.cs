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
using System.IO;
namespace Lab1_s2
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private bool is_fill()
        {
            for (int i=1;i<=4;i++)
            {
                TextBox tb = this.FindName("t" + i.ToString()) as TextBox;
                if (tb.Text == "")
                    return false;
            }
            return true;
        }
        private void addfunc()
        {
            StreamWriter fout = new StreamWriter("data.txt", true);
            string s = t1.Text;
            s = s.Replace(' ', '_');
            fout.WriteLine(s+" "+t2.Text+" "+t3.Text+" "+t4.Text);
            fout.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!is_fill())
            {
                MessageBox.Show("Не всі поля заповнені");
                return;
            }
            StreamReader fin;
            bool add = true;
            try
            {
                fin = new StreamReader("data.txt");
                while (!fin.EndOfStream)
                {
                    string s = fin.ReadLine();
                    string[] smas = s.Split();
                    if (smas[3]==t4.Text)
                    {
                        add = false;
                        break;
                    }
                }
                fin.Close();
            }
            catch
            {
                add = true;
            }
            
            if (!add)
            {
                MessageBox.Show("Студент з такою заліковою книжкою уже є в системі");
            }
            else
            {
                addfunc();
                MessageBox.Show("Операція пройшла успішно");
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StreamReader fin;
            try
            {
                fin = new StreamReader("data.txt");
                bool f = false;
                string res = "";
                while (!fin.EndOfStream)
                {
                    string s = fin.ReadLine();
                    string[] smas = s.Split();
                    if (smas[3] == t4_Copy.Text)
                    {
                        f = true;
                    }
                    else
                        res += s+"\n";
                }
                fin.Close();

                if (f)
                {
                    StreamWriter fout = new StreamWriter("data.txt");
                    fout.Write(res);
                    MessageBox.Show("Операція пройшла успішно");
                    fout.Close();
                }
                else
                {
                    MessageBox.Show("Студента з такою заліковою немає");
                }
                
            }
            catch
            {
                MessageBox.Show("Файла зі студентами ще не існує (нуль студентів в базі)");
                return;
            }
        }
    }
}
