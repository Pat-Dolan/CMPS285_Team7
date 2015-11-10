using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]

	public class MainActivity : Activity
	{
		GlobalVars g = GlobalVars.getInstance();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		

			// Setting layout
			SetContentView (Resource.Layout.Main);

			//Adding layout components


			Button bandScreen = FindViewById<Button> (Resource.Id.btnBandScreen);
			bandScreen.Click += BandScreen_Click;

			Button tourScreen = FindViewById<Button> (Resource.Id.btnTourScreen);
			tourScreen.Click += TourScreen_Click;

			Button myAccount = FindViewById<Button> (Resource.Id.btnAccount);
			myAccount.Click += MyAccount_Click;

			//Test components
			Button logoutScreen = FindViewById<Button> (Resource.Id.btnLogoutScreen);
			logoutScreen.Click += LogoutScreen_Click;
		}

		//For all events: Starting corresponding activity
		void DBAccess_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(DBAccessActivity));
		}

		void MyAccount_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(AccountMainActivity));
		}

		void TourScreen_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(TourMainActivity));
		}

		void BandScreen_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}


		void LogoutScreen_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(LoginActivity));
		}
	}
}


