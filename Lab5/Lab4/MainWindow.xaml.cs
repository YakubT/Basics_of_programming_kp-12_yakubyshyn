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
        private int cnt_grounded_by_mark(int classid,int min_mark)
        {
            connection = new SqlConnection(connectionstring);
           string SQLQuery = "SELECT COUNT(*) AS [Кількість відмінників] " +
                       "FROM (SELECT dbo.Pupil.PersonID " +
                       "FROM      dbo.Mark INNER JOIN " +
                       "dbo.Pupil ON dbo.Mark.PersonID = dbo.Pupil.PersonID INNER JOIN " +
                       "dbo.Class ON dbo.Pupil.Idclass = dbo.Class.ID " +
                       "GROUP BY dbo.Pupil.PersonID, dbo.Class.ID " +
                       "HAVING(dbo.Class.ID = "+classid.ToString()+") AND(MIN(dbo.Mark.Mark) >= "+min_mark.ToString()+")) AS derivedtbl_1";
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt3 = new DataTable();
            adapter.Fill(dt3);
            int excell = int.Parse(dt3.Rows[0][0].ToString());
            connection.Close();
            return excell;
        }
        private int cnt_teachers_subject(int subj)
        {
            connection = new SqlConnection(connectionstring);
            string SQLQuery = "SELECT COUNT(*) AS Expr1 " +
                "FROM(SELECT dbo.Subject.IDSubject, dbo.Teacher.PersonID " +
                "FROM      dbo.Schedule INNER JOIN " +
                "dbo.Subject ON dbo.Schedule.IDSubject = dbo.Subject.IDSubject INNER JOIN " +
                "dbo.Teacher ON dbo.Schedule.ID_teacher = dbo.Teacher.PersonID " +
                "GROUP BY dbo.Teacher.PersonID, dbo.Subject.IDSubject " +
                "HAVING(dbo.Subject.IDSubject = "+subj.ToString()+")) AS derivedtbl_1";
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt3 = new DataTable();
            adapter.Fill(dt3);
            int excell = int.Parse(dt3.Rows[0][0].ToString());
            connection.Close();
            return excell;
        }
        private int cnt_cab ()
        {
            string s = "SELECT COUNT(*) From Cabinet";
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
                    csvcontent.AppendLine("кількість учнів в класі;"+cnt.ToString());
                    //кількість відмінників
                    int vidm = cnt_grounded_by_mark(int.Parse(dt.Rows[i][0].ToString()),10);
                    csvcontent.AppendLine("Кількість відмінників:;"+vidm);
                   int horosh = cnt_grounded_by_mark(int.Parse(dt.Rows[i][0].ToString()), 7)-vidm;
                    csvcontent.AppendLine("Кількість хорошистів:;"+horosh);
                    int troish = cnt_grounded_by_mark(int.Parse(dt.Rows[i][0].ToString()), 4)-horosh-vidm;
                    csvcontent.AppendLine("Кількість тріїшників:;" + troish);
                    int dvoish = cnt - vidm - horosh - troish;
                    csvcontent.AppendLine("Кількість двіїшників:;" + dvoish);

                    SQLQuery = "SELECT dbo.Teacher.Surname, dbo.Teacher.Name, dbo.Teacher.Middle_name " +
                        "FROM dbo.Class INNER JOIN " +
                        "dbo.Classbossing ON dbo.Class.ID = dbo.Classbossing.ID_class INNER JOIN " +
                        "dbo.Teacher ON dbo.Classbossing.ID_teacher = dbo.Teacher.PersonID " +
                        "WHERE(dbo.Class.ID = " + dt.Rows[i][0].ToString() + ")";
                    connection.Open();
                    command = new SqlCommand(SQLQuery,connection);
                    adapter = new SqlDataAdapter(command);
                    DataTable tmpdt = new DataTable();
                    adapter.Fill(tmpdt);
                    csvcontent.AppendLine("Класний керівник;"+tmpdt.Rows[0][0].ToString()+";"+tmpdt.Rows[0][1].ToString()+";"+tmpdt.Rows[0][2].ToString());
                    connection.Close();
                }
                csvcontent.AppendLine("Предмет;Кількість вчителів з предмету");
                DataTable buf = new DataTable();
                SQLQuery = "Select * From Subject Order BY NameSubject";
                connection.Open();
                command = new SqlCommand(SQLQuery, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(buf);
                connection.Close();
                for (int i=0;i<buf.Rows.Count;i++)
                {
                    csvcontent.AppendLine(buf.Rows[i][1].ToString()+";"+cnt_teachers_subject(int.Parse(buf.Rows[i][0].ToString())));
                }
                csvcontent.AppendLine("Всього учнів:;"+cnt_all_pup().ToString());
                csvcontent.AppendLine("Кількість кабінетів:;" +cnt_cab().ToString());
                FileStream stream  = new FileStream(path, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(stream,Encoding.Unicode);
                sw.Write(csvcontent.ToString());
                sw.Close();
                MessageBox.Show("Файл збережено");
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Change_window chw = new Change_window();
            Hide();
            chw.Show();
        }
    }
}
