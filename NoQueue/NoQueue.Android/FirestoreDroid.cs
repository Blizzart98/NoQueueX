using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using NoQueue.Entities;
using NoQueue.Interfaces;

namespace NoQueue.Droid
{
    public class FirestoreDroid : Java.Lang.Object, InterfaceDB, IOnCompleteListener
    {
        //public IntPtr Handle => throw new NotImplementedException();
        List<Utente> utenti;
        bool hasReadSubscriptions = false;

        public FirestoreDroid()
        {
            utenti = new List<Utente>();
        }

      
        public bool InserisciUtente(Utente utente)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("utenti");
                var utentiDocument = new Dictionary<string, Java.Lang.Object>
            {
                {"uid", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid},
                {"cognome", utente.Cognome},
                {"name", utente.Nome},
                {"email", utente.Email},
                {"password", utente.Password}
            };
                collection.Add(utentiDocument);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<Utente>> ReadUtenti()
        {
            hasReadSubscriptions = false;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("utenti");
            var query = collection.WhereEqualTo("uid", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
            query.Get().AddOnCompleteListener(this);

            for (int i = 0; i < 25; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (hasReadSubscriptions)
                    break;
            }

            return utenti;
        }

        public Task<bool> UpdateUtente(Utente utente)
        {
            throw new NotImplementedException();
        }

     
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;

                utenti.Clear();
                foreach (var doc in documents.Documents)
                {
                    Utente utente = new Utente
                    {

                        Nome = doc.Get("name").ToString(),
                        Cognome = doc.Get("Cognome").ToString(),
                        UserId = doc.Get("uid").ToString(),
                        Email = doc.Get("email").ToString(),
                        Password = doc.Get("password").ToString()
                        
                    };

                    utenti.Add(utente);
                }
            }
            else
            {
                utenti.Clear();
            }
            hasReadSubscriptions = true;
        }
    }
}