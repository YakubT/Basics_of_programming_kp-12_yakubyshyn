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
        Button[] arr = new Button[11];
        private void Bcl (object sender,EventArgs e)
        {
            if ((string) label1.Content == "0")
                label1.Content = "";
            Button b = (Button)sender;
            label1.Content += b.Name.Substring(1);
        }
        private void comaclick(object sender, EventArgs e)
        {

            if (!label1.Content.ToString().Contains(','))
                label1.Content+= ",";
            
        }
        private void comaclick()
        {

            if (!label1.Content.ToString().Contains(','))
                label1.Content += ",";

        }
        public Window1()
        {
            InitializeComponent();
            label1.HorizontalContentAlignment = HorizontalAlignment.Right;
            for (int i=1;i<10;i++)
            {
                arr[i] = new Button();
                Thickness tmp = new Thickness(77* ((i - 1) % 3)+im.Margin.Left, (((10-i) - 1) / 3) * 51+190,0,0);
                
               
                
                arr[i].Name = "B"+i.ToString();
                arr[i].Margin = tmp;
                arr[i].Width = 77;
                arr[i].Height = 51;
                arr[i].Content = i.ToString();
                arr[i].VerticalAlignment = VerticalAlignment.Top;
                arr[i].HorizontalAlignment = HorizontalAlignment.Left;
                arr[i].Click += Bcl;
                arr[i].Background = Brushes.White;
                layoutGrid.Children.Add(arr[i]);
            }
            arr[0] = new Button();
            arr[0].Name = "B0";
            arr[0].Margin = new Thickness(77 + im.Margin.Left, 3 * 51 + 190, 0, 0);
            arr[0].Width = 77;
            arr[0].Height = 51;
            arr[0].Content = "0";
            arr[0].VerticalAlignment = VerticalAlignment.Top;
            arr[0].HorizontalAlignment = HorizontalAlignment.Left;
            arr[0].Click += Bcl;
            arr[0].Background = Brushes.White;
            layoutGrid.Children.Add(arr[0]);

            arr[10] = new Button();
            arr[10].Name = "B10";
            arr[10].Margin = new Thickness(77*2 + im.Margin.Left, 3 * 51 + 190, 0, 0);
            arr[10].Width = 77;
            arr[10].Height = 51;
            arr[10].Content = ",";
            arr[10].VerticalAlignment = VerticalAlignment.Top;
            arr[10].HorizontalAlignment = HorizontalAlignment.Left;
            arr[10].Click += comaclick;
            arr[10].Background = Brushes.White;
            layoutGrid.Children.Add(arr[10]);


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
            if ((int)c == 142)
                comaclick();
        }
    }
}
