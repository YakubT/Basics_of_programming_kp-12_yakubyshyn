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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Button[] arr = new Button[10];
        private void Bcl (object sender,EventArgs e)
        {
            if ((string) label1.Content == "0")
                label1.Content = "";
            Button b = (Button)sender;
            label1.Content += b.Name.Substring(1);
        }
        public Window1()
        {
            InitializeComponent();
            label1.HorizontalContentAlignment = HorizontalAlignment.Right;
            for (int i=1;i<10;i++)
            {
                arr[i] = new Button();
                Thickness tmp = new Thickness(77* ((i - 1) % 3)+im.Margin.Left, (((10-i) - 1) / 3) * 52+190,0,0);
                //MessageBox.Show(((i-1)%3).ToString());
                
               
                
                arr[i].Name = "B"+i.ToString();
                arr[i].Margin = tmp;
                arr[i].Width = 77;
                arr[i].Height = 52;
                arr[i].Content = i.ToString();
                arr[i].VerticalAlignment = VerticalAlignment.Top;
                arr[i].HorizontalAlignment = HorizontalAlignment.Left;
                arr[i].Click += Bcl;
                layoutGrid.Children.Add(arr[i]);
                //MessageBox.Show(arr[i].Margin.Left.ToString());
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

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            
            char c = (char)e.Key;
            if (((int)c >= 34) && ((int) c <=43))
            {
                if ((string)label1.Content == "0")
                    label1.Content = "";
                c = (char)((int)c - 34 + 48);
                label1.Content += c.ToString();
            }
        }
    }
}
