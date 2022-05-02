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
        DataTable Table;
        string SQLQuery;
        int knt_sprob;
        int index_user;
        bool f = true;
        public Window1()
        {
            InitializeComponent();
            paradmin.Password = null;
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            knt_sprob = 0;
            index_user = 0;
            Dgrid.CanUserAddRows = false;
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

                if (Pass[i]>=48 && Pass[i]<=(48+9))
                    Count3++;
            }
            return (Count1 * Count2 * Count3 != 0);
        }
        void update_info ()
        {
            Name_of_person.Content = Table.Rows[index_user][0].ToString();
            Surname.Content = Table.Rows[index_user][1].ToString();
            Login.Content = Table.Rows[index_user][2].ToString();
            Status.Content = Table.Rows[index_user][3].ToString();
            Restriction.Content = Table.Rows[index_user][4].ToString();
            cb.SelectedIndex = index_user;
            act.IsChecked = ((string) Status.Content == "True");
            rest.IsChecked = ((string)Restriction.Content == "True");
            if (Login.Content.ToString() == "ADMIN")
                act.IsEnabled = false;
            else
                act.IsEnabled = true;
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
            connection.Close();
            if (dt.Rows[0][0].ToString() == paradmin.Password)
            {
                MessageBox.Show("Успішна авторизація");
                foreach (Control el in myGrid.Children)
                    el.IsEnabled = true;
                knt_sprob = 0;
                updateTable();

            }
            else
            {
                knt_sprob++;
                MessageBox.Show($"Неправильно введений пароль! Помилкове введення №{knt_sprob}");
                if (knt_sprob == 3)
                {
                    Application.Current.Shutdown();
                }
            }
            paradmin.Clear();
            

        }

        bool is_have_restriction(string login)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            SQLQuery = "SELECT Restriction FROM     dbo.Main_Table " +
                "WHERE Login ='"+login+"';";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return (bool) dt.Rows[0][0];
        }
        bool Correct_pass(string login, string pass)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            SQLQuery = "SELECT Password FROM     dbo.Main_Table " +
              "WHERE Login ='" + login + "';";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return (pass == dt.Rows[0][0].ToString());

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Correct_pass("ADMIN", old_par.Password))
            {
                if (new_par1.Password == new_par2.Password)
                {
                    if (!is_have_restriction("ADMIN") || RestrictionFunc(new_par1.Password))
                    {
                        connection = new SqlConnection(connectionstring);
                        connection.Open();
                        if (new_par1.Password != "")
                            SQLQuery = "UPDATE dbo.Main_Table SET Password = '" + new_par1.Password;
                        else
                            SQLQuery = "UPDATE dbo.Main_Table SET Password = NULL";
                        if (new_par1.Password != "")
                            SQLQuery += "'";
                        SQLQuery += " WHERE Login = 'ADMIN';";
                        command = new SqlCommand(SQLQuery, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                        old_par.Clear();
                        new_par1.Clear();
                        new_par2.Clear();
                        MessageBox.Show("Новий пароль встановлено");
                    }
                    else
                        MessageBox.Show("Пароль має містити хоча б одну велику літеру латинського алфавіту, " +
                            "хоча б одну маленьку літеру та хоча б одну цифру");
                }
                else
                    MessageBox.Show("Паролі не співпадають");
            }
            else
                MessageBox.Show("Неправильно введений старий пароль");
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            index_user = (index_user - 1+ Table.Rows.Count) % (Table.Rows.Count);
            update_info();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            index_user = (index_user + 1) % (Table.Rows.Count);
            update_info();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void updateTable()
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            SQLQuery = "SELECT Name, Surname, Login, Status, Restriction FROM dbo.Main_Table ;";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            Table = new DataTable();
            adapter.Fill(Table);
            Dgrid.ItemsSource = Table.DefaultView;
            connection.Close();
          
            cb.Items.Clear();
            for (int i = 0; i < Table.Rows.Count; i++)
                cb.Items.Add(Table.Rows[i][2]);
            cb.SelectedIndex = index_user;
            f = true;
            update_info();
        }
        private void Adduser_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            try
            {
                SQLQuery = "INSERT INTO dbo.Main_Table (Name, Surname, Login, Status,Restriction) values('', '', '" + new_log.Text + "', 1, 0); ";
                command = new SqlCommand(SQLQuery, connection);
                command.ExecuteNonQuery();
                
                MessageBox.Show("Користувач доданий");
            }
            catch
            {
                MessageBox.Show("Користувача не додано! Можливо такий в базі вже є!");
            }
            connection.Close();
            updateTable();
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (f)
            {
                index_user = cb.SelectedIndex;
                if (index_user == -1)
                    index_user = 0;
                update_info();
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string stat = (act.IsChecked).ToString();
            string restp = (rest.IsChecked).ToString();
            if (stat == "True")
                stat = "1";
            else
                stat = "0";
            if (restp == "True")
                restp = "1";
            else
                restp = "0";
            SQLQuery = "Update dbo.Main_Table Set Status = "+stat+", Restriction = "+restp+" ";
            string log = (string) Login.Content;
            SQLQuery += " WHERE Login = '" + log + "';";
            command = new SqlCommand(SQLQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            f = false;
            updateTable();
        }
    }
}
