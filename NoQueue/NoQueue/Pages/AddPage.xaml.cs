using Android.Icu.Util;
using NoQueue.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public string market;
        public AddPage()
        {
            InitializeComponent();
        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            city= CityPicker.Items[CityPicker.SelectedIndex];
            PopolaMarketPicker(city);

        }

        private void DatePickerDateSelected(object sender, EventArgs e) 
        {
           date = DatePicker.Date.ToString();
        }

        private void MarketSelectedIndexChanged(object sender, EventArgs e)
        {
            market = MarketPicker.Items[MarketPicker.SelectedIndex];
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

        
    }
}