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
        private bool isx=false;
        private bool start = false;
        private int[,] matr = new int[6, 6];
        private void wclose(object sender, RoutedEventArgs e)
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
        private Button findBut(string s)
        {
            Grid g = (Grid)w.Content;
            foreach (Button t in g.Children.OfType<Button>())
                if (t.Name == s)
                    return t;
            return new Button();
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
            bmas[3].Click += wclose;
            w.Closed += Window_Closed;
        }
        // функції для третього вікна
        private string from_matr_tostr(int[,] mas)
        {
            string s = "";
            for (int i = 1; i <= 5; i++)
                for (int j = 1; j <= 5; j++)
                    s += mas[i, j].ToString();
            return s;
        }
        private int[,] from_str_to_matr(string s)
        {
            int[,] res = new int[6, 6];
            for (int i = 0; i < s.Length; i++)
            {
                int ii = i / 5 + 1;
                int j = (i % 5) + 1;
                res[ii, j] = (int)(s[i] - 48);
            }
            return res;
        }
        int nearfork(int num, int[,] mas)
        {

            if (num == 2)
                num = 1;
            else
                num = 2;
            int maxim = 0;
            for (int ii = 1; ii <= 5; ii++)
                for (int jj = 1; jj <= 5; jj++)
                {
                    if (mas[ii, jj] == 0)
                    {
                        int k1, k2, k3, k4;
                        k1 = k2 = k3 = k4 = -1;
                        freeriad(ii, num, ref k1, mas);
                        freecolumn(jj, num, ref k2, mas);
                        freediag1(num, ref k3, mas);
                        freediag2(num, ref k4, mas);
                        int[] masiv = new int[4];
                        masiv[0] = k1;
                        masiv[1] = k2;
                        masiv[2] = k3;
                        masiv[3] = k4;
                        int cnt = 0;
                        int res = 10;
                        List<int> l = new List<int>();
                        for (int z = 0; z < 4; z++)
                            if (masiv[z] > 0 && masiv[z] < 4)
                            {
                                cnt++;
                                l.Add(masiv[z]);
                            }
                        if (cnt > 1)
                        {
                            l.Sort();
                            //MessageBox.Show(l[l.Count - 2].ToString());
                            maxim = Math.Max(l[l.Count - 2] + l[l.Count - 1], maxim);
                        }

                    }
                }
            //MessageBox.Show(maxim.ToString());
            return maxim;
        }
        private int func(string sost, bool type)
        {
            //1-x
            int[,] arr = from_str_to_matr(sost);
            if (nich(arr))
            {
                return 500;
            }
            if (win(1, arr))
            {
                return 0;
            }
            if (win(2, arr))
            {
                return 1000;
            }
            int et;
            if (type)
                et = 2;
            else
                et = 1;
            int x, y;
            (x, y) = canwin(et, arr);
            if (x != -1)
            {
                if (type)
                    return 1000;
                else
                    return 0;
            }

            int op;
            if (et == 2)
                op = 1;
            else
                op = 2;
            int el1, el2;
            el1 = el2 = -1;
            fork(op, ref el1, ref el2, arr);
            if (el1 != -1)
            {
                if (type)
                    return 1000;
                else
                    return 0;
            }
            if (nearfork(op, arr) != 0)
            {

                //MessageBox.Show(nearfork(op,arr).ToString());
                if (type)
                    return 500 + nearfork(op, arr);
                else
                    return 10 - nearfork(op, arr);
            }


            int k1, k2, k3, k4;
            k1 = k2 = k3 = k4 = int.MinValue;
            freediag1(2, ref k1, arr);
            freediag2(2, ref k2, arr);
            int maxk3 = int.MinValue;
            for (int i = 1; i <= 5; i++)
            {

                freeriad(i, 2, ref k3, arr);
                maxk3 = Math.Max(k3, maxk3);
            }
            k3 = maxk3;
            int maxk4 = int.MinValue;
            for (int i = 1; i <= 5; i++)
            {
                freecolumn(i, 2, ref k4, arr);
                maxk4 = Math.Max(k4, maxk4);
            }
            k4 = maxk4;
            double res1 = Math.Max(Math.Max(k1, Math.Max(k2, Math.Max(k3, k4))), 1);

            k1 = k2 = k3 = k4 = int.MinValue;
            freediag1(1, ref k1, arr);
            freediag2(1, ref k2, arr);
            maxk3 = int.MinValue;
            for (int i = 1; i <= 5; i++)
            {
                freeriad(i, 1, ref k3, arr);
                maxk3 = Math.Max(k3, maxk3);
            }
            k3 = maxk3;
            maxk4 = int.MinValue;
            for (int i = 1; i <= 5; i++)
            {
                freecolumn(i, 1, ref k4, arr);
                maxk4 = Math.Max(k4, maxk4);
            }
            k4 = maxk4;
            double res2 = Math.Max(Math.Max(k1, Math.Max(k2, Math.Max(k3, k4))), 1);
            double oc;
            if (type)
            {

                if (res2 < 0.000001)
                    oc = res1 * 100;
                else
                    oc = res1 / res2 * 100;

                oc = Math.Min(oc, 499);
            }
            else
            {
                // MessageBox.Show(res1.ToString());
                // MessageBox.Show(res2.ToString());

                if (res1 < 0.000001)
                    oc = 1.0 / res2 * 100;
                else
                    oc = res1 / res2 * 100;
                oc = Math.Min(oc, 499);
            }
            //MessageBox.Show(res1.ToString()+" "+res2.ToString());
            // MessageBox.Show(oc.ToString());
            return (int)oc;


        }
        private (int, string) minmaxproc(string sostoy, bool type, int alpha, int beta, int h)//if type==1 - x else o
        {

            if (h == 0)
            {
                string tmp = "";
                // MessageBox.Show(func(sostoy, type).ToString());
                return (func(sostoy, type), tmp);

            }
            //if (cnt > 1000000)
            //  MessageBox.Show("YEs");
            //MessageBox.Show(type.ToString());
            if (type) // X, so max
            {
                int res = int.MinValue / 2;
                int[,] arr = from_str_to_matr(sostoy);
                //MessageBox.Show(str(arr));
                if (nich(arr))
                {
                    return (500, "");
                }
                else
                    if (win(2, arr))
                {
                    return (1000, "");
                }
                else
                    if (win(1, arr))
                {
                    return (0, "");
                }

                string pred = "";
                // bool f = false;
                for (int i = 1; i <= 5; i++)
                    for (int j = 1; j <= 5; j++)
                    {
                        if (arr[i, j] == 0)
                        {
                            int[,] cop = new int[6, 6];
                            for (int i2 = 1; i2 <= 5; i2++)
                                for (int j2 = 1; j2 <= 5; j2++)
                                    cop[i2, j2] = arr[i2, j2];
                            cop[i, j] = 2;
                            //MessageBox.Show("at");
                            // MessageBox.Show(str(cop))
                            int val;
                            string str;
                            (val, str) = minmaxproc(from_matr_tostr(cop), false, alpha, beta, h - 1);
                            if (res < val)
                            {
                                res = val;
                                pred = from_matr_tostr(cop);
                            }

                            alpha = Math.Max(alpha, res);
                            //MessageBox.Show("finish "+sostoy.ToString());

                            if (alpha >= beta)
                                return (res, pred);

                            //ocX[sostoy] = Math.Max(ocX[sostoy], ocX[from_matr_tostr(cop)]);


                        }
                    }
                return (res, pred);
            }
            else //minim
            {

                int[,] arr = from_str_to_matr(sostoy);
                //MessageBox.Show(str(arr));
                if (nich(arr))
                {
                    return (500, "");
                }
                else
                    if (win(1, arr))
                {
                    return (0, "");
                }
                else
                    if (win(2, arr))
                {
                    return (1000, "");
                }
                int res = int.MaxValue / 2;
                string pred = "";
                bool f = false;
                for (int i = 1; i <= 5; i++)
                    for (int j = 1; j <= 5; j++)
                    {
                        if (arr[i, j] == 0)
                        {
                            int[,] cop = new int[6, 6];
                            for (int i2 = 1; i2 <= 5; i2++)
                                for (int j2 = 1; j2 <= 5; j2++)
                                    cop[i2, j2] = arr[i2, j2];
                            cop[i, j] = 1;
                            //MessageBox.Show("at");
                            //MessageBox.Show(str(cop));
                            // if (alpha >= beta)
                            // { 
                            //MessageBox.Show(alpha.ToString() + " " + beta.ToString());
                            //     return;
                            //  }
                            int val;
                            string str;
                            (val, str) = minmaxproc(from_matr_tostr(cop), true, alpha, beta, h - 1);
                            if (res > val)
                            {
                                res = val;
                                pred = from_matr_tostr(cop);
                            }
                            //f = true;
                            beta = Math.Min(beta, res);

                            if (alpha >= beta)
                                return (res, pred);
                        }
                    }
                return (res, pred);
            }

        }
        private bool freeriad(int i, int num, ref int kol, int[,] mas)
        {
            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
            {
                if (mas[i, j] == t)
                {
                    kol = -1;
                    return false;
                }
                if (mas[i, j] != 0)
                    kol++;
            }
            //kol = 5 - kol;
            return true;
        }
        private bool freecolumn(int i, int num, ref int kol, int[,] mas)
        {
            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
            {
                if (mas[j, i] == t)
                {
                    kol = -1;
                    return false;
                }
                if (mas[j, i] != 0)
                    kol++;
            }
            return true;
        }
        private bool freediag1(int num, ref int kol, int[,] mas)
        {


            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
                if (mas[j, j] == t)
                {
                    kol = -1;
                    return false;

                }
                else
                    if (mas[j, j] != 0)
                    kol++;
            return true;
        }
        private bool freediag2(int num, ref int kol, int[,] mas)
        {


            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
                if (mas[j, 5 - j + 1] == t)
                {
                    kol = -1;
                    return false;

                }
                else
                    if (mas[j, 5 - j + 1] != 0)
                    kol++;
            return true;
        }
        private bool fork(int num, ref int i, ref int j, int[,] mas)
        {
            i = -1;
            j = -1;
            if (num == 2)
                num = 1;
            else
                num = 2;
            for (int ii = 1; ii <= 5; ii++)
                for (int jj = 1; jj <= 5; jj++)
                {
                    if (mas[ii, jj] == 0)
                    {
                        int k1, k2, k3, k4;
                        k1 = k2 = k3 = k4 = -1;
                        freeriad(ii, num, ref k1, mas);
                        freecolumn(jj, num, ref k2, mas);
                        freediag1(num, ref k3, mas);
                        freediag2(num, ref k4, mas);
                        int[] masiv = new int[4];
                        masiv[0] = k1;
                        masiv[1] = k2;
                        masiv[2] = k3;
                        masiv[3] = k4;
                        int cnt = 0;
                        for (int z = 0; z < 4; z++)
                            if (masiv[z] == 3)
                                cnt++;
                        if (cnt > 1)
                        {
                            i = ii;
                            j = jj;
                            return true;
                        }

                    }
                }
            return false;
        }
        private (int, int) canwin(int num, int[,] mas)
        {
            int x = -1;
            int y = -1;
            for (int i = 1; i <= 5; i++)
            {
                int cnt1 = 0;
                int cntpust = 0;
                int cx, cy;
                cx = cy = -1;
                for (int j = 1; j <= 5; j++)
                {
                    if (mas[i, j] == num)
                        cnt1++;
                    if (mas[i, j] == 0)
                    {
                        cntpust++;
                        cx = i;
                        cy = j;
                    }
                }
                if (cnt1 == 4 && cntpust == 1)
                {
                    return (cx, cy);
                }
            }
            for (int i = 1; i <= 5; i++)
            {
                int cnt1 = 0;
                int cntpust = 0;
                int cx, cy;
                cx = cy = -1;
                for (int j = 1; j <= 5; j++)
                {
                    if (mas[j, i] == num)
                        cnt1++;
                    if (mas[j, i] == 0)
                    {
                        cntpust++;
                        cx = j;
                        cy = i;
                    }

                }
                if (cnt1 == 4 && cntpust == 1)
                {
                    return (cx, cy);
                }
            }
            int cnt11;
            int cntpust1;
            cnt11 = cntpust1 = 0;
            int cx1, cy1;
            cx1 = cy1 = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (mas[i, i] == num)
                    cnt11++;
                if (mas[i, i] == 0)
                {
                    cntpust1++;
                    cx1 = i;
                    cy1 = i;
                }
            }
            if (cnt11 == 4 && cntpust1 == 1)
            {
                return (cx1, cy1);
            }
            cnt11 = cntpust1 = 0;
            cx1 = cy1 = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (mas[i, 5 - i + 1] == num)
                    cnt11++;
                if (mas[i, 5 - i + 1] == 0)
                {
                    cntpust1++;
                    cx1 = i;
                    cy1 = 5 - i + 1;
                }
            }
            if (cnt11 == 4 && cntpust1 == 1)
            {
                return (cx1, cy1);
            }
            return (x, y);
        }
        private bool win(int num, int[,] mas)
        {

            for (int i = 1; i <= 5; i++)
            {
                bool f = true;
                for (int j = 1; j <= 5; j++)
                    if (mas[i, j] != num)
                    {
                        f = false;
                    }
                if (f)
                    return true;
            }
            for (int i = 1; i <= 5; i++)
            {
                bool f = true;
                for (int j = 1; j <= 5; j++)
                    if (mas[j, i] != num)
                    {
                        f = false;
                    }
                if (f)
                    return true;
            }
            bool flag = true;
            for (int i = 1; i <= 5; i++)
            {
                if (mas[i, i] != num)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
                return true;
            flag = true;
            for (int i = 1; i <= 5; i++)
            {
                if (mas[i, 5 - i + 1] != num)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        private bool nich(int[,] mas)
        {
            /*
            int zag = 0;
            for (int i = 1; i <= 5; i++)
                for (int j = 1; j <= 5; j++)
                    if (mas[i, j] == 0)
                        zag++;
            */
            int cnt1, cnt2;
            for (int i = 1; i <= 5; i++)
            {
                cnt1 = cnt2 = 0;
                for (int j = 1; j <= 5; j++)
                {
                    if (mas[i, j] == 1)
                        cnt1++;
                    if (mas[i, j] == 2)
                        cnt2++;
                }


                if (cnt1 == 0 || cnt2 == 0)
                    return false;
            }
            for (int i = 1; i <= 5; i++)
            {
                cnt1 = cnt2 = 0;
                for (int j = 1; j <= 5; j++)
                {
                    if (mas[j, i] == 1)
                        cnt1++;
                    if (mas[j, i] == 2)
                        cnt2++;
                }
                // if (cnt1 == 0 && cnt2 == 0 && zag < 10)
                //   return true;
                if (cnt1 == 0 || cnt2 == 0)
                    return false;
            }
            cnt1 = cnt2 = 0;
            for (int i = 1; i <= 5; i++)
                if (mas[i, i] == 1)
                    cnt1++;
                else
                    if (mas[i, i] == 2)
                    cnt2++;
            // if (cnt1 == 0 && cnt2 == 0 && zag < 10)
            //   return true;
            if (cnt1 == 0 || cnt2 == 0)
                return false;
            cnt1 = cnt2 = 0;
            for (int i = 1; i <= 5; i++)
                if (mas[i, 5 - i + 1] == 1)
                    cnt1++;
                else
                    if (mas[i, 5 - i + 1] == 2)
                    cnt2++;
            // if (cnt1 == 0 && cnt2 == 0 && zag < 10)
            //    return true;
            if (cnt1 == 0 || cnt2 == 0)
                return false;
            return true;
        }
        private void CommonCBchoose(object sender, EventArgs e)
        {
            if (start)
            {

                bool f = false;
                ComboBox tmp = (ComboBox)sender;

                if (isx)
                {

                    if (tmp.SelectedIndex == 0)
                    {
                        string s = tmp.Name;
                        int ind = int.Parse(s.Substring(2));
                        int X, Y;
                        if (ind % 5 == 0)
                        {
                            X = ind / 5;
                            Y = 5;
                        }
                        else
                        {
                            X = ind / 5 + 1;
                            Y = ind - (X - 1) * 5;
                        }
                        matr[X, Y] = 2;
                        tmp.IsEnabled = false;
                        f = true;
                    }
                    else
                    {
                        tmp.SelectedIndex = -1;
                    }
                }
                else
                {

                    if (tmp.SelectedIndex == 1)
                    {
                        string s = tmp.Name;
                        int ind = int.Parse(s.Substring(2));
                        int X, Y;
                        if (ind % 5 == 0)
                        {
                            X = ind / 5;
                            Y = 5;
                        }
                        else
                        {
                            X = ind / 5 + 1;
                            Y = ind - (X - 1) * 5;
                        }
                        matr[X, Y] = 1;

                        tmp.IsEnabled = false;


                        f = true;

                    }
                    else
                    {
                        tmp.SelectedIndex = -1;
                    }
                }
                int num;
                if (f)
                {
                    if (isx)
                        num = 2;
                    else
                        num = 1;
                    if (win(num, matr))
                    {
                        MessageBox.Show("Ви виграли");
                    }
                    else
                    {
                        if (num == 1)
                        {
                            tmp.Foreground = Brushes.Green;
                            make_step(true);

                        }
                        else
                        {
                            tmp.Foreground = Brushes.Red;
                            make_step(false);

                        }
                    }
                }
            }
        }
        private void make_step(bool symb)
        {
            if (symb)
            {
                Grid g = (Grid)w.Content;
                if (nich(matr))
                {
                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }
                    MessageBox.Show("Нічия");
                    return;
                }
                if (win(1, matr))
                {
                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }
                    
                    MessageBox.Show("Ви виграли");
                    return;
                }

                //MessageBox.Show(from_matr_tostr(matr));

                Random rnd = new Random();
                // MessageBox.Show(l.Count.ToString());

                string res;
                int val;
                (val, res) = minmaxproc(from_matr_tostr(matr), true, int.MinValue, int.MaxValue, 4);
                matr = from_str_to_matr(res);
                //MessageBox.Show(val.ToString());
                foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                {
                    cb.IsEnabled = true;
                    cb.SelectedIndex = -1;
                    cb.Foreground = Brushes.Black;
                }
                foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                {
                    cb.IsEnabled = true;
                    cb.SelectedIndex = -1;
                    cb.Foreground = Brushes.Black;
                }
               
               
                foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                {
                  
                    int i = int.Parse(cb.Name.Substring(2));
                    int ii = (i - 1) / 5 + 1;
                    int jj = (i - 1) % 5 + 1;
                
                    if (matr[ii, jj] == 1)
                    {
                        cb.IsEnabled = false;
                        cb.SelectedIndex = 1;
                        cb.Foreground = Brushes.Green;
                    }
                    else
                        if (matr[ii, jj] == 2)
                    {
                        cb.IsEnabled = false;
                        cb.SelectedIndex = 0;
                        cb.Foreground = Brushes.Red;
                    }
                }
                
                if (win(2, matr))
                {
                    
                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }
                    MessageBox.Show("Нажаль ви програли");
                    return;

                }
                if (nich(matr))
                {
                    
                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }
                    MessageBox.Show("Нічия");
                }
            }
            else
            {
                Grid g = (Grid)w.Content;
                if (nich(matr))
                {
                    
                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }
                    MessageBox.Show("Нічия");
                    return;
                }
                if (win(2, matr))
                {
                   
                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }
                    MessageBox.Show("Ви виграли");
                    return;
                }
                //MessageBox.Show(from_matr_tostr(matr));
                string res;
                int val;
                (val, res) = minmaxproc(from_matr_tostr(matr), false, int.MinValue, int.MaxValue, 4);
                matr = from_str_to_matr(res);
                // MessageBox.Show(val.ToString());
             
                foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                {
                    cb.IsEnabled = true;
                    cb.SelectedIndex = -1;
                    cb.Foreground = Brushes.Black;
                }
                foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                {
                    int i = int.Parse(cb.Name.Substring(2));
                    int ii = (i - 1) / 5 + 1;
                    int jj = (i - 1) % 5 + 1;
                    if (matr[ii, jj] == 1)
                    {
                        cb.IsEnabled = false;
                        cb.SelectedIndex = 1;
                        cb.Foreground = Brushes.Green;
                    }
                    else
                        if (matr[ii, jj] == 2)
                    {
                        cb.IsEnabled = false;
                        cb.SelectedIndex = 0;
                        cb.Foreground = Brushes.Red;
                    }
                }
                
                if (win(1, matr))
                {
                    
                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }
                    MessageBox.Show("Нажаль ви програли");
                    return;

                }
                if (nich(matr))
                {

                    foreach (ComboBox cb in g.Children.OfType<ComboBox>())
                    {
                        cb.IsEnabled = false;
                    }

                    MessageBox.Show("Нічия");
                }
            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Grid g = (Grid)w.Content;
            start = true;

            matr = new int[6, 6]; // 0 -пуста клітинка, 1 - нулик, 2 - хрестик 
            foreach (ComboBox cb in g.Children.OfType<ComboBox>())
            {
                cb.IsEnabled = true;
                cb.SelectedIndex = -1;
                cb.Foreground = Brushes.Black;
            }
            
            Random rnd = new Random();
            isx = isx ^ true;
            if (isx)
                MessageBox.Show("Ви ходите \"X\", хрестики ходять першими");
            else
                MessageBox.Show("Ви ходите \"О\", нулики ходять другими");
             
           


            if (!isx)
            {
                make_step(true);
            }
        }
        // функції для калькулятора
       
        char type = '+';
        bool cl = false;
        double mem = 0;
        bool input = false;
        private Label findl (string s)
        {
            Grid g = (Grid)w.Content;
            foreach (Label t in g.Children.OfType<Label>())
                if (t.Name == s)
                    return t;
            return new Label();
        }
        private void Bcl(object sender, EventArgs e)
        {
            input = true;
            if ((string)findl("label1").Content == "0" || cl == true || (string)findl("label1").Content == "Incorrect data")
                findl("label1").Content = "";
            Button b = (Button)sender;
            findl("label1").Content += b.Name.Substring(1);
            cl = false;
        }
        private void comaclick(object sender, EventArgs e)
        {

            if (!findl("label1").Content.ToString().Contains(','))
                findl("label1").Content += ",";

        }
        private void comaclick()
        {

            if (!findl("label1").Content.ToString().Contains(','))
                findl("label1").Content += ",";

        }

      
        

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            char c = (char)e.Key;
            if (((int)c >= 34) && ((int)c <= 43))
            {
                input = true;
                if ((string)findl("label1").Content == "0" || cl == true || (string)findl("label1").Content == "Incorrect data")
                    findl("label1").Content = "";
                c = (char)((int)c - 34 + 48);
                findl("label1").Content += c.ToString();
                cl = false;
            }
            if ((int)c == 142)
                comaclick();
        }

        private void Backspaceb_Click(object sender, RoutedEventArgs e)
        {
            findl("label1").Content = findl("label1").Content.ToString().Substring(0, findl("label1").Content.ToString().Length - 1);
            if ((string)findl("label1").Content == "")
                findl("label1").Content = "0";
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            mem = 0;
            type = '+';
            findl("label1").Content = "0";
            input = false;
        }

        private void div_Click(object sender, RoutedEventArgs e)
        {
            if (input)
            {
                switch (type)
                {
                    case '+':
                        findl("label1").Content = Math.Round(Convert.ToDouble(findl("label1").Content.ToString()) + mem, 7).ToString();
                        break;
                    case '-':
                        findl("label1").Content = Math.Round(mem - Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '/':
                        if (Math.Abs(Convert.ToDouble(findl("label1").Content.ToString())) < 0.00000001)
                            findl("label1").Content = "Incorrect data";
                        else
                            findl("label1").Content = Math.Round(mem / Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '*':
                        findl("label1").Content = Math.Round((Convert.ToDouble(findl("label1").Content.ToString()) * mem), 7).ToString();
                        break;
                }
                if ((string)findl("label1").Content == "Incorrect data")
                {
                    type = '+';
                    input = false;
                    mem = 0;
                    return;
                }
                mem = Convert.ToDouble(findl("label1").Content.ToString());
                cl = true;
                type = '/';
                input = false;
            }
        }

        private void mul_Click(object sender, RoutedEventArgs e)
        {
            if (input)
            {
                switch (type)
                {
                    case '+':
                        findl("label1").Content = Math.Round(Convert.ToDouble(findl("label1").Content.ToString()) + mem, 7).ToString();
                        break;
                    case '-':
                        findl("label1").Content = Math.Round(mem - Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '/':
                        if (Math.Abs(Convert.ToDouble(findl("label1").Content.ToString())) < 0.00000001)
                            findl("label1").Content = "Incorrect data";
                        else
                            findl("label1").Content = Math.Round(mem / Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '*':
                        findl("label1").Content = Math.Round((Convert.ToDouble(findl("label1").Content.ToString()) * mem), 7).ToString();
                        break;
                }
                if ((string)findl("label1").Content == "Incorrect data")
                {
                    type = '+';
                    input = false;
                    mem = 0;
                    return;
                }
                mem = Convert.ToDouble(findl("label1").Content.ToString());
                cl = true;
                type = '*';
                input = false;
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (input)
            {
                switch (type)
                {
                    case '+':
                        findl("label1").Content = Math.Round(Convert.ToDouble(findl("label1").Content.ToString()) + mem, 7).ToString();
                        break;
                    case '-':
                        findl("label1").Content = Math.Round(mem - Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '/':
                        if (Math.Abs(Convert.ToDouble(findl("label1").Content.ToString())) < 0.00000001)
                            findl("label1").Content = "Incorrect data";
                        else
                            findl("label1").Content = Math.Round(mem / Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '*':
                        findl("label1").Content = Math.Round((Convert.ToDouble(findl("label1").Content.ToString()) * mem), 7).ToString();
                        break;
                }
                if ((string)findl("label1").Content == "Incorrect data")
                {
                    type = '+';
                    input = false;
                    mem = 0;
                    return;
                }
                mem = Convert.ToDouble(findl("label1").Content.ToString());
                cl = true;
                type = '-';
                input = false;
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (input)
            {
                switch (type)
                {
                    case '+':
                        findl("label1").Content = Math.Round(Convert.ToDouble(findl("label1").Content.ToString()) + mem, 7).ToString();
                        break;
                    case '-':
                        findl("label1").Content = Math.Round(mem - Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '/':
                        if (Math.Abs(Convert.ToDouble(findl("label1").Content.ToString())) < 0.00000001)
                            findl("label1").Content = "Incorrect data";
                        else
                            findl("label1").Content = Math.Round(mem / Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '*':
                        findl("label1").Content = Math.Round((Convert.ToDouble(findl("label1").Content.ToString()) * mem), 7).ToString();
                        break;
                }
                if ((string)findl("label1").Content == "Incorrect data")
                {
                    type = '+';
                    input = false;
                    mem = 0;
                    return;
                }
                mem = Convert.ToDouble(findl("label1").Content.ToString());
                cl = true;
                type = '+';
                input = false;
            }
        }

        private void eq_Click(object sender, RoutedEventArgs e)
        {
            if (input)
            {
                switch (type)
                {
                    case '+':
                        findl("label1").Content = Math.Round(Convert.ToDouble(findl("label1").Content.ToString()) + mem, 7).ToString();
                        break;
                    case '-':
                        findl("label1").Content = Math.Round(mem - Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '/':
                        if (Math.Abs(Convert.ToDouble(findl("label1").Content.ToString())) < 0.00000001)
                            findl("label1").Content = "Incorrect data";
                        else
                            findl("label1").Content = Math.Round(mem / Convert.ToDouble(findl("label1").Content.ToString()), 7).ToString();
                        break;
                    case '*':
                        findl("label1").Content = Math.Round((Convert.ToDouble(findl("label1").Content.ToString()) * mem), 7).ToString();
                        break;
                }
                if ((string)findl("label1").Content == "Incorrect data")
                {
                    type = '+';
                    input = false;
                    mem = 0;
                    return;
                }
            }
            type = '+';
            mem = 0;
            cl = true;
            input = true;
        }

        private void change_znak_Click(object sender, RoutedEventArgs e)
        {
            
            if ((string)findl("label1").Content != "0" && (string)findl("label1").Content != "Incorrect data")
            {
                string tmp = (string)findl("label1").Content;
                if (tmp[0] == '-')
                    tmp = tmp.Substring(1);
                else
                    tmp = '-' + tmp;
                findl("label1").Content = tmp;
            }
        }

        private void setting3()
        {
            Grid g = (Grid)w.Content;

            foreach (ComboBox ch in g.Children.OfType<ComboBox>())
            {
                ch.DropDownClosed += CommonCBchoose;
            }
            Button b = findBut("exit");
            b.Click += wclose;
            w.Closed += Window_Closed;
            b = findBut("newgame");
            b.Click += Button_Click_1;

        }
        private void setting4()
        {
            Grid g = (Grid)w.Content;
            Button b = findBut("exit");
            b.Click += wclose;
            w.Closed += Window_Closed;
            w.KeyUp += Window_KeyUp;
            findBut("minus").Click += minus_Click;
            findBut("div").Click += div_Click;
            findBut("mul").Click += mul_Click;
            findBut("eq").Click += eq_Click;
            findBut("plus").Click += plus_Click;
            w.Closed += Window_Closed;
            for (int i=0;i<=9;i++)
            {
                findBut("B" + i.ToString()).Click += Bcl;
            }
            findBut("B10").Click += comaclick;
            findBut("BackspaceB").Click += Backspaceb_Click;
            findBut("clear").Click += clear_Click;
            findBut("change_znal").Click += change_znak_Click;
        }
        private void setting5()
        {
            Grid g = (Grid)w.Content;
            Button b = findBut("exit");
            b.Click += wclose;
            w.Closed += Window_Closed;
            findl("lb").Content = "Відомості про розробника: \n Якубишин Анатолій Сергійович, група КП-12, рік створення - 2022";
 
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
                case "w4":
                    setting4();
                    break;
                case "w5":
                    setting5();
                    break;
            }
        }
    }
}
