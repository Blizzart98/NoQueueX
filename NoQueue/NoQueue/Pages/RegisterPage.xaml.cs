﻿using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Collections.Generic;
using System.Linq;
using Plugin.CloudFirestore;
using NoQueue.Entities;
using NoQueue.Pages;

namespace NoQueue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        InterfaceAuth auth;
        

        public RegisterPage()
        {
            InitializeComponent();       
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
            Utente user = new Utente(Entry_name.Text, Entry_surname.Text, Entry_email.Text, Entry_Password.Text);
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("utenti")
                         .GetDocument(Entry_email.Text)
                         .GetCollection("Info")
                         .CreateDocument()
                         .SetDataAsync(user);
            if (created)
            {
                await DisplayAlert("Success", "Welcome to our system. Log in to have full access", "OK");
                await Navigation.PushAsync(new ListPage());
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