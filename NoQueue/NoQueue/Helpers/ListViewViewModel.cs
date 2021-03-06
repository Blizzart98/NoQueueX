﻿using Android.Media;
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
        readonly INterfaceAuth auth = DependencyService.Get<INterfaceAuth>();

        public ObservableCollection<Prenotazione> PersonList { get; set; } = new ObservableCollection<Prenotazione>();
        public ListViewViewModel()
        {
            Start();
            /**/
        }

        public async void Start()
        {
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
            string email = auth.GetEmail();
            var group = await CrossCloudFirestore.Current
                                                .Instance
                                                .GetCollection("utenti")
                                                .GetDocument(email)
                                                .GetCollection("Prenotazioni")
                                                .GetDocumentsAsync();

            var yourModels = group.ToObjects<Prenotazione>();
            return yourModels;
        }
    }
   
}
