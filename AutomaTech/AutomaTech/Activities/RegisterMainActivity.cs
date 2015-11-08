﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data.SqlClient;
using System.Data;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class RegisterMainActivity : Activity
	{
		int accessLevel;	//Variable for manager or artist status
		EditText username;
		EditText pass;
		EditText rePass;
		EditText fName;
		EditText lName;
		EditText email;
		EditText profileName;
		GlobalVariables GUser = GlobalVariables.getInstance();
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			//Setting layout
			SetContentView(Resource.Layout.RegisterMainLayout);

			//Components of layout
			//username = FindViewById<EditText> (Resource.Id.txtUsername);
			//pass = FindViewById < EditText> (Resource.Id.txtPassword);
			//rePass = FindViewById<EditText> (Resource.Id.txtRePassword);
			//fName = FindViewById<EditText> (Resource.Id.txtFName);
			//lName = FindViewById<EditText> (Resource.Id.txtLName);
			//email = FindViewById<EditText> (Resource.Id.txtEmail);
			//profileName = FindViewById<EditText> (Resource.Id.txtPName);

			//Radio group components
			RadioButton accessM = FindViewById<RadioButton> (Resource.Id.rBtnManager);
			RadioButton accessA = FindViewById<RadioButton> (Resource.Id.rBtnArtist);
			accessM.Click += AccessM_Click;
			accessA.Click += AccessA_Click;
		
			//Begins registration check
			Button register = FindViewById<Button>(Resource.Id.btnRegister);
			register.Click += Register_Click;
			//valid = testPass(pass.Text, rePass.Text);
		}


		//This handler sets access level for Artists
		void AccessA_Click (object sender, EventArgs e)
		{
			GUser.setAccessLevel (1);
			GUser.setManagerId (GUser.getUserId ());
		}

		//This handler sets access level for Managers
		void AccessM_Click (object sender, EventArgs e)
		{
			GUser.setAccessLevel (0);
			GUser.setManagerId (0);
		}

		//This handler tests and saves information for a new user
		void Register_Click (object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("INSERT INTO userList (id, userId, userName, managerId, visible, access, defaultBandId, confirmed ) VALUES (@Id, @UserId, @UserName, @ManagerId, @Visible, @Default, @Confirm)");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;
				cmd.Parameters.AddWithValue ("@Id", (GUser.getUserTotal() + 1));
				cmd.Parameters.AddWithValue ("@UserId", GUser.getUserId ());
				cmd.Parameters.AddWithValue ("@UserName", ("User" +	(GUser.getUserTotal() + 1)));		//GUser.getUserName()); from FACEBOOK
				cmd.Parameters.AddWithValue ("@ManagerId", GUser.getManagerId());
				cmd.Parameters.AddWithValue ("@Visible", 1);
				cmd.Parameters.AddWithValue ("@Default", GUser.getDefaultBandId ());
				cmd.Parameters.AddWithValue ("@Confirm", 1);
				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}

			StartActivity (typeof(MainActivity));


			/*
			//Saving input into strings(access level is assigned in other event handlers)
			string User = FindViewById<EditText>(Resource.Id.txtUsername).Text.ToString();
			string Password = FindViewById<EditText>(Resource.Id.txtPassword).Text.ToString();
			string RePassword = FindViewById<EditText>(Resource.Id.txtRePassword).Text.ToString();
			string Fname = FindViewById<EditText>(Resource.Id.txtFName).Text.ToString();
			string Lname = FindViewById<EditText>(Resource.Id.txtLName).Text.ToString();
			string Email = FindViewById<EditText>(Resource.Id.txtEmail).Text.ToString();
			string ProfileName = FindViewById<EditText>(Resource.Id.txtPName).Text.ToString();

			//Testing username and password/repassword
			bool validUser, validPass;
			validUser = verifyUsername(User);
			validPass = verifyPassword(Password, RePassword);

			if (!validUser) 
				{
				string userError = "This username is already selected!";
				Toast.MakeText (this, userError, ToastLength.Short).Show ();
				} 
			else if (!validPass) 
				{
				string passwordError = "The passwords do not match.";
				Toast.MakeText (this, passwordError, ToastLength.Short).Show ();
				} 
			else 
				{
					DBRepository dbr = new DBRepository ();
					var result = dbr.CreateDB (User, Password, ProfileName, accessLevel, Fname, Lname, Email);
					Toast.MakeText (this, result, ToastLength.Short).Show ();
					StartActivity (typeof(RegisterConfirmActivity));
				}
			*/
		}

		//This function checks if the username is already taken. If yes, the function returns false. If no: true
		public bool verifyUsername(string name)
		{
			bool valid = false;

			//Creating a connection and sending the username to the database repository for testing
			DBRepository dbrUserTest = new DBRepository ();
			valid = dbrUserTest.TestUsername(name);

			return valid;
		}

		//This function tests the password and rePassword equality and returns results
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

