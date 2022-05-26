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
    /// Логика взаимодействия для Select_queries.xaml
    /// </summary>
    public partial class Select_queries : Window
    {
        string connectionstring = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataGrid[] dgmas;  
        public Select_queries()
        {
           
            InitializeComponent();
            dgmas = new DataGrid[5] { d1, d2, d3, d4, d5 };
            for (int i = 0; i < 5; i++)
                dgmas[i].CanUserAddRows = false;
               
            
            connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable dt = new DataTable();
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string SQLQuery = "SELECT * From Class;";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cb_class.Items.Add(dt.Rows[i][1]);
                Cb_class_2.Items.Add(dt.Rows[i][1]);
                Cb_class_Copy.Items.Add(dt.Rows[i][1]);
            }
            Cb_class_Copy.SelectedIndex = Cb_class_2.SelectedIndex =  Cb_class.SelectedIndex = 0;
            Cb_day_of_week.Items.Add("Понеділок");
            Cb_day_of_week.Items.Add("Вівторок");
            Cb_day_of_week.Items.Add("Середа");
            Cb_day_of_week.Items.Add("Четвер");
            Cb_day_of_week.Items.Add("П'ятниця");
            Cb_day_of_week.SelectedIndex = 0;
            for (int i = 1; i <= 8; i++)
                number_of_lesson.Items.Add(i);
            number_of_lesson.SelectedIndex = 0;
            connection.Close();
            dg.CanUserAddRows = false;
            dg2.CanUserAddRows = false;
            
            connection.Open();
            dt = new DataTable();
            SQLQuery = "SELECT NameSubject From Subject;";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cb_sub.Items.Add(dt.Rows[i][0]);
              //  MessageBox.Show(dt.Rows[i][0].ToString());
            }
            Cb_sub.SelectedIndex = 0;
            connection.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            int l_WindowCount = 0;

            foreach (Window w in App.Current.Windows)
            {
                if (w.IsVisible)
                l_WindowCount += 1;
            }
            if (l_WindowCount==0)
            {
                Application.Current.Shutdown();
            }
            else
            {
                Hide();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string res = Cb_day_of_week.SelectedItem.ToString();
            if (res == "П'ятниця")
                res = "П''ятниця";
            string SQLQuery =" SELECT dbo.Subject.NameSubject, dbo.Cabinet.Cabinet "+
                "FROM     dbo.Class INNER JOIN "+
                  "dbo.Schedule ON dbo.Class.ID = dbo.Schedule.ID_class INNER JOIN "+
                  " dbo.Subject ON dbo.Schedule.IDSubject = dbo.Subject.IDSubject INNER JOIN"+
                  " dbo.Lesson ON dbo.Schedule.ID_lesson = dbo.Lesson.ID_lesson INNER JOIN"+
                  " dbo.Day_of_the_week ON dbo.Lesson.ID_Day_of_the_week = dbo.Day_of_the_week.ID_Day_of_the_week INNER JOIN"+
                  " dbo.Cabinet ON dbo.Schedule.ID_cabinet = dbo.Cabinet.ID_cabinet "+
            "WHERE (dbo.Class.Name = '" +Cb_class.SelectedItem.ToString()+ "') AND (dbo.Day_of_the_week.Name = '" +res+"') AND (dbo.Lesson.lesson_number = "+number_of_lesson.SelectedItem.ToString()+");";
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                Subj.Content = dt.Rows[0][0].ToString();
                Cab_t.Content = dt.Rows[0][1].ToString();
            }
            else
            {
               Cab_t.Content =  Subj.Content = "Нема уроку";
                
            }
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow nw = new  MainWindow();
            nw.Show();
        }

        private string delete_spaces(string s)
        {
            s = s.Replace(" ","");
            return s;
        }
        private void Cb_class_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SQLQuery = "SELECT dbo.Teacher.Surname AS [Прізвище], dbo.Teacher.Name AS [Ім'я], dbo.Teacher.Middle_name AS [По-батькові] " +
                "FROM dbo.Class INNER JOIN " +
                  "dbo.Schedule ON dbo.Class.ID = dbo.Schedule.ID_class INNER JOIN " +
                  "dbo.Teacher ON dbo.Schedule.ID_teacher = dbo.Teacher.PersonID " +
                $"WHERE(dbo.Class.Name = '{Cb_class_2.SelectedItem.ToString()}') " +
                "GROUP BY dbo.Teacher.Surname, dbo.Teacher.Name, dbo.Teacher.Middle_name ;";
            DataTable dt = new DataTable();
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
            connection.Close();
        }
        List<int> Teacher;
        List<string> Teacher_strings; 
        private void fill_teacher()
        {
            Cb_teacher.Items.Clear();
            foreach (string el in Teacher_strings)
                Cb_teacher.Items.Add(el);
            Cb_teacher.SelectedIndex = 0;
        }
        private void Cb_sub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dt = new DataTable();
            connection = new SqlConnection(connectionstring);
            connection.Open();
            string str = Cb_sub.SelectedItem.ToString();
            //MessageBox.Show(str);
            if (str.Contains("'"))
            {
                str= str.Replace("'", "''");
            }
            //MessageBox.Show(str);
            string SQLQuery = "SELECT dbo.Teacher.PersonID, dbo.Teacher.Surname, dbo.Teacher.Name, dbo.Teacher.Middle_name FROM dbo.Schedule INNER JOIN " +
                "dbo.Subject ON dbo.Schedule.IDSubject = dbo.Subject.IDSubject INNER JOIN " +
                "dbo.Teacher ON dbo.Schedule.ID_teacher = dbo.Teacher.PersonID " +
                "WHERE ( dbo.Subject.NameSubject = N'"+str+ "') GROUP BY dbo.Teacher.PersonID, dbo.Teacher.Surname, dbo.Teacher.Name, dbo.Teacher.Middle_name";
           // MessageBox.Show(SQLQuery);
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            Teacher = new List<int>();
            Teacher_strings = new List<string>();
            //MessageBox.Show(dt.Rows.Count.ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Teacher.Add(Convert.ToInt32(dt.Rows[i][0]));
                Teacher_strings.Add(delete_spaces(Convert.ToString(dt.Rows[i][1]))+ " " + delete_spaces(Convert.ToString(dt.Rows[i][2]))+ " " + delete_spaces(Convert.ToString(dt.Rows[i][3])));
            }
            connection.Close();
            fill_teacher();

        }

        private void Cb_teacher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int i = Cb_teacher.SelectedIndex;
            if (i == -1)
                return;
        
            i = Teacher[i];
            string Sq = "SELECT dbo.Class.Name AS [Клас]" +
                "FROM dbo.Class INNER JOIN" +
                " dbo.Schedule ON dbo.Class.ID = dbo.Schedule.ID_class INNER JOIN" +
                " dbo.Teacher ON dbo.Schedule.ID_teacher = dbo.Teacher.PersonID " +
                "WHERE(dbo.Teacher.PersonID = " +i.ToString()+" )"+
                "GROUP BY dbo.Class.Name, dbo.Class.ID "+ "ORDER BY dbo.Class.ID";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(Sq, connection);
            adapter = new SqlDataAdapter(Sq, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dg2.ItemsSource = dt.DefaultView;
            connection.Close();
        }
        
        private int cntpup()
        {
            string s = "SELECT COUNT(*) From dbo.Pupil Inner Join dbo.Class ON dbo.Pupil.Idclass = dbo.Class.ID Where ( dbo.Class.Name = '" + Cb_class_Copy.SelectedItem.ToString() + "');";
            connection = new SqlConnection(connectionstring);
            connection.Open();
            command = new SqlCommand(s, connection);
            adapter = new SqlDataAdapter(s, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        private void Cb_class_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
            for (int i=1;i<=5;i++)
            {
                string s = "SELECT dbo.Lesson.lesson_number AS [№], dbo.Subject.NameSubject AS [назва предмету], dbo.Cabinet.Cabinet AS каб " +
                    "FROM dbo.Class INNER JOIN " +
                    "dbo.Schedule ON dbo.Class.ID = dbo.Schedule.ID_class INNER JOIN " +
                    "dbo.Lesson ON dbo.Schedule.ID_lesson = dbo.Lesson.ID_lesson INNER JOIN " +
                    "dbo.Day_of_the_week ON dbo.Lesson.ID_Day_of_the_week = dbo.Day_of_the_week.ID_Day_of_the_week INNER JOIN " +
                    "dbo.Subject ON dbo.Schedule.IDSubject = dbo.Subject.IDSubject INNER JOIN " +
                    "dbo.Cabinet ON dbo.Schedule.ID_cabinet = dbo.Cabinet.ID_cabinet " +
                    "WHERE (dbo.Day_of_the_week.ID_Day_of_the_week = " + i.ToString() + ") AND (dbo.Class.Name = '" + Cb_class_Copy.SelectedItem.ToString() + "');";
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(s, connection);
                adapter = new SqlDataAdapter(s, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                connection.Close();
                dgmas[i - 1].ItemsSource = dt.DefaultView;
                cnt_pupil.Content = cntpup();
            }
        }
    }
}
