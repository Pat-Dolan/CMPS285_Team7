
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AutomaTech
{
	[Activity (Label = "AccountMainActivity")]			
	public class AccountMainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			Button loadAccount = FindViewById<Button>(Resource.Id.btnLoadAccount);
			//loadAccount.Click += LoadAccount_Click;

		}

		void LoadAccount_Click (object sender, EventArgs e)
		{
			DBRepository dbr = new DBRepository ();
			var result = dbr.GetAccount();
			Toast.MakeText (this, result, ToastLength.Short).Show ();

			EditText ID = FindViewById<EditText> (Resource.Id.txtGetId);
			//ID.SetText = dbr.GetAccount(
			EditText user = FindViewById<EditText> (Resource.Id.txtGetUsername);
			EditText pass = FindViewById<EditText> (Resource.Id.txtGetPassword);
			EditText access = FindViewById<EditText> (Resource.Id.txtGetAccess);
		}
	}
}

