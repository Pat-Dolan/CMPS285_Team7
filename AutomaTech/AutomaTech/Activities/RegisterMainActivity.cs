
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
	[Activity (Label = "RegisterMainActivity")]			
	public class RegisterMainActivity : Activity
	{
		int accessLevel;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//bool valid = false;
			// Create your application here
			SetContentView(Resource.Layout.RegisterMainLayout);

			EditText username = FindViewById<EditText> (Resource.Id.txtUsername);
			EditText pass = FindViewById < EditText> (Resource.Id.txtPassword);
			EditText rePass = FindViewById<EditText> (Resource.Id.txtRePassword);
			EditText fName = FindViewById<EditText> (Resource.Id.txtFName);
			EditText lName = FindViewById<EditText> (Resource.Id.txtLName);
			EditText email = FindViewById<EditText> (Resource.Id.txtEmail);

			EditText profileName = FindViewById<EditText> (Resource.Id.txtPName);

			RadioButton accessM = FindViewById<RadioButton> (Resource.Id.rBtnManager);
			RadioButton accessA = FindViewById<RadioButton> (Resource.Id.rBtnArtist);
			accessM.Click += AccessM_Click;
			accessA.Click += AccessA_Click;
		

			Button register = FindViewById<Button>(Resource.Id.btnRegister);
			register.Click += Register_Click;
			//valid = testPass(pass.Text, rePass.Text);

		}

		void AccessA_Click (object sender, EventArgs e)
		{
			accessLevel = 1;
		}

		/*
		bool testPass (string pass, string rePass)
		{
			bool valid = true;
			if(pass == rePass)
				valid = true;
			else
				valid = false;
			
			return valid;
		}
*/
		void AccessM_Click (object sender, EventArgs e)
		{
			accessLevel = 0;
		}


		void Register_Click (object sender, EventArgs e)
		{
			//saving input into strings
			string User = FindViewById<EditText>(Resource.Id.txtUsername).Text.ToString();
			string Password = FindViewById<EditText>(Resource.Id.txtPassword).Text.ToString();
			string RePassword = FindViewById<EditText>(Resource.Id.txtRePassword).Text.ToString();
			string Fname = FindViewById<EditText>(Resource.Id.txtFName).Text.ToString();
			string Lname = FindViewById<EditText>(Resource.Id.txtLName).Text.ToString();
			string Email = FindViewById<EditText>(Resource.Id.txtEmail).Text.ToString();
			string ProfileName = FindViewById<EditText>(Resource.Id.txtPName).Text.ToString();
			//int acc = getAccess()

			bool validUser, validPass;
			validUser = verifyUsername(User);
			validPass = verifyPassword(Password, RePassword);

			//char[] errorPass = {"Passwords do not match!"};
			if (!validUser) 
				{
				//FindViewById<EditText> (Resource.Id.txtUsername).SetText (errorName);
				} 
			else if (!validPass) 
				{
				//FindViewById<EditText> (Resource.Id.txtRePassword).SetText (errorPass);
				} 
			else 
				{
					DBRepository dbr = new DBRepository ();
					var result = dbr.CreateDB (User, Password, ProfileName, accessLevel, Fname, Lname, Email);
					Toast.MakeText (this, result, ToastLength.Short).Show ();
					StartActivity (typeof(RegisterConfirmActivity));
				}
		}

		public bool verifyUsername(string name)
		{
			bool valid = false;
			DBRepository dbrUserTest = new DBRepository ();
			//var nameTestResult = dbrUserTest.TestNewName (name);
			//string errorMessage = "Username is already taken!"; 
			//if (!nameTestResult)
			//	//make error message for taken username
			//	Toast.MakeText(this, errorMessage, ToastLength.Short).Show();
			// set valid to false
			//else
				valid = true;
			return valid;
				
		}

		public bool verifyPassword(string pass, string rePass)
		{
			bool validPass = false;
			if (pass != rePass)
				validPass = false;
			else
				validPass = true;
			return validPass;
		}
	}
}

