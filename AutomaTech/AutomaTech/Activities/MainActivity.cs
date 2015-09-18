using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			Button loginScreen = FindViewById<Button> (Resource.Id.btnLoginScreen);
			loginScreen.Click += LoginScreen_Click;

			Button eventScreen = FindViewById<Button> (Resource.Id.btnEventScreen);
			eventScreen.Click += EventScreen_Click;

			Button bandScreen = FindViewById<Button> (Resource.Id.btnBandScreen);
			bandScreen.Click += BandScreen_Click;

			Button tourScreen = FindViewById<Button> (Resource.Id.btnTourScreen);
			tourScreen.Click += TourScreen_Click;


		
		}

		void TourScreen_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(TourMainActivity));
		}

		void BandScreen_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}

		void EventScreen_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventMainActivity));
		}

		void LoginScreen_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(LoginActivity));
		}
	}
}


