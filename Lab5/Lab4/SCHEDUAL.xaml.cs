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
    /// Логика взаимодействия для SCHEDUAL.xaml
    /// </summary>
    public partial class SCHEDUAL : Window
    {
        string connectionstring = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public SCHEDUAL()
        {
            InitializeComponent();
            teach_grid.CanUserAddRows = false;
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string s = "SELECT * FROM TEACHER";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            connection.Close();
            teach_grid.ItemsSource = dt.DefaultView;
            cbday_of_week.Items.Clear();
            cbday_of_week_Copy.Items.Clear();
            for (int i=1;i<=6;i++)
            {
                s = "SELECT NAME FROM Day_of_the_week WHERE ID_DAY_OF_THE_WEEK = "+i.ToString();
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                DataTable tmp = new DataTable();
                adapter = new SqlDataAdapter(command);
                adapter.Fill(tmp);
                connection.Close();
                cbday_of_week.Items.Add(tmp.Rows[0][0]);
                cbday_of_week_Copy.Items.Add(tmp.Rows[0][0]);
            }
            cbday_of_week.SelectedIndex = 0;
            cbday_of_week_Copy.SelectedIndex = 0;
            //заповення номерів уроку
            lesson_number.Items.Clear();
            lesson_number_Copy.Items.Clear();
            for (int i = 1; i <= 8; i++)
            {
                lesson_number.Items.Add(i);
                lesson_number_Copy.Items.Add(i);
            }
            lesson_number.SelectedIndex = 0;
            lesson_number_Copy.SelectedIndex = 0;
            //заповнення класів
            s = "SELECT Name From Class ORDER BY Num_cl, Let_cl";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dcl = new DataTable();
            adapter.Fill(dcl);
            connection.Close();
            cbclass.Items.Clear();
            cbclass_Copy.Items.Clear();
            for (int i=0;i<dcl.Rows.Count;i++)
            {
                cbclass.Items.Add(dcl.Rows[i][0]);
                cbclass_Copy.Items.Add(dcl.Rows[0][0]);
            }
            cbclass.SelectedIndex = 0;
            cbclass_Copy.SelectedIndex = 0;
            s = "SELECT Cabinet FROM CABINET";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            dcl = new DataTable();
            adapter.Fill(dcl);
            cab.Items.Clear();
            for (int i = 0; i < dcl.Rows.Count; i++)
                cab.Items.Add(dcl.Rows[i][0]);
            cab.SelectedIndex = 0;
            connection.Close();
            //subj
            s = "SELECT NameSubject from Subject ORDER BY NameSubject";
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            dcl = new DataTable();
            adapter.Fill(dcl);
            connection.Close();
            for (int i = 0; i < dcl.Rows.Count; i++)
                subj.Items.Add(dcl.Rows[i][0]);
            subj.SelectedIndex = 0;

        }
        private int find_ID_class(string s)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string Scom = "SELECT ID FROM Class WHERE Name = '" + s + "'";
          //  MessageBox.Show(Scom);
            command = new SqlCommand(Scom, connection);
            adapter = new SqlDataAdapter(command);
            DataTable d = new DataTable();
            adapter.Fill(d);
            connection.Close();
            return int.Parse(d.Rows[0][0].ToString());

        }
        private int find_ID_cab(int num)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string Scom = "SELECT ID_cabinet FROM Cabinet WHERE Cabinet = " + num.ToString();
            //MessageBox.Show(Scom);
            command = new SqlCommand(Scom, connection);
            adapter = new SqlDataAdapter(command);
            DataTable d = new DataTable();
            adapter.Fill(d);
            connection.Close();
            return int.Parse(d.Rows[0][0].ToString());

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Change_window cw = new Change_window();
            cw.Show();
            Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Select_queries sw = new Select_queries();
            sw.Show();
           
        }
        private int find_id_d_o_w (string s)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string Scom = "SELECT ID_DAY_OF_THE_WEEK FROM Day_of_the_week WHERE Name = '" +s+"'";
           // MessageBox.Show(Scom);
            command = new SqlCommand(Scom, connection);
            adapter = new SqlDataAdapter(command);
            DataTable d = new DataTable();
            adapter.Fill(d);
            connection.Close();
            return int.Parse(d.Rows[0][0].ToString());
        }
        private int find_id_less (int id_d_o_w,int num)
        {
            return 8 * (id_d_o_w - 1) + num;
        }
        private int id_subj (string subj)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string Scom = "SELECT IDSubject FROM Subject WHERE NameSubject = '" + subj + "'";
            command = new SqlCommand(Scom, connection);
            adapter = new SqlDataAdapter(command);
            DataTable d = new DataTable();
            adapter.Fill(d);
            connection.Close();
            return int.Parse(d.Rows[0][0].ToString());
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
               // MessageBox.Show("1");
                string d_o_w = cbday_of_week.SelectedItem.ToString();
                int num_les = int.Parse(lesson_number.SelectedItem.ToString());
                int id_class = find_ID_class(cbclass.SelectedItem.ToString());
                int id_cab = find_ID_cab(int.Parse(cab.SelectedItem.ToString()));
                int id_d_o_w = find_id_d_o_w(d_o_w);
               // MessageBox.Show(id_d_o_w.ToString());
                //MessageBox.Show(num_les.ToString());
                int id_less = find_id_less(id_d_o_w, num_les);
                int id_subj1 = id_subj(subj.SelectedItem.ToString());
                //MessageBox.Show(id_subj1.ToString());
                int idt = int.Parse(id_teach.Text.ToString());
               // MessageBox.Show("2");
                string s = "INSERT INTO SCHEDULE(ID_cabinet, ID_lesson, ID_teacher, ID_class, IDSUBJECT) Values("+id_cab.ToString()+
                    ", "+id_less.ToString()+", "+idt.ToString()+", "+id_class.ToString()+", "+id_subj1.ToString()+")";
                //MessageBox.Show(s);
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Рядок вставлений у розклад");
            }
            catch
            {
                try
                {
                    string d_o_w = cbday_of_week.SelectedItem.ToString();
                    int num_les = int.Parse(lesson_number.SelectedItem.ToString());
                    int id_class = find_ID_class(cbclass.SelectedItem.ToString());
                    int id_cab = find_ID_cab(int.Parse(cab.SelectedItem.ToString()));
                    int id_d_o_w = find_id_d_o_w(d_o_w);
                    // MessageBox.Show(id_d_o_w.ToString());
                    //MessageBox.Show(num_les.ToString());
                    int id_less = find_id_less(id_d_o_w, num_les);
                    int id_subj1 = id_subj(subj.SelectedItem.ToString());
                    //MessageBox.Show(id_subj1.ToString());
                    int idt = int.Parse(id_teach.Text.ToString());
                    string s = "UPDATE SCHEDULE SET ID_cabinet = " + id_cab.ToString() + " , " + "ID_teacher = " + idt.ToString()
                        + " , " + "IDSUBJECT = " + id_subj1.ToString();
                    s += " Where (ID_class = " + id_class.ToString() + " AND " + "ID_lesson = " + id_less.ToString() + ")";
                   // MessageBox.Show(s);
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    command = new SqlCommand(s, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Рядок змінено у розкладі");
                }
                catch
                {
                    MessageBox.Show("Помилка заповнення даних");
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string d_o_w = cbday_of_week_Copy.SelectedItem.ToString();
                int num_les = int.Parse(lesson_number_Copy.SelectedItem.ToString());
                int id_class = find_ID_class(cbclass_Copy.SelectedItem.ToString());
                int id_d_o_w = find_id_d_o_w(d_o_w);
                int id_less = find_id_less(id_d_o_w, num_les);
                string s = "DELETE FROM SCHEDULE WHERE ID_lesson = "+id_less.ToString()+" AND ID_class = "+id_class.ToString();
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Рядок видалено у розкладі");
            }
            catch
            {
                MessageBox.Show("Помилка");
            }
        }
    }
}
