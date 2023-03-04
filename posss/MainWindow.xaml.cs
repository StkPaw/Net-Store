using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace posss
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }
        //with that we can move our window around
        private void Ruszamy(object sender,MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        //Click event for "X" button to exit program
        private void Wyjscie_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //Click event for "Pokaz Baze" button 
        private void Pokaz_Click(object sender, RoutedEventArgs e)
        {
            Pokazz();
        }
        //Click event for "Historia Transakcji" button 
        private void HisTran_Click(object sender,RoutedEventArgs e)
        {
            Historia();
        }
        //Click event for "Sprzedaz" button 
        private void Sprzedaj_Click(object sender, RoutedEventArgs e)
        {
            Dostaw('-');
        }

        //Click event for "Dostawa" button
        private void Dostawa_Click(object sender, RoutedEventArgs e)
        {
            Dostaw('+');
        }

        //Method that shows all past transactions 
        private void Historia()
        {
            Głowa.Children.Clear();
            Pokaz.IsEnabled = true;
            Dodaj.IsEnabled = true;
            HisTran.IsEnabled = false;
            Dostawa.IsEnabled = true;

            //We are creating a menu that shows past transactions 1 scroll viewer for transactions second for items in transaction user want to see
            AddEl a = new AddEl();
            ScrollViewer rolka = new ScrollViewer();
            rolka.Height = 530;
            rolka.Width = 350;
            ScrollViewer rolka1 = new ScrollViewer();
            rolka1.Height = 530;
            rolka1.Width = 550;

            a.AStPa(Głowa, Orientation.Vertical, false);
            a.AStPa(Głowa, Orientation.Horizontal);
            a.AStPa(Głowa, Orientation.Horizontal);
            a.AStPa(Głowa, Orientation.Vertical, false);
            rolka.Content = a.b[0];
            rolka1.Content = a.b[3];

            a.ATxBl(a.b[1], "Data", 20, 50, new Thickness(20, 10, 0, 0));
            a.ATxBl(a.b[1], "Typ", 20, 50, new Thickness(100, 10, 0, 0));
            a.ATxBl(a.b[1], "Id", 20, 30, new Thickness(155, 10, 0, 0));
            a.ATxBl(a.b[1], "Nazwa", 20, 50, new Thickness(20, 10, 0, 0));
            a.ATxBl(a.b[1], "Kod Kreskowy", 20, 150, new Thickness(100, 10, 0, 0));
            a.ATxBl(a.b[1], "Cena", 20, 50, new Thickness(0, 10, 0, 0));
            a.ATxBl(a.b[1], "Zmiana", 20, 60, new Thickness(28, 10, 0, 0));
            a.b[2].Children.Add(rolka);
            a.b[2].Children.Add(rolka1);


            // Here we connect to database and create textboxes holding database records when textbox is doubleclicked the second scroll viwer will show products
            var db = new Model();
            var transakcje = db.Transakcje.ToList();
            int x = 0;
            int y = 0;
            AddEl InLoop = new AddEl();
            while (transakcje.Count > x)
            {
                string hr = "";
                string min = "";
                string day = "";
                string mon = "";
                if (transakcje[x].Date.Month < 10)
                { mon = "0"; }
                if (transakcje[x].Date.Day < 10)
                { day = "0"; }
                if (transakcje[x].Date.Hour<10)
                { hr = "0"; }
                if (transakcje[x].Date.Minute < 10)
                { min = "0"; }
                int param = x;
                InLoop.AStPa(a.b[0], Orientation.Horizontal);
                a.ATxBo(InLoop.b[x], 25, 150, new Thickness(20, 10, 0, 0), true, Convert.ToString(transakcje[x].Date.Year)+" "+mon+ Convert.ToString(transakcje[x].Date.Month+" "+day+ Convert.ToString(transakcje[x].Date.Day)+" "+hr+ Convert.ToString(transakcje[x].Date.Hour)+":")+min+ Convert.ToString(transakcje[x].Date.Minute));
                a.ATxBo(InLoop.b[x], 25, 80, new Thickness(0, 10, 0, 0), true, transakcje[x].typ);
                a.c[y].MouseDoubleClick += new MouseButtonEventHandler((sender, e) => { Hist(sender, e, param, a.b[3]); });
                a.c[y+1].MouseDoubleClick += new MouseButtonEventHandler((sender, e) => { Hist(sender, e, param, a.b[3]); });
                y = y + 2;
                x++;
            }
        }

        //Double click event that shows produkts from given transaction
        private void Hist(object sender, RoutedEventArgs e,int s,StackPanel hor)
        {
            hor.Children.Clear();
            AddEl a = new AddEl();
            var db = new Model();
            var transakcja = db.Transakcje
                                .Include(b=>b.produkts)
                                .Single(b => b.TransakcjaId == s+1);


            int i = 0;
            foreach(Produkt ab in transakcja.produkts)
            {
                a.AStPa(hor, Orientation.Horizontal);
                a.ATxBo(a.b[i], 25, 50, new Thickness(20, 10, 0, 0), true, Convert.ToString(ab.ProduktId));
                a.ATxBo(a.b[i], 25, 150, new Thickness(0, 10, 0, 0), true, ab.Nazwa);
                a.ATxBo(a.b[i], 25, 150, new Thickness(0, 10, 0, 0), true, ab.KodKreskowy);
                a.ATxBo(a.b[i], 25, 80, new Thickness(0, 10, 0, 0), true, Convert.ToString(ab.Cena));

                string ilos = "";
                if(transakcja.typ== "sprzedaz")
                { ilos = "-"; }
                if (transakcja.typ == "dostawa")
                { ilos = "+"; }
                int kropka = 0;
                for(int j = 0; j < transakcja.ilosc.Length-1; j++)
                {
                    if (kropka < i)
                    {
                        if(transakcja.ilosc[j] == '.')
                        { kropka++;j++; }
                    }
                    if (kropka==i)
                    {
                        if (transakcja.ilosc[j] == '.')
                        { kropka++; j = transakcja.ilosc.Length; }
                        else
                        { ilos = ilos + transakcja.ilosc[j]; }

                    
                    }
                    
                }

                a.ATxBo(a.b[i], 25, 60, new Thickness(0, 10, 0, 0), true, ilos);
                i++;
            }

        }

        //Method for both adding and deacreasing amount of products "sellling"-> argument '-' and "supplying" -> '+' 
        private void Dostaw(char znak)
        {
            //turning off and on butons depending on argument
            if (znak == '+')
            {
                Głowa.Children.Clear();
                Pokaz.IsEnabled = true;
                Dodaj.IsEnabled = true;
                HisTran.IsEnabled = true;
                Dostawa.IsEnabled = false;
            }
            if(znak == '-')
            {
                Głowa.Children.Clear();
                Pokaz.IsEnabled = true;
                Dostawa.IsEnabled = true;
                HisTran.IsEnabled = true;
                Dodaj.IsEnabled = false;
            }

            //building a menu for user
            AddEl a = new AddEl();
            ScrollViewer rolka = new ScrollViewer();
            rolka.Height = 350;
            string nazwa="";
            if(znak=='-')
            {  nazwa = "Zakończ Trasakcję"; }
            if (znak == '+')
            {  nazwa = "Zapisz Dane"; }

            //List with id's of products that user entered 
            List<int> ids = new List<int>();

            //still building a menu for user
            a.AStPa(Głowa, Orientation.Vertical,false);
            rolka.Content = a.b[0];
            Głowa.Children.Add(rolka);
            a.AStPa(Głowa, Orientation.Vertical);
            a.AStPa(a.b[1], Orientation.Horizontal);
            a.ATxBl(a.b[2], "Wyszukaj", 20, 100, new Thickness(20, 10, 0, 0));
            a.AStPa(a.b[1], Orientation.Horizontal);
            a.ATxBo(a.b[3], 25, 150, new Thickness(20, 10, 0, 0), false);

            //thats a button that after user entered few products and it s amount updates database
            a.ABt(a.b[3], nazwa, 60, 200, new Thickness(300, 10, 0, 0), (sender, EventArgs) => { DodUj(sender, EventArgs, znak,ids,a.b[0]); });

            a.c[0].Focus();

            //thats a textbox for user to enter "kodkreskowy" and confirm with key Enter
            a.c[0].KeyDown +=new KeyEventHandler((sender, EventArgs) => { Dostaw_Enter(sender, EventArgs, ids,a.b[0]); });
        }

        //Event for button in "Sprzedaż" and "Dostawa" menu that updates database 
        private void DodUj(object sender, RoutedEventArgs e,char znak, List<int> ids, StackPanel panel)
        {
            var db = new Model();
            
            string ilosc = "";

            //here we create record in db for transaction
            string typp = "";
            if (znak == '-')
            { typp = "sprzedaz"; }
            if (znak == '+')
            { typp = "dostawa"; }
            int szuk = 1;
            var transakcje = db.Transakcje.ToList();
            if (transakcje.Count != 0)
            {
                szuk = transakcje[transakcje.Count - 1].TransakcjaId + 1;
            }
            Transakcja a = new Transakcja { TransakcjaId = szuk, Date = DateTime.Now, typ = typp, ilosc = ilosc };
            ICollection<Produkt>coll =new List<Produkt>();
            

            for (int i=0;i<ids.Count;i++)
            {
                //here we need to get access to textbox with amount that user entered
                StackPanel hor = (StackPanel)panel.Children[i];
                TextBox hor1 = (TextBox)hor.Children[4];
                

                //checking if user input is correct TO DO return info to user
                if (hor1.Text=="")
                { return; }
                try
                { Convert.ToInt64(hor1.Text); }
                catch (FormatException)
                { return; }

                

                //searching for product
                var produkty = db.Produkty
                    .Single(b => b.ProduktId == ids[i]);

                //adding product to transaction

                coll.Add(produkty);
                
                ilosc = ilosc + hor1.Text + ".";
                a.ilosc = ilosc;
                //updating products amount
                if (znak == '+')
                { produkty.Mag = produkty.Mag + Convert.ToInt32(hor1.Text); }
                if (znak == '-')
                { produkty.Mag = produkty.Mag - Convert.ToInt32(hor1.Text);}
                db.SaveChanges();

            }
            a.produkts = coll;
            db.Transakcje.Add(a);
            db.SaveChanges();
            panel.Children.Clear();
            ids.Clear();
        }

        //here after user entered "kodkreskowy" we need to show him the product he want to add or sell
        private void Dostaw_Enter(object sender, KeyEventArgs e, List<int> ids, StackPanel panel)
        {
            if(e.Key==Key.Enter)
            {
                AddEl a= new AddEl();
                TextBox tb=(TextBox)sender;
                var db = new Model();

                //checking if user input is correct TO DO return info to user
                try
                {
                    var test = db.Produkty
                        .Single(b => b.KodKreskowy == tb.Text);
                }
                catch(InvalidOperationException)
                {
                    return;
                }
                var produkty = db.Produkty
                    .Single(b => b.KodKreskowy == tb.Text);


                if (ids.Contains(produkty.ProduktId))
                {
                    StackPanel hor=(StackPanel)panel.Children[ids.IndexOf(produkty.ProduktId)];
                    TextBox hor1 = (TextBox)hor.Children[4];
                    hor1.Text = Convert.ToString(Convert.ToInt64(hor1.Text)+1);
                }
                else
                {
                    ids.Add(produkty.ProduktId);
                    a.AStPa(panel, Orientation.Horizontal);
                    a.ATxBo(a.b[0], 25, 50, new Thickness(20, 10, 0, 0), true, Convert.ToString(produkty.ProduktId));
                    a.ATxBo(a.b[0], 25, 150, new Thickness(0, 10, 0, 0), true, produkty.Nazwa);
                    a.ATxBo(a.b[0], 25, 150, new Thickness(0, 10, 0, 0), true, produkty.KodKreskowy);
                    a.ATxBo(a.b[0], 25, 80, new Thickness(0, 10, 0, 0), true, Convert.ToString(produkty.Cena));
                    a.ATxBo(a.b[0], 25, 60, new Thickness(0, 10, 0, 0), false, "1");
                }
            }
        }
        
        //method that shows  all products in database and a menu for adding new products
        private void Pokazz()
        {
            //We create stack panels and textblocks for database records
            Głowa.Children.Clear();
            Pokaz.IsEnabled = false;
            HisTran.IsEnabled = true;
            Dodaj.IsEnabled = true;
            Dostawa.IsEnabled = true;
            AddEl a = new AddEl();
            ScrollViewer rolka = new ScrollViewer();
            rolka.Height = 350;

            a.AStPa(Głowa, Orientation.Vertical, false);
            a.AStPa(Głowa, Orientation.Horizontal);
            rolka.Content = a.b[0];
            Głowa.Children.Add(rolka);
            a.ATxBl(a.b[1], "Id", 20, 30, new Thickness(20, 10, 0, 0));
            a.ATxBl(a.b[1], "Nazwa", 20, 50, new Thickness(20, 10, 0, 0));
            a.ATxBl(a.b[1], "Kod Kreskowy", 20, 150, new Thickness(100, 10, 0, 0));
            a.ATxBl(a.b[1], "Cena", 20, 50, new Thickness(0, 10, 0, 0));
            a.ATxBl(a.b[1], "Ilość" ,20, 50, new Thickness(28, 10, 0, 0));

            // Here we connect to database and create textboxes holding database records
            var db = new Model();
            var produkty = db.Produkty.ToList();
            int x = 0;
            AddEl InLoop = new AddEl();
            while (produkty.Count > x)
            {
                int param = x;
                InLoop.AStPa(a.b[0], Orientation.Horizontal);
                a.ATxBo(InLoop.b[x], 25, 50, new Thickness(20, 10, 0, 0), true, Convert.ToString(produkty[x].ProduktId));
                a.ATxBo(InLoop.b[x], 25, 150, new Thickness(0, 10, 0, 0), true, produkty[x].Nazwa);
                a.ATxBo(InLoop.b[x], 25, 150, new Thickness(0, 10, 0, 0), true, produkty[x].KodKreskowy);
                a.ATxBo(InLoop.b[x], 25, 80, new Thickness(0, 10, 0, 0), true, Convert.ToString(produkty[x].Cena));
                a.ATxBo(InLoop.b[x], 25, 60, new Thickness(0, 10, 0, 0), true, Convert.ToString(produkty[x].Mag));
                a.ABt(InLoop.b[x], "Usun", 30, 50, new Thickness(0, 10, 0, 0), (sender, EventArgs) => { Usun_Click(sender, EventArgs, param); });
                x++;
            }

            //Here we create menu where user can add new product to database
            a.AStPa(Głowa, Orientation.Horizontal);
            a.ATxBl(a.b[2], "Dodaj produkt", 20, 150, new Thickness(20, 10, 0, 0));
            a.AStPa(Głowa, Orientation.Horizontal);
            a.ATxBl(a.b[3], "Nazwa", 20, 50, new Thickness(25, 10, 0, 0));
            a.ATxBl(a.b[3], "Kod Kreskowy", 20, 150, new Thickness(100, 10, 0, 0));
            a.ATxBl(a.b[3], "Cena", 20, 50, new Thickness(0, 10, 0, 0));
            a.AStPa(Głowa, Orientation.Horizontal);
            a.ATxBo(a.b[4], 25, 150, new Thickness(20, 10, 0, 0), false);
            a.ATxBo(a.b[4], 25, 150, new Thickness(0, 10, 0, 0), false);
            a.ATxBo(a.b[4], 25, 80, new Thickness(0, 10, 0, 0), false);
            a.AStPa(Głowa, Orientation.Horizontal);
            a.ABt(a.b[5], "Zapisz", 30, 150, new Thickness(20, 10, 0, 0), Zapisz_Click);
        }

        //Click event for "Zapisz" button that creates new record in database
        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            
            StackPanel a = (StackPanel)Głowa.Children[4];
            var db = new Model();
            var produkty = db.Produkty.ToList();

            //checking if user input is correct TO DO return info to user
            if (((TextBox)a.Children[0]).Text==""|| ((TextBox)a.Children[1]).Text == ""|| ((TextBox)a.Children[2]).Text == "")
            {
                return;
            }
            try
            { Convert.ToDouble(((TextBox)a.Children[2]).Text); }
            catch(FormatException)
            { return; }


            //Here we are looking for free Id number for new record
            int szuk = 1;
            if (produkty.Count != 0)
            {
                szuk = produkty[produkty.Count - 1].ProduktId + 1;
                for (int i = 0; i < produkty.Count; i++)
                {
                    if (i + 1 != produkty[i].ProduktId)
                    {
                        szuk = i + 1;
                        i = produkty.Count;
                    }
                }
            }
            Produkt test = new Produkt { ProduktId = szuk, Nazwa = ((TextBox)a.Children[0]).Text, KodKreskowy = ((TextBox)a.Children[1]).Text, Cena = Convert.ToDouble(((TextBox)a.Children[2]).Text), Mag = 0 };
            db.Produkty.Add(test);
            db.SaveChanges();
            Pokazz();
        }
        //Click event for "Usun" buttons that is deleting record from database
        private void Usun_Click(object sender, RoutedEventArgs e,int a)
        {
            var db = new Model();
            var produkty = db.Produkty.ToList();
            db.Produkty.Remove(produkty[a]);
            db.SaveChanges();
            Pokazz();
        }
    }
}
