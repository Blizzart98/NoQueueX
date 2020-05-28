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
         bool InserisciUtente(Utente utente);
        Task<bool> UpdateUtente(Utente utente);
        Task<IList<Utente>> ReadUtenti();
    }

    public class Database
    {
        private static InterfaceDB firestore = DependencyService.Get<InterfaceDB>();

        public static bool InserisciUtente(Utente utente)
        {
            return firestore.InserisciUtente(utente);
        }

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
