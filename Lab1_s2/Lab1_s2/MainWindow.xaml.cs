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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Window1 neww = new Window1();
            neww.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            Window2 neww = new Window2();
            neww.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hide();
            Window3 neww = new Window3();
            neww.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Hide();
            Window4 neww = new Window4();
            neww.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
