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
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class AccountMainActivity : Activity
	{
		private TextView mTxtFirstName;
		public myProfileTracker mProfileTracker;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.AccountMainLayout);

			//mProfileTracker.mOnProfileChanged += mProfileTracker_mOnProfileChanged;

			mTxtFirstName = FindViewById<TextView> (Resource.Id.txtFirstName);
			Button loadAccount = FindViewById<Button>(Resource.Id.btnLoadAccount);
			
			//loadAccount.Click += LoadAccount_Click;

			Button homeFromAccount = FindViewById<Button> (Resource.Id.homeFromAccount);
			homeFromAccount.Click += HomeFromAccount_Click;
		}

		void mProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
		{
			mTxtFirstName.Text = e.mProfile.FirstName;
		}

		public class myProfileTracker : ProfileTracker
		{
			public event EventHandler<OnProfileChangedEventArgs> mOnProfileChanged;

			protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile)
			{
				if (mOnProfileChanged != null) 
				{
					mOnProfileChanged.Invoke (this, new OnProfileChangedEventArgs (newProfile));
				}
			}
		}

		//This function loads the text fields with user information
		//UNDER CONSTRUCTION !! GLOBAL VARIABLE FOR DETERMINING USER ID IS NECESSARY
		void LoadAccount_Click (object sender, EventArgs e)
		{
			DBRepository dbr = new DBRepository ();
			var result = dbr.GetAccount();
			Toast.MakeText (this, result, ToastLength.Short).Show ();

			EditText ID = FindViewById<EditText> (Resource.Id.txtGetId);
			//ID.SetText = dbr.GetAccount(UGHHHHH)
			EditText user = FindViewById<EditText> (Resource.Id.txtGetUsername);
			EditText pass = FindViewById<EditText> (Resource.Id.txtGetPassword);
			EditText access = FindViewById<EditText> (Resource.Id.txtGetAccess);
		}
		void HomeFromAccount_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

