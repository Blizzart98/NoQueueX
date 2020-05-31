using Android.Media;
using NoQueue.Entities;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Android.App.DownloadManager;

namespace NoQueue.Helpers
{
    public class ListViewViewModel
    {
        InterfaceAuth auth = DependencyService.Get<InterfaceAuth>();

        public ObservableCollection<Prenotazione> PersonList { get; set; } = new ObservableCollection<Prenotazione>();
        public ListViewViewModel()
        {
            Start();
            /**/
        }

        public async void Start()
        {
            /*PersonList = new ObservableCollection<Prenotazione>
            {
                new Prenotazione(){ citta = "John", negozio = "Los Angeles"},
                new Prenotazione(){ citta = "John", negozio = "Los Angeles"},
                new Prenotazione(){ citta = "John", negozio = "Los Angeles"},
                new Prenotazione(){ citta = "John", negozio = "Los Angeles"},
            };*/

            IEnumerable<Prenotazione> list = await GetUserPostsList();//lancia funzione sotto e salva in list
           
            foreach (Prenotazione a in list)//aggiunge le prenotazioni alla observable collection, colection che verrà poi visualizzata
            {
                PersonList.Add(
                    new Prenotazione() { citta = a.GetCitta(), negozio = a.GetNegozio(), ora= a.GetOra(), minuti = a.GetMinuti(), data = a.GetData() }
                    );
            }
        }


        private async Task<IEnumerable<Prenotazione>> GetUserPostsList() //prende tutte le pren da firebase e le mette in un IEnumerable
        {

            var group = await CrossCloudFirestore.Current.
                Instance.
                GetCollection("utenti/" + auth.GetEmail() + "/Prenotazioni").
                GetDocumentsAsync();

            var yourModels = group.ToObjects<Prenotazione>();
            return yourModels;

        }
    }
   
}
