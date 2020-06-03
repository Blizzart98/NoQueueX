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
        private readonly ISharedPreferences mSharedPrefs;
        private readonly ISharedPreferencesEditor mPrefsEditor;
        private readonly Context mContext;

        private static readonly String PREFERENCE_ACCESS_KEY = "PREFERENCE_ACCESS_KEY";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void SaveAccessKey(bool key)
        {
            mPrefsEditor.PutBoolean(PREFERENCE_ACCESS_KEY, key);
            mPrefsEditor.Commit();
        }

        public bool GetAccessKey()
        {
            return mSharedPrefs.GetBoolean(PREFERENCE_ACCESS_KEY, false);
        }

        public void SaveAccessEmail(string key)
        {
            mPrefsEditor.PutString("Email", key);
            mPrefsEditor.Commit();
        }

        public void SaveAccessPass(string key)
        {
            mPrefsEditor.PutString("pass", key);
            mPrefsEditor.Commit();
        }

        public string GetAccessPass()
        {
            return mSharedPrefs.GetString("pass", "");
        }

        public string GetAccessEmail()
        {
            return mSharedPrefs.GetString("Email", "");
        }

    }
}