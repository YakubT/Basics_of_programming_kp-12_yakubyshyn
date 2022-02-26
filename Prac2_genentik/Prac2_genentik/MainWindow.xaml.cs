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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
namespace Prac2_genentik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        static DispatcherTimer dT =new DispatcherTimer();
        static int Radius = 20;
        static uint PointCount;
        static uint size_of_populp;
        static double probabilityV;
        static uint countit;
        static uint time_pause;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static List <List <int>> Lpop = new List<List <int> >();
        static PointCollection pC = new PointCollection();
        public MainWindow()
        {
            InitializeComponent();
            InitPolygon();
            MyCanvas.Children.Add(myPolygon);
            dT.Tick += new EventHandler(OneStep);
        }
        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
        }
        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75 * MyCanvas.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * MyCanvas.Height - 3 * Radius));
                pC.Add(p);
            }

            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();

                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                el.Fill = Brushes.LightBlue;
                EllipseArray.Add(el);
            }
        }
        private void PlotPoints()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }
        private void RandomShuffle (List <int> l)
        {
            for (int k=0;k<l.Count;k++)
            {
                int i = rnd.Next(l.Count);
                int j = rnd.Next(l.Count);
                int c = l[i];
                l[i] = l[j];
                l[j] = c;
            }
        }
        private List<int> CreatePosl()
        {
            List<int> p = new List<int>();
            for (int i = 1; i <= PointCount; i++)
                p.Add(i);
            RandomShuffle(p);
            return p;
        }
        private void uniq(List<int> l)
        {
            List<int> cop = new List<int>();
            HashSet<int> hs = new HashSet<int>();
            for (int i=0;i<l.Count;i++)
            {
                if (!hs.Contains(l[i]))
                {
                    cop.Add(l[i]);
                    hs.Add(l[i]);
                }
            }
            l.Clear();
            for (int i = 0; i < cop.Count; i++)
                l.Add(cop[i]);
        }
        private void swap(ref int a,ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        private void make_child(List<int> p1, List<int> p2)
        {
            int point_of_kross = rnd.Next(p1.Count);
            List<int> tmp1= new List<int>();
            List<int> tmp2 = new List<int>();
            for (int i = 0; i <= point_of_kross; i++)
            {
                tmp1.Add(p1[i]);
                tmp2.Add(p2[i]);
            }
            for (int i = point_of_kross + 1; i < p1.Count; i++)
            {
                tmp1.Add(p2[i]);
                tmp2.Add(p1[i]);
            }
            List<int> child1 = new List<int>();
            List<int> child2 = new List<int>();
            for (int i = 0; i < tmp1.Count; i++)
            {
                child1.Add(tmp1[i]);
                child2.Add(tmp2[i]);
            }
            for (int i = 0; i < tmp2.Count; i++)
            {
                child1.Add(tmp2[i]);
                child2.Add(tmp1[i]);
            }
            double p = rnd.NextDouble();
            if (p >= 0.5)
                Lpop.Add(child1);
            else
                Lpop.Add(child2);
            uniq(Lpop[Lpop.Count - 1]);
            p= rnd.NextDouble();
            //мутація
            if (p<=probabilityV)
            {
                int i1 = rnd.Next(Lpop[Lpop.Count - 1].Count);
                int i2 = rnd.Next(Lpop[Lpop.Count - 1].Count);
                if (i2 < i1)
                    swap(ref i2,ref i1);
                for (int i = i1; i < i1 + (i2 - i1 + 1) / 2; i++)
                {
                    // swap(ref Lpop[Lpop.Count - 1][i], ref Lpop[Lpop.Count - 1][i2 - (i - i1)]);
                    int c = Lpop[Lpop.Count - 1][i];
                    Lpop[Lpop.Count - 1][i] = Lpop[Lpop.Count - 1][i2 - (i - i1)];
                    Lpop[Lpop.Count - 1][i2 - (i - i1)] = c;
                }

            }
            
        }
        private void OneStep(object sender, EventArgs e)
        {
           // MessageBox.Show(countit.ToString());
            if (countit != 0)
            {

                while (Lpop.Count != 2 * size_of_populp)
                {
                    int i1 = rnd.Next((int)size_of_populp);
                    int i2 = rnd.Next((int)size_of_populp);
                    make_child(Lpop[i1], Lpop[i2]);
                }
                List<Tuple<double, string>> odbor = new List<Tuple<double, string>>();
                for (int i = 0; i < Lpop.Count; i++)
                {
                    string s;
                    s = Lpop[i][0].ToString();
                    for (int j = 1; j < PointCount; j++)
                        s += " " + Lpop[i][j].ToString();
                    odbor.Add(new Tuple <double, string>(len(Lpop[i]),s));
                }
                odbor.Sort();
                Lpop.Clear();
                for (int i = 0; i < size_of_populp; i++)
                {
                    List<int> lis = new List<int>();
                    string[] smas = odbor[i].Item2.Split(" ");
                    for (int j = 0; j < smas.Length; j++)
                        lis.Add(int.Parse(smas[j]));
                    Lpop.Add(lis);
                }
                //MessageBox.Show(Lpop[0].Count().ToString());
                //for (int i = 0; i < Lpop[0].Count; i++)
                   // MessageBox.Show(Lpop[0][i].ToString());
                PlotWay(Lpop[0]);
                countit--;
                lb.Content = len(Lpop[0]).ToString();
            }
        }
        private void PlotWay(List<int> BestWay)
        {
            MyCanvas.Children.Remove(myPolygon);

            PointCollection Points = new PointCollection();
           // MessageBox.Show(BestWay.Count.ToString());
            for (int i = 0; i < BestWay.Count; i++)
            {
               
                Points.Add(pC[BestWay[i] - 1]);
            }
            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }
        private double len (List <int> l)
        {
            double res = 0;
            for (int i=1;i<PointCount;i++)
            {
                res += Math.Sqrt((pC[l[i]-1].X - pC[l[i - 1]-1].X) * (pC[l[i]-1].X - pC[l[i - 1]-1].X) + (pC[l[i]-1].Y - pC[l[i - 1]-1].Y) * (pC[l[i]-1].Y - pC[l[i - 1]-1].Y)); 
            }
            res += Math.Sqrt((pC[l[(int)PointCount-1]-1].X - pC[l[0]-1].X) * (pC[l[(int)PointCount - 1]-1].X - pC[l[0]-1].X)+ (pC[l[(int)PointCount - 1]-1].Y - pC[l[0]-1].Y)* (pC[l[(int)PointCount - 1]-1].Y - pC[l[0]-1].Y)) ;
            return res;
        }
        private void Start()
        {
            MyCanvas.Children.Clear();
            InitPoints();
            PlotPoints();
            Lpop.Clear();
            for (int i=0;i<size_of_populp;i++)
                Lpop.Add(CreatePosl());
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PointCount = uint.Parse(count_city.Text);
                size_of_populp = uint.Parse(size_of_pop.Text);
                probabilityV = double.Parse(probability.Text);
                if (probabilityV<0 || probabilityV>1 || size_of_populp<2)
                {
                    MessageBox.Show("Помилка заповнення даних");
                    return;
                }
                countit = uint.Parse(count_of_op.Text);
                ListBoxItem li = (ListBoxItem)cb.Items[cb.SelectedIndex];
                time_pause =uint.Parse(li.Content.ToString());
                Start();
                dT.IsEnabled = true;
                dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(time_pause));
                

            }
            catch
            {
                MessageBox.Show("Помилка заповнення даних");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dT.IsEnabled = dT.IsEnabled ^ true;
        }
    }
}
