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
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace AutomaTech
{
	
	[Activity (Label = "TourPlus+", MainLauncher = true, Icon = "@drawable/Icon")]			
	public class LoginActivity : Activity, IFacebookCallback
	{
		//Declaring variables that are accessed in functions
		GlobalVars g = GlobalVars.getInstance();

		private ICallbackManager mCallBackManager;
		public myProfileTracker mProfileTracker;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			FacebookSdk.SdkInitialize (this.ApplicationContext); //This Function will Allow users to Register Via FaceBook

			//Setting layout
			SetContentView (Resource.Layout.LoginLayout);

			mProfileTracker = new myProfileTracker ();
			mProfileTracker.StartTracking ();

			LoginButton button = FindViewById<LoginButton> (Resource.Id.login_button);

			Button tempLogin = FindViewById<Button> (Resource.Id.tempLogin);
			tempLogin.Click += TempLogin_Click;



			button.SetReadPermissions ("user_friends");
			mCallBackManager = CallbackManagerFactory.Create ();
			button.RegisterCallback (mCallBackManager, this);
		}

		void TempLogin_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}

		public void OnCancel ()
		{
			//throw new NotImplementedException ();
		}

		public void OnError (FacebookException error)
		{
			//throw new NotImplementedException ();
		}

		public void OnSuccess (Java.Lang.Object result)
		{
			LoginResult loginResult = result as LoginResult; 
			Console.WriteLine(loginResult.AccessToken.UserId); //Token is the Unique Key for UserFacebooks
			StartActivity (typeof(MainActivity));
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			mCallBackManager.OnActivityResult (requestCode, (int)resultCode, data);
		}

		protected override void OnDestroy ()
		{
			mProfileTracker.StopTracking();
			base.OnDestroy();
		}
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

	public class OnProfileChangedEventArgs : EventArgs
	{
		public Profile mProfile;
		public OnProfileChangedEventArgs (Profile profile) {mProfile = profile;}
	}

	//public static class GlobalVar
	//{
	//	public const int userProfile = AccessToken.CurrentAccessToken;
	//}
}
//
//		protected override void OnDestroy()
//		{
//			mProfileTracker.StopTracking ();
//			base.OnDestroy ();
//		}
//
//			//Opening database connection for testing login info
//			DBRepository dbr = new DBRepository ();
//			var result = dbr.CreateDB ();						//returns testing string
//			//Toast.MakeText (this, result.ToString(), ToastLength.Short).Show ();		//displaying testing value
//
			
//		}
		
//	}
//}

