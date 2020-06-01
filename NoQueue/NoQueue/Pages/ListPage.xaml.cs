using NoQueue.Entities;
using NoQueue.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoQueue.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        InterfaceAuth auth;
        public ListPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<InterfaceAuth>();
            BindingContext = new ListViewViewModel();
            
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
        public void ToolbarItem_ClickedAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPage());
        }
        async private void ShowError()
        {
            await DisplayAlert("Logout Failed", "Lol", "OK");
        }

    }
}
