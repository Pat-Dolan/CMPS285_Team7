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
	[Activity (Label = "LoginActivity", MainLauncher = true, Icon = "@drawable/icon")]			
	public class LoginActivity : Activity
	{
		Button login;
		Button register;
		int validLoginId;
		bool validLogin;
		string display;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			SetContentView(Resource.Layout.LoginLayout);

			login = FindViewById<Button> (Resource.Id.btnLogin);
			login.Click += Login_Click;

			register = FindViewById<Button> (Resource.Id.btnRegisterScreen);
			register.Click += Register_Click;
		}

		void Register_Click (object sender, EventArgs e)
		{
			
			StartActivity (typeof(RegisterMainActivity));
		}

		void Login_Click (object sender, EventArgs e)
		{
			string name = FindViewById<EditText> (Resource.Id.txtLoginName).Text.ToString();
			string pass = FindViewById<EditText>(Resource.Id.txtLoginPassword).Text.ToString();
			validLoginId = verifyLogin (name, pass);

			if (validLogin) {
				DBRepository dbr = new DBRepository ();
				display = dbr.DisplayUserById (validLoginId);
				Toast.MakeText (this, display, ToastLength.Short).Show ();
				StartActivity (typeof(MainActivity));
			} 
			else 
			{
				display = "Info not found";
				Toast.MakeText (this, display, ToastLength.Short).Show ();
			
			}
		}

		int verifyLogin(string name, string pass)
		{
			int id;

			DBRepository dbr = new DBRepository ();
			id = dbr.GetUserByLogin (name, pass);
			if (id == -1)
				validLogin = false;
			else
				validLogin = true;
			
			return id;
		}
			

	}
}

