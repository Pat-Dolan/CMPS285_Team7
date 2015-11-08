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
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{

	[Activity (Label = "TourPlus+", MainLauncher = true, Icon = "@drawable/Icon")]			
	public class LoginActivity : Activity, IFacebookCallback
	{
		//Declaring variables that are accessed in functions
		GlobalVars g = GlobalVars.getInstance();
		GlobalVariables GUser = GlobalVariables.getInstance();
		private ICallbackManager mCallBackManager;
		public myProfileTracker mProfileTracker;
		int nCount;

		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
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
			GUser.setUserId (120195718336160);
			GUser.setDefaultBandId (1);
			GUser.setAccessLevel (1);
			//Should be from Facebook, but is not necessary
			GUser.setUserName ("John Smith");
			GUser.setManagerId (120195718336160);
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
			bool found = false;
			LoginResult loginResult = result as LoginResult; 
			Console.WriteLine(loginResult.AccessToken.UserId); //Token is the Unique Key for UserFacebooks
			long thingy = Int64.Parse(loginResult.AccessToken.UserId);
			GUser.setUserId(thingy);
			string thing;
			//if(loginResult.AccessToken.UserId == GUser.getUserId())
			//	thing ="Good";
			//else
			//	 thing = "Bad";
			//Toast.MakeText(this, thing, ToastLength.Short).Show();
			

			found = loadUser ();
			//if not found, begin new registration


			if(found == true){
				StartActivity (typeof(MainActivity));
			}
			else{
				GUser.setDefaultBandId (0);
				GUser.setBandId (0);
				GUser.setManagerId (0);

				StartActivity (typeof(RegisterMainActivity));		//New User
			}
		}
		public bool loadUser ()
		{
			//################################################
			// EXPERIMENTAL DATABASE TEST  	FOR INSERTING A NEW USER
			//###############################################
			/*
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("INSERT INTO userList (id, userId, userName, access, managerId, visible, defaultBandId, confirmed) " + 
												"VALUES (@Id, @UserId, @UserName,@Access, @ManagerId, @Visible, @DefaultBandId, @Confirm)");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;
				cmd.Parameters.AddWithValue ("@Id", 1);
				cmd.Parameters.AddWithValue ("@UserId", GUser.getUserId());
				cmd.Parameters.AddWithValue ("@UserName", "John Smith");
				cmd.Parameters.AddWithValue ("@Access", 1);
				cmd.Parameters.AddWithValue ("@ManagerId", GUser.getUserName());
				cmd.Parameters.AddWithValue ("@Visible", 1); 
				cmd.Parameters.AddWithValue ("@DefaultBandId", 1); 
				cmd.Parameters.AddWithValue ("@Confirm", 1); 
				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}
string didit = "Found";
Toast.MakeText(this, didit, ToastLength.Short).Show();

*/
			//##############################################
			nCount = 0;
			bool found = false;
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetTitle = " SELECT (userId), (userName), (access), (visible),(defaultBandId),(confirmed) " +
						" FROM userList ";
					dbcmd.CommandText = sqlGetTitle;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						found = false;
						while ((reader.Read ()) && (found == false)) 
						{
							long userId = (long)reader ["userId"];
							//string userId = (string)reader ["userId"];
							string userName = (string)reader ["userName"];
							int access = (int)reader ["access"];
							int visible = (int)reader ["visible"];
							int defaultBandId = (int)reader ["defaultBandId"];
							int confirm = (int)reader ["confirmed"];

							if ((userId == GUser.getUserId()) && (visible == 1)) {
							found = true;
								GUser.setUserId (userId);
								GUser.setDefaultBandId (defaultBandId);
								GUser.setAccessLevel (access);
								//Should be from Facebook, but can be from database
								GUser.setUserName (userName);
								GUser.setConfirm (confirm);
							
							}
							nCount++;
						}
						reader.Close ();
						dbcon.Close ();
						GUser.setUserTotal (nCount);
					}
				}
			}
			return found;
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

