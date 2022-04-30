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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace Prac3
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string connectionstring = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        string SQLQuery;
        bool stanp = false;
        public Window1()
        {
            InitializeComponent();
            paradmin.Text = null;
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Window w = new MainWindow();
            w.Show();
        }

        private void adminwind_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            SQLQuery= "SELECT Password FROM     dbo.Main_Table "+
                "WHERE Login = 'ADMIN';";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows[0][0].ToString()==paradmin.Text)
            {
                MessageBox.Show("Успішна авторизація");
                stanp = true; 
                foreach (Control el in myGrid.Children)
                    el.IsEnabled = true;
                SQLQuery = "SELECT Name, Surname, Login, Status, Restriction FROM dbo.Main_Table ;";
                command = new SqlCommand(SQLQuery, connection);
                adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
                Dgrid.ItemsSource = dt.DefaultView;
            }
            else
                MessageBox.Show("Неуспішна авторизація");
            connection.Close();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            DataTable dt = new DataTable();
            SQLQuery = "UPDATE dbo.Main_Table SET Password='" + tbnewpar1.Text;
            SQLQuery += "' WHERE Login = 'ADMIN';";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
        }
    }
}
