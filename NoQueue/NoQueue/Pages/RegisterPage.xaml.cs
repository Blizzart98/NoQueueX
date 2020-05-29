using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Collections.Generic;
using System.Linq;
using Java.Util;
using NoQueue.Interfaces;

namespace NoQueue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        InterfaceAuth auth;
        InterfaceDB db;
        

        public RegisterPage()
        {
            InitializeComponent();       
            db = DependencyService.Get<InterfaceDB>();
            auth = DependencyService.Get<InterfaceAuth>();
     
        }

        private async void Btn_registration_clicked(object sender, EventArgs e)
        {
            if (Entry_name.Text == null || Entry_email.Text == null || Entry_Password.Text == null || Entry_surname.Text == null)
            {
                await DisplayAlert("Errore", "Inserisci tutti i campi", "OK");
                return;
            }

            bool created = await auth.RegisterUser(Entry_email.Text, Entry_Password.Text);
            bool added =  await db.InserisciUtente(Entry_email.Text, Entry_Password.Text, Entry_name.Text, Entry_surname.Text);
            if (created && added)
            {
                await DisplayAlert("Success", "Welcome to our system. Log in to have full access", "OK");
                await Navigation.PushAsync(new ProfilePage());
                Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert("Sign Up Failed", "Something went wrong. Try again!", "OK");
            }
        }

        void Btn_signin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}