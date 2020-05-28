﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoQueue
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible (false)]
    public partial class MainPage : ContentPage
    {
        InterfaceAuth auth;
        public MainPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<InterfaceAuth>();
        }

        async  void Btn_signin_Clicked(object sender, EventArgs e)
        {
            bool result = await auth.AuthenticateUser(Entry_email.Text, Entry_Password.Text);
            if (result)
            {
                await Navigation.PushAsync(new ProfilePage());
                Navigation.RemovePage(this);
            }
            else
            {
                ShowError();
            }

        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

         void Btn_registration_Clicked(object sender, EventArgs e)
        { 
            Navigation.PushAsync(new RegisterPage());
            Navigation.RemovePage(this);
        }
    }
}
