using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Collections.Generic;
using System.Linq;

namespace NoQueue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        InterfaceAuth auth;
        

        public RegisterPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<InterfaceAuth>();
        }

        private async void Btn_registration_clicked(object sender, EventArgs e)
        {
            bool created = auth.RegisterWithEmailPassword(Entry_email.Text, Entry_Password.Text);
            if (created)
            {
              /*  var user = new HashMap<>();
                user.put("nome", Entry_name);
                user.put("cognome", Entry_surname);
                user.put("email", Entry_email.Text);
                user.put("password", Entry_Password.Text);

                FirebaseFirestore db = FirebaseFirestore.getInstance();
                FirebaseUser currentUser = auth.getCurrentUser();
                db.collection("utenti").document(currentUser.getEmail()).collection("Info").document(uid).set(user);*/
                await DisplayAlert("Success", "Welcome to our system. Log in to have full access", "OK");
                await Navigation.PopAsync();
                await Navigation.PushAsync(new ProfilePage());
            }
            else
            {
                await DisplayAlert("Sign Up Failed", "Something went wrong. Try again!", "OK");
            }
        }

        async void Btn_signin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

     /*   public async Task AddPerson(string email, string password)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Person() { PersonId = Guid.NewGuid(), Name = name, Phone = phone });
        }*/
    }
}