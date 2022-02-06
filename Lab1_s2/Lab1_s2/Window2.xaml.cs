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

namespace Lab1_s2
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        bool isx;
        bool start =false;
        int[,] matr = new int[6,6];
        
        public Window2()
        {
            InitializeComponent();
            for (int i = 1; i <= 25; i++)
            {
                ComboBox cb = this.FindName("CB" + i.ToString()) as ComboBox;
                cb.IsEnabled = true;
                cb.SelectedIndex = -1;
                isx = false;
                cb.DropDownClosed += CommonCBchoose;
                cb.IsEditable = false;
            }
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
        private bool freeriad(int i,int num,ref int kol)
        {
            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
            {
                if (matr[i, j] == t)
                {
                    kol = -1;
                    return false;
                }
                if (matr[i, j] != 0)
                    kol++;
            }
            return true;
        }
        private bool freecolumn(int i,int num,ref int kol)
        {
            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
            {
                if (matr[j, i] == t)
                {
                    kol = -1;
                    return false;
                }
                if (matr[j, i] != 0)
                    kol++;
            }
            return true;
        }
        private bool freediag1(int num,ref int kol)
        {

            
            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
                if (matr[j, j] == t)
                {
                    kol = -1;
                    return false;
                    
                }
                else
                    if (matr[j, j] != 0)
                    kol++;
            return true;
        }
        private bool freediag2(int num, ref int kol)
        {


            kol = 0;
            int t;
            if (num == 2)
                t = 1;
            else
                t = 2;
            for (int j = 1; j <= 5; j++)
                if (matr[j, 5-j+1] == t)
                {
                    kol = -1;
                    return false;
                    
                }
                else
                    if (matr[j, 5-j+1] != 0)
                    kol++;
            return true;
        }
        
        private bool isdiag (int i,int j)
        {
            return ((i == j) || (j == 5 - i + 1));
        }
        private bool fork(int num,ref int i,ref int j)
        {
            i = -1;
            j = -1;
            if (num == 2)
                num = 1;
            else
                num = 2;
            for (int ii=1;ii<=5;ii++)
                for (int jj=1;jj<=5;jj++)
                {
                    if (matr[ii,jj]==0)
                    {
                        int k1, k2, k3,k4;
                        k1 = k2 = k3 = k4 = -1;
                        freeriad(ii,num, ref k1);
                        freecolumn(jj, num, ref k2);
                        freediag1(num, ref k3);
                        freediag2(num, ref k4);
                        int[] masiv = new int[4];
                        masiv[0] = k1;
                        masiv[1] = k2;
                        masiv[2] = k3;
                        masiv[3] = k4;
                        int cnt = 0;
                        for (int z = 0; z < 4; z++)
                            if (masiv[z] == 3)
                                cnt++;
                        if (cnt>1)
                        {
                            i = ii;
                            j = jj;
                            return true;
                        }
                        
                    }
                }
            return false;
        }
        private (int,int) canwin(int num)
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
                    if (matr[i, j] == num)
                        cnt1++;
                    if (matr[i, j] == 0)
                    {
                        cntpust++;
                        cx = i;
                        cy = j;
                    }
                }
                if (cnt1==4 && cntpust==1)
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
                    if (matr[j, i] == num)
                        cnt1++;
                    if (matr[j, i] == 0)
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
            for (int i=1;i<=5;i++)
            {
                if (matr[i, i] == num)
                    cnt11++;
                if (matr[i, i] == 0)
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
                if (matr[i, 5-i+1] == num)
                    cnt11++;
                if (matr[i, 5-i+1] == 0)
                {
                    cntpust1++;
                    cx1 = i;
                    cy1 = 5-i+1;
                }
            }
            if (cnt11 == 4 && cntpust1 == 1)
            {
                return (cx1, cy1);
            }
            return (x, y);
        }
        private void make_step(bool symb)
        {

            //true - computer plays x
            // false - computer plays o
            
            if (nich())
            {
                MessageBox.Show("Нічия");
                return;
            }
            int et;
            if (symb)
            {
                et = 2;
            }
            else
                et = 1;
           
            ComboBox cb;
            int x, y;
            (x, y) = canwin(et);
            if (x!=-1)
            {
                matr[x, y] = et;
                cb = this.FindName("CB" + ((x - 1) * 5 + y).ToString()) as ComboBox;
                cb.IsEnabled = false;
                if (et == 1)
                {
                    cb.SelectedIndex = 1;
                    cb.Foreground = Brushes.Green;
                }
                else
                {
                    cb.SelectedIndex = 0;
                    cb.Foreground = Brushes.Red;
                }
                for (int i = 1; i <= 25; i++)
                {
                    cb = this.FindName("CB" + i.ToString()) as ComboBox;
                    cb.IsEnabled = false;
                }
                MessageBox.Show("На жаль, Ви програли");
                return;
            }

            if (et == 1)
                (x, y) = canwin(2);
            else
                (x, y) = canwin(1);
            if (x != -1)
            {
                matr[x, y] = et;
                 cb = this.FindName("CB" + ((x - 1) * 5 + y).ToString()) as ComboBox;
                
                if (et == 1)
                {
                    cb.SelectedIndex = 1;
                    cb.Foreground = Brushes.Green;
                }
                else
                {
                    cb.SelectedIndex = 0;
                    cb.Foreground = Brushes.Red;
                }
                return;
            }
            fork(et,ref x,ref y);
            if (x!=-1)
            {
                matr[x, y] = et;
                cb = this.FindName("CB" + ((x - 1) * 5 + y).ToString()) as ComboBox;

                if (et == 1)
                {
                    cb.SelectedIndex = 1;
                    cb.Foreground = Brushes.Green;
                }
                else
                {
                    cb.SelectedIndex = 0;
                    cb.Foreground = Brushes.Red;
                }
                return;
            }
            int max = int.MinValue;
            int ir, jr;
            ir = jr = -1;
         
            for (int i=1;i<=5;i++)
                for (int j=1;j<=5;j++)
                {
                    int k1; int k2;
                    int k3;int k4;
                    k1 = int.MinValue;
                    k2 = int.MinValue;
                    k3 = int.MinValue;
                    k4 = int.MinValue;
                    bool f1=false;
                    bool f2 = false;
                    bool f3 = false;
                    bool f4 = false;
                    if ((j == i) && freediag1(et, ref k3))
                        f1 = true;
                    if ((j == 5 - i + 1) && freediag2(et, ref k4))
                        f2 = true;
                    if (freeriad(i, et, ref k1))
                        f3 = true;
                    if (freecolumn(j, et, ref k2))
                        f4 = true;
                    if ((matr[i,j]==0) && (f3 || f4  || f1 || f2))
                    {
                        int res = Math.Max(k1, k2);
                        res = Math.Max(res, Math.Max(k3,k4));
                        if (res>max || (res==max && isdiag(i,j)))
                        {
                            ir = i;
                            jr = j;
                            max = res;
                        }
                    }

                }
           if (ir==-1)
            {
                for (int i = 1; i <= 5; i++)
                    for (int j = 1; j <= 5; j++)
                    {
                        
                        if ((matr[i, j] == 0 && ir==-1) || (matr[i,j]==0 && isdiag(i,j)))
                        {
                            ir = i;
                            jr = j;
                        }

                    }
        
               
            }
            matr[ir, jr] = et;
             cb = this.FindName("CB" + ((ir - 1) * 5 + jr).ToString()) as ComboBox;
            cb.IsEnabled = false;
            if (et == 1)
            {
                cb.SelectedIndex = 1;
                cb.Foreground = Brushes.Green;
            }
            else
            {
                cb.SelectedIndex = 0;
                cb.Foreground = Brushes.Red;
            }
            
            if (nich())
            {
                MessageBox.Show("Нічия");
                return;
            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            start = true;
           
            matr = new int[6, 6]; // 0 -пуста клітинка, 1 - нулик, 2 - хрестик 
            for (int i=1;i<=25;i++)
            {
                ComboBox cb = this.FindName("CB"+i.ToString()) as ComboBox;
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
            for (int i=1;i<=25;i++)
            {
                ComboBox cb = this.FindName("CB" + i.ToString()) as ComboBox;
                cb.IsEnabled = true;
                cb.SelectedIndex = -1;
            }

                
            if (!isx)
            {
                matr[3, 3] = 2;
     
                CB13.SelectedIndex = 0;
                CB13.IsEnabled = false;
                CB13.Foreground = Brushes.Red;
            }
        }

        
        private bool win(int num)
        {
            
            for (int i = 1; i <= 5; i++)
            {
                bool f = true;
                for (int j = 1; j <= 5; j++)
                    if (matr[i, j] != num)
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
                    if (matr[j, i] != num)
                    {
                        f = false;
                    }
                if (f)
                    return true;
            }
            bool flag = true;
            for (int i = 1; i <= 5; i++)
            {
                if (matr[i, i] != num)
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
                if (matr[i, 5-i+1] != num)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        private bool nich ()
        {
            int cnt1, cnt2;
            for (int i = 1; i <= 5; i++)
            {
                cnt1 = cnt2 = 0;
                for (int j = 1; j <= 5; j++)
                {
                    if (matr[i, j] == 1)
                        cnt1++;
                    if (matr[i, j] == 2)
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
                    if (matr[j, i] == 1)
                        cnt1++;
                    if (matr[j, i] == 2)
                        cnt2++;
                }
                if (cnt1 == 0 || cnt2 == 0)
                    return false;
            }
            cnt1 = cnt2 = 0;
            for (int i = 1; i <= 5; i++)
                if (matr[i, i] == 1)
                    cnt1++;
                else
                    if (matr[i, i] == 2)
                    cnt2++;
            if (cnt1 == 0 || cnt2 == 0)
                return false;
            cnt1 = cnt2 = 0;
            for (int i = 1; i <= 5; i++)
                if (matr[i, 5-i+1] == 1)
                    cnt1++;
                else
                    if (matr[i, 5-i+1] == 2)
                    cnt2++;
            if (cnt1 == 0 || cnt2 == 0)
                return false;
            return true;
        }
        private void CommonCBchoose (object sender, EventArgs e)
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
                    if (win(num))
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
        private void Window_Activated(object sender, EventArgs e)
        {
           
        }

       
    }
}
