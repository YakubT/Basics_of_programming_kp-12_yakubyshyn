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
    /// Логика взаимодействия для Change_teacher.xaml
    /// </summary>
    public partial class Change_teacher : Window
    {
        string connectionstring = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable dt;
        DataTable showTable;
        public void showandget_start_information()
        {
            string s = "SELECT PersonId, Surname AS [Прізвище], Name AS [Ім'я], Middle_name AS [По-батькові] FROM Teacher ORDER BY Surname, Name, Middle_name";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            dt = new DataTable();
            adapter.Fill(dt);
            search_grid.ItemsSource = dt.DefaultView;
            showTable = new DataTable();
            adapter.Fill(showTable);
            connection.Close();
            Sur.TextChanged += serching_by_params;
            Name.TextChanged += serching_by_params;
            Mid.TextChanged += serching_by_params;
            connection.Close();
            fill_classes();
        }
        private void set_cabs()
        {
            string s = "SELECT Cabinet From Cabinet";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            cbcab.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
                cbcab.Items.Add(dt.Rows[i][0]);
            cbcab.SelectedIndex = 0;

        }
        private void fill_classes()
        {
            string s = "SELECT Name From Class ORDER BY Num_cl, Let_cl";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dcl = new DataTable();
            adapter.Fill(dcl);
            connection.Close();

        }
        string surn, name, mid;
        private bool restrictS(string s)
        {
            return (surn.Length == 0) || ((s.Length >= surn.Length) && (s.Substring(0, surn.Length) == surn));
        }
        private bool restrictN(string s)
        {
            return (name.Length == 0) || ((s.Length >= name.Length) && (s.Substring(0, name.Length) == name));
        }
        private bool restrictM(string s)
        {
            return (mid.Length == 0) || ((s.Length >= mid.Length) && (s.Substring(0, mid.Length) == mid));
        }
        private void serching_by_params(object sender, EventArgs e)
        {
            serching_by_params();
        }
        private void serching_by_params()
        {
            showTable.Clear();
            surn = Sur.Text.ToString();
            name = Name.Text.ToString();
            mid = Mid.Text.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = showTable.NewRow();
                r[0] = dt.Rows[i][0];
                r[1] = dt.Rows[i][1];
                r[2] = dt.Rows[i][2];
                r[3] = dt.Rows[i][3];
                if ((restrictS(dt.Rows[i][1].ToString())
                        && restrictN(dt.Rows[i][2].ToString())) && restrictM(dt.Rows[i][3].ToString()))
                    showTable.Rows.Add(r);


            }
            search_grid.ItemsSource = showTable.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string adds = Surnadd.Text.ToString();
            string addn = Nameadd.Text.ToString();
            string addmid = Middname.Text.ToString();
            if (adds.Contains("'"))
               adds =  adds.Replace("'", "''");
            if (addn.Contains("'"))
                addn = addn.Replace("'", "''");
            if (addmid.Contains("'"))
                addmid = addmid.Replace("'", "''");
            try
            {
                string s = "INSERT  INTO TEACHER (Surname, Name, Middle_Name) VALUES('"+adds.ToString()+"','"+addn.ToString()
                    +"', '"+addmid.ToString()+"')";
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                showandget_start_information();
            }
            catch
            {
               MessageBox.Show("Помилка");
            }
            Surnadd.Text = "";
            Nameadd.Text = "";
            Middname.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                int idteach = int.Parse(DeleteId.Text.ToString());
                string s = "Delete From Teacher Where PersonID = " + idteach.ToString();
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                showandget_start_information();
                DeleteId.Text = "";
            }
            catch
            {
                MessageBox.Show("Вчителя з таким ID не існує");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private int find_ID_class(string s)
        {
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string Scom = "SELECT ID FROM Class WHERE Name = '" + s + "'";
            command = new SqlCommand(Scom, connection);
            adapter = new SqlDataAdapter(command);
            DataTable d = new DataTable();
            adapter.Fill(d);
            connection.Close();
            return int.Parse(d.Rows[0][0].ToString());

        }
        public void show_classbossing()
        {
            dgbossing.CanUserAddRows = false;
            string s = "SELECT dbo.Class.Name AS Клас,  dbo.Classbossing.ID_teacher AS [ID Вчителя], dbo.Teacher.Surname AS [Прізвище] " +
                "FROM dbo.Class INNER JOIN " +
                "dbo.Classbossing ON dbo.Class.ID = dbo.Classbossing.ID_class INNER JOIN" +
                " dbo.Teacher ON dbo.Classbossing.ID_teacher = dbo.Teacher.PersonID";
            s += " ORDER BY NUM_Cl,Let_cl";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            DataTable test = new DataTable();
            adapter.Fill(test);
            dgbossing.ItemsSource = test.DefaultView;
            connection.Close();

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
            try
            {
                HashSet<int> hs = new HashSet<int>();
                int[,] mas = new int[dgbossing.Items.Count, 2];
                for (int i = 0; i < dgbossing.Items.Count; i++)
                {
                    DataRowView row = (DataRowView)dgbossing.Items[i];
                    string classname = row["Клас"].ToString();
                    int idteach = int.Parse(row["ID Вчителя"].ToString());
                    hs.Add(idteach);
                    mas[i, 0] = find_ID_class(classname);
                    mas[i, 1] = idteach;
                }
                if (hs.Count != dgbossing.Items.Count)
                {
                    MessageBox.Show("Два вчителя не можуть бути класними керівниками в двох і більше класах одночасно");
                    return;
                }
                string s = "Truncate Table Classbossing";
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                connection.Close();
                for (int i = 0; i < dgbossing.Items.Count; i++)
                {
                    s = "INSERT INTO CLASSBOSSING (ID_class, ID_teacher) VALUES(" + mas[i, 0].ToString() + ", " + mas[i, 1].ToString() + ")";
                    //MessageBox.Show(s);
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    command = new SqlCommand(s, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                show_classbossing();
            }
            catch
            {
                MessageBox.Show("Помилка");
            }
        }

        private int idcab_from_name (int name)
        {
            string s = "Select ID_cabinet from Cabinet Where cabinet = " + name.ToString();
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(command);
            DataTable tmp = new DataTable();
            adapter.Fill(tmp);
            connection.Close();
            return int.Parse(tmp.Rows[0][0].ToString());
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                int idcab = idcab_from_name(int.Parse(cbcab.SelectedItem.ToString()));
                string s = "INSERT INTO Teacher_cab (ID_cabinet,ID_teacher) Values (";
                s += idcab.ToString()+", ";
                int idteach = int.Parse(teach_cab.Text.ToString());
                s += idteach.ToString();
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
                MessageBox.Show(cbcab.SelectedItem.ToString() + " - " + idteach.ToString());
            }
            catch
            {
                try
                {
                    int idcab = idcab_from_name(int.Parse(cbcab.SelectedItem.ToString()));
                    int idteach = int.Parse(teach_cab.Text.ToString());
                    string s = "Update Teacher_cab  Set ID_teacher = "+idteach.ToString();
                    s += "Where Id_cabinet = " + idcab.ToString();
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    command = new SqlCommand(s, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show(cbcab.SelectedItem.ToString() + " - " + idteach.ToString());
                }
                catch
                {
                    MessageBox.Show("Вчителя з таким ID не існує");
                }
            }
        }

        public Change_teacher()
        {
            InitializeComponent();
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            search_grid.CanUserAddRows = false;
            showandget_start_information();
            show_classbossing();
            set_cabs();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
