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
using System.Windows.Shapes;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для Change_window.xaml
    /// </summary>
    public partial class Change_window : Window
    {
        public Change_window()
        {
            InitializeComponent();
        }

        private void Change_window1_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Change_data_pupil w = new Change_data_pupil();
            Hide();
            w.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Change_teacher ct = new Change_teacher();
            ct.Show();
            Hide();
        }
    }
}
