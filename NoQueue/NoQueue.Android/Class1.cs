using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NoQueue.Droid
{
    public class AppPreferences
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private static String PREFERENCE_ACCESS_KEY = "PREFERENCE_ACCESS_KEY";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void saveAccessKey(bool key)
        {
            mPrefsEditor.PutBoolean(PREFERENCE_ACCESS_KEY, key);
            mPrefsEditor.Commit();
        }

        public bool getAccessKey()
        {
            return mSharedPrefs.GetBoolean(PREFERENCE_ACCESS_KEY, false);
        }

        public void saveAccessEmail(string key)
        {
            mPrefsEditor.PutString("Email", key);
            mPrefsEditor.Commit();
        }

        public void saveAccessPass(string key)
        {
            mPrefsEditor.PutString("pass", key);
            mPrefsEditor.Commit();
        }

        public string getAccessPass()
        {
            return mSharedPrefs.GetString("pass", "");
        }

        public string getAccessEmail()
        {
            return mSharedPrefs.GetString("Email", "");
        }

    }
}