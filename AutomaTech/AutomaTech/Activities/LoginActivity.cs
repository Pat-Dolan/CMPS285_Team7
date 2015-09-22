﻿
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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.LoginLayout);

			Button login = FindViewById<Button> (Resource.Id.btnLogin);
			login.Click += Login_Click;

			Button register = FindViewById<Button> (Resource.Id.btnRegisterScreen);
			register.Click += Register_Click;
		}

		void Register_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(RegisterMainActivity));
		}

		void Login_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(MainActivity));
		}
	}
}
