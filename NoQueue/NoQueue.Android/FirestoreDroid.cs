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
using Firebase;
using Firebase.Firestore;
using Java.Util;
using NoQueue.Entities;
using NoQueue.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoQueue.Droid.FirestoreDroid))]
namespace NoQueue.Droid
{
    public class FirestoreDroid : InterfaceDB
    {
        //public IntPtr Handle => throw new NotImplementedException();
        List<Utente> utenti;
        bool hasReadSubscriptions = false;
        FirebaseFirestore db;
        Context context = Android.App.Application.Context;

        public FirestoreDroid()
        {
            utenti = new List<Utente>();
        }

      
        public  Task<bool> InserisciUtente(string email, string password, string nome, string cognome)
        {
            
                HashMap map = new HashMap();
                map.Put("Nome", nome);
                map.Put("Cognome", cognome);
                map.Put("Email", email);
                map.Put("Password", password);

                GetDatabase();
            try
            {
                DocumentReference docRef = db.Collection("new").Document();
                 docRef.Set(map);
                return System.Threading.Tasks.Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return System.Threading.Tasks.Task.FromResult(false);
            }
        }

        public async Task<IList<Utente>> ReadUtenti()
        {/*
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
            */
            return utenti;
        }

            public Task<bool> UpdateUtente(Utente utente)
        {
            throw new NotImplementedException();
        }

     
        public void OnComplete(Androi d.Gms.Tasks.Task task)
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

        public void GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(Android.App.Application.Context);
            FirebaseFirestore database;
            if(app == null)
            {
                var options = new FirebaseOptions.Builder()
                .SetProjectId("noqueue-847af")
                .SetApplicationId("noqueue-847af")
                .SetApiKey("AIzaSyBWK1vhM6BKN1flO2PGRcTB-p7XR7xT41g")
                .SetDatabaseUrl("https://noqueue-847af.firebaseio.com")
                .SetStorageBucket("noqueue-847af.appspot.com")
                .Build();

                app = FirebaseApp.InitializeApp(Android.App.Application.Context, options);
                database = FirebaseFirestore.GetInstance(app);
            }
            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }
            db = database;

           /* const string AppName = "SampleAppName";

            var appTest = FirebaseApp.Instance;
            var options = new FirebaseOptions.Builder()
                .SetProjectId("noqueue-847af")
                .SetApplicationId("noqueue-847af")
                .SetApiKey("AIzaSyBWK1vhM6BKN1flO2PGRcTB-p7XR7xT41g")
                .SetDatabaseUrl("https://noqueue-847af.firebaseio.com")
                .SetStorageBucket("noqueue-847af.appspot.com")
                .Build();
           var appl = FirebaseApp.InitializeApp(context, options);
            database = FirebaseFirestore.GetInstance(appTest);*/



            /*var baseOptions = Firebase.FirebaseOptions.FromResource(context);
            var options = new Firebase.FirebaseOptions.Builder(baseOptions).SetProjectId("noqueue-847af")
                .SetApplicationId("noqueue-847af")
                .SetApiKey("AIzaSyBWK1vhM6BKN1flO2PGRcTB-p7XR7xT41g")
                .SetDatabaseUrl("https://noqueue-847af.firebaseio.com")
                .SetStorageBucket("noqueue-847af.appspot.com")
                .Build();
            var app = Firebase.FirebaseApp.InitializeApp(context, options, AppName);
            database = FirebaseFirestore.GetInstance(app);*/
        }
    }
}