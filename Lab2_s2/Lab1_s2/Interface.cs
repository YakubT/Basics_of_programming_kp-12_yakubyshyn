﻿using System;
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
            }
        }
        public Window Get_Window()
        {
            return w;
        }
    }
}
