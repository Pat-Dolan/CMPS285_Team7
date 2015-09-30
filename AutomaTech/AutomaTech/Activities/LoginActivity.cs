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
using Xamarin.Facebook;

namespace AutomaTech
{
	
	[Activity (Label = "LoginActivity", MainLauncher = true, Icon = "@drawable/icon")]			
	public class LoginActivity : Activity
	{
		//Declaring variables that are accessed in functions
		Button register;
		GlobalVars g = GlobalVars.getInstance();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//This Function will Allow users to Register Via FaceBook
			FacebookSdk.SdkInitialize(this.ApplicationContext); 

			//Setting layout
			SetContentView(Resource.Layout.LoginLayout);

			//Opening database connection for testing login info
			DBRepository dbr = new DBRepository ();
			var result = dbr.CreateDB ();						//returns testing string
			Toast.MakeText (this, result.ToString(), ToastLength.Short).Show ();		//displaying testing value

			//Adding event handlers to components

			register = FindViewById<Button> (Resource.Id.btnRegisterScreen);
			register.Click += Register_Click;
		}


		//This function sends the user to the registration page
		void Register_Click (object sender, EventArgs e)
		{
			
			StartActivity (typeof(RegisterMainActivity));
		}


			

		//This function tests the information entered into the login fields, and returns the user Id
		//****May use this function in DBRepository to send user info to each activity page****
			

	}
}

