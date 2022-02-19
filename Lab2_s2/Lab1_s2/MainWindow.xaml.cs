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

namespace Lab1_s2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void close2(object sender,RoutedEventArgs e)
        {
            Button b = (Button)sender;
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Interface Inter = new Interface(2);
            Window w = Inter.Get_Window();
            Logic l = new Logic();
            l.SetLogic(w);
            w.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            Interface Inter = new Interface(3);
            Window w = Inter.Get_Window();
            w.Show();
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hide();
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Hide();
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
