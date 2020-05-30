using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
        public AddPage()
        {
            InitializeComponent();
        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            city= CityPicker.Items[CityPicker.SelectedIndex];
        }

        private void DatePickerDateSelected(object sender, EventArgs e) 
        {
           date = DatePicker.Date.ToString();
        }

    }
}