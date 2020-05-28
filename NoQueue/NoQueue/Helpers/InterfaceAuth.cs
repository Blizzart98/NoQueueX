using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoQueue
{
   public interface InterfaceAuth
    {
        Task<bool> AuthenticateUser(string email, string password);
       
        Task<bool> RegisterUser(string email, string password);

        bool IsAuthenticated();
        string GetCurrentUserId();

    }

    public class Auth
    {
        private static InterfaceAuth auth = DependencyService.Get<InterfaceAuth>();

        public static async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }
        }

        public static async Task<bool> AuthenticateUser(string email, string password)
        {
            try
            {
                return await auth.AuthenticateUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }
        }

        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }

        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }
}
