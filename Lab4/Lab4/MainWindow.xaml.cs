using System;
using Microsoft.Win32;
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
using System.IO;
namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionstring = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public MainWindow()
        {
            InitializeComponent();
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Select_queries nw = new Select_queries();
            nw.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private int cntpup(int i)
        {
            string s = "SELECT COUNT(*) From dbo.Pupil Inner Join dbo.Class ON dbo.Pupil.Idclass = dbo.Class.ID Where ( dbo.Class.ID = '" + i.ToString() + "');";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(s, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        private int cnt_all_pup()
        {
            string s = "SELECT COUNT(*) From dbo.Pupil;";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(s, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog of = new SaveFileDialog();
            of.Filter = "CSV Files(*.csv) | *.csv";
            if (of.ShowDialog()==true)
            {
                string path = of.FileName;
                StringBuilder csvcontent = new StringBuilder();
                
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string SQLQuery = "Select * From Class ORDER BY Class.ID;";
                command = new SqlCommand(SQLQuery, connection);
                adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                connection.Close();
                //csvcontent.AppendLine("sep = ;");
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    csvcontent.AppendLine(dt.Rows[i][1].ToString());
                   csvcontent.AppendLine("Предмет;Середній бал з предмету по класу;Середній бал з предмету по школі");
                    SQLQuery = "SELECT dbo.Subject.NameSubject, ROUND(AVG(CAST(dbo.Mark.Mark AS float)), 2) " +
                        " FROM dbo.Mark INNER JOIN " +
                        "dbo.Pupil ON dbo.Mark.PersonID = dbo.Pupil.PersonID INNER JOIN " +
                        "dbo.Subject ON dbo.Mark.IDSubject = dbo.Subject.IDSubject " +
                        "GROUP BY dbo.Pupil.Idclass, dbo.Subject.NameSubject " +
                        "HAVING(dbo.Pupil.Idclass = "+dt.Rows[i][0].ToString()+" );";
                    connection.Open();
                    command = new SqlCommand(SQLQuery, connection);
                    adapter = new SqlDataAdapter(command);
                    DataTable dt2 = new DataTable();
                    adapter.Fill(dt2);
                    connection.Close();
                    
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        string str = dt2.Rows[j][0].ToString();
                        str= str.Replace("'", "''");
                        SQLQuery = "SELECT ROUND(AVG(CAST(dbo.Mark.Mark AS float)), 2) " +
                            "FROM dbo.Mark INNER JOIN " +
                            "dbo.Pupil ON dbo.Mark.PersonID = dbo.Pupil.PersonID INNER JOIN" +
                            " dbo.Subject ON dbo.Mark.IDSubject = dbo.Subject.IDSubject " +
                            "GROUP BY dbo.Subject.NameSubject " +
                            "HAVING(dbo.Subject.NameSubject = N'"+str+"');";
                        connection.Open();
                        command = new SqlCommand(SQLQuery, connection);
                        adapter = new SqlDataAdapter(command);
                        DataTable d3 = new DataTable();
                        adapter.Fill(d3);
                        connection.Close();
                        csvcontent.AppendLine(dt2.Rows[j][0].ToString() + ";" + dt2.Rows[j][1].ToString() + ";" + d3.Rows[0][0].ToString());
                    }
                    int cnt = cntpup(int.Parse(dt.Rows[i][0].ToString()));
                    csvcontent.AppendLine("кількісь учнів в класі;"+cnt.ToString());
                }
                csvcontent.AppendLine("Всього учнів:;"+cnt_all_pup().ToString());
                FileStream stream  = new FileStream(path, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(stream,Encoding.Unicode);
                sw.Write(csvcontent.ToString());
                sw.Close();
                MessageBox.Show("Файл збережено");
            }
        }
    }
}
