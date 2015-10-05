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
//using Xamarin.Facebook;

namespace AutomaTech
{
	
	[Activity (Label = "TourPlus+", MainLauncher = true, Icon = "@drawable/icon")]			
	public class LoginActivity : Activity
	{
		//Declaring variables that are accessed in functions
		GlobalVars g = GlobalVars.getInstance();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//This Function will Allow users to Register Via FaceBook
			//FacebookSdk.SdkInitialize(this.ApplicationContext); 

			//Setting layout
			SetContentView(Resource.Layout.LoginLayout);

			//Opening database connection for testing login info
			DBRepository dbr = new DBRepository ();
			var result = dbr.CreateDB ();						//returns testing string
			Toast.MakeText (this, result.ToString(), ToastLength.Short).Show ();		//displaying testing value

			Button tempLogin = FindViewById<Button> (Resource.Id.tempLogin);
			tempLogin.Click += TempLogin_Click;
		}
		void TempLogin_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

