using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace Lab1_s2
{
    class Logic
    {
        private Window w;
        private void w2close(object sender, RoutedEventArgs e)
        {
            
            w.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
        private TextBox findeltb(string s)
        {
            Grid g = (Grid)w.Content;
            foreach (TextBox t in g.Children.OfType<TextBox>())
                if (t.Name == s)
                    return t;
            return new TextBox();
        }
        private bool is_fill()
        {
            Grid g = (Grid)w.Content;
            foreach (TextBox t in g.Children.OfType<TextBox>())
            {
                if (t.Name!="t5")
                {
                    if (t.Text == "")
                        return false;
                }
            }
            return true;
        }
        private void b1clw2(object sender, RoutedEventArgs e)
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
                    if (smas[3] == findeltb("t4").Text)
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
        private void bclw2(object sender, RoutedEventArgs e)
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
                    if (smas[3] == findeltb("t5").Text)
                    {
                        f = true;
                    }
                    else
                        res += s + "\n";
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
    
        private void addfunc()
        {
            StreamWriter fout = new StreamWriter("data.txt", true);
            Grid g = (Grid)w.Content;
            TextBox[] tb = new TextBox[6];
            foreach (TextBox t in g.Children.OfType<TextBox>())
            {
                tb[int.Parse(t.Name.Substring(1))] = t;
            }
            string s = tb[1].Text;
            s = s.Replace(' ', '_');
            fout.WriteLine(s+" "+tb[2].Text+" "+tb[3].Text+" "+tb[4].Text);
            fout.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void setting2()
        {
            Grid g = (Grid)w.Content;
            Button[] bmas = new Button[4];
            foreach (Button b in g.Children.OfType<Button>())
            {
                bmas[int.Parse(b.Name.Substring(1))] = b;

            }
            bmas[1].Click += b1clw2;
            bmas[2].Click += bclw2;
            bmas[3].Click += w2close;
            w.Closed += Window_Closed;
        }
        private void setting3()
        {

        }
        public void SetLogic(Window w)
        {
            this.w = w;
            switch (w.Name)
            {
                case "w2":
                    setting2();
                    break;
                case "w3":
                    setting3();
                    break;
            }
        }
    }
}
