using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoQueue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        InterfaceAuth auth;
        public ProfilePage()
        {
            InitializeComponent();
            auth = DependencyService.Get<InterfaceAuth>();

        }

        public async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            bool result = await auth.LogOut();
            if (result)
            {

                Navigation.PushAsync(new MainPage());
                Navigation.RemovePage(this);
            }
            else
            {
                ShowError();
            }
        }



        async private void ShowError()
        {
            await DisplayAlert("Logout Failed", "Lol", "OK");
        }
    }
}