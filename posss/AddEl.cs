using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;


namespace posss
{
    //Class with methods to add various elements to form like textblocks
    class AddEl
    {
        //Lists with element created so we can manipulate them
       public List<TextBlock> a =new List<TextBlock>();
       public List<StackPanel> b = new List<StackPanel>();
       public List<TextBox> c = new List<TextBox>();
       public List<Border> d = new List<Border>();

        //Method that adds textblock to form
        public void ATxBl(StackPanel c, string d,int height,int width,Thickness m,bool add=true)
       {
            TextBlock b = new TextBlock();
            b.Text = d;
            b.Height = height;
            b.Width = width;
            b.FontFamily = new FontFamily("Consolas");
            b.FontSize = 16;
            b.Foreground = Brushes.White;
            b.Margin = m;
            if (add == true)
            { c.Children.Add(b); }
            a.Add(b);
       }

        //Method that adds stackpanel to form
       public void AStPa(StackPanel c,Orientation o,bool add=true)
       {
            StackPanel a=new StackPanel();
            a.Orientation = o;
            if(add==true)
            { c.Children.Add(a); }
            b.Add(a);
       }

        //Method that adds textblox to form
        public void ATxBo(StackPanel c,int height , int width , Thickness margin,bool read, string d = "")
        {
            TextBox a = new TextBox();
            a.Text = d;
            a.Height = height;
            a.Width = width;
            a.FontFamily = new FontFamily("Consolas");
            a.FontSize = 16;
            BrushConverter brushConverter = new BrushConverter();
            a.Background = (Brush)brushConverter.ConvertFrom("#272537");
            a.Foreground = Brushes.White;
            a.IsReadOnly = read;
            a.Margin = margin;
            c.Children.Add(a);
            this.c.Add(a);
        }

        //Method that adds button to form
        public void ABt(StackPanel c,string text,int height,int width,Thickness margin,RoutedEventHandler d)
        {
            Border a = new Border();
            a.CornerRadius = new CornerRadius(10);
            BrushConverter brushConverter = new BrushConverter();
            a.Background = (Brush)brushConverter.ConvertFrom("#272537");
            a.Width = width;
            a.Height = height;
            Button b = new Button();
            b.Content = text;
            b.Foreground = Brushes.White;
            b.Background= (Brush)brushConverter.ConvertFrom("#FF1A1925");
            b.BorderBrush = null;
            b.Width = width-5;
            b.Height = height-5;
            b.Click += d;
            a.Margin = margin;
            a.Child = b;
            c.Children.Add(a);
            this.d.Add(a);
        }
    }
}
