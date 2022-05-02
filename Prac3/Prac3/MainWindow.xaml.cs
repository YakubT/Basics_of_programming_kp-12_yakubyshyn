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
using System.Configuration;
using System.Data.SqlClient;
namespace Prac3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            Window nw = new Window1();
            nw.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hide();
            Window nw = new Window2();
            nw.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Hide();
            Window nw = new Info();
            nw.Show();
        }
    }
}
