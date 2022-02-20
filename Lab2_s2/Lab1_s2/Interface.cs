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
    class Interface
    {
        private Window w;
        private void setting2()
        {
            w.Name = "w2";
            w.Title = "Вікно №2";
            w.Height = 479;
            w.Width = 800;
            w.ResizeMode = System.Windows.ResizeMode.NoResize;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.Background = Brushes.White;
            Grid gr = new Grid();
        gr.Name = "gr";
                    gr.Width = 800;
                    gr.Height = 463.04;
                    gr.HorizontalAlignment = HorizontalAlignment.Stretch;
                    gr.VerticalAlignment = VerticalAlignment.Stretch;
                    //labels
                    Label label1 = new Label();
        label1.Content = "Введіть ПІП";
                    label1.HorizontalAlignment = HorizontalAlignment.Left;
                    label1.VerticalAlignment = VerticalAlignment.Top;
                    label1.Width = 227;
                    label1.Height = 25.96;
                    label1.Margin =new Thickness(42,24,0,0);
        gr.Children.Add(label1);
                    Label label2 = new Label();
        label2.Content = "Введіть групу";
                    label2.HorizontalAlignment = HorizontalAlignment.Left;
                    label2.VerticalAlignment = VerticalAlignment.Top;
                    label2.Width = 227;
                    label2.Height = 25.96;
                    label2.Margin = new Thickness(42, 114, 0, 0);
        gr.Children.Add(label2);
                    Label label3 = new Label();
        label3.Content = "Введіть рік народження";
                    label3.HorizontalAlignment = HorizontalAlignment.Left;
                    label3.VerticalAlignment = VerticalAlignment.Top;
                    label3.Width = 227;
                    label3.Height = 25.96;
                    label3.Margin = new Thickness(42, 206, 0, 0);
        gr.Children.Add(label3);
                    Label label4 = new Label();
        label4.Content = "Введіть номер залікової книжки";
                    label4.HorizontalAlignment = HorizontalAlignment.Left;
                    label4.VerticalAlignment = VerticalAlignment.Top;
                    label4.Width = 227;
                    label4.Height = 25.96;
                    label4.Margin = new Thickness(42, 295, 0, 0);
        gr.Children.Add(label4);
                    Label label5 = new Label();
        label5.Content = "Введіть номер залікової книжки";
                    label5.HorizontalAlignment = HorizontalAlignment.Left;
                    label5.VerticalAlignment = VerticalAlignment.Top;
                    label5.Width = 227;
                    label5.Height = 25.96;
                    label5.Margin = new Thickness(307, 104, 0, 0);
        gr.Children.Add(label5);

                    //Buttons
                    Button b1 = new Button();
        b1.Content = "Додати до бази";
                    b1.Name = "b1";
                    b1.HorizontalAlignment = HorizontalAlignment.Left;
                    b1.VerticalAlignment = VerticalAlignment.Top;
                    b1.Width = 114;
                    b1.Height = 39;
                    b1.Margin = new Thickness(81,382,0,0);
        gr.Children.Add(b1);
                    Button b2 = new Button();
        b2.Content = "Видалити з бази";
                    b2.Name = "b2";
                    b2.HorizontalAlignment = HorizontalAlignment.Center;
                    b2.VerticalAlignment = VerticalAlignment.Top;
                    b2.Width = 114;
                    b2.Height = 39;
                    b2.Margin = new Thickness(0, 182, 0, 0);
        gr.Children.Add(b2);
                    Button b3 = new Button();
        b3.Content = "На головне вікно (№1)";
                    b3.Name = "b3";
                    b3.Width = 235;
                    b3.Height = 93;
                    b3.HorizontalAlignment = HorizontalAlignment.Left;
                    b3.VerticalAlignment = VerticalAlignment.Top;
                    b3.Margin = new Thickness(537, 313, 0, 0);
        b3.FontSize = 22;
                    gr.Children.Add(b3);

                    //Textboxes
                    TextBox[] tbmas = new TextBox[5];
                    for (int i=0;i<5;i++)
                    {
                        tbmas[i] = new TextBox();
        tbmas[i].Text ="";
                        tbmas[i].Name = "t" + (i + 1).ToString();
        tbmas[i].Height = 23;
                        tbmas[i].Width = 227;
                        tbmas[i].HorizontalAlignment = HorizontalAlignment.Left;
                        tbmas[i].VerticalAlignment = VerticalAlignment.Top;
                        switch (i)
                        {
                            case 0:
                                tbmas[i].Margin = new Thickness(42,60,0,0);
                                break;
                            case 1:
                                tbmas[i].Margin = new Thickness(42, 145, 0, 0);
                                break;
                            case 2:
                                tbmas[i].Margin = new Thickness(42, 246, 0, 0);
                                break;
                            case 3:
                                tbmas[i].Margin = new Thickness(42, 336, 0, 0);
                                break;
                            case 4:
                                tbmas[i].Margin = new Thickness(307,145,0,0);
                                break;
                        }
    gr.Children.Add(tbmas[i]);
                    }

w.Content = gr;
        }
        private void setting3()
        {
            w.Name = "w3";
            w.Title = "Вікно №3";
            w.Height = 450;
            w.Width = 800;
            w.ResizeMode = System.Windows.ResizeMode.NoResize;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.Background = new SolidColorBrush(Color.FromRgb(203, 192, 192));
            Grid g = new Grid();
            g.Width = 800;
            g.Height = 434.04;
            for (int i=1;i<=5;i++)
                for (int j=1;j<=5;j++)
                {
                    ComboBox cb = new ComboBox();
                    cb.Name = "CB"+((i - 1) * 5 + j).ToString();
                    cb.FontWeight = FontWeights.Bold;
                    ListBoxItem l1 = new ListBoxItem();
                    l1.Content = "X";
                    l1.Foreground = Brushes.Red;
                    l1.FontWeight = FontWeights.Bold;
                    cb.Items.Add(l1);
                    ListBoxItem l2 = new ListBoxItem();
                    l2.Content = "O";
                    l2.Foreground = Brushes.Lime;
                    l2.FontWeight = FontWeights.Bold;
                    cb.Items.Add(l2);
                    cb.SelectedIndex = -1;
                    cb.Width = 47;
                    cb.Height = 32;
                    cb.HorizontalAlignment = HorizontalAlignment.Left;
                    cb.VerticalAlignment = VerticalAlignment.Top;
                    cb.Margin = new Thickness(28+62*(j-1),44+42*(i-1),0,0);
                    g.Children.Add(cb);
                }
            Button newgame = new Button();
            newgame.Name = "newgame";
            newgame.Content = "Нова гра";
            newgame.FontSize = 22;
            newgame.VerticalAlignment = VerticalAlignment.Top;
            newgame.HorizontalAlignment = HorizontalAlignment.Left;
            newgame.Width = 201;
            newgame.Height = 90;
            newgame.Margin = new Thickness(74, 272, 0, 0);
            g.Children.Add(newgame);
            Button exitButton2 = new Button();
            exitButton2.Width = 235;
            exitButton2.Height = 93;
            exitButton2.Content = "На головне вікно (№1)";
            exitButton2.Name = "exit";
            exitButton2.VerticalAlignment = VerticalAlignment.Top;
            exitButton2.HorizontalAlignment = HorizontalAlignment.Left;
            exitButton2.FontSize = 22;
            exitButton2.Margin = new Thickness(542, 315, 0, 0);
            g.Children.Add(exitButton2);
            w.Content = g;
        }
        private void setting4()
        {
            w.Name = "w4";
            w.Title = "Вікно №4";
            w.Height = 450;
            w.Width = 800;
            w.ResizeMode = System.Windows.ResizeMode.NoResize;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Grid g = new Grid();
            Image image = new Image();
            //MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.Length-11));
            image.Source = new BitmapImage(new Uri(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 11) + "//200x200bb.jpg"));
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = image.Source;
            g.Background  = myBrush;
            g.Width = 800;
            g.Height = 434.04;
            Image im = new Image();
            im.Width = 312;
            im.Height = 345;
            im.VerticalAlignment = VerticalAlignment.Top;
            im.HorizontalAlignment = HorizontalAlignment.Left;
            im.Margin = new Thickness(50,48,0,0);
            im.Source = new BitmapImage(new Uri(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 11) + "//кальк.png"));
            im.Name = "im";
            g.Children.Add(im);
            Label label1 = new Label();
            label1.Name = "label1";
            label1.Content = "0";
            label1.FontSize = 40;
            label1.HorizontalContentAlignment = HorizontalAlignment.Right;
            label1.HorizontalAlignment = HorizontalAlignment.Left;
            label1.VerticalAlignment = VerticalAlignment.Top;
            label1.Width = 312;
            label1.Height = 72;
            label1.Margin = new Thickness(50, 69, 0, 0);
            g.Children.Add(label1);
            for (int i=1;i<10;i++)
            {
                Button b = new Button();
                Thickness tmp = new Thickness(77 * ((i - 1) % 3) + im.Margin.Left, (((10 - i) - 1) / 3) * 51 + 190, 0, 0);
                b.Name = "B" + i.ToString();
                b.Margin = tmp;
                b.Width = 77;
                b.Height = 51;
                b.Content = i.ToString();
                b.VerticalAlignment = VerticalAlignment.Top;
                b.HorizontalAlignment = HorizontalAlignment.Left;
                b.Background = Brushes.White;
                g.Children.Add(b);
            }
            Button b0 = new Button();
            b0.Name = "B0";
            b0.Margin = new Thickness(77 + im.Margin.Left, 3 * 51 + 190, 0, 0);
            b0.Width = 77;
            b0.Height = 51;
            b0.Content = "0";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Background = Brushes.White;
            g.Children.Add(b0);
            b0 = new Button();
            b0.Name = "B10";
            b0.Margin = new Thickness(77 * 2 + im.Margin.Left, 3 * 51 + 190, 0, 0);
            b0.Width = 77;
            b0.Height = 51;
            b0.Content = ",";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Background = Brushes.White;
            g.Children.Add(b0);
            b0 = new Button();
            b0.Name = "change_znak";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Content = "+/-";
            b0.Width = 77;
            b0.Height = 52;
            b0.Margin = new Thickness(50,342,0,0);
            b0.Background = Brushes.White;
            g.Children.Add(b0);

            b0 = new Button();
            b0.Name = "eq";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Content = "=";
            b0.Width = 77;
            b0.Height = 51;
            b0.Margin = new Thickness(125, 139, 0, 0);
            b0.Background = new SolidColorBrush(Color.FromRgb(232,178,0));
            g.Children.Add(b0);

            b0 = new Button();
            b0.Name = "clear";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Content = "C";
            b0.Width = 77;
            b0.Height = 51;
            b0.Margin = new Thickness(202, 139, 0, 0);
            b0.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            g.Children.Add(b0);

            b0 = new Button();
            b0.Name = "BackspaceB";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            image = new Image();
            image.Source = new BitmapImage(new Uri(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 11) + "//pic.png"));
            myBrush = new ImageBrush();
            myBrush.ImageSource = image.Source;
            b0.Width = 83;
            b0.Height = 51;
            b0.Margin = new Thickness(279, 139, 0, 0);
            myBrush.TileMode = TileMode.None;
            myBrush.Stretch =Stretch.Fill;
            b0.Background = myBrush;
            g.Children.Add(b0);
            b0 = new Button();

            b0.Name = "div";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            image = new Image();
            image.Source = new BitmapImage(new Uri(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 11) + "//div.png"));
            myBrush = new ImageBrush();
            myBrush.ImageSource = image.Source;
            b0.Width = 83;
            b0.Height = 51;
            b0.Margin = new Thickness(279, 190, 0, 0);
            myBrush.TileMode = TileMode.None;
            myBrush.Stretch = Stretch.Fill;
            b0.Background = myBrush;
            g.Children.Add(b0);
            
            b0 = new Button();
            b0.Name = "mul";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Content = "x";
            b0.FontSize = 16;
            b0.Width = 83;
            b0.Height = 51;
            b0.Margin = new Thickness(279, 240, 0, 0);
            b0.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            g.Children.Add(b0);
            w.Content = g;

            b0 = new Button();
            b0.Name = "minus";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Content = "-";
            b0.FontSize = 20;
            b0.Width = 83;
            b0.Height = 51;
            b0.Margin = new Thickness(279, 291, 0, 0);
            b0.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            g.Children.Add(b0);
            w.Content = g;

            b0 = new Button();
            b0.Name = "plus";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Content = "+";
            b0.FontSize = 20;
            b0.Width = 83;
            b0.Height = 52;
            b0.Margin = new Thickness(279, 342, 0, 0);
            b0.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            g.Children.Add(b0);
            
            b0 = new Button();
            b0.Name = "exit";
            b0.VerticalAlignment = VerticalAlignment.Top;
            b0.HorizontalAlignment = HorizontalAlignment.Left;
            b0.Content = "На головне вікно (№1)";
            b0.FontSize = 22;
            b0.Width = 235;
            b0.Height = 93;
            b0.Margin = new Thickness(542, 315, 0, 0);
            b0.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            g.Children.Add(b0);
            b0 = new Button();
            w.Content = g;
        }
        private void setting5()
        {
            w.Name = "w5";
            w.Title = "Вікно №5";
            w.Height = 450;
            w.Width = 826;
            w.ResizeMode = System.Windows.ResizeMode.NoResize;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.Background = Brushes.AliceBlue;
            Grid g = new Grid();
            Label lb = new Label();
            lb.Name = "lb";
            lb.Width = 780;
            lb.Height = 259;
            lb.FontSize = 20;
            lb.VerticalAlignment = VerticalAlignment.Top;
            lb.HorizontalAlignment = HorizontalAlignment.Left;
            lb.Margin = new Thickness(0, 23, 0, 0);
            g.Children.Add(lb);
            Button b = new Button();
            b.Name = "exit";
            b.VerticalAlignment = VerticalAlignment.Top;
            b.HorizontalAlignment = HorizontalAlignment.Left;
            b.Content = "На головне вікно (№1)";
            b.FontSize = 22;
            b.Width = 235;
            b.Height = 93;
            b.Margin = new Thickness(542, 315, 0, 0);
            b.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            g.Children.Add(b);
            w.Content = g;
        }
        public Interface(int num)
        {
            w = new Window();
            
            switch (num)
            {
                case 2:
                    setting2();
                    break;
                case 3:
                    setting3();
                    break;
                case 4:
                    setting4();
                    break;
                case 5:
                    setting5();
                    break;
            }
        }
        public Window Get_Window()
        {
            return w;
        }
    }
}
