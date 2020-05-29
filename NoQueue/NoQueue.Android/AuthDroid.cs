using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Firebase.Auth;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Firebase.Firestore;
using Firebase;
using Xamarin.Forms.Xaml;
using Java.Util;
using Android.Preferences;

[assembly:Dependency(typeof(NoQueue.Droid.AuthDroid))]
namespace NoQueue.Droid
{
    public class AuthDroid : InterfaceAuth
    {


        public AuthDroid()
        {

        }

        public async Task<bool> AuthenticateUser(string email, string password)
        {

            try
            {
                await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                //shared preferences
                Context mContext = Android.App.Application.Context;
                AppPreferences ap = new AppPreferences(mContext);
                ap.saveAccessKey(true);
                ap.saveAccessEmail(email);
                ap.saveAccessPass(password);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetCurrentUserId()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public bool GetSharedPreferences()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            return ap.getAccessKey();
        }

        public string GetEmail()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            return ap.getAccessEmail();
        }

        public string GetPass()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            return ap.getAccessPass();
        }
        public bool IsAuthenticated()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null;
        }

        public async Task<bool> LogOut()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            ap.saveAccessKey(false);
            ap.saveAccessPass("");
            ap.saveAccessEmail("");
            FirebaseAuth.Instance.SignOut();
            return true;

        }

        public async Task<bool> RegisterUser( string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

                var profileUpdates = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileUpdates.SetDisplayName(email);
                var build = profileUpdates.Build();
                var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
                await user.UpdateProfileAsync(build);

                //shared preferences
                Context mContext = Android.App.Application.Context;
                AppPreferences ap = new AppPreferences(mContext);
                ap.saveAccessKey(true);
                ap.saveAccessEmail(email);
                ap.saveAccessPass(password);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unknown error occurred, please try again.");
            }

        }
    }
    
}
