using NoQueue.Entities;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoQueue.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public string city;

        public string date;
        static public int month;
        static public int day;
        static public int year;

        public string market;

        static public string minute;
        static public int iminute;
        static public string hour;
        static public int ihour;

        private long ts;
        DateTime dt;
        readonly INterfaceAuth auth;
        public AddPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<INterfaceAuth>();
        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            city= CityPicker.Items[CityPicker.SelectedIndex]; 
            PopolaMarketPicker(city);
            MarketPicker.IsEnabled = true;
            Btn_Prenota.IsEnabled = false;

        }
        private void MarketSelectedIndexChanged(object sender, EventArgs e)
        {
            market = MarketPicker.Items[MarketPicker.SelectedIndex];
            DatePicker.IsEnabled = true;
            Btn_Prenota.IsEnabled = false;
            HourPicker.IsEnabled = true;
        }

        private void DatePickerDateSelected(object sender, EventArgs e) 
        {
            year = DatePicker.Date.Year;
            month = DatePicker.Date.Month;
            day = DatePicker.Date.Day;
            date = DatePicker.Date.ToLongDateString();
            Btn_Prenota.IsEnabled = false;
        }

        private void HourSelectedIndexChanged(object sender, EventArgs e)
        {
            hour = HourPicker.Items[HourPicker.SelectedIndex];
            MinutePicker.IsEnabled = true;
           ihour = Convert.ToInt32(hour);
            Btn_Prenota.IsEnabled = false;
        }

        private void MinuteSelectedIndexChanged(object sender, EventArgs e)
        {
            minute = MinutePicker.Items[MinutePicker.SelectedIndex];
            Btn_Check.IsEnabled = true;
            iminute = Convert.ToInt32(minute);
            Btn_Prenota.IsEnabled = false;
        }

        private async void Btn_Check_Clicked(object sender, EventArgs e)
        {
            var email = auth.GetEmail();
            int[] count = { 0 };
            dt = new DateTime(year, month, day, ihour, iminute, 0, 0);
            ts = MillisecondsTimestamp(dt);

            var doc = await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("utenti")
                         .GetDocument(email)
                         .GetCollection("Prenotazioni")
                         .GetDocument(market + date)
                         .GetDocumentAsync();

            if (doc.Exists)
            {
                await DisplayAlert("Errore", "Hai già effettuato una prenotazione per questa data in questo supermercato." +
                    "\n Lascia spazio per tutti", "OK");
                Btn_Prenota.IsEnabled = false;
                return;
            }

            var group = await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Supermercati")
                         .GetDocument(market)
                         .GetCollection("Prenotazioni")
                         .WhereEqualsTo("ts", ts)
                         .GetDocumentsAsync();

            for (int i = 0; i < group.Count; i++)
            { //per ogni documento della lista incremento contatore di uno
                count[0] += 1;//se trovo aumento di 1
            }

            if (count[0] <= 5)
            {
                lblCheck.TextColor= Color.Green;
                lblCheck.Text = $"Libero: {count[0]} persone.";
            }
            else if (count[0] <= 10)
            {
                lblCheck.TextColor = Color.Yellow;
                lblCheck.Text =$"Leggermente affollato: {count[0]} persone.";
            }
            else
            {
                lblCheck.TextColor = Color.Red;
                lblCheck.Text = $"Molto affollato: {count[0]} persone.";
            }

            Btn_Prenota.IsEnabled= true;
        }
        private async void Btn_Prenota_Clicked(object sender, EventArgs e)
        {
            
            var email = auth.GetEmail();

            Prenotazione prenotazione = new Prenotazione(city, market, ihour, iminute, date, ts);
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("utenti")
                         .GetDocument(email)
                         .GetCollection("Prenotazioni")
                         .GetDocument(market + date)
                         .SetDataAsync(prenotazione);

            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Supermercati")
                         .GetDocument(market)
                         .GetCollection("Prenotazioni")
                         .GetDocument(email + date)
                         .SetDataAsync(prenotazione);

            await Navigation.PushAsync(new ListPage());
            Navigation.RemovePage(this);


        }
        private void PopolaMarketPicker(string city)
        {

            if (city == "Camerano")
            {
                MarketPicker.Items.Clear();
                MarketPicker.Items.Add("Si con te - Via Fazioli");
                MarketPicker.Items.Add("Si con te - Via Loretana");
                MarketPicker.Items.Add("Supermercato Coal - San Germano");
                MarketPicker.Items.Add("Ipermercato Carrefour");

            }
            else if (city == "Cupramontana")
            {
                MarketPicker.Items.Clear();
                MarketPicker.Items.Add("Si con te - Cavalletti Michele");
                MarketPicker.Items.Add("Supermercato Coal - Via C. Battisti");
                MarketPicker.Items.Add("Alimentari Mauro");
            }
            else
            {
                DisplayAlert("Errore", "Seleziona città", "OK");
            }
        }

        public static long MillisecondsTimestamp( DateTime d)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(d.ToUniversalTime() - baseDate).TotalMilliseconds / 1000;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListPage());
            Navigation.RemovePage(this);
        }
    }
}