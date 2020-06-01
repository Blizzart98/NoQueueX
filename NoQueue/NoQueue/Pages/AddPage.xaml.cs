using NoQueue.Entities;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        int timeCheck = 0;

        private long ts;
        DateTime dt;
        InterfaceAuth auth;
        public AddPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<InterfaceAuth>();
            DatePicker.SetValue(DatePicker.MinimumDateProperty, DateTime.Now);
        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            city= CityPicker.Items[CityPicker.SelectedIndex];
            PopolaMarketPicker(city);
            MarketPicker.IsEnabled = true;
        }
        private async void MarketSelectedIndexChanged(object sender, EventArgs e)
        {
            market = MarketPicker.Items[MarketPicker.SelectedIndex];
            DatePicker.IsEnabled = true;
        }

        private void DatePickerDateSelected(object sender, EventArgs e) 
        {
            year = DatePicker.Date.Year;
            month = DatePicker.Date.Month;
            day = DatePicker.Date.Day;
            date = DatePicker.Date.ToString().Substring(0, 8);
            HourPicker.IsEnabled = true;
        }

        private void HourSelectedIndexChanged(object sender, EventArgs e)
        {
            hour = HourPicker.Items[HourPicker.SelectedIndex];
            MinutePicker.IsEnabled = true;
           ihour = Convert.ToInt32(hour);
        }

        private void MinuteSelectedIndexChanged(object sender, EventArgs e)
        {
            minute = MinutePicker.Items[MinutePicker.SelectedIndex];
            timeCheck++;
            //if (timeCheck > 9)
                Btn_Check.IsEnabled = true;
            iminute = Convert.ToInt32(minute);
        }

        private async void Btn_Check_Clicked(object sender, EventArgs e)
        {
             int[] count = { 0 };

            
            

            dt = new DateTime(year, month, day, ihour, iminute, 0, 0);

            ts = MillisecondsTimestamp(dt);

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
                         .CreateDocument()
                         .SetDataAsync(prenotazione);
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Supermercati")
                         .GetDocument(market)
                         .GetCollection("Prenotazioni")
                         .CreateDocument()
                         .SetDataAsync(prenotazione);

            Navigation.PushAsync(new ListPage());
            Navigation.RemovePage(this);


        }
        private void PopolaMarketPicker(string city)
        {
           /* var picker = new Picker();
            var marketsCupra = new List<string>
            {
                "Si con te - Cavalletti Michele",
                "Supermercato Coal - Via C.Battisti",
                "Alimentari Mauro"
            };


            var marketsCamerano = new List<string> 
            {   "Si con te - Via Fazioli",
                "Si con te - Via Loretana",
                "Supermercato Coal - San Germano",
                "Ipermercato Carrefour"
            };*/


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
                MarketPicker.Items.Add("Supermercato Coal - Via C.Battisti");
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
    }
}