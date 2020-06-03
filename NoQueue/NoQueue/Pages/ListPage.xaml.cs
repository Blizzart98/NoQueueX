using NoQueue.Entities;
using NoQueue.Helpers;
using Plugin.CloudFirestore;
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

        readonly INterfaceAuth auth;
        public ListPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<INterfaceAuth>();
            BindingContext = new ListViewViewModel();
            
        }

        public async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            bool logout = await DisplayAlert("LOGOUT", "Vuoi davvero fare il logout?", "OK", "ANNULLA");

            if (logout)
            {
                bool result = await auth.LogOut();
                if (result)
                {

                    await Navigation.PushAsync(new MainPage());
                    Navigation.RemovePage(this);
                }
                else
                {
                    ShowError();
                }
            }
           
        }
        public void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPage());
            Navigation.RemovePage(this);

        }
        async private void ShowError()
        {
            await DisplayAlert("Logout Failed", "Lol", "OK");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var mItem = sender as MenuItem;
            var info = (Prenotazione)mItem.CommandParameter;
            
            await CrossCloudFirestore.Current
                          .Instance
                          .GetCollection("utenti")
                          .GetDocument(auth.GetEmail())
                          .GetCollection("Prenotazioni")
                          .GetDocument(info.negozio + info.data)
                          .DeleteDocumentAsync();

            await CrossCloudFirestore.Current
                          .Instance
                          .GetCollection("Supermercati")
                          .GetDocument(info.negozio)
                          .GetCollection("Prenotazioni")
                          .GetDocument(auth.GetEmail() + info.data)
                          .DeleteDocumentAsync();

            await Navigation.PushAsync(new ListPage()); //così la pagina si aggiorna
            Navigation.RemovePage(this);
        }
    }
}
