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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        string connectionstring = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable dt;
        int cnt;
        string log = null;
        public Window2()
        {
            InitializeComponent();
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Window nw = new MainWindow();
            nw.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void show_all()
        {
            foreach (Control el in Mygrid.Children)
                el.IsEnabled = true;
           
        }
        private void Avtorizat_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            log = login_tb.Text;
            string pass = pass_b1.Password;
            string SQLQuery;
            SQLQuery = "SELECT Password, Status FROM     dbo.Main_Table " +
               "WHERE Login = '"+log+"';";
            command = new SqlCommand(SQLQuery,connection);
            dt = new DataTable();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            if (dt.Rows.Count==0)
            {
                MessageBox.Show("Користувача з таким логіном немає в системі");
                return;
            }
            if (dt.Rows[0][1].ToString() == "False")
            {
                MessageBox.Show("Користувач заблокований");
                login_tb.Text = "";
                pass_b1.Password = "";
                return;
            }


            if (dt.Rows[0][0].ToString()==pass)
            {
                login_tb.Text = "";
                MessageBox.Show("Ви успішно авторизовані");
                show_all();
                cnt = 0;

            }
            else
            {
                cnt++;
                MessageBox.Show($"Неправильно введений пароль! Помилкове введення №{cnt}");
                if (cnt == 3)
                    Application.Current.Shutdown();
            }
            connection.Close();
            
            pass_b1.Password = "";
        }

        private void login_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            cnt = 0;
        }
        bool is_have_restriction(string login)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string SQLQuery = "SELECT Restriction FROM     dbo.Main_Table " +
                "WHERE Login ='" + login + "';";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return (bool)dt.Rows[0][0];
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string Name =  Name_tb.Text;
            string Surname = Surname_tb.Text;
            string pass1 = pass_bnew.Password;
            string pass2 = pass_bnewrep.Password;
            if (pass1!=pass2)
            {
                MessageBox.Show("Паролі не співпадають");
                return;
            }
            if (is_have_restriction(log) && !RestrictionFunc(pass1))
            {
                MessageBox.Show("Пароль має містити хоча б одну велику літеру латинського алфавіту," +
                           "хоча б одну маленьку літеру та хоча б одну цифру");
                return;
            }
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string SQlQuery = "Update dbo.Main_Table Set Name = '" + Name + "'," + " Surname = '" + Surname + "', "+"Password = '"+pass1+"' ";
            SQlQuery+=" WHERE Login = '" + log + "';";
            command = new SqlCommand(SQlQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Name_tb.Text = "";
            Surname_tb.Text= "";
            pass_bnew.Password = "";
            pass_bnewrep.Password = "";
            MessageBox.Show("Ви успішні оновили дані");
        }
        Boolean RestrictionFunc(String Pass)
        {
            Byte Count1, Count2, Count3;
            Byte LenPass = (Byte)Pass.Length;
            Count1 = Count2 = Count3 = 0;
            for (Byte i = 0; i < LenPass; i++)
            {
                if ((Convert.ToInt32(Pass[i]) >= 65) &&

                (Convert.ToInt32(Pass[i]) <= 65 + 25))

                    Count1++;
                if ((Convert.ToInt32(Pass[i]) >= 97) &&

                (Convert.ToInt32(Pass[i]) <= 97 + 25))
                    Count2++;

                if (Pass[i] >= 48 && Pass[i] <= (48 + 9))
                    Count3++;
            }
            return (Count1 * Count2 * Count3 != 0);
        }
        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            string Name = NameRegistr_tb.Text;
            string Surname = SurnameRegistr_tb.Text;
            string login = login_registr.Text;
            string pass1 = passreg.Password;
            string pass2 =passreg_Copy.Password;
            if (pass1!=pass2)
            {
                MessageBox.Show("Паролі не співпадають");
                return;
            }
            if (!RestrictionFunc(pass1))
            {
                MessageBox.Show("Пароль має містити хоча б одну велику літеру латинського алфавіту, " +
                           "хоча б одну маленьку літеру та хоча б одну цифру");
                return;
            }
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string SQLQuery = "INSERT INTO dbo.Main_Table (Name, Surname, Login, Password, Status,Restriction) values('"+Name+"', '"+Surname+"', '"+login+"', '"+pass1+"', 1, "+"1);";
            command = new SqlCommand(SQLQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Користувач успішно зареєстрований");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
