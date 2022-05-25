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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для Change_data_pupil.xaml
    /// </summary>
    public partial class Change_data_pupil : Window
    {
        string connectionstring = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable dt;
        DataTable showTable;
        public Change_data_pupil()
        {
            InitializeComponent();
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string s = "SELECT dbo.Pupil.PersonID AS [ID], dbo.Pupil.Surname AS [Прізвище], dbo.Pupil.FirstName AS [Ім'я], dbo.Pupil.MiddleName AS [По-батькові], dbo.Class.Name AS [Клас] " +
                "FROM dbo.Pupil INNER JOIN" +
                " dbo.Class ON dbo.Pupil.Idclass = dbo.Class.ID";
            s += " ORDER BY dbo.Class.ID, dbo.Pupil.Surname, dbo.Pupil.FirstName, dbo.Pupil.MiddleName";
            connection = new SqlConnection(connectionstring);
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            dt = new DataTable();
            adapter.Fill(dt);
            showTable = new DataTable();
            adapter.Fill(showTable);
            dgrid.ItemsSource = dt.DefaultView;
            dgrid.CanUserAddRows = false;
            connection.Close();
            updateclasses();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Change_window w = new Change_window();
            w.Show();
            Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        string surn, name, mid;
        private bool restrictS(string s)
        {
            return (surn.Length == 0) || ( (s.Length >= surn.Length) && (s.Substring(0, surn.Length) == surn));
        }
        private bool restrictN(string s)
        {
            return (name.Length == 0) || ((s.Length >= name.Length) && (s.Substring(0, name.Length) == name));
        }

        private void Surn_TextChanged(object sender, TextChangedEventArgs e)
        {
            superfunc();
        }

        private bool restrictM (string s)
        {
            return (mid.Length == 0) || ((s.Length >= mid.Length) && (s.Substring(0, mid.Length) == mid));
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            superfunc();
        }

        private void Middle_TextChanged(object sender, TextChangedEventArgs e)
        {
            superfunc();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Name.Text = "";
            Surn.Text = "";
            Middle.Text = "";
            cbclasses.SelectedIndex = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(tbdel.Text.ToString());
                string s = "Delete From pupil WHERE PersonID = " + id.ToString();
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                s = "SELECT dbo.Pupil.PersonID AS [ID], dbo.Pupil.Surname AS [Прізвище], dbo.Pupil.FirstName AS [Ім'я], dbo.Pupil.MiddleName AS [По-батькові], dbo.Class.Name AS [Клас] " +
                    "FROM dbo.Pupil INNER JOIN" +
                    " dbo.Class ON dbo.Pupil.Idclass = dbo.Class.ID";
                s += " ORDER BY dbo.Class.ID, dbo.Pupil.Surname, dbo.Pupil.FirstName, dbo.Pupil.MiddleName";
                connection = new SqlConnection(connectionstring);
                command = new SqlCommand(s, connection);
                adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
                showTable = new DataTable();
                adapter.Fill(showTable);
                dgrid.ItemsSource = dt.DefaultView;
                dgrid.CanUserAddRows = false;
                connection.Close();
                superfunc();
                tbdel.Text = "";
            }
            catch
            {
                MessageBox.Show("Школяра з таким ID не існує");
            }
        }

        private int find_ID_class(string s)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string Scom = "SELECT ID FROM Class WHERE Name = '"+s+"'";
            command = new SqlCommand(Scom,connection);
            adapter = new SqlDataAdapter(command);
            DataTable d = new DataTable();
            adapter.Fill(d);
            connection.Close();
            return int.Parse(d.Rows[0][0].ToString());
            
        }
        private void update ()
        {
            string s;
            s = "SELECT dbo.Pupil.PersonID AS [ID], dbo.Pupil.Surname AS [Прізвище], dbo.Pupil.FirstName AS [Ім'я], dbo.Pupil.MiddleName AS [По-батькові], dbo.Class.Name AS [Клас] " +
              "FROM dbo.Pupil INNER JOIN" +
              " dbo.Class ON dbo.Pupil.Idclass = dbo.Class.ID";
            s += " ORDER BY dbo.Class.ID, dbo.Pupil.Surname, dbo.Pupil.FirstName, dbo.Pupil.MiddleName";
            connection = new SqlConnection(connectionstring);
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            dt = new DataTable();
            adapter.Fill(dt);
            showTable = new DataTable();
            adapter.Fill(showTable);
            dgrid.ItemsSource = dt.DefaultView;
            dgrid.CanUserAddRows = false;
            connection.Close();
            superfunc();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string adds = tbs.Text.ToString();
                string addn = tbn.Text.ToString();
                string addm = tbm.Text.ToString();
                string cl = cb2.SelectedItem.ToString();
                int ID = find_ID_class(cl);
                string s = "INSERT INTO Pupil (Surname, FirstName, MiddleName, Idclass) VALUES ('" + adds + "', '" + addn
                    + "', '" + addm + "', " + ID.ToString() + ")";
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                update();
                tbs.Text = "";
                tbn.Text = "";
                tbm.Text = "";
            }
            catch
            {
                MessageBox.Show("Помилка додавання, невірно внесені дані");
            }
        }

        private void updateclasses()
        {
            string s = "Select Name From Class ORDER BY Num_cl, Let_cl";
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt2 = new DataTable();
            adapter.Fill(dt2);
            connection.Close();
            cbclasses.Items.Clear();
            cb2.Items.Clear();
            cb_delete_class.Items.Clear();
            cbclasses.Items.Add("Всі класи");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                cbclasses.Items.Add(dt2.Rows[i][0]);
                cb2.Items.Add(dt2.Rows[i][0].ToString());
                cb_delete_class.Items.Add(dt2.Rows[i][0].ToString());
            }
            connection.Close();
            cb2.SelectedIndex = 0;
            cbclasses.SelectedIndex = 0;
            cb_delete_class.SelectedIndex = 0;
           
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
           try
            {
                string ncl = tbaddclass.Text.ToString();
                int num = int.Parse(ncl.Substring(0, ncl.IndexOf("-")));
                string s = "INSERT INTO Class (Name, Num_cl, Let_cl) VALUES ('" + ncl + "', "+num.ToString()
                    + ", '"+ncl.Substring(ncl.IndexOf("-")+1)+"')";
               // MessageBox.Show(s);
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                tbaddclass.Text = "";
                connection.Close();
                updateclasses();
                
            }
            catch
            {
                MessageBox.Show("Такий клас вже існує, або клас не відповідає формату число-*");
            }
            
            
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
                string cl = cb_delete_class.SelectedItem.ToString();
                string s = "Delete From Class WHERE Name = '" + cl.ToString()+"'";
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                updateclasses();
           
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(tbdel.Text.ToString());
                
            }
            catch
            {
                MessageBox.Show("Школяра з таким ID не існує");
            }
        }

        private void superfunc()
        {
            showTable.Clear();
            int index = cbclasses.SelectedIndex;
            if (index == -1)
                return;
            surn = Surn.Text.ToString();
            name = Name.Text.ToString();
            mid = Middle.Text.ToString();
            showTable.Clear();
            //MessageBox.Show(index.ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = showTable.NewRow();
                r[0] = dt.Rows[i][0];
                r[1] = dt.Rows[i][1];
                r[2] = dt.Rows[i][2];
                r[3] = dt.Rows[i][3];
                r[4] = dt.Rows[i][4];
                if (index == 0)
                {

                    if ((restrictS(dt.Rows[i][1].ToString())
                        && restrictN(dt.Rows[i][2].ToString())) && restrictM(dt.Rows[i][3].ToString()))
                        showTable.Rows.Add(r);



                }
                else
                {
                    if (((restrictS(dt.Rows[i][1].ToString())
                        && restrictN(dt.Rows[i][2].ToString())) && restrictM(dt.Rows[i][3].ToString())) &&
                       ((dt.Rows[i][4].ToString() == cbclasses.Items[index].ToString())))
                        showTable.Rows.Add(r);
                }
            }

            dgrid.ItemsSource = showTable.DefaultView;
        }
        private void cbclasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            superfunc();
        }

    }
}
