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
		Button login;
		Button register;
		int LoginId;		
		bool validLogin;
		string display;
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
			login = FindViewById<Button> (Resource.Id.btnLogin);
			login.Click += Login_Click;

			register = FindViewById<Button> (Resource.Id.btnRegisterScreen);
			register.Click += Register_Click;
		}


		//This function sends the user to the registration page
		void Register_Click (object sender, EventArgs e)
		{
			
			StartActivity (typeof(RegisterMainActivity));
		}

		//This function checks the information entered with the information in the database
		void Login_Click (object sender, EventArgs e)
		{
			string name = FindViewById<EditText> (Resource.Id.txtLoginName).Text.ToString();
			string pass = FindViewById<EditText>(Resource.Id.txtLoginPassword).Text.ToString();

			LoginId = verifyLogin (name, pass);

			//If valid
			if (validLogin) 
			{
				DBRepository dbr = new DBRepository ();
				display = dbr.DisplayUserById (LoginId);		//returns testing string
				Toast.MakeText (this, display, ToastLength.Short).Show ();		//displays testing string
				g.setTest(LoginId);
				//initial start of the Main activity
				StartActivity (typeof(MainActivity));
			} 
			//If not valid
			else 
			{
				display = "Username or Password not valid.";
				Toast.MakeText (this, display, ToastLength.Short).Show ();
			
			}
		}
			

		//This function tests the information entered into the login fields, and returns the user Id
		//****May use this function in DBRepository to send user info to each activity page****
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

