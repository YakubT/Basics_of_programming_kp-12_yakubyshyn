using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab_2_First_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DispatcherTimer dT;
        static int Radius = 15;
        static int PointCount = 5;
        static List<Ellipse> EllipseArray = new List <Ellipse>();
        static PointCollection pC = new PointCollection();
        int potpoint = 0;
        Line[] lmas;
        HashSet<int> hs = new HashSet<int>();
        double s = 0;
        public MainWindow()
        {
            dT = new DispatcherTimer();

            InitializeComponent();
            InitPoints();
            

            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);            
        }

        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75*MainWin.Width)-3*Radius);
                p.Y = rnd.Next(Radius, (int)(0.90*MainWin.Height-3*Radius));                
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
            potpoint = 0;
            lmas = new Line[PointCount];
            hs.Clear();
            s = 0;
        }
       
       
        private void PlotPoints()
        {            
            for (int i=0; i<PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius/2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius/2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }



        private void VelCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
                        
            dT.Interval = new TimeSpan(0,0,0,0,Convert.ToInt16(item.Content));
        }

        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled)
            {
                dT.Stop();
                NumElemCB.IsEnabled = true;
            }
            else
            {                
                NumElemCB.IsEnabled = false;
                dT.Start();
            }
        }
        
        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            MyCanvas.Children.Clear();
            Plotway();
            PlotPoints();

        }
        
        private double len(int i,int j)
        {
            return Math.Sqrt((pC[i].X - pC[j].X) * (pC[i].X - pC[j].X) + (pC[i].Y - pC[j].Y) * (pC[i].Y - pC[j].Y));
        }

        
        private void OneStep(object sender, EventArgs e)
        {
           
            if (hs.Count!=PointCount)
            {
                MyCanvas.Children.Clear();
                int tmp = -1;
                double lentmp = int.MaxValue;
                for (int i=0;i<PointCount;i++)
                {
                    if (hs.Count!=PointCount-1 && (i == 0))
                        continue;
                    if (!hs.Contains(i) && len(potpoint,i)<lentmp)
                    {
                        tmp = i;
                        lentmp = len(potpoint, i);
                    }
                }
                hs.Add(tmp);
                lmas[hs.Count - 1] = new Line();
                lmas[hs.Count - 1].X1 = pC[potpoint].X;
                lmas[hs.Count - 1].Y1 = pC[potpoint].Y;
                lmas[hs.Count - 1].X2 = pC[tmp].X;
                lmas[hs.Count - 1].Y2 = pC[tmp].Y;
                lmas[hs.Count - 1].StrokeThickness = 2;
                lmas[hs.Count - 1].Stroke = Brushes.Black;
                potpoint = tmp;
                s += lentmp;
                Plotway();
                PlotPoints();
                if (hs.Count == PointCount)
                    MessageBox.Show("Шлях побудований, його довжина = " + Math.Round(s,3).ToString());
            }


           
            
           
        }
        private void Plotway()
        {
            for (int i = 0; i < hs.Count; i++)
                MyCanvas.Children.Add(lmas[i]);
        }
    }
}