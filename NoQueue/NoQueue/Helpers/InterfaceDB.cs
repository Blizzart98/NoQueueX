using NoQueue.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoQueue.Interfaces
{
    public interface InterfaceDB
    {
         Task<bool> InserisciUtente(string email, string password, string nome, string cognome);
        Task<bool> UpdateUtente(Utente utente);
        Task<IList<Utente>> ReadUtenti();
        void GetDatabase();
    }

    public class Database
    {
        private static InterfaceDB firestore = DependencyService.Get<InterfaceDB>();

        public static Task<IList<Utente>> ReadUtenti()
        {
            return firestore.ReadUtenti();
        }

        public static Task<bool> UpdateUtente(Utente utente)
        {
            return firestore.UpdateUtente(utente);
        }
    }
}
