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

namespace Lab1_s2
{
    class Logic
    {
        Window w;
        public void w2close(object sender, RoutedEventArgs e)
        {
            
            w.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
        public void SetLogic(Window w)
        {
            this.w = w;
            switch (w.Name)
            {
                case "w2":
                    Grid g =(Grid) w.Content;
                    Button b3= new Button();
                    foreach (Button b in g.Children.OfType<Button>())
                    {
                        if (b.Name == "b3")
                            b3 = b;

                    }
                    b3.Click += w2close;
                    break;
                    
            }
        }
    }
}
